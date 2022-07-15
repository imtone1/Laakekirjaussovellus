using Microsoft.AspNetCore.Components.Forms;

namespace LaakeApp100722.Components;

public class MyInputRadioGroup<TValue> : InputRadioGroup<TValue>
{
   private string _name;
   private string _fieldClass;

   protected override void OnParametersSet()
   {
      //fielsindetifier of empty string if not fieldindentifier
      var fieldClass = EditContext?.FieldCssClass(FieldIdentifier) ?? string.Empty;
      if (fieldClass != _fieldClass || Name != _name)
      {

         _fieldClass = fieldClass;
         _name = Name;
         base.OnParametersSet();
      }
   }
}
