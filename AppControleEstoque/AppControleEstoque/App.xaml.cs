using AppControleEstoque.Core;
using AppControleEstoque.DAL;
using AppControleEstoque.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static AppControleEstoque.Core.AppSetup;

namespace AppControleEstoque
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            IDatabaseConfiguration databaseConfiguration = DependencyService.Get<IDatabaseConfiguration>();
            DBConfiguration.CaminhoArquivo = databaseConfiguration.GetCaminhoDatabase();

            AppSetup appSetup = new AppSetup();
            AppContainer.Container = appSetup.CreateContainer();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
