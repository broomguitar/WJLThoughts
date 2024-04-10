using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WJLThoughts.Common.Winform.Contorls
{
    public partial class MyGroupBox : GroupBox
    {
        public MyGroupBox()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        public MyGroupBox(IContainer container) : this()
        {
            container.Add(this);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //背景颜色

            e.Graphics.Clear(this.BackColor);

            SizeF fontSize = e.Graphics.MeasureString(this.Text, this.Font);

            //背景颜色
            var foreBrush = new SolidBrush(this.ForeColor);
            Pen pen = new Pen(this.ForeColor);
            e.Graphics.DrawString(this.Text, this.Font, foreBrush, (this.Width - fontSize.Width) / 2, -3);

            e.Graphics.DrawLine(pen, 1, 7, (this.Width - fontSize.Width) / 2, 7);

            e.Graphics.DrawLine(pen, (this.Width + fontSize.Width) / 2 - 4, 7, this.Width - 2, 7);

            e.Graphics.DrawLine(pen, 1, 7, 1, this.Height - 2);

            e.Graphics.DrawLine(pen, 1, this.Height - 2, this.Width - 2, this.Height - 2);

            e.Graphics.DrawLine(pen, this.Width - 2, 7, this.Width - 2, this.Height - 2);
            //base.OnPaint(e);
        }
    }
}
