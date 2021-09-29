using System.Collections.Generic;
using System.Linq;
using System;

namespace ProyRepositorio.Helpers
{
    public class ListaPaginada<T>: List<T>
    {
        public int PagActual {get;set;}
        public int TotalPaginas{get;set;}
        public int TamPag{get;set;}
        public int TotalReg{get;set;}

        public bool TieneAnt {
            get{
                return PagActual>1;
                }
        }
        public bool TieneProx{
            get{
                return PagActual<TotalPaginas;
            }
        }
        public ListaPaginada(List<T> items,int cantidad,int nro,int tam){
            //inicializando
            TotalReg=cantidad;
            TamPag=tam;
            PagActual=nro;
            TotalPaginas=(int) Math.Ceiling(cantidad/ (double) tam );
            AddRange(items);
        }
        public static ListaPaginada<T> Paginar(IQueryable<T> entidades,int nropag,int tampag){
            var resultado=entidades.Skip((nropag -1) * tampag ).Take(tampag).ToList();
            var cantidad=entidades.Count();
            return new ListaPaginada<T>(resultado,cantidad,nropag,tampag);
        }
    }
}