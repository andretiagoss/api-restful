using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class PedidoRepository : IPedidoRepository
    {
        public IEnumerable<Pedido> Obter()
        {
            List<Pedido> lista = new List<Pedido>();

            var pedido1 = new Pedido(new Cliente("47981699193", "André Silva"));

            pedido1.AdicionarItem(new PedidoItem(Guid.Parse("db908266-5d03-4ef1-879a-a1371ad1cea3"), "Produto 1", 1, new decimal(1.99)));
            pedido1.AdicionarItem(new PedidoItem(Guid.Parse("50efd159-1533-4a06-9b33-ec0b83ba243d"), "Produto 2", 1, new decimal(2.99)));
            pedido1.AdicionarItem(new PedidoItem(Guid.Parse("482218f1-f0d8-4df9-95f8-1135cb9a387b"), "Produto 3", 1, new decimal(2.99)));

            lista.Add(pedido1);

            var pedido2 = new Pedido(new Cliente("16724311748", "Aline Silva"));

            pedido2.AdicionarItem(new PedidoItem(Guid.Parse("ea278380-52b6-4480-bc11-19f4b9d434d5"), "Produto 4", 1, new decimal(3.99)));
            pedido2.AdicionarItem(new PedidoItem(Guid.Parse("e5726e6b-2b1f-4273-9483-46860ee8700a"), "Produto 5", 1, new decimal(4.99)));
            pedido2.AdicionarItem(new PedidoItem(Guid.Parse("e890b99b-aa92-4e06-ab18-ceef478b50d4"), "Produto 6", 1, new decimal(5.99)));

            lista.Add(pedido2);

            return lista;
        }

        public Pedido Obter(Guid id)
        {
            var pedido = new Pedido(new Cliente("47981699193", "André Silva"));

            pedido.AdicionarItem(new PedidoItem(Guid.Parse("db908266-5d03-4ef1-879a-a1371ad1cea3"), "Produto 1", 1, new decimal(1.99)));
            pedido.AdicionarItem(new PedidoItem(Guid.Parse("50efd159-1533-4a06-9b33-ec0b83ba243d"), "Produto 2", 1, new decimal(2.99)));
            pedido.AdicionarItem(new PedidoItem(Guid.Parse("482218f1-f0d8-4df9-95f8-1135cb9a387b"), "Produto 3", 1, new decimal(2.99)));

            return pedido;
        }

        public bool Atualizar(Pedido pedido)
        {
            return true;
        }

        public bool Criar(Pedido pedido)
        {
            return true;
        }

        public bool Deletar(Guid id)
        {
            return true;
        }

        public bool AdicionarItem(PedidoItem item)
        {
            return true;
        }

        public bool AtualizarItem(PedidoItem item)
        {
            return true;
        }

        public bool DeletarItem(PedidoItem item)
        {
            return true;
        }
    }
}
