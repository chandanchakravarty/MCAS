using System;
using System.Collections.Generic;

namespace Cms.BusinessLayer.BlBoleto
{
    public class Boletos : List<Boleto>
    {

        # region Varibles

        private Bank _bank;
        private BankAccount _BankAccount;
        private Assginor _Assginor;

        # endregion

        # region Properties

        public Bank Bank
        {
            get { return _bank; }
            set { this._bank = value; }
        }

        public BankAccount BankAccount
        {
            get { return _BankAccount; }
            set { this._BankAccount = value; }
        }

        public Assginor Assginor
        {
            get { return _Assginor; }
            set { this._Assginor = value; }
        }

        # endregion

        # region Methods

        /// <summary>
        /// Checks if the file already exists on the shipment, if there is a file is created. "Rem".
        /// 
        /// The default files and Return Shipping, obeys the rules established by CNAB (National Center
        /// Banking Automation) and should be written containing:
        /// Header record: First record of the file containing the identification of the business
        /// Detail Record: Record containing information of Payments:
        /// - Inclusion of commitments
        /// - Modification of Commitments
        /// - Payments made
        /// - Lock / unlock
        /// Trailer Record: Last record indicating completion of the Archive
        /// 0D = 0A mandatory character (Final Record) 0D 0A 1A (Final Archive)
        /// </summary>

        private new void Add(Boleto item)
        {
            if (item.Bank == null)
                throw new Exception("Billet does not have Bank.");

            if (item.BankAccount == null)
                throw new Exception("Billet does not have a bank account.");

            if (item.Assginor == null)
                throw new Exception("Billet does not have any vendor.");

            item.Validates();
            this.Add(item);
        }

        # endregion

    }
}
