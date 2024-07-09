namespace Domain.interfaces;

public interface IUpdate<in T> where T : class
{
    void Update(T entity);
}