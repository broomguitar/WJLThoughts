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
    [ToolboxItem(true)]
    public partial class MyProgressBar : ProgressBar
    {
        public MyProgressBar()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        public MyProgressBar(IContainer container) : this()
        {
            container.Add(this);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), 1, 1, rect.Width - 2, rect.Height - 2);
            rect.Height -= 4;
            rect.Width = (int)(rect.Width * ((double)Value / Maximum)) - 4;
            e.Graphics.FillRectangle(new SolidBrush(ForeColor), 2, 2, rect.Width, rect.Height);
            //Rectangle rect = e.ClipRectangle;
            //rect.Width = (int)(rect.Width*((double)Value/Maximum))-4;
            //if(ProgressBarRenderer.IsSupported)
            //{
            //    ProgressBarRenderer.DrawHorizontalBar(e.Graphics,e.ClipRectangle);
            //}
            //rect.Height = rect.Height - 4;
            //e.Graphics.FillRectangle(new SolidBrush(ForeColor), 2, 2, rect.Width,rect.Height);
            ////base.OnPaint(e);
        }
    }
}
