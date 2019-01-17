using System.Windows.Forms;
using System.Drawing;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views;
using Detyra___EPacient.Views.Manager;

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
            Analytics managerAnalyticsPanel = new Analytics(managerMainPanel.Panel);
            DoctorInCharge managerDICPanel = new DoctorInCharge(managerMainPanel.Panel);
            Medicaments managerMedicamentsPanel = new Medicaments(managerMainPanel.Panel);
            Services managerServicesPanel = new Services(managerMainPanel.Panel);
            Timetables managerTimetablesPanel = new Timetables(managerMainPanel.Panel);
            Users managerUsersPanel = new Users(managerMainPanel.Panel);

            OperatorMainPanel operatorMainPanel = new OperatorMainPanel();
            Patients operatorPatientsPanel = new Patients(operatorMainPanel.Panel);
            Reservations operatorReservationsPanel = new Reservations(operatorMainPanel.Panel);
            TimeTables operatorTimeTablesPanel = new TimeTables(operatorMainPanel.Panel);
          
            DoctorMainPanel doctorMainPanel = new DoctorMainPanel();
            NurseMainPanel nurseMainPanel = new NurseMainPanel();

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
                operatorReservationsPanel,
                operatorTimeTablesPanel,
                logInPanel
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
            this.Controls.Add(operatorReservationsPanel.Panel);
            this.Controls.Add(operatorTimeTablesPanel.Panel);
 

            this.Controls.Add(doctorMainPanel.Panel);
            this.Controls.Add(nurseMainPanel.Panel);
        }

        #endregion
    }
}

