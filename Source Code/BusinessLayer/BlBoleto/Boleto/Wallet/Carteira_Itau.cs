using System;
using System.Collections;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    #region Enumerado

    public enum EnumCarteiras_Itau
    {

        EscritualEletronicaSimples = 112,
        EscritualEletronicaSimplesNossoNumeroLivre = 115,
        EscritualEletronicaCarne = 104,
        EscritualEletronicaDolar = 147,
        EscritualEletronicaCobrancaInteligente = 188,
        DiretaEletronicaEmissaoIntegralCarne = 108,
        DiretaEletronicaSemEmissaoSimples = 109,
        DiretaEletronicaSemEmissaoDolar = 150,
        DiretaEletronicaEmissaoParcialSimples = 121,
        DiretaEletronicaEmissaoInegralSimples = 180,
        SemRegistroSemEmissaoComProtestoEletronico = 175,
        SemRegistroSemEmissao15Digitos = 198,
        SemRegistroSemEmissao15DigitosIOF4 = 142,
        SemRegistroSemEmissao15DigitosIOF7 = 143,
        SemRegistroEmissaoParcialComProtestoBordero = 174,
        SemRegistroEmissaoParcialComProtestoEletronico = 177,
        SemRegistroEmissaoParcialSegurosIOF2 = 129,
        SemRegistroEmissaoParcialSegurosIOF4 = 139,
        SemRegistroEmissaoParcialSegurosIOF7 = 169,
        SemRegistroEmissaoIntegral = 172,
        SemRegistroEmissaoIntegralCarne = 102,
        SemRegistroEmissaoIntegral15PosicoesCarne = 107,
        SemRegistroEmissaoEntrega = 173,
        SemRegistroEmissaoEntregaCarne = 103,
        SemRegistroEmissaoEntrega15Posicoes = 196,

    }

    #endregion 

    public class Carteira_Itau : AbstractPortfolio, IPortfolio
    {

        #region Construtores 

		public Carteira_Itau()
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

        public Carteira_Itau(int carteira)
        {
            try
            {
                this.carregar(carteira);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading object", ex);
            }
        }

		#endregion 

        #region Metodos privates

        private void carregar(int carteira)
        {
            try
            {
                this.Bank = new Bank_Itau();

                switch ((EnumCarteiras_Itau)carteira)
                {
                    case EnumCarteiras_Itau.EscritualEletronicaSimples:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.EscritualEletronicaSimples;
                        this.Code = "I";
                        this.Type = "E";
                        this.Description = "Simple electronic book";
                        break;
                    case EnumCarteiras_Itau.EscritualEletronicaSimplesNossoNumeroLivre:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.EscritualEletronicaSimplesNossoNumeroLivre;
                        this.Code = "I";
                        this.Type = "E";
                        this.Description = "Simple electronic book - Track our free number";
                        break;
                    case EnumCarteiras_Itau.EscritualEletronicaCarne:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.EscritualEletronicaCarne;
                        this.Code = "I";
                        this.Type = "E";
                        this.Description = "Electronic book - Tickets";
                        break;
                    case EnumCarteiras_Itau.EscritualEletronicaDolar:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.EscritualEletronicaDolar;
                        this.Code = "E";
                        this.Type = "E";
                        this.Description = "Electronic book - Dollar";
                        break;
                    case EnumCarteiras_Itau.EscritualEletronicaCobrancaInteligente:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.EscritualEletronicaCobrancaInteligente;
                        this.Code = "I";
                        this.Type = "E";
                        this.Description = "Scriptural electronics - Intelligent Collection";
                        break;
                    case EnumCarteiras_Itau.DiretaEletronicaEmissaoIntegralCarne:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.DiretaEletronicaEmissaoIntegralCarne;
                        this.Code = "I";
                        this.Type = "D";
                        this.Description = "Direct electron emission integral - Tickets";
                        break;
                    case EnumCarteiras_Itau.DiretaEletronicaSemEmissaoSimples:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.DiretaEletronicaSemEmissaoSimples;
                        this.Code = "I";
                        this.Type = "D";
                        this.Description = "Without direct electron emission - Simple";
                        break;
                    case EnumCarteiras_Itau.DiretaEletronicaSemEmissaoDolar:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.DiretaEletronicaSemEmissaoDolar;
                        this.Code = "U";
                        this.Type = "D";
                        this.Description = "Without direct electron emission - Dollar";
                        break;
                    case EnumCarteiras_Itau.DiretaEletronicaEmissaoParcialSimples:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.DiretaEletronicaEmissaoParcialSimples;
                        this.Code = "I";
                        this.Type = "D";
                        this.Description = "Direct electron emission part - Simple";
                        break;
                    case EnumCarteiras_Itau.DiretaEletronicaEmissaoInegralSimples:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.DiretaEletronicaEmissaoInegralSimples;
                        this.Code = "I";
                        this.Type = "D";
                        this.Description = "Direct electron emission integral - Simple";
                        break;
                    case EnumCarteiras_Itau.SemRegistroSemEmissaoComProtestoEletronico:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroSemEmissaoComProtestoEletronico;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "No registration, no issue and with protest e";
                        break;
                    case EnumCarteiras_Itau.SemRegistroSemEmissao15Digitos:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroSemEmissao15Digitos;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "Without registration and without issue - 15 digits";
                        break;
                    case EnumCarteiras_Itau.SemRegistroSemEmissao15DigitosIOF4:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroSemEmissao15DigitosIOF4;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "Without registration and without issue - 15 digits IOF 4%";
                        break;
                    case EnumCarteiras_Itau.SemRegistroSemEmissao15DigitosIOF7:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroSemEmissao15DigitosIOF7;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "Without registration and without issue - 15 digits IOF 7%";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoParcialComProtestoBordero:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoParcialComProtestoBordero;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "No registration, issuing partial to protest borderô";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoParcialComProtestoEletronico:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoParcialComProtestoEletronico;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "Without registration, issue with protest e-partial";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoParcialSegurosIOF2:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoParcialSegurosIOF2;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "No registration, issuing partial insurance with IOF 2%";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoParcialSegurosIOF4:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoParcialSegurosIOF4;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "No registration, issuing partial insurance with IOF 4%";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoParcialSegurosIOF7:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoParcialSegurosIOF7;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "No registration, issuing partial insurance with IOF 7%";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoIntegral:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoIntegral;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "No registration, issuing full";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoIntegralCarne:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoIntegralCarne;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "Without registration, emission integral - Tickets";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoIntegral15PosicoesCarne:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoIntegral15PosicoesCarne;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "No registration, issuing full - 15 poições - Tickets";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoEntrega:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoEntrega;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "Without registration, with the issuance and delivery";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoEntregaCarne:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoEntregaCarne;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "Without registration, with the issuance and delivery - Tickets";
                        break;
                    case EnumCarteiras_Itau.SemRegistroEmissaoEntrega15Posicoes:
                        this.PortfolioNumber = (int)EnumCarteiras_Itau.SemRegistroEmissaoEntrega15Posicoes;
                        this.Code = "I";
                        this.Type = "S";
                        this.Description = "Without registration, with the issuance and delivery - 15 positions";
                        break;
                    default:
                        this.PortfolioNumber = 0;
                        this.Code = " ";
                        this.Type = " ";
                        this.Description = "( Select )";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading object", ex);
            }
        }

        public static Portfolios CarregaTodas()
        {
            try
            {
                Portfolios alCarteiras = new Portfolios();

                Carteira_Itau obj;

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.EscritualEletronicaSimples);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.EscritualEletronicaSimplesNossoNumeroLivre);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.EscritualEletronicaCarne);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.EscritualEletronicaDolar);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.EscritualEletronicaCobrancaInteligente);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.DiretaEletronicaEmissaoIntegralCarne);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.DiretaEletronicaSemEmissaoSimples);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.DiretaEletronicaSemEmissaoDolar);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.DiretaEletronicaEmissaoParcialSimples);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.DiretaEletronicaEmissaoInegralSimples);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroSemEmissaoComProtestoEletronico);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroSemEmissao15Digitos);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroSemEmissao15DigitosIOF4);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroSemEmissao15DigitosIOF7);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoParcialComProtestoBordero);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoParcialComProtestoEletronico);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoParcialSegurosIOF2);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoParcialSegurosIOF4);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoParcialSegurosIOF7);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoIntegral);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoIntegralCarne);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoIntegral15PosicoesCarne);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoEntrega);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoEntregaCarne);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Itau.SemRegistroEmissaoEntrega15Posicoes);
                alCarteiras.Add(obj);

                return alCarteiras;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar objetos", ex);
            }
        }

        #endregion

    }
}
