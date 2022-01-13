using Proyecto.Entities.Bases;
using Proyecto.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proyecto.Tests.Entities
{
    public class EnttyBaseTest
    {
        [Fact(DisplayName ="Base de entidad cuenta con propiedad de Id")]
        public void BaseHasIdProperyTest()
        {
            EntityBase entityBase = new EntityBase();
            bool actua = entityBase.HasProperty("Id");
            Assert.True(actua);
        }

        [Fact(DisplayName = "Base de entidad cuenta con propiedad de IsEnable")]
        public void BaseHasIsEnableProperyTest()
        {
            EntityBase entityBase = new EntityBase();
            bool actua = entityBase.HasProperty("IsEnable");
            Assert.True(actua);
        }
    }
}
