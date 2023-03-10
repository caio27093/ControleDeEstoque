using AppControleEstoque.Regras.Models;
using AppControleEstoque.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppControleEstoque.Views.Produto
{
    [XamlCompilation ( XamlCompilationOptions.Compile )]
    public partial class DetalheProdutoEstoque : ContentPage
    {
        #region Variaveis
        DetalheProdutoEstoqueViewModel _vm = new DetalheProdutoEstoqueViewModel();
        #endregion

        #region Construtor
        public DetalheProdutoEstoque ( EstoqueModel estoque = null )
        {
            InitializeComponent ( );
            
            if (estoque != null)
            {
                lblTitulo.Text = "Alterar Produto";
                _vm.Produto = estoque;
                btnSalvar.Clicked += ButtonAlterar_Clicked;
            }
            else
            {
                lblTitulo.Text = "Incluir Produto";
                _vm.Produto = new EstoqueModel();
                btnSalvar.Clicked += ButtonInserir_Clicked;
            }

            this.BindingContext = _vm;
        }
        #endregion

        #region Eventos
        private async void ButtonInserir_Clicked(object sender, EventArgs e)
        {
            if (_vm.ValidadorDeDados(true))
            {
                if (_vm.CadastrarProduto())
                {
                    await DisplayAlert("Sucesso", "Produto adicionado ao estoque com sucesso", "OK");
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                }
                else
                    await DisplayAlert("Erro", "Houve um problema ao cadastrar um produto no estoque! Tente novamente mais tarde.", "OK");
                return;
            }
            await DisplayAlert("Erro ao validar os dados", "A quantidade minima para cadastrar um produto é 1\n  O limite máximo de caracteres para o nome é 200 e ele não pode estar vazio.", "OK");
        }

        private async void ButtonAlterar_Clicked(object sender, EventArgs e)
        {
            if (_vm.ValidadorDeDados(false))
            {
                if (_vm.AlterarProduto())
                {
                    await DisplayAlert("Sucesso", "Produto alterado com sucesso", "OK");
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                }
                else
                    await DisplayAlert("Erro", "Houve um problema ao alterar o produto! Tente novamente mais tarde.", "OK");

                return;
            }
            await DisplayAlert("Erro ao validar os dados", "O limite máximo de caracteres para o nome é 200 e ele não pode estar vazio.", "OK");
        }

        #endregion
    }
}