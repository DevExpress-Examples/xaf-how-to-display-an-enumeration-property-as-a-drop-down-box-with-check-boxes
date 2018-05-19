using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
//using DevExpress.ExpressApp.Reports;
//using DevExpress.ExpressApp.PivotChart;
//using DevExpress.ExpressApp.Security.Strategy;
//using E689.Module.BusinessObjects;

namespace E689.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            string name = "Demo Object with a FlagsAttribute Enumeration Property";
            DemoObject theObject = ObjectSpace.FindObject<DemoObject>(CriteriaOperator.Parse("Name=?", name));
            if(theObject == null) {
                theObject = ObjectSpace.CreateObject<DemoObject>();
                theObject.Name = name;
                theObject.TestMe = TestFlagsAttributeEnum.Air | TestFlagsAttributeEnum.Water;
            }
        }
    }
}
