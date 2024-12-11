using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BuildingBlocks.Core.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace BuildingBlocks.Core.DomainObjects;
public abstract class Entity<T> : IValidatable
{
    public T? Id { get; set; }

    [NotMapped]
    private List<Event<T>>? _notificacoes;

    [JsonIgnore]
    [NotMapped]
    public IReadOnlyCollection<Event<T>>? Notificacoes => _notificacoes?.AsReadOnly();

    [JsonIgnore]
    [NotMapped]
    public ValidationResult? ValidationResult { get; set; }

    public void AddEvent(Event<T> evento)
    {
        _notificacoes = _notificacoes ?? new List<Event<T>>();
        _notificacoes.Add(evento);
    }

    public void RemoveEvent(Event<T> eventItem)
    {
        _notificacoes?.Remove(eventItem);
    }

    public void ClearEvent()
    {
        _notificacoes?.Clear();
    }

    #region Comparations
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity<T>;

        if (ReferenceEquals(this, compareTo))
        {
            return true;
        }

        if (ReferenceEquals(null, compareTo))
        {
            return false;
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity<T> a, Entity<T> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
        {
            return true;
        }

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity<T> a, Entity<T> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }
    #endregion
    public abstract bool IsValid();

    public bool IsValid<U>(AbstractValidator<U> validation, U instance)
    {
        ValidationResult = validation.Validate(instance);
        return ValidationResult.IsValid;
    }
}