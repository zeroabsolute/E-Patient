using System.Windows.Forms;
using System.Drawing;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views;
using Detyra___EPacient.Views.Operator;
using Detyra___EPacient.Views.Nurse;
using Detyra___EPacient.Views.Manager;
using Detyra___EPacient.Views.Doctor;

namespace Detyra___EPacient {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SuspendLayout();
            // 
            // Panels
            // 
            LogInPanel logInPanel = new LogInPanel();

            ManagerMainPanel managerMainPanel = new ManagerMainPanel();
            ManagerAnalytics managerAnalyticsPanel = new ManagerAnalytics(managerMainPanel.Panel);
            DoctorInCharge managerDICPanel = new DoctorInCharge(managerMainPanel.Panel);
            Medicaments managerMedicamentsPanel = new Medicaments(managerMainPanel.Panel);
            Services managerServicesPanel = new Services(managerMainPanel.Panel);
            Timetables managerTimetablesPanel = new Timetables(managerMainPanel.Panel);
            Users managerUsersPanel = new Users(managerMainPanel.Panel);

            OperatorMainPanel operatorMainPanel = new OperatorMainPanel();
            OperatorPatientCharts operatorPatientChartsPanel = new OperatorPatientCharts(operatorMainPanel.Panel);
            Patients operatorPatientsPanel = new Patients(operatorMainPanel.Panel);
            Reservations operatorReservationsPanel = new Reservations(operatorMainPanel.Panel);
            OperatorTimetables operatorTimeTablesPanel = new OperatorTimetables(operatorMainPanel.Panel);
          
            DoctorMainPanel doctorMainPanel = new DoctorMainPanel();
            TimeTablesDoc doctorTimeTablesDocPanel = new TimeTablesDoc(doctorMainPanel.Panel);
            ReservationsDoc doctorReservationsDocPanel = new ReservationsDoc(doctorMainPanel.Panel);
            Prescription doctorPrescriptionPanel = new Prescription(doctorMainPanel.Panel);

            NurseMainPanel nurseMainPanel = new NurseMainPanel();
            Schedule nurseSchedulePanel = new Schedule(nurseMainPanel.Panel);
            ReservationsNurse nurseReservationsNursePanel = new ReservationsNurse(nurseMainPanel.Panel);
            AnalysisNurse nurseAnalysisPanel = new AnalysisNurse(nurseMainPanel.Panel);

            logInPanel.initNextPanels(
                managerMainPanel,
                operatorMainPanel,
                doctorMainPanel,
                nurseMainPanel
            );

            managerMainPanel.initNextPanels(
                managerUsersPanel,
                managerTimetablesPanel,
                managerServicesPanel,
                managerMedicamentsPanel,
                managerDICPanel,
                managerAnalyticsPanel,
                logInPanel
            );

            operatorMainPanel.initNextPanels(
                operatorPatientsPanel,
                operatorPatientChartsPanel,
                operatorReservationsPanel,
                operatorTimeTablesPanel,
                logInPanel
            );

            doctorMainPanel.initNextPanels(
                doctorTimeTablesDocPanel,
                doctorReservationsDocPanel,
                doctorPrescriptionPanel,
                logInPanel
            );

            nurseMainPanel.initNextPanels(
                  nurseSchedulePanel,
                  logInPanel,
                  nurseReservationsNursePanel,
                  nurseAnalysisPanel
              );

            // 
            // MainForm
            // 
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Left = this.Top = 0;
            this.MaximumSize = new Size(Dimensions.VIEW_WIDTH, Dimensions.VIEW_HEIGHT);
            this.MinimumSize = this.MaximumSize;
            this.Name = "MainForm";
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "E-Pacient GG";
            this.ResumeLayout(false);
            this.PerformLayout();

            this.Controls.Add(logInPanel.Panel);

            this.Controls.Add(managerMainPanel.Panel);
            this.Controls.Add(managerAnalyticsPanel.Panel);
            this.Controls.Add(managerDICPanel.Panel);
            this.Controls.Add(managerMedicamentsPanel.Panel);
            this.Controls.Add(managerTimetablesPanel.Panel);
            this.Controls.Add(managerUsersPanel.Panel);
            this.Controls.Add(managerServicesPanel.Panel);

            this.Controls.Add(operatorMainPanel.Panel);
            this.Controls.Add(operatorPatientsPanel.Panel);
            this.Controls.Add(operatorPatientChartsPanel.Panel);
            this.Controls.Add(operatorReservationsPanel.Panel);
            this.Controls.Add(operatorTimeTablesPanel.Panel);

            this.Controls.Add(doctorMainPanel.Panel);
            this.Controls.Add(doctorTimeTablesDocPanel.Panel);
            this.Controls.Add(doctorReservationsDocPanel.Panel);
            this.Controls.Add(doctorPrescriptionPanel.Panel);

            this.Controls.Add(nurseMainPanel.Panel);
            this.Controls.Add(nurseSchedulePanel.Panel);
            this.Controls.Add(nurseAnalysisPanel.Panel);
            this.Controls.Add(nurseReservationsNursePanel.Panel);
        }

        #endregion
    }
}

