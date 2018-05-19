# How to represent an enumeration property via a drop-down box with check boxes


<p><strong>Scenario:</strong></p>
<p>There is an enumeration type decorated with the FlagsAttribute, which means that an enumeration can be treated as a set of flags. There is also a property of this enumeration type inside the business class. This is helpful when several predefined enumeration values can be stored using this data property. In the UI, this data property is usually represented via an editor with multiple check boxes:</p>
<p><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-represent-an-enumeration-property-via-a-drop-down-box-with-check-boxes-e689/13.1.4+/media/511516db-df86-11e3-80b5-00155d624807.png"></p>
<br />
<p><strong>Steps To Implement:</strong></p>
<p>Since there is no <a href="https://documentation.devexpress.com/#Xaf/CustomDocument3552">standard  PropertyEditor for enumerations</a> that would allow you to store several values at once, it is common to <a href="http://documentation.devexpress.com/#Xaf/CustomDocument3097">implement custom Property Editors</a> for this task. A custom PropertyEditor will take data property value as its input and represent it in the UI using a custom visual control. There will be one PropertyEditor for WinForms (based on the <em>CheckedComboBoxEdit</em> control from the XtraEditors Suite) and one for ASP.NET (based on the <em>ASPxGridLookup</em> control from our ASPxGridView and Editors Suite), because there is no platform-agnostic way to achieve such a look and feel.</p>
<p><br />Consider following the steps below to implement and use editors from this example in your project.<br /><strong>1.</strong> Implement an enumeration type decorated with the FlagsAttribute as shown in the <em>E689.Module\BusinessObjects\DemoObjects.xx</em>  file;<br /><strong>2.</strong> Copy the <em>E689.Module.Win\Editors\EnumPropertyEditorEx.xx</em> file into <em>YourSolutionName.Module.Win</em> project;<br /><strong>3.</strong> Copy the<em> E689.Module.Web\Editors\ASPxEnumPropertyEditorEx.xx</em> file into <em>YourSolutionName.Module.Web</em> project;<br /><strong>4.</strong> Build the solution and invoke the Model Editor for the <em>ModelDesignedDiffs.xafml</em> files from <em>YourSolutionName.Module.Win </em>and<em> YourSolutionName.Module.Web</em> projects.</p>
<p><strong>5.</strong> Locate the <em>BOModel | YourClassName | Members | YourEnumerationProperty</em> node and set its <em>PropertyEditorType</em> property to the corresponding EnumPropertyEditorEx types.</p>
<p><strong> </strong></p>
<p><strong>See Also:</strong></p>
<p><a href="http://documentation.devexpress.com/#Xaf/CustomDocument3097"><u>Implement Custom Property Editors</u></a><br /><a href="https://www.devexpress.com/Support/Center/p/E444">How to represent a enumeration property via radio buttons or check boxes on the Web</a></p>

<br/>


