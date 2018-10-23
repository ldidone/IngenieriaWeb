using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Wappo.Models;

namespace Wappo.ViewsModel
{
    public class Pedidos
    {
        public ObservableCollection<PedidoModel> PedidosList { get; set; }
        public Pedidos()
        {
            PedidosList = new ObservableCollection<PedidoModel>();
            //aca le pasaria la lista de pedidos asignado al delivery
            DatosTemp _context = new DatosTemp();
            foreach (var item in _context.ListaPedidos)
            {
                PedidosList.Add(item);
            }
        }

    }
}
