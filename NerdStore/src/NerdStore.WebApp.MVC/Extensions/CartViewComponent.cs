﻿using Microsoft.AspNetCore.Mvc;
using NerdStore.Vendas.Application.Queries;

namespace NerdStore.WebApp.MVC.Extensions
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IPedidoQueries _pedidoQueries;

        // TODO: Obter cliente logado
        protected Guid ClienteId = Guid.Parse("8f724785-c43c-454e-9d70-bca7e6416929");

        public CartViewComponent(IPedidoQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            var itens = carrinho?.Items.Count ?? 0;

            return View(itens);
        }
    }
}
