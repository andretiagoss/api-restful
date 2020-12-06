using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Pedido
    {
        public Pedido(Cliente cliente)
        {
            Id = Guid.NewGuid();
            DataCompra = DateTime.Now;
            Cliente = cliente;
            Status = PedidoStatus.Pendente;
            _pedidoItems = new List<PedidoItem>();
        }

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public Guid Id { get; private set; }
        public DateTime DataCompra { get; private set; }
        public decimal ValorTotal { get; private set; }
        public Cliente Cliente { get; private set; }
        public PedidoStatus Status { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public void AtualizarStatus(PedidoStatus status)
        {
            Status = status;
        }

        public void AdicionarItem(PedidoItem item)
        {
            _pedidoItems.Add(item);

            AtualizarValorTotal();
        }

        public void AtualizarItem(PedidoItem item)
        {
            var itemExistente = _pedidoItems.FirstOrDefault(a => a.Id == item.Id);
            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(item);

            AtualizarValorTotal();
        }

        public void RemoverItem(PedidoItem item)
        {
            var itemExistente = _pedidoItems.FirstOrDefault(a => a.Id == item.Id);
            _pedidoItems.Remove(itemExistente);

            AtualizarValorTotal();
        }

        public void AtualizarValorTotal()
        {
            ValorTotal = _pedidoItems.Sum(a => a.Valor);
        }
    }
}
