using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Biblioteca
{
    internal class Livros
    {

        private List<Livro> acervo = new List<Livro>();

        public List<Livro> Acervo { get => acervo; set => acervo = value; }

        public void adicionar(Livro livro)
        {
            Acervo.Add(livro);
        }

        public Livro pesquisar(Livro livro)
        {
            return Acervo.FirstOrDefault(l => l.Isbn == livro.Isbn);
        }
    }
}
