using System;
using System.Collections;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    #region Enumeration

    public enum EnumInstrucoes_BankBrasil
    {
        Protestar = 9,                      // Sends notice to the drawee vencto after N days, and sends to the office after 5 days
        NaoProtestar = 10,                  // Inhibits protest, when there is continuous education in the current account
        ImportanciaporDiaDesconto = 30,
        ProtestoFinsFalimentares = 42,
        ProtestarAposNDiasCorridos = 81,
        ProtestarAposNDiasUteis = 82,
        NaoReceberAposNDias = 91,
        DevolverAposNDias = 92,
        JurosdeMora = 998,
        DescontoporDia = 999,
    }

    #endregion 

    public class Instruction_BankBrazil: AbstractInstructions, IInstructions
    {

        #region Construtores 

		public Instruction_BankBrazil()
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

        public Instruction_BankBrazil(int Code)
        {
            this.Load(Code, 0);
        }

        public Instruction_BankBrazil(int Code, int nrDias)
        {
            this.Load(Code, nrDias);
        }

		#endregion 

        #region Metodos Privados

        private void Load(int idInstrucao, int nrDias)
        {
            try
            {
                this.Bank = new Bank_Brazil();
                this.Validate();

                switch ((EnumInstrucoes_BankBrasil)idInstrucao)
                {
                    case EnumInstrucoes_BankBrasil.Protestar:
                        this.Code = (int)EnumInstrucoes_BankBrasil.Protestar;
                        this.Description = "After protest " + nrDias + " working days.";
                        break;
                    case EnumInstrucoes_BankBrasil.NaoProtestar:
                        this.Code = (int)EnumInstrucoes_BankBrasil.NaoProtestar;
                        this.Description = "No protest";
                        break;
                    case EnumInstrucoes_BankBrasil.ImportanciaporDiaDesconto:
                        this.Code = (int)EnumInstrucoes_BankBrasil.ImportanciaporDiaDesconto;
                        this.Description = "Importance per day discount";
                        break;
                    case EnumInstrucoes_BankBrasil.ProtestoFinsFalimentares:
                        this.Code = (int)EnumInstrucoes_BankBrasil.ProtestoFinsFalimentares;
                        this.Description = "Protest for bankruptcy";
                        break;
                    case EnumInstrucoes_BankBrasil.ProtestarAposNDiasCorridos:
                        this.Code = (int)EnumInstrucoes_BankBrasil.ProtestarAposNDiasCorridos;
                        this.Description = "After protest" + nrDias + " calendar days of winning";
                        break;
                    case EnumInstrucoes_BankBrasil.ProtestarAposNDiasUteis:
                        this.Code = (int)EnumInstrucoes_BankBrasil.ProtestarAposNDiasUteis;
                        this.Description = "After protest" + nrDias + " working days of winning";
                        break;
                    case EnumInstrucoes_BankBrasil.NaoReceberAposNDias:
                        this.Code = (int)EnumInstrucoes_BankBrasil.NaoReceberAposNDias;
                        this.Description = "Not receive after " + nrDias + " days maturity";
                        break;
                    case EnumInstrucoes_BankBrasil.DevolverAposNDias:
                        this.Code = (int)EnumInstrucoes_BankBrasil.DevolverAposNDias;
                        this.Description = "Return after " + nrDias + " days maturity";
                        break;
                    case EnumInstrucoes_BankBrasil.JurosdeMora:
                        this.Code = (int)EnumInstrucoes_BankBrasil.JurosdeMora;
                        this.Description = "After winning charge $ "; // por dia de atraso
                        break;
                    case EnumInstrucoes_BankBrasil.DescontoporDia:
                        this.Code = (int)EnumInstrucoes_BankBrasil.DescontoporDia;
                        this.Description = "Grant discount of $ "; // por dia de antecipação
                        break;
                    default:
                        this.Code = 0;
                        this.Description = "( Selection )";
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
