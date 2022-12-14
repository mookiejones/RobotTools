using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Plugin_Setup.Controls
{
    [DefaultEvent("ButtonClick")]
    public class Krc4Button : UserControl
    {
        public delegate void ButtonClickHandler();
        public delegate void ButtonDownHandler();
        public delegate void ButtonUpHandler();
        public delegate void ButtonCheckHandler();
        private bool bMouseDown;
        private bool bDarkMode;
        private bool bCheckBox;
        private bool bChecked;
        private IContainer components;
        private Label label1;
        [Category("Action"), Description("Fires when the button is clicked.")]
        public event Krc4Button.ButtonClickHandler ButtonClick;
        [Category("Action"), Description("Fires when the button is down.")]
        public event Krc4Button.ButtonDownHandler ButtonDown;
        [Category("Action"), Description("Fires when the button is up.")]
        public event Krc4Button.ButtonUpHandler ButtonUp;
        [Category("Action"), Description("Fires when button checked state changed.")]
        public event Krc4Button.ButtonClickHandler ButtonCheckedChanged;
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
        [Browsable(true), Description("Image for you Krc4 Button. Use 66x40px PNG Image with transparency")]
        public Image ButtonImage
        {
            get
            {
                return this.label1.Image;
            }
            set
            {
                this.label1.Image = value;
            }
        }
        public Krc4Button()
        {
            this.InitializeComponent();
        }
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            this.bMouseDown = true;
            base.Invalidate();
            this.OnButtonDown();
        }
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.bMouseDown = false;
            base.Invalidate();
        }
        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            this.bMouseDown = false;
            base.Invalidate();
            this.OnButtonUp();
        }
        protected virtual void OnButtonClick()
        {
            if (this.ButtonClick != null)
            {
                this.ButtonClick();
            }
        }
        protected virtual void OnButtonDown()
        {
            if (this.ButtonDown != null)
            {
                this.ButtonDown();
            }
        }
        protected virtual void OnButtonUp()
        {
            if (this.ButtonUp != null)
            {
                this.ButtonUp();
            }
        }
        protected virtual void OnButtonCheckedChanged()
        {
            if (this.ButtonCheckedChanged != null)
            {
                this.ButtonCheckedChanged();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            if (this.bCheckBox)
            {
                this.bChecked = !this.bChecked;
                base.Invalidate();
                this.OnButtonCheckedChanged();
            }
            this.OnButtonClick();
        }
        private void Krc4Button_Paint(object sender, PaintEventArgs e)
        {
            Rectangle clientRectangle = base.ClientRectangle;
            new Pen(Color.FromArgb(100, 100, 100));
            LinearGradientBrush brush = new LinearGradientBrush(clientRectangle, Color.FromArgb(233, 233, 233), Color.FromArgb(151, 151, 151), LinearGradientMode.Vertical);
            LinearGradientBrush brush2 = new LinearGradientBrush(clientRectangle, Color.FromArgb(1, 170, 255), Color.FromArgb(109, 200, 255), LinearGradientMode.Vertical);
            LinearGradientBrush brush3 = new LinearGradientBrush(clientRectangle, Color.FromArgb(204, 204, 204), Color.FromArgb(100, 100, 100), LinearGradientMode.Vertical);
            if (this.bMouseDown)
            {
                e.Graphics.FillRectangle(brush2, 1, 1, clientRectangle.Width - 2, clientRectangle.Height - 2);
            }
            else
            {
                if (!this.bDarkMode)
                {
                    e.Graphics.FillRectangle(brush, 1, 1, clientRectangle.Width - 2, clientRectangle.Height - 2);
                }
                else
                {
                    e.Graphics.FillRectangle(brush3, 1, 1, clientRectangle.Width - 2, clientRectangle.Height - 2);
                }
            }
            if (this.bCheckBox)
            {
                Pen pen = new Pen(Color.FromArgb(127, 127, 127));
                Rectangle rect = new Rectangle(6, clientRectangle.Height / 2 - 7, 12, 12);
                LinearGradientBrush brush4 = new LinearGradientBrush(rect, Color.FromArgb(186, 186, 186), Color.FromArgb(237, 237, 237), LinearGradientMode.Vertical);
                e.Graphics.DrawLine(pen, 6, clientRectangle.Height / 2 - 7, 17, clientRectangle.Height / 2 - 7);
                e.Graphics.DrawLine(pen, 6, clientRectangle.Height / 2 + 6, 17, clientRectangle.Height / 2 + 6);
                e.Graphics.DrawLine(pen, 5, clientRectangle.Height / 2 - 6, 5, clientRectangle.Height / 2 + 5);
                e.Graphics.DrawLine(pen, 18, clientRectangle.Height / 2 - 6, 18, clientRectangle.Height / 2 + 5);
                e.Graphics.FillRectangle(brush4, 6, clientRectangle.Height / 2 - 6, 12, 12);
                if (this.bChecked)
                {
                    e.Graphics.DrawImage(Resources._checked, 7, clientRectangle.Height / 2 - 5, 10, 10);
                }
            }
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int num = 0;
            int num2 = 0;
            int width = clientRectangle.Width;
            int height = clientRectangle.Height;
            Point point = new Point(num + 1, num2 + 1);
            Point point2 = new Point(num + 1, num2);
            Point point3 = new Point(width - 2, num2);
            Point point4 = new Point(width - 2, num2 + 1);
            Point point5 = new Point(width - 1, num2 + 1);
            Point point6 = new Point(width - 1, height - 2);
            Point point7 = new Point(width - 2, height - 2);
            Point point8 = new Point(width - 2, height - 1);
            Point point9 = new Point(num + 1, height - 1);
            Point point10 = new Point(num + 1, height - 2);
            Point point11 = new Point(num, height - 2);
            Point point12 = new Point(num, num2 + 1);
            Point[] points = new Point[]
			{
				point12,
				point,
				point2
			};
            Point[] points2 = new Point[]
			{
				point3,
				point4,
				point5
			};
            Point[] points3 = new Point[]
			{
				point6,
				point7,
				point8
			};
            Point[] points4 = new Point[]
			{
				point9,
				point10,
				point11
			};
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddCurve(points);
            graphicsPath.AddLine(point2, point3);
            graphicsPath.AddCurve(points2);
            graphicsPath.AddLine(point5, point6);
            graphicsPath.AddCurve(points3);
            graphicsPath.AddLine(point8, point9);
            graphicsPath.AddCurve(points4);
            graphicsPath.AddLine(point11, point12);
            Pen pen2 = new Pen(Color.FromArgb(100, 100, 100), 1f);
            e.Graphics.DrawPath(pen2, graphicsPath);
            pen2.Dispose();
            if (!base.Enabled)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, 210, 210, 210)), 0, 0, clientRectangle.Width, clientRectangle.Height);
            }
        }
        private void Krc4Button_FontChanged(object sender, EventArgs e)
        {
            this.label1.Font = this.Font;
        }
        private void Krc4Button_Resize(object sender, EventArgs e)
        {
            this.label1.Size = base.Size;
            if (this.bCheckBox)
            {
                this.label1.Width = base.Width - 22;
                this.label1.Left = 22;
                this.label1.TextAlign = ContentAlignment.MiddleLeft;
                return;
            }
            this.label1.Left = 0;
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.label1 = new Label();
            base.SuspendLayout();
            this.label1.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.label1.Location = new Point(0, 0);
            this.label1.Margin = new Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(66, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Button1";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.label1.MouseLeave += new EventHandler(this.label1_MouseLeave);
            this.label1.Click += new EventHandler(this.label1_Click);
            this.label1.MouseDown += new MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseUp += new MouseEventHandler(this.label1_MouseUp);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.Transparent;
            this.BackgroundImageLayout = ImageLayout.None;
            base.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
            base.Margin = new Padding(0);
            this.MaximumSize = new Size(520, 520);
            this.MinimumSize = new Size(3, 3);
            base.Name = "Krc4Button";
            base.Size = new Size(66, 40);
            base.Paint += new PaintEventHandler(this.Krc4Button_Paint);
            base.FontChanged += new EventHandler(this.Krc4Button_FontChanged);
            base.Resize += new EventHandler(this.Krc4Button_Resize);
            base.ResumeLayout(false);
        }
    }
}
