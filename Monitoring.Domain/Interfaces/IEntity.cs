namespace Monitoring.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сущности
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        object Id { get; }
    }

    /// <summary>
    /// Интерфейс сущности
    /// </summary>
    public interface IEntity<out T>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        T Id { get; }
    }
}
