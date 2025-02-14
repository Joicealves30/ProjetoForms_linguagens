using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCadastro
{
    public class Produto
    {
        private string nome;
        private double valor;
        private string descricao;
        private int categoria;

        public string Nome { get => nome; set => nome = value; }
        public double Valor { get => valor; set => valor = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public int Categoria { get => categoria; set => categoria = value; }
    }
}
