using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views.Manager;

namespace Detyra___EPacient.Controllers.Manager {
    class AnalyticsController {
        private ManagerAnalytics view;

        private Models.Service serviceModel;
        private Models.Patient patientModel;
        private Models.Doctor doctorModel;
        private Models.Nurse nurseModel;

        public AnalyticsController(ManagerAnalytics view) {
            this.view = view;
            this.serviceModel = new Models.Service();
            this.patientModel = new Models.Patient();
            this.doctorModel = new Models.Doctor();
            this.nurseModel = new Models.Nurse();
        }

        /**
         * Controller to read initial data
         */

        public async void init() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                // Read and change counters value
                int servicesCount = await this.serviceModel.getServicesCount();
                int patientsCount = await this.patientModel.getPatientsCount();
                int doctorsCount = await this.doctorModel.getDoctorsCount();
                int nursesCount = await this.nurseModel.getNursesCount();

                this.view.ServicesCard.RightTopLabel.Text = servicesCount.ToString();
                this.view.PatientsCard.RightTopLabel.Text = patientsCount.ToString();
                this.view.DoctorsCard.RightTopLabel.Text = doctorsCount.ToString();
                this.view.NursesCard.RightTopLabel.Text = nursesCount.ToString();

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
