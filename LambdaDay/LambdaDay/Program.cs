using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;

namespace LambdaDay
{
    class Program
    {
        private static readonly ISessionFactory SessionFactory = NHibernateConfiguration.SessionFactory;

        static void Main()
        {

            App_Start.NHibernateProfilerBootstrapper.PreStart();

            try
            {
                using (SessionFactory)
                {
                    DoWork();
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }

        private static void DoWork()
        {
            var livroId = Seed();

            //using (var session = SessionFactory.OpenSession())
            //using (var tx = session.BeginTransaction())
            //{
            //    var autores = session.Query<Autor>().Cacheable().ToFuture();
            //    var editoras = session.Query<Editora>().Cacheable().ToFuture().ToList();

            //    var livro = session.Get<Livro>(1);

            //    Console.WriteLine("Livro");
            //    Console.WriteLine(livro);

            //    Console.WriteLine("Editoras");
            //    foreach (var editora in editoras)
            //        Console.WriteLine(editora);

            //    Console.WriteLine("Autores");
            //    foreach (var autor in autores)
            //        Console.WriteLine(autor);

            //    tx.Commit();
            //}
            for (var i = 0; i < 2; i++)
                using (var session = SessionFactory.OpenSession())
                using (var tx = session.BeginTransaction())
                {
                    var livro = session.Get<Livro>(livroId);

                    Console.WriteLine(livro);
                    Console.WriteLine(livro.Editora);

                    foreach (var autor in livro.Autores)
                        Console.WriteLine(autor);

                    tx.Commit();
                }
        }

        private static int Seed()
        {
            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var editora = new Editora { Nome = "Addion Wesley" };
                session.Save(editora);

                var autores = new[] { "Gamma", "Helm", "Jhonson", "Vlissides" }
                    .Select(nome => new Autor { Nome = nome })
                    .ToArray();

                foreach (var autor in autores)
                    session.Save(autor);

                var livro = new Livro
                                {
                                    Nome = "Design Patterns - GoF",
                                    Sinopse = "O clássico",
                                    Editora = editora
                                };

                foreach (var autor in autores)
                    livro.Autores.Add(autor);

                session.Save(livro);

                tx.Commit();

                return livro.Id;
            }
        }
    }
}


