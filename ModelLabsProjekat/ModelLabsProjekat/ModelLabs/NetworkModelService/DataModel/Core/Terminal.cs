using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private bool connected;
        private PhaseCode phases;
        private int sequenceNumber;
        private long conductingEquipment;
        private List<long> transformerEnd = new List<long>();
        private List<long> regulatingControl = new List<long>();

        public Terminal(long globalId) : base(globalId) { }

        public bool Connected { get { return connected; } set { connected = value; } }
        public PhaseCode Phases { get { return phases; } set { phases = value; } }
        public int SequenceNumber { get { return sequenceNumber; } set { sequenceNumber = value; } }
        public long ConductingEquipment { get { return conductingEquipment; } set { conductingEquipment = value; } }
        public List<long> TransformerEnd { get { return transformerEnd; } set { transformerEnd = value; } }
        public List<long> RegulatingControl { get { return regulatingControl; } set { regulatingControl = value; } }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return ((x.connected == this.connected) && (x.phases == this.phases) && (x.sequenceNumber == this.sequenceNumber)
                        && (x.conductingEquipment == this.conductingEquipment)
                        && (CompareHelper.CompareLists(x.transformerEnd, this.transformerEnd))
                        && (CompareHelper.CompareLists(x.regulatingControl, this.regulatingControl)));
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
                case ModelCode.TERMINAL_CONNECTED:
                case ModelCode.TERMINAL_PHASES:
                case ModelCode.TERMINAL_SEQNUMBER:
                case ModelCode.TERMINAL_TRANSFORMEREND:
                case ModelCode.TERMINAL_CONEQUIP:
                case ModelCode.TERMINAL_REGULATINGCONTROL:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TERMINAL_CONNECTED:
                    prop.SetValue(connected);
                    break;
                case ModelCode.TERMINAL_PHASES:
                    prop.SetValue((short)phases);
                    break;
                case ModelCode.TERMINAL_SEQNUMBER:
                    prop.SetValue(sequenceNumber);
                    break;
                case ModelCode.TERMINAL_TRANSFORMEREND:
                    prop.SetValue(transformerEnd);
                    break;
                case ModelCode.TERMINAL_CONEQUIP:
                    prop.SetValue(conductingEquipment);
                    break;
                case ModelCode.TERMINAL_REGULATINGCONTROL:
                    prop.SetValue(regulatingControl);
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
                case ModelCode.TERMINAL_CONNECTED:
                    connected = property.AsBool();
                    break;
                case ModelCode.TERMINAL_PHASES:
                    phases = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.TERMINAL_SEQNUMBER:
                    sequenceNumber = property.AsInt();
                    break;
                case ModelCode.TERMINAL_CONEQUIP:
                    conductingEquipment = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return transformerEnd.Count > 0 || regulatingControl.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (conductingEquipment != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONEQUIP] = new List<long>();
                references[ModelCode.TERMINAL_CONEQUIP].Add(conductingEquipment);
            }

            if (transformerEnd != null && transformerEnd.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_TRANSFORMEREND] = transformerEnd.GetRange(0, transformerEnd.Count);
            }

            if (regulatingControl != null && regulatingControl.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_REGULATINGCONTROL] = regulatingControl.GetRange(0, regulatingControl.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TRANSFORMEREND_TERMINAL:
                    transformerEnd.Add(globalId);
                    break;

                case ModelCode.REGULATIONGCONTROL_TERMINAL:
                    regulatingControl.Add(globalId);
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
                case ModelCode.TRANSFORMEREND_TERMINAL:

                    if (transformerEnd.Contains(globalId))
                    {
                        transformerEnd.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                case ModelCode.REGULATIONGCONTROL_TERMINAL:

                    if (regulatingControl.Contains(globalId))
                    {
                        regulatingControl.Remove(globalId);
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