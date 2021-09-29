namespace ProyRepositorio.Models
{
    public class Producto : ModeloBase
    {
        
        public string Nombre{get;set;}

        public int stock { get; set; }

        public decimal Precio { get; set; }

        public Categoria Categoria { get; set; }
    }
}