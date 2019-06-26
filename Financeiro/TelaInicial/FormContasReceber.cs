using ContaPagarRepository;
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

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            ContasReceber contaReceber = new ContasReceber();
            contaReceber.Nome = txtNome.Text;
            contaReceber.Valor = Convert.ToDecimal(mtxtValorConta.Text.Replace("R$", ""));
            contaReceber.Valor_Recebido = Convert.ToDecimal(mtxtValorRecebido.Text.Replace("R$", ""));
            contaReceber.Data_Recebimento =Convert.ToDateTime(mtxtDataPagamento.Text);
            bool verifica = false;

            ContasReceberRepository repositorio = new ContasReceberRepository();
            verifica = repositorio.Adicionar(contaReceber);
            if (verifica == true)
            {
                MessageBox.Show("Adicionado com sucesso","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Um erro ocorreu ao adicionar", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
