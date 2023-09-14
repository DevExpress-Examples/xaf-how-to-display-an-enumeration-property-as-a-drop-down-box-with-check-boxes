using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraRichEdit.Import.Html;
using EnumCheckboxModule.Module;
using DevExpress.Data.Helpers;
using System.Collections;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using Microsoft.AspNetCore.Components;

namespace EnumCheckbox.Blazor.Server.Editors.EnumPropertyEditor {
    [DomainComponent]  
    
    public class MyEnumDescriptor {
        public MyEnumDescriptor(int _value,string _text) {
            Value = _value;
            Text = _text; 
        }
        public int Value { get; set; }
        public string Text { get; set; }
    }

    [PropertyEditor(typeof(System.Enum), false)]
    public class MyEnumPropertyEditor : BlazorPropertyEditorBase {
        public MyEnumPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }

        protected override IComponentAdapter CreateComponentAdapter() {
            var tp = GetUnderlyingType();
            var resultList = GetDataSource();
            var model = new EnumEditorModel(resultList,  tp);
            Type genericEnumType = typeof(EnumAdapter<>).MakeGenericType(tp);
            return (IComponentAdapter)Activator.CreateInstance(genericEnumType,model);
        }
        public override EnumEditorModel ComponentModel => (Control as EnumAdapter)?.ComponentModel;

        protected override void OnCurrentObjectChanging() {
            base.OnCurrentObjectChanging();
            if (ComponentModel is not null) {
                ComponentModel.DataSource = null;
            }
        }
        protected override void OnCurrentObjectChanged() {
            base.OnCurrentObjectChanged();
            if (ComponentModel is not null) {
                ComponentModel.DataSource = GetDataSource();
            }
        }

        private List<MyEnumDescriptor> GetDataSource() {
            var tp = GetUnderlyingType();
            var enumValues = Enum.GetValues(tp);
            var resultList = new List<MyEnumDescriptor>();
            var enumDescriptor = new EnumDescriptor(GetUnderlyingType());
            foreach (var t in enumValues) {
                if ((int)t == 0) {
                    continue;
                }
                resultList.Add(new MyEnumDescriptor((int)t, GetEnumCaption((Enum)t, enumDescriptor)));
            }
            return resultList;
        }

        protected override RenderFragment CreateViewComponentCore(object dataContext) {
            var propertyValue = MemberInfo.GetValue(dataContext);
            var enumDescriptor = new EnumDescriptor(GetUnderlyingType());
            return builder => {
                builder.AddContent(0, GetEnumCaption((Enum)propertyValue, enumDescriptor));
            };
        }

        private string GetEnumCaption(Enum enumValue, EnumDescriptor enumDescriptor) {
            return string.Join(", ", enumValue.ToString().Split(',').Select(x => enumDescriptor.GetCaption(Enum.Parse(enumDescriptor.EnumType, x.Trim()))));
        }
    }
}
