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
            builder.RegisterType<MovieRepository>().As<IMovieRepository>();
            builder.RegisterType<MovieGenreRepository>().As<IMovieGenreRepository>();
            builder.RegisterType<MovieLanguageRepository>().As<IMovieLanguageRepository>();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>();
            builder.RegisterType<LanguageRepository>().As<ILanguageRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<MovieContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
        }
    }
}
