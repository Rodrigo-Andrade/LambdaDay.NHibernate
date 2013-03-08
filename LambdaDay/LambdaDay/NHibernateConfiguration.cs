using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace LambdaDay
{
    public class NHibernateConfiguration
    {
        private static readonly Lazy<ISessionFactory> LazySessionFactory =
            new Lazy<ISessionFactory>(
                () => Fluently.Configure()
                          .Database(
                              MsSqlConfiguration.MsSql2008
                                  .ConnectionString(
                                      @"data source=.\sqlexpress; initial catalog=Testes; integrated security=sspi")
                                  .ShowSql()
                                  .FormatSql())
                          .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Livro>())
                          .Cache(x => x.UseSecondLevelCache().UseQueryCache().ProviderClass<NHibernate.Cache.HashtableCacheProvider>())
                           .ExposeConfiguration(
                              c =>
                              {
                                  c.DataBaseIntegration(cfg => { cfg.SchemaAction = SchemaAutoAction.Recreate; });
                                  c.SessionFactory().GenerateStatistics();
                              })
                          .BuildSessionFactory());

        public static ISessionFactory SessionFactory { get { return LazySessionFactory.Value; } }
    }
}

