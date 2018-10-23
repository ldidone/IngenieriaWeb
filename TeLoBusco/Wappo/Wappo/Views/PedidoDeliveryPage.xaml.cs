using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Wappo.Models;
using Wappo.ViewsModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wappo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoDeliveryPage : ContentPage
    {
        public ObservableCollection<PedidoModel> ListaPedidos { get; set; }
        public ObservableCollection<string> Items { get; set; }

        public PedidoDeliveryPage()
        {
            InitializeComponent();
            BindingContext = new Pedidos();
            ListaPedidos = new ObservableCollection<PedidoModel>();			
			MyListView.ItemsSource = ListaPedidos   ;
        }

        //async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item == null)
        //        return;

        //    await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

        //    //Deselect Item
        //    ((ListView)sender).SelectedItem = null;
        //}
    }
}
