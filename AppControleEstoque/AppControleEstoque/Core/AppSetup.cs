using AppControleEstoque.DAL;
using AppControleEstoque.DAL.Implementation;
using AppControleEstoque.Regras.Infraestructure;
using AppControleEstoque.Regras.Models;
using AppControleEstoque.Regras.Services;
using AppControleEstoque.Service.API.Implementations;
using AppControleEstoque.Service.API.Interface;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleEstoque.Core
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            if (!string.IsNullOrEmpty(DBConfiguration.CaminhoDb))
                ConfigurarAcessoDadosSqlite(containerBuilder);

            RegisterDependenciesApi(containerBuilder);

            return containerBuilder.Build();
        }
        private void ConfigurarAcessoDadosSqlite(ContainerBuilder cb)
        {
            cb.Register(c => new EstoqueCRUD(DBConfiguration.CaminhoDb)).As<IDatabaseCRUD<EstoqueModel>>().SingleInstance();
        }

        protected virtual void RegisterDependenciesApi(ContainerBuilder cb)
        {
            cb.Register(c => new ApiClient()).As<IAPIClient>().SingleInstance();
            cb.RegisterType<Service.API.Implementations.EstoqueService>().As<IEstoqueService>().SingleInstance();
        }
        public static class AppContainer
        {
            public static IContainer Container { get; set; }
        }
    }
}
