using System;
using System.Collections.Generic;
using System.Linq;

namespace Proj.Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char continuar = 's';
            int opcao;
            int tombo = 0;
            Livros biblioteca = new Livros();

            while (continuar == 's' || continuar == 'S')
            {
                Console.WriteLine("Escolha uma opção.");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Adicionar livro");
                Console.WriteLine("2. Pesquisar livro (sintético)");
                Console.WriteLine("3. Pesquisar livro (analítico)");
                Console.WriteLine("4. Adicionar exemplar");
                Console.WriteLine("5. Registrar empréstimo");
                Console.WriteLine("6. Registrar devolução\n");

                opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Finalizando o programa.");
                        continuar = 'n';
                        break;
                    case 1:
                        AdicionarLivro(biblioteca);
                        break;
                    case 2:
                        PesquisarLivroSintetico(biblioteca);
                        break;
                    case 3:
                        PesquisarLivroAnalitico(biblioteca);
                        break;
                    case 4:
                        AdicionarExemplar(biblioteca, ref tombo);
                        break;
                    case 5:
                        RegistrarEmprestimo(biblioteca);
                        break;
                    case 6:
                        RegistrarDevolucao(biblioteca);
                        break;
                }
            }
        }

        static int ObterIsbn()
        {
            Console.WriteLine("Digite o ISBN do livro: ");
            return int.Parse(Console.ReadLine());
        }

        static void AdicionarLivro(Livros biblioteca)
        {
            Console.WriteLine("Adicionando livro.");
            int isbn = ObterIsbn();
            Console.WriteLine("Digite o título do livro: ");
            string titulo = Console.ReadLine();
            Console.WriteLine("Digite o autor do livro: ");
            string autor = Console.ReadLine();
            Console.WriteLine("Digite a editora do livro: ");
            string editora = Console.ReadLine();

            Livro livro = new Livro(isbn, titulo, autor, editora);
            biblioteca.adicionar(livro);
            Console.WriteLine("Livro adicionado.\n");
        }

        static void PesquisarLivroSintetico(Livros biblioteca)
        {
            int isbn = ObterIsbn();
            Livro livroPesquisado = new Livro(isbn);
            Livro resultado = biblioteca.pesquisar(livroPesquisado);

            if (resultado != null)
            {
                Console.WriteLine($"\nLivro encontrado: {resultado.Titulo} por {resultado.Autor}");
                Console.WriteLine($"Total de Exemplares: {resultado.qtdeExemplares()}");
                Console.WriteLine($"Total de Exemplares Disponíveis: {resultado.qtdeDisponiveis()}");
                Console.WriteLine($"Total de Empréstimos: {resultado.qtdeEmprestimos()}");
                Console.WriteLine($"Percentual de Disponibilidade: {resultado.percDisponibilidade()}% \n");
            }
            else
            {
                Console.WriteLine("\nLivro não encontrado.\n");
            }
        }

        static void PesquisarLivroAnalitico(Livros biblioteca)
        {
            int isbn = ObterIsbn();
            Livro livroPesquisado = new Livro(isbn);
            Livro resultado = biblioteca.pesquisar(livroPesquisado);

            if (resultado != null)
            {
                Console.WriteLine($"\nLivro encontrado: {resultado.Titulo} por {resultado.Autor}, publicado pela editora {resultado.Editora}.");
                Console.WriteLine($"Total de Exemplares: {resultado.qtdeExemplares()}");
                Console.WriteLine($"Total de Exemplares Disponíveis: {resultado.qtdeDisponiveis()}");
                Console.WriteLine($"Total de Empréstimos: {resultado.qtdeEmprestimos()}");
                Console.WriteLine($"Percentual de Disponibilidade: {resultado.percDisponibilidade()}%  \n");

                Console.WriteLine("Detalhes dos Exemplares:");
                foreach (var exemplar in resultado.Exemplares)
                {
                    Console.WriteLine($"Exemplar {exemplar.Tombo} - Disponível: {exemplar.disponivel()}");
                    foreach (var emprestimo in exemplar.Emprestimos)
                    {
                        Console.WriteLine($"Empréstimo do Exemplar {exemplar.Tombo}: {emprestimo.DtEmprestimo}");
                        if (emprestimo.DtDevolucao != default)
                        {
                            Console.WriteLine($"Devolução do Exemplar {exemplar.Tombo}: {emprestimo.DtDevolucao}");

                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\nLivro não encontrado.\n");
            }
        }

        static void AdicionarExemplar(Livros biblioteca, ref int tombo)
        {
            Console.WriteLine("Registrando exemplar.");
            tombo++;
            int isbn = ObterIsbn();
            Livro livroExemplar = new Livro(isbn);

            Livro livroEncontrado = biblioteca.pesquisar(livroExemplar);
            if (livroEncontrado != null)
            {
                Exemplar exemplar = new Exemplar(tombo);
                livroEncontrado.adicionarExemplar(exemplar);
                Console.WriteLine("Exemplar adicionado ao livro.");
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void RegistrarEmprestimo(Livros biblioteca)
        {
            Console.WriteLine("Registrando empréstimo.");
            int isbn = ObterIsbn();
            Livro livroEmprestimo = new Livro(isbn);

            Livro livroEncontrado = biblioteca.pesquisar(livroEmprestimo);
            if (livroEncontrado != null)
            {
                if (livroEncontrado.qtdeDisponiveis() > 0) 
                {
                    foreach (var exemplar in livroEncontrado.Exemplares)
                    {
                        if (exemplar.emprestar())
                        {
                            Console.WriteLine("Exemplar emprestado.");
                            return; 
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Não é possível emprestar, todos os exemplares estão emprestados.");
                }
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }

        static void RegistrarDevolucao(Livros biblioteca)
        {
            Console.WriteLine("Registrando devolução.");
            int isbn = ObterIsbn();
            Livro livroDevolucao = new Livro(isbn);

            Livro livroEncontrado = biblioteca.pesquisar(livroDevolucao);
            if (livroEncontrado != null)
            {
                if (livroEncontrado.qtdeDisponiveis() >= 0)
                {
                    foreach (var exemplar in livroEncontrado.Exemplares)
                    {
                        if (exemplar.devolver())
                        {
                            Console.WriteLine("Exemplar devolvido.");
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Não é possível devolver, nenhum exemplar está sendo emprestado.");
                }
            }
            else
            {
                Console.WriteLine("Livro não encontrado.");
            }
        }
    }
}
