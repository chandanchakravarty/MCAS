using System;
using System.Web.UI;
using Microsoft.VisualBasic;
using System.Text;

[assembly: WebResource("BoletoNET.Images.341.jpg", "image/jpg")]
namespace Cms.BusinessLayer.BlBoleto
{
    /// <summary>
    /// Class relating to the bank Itau
    /// </summary>
    internal class Bank_Itau : AbstractBank, IBank
    {

        #region Variáveis

        private int _dacBoleto = 0;
        private int _dacNossoNumero = 0;

        #endregion

        #region Construtores

        internal Bank_Itau()
        {
            try
            {
                this.Code = 341;
                this.Digit= 7;
                this.Name = "Itaú";
            }
            catch (Exception ex)
            {
                throw new Exception("Error instantiating object.", ex);
            }
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Validações particulares do banco Itaú
        /// </summary>
        public override void ValidateBoleto(Boleto boleto)
        {
            try
            {
                //Carteiras válidas
                int[] cv = new int[] { 175, 109, 198, 107, 122, 142, 143, 196, 126, 131, 146, 150, 169 };//Flavio(fhlviana@hotmail.com) - adicionado a carteira 109
                bool validate = false;

                foreach (int c in cv)
                    if (Utils.ToString(boleto.wallet) == Utils.ToString(c))
                        validate = true;

                if (!validate){
                    StringBuilder carteirasImplementadas = new StringBuilder(100);

                    carteirasImplementadas.Append(". Carteiras implementadas: ");
                    foreach(int c in cv ){
                        carteirasImplementadas.AppendFormat(  " {0}" , c); 
                    }
                    throw new NotImplementedException("Portfolio unimplemented: " + boleto.wallet + carteirasImplementadas.ToString());
                }
                //Verifies that the size for the eight digits are NossoNumero
                if (Convert.ToInt32(boleto.OurNumber).ToString().Length > 8)
                    throw new NotImplementedException("The number of digits of our number for the portfolio" + boleto.wallet + ", 8 issues are.");
                else if (Convert.ToInt32(boleto.OurNumber).ToString().Length < 8)
                    boleto.OurNumber = Utils.FormatCode(boleto.OurNumber, 8);

                //You must fill out the document number
                if (boleto.wallet == "106" || boleto.wallet == "107"
                        || boleto.wallet == "122" || boleto.wallet == "142"
                        || boleto.wallet == "143" || boleto.wallet == "195"
                        || boleto.wallet == "196" || boleto.wallet == "198")
                {
                    if (Utils.ToInt32(boleto.Documentnumber) == 0)
                        throw new NotImplementedException("The document number can not be zero.");
                }

                //Formato o número do documento 
                if (Utils.ToInt32(boleto.Documentnumber) > 0)
                    boleto.Documentnumber = Utils.FormatCode(boleto.Documentnumber, 7);

                // Computes the DAC Our most number of portfolios
                // agency / account / portfolio / our number
                if (boleto.wallet != "126" && boleto.wallet != "131"
                    && boleto.wallet != "146" && boleto.wallet != "150"
                    && boleto.wallet != "168")
                    _dacNossoNumero = Mod10(boleto.Assginor.bankaccount.Agency + boleto.Assginor.bankaccount.Account + boleto.wallet + boleto.OurNumber);
                else
                    // Excessão 126 - 131 - 146 - 150 - 168
                    // carteira/nosso numero
                    _dacNossoNumero = Mod10(boleto.wallet + boleto.OurNumber);

                // Calcula o DAC da Conta Corrente
                boleto.Assginor.bankaccount.AccountDigit = Mod10(boleto.Assginor.bankaccount.Agency + boleto.Assginor.bankaccount.Account).ToString();
                //Atribui o nome do banco ao local de pagamento
                boleto.LocalPayment += Name + ". After winning only in ITAU";

                //Verifies that our number is valid
                if (Utils.ToInt64(boleto.OurNumber) == 0)
                    throw new NotImplementedException("Our number invalid");

                //Checks whether data processing is valid
                if (boleto.Processingdate.ToString("dd/MM/yyyy") == "01/01/0001")
                    boleto.Processingdate = DateTime.Now;

                //Checks if the date the document is valid
                if (boleto.Documentdate.ToString("dd/MM/yyyy") == "01/01/0001")
                    boleto.Documentdate = DateTime.Now;

                boleto.FormattingFields();
            }
            catch(Exception e)
            {
                throw new Exception("Error validating slips.", e);
            }
        }

        # endregion

        # region Formatting methods of the fetlock

        public override void BarCodeFormats(Boleto boleto)
        {
            try
            {
                // Código de Barras
                //banco & moeda & fator & valor & carteira & nossonumero & dac_nossonumero & agencia & conta & dac_conta & "000"

                string valorBoleto = boleto.Billetvalue.ToString("f").Replace(",", "").Replace(".", "");
                valorBoleto = Utils.FormatCode(valorBoleto, 10);

                string numeroDocumento = Utils.FormatCode(boleto.Documentnumber.ToString(), 7);
                string codigoCedente = Utils.FormatCode(boleto.Assginor.Code.ToString(), 5);

                if (boleto.wallet == "175" || boleto.wallet == "109")//Flavio(fhlviana@hotmail.com) - Adicionado carteira 109
                {
                    boleto.Barcode.Code =
                        string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}000", Code, boleto.currency,
                                      FatorVencimento(boleto), valorBoleto, boleto.wallet,
                                      boleto.OurNumber, _dacNossoNumero, boleto.Assginor.bankaccount.Agency,//Flavio(fhlviana@hotmail.com) => Cedente.ContaBancaria.Agencia --> boleto.Cedente.ContaBancaria.Agencia
                                      Utils.FormatCode(boleto.Assginor.bankaccount.Account, 5), boleto.Assginor.bankaccount.AccountDigit);//Flavio(fhlviana@hotmail.com) => Cedente.ContaBancaria.DigitoConta --> boleto.Cedente.ContaBancaria.DigitoConta
                }
                else if (boleto.wallet == "198" || boleto.wallet == "107"
                         || boleto.wallet == "122" || boleto.wallet == "142"
                         || boleto.wallet == "143" || boleto.wallet == "196")
                {
                    boleto.Barcode.Code = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}0", Code, boleto.currency,
                        FatorVencimento(boleto), valorBoleto, boleto.wallet,
                        boleto.OurNumber, numeroDocumento, codigoCedente,
                        Mod10(boleto.wallet + boleto.OurNumber + numeroDocumento + codigoCedente));
                }

                _dacBoleto = Mod11(boleto.Barcode.Code, 9, 0);

                boleto.Barcode.Code = Strings.Left(boleto.Barcode.Code, 4) + _dacBoleto + Strings.Right(boleto.Barcode.Code, 39);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao formatar código de barras.", ex);
            }
        }

        public override void FormatLineDigitavel(Boleto boleto)
        {
            try
            {
                string numeroDocumento = Utils.FormatCode(boleto.Documentnumber.ToString(), 7);
                string codigoCedente = Utils.FormatCode(boleto.Assginor.Code.ToString(), 5);

                string AAA = Utils.FormatCode(Code.ToString(), 3);
                string B = boleto.currency.ToString();
                string CCC = boleto.wallet.ToString();
                string DD = boleto.OurNumber.Substring(0, 2);
                string X = Mod10(AAA + B + CCC + DD).ToString();
                string LD = string.Empty; //Linha Digitável

                string DDDDDD = boleto.OurNumber.Substring(2, 6);

                string K = string.Format(" {0} ", _dacBoleto);

                string UUUU = FatorVencimento(boleto).ToString();
                string VVVVVVVVVV = boleto.Billetvalue.ToString("f").Replace(",", "").Replace(".", "");

                string C1 = string.Empty;
                string C2 = string.Empty;
                string C3 = string.Empty;
                string C5 = string.Empty;

                #region AAABC.CCDDX

                C1 = string.Format("{0}{1}{2}.", AAA, B, CCC.Substring(0, 1));
                C1 += string.Format("{0}{1}{2} ", CCC.Substring(1, 2), DD, X);

                #endregion AAABC.CCDDX

                #region UUUUVVVVVVVVVV

                VVVVVVVVVV = Utils.FormatCode(VVVVVVVVVV, 10);
                C5 = UUUU + VVVVVVVVVV;

                #endregion UUUUVVVVVVVVVV

                if (boleto.wallet == "175" || boleto.wallet == "109")//Flavio(fhlviana@hotmail.com) - adicionado carteira 109
                {
                    #region Definitions
                    /* AAABC.CCDDX.DDDDD.DEFFFY.FGGGG.GGHHHZ.K.UUUUVVVVVVVVVV
              * ------------------------------------------------------
              * Campo 1
              * AAABC.CCDDX
              * AAA - Código do Banco
              * B   - Moeda
              * CCC - Carteira
              * DD  - 2 primeiros números Nosso Número
              * X   - DAC Campo 1 (AAABC.CCDD) Mod10
              * 
              * Campo 2
              * DDDDD.DEFFFY
              * DDDDD.D - Restante Nosso Número
              * E       - DAC (Agência/Conta/Carteira/Nosso Número)
              * FFF     - Três primeiros da agência
              * Y       - DAC Campo 2 (DDDDD.DEFFF) Mod10
              * 
              * Campo 3
              * FGGGG.GGHHHZ
              * F       - Restante da Agência
              * GGGG.GG - Número Conta Corrente + DAC
              * HHH     - Zeros (Não utilizado)
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
                    #endregion Definitions

                    #region DDDDD.DEFFFY

                    string E = _dacNossoNumero.ToString();
                    string FFF = boleto.Assginor.bankaccount.Agency.Substring(0, 3);
                    string Y = Mod10(DDDDDD + E + FFF).ToString();

                    C2 = string.Format("{0}.", DDDDDD.Substring(0, 5));
                    C2 += string.Format("{0}{1}{2}{3} ", DDDDDD.Substring(5, 1), E, FFF, Y);

                    #endregion DDDDD.DEFFFY

                    #region FGGGG.GGHHHZ

                    string F = boleto.Assginor.bankaccount.Agency.Substring(3, 1);
                    string GGGGGG = boleto.Assginor.bankaccount.Account + boleto.Assginor.bankaccount.AccountDigit;
                    string HHH = "000";
                    string Z = Mod10(F + GGGGGG + HHH).ToString();

                    C3 = string.Format("{0}{1}.{2}{3}{4}", F, GGGGGG.Substring(0, 4), GGGGGG.Substring(4, 2), HHH, Z);

                    #endregion FGGGG.GGHHHZ
                }
                else if (boleto.wallet == "198" || boleto.wallet == "107"
                     || boleto.wallet == "122" || boleto.wallet == "142"
                     || boleto.wallet == "143" || boleto.wallet == "196")
                {
                    #region Definições
                    /* AAABC.CCDDX.DDDDD.DEEEEY.EEEFF.FFFGHZ.K.UUUUVVVVVVVVVV
              * ------------------------------------------------------
              * Campo 1 - AAABC.CCDDX
              * AAA - Código do Banco
              * B   - Moeda
              * CCC - Carteira
              * DD  - 2 primeiros números Nosso Número
              * X   - DAC Campo 1 (AAABC.CCDD) Mod10
              * 
              * Campo 2 - DDDDD.DEEEEY
              * DDDDD.D - Restante Nosso Número
              * EEEE    - 4 primeiros numeros do número do documento
              * Y       - DAC Campo 2 (DDDDD.DEEEEY) Mod10
              * 
              * Campo 3 - EEEFF.FFFGHZ
              * EEE     - Restante do número do documento
              * FFFFF   - Código do Cliente
              * G       - DAC (Carteira/Nosso Numero(sem DAC)/Numero Documento/Codigo Cliente)
              * H       - zero
              * Z       - DAC Campo 3
              * 
              * Campo 4 - K
              * K       - DAC Código de Barras
              * 
              * Campo 5 - UUUUVVVVVVVVVV
              * UUUU       - Fator Vencimento
              * VVVVVVVVVV - Valor do Título 
              */
                    #endregion Definições

                    #region DDDDD.DEEEEY

                    string EEEE = numeroDocumento.Substring(0, 4);
                    string Y = Mod10(DDDDDD + EEEE).ToString();

                    C2 = string.Format("{0}.", DDDDDD.Substring(0, 5));
                    C2 += string.Format("{0}{1}{2} ", DDDDDD.Substring(5, 1), EEEE, Y);

                    #endregion DDDDD.DEEEEY

                    #region EEEFF.FFFGHZ

                    string EEE = numeroDocumento.Substring(4, 3);
                    string FFFFF = codigoCedente;
                    string G = Mod10(boleto.wallet + boleto.OurNumber + numeroDocumento + codigoCedente).ToString();
                    string H = "0";
                    string Z = Mod10(EEE + FFFFF + G + H).ToString();
                    C3 = string.Format("{0}{1}.{2}{3}{4}{5}", EEE, FFFFF.Substring(0, 2), FFFFF.Substring(2, 3), G, H, Z);

                    #endregion EEEFF.FFFGHZ
                }
                else if (boleto.wallet == "126" || boleto.wallet == "131" || boleto.wallet == "146" || boleto.wallet == "150" || boleto.wallet == "168")
                {
                    throw new NotImplementedException("Função não implementada.");
                }

                boleto.Barcode.LineDigitavel = C1 + C2 + C3 + K + C5;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao formatar linha digitável.", ex);
            }
        }

        public override void OurNumberFormats(Boleto boleto)
        {
            try
            {
                boleto.OurNumber = string.Format("{0}/{1}-{2}", boleto.wallet, boleto.OurNumber, _dacNossoNumero);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao formatar nosso número", ex);
            }
        }

        public override void DocumentNumberFormats(Boleto boleto)
        {
            try
            {
                boleto.Documentnumber = string.Format("{0}-{1}", boleto.Documentnumber, Mod10(boleto.Documentnumber));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao formatar número do documento.", ex);
            }
        }

        # endregion

        # region Methods for file generation referral CNAB400

        # region HEADER

        /// <summary>
        /// HEADER do arquivo CNAB
        /// Gera o HEADER do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        
        /// Commented
        //public override string GerarHeaderRemessa(string numeroConvenio, Assginor assginor, Filetype filetype)
        //{
        //    try
        //    {
        //        string _header = " ";

        //        base.GenerateShipmentHeader("0", assginor, filetype);

        //        switch (filetype)
        //        {

        //            case Filetype.CNAB240:
        //                _header = GerarHeaderRemessaCNAB240();
        //                break;
        //            case Filetype.CNAB400:
        //                _header = GerarHeaderRemessaCNAB400(0, assginor);
        //                break;
        //            case Filetype.Other:
        //                throw new Exception("File Type nonexistent.");
        //        }

        //        return _header;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error during generation of the HEADER file SHIPPING.", ex);
        //    }
        //}

        //public string GerarHeaderRemessaCNAB240()
        //{
        //    throw new NotImplementedException("Function not implemented.");
        //}

        //public string GerarHeaderRemessaCNAB400(int numeroConvenio, Assginor assginor)
        //{
        //     try
        //     {
        //        string complemento = new string(' ', 294);
        //        string _header;

        //        _header = "01REMESSA01COBRANCA       ";
        //        _header += Utils.FitStringLength(assginor.bankaccount.Agency, 4, 4, '0', 0, true, true, true);
        //        _header += "00";
        //        _header += Utils.FitStringLength(assginor.bankaccount.Account, 5, 5, '0', 0, true, true, true);
        //        _header += Utils.FitStringLength(assginor.bankaccount.AccountDigit, 1, 1, ' ', 0, true, true, false);
        //        _header += "        ";
        //        _header += Utils.FitStringLength(assginor.Name, 30, 30, ' ', 0, true, true, false).ToUpper();
        //        _header += "341";
        //        _header += "BANCO ITAU SA  ";
        //        _header += DateTime.Now.ToString("ddMMyy");
        //        _header += complemento;
        //        _header += "000001";

        //        _header = Utils.SubstituiCaracteresEspeciais(_header);

        //        return _header;
        //     }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error generating HEADER file referral CNAB400.", ex);
        //    }
        //}

        # endregion

        # region DETALHE

        /// <summary>
        /// DETALHE do arquivo CNAB
        /// Gera o DETALHE do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        //public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, Filetype filetype)
        //{
        //    try
        //    {
        //        string _detalhe = " ";

        //        base.GenerateShipmentDetail(boleto, numeroRegistro, filetype);

        //        switch (filetype)
        //        {
        //            case Filetype.CNAB240:
        //                _detalhe = GerarDetalheRemessaCNAB240();
        //                break;
        //            case Filetype.CNAB400:
        //                _detalhe = GerarDetalheRemessaCNAB400(boleto, numeroRegistro, filetype);
        //                break;
        //            case Filetype.Other:
        //                throw new Exception("Tipo de arquivo inexistente.");
        //        }

        //        return _detalhe;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", ex);
        //    }
        //}

        public string GerarDetalheRemessaCNAB240()
        {
            throw new NotImplementedException("Função não implementada.");
        }

        //public string GerarDetalheRemessaCNAB400(Boleto boleto, int numeroRegistro, Filetype filetype)
        //{
        //    try
        //    {
        //        base.GenerateShipmentDetail(boleto, numeroRegistro, filetype);

        //        // USO DO BANCO - Identificação da operação no Banco (posição 87 a 107)
        //        string identificaOperacaoBanco = new string(' ', 21);
        //        string usoBanco = new string(' ', 10);
        //        string nrDocumento = new string(' ', 25);
        //        string _detalhe;

        //        _detalhe = "1";

        //        // Tipo de inscrição da empresa

        //        // Normalmente definem o tipo (CPF/CNPJ) e o número de inscrição do cedente. 
        //        // Se o título for negociado, deverão ser utilizados para indicar o CNPJ/CPF do sacador 
        //        // (cedente original), uma vez que os cartórios exigem essa informação para efetivação 
        //        // dos protestos. Para este fim, também poderá ser utilizado o registro tipo “5”.
        //        // 01 - CPF DO CEDENTE
        //        // 02 - CNPJ DO CEDENTE
        //        // 03 - CPF DO SACADOR
        //        // 04 - CNPJ DO SACADOR
        //        // O arquivo gerado pelo aplicativo do Banco ITAÚ, sempre atriubuiu 04 para o tipo de inscrição da empresa

        //        if (boleto.Assginor.CPFCNPJ.Length <= 11)
        //            _detalhe += "03";
        //        else
        //            _detalhe += "04";
        //        _detalhe += Utils.FitStringLength(boleto.Drawee.CPFCNPJ.ToString(), 14, 14, '0', 0, true, true, true);
        //        _detalhe += Utils.FitStringLength(boleto.Assginor.bankaccount.Agency.ToString(), 4, 4, '0', 0, true, true, true);
        //        _detalhe += "00";
        //        _detalhe += Utils.FitStringLength(boleto.Assginor.bankaccount.Account.ToString(), 5, 5, '0', 0, true, true, true);
        //        _detalhe += Utils.FitStringLength(boleto.Assginor.bankaccount.AccountDigit.ToString(), 1, 1, ' ', 0, true, true, false);
        //        _detalhe += "    "; // Completion of the record - four blank positions

        //        // Code statement / claim to be canceled
        //        // Must be completed only when used in the consignment, at position 109-110, the codes occurring 35 -
        //        // Cancellation of Instruction and 38 - Assignor does not agree with the claim withdrawn. For the other codes
        //        // Occurrence of this field should be filled with zeros.
        //        // NOTE: In the file returns the same code will be informed of the investigation terminated, and the cancellation of claim
        //        // Drawee of no feedback.

        //        // Por enquanto o objetivo é apenas gerar o arquivo de remessa e não utilizar o arquivo para enviar instruções
        //        // para títulos que já estão no banco, portanto o campo será preenchido com zeros.
        //        _detalhe += "0000";

        //        _detalhe += nrDocumento; // Utils.FitStringLength(boleto.NumeroDocumento, 25, 25, ' ', 0, true, true, false); //Identificação do título na empresa
        //        _detalhe += Utils.FitStringLength(boleto.OurNumber, 8, 8, '0', 0, true, true, true);
        //        // Quantidade de moeda variável - Preencher com zeros se a moeda for REAL
        //        // O manual do Banco ITAÚ não diz como preencher caso a moeda não seja o REAL
        //        if (boleto.currency == 9)
        //            _detalhe += "0000000000000"; 

        //        _detalhe += Utils.FitStringLength(boleto.wallet, 3, 3, '0', 0, true, true, true);
        //        _detalhe += Utils.FitStringLength(identificaOperacaoBanco, 21, 21, ' ', 0, true, true, true);
        //        // Código da carteira
        //        if (boleto.currency == 9)
        //            _detalhe += "I"; //O código da carteira só muda para dois tipos, quando a cobrança for em dólar

        //        _detalhe += "01"; // Identificação da ocorrência - 01 REMESSA
        //        _detalhe += Utils.FitStringLength(boleto.Documentnumber, 10, 10, ' ', 0, true, true, false);
        //        _detalhe += boleto.Expirationdate.ToString("ddMMyy");
        //        _detalhe += Utils.FitStringLength(Convert.ToString(boleto.Billetvalue * 100), 13, 13, '0', 0, true, true, true);
        //        _detalhe += "341";
        //        _detalhe += "00000"; // Agência onde o título será cobrado - no arquivo de remessa, preencher com ZEROS
                
        //        _detalhe += Utils.FitStringLength(Documentkind.ValidateCode(boleto.documentkind).ToString(), 2, 2, '0', 0, true, true, true);
        //        _detalhe += "N"; // Identificação de título, Aceito ou Não aceito

        //        //A data informada neste campo deve ser a mesma data de emissão do título de crédito 
        //        //(Duplicata de Serviço / Duplicata Mercantil / Nota Fiscal, etc), que deu origem a esta Cobrança. 
        //        //Existindo divergência, na existência de protesto, a documentação poderá não ser aceita pelo Cartório.
        //        _detalhe += boleto.Documentdate.ToString("ddMMyy");
                
        //        if(boleto.Instructions.Count > 1)
        //            _detalhe += Utils.FitStringLength(boleto.Instructions[0].Code.ToString(), 2, 2, '0', 0, true, true, true);

        //        if (boleto.Instructions.Count > 2)
        //            _detalhe += Utils.FitStringLength(boleto.Instructions[1].Code.ToString(), 2, 2, '0', 0, true, true, true);
        //        else
        //            _detalhe += "  ";

        //        // Juros de 1 dia
        //        //Se o cliente optar pelo padrão do Banco Itaú ou solicitar o cadastramento permanente na conta corrente, 
        //        //não haverá a necessidade de informar esse valor.
        //        //Caso seja expresso em moeda variável, deverá ser preenchido com cinco casas decimais.

        //        _detalhe += "0000000000000"; 

        //        // Data limite para desconto
        //        _detalhe += boleto.Expirationdate.ToString("ddMMyy");
        //        _detalhe += Utils.FitStringLength(boleto.Discountvalue.ToString(), 13, 13, '0', 0, true, true, true);
        //        _detalhe += "0000000000000"; // Valor do IOF
        //        _detalhe += "0000000000000"; // Valor do Abatimento

        //        if (boleto.Assginor.CPFCNPJ.Length > 11)
        //            _detalhe += "01";  // CPF
        //        else
        //            _detalhe += "02"; // CNPJ

        //        _detalhe += Utils.FitStringLength(boleto.Drawee.CPFCNPJ, 14, 14, '0', 0, true, true, true).ToUpper();
        //        _detalhe += Utils.FitStringLength(boleto.Drawee.Name, 30, 30, ' ', 0, true, true, false);
        //        _detalhe += usoBanco;
        //        _detalhe += Utils.FitStringLength((boleto.Drawee.Address.address + " " + boleto.Drawee.Address.Number + " - " + boleto.Drawee.Address.Completion), 40, 40, ' ', 0, true, true, true).ToUpper();
        //        _detalhe += Utils.FitStringLength(boleto.Drawee.Address.District, 12, 12, ' ', 0, true, true, false).ToUpper();
        //        _detalhe += Utils.FitStringLength(boleto.Drawee.Address.CEP, 8, 8, ' ', 0, true, true, false).ToUpper(); ;
        //        _detalhe += Utils.FitStringLength(boleto.Drawee.Address.City, 15, 15, ' ', 0, true, true, false).ToUpper();
        //        _detalhe += Utils.FitStringLength(boleto.Drawee.Address.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
        //        // SACADOR/AVALISTA
        //        // Normalmente deve ser preenchido com o nome do sacador/avalista. Alternativamente este campo poderá 
        //        // ter dois outros usos:
        //        // a) 2o e 3o descontos: para de operar com mais de um desconto(depende de cadastramento prévio do 
        //        // indicador 19.0 pelo Banco Itaú, conforme item 5)
        //        // b) Mensagens ao sacado: se utilizados as instruções 93 ou 94 (Nota 11), transcrever a mensagem desejada
        //        _detalhe += Utils.FitStringLength(boleto.Drawee.Name, 30, 30, ' ', 0, true, true, false).ToUpper();
        //        _detalhe += "    "; // Complemento do registro
        //        _detalhe += boleto.Expirationdate.ToString("ddMMyy");
        //        // PRAZO - Quantidade de DIAS - ver nota 11(A) - depende das instruções de cobrança 
        //        _detalhe += "00";
        //        _detalhe += " "; // Complemento do registro
        //        _detalhe += Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true);

        //        _detalhe = Utils.SubstituiCaracteresEspeciais(_detalhe);

        //        return _detalhe;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error generating CNAB400 DETAIL file.", ex);
        //    }
        //}

        # endregion DETALHE

        # region TRAILER

        /// <summary>
        /// TRAILER do arquivo CNAB
        /// Gera o TRAILER do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        //public override string GerarTrailerRemessa(int numeroRegistro, Filetype filetype)
        //{
        //    try
        //    {
        //        string _trailer = " ";

        //        base.GenerateTrailerShipping(numeroRegistro, filetype);

        //        switch (filetype)
        //        {
        //            case Filetype.CNAB240:
        //                _trailer = GerarTrailerRemessa240();
        //                break;
        //            case Filetype.CNAB400:
        //                _trailer = GerarTrailerRemessa400(numeroRegistro);
        //                break;
        //            case Filetype.Other:
        //                throw new Exception("File Type nonexistent.");
        //        }

        //        return _trailer;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }
        //}

        public string GerarTrailerRemessa240()
        {
            throw new NotImplementedException("Function not implemented.");
        }

        public string GerarTrailerRemessa400(int numeroRegistro)
        {
            try
            {
                string complemento = new string(' ', 393);
                string _trailer;

                _trailer = "9";
                _trailer += complemento;
                _trailer += Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true); // Número sequencial do registro no arquivo.

                _trailer = Utils.SubstituiCaracteresEspeciais(_trailer);

                return _trailer;
            }
            catch (Exception ex)
            {
                throw new Exception("Error during generation of the register file TRAILER SHIPMENT.", ex);
            }
        }

        # endregion

        #endregion

        #region Methods of processing the return file CNAB400


        #endregion

    }
}
