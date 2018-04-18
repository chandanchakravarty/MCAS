using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.VisualBasic;


namespace Cms.BusinessLayer.BlBoleto
{

    [Serializable(), Browsable(false)]
    public class Boleto
    {
        #region Variables

        private string _wallet = "";
        private string _OurNumber = "";
        private DateTime _Expirationdate;
        private DateTime _Documentdate;
        private DateTime _Processingdate;
        private double _Billetvalue = 0;
        private string _LocalPayment = "";//"To maturity, preferably in ";  //Commented by Pradeep Kushwaha on 25-Feb-2011 itrack 685
        private int _Currencyamount = 1;
        private string _Currencyvalue = "";
        private List<IInstructions> _instructions = new List<IInstructions>();
        private IDocumentkind _Documentkind = new Documentkind();
        private string _acceptance = "";//"N";
        private string _Documentnumber = "";
        private string _especie = "R$";
        private String _especieDoc = String.Empty;
        private int _currency = 9;
        private string _Bankuse = string.Empty;
        private BarCode _barcode = new BarCode();
        private Assginor _Assginor;
        private int _category = 0;
        //private string _instrucoesHtml = string.Empty;
        private IBank _bank;
        private BankAccount _BankAccount = new BankAccount();
        private double _Discountvalue;
        private Drawee _Drawee;

        private double _interestMora;
        private double _iof;
        private double _rebate;
        private double _Penaltyvalue;
        private double _OtherAdditions;
        private double _OtherDiscounts;
        private DateTime _InterestdateMora;
        private DateTime _Fineday;
        private DateTime _DiscountDate;
        private DateTime _DateOtherAdditions;
        private DateTime _DateOtherDiscounts;

        private string _modaltypedae = string.Empty;
        #endregion 

        #region Construtor

        public Boleto()
        {
        }


         public Boleto(DateTime Expirationdate, double valorBoleto, string wallet, string OurNumber,Assginor assginor, Documentkind documentkind)
        {
            _wallet = wallet;
            _OurNumber = OurNumber;
            _Expirationdate = Expirationdate;
            _Billetvalue = valorBoleto;
            _Billetvalue = valorBoleto;

            _Assginor = assginor;

            _Documentkind = documentkind;
        }

          public Boleto(double Billetvalue, string wallet, string OurNumber, Assginor assginor)
        {
            _wallet = wallet;
            _OurNumber = OurNumber;
            _Billetvalue = Billetvalue;
            //_Billetvalue = valorBoleto;

            _Assginor = assginor;
        }

        public Boleto(DateTime Expirationdate, double Billetvalue, string wallet, string OurNumber, Assginor assginor)
        {
            _wallet = wallet;
            _OurNumber = OurNumber;
            _Expirationdate = Expirationdate;
            _Billetvalue = Billetvalue;
            _Billetvalue = Billetvalue;
            _Assginor = assginor;
            
        }
       
        public Boleto(DateTime Expirationdate, double Billetvalue, string wallet, string OurNumber, string agency, string account)
        {
            _wallet = wallet;
            _OurNumber = OurNumber;
            _Expirationdate = Expirationdate;
            _Billetvalue = Billetvalue;
            _Billetvalue = Billetvalue;

            _Assginor = new Assginor(new BankAccount(agency, account));
        }
   


        #endregion Construtor

        #region Properties
        /// <summary> 
        /// Return to Category billet
        /// </summary>
        public int Category
        {
            get { return _category; }
            set { this._category = value; }
        }

        /// <summary> 
        /// Returns the number of the portfolio.
        /// </summary>
        public string wallet
        {
            get { return _wallet; }
            set { this._wallet = value; }
        }

        /// <summary> 
        /// Returns the expiration date of the title.
        /// </summary>
        public DateTime Expirationdate
        {
            get { return _Expirationdate; }
            set { this._Expirationdate = value; }
        }

        /// <summary> 
        /// Returns the title.
        /// </summary>
        public double Billetvalue
        {
            get { return _Billetvalue; }
            set { _Billetvalue = value; }
        }

        /// <summary> 
        /// Returns the field for the first line of instruction.
        /// </summary>
        public List<IInstructions> Instructions
        {
            get { return _instructions; }
            set { _instructions = value; }
        }


        /// <summary> 
        /// Returns the place of payment.
        /// </summary>
        public string LocalPayment
        {
            get { return _LocalPayment; }
            set { this._LocalPayment = value; }
        }

        /// <summary> 
        /// Returns the amount of currency.
        /// </summary>
        public int Currencyamount
        {
            get { return _Currencyamount; }
            set { _Currencyamount = value; }
        }

        /// <summary> 
        /// Return Currency value
        /// </summary>
        public String Currencyvalue
        {
            get { return _Currencyvalue; }
            set { this._Currencyvalue = value; }
        }

        /// <summary> 
        /// Returns the field accepted that by default comes with N.
        /// </summary>
        public String Acceptance
        {
            get { return _acceptance; }
            set { _acceptance = value; }
        }

        /// <summary> 
        /// Returns the Especie (INSURANCE)
        /// </summary>
        public string Especie
        {
            get { return _especie; }
            set { _especie = value; }
        }
        public string EspecieDoc
        {
            get { return _especieDoc; }
            set { _especieDoc = value; }
        }

        /// <summary> 
        ///Returns the kind of document that comes standard with DV
        /// </summary>
        public IDocumentkind documentkind 
        {
            get
            {
                if (_Documentkind == null)
                    _Documentkind = new Documentkind();
                return _Documentkind;
            }
            set { _Documentkind = value; }
        }

        /// <summary> 
        /// Returns the date the document.
        /// </summary>        
        public DateTime Documentdate
        {
            get { return _Documentdate; }
            set { _Documentdate = value; }
        }

        /// <summary> 
        /// Returns the date of Processingdate
        /// </summary>        
        public DateTime Processingdate
        {
            get { return _Processingdate; }
            set { _Processingdate = value; }
        }


        /// <summary> 
        ///Retrieves the document number
        /// </summary>        
        public string Documentnumber
        {
            get { return _Documentnumber; }
            set { _Documentnumber = value; }
        }
        
        /// <summary> 
        /// Retrieves our number
        /// </summary>        
        public string OurNumber
        {
            get { return _OurNumber; }
            set { _OurNumber = value; }
        }


        /// <summary> 
        ///Retrieves the value of money
        /// </summary>  
        public int currency
        {
            get { return _currency; }
            set { _currency = value; }
        }


        public Assginor Assginor
        {
            get { return _Assginor; }
            set { _Assginor = value; }
        }


        public BarCode Barcode
        {
            get { return _barcode; }
        }


        public IBank Bank
        {
            get { return _bank; }
            set { _bank = value; }
        }

        public BankAccount BankAccount
        {
            get { return _BankAccount; }
            set { _BankAccount = value; }
        }
       
        /// <summary> 
        /// Returns the discount value of the title.
        /// </summary>
        public double Discountvalue
        {
            get { return _Discountvalue; }
            set { _Discountvalue = value; }
        }

        /// <summary>
        /// Retorna do Sacado
        /// </summary>
        public Drawee Drawee
        {
            get { return _Drawee; }
            set { _Drawee = value; }
        }

        /// <summary> 
        /// Retruns To use of bank
        /// </summary>        
        public string Bankuse
        {
            get { return _Bankuse; }
            set { _Bankuse = value; }
        }

        /// <summary> 
        /// Default interest
        /// </summary>  
        public double interestMora
        {
            get { return _interestMora; }
            set { _interestMora = value; }
        }


        /// <summary> 
        /// IOF
        /// </summary>  
        public double IOF
        {
            get { return _iof; }
            set { _iof = value; }
        }


        /// <summary> 
        /// Returns Rebate
        /// </summary>  
        public double rebate
        {
            get { return _rebate; }
            set { _rebate = value; }
        }

        /// <summary> 
        /// Returns Amount of Fine
        /// </summary>  
        public double Penaltyvalue
        {
            get { return _Penaltyvalue; }
            set { _Penaltyvalue = value; }
        }


        /// <summary> 
        /// returns Outros Acréscimos(Other Accruals)
        /// </summary>  
        public double OtherAdditions
        {
            get { return _OtherAdditions; }
            set { _OtherAdditions = value; }
        }

        /// <summary> 
        /// Returns Other discounts
        /// </summary>  
        public double OtherDiscounts
        {
            get { return _OtherDiscounts; }
            set { _OtherDiscounts = value; }
        }


        /// <summary> 
        /// Returns Date of Interest on Delayed (Data do Juros de Mora)
        /// </summary>  
        public DateTime InterestdateMora
        {
            get { return _InterestdateMora; }
            set { _InterestdateMora = value; }
        }


        /// <summary> 
        /// Returns Date of Interest Penalty (Data do Juros da Multa)
        /// </summary>  
        public DateTime Fineday
        {
            get { return _Fineday; }
            set { _Fineday = value; }
        }


        /// <summary> 
        /// Returns Date of Interest Discount (Data do Juros do Desconto)
        /// </summary>  
        public DateTime DiscountDate
        {
            get { return _DiscountDate; }
            set { _DiscountDate = value; }
        }

        /// <summary> 
        /// Returns Date Other Accruals (Data de Outros Acréscimos)
        /// </summary>  
        public DateTime DateOtherAdditions
        {
            get { return _DateOtherAdditions; }
            set { _DateOtherAdditions = value; }
        }

        /// <summary> 
        /// Returns Date of Other Discounts (Data de Outros Descontos)
        /// </summary>  
        public DateTime DateOtherDiscounts
        {
            get { return _DateOtherDiscounts; }
            set { _DateOtherDiscounts = value; }
        }

        /// <summary> 
        /// Returns Returns the type of mode (Retorna o tipo da modalidade)
        /// </summary>
        public String modaltypedae
        {
            get { return _modaltypedae; }
            set { this._modaltypedae = value; }
        }
       
        
        #endregion Properties


        public void Validates()
        {
            //Basic validations, if it has implemented in the class of bank.ValidaBoleto ()
            if (this.Assginor == null)
                throw new Exception("Assignor not registered.");

                // Assign the database name to the place of payment
                 // Commented for duplicity in the name of the bank
                 // This.LocalPagamento this.Banco.Nome + + = "";

            // Check if data processing is valid
            if (this.Processingdate.ToString("dd/MM/yyyy") == "01/01/0001")
                this.Processingdate = DateTime.Now;

            //Verifica se data do documento é valida
            if (this.Documentdate.ToString("dd/MM/yyyy") == "01/01/0001")
                this.Documentdate = DateTime.Now;

            this.Bank.ValidateBoleto(this);
        }

        public void FormattingFields()
        {
            try
            {
                this.Currencyamount = 0;
                this.Bank.BarCodeFormats(this);
                this.Bank.FormatLineDigitavel(this);
                this.Bank.OurNumberFormats(this);
            }
            catch (Exception ex)
            {
                throw new Exception("Error during the formatting of the fields.", ex);
            }
        }


    }

}
