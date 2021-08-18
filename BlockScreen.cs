using System.Drawing;
using System.Windows.Forms;

namespace Zen_Mode
{
    public class BlockScreen : Form
    {
        public BlockScreen(Screen screen)
        {
            this.Show();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Left = screen.Bounds.Left;
            this.Top = screen.Bounds.Top;
            this.Width = screen.Bounds.Width;
            this.Height = screen.Bounds.Height;
            this.BackColor = Color.Black;
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
    }
}
