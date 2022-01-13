using Microsoft.EntityFrameworkCore;
using Proyecto.Abstractions;
using Proyecto.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Repositories
{
    public class Crud <T> : ICrud<T>, INotifyPropertyChanged where T : class, new()
    {
        protected readonly DbContext context;
        private string _error="";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Notify([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// catch the functions error
        /// </summary>
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                Notify();
            }
        }

        /// <summary>
        /// Constructor para definir el DbContext de acceso a base de datos
        /// </summary>
        /// <param name="dbContext">Context para acceso</param>
        public Crud(PlutoContext dbContext)
        {
            context = dbContext;
        }

        /// <summary>
        /// Agrega un nuevo item ala base de datos
        /// </summary>
        /// <param name="items">arreglo de paramatros tipo <see cref="GetGenericValue"/></param>
        /// <returns>reorna True si se salvo con exito</returns>
        public virtual bool Add(params T[] items)
        {
            bool Op = false;
            try
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }
                context.SaveChanges();
                Op = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Op = false;
            }
            return Op;
        }

        /// <summary>
        /// Funcion para tomar HashSet<typeparamref Type="T"/>
        /// </summary>
        /// <param name="navigationProperties"> para incluir dependencias</param>
        /// <returns>List<typeparamref name="T"/></returns>
        public virtual HashSet<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            HashSet<T> list = new HashSet<T>();
            try
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                list = dbQuery.AsNoTracking().ToHashSet<T>();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return list;
        }

        /// <summary>
        ///  Funcion para tomar HashSet<typeparamref Type="T"/>
        /// </summary>
        /// <param name="where"> is the conditions</param>
        /// <param name="navigationProperties"> are the dependencies</param>
        /// <returns>Returns a list of T</returns>
        public virtual HashSet<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            HashSet<T> list = new HashSet<T>();
            try
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                list = dbQuery.AsNoTracking().Where(where).ToHashSet<T>();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return list;
        }

        /// <summary>
        /// Funcion para tomar Un <typeparamref Type="T"/>
        /// </summary>
        /// <param name="where">is the conditions</param>
        /// <param name="navigationProperties">are the dependencies</param>
        /// <returns>One <typeparamref name="T"/></returns>
        public virtual T GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T? item = new();
            try
            {
                IQueryable<T> dbQuery = context.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                {
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                }

                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return item == null? new(): item;
        }

        /// <summary>
        /// Elimina <typeparamref name="T"/>
        /// </summary>
        /// <param name="items">uno o mas en array <typeparamref name="T"/> para eliminar</param>
        /// <returns>true wen is done otherwise false  </returns>
        public virtual bool Remove(params T[] items)
        {
            bool Op = false;
            try
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }
                context.SaveChanges();
                Op = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Op = false;
            }
            return Op;
        }

        /// <summary>
        /// Updates <typeparamref name="T"/>
        /// </summary>
        /// <param name="items">uno o mas en array <typeparamref name="T"/> para eliminar</param>
        /// <returns>true wen is done otherwise false  </returns>
        public virtual bool Update(params T[] items)
        {
            bool Op = false;
            try
            {
                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }
                context.SaveChanges();
                Op = true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Op = false;
            }
            return Op;
        }

        /// <summary>
        /// Get the maximus value of the column selected
        /// </summary>
        /// <param name="ColumnSelected">Name of column</param>
        /// <returns>Long</returns>
        public virtual long GetMax(Func<T, int> ColumnSelected)
        {
            long Max = 0;
            try
            {
                Max = context.Set<T>().Max(ColumnSelected);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
            return Max;
        }

        /// <summary>
        /// check if exist a value on database
        /// </summary>
        /// <param name="Parametro">is the value to check</param>
        /// <returns>true wen is found otherwise false</returns>
        public virtual bool Existe(Func<T, bool> Parametro)
        {
            bool Op = false;
            try
            {
                Op = context.Set<T>().Any(Parametro);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Op = false;
            }
            return Op;
        }
    }
}
