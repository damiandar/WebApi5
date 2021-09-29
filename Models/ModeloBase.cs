using System.Collections.Generic;

namespace ProyRepositorio.Models
{
    public abstract class ModeloBase
    {
        public int Id {get;set;}

        //public  List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }

    public class LinkDto
    {
        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string Method { get; private set; }

        public LinkDto(string href, string rel, string method)
        {
            this.Href = href;
            this.Rel = rel;
            this.Method = method;
        }
    }

}