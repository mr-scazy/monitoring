namespace Monitoring.Domain.Services
{
    /// <summary>
    /// Интерфейс с идентификатором
    /// </summary>
    public interface IHasId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        object Id { get; }
    }

    /// <summary>
    /// Интерфейс с идентификатором
    /// </summary>
    public interface IHasId<out T>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        T Id { get; }
    }
}
