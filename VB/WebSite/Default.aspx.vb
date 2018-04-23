﻿Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Xpo
Imports DevExpress.Web

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private session As Session = XpoHelper.GetNewSession()

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		xds.Session = session
	End Sub
	Protected Sub chk_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim chk As ASPxCheckBox = TryCast(sender, ASPxCheckBox)
		Dim container As GridViewDataItemTemplateContainer = TryCast(chk.NamingContainer, GridViewDataItemTemplateContainer)

		chk.ClientSideEvents.CheckedChanged = String.Format("function (s, e) {{cb.PerformCallback('{0}|' + s.GetValue()); }}", container.KeyValue)
	End Sub
	Protected Sub cb_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgs)
		Dim p() As String = e.Parameter.Split("|"c)

		Dim obj As MyObject = session.GetObjectByKey(Of MyObject)(Convert.ToInt32(p(0))) ' get the record from the Session
		obj.Active = Convert.ToInt32(p(1))

		obj.Save()
	End Sub
End Class