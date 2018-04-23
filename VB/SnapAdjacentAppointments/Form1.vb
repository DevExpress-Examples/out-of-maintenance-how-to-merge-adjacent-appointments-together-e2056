Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler

Namespace MergeAdjacentAppointments
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
			Me.schedulerControl1.Start = DateTime.Now.Date
			CreateAppointments()
		End Sub

		Private Sub CreateAppointments()
			schedulerStorage1.BeginUpdate()
			For i As Integer = 0 To 1
				Dim apt As Appointment = schedulerStorage1.CreateAppointment(AppointmentType.Normal)
				apt.Subject = "Subject" & i.ToString()
				apt.Description = "Description" & i.ToString()
				apt.Start = DateTime.Now.Date.AddHours(i + 10)
				apt.End = DateTime.Now.Date.AddHours(i + 10.5f)
				schedulerStorage1.Appointments.Add(apt)
			Next i
			schedulerStorage1.EndUpdate()
		End Sub

		Private Sub schedulerControl1_AppointmentDrop(ByVal sender As Object, ByVal e As AppointmentDragEventArgs) Handles schedulerControl1.AppointmentDrop
			Dim ts As TimeSpan = schedulerControl1.DayView.TimeScale
			Dim currentday As DateTime = schedulerControl1.SelectedInterval.Start.Date
			Dim ti As New TimeInterval(currentday, currentday.AddHours(24))
			Dim apts As AppointmentBaseCollection = schedulerStorage1.GetAppointments(ti)
			Dim i As Integer = 0
			Do While i < apts.Count
				If Object.Equals(apts(i), e.SourceAppointment) Then
					apts.Remove(apts(i))
					Exit Do
				End If
				i += 1
			Loop
			Dim interval As New TimeInterval(e.EditedAppointment.Start, e.EditedAppointment.End)
			schedulerStorage1.BeginUpdate()
			Dim snapFlag As Boolean = False
            For j As Integer = 0 To apts.Count - 1
                Dim currentInterval As New TimeInterval(apts(i).Start, apts(i).End)
                If currentInterval.IntersectsWith(interval) Then
                    SnapAppointments(e.EditedAppointment, apts(j))
                    snapFlag = True
                End If
            Next j
			schedulerStorage1.EndUpdate()
			If snapFlag Then
				BeginInvoke(New MethodInvoker(AddressOf RemoveSelectedAppointment))
			End If
		End Sub

		Private Sub RemoveSelectedAppointment()
			schedulerStorage1.Appointments.Remove(schedulerControl1.SelectedAppointments(0))
		End Sub

		Private Sub SnapAppointments(ByVal firstAppointment As Appointment, ByVal secondAppointment As Appointment)
			schedulerStorage1.Appointments.Remove(firstAppointment)
			secondAppointment.End = secondAppointment.End.Add(firstAppointment.Duration)
		End Sub
	End Class
End Namespace