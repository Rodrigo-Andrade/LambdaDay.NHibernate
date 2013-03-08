using FluentNHibernate.Mapping;

namespace LambdaDay
{
    public class LivroMap : ClassMap<Livro>
    {
        public LivroMap()
        {
            Cache.ReadWrite();

            Id(x => x.Id).GeneratedBy.HiLo("1024");
            Map(x => x.Nome);
            Map(x => x.Sinopse);

            References(x => x.Editora);

            HasMany(x => x.Autores).Cache.ReadWrite();
        }
    }

    public class EdtiraMap : ClassMap<Editora>
    {
        public EdtiraMap()
        {
            Cache.ReadWrite();

            Id(x => x.Id).GeneratedBy.HiLo("1024");
            Map(x => x.Nome);
        }
    }

    public class AutorMap : ClassMap<Autor>
    {
        public AutorMap()
        {
            Cache.ReadWrite();

            Id(x => x.Id).GeneratedBy.HiLo("1024");
            Map(x => x.Nome);
        }
    }
}
