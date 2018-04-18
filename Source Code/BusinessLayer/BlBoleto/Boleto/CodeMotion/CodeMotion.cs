using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public class CodeMotion : AbstractCodeMotion, ICodeMotion
    {

        #region Variaveis

        private ICodeMotion _ICodeMotion;

        #endregion

        # region Construtores

        internal CodeMotion()
        {
        }

        public CodeMotion(int bankcode, int codemotion)
        {
            try
            {
                InstanceCodeMovement(bankcode, codemotion);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto.", ex);
            }
        }

        # endregion

        #region Interface Properties

        public override IBank Bank
        {
            get { return _ICodeMotion.Bank; }
        }

        public override int Code
        {
            get { return _ICodeMotion.Code; }
        }

        public override string Description
        {
            get { return _ICodeMotion.Description; }
        }

        #endregion

        # region private methods

        private void InstanceCodeMovement(int bankcode, int codemotion)
        {
            try
            {
                switch (bankcode)
                {
                    //341 - Itaú
                    case 341:
                        //_ICodeMotion = new CodigoMovimento_Itau();
                        throw new Exception("Bank Code not implementing: " + bankcode);
                    //1 - Banco do Brasil
                    case 1:
                        //_ICodeMotion = new CodigoMovimento_BancoBrasil(codigoMovimento);
                        break;
                    //356 - Real
                    case 356:
                        //_ICodeMotion = new CodigoMovimento_Real();
                        throw new Exception("Bank Code not implementing: " + bankcode);
                    //422 - Safra
                    case 422:
                        //_ICodeMotion = new CodigoMovimento_Safra();
                        throw new Exception("Bank Code not implementing: " + bankcode);
                    //237 - Bradesco
                    case 237:
                        //_ICodeMotion = new CodigoMovimento_Bradesco();
                        throw new Exception("Bank Code not implementing: " + bankcode);
                    //347 - Sudameris
                    case 347:
                        //_ICodeMotion = new CodigoMovimento_Sudameris();
                        throw new Exception("Bank Code not implementing: " + bankcode);
                    //353 - Santander
                    case 353:
                        //_ICodeMotion = new CodigoMovimento_Santander();
                        throw new Exception("Bank Code not implementing: " + bankcode);
                    //070 - BRB
                    case 70:
                        //_ICodeMotion = new CodigoMovimento_BRB();
                        throw new Exception("Bank Code not implementing: " + bankcode);
                    //479 - BankBoston
                    case 479:
                        //_ICodeMotion = new CodigoMovimento_BankBoston();
                        throw new Exception("Bank Code not implementing: " + bankcode);
                    default:
                        throw new Exception("Bank Code not implementing: " + bankcode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error during execution of the transaction.", ex);
            }
        }

        # endregion

    }
}
