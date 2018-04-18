using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public abstract class AbstractPortfolio : IPortfolio
    {

        #region Variables

        private IBank _bank;
        private int _Wallet;
        private string _code;
        private string _Type;
        private string _description;

        #endregion

        # region Propriedades

        public virtual IBank Bank
        {
            get { return _bank; }
            set { _bank = value; }
        }

        public virtual int PortfolioNumber
        {
            get{ return _Wallet; }
            set { _Wallet = value; }
        }

        public virtual string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public virtual string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        # endregion

    }
}
