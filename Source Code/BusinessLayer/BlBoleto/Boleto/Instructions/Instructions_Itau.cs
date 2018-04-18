using System;
using System.Collections;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    #region Enumerado

    public enum EnumInstrucoes_Itau
    {
        Protest = 9,                      // Sends notice to the drawee vencto after N days, and sends to the office after 5 days
        Not_Protest = 10,                  // Inhibits protest, when there is continuous education in the current account
        Importance_Per_Day_Discount = 30,
        ProtestoFinsFalimentares = 42,
        ProtestarAposNDiasCorridos = 81,
        ProtestarAposNDiasUteis = 82,
        NaoReceberAposNDias = 91,
        DevolverAposNDias = 92,
        JurosdeMora = 998,
        Discount_oppose_Day = 999,
    }

    #endregion 

    public class Instructions_Itau : AbstractInstructions, IInstructions
    {

        #region Construtores 

		public Instructions_Itau()
		{
			try
			{
                this.Bank = new Bank(341);
			}
			catch (Exception ex)
			{
                throw new Exception("Error loading object", ex);
			}
		}

        public Instructions_Itau(int Code, int nrDias)
        {
            this.Load(Code, nrDias);
        }

        public Instructions_Itau(int Code)
        {
            this.Load(Code, 0);
        }

		#endregion 

        #region Private Methods

        private void Load(int idInstrucao, int nrDias)
        {
            try
            {
                this.Bank = new Bank_Itau();
                this.Validate();

                switch ((EnumInstrucoes_Itau)idInstrucao)
                {
                    case EnumInstrucoes_Itau.Protest:
                        this.Code = (int)EnumInstrucoes_Itau.Protest;
                        this.Description = "Protest after 5 days.";
                        break;
                    case EnumInstrucoes_Itau.Not_Protest:
                        this.Code = (int)EnumInstrucoes_Itau.Not_Protest;
                        this.Description = "No protest";
                        break;
                    case EnumInstrucoes_Itau.Importance_Per_Day_Discount:
                        this.Code = (int)EnumInstrucoes_Itau.Importance_Per_Day_Discount;
                        this.Description = "Importance per day discount.";
                        break;
                    case EnumInstrucoes_Itau.ProtestoFinsFalimentares:
                        this.Code = (int)EnumInstrucoes_Itau.ProtestoFinsFalimentares;
                        this.Description = "Protest for bankruptcy.";
                        break;
                    case EnumInstrucoes_Itau.ProtestarAposNDiasCorridos:
                        this.Code = (int)EnumInstrucoes_Itau.ProtestarAposNDiasCorridos;
                        this.Description = "After protest "; //N dias corridos do vencimento";
                        break;
                    case EnumInstrucoes_Itau.ProtestarAposNDiasUteis:
                        this.Code = (int)EnumInstrucoes_Itau.ProtestarAposNDiasUteis;
                        this.Description = "Protest after N days to maturity";
                        break;
                    case EnumInstrucoes_Itau.NaoReceberAposNDias:
                        this.Code = (int)EnumInstrucoes_Itau.NaoReceberAposNDias;
                        this.Description = "Do not receive N days after the due";
                        break;
                    case EnumInstrucoes_Itau.DevolverAposNDias:
                        this.Code = (int)EnumInstrucoes_Itau.DevolverAposNDias;
                        this.Description = "Return after N days of winning";
                        break;
                    case EnumInstrucoes_Itau.JurosdeMora:
                        this.Code = (int)EnumInstrucoes_Itau.JurosdeMora;
                        this.Description = "After winning charge $ "; // por dia de atraso
                        break;
                    case EnumInstrucoes_Itau.Discount_oppose_Day:
                        this.Code = (int)EnumInstrucoes_Itau.Discount_oppose_Day;
                        this.Description = "Grant discount of $ "; // por dia de antecipação
                        break;
                    default:
                        this.Code = 0;
                        this.Description = "( Select )";
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
