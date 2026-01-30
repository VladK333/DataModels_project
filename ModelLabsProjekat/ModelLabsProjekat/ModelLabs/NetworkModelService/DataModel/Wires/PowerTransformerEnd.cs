using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PowerTransformerEnd : TransformerEnd
    {
        private float b;
        private float b0;
        private WindingConnection connectionKind;
        private float g;
        private float g0;
        private int phaseAngleClock;
        private float r;
        private float r0;
        private float ratedS;
        private float ratedU;
        private float x;
        private float x0;
        private long powerTransformer;

        public PowerTransformerEnd(long globalId) : base(globalId) { }
    }
}
