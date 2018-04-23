// Developer Express Code Central Example:
// How to merge adjacent appointments together
// 
// This example illustrates how to combine two adjacent appointments into one
// appointment by handling the XtraScheduler.AppointmentDrop event. Please drag the
// first appointment over the second one and drop it. As a result, the first
// appointment disappears and the second appointment's duration is increased.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2056

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MergeAdjacentAppointments
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}