using System;
using System.Web.UI;
using Microsoft.VisualBasic;

[assembly: WebResource("BoletoNET.Images.399.jpg", "image/jpg")]
namespace Cms.BusinessLayer.BlBoleto
{
    internal class Bank_HSBC : AbstractBank, IBank
    {
        #region Variables
        private string dacBoleto;
        #endregion

        #region Construtores

        internal Bank_HSBC()
        {
            try
            {
                this.Code = 399;
                this.Digit= 9;
                this.Name = "HSBC";
            }
            catch (Exception ex)
            {
                throw new Exception("Error instantiating the object.", ex);
            }
        }
        #endregion

        #region Instance Methods

        /// <summary>
        /// Validations private bank Itau
        /// </summary>
        public override void ValidateBoleto(Boleto boleto)
        {
            OurNumberFormats(boleto);
            BarCodeFormats(boleto);
            FormatLineDigitavel(boleto);
        }

        # endregion

        # region Formatting methods of the fetlock
        /// <summary>
        /// Formats the barcode
        /// </summary>
        /// <example>
        /// BY SIZE OF CONTENTS
        /// -----------------------
        /// March 1, 2003 Code of HSBC in the clearinghouse, equal to 399.
        /// April 4, 2001 Type of Currency (9 Real currency or currency variable to 0).
        /// May 5 2001 Digit Autoconferência (DAC).
        /// 06 09 04 Fator de Vencimento.
        /// 19 October 10 marks the Document. If currency variable, the value should be equal to zero.
        /// 20 26 07 Code Assignor
        /// 27 39 13 Number Bank, equal to the code document, without check digits and type identifier.
        /// 40 43 04 Maturity Date in Julian format.
        /// 44 44 01 Product Code CNR equal to 2.
        /// </example>
        public override void BarCodeFormats(Boleto boleto)
        {
            boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}"
                , Code.ToString()
                , boleto.currency
                , FatorVencimento(boleto)
                , boleto.Billetvalue.ToString("f").Replace(",", "").Replace(".", "").PadLeft(10, '0')
                , boleto.Assginor.Code.ToString()
                , boleto.OurNumber.Substring(0, 13)
                , boleto.Expirationdate.DayOfYear.ToString("000") + boleto.Expirationdate.ToString("yy").Substring(1, 1)
                , "2");

            dacBoleto = DVDAC(boleto.Barcode.Code);

            boleto.Barcode.Code = Strings.Left(boleto.Barcode.Code, 4) + dacBoleto + Strings.Right(boleto.Barcode.Code, 39);
        }

        /// <summary>
        /// Format line digitavel
        /// </summary>
        public override void FormatLineDigitavel(Boleto boleto)
        {
            //AAABC.CCCCd CCDDD.DDDDDd DDDDD.EEEE2d d FFFFVVVVVVVVVV

            string part1, part2;
            string Group1, Group2, Group3, Group4, Group5;

            #region Field 1

            string cd = boleto.Assginor.Code.ToString().Length < 7 ? "0000000" : boleto.Assginor.Code.ToString();

            part1 = "399" + boleto.currency.ToString() + boleto.Assginor.Code.ToString().Substring(0, 1);

            //part1 = this.Code + boleto.currency.ToString() + boleto.Assginor.Code.ToString().Substring(0, 1);
            
            part2 = cd.Substring(1, 4);
            Group1 = string.Format("{0}.{1}{2} ", part1, part2, DVLinhaDigitavel(part1 + part2));

            #endregion

            #region Field 2

            part1 = cd.ToString().Substring(5, 2) + boleto.OurNumber.Substring(0, 3);
            part2 = boleto.OurNumber.Substring(3, 5);
            Group2 = string.Format("{0}.{1}{2} ", part1, part2, DVLinhaDigitavel(part1 + part2));

            #endregion

            #region Field 3

            part1 = boleto.OurNumber.Substring(8, 5);
            part2 = boleto.Expirationdate.DayOfYear.ToString("000") + boleto.Expirationdate.ToString("yy").Substring(1, 1) + "2";
            Group3 = string.Format("{0}.{1}{2} ", part1, part2, DVLinhaDigitavel(part1 + part2));

            #endregion

            #region Field 4

            Group4 = string.Format("{0} ", dacBoleto);

            #endregion

            #region Field 5

            part1 = FatorVencimento(boleto) + boleto.Billetvalue.ToString("f").Replace(",", "").Replace(".", "").PadLeft(10, '0');
            Group5 = string.Format("{0}", part1);

            #endregion

            boleto.Barcode.LineDigitavel = Group1 + Group2 + Group3 + Group4 + Group5;
        }

        /// <summary>
        /// Perform formatting NossoNumero according to the rules of the HSBC
        /// </summary>
        public override void OurNumberFormats(Boleto boleto)
        {
            string dv1, dv2;
            long soma;
            boleto.OurNumber = boleto.OurNumber.PadLeft(13, '0');

            dv1 = DVNossoNumero(boleto.OurNumber);

            soma = Convert.ToInt64(boleto.OurNumber + dv1 + "4") + boleto.Assginor.Code+ int.Parse(boleto.Expirationdate.ToString("ddMMyy"));

            dv2 = DVNossoNumero(soma.ToString());

            boleto.OurNumber += dv1 + "4" + dv2;
        }

        public override void DocumentNumberFormats(Boleto boleto)
        {

        }
      
        # endregion

        //# region Methods for file generation referral CNAB400

        //# region HEADER

        ///// <summary>
        ///// HEADER do arquivo CNAB
        ///// Gera o HEADER do arquivo remessa de acordo com o lay-out informado
        ///// </summary>
        ////public override string GerarHeaderRemessa(string numeroConvenio, Assginor assginor, Filetype filetype)
        ////{
        ////    throw new NotImplementedException("Function not implemented.");
        ////}

        //public string GerarHeaderRemessaCNAB240()
        //{
        //    throw new NotImplementedException("Function not implemented.");
        //}

        //public string GerarHeaderRemessaCNAB400(int numeroConvenio, Assginor assginor)
        //{
        //    throw new NotImplementedException("Function not implemented.");
        //}

        //# endregion

        //# region DETALHE

        ///// <summary>
        ///// DETALHE do arquivo CNAB
        ///// Gera o DETALHE do arquivo remessa de acordo com o lay-out informado
        ///// </summary>
        //public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, Filetype filetype)
        //{
        //    throw new NotImplementedException("Function not implemented.");
        //}

        //public string GerarDetalheRemessaCNAB240()
        //{
        //    throw new NotImplementedException("Function not implemented.");
        //}

        //public string GerarDetalheRemessaCNAB400(Boleto boleto, int numeroRegistro, Filetype filetype)
        //{
        //    throw new NotImplementedException("Function not implemented.");
        //}

        //# endregion DETALHE

        //# region TRAILER

        ///// <summary>
        ///// TRAILER do arquivo CNAB
        ///// Gera o TRAILER do arquivo remessa de acordo com o lay-out informado
        ///// </summary>
        //public override string GerarTrailerRemessa(int numeroRegistro, Filetype filetype)
        //{
        //    throw new NotImplementedException("Function not implemented.");
        //}

        //public string GerarTrailerRemessa240()
        //{
        //    throw new NotImplementedException("Function not implemented.");
        //}

        //public string GerarTrailerRemessa400(int numeroRegistro)
        //{
        //    throw new NotImplementedException("Function not implemented.");
        //}

        //# endregion

        //#endregion

        //#region Methods of processing the return file CNAB400


        //#endregion

        #region private methods
        /// <summary>
        /// DV DAC Mod11
        /// </summary>
        /// <param name="cVariavel"></param>
        /// <returns></returns>
        private string DVDAC(string cVariavel)
        {
            string lRetorno = "0";
            int nSoma = 0, nMult = 2, nIndice;

            for (nIndice = cVariavel.Length - 1; nIndice >= 0; nIndice--)
            {
                nSoma += (Convert.ToByte(cVariavel[nIndice]) - 48) * nMult;
                if (nMult == 9) nMult = 2;
                else nMult++;
            }

            nSoma = nSoma % 11;
            if (nSoma < 2 || nSoma > 9) lRetorno = "1";
            else lRetorno = (11 - nSoma).ToString();

            return lRetorno;
        }

        /// <summary>
        /// DV NossoNumero Mod11
        /// </summary>
        /// <param name="cVariavel"></param>
        /// <returns></returns>
        private string DVNossoNumero(string cVariavel)
        {
            string lRetorno = "0";
            int nSoma = 0, nMult = 9, nIndice;

            for (nIndice = cVariavel.Length - 1; nIndice >= 0; nIndice--)
            {
                nSoma += (Convert.ToByte(cVariavel[nIndice]) - 48) * nMult;
                if (nMult == 2) nMult = 9;
                else nMult--;
            }

            nSoma = nSoma % 11;
            if (nSoma > 9) lRetorno = "0";
            else lRetorno = nSoma.ToString();

            return lRetorno;
        }

        /// <summary>
        /// DV Linha Digitavel Mod10
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        private string DVLinhaDigitavel(string seq)
        {

            int Digito, Soma = 0, Peso = 2, res;

            for (int i = seq.Length; i > 0; i--)
            {
                res = (Convert.ToInt32(Strings.Mid(seq, i, 1)) * Peso);

                if (res > 9)
                    res = (res - 9);

                Soma += res;

                if (Peso == 2)
                    Peso = 1;
                else
                    Peso = Peso + 1;
            }

            Digito = ((10 - (Soma % 10)) % 10);

            return Digito.ToString();
        }

        #endregion
    }
}
