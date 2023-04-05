using DevExpress.Blazor.Internal;
using DevExpress.Drawing.Internal.Fonts.Interop;
using DevExpress.ExpressApp.Blazor.Components;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using EnumCheckboxModule.Module;
using Microsoft.AspNetCore.Components;
using System;

namespace EnumCheckbox.Blazor.Server.Editors.EnumPropertyEditor {

    public class EnumAdapter<T> : ComponentAdapterBase, IHasModelAdapter where T : Enum {


        public EnumAdapter(EnumEditorModel componentModel) {
            ComponentModel = componentModel ?? throw new ArgumentNullException(nameof(componentModel));
            ComponentModel.ValueChanged += ComponentModel_ValueChanged;
        }

        public EnumEditorModel ComponentModel { get; }
        public override void SetAllowEdit(bool allowEdit) {
            ComponentModel.ReadOnly = !allowEdit;
        }
        public override object GetValue() {

            int intValue = 0;

            foreach (int val in ComponentModel.Values) {
                intValue |= val;
            }
            var enumResult = Enum.ToObject(typeof(T), intValue);

            return enumResult;

            //TestFlagsAttributeEnum var=TestFlagsAttributeEnum.None;
            //foreach (int val in ComponentModel.Values) {
            //    var |= (TestFlagsAttributeEnum)val;
            //}
            //return var;

        }
        public override void SetValue(object value) {
      
            var en = (Enum)value;

            var names = Enum.GetNames(ComponentModel.PropertyType);
            var res = new List<int>();
            foreach (var name in names) {
                var item = (T)Enum.Parse(ComponentModel.PropertyType, name);
                var tmp = (int)Convert.ChangeType(item, typeof(int));
                if (tmp == 0) {
                    continue;
                }

                if (en.HasFlag(item)) {
                    

                    res.Add(tmp);
                    Console.WriteLine("Enum has " + name);
                }
            }

            ComponentModel.Values = res;
        }
        protected override RenderFragment CreateComponent() {
            return ComponentModelObserver.Create(ComponentModel, EnumRenderer.Create(ComponentModel));
        }
        private void ComponentModel_ValueChanged(object sender, EventArgs e) => RaiseValueChanged();
        public override void SetAllowNull(bool allowNull) { /* ...*/ }
        public override void SetDisplayFormat(string displayFormat) { /* ...*/ }
        public override void SetEditMask(string editMask) { /* ...*/ }
        public override void SetEditMaskType(EditMaskType editMaskType) { /* ...*/ }
        public override void SetErrorIcon(ImageInfo errorIcon) { /* ...*/ }
        public override void SetErrorMessage(string errorMessage) { /* ...*/ }
        public override void SetIsPassword(bool isPassword) { /* ...*/ }
        public override void SetMaxLength(int maxLength) { /* ...*/ }
        public override void SetNullText(string nullText) { /* ...*/ }
    }
}
