using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ContaPagarRepository
{
    public class ContasReceberRepository
    {
        string caminhoConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=T:\Documentos\GitHub\C-Sharpe-Entra21-Exercicio-CRUD-Financeiro\Financeiro\BD_Financeiro.mdf;Integrated Security=True;Connect Timeout=30";

        public bool Adicionar(ContasReceber contasReceber)
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
            comando.CommandText = "INSER INTO ContasReceber VALUES (@NOME,@VALOR,@VALOR_RECEBIDO,@DATA,@FECHADA";
            comando.Parameters.AddWithValue("@NOME",contasReceber.Nome);
            comando.Parameters.AddWithValue("@VALOR",contasReceber.Valor);
            comando.Parameters.AddWithValue("@VALOR_RECEBIDO",contasReceber.Valor_Recebido);
            comando.Parameters.AddWithValue("@DATA",contasReceber.Data_Recebimento);
            comando.Parameters.AddWithValue("@FECHADA",contasReceber.Fechada);

                comando.ExecuteNonQuery();
            try
            {
                conexao.Close();
                return true;
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro.ToString());
                conexao.Close();
                return false;
            }

        }
    }
}
