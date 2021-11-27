using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaDental.Modelos.Entidades
{
    public class Consulta
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoServicio { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
    }
}
