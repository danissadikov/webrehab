namespace Data.Models;

/// <summary>
/// Базовая сущность бд
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    int Id { get; set; }
}