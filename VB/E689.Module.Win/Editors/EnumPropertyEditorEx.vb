Imports DevExpress.XtraEditors
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.ExpressApp.Win.Editors
Imports System.Linq

Namespace E689.Module.Win.Editors
    <PropertyEditor(GetType(System.Enum), False)>
    Public Class EnumPropertyEditorEx
        Inherits EnumPropertyEditor
        Private noneValue As Object
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
        Private Function TypeHasFlagsAttribute() As Boolean
            Return GetUnderlyingType().GetCustomAttributes(GetType(FlagsAttribute), True).Length > 0
        End Function
        Protected Overrides Function CreateControlCore() As Object
            Dim checkedEdit As New CheckedComboBoxEdit()
            If TypeHasFlagsAttribute() Then
                Return checkedEdit
            End If
            Return MyBase.CreateControlCore()
        End Function
        Protected Overrides Function CreateRepositoryItem() As RepositoryItem
            If TypeHasFlagsAttribute() Then
                Return New RepositoryItemCheckedComboBoxEdit()
            End If
            Return MyBase.CreateRepositoryItem()
        End Function
        Protected Overrides Sub SetupRepositoryItem(ByVal item As RepositoryItem)
            MyBase.SetupRepositoryItem(item)
            If TypeHasFlagsAttribute() Then
                Dim checkedItem As RepositoryItemCheckedComboBoxEdit = (CType(item, RepositoryItemCheckedComboBoxEdit))
                checkedItem.BeginUpdate()
                noneValue = GetNoneValue()
                checkedItem.SetFlags(GetUnderlyingType())
                'Dennis: this is required to show localized items in the editor.
                For Each itm As CheckedListBoxItem In checkedItem.Items
                    itm.Description = EnumDescriptor.GetCaption(itm.Value)
                Next itm
                checkedItem.EndUpdate()
                AddHandler checkedItem.ParseEditValue, AddressOf checkedEdit_ParseEditValue
                AddHandler checkedItem.CustomDisplayText, AddressOf checkedItem_CustomDisplayText
                AddHandler checkedItem.Disposed, AddressOf checkedItem_Disposed
            End If
        End Sub
        Private Sub checkedItem_Disposed(ByVal sender As Object, ByVal e As EventArgs)
            Dim checkedItem As RepositoryItemCheckedComboBoxEdit = CType(sender, RepositoryItemCheckedComboBoxEdit)
            RemoveHandler checkedItem.ParseEditValue, AddressOf checkedEdit_ParseEditValue
            RemoveHandler checkedItem.CustomDisplayText, AddressOf checkedItem_CustomDisplayText
            RemoveHandler checkedItem.Disposed, AddressOf checkedItem_Disposed
        End Sub
        Private Sub checkedEdit_ParseEditValue(ByVal sender As Object, ByVal e As ConvertEditValueEventArgs)
            If String.IsNullOrEmpty(Convert.ToString(e.Value)) Then
                e.Value = noneValue
                e.Handled = True
            End If
        End Sub
        Private Sub checkedItem_CustomDisplayText(ByVal sender As Object, ByVal e As CustomDisplayTextEventArgs)
            If EnumDescriptor Is Nothing Then
                Return
            End If
            e.DisplayText = GetCaption(CType(e.Value, [Enum]))
        End Sub
        Public Function GetCaption(ByVal enumValue As System.Enum) As String
            If EnumDescriptor Is Nothing OrElse enumValue Is Nothing Then
                Return String.Empty
            End If
            Return String.Join(", ", enumValue.ToString().Split(","c).Select(Function(x) EnumDescriptor.GetCaption(System.Enum.Parse(EnumDescriptor.EnumType, x.Trim()))))
        End Function
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