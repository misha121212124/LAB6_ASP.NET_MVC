using lab6.Models;
using System.Collections.Generic;
using System.Linq;


namespace lab6.Repositories
{
    public abstract class MainRepository<T> where T : class
    {
        static private BdContext context;// = new BdContext();

        static public BdContext GetContext {
            //return context;
            get{
                if (context == null)
                    context = MainRepository<SuperEntity>.GetContext;//new BdContext();
                return context;// = context ?? new BdContext();
            }
            set{
                context = value;
            }
        }
        public void SaveChanges()
        {
            GetContext.SaveChanges();
            //context = null;
        }

        public abstract void Insert(T item);
        public abstract T GetById(int id);
        public abstract List<T> GetAll();
        public abstract void Remove(T item);
        public abstract void Update(T item);
    }
}
