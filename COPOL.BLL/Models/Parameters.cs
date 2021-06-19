using System;
using System.Collections.Generic;

namespace COPOL.BLL.Models
{
    public class Parameters
    {
        // Параметры контуров.
        private List<float> _frequences;
        private int _n;
        private float _step;

        // Параметры транзистора.
        private float _cgs;
        private float _cds;
        private float _cgd; 
        private float _ls; 
        private float _ld; 
        private float _gm; 

        // Рабочая точка.
        private float _vds0; 
        private float _ids0; 
        private float _loopP; 
        
        public List<float> Frequences
        {
            get => _frequences;
            set
            {
                value.ForEach(x => ValidateValue(x, "Частота"));
                _frequences = value;
            }
        }
        
        public int N
        {
            get => _n;
            set
            {
                ValidateValue(value, "N");
                _n = value;
            }
        }
        
        public float Step
        {
            get => _step;
            set
            {
                ValidateValue(value, "Step");
                _step = value;
            }
        }
        
        public float Cgs
        {
            get => _cgs;
            set
            {
                ValidateValue(value, "Cgs");
                _cgs = value;
            }
        }

        public float Cds 
        {
            get => _cds;
            set
            {
                ValidateValue(value, "Cds");
                _cds = value;
            }
        }
        
        public float Cgd
        {
            get => _cgd;
            set
            {
                ValidateValue(value, "Cgd");
                _cgd = value;
            }
        }
        
        public float Ls 
        {
            get => _ls;
            set
            {
                ValidateValue(value, "Ls");
                _ls = value;
            }
        }
        
        public float Ld 
        {
            get => _ld;
            set
            {
                ValidateValue(value, "Ld");
                _ld = value;
            }
        }
        
        public float Gm 
        {
            get => _gm;
            set
            {
                ValidateValue(value, "Gm");
                _gm = value;
            }
        }
        
        public float Vds0  {
            get => _vds0;
            set
            {
                ValidateValue(value, "Vds0");
                _vds0 = value;
            }
        }
        public float Ids0  {
            get => _ids0;
            set
            {
                ValidateValue(value, "Ids0");
                _ids0 = value;
            }
        }
        
        public float LoopP
        {
            get => _loopP;
            set
            {
                ValidateValue(value, "P");
                _loopP = value;
            }
        }

        public Parameters()
        {
            _n = 0;
            _step = 0;
            _cgs = 0;
            _cds = 0;
            _cgd = 0;
            _ls = 0;
            _ld = 0;
            _gm = 0;
            _vds0 = 0;
            _ids0 = 0;
            _loopP = 0;
        }
        
        /// <summary>
        /// Проверка присваиваемого значения.
        /// </summary>
        /// <param name="value">Присваиваемая переменная.</param>
        /// <param name="name">Имя параметра.</param>
        private static void ValidateValue(double value, string name)
        {
            if (value < 0)
            {
                throw new ArgumentException($"{name} не может быть меньше нуля!");
            }
        }
    }
}
