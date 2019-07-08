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


        public bool VerificaCampos()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Digite o nome","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            if (mtxtCPF.Text == "   -   -   -  -")
            {
                MessageBox.Show("Digite o cpf","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }

            if (mtxtRG.Text == "  .   .   .-")
            {
                MessageBox.Show("Digite o rg","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;

            }
            return true;


        }

        private void AtualizaTabela()
        {
            List<Cliente> listaClientes = repository.Listar();
            for (int i = 0; i < listaClientes.Count; i++)
            {
                dataGridView1.Rows.Add(listaClientes[i]);

            }
        }


        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            bool verificado = VerificaCampos();
            if (verificado == true)
            {
                Cliente cliente = new Cliente();
                cliente.Nome = txtNome.Text;
                cliente.CPF = mtxtCPF.Text;
                cliente.Data_Nascimento = Convert.ToDateTime(dateTimePicker1.Value);
                cliente.RG = mtxtRG.Text;

                repository.Inserir(cliente);
                AtualizaTabela();

            }
        }

        private void FormCliente_Load(object sender, EventArgs e)
        {
            AtualizaTabela();
        }
    }
}
