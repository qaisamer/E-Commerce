namespace E_Commerce.Data.Repository
{
    public class Repo<T> :IRepo<T> where T : class
    {
        private readonly ApplicationDbContext db;
        public Repo(ApplicationDbContext db)
        {
            this.db = db;
        }
        public T Find_by_id(int id)
        {
            return db.Set<T>().Find(id);
        }

        public List<T> show()
        {
            List<T> items = new List<T>();
            items = db.Set<T>().ToList();
            return items;
        }

        public void add_item(T t)
        {

            db.Set<T>().Add(t);
            db.SaveChanges();
        }

        public void remove_item(T t)
        {
            db.Set<T>().Remove(t);
            db.SaveChanges();
        }

        public void update_item(T t)
        {
            db.Set<T>().Update(t);
            db.SaveChanges();
        }

        
    }
}
