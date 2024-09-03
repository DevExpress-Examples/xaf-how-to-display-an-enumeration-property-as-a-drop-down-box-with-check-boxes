<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128592677/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E689)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->

# XAF - How to Display an Enumeration Property as a Check List

This example demonstrates how to create a custom Property Editor that displays an enumeration property as a check list. The enumeration type must be declared with a `FlagsAttribute` (allows to treat this enumeration as a set of flags).

![image](https://user-images.githubusercontent.com/14300209/234846358-4435cbbe-130d-410b-9958-fb8450a11480.png)

> [!WARNING]
> We created this example for demonstration purposes and it is not intended to address all possible usage scenarios with it.
> If this example does not have certain functionality or you want to change its behavior, you can extend this example as needed. Please note that this can be a complex task that requires good knowledge of XAF: [UI Customization Categories by Skill Level](https://www.devexpress.com/products/net/application_framework/xaf-considerations-for-newcomers.xml#ui-customization-categories). You will likely need to research how our components work under the hood. Refer to the following help topic for more information: [Debug DevExpress .NET Source Code with PDB Symbols](https://docs.devexpress.com/GeneralInformation/403656/support-debug-troubleshooting/debug-controls-with-debug-symbols).
> We are unable to help with such tasks as custom programming is outside our Support Service scope: [Technical Support Scope](https://www.devexpress.com/products/net/application_framework/xaf-considerations-for-newcomers.xml#support).


## Implementation Details

XAF does not have a built-in Property Editor for [enumeration properties](https://docs.devexpress.com/eXpressAppFramework/113552/business-model-design-orm/data-types-supported-by-built-in-editors/enumeration-properties) that can store several values at once.  You need to implement a custom Property Editor that obtains the data property value and displays it in the UI with the help of a custom visual control. In an XAF Windows Forms application, the control is [`CheckedComboBoxEdit`](https://docs.devexpress.com/WindowsForms/DevExpress.XtraEditors.CheckedComboBoxEdit). In an XAF ASP.NET Core Blazor application, the control is [DxListBox](https://docs.devexpress.com/Blazor/DevExpress.Blazor.DxListBox-2).

## Files To Look At

* [BlazorEnumPropertyEditor](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Blazor.Server/Editors)
* [WinEnumPropertyEditor.cs](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Win/Editors/EnumPropertyEditorEx.cs) 
* [DemoObjects.cs](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Module/BusinessObjects/DemoObjects.cs)
* [TestFlagsAttributeEnum.cs](./CS/EFCore/EnumCheckBoxEF/EnumCheckBoxEF.Module/BusinessObjects/TestFlagsAttributeEnum.cs)

## Documentation

* [Custom Property Editors](https://docs.devexpress.com/eXpressAppFramework/113097/ui-construction/view-items-and-property-editors/property-editors#custom-property-editors)
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-display-an-enumeration-property-as-a-drop-down-box-with-check-boxes&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=xaf-how-to-display-an-enumeration-property-as-a-drop-down-box-with-check-boxes&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
