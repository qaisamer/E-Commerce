namespace E_Commerce.Data.Repository
{
    public interface IRepo<T> where T : class
    {
        T Find_by_id(int id);
        List<T> show();
        void add_item(T t);
        void remove_item(T t);
        void update_item(T t);
    


    }
}
