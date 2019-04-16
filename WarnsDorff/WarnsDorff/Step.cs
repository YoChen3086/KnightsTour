using System;
using System.Collections.Generic;
using System.Text;

namespace WarnsDorff
{
    public class Step
    {
        public Step(int ctr, int i)
        {
            this.ctr = ctr;
            this.i = i;
        }

        public int ctr { get; set; }
        public int i { get; set; }
    }
}
