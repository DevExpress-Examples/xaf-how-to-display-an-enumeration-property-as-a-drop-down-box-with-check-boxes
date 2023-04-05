using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.DC;
using System.ComponentModel;

namespace EnumCheckboxModule.Module {
    [FlagsAttribute]
    public enum TestFlagsAttributeEnum {
        [XafDisplayName("")]
        None = 0,
        [XafDisplayName("Air/Water")]
        AirWater = 1,
        [XafDisplayName("Air/Ground")]
        AirGround = 2,
        [XafDisplayName("Air")]
        AirTest = 4,
    }

    [DefaultClassOptions]
    public class DemoObject : BaseObject {
        public DemoObject(Session session) : base(session) { }
        private string _Name;
        public string Name {
            get { return _Name; }
            set { SetPropertyValue(nameof(Name), ref _Name, value); }
        }
        private TestFlagsAttributeEnum _TestMe;
        public TestFlagsAttributeEnum TestMe {
            get { return _TestMe; }
            set { SetPropertyValue(nameof(TestMe), ref _TestMe, value); }
        }
    }
}
