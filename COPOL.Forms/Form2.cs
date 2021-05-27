using System;

namespace COPOL.Forms
{
    public partial class Form2 : System.Windows.Forms.Form
    {
        public Form2()
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
