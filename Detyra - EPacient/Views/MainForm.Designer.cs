using System.Windows.Forms;
using System.Drawing;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Views;


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
            OperatorMainPanel operatorMainPanel = new OperatorMainPanel();
            DoctorMainPanel doctorMainPanel = new DoctorMainPanel();
            NurseMainPanel nurseMainPanel = new NurseMainPanel();

            logInPanel.initNextPanels(
                managerMainPanel,
                operatorMainPanel,
                doctorMainPanel,
                nurseMainPanel
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
            this.Controls.Add(operatorMainPanel.Panel);
            this.Controls.Add(doctorMainPanel.Panel);
            this.Controls.Add(nurseMainPanel.Panel);
        }

        #endregion
    }
}

