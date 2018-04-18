using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    #region Enumeration

    public enum EnumCarteiras_Santander
    {
        //101-Cobrança Simples Rápida COM Registro
        CobrancaSimplesComRegistro = 101,
        //102- Cobrança simples – SEM Registro
        CobrancaSimplesSemRegistro = 102,
        //201- Penhor Rápida com Registro
        PenhorRapida = 201

  //CC - Cobrança Caucionada
  //CD - Cobrança Descontada
  //CSR - Cobrança Simples Sem Registro
  //ECR - Cobrança Simples Com Registro
  //ECR2 - Cobrança Simples Com Registro - Emissão Banco
  //PENHOR - Penhor Rápida com Registro
  //PENHOR-Eletron - Penhor Eletrônica com Registro
    }
    #endregion Enumeration

    public class Carteira_Santander : AbstractPortfolio, IPortfolio
    {

        #region Construtores 

		public Carteira_Santander()
		{
			try
			{
                this.Bank = new Bank(33);
			}
			catch (Exception ex)
			{
                throw new Exception("Erro ao carregar objeto", ex);
			}
		}

        public Carteira_Santander(int portfolio)
        {
            try
            {
                this.carregar(portfolio);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

		#endregion 

        #region Metodos Privados

        private void carregar(int portfolio)
        {
            try
            {
                this.Bank = new Bank_Santander();

                switch ((EnumCarteiras_Santander)portfolio)
                {
                    case EnumCarteiras_Santander.CobrancaSimplesComRegistro:
                        this.PortfolioNumber = (int)EnumCarteiras_Santander.CobrancaSimplesComRegistro;
                        this.Code = "ECR";
                        this.Type = "";
                        this.Description = "With Registry Easy Recovery";
                        break;
                    case EnumCarteiras_Santander.CobrancaSimplesSemRegistro:
                        this.PortfolioNumber = (int)EnumCarteiras_Santander.CobrancaSimplesSemRegistro;
                        this.Code = "CSR";
                        this.Type = "";
                        this.Description = "Simple Recovery Without Registration";
                        break;
                    case EnumCarteiras_Santander.PenhorRapida:
                        this.PortfolioNumber = (int)EnumCarteiras_Santander.PenhorRapida;
                        this.Code = "CSR";
                        this.Type = "";
                        this.Description = "Pledge to Fast Registration";
                        break;
                    default:
                        this.PortfolioNumber = 0;
                        this.Code = " ";
                        this.Type = " ";
                        this.Description = "";
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

                obj = new Carteira_Itau((int)EnumCarteiras_Santander.CobrancaSimplesComRegistro);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Santander.CobrancaSimplesSemRegistro);
                alCarteiras.Add(obj);

                obj = new Carteira_Itau((int)EnumCarteiras_Santander.PenhorRapida);
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
