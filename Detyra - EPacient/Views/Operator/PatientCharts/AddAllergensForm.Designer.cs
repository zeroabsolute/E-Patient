using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Detyra___EPacient.Views.Operator.PatientCharts {
    partial class AddAllergensForm {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(AddAllergensForm));

            int windowWidth = 450;
            int windowHeight = 280;
            int listBoxHeight = 220;
            int buttonHeight = 40;
            int windowPadding = 10;

            this.SuspendLayout();

            // Allergens
            this.medicamentsListBox = new ListBox();
            this.medicamentsListBox.Size = new Size(windowWidth - (2 * windowPadding), listBoxHeight);
            this.medicamentsListBox.Location = new Point(
                windowPadding,
                windowPadding
            );
            this.medicamentsListBox.MultiColumn = false;
            this.medicamentsListBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.medicamentsListBox.ForeColor = Colors.BLACK;
            this.medicamentsListBox.SelectionMode = SelectionMode.One;
            this.medicamentsListBox.DisplayMember = "name";
            this.medicamentsListBox.ValueMember = "id";
            this.Controls.Add(this.medicamentsListBox);

            // Submit button
            this.submitBtn = new Button();
            this.submitBtn.Size = new Size(windowWidth - (2 * windowPadding) , buttonHeight);
            this.submitBtn.Location = new Point(
                windowPadding,
                windowHeight - (windowPadding + buttonHeight)
            );
            this.submitBtn.Text = "RUAJ";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.submitBtn.ForeColor = Colors.WHITE;
            this.submitBtn.BackColor = Colors.MALACHITE;
            this.submitBtn.FlatStyle = FlatStyle.Flat;
            this.submitBtn.Click += new EventHandler(onSubmitButtonClicked);
            this.submitBtn.Image = Image.FromFile("../../Resources/save.png");
            this.submitBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.Controls.Add(this.submitBtn);

            // Form
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(windowWidth, windowHeight);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddAllergensForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Shto Alergen";
            this.Load += new EventHandler(onFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private ListBox medicamentsListBox;
        private Button submitBtn;
    }
}