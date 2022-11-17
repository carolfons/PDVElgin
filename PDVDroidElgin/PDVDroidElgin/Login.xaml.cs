using PDVDroidElgin.Contracts;
using PDVDroidElgin.Data;
using PDVDroidElgin.Hardware;
using PDVDroidElgin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDVDroidElgin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {

        public static Usuario UsuarioLogado { get; private set; }

        public Login()
        {
            InitializeComponent();

            lbVersao.Text = AppInfo.VersionString;
        }

        private async void btLogin_Clicked(object sender, EventArgs e)
        {
            btLogin.IsEnabled = false;
            btLogin.Text = "Aguarde...";

            string usr = txUsuario.Text;
            string senha = txSenha.Text;

            bool logado = await EfetuaLogin(usr, senha);

            btLogin.IsEnabled = true;
            btLogin.Text = "Login";

            if (logado)
                await Navigation.PushAsync(new Menu());

        }

        private async Task<bool> EfetuaLogin(string nome, string senha)
        {
            using (PdvContext db = new PdvContext())
            {
                Usuario usuario = await Task.Run(() =>
                        db.Usuarios
                        .FirstOrDefault(u =>
                            u.Nome.Equals(nome) &&
                            u.Senha.Equals(senha))
                    );

                if (usuario == null)
                {
                    await DisplayAlert("Atenção", "Usuário ou senha inválidos", "Ok");
                    return false;
                }

                UsuarioLogado = usuario;
                return true;
            }
        }
    }
}