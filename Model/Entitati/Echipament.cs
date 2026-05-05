using Model.Enumerari;

namespace Model.Entitati
{
    public class Echipament
    {
        public int Id { get; set; }
        public string Denumire { get; set; }
        public TipEchipament Tip { get; set; }
        public int NumarTotal { get; set; }
        public int NumarDisponibil { get; set; }
        public OptiuniEchipament Optiuni { get; set; }
    }
}