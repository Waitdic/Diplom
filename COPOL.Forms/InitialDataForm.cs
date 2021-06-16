using System;
using System.Collections.Generic;
using System.Linq;
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
            
            SetValueToForm(parameters);
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

            _parameters.Frequences = GetFrequencesFromString();

            this.Close();
            this.Dispose();
        }

        private void SetValueToForm(Parameters parameters)
        {
            N.Value = parameters.N;
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

            f.Text = ConvertFrequencesFromListToString(parameters.Frequences);
        }

        private List<float> GetFrequencesFromString()
        {
            return !string.IsNullOrWhiteSpace(f.Text) ? f.Text
                .Replace('.', ',')
                .Split(';')
                .Select(float.Parse)
                .ToList() : new List<float>();
        }

        private string ConvertFrequencesFromListToString(IEnumerable<float> frequences)
        {
            var newString = "";
            if (frequences != null)
            {
                var list = frequences.Select(x => x + "; ");
                newString = list.Aggregate("", (current, value) => current + value);
            }

            return newString.TrimEnd(new char[] {';', ' '});
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

        }
    }
}
