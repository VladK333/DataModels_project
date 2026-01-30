using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ACLineSegment : Conductor
    {
        private float b0ch;
        private float bch;
        private float g0ch;
        private float gch;
        private float r;
        private float r0;
        private float x;
        private float x0;

        public ACLineSegment(long globalId) : base(globalId)
        {

        }

        #region Properties
        public float R
        {
            get { return r; }
            set { r = value; }
        }

        public float R0
        {
            get { return r0; }
            set { r0 = value; }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float X0
        {
            get { return x0; }
            set { x0 = value; }
        }

        public float B0ch
        {
            get { return b0ch; }
            set { b0ch = value; }
        }

        public float Bch
        {
            get { return bch; }
            set { bch = value; }
        }

        public float G0ch
        {
            get { return g0ch; }
            set { g0ch = value; }
        }

        public float Gch
        {
            get { return gch; }
            set { gch = value; }
        }
        #endregion Properties

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, null))
            {
                return false;
            }
            else
            {
                ACLineSegment io = (ACLineSegment)obj;
                return ((io.R == this.R) && (io.R0 == this.R0) && (io.X == this.X) && (io.X0 == this.X0) 
                        && (io.B0ch == this.B0ch) && (io.Bch == this.Bch) && (io.G0ch == this.G0ch) && (io.Gch == this.Gch));
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
                case ModelCode.ACLSEGMENT_R:
                case ModelCode.ACLSEGMENT_R0:
                case ModelCode.ACLSEGMENT_X:
                case ModelCode.ACLSEGMENT_X0:
                case ModelCode.ACLSEGMENT_B0CH:
                case ModelCode.ACLSEGMENT_BCH:
                case ModelCode.ACLSEGMENT_G0CH:
                case ModelCode.ACLSEGMENT_GCH:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ACLSEGMENT_R:
                    property.SetValue(r);
                    break;
                case ModelCode.ACLSEGMENT_R0:
                    property.SetValue(r0);
                    break;
                case ModelCode.ACLSEGMENT_X:
                    property.SetValue(x);
                    break;
                case ModelCode.ACLSEGMENT_X0:
                    property.SetValue(x0);
                    break;
                case ModelCode.ACLSEGMENT_B0CH:
                    property.SetValue(b0ch);
                    break;
                case ModelCode.ACLSEGMENT_BCH:
                    property.SetValue(bch);
                    break;
                case ModelCode.ACLSEGMENT_G0CH:
                    property.SetValue(g0ch);
                    break;
                case ModelCode.ACLSEGMENT_GCH:
                    property.SetValue(gch);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ACLSEGMENT_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.ACLSEGMENT_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.ACLSEGMENT_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.ACLSEGMENT_X0:
                    x0 = property.AsFloat();
                    break;
                case ModelCode.ACLSEGMENT_B0CH:
                    b0ch = property.AsFloat();
                    break;
                case ModelCode.ACLSEGMENT_BCH:
                    bch = property.AsFloat();
                    break;
                case ModelCode.ACLSEGMENT_G0CH:
                    g0ch = property.AsFloat();
                    break;
                case ModelCode.ACLSEGMENT_GCH:
                    bch = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion IAccess implementation
    }
}
