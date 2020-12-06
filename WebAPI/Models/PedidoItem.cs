using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class PedidoItem
    {
        public PedidoItem(Guid id, string nome, int quantidade, decimal valor)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            Valor = valor;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Valor { get; private set; }

    }
}
