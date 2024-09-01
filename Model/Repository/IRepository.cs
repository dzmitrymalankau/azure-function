namespace Model.Repository;

public interface IRepository<out T>
{
    IEnumerable<T> Get(string param);
}