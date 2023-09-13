<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592677/22.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E689)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [BlazorEnumPropertyEditor](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Blazor.Server/Editors)
* [WinEnumPropertyEditor.cs](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Win/Editors/EnumPropertyEditorEx.cs) 
* [DemoObjects.cs](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Module/BusinessObjects/DemoObjects.cs)
* [TestFlagsAttributeEnum.cs](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Module/BusinessObjects/TestFlagsAttributeEnum.cs)
<!-- default file list end -->
# How to represent an enumeration property via a drop-down box with check boxes


<p><strong>Scenario:</strong></p>
<p>There is an enumeration type decorated with the FlagsAttribute, which means that an enumeration can be treated as a set of flags. There is also a property of this enumeration type inside the business class. This is helpful when several predefined enumeration values can be stored using this data property. This example shows how to create an editor to represent such property via an editor with multiple check boxes:</p>

![image](https://user-images.githubusercontent.com/14300209/234846358-4435cbbe-130d-410b-9958-fb8450a11480.png)

Steps To Implement:
Since there is no <a href="https://documentation.devexpress.com/#Xaf/CustomDocument3552">standard  PropertyEditor for enumerations</a> that would allow you to store several values at once, it is common to <a href="http://documentation.devexpress.com/#Xaf/CustomDocument3097">implement custom Property Editors</a> for this task. A custom PropertyEditor will take data property value as its input and represent it in the UI using a custom visual control. There will be one PropertyEditor for WinForms (based on the <em>CheckedComboBoxEdit</em> control from the XtraEditors Suite) and one for Blazor (based on the [DxListBox](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxListBox-2) control from our Blazor Suite), because there is no platform-agnostic way to achieve such a look and feel.

<p><a href="http://documentation.devexpress.com/#Xaf/CustomDocument3097"><u>Implement Custom Property Editors</u></a>

<br/>


