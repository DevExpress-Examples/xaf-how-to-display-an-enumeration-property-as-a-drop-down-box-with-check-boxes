using DevExpress.ExpressApp.Blazor.Components.Models;

namespace EnumCheckbox.Blazor.Server.Editors.EnumPropertyEditor {
  
    public class EnumEditorModel : ComponentModelBase {
        public EnumEditorModel(List<MyEnumDescriptor> _source,Type propertyType) {
            DataSource = _source;
            //FieldName = _fieldName;
            PropertyType = propertyType;
        }

        public Type PropertyType {
            get => GetPropertyValue<Type>();
            set => SetPropertyValue(value);
        }

        //public object Value {
        //    get => GetPropertyValue<object>();
        //    set => SetPropertyValue(value);
        //}
        public bool ReadOnly {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        //public string FieldName {
        //    get => GetPropertyValue<string>();
        //    set => SetPropertyValue(value);
        //}
        // ...
        public void SetValueFromUI(IEnumerable<int> value) {
            SetPropertyValue(value, notify: false, nameof(Values));
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ValueChanged;

        public List<MyEnumDescriptor> DataSource {
            get => GetPropertyValue<List<MyEnumDescriptor>>();
            set => SetPropertyValue(value);
        }

        public IEnumerable<int> Values {
            get => GetPropertyValue<IEnumerable<int>>();
            set => SetPropertyValue(value);
        }
    }
}
