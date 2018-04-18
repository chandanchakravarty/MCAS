using System;
using System.ComponentModel;
using System.Collections.Generic;


namespace Cms.BusinessLayer.BlBoleto
{
    [Serializable(), Browsable(false)]

   public class Drawee
    {
        #region Variables

        private string _cpfcnpj = string.Empty;
        private string _name = string.Empty;
        private Address _Address = new Address();
        private DraweeInformation _info = new DraweeInformation();
        //Flavio (fhlviana@hotmail.com) - list of all information to be displayed below the name of the drawee

        #endregion

         # region Constructors

        public Drawee()
        {
        }

        public Drawee(string name)//Flavio(fhlviana@hotmail.com) - tem boleto que o sacado nao apresenta o CPF, sendo assim, adicionei a possibilidade
        {
            _name = name;
        }

        public Drawee(string cpfcnpj, string name)
        {
            CPFCNPJ = cpfcnpj;
            _name = name;
        }

        public Drawee(string cpfcnpj, string name, Address address)
        {
            CPFCNPJ = cpfcnpj;
            _name = name;
            Address = address;
        }

        # endregion

        #region Properties
       
        /// <summary>
        /// Returns the address of the drawee.
        /// </summary>
        public Address Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }
           /// <summary>
        /// Returns the information drawn
        /// </summary>
        public DraweeInformation DraweeInformation
        {
            get { return _info; }
        }

        /// <summary>
        /// Retorna CPF ou CNPJ
        /// </summary>
        public string CPFCNPJ
        {
            get
            {                   
                //return _cpfcnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                return _cpfcnpj;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Error. CPF / CNPJ customer can not be null.");
                }
                string o = value.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
                
                //if (o == null || (o.Length != 11 && o.Length != 14))
                //    throw new ArgumentException("O CPF/CNPJ inválido. Utilize 11 dígitos para CPF ou 14 para CPNJ.");
                if (o == null || o == string.Empty)//Flavio (fhlviana@hotmail.com) - due to the addition of the possibility of the fetlock does not provide Tax ID rendering
                    _cpfcnpj = string.Empty;
                else if (o.Length != 11 && o.Length != 14)
                    throw new ArgumentException("CPF / CNPJ invalid. Use 11 or 14 digits for CPF to CPNJ.");

                //this._cpfcnpj = value;
                _cpfcnpj = o;   
                // Flavio (fhlviana@hotmail.com) - if there is a set of functions in "Utils" class to generate the CPF
                //and CNPJ, and the same are already used in rendering, no need to store. "", "-"
                //and "/" them and every time the method "get" the property is required to have these same
                //be removed by the method "Replace". Thus the three "Replace" threads are only executed at once.
            }
        }

        /// <summary>
        /// Drawee Name 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
            }
        }

        #endregion Properties




    }
}
