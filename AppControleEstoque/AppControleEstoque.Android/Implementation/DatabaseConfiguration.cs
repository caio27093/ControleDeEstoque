using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppControleEstoque.Droid.Implementation;
using AppControleEstoque.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConfiguration))]
namespace AppControleEstoque.Droid.Implementation
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public DatabaseConfiguration()
        {
        }

        public string GetCaminhoDatabase()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }
    }
}