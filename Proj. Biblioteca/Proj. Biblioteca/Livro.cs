using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Biblioteca
{
    internal class Livro
    {
        private int isbn;
        private string titulo;
        private string autor;
        private string editora;
        private List<Exemplar> exemplares = new List<Exemplar>();

        public int Isbn { get => isbn; set => isbn = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Autor { get => autor; set => autor = value; }
        public string Editora { get => editora; set => editora = value; }
        public List<Exemplar> Exemplares { get => exemplares; set => exemplares = value; }

        public Livro()
        {
            Isbn = 0;
            Titulo = "";
            Autor = "";
            Editora = "";
            Exemplares = new List<Exemplar>();
        }

        public Livro(int isbn)
        {
            Isbn = isbn;
            Exemplares = new List<Exemplar>();
        }
        public Livro(int isbn, string titulo, string autor, string editora)
        {
            Isbn = isbn;
            Titulo = titulo;
            Autor = autor;
            Editora = editora;
        }

        public Livro(int isbn, string titulo, string autor, string editora, List<Exemplar> exemplar)
        {
            Isbn = isbn;
            Titulo = titulo;
            Autor = autor;
            Editora = editora;
            Exemplares = exemplar;
        }

        public void adicionarExemplar(Exemplar exemplar)
        {
            Exemplares.Add(exemplar);
        }

        public int qtdeExemplares()
        {
            return Exemplares.Count();
        }

        public int qtdeDisponiveis()
        {
            int count_disponivel = 0;
            foreach (var exemplar in Exemplares)
            {
                if (exemplar.disponivel())
                {
                    count_disponivel++;
                }
            }
            return count_disponivel;
        }

        public int qtdeEmprestimos()
        {
            int count_emprestimos = 0;
            foreach (var exemplar in Exemplares)
            {
                count_emprestimos += exemplar.qtdeEmprestimos();
            }
            return count_emprestimos;
        }

        public double percDisponibilidade()
        {
            int disponiveis = qtdeDisponiveis();
            int total = qtdeExemplares();

            return total > 0 ? (double)disponiveis / total * 100 : 0;
        }


    }
}
