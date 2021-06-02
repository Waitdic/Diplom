using System;
using System.Windows.Forms;
using COPOL.BLL.Models;

namespace COPOL.Forms
{
    public partial class InitialDataForm : Form
    {
        private Parameters _parameters;

        public InitialDataForm()
        {
            InitializeComponent();
        }

        public void SetParameters(Parameters parameters)
        {
            if (parameters != null)
            {
                SetValueToForm(parameters);
            }

            _parameters = parameters;
        }


        private void CleanButton_Click(object sender, EventArgs e)
        {
            SetValueToForm(new Parameters());
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _parameters.Vds0 = (float) Vds0.Value;
            _parameters.Ids0 = (float) Ids0.Value;

            _parameters.Cgs = (float) Cgs.Value;
            _parameters.Cds = (float) Cds.Value;
            _parameters.Cgd = (float) Cgd.Value;
            _parameters.Ls = (float) Ls.Value;
            _parameters.Ld = (float) Ld.Value;
            _parameters.Gm = (float) gm.Value;

            _parameters.N = (int) N.Value;
            _parameters.Difference = (float) Z.Value;
            _parameters.POfContour = (float) P.Value;

            

            this.Close();
            this.Dispose();
        }

        private void SetValueToForm(Parameters parameters)
        {
            N.Value = (decimal)parameters.N;
            Z.Value = (decimal)parameters.Difference;
            P.Value = (decimal)parameters.POfContour;

            Vds0.Value = (decimal)parameters.Vds0;
            Ids0.Value = (decimal)parameters.Ids0;

            Cgs.Value = (decimal)parameters.Cgs;
            Cds.Value = (decimal)parameters.Cds;
            Cgd.Value = (decimal)parameters.Cgd;
            Ls.Value = (decimal)parameters.Ls;
            Ld.Value = (decimal)parameters.Ld;
            gm.Value = (decimal)parameters.Gm;
        }
    }
}
