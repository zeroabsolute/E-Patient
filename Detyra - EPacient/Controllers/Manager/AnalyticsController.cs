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
        private Models.ReservationAnalytics reservationAnalytics;

        public AnalyticsController(ManagerAnalytics view) {
            this.view = view;
            this.serviceModel = new Models.Service();
            this.patientModel = new Models.Patient();
            this.doctorModel = new Models.Doctor();
            this.nurseModel = new Models.Nurse();
            this.reservationAnalytics = new Models.ReservationAnalytics();
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

                // Read doctors
                List<Models.Doctor> doctors = await doctorModel.readDoctors();
                this.view.DoctorsCBox.comboBox.DisplayMember = "fullname";
                this.view.DoctorsCBox.comboBox.ValueMember = "id";
                this.view.DoctorsCBox.comboBox.DataSource = doctors;

                // Populate months combobox
                this.view.MonthCBox.comboBox.DisplayMember = "name";
                this.view.MonthCBox.comboBox.ValueMember = "value";
                this.view.MonthCBox.comboBox.DataSource = new List<Models.Month> {
                    Months.JANUARY,
                    Months.FEBRUARY,
                    Months.MARCH,
                    Months.APRIL,
                    Months.MAY,
                    Months.JUNE,
                    Months.JULY,
                    Months.AUGUST,
                    Months.SEPTEMBER,
                    Months.OCTOBER,
                    Months.NOVEMBER,
                    Months.DECEMBER
                };

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
         * Handle filter change
         */

        private void populateTable(List<Models.ReservationAnalytics> data) {
            this.view.DoctorReservationsTable.DataGrid.Rows.Clear();
            this.view.DoctorReservationsTable.DataGrid.Refresh();

            data.ForEach((item) => {
                this.view.DoctorReservationsTable.DataGrid.Rows.Add(
                    item.Day,
                    item.ReservationsCount
                );
            });
        }

        public async void handleFilterChange() {
            try {
                Cursor.Current = Cursors.WaitCursor;

                if (
                    this.view.DoctorsCBox.comboBox.SelectedValue != null
                        && this.view.MonthCBox.comboBox.SelectedValue != null
                        && this.view.YearTxtBox.Text.Length == 4
                ) {
                    List<Models.ReservationAnalytics> data = await reservationAnalytics.readReservationAnalytics(
                        (int) this.view.DoctorsCBox.comboBox.SelectedValue,
                        this.view.MonthCBox.comboBox.SelectedValue.ToString(),
                        this.view.YearTxtBox.Text
                    );

                    this.populateTable(data);

                    foreach (var series in this.view.DoctorReservationsChart.Series) {
                        series.Points.Clear();
                    }

                    data.ForEach((item) => {
                        this.view.ReservationsSeries.Points.AddXY(item.Day, item.ReservationsCount);
                    });
                }

                Cursor.Current = Cursors.Arrow;
            } catch (Exception e) {
                string caption = "Problem në lexim";
                MessageBox.Show(e.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
