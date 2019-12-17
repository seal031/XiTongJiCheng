using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangChunMenJin
{
    public partial class CloseForm : Form
    {
        public bool canClose = false;
        public CloseForm()
        {
            InitializeComponent();
        }

        private void btn_yes_Click(object sender, EventArgs e)
        {
            if (txt_close.Text.Trim() == "exit")
            {
                canClose = true;
                this.Close();
                Application.Exit();
            }
            else
            {
                canClose = false;
                this.Close();
            }
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            canClose = false;
            this.Close();
        }
    }
}
