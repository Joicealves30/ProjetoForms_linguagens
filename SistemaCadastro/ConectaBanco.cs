using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
namespace SistemaCadastro
{
    public class ConectaBanco
    {

       //String de conexão
          MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=;database=sistemalanchonete");
        public string mensagem="";

        public bool insereProduto(Produto novoProduto)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd =new MySqlCommand("sp_insereProduto", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", novoProduto.Nome);
                cmd.Parameters.AddWithValue("descricao", novoProduto.Descricao);
                cmd.Parameters.AddWithValue("valor", novoProduto.Valor);
                cmd.Parameters.AddWithValue("categoria", novoProduto.Categoria);
                cmd.ExecuteNonQuery();//executar no banco
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }

        }// fim do insereProduto
        public DataTable listaProdutos()
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("sp_listaProdutos", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable produtos = new DataTable();
                da.Fill(produtos);
                return produtos;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return null;
            }
        }//fim do listaProdutos



        public DataTable buscaProduto(string nome)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("sp_buscaProduto", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", nome);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable produtos = new DataTable();
                da.Fill(produtos);
                return produtos;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return null;
            }
        }//fim do buscaProduto

        public bool removeProduto(int idremoveproduto )
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("sp_removeProduto", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("idproduto", idremoveproduto);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }
        }//fim do removeProduto

        public bool alteraProduto(Produto produto,int idproduto)
        {
                MySqlCommand cmd = new MySqlCommand("sp_alteraProduto", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("idproduto", idproduto);
                cmd.Parameters.AddWithValue("descricao", produto.Descricao);
                cmd.Parameters.AddWithValue("valor", produto.Valor);
                cmd.Parameters.AddWithValue("categoria", produto.Categoria);
                cmd.ExecuteNonQuery();
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();//executa o comando

                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }
        }//fim do alteraProduto


        public bool insereCategoria(string nome)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("sp_insereCategoria", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", nome);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }
        }//fim do insereCategoria

        public DataTable listaCategorias()
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("sp_listaCategoria", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable categorias = new DataTable();
                da.Fill(categorias);
                return categorias;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return null;
            }
        }//fim do listaCategorias

        public bool removeCategoria(int id)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("sp_removeCategoria", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }
        }//fim do removeCategoria

        public bool alteraCategoria(int id, string nome)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("sp_alteraCategoria", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("nome", nome);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }
        }//fim do alteraCategoria

        public DataTable buscaCategoria(string nome)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("sp_buscaCategoria", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", nome);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable categorias = new DataTable();
                da.Fill(categorias);
                return categorias;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return null;
            }
        }//fim do buscaCategoria

        public DataTable listaProdutosCategoria(int id)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("sp_listaProdutosCategoria", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id", id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable produtos = new DataTable();
                da.Fill(produtos);
                return produtos;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return null;
            }
        }//fim do listaProdutosCategoria

       
    }//fim da classe
}
