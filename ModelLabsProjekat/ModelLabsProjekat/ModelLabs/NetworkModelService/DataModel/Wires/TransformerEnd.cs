using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class TransformerEnd : IdentifiedObject
    {
        private long terminal;

        public TransformerEnd(long globalId) : base(globalId) { }

        public long Terminal { get { return terminal; } set { terminal = value; } }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TransformerEnd x = (TransformerEnd)obj;
                return (x.terminal == this.terminal);
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
                case ModelCode.TRANSFORMEREND_TERMINAL:
                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TRANSFORMEREND_TERMINAL:
                    prop.SetValue(terminal);
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
                case ModelCode.TRANSFORMEREND_TERMINAL:
                    terminal = property.AsReference();
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
            if (terminal != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TRANSFORMEREND_TERMINAL] = new List<long>();
                references[ModelCode.TRANSFORMEREND_TERMINAL].Add(terminal);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}