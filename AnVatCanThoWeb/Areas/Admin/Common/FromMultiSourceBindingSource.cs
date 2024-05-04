using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AnVatCanThoWeb.Areas.Admin.Common;

public sealed class FromMultiSourceBindingSource : BindingSource
{
    public FromMultiSourceBindingSource() 
        : base("FromMultiSource", 
            "FromMultiSource", 
            true, 
            true)
    {
    }

    public override bool CanAcceptDataFrom(BindingSource bindingSource)
    {
        return bindingSource.Equals(ModelBinding);
    }
}