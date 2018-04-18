using System;
using System.ComponentModel;

namespace Cms.BusinessLayer.BlBoleto
{
    [Serializable(), Browsable(false)]

    public class Assginor
    {
        #region Variables

        private int _Code = 0;
        private string _cpfcnpj;
        private string _name;
        private BankAccount _BankAccount;
        private int _convenio = 0;
        private int _AssignorDigit = -1;

        #endregion Variables

   

        public Assginor()
        {

        }
        public Assginor(BankAccount bankaccount)
        {
            _BankAccount = bankaccount;
        }

        public Assginor(string cpfcnpj, string name)
        {
            CPFCNPJ = cpfcnpj;
            _name = name;
        }
        public Assginor(string cpfcnpj, string name, string agency, string Agencydigit, string account, string accountdigit)
        {
            CPFCNPJ = cpfcnpj;
            _name = name;
            _BankAccount = new BankAccount();
            _BankAccount.Agency = agency;
            _BankAccount.AgencyDigit = Agencydigit;
            _BankAccount.Account = account;
            _BankAccount.AccountDigit = accountdigit;
        }
        //Added by Pradeep Kushwaha on 01-10-2010  for Brazil Bank
        public Assginor(string cpfcnpj, string name, string agency, string Agencydigit, string account, int Agreement_Number,String AccountNumber)
        {
            CPFCNPJ = cpfcnpj;
            _name = name;
            Convenio = Agreement_Number; 
            _BankAccount = new BankAccount();
            _BankAccount.Agency = agency;
            _BankAccount.AgencyDigit = Agencydigit;
            _BankAccount.Account = account;
            _BankAccount.AccountNumber = AccountNumber;
        }


        public Assginor(string cpfcnpj, string name, string agency, string account, string accountdigit)
        {
            CPFCNPJ = cpfcnpj;
            _name = name;

            _BankAccount = new BankAccount();
            _BankAccount.Agency = agency;
            _BankAccount.Account = account;
            _BankAccount.AccountDigit = accountdigit;
        }

        public Assginor(string cpfcnpj, string name, string agency, string account)
        {
            CPFCNPJ = cpfcnpj;
            _name = name;

            _BankAccount = new BankAccount();
            _BankAccount.Agency = agency;
            _BankAccount.Account = account;
        }

        #region Properties
        /// <summary>
        /// Assignor Code
        /// </summary>
        public int Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        public int AssinorDigit
        {
            get
            {
                return _AssignorDigit;
            }
            set
            {
                _AssignorDigit = value;
            }

        }

        /// <summary>
        /// Retona o CPF ou CNPJ do Cedente
        /// </summary>
        public string CPFCNPJ
        {
            get
            {
                return _cpfcnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            }
            set
            {
                string o = value.Replace(".", "").Replace("-", "").Replace("/", "");
                if (o == null || (o.Length != 11 && o.Length != 14))
                    throw new ArgumentException("CPF / CNPJ invalid. Use 11 or 14 digits for CPF to CPNJ.");

                _cpfcnpj = value;
            }
        }

        /// <summary>
        /// Name of Assignor
        /// </summary>
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Current account of the Transferor
        /// </summary>
        public BankAccount bankaccount
        {
            get
            {
                return _BankAccount;
            }
            set
            {
                _BankAccount = value;
            }
        }

        /// <summary>
        /// Number Agreement
        /// </summary>
        public int Convenio
        {
            get
            {
                return _convenio;
            }
            set
            {
                _convenio = value;
            }
        }
        #endregion Propertites


 }


}
