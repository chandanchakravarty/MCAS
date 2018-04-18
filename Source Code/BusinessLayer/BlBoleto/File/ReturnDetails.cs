using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public class ReturnDetails
    {

        #region Variables

        private int _CodeInscription = 0;
        private string _NumberInscription = string.Empty;
        private int _Account = 0;
        private int _Bankcode = 0;
        private int _DacAccount = 0;
        private string _UseCompany = string.Empty;
        private int _DacOurNumber = 0;
        private string _Portfolio = string.Empty;
        private int _CodeOccurrence = 0;
        private int _ConfirmationOurNumber = 0;
        private double _TitleValue = 0;
        private int _Collectingagencies = 0;
        private int _Daccollectingagencies = 0;
        private int _Species = 0;
        private double _Farecollection = 0;
        private double _ValueAbatement = 0;
        private double _HomeValue = 0;
        private double _InterestMora = 0;
        private DateTime _Creditdate = new DateTime(1, 1, 1);
        private int _InstructionCancelled = 0;
        private string _NameDrawee = string.Empty;
        private string _Errors = string.Empty;
        private string _CodeSettlement = string.Empty;
        private int _Sequentialnumber = 0;
        private string _Record = string.Empty;
        private double _ValueExpense = 0;
        private double _ValueOtherExpenses = 0;
        private string _Paymentsource = string.Empty;
        private string _ReasonOccurrenceCode = string.Empty;
        private string _IdentificationTitle = string.Empty;
        private string _Rejectionreasons = string.Empty;
        private int _NumberCartorio = 0;
        private string _ProtocolIssue = string.Empty;
        private string _Controlnumber = string.Empty;

        #region Properties BRB

        private int _RecordID = 0;
        private int _TypeofRegistration = 0;
        private string _cgcCpf = string.Empty;
        private int _Currentaccount = 0;
        private string _OurNumber = string.Empty;
        private string _YourNumber = string.Empty;
        private int _Instruction = 0;
        private DateTime _Occurrencedate = new DateTime(1, 1, 1);
        private string _Documentnumber = string.Empty;
        private int _CodeApportionment = 0;
        private DateTime _Expirationdatedate = new DateTime(1, 1, 1);
        private double _ValTitle = 0;
        private int _Thebanktaxman = 0;
        private int _Agency = 0;
        private string _SpeciesTitle = string.Empty;
        private double _DespeasaCollection = 0;
        private double _OtherExpenses = 0;
        private double _Interest = 0;
        private double _iof = 0;
        private double _Rebates = 0;
        private double _Discounts = 0;
        private double _ValuePaid = 0;
        private double _Otherdebts = 0;
        private double _OtherCredits = 0;
        private DateTime _Settlementdate = new DateTime(1, 1, 1);
        private int _Sequential = 0;

        #endregion

        #endregion


        #region RegionDetails

        #region Construtores

        public ReturnDetails()
        {
        }

        public ReturnDetails(string Record)
        {
            _Record = Record;
        }

        #endregion

        #region Propriedades

        public int CodeInscription
        {
            get { return _CodeInscription; }
            set { _CodeInscription = value; }
        }

        public string NumberInscription
        {
            get { return _NumberInscription; }
            set { _NumberInscription = value; }
        }

        public int Agency
        {
            get { return _Agency; }
            set { _Agency = value; }
        }

        public int Account
        {
            get { return _Account; }
            set { _Account = value; }
        }

        public int DacAccount
        {
            get { return _DacAccount; }
            set { _DacAccount = value; }
        }

        public string UseCompany
        {
            get { return _UseCompany; }
            set { _UseCompany = value; }
        }

        public string OurNumber
        {
            get { return _OurNumber; }
            set { _OurNumber = value; }
        }

        public int DacOurNumber
        {
            get { return _DacOurNumber; }
            set { _DacOurNumber = value; }
        }

        public string Portfolio
        {
            get { return _Portfolio; }
            set { _Portfolio = value; }
        }

        public int CodeOccurrence
        {
            get { return _CodeOccurrence; }
            set { _CodeOccurrence = value; }
        }

        public DateTime Occurrencedate
        {
            get { return _Occurrencedate; }
            set { _Occurrencedate = value; }
        }

        public string Documentnumber
        {
            get { return _Documentnumber; }
            set { _Documentnumber = value; }
        }

        public int ConfirmationOurNumber
        {
            get { return _ConfirmationOurNumber; }
            set { _ConfirmationOurNumber = value; }
        }

        public DateTime Expirationdatedate
        {
            get { return _Expirationdatedate; }
            set { _Expirationdatedate = value; }
        }

        public double TitleValue
        {
            get { return _TitleValue; }
            set { _TitleValue = value; }
        }

        public int Bankcode
        {
            get { return _Bankcode; }
            set { _Bankcode = value; }
        }

        public int Collectingagencies
        {
            get { return _Collectingagencies; }
            set { _Collectingagencies = value; }
        }

        public int Daccollectingagencies
        {
            get { return _Daccollectingagencies; }
            set { _Daccollectingagencies = value; }
        }

        public int Species
        {
            get { return _Species; }
            set { _Species = value; }
        }

        public double Farecollection
        {
            get { return _Farecollection; }
            set { _Farecollection = value; }
        }

        public double IOF
        {
            get { return _iof; }
            set { _iof = value; }
        }

        public double ValueAbatement
        {
            get { return _ValueAbatement; }
            set { _ValueAbatement = value; }
        }

        public double Discounts
        {
            get { return _Discounts; }
            set { _Discounts = value; }
        }

        public double HomeValue
        {
            get { return _HomeValue; }
            set { _HomeValue = value; }
        }

        public double InterestMora
        {
            get { return _InterestMora; }
            set { _InterestMora = value; }
        }

        public double OtherCredits
        {
            get { return _OtherCredits; }
            set { _OtherCredits = value; }
        }

        public double Otherdebts
        {
            get { return _Otherdebts; }
            set { _Otherdebts = value; }
        }

        public DateTime Creditdate
        {
            get { return _Creditdate; }
            set { _Creditdate = value; }
        }

        public int InstructionCancelled
        {
            get { return _InstructionCancelled; }
            set { _InstructionCancelled = value; }
        }

        public string NameDrawee
        {
            get { return _NameDrawee; }
            set { _NameDrawee = value; }
        }

        public string Errors
        {
            get { return _Errors; }
            set { _Errors = value; }
        }

        public string CodeSettlement
        {
            get { return _CodeSettlement; }
            set { _CodeSettlement = value; }
        }

        public int Sequentialnumber
        {
            get { return _Sequentialnumber; }
            set { _Sequentialnumber = value; }
        }

        public string Record
        {
            get { return _Record; }
        }
        public double ValueExpense
        {
            get { return _ValueExpense; }
            set { _ValueExpense = value; }
        }
        public double ValueOtherExpenses
        {
            get { return _ValueOtherExpenses; }
            set { _ValueOtherExpenses = value; }
        }
        public double ValuePaid
        {
            get { return _ValuePaid; }
            set { _ValuePaid = value; }
        }
        public string ReasonOccurrenceCode
        {
            get { return _ReasonOccurrenceCode; }
            set { _ReasonOccurrenceCode = value; }
        }
        public string Paymentsource
        {
            get { return _Paymentsource; }
            set { _Paymentsource = value; }
        }
        public string IdentificationTitle
        {
            get { return _IdentificationTitle; }
            set { _IdentificationTitle = value; }
        }
        public string Rejectionreasons
        {
            get { return _Rejectionreasons; }
            set { _Rejectionreasons = value; }
        }
        public string ProtocolIssue
        {
            get { return _ProtocolIssue; }
            set { _ProtocolIssue = value; }
        }
        public int NumberCartorio
        {
            get { return _NumberCartorio; }
            set { _NumberCartorio = value; }
        }
        public string Controlnumber
        {
            get { return _Controlnumber; }
            set { _Controlnumber = value; }
        }

        public int RecordID
        {
            get { return _RecordID; }
            set { _RecordID = value; }
        }

        public int TypeofRegistration
        {
            get { return _TypeofRegistration; }
            set { _TypeofRegistration = value; }
        }

        public string CgcCpf
        {
            get { return _cgcCpf; }
            set { _cgcCpf = value; }
        }

        public int Currentaccount
        {
            get { return _Currentaccount; }
            set { _Currentaccount = value; }
        }

        public string YourNumber
        {
            get { return _YourNumber; }
            set { _YourNumber = value; }
        }

        public int Instruction
        {
            get { return _Instruction; }
            set { _Instruction = value; }
        }

        public int CodeApportionment
        {
            get { return _CodeApportionment; }
            set { _CodeApportionment = value; }
        }

        public double ValTitle
        {
            get { return _ValTitle; }
            set { _ValTitle = value; }
        }

        public int Thebanktaxman
        {
            get { return _Thebanktaxman; }
            set { _Thebanktaxman = value; }
        }

        public string SpeciesTitle
        {
            get { return _SpeciesTitle; }
            set { _SpeciesTitle = value; }
        }

        public double DespeasaCollection
        {
            get { return _DespeasaCollection; }
            set { _DespeasaCollection = value; }
        }

        public double OtherExpenses
        {
            get { return _OtherExpenses; }
            set { _OtherExpenses = value; }
        }

        public double Interest
        {
            get { return _Interest; }
            set { _Interest = value; }
        }

        public double Rebates
        {
            get { return _Rebates; }
            set { _Rebates = value; }
        }

        public DateTime Settlementdate
        {
            get { return _Settlementdate; }
            set { _Settlementdate = value; }
        }

        public int Sequencial
        {
            get { return _Sequential; }
            set { _Sequential = value; }
        }

        #endregion

        #region Instance methods

        public static string PrimeiroCaracter(string returns)
        {
            try
            {
                return returns.Substring(0, 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Error when dismembering record.", ex);
            }
        }

        #endregion


    #endregion RegionDetails
    }
        
}
