using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Cliente
    {
        public Cliente(string cpf, string nome)
        {
            Cpf = cpf;
            Nome = nome;
        }

        public string Cpf { get; private set; }
        public string Nome { get; private set; }

    }
}
