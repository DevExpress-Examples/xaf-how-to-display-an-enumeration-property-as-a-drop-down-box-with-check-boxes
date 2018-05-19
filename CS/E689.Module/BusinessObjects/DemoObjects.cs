using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;

namespace E689.Module {
    [FlagsAttribute]
    public enum TestFlagsAttributeEnum {
        [XafDisplayName("")]
        None = 0,
        [XafDisplayName("FireEx")]
        Fire = 1,
        [XafDisplayName("AirEx")]
        Air = 2,
        [XafDisplayName("WaterEx")]
        Water = 4,
        [XafDisplayName("EarthEx")]
        Earth = 8
    }

    [DefaultClassOptions]
    public class DemoObject : BaseObject {
        public DemoObject(Session session) : base(session) { }
        private string _Name;
        public string Name {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }
        private TestFlagsAttributeEnum _TestMe;
        public TestFlagsAttributeEnum TestMe {
            get { return _TestMe; }
            set { SetPropertyValue("TestMe", ref _TestMe, value); }
        }
    }
}
