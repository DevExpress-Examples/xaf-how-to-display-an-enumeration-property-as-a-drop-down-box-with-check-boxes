using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp;
using EnumCheckboxModule.Module;
using EnumCheckbox.Blazor.Server.Editors.EnumPropertyEditor;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Editors;

namespace EnumCheckbox.Blazor.Server.Controllers {
    public partial class DateEditCalendarController : ObjectViewController<DetailView, DemoObject> {
        public DateEditCalendarController() {
            
        }
        protected override void OnActivated() {
            base.OnActivated();
            //Access the Birthday property editor settings
            View.CustomizeViewItemControl<MyEnumPropertyEditor>(this, SetCalendarView, nameof(DemoObject.TestMe));
        }
        private void SetCalendarView(MyEnumPropertyEditor propertyEditor) {
            //Obtain the Component Adapter
            var dateEditAdapter = (IHasModelAdapter)propertyEditor.Control;
            //Set the date picker display mode to scroll picker
            var tst = dateEditAdapter.ComponentModel.Values;
        }
    }
    public class CustomBlazorController : ObjectViewController<DetailView, DemoObject> {
        public CustomBlazorController() {
            var myAction1 = new SimpleAction(this, "MyBlazorAction1", null);
            myAction1.Execute += MyAction1_Execute;

        }

        private void MyAction1_Execute(object sender, SimpleActionExecuteEventArgs e) {
            //var os = Application.CreateObjectSpace(typeof(MyNonPersistentClass));
            //var obj = os.CreateObject<MyNonPersistentClass>();
            //var detailView = Application.CreateDetailView(os, obj);
            var it = View.FindItem(nameof(DemoObject.TestMe)) as MyEnumPropertyEditor;
            var dateEditAdapter = (IHasModelAdapter)it.Control;
            //Set the date picker display mode to scroll picker
            var tst = dateEditAdapter.ComponentModel.Values;

        }

        protected override void OnActivated() {
            base.OnActivated();
            var cnt = Frame.GetController<NewObjectViewController>();
            if (cnt != null) {

            }
        }
        protected override void OnViewControlsCreated() {
            base.OnViewControlsCreated();
            //if (View.Editor is DxGridListEditor gridListEditor) {
            //           IDxGridAdapter dataGridAdapter = gridListEditor.GetGridAdapter();
            //           dataGridAdapter.GridModel.ColumnResizeMode = DevExpress.Blazor.GridColumnResizeMode.ColumnsContainer;
            //       }
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
        }
    }
}
