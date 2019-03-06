using Detyra___EPacient.Models;
using Detyra___EPacient.Styles;
using Detyra___EPacient.Views.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Detyra___EPacient.Views.Nurse.Analysis {
    partial class AddAnalysisForm {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(AddAnalysisForm));

            int windowWidth = 450;
            int windowHeight = 280;
            int formComponentHeight = 40;
            int windowPadding = 10;
            int formComponentWidth = (int)((windowWidth - 2 * windowPadding) / 2);
            int verticalMargin = 20;
            this.SuspendLayout();

            // Document types
            this.docTypeLabel = new Label();
            this.docTypeLabel.Location = new Point(windowPadding, windowPadding);
            this.docTypeLabel.Name = "docTypeLabel";
            this.docTypeLabel.Size = new Size(formComponentWidth, formComponentHeight);
            this.docTypeLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.docTypeLabel.ForeColor = Colors.BLACK;
            this.docTypeLabel.Text = "Lloji i dokumentit";
            this.Controls.Add(this.docTypeLabel);

            Point cBoxLocation = new Point(
                windowPadding + formComponentWidth,
                windowPadding
            );
            Size cBoxSize = new Size(formComponentWidth, formComponentHeight);
            this.docTypesComboBox = new StaticComboBox(
                cBoxSize,
                cBoxLocation
            );
            this.Controls.Add(this.docTypesComboBox.comboBox);

            // Name
            this.nameLabel = new Label();
            this.nameLabel.Size = new Size(formComponentWidth, formComponentHeight);
            this.nameLabel.Location = new Point(
                windowPadding,
                windowPadding + verticalMargin + (1 * formComponentHeight)
            );
            this.nameLabel.Text = "Emri";
            this.nameLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.nameLabel.ForeColor = Colors.BLACK;
            this.Controls.Add(this.nameLabel);

            this.nameTxtBox = new TextBox();
            this.nameTxtBox.Size = new Size(formComponentWidth, formComponentHeight);
            this.nameTxtBox.Location = new Point(
                windowPadding + formComponentWidth,
                windowPadding + verticalMargin + (1 * formComponentHeight)
            );
            this.nameTxtBox.Font = new Font(Fonts.primary, 12, FontStyle.Regular);
            this.Controls.Add(this.nameTxtBox);

            // File
            this.fileLabel = new Label();
            this.fileLabel.Size = new Size(formComponentWidth, formComponentHeight);
            this.fileLabel.Font = new Font(Fonts.primary, 12, FontStyle.Bold);
            this.fileLabel.ForeColor = Colors.BLACK;
            this.fileLabel.Location = new Point(
                windowPadding,
                windowPadding + (2 * verticalMargin) + (2 * formComponentHeight)
            );
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Text = "File";
            this.Controls.Add(this.fileLabel);

            this.choseFileBtn = new Button();
            this.choseFileBtn.Location = new Point(
                windowPadding + formComponentWidth,
                windowPadding + (2 * verticalMargin) + (2 * formComponentHeight)
            );
            this.choseFileBtn.Name = "choseFileBtn";
            this.choseFileBtn.Size = new Size(formComponentWidth, formComponentHeight);
            this.choseFileBtn.Image = Image.FromFile("../../Resources/attach.png");
            this.choseFileBtn.ImageAlign = ContentAlignment.MiddleLeft;
            this.choseFileBtn.FlatStyle = FlatStyle.Flat;
            this.choseFileBtn.Click += new EventHandler(onChoseFileClicked);
            this.choseFileBtn.Text = "Zgjidh";
            this.choseFileBtn.UseVisualStyleBackColor = true;
            this.Controls.Add(this.choseFileBtn);

            // Submit button
            this.submitBtn = new Button();
            this.submitBtn.Size = new Size(windowWidth - (2 * windowPadding), formComponentHeight);
            this.submitBtn.Location = new Point(
                windowPadding,
                windowPadding + (5 * verticalMargin) + (3 * formComponentHeight)
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
            this.Name = "AddDocsForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Shto Dokument";
            this.Load += new EventHandler(onFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label fileLabel;
        private Label docTypeLabel;
        private Label nameLabel;
        private TextBox nameTxtBox;
        private Button choseFileBtn;
        private StaticComboBox docTypesComboBox;
        private Button submitBtn;

    }
}