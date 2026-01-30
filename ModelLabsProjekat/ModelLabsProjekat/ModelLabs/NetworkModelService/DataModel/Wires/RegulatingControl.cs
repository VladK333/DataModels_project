using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingControl : PowerSystemResource
    {
        private bool discrete;
        private RegulatingControlModeKind mode;
        private PhaseCode monitoredPhase;
        private float targetRange;
        private float targetValue;
        private long terminal;

        //kako reference?
        //kako enum u get metodi

        public RegulatingControl(long globalId) : base(globalId) { }

        public bool Discrete { get { return discrete; } set { discrete = value; } }
        public RegulatingControlModeKind Mode { get { return mode; } set { mode = value; } }
        public PhaseCode MonitoredPhase { get { return monitoredPhase; } set { monitoredPhase = value; } }
        public float TargetRange { get { return targetRange; } set { targetRange = value; } }
        public float TargetValue { get { return targetValue; } set { targetValue = value; } }
        public long Terminal { get { return terminal; } set { terminal = value; } }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingControl x = (RegulatingControl)obj;
                return ((x.discrete == this.discrete) && (x.mode == this.mode) && (x.monitoredPhase == this.monitoredPhase)
                        && (x.targetRange == this.targetRange) && (x.targetValue == this.targetValue) && (x.terminal == this.terminal));
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
                case ModelCode.REGULATIONGCONTROL_DISC:
                case ModelCode.REGULATIONGCONTROL_MODE:
                case ModelCode.REGULATIONGCONTROL_MP:
                case ModelCode.REGULATIONGCONTROL_TR:
                case ModelCode.REGULATIONGCONTROL_TV:
                case ModelCode.REGULATIONGCONTROL_TERMINAL:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULATIONGCONTROL_DISC:
                    property.SetValue(discrete);
                    break;

                case ModelCode.REGULATIONGCONTROL_MODE:
                    property.SetValue((short)mode);
                    break;

                case ModelCode.REGULATIONGCONTROL_MP:
                    property.SetValue((short)monitoredPhase);
                    break;

                case ModelCode.REGULATIONGCONTROL_TR:
                    property.SetValue(targetRange);
                    break;

                case ModelCode.REGULATIONGCONTROL_TV:
                    property.SetValue(targetValue);
                    break;

                case ModelCode.REGULATIONGCONTROL_TERMINAL:
                    property.SetValue(terminal);
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
                case ModelCode.REGULATIONGCONTROL_DISC:
                    discrete = property.AsBool();
                    break;

                case ModelCode.REGULATIONGCONTROL_MODE:
                    mode = (RegulatingControlModeKind)property.AsEnum();
                    break;

                case ModelCode.REGULATIONGCONTROL_MP:
                    monitoredPhase = (PhaseCode)property.AsEnum();
                    break;

                case ModelCode.REGULATIONGCONTROL_TR:
                    TargetRange = property.AsFloat();
                    break;

                case ModelCode.REGULATIONGCONTROL_TV:
                    targetValue = property.AsFloat();
                    break;

                case ModelCode.REGULATIONGCONTROL_TERMINAL:
                    terminal = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (terminal != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULATIONGCONTROL_TERMINAL] = new List<long>();
                references[ModelCode.REGULATIONGCONTROL_TERMINAL].Add(terminal);
            }

            base.GetReferences(references, refType);
        }
    }
}
