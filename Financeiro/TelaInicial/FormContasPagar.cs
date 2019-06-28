using System;
using FinanceiroRepository;
using Model;
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
    public partial class FormContasPagar : Form
    {
        public FormContasPagar()
        {
            InitializeComponent();
        }

        //Variavel que irá receber o id quando o usuario der double click na tabela
        int idAlterar = 0;





        // Limpa campos da tela
        private void LimpaCampos()
        {
            txtNome.Clear();
            mtxtValorConta.Clear();
            dateTimePicker1.Value = DateTime.Now;
            txtTipo.Clear();
            checkPaga.Checked = false;
        }

        //Verifica se o usuario prencheu os campos corretamente
        private bool VerificaCampos()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Digite o nome", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }

            if ((mtxtValorConta.Text == "R$        ,") || (Convert.ToDecimal(mtxtValorConta.Text.Replace("R$", "")) == 0))
            {
                MessageBox.Show("Digite o valor da conta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtValorConta.Focus();
                return false;
            }
            if (txtTipo.Text == "")
            {
                MessageBox.Show("Digite o tipo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTipo.Focus();
                return false;
            }

            return true;
        }

        // Pega os dados digitados e passa para a função para inserir no banco de dados
        private void Inserir()
        {
            bool verifica = VerificaCampos();
            if (verifica == true)
            {



                ContaPagarRepository repositorio = new ContaPagarRepository();
                ContaPagar conta = new ContaPagar();

                conta.Nome = txtNome.Text;
                conta.Valor = Convert.ToDecimal(mtxtValorConta.Text.Replace("R$", ""));
                conta.Data_Vencimento = dateTimePicker1.Value;
                conta.Tipo = txtTipo.Text;
                conta.Fechada = checkPaga.Checked;


                bool inseriu = repositorio.Inserir(conta);
                if (inseriu == true)
                {
                    MessageBox.Show("Adicionado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ocorreu um erro ao adicionar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LimpaCampos();
                AtualizarTabela();
            }
        }

        //Atualiza os regstros da tabela com o banco de dados
        private void AtualizarTabela()
        {
            dataGridView1.Rows.Clear();

            ContaPagarRepository conta = new ContaPagarRepository();
            List<ContaPagar> listaContas = new List<ContaPagar>();

            listaContas = conta.ListarTodos();
            for (int i = 0; i < listaContas.Count; i++)
            {
                ContaPagar contaRecebida = listaContas[i];
                dataGridView1.Rows.Add(new object[] { contaRecebida.Id, contaRecebida.Nome, contaRecebida.Valor, contaRecebida.Tipo, contaRecebida.Data_Vencimento });

            }
        }

        //Exclui um regstro do banco de dados
        private void Excluir()
        {
            ContaPagarRepository repository = new ContaPagarRepository();
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            bool verifica = repository.Excluir(id);
            if (verifica == true)
            {
                MessageBox.Show("Excluido com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarTabela();
            }
            else
            {
                MessageBox.Show("Não foi possivel exlcuir", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Busca pelo id selecionado quando der um double click na tabela
        private void BuscaId()
        {
            ContaPagarRepository repository = new ContaPagarRepository();
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            ContaPagar conta = repository.ObterConta(id);
            if (conta.Id != 0)
            {
                idAlterar = conta.Id;
                txtNome.Text = conta.Nome;
                mtxtValorConta.Text = conta.Valor.ToString();
                dateTimePicker1.Value = conta.Data_Vencimento;
                txtTipo.Text = conta.Tipo;
                checkPaga.Checked = conta.Fechada;

                btnAdicionar.Enabled = false;
                btnExcluir.Enabled = false;
                btnAlterar.Enabled = true;
                dataGridView1.Enabled = false;

            }
            else
            {
                MessageBox.Show("Não foi possivel buscar o registro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Altera o registro selecionado no banco de dados
        private void Alterar()
        {
            bool verifica = VerificaCampos();

            if (verifica == true)
            {
                btnAdicionar.Enabled = true;
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = true;
                dataGridView1.Enabled = true;


                ContaPagarRepository repositorio = new ContaPagarRepository();
                ContaPagar conta = new ContaPagar();

                conta.Id = idAlterar;
                conta.Nome = txtNome.Text;
                conta.Valor = Convert.ToDecimal(mtxtValorConta.Text.Replace("R$", ""));
                conta.Data_Vencimento = dateTimePicker1.Value;
                conta.Tipo = txtTipo.Text;
                conta.Fechada = checkPaga.Checked;


                bool alterou = repositorio.Alterar(conta);

                if (alterou == true)
                {
                    MessageBox.Show("Alterado com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ocorreu um erro ao Alterar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LimpaCampos();
                AtualizarTabela();


            }
        }




        // Eventos
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Inserir();

        }

        private void FormContasPagar_Load(object sender, EventArgs e)
        {
            AtualizarTabela();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja exlcuir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Excluir();

            }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BuscaId();

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Alterar();

        }

    }
}
