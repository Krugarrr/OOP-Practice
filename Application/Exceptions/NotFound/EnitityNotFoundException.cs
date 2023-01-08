namespace Application.Exceptions.NotFound;

public class EntityNotFoundException<T> : NotFoundException
{
    private EntityNotFoundException(string? message) : base(message) { }

    public static EntityNotFoundException<T> Create(Guid id)
        => new EntityNotFoundException<T>($"{typeof(T).Name} with id {id} was not found.");
    
    public static EntityNotFoundException<T> Create(int id)
        => new EntityNotFoundException<T>($"{typeof(T).Name} with id {id} was not found.");
    
    public static EntityNotFoundException<T> Create(string name)
        => new EntityNotFoundException<T>($"{typeof(T).Name} with name {name} was not found.");
}