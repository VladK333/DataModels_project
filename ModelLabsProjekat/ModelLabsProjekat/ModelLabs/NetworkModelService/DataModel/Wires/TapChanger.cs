using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class TapChanger : PowerSystemResource
    {
        private int highStep;
        private float initialDelay;
        private int lowStep;
        private bool ltcFlag;
        private int neutralStep;
        private float neutralU;
        private int normalStep;
        private bool regulationStatus;
        private float subsequentDelay;
        private long tapChangerControl;

        public TapChanger(long globalId) : base(globalId) { }

        public int HighStep { get { return highStep; } set { highStep = value; } }
        public float InitialDelay { get { return initialDelay; } set { initialDelay = value; } }
        public int LowStep { get { return lowStep; } set { lowStep = value; } }
        public bool LtcFlag { get { return ltcFlag; } set { ltcFlag = value; } }
        public int NeutralStep { get { return neutralStep; } set { neutralStep = value; } }
        public float NeutralU { get { return neutralU; } set { neutralU = value; } }
        public int NormalStep { get { return normalStep; } set { normalStep = value; } }
        public bool RegulationStatus { get { return regulationStatus; } set { regulationStatus = value; } }
        public float SubsequentDelay { get { return subsequentDelay; } set { subsequentDelay = value; } }
        public long TapChangerControl { get { return tapChangerControl; } set { tapChangerControl = value; } }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TapChanger x = (TapChanger)obj;
                return ((x.highStep == this.highStep) && (x.initialDelay == this.initialDelay) && (x.lowStep == this.lowStep)
                        && (x.ltcFlag == this.ltcFlag) && (x.neutralStep == this.neutralStep) && (x.neutralU == this.neutralU)
                        && (x.normalStep == this.normalStep) && (x.regulationStatus == this.regulationStatus)
                        && (x.subsequentDelay == this.subsequentDelay) && (x.tapChangerControl == this.tapChangerControl));
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
                case ModelCode.TAPCHANGER_HS:
                case ModelCode.TAPCHANGER_INITDEL:
                case ModelCode.TAPCHANGER_LS:
                case ModelCode.TAPCHANGER_IF:
                case ModelCode.TAPCHANGER_NEUS:
                case ModelCode.TAPCHANGER_NU:
                case ModelCode.TAPCHANGER_NORMS:
                case ModelCode.TAPCHANGER_RS:
                case ModelCode.TAPCHANGER_SD:
                case ModelCode.TAPCHANGER_TAPCHANGERCONTROL:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TAPCHANGER_HS:
                    prop.SetValue(highStep);
                    break;
                case ModelCode.TAPCHANGER_INITDEL:
                    prop.SetValue(initialDelay);
                    break;
                case ModelCode.TAPCHANGER_LS:
                    prop.SetValue(lowStep);
                    break;
                case ModelCode.TAPCHANGER_IF:
                    prop.SetValue(ltcFlag);
                    break;
                case ModelCode.TAPCHANGER_NEUS:
                    prop.SetValue(neutralStep);
                    break;
                case ModelCode.TAPCHANGER_NU:
                    prop.SetValue(neutralU);
                    break;
                case ModelCode.TAPCHANGER_NORMS:
                    prop.SetValue(normalStep);
                    break;
                case ModelCode.TAPCHANGER_RS:
                    prop.SetValue(regulationStatus);
                    break;
                case ModelCode.TAPCHANGER_SD:
                    prop.SetValue(subsequentDelay);
                    break;
                case ModelCode.TAPCHANGER_TAPCHANGERCONTROL:
                    prop.SetValue(tapChangerControl);
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
                case ModelCode.TAPCHANGER_HS:
                    highStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_INITDEL:
                    initialDelay = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGER_LS:
                    lowStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_IF:
                    ltcFlag = property.AsBool();
                    break;
                case ModelCode.TAPCHANGER_NEUS:
                    neutralStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_NU:
                    neutralU = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGER_NORMS:
                    normalStep = property.AsInt();
                    break;
                case ModelCode.TAPCHANGER_RS:
                    regulationStatus = property.AsBool();
                    break;
                case ModelCode.TAPCHANGER_SD:
                    subsequentDelay = property.AsFloat();
                    break;
                case ModelCode.TAPCHANGER_TAPCHANGERCONTROL:
                    tapChangerControl = property.AsReference();
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
            if (tapChangerControl != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TAPCHANGER_TAPCHANGERCONTROL] = new List<long>();
                references[ModelCode.TAPCHANGER_TAPCHANGERCONTROL].Add(tapChangerControl);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}