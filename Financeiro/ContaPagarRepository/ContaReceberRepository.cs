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
    public class ContaReceberRepository
    {
        //Caminho do arquivo do banco de dados
        string caminhoConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=T:\Documentos\GitHub\C-Sharpe-Entra21-Exercicio-CRUD-Financeiro\BD_Financeiro.mdf;Integrated Security=True;Connect Timeout=30";

        //Adiciona um registro no banco
        public bool Adicionar(ContaReceber contaReceber)
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
            comando.CommandText = "INSERT INTO Contas_Receber VALUES (@NOME,@VALOR,@VALOR_RECEBIDO,@DATA,@FECHADA)";
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            comando.Parameters.AddWithValue("@VALOR_RECEBIDO", contaReceber.Valor_Recebido);
            comando.Parameters.AddWithValue("@DATA", contaReceber.Data_Recebimento);
            comando.Parameters.AddWithValue("@FECHADA", contaReceber.Fechada);

            try
            {
                comando.ExecuteNonQuery();
                conexao.Close();
                return true;
            }
            catch (Exception)
            {
                conexao.Close();
                return false;
            }

        }

        //Busca no banco um registro e retorna uma lista com o resultado
        public List<ContaReceber> Listar(int id)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = caminhoConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            if (id == 0)
            {
            comando.CommandText = "SELECT * FROM Contas_Receber";

            }
            else
            {
                comando.CommandText = "SELECT * FROM Contas_Receber WHERE id = @ID";
                comando.Parameters.AddWithValue("@ID", id);
            }
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            conexao.Close();

            List<ContaReceber> listaContas = new List<ContaReceber>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                ContaReceber conta = new ContaReceber();
                conta.Id = Convert.ToInt32(linha["id"]);
                conta.Nome = linha["nome"].ToString();
                conta.Valor = Convert.ToDecimal(linha["valor"]);
                conta.Valor_Recebido = Convert.ToDecimal(linha["valor_recebido"]);
                conta.Data_Recebimento = Convert.ToDateTime(linha["data_recebimento"]);
                conta.Fechada = Convert.ToBoolean(linha["fechada"]);

                listaContas.Add(conta);
            }
            return listaContas;


        }

        //Exlcui um registro no banco de dados
        public bool Excluir(int id)
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

            comando.CommandText = "DELETE FROM Contas_Receber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            try
            {
                comando.ExecuteNonQuery();
                conexao.Close();
                return true;
            }
            catch (Exception)
            {
                conexao.Close();
                return false;
            }


        }

        //Altera o registro no banco de dados
        public bool Alterar(ContaReceber conta)
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
            comando.CommandText = "UPDATE Contas_Receber SET nome = @NOME, valor = @VALOR, valor_recebido = @VALOR_RECEBIDO, data_recebimento = @DATA, fechada = @FECHADA WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", conta.Id);
            comando.Parameters.AddWithValue("@NOME", conta.Nome);
            comando.Parameters.AddWithValue("@VALOR", conta.Valor);
            comando.Parameters.AddWithValue("@VALOR_RECEBIDO", conta.Valor_Recebido);
            comando.Parameters.AddWithValue("@DATA", conta.Data_Recebimento);
            comando.Parameters.AddWithValue("@FECHADA", conta.Fechada);

            try
            {
                comando.ExecuteNonQuery();
                conexao.Close();
                return true;
            }
            catch (Exception)
            {
                conexao.Close();
                return false;
            }
        }
    }
}
