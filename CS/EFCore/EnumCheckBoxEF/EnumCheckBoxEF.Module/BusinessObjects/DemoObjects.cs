using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.BaseImpl.EF;

namespace EnumCheckboxModule.Module {

    [DefaultClassOptions]
    public class DemoObject : BaseObject {
        public virtual string Name { get; set; }
        [EditorAlias("MyEnumPropertyEditorAlias")]
        public virtual TestFlagsAttributeEnum TestMe { get; set; }
    }
}
