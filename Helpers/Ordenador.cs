using System.Linq;
using System.Reflection;
using System.Text;
using System;
using System.Linq.Dynamic.Core;

namespace ProyRepositorio.Helpers
{
    public class Ordenador<T>
    {
            //nombre,
            //precio,
            //marca 
        public static IQueryable<T> Ordenar(IQueryable<T> entidades,string CampoOrdenamiento){
            if (!entidades.Any())
                return entidades;
            if (String.IsNullOrEmpty(CampoOrdenamiento))
                return entidades;

            var parametros= CampoOrdenamiento.Trim().Split(',');
            var infopropiedades= typeof(T).GetProperties(BindingFlags.Public |BindingFlags.Instance);
            var BuilderOrden= new StringBuilder();

            foreach( var par in parametros){
                if (string.IsNullOrEmpty(par))
                    continue;
                var propQuery=par.Split(" ")[0];
                var objprop= infopropiedades.FirstOrDefault(x=>x.Name.Equals(propQuery));
                if (objprop == null)
                    continue;
                var ordenAscDes = par.EndsWith(" desc") ? "descending" : "ascending";
                BuilderOrden.Append($"{objprop.Name} {ordenAscDes} ,");
            }

            var consultaOrden=BuilderOrden.ToString().TrimEnd(',',' ');
            return entidades.OrderBy(consultaOrden);
            
        }
        
    }
}