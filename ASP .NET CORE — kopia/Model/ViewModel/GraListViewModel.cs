using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model.ViewModel
{
    public class GraListViewModel
    {
        public List<Gra> GraList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
