using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Abstractions
{
    public interface ICrud <T>
    {
        HashSet<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);

        HashSet<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        bool Add(params T[] items);

        bool Update(params T[] items);

        bool Remove(params T[] items);

        long GetMax(Func<T, int> ColumnSelected);

        bool Existe(Func<T, bool> Parametro);
    }
}
