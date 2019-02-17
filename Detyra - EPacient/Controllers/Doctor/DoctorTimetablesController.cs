using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Doctor;

namespace Detyra___EPacient.Controllers.Doctor {
    class DoctorTimetablesController {
        private TimeTablesDoc view;
        
        private Models.Employee employeeModel;
        private Models.Doctor doctorModel;
        private Models.WorkingHours workingHoursModel;

        public DoctorTimetablesController(TimeTablesDoc view) {
            this.view = view;
            this.employeeModel = new Models.Employee();
            this.doctorModel = new Models.Doctor();
            this.workingHoursModel = new Models.WorkingHours();
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                int employeeId = await employeeModel.getEmployeeByUserId(this.view.LoggedInUser.Id);

                Models.WorkingHours workingHours = await workingHoursModel.readWorkingHours(employeeId);

                this.view.MondayStartLabel.Text = workingHours != null ? workingHours.MondayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.MondayEndLabel.Text = workingHours != null ? workingHours.MondayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.TuesdayStartLabel.Text = workingHours != null ? workingHours.TuesdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.TuesdayEndLabel.Text = workingHours != null ? workingHours.TuesdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.WednesdayStartLabel.Text = workingHours != null ? workingHours.WednesdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.WednesdayEndLabel.Text = workingHours != null ? workingHours.WednesdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.ThursdayStartLabel.Text = workingHours != null ? workingHours.ThursdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.ThursdayEndLabel.Text = workingHours != null ? workingHours.ThursdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.FridayStartLabel.Text = workingHours != null ? workingHours.MondayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.FridayEndLabel.Text = workingHours != null ? workingHours.MondayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.SaturdayStartLabel.Text = workingHours != null ? workingHours.SaturdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.SaturdayEndLabel.Text = workingHours != null ? workingHours.SaturdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.SundayStartLabel.Text = workingHours != null ? workingHours.SundayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";
                this.view.SundayEndLabel.Text = workingHours != null ? workingHours.SundayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "-";

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
