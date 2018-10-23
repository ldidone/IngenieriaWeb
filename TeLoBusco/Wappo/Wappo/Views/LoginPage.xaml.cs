using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using Wappo.Views;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Wappo.Services;

namespace Wappo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public readonly RepositoriosServicios servicios = new RepositoriosServicios();

        public LoginPage ()
		{
			InitializeComponent ();
		}
        private async Task RequestPermissions()
        {
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
        }  

        public async void OnLoginButtonClicked(object sender, EventArgs e) //aca va task --> diego dijo que no
        {
            //Aca hacer verificacion de logueo

            var response = await servicios.ObtenerRepoByUser("fisadev");
            await Navigation.PushAsync(new PedidoDeliveryPage());

        }

    }
}