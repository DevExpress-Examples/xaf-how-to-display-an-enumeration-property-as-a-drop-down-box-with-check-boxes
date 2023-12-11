<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592677/23.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E689)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# XAF - How to Display an Enumeration Property as a Drop-down Box with Check Boxes

This example demonstrates how to create a custom Property Editor that displays an enumeration property as a drop-down box with check boxes, if the enumeration type has `FlagsAttribute` that allows to treat this enumeration as a set of flags.

![image](https://user-images.githubusercontent.com/14300209/234846358-4435cbbe-130d-410b-9958-fb8450a11480.png)

## Implementation Details

Since XAF does not have a built-in Property Editor for [enumeration properties](https://docs.devexpress.com/eXpressAppFramework/113552/business-model-design-orm/data-types-supported-by-built-in-editors/enumeration-properties) that can store several values at once,  you need to implement a custom Property Editor. It takes data property value as input and displays it in the UI with the help of a custom visual control. In an XAF Windows Forms application, the control is based on [`CheckedComboBoxEdit`](https://docs.devexpress.com/WindowsForms/DevExpress.XtraEditors.CheckedComboBoxEdit) editor from the XtraEditors Library. In an XAF ASP.NET Core Blazor application, it is based on the [DxListBox](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxListBox-2) control from the Blazor Library.

## Files To Look At

* [BlazorEnumPropertyEditor](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Blazor.Server/Editors)
* [WinEnumPropertyEditor.cs](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Win/Editors/EnumPropertyEditorEx.cs) 
* [DemoObjects.cs](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Module/BusinessObjects/DemoObjects.cs)
* [TestFlagsAttributeEnum.cs](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Module/BusinessObjects/TestFlagsAttributeEnum.cs)

## Documentation

* [Custom Property Editors](https://docs.devexpress.com/eXpressAppFramework/113097/ui-construction/view-items-and-property-editors/property-editors#custom-property-editors)
