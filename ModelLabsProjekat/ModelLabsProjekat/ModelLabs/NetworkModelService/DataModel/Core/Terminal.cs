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
        private long conductingEquipment;
        private long connectivityNode;
        public Terminal(long globalId) : base(globalId)
        {
            
        }

        #region Properties
        public long ConductingEquipment
        {
            get { return conductingEquipment; }
            set { conductingEquipment = value; }
        }

        public long ConnectivityNode
        {
            get { return connectivityNode; }
            set { connectivityNode = value; }
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
                Terminal io = (Terminal)obj;
                return ((io.ConnectivityNode == this.ConnectivityNode) && (io.ConductingEquipment == this.ConductingEquipment));
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
                case ModelCode.TERMINAL_CONEQUIP:
                case ModelCode.TERMINAL_CONNODE:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TERMINAL_CONEQUIP:
                    property.SetValue(conductingEquipment);
                    break;

                case ModelCode.TERMINAL_CONNODE:
                    property.SetValue(connectivityNode);
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
                case ModelCode.TERMINAL_CONEQUIP:
                    conductingEquipment = property.AsReference();
                    break;

                case ModelCode.TERMINAL_CONNODE:
                    connectivityNode = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion IAccess implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (connectivityNode != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONNODE] = new List<long>();
                references[ModelCode.TERMINAL_CONNODE].Add(connectivityNode);
            }

            if (conductingEquipment != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONEQUIP] = new List<long>();
                references[ModelCode.TERMINAL_CONEQUIP].Add(conductingEquipment);
            }

            base.GetReferences(references, refType);
        }
    }
}
