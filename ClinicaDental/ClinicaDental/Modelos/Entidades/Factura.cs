using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaDental.Modelos.Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public int IdConsulta { get; set; }
        public decimal SubTotal {get;set;}
        public decimal Descuento{get;set;}
        public decimal ISV{get;set;}
        public decimal Total{get;set;}
}
}
