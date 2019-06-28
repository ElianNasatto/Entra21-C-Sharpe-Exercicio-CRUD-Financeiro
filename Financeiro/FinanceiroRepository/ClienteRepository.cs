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
    class ClienteRepository
    {
        string caminhoConexao = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = T:\Documentos\GitHub\C - Sharpe - Entra21 - Exercicio - CRUD - Financeiro\C - Sharpe - Entra21 - Exercicio - CRUD - Financeiro\Financeiro\BD_Financeiro.mdf; Integrated Security = True; Connect Timeout = 30";

        //Insere no banco de dados
        public bool Inserir(Cliente cliente)
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

            comando.CommandText = "INSERT INTO Clientes nome,cpf,data_nascimento,rg VALUES (@NOME,@CPF,@DATA_NASCIMENTO,@RG)";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.Data_Nascimento);
            comando.Parameters.AddWithValue("@RG", cliente.RG);
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

        public List<Cliente> Listar()
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

            comando.CommandText = "SELECT * FROM Clientes";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
        }

    }

}

