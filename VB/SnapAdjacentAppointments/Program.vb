' Developer Express Code Central Example:
' How to merge adjacent appointments together
' 
' This example illustrates how to combine two adjacent appointments into one
' appointment by handling the XtraScheduler.AppointmentDrop event. Please drag the
' first appointment over the second one and drop it. As a result, the first
' appointment disappears and the second appointment's duration is increased.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2056

Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace MergeAdjacentAppointments
    Friend NotInheritable Class Program

        Private Sub New()
        End Sub

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread> _
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New Form1())
        End Sub
    End Class
End Namespace