using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

namespace EnumCheckbox.Blazor.Server.Editors.EnumPropertyEditor {
  
    [PropertyEditor(typeof(System.Enum), false)]
    public class EnumPropertyEditor : BlazorPropertyEditorBase {
        public EnumPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override IComponentAdapter CreateComponentAdapter() => new EnumAdapter(new EnumEditorModel());
    }
}
