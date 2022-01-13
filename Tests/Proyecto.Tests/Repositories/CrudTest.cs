using Proyecto.Abstractions;
using Proyecto.Entities.Bases;
using Proyecto.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proyecto.Tests.Repositories
{
    public class CrudTest
    {
        [Fact(DisplayName ="erificar si el Crud implemeta ICrud")]
        public void CrudImplementsICrudTest()
{
            var actual = typeof(ICrud<MyTestClass>).IsAssignableFrom(typeof(Crud<MyTestClass>));
            Assert.True(actual);
        }
        class MyTestClass : EntityBase
        {

        }
    }
}
