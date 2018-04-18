using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public abstract class AbstractInstructions : IInstructions
    {

        #region Variaveis

        private IBank _bank;
        private int _code;
        private string _description;
        private int _QuantityDays;

        #endregion

        # region Propriedades

        public virtual IBank Bank
        {
            get { return _bank; }
            set { _bank = value; }
        }

        public virtual int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual int QuantityDays
        {
            get { return _QuantityDays; }
            set { _QuantityDays = value; }
        }

        # endregion

        # region Metodos

        public virtual void Validate()
        {
            throw new NotImplementedException("Function not implemented");
        }

        #endregion
    }
}
