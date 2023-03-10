using AppControleEstoque.Interfaces;
using AppControleEstoque.iOS.Implementation;
using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConfiguration))]
namespace AppControleEstoque.iOS.Implementation
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string GetCaminhoDatabase()
        {
            var personalfolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(personalfolder, "..", "Library");
        }
    }
}