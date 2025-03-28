using Vehiculos.Models.Vehicle;
using Vehiculos.Models.Supplier;

namespace Vehiculos.Models.Purchase
{
    public class Purchases
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public Vehicles Vehicle { get; set; }
        public Suppliers Supplier { get; set; }
    }
}
