using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public abstract class AbstractCodeMotion : ICodeMotion
    {

        #region Variables

        private IBank _bank;
        private int _code;
        private string _description;

        #endregion

        # region Properties

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

        # endregion

    }
}
