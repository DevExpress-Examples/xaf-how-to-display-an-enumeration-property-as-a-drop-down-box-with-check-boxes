Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
'using DevExpress.ExpressApp.Reports;
'using DevExpress.ExpressApp.PivotChart;
'using DevExpress.ExpressApp.Security.Strategy;
'using E689.Module.BusinessObjects;

Namespace E689.Module.DatabaseUpdate
	' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppUpdatingModuleUpdatertopic
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim name As String = "Demo Object with a FlagsAttribute Enumeration Property"
			Dim theObject As DemoObject = ObjectSpace.FindObject(Of DemoObject)(CriteriaOperator.Parse("Name=?", name))
			If theObject Is Nothing Then
				theObject = ObjectSpace.CreateObject(Of DemoObject)()
				theObject.Name = name
				theObject.TestMe = TestFlagsAttributeEnum.Air Or TestFlagsAttributeEnum.Water
			End If
		End Sub
	End Class
End Namespace
