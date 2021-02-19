
using System;
using System.Collections.Generic;

namespace Tim.Domain.Entities
{
    public class Lote : Entity
    {

        public Lote(DateTime dataLote, int id = 0) : base(id)
        {
            DataLote = dataLote;
           
        }

        public DateTime DataLote{ get; private set; }
    


    }
}