
using Autofac;
using DataAccessLibrary.Repos.Interfaces;
using DataAccessLibrary.Repos.SQL;
using DataAccessLibrary.RepositoryContainer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.DataAccess
{
    public class DALAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<SQLMovieRepository>().As<IMovieRepository>();
            builder.RegisterType<SQLMovieGenreRepository>().As<IMovieGenreRepository>();
            builder.RegisterType<SQLMovieLanguageRepository>().As<IMovieLanguageRepository>();
            builder.RegisterType<SQLGenreRepository>().As<IGenreRepository>();
            builder.RegisterType<SQLLanguageRepository>().As<ILanguageRepository>();
            builder.RegisterType<MovieContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
        }
    }
}
