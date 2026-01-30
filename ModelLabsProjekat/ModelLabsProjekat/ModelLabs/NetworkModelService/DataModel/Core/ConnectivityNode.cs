using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class ConnectivityNode : IdentifiedObject
    {
        private List<long> terminals = new List<long>();
        private long connectivityNodeContainer;
        private string description;
        public ConnectivityNode(long globalId) : base(globalId)
        {
            
        }

        #region Properties
        public List<long> Terminals
        {
            get { return terminals; }
            set { terminals = value; }
        }

        public long ConnectivityNodeCintainer
        {
            get { return connectivityNodeContainer; }
            set { connectivityNodeContainer = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
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
                ConnectivityNode io = (ConnectivityNode)obj;
                return ((io.ConnectivityNodeCintainer == this.ConnectivityNodeCintainer) && (io.Description == this.Description) &&
                    (CompareHelper.CompareLists(io.Terminals, this.Terminals)));
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
                case ModelCode.CONNODE_CONNODECON:
                case ModelCode.CONNODE_TERMINALS:
                case ModelCode.CONNODE_DESC:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CONNODE_CONNODECON:
                    property.SetValue(connectivityNodeContainer);
                    break;

                case ModelCode.CONNODE_TERMINALS:
                    property.SetValue(terminals);
                    break;
                case ModelCode.CONNODE_DESC:
                    property.SetValue(description);
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
                case ModelCode.CONNODE_CONNODECON:
                    connectivityNodeContainer = property.AsReference();
                    break;

                case ModelCode.CONNODE_DESC:
                    description = property.AsString();
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
                return terminals.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (connectivityNodeContainer != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONNODE_CONNODECON] = new List<long>();
                references[ModelCode.CONNODE_CONNODECON].Add(connectivityNodeContainer);
            }

            if (terminals != null && terminals.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONEQUIP_TERMINALS] = terminals.GetRange(0, terminals.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TERMINAL_CONNODE:
                    terminals.Add(globalId);
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
                case ModelCode.TERMINAL_CONNODE:

                    if (terminals.Contains(globalId))
                    {
                        terminals.Remove(globalId);
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
