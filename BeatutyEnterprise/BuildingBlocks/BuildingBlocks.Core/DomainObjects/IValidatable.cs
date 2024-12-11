
using FluentValidation.Results;

namespace BuildingBlocks.Core.DomainObjects;

public interface IValidatable
{
    ValidationResult ValidationResult { get; set; }
}