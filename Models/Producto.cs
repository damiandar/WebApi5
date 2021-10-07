using Newtonsoft.Json;

namespace ProyRepositorio.Models
{
    public class Producto : ModeloBase
    {
        
        public string Nombre{get;set;}

        public int stock { get; set; }

        public decimal Precio { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Categoria Categoria { get; set; }

        //requiere newton soft
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Vendedor Vendedor { get; set; }
    }
}