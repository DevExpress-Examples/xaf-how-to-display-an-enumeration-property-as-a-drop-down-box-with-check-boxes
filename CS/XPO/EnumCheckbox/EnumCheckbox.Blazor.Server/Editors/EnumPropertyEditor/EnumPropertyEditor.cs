using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraRichEdit.Import.Html;
using EnumCheckboxModule.Module;
using DevExpress.Data.Helpers;
using System.Collections;
using DevExpress.ExpressApp.Blazor.Components.Models;

namespace EnumCheckbox.Blazor.Server.Editors.EnumPropertyEditor {
  

    public class MyEnumDescriptor {
        public MyEnumDescriptor(int _value,string _text) {
            Value = _value;
            Text = _text; 
        }
        public int Value { get; set; }
        public string Text { get; set; }
    }
    

    [PropertyEditor(typeof(System.Enum), false)]
    public class EnumPropertyEditor : BlazorPropertyEditorBase {
        public EnumPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        private IHasModelAdapter _adapter;
        protected override IComponentAdapter CreateComponentAdapter() {
            var tp = GetUnderlyingType();
            var resultList = GetDataSource();
            var model = new EnumEditorModel(resultList,  tp);
            Type genericEnumType = typeof(EnumAdapter<>).MakeGenericType(tp);
            _adapter = (IHasModelAdapter)Activator.CreateInstance(genericEnumType,model);
            return (IComponentAdapter)_adapter;

        }

        protected override void OnCurrentObjectChanging() {
            base.OnCurrentObjectChanging();
            if (_adapter?.ComponentModel is not null) {
                _adapter.ComponentModel.DataSource = null;
            }
        }
        protected override void OnCurrentObjectChanged() {
            base.OnCurrentObjectChanged();
            if (_adapter?.ComponentModel is not null) {
                _adapter.ComponentModel.DataSource = GetDataSource();
            }
        }

        private List<MyEnumDescriptor> GetDataSource() {
            var tp = GetUnderlyingType();
            var enumValues = Enum.GetValues(tp);
            var resultList = new List<MyEnumDescriptor>();
            foreach (var t in enumValues) {
                if ((int)t == 0) {
                    continue;
                }
                resultList.Add(new MyEnumDescriptor((int)t, t.ToString()));
            }
            return resultList;
        }
    }

    interface IHasModelAdapter {
        EnumEditorModel ComponentModel { get; }
    }
}
