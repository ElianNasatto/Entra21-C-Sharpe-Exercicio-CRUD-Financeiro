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
    public class ClienteRepository
    {
        string caminhoConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=T:\Documentos\GitHub\C-Sharpe-Entra21-Exercicio-CRUD-Financeiro\Financeiro\BD_Financeiro.mdf;Integrated Security=True;Connect Timeout=30";

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

            comando.CommandText = "INSERT INTO clientes (nome,cpf,data_nascimento,rg) VALUES (@NOME,@CPF,@DATA_NASCIMENTO,@RG)";
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.Data_Nascimento);
            comando.Parameters.AddWithValue("@RG", cliente.RG);
            
                comando.ExecuteNonQuery();
                conexao.Close();
                return true;
            

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
                return null;
            }

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;

            comando.CommandText = "SELECT * FROM Clientes";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            List<Cliente> listaClientes = new List<Cliente>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Cliente cliente = new Cliente();

                cliente.Id = Convert.ToInt32(linha["id"]);
                cliente.Nome = linha["nome"].ToString();
                cliente.CPF = linha["cpf"].ToString();
                cliente.Data_Nascimento = Convert.ToDateTime(linha["data_nascimento"]);
                cliente.RG = linha["rg"].ToString();

                listaClientes.Add(cliente);

            }

            return listaClientes;

        }

        public bool Deletar(int id)
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
            comando.CommandText = "DELETE FROM clientes WHERE id = @ID";
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

        public bool Alterar(Cliente cliente)
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
            comando.CommandText = "UPDATE clientes SET nome = @NOME, cpf = @CPF,data_nascimento = @DATA_NASCIMENTO,rg = @RG WHERE id = @id ";

            comando.Parameters.AddWithValue("@ID", cliente.Id);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", cliente.Data_Nascimento);
            comando.Parameters.AddWithValue("@RG", cliente.RG);



            comando.ExecuteNonQuery();
            conexao.Close();
            return true;

        }

        public Cliente BuscarPeloId(int id)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = caminhoConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT * FROM clientes WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            conexao.Close();

            DataRow linha = tabela.Rows[0];
            Cliente cliente = new Cliente();
            cliente.Id = Convert.ToInt32(linha["id"]);
            cliente.Nome = linha["nome"].ToString();
            cliente.CPF = linha["cpf"].ToString();
            cliente.Data_Nascimento = Convert.ToDateTime(linha["data_nascimento"]);
            cliente.RG = linha["rg"].ToString();

            return cliente;
        }
    }

}

