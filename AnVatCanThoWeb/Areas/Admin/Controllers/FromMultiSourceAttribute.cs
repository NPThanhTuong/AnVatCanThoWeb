using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnVatCanThoWeb.Areas.Admin.Controllers;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field)]
public sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource? BindingSource { get; } = new FromMultiSourceBindingSource();
}