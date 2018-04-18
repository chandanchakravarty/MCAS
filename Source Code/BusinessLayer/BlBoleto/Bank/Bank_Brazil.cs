
using System;
using System.Web.UI;
using Microsoft.VisualBasic;

[assembly: WebResource("BoletoNET.Imagens.001.jpg", "image/jpg")]
namespace Cms.BusinessLayer.BlBoleto
{
    /// <summary>
    /// Classe referente ao Banco do Brasil
    /// </summary>
    internal class Bank_Brazil : AbstractBank, IBank
    {

        #region Varibles

        private string _dacNossoNumero = string.Empty;
        private int _dacBoleto = 0;

        #endregion

        #region Construtores

        internal Bank_Brazil()
        {
            try
            {
                this.Code = 1;
                this.Digit = 9;

                this.Name = "Pagável em qualquer banco até o vencimento.";//"Bank of Brazil";  //Commented by Pradeep Kushwaha on 25-Feb-2011 itrack 685
            }
            catch (Exception ex)
            {
                throw new Exception("Error instantiating the object.", ex);
            }
        }
        #endregion

        #region Instance Methods

        /// <summary>
        /// Validations Private Bank of Brazil
        /// </summary>
        public override void ValidateBoleto(Boleto boleto)
        {
            if (string.IsNullOrEmpty(boleto.wallet))
                throw new NotImplementedException("Portfolio as informed. Use the wallet 16, 17, 18 or 18-019.");

            //Verifica as carteiras implementadas
            if (!boleto.wallet.Equals("16") &
                !boleto.wallet.Equals("17") &
                !boleto.wallet.Equals("18") &
                !boleto.wallet.Equals("18-019"))
                throw new NotImplementedException("Portfolio not implemented. Use the wallet 16, 17, 18 or 18-019.");

            //Verifies that our number is valid
            if (Utils.ToString(boleto.OurNumber) == string.Empty)
                throw new NotImplementedException("Our number invalid");

            #region wallet 16
            //Portfolio 18 with our number 11 position
            if (boleto.wallet.Equals("16"))
            {
                if (!boleto.modaltypedae.Equals("21"))
                {
                    if (boleto.OurNumber.Length > 7)
                        throw new NotImplementedException(string.Format("For the portfolio (0), the maximum amount of 11 positions are to our number", boleto.wallet));

                    if (boleto.Assginor.Convenio.ToString().Length == 4)
                        boleto.OurNumber = string.Format("{0}{1}", boleto.Assginor.Convenio, Utils.FormatCode(boleto.OurNumber, 7));
                    else
                        boleto.OurNumber = Utils.FormatCode(boleto.OurNumber, 11);
                    #region C O M M E N T E D BY Pradeep Kushwaha on 01-12-2010
                    //if (boleto.OurNumber.Length > 11)
                    //    throw new NotImplementedException(string.Format("For the portfolio (0), the maximum amount of 11 positions are to our number", boleto.wallet));

                    //if (boleto.Assginor.Convenio.ToString().Length == 6)
                    //    boleto.OurNumber = string.Format("{0}{1}", boleto.Assginor.Convenio, Utils.FormatCode(boleto.OurNumber, 11));
                    //else
                    //    boleto.OurNumber = Utils.FormatCode(boleto.OurNumber, 11);
                    #endregion
                }
                else
                {
                    if (boleto.Assginor.Convenio.ToString().Length != 6)
                        throw new NotImplementedException(string.Format("For the portfolio (0) and type of mode 21, the number of covenant are 6 positions", boleto.wallet));

                    boleto.OurNumber = Utils.FormatCode(boleto.OurNumber, 17);
                }
            }
            #endregion wallet 16

            #region wallet 17
            //Carteira 17
            if (boleto.wallet.Equals("17"))
            {
                switch (boleto.Assginor.Convenio.ToString().Length)
                {
                    //The BB sends default seven positions, but you can request an agreement with six positions in 17 portfolio
                    case 6:
                        if (boleto.OurNumber.Length > 12)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 12 de posições para o nosso número", boleto.wallet));
                        boleto.OurNumber = Utils.FormatCode(boleto.OurNumber, 12);
                        break;
                    case 7:
                        if (boleto.OurNumber.Length > 17)
                            throw new NotImplementedException(string.Format("For the portfolio (0), the maximum amount of 10 positions are to our number", boleto.wallet));
                        boleto.OurNumber = string.Format("{0}{1}", boleto.Assginor.Convenio, Utils.FormatCode(boleto.OurNumber, 11));
                        break;
                    default:
                        throw new NotImplementedException(string.Format("For the portfolio (0), the number of the agreement must have six or seven positions ", boleto.wallet));
                }
            }
            #endregion wallet 17


            #region wallet 18
            //Portfolio 18 with our number 11 position
            if (boleto.wallet.Equals("18"))
                boleto.OurNumber = Utils.FormatCode(boleto.OurNumber, 11);
            #endregion Carteira 18

            #region wallet 18-019
            //Portfolio 18, range 019
            if (boleto.wallet.Equals("18-019"))
            {
                /*
                 * Agreement of 7 positions
                 * Our number with 17 positions
                 */
                if (boleto.Assginor.Convenio.ToString().Length == 7)
                {
                    if (boleto.OurNumber.Length > 10)
                        throw new NotImplementedException(string.Format("For the portfolio (0), the maximum amount of 10 positions are to our number", boleto.wallet));

                    boleto.OurNumber = string.Format("{0}{1}", boleto.Assginor.Convenio, Utils.FormatCode(boleto.OurNumber, 10));
                }
                /*
                 * Agreement of 6 positions
                 *Our number with 11 positions
                 */
                else if (boleto.Assginor.Convenio.ToString().Length == 6)
                {
                    //Modalidades de Cobrança Sem Registro – Carteira 16 e 18
                    //Nosso Número com 17 posições
                    if (!boleto.modaltypedae.Equals("21"))
                    {
                        if ((boleto.Assginor.Code.ToString().Length + boleto.OurNumber.Length) > 11)
                            throw new NotImplementedException(string.Format("For the portfolio (0), the maximum is 11 positions for our number. Where our number is formed by CCCCCCNNNNN-X: C -> number of the agreement provided by Bank, N -> sequentially assigned by the client and X -> digit checker “Our number-”.", boleto.wallet));

                        boleto.OurNumber = string.Format("{0}{1}", boleto.Assginor.Convenio, Utils.FormatCode(boleto.OurNumber, 5));
                    }
                    else
                    {
                        if (boleto.Assginor.Convenio.ToString().Length != 6)
                            throw new NotImplementedException(string.Format("For the portfolio (0) and type of mode 21, the number of covenant are 6 positions", boleto.wallet));

                        boleto.OurNumber = Utils.FormatCode(boleto.OurNumber, 17);
                    }
                }
                /*
                  * Accord of 4 positions
                  * Our number with 11 positions
                  */
                else if (boleto.Assginor.Convenio.ToString().Length == 4)
                {
                    if (boleto.OurNumber.Length > 7)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 7 de posições para o nosso número [{1}]", boleto.wallet, boleto.wallet));

                    boleto.OurNumber = string.Format("{0}{1}", boleto.Assginor.Convenio, Utils.FormatCode(boleto.OurNumber, 7));
                }
                else
                    boleto.OurNumber = Utils.FormatCode(boleto.OurNumber, 11);
            }
            #endregion wallet 18-019

            #region Agency and the Current Account

            //Check if the agency is correct
            if (boleto.Assginor.bankaccount.Agency.Length > 4)
                throw new NotImplementedException("A quantidade de dígitos da Agência " + boleto.Assginor.bankaccount.Agency + ", are 4 numbers.");
            else if (boleto.Assginor.bankaccount.Agency.Length < 4)
                boleto.Assginor.bankaccount.Agency = Utils.FormatCode(boleto.Assginor.bankaccount.Agency, 4);

            //Verificar se a Conta esta correta
            if (boleto.Assginor.bankaccount.Account.Length > 8)
                throw new NotImplementedException("A quantidade de dígitos da Conta " + boleto.Assginor.bankaccount.Account + ", are 8 numbers.");
            else if (boleto.Assginor.bankaccount.Account.Length < 8)
                boleto.Assginor.bankaccount.Account = Utils.FormatCode(boleto.Assginor.bankaccount.Account, 8);
            #endregion Agência e Conta Corrente

            //Assign the database name to the place of payment
            boleto.LocalPayment += Name + "";

            //Verifica se data do processamento é valida
            if (boleto.Processingdate.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.Processingdate = DateTime.Now;

            //Verifica se data do documento é valida
            if (boleto.Documentdate.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.Documentdate = DateTime.Now;

            boleto.Currencyamount = 0;

            BarCodeFormats(boleto);
            FormatLineDigitavel(boleto);
            OurNumberFormats(boleto);
        }

        # endregion

        private string ClearPortfolio(string Wallet)
        {
            return Wallet.Split('-')[0];
        }

        # region Formatting methods of the fetlock

        public override void BarCodeFormats(Boleto boleto)
        {
            string valorBoleto = boleto.Billetvalue.ToString("f").Replace(",", "").Replace(".", "");
            valorBoleto = Utils.FormatCode(valorBoleto, 10);

            #region wallet 16
            if (boleto.wallet.Equals("16"))
            {
                if (boleto.Assginor.Convenio.ToString().Length == 6)
                {
                    if (boleto.modaltypedae.Equals("21"))
                        boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                            Utils.FormatCode(Code.ToString(), 3),
                            boleto.currency,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.Assginor.Convenio,
                            boleto.OurNumber,
                            "21");
                }
                #region Comment On 01-10-2010
                //if (boleto.Assginor.Convenio.ToString().Length == 6)
                //{
                //    if (boleto.modaltypedae.Equals("21"))
                //        boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                //            Utils.FormatCode(Code.ToString(), 3),
                //            boleto.currency,
                //            FatorVencimento(boleto),
                //            valorBoleto,
                //            boleto.Assginor.Convenio,
                //            boleto.OurNumber,
                //            "21");
                //}
                #endregion
                else
                {
                    boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Code.ToString(), 3),
                        boleto.currency,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.OurNumber,
                        boleto.Assginor.bankaccount.Agency,
                        boleto.Assginor.bankaccount.Account,
                        boleto.wallet);
                }
            }
            #endregion Carteira 16

            #region Carteira 17
            if (boleto.wallet.Equals("17"))
            {
                if (boleto.Assginor.Convenio.ToString().Length == 7)
                {
                    boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Code.ToString(), 3),
                        boleto.currency,
                        FatorVencimento(boleto),
                        valorBoleto,
                        "000000",
                        boleto.OurNumber,
                        Utils.FormatCode(ClearPortfolio(boleto.wallet), 2));
                }
                if (boleto.Assginor.Convenio.ToString().Length == 6)
                {
                    boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Code.ToString(), 3),
                        boleto.currency,
                        FatorVencimento(boleto),
                        valorBoleto,
                        Strings.Mid(boleto.OurNumber, 1, 11),
                        boleto.Assginor.bankaccount.Agency,
                        boleto.Assginor.bankaccount.Account,
                        boleto.wallet);
                }
                else
                {
                    boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Code.ToString(), 3),
                        boleto.currency,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.OurNumber,
                        boleto.Assginor.bankaccount.Agency,
                        boleto.Assginor.bankaccount.Account,
                        boleto.wallet);
                }
            }
            #endregion wallet 17

            #region wallet 18

            if (boleto.wallet.Equals("18"))
            {
                boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                    Utils.FormatCode(Code.ToString(), 3),
                    boleto.currency,
                    FatorVencimento(boleto),
                    valorBoleto,
                    boleto.OurNumber,
                    boleto.Assginor.bankaccount.Agency,
                    boleto.Assginor.bankaccount.Account,
                    boleto.wallet);
            }
            #endregion wallet 18

            #region wallet 18-019

            if (boleto.wallet.Equals("18-019"))
            {
                if (boleto.Assginor.Convenio.ToString().Length == 7)
                {
                    #region Agreement Specification 7 positions
                    /*
                    Position     Size     Picture           Content
                    01 a 03         03      9(3)            Bank Code on Clearing = '001 '
                    04 a 04         01      9(1)            Currency Code = '9 '
                    05 a 05         01      9(1)            DV Bar Code (Annex 10)
                    06 a 09         04      9(04)           Maturity Factor (Annex 8)
                    10 a 19         10      9(08)           V (2) Value
                    20 a 25         06      9(6)            Zeros
                    26 a 42         17      9(17)           Our number-without the DV
                    26 a 32         9       (7)             Number of the agreement provided by Bank (CCCCCCC)
                    33 a 42         9       (10)            Completion of Our number-without DV (NNNNNNNNNN)
                    43 a 44         02      9(2)            Portfolio Type / Modality of Collection
                     */
                    #endregion Agreement Specification 7 positions

                    boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Code.ToString(), 3),
                        boleto.currency,
                        FatorVencimento(boleto),
                        valorBoleto,
                        "000000",
                        boleto.OurNumber,
                        Utils.FormatCode(ClearPortfolio(boleto.wallet), 2));
                }
                else if (boleto.Assginor.Convenio.ToString().Length == 6)
                {
                    if (boleto.modaltypedae.Equals("21"))
                        boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                            Utils.FormatCode(Code.ToString(), 3),
                            boleto.currency,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.Assginor.Convenio,
                            boleto.OurNumber,
                            "21");
                    else
                        boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                            Utils.FormatCode(Code.ToString(), 3),
                            boleto.currency,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.OurNumber,
                            boleto.Assginor.bankaccount.Agency,
                            boleto.Assginor.bankaccount.Account,
                            ClearPortfolio(boleto.wallet));
                }
                else if (boleto.Assginor.Convenio.ToString().Length == 4)
                {
                    boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Code.ToString(), 3),
                        boleto.currency,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.OurNumber,
                        boleto.Assginor.bankaccount.Agency,
                        boleto.Assginor.bankaccount.Account,
                        ClearPortfolio(boleto.wallet));
                }
            }
            #endregion wallet 18-019

            //_dacBoleto = Mod11(boleto.Barcode.Code, 9);//Commented on 06-12-2010 by Pradeep Kushwaha 
            _dacBoleto = Mod11OFBrizilBank(boleto.Barcode.Code, 9);//Added by Pradeep Kushwaha on 06-12-2010

            boleto.Barcode.Code= Strings.Left(boleto.Barcode.Code, 4) + _dacBoleto + Strings.Right(boleto.Barcode.Code, 39);
        }

        public override void FormatLineDigitavel(Boleto boleto)
        {
            string cmplivre = string.Empty;
            string campo1 = string.Empty;
            string campo2 = string.Empty;
            string campo3 = string.Empty;
            string campo4 = string.Empty;
            string campo5 = string.Empty;
            long icampo5 = 0;
            int digitoMod = 0;

            /*
            Fields 1 (AAABC.CCCCX):
            A = Code of Bank's Clearing House "001"
            B = Código da moeda "9" (*)
            C = Posição 20 a 24 do código de barras
            X = DV que amarra o campo 1 (Módulo 10, contido no Anexo 7)
             */

            cmplivre = Strings.Mid(boleto.Barcode.Code, 20, 25);

            campo1 = Strings.Left(boleto.Barcode.Code, 4) + Strings.Mid(cmplivre, 1, 5);
            digitoMod = Mod10(campo1);
            campo1 = campo1 + digitoMod.ToString();
            campo1 = Strings.Mid(campo1, 1, 5) + "." + Strings.Mid(campo1, 6, 5);
            /*
            Fields 2 (DDDDD.DDDDDY)
            D = Posição 25 a 34 do código de barras
            Y = DV que amarra o campo 2 (Módulo 10, contido no Anexo 7)
             */
            campo2 = Strings.Mid(cmplivre, 6, 10);
            digitoMod = Mod10(campo2);
            campo2 = campo2 + digitoMod.ToString();
            campo2 = Strings.Mid(campo2, 1, 5) + "." + Strings.Mid(campo2, 6, 6);


            /*
            Fields 3 (EEEEE.EEEEEZ)
            E = Posição 35 a 44 do código de barras
            Z = DV que amarra o campo 3 (Módulo 10, contido no Anexo 7)
             */
            campo3 = Strings.Mid(cmplivre, 16, 10);
            digitoMod = Mod10(campo3);
            campo3 = campo3 + digitoMod;
            campo3 = Strings.Mid(campo3, 1, 5) + "." + Strings.Mid(campo3, 6, 6);

            /*
            Fields 4 (K)
            K = DV do Código de Barras (Módulo 11, contido no Anexo 10)
             */
            campo4 = Strings.Mid(boleto.Barcode.Code, 5, 1);

            /*
            Campo 5 (UUUUVVVVVVVVVV)
            U = Fator de Vencimento ( Anexo 10)
            V = Valor do Título (*)
             */
            icampo5 = Convert.ToInt64(Strings.Mid(boleto.Barcode.Code, 6, 14));

            if (icampo5 == 0)
                campo5 = "000";
            else
                campo5 = icampo5.ToString();

            boleto.Barcode.LineDigitavel = campo1 + "  " + campo2 + "  " + campo3 + "  " + campo4 + "  " + campo5;
        }

        /// <summary>
        /// Formata o nosso número para ser mostrado no boleto.
        /// </summary>
        /// <remarks>
        /// Última a atualização por jsoda em 01/07/2009
        /// </remarks>
        /// <param name="boleto"></param>
        public override void OurNumberFormats(Boleto boleto)
        {
            if (boleto.wallet.Equals("18-019"))
                boleto.OurNumber = string.Format("{0}/{1}", ClearPortfolio(boleto.wallet), boleto.OurNumber);
            else
                boleto.OurNumber = string.Format("{0}-{1}", boleto.OurNumber,Mod11BrazilBank(boleto.OurNumber));

            #region Commented By Pradeep Kushwaha on 01-12-2010
            //if (boleto.wallet.Equals("18-019"))
            //    boleto.OurNumber = string.Format("{0}/{1}", ClearPortfolio(boleto.wallet), boleto.OurNumber);
            //else
            //    boleto.OurNumber = string.Format("{0}/{1}-{2}", ClearPortfolio(boleto.wallet), boleto.OurNumber,
            //                                    Mod11BancoBrasil(boleto.OurNumber));
            #endregion
        }


        public override void DocumentNumberFormats(Boleto boleto)
        {
        }

        # endregion

        ////# region Métodos de geração do arquivo remessa CNAB240

        ////# region HEADER

        /////// <summary>
        /////// HEADER do arquivo CNAB
        /////// Gera o HEADER do arquivo remessa de acordo com o lay-out informado
        /////// </summary>
        ////public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo)
        ////{
        ////    try
        ////    {
        ////        string _header = " ";

        ////        base.GerarHeaderRemessa(numeroConvenio, cedente, tipoArquivo);

        ////        switch (tipoArquivo)
        ////        {

        ////            case TipoArquivo.CNAB240:
        ////                _header = GerarHeaderRemessaCNAB240(cedente);
        ////                break;
        ////            case TipoArquivo.CNAB400:
        ////                _header = GerarHeaderRemessaCNAB400(cedente);
        ////                break;
        ////            case TipoArquivo.Outro:
        ////                throw new Exception("Tipo de arquivo inexistente.");
        ////        }

        ////        return _header;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do HEADER do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////public string GerarHeaderRemessaCNAB240(Cedente cedente)
        ////{
        ////    try
        ////    {
        ////        throw new NotImplementedException("Função não implementada!");
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do TRAILER do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////public override string GerarHeaderRemessa(Cedente cedente, TipoArquivo tipoArquivo)
        ////{
        ////    try
        ////    {
        ////        base.GerarHeaderRemessa(cedente, tipoArquivo);

        ////        string _brancos20 = new string(' ', 20);
        ////        string _brancos10 = new string(' ', 10);
        ////        string _header;

        ////        _header = "00100000         ";
        ////        if (cedente.CPFCNPJ.Length <= 11)
        ////            _header += "1";
        ////        else
        ////            _header += "2";
        ////        _header += Utils.FitStringLength(cedente.CPFCNPJ, 14, 14, '0', 0, true, true, true);
        ////        _header += _brancos20;
        ////        _header += Utils.FitStringLength(cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
        ////        _header += Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, ' ', 0, true, true, false);
        ////        _header += Utils.FitStringLength(cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
        ////        _header += Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false);
        ////        _header += " "; // DÍGITO VERIFICADOR DA AG./CONTA
        ////        _header += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false);
        ////        _header += Utils.FitStringLength("BANCO DO BRASIL S.A.", 30, 30, ' ', 0, true, true, false);
        ////        _header += _brancos10;
        ////        _header += "1";
        ////        _header += DateTime.Now.ToString("ddMMyyyy");
        ////        _header += DateTime.Now.ToString("hhMMss");
        ////        _header += "000001"; // NÚMERO SEQUENCIAL DO ARQUIVO *EVOLUIR UM NÚMERO A CADA HEADER DE ARQUIVO
        ////        _header += "03000000";
        ////        _header += _brancos20;
        ////        _header += _brancos20;
        ////        _header += _brancos10;
        ////        _header += "    ";
        ////        _header += "000  ";
        ////        _header += _brancos10;

        ////        _header = Utils.SubstituiCaracteresEspeciais(_header);

        ////        return _header;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do HEADER DE ARQUIVO do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
        ////{
        ////    try
        ////    {
        ////        string _brancos40 = new string(' ', 40);
        ////        string _brancos33 = new string(' ', 33);
        ////        int _tamanho = numeroConvenio.Length;
        ////        string _numeroConvenio = Utils.FitStringLength(numeroConvenio.Substring(0, _tamanho), 18, 18, '0', 0, true, true, true);
        ////        string _headerLote;

        ////        _headerLote = "00100011R0100020 ";
        ////        if (cedente.CPFCNPJ.Length <= 11)
        ////            _headerLote += "1";
        ////        else
        ////            _headerLote += "2";
        ////        _headerLote += Utils.FitStringLength(cedente.CPFCNPJ, 15, 15, '0', 0, true, true, true);
        ////        _headerLote += Utils.FitStringLength(_numeroConvenio, 20, 20, ' ', 0, true, true, false);
        ////        _headerLote += Utils.FitStringLength(cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
        ////        _headerLote += Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, ' ', 0, true, true, false);
        ////        _headerLote += Utils.FitStringLength(cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
        ////        _headerLote += Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false);
        ////        _headerLote += " "; // DÍGITO VERIFICADOR DA AG./CONTA
        ////        _headerLote += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false);
        ////        _headerLote += _brancos40;
        ////        _headerLote += _brancos40;
        ////        _headerLote += Utils.FitStringLength(numeroArquivoRemessa.ToString(), 8, 8, '0', 0, true, true, true);
        ////        _headerLote += DateTime.Now.ToString("ddMMyyyy");
        ////        _headerLote += "00000000";
        ////        _headerLote += _brancos33;

        ////        _headerLote = Utils.SubstituiCaracteresEspeciais(_headerLote);

        ////        return _headerLote;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do HEADER DE LOTE do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////# endregion

        ////# region DETALHE

        /////// <summary>
        /////// DETALHE do arquivo CNAB
        /////// Gera o DETALHE do arquivo remessa de acordo com o lay-out informado
        /////// </summary>
        ////public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        ////{
        ////    try
        ////    {
        ////        string _detalhe = " ";

        ////        base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);

        ////        switch (tipoArquivo)
        ////        {
        ////            case TipoArquivo.CNAB240:
        ////                _detalhe = GerarDetalheRemessaCNAB240(boleto, numeroRegistro, tipoArquivo);
        ////                break;
        ////            case TipoArquivo.CNAB400:
        ////                _detalhe = GerarDetalheRemessaCNAB400(boleto, numeroRegistro, tipoArquivo);
        ////                break;
        ////            case TipoArquivo.Outro:
        ////                throw new Exception("Tipo de arquivo inexistente.");
        ////        }

        ////        return _detalhe;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////public override string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio)
        ////{
        ////    try
        ////    {
        ////        string _segmentoP;
        ////        string _nossoNumero;

        ////        _segmentoP = "00100013";
        ////        _segmentoP += Utils.FitStringLength(numeroRegistro.ToString(), 5, 5, '0', 0, true, true, true);
        ////        _segmentoP += "P 01";
        ////        _segmentoP += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
        ////        _segmentoP += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, ' ', 0, true, true, false);
        ////        _segmentoP += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
        ////        _segmentoP += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, false);
        ////        _segmentoP += " ";

        ////        int totalCaracteres = numeroConvenio.Length - 9;
        ////        _segmentoP += numeroConvenio.Substring(0, totalCaracteres);

        ////        _nossoNumero = Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, '0', 0, true, true, true);
        ////        int _total = numeroConvenio.Substring(0, totalCaracteres).Length + _nossoNumero.Length;
        ////        int subtotal = 0;
        ////        subtotal = 20 - _total;
        ////        string _comnplemento = new string(' ', subtotal);
        ////        _segmentoP += _nossoNumero;
        ////        _segmentoP += _comnplemento;

        ////        _segmentoP += Utils.FitStringLength(LimparCarteira(boleto.Carteira), 1, 1, '0', 0, true, true, true);
        ////        _segmentoP += "1111";
        ////        _segmentoP += Utils.FitStringLength(boleto.NumeroDocumento, 15, 15, ' ', 0, true, true, false);
        ////        _segmentoP += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);
        ////        _segmentoP += Utils.FitStringLength(Convert.ToString(boleto.ValorBoleto * 100), 15, 15, '0', 0, true, true, true);
        ////        _segmentoP += "000000";
        ////        _segmentoP += Utils.FitStringLength(boleto.EspecieDocumento.Codigo.ToString(), 2, 2, '0', 0, true, true, true);
        ////        _segmentoP += "N";
        ////        _segmentoP += Utils.FitStringLength(boleto.DataDocumento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);

        ////        if (boleto.JurosMora > 0)
        ////        {
        ////            _segmentoP += "1";
        ////            _segmentoP += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
        ////            _segmentoP += Utils.FitStringLength(Convert.ToString(boleto.JurosMora * 100), 15, 15, '0', 0, true, true, true);
        ////        }
        ////        else
        ////        {
        ////            _segmentoP += "3";
        ////            _segmentoP += "00000000";
        ////            _segmentoP += "000000000000000";
        ////        }

        ////        if (boleto.ValorDesconto > 0)
        ////        {
        ////            _segmentoP += "1";
        ////            _segmentoP += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
        ////            _segmentoP += Utils.FitStringLength(Convert.ToString(boleto.ValorDesconto * 100), 15, 15, '0', 0, true, true, true);
        ////        }
        ////        else
        ////            _segmentoP += "000000000000000000000000";

        ////        _segmentoP += "000000000000000";
        ////        _segmentoP += "000000000000000";
        ////        _segmentoP += Utils.FitStringLength(boleto.NumeroDocumento, 25, 25, ' ', 0, true, true, false);

        ////        if (boleto.Instrucoes.Count > 1 && boleto.Instrucoes[0].QuantidadeDias > 0)
        ////        {
        ////            _segmentoP += "2";
        ////            _segmentoP += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(), 2, 2, '0', 0, true, true, true);
        ////        }
        ////        else
        ////            _segmentoP += "300";

        ////        _segmentoP += "2000090000000000 ";

        ////        _segmentoP = Utils.SubstituiCaracteresEspeciais(_segmentoP);

        ////        return _segmentoP;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////public override string GerarDetalheSegmentoQRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        ////{
        ////    try
        ////    {
        ////        string _nossoNumero = new string('0', 20);
        ////        string _zeros16 = new string('0', 16);
        ////        string _brancos40 = new string(' ', 40);
        ////        string _brancos28 = new string(' ', 28);

        ////        string _segmentoQ;

        ////        _segmentoQ = "00100013";
        ////        _segmentoQ += Utils.FitStringLength(numeroRegistro.ToString(), 5, 5, '0', 0, true, true, true);
        ////        _segmentoQ += "Q 01";
        ////        if (boleto.Sacado.CPFCNPJ.Length <= 11)
        ////            _segmentoQ += "1";
        ////        else
        ////            _segmentoQ += "2";
        ////        _segmentoQ += Utils.FitStringLength(boleto.Sacado.CPFCNPJ, 15, 15, '0', 0, true, true, true);
        ////        _segmentoQ += Utils.FitStringLength(boleto.Sacado.Nome.ToUpper(), 40, 40, ' ', 0, true, true, false);
        ////        _segmentoQ += Utils.FitStringLength((boleto.Sacado.Endereco.End + " " + boleto.Sacado.Endereco.Numero + " - " + boleto.Sacado.Endereco.Complemento), 40, 40, ' ', 0, true, true, true).ToUpper();
        ////        _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro, 15, 15, ' ', 0, true, true, false).ToUpper();
        ////        _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.CEP, 8, 8, ' ', 0, true, true, false).ToUpper(); ;
        ////        _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 15, 15, ' ', 0, true, true, false).ToUpper();
        ////        _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
        ////        _segmentoQ += _zeros16;
        ////        _segmentoQ += _brancos40;
        ////        _segmentoQ += "000";
        ////        _segmentoQ += _brancos28;

        ////        _segmentoQ = Utils.SubstituiCaracteresEspeciais(_segmentoQ);

        ////        return _segmentoQ;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do SEGMENTO Q DO DETALHE do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////public override string GerarDetalheSegmentoRRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        ////{
        ////    try
        ////    {
        ////        string _brancos90 = new string(' ', 90);
        ////        string _brancos33 = new string(' ', 33);
        ////        string _zeros27 = new string('0', 27);
        ////        string _segmentoR;

        ////        _segmentoR = "00100013";
        ////        _segmentoR += Utils.FitStringLength(numeroRegistro.ToString(), 5, 5, '0', 0, true, true, true);
        ////        _segmentoR += "R ";
        ////        _segmentoR += "01";
        ////        // Código do desconto 2 - percentual até a data informada
        ////        // Data do desconto
        ////        // Valor/Percentual do desconto
        ////        // Campos: Desconto 2 e Desconto 3
        ////        _segmentoR += "000000000000000000000000";
        ////        _segmentoR += "000000000000000000000000";
        ////        // Código da multa 2 - percentual
        ////        _segmentoR += "2";
        ////        _segmentoR += Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
        ////        _segmentoR += Utils.FitStringLength(Convert.ToString(boleto.ValorMulta * 100), 15, 15, '0', 0, true, true, true);
        ////        _segmentoR += _brancos90;
        ////        _segmentoR += _zeros27;
        ////        _segmentoR += _brancos33;

        ////        _segmentoR = Utils.SubstituiCaracteresEspeciais(_segmentoR);

        ////        return _segmentoR;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////public string GerarDetalheRemessaCNAB240(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        ////{
        ////    throw new NotImplementedException("Função não implementada.");
        ////}

        ////public string GerarDetalheRemessaCNAB400(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        ////{
        ////    throw new NotImplementedException("Função não implementada.");
        ////}

        ////# endregion DETALHE

        ////# region TRAILER

        /////// <summary>
        /////// TRAILER do arquivo CNAB
        /////// Gera o TRAILER do arquivo remessa de acordo com o lay-out informado
        /////// </summary>
        ////public override string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo)
        ////{
        ////    try
        ////    {
        ////        string _trailer = " ";

        ////        base.GerarTrailerRemessa(numeroRegistro, tipoArquivo);

        ////        switch (tipoArquivo)
        ////        {
        ////            case TipoArquivo.CNAB240:
        ////                _trailer = GerarTrailerRemessa240();
        ////                break;
        ////            case TipoArquivo.CNAB400:
        ////                _trailer = GerarTrailerRemessa400(numeroRegistro);
        ////                break;
        ////            case TipoArquivo.Outro:
        ////                throw new Exception("Tipo de arquivo inexistente.");
        ////        }

        ////        return _trailer;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do TRAILER de arquivo do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////public string GerarTrailerRemessa240()
        ////{
        ////    throw new NotImplementedException("Função não implementada.");
        ////}

        ////public override string GerarTrailerLoteRemessa(int numeroRegistro)
        ////{
        ////    try
        ////    {
        ////        string _brancos92 = new string('0', 92);
        ////        string _brancos125 = new string(' ', 125);
        ////        string _trailerLote;

        ////        _trailerLote = "00100015         ";
        ////        _trailerLote += Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true);
        ////        _trailerLote += _brancos92;
        ////        _trailerLote += _brancos125;

        ////        _trailerLote = Utils.SubstituiCaracteresEspeciais(_trailerLote);

        ////        return _trailerLote;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("", ex);
        ////    }
        ////}

        ////public override string GerarTrailerArquivoRemessa(int numeroRegistro)
        ////{
        ////    try
        ////    {
        ////        string _brancos205 = new string(' ', 205);
        ////        string _trailerArquivo;

        ////        _trailerArquivo = "00199999         000001";
        ////        _trailerArquivo += Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true);
        ////        _trailerArquivo += "000000";
        ////        _trailerArquivo += _brancos205;

        ////        _trailerArquivo = Utils.SubstituiCaracteresEspeciais(_trailerArquivo);

        ////        return _trailerArquivo;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("", ex);
        ////    }
        ////}

        ////public string GerarTrailerRemessa400(int numeroRegistro)
        ////{
        ////    try
        ////    {
        ////        string complemento = new string(' ', 393);
        ////        string _trailer;

        ////        _trailer = "9";
        ////        _trailer += complemento;
        ////        _trailer += Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true); // Número sequencial do registro no arquivo.

        ////        _trailer = Utils.SubstituiCaracteresEspeciais(_trailer);

        ////        return _trailer;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////# endregion


        ////public string GerarHeaderRemessaCNAB400(Cedente cedente)
        ////{
        ////    try
        ////    {
        ////        throw new NotImplementedException("Funçao não implementada!");
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("Erro durante a geração do HEADER do arquivo de REMESSA.", ex);
        ////    }
        ////}

        ////#endregion

        ////# region Métodos de processamento do arquivo retorno CNAB240



        ////# endregion

        internal static string Mod11BancoBrasil(string value)
        {
            #region Trecho do manual DVMD11.doc
            /* 
            Multiplicar cada algarismo que compõe o número pelo seu respectivo multiplicador (PESO).
            Os multiplicadores(PESOS) variam de 9 a 2.
            O primeiro dígito da direita para a esquerda deverá ser multiplicado por 9, o segundo por 8 e assim sucessivamente.
            O resultados das multiplicações devem ser somados:
            72+35+24+27+4+9+8=179
            O total da soma deverá ser dividido por 11:
            179 / 11=16
            RESTO=3

            Se o resto da divisão for igual a 10 o D.V. será igual a X. 
            Se o resto da divisão for igual a 0 o D.V. será igual a 0.
            Se o resto for menor que 10, o D.V.  será igual ao resto.

            No exemplo acima, o dígito verificador será igual a 3
            */
            #endregion

            /* d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            string d;
            int s = 0, p = 9, b = 2;

            for (int i = value.Length - 1; i >= 0; i--)
            {
                s += (int.Parse(value[i].ToString()) * p);
                if (p == b)
                    p = 9;
                else
                    p--;
            }

            int r = (s % 11);
            if (r == 10)
                d = "X";
            else if (r == 0)
                d = "0";
            else
                d = r.ToString();

            return d;
        }
        /// <summary>
        /// for the digit of NOSSO NUMERO / Reference Number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //Added By Pradeep Kushwaha on 07-12-2010
        internal static string Mod11BrazilBank(string value)
        {
            int d, s = 0, p = 2,b = 9;

            for (int i = value.Length; i > 0; i--)
            {
                s = s + (Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(value, i, 1)) * p);
                if (p == b)
                    p = 2;
                else
                    p = p + 1;
            }

            d = (s * 10) % 11;

            if ((d == 10) || (d == 0))
                d = 0;

            return d.ToString();
        }

    }
}
