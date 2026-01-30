using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class TapChangerControl : RegulatingControl
    {
        private float limitVoltage;
        private bool lineDropCompensation;
        private float lineDropR;
        private float lineDropX;
        private float reverseLineDropR;
        private float reverseLineDropX;
        private List<long> tapChanger = new List<long>();
        public TapChangerControl(long globalId) : base(globalId) { }
        public float LimitVoltage { get { return limitVoltage; } set { limitVoltage = value; } }
        public bool LineDropCompensation { get { return lineDropCompensation; } set { lineDropCompensation = value; } }
        public float LineDropR { get { return lineDropR; } set { lineDropR = value; } }
        public float LineDropX { get { return lineDropX; } set { lineDropX = value; } }
        public float ReverseLineDropR { get { return reverseLineDropR; } set { reverseLineDropR = value; } }
        public float ReverseLineDropX { get { return reverseLineDropX; } set { reverseLineDropX = value; } }
        public List<long> TapChanger { get { return tapChanger; } set { tapChanger = value; } }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TapChangerControl x = (TapChangerControl)obj;
                return ((x.limitVoltage == this.limitVoltage) && (x.lineDropCompensation == this.lineDropCompensation) && (x.lineDropR == this.lineDropR)
                        && (x.lineDropX == this.lineDropX) && (x.reverseLineDropR == this.reverseLineDropR) && (x.reverseLineDropX == this.reverseLineDropX)
                        && (CompareHelper.CompareLists(x.tapChanger, this.tapChanger)));
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
        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.TAPCHANGERCONTROL_LV:
                case ModelCode.TAPCHANGERCONTROL_LDC:
                case ModelCode.TAPCHANGERCONTROL_LDR:
                case ModelCode.TAPCHANGERCONTROL_LDX:
                case ModelCode.TAPCHANGERCONTROL_RLDR:
                case ModelCode.TAPCHANGERCONTROL_RLDX:
                case ModelCode.TAPCHANGERCONTROL_TAPCHANGER:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }
        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TAPCHANGERCONTROL_LV:
                    prop.SetValue(limitVoltage);
                    break;
                case ModelCode.TAPCHANGERCONTROL_LDC:
                    prop.SetValue(lineDropCompensation);
                    break;
                case ModelCode.TAPCHANGERCONTROL_LDR:
                    prop.SetValue(lineDropR);
                    break;
                case ModelCode.TAPCHANGERCONTROL_LDX:
                    prop.SetValue(lineDropX);
                    break;
                case ModelCode.TAPCHANGERCONTROL_RLDR:
                    prop.SetValue(reverseLineDropR);
                    break;
                case ModelCode.TAPCHANGERCONTROL_RLDX:
                    prop.SetValue(reverseLineDropX);
                    break;
                case ModelCode.TAPCHANGERCONTROL_TAPCHANGER:
                    prop.SetValue(tapChanger);
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
                case ModelCode.TAPCHANGERCONTROL_LV:
                    limitVoltage = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGERCONTROL_LDC:
                    lineDropCompensation = property.AsBool();
                    break;
                case ModelCode.TAPCHANGERCONTROL_LDR:
                    lineDropR = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGERCONTROL_LDX:
                    lineDropX = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGERCONTROL_RLDR:
                    reverseLineDropR = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGERCONTROL_RLDX:
                    reverseLineDropX = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return tapChanger.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (tapChanger != null && tapChanger.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TAPCHANGERCONTROL_TAPCHANGER] = tapChanger.GetRange(0, tapChanger.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TAPCHANGER_TAPCHANGERCONTROL:
                    tapChanger.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TAPCHANGER_TAPCHANGERCONTROL:

                    if (tapChanger.Contains(globalId))
                    {
                        tapChanger.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation
    }
}