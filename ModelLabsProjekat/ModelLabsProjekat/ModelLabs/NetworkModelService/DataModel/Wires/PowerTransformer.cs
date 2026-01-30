using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PowerTransformer : ConductingEquipment
    {
        private string vectorGroup;

        private List<long> powerTransformerEnd = new List<long>();

        public PowerTransformer(long globalId) : base(globalId) { }

        public string VectorGroup { get { return vectorGroup; } set { vectorGroup = value; } }
        public List<long> PowerTransformerEnd { get { return powerTransformerEnd; } set { powerTransformerEnd = value; } }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PowerTransformer x = (PowerTransformer)obj;
                return ((x.vectorGroup==this.vectorGroup) && (CompareHelper.CompareLists(x.powerTransformerEnd, this.powerTransformerEnd)));
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
                case ModelCode.POWERTRANS_VECTOR:
                case ModelCode.POWERTRANS_PTRANSEND:

                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.POWERTRANS_VECTOR:
                    prop.SetValue(vectorGroup);
                    break;
                case ModelCode.POWERTRANS_PTRANSEND:
                    prop.SetValue(powerTransformerEnd);
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
                case ModelCode.POWERTRANS_VECTOR:
                    vectorGroup = property.AsString();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced
        {
            get
            {
                return powerTransformerEnd.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (powerTransformerEnd != null && powerTransformerEnd.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONEQUIP_TERMINALS] = powerTransformerEnd.GetRange(0, powerTransformerEnd.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.POWERTRANSEND_PTRANS:
                    powerTransformerEnd.Add(globalId);
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
                case ModelCode.POWERTRANSEND_PTRANS:

                    if (powerTransformerEnd.Contains(globalId))
                    {
                        powerTransformerEnd.Remove(globalId);
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
    }
}
