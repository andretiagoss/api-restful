using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public interface IPedidoRepository
    {
        IEnumerable<Pedido> Obter();
        Pedido Obter(Guid id);
        bool Criar(Pedido pedido);
        bool Atualizar(Pedido pedido);
        bool Deletar(Guid id);

        bool AdicionarItem(PedidoItem item);
        bool AtualizarItem(PedidoItem item);
        bool DeletarItem(PedidoItem item);
    }
}
