using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
   public class AbstractDocumentkind : IDocumentkind
    {


        #region Variables

        private IBank _bank;
        private int _code;
        private string _sigla;
        private string _especie;

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

        public virtual string Sigla
        {
            get { return _sigla; }
            set { _sigla = value; }
        }

        public virtual string Especie
        {
            get { return _especie; }
            set { _especie = value; }
        }

        # endregion


    }
}
