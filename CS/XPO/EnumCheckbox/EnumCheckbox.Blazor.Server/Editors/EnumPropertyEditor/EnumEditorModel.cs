using DevExpress.ExpressApp.Blazor.Components.Models;

namespace EnumCheckbox.Blazor.Server.Editors.EnumPropertyEditor {
  
    public class EnumEditorModel : ComponentModelBase {
        public EnumEditorModel(List<MyEnumDescriptor> _source, string _fieldName) {
            DataSource = _source;
            FieldName = _fieldName;
        }
        public object Value {
            get => GetPropertyValue<object>();
            set => SetPropertyValue(value);
        }
        public bool ReadOnly {
            get => GetPropertyValue<bool>();
            set => SetPropertyValue(value);
        }
        public string FieldName {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        // ...
        public void SetValueFromUI(IEnumerable<object> value) {
            SetPropertyValue(value, notify: false, nameof(Value));
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ValueChanged;

        public List<MyEnumDescriptor> DataSource {
            get => GetPropertyValue<List<MyEnumDescriptor>>();
            set => SetPropertyValue(value);
        }

        public IEnumerable<object> Values {
            get => GetPropertyValue<IEnumerable<object>>();
            set => SetPropertyValue(value);
        }
    }
}
