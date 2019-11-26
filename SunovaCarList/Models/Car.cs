using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SunovaCarList.Models
{
    public class Car
    {
        public int Id { get; set; }
        public int Mileage { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public string Color { get; set; }

    }
}