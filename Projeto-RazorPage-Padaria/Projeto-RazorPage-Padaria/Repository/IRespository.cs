namespace Projeto_RazorPage_Padaria.Repository
{
    public abstract class IRepository<T>
    {
        protected List<T> Data { get; set; }
        public IRepository()
        {
            this.Data = new();
        }
        public abstract T Create(T t);
        public abstract T Alter(T t);
        public abstract List<T> FindAll();

        public abstract T FindById(int id);

        public abstract void Delete(int id);



    }
}
