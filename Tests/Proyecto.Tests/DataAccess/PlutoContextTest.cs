using Microsoft.EntityFrameworkCore;
using Proyecto.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proyecto.Tests.DataAccess
{
    public class PlutoContextTest
    {
        [Fact(DisplayName ="Comprovar herencia de DbContext")]
        public void PlutoContextHasDbContext()
        {
            var actual = typeof(PlutoContext).IsSubclassOf(typeof(DbContext));
            Assert.True(actual);
        }
    }
}
