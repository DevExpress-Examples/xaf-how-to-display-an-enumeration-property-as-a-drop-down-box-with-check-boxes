using DevExpress.ExpressApp.DC;

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
}
