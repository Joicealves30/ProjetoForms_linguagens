using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace SistemaCadastro
{
    public partial class Sistema : Form
    {
        int idAlterar;
        public Sistema()
        {
            InitializeComponent();
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCadastra_Click(object sender, EventArgs e)
        {
            marcador.Height = btnCadastra.Height;
            marcador.Top = btnCadastra.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }
        

        private void btnBusca_Click(object sender, EventArgs e)
        {
            marcador.Height = btnBusca.Height;
            marcador.Top = btnBusca.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }







        private void Sistema_Load(object sender, EventArgs e)
        {

            mostrarCategoria();
            listar_gridProdutos();

        }

        void listar_gridProdutos() {
            ConectaBanco conectaBanco = new ConectaBanco();
           dgProdutos.DataSource = conectaBanco.listaProdutos();
            dgProdutos.Columns["idprodutos"].Visible = false;
            //MessageBox.Show(conectaBanco.mensagem);
        }

         void mostrarCategoria()
        {
            ConectaBanco conectaBanco = new ConectaBanco();
            DataTable categorias = conectaBanco.listaCategorias();
            cbCategoria.DataSource = categorias;
            cbCategoria.DisplayMember = "nome";
            cbCategoria.ValueMember = "idcategoria";
        }



        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            (dgProdutos.DataSource as DataTable).DefaultView.RowFilter =
                 string.Format("nome like '{0}%'", txtBusca.Text);
            
        }


        private void btnRemoveBanda_Click(object sender, EventArgs e)
        {
            int linha = dgProdutos.CurrentRow.Index;
            int id = Convert.ToInt32
                (dgProdutos.Rows[linha].Cells["idprodutos"].Value.ToString());
            DialogResult resp = MessageBox.Show("Deseja realmente excluir o produto?",
            "Remove Banda ", MessageBoxButtons.OKCancel);
            if (resp == DialogResult.OK)
            {
                ConectaBanco conectaBanco = new ConectaBanco();
                bool retorno = conectaBanco.removeProduto(id);
                if (retorno == true)
                {
                    MessageBox.Show("Produto excluído com sucesso!");
                    listar_gridProdutos();
                }
                //fim if true
                else
                    MessageBox.Show(conectaBanco.mensagem);
            }
            else
            {
                MessageBox.Show("Exclusão cancelada!");
                listar_gridProdutos();
            }

        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int linha = dgProdutos.CurrentRow.Index;
            int idAlterar = Convert.ToInt32(dgProdutos.Rows[linha].Cells["idprodutos"].Value.ToString());
            txtAlteraNome.Text = dgProdutos.Rows[linha].Cells["nome"].Value.ToString();
            cbAlteraCategoria.Text = dgProdutos.Rows[linha].Cells["fkcategoria"].Value.ToString();
            txtAlteradescricao.Text = dgProdutos.Rows[linha].Cells["descricao"].Value.ToString();
            txtAlterapreco.Text = dgProdutos.Rows[linha].Cells["preco"].Value.ToString();
        }
        private void btnConfirmaAlteracao_Click(object sender, EventArgs e)
        {
            
            int idproduto = idAlterar;
            string nome = cbAlteraCategoria.Text;
            double preco = double.Parse(txtAlterapreco.Text);
            int categoria = int.Parse(cbAlteraCategoria.Text);
            string descricao = txtAlteradescricao.Text;

           
            string connectionString = "server=localhost;user id=root;password=;database=sistemalanchonete";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                   
                    MySqlCommand cmd = new MySqlCommand("sp_alteraProduto", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                
                    cmd.Parameters.AddWithValue("@idproduto", idproduto);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@preco", preco);
                    cmd.Parameters.AddWithValue("@categoria", categoria);
                    cmd.Parameters.AddWithValue("@descricao", descricao);

                    
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produto alterado com sucesso !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);



                }
            }
        }
        private void bntAddGenero_Click(object sender, EventArgs e)
        {
          
        }

        private void BtnConfirmaCadastro_Click(object sender, EventArgs e)
        {
            ConectaBanco conectaBanco = new ConectaBanco();
            Produto novoproduto = new Produto();
            novoproduto.Nome = txtnome.Text;
            novoproduto.Categoria =Convert.ToInt32( cbCategoria.SelectedValue.ToString());
            novoproduto.Descricao = txtdescricao.Text;
            novoproduto.Valor =double.Parse( txtvalor.Text);
           
            bool retorno = conectaBanco.insereProduto(novoproduto);
            if (retorno==false) 
                MessageBox.Show(conectaBanco.mensagem);

            else { MessageBox.Show("Produto cadastrado com sucesso!");
                limparCampos();
                listar_gridProdutos();
            }

        }
           
        void limparCampos()
        {
            txtnome.Clear();
            txtdescricao.Clear();   
            txtvalor.Clear();
        }

        private void dgProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtAlteraRanking_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbAlteraCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
