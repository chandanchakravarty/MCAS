using System;
using System.Web.UI;
using Microsoft.VisualBasic;

[assembly: WebResource("BoletoNET.Imagens.347.jpg", "image/jpg")]

namespace Cms.BusinessLayer.BlBoleto
{
    /// <author>  
    /// Eduardo Frare
    /// Stiven 
    /// Diogo(Allmatech)
    /// Miamoto(Allmatech)
    /// Rodrigo(Allmatech)
    /// </author>    

    ///<summary>
    /// Classe referente ao Banco Bank_Sudameris
    ///</summary>
    internal class Bank_Sudameris : AbstractBank, IBank
    {
        private int _dacDigitaoCobranca = 0;
        private int _dacBoleto = 0;

        internal Bank_Sudameris()
        {
            this.Code = 347;
            this.Digit = 6;
            this.Name = "Bank_Sudameris";
        }

        #region IBanco Members

        public override void ValidateBoleto(Boleto boleto)
        {
            //Verifies that our number is valid
            if (Utils.ToInt32(boleto.OurNumber) == 0)
                throw new NotImplementedException("Our number invalid");

            //The account number is 7 digits
            if (boleto.Assginor.bankaccount.Account.Length != 7)
                throw new Exception("The account number of the transferor are 7 numbers.");

             // Check if the size for the NossoNumero
             // 7 billing logged
            // 13 for collection without registration

            boleto.OurNumber= Utils.FormatCode(boleto.OurNumber, 13);

            // Calculate the Digita recovery DAC (Our Number / Agency / Current Account)
            _dacDigitaoCobranca = Mod10(boleto.OurNumber + boleto.Assginor.bankaccount.Account + boleto.Assginor.bankaccount.Account);

            // Assign the database name to the place of payment
            boleto.LocalPayment += Name;

           //Checks whether date processing is valid
            if (boleto.Processingdate.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.Processingdate = DateTime.Now;

            //Checks if the date the document is valid
            if (boleto.Documentdate.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.Documentdate = DateTime.Now;

            BarCodeFormats(boleto);
            FormatLineDigitavel(boleto);
            DocumentNumberFormats(boleto);
        }

        public override void FormatLineDigitavel(Boleto boleto)
        {
            #region Definições
            /* AAABC.CCCDDX.DDDDD.DEFFFY.FGGGG.GGHHHZ.K.UUUUVVVVVVVVVV
              * ------------------------------------------------------
              * Campo 1
              * AAABC.CCCDX
              * AAA - Código do Banco
              * B   - Moeda
              * CCCC - Agência
              * D  - 1 primeiro número da Conta Corrente
              * X   - DAC Campo 1 (AAABC.CCDD) Mod10
              * 
              * Campo 2
              * DDDDD.DEFFFY
              * DDDDD.D - Restante do Conta Corrente
              * E       - DAC (Nosso Número/Agência/Conta)
              * FFF     - Três primeiros do Nosso Número
              * Y       - DAC Campo 2 (DDDDD.DEFFF) Mod10
              * 
              * Campo 3
              * FFFFF.FFFFZ
              * FFFFFFFFF- Restante Nosso Número
              * Z       - DAC Campo 3
              * 
              * Campo 4
              * K       - DAC Código de Barras
              * 
              * Campo 5
              * UUUUVVVVVVVVVV
              * UUUU       - Fator Vencimento
              * VVVVVVVVVV - Valor do Título 
              */
            #endregion Definições

            string numeroDocumento = Utils.FormatCode(boleto.Documentnumber.ToString(), 7);
            string codigoCedente = Utils.FormatCode(boleto.Assginor.Code.ToString(), 5);

            string AAA = Utils.FormatCode(Code.ToString(), 3);
            string B = boleto.currency.ToString();
            string CCCC = boleto.Assginor.bankaccount.Agency;
            string D = boleto.Assginor.bankaccount.Account.Substring(0, 1);
            string X = Mod10(AAA + B + CCCC + D).ToString();

            string DDDDDD = boleto.Assginor.bankaccount.Account.Substring(1, 6);

            string K = string.Format(" {0} ", _dacDigitaoCobranca);

            string UUUU = FatorVencimento(boleto).ToString();
            string VVVVVVVVVV = boleto.Billetvalue.ToString("f").Replace(",", "").Replace(".", "");

            string C1 = string.Empty;
            string C2 = string.Empty;
            string C3 = string.Empty;
            string C4 = string.Empty;

            #region AAABC.CCDDX

            C1 = string.Format("{0}{1}{2}.", AAA, B, CCCC.Substring(0, 1));
            C1 += string.Format("{0}{1}{2} ", CCCC.Substring(1, 3), D, X);

            #endregion AAABC.CCDDX

            #region UUUUVVVVVVVVVV

            VVVVVVVVVV = Utils.FormatCode(VVVVVVVVVV, 10);
            C4 = UUUU + VVVVVVVVVV;

            #endregion UUUUVVVVVVVVVV

            #region DDDDD.DEFFFY

            string E = _dacDigitaoCobranca.ToString();
            string FFF = boleto.OurNumber.Substring(0, 3);
            string Y = Mod10(DDDDDD + E + FFF).ToString();

            C2 = string.Format("{0}.", DDDDDD.Substring(0, 5));
            C2 += string.Format("{0}{1}{2}{3} ", DDDDDD.Substring(5, 1), E, FFF, Y);

            #endregion DDDDD.DEFFFY

            #region FFFFF.FFFFFZ

            string FFFFFFFFF = boleto.OurNumber.Substring(3, 10);
            string Z = Mod10(FFFFFFFFF).ToString();

            C3 = string.Format("{0}.{1}{2}", FFFFFFFFF.Substring(0, 5), FFFFFFFFF.Substring(5, 5), Z);

            #endregion FGGGG.GGHHHZ

            boleto.Barcode.LineDigitavel = C1 + C2 + C3 + K + C4;
        }

        public void OurNumberFormats()
        {
        }

        public override void DocumentNumberFormats(Boleto boleto)
        {
            boleto.Documentnumber = Utils.FormatCode(boleto.OurNumber, 13);
        }

        public override void BarCodeFormats(Boleto boleto)
        {
            // Código de Barras
            //Código do Banco/Moeda/ DAC /Fator Vencimento/Valor/Agência/Conta + Digitão/Nosso Número

            string valorBoleto = boleto.Billetvalue.ToString("f").Replace(",", "").Replace(".", "");
            valorBoleto = Utils.FormatCode(valorBoleto, 10);

            boleto.Barcode.Code =
                    string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", Code, boleto.currency,
                                  FatorVencimento(boleto), valorBoleto, boleto.Assginor.bankaccount.Agency,
                                  boleto.Assginor.bankaccount.Account, _dacDigitaoCobranca, boleto.OurNumber);


            _dacBoleto = Mod11(boleto.Barcode.Code, 9, 0);

            boleto.Barcode.Code = Strings.Left(boleto.Barcode.Code, 4) + _dacBoleto + Strings.Right(boleto.Barcode.Code, 39);
        }
        #endregion IBanco Members
    }
}
