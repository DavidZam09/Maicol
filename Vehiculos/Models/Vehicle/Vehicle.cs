﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Vehiculos.Models.Owner;

namespace Vehiculos.Models.Vehicle
{
    public class Vehicles
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string EngineCapacity { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El campo OwnerId es obligatorio")]
        public int OwnerId { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }
        public string Status { get; set; }
        public Propietarios Owner { get; set; }
    }
}
