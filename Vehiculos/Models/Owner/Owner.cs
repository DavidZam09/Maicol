using Vehiculos.Models.Vehicle;

namespace Vehiculos.Models.Owner
{
    public class Propietarios
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string DocumentType { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<Vehicles> Vehicles { get; set; }
    }
}
