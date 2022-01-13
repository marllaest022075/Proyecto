using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Tests.Helper
{
    public static class Help
    {
        public static bool HasProperty(this object obj, string propertyName)
        {
            var oj = obj.GetType().GetProperty(propertyName);
            return oj != null;
        }

    }
}
