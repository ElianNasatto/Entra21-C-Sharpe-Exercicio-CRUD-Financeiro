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

        private void LimpaCampos()
        {
            txtNome.Clear();
            mtxtValorConta.Clear();
            dateTimePicker1.Value = DateTime.Now;
            txtTipo.Clear();
            checkPaga.Checked = false;
        }

        private bool VerificaCampos()
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Digite o nome","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtNome.Focus();
                return false;
            }

            if ((mtxtValorConta.Text == "R$        ,")|| (Convert.ToDecimal(mtxtValorConta.Text.Replace("R$","")) == 0))
            {
                MessageBox.Show("Digite o valor da conta","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                mtxtValorConta.Focus();
                return false;
            }
            if (txtTipo.Text == "")
            {
                MessageBox.Show("Digite o tipo","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtTipo.Focus();
                return false;
            }

            return true;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            ContaPagarRepository repositorio = new ContaPagarRepository();

        }
    }
}
