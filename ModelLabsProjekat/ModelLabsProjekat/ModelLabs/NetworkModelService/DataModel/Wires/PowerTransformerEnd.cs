using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

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

        public float B { get { return b; } set { b = value; } }
        public float B0 { get { return b0; } set { b0 = value; } }
        public WindingConnection ConnectionKind { get { return connectionKind; } set { connectionKind = value; } }
        public float G { get { return g; } set { g = value; } }
        public float G0 { get { return g0; } set { g0 = value; } }
        public int PhaseAngleClock { get { return phaseAngleClock; } set { phaseAngleClock = value; } }
        public float R { get { return r; } set { r = value; } }
        public float R0 { get { return r0; } set { r0 = value; } }
        public float RatedS { get { return ratedS; } set { ratedS = value; } }
        public float RatedU { get { return ratedU; } set { ratedU = value; } }
        public float X { get { return x; } set { x = value; } }
        public float X0 { get { return x0; } set { x0 = value; } }
        public long PowerTransformer { get { return powerTransformer; } set { powerTransformer = value; } }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PowerTransformerEnd x = (PowerTransformerEnd)obj;
                return ((x.b == this.b) && (x.b0 == this.b0) && (x.connectionKind == this.connectionKind)
                        && (x.g == this.g) && (x.g0 == this.g0) && (x.phaseAngleClock == this.phaseAngleClock)
                        && (x.r == this.r) && (x.r0 == this.r0) && (x.ratedS == this.ratedS)
                        && (x.ratedU == this.ratedU) && (x.x == this.x) && (x.x0 == this.x0)
                        && (x.powerTransformer == this.powerTransformer));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.POWERTRANSEND_B:
                case ModelCode.POWERTRANSEND_B0:
                case ModelCode.POWERTRANSEND_CK:
                case ModelCode.POWERTRANSEND_G:
                case ModelCode.POWERTRANSEND_G0:
                case ModelCode.POWERTRANSEND_PAC:
                case ModelCode.POWERTRANSEND_R:
                case ModelCode.POWERTRANSEND_R0:
                case ModelCode.POWERTRANSEND_RS:
                case ModelCode.POWERTRANSEND_RU:
                case ModelCode.POWERTRANSEND_X:
                case ModelCode.POWERTRANSEND_X0:
                case ModelCode.POWERTRANSEND_PTRANS:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.POWERTRANSEND_B:
                    prop.SetValue(b);
                    break;
                case ModelCode.POWERTRANSEND_B0:
                    prop.SetValue(b0);
                    break;
                case ModelCode.POWERTRANSEND_CK:
                    prop.SetValue((short)connectionKind);
                    break;
                case ModelCode.POWERTRANSEND_G:
                    prop.SetValue(g);
                    break;
                case ModelCode.POWERTRANSEND_G0:
                    prop.SetValue(g0);
                    break;
                case ModelCode.POWERTRANSEND_PAC:
                    prop.SetValue(phaseAngleClock);
                    break;
                case ModelCode.POWERTRANSEND_R:
                    prop.SetValue(r);
                    break;
                case ModelCode.POWERTRANSEND_R0:
                    prop.SetValue(r0);
                    break;
                case ModelCode.POWERTRANSEND_RS:
                    prop.SetValue(ratedS);
                    break;
                case ModelCode.POWERTRANSEND_RU:
                    prop.SetValue(ratedU);
                    break;
                case ModelCode.POWERTRANSEND_X:
                    prop.SetValue(x);
                    break;
                case ModelCode.POWERTRANSEND_X0:
                    prop.SetValue(x0);
                    break;
                case ModelCode.POWERTRANSEND_PTRANS:
                    prop.SetValue(powerTransformer);
                    break;
                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.POWERTRANSEND_B:
                    b = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_B0:
                    b0 = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_CK:
                    connectionKind = (WindingConnection)property.AsEnum();
                    break;
                case ModelCode.POWERTRANSEND_G:
                    g = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_G0:
                    g0 = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_PAC:
                    phaseAngleClock = property.AsInt();
                    break;
                case ModelCode.POWERTRANSEND_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_RS:
                    ratedS = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_RU:
                    ratedU = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_X0:
                    x0 = property.AsFloat();
                    break;
                case ModelCode.POWERTRANSEND_PTRANS:
                    powerTransformer = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (powerTransformer != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.POWERTRANSEND_PTRANS] = new List<long>();
                references[ModelCode.POWERTRANSEND_PTRANS].Add(powerTransformer);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}