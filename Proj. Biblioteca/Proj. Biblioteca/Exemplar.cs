using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Biblioteca
{
    internal class Exemplar
    {
        private int tombo;
        private List<Emprestimo> emprestimos = new List<Emprestimo>();

        public int Tombo { get =>  tombo; set => tombo = value; }
        public List<Emprestimo> Emprestimos { get => emprestimos; set => emprestimos = value; }

        public Exemplar()
        {
            Tombo = 0;
            Emprestimos = new List<Emprestimo>();        
        }
        public Exemplar(int tombo)
        {
            Tombo = tombo;
            Emprestimos = new List<Emprestimo>();
        }

        public bool emprestar()
        {
            if (disponivel())
            {
                Emprestimos.Add(new Emprestimo { DtEmprestimo = DateTime.Now });
                return true;
            } 
            return false;
        }

        public bool devolver()
        {
            var emprestimoAtivo = Emprestimos.FirstOrDefault(e => e.DtDevolucao == default(DateTime));

            if (emprestimoAtivo != null)
            {
                emprestimoAtivo.DtDevolucao = DateTime.Now;
                return true;
            }

            return false;
        }

        public bool disponivel()
        {
            return Emprestimos.Count == 0 || Emprestimos.Any(e => e.DtDevolucao != default(DateTime));
        }

        public int qtdeEmprestimos()
        {
            return Emprestimos.Count();
        }
    }
}
