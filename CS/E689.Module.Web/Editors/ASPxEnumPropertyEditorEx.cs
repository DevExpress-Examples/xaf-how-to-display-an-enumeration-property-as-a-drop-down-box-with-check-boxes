using System;
using System.Data;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.Web;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;

namespace E689.Module.Web.Editors {
    [PropertyEditor(typeof(System.Enum), false)]
    public class EnumPropertyEditorEx : ASPxPropertyEditor {
        private const string LookupValueColumnName = "Value";
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
        protected override System.Web.UI.WebControls.WebControl CreateEditModeControlCore() {
            ASPxGridLookup lookupEdit = new ASPxGridLookup();
            lookupEdit.ID = PropertyName + "_CheckedComboBox";
            lookupEdit.Width = Unit.Percentage(100);
            lookupEdit.SelectionMode = GridLookupSelectionMode.Multiple;
            lookupEdit.TextChanged += EditValueChangedHandler;
            lookupEdit.KeyFieldName = LookupValueColumnName;
            lookupEdit.MultiTextSeparator = ", ";
            lookupEdit.AllowUserInput = false;
            // Set the drop-down editor width.
            lookupEdit.GridView.Width = 500;
            // OR
            //string setSizeScript = "function(s, e){s.GetGridView().SetWidth(s.GetWidth()); }";
            //lookupEdit.ClientSideEvents.Init = lookupEdit.ClientSideEvents.DropDown = setSizeScript;
            lookupEdit.GridViewProperties.Settings.ShowColumnHeaders = false;
            lookupEdit.GridViewProperties.Settings.GridLines = GridLines.None;
            lookupEdit.GridViewStyles.SelectedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#00fbfbfb");
            lookupEdit.GridViewStyles.FocusedRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#00fbfbfb");
            GridViewCommandColumn selectionColumn = new GridViewCommandColumn();
            selectionColumn.ShowSelectCheckbox = true;
            lookupEdit.Columns.Add(selectionColumn);
            GridViewDataColumn valueColumn = new GridViewDataColumn(LookupValueColumnName);
            lookupEdit.Columns.Add(valueColumn);

            lookupEdit.DataSource = GetLookupData();
            return lookupEdit;
        }
        private DataTable GetLookupData() {
            DataTable table = new DataTable();
            table.Columns.Add(LookupValueColumnName, typeof(string));
            foreach(object value in EnumDescriptor.Values) {
                if(!IsNoneValue(value)) {
                    table.Rows.Add(EnumDescriptor.GetCaption(value));
                }
            }
            return table;
        }
        public override void BreakLinksToControl(bool unwireEventsOnly) {
            ASPxGridLookup lookupEdit = Editor as ASPxGridLookup;
            if(lookupEdit != null) {
                lookupEdit.TextChanged -= EditValueChangedHandler;
            }
            base.BreakLinksToControl(unwireEventsOnly);
        }
        protected override void ReadViewModeValueCore() {
            base.ReadViewModeValueCore();
            Label viewModeControl = InplaceViewModeEditor as Label;
            if(viewModeControl != null) {
                viewModeControl.Text = ConvertToLocalizedString(viewModeControl.Text);
            }
        }
        protected override void ReadEditModeValueCore() {
            if(Editor != null) {
                Editor.Text = PropertyValue == null ? String.Empty : ConvertToLocalizedString(PropertyValue.ToString());
            }
        }
        protected override object GetControlValueCore() {
            if(Editor != null) {
                if(String.IsNullOrEmpty(Editor.Text)) {
                    return GetNoneValue();
                } else {
                    return Enum.Parse(GetUnderlyingType(), ConvertFromLocalizedString(Editor.Text));
                }
            } else {
                return base.GetControlValueCore();
            }
        }
        private string ConvertToLocalizedString(string unlocalizedString) {
            string result = unlocalizedString;
            foreach(object enumValue in EnumDescriptor.Values) {
                string localizedEnumValueCaption = EnumDescriptor.GetCaption(enumValue);
                if(!string.IsNullOrEmpty(localizedEnumValueCaption)) {
                    result = result.Replace(enumValue.ToString(), localizedEnumValueCaption);
                }
            }
            return result;
        }
        private string ConvertFromLocalizedString(string localizedString) {
            string result = localizedString;
            foreach(object enumValue in EnumDescriptor.Values) {
                string localizedEnumValueCaption = EnumDescriptor.GetCaption(enumValue);
                if(!string.IsNullOrEmpty(localizedEnumValueCaption)) {
                    result = result.Replace(localizedEnumValueCaption, enumValue.ToString());
                }
            }
            return result;
        }
        protected override void SetImmediatePostDataScript(string script) {
            Editor.ClientSideEvents.CloseUp = script;
        }
        public new ASPxGridLookup Editor {
            get {
                return (ASPxGridLookup)base.Editor;
            }
        }
        private bool IsNoneValue(object value) {
            if(value is string) return false;
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