using PDVDroidElgin.Contracts;
using PDVDroidElgin.Data;
using PDVDroidElgin.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PDVDroidElgin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Splash : ContentPage
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            DependencyService.Get<IPrinter>().PrinterInternalImpStart();

            IControladorPDV controladorPdv = new ControladorPDVImpl();
            DependencyService.RegisterSingleton<IControladorPDV>(controladorPdv);

            Task.Run(() =>
            {
                try
                {
                    var fNotificacaoCriarBanci = new Func<string, int>(delegate (string x)
                    {
                        Application.Current.Dispatcher.BeginInvokeOnMainThread(() =>
                        {
                            DependencyService.Get<IToastFacade>().ShortAlert(x);
                        });

                        return 0;
                    });

                    using (PdvContext db = new PdvContext())
                    {
                        db.CheckCreated(fNotificacaoCriarBanci);
                    }
                }
                catch (Exception ex)
                {
                    DependencyService.Get<IToastFacade>().ShortAlert($"A criação do esquema de dados falhou: {ex.Message}");
                }



                App.Current.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage = new NavigationPage(new Login());
                });
            });

        }
    }
}