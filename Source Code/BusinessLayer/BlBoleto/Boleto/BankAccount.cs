using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cms.BusinessLayer.BlBoleto
{
   public class BankAccount
    {
        #region Variables
       
        private string _Agency = string.Empty;
        private string _Agencydigit = string.Empty;
        private string _Account = string.Empty;
        private string _AccountDigit = string.Empty;
        private string _AccountNumber = string.Empty;//Added By Pradeep Kushwaha 
        #endregion Variables

        public BankAccount()
        {
        }

        public BankAccount(string agency, string account)
        {
            _Agency = agency;
            _Account = account;
        }

        public BankAccount(string agency, string Agencydigit, string account, string Accountdigit)
        {
            _Agency = agency;
            _Agencydigit = Agencydigit;
            _Account = account;
            _AccountDigit = Accountdigit;
        }

        #region Properties
        /// <summary>
        /// Returns the number of the agency.
        /// Complete with leading zeros when necessary
        /// </summary>
        public string Agency
        {
            get
            {
                return _Agency;
            }

            set
            {
                _Agency = value;
            }
        }

        /// <summary>
        /// Agency Digit
        /// </summary>
        public string AgencyDigit
        {
            get
            {
                return _Agencydigit;
            }
            set
            {
                _Agencydigit = value;
            }
        }

        /// <summary>
        /// Current Account Number
        /// </summary>
        public string Account
        {
            get
            {
                return _Account;
            }
            set
            {
                _Account = value;
            }
        }
        /// <summary>
        /// Current Account Number
        /// </summary>
        //Added By Pradeep Kushwaha on 07-Dec-2010
        public string AccountNumber
        {
            get
            {
                return _AccountNumber;
            }
            set
            {
                _AccountNumber = value;
            }
        }
        /// <summary>
        /// Digit Current Account
        /// </summary>
        public string AccountDigit
        {
            get
            {
                return _AccountDigit;
            }
            set
            {
                _AccountDigit = value;
            }
        }
        #endregion Properties











    }

}
