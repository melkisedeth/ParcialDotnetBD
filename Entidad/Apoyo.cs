namespace Entidad
{
    public class Apoyo
    {
        public int IdApoyo { get; set; }
        public string modalidad { get; set; }
        public string Aporte { get; set; }
        public string fecha { get; set; }
        public Persona Persona { get; set; }
    }
}