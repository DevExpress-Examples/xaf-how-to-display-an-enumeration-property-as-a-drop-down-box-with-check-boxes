Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.DC
Imports System.ComponentModel

Namespace E689.Module
    <FlagsAttribute>
    Public Enum TestFlagsAttributeEnum
        <XafDisplayName("")>
        None = 0
        <XafDisplayName("FireEx")>
        Fire = 1
        <XafDisplayName("AirEx")>
        Air = 2
        <XafDisplayName("WaterEx")>
        Water = 4
        <XafDisplayName("EarthEx")>
        Earth = 8
    End Enum
    <DefaultClassOptions> _
	Public Class DemoObject
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _Name As String
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", _Name, value)
			End Set
		End Property
		Private _TestMe As TestFlagsAttributeEnum
		Public Property TestMe() As TestFlagsAttributeEnum
			Get
				Return _TestMe
			End Get
			Set(ByVal value As TestFlagsAttributeEnum)
				SetPropertyValue("TestMe", _TestMe, value)
			End Set
		End Property
	End Class
End Namespace
