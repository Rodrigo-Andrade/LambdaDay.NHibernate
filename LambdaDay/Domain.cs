using System.Collections.Generic;

namespace LambdaDay
{
    public class Livro
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Sinopse { get; set; }

        public virtual Editora Editora { get; set; }

        public virtual IList<Autor> Autores { get; set; }

        public Livro()
        {
            Autores = new List<Autor>();
        }

        public override string ToString()
        {
            return string.Format("Id: {0}, Nome: {1}, Sinopse: {2}", Id, Nome, Sinopse);
        }
    }

    public class Autor
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Nome: {1}", Id, Nome);
        }
    }

    public class Editora
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Nome: {1}", Id, Nome);
        }
    }
}
