#if EASYTEST

using DevExpress.ExpressApp.DC;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E689.Module.Web.Editors {
    [FlagsAttribute]
    public enum TestEnum1 {
        [XafDisplayName("")]
        None = 0,
        [XafDisplayName("Test")]
        Test = 1,
        [XafDisplayName("Value")]
        Value = 1,
    }

    [FlagsAttribute]
    public enum TestEnum2 {
        [XafDisplayName("Air/Water")]
        AirWater = 0,
        [XafDisplayName("Air/Ground")]
        AirGround = 1,
        [XafDisplayName("Air")]
        AirTest = 2,
    }


    [TestFixture]
    public class EditorTests {
        [Test]
        public void ConvertFromLocalizedStringRepeatedEnums() {
            //arrange
            var editor = new EnumPropertyEditorEx(typeof(TestEnum2),null);
            editor.enumDescriptorCore = new DevExpress.ExpressApp.Utils.EnumDescriptor(typeof(TestEnum2));
            var inputString = "Air,Air/Water";

            //act
            var resultString = editor.ConvertFromLocalizedString(inputString);
            var resultEnum = Enum.Parse(typeof(TestEnum2), resultString);
            //assert
            Assert.AreEqual(TestEnum2.AirTest | TestEnum2.AirWater, resultEnum);
        }

        [Test]
        public void ConvertFromLocalizedStringEmptyString() {
            //arrange
            var editor = new EnumPropertyEditorEx(typeof(TestEnum1), null);
            editor.enumDescriptorCore = new DevExpress.ExpressApp.Utils.EnumDescriptor(typeof(TestEnum1));
            var inputString = "Test,Value";

            //act
            var resultString = editor.ConvertFromLocalizedString(inputString);
            var resultEnum = Enum.Parse(typeof(TestEnum1), resultString);
            //assert
            Assert.AreEqual(TestEnum1.Test | TestEnum1.Value, resultEnum);
        }
    }
}

#endif