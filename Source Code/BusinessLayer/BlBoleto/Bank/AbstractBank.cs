using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
   public abstract class AbstractBank
    {
        #region Variables

        private int _code = 0;
        private int _digit = 0;
        private string _name = string.Empty;
        private Assginor _Assginor = null;

        #endregion Variables

       #region Propriedades
      /// <summary>
        /// Código do Banco
        /// 237 - Bradesco; 341 - Itaú
        /// </summary>
        public virtual int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        /// <summary>
        /// Dígito do Banco
        /// </summary>
        public virtual int Digit
        {
            get { return _digit; }
            protected set { _digit = value; }
        }

        /// <summary>
        /// Nome da Instituição Financeira
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        /// <summary>
        /// Cedente
        /// </summary>
        public virtual Assginor Assginor
        {
            get { return _Assginor; }
            protected set { _Assginor = value; }
        }

        #endregion Propriedades


        #region Methods

        /// <summary>
        /// Retorna o campo que compos o código de barras que para todos os bancos são iguais foramado por:
        /// </summary>
        /// <returns></returns>
       // public virtual string CampoFixo()
       // {
       //     throw new NotImplementedException("Function not implemented");
       // }
       // /// <summary>
       // /// Gera os registros de header do aquivo de remessa
       // /// </summary>
        
       ///// //Commented
       //                 //public virtual string GerarHeaderRemessa(string numeroConvenio, Assginor assginor, Filetype filetype)
       //                 //{
       //                 //    string _header = "";
       //                 //    return _header;
       //                 //}
       // /// <summary>
       // /// Gera registros de detalhe do arquivo remessa
       // /// </summary>
       // public virtual string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, Filetype tipoArquivo)
       // {
       //     string _remessa = "";
       //     return _remessa;
       // }
       // /// <summary>
       // /// Gera os registros de Trailer do arquivo de remessa
       // /// </summary>
       // public virtual string GerarTrailerRemessa(int numeroRegistro, Filetype tipoArquivo)
       // {
       //     string _trailer = "";
       //     return _trailer;
       // }
       // /// <summary>
       // /// Gera os registros de header de aquivo do arquivo de remessa
       // /// </summary>
       // public virtual string GerarHeaderRemessa(Assginor assginor, Filetype filetype)
       // {
       //     string _headerArquivo = "";
       //     return _headerArquivo;
       // }
       // /// <summary>
       // /// Gera os registros de header de lote do arquivo de remessa
       // /// </summary>
       // public virtual string GerarHeaderLoteRemessa(string numeroConvenio, Assginor assginor, int numeroArquivoRemessa)
       // {
       //     string _headerLote = "";
       //     return _headerLote;
       // }
       // /// <summary>
       // /// Gera os registros de header de lote do arquivo de remessa
       // /// </summary>
       // public virtual string GerarHeaderLoteRemessa(string numeroConvenio, Assginor assginor, int numeroArquivoRemessa, Filetype filetype)
       // {
       //     string _headerLote = "";
       //     return _headerLote;
       // }
       // /// <summary>
       // /// Gera registros de detalhe do arquivo remessa - SEGMENTO P
       // /// </summary>
       // public virtual string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio, Assginor assginor)
       // {
       //     string _segmentoP = "";
       //     return _segmentoP;
       // }
       // /// <summary>
       // /// Gera registros de detalhe do arquivo remessa - SEGMENTO P
       // /// </summary>
       // public virtual string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio)
       // {
       //     string _segmentoP = "";
       //     return _segmentoP;
       // }
       // /// <summary>
       // /// Gera registros de detalhe do arquivo remessa - SEGMENTO Q
       // /// </summary>
       // public virtual string GerarDetalheSegmentoQRemessa(Boleto boleto, int numeroRegistro, Filetype tipoArquivo, Assginor assginor)
       // {
       //     string _segmentoQ = "";
       //     return _segmentoQ;
       // }
       // /// <summary>
       // /// Gera registros de detalhe do arquivo remessa - SEGMENTO Q
       // /// </summary>
       // public virtual string GerarDetalheSegmentoQRemessa(Boleto boleto, int numeroRegistro, Filetype filetype)
       // {
       //     string _segmentoQ = "";
       //     return _segmentoQ;
       // }
       // /// <summary>
       // /// Gera registros de detalhe do arquivo remessa - SEGMENTO R
       // /// </summary>
       // public virtual string GerarDetalheSegmentoRRemessa(Boleto boleto, int numeroRegistro, Filetype filetype)
       // {
       //     string _segmentoR = "";
       //     return _segmentoR;
       // }
       // /// <summary>
       // /// Gera os registros de Trailer de arquivo do arquivo de remessa
       // /// </summary>
       // public virtual string GerarTrailerArquivoRemessa(int numeroRegistro)
       // {
       //     string _trailerArquivo = "";
       //     return _trailerArquivo;
       // }
       // /// <summary>
       // /// Gera os registros de Trailer de lote do arquivo de remessa
       // /// </summary>
       // public virtual string GerarTrailerLoteRemessa(int numeroRegistro)
       // {
       //     string _trailerLote = "";
       //     return _trailerLote;
       // }
        
       

       
       
       ///// <summary>
       // /// Formata nosso número
       // /// </summary>
       // public virtual void FormataNossoNumero(Boleto boleto)
       // {
       //     throw new NotImplementedException("Function not implemented na classe filha. Implemente na classe que está sendo criada.");
       // }

        /// <summary>
        /// Formata código de barras
        /// </summary>      
        public virtual void BarCodeFormats(Boleto boleto)
        {
            throw new NotImplementedException("Function not implemented na classe filha. Implemente na classe que está sendo criada.");
        }
        /// <summary>
        /// Formata linha digitável
        /// </summary>
        public virtual void FormatLineDigitavel(Boleto boleto)
        {
            throw new NotImplementedException("Function not implemented na classe filha. Implemente na classe que está sendo criada.");
        }
        /// <summary>
        /// Formata nosso número
        /// </summary>
        public virtual void OurNumberFormats(Boleto boleto)
        {
            throw new NotImplementedException("Function not implemented na classe filha. Implemente na classe que está sendo criada.");
        }
        /// <summary>
        /// Formata número do documento
        /// </summary>
        public virtual void DocumentNumberFormats(Boleto boleto)
        {
            throw new NotImplementedException("Function not implemented na classe filha. Implemente na classe que está sendo criada.");
        }
        /// <summary>
        /// Valida o boleto
        /// </summary>
       
   
       
       
       //public virtual void ValidaBoleto(Boleto boleto)
       // {
       //     throw new NotImplementedException("Function Not Implemented in child class. Implement the class that is being created.");
       // }
        public virtual void ValidateBoleto(Boleto boleto)
        {
            throw new NotImplementedException("Function Not Implemented in child class. Implement the class that is being created");
        }


        #endregion Methods

        ///// <summary>
        ///// Gera registros de detalhe do arquivo remessa
        ///// </summary>
        //public virtual string GenerateShipmentDetail(Boleto boleto, int numeroRegistro, Filetype filetype)
        //{
        //    string _remessa = "";
        //    return _remessa;
        //}

 
        /// <summary>
        /// Gera registros de detalhe do arquivo remessa
        /// </summary>
        //public virtual string GenerateShipmentDetail(Boleto boleto, int numeroRegistro, Filetype filetype)
        //{
        //    string _remessa = "";
        //    return _remessa;
        //}

    
        ///// <summary>
        ///// Gera os registros de Trailer do arquivo de remessa
        ///// </summary>
        //public virtual string GenerateTrailerShipping(int numeroRegistro, Filetype filetype)
        //{
        //    string _trailer = "";
        //    return _trailer;
        //}
        ///// <summary>
        ///// Gera os registros de header do aquivo de remessa
        ///// </summary>
        //public virtual string GenerateShipmentHeader(string numeroConvenio, Assginor assginor, Filetype filetype)
        //{
        //    string _header = "";
        //    return _header;
        //}
       
       
       /// <summary>
        /// Factor winning billet
        /// </summary>
        /// <param name="boleto"></param>
        /// <returns></returns>
        protected static long FatorVencimento(Boleto boleto)
        {
            DateTime dateBase = new DateTime(2000, 7, 3, 0, 0, 0);
            return Utils.DateDiff(DateInterval.Day, dateBase, boleto.Expirationdate) + 1000;
        }
     
        //internal static string CalcFatorVencimento(DateTime dtv)
        //{
        //    DateTime dateBase = new DateTime(2000, 7, 3, 0, 0, 0);
        //    return Convert.ToString(Utils.DateDiff(DateInterval.Day, dateBase, dtv) + 1000);
        //}

 //**************************************************************************************************
       //Commented
        //public virtual TSegmentDetailReturnCNAB240 LerDetalheSegmentoTRetornoCNAB240(string Record )
        //{
        //    try
        //    {
        //        TSegmentDetailReturnCNAB240 segmentoT = new TSegmentDetailReturnCNAB240(Record );

        //        if (Record .Substring(13, 1) != "T")
        //            throw new Exception("Invalid record. The detail does not possess the characteristics of the segment T.");

        //        int dataVencimento = Convert.ToInt32(Record .Substring(69, 8));

        //        segmentoT.Bankcode = Convert.ToInt32(Record .Substring(0, 3)); ;
        //        segmentoT.IdCodeMovement = Convert.ToInt32(Record .Substring(15, 2));
        //        segmentoT.Agency = Convert.ToInt32(Record .Substring(17, 4));
        //        segmentoT.Agencydigit = Record .Substring(21, 1);
        //        segmentoT.Account = Convert.ToInt32(Record .Substring(22, 9));
        //        segmentoT.DigitAccount = Record .Substring(31, 1);

        //        segmentoT.OurNumber = Record .Substring(40, 13);
        //        segmentoT.Walletcode = Convert.ToInt32(Record .Substring(53, 1));
        //        segmentoT.Documentnumber = Record .Substring(54, 15);
        //        segmentoT.Expirationdate = Convert.ToDateTime(dataVencimento.ToString("##-##-####"));
        //        double valorTitulo = Convert.ToInt64(Record .Substring(77, 15));
        //        segmentoT.TitleValue = valorTitulo / 100;
        //        segmentoT.IdentificationTitleCompany = Record .Substring(100, 25);
        //        segmentoT.TypeofRegistration = Convert.ToInt32(Record .Substring(127, 1));
        //        segmentoT.NumberInscricao = Record .Substring(128, 15);
        //        segmentoT.NameDrawee = Record .Substring(143, 40);
        //        double valorTarifas = Convert.ToUInt64(Record .Substring(193, 15));
        //        segmentoT.ValueRates = valorTarifas / 100;
        //        segmentoT.sourcerejection = Convert.ToInt32(Record .Substring(208, 10));

        //        return segmentoT;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error processing file RETURN - T SEGMENT.", ex);
        //    }
        //}

        //public virtual USegmentDetailReturnCNAB240 LerDetalheSegmentoURetornoCNAB240(string Record )
        //{
        //    try
        //    {
        //        USegmentDetailReturnCNAB240 segmentoU = new USegmentDetailReturnCNAB240(Record );

        //        if (Record .Substring(13, 1) != "U")
        //            throw new Exception("Invalid record. The detail does not possess the characteristics of the segment U.");

        //        int dataOcorrencia = Convert.ToInt32(Record .Substring(137, 8));
        //        int dataCredito = Convert.ToInt32(Record .Substring(145, 8));

        //        int dataOcorrenciaSacado = 0;
        //        if (Record .Substring(153, 4) != "    ")
        //            dataOcorrenciaSacado = Convert.ToInt32(Record .Substring(157, 8));

        //        double jurosMultaEncargos = Convert.ToInt64(Record .Substring(17, 15));
        //        segmentoU.InterestPenaltyCharges = jurosMultaEncargos / 100;
        //        double valorDescontoConcedido = Convert.ToInt64(Record .Substring(32, 15));
        //        segmentoU.ValueDiscountGranted= valorDescontoConcedido / 100;
        //        double valorAbatimentoConcedido = Convert.ToInt64(Record .Substring(47, 15));
        //        segmentoU.ValueAbatementGranted = valorAbatimentoConcedido / 100;
        //        double valorIOFRecolhido = Convert.ToInt64(Record .Substring(62, 15));
        //        segmentoU.IOFCollapsedvalue = valorIOFRecolhido / 100;
        //        double valorPagoPeloSacado = Convert.ToInt64(Record .Substring(77, 15));
        //        segmentoU.Amountpaidbythedrawee = valorPagoPeloSacado / 100;
        //        double valorLiquidoASerCreditado = Convert.ToInt64(Record .Substring(92, 15));
        //        segmentoU.Netvaluetobecredited = valorLiquidoASerCreditado / 100;
        //        double valorOutrasDespesas = Convert.ToInt64(Record .Substring(107, 15));
        //        segmentoU.ValueOtherExpenses = valorOutrasDespesas / 100;
        //        double valorOutrosCreditos = Convert.ToInt64(Record .Substring(122, 15));
        //        segmentoU.OtherCreditsvalue = valorOutrosCreditos / 100;
        //        segmentoU.Occurrencedate = Convert.ToDateTime(dataOcorrencia.ToString("##-##-####"));
        //        segmentoU.Creditdate = Convert.ToDateTime(dataCredito.ToString("##-##-####"));
        //        segmentoU.DraweeOccurrencecode = Record .Substring(153, 4);
        //        if (dataOcorrenciaSacado != 0)
        //            segmentoU.OccurrencedateDrawee = Convert.ToDateTime(dataOcorrenciaSacado.ToString("##-##-####"));
        //        double valorOcorrenciaSacado = Convert.ToInt64(Record .Substring(165, 15));
        //        segmentoU.ValueOccurrenceDrawee = valorOcorrenciaSacado / 100;

        //        return segmentoU;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error processing file RETURN - U SEGMENT.", ex);
        //    }
        //}

        //public virtual ReturnDetails LerDetalheRetornoCNAB400(string Record )
        //{
        //    try
        //    {
        //        int Occurrencedate = Utils.ToInt32(Record.Substring(110, 6));
        //        int Expirationdate = Utils.ToInt32(Record.Substring(146, 6));
        //        int Creditdate = Utils.ToInt32(Record.Substring(295, 6));

        //        ReturnDetails detail = new ReturnDetails(Record);

        //        detail.CodeInscription = Utils.ToInt32(Record.Substring(1, 2));
        //        detail.NumberInscription = Record.Substring(3, 14);
        //        detail.Agency = Utils.ToInt32(Record.Substring(17, 4));
        //        detail.Account= Utils.ToInt32(Record.Substring(23, 5));
        //        detail.DacAccount = Utils.ToInt32(Record.Substring(28, 1));
        //        detail.UseCompany = Record.Substring(37, 25);
        //        detail.OurNumber = Record.Substring(85, 8);
        //        detail.DacOurNumber = Utils.ToInt32(Record.Substring(93, 1));
        //        detail.Portfolio = Record.Substring(107, 1);
        //        detail.CodeOccurrence = Utils.ToInt32(Record.Substring(108, 2));
        //        detail.Occurrencedate = Utils.ToDateTime(Occurrencedate.ToString("##-##-##"));
        //        detail.Documentnumber = Record.Substring(116, 10);
        //        detail.OurNumber = Record.Substring(126, 9);
        //        detail.Expirationdatedate = Utils.ToDateTime(Expirationdate.ToString("##-##-##"));
        //        double valorTitulo = Convert.ToInt64(Record .Substring(152, 13));
        //        detail.TitleValue = valorTitulo / 100;
        //        detail.Bankcode = Utils.ToInt32(Record.Substring(165, 3));
        //        detail.Collectingagencies = Utils.ToInt32(Record.Substring(168, 4));
        //        detail.Species = Utils.ToInt32(Record.Substring(173, 2));
        //        double tarifaCobranca = Convert.ToUInt64(Record .Substring(174, 13));
        //        detail.Farecollection = tarifaCobranca / 100;
        //        // 26 brancos
        //        double iof = Convert.ToUInt64(Record .Substring(214, 13));
        //        detail.IOF = iof / 100;
        //        double valorAbatimento = Convert.ToUInt64(Record .Substring(227, 13));
        //        detail.ValueAbatement = valorAbatimento / 100;
        //        double valorPrincipal = Convert.ToUInt64(Record .Substring(253, 13));
        //        detail.HomeValue = valorPrincipal / 100;
        //        double jurosMora = Convert.ToUInt64(Record .Substring(266, 13));
        //        detail.InterestMora = jurosMora / 100;
        //        detail.Occurrencedate = Utils.ToDateTime(Occurrencedate.ToString("##-##-##"));
        //        // 293 - 3 brancos
        //        detail.Creditdate = Utils.ToDateTime(Creditdate.ToString("##-##-##"));
        //        detail.InstructionCancelled = Utils.ToInt32(Record.Substring(301, 4));
        //        // 306 - 6 brancos
        //        // 311 - 13 zeros
        //        detail.NameDrawee = Record.Substring(324, 30);
        //        // 354 - 23 brancos
        //        detail.Errors = Record.Substring(377, 8);
        //        // 377 - Registros rejeitados ou alegação do sacado
        //        // 386 - 7 brancos

        //        detail.CodeSettlement = Record.Substring(392, 2);
        //        detail.Sequentialnumber = Utils.ToInt32(Record.Substring(394, 6));

        //        return detail;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
        //    }
        //}

//**************************************************************************************************


        #region Modulo
        internal static int Mod10(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, r;

            for (int i = seq.Length; i > 0; i--)
            {
                r = (Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(seq, i, 1)) * p);

                if (r > 9)
                    r = (r / 10) + (r % 10);

                s += r;

                if (p == 2)
                    p = 1;
                else
                    p = p + 1;
            }
            d = ((10 - (s % 10)) % 10);
            return d;
        }
       /// <summary>
       /// Use to calculate mod11 for Brazil Bank of Seq Number (Barcode)
       /// </summary>
       /// <returns></returns>
       //Added By Pradeep Kushwaha on 06-Dec-2010
        protected static int Mod11OFBrizilBank(string seq, int b)
        {

            int d, s = 0, p = 2;

            for (int i = seq.Length; i > 0; i--)
            {
                s = s + (Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(seq, i, 1)) * p);
                if (p == b)
                    p = 2;
                else
                    p = p + 1;
            }

            d = (s * 10) % 11;

            if ((d == 10) || (d == 0) || (d == 1))
                d = 1;

            return d;
        }
        protected static int Mod11(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, b = 9;

            for (int i = 0; i < seq.Length; i++)
            {
                s = s + (Convert.ToInt32(seq[i]) * p);
                if (p < b)
                    p = p + 1;
                else
                    p = 2;
            }

            d = 11 - (s % 11);
            if (d > 9)
                d = 0;
            return d;
        }

        protected static int Mod11(string seq, int b)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2;


            for (int i = seq.Length; i > 0; i--)
            {
                s = s + (Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(seq, i, 1)) * p);
                if (p == b)
                    p = 2;
                else
                    p = p + 1;
            }

            d = 11 - (s % 11);


            if ((d > 9) || (d == 0) || (d == 1))
                d = 1;

            return d;
        }

        protected static int Mod11Base9(string seq)
        {
            /* Variáveis
             * -------------
             * d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int d, s = 0, p = 2, b = 9;


            for (int i = seq.Length - 1; i >= 0; i--)
            {
                string aux = Convert.ToString(seq[i]);
                s += (Convert.ToInt32(aux) * p);
                if (p >= b)
                    p = 2;
                else
                    p = p + 1;
            }

            if (s < 11)
            {
                d = 11 - s;
                return d;
            }
            else
            {
                d = 11 - (s % 11);
                if ((d > 9) || (d == 0))
                    d = 0;

                return d;
            }
        }

        protected static int Mod11(string seq, int lim, int flag)
        {
            int mult = 0;
            int total = 0;
            int pos = 1;
            //int res = 0;
            int ndig = 0;
            int nresto = 0;
            string num = string.Empty;

            mult = 1 + (seq.Length % (lim - 1));

            if (mult == 1)
                mult = lim;


            while (pos <= seq.Length)
            {
                num = Microsoft.VisualBasic.Strings.Mid(seq, pos, 1);
                total += Convert.ToInt32(num) * mult;

                mult -= 1;
                if (mult == 1)
                    mult = lim;

                pos += 1;
            }
            nresto = (total % 11);
            if (flag == 1)
                return nresto;
            else
            {
                if (nresto == 0 || nresto == 1 || nresto == 10)
                    ndig = 1;
                else
                    ndig = (11 - nresto);

                return ndig;
            }
        }
        #endregion Mod



    }
}
