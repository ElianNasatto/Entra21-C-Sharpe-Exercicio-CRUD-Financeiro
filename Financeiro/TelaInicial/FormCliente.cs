using Model;
using System;
using FinanceiroRepository;
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
    public partial class FormCliente : Form
    {
        ClienteRepository repository = new ClienteRepository();

        public FormCliente()
        {
            InitializeComponent();
        }

        int idAlterar = 0;

        private void Inserir()
        {
            bool verificado = VerificaCampos();
            if (verificado == true)
            {
                Cliente cliente = new Cliente();
                cliente.Nome = txtNome.Text;
                cliente.CPF = mtxtCPF.Text;
                cliente.Data_Nascimento = Convert.ToDateTime(dateTimePicker1.Value);
                cliente.RG = mtxtRG.Text;

                bool inserido = repository.Inserir(cliente);
                if (inserido == true)
                {
                    MessageBox.Show("Adicionado com sucesso", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizaTabela();
                    LimpaCampos();

                }
                else
                {
                    MessageBox.Show("Não foi possivel adicionar", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LimpaCampos();
                }


            }
        }

        public bool VerificaCampos()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Digite o nome", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (mtxtCPF.Text == "   -   -   -  -")
            {
                MessageBox.Show("Digite o cpf", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (mtxtRG.Text == "  .   .   .-")
            {
                MessageBox.Show("Digite o rg", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return true;


        }

        private void AtualizaTabela()
        {
            dataGridView1.Rows.Clear();
            List<Cliente> listaClientes = repository.Listar();
            for (int i = 0; i < listaClientes.Count; i++)
            {
                Cliente cliente = listaClientes[i];
                dataGridView1.Rows.Add(new object[] { cliente.Id, cliente.Nome, cliente.CPF, cliente.Data_Nascimento, cliente.RG });

            }
        }

        private void LimpaCampos()
        {
            txtNome.Text = "";
            mtxtCPF.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            mtxtRG.Text = "";
        }

        private void Excluir()
        {
            DialogResult result = MessageBox.Show("Deseja exlcuir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {


                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                bool excluido = repository.Deletar(id);
                if (excluido == true)
                {
                    MessageBox.Show("Deletado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizaTabela();
                }
                else
                {
                    MessageBox.Show("Não foi possivel exlcuir", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            Inserir();
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            AtualizaTabela();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Excluir();

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            Cliente cliente = repository.BuscarPeloId(id);
            idAlterar = cliente.Id;
            txtNome.Text = cliente.Nome;
            mtxtCPF.Text = cliente.CPF;
            dateTimePicker1.Value = cliente.Data_Nascimento;
            mtxtRG.Text = cliente.RG;

            btnAdicionar.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = false;
            dataGridView1.Enabled = false;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            bool verificado = VerificaCampos();
            if (verificado == true)
            {
                Cliente cliente = new Cliente();
                cliente.Id = idAlterar;
                cliente.Nome = txtNome.Text;
                cliente.CPF = mtxtCPF.Text;
                cliente.Data_Nascimento = Convert.ToDateTime(dateTimePicker1.Value);
                cliente.RG = mtxtRG.Text;

                bool alterado = repository.Alterar(cliente);
                if (alterado == true)
                {
                    MessageBox.Show("Registro alterado com sucesso","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    btnAdicionar.Enabled = true;
                    btnAlterar.Enabled = false;
                    btnExcluir.Enabled = true;
                    dataGridView1.Enabled = true;
                    LimpaCampos();
                    AtualizaTabela();
                }
                else
                {
                    MessageBox.Show("Um erro ocorreu e não foi possivel alterar","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    LimpaCampos();
                    btnAdicionar.Enabled = true;
                    btnAlterar.Enabled = false;
                    btnExcluir.Enabled = true;
                    dataGridView1.Enabled = true;
                }

            }
        }
    }
}
