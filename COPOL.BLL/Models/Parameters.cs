﻿using System.Collections.Generic;

namespace COPOL.BLL.Models
{
    public class Parameters
    {
        //структура для передачи параметров между модулем и основной программой
        public List<float> Frequences { get; set; }
        public int N { get; set; }
        public float Difference { get; set; }
        
        //параметры транзистора
        public float Cgs { get; set; }
        public float Cds { get; set; }
        public float Cgd { get; set; }
        public float Ls { get; set; }
        public float Ld { get; set; }
        public float Gm { get; set; }

        //рабочая точка
        public float Vds0 { get; set; }
        public float Ids0 { get; set; }
        public float POfContour { get; set; }

        public Parameters()
        {
            N = 0;
            Difference = 0;
            Cgs = 0;
            Cds = 0;
            Cgd = 0;
            Ls = 0;
            Ld = 0;
            Gm = 0;
            Vds0 = 0;
            Ids0 = 0;
            POfContour = 0;
        }
    }
}