using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model.ViewModel
{
    public class ZakazanaListViewModel
    {
        public List<ListaZakazana> GraList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
