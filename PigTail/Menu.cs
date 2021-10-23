using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigTail
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Local_Mode_Loading(object sender, EventArgs e)
        {
            LocalMode local_screen = new LocalMode(this);
            this.Hide();
            local_screen.StartPosition = FormStartPosition.Manual;
            local_screen.Location = this.Location;
            local_screen.ShowDialog();
        }

        private void Internet_Mode_Loading(object sender, EventArgs e)
        {
            InternetMenu internet_screen = new InternetMenu(this);
            this.Hide();
            internet_screen.StartPosition = FormStartPosition.Manual;
            internet_screen.Location = this.Location;
            internet_screen.ShowDialog();
        }

        private void AI_Mode_Loading(object sender, EventArgs e)
        {
            AIMode ai_screen = new AIMode(this);
            this.Hide();
            ai_screen.StartPosition = FormStartPosition.Manual;
            ai_screen.Location = this.Location;
            ai_screen.ShowDialog();
        }
    }
}
