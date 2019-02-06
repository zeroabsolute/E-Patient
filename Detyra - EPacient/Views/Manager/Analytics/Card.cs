using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Detyra___EPacient.Constants;
using Detyra___EPacient.Styles;

namespace Detyra___EPacient.Views.Manager.Analytics {
    class Card {
        public TableLayoutPanel Container { get; set; }
        public Label RightTopLabel;

        private Label leftIconLabel;
        private TableLayoutPanel rightContainer;
        private Label rightBottomLabel;

        private Image leftIcon;
        private string bottomRightValue;
        private Color leftLabelColor;
        private Point containerLocation;

        public Card(
            Image leftIcon,
            string bottomRightValue,
            Color leftLabelColor,
            Point containerLocation
        ) {
            // Init dynamic params
            this.leftIcon = leftIcon;
            this.bottomRightValue = bottomRightValue;
            this.leftLabelColor = leftLabelColor;
            this.containerLocation = containerLocation;

            // Init container
            this.Container = new TableLayoutPanel();
            this.Container.Size = Dimensions.TOP_CARD_SIZE;
            this.Container.Location = this.containerLocation;
            this.Container.BackColor = Colors.WHITE;
            this.Container.ColumnCount = 2;

            // Left label
            this.leftIconLabel = new Label();
            this.leftIconLabel.Width = (int) Dimensions.TOP_CARD_SIZE.Width / 2;
            this.leftIconLabel.Height = Dimensions.TOP_CARD_SIZE.Height;
            this.leftIconLabel.BackColor = this.leftLabelColor;
            this.leftIconLabel.Image = this.leftIcon;
            this.leftIconLabel.ImageAlign = ContentAlignment.MiddleCenter;
            this.leftIconLabel.Margin = new Padding(0);
            this.Container.Controls.Add(this.leftIconLabel, 0, 0);

            // Right container
            this.rightContainer = new TableLayoutPanel();
            this.rightContainer.RowCount = 2;
            this.rightContainer.Margin = new Padding(0);
            this.rightContainer.Width = (int) Dimensions.TOP_CARD_SIZE.Width / 2;
            this.rightContainer.Height = Dimensions.TOP_CARD_SIZE.Height;
            this.Container.Controls.Add(this.rightContainer, 1, 0);

            // Top right label
            this.RightTopLabel = new Label();
            this.RightTopLabel.Width = (int) Dimensions.TOP_CARD_SIZE.Width / 2;
            this.RightTopLabel.Height = (int) Dimensions.TOP_CARD_SIZE.Height / 2;
            this.RightTopLabel.Image = this.leftIcon;
            this.RightTopLabel.Text = "0";
            this.RightTopLabel.Font = new Font(Fonts.primary, 24, FontStyle.Bold);
            this.RightTopLabel.ForeColor = Colors.BLACK;
            this.RightTopLabel.TextAlign = ContentAlignment.BottomCenter;
            this.RightTopLabel.Margin = new Padding(0, 0, 0, 2);
            this.rightContainer.Controls.Add(this.RightTopLabel, 0, 0);

            // Bottom right label
            this.rightBottomLabel = new Label();
            this.rightBottomLabel.Width = (int) Dimensions.TOP_CARD_SIZE.Width / 2;
            this.rightBottomLabel.Height = (int) Dimensions.TOP_CARD_SIZE.Height / 2;
            this.rightBottomLabel.Image = this.leftIcon;
            this.rightBottomLabel.Text = this.bottomRightValue;
            this.rightBottomLabel.Font = new Font(Fonts.primary, 10, FontStyle.Regular);
            this.rightBottomLabel.ForeColor = Colors.DOVE_GRAY;
            this.rightBottomLabel.TextAlign = ContentAlignment.TopCenter;
            this.rightBottomLabel.Margin = new Padding(0, 2, 0, 0);
            this.rightContainer.Controls.Add(this.rightBottomLabel, 0, 1);

        }
    }
}
