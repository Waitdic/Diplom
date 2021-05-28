using System;
using System.Windows.Forms;

namespace COPOL.Forms
{
    public partial class InitialDataForm : Form
    {
        public InitialDataForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new DiagrammForm {Visible = true};
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
