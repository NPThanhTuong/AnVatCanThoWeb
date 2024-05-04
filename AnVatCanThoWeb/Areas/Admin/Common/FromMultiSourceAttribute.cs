using AnVatCanThoWeb.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnVatCanThoWeb.Areas.Admin.Common;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field)]
public sealed class FromMultiSourceAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource? BindingSource { get; } = new FromMultiSourceBindingSource();
}