using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
    public class TSegmentDetailReturnCNAB240
    {

        #region Variables

        private int _Bankcode;
        private int _IdCodeMovement;
       // private CodeMotion _codeMotion;
        private int _agency;
        private string _agencydigit;
        private long _Account;
        private string _DigitAccount;
        private int _DacAgAccount;
        private string _OurNumber; //identification of the title in the bank
        private int _Walletcode;
        private string _Documentnumber; //number used by the client to identify the title
        private DateTime _Expirationdate;
        private double _TitleValue;
        private string _IdentificationTitleCompany;
        private int _TypeofRegistration;
        private string _numberInscricao;
        private string _nameDrawee;
        private double _ValueRates;
        private int _sourcerejection;
        private string _record;

        private List<TSegmentDetailReturnCNAB240> _ListDetail = new List<TSegmentDetailReturnCNAB240>();

        #endregion

        #region Constructors

        public TSegmentDetailReturnCNAB240()
		{
        }

        public TSegmentDetailReturnCNAB240(string record)
        {
            _record = record;
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        #region Properties

        public int IdCodeMovement
        {
            get { return _IdCodeMovement; }
            set { _IdCodeMovement = value; }
        }

        public int Bankcode
        {
            get { return _Bankcode; }
            set { _Bankcode = value; }
        }

        public string Record
        {
            get { return _record; }            
        }

        //public CodeMotion codeMotion
        //{
        //    get 
        //    {
        //        _codeMotion = new CodeMotion(_Bankcode, _IdCodeMovement);
        //        return _codeMotion;
        //    }
        //    set 
        //    {
        //        _codeMotion = value;
        //        _IdCodeMovement = _codeMotion.Code;
        //    }
        //}

        public int Agency
        {
            get { return _agency; }
            set { _agency = value; }
        }

        public string Agencydigit
        {
            get { return _agencydigit; }
            set { _agencydigit = value; }
        }

        public long Account
        {
            get { return _Account; }
            set { _Account = value; }
        }

        public string DigitAccount
        {
            get { return _DigitAccount; }
            set { _DigitAccount = value; }
        }

        public int DacAgAccount
        {
            get { return _DacAgAccount; }
            set { _DacAgAccount = value; }
        }

        public string OurNumber
        {
            get { return _OurNumber; }
            set { _OurNumber = value; }
        }

        public int Walletcode
        {
            get { return _Walletcode; }
            set { _Walletcode = value; }
        }

        public string Documentnumber
        {
            get { return _Documentnumber; }
            set { _Documentnumber = value; }
        }

        public DateTime Expirationdate
        {
            get { return _Expirationdate; }
            set { _Expirationdate = value; }
        }

        public double TitleValue
        {
            get { return _TitleValue; }
            set { _TitleValue = value; }
        }

        public string IdentificationTitleCompany
        {
            get { return _IdentificationTitleCompany; }
            set { _IdentificationTitleCompany = value; }
        }

        public int TypeofRegistration
        {
            get { return _TypeofRegistration; }
            set { _TypeofRegistration = value; }
        }

        public string NumberInscricao
        {
            get { return _numberInscricao; }
            set { _numberInscricao = value; }
        }

        public string NameDrawee
        {
            get { return _nameDrawee; }
            set { _nameDrawee = value; }
        }

        public double ValueRates
        {
            get { return _ValueRates; }
            set { _ValueRates = value; }
        }

        public int sourcerejection
        {
            get { return _sourcerejection; }
            set { _sourcerejection = value; }
        }

        public List<TSegmentDetailReturnCNAB240> ListDetail

        {
            get { return _ListDetail; }
            set { _ListDetail = value; }
        }

        #endregion
    }
}
