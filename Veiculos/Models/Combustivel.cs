using System;

namespace Veiculos.Models
{
    public class Combustivel
    {

        public virtual int Id { get; set; }

        public virtual String Descricao { get; set; }

        public virtual decimal Preco { get; set; }

        public virtual void Atualiza(Combustivel combustivel)
        {
            Descricao = combustivel.Descricao;
            Preco = combustivel.Preco;
        }
    }
}