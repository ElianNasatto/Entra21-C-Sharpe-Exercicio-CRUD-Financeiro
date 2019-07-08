using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceiroRepository
{
    public class ContaPagarRepository
    {
        string caminhoConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\elian\Documents\GitHub\C-Sharpe-Entra21-Exercicio-CRUD-Financeiro\Financeiro\Model\BD_Financeiro.mdf;Integrated Security=True;Connect Timeout=30";

        // Lista todas as contas
        public List<ContaPagar> ListarTodos()
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = caminhoConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;

            comando.CommandText = "SELECT * FROM Contas_Pagar";


            DataTable tabela = new DataTable();
            try
            {
                tabela.Load(comando.ExecuteReader());

            }
            catch (Exception)
            {
                return null;
            }
            conexao.Close();
            List<ContaPagar> listaContas = new List<ContaPagar>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                ContaPagar conta = new ContaPagar();
                conta.Id = Convert.ToInt32(linha["Id"]);
                conta.Nome = linha["nome"].ToString();
                conta.Valor = Convert.ToDecimal(linha["valor"]);
                conta.Tipo = linha["tipo"].ToString();
                conta.Data_Vencimento = Convert.ToDateTime(linha["data_Vencimento"]);
                conta.Fechada = Convert.ToBoolean(linha["fechada"]);
                listaContas.Add(conta);
            }
            return listaContas;


        }

        // Insere no banco de dados
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
            comando.CommandText = "INSERT INTO Contas_Pagar (nome,valor,data_vencimento, tipo, fechada) VALUES (@NOME,@VALOR,@DATA_VENCIMENTO,@TIPO,@FECHADA)";
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
                conexao.Close();
                return false;
            }
        }

        //Recebe o ID do regsitro que deve ser apagado no banco
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
            comando.CommandText = "DELETE FROM Contas_Pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
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

        //Retorna a conta selecionada pelo usuario
        public ContaPagar ObterConta(int id)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = caminhoConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;

            comando.CommandText = "SELECT * FROM Contas_Pagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);


            DataTable tabela = new DataTable();
            try
            {
                tabela.Load(comando.ExecuteReader());
                conexao.Close();
            }
            catch (Exception)
            {

                conexao.Close();
                return null;
            }
            DataRow linha = tabela.Rows[0];
            ContaPagar conta = new ContaPagar();
            conta.Id =Convert.ToInt32(linha["id"]);
            conta.Nome = linha["nome"].ToString();
            conta.Valor =Convert.ToDecimal(linha["valor"]);
            conta.Data_Vencimento = Convert.ToDateTime(linha["data_vencimento"]);
            conta.Tipo = linha["tipo"].ToString();
            conta.Fechada = Convert.ToBoolean(linha["fechada"]);
            return conta;
        }
        
        //Aletera o resgistro no banco de dados
        public bool Alterar(ContaPagar conta)
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
            comando.CommandText = "UPDATE Contas_Pagar SET nome = @NOME, valor = @VALOR, data_vencimento = @DATA_VENCIMENTO, tipo = @TIPO, fechada = @FECHADA WHERE id = @ID";

            comando.Parameters.AddWithValue("@ID", conta.Id);
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
                conexao.Close();
                return false;
            }

        }
    }
}
