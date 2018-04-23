using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;

namespace MergeAdjacentAppointments
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.schedulerControl1.Start = DateTime.Now.Date;
            CreateAppointments();
        }

        private void CreateAppointments()
        {
            schedulerStorage1.BeginUpdate();
            for (int i = 0; i < 2; i++)
            {
                Appointment apt = schedulerStorage1.CreateAppointment(AppointmentType.Normal);
                apt.Subject = "Subject" + i.ToString();
                apt.Description = "Description" + i.ToString();
                apt.Start = DateTime.Now.Date.AddHours(i + 10);
                apt.End = DateTime.Now.Date.AddHours(i + 10.5f);
                schedulerStorage1.Appointments.Add(apt);
            }
            schedulerStorage1.EndUpdate();
        }

        private void schedulerControl1_AppointmentDrop(object sender, AppointmentDragEventArgs e)
        {
            TimeSpan ts = schedulerControl1.DayView.TimeScale;
            DateTime currentday = schedulerControl1.SelectedInterval.Start.Date;
            TimeInterval ti = new TimeInterval(currentday, currentday.AddHours(24));
            AppointmentBaseCollection apts = schedulerStorage1.GetAppointments(ti);
            for (int i = 0; i < apts.Count; i++) {
                if (object.Equals(apts[i], e.SourceAppointment))
                {
                    apts.Remove(apts[i]);
                    break;
                }
            }
            TimeInterval interval = new TimeInterval(e.EditedAppointment.Start, e.EditedAppointment.End);
            schedulerStorage1.BeginUpdate();
            bool snapFlag = false;
            for (int i = 0; i < apts.Count; i++)
            {
                TimeInterval currentInterval = new TimeInterval(apts[i].Start, apts[i].End);
                if (currentInterval.IntersectsWith(interval))
                {
                    SnapAppointments(e.EditedAppointment, apts[i]);
                    snapFlag = true;
                }
            }
            schedulerStorage1.EndUpdate();
            if(snapFlag)
                BeginInvoke(new MethodInvoker(RemoveSelectedAppointment));
        }

        private void RemoveSelectedAppointment()
        {
            schedulerStorage1.Appointments.Remove(schedulerControl1.SelectedAppointments[0]);
        }

        private void SnapAppointments(Appointment firstAppointment, Appointment secondAppointment)
        {
            schedulerStorage1.Appointments.Remove(firstAppointment);
            secondAppointment.End = secondAppointment.End.Add(firstAppointment.Duration);            
        }
    }
}