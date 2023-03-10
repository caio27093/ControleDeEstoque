using AppControleEstoque.ViewModels;
using AppControleEstoque.Views.Produto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppControleEstoque
{
    public partial class MainPage : ContentPage
    {
        #region Variaveis
        MainPageViewModel _vm = new MainPageViewModel();
        #endregion

        #region Construtor
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = _vm;
        }
        #endregion

        #region Métodos
        private async void ButtonPesquisar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodProd.Text))
                _vm.PesquisaProduto(Convert.ToInt32(txtCodProd.Text));
            else
                _vm.ExibirListaCompleta();

            if(_vm.ListaDeProdutosExibida[0] == null)
                await DisplayAlert("Atenção", "Item não encontrado", "Okay");
        }

        private void Button_Clicked_1 ( object sender, EventArgs e )
        {
            Navigation.PushAsync (new DetalheProdutoEstoque ( ));
        }

        private async void SwipeExcluir_Clicked(object sender, EventArgs e)
        {
            SwipeItem botao = (SwipeItem)sender;
            int id = Convert.ToInt32(botao.CommandParameter);

            if (id == 0)
                return;

            bool confirmacao = await DisplayAlert("Atenção", $"Deseja relamente apagar o item {id}?", "Sim", "Não");

            if (confirmacao)
                _vm.DeletarProduto(id);
        }
        private void SwipeAlterar_Clicked(object sender, EventArgs e)
        {
            SwipeItem botao = (SwipeItem)sender;
            int id = Convert.ToInt32(botao.CommandParameter);
            Navigation.PushAsync(new NavigationPage(new DetalheProdutoEstoque(_vm.DevolveProdutoPeloCodigo(id))));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Atualizar os dados
            _vm.CarregarEstoque();
            _vm.ExibirListaCompleta();
        }
        #endregion

    }
}
