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
            SetValueToForm(parameters);
            _parameters = parameters;
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            SetValueToForm(new Parameters());
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            GetValueFromForm();

            this.Close();
            this.Dispose();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            GetValueFromForm();
            var saveDialog = new SaveFileDialog
            {
                Filter = "Файл параметров|*.txt"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK) return;
            
            ParametersRepository.SaveParameters(_parameters, saveDialog.FileName);
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "Файл параметров|*.txt"
            };
            
            if (openDialog.ShowDialog() != DialogResult.OK) return;

            SetValueToForm(ParametersRepository.GetParameters(openDialog.FileName));
        }

        private void SetValueToForm(Parameters parameters)
        {
            if (parameters.Frequences == null)
            {
                return;
            }
            
            N.Value = parameters.N;
            Z.Value = (decimal)parameters.Step;
            P.Value = (decimal)parameters.LoopP;

            Vds0.Value = (decimal)parameters.Vds0;
            Ids0.Value = (decimal)parameters.Ids0;

            Cgs.Value = (decimal)parameters.Cgs;
            Cds.Value = (decimal)parameters.Cds;
            Cgd.Value = (decimal)parameters.Cgd;
            Ls.Value = (decimal)parameters.Ls;
            Ld.Value = (decimal)parameters.Ld;
            gm.Value = (decimal)parameters.Gm;

            f.Text = parameters.ConvertFrequencesFromListToString();
        }

        private void GetValueFromForm()
        {
            if (_parameters == null)
            {
                _parameters = new Parameters();
            }

            _parameters.Vds0 = (float)Vds0.Value;
            _parameters.Ids0 = (float)Ids0.Value;

            _parameters.Cgs = (float)Cgs.Value;
            _parameters.Cds = (float)Cds.Value;
            _parameters.Cgd = (float)Cgd.Value;
            _parameters.Ls = (float)Ls.Value;
            _parameters.Ld = (float)Ld.Value;
            _parameters.Gm = (float)gm.Value;

            _parameters.N = (int)N.Value;
            _parameters.Step = (float)Z.Value;
            _parameters.LoopP = (float)P.Value;

            _parameters.Frequences = _parameters.GetFrequencesFromString(f.Text);
        }
    }
}
