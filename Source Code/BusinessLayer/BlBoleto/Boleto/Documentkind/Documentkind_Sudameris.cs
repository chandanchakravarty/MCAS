using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    #region Enumeration

    public enum EnumDocumentkind_Sudameris
    {
        DuplicataMercantil = 1,
        NotaPromissoria = 2,
        NotaSeguro = 3,
        MensalidadeEscolar = 4,
        Recibo = 5,
        Contrato = 6,
        Cosseguros = 7,
        DuplicataServico = 8,
        LetraCambio = 9,
        NotaDebito = 13,
        DocumentoDivida = 15,
        EncargosCondominais = 16,
        Diversos = 99,
    }

    #endregion 

    public class Documentkind_Sudameris: Documentkind, IDocumentkind
    { 
        //This class needs implementaion when we need to display document kind in render  html
      
        #region Construtctors 
       
		public Documentkind_Sudameris()
		{
			try
			{
			}
			catch (Exception ex)
			{
                throw new Exception("Load Error object", ex);
			}
		}

        public Documentkind_Sudameris(int Code)
        {
            try
            {
                this.Load(Code);
            }
            catch (Exception ex)
            {
                throw new Exception("Load Error object", ex);
            }
        }

		#endregion 

        #region Metodos Privates

        private void Load(int idCode)
        {
            try
            {
                //this.Bank = new Bank_Sudameris();

                switch ((EnumDocumentkind_Sudameris)idCode)
                {
                    case EnumDocumentkind_Sudameris.DuplicataMercantil:
                        this.Code = (int)EnumDocumentkind_Sudameris.DuplicataMercantil;
                        this.Especie = "Duplicata mercantil";
                        this.Sigla = "DM";
                        break;
                    case EnumDocumentkind_Sudameris.NotaPromissoria:
                        this.Code = (int)EnumDocumentkind_Sudameris.NotaPromissoria;
                        this.Especie = "Nota promissória";
                        this.Sigla = "NP";
                        break;
                    case EnumDocumentkind_Sudameris.NotaSeguro:
                        this.Code = (int)EnumDocumentkind_Sudameris.NotaSeguro;
                        this.Especie = "Nota de seguro";
                        this.Sigla = "NS";
                        break;
                    case EnumDocumentkind_Sudameris.MensalidadeEscolar:
                        this.Code = (int)EnumDocumentkind_Sudameris.MensalidadeEscolar;
                        this.Especie = "Mensalidade escolar";
                        this.Sigla = "ME";
                        break;
                    case EnumDocumentkind_Sudameris.Recibo:
                        this.Code = (int)EnumDocumentkind_Sudameris.Recibo;
                        this.Especie = "Recibo";
                        this.Sigla = "R";
                        break;
                    case EnumDocumentkind_Sudameris.Contrato:
                        this.Code = (int)EnumDocumentkind_Sudameris.Contrato;
                        this.Especie = "Contrato";
                        this.Sigla = "C";
                        break;
                    case EnumDocumentkind_Sudameris.Cosseguros:
                        this.Code = (int)EnumDocumentkind_Sudameris.Cosseguros;
                        this.Especie = "Cosseguros";
                        this.Sigla = "CS";
                        break;
                    case EnumDocumentkind_Sudameris.DuplicataServico:
                        this.Code = (int)EnumDocumentkind_Sudameris.DuplicataServico;
                        this.Especie = "Duplicata de serviço";
                        this.Sigla = "DS";
                        break;
                    case EnumDocumentkind_Sudameris.LetraCambio:
                        this.Code = (int)EnumDocumentkind_Sudameris.LetraCambio;
                        this.Especie = "Letra de câmbio";
                        this.Sigla = "LC";
                        break;
                    case EnumDocumentkind_Sudameris.NotaDebito:
                        this.Code = (int)EnumDocumentkind_Sudameris.NotaDebito;
                        this.Especie = "Nota de débito";
                        this.Sigla = "ND";
                        break;
                    case EnumDocumentkind_Sudameris.DocumentoDivida:
                        this.Code = (int)EnumDocumentkind_Sudameris.DocumentoDivida;
                        this.Especie = "Documento de dívida";
                        this.Sigla = "DD";
                        break;
                    case EnumDocumentkind_Sudameris.EncargosCondominais:
                        this.Code = (int)EnumDocumentkind_Sudameris.EncargosCondominais;
                        this.Especie = "Encargos condominais";
                        this.Sigla = "EC";
                        break;
                    case EnumDocumentkind_Sudameris.Diversos:
                        this.Code = (int)EnumDocumentkind_Sudameris.Diversos;
                        this.Especie = "Diversos";
                        this.Sigla = "D";
                        break;
                    default:
                        this.Code = 0;
                        this.Especie = "( Selecione )";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Load Error object", ex);
            }
        }

        public static DocumentKinds AllDownloads()
        {
            DocumentKinds especiesDocumento = new DocumentKinds();

            foreach (EnumDocumentkind_Sudameris item in Enum.GetValues(typeof(EnumDocumentkind_Sudameris)))
                especiesDocumento.Add(new Documentkind_Sudameris((int)item));

            return especiesDocumento;
        }

        #endregion
    }
}
