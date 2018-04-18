using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    #region Enumeration

    public enum EnumDocumentKind_Itau
    {
        DuplicataMercantil = 1,
        PromissoryNote = 2,
        NoteInsurance = 3,
        Tuition = 4,
        Receipt = 5,
        Contract = 6,
        Coinsurance = 7,
        DuplicateService = 8,
        CambioLyrics = 9,
        DebitNote = 13,
        DocumentDivide = 15,
        ChargesCondominas = 16,
        Several = 99,
    }

    #endregion 

    public class DocumentKind_Itau : AbstractDocumentkind,IDocumentkind
    {
        //This class needs implementaion when we need to display document kind in render  html
        #region Constructors

		public DocumentKind_Itau()
		{
			try
			{
			}
			catch (Exception ex)
			{
                throw new Exception("Error loading object", ex);
			}
		}

        public DocumentKind_Itau(int Code)
        {
            try
            {
                this.carregar(Code);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading object", ex);
            }
        }

		#endregion 

        #region private methods

        private void carregar(int idCodigo)
        {
            try
            {
                //this.Bank = new Banco_Itau();

                switch ((EnumDocumentKind_Itau)idCodigo)
                {
                    case  EnumDocumentKind_Itau.DuplicataMercantil:
                        this.Code = (int)EnumDocumentKind_Itau.DuplicataMercantil;
                        this.Especie = "Duplicate mercantile";
                        this.Sigla = "DM";
                        break;
                    case EnumDocumentKind_Itau.PromissoryNote:
                        this.Code = (int)EnumDocumentKind_Itau.PromissoryNote;
                        this.Especie = "Promissory note";
                        this.Sigla = "NP";
                        break;
                    case EnumDocumentKind_Itau.NoteInsurance:
                        this.Code = (int)EnumDocumentKind_Itau.NoteInsurance;
                        this.Especie = "Note insurance";
                        this.Sigla = "NS";
                        break;
                    case EnumDocumentKind_Itau.Tuition:
                        this.Code = (int)EnumDocumentKind_Itau.Tuition;
                        this.Especie = "Tuition";
                        this.Sigla = "ME";
                        break;
                    case EnumDocumentKind_Itau.Receipt:
                        this.Code = (int)EnumDocumentKind_Itau.Receipt;
                        this.Especie = "Receipt";
                        this.Sigla = "R"; 
                        break;
                    case EnumDocumentKind_Itau.Contract:
                        this.Code = (int)EnumDocumentKind_Itau.Contract;
                        this.Sigla = "C";
                        this.Especie = "Contract";
                        break;
                    case EnumDocumentKind_Itau.Coinsurance:
                        this.Code = (int)EnumDocumentKind_Itau.Coinsurance;
                        this.Sigla = "CS";
                        this.Especie = "Coinsurance";
                        break;
                    case EnumDocumentKind_Itau.DuplicateService:
                        this.Code = (int)EnumDocumentKind_Itau.DuplicateService;
                        this.Sigla = "DS";
                        this.Especie = "Duplicate Service";
                        break;
                    case EnumDocumentKind_Itau.CambioLyrics:
                        this.Code = (int)EnumDocumentKind_Itau.CambioLyrics;
                        this.Sigla = "LC";
                        this.Especie = "Bill of exchange";
                        break;
                    case EnumDocumentKind_Itau.DebitNote:
                        this.Code = (int)EnumDocumentKind_Itau.DebitNote;
                        this.Sigla = "ND";
                        this.Especie = "Debit Note";
                        break;
                    case EnumDocumentKind_Itau.DocumentDivide:
                        this.Code = (int)EnumDocumentKind_Itau.DocumentDivide;
                        this.Sigla = "DD";
                        this.Especie = "Document debt";
                        break;
                    case EnumDocumentKind_Itau.ChargesCondominas:
                        this.Code = (int)EnumDocumentKind_Itau.ChargesCondominas;
                        this.Sigla = "EC";
                        this.Especie = "Charges Condominas";
                        break;
                    case EnumDocumentKind_Itau.Several:
                        this.Code = (int)EnumDocumentKind_Itau.Several;
                        this.Especie = "Several";
                        break;
                    default:
                        this.Code = 0;
                        this.Especie = "( Select )";
                        this.Sigla = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading object", ex);
            }
        }

        public static DocumentKinds AllDownloads()
        {
            DocumentKinds documentKinds = new DocumentKinds();

            foreach (EnumDocumentKind_Itau item in Enum.GetValues(typeof(EnumDocumentKind_Itau)))
                documentKinds.Add(new DocumentKind_Itau((int)item));

            return documentKinds;
        }

        #endregion







    }


}
