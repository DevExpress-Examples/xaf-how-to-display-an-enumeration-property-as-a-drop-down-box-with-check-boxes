Imports System.Web.UI.WebControls
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.Web
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Web.Editors.ASPx

Namespace E689.Module.Web.Editors
	<PropertyEditor(GetType(System.Enum), False)> _
	Public Class EnumPropertyEditorEx
		Inherits ASPxPropertyEditor
		Private Const LookupValueColumnName As String = "Value"
		Public Sub New(ByVal objectType As Type, ByVal model As IModelMemberViewItem)
			MyBase.New(objectType, model)
		End Sub
		Private enumDescriptorCore As EnumDescriptor = Nothing
		Protected ReadOnly Property EnumDescriptor() As EnumDescriptor
			Get
				If enumDescriptorCore Is Nothing Then
					enumDescriptorCore = New EnumDescriptor(GetUnderlyingType())
				End If
				Return enumDescriptorCore
			End Get
		End Property
		Protected Overrides Function CreateEditModeControlCore() As System.Web.UI.WebControls.WebControl
			Dim lookupEdit As New ASPxGridLookup()
			lookupEdit.ID = PropertyName & "_CheckedComboBox"
			lookupEdit.Width = Unit.Percentage(100)
			lookupEdit.SelectionMode = GridLookupSelectionMode.Multiple
			AddHandler lookupEdit.TextChanged, AddressOf EditValueChangedHandler
			lookupEdit.KeyFieldName = LookupValueColumnName
			lookupEdit.MultiTextSeparator = ", "
			lookupEdit.AllowUserInput = False
			' Set the drop-down editor width.
			lookupEdit.GridView.Width = 500
			' OR
			'string setSizeScript = "function(s, e){s.GetGridView().SetWidth(s.GetWidth()); }";
			'lookupEdit.ClientSideEvents.Init = lookupEdit.ClientSideEvents.DropDown = setSizeScript;
			lookupEdit.GridViewProperties.Settings.ShowColumnHeaders = False
			lookupEdit.GridViewProperties.Settings.GridLines = GridLines.None
			lookupEdit.GridViewStyles.SelectedRow.BackColor = ColorTranslator.FromHtml("#00fbfbfb")
			lookupEdit.GridViewStyles.FocusedRow.BackColor = ColorTranslator.FromHtml("#00fbfbfb")
			Dim selectionColumn As New GridViewCommandColumn()
			selectionColumn.ShowSelectCheckbox = True
			lookupEdit.Columns.Add(selectionColumn)
			Dim valueColumn As New GridViewDataColumn(LookupValueColumnName)
			lookupEdit.Columns.Add(valueColumn)

			lookupEdit.DataSource = GetLookupData()
			Return lookupEdit
		End Function
		Private Function GetLookupData() As DataTable
			Dim table As New DataTable()
			table.Columns.Add(LookupValueColumnName, GetType(String))
			For Each value As Object In EnumDescriptor.Values
				If Not IsNoneValue(value) Then
					table.Rows.Add(EnumDescriptor.GetCaption(value))
				End If
			Next value
			Return table
		End Function
		Public Overrides Sub BreakLinksToControl(ByVal unwireEventsOnly As Boolean)
			Dim lookupEdit As ASPxGridLookup = TryCast(Editor, ASPxGridLookup)
			If lookupEdit IsNot Nothing Then
				RemoveHandler lookupEdit.TextChanged, AddressOf EditValueChangedHandler
			End If
			MyBase.BreakLinksToControl(unwireEventsOnly)
		End Sub
		Protected Overrides Sub ReadViewModeValueCore()
			MyBase.ReadViewModeValueCore()
			Dim viewModeControl As Label = TryCast(InplaceViewModeEditor, Label)
			If viewModeControl IsNot Nothing Then
				viewModeControl.Text = ConvertToLocalizedString(viewModeControl.Text)
			End If
		End Sub
		Protected Overrides Sub ReadEditModeValueCore()
			If Editor IsNot Nothing Then
				Editor.Text = If(PropertyValue Is Nothing, String.Empty, ConvertToLocalizedString(PropertyValue.ToString()))
			End If
		End Sub
		Protected Overrides Function GetControlValueCore() As Object
			If Editor IsNot Nothing Then
				If String.IsNullOrEmpty(Editor.Text) Then
					Return GetNoneValue()
				Else
					Return System.Enum.Parse(GetUnderlyingType(), ConvertFromLocalizedString(Editor.Text))
				End If
			Else
				Return MyBase.GetControlValueCore()
			End If
		End Function
		Private Function ConvertToLocalizedString(ByVal unlocalizedString As String) As String
			Dim result As String = unlocalizedString
			For Each enumValue As Object In EnumDescriptor.Values
				Dim localizedEnumValueCaption As String = EnumDescriptor.GetCaption(enumValue)
				result = result.Replace(enumValue.ToString(), localizedEnumValueCaption)
			Next enumValue
			Return result
		End Function
		Private Function ConvertFromLocalizedString(ByVal localizedString As String) As String
			Dim result As String = localizedString
			For Each enumValue As Object In EnumDescriptor.Values
				Dim localizedEnumValueCaption As String = EnumDescriptor.GetCaption(enumValue)
				result = result.Replace(localizedEnumValueCaption, enumValue.ToString())
			Next enumValue
			Return result
		End Function
		Protected Overrides Sub SetImmediatePostDataScript(ByVal script As String)
			Editor.ClientSideEvents.CloseUp = script
		End Sub
		Public Shadows ReadOnly Property Editor() As ASPxGridLookup
			Get
				Return CType(MyBase.Editor, ASPxGridLookup)
			End Get
		End Property
		Private Function IsNoneValue(ByVal value As Object) As Boolean
			If TypeOf value Is String Then
				Return False
			End If
			Dim result As Integer = Integer.MinValue
			Try
				result = Convert.ToInt32(value)
			Catch
			End Try
			Return 0.Equals(result)
		End Function
		Private Function GetNoneValue() As Object
			Return System.Enum.ToObject(GetUnderlyingType(), 0)
		End Function
	End Class
End Namespace