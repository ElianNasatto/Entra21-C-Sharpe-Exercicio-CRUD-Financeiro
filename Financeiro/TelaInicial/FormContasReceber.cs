using FinanceiroRepository;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelaInicial
{
    public partial class FormContasReceber : Form
    {
        public FormContasReceber()
        {
            InitializeComponent();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Variavel irá armazenar o id do registro quando o ususario der um doubleclick na tabela
        int idAlterar = 0;

        //Veirifica se os campos foram preenchidos corretamente antes de fazer uma operação
        private bool VerificaCampos()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Digite o nome", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNome.Focus();
                return false;
            }

            if ((mtxtValorConta.Text == "R$        ,") || (Convert.ToDecimal(mtxtValorConta.Text.Replace("R$", "")) == 0))
            {
                MessageBox.Show("Digite o valor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtxtValorConta.Focus();
                return false;
            }

            if ((mtxtValorRecebido.Text == "R$        ,") || (Convert.ToDecimal(mtxtValorRecebido.Text.Replace("R$", "")) == 0))
            {
                DialogResult result = MessageBox.Show("Você não digitou o valor recebido, deseja continuar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    mtxtValorRecebido.Focus();
                    return false;
                }
                else
                {
                    mtxtValorRecebido.Text = "000000000.00";
                }
            }

            /* if (mtxtDataPagamento.Text == "  /  /")
             {
                 DialogResult result = MessageBox.Show("Você não digitou a data do recebimento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                 mtxtDataPagamento.Focus();
                 return false;


             }*/
            

            if (Convert.ToDecimal(mtxtValorRecebido.Text.Replace("R$", "")) >= Convert.ToDecimal(mtxtValorConta.Text.Replace("R$", "")))
            {
                if (checkPaga.Checked == false)
                {


                    DialogResult result = MessageBox.Show("O valor pago é maior ou igual a conta, marcar a conta como paga?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if ((result == DialogResult.Yes) && (checkPaga.Checked == false))
                    {
                        checkPaga.Checked = true;
                    }
                }

            }
            else if (Convert.ToDecimal(mtxtValorRecebido.Text.Replace("R$", "")) <= Convert.ToDecimal(mtxtValorConta.Text.Replace("R$", "")))
            {
                DialogResult result = MessageBox.Show("O valor pago é menor ou igual a conta e a conta esta marcada como PAGA.\n\nDeseja desmarcar a conta como paga?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if ((result == DialogResult.Yes) && (checkPaga.Checked == false))
                {
                    checkPaga.Checked = false;
                }
            }


            return true;

        }

        // Metodo recebe o valor e modifica para preencher o valor corretamente na mascara
        private string PrencheMascara(string valor)
        {
            while (valor.Count() < 11)
            {
                valor = "0" + valor;
            }
            return valor;
        }

        //Busca os registros no banco de dados e preenche no datagridview
        private void AtualizarTabela()
        {
            ContaReceberRepository repositorio = new ContaReceberRepository();
            List<ContaReceber> listaConta = repositorio.Listar(0);
            dataGridView1.Rows.Clear();
            for (int i = 0; i < listaConta.Count; i++)
            {
                ContaReceber conta = listaConta[i];
                dataGridView1.Rows.Add(new object[] { conta.Id.ToString(), conta.Nome, conta.Valor.ToString(), conta.Valor_Recebido.ToString(), conta.Data_Recebimento.ToString(), conta.Fechada.ToString() });
            }
        }

        //Insere os registros no banco de dados
        private void Inserir()
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Nome = txtNome.Text;
            contaReceber.Valor = Convert.ToDecimal(mtxtValorConta.Text.Replace("R$", ""));
            contaReceber.Valor_Recebido = Convert.ToDecimal(mtxtValorRecebido.Text.Replace("R$", ""));
            contaReceber.Data_Recebimento = dateTimePicker1.Value;
            contaReceber.Fechada = checkPaga.Checked;
            bool adicionado = false;

            ContaReceberRepository repositorio = new ContaReceberRepository();
            adicionado = repositorio.Adicionar(contaReceber);
            if (adicionado == true)
            {
                MessageBox.Show("Adicionado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarTabela();
            }
            else
            {
                MessageBox.Show("Um erro ocorreu ao adicionar", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LimpaCampos();
        }

        //Exclui o resitro no banco de dados
        private void Excluir()
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja exlcuir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {


                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                ContaReceberRepository repositorio = new ContaReceberRepository();
                bool verifica = repositorio.Excluir(id);
                if (verifica == true)
                {
                    MessageBox.Show("Excluido com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarTabela();
                }
                else
                {
                    MessageBox.Show("Não foi possivel excluir", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        //Double click na tabela busca no banco as informações e preenche os campos da tela
        private void RetornaBanco()
        {
            btnAdicionar.Enabled = false;
            btnExcluir.Enabled = false;
            dataGridView1.Enabled = false;
            btnAlterar.Enabled = true;

            idAlterar = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            ContaReceberRepository repository = new ContaReceberRepository();
            List<ContaReceber> listaContas = repository.Listar(idAlterar);
            ContaReceber conta = listaContas[0];
            txtNome.Text = conta.Nome;
            mtxtValorConta.Text = PrencheMascara(conta.Valor.ToString());
            mtxtValorRecebido.Text = PrencheMascara(conta.Valor_Recebido.ToString());
            dateTimePicker1.Value = Convert.ToDateTime(conta.Data_Recebimento);
            checkPaga.Checked = conta.Fechada;
        }

        //Limpa os campos da tela
        private void LimpaCampos()
        {
            txtNome.Clear();
            mtxtValorConta.Clear();
            mtxtValorRecebido.Clear();
            dateTimePicker1.Value = DateTime.Now;
            checkPaga.Checked = false;
        }

        // Altera o registro selecionado no banco
        private void Alterar()
        {
            bool verifica = VerificaCampos();
            if (verifica == true)
            {


                ContaReceber contaReceber = new ContaReceber();
                contaReceber.Id = idAlterar;
                contaReceber.Nome = txtNome.Text;
                contaReceber.Valor = Convert.ToDecimal(mtxtValorConta.Text.Replace("R$", ""));
                contaReceber.Valor_Recebido = Convert.ToDecimal(mtxtValorRecebido.Text.Replace("R$", ""));
                contaReceber.Data_Recebimento = dateTimePicker1.Value;
                contaReceber.Fechada = checkPaga.Checked;
                bool adicionado = false;

                ContaReceberRepository repositorio = new ContaReceberRepository();
                adicionado = repositorio.Alterar(contaReceber);
                if (adicionado == true)
                {
                    MessageBox.Show("Alterado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarTabela();
                }
                else
                {
                    MessageBox.Show("Um erro ocorreu ao alterar", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LimpaCampos();
            }
        }




        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            bool verifica = VerificaCampos();
            if (verifica == true)
            {
                Inserir();
            }
        }

        private void FormContasReceber_Load(object sender, EventArgs e)
        {
            AtualizarTabela();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RetornaBanco();
        }


        private void btnAlterar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja alterar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Alterar();
                btnExcluir.Enabled = true;
                btnAlterar.Enabled = false;
                btnAdicionar.Enabled = true;
                dataGridView1.Enabled = true;
                LimpaCampos();
            }
            else
            {
                btnExcluir.Enabled = true;
                btnAlterar.Enabled = false;
                btnAdicionar.Enabled = true;
                dataGridView1.Enabled = true;
                LimpaCampos();
            }

        }
    }
}
