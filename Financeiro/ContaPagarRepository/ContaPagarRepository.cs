using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContaPagarRepository
{
    class ContaPagarRepository
    {
        string caminhoConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=T:\Documentos\GitHub\C-Sharpe-Entra21-Exercicio-CRUD-Financeiro\BD_Financeiro.mdf;Integrated Security=True;Connect Timeout=30";


        public List<ContaPagar> Listar(int id)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = caminhoConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            if (id == 0)
            {
                comando.CommandText = "SELECT * FROM Contas_Pagar";
            }

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            conexao.Close();
            List<ContaPagar> listaContas = new List<ContaPagar>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                ContaPagar conta = new ContaPagar();
                conta.Id = Convert.ToInt32(linha["id"]);
                conta.Nome = linha["nome"].ToString();
                conta.Valor = Convert.ToDecimal(linha["valor"]);
                conta.Data_Vencimento = Convert.ToDateTime(linha["data_Vencimento"]);
                conta.Fechada = Convert.ToBoolean(linha["fechada"]);
                listaContas.Add(conta);
            }
            return listaContas;


        }

        public bool Inserir(ContaPagar conta)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = caminhoConexao;
            try
            {
                conexao.Open();
            }
            catch (Exception)
            {
                return false;
            }


            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "INSERT INTO Contas_Pagar VALUES @NOME,@VALOR,@DATA_VENCIMENTO,@TIPO,@FECHADA";
            comando.Parameters.AddWithValue("@NOME", conta.Nome);
            comando.Parameters.AddWithValue("@VALOR", conta.Valor);
            comando.Parameters.AddWithValue("@DATA_VENCIMENTO", conta.Data_Vencimento);
            comando.Parameters.AddWithValue("@TIPO", conta.Tipo);
            comando.Parameters.AddWithValue("@FECHADA", conta.Fechada);
            try
            {
                comando.ExecuteNonQuery();
                conexao.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
