﻿using NerdStore.Vendas.Application.Queries.DTO;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Application.Queries
{
    public class PedidoQueries : IPedidoQueries
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<CarrinhoDTO?> ObterCarrinhoCliente(Guid clienteId)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(clienteId);
            if (pedido is null) return null;

            var carrinho = new CarrinhoDTO
            {
                ClienteId = pedido.ClienteId,
                ValorTotal = pedido.ValorTotal,
                PedidoId = pedido.Id,
                ValorDesconto = pedido.Desconto,
                SubTotal = pedido.Desconto + pedido.ValorTotal
            };

            if (pedido.VoucherId != null)
            {
                carrinho.VoucherCodigo = pedido.Voucher.Codigo;   
            }

            foreach (var item in pedido.PedidoItems)
            {
                carrinho.Items.Add(new CarrinhoItemDTO {
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.ProdutoNome,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorUnitario * item.Quantidade,
                });
            }

            return carrinho;

        }

        public async Task<IEnumerable<PedidoDTO>?> ObterPedidosCliente(Guid clienteId)
        {
            var pedidos = await _pedidoRepository.ObterListaPorClienteId(clienteId);

            pedidos = pedidos.Where(p => p.PedidoStatus == PedidoStatus.Pago || p.PedidoStatus == PedidoStatus.Cancelado)
                .OrderByDescending(p => p.Codigo);

            if (!pedidos.Any()) return null;

            var pedidosDTO = new List<PedidoDTO>();

            foreach (var pedido in pedidos)
            {
                pedidosDTO.Add(new PedidoDTO
                {
                    ValorTotal = pedido.ValorTotal,
                    PedidoStatus = (int)pedido.PedidoStatus,
                    Codigo = pedido.Codigo,
                    DataCadastro = pedido.DataCadastro,
                });
            }

            return pedidosDTO;
        }
    }
}
