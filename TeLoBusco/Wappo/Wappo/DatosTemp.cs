using System;
using System.Collections.Generic;
using System.Text;
using Wappo.Models;

namespace Wappo
{
    public class DatosTemp
    {
        public List<PedidoModel> ListaPedidos = new List<PedidoModel>()
        {
            new PedidoModel()
            {
                NumeroPedido = 1,
                Usuario = "Pepito 1",
                DireccionEntrega = "Gral Paz 1111"
            },
            new PedidoModel()
            {
                NumeroPedido = 2,
                Usuario = "Josesito 1",
                DireccionEntrega = "Gral Paz 22222"
            },
            new PedidoModel()
            {
                NumeroPedido = 3,
                Usuario = "Pancho 1",
                DireccionEntrega = "Corrientes 1111"
            },
            new PedidoModel()
            {
                NumeroPedido = 4,
                Usuario = "Francisco",
                DireccionEntrega = "Santa Fe 1111"
            },
            new PedidoModel()
            {
                NumeroPedido = 5,
                Usuario = "Javier 1",
                DireccionEntrega = "Entre Rios 1111"
            }
        };

    }
}
