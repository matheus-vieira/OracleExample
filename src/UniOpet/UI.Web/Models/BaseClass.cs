using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Web.Models
{
    public abstract class BaseClass
    {
        public abstract string RouteUrl { get; }
        public object Id { get; set; }
    }
}
