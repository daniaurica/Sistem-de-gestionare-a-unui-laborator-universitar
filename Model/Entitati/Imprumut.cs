using System;
using Model.Enumerari;

namespace Model.Entitati
{
    public class Imprumut
    {
        public int Id { get; set; }

        public int IdStudent { get; set; }

        public int IdEchipament { get; set; }

        public DateTime DataImprumut { get; set; }

        public DateTime DataReturnare { get; set; }

        public StatusImprumut Status { get; set; }
    }
}