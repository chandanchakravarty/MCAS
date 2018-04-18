using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    #region Enumeration

    public enum EnumDocumentkind_Santander
    {
        //This class needs implementaion when we need to display document kind in render  html

        DuplicataMercantil = 2,
        DuplicataServico = 4,
        LetraCambio353 = 7,
        LetraCambio008 = 30,
        NotaPromissoria = 12,
        NotaPromissoriaRural = 13,
        Recibo = 17,
        ApoliceSeguro = 20,
        Cheque = 97,
        NotaPromissoariaDireta = 98
         //02   DM - DUPLICATE MERCANTIL               
         //04   DS - DUPLICATE OF SERVICEO                
         //07	LC - LETTER OF EXCHANGE (FOR Bank 353)
         //30	LC - LETTER OF EXCHANGE (FOR Bank 008)
         //12   NP - Promissory Note                   
         //13	NR - Promissory Note RURAL 
         //17   RC - RECEIPT                              
         //20   AP – Insurance Policy               
         //97	CH – CHEQUE
         //98	ND - Promissory Note DIRECT
    }

    #endregion

    public class Documentkind_Santander :  AbstractDocumentkind,IDocumentkind
    {
        #region Construtores

        public Documentkind_Santander()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception("Load Error object", ex);
            }
        }

        public Documentkind_Santander(int Code)
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

        #region Metodos Privados

        private void Load(int idCode)
        {
            try
            {
                this.Bank = new Bank_Santander();

                switch ((EnumDocumentkind_Santander)idCode)
                {
                    case EnumDocumentkind_Santander.DuplicataMercantil:
                        this.Code = (int)EnumDocumentkind_Sudameris.DuplicataMercantil;
                        this.Especie = "Duplicate Mercantil";
                        this.Sigla = "DM";
                        break;
                    case EnumDocumentkind_Santander.DuplicataServico:
                        this.Code = (int)EnumDocumentkind_Sudameris.DuplicataServico;
                        this.Especie = "Duplicate Service";
                        this.Sigla = "DS";
                        break;
                    case EnumDocumentkind_Santander.Recibo:
                        this.Code = (int)EnumDocumentkind_Sudameris.Recibo;
                        this.Especie = "Receipt";
                        this.Sigla = "R";
                        break;
                    case EnumDocumentkind_Santander.LetraCambio353:
                        this.Code = (int)EnumDocumentkind_Santander.LetraCambio353;
                        this.Especie = "Bank Draft (For Bank 353)";
                        this.Sigla = "LS";
                        break;
                    case EnumDocumentkind_Santander.LetraCambio008:
                        this.Code = (int)EnumDocumentkind_Santander.LetraCambio008;
                        this.Especie = "Bank Draft (For Bank 008)";
                        this.Sigla = "LS";
                        break;
                    case EnumDocumentkind_Santander.ApoliceSeguro:
                        this.Code = (int)EnumDocumentkind_Santander.ApoliceSeguro;
                        this.Especie = "Insurance Policy";
                        this.Sigla = "AP";
                        break;
                    case EnumDocumentkind_Santander.NotaPromissoariaDireta:
                        this.Code = (int)EnumDocumentkind_Santander.NotaPromissoariaDireta;
                        this.Especie = "Promissory Note Direct";
                        this.Sigla = "ND";
                        break;
                    case EnumDocumentkind_Santander.NotaPromissoria:
                        this.Code = (int)EnumDocumentkind_Santander.NotaPromissoria;
                        this.Especie = "Promissory note";
                        this.Sigla = "NP";
                        break;
                    case EnumDocumentkind_Santander.NotaPromissoriaRural:
                        this.Code = (int)EnumDocumentkind_Santander.NotaPromissoriaRural;
                        this.Especie = "Rural Promissory Note";
                        this.Sigla = "NR";
                        break;
                    case EnumDocumentkind_Santander.Cheque:
                        this.Code = (int)EnumDocumentkind_Santander.Cheque;
                        this.Especie = "Cheque";
                        this.Sigla = "CH";
                        break;
                    default:
                        this.Code = 0;
                        this.Especie = "";
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

            foreach (EnumDocumentkind_Santander item in Enum.GetValues(typeof(EnumDocumentkind_Santander)))
                especiesDocumento.Add(new Documentkind_Santander((int)item));

            return especiesDocumento;
        }

        #endregion
    }
}
