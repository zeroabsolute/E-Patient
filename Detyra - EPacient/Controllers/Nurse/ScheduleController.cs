using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Nurse;


namespace Detyra___EPacient.Controllers.Nurse {
    class ScheduleController {
        private Schedule view;
        private Models.Nurse nurseModel;
        private Models.WorkingHours workingHoursModel;

        public ScheduleController(Schedule view) {
            this.view = view;
            this.workingHoursModel = new Models.WorkingHours();
        }
    }

        public async void init() { 

        int id = (int)selectedRow.Cells[0].Value;
        string firstName = selectedRow.Cells[2].Value.ToString();
        string lastName = selectedRow.Cells[3].Value.ToString();

        this.view.SelectedEmployee = $"{firstName} {lastName}";
        this.view.SelectedEmployeeId = id;
        this.view.EmployeeLabelValue.Text = this.view.SelectedEmployee;
        // Read working hours (if there are any existing)
        Models.WorkingHours workingHours = await workingHoursModel.readWorkingHours(id);

                this.view.SelectedWorkingHoursId = workingHours != null ? workingHours.Id : -1;
                this.view.MondayStartLabel.Text = workingHours != null ? workingHours.MondayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.MondayEndLabel.Text = workingHours != null ? workingHours.MondayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.TuesdayStartLabel.Text = workingHours != null ? workingHours.TuesdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.TuesdayEndLabel.Text = workingHours != null ? workingHours.TuesdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.WednesdayStartLabel.Text = workingHours != null ? workingHours.WednesdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.WednesdayEndLabel.Text = workingHours != null ? workingHours.WednesdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.ThursdayStartLabel.Text = workingHours != null ? workingHours.ThursdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.ThursdayEndLabel.Text = workingHours != null ? workingHours.ThursdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.FridayStartLabel.Text = workingHours != null ? workingHours.MondayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.FridayEndLabel.Text = workingHours != null ? workingHours.MondayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.SaturdayStartLabel.Text = workingHours != null ? workingHours.SaturdayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.SaturdayEndLabel.Text = workingHours != null ? workingHours.SaturdayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.SundayStartLabel.Text = workingHours != null ? workingHours.SundayStartTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";
                this.view.SundayEndLabel.Text = workingHours != null ? workingHours.SundayEndTime.ToString(DateTimeFormats.SQ_SHORT_TIME) : "";


            
        }
}
