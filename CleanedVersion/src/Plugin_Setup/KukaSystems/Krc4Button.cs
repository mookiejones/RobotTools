using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using KukaSystems.Properties;

namespace KukaSystems
{
    public sealed partial class Krc4Button : UserControl
    {
        public delegate void ButtonClickHandler();
        public delegate void ButtonDownHandler();
        public delegate void ButtonUpHandler();
        public delegate void ButtonCheckHandler();
        private bool bMouseDown;
        private bool bDarkMode;
        private bool bCheckBox;
        private bool bChecked;
        public event ButtonClickHandler ButtonClick;
        [Category("Action"), Description("Fires when the button is down.")]
        public event ButtonDownHandler ButtonDown;
        [Category("Action"), Description("Fires when the button is up.")]
        public event ButtonUpHandler ButtonUp;
        [Category("Action"), Description("Fires when button checked state changed.")]
        public event ButtonClickHandler ButtonCheckedChanged;
        public Krc4Button()
        {
            InitializeComponent();
        }

       // KukaSystems.Krc4Button
        [Browsable(true), Description("Image for you Krc4 Button. Use 66x40px PNG Image with transparency")]
        public Image ButtonImage { get; set; }
        [Browsable(true), DefaultValue("false"), Description("CheckBox Krc4 Button")]
        public bool CheckBox
        {
            get
            {
                return this.bCheckBox;
            }
            set
            {
                this.bCheckBox = value;
                this.label1.Size = base.Size;
                if (this.bCheckBox)
                {
                    this.label1.Width = base.Width - 22;
                    this.label1.Left = base.Left + 22;
                    this.label1.TextAlign = ContentAlignment.MiddleLeft;
                }
                else
                {
                    this.label1.Left = 0;
                    this.label1.TextAlign = ContentAlignment.MiddleCenter;
                }
                base.Invalidate();
            }
        }
        [Browsable(true), DefaultValue("false"), Description("Checked Krc4 Button")]
        public bool Checked
        {
            get
            {
                return this.bChecked;
            }
            set
            {
                this.bChecked = value;
                base.Invalidate();
            }
        }
        [Browsable(true), DefaultValue("false"), Description("Dark Krc4 Button")]
        public bool DarkMode
        {
            get
            {
                return this.bDarkMode;
            }
            set
            {
                this.bDarkMode = value;
            }
        }
        // KukaSystems.Krc4Button
        [Browsable(true), DefaultValue("Button"), Description("Text displayed on your cool Krc4 Button")]
        public string TextCaption
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }


        private void OnButtonClick()
        {
            if (ButtonClick != null)
            {
                ButtonClick();
            }
        }

        private void OnButtonDown()
        {
            if (ButtonDown != null)
            {
                ButtonDown();
            }
        }

        private void OnButtonUp()
        {
            if (ButtonUp != null)
            {
                ButtonUp();
            }
        }

        private void OnButtonCheckedChanged()
        {
            if (ButtonCheckedChanged != null)
            {
                ButtonCheckedChanged();
            }
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            bMouseDown = false;
            base.Invalidate();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (bCheckBox)
            {
                bChecked = !bChecked;
                base.Invalidate();
                OnButtonCheckedChanged();
            }
            OnButtonClick();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDown = true;
            Invalidate();
            OnButtonDown();
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDown = false;
            Invalidate();
            OnButtonUp();
        }

        // KukaSystems.Krc4Button
        private void Krc4Button_Paint(object sender, PaintEventArgs e)
        {
            var clientRectangle = ClientRectangle;
            new Pen(Color.FromArgb(100, 100, 100));
            var brush = new LinearGradientBrush(clientRectangle, Color.FromArgb(233, 233, 233), Color.FromArgb(151, 151, 151), LinearGradientMode.Vertical);
            var brush2 = new LinearGradientBrush(clientRectangle, Color.FromArgb(1, 170, 255), Color.FromArgb(109, 200, 255), LinearGradientMode.Vertical);
            var brush3 = new LinearGradientBrush(clientRectangle, Color.FromArgb(204, 204, 204), Color.FromArgb(100, 100, 100), LinearGradientMode.Vertical);
            if (bMouseDown)
            {
                e.Graphics.FillRectangle(brush2, 1, 1, clientRectangle.Width - 2, clientRectangle.Height - 2);
            }
            else
            {
                if (!bDarkMode)
                {
                    e.Graphics.FillRectangle(brush, 1, 1, clientRectangle.Width - 2, clientRectangle.Height - 2);
                }
                else
                {
                    e.Graphics.FillRectangle(brush3, 1, 1, clientRectangle.Width - 2, clientRectangle.Height - 2);
                }
            }
            if (bCheckBox)
            {
                var pen = new Pen(Color.FromArgb(127, 127, 127));
                var rect = new Rectangle(6, clientRectangle.Height / 2 - 7, 12, 12);
                var brush4 = new LinearGradientBrush(rect, Color.FromArgb(186, 186, 186), Color.FromArgb(237, 237, 237), LinearGradientMode.Vertical);
                e.Graphics.DrawLine(pen, 6, clientRectangle.Height / 2 - 7, 17, clientRectangle.Height / 2 - 7);
                e.Graphics.DrawLine(pen, 6, clientRectangle.Height / 2 + 6, 17, clientRectangle.Height / 2 + 6);
                e.Graphics.DrawLine(pen, 5, clientRectangle.Height / 2 - 6, 5, clientRectangle.Height / 2 + 5);
                e.Graphics.DrawLine(pen, 18, clientRectangle.Height / 2 - 6, 18, clientRectangle.Height / 2 + 5);
                e.Graphics.FillRectangle(brush4, 6, clientRectangle.Height / 2 - 6, 12, 12);
                if (bChecked)
                {
                    e.Graphics.DrawImage(Resources._checked, 7, clientRectangle.Height / 2 - 5, 10, 10);
                }
            }
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var num = 0;
            var num2 = 0;
            var width = clientRectangle.Width;
            var height = clientRectangle.Height;
            var point = new Point(num + 1, num2 + 1);
            var point2 = new Point(num + 1, num2);
            var point3 = new Point(width - 2, num2);
            var point4 = new Point(width - 2, num2 + 1);
            var point5 = new Point(width - 1, num2 + 1);
            var point6 = new Point(width - 1, height - 2);
            var point7 = new Point(width - 2, height - 2);
            var point8 = new Point(width - 2, height - 1);
            var point9 = new Point(num + 1, height - 1);
            var point10 = new Point(num + 1, height - 2);
            var point11 = new Point(num, height - 2);
            var point12 = new Point(num, num2 + 1);
            var points = new Point[]
	{
		point12,
		point,
		point2
	};
            var points2 = new[]
	{
		point3,
		point4,
		point5
	};
            var points3 = new[]
	{
		point6,
		point7,
		point8
	};
            var points4 = new[]
	{
		point9,
		point10,
		point11
	};
            var graphicsPath = new GraphicsPath();
            graphicsPath.AddCurve(points);
            graphicsPath.AddLine(point2, point3);
            graphicsPath.AddCurve(points2);
            graphicsPath.AddLine(point5, point6);
            graphicsPath.AddCurve(points3);
            graphicsPath.AddLine(point8, point9);
            graphicsPath.AddCurve(points4);
            graphicsPath.AddLine(point11, point12);
            var pen2 = new Pen(Color.FromArgb(100, 100, 100), 1f);
            e.Graphics.DrawPath(pen2, graphicsPath);
            pen2.Dispose();
            if (!Enabled)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, 210, 210, 210)), 0, 0, clientRectangle.Width, clientRectangle.Height);
            }
        }


        private void Krc4Button_FontChanged(object sender, EventArgs e)
        {
            label1.Font = Font;
        }

        private void Krc4Button_Resize(object sender, EventArgs e)
        {
            label1.Size = Size;
            if (bCheckBox)
            {
                label1.Width = Width - 22;
                label1.Left = 22;
                label1.TextAlign = ContentAlignment.MiddleLeft;
                return;
            }
            label1.Left = 0;
            label1.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}
