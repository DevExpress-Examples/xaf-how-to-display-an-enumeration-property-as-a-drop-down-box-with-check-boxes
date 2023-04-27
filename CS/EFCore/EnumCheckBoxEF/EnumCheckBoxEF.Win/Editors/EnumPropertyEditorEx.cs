using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Editors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.ExpressApp.Win.Editors;
using System.Linq;

namespace EnumCheckboxModule.Module.Win.Editors {
    [PropertyEditor(typeof(System.Enum), "MyEnumPropertyEditorAlias", false)]
    public class EnumPropertyEditorEx : EnumPropertyEditor {
        private object noneValue;
        public EnumPropertyEditorEx(Type objectType, IModelMemberViewItem model)
            : base(objectType, model) {
        }
        private EnumDescriptor enumDescriptorCore = null;
        protected EnumDescriptor EnumDescriptor {
            get {
                if(enumDescriptorCore == null) {
                    enumDescriptorCore = new EnumDescriptor(GetUnderlyingType());
                }
                return enumDescriptorCore;
            }
        }
        private bool TypeHasFlagsAttribute() {
            return GetUnderlyingType().GetCustomAttributes(typeof(FlagsAttribute), true).Length > 0;
        }
        protected override object CreateControlCore() {
            CheckedComboBoxEdit checkedEdit = new CheckedComboBoxEdit();
			checkedEdit.Properties.ForceUpdateEditValue = DevExpress.Utils.DefaultBoolean.True;
            if(TypeHasFlagsAttribute()) {
                return checkedEdit;
            }
            return base.CreateControlCore();
        }
        protected override RepositoryItem CreateRepositoryItem() {
            if(TypeHasFlagsAttribute()) {
                return new RepositoryItemCheckedComboBoxEdit();
            }
            return base.CreateRepositoryItem();
        }
        protected override void SetupRepositoryItem(RepositoryItem item) {
            base.SetupRepositoryItem(item);
            if(TypeHasFlagsAttribute()) {
                RepositoryItemCheckedComboBoxEdit checkedItem = ((RepositoryItemCheckedComboBoxEdit)item);
                checkedItem.BeginUpdate();
                noneValue = GetNoneValue();
                checkedItem.SetFlags(GetUnderlyingType());
                //Dennis: this is required to show localized items in the editor.
                foreach(CheckedListBoxItem itm in checkedItem.Items) {
                    itm.Description = EnumDescriptor.GetCaption(itm.Value);
                }
                checkedItem.EndUpdate();
                checkedItem.ParseEditValue += checkedEdit_ParseEditValue;
                checkedItem.CustomDisplayText += checkedItem_CustomDisplayText;
                checkedItem.Disposed += checkedItem_Disposed;
            }
        }
        void checkedItem_Disposed(object sender, EventArgs e) {
            RepositoryItemCheckedComboBoxEdit checkedItem = (RepositoryItemCheckedComboBoxEdit)sender;
            checkedItem.ParseEditValue -= checkedEdit_ParseEditValue;
            checkedItem.CustomDisplayText -= checkedItem_CustomDisplayText;
            checkedItem.Disposed -= checkedItem_Disposed;
        }
        private void checkedEdit_ParseEditValue(object sender, ConvertEditValueEventArgs e) {
            if(string.IsNullOrEmpty(Convert.ToString(e.Value))) {
                e.Value = noneValue;
                e.Handled = true;
            }
        }
        private void checkedItem_CustomDisplayText(object sender, CustomDisplayTextEventArgs e) {
            if(EnumDescriptor == null)
                return;
            e.DisplayText = GetCaption((Enum)e.Value);
        }
        public string GetCaption(Enum enumValue) {
            if(EnumDescriptor == null || enumValue == null)
                return string.Empty;
            return string.Join(", ", enumValue.ToString().Split(',').Select(x => EnumDescriptor.GetCaption(Enum.Parse(EnumDescriptor.EnumType, x.Trim()))));
        }
        private bool IsNoneValue(object value) {
            if(value is string) {
                return false;
            }
            int result = int.MinValue;
            try {
                result = Convert.ToInt32(value);
            }
            catch { }
            return 0.Equals(result);
        }
        private object GetNoneValue() {
            return Enum.ToObject(GetUnderlyingType(), 0);
        }
    }
}