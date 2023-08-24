using Microsoft.AspNetCore.Components.Forms;

namespace Bizer.AspNetCore.Components;

class BootstrapFieldCssClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any();
        if (editContext.IsModified(fieldIdentifier))
        {
            return isValid ? "modified is-valid" : "modified is-invalid";
        }
        else
        {
            return isValid ? "is-valid" : "is-invalid";
        }
    }
}
