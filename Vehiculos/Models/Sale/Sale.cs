using Vehiculos.Models.Vehicle;
using Vehiculos.Models.Client;

namespace Vehiculos.Models.Sale
{
    public class Sales
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int ClientId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }
        public Vehicles Vehicle { get; set; }
        public Clients Client { get; set; }
    }
}
