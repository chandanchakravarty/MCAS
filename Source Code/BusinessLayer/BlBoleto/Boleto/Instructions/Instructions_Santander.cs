using System;
using System.Collections;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    #region Enumeration

    public enum EnumInstrucoes_Santander
    {
    }

    #endregion

    public class Instructions_Santander: AbstractInstructions, IInstructions
    {

        #region Constructors
        public Instructions_Santander()
        {
            try
            {
                this.Bank = new Bank(33);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading object", ex);
            }
        }
        public Instructions_Santander(Bank Bank, int Code)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Error loading object", ex);
            }
        }

        public Instructions_Santander(int Code)
        {
            this.Load(Code, 0);
        }

        public Instructions_Santander(EnumInstrucoes_Santander Code)
        {
            this.Load((int)Code, 0);
        }

        public Instructions_Santander(int Code, int nrDias)
        {
            this.Load(Code, nrDias);
        }
        #endregion

        #region Metodos Privados

        private void Load(int idInstrucao, int nrDias)
        {
            try
            {
                this.Bank = new Bank_Santander();
                this.Validate();

                switch ((EnumInstrucoes_Santander)idInstrucao)
                {
                    //case EnumInstrucoes_Bradesco.Protestar:
                    //    this.Code = (int)EnumInstrucoes_Bradesco.Protestar;
                    //    this.Descricao = "Protestar";
                    //    break;
                    default:
                        this.Code = 0;
                        this.Description = "";
                        break;
                }

                this.QuantityDays = nrDias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading object", ex);
            }
        }

        public override void Validate()
        {
            //base.Valida();
        }

        #endregion

    }
}
