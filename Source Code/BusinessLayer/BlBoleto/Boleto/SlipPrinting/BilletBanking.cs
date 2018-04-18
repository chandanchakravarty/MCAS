using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Microsoft.VisualBasic;
//Envio por email
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Globalization;
using System.Threading;

[assembly: WebResource("BoletoNET.SlipPrinting.BoletoNet.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("BoletoNET.Images.barra.gif", "image/gif")]
//[assembly: WebResource("BoletoNet.Imagens.corte.gif", "image/gif")]
//[assembly: WebResource("BoletoNet.Imagens.barrainterna.gif", "image/gif")]
//[assembly: WebResource("BoletoNet.Imagens.ponto.gif", "image/gif")]


namespace Cms.BusinessLayer.BlBoleto
{

[Serializable(),
Designer(typeof(BilletBankingDesigner)),
ToolboxBitmap(typeof(BilletBanking)),
ToolboxData("<{0}:BilletBanking Runat=\"server\"></{0}:BilletBanking>")]


    public class BilletBanking : System.Web.UI.Control
    {


        #region Variables

        private Bank _ibank = null;
        private short _Bankcode = 0;
        private Boleto _boleto;
        private Assginor _Assginor;
        private Drawee _Drawee;
        private List<IInstructions> _instructions = new List<IInstructions>();
        private string _instrucoesHtml = string.Empty;
        private bool _DisplayWalletCode = false;
        private bool _FormatMeat = false;
       private String _CurrencySymbol = "";
       public NumberFormatInfo nfi;
        #endregion Variables
      
         #region Properties

        [Browsable(true), Description("Bank code that is generated in the billet. Eg 341-Itau, Bradesco-237")]
        public short BankCode
        {
            get { return _Bankcode; }
            set { _Bankcode = value; }
        }

        public String CurrencySymbol
        {
            get { return _CurrencySymbol; }
            set { _CurrencySymbol = value; }
        }

        /// <summary>
        /// Shows the wallet code
        /// </summary>
        [Browsable(true), Description("Shows the wallet code")]
        public bool DisplayWalletCode
        {
            get { return _DisplayWalletCode; }
            set { _DisplayWalletCode = value; }
        }
        
    
        /// <summary>
        ///Shows the wallet code
        /// </summary>
        [Browsable(true), Description("Formats the billet in the layout of booklet")]
        public bool FormatMeat
        {
            get { return _FormatMeat; }
            set { _FormatMeat = value; }
        }


        [Browsable(false)]
        public Boleto Boleto
        {
            get { return _boleto; }
            set
            {
                _boleto = value;

                if (_ibank == null)
                    _boleto.Bank = this.Bank;

                _Assginor = _boleto.Assginor;
                _Drawee = _boleto.Drawee;
            }
        }


        [Browsable(false)]
        public Drawee Drawee
        {
            get { return _Drawee; }
        }


        [Browsable(false)]
        public Assginor Assignor
        {
            get { return _Assginor; }
        }


        [Browsable(false)]
        public Bank Bank
        {
            get
            {
                if ((_ibank == null) ||
                    (_ibank.Code != _Bankcode))
                {
                    _ibank = new Bank(_Bankcode);
                }

                if (_boleto != null)
                    _boleto.Bank = _ibank;

                return _ibank;
            }
        }

        #endregion

    #region Properties

    [Browsable(true), Description("Shows the data without proof of delivery to schedule")]
   public bool MostrarComprovanteEntregaLivre  //translate ShowVoucherFreeDelivery
    {
        get { return Utils.ToBool(ViewState["MostrarComprovanteEntregaSemLivre"]); }
        set { ViewState["MostrarComprovanteEntregaSemLivre"] = value; }
    }

    [Browsable(true), Description("Shows proof of delivery")]
    public bool ProofofDilivery
    {
        get { return Utils.ToBool(ViewState["ProofofDilivery"]); }
        set { ViewState["ProofofDilivery"] = value; }
    }

    [Browsable(true), Description("Oculta as intruções do boleto")]
    public bool OcultarEnderecoSacado
    {
        get { return Utils.ToBool(ViewState["OcultarEnderecoSacado"]); }
        set { ViewState["OcultarEnderecoSacado"] = value; }
    }

    [Browsable(true), Description("Oculta as intruções do boleto")]
    public bool OcultarInstrucoes
    {
        get { return Utils.ToBool(ViewState["OcultarInstrucoes"]); }
        set { ViewState["OcultarInstrucoes"] = value; }
    }

    [Browsable(true), Description("Hides the receipt of the drawee of the fetlock")]
    public bool OcultarReciboSacado
    {
        get { return Utils.ToBool(ViewState["OcultarReciboSacado"]); }
        set { ViewState["OcultarReciboSacado"] = value; }
    }

    [Browsable(true), Description("Generate file for referral")]
    public bool GerarArquivoRemessa
    {
        get { return Utils.ToBool(ViewState["GerarArquivoRemessa"]); }
        set { ViewState["GerarArquivoRemessa"] = value; }
    }
    /// <summary> 
    /// Shows the term "Contra Presentation" on the expiration dates of the fetlock
    /// </summary>
    public bool ShowPresentationOnAgainstMaturityDate
    {
        get { return Utils.ToBool(ViewState["MCANDV"]); }
        set { ViewState["MCANDV"] = value; }
    }

    #endregion Properties


    /// <summary> 
    ///Returns the field is the first line of instruction.
    /// </summary>
    public List<IInstructions> Instructions
    {
        get
        {
            return _instructions;
        }

        set
        {
            _instructions = value;
        }
    }


    public static string UrlLogo(int bank)
    {
        Page page = System.Web.HttpContext.Current.CurrentHandler as Page;
        
        return page.ClientScript.GetWebResourceUrl(typeof(BilletBanking), "BoletoNET.Images." + Utils.FormatCode(bank.ToString(), 3) + ".jpg");
        
        //return page.ClientScript.GetWebResourceUrl(typeof(BilletBanking), "EbixBoleto.Images." + Utils.FormatCode(bank.ToString(), 3) + ".jpg");
    }


    #region Override
    
    protected override void OnPreRender(EventArgs e)
    {
        string alias = "BoletoNET.SlipPrinting.BoletoNet.css";
        string csslink = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" +
            Page.ClientScript.GetWebResourceUrl(typeof(BilletBanking), alias) + "\" />";

        LiteralControl include = new LiteralControl(csslink);
        Page.Header.Controls.Add(include);
      
        base.OnPreRender(e);
    }

    protected override void OnLoad(EventArgs e)
    {
    }

    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "Execution")]
    protected override void Render(HtmlTextWriter output)
    {
        if (_ibank == null)
        {
            output.Write("<b>Error generating boleto: failed to set the bank.</b>");
            return;
        }
       
      // string urlImagemLogo = Page.ClientScript.GetWebResourceUrl(typeof(BilletBanking), "BoletoNET.Images." + Utils.FormatCode(_ibank.Code.ToString(), 3) + ".jpg");
       //string urlImagemLogo =  "../Images/" + Utils.FormatCode(_ibank.Code.ToString(), 3) + ".jpg";
       string urlImagemLogo = "/Cms/CmsWeb/images/Boleto/" + Utils.FormatCode(_ibank.Code.ToString(), 3) + ".jpg";
     //Taking images
  

       // string urlImagemBarra = Page.ClientScript.GetWebResourceUrl(typeof(BilletBanking), "BoletoNET.Images.barra.gif");
        
        // string urlImagemBarra = "../Images/barra.gif";
       string urlImagemBarra = "/Cms/CmsWeb/images/Boleto/barra.gif";
        //Taking Line after image


        //string urlImagemBarraInterna = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.barrainterna.gif");
        //string urlImagemCorte = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.corte.gif");
        //string urlImagemPonto = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.ponto.gif");

        //Assigns values to the html of the bank
        //output.Write(BuildHtml(urlImagemCorte, urlImagemLogo, urlImagemBarra, urlImagemPonto, urlImagemBarraInterna,
        //    "<img src=\"ImagemCodigoBarra.ashx?code=" + Boleto.CodigoBarra.Codigo + "\" alt=\"Código de Barras\" />"));
         string barhtml = (BuildHtml(urlImagemLogo, urlImagemBarra, "<img src=\"ImagemCodigoBarra.ashx?code=" + Boleto.Barcode.Code + "\" alt=\"Barcode\" />"));
         output.Write(BuildHtml(urlImagemLogo, urlImagemBarra, "<img src=\"ImagemCodigoBarra.ashx?code=" + Boleto.Barcode.Code + "\" alt=\"Barcode\" />"));
    }

    #endregion override

        #region Html

    public string GeneratesHtmlInstructions()
    {
        try
        {
            StringBuilder html = new StringBuilder();

            //string titulo = "Printing instructions";
            //string instrucoes = "Print on ink jet printer (ink jet) or laser in normal quality. (Do not use economic mode). <br> Use A4 (210 x 297 mm) or Letter (216 x 279 mm) - Cutting the line indicated <br>";
            string titulo = "Instruções de Impressão";
            string instrucoes = "Imprimir em impressora jato de tinta (ink jet) ou laser em qualidade normal. (Não use modo econômico).<br>Utilize folha A4 (210 x 297 mm) ou Carta (216 x 279 mm) - Corte na linha indicada<br>";

            html.Append(Html.Instrucoes);
            html.Append("<br />");

            return html.ToString()
                .Replace("@TITULO", titulo)
                .Replace("@INSTRUCAO", instrucoes);
        }
        catch (Exception ex)
        {
            throw new Exception("Error during execution of the transaction.", ex);
        }
    }


    private string GeneratesHtmlMeat(string telefone, string htmlBoleto)
    {
        StringBuilder html = new StringBuilder();

        html.Append(Html.Carne);

        return html.ToString()
            .Replace("@TELEFONE", telefone)
            .Replace("#BOLETO#", htmlBoleto);
    }

    public string GeneratesHtmlReceiptDrawee()
    {
        try
        {
            StringBuilder html = new StringBuilder();

            html.Append(Html.ReciboSacadoParte1);
            html.Append("<br />");
            html.Append(Html.ReciboSacadoParte2);
            html.Append(Html.ReciboSacadoParte3);
            html.Append(Html.ReciboSacadoParte4);
            html.Append(Html.ReciboSacadoParte5);
            html.Append(Html.ReciboSacadoParte6);
            html.Append(Html.ReciboSacadoParte7);

            if (Instructions.Count == 0)
                html.Append(Html.ReciboSacadoParte8);

            AssembleInstructions();

            return html.ToString().Replace("@INSTRUCOES", _instrucoesHtml);
        }
        catch (Exception ex)
        {
            throw new Exception("Error while running transaction.", ex);
        }
    }

    public string GeneratesHtmlReceiptAssignor()
    {
        try
        {
            StringBuilder html = new StringBuilder();

            html.Append(Html.ReciboCedenteParte1);
            html.Append(Html.ReciboCedenteParte2);
            html.Append(Html.ReciboCedenteParte3);
            html.Append(Html.ReciboCedenteParte4);
            html.Append(Html.ReciboCedenteParte5);
            html.Append(Html.ReciboCedenteParte6);
            html.Append(Html.ReciboCedenteParte7);
            html.Append(Html.ReciboCedenteParte8);
            html.Append(Html.ReciboCedenteParte9);
            html.Append(Html.ReciboCedenteParte10);
            html.Append(Html.ReciboCedenteParte11);
            html.Append(Html.ReciboCedenteParte12);

            // Banco Itau, the text "(Text of liability of the transferor)" should be
            //(All information in this bloquetos are the sole responsibility of the vendor).
            if (Boleto.Bank.Code == 341)
            {
                html.Replace("(Texto de responsabilidade do cedente)", "(Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente)");
            }

            //Portfolio for "18-019" Bank of Brazil, the form has no compensation wallet code
            //formatting of the field
            if (Boleto.Bank.Code == 1 & Boleto.Assginor.Equals("18-019"))
            {
                html.Replace("Carteira /", "");
                html.Replace("@NOSSONUMERO", "@NOSSONUMEROBB");
            }

            AssembleInstructions();

            return html.ToString().Replace("@INSTRUCOES", _instrucoesHtml);
        }
        catch (Exception ex)
        {
            throw new Exception("Error in transaction execution.", ex);
        }
    }

    public string HtmlProofDelivery
    {
        get
        {
            StringBuilder html = new StringBuilder();

            html.Append(Html.ComprovanteEntrega1);
            html.Append(Html.ComprovanteEntrega2);
            html.Append(Html.ComprovanteEntrega3);
            html.Append(Html.ComprovanteEntrega4);
            html.Append(Html.ComprovanteEntrega5);
            html.Append(Html.ComprovanteEntrega6);

            if (MostrarComprovanteEntregaLivre)
                html.Append(Html.ComprovanteEntrega71);
            else
                html.Append(Html.ComprovanteEntrega7);

            html.Append("<br />");
            return html.ToString();
        }
    }



    private void AssembleInstructions()
    {
        if (string.IsNullOrEmpty(_instrucoesHtml))
            if (Boleto.Instructions.Count > 0)
            {
                _instrucoesHtml = string.Empty;
                //Flavio (fhlviana@hotmail.com) - removed the tag of each instruction by <span> Necessary since no longer in
                //div that contains the instructions to apply cpn class, which is the same in content, class cp
                foreach (IInstructions instrucao in Boleto.Instructions)
                    _instrucoesHtml += string.Format("{0}<br />", instrucao.Description);

                _instrucoesHtml = Strings.Left(_instrucoesHtml, _instrucoesHtml.Length - 6);
            }
    }

    //private string BuildHtml (urlImagemCorte string, string urlImagemLogo, urlImagemBarra string, string urlImagemPonto, urlImagemBarraInterna string, string imagemCodigoBarras)
    private string BuildHtml(string urlImagemLogo, string urlImagemBarra, string imagemCodigoBarras)
    {
        StringBuilder html = new StringBuilder();

        //Hides the instructions in the header of the fetlock
        if (!OcultarInstrucoes)
            html.Append(GeneratesHtmlInstructions());

        if (!FormatMeat)
        {
            //Shows proof of delivery
            if (ProofofDilivery | MostrarComprovanteEntregaLivre)
                html.Append(HtmlProofDelivery);

            //Hides the receipt of the fetlock Sacaba
            if (!OcultarReciboSacado)
                html.Append(GeneratesHtmlReceiptDrawee());
        }

        string drawee = "";
        //Flavio (fhlviana@hotmail.com) - added the possibility of the fetlock does not necessarily have to inform the CPF or CNPJ the drawee.
        //Formats the CPF / CNPJ (if any) and name Sacked for presentation
        if (Drawee.CPFCNPJ == string.Empty)
        {
            drawee = Drawee.Name;
        }
        else
        {
            if (Drawee.CPFCNPJ.Length <= 11)
                drawee = string.Format("{0}  CPF: {1}", Drawee.Name, Utils.FormataCPF(Drawee.CPFCNPJ));
            else
                drawee = string.Format("{0}  CNPJ: {1}", Drawee.Name, Utils.FormataCNPJ(Drawee.CPFCNPJ));
        }

        String infoDrawee = Drawee.DraweeInformation.GenerateHTML(false);

        //If you do not hide the address of the drawee,
        if (!OcultarEnderecoSacado)
        {
            //String enderecoSacado = "";
            String DraweeAddress = "";

            if (Drawee.Address.CEP == String.Empty)
            { 
                #region Commented by Pradeep iTrack 685 Notes By Paula Dated 12-July-2011
                //DraweeAddress = string.Format("{0} - {1}/{2} - {3}", Drawee.Address.District, Drawee.Address.City, Drawee.Address.UF, Drawee.Address.State);
                 #endregion
                //itrack 685
                DraweeAddress = string.Format("{0} - {1}/{2}", Drawee.Address.District, Drawee.Address.City, Drawee.Address.UF);
                //till here 
            }
            else
            {
                #region Commented by Pradeep iTrack 685 Notes By Paula Dated 12-July-2011
                //DraweeAddress = string.Format("{0} - {1}/{2} - CEP: {3} - {4}", Drawee.Address.District,
                //Drawee.Address.City, Drawee.Address.UF, Utils.FormataCEP(Drawee.Address.CEP), Drawee.Address.State);
                #endregion
                //itrack 685
                DraweeAddress = string.Format("{0} - {1}/{2} - CEP: {3}", Drawee.Address.District,
                Drawee.Address.City, Drawee.Address.UF, Utils.FormataCEP(Drawee.Address.CEP));
                //till here 
                //if (Drawee.Address.Completion == String.Empty)
                //{
                //    DraweeAddress = string.Format("{0} - {1} - {2}/{3} - CEP: {4} - {5}", Drawee.Address.Number, Drawee.Address.District,
                //    Drawee.Address.City, Drawee.Address.UF, Utils.FormataCEP(Drawee.Address.CEP), Drawee.Address.State);
                //}
                //else
                //{
                //    DraweeAddress = string.Format("{0} - {1} - {2} - {3}/{4} - CEP: {5} - {6}", Drawee.Address.Number, Drawee.Address.Completion, Drawee.Address.District,
                //                      Drawee.Address.City, Drawee.Address.UF, Utils.FormataCEP(Drawee.Address.CEP), Drawee.Address.State);
                //}
            }

            String Address = String.Empty;
            if (Drawee.Address.address != string.Empty && DraweeAddress != string.Empty)
            {
                if (Drawee.Address.Completion == String.Empty)
                    Address = string.Format(" - {0}", Drawee.Address.Number);
                else
                    Address = string.Format(" - {0} - {1}", Drawee.Address.Number, Drawee.Address.Completion);


                if (infoDrawee == string.Empty)
                {
                    infoDrawee += Drawee.Address.address + Address + " - " + DraweeAddress;
                    //infoDrawee += DraweeInfo.Render(Drawee.Address.address , DraweeAddress, false);//Commneted by Pradeep Kushwaha on 09-Feb-2011,To make address in one line  
                }
                else
                {
                    infoDrawee += Drawee.Address.address + Address + " - " + DraweeAddress;
                    //infoDrawee += DraweeInfo.Render(Drawee.Address.addresss, DraweeAddress, true);//Commneted by Pradeep Kushwaha on 09-Feb-2011,To make address in one line  
                }
            }
            //Sacked Information" was introduced to allow the billet in only reporting the address of the drawee
            //as in other situations where you print registrations codes, etc., on the drawee.
            //Thus the address of the drawee becomes one of Drawee information that is added at the time of rendering
            //according to the flag  "OcultarEnderecoSacado"
        }

        //string agencyAccount = Utils.FormataAgenciaConta(Assignor.bankaccount.Agency, Assignor.bankaccount.AgencyDigit, Assignor.bankaccount.Account, Assignor.bankaccount.AccountDigit);//Commented By Pradeep Kushwaha on 07-Dec-2010
        string agencyAccount = Utils.FormataAgenciaConta(Assignor.bankaccount.Agency, Assignor.bankaccount.AgencyDigit, Assignor.bankaccount.AccountNumber, Assignor.bankaccount.AccountDigit);//Added By Pradeep on 07-Dec-2010
        // Trecho adicionado por Fabrício Nogueira de Almeida :fna - fnalmeida@gmail.com - 09/12/2008
        /* Esse código foi inserido pq no campo Agência/Cod Cedente, estava sendo impresso sempre a agência / número da conta
         * No boleto da caixa que eu fiz, coloquei no método validarBoleto um trecho para calcular o dígito do cedente, e adicionei esse atributo na classe cedente
         * O trecho abaixo testa se esse digito foi calculado, se foi insere no campo Agencia/Cod Cedente, a agência e o código com seu digito
         * caso contrário mostra a agência / conta, como era anteriormente.
         * Com esse código ele ira atender as necessidades do boleto caixa e não afetará os demais
         * Caso queira que apareça o Agência/cod. cedente para outros boletos, basta calcular e setar o digito, como foi feito no boleto Caixa 
         */

       // string agenciaCodigoCedente;

        //Start Culture info setting here 
        DateTime dtDocumentdate = new DateTime();
        DateTime dtProcessingdate = new DateTime();
        DateTime dtExpirationdate = new DateTime();
        dtDocumentdate = Convert.ToDateTime(Boleto.Documentdate);
        dtProcessingdate = Convert.ToDateTime(Boleto.Processingdate);
        dtExpirationdate = Convert.ToDateTime(Boleto.Expirationdate);

        System.Globalization.CultureInfo oldculture = System.Threading.Thread.CurrentThread.CurrentCulture;
        CultureInfo culture = new CultureInfo("pt-BR");
        Thread.CurrentThread.CurrentCulture = culture;
        culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
        String format = "dd/MM/yyyy";

        nfi = culture.NumberFormat;
        nfi.NumberDecimalDigits = 2;

        String strBilletvalue=Boleto.Billetvalue == 0 ? "" : Boleto.Billetvalue.ToString("N",nfi);

        String strDocumentdate = dtDocumentdate.ToString(format);
        String strProcessingdate= dtProcessingdate.ToString(format);
        string expirationdate = dtExpirationdate.ToString(format); //ToString("dd/MM/yyyy");


        Thread.CurrentThread.CurrentCulture = oldculture;
        //Culture Info till here 

        string agenciaCodeAssignor;

        if (!Assignor.AssinorDigit.Equals(-1))
            agenciaCodeAssignor = string.Format("{0}/{1}-{2}", Assignor.bankaccount.Agency, Utils.FormatCode(Assignor.Code.ToString(), 6), Assignor.AssinorDigit.ToString());
        else
            agenciaCodeAssignor = agencyAccount;

        if (!FormatMeat)
            html.Append(GeneratesHtmlReceiptAssignor());
        else
        {
            html.Append(GeneratesHtmlMeat("", GeneratesHtmlReceiptAssignor()));
        }

      

        if (ShowPresentationOnAgainstMaturityDate)
            expirationdate = "Against Presentation";

        return html.ToString()
            .Replace("@CODIGOBANCO", Utils.FormatCode(_ibank.Code.ToString(), 3))
            .Replace("@DIGITOBANCO", _ibank.Digit.ToString())
            //.Replace("@URLIMAGEMBARRAINTERNA", urlImagemBarraInterna)
            //.Replace("@URLIMAGEMCORTE", urlImagemCorte)
            //.Replace("@URLIMAGEMPONTO", urlImagemPonto)
            .Replace("@URLIMAGEMLOGO", urlImagemLogo)
            .Replace("@URLIMAGEMBARRA", urlImagemBarra)
            .Replace("@LINHADIGITAVEL", Boleto.Barcode.LineDigitavel)
            .Replace("@LOCALPAGAMENTO", Boleto.LocalPayment)
            .Replace("@DATAVENCIMENTO", expirationdate)
            .Replace("@CEDENTE", Assignor.Name)
            .Replace("@DATADOCUMENTO", dtDocumentdate.ToString(format))
            .Replace("@NUMERODOCUMENTO", Boleto.Documentnumber)
            //.Replace("@ESPECIEDOCUMENTO", Documentkind.ValidateSigla(Boleto.documentkind))//Commented On 07-Dec-2010 By Pradeep Kushwaha
            .Replace("@ESPECIEDOCUMENTO", Boleto.EspecieDoc)//Added By Pradeep Kushwaha on On 07-Dec-2010 (SEGURO)
            .Replace("@DATAPROCESSAMENTO", strProcessingdate)

        #region Implementation for the Bank of Brazil



            //Variable inserted to meet specifications of the portfolio "18-019" Bank of Brazil
            //just for the token compensation.
            //Since the variable does not exist if not the wallet "18-019" was not put the [if].
            .Replace("@NOSSONUMEROBB", Boleto.Bank.Code == 1 & Boleto.wallet.Equals("18-019") ? "AI   " + Boleto.OurNumber.Substring(3) : "AI   " + string.Empty)


        #endregion Implementation for the Bank of Brazil

.Replace("@NOSSONUMERO","AI   "+ Boleto.OurNumber)
            .Replace("@CARTEIRA", (DisplayWalletCode ? string.Format("{0} - {1}", Boleto.wallet.ToString(), new Carteira_Santander(Utils.ToInt32(Boleto.wallet)).Code) : Boleto.wallet.ToString()))
            .Replace("@ESPECIE", Boleto.Especie)
            .Replace("@QUANTIDADE", (Boleto.Currencyamount == 0 ? "" : Boleto.Currencyamount.ToString()))
            .Replace("@VALORDOCUMENTO", Boleto.Currencyvalue)
            .Replace("@=VALORDOCUMENTO", (strBilletvalue) )  
           // .Replace("@=VALORDOCUMENTO", (Boleto.Billetvalue == 0 ? "" : Boleto.Billetvalue.ToString(CurrencySymbol + " ##,##0.00")))   //Boleto.Billetvalue.ToString("R$ ##,##0.00")))
            .Replace("@VALORCOBRADO", "")
            .Replace("@OUTROSACRESCIMOS", "")
            .Replace("@OUTRASDEDUCOES", "")
            .Replace("@DESCONTOS", (Boleto.Discountvalue == 0 ? "" : Boleto.Discountvalue.ToString(CurrencySymbol + " ##,##0.00"))) //Boleto.Discountvalue.ToString("R$ ##,##0.00")))
            .Replace("@AGENCIACONTA", agenciaCodeAssignor)
            .Replace("@SACADO", drawee)
            .Replace("@INFOSACADO", infoDrawee)
            .Replace("@AGENCIACODIGOCEDENTE", agenciaCodeAssignor)
            .Replace("@CPFCNPJ", Assignor.CPFCNPJ)
            .Replace("@MORAMULTA", (Boleto.Penaltyvalue == 0 ? "" : Boleto.Penaltyvalue.ToString(CurrencySymbol + " ##,##0.00"))) // Boleto.Penaltyvalue.ToString("R$ ##,##0.00")))
            .Replace("@AUTENTICACAOMECANICA", "")
            .Replace("@USODOBANCO", Boleto.Bankuse)
            .Replace("@IMAGEMCODIGOBARRA", imagemCodigoBarras);
       
    }
      
        #endregion html
   

         #region Generating the Html OffLine

        /// <summary>
        ///Function used to generate the html billet without which it is inside a Web page
        /// </summary>
        /// <param name="srcLogo">Location pointed by the image of logo.</param>
        /// <param name="srcBarra">Location pointed by the image of the bar.</param>
        /// <param name="srcCodigoBarra">Location pointed by the image of the barcode.</param>
        /// <returns>StringBuilder containing the html code of the bank.</returns>



    protected StringBuilder HtmlOffLine(string InthetextHomeEmail, string srcLogo, string srcBar, string srcBarCode)
    {//protected StringBuilder HtmlOffLine(string srcCorte, string srcLogo, string srcBarra, string srcPonto, string srcBarraInterna, string srcCodigoBarra)
        this.OnLoad(EventArgs.Empty);

        StringBuilder html = new StringBuilder();
        HtmlOfflineHeader(html);
        if (InthetextHomeEmail != null && InthetextHomeEmail != "")
        {
            html.Append(InthetextHomeEmail);
        }
        html.Append(BuildHtml(srcLogo, srcBar, "<img src=\"" + srcBarCode + "\" alt=\"Código de Barras\" />"));
        HtmlOfflineFooter(html);
        return html;
    }


    /// <summary>
    /// Monta o Header de um email com pelo menos um boleto dentro.
    /// </summary>
    /// <param name="saida">StringBuilder onde o conteudo sera salvo.</param>
    protected static void HtmlOfflineHeader(StringBuilder html)
    {
        html.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
        html.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\n");
        html.Append("<head>");
        html.Append("    <title>Boleto.NET</title>\n");

        #region Css
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BoletoNET.SlipPrinting.BoletoNet.css");

            using (StreamReader sr = new StreamReader(stream))
            {
                html.Append("<style>\n");
                html.Append(sr.ReadToEnd());
                html.Append("</style>\n");
                sr.Close();
                sr.Dispose();
            }
        }
        #endregion Css

        html.Append("     </head>\n");
        html.Append("<body>\n");
    }

    /// <summary>
    /// Footer Build an email with at least one slip inside.
    /// </summary>
    /// <param name="saida">StringBuilder onde o conteudo sera salvo.</param>
    protected static void HtmlOfflineFooter(StringBuilder saida)
    {
        saida.Append("</body>\n");
        saida.Append("</html>\n");
    }


    /// <summary>
    /// Joins several slips in a single AlternateView to all be sent together in the same email
    /// </summary>
    /// <param name="arrayDeBoletos">Array contendo os boletos a serem mesclados</param>
    /// <returns></returns>
    public static AlternateView GeneratesHtmlEmailFromVariousTicketsTo(BilletBanking[] arrayDeBoletos)
    {
        return GeneratesHtmlEmailFromVariousTicketsTo(null, arrayDeBoletos);
    }

    /// <summary>
    /// Joins several slips in a single AlternateView to be sent all together in the same email
    /// </summary>
    /// <param name="textoNoComecoDoEmail">HTML text to be added at the beginning of the email</param>
    /// <param name="arrayDeBoletos">Array containing the slips to be merged</param>
    /// <returns>AlternateView with the data of all the fetlock.</returns>
    public static AlternateView GeneratesHtmlEmailFromVariousTicketsTo(string textoNoComecoDoEmail, BilletBanking[] arrayDeBoletos)
    {
        var corpoDoEmail = new StringBuilder();

        var linkedResources = new List<LinkedResource>();
        HtmlOfflineHeader(corpoDoEmail);
        if (textoNoComecoDoEmail != null && textoNoComecoDoEmail != "")
        {
            corpoDoEmail.Append(textoNoComecoDoEmail);
        }
        foreach (var umBoleto in arrayDeBoletos)
        {
            if (umBoleto != null)
            {
                LinkedResource lrImagemLogo;
                LinkedResource lrImagemBarra;
                LinkedResource lrImagemCodigoBarra;
                umBoleto.GenerateGraphicsForEmailOffLine(out lrImagemLogo, out lrImagemBarra, out lrImagemCodigoBarra);
                var theOutput = umBoleto.BuildHtml(
                    "cid:" + lrImagemLogo.ContentId,
                    "cid:" + lrImagemBarra.ContentId,
                    "<img src=\"cid:" + lrImagemCodigoBarra.ContentId + "\" alt=\"Barcode\" />");

                corpoDoEmail.Append(theOutput);

                linkedResources.Add(lrImagemLogo);
                linkedResources.Add(lrImagemBarra);
                linkedResources.Add(lrImagemCodigoBarra);
            }
        }
        HtmlOfflineFooter(corpoDoEmail);



        AlternateView av = AlternateView.CreateAlternateViewFromString(corpoDoEmail.ToString(), Encoding.Default, "text/html");
        foreach (var theResource in linkedResources)
        {
            av.LinkedResources.Add(theResource);
        }



        return av;
    }

    /// <summary>
    ///Function used to generate AlternateView required to send a bank payment via email.
    /// </summary>
    /// <returns>AlternateView with the data of the fetlock.</returns>
    public AlternateView HtmlBoletoParaEnvioEmail()
    {
        return HtmlBoletoParaEnvioEmail(null);
    }


    /// <summary>
    /// Function used to generate AlternateView required to send a bank payment via email
    /// </summary>
    /// <param name="textoNoComecoDoEmail">Texto (em HTML) a ser incluido no começo do Email.</param>
    /// <returns>AlternateView data with billet.</returns>
    public AlternateView HtmlBoletoParaEnvioEmail(string textoNoComecoDoEmail)
    {
        LinkedResource lrImagemLogo;
        LinkedResource lrImagemBarra;
        LinkedResource lrImagemCodigoBarra;

        GenerateGraphicsForEmailOffLine(out lrImagemLogo, out lrImagemBarra, out lrImagemCodigoBarra);
        StringBuilder html = HtmlOffLine(textoNoComecoDoEmail, "cid:" + lrImagemLogo.ContentId, "cid:" + lrImagemBarra.ContentId, "cid:" + lrImagemCodigoBarra.ContentId);

        AlternateView av = AlternateView.CreateAlternateViewFromString(html.ToString(), Encoding.Default, "text/html");

        av.LinkedResources.Add(lrImagemLogo);
        av.LinkedResources.Add(lrImagemBarra);
        av.LinkedResources.Add(lrImagemCodigoBarra);
        return av;
    }


    /// <summary>
    /// Generates the three Images required for Billet
    /// </summary>
    /// <param name="lrImagemLogo">The Right Bank</param>
    /// <param name="lrImagemBarra">The Horizontal Bar</param>
    /// <param name="lrImagemCodigoBarra">The Bar Code</param>
    void GenerateGraphicsForEmailOffLine(out LinkedResource lrImagemLogo, out LinkedResource lrImagemBarra, out LinkedResource lrImagemCodigoBarra)
    {
        this.OnLoad(EventArgs.Empty);

        var randomSufix = new Random().Next().ToString(); //so we can put the same email several different slips

        Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BoletoNET.Images." + Utils.FormatCode(_ibank.Code.ToString(), 3) + ".jpg");
        lrImagemLogo = new LinkedResource(stream, MediaTypeNames.Image.Jpeg);
        lrImagemLogo.ContentId = "logo" + randomSufix;


        MemoryStream ms = new MemoryStream(Utils.ConvertImageToByte(Html.barra));
        lrImagemBarra = new LinkedResource(ms, MediaTypeNames.Image.Gif);
        lrImagemBarra.ContentId = "barra" + randomSufix; ;

        C2of5i cb = new C2of5i(Boleto.Barcode.Code, 1, 50, Boleto.Barcode.Code.Length);
        ms = new MemoryStream(Utils.ConvertImageToByte(cb.ToBitmap()));

        lrImagemCodigoBarra = new LinkedResource(ms, MediaTypeNames.Image.Gif);
        lrImagemCodigoBarra.ContentId = "codigobarra" + randomSufix; ;

    }
    /// <summary>
    /// Function used to write to a local file, the contents of the fetlock. This file can be opened in a browser without the site is in the air.
    /// </summary>
    /// <param name="fileName">Path of the file should contain the html.</param>
   
    public void GenerateHtmlIntheLocalFile(string fileName)  //MontaHtmlNoArquivoLocal
    {
        using (FileStream f = new FileStream(fileName, FileMode.Create))
        {
            StreamWriter w = new StreamWriter(f, System.Text.Encoding.Default);
            w.Write(BuildHtml());
            w.Close();
            f.Close();
        }
    }

    /// <summary>
    ///Build Html of the bank
    /// </summary>
    /// <returns>string</returns>
    public string BuildHtml()
    {
        return BuildHtml(null);
    }
    /// <summary>
    /// Monta o Html do boleto bancário
    /// </summary>
    /// <param name="fileName">Caminho do arquivo</param>
    /// <returns>Html do boleto gerado</returns>
    public string BuildHtml(string fileName)
    {
        if (fileName == null)
            fileName = System.IO.Path.GetTempPath();

        this.OnLoad(EventArgs.Empty);

        //string fnCorte = fileName + @"BoletoNetCorte.gif";
        //if (!System.IO.File.Exists(fnCorte))
        //    Html.corte.Save(fnCorte);

        string fnLogo = fileName + @"BoletoNET" + Utils.FormatCode(_ibank.Code.ToString(), 3) + ".jpg";

        if (!System.IO.File.Exists(fnLogo))
            Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Boleto.NET.Images." + Utils.FormatCode(_ibank.Code.ToString(), 3) + ".jpg")).Save(fnLogo);

        //string fnPonto = fileName + @"BoletoNetPonto.gif";
        //if (!System.IO.File.Exists(fnPonto))
        //    Html.ponto.Save(fnPonto);

        //string fnBarraInterna = fileName + @"BoletoNetBarraInterna.gif";
        //if (!File.Exists(fnBarraInterna))
        //    Html.barrainterna.Save(fnBarraInterna);

        string fnBarra = fileName + @"BoletoNetBarra.gif";
        if (!File.Exists(fnBarra))
            Html.barra.Save(fnBarra);

        string fnCodigoBarras = System.IO.Path.GetTempFileName();
        C2of5i cb = new C2of5i(Boleto.Barcode.Code, 1, 50, Boleto.Barcode.Code.Length);
        cb.ToBitmap().Save(fnCodigoBarras);

        //return HtmlOffLine(fnCorte, fnLogo, fnBarra, fnPonto, fnBarraInterna, fnCodigoBarras).ToString();
        return HtmlOffLine(null, fnLogo, fnBarra, fnCodigoBarras).ToString();
    }


        #endregion Generating the Html OffLine


    }
}


