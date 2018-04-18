namespace Cms.BusinessLayer.BlBoleto
{
    public class Address
    {

        private string _Street;
        private string _Address;
        private string _Number;
        private string _Completion;
        private string _District;
        private string _City;
        private string _uf;
        private string _cep;
        private string _email;
        private string _State;


        /// <summary>
        /// Retorna o Logradouro
        /// Exemplo : Rua, Av., Travessa...
        /// </summary>        
        public string Street
        {
            get
            {
                return _Street;
            }
            set
            {
                this._Street = value;
            }
        }

        /// <summary>
        /// Returns the address
        /// </summary>
        public string address
        {
            get
            {
                return _Address;
            }
            set
            {
                this._Address = value;
            }
        }

        /// <summary>
        /// Retorna o Número do endereço
        /// </summary>
        public string Number
        {
            get
            {
                return _Number;
            }
            set
            {
                this._Number = value;
            }
        }

        /// <summary>
        /// Retorna o complemento
        /// </summary>
        public string Completion
        {
            get
            {
                return _Completion;
            }
            set
            {
                this._Completion = value;
            }
        }

        /// <summary>
        /// Return District
        /// </summary>
        public string District
        {
            get
            {
                return _District;
            }
            set
            {
                this._District = value;
            }
        }

        /// <summary>
        /// Return City
        /// </summary>
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                this._City = value;
            }
        }

        /// <summary>
        /// Retorna o UF
        /// Exemplo :
        /// SP - São Paulo
        /// SC - Santa Catarina
        /// </summary>
        public string UF
        {
            get
            {
                return _uf;
            }
            set
            {
                this._uf = value;
            }
        }

        /// <summary>
        /// Retorna o número do CEP
        /// </summary>
        public string CEP
        {
            get
            {
                //return _cep.Replace(".", "").Replace("-", "");
                return _cep;
            }
            //Flavio (fhlviana@hotmail.com) - the method "Set" comes less times than get the estimate. Therefore, store
            //sem o "." e o "-" faz com que o código tenda a executar os dois Replace uma vez só.
            set
            {
                //this._cep = value;
                this._cep = value.Replace(".", "").Replace("-", "");
            }
        }
        /// <summary>
        /// State
        /// </summary>
        public string State 
        { 
            get { return _State ; }
            set{ this._State = value;}
        }
        /// <summary>
        /// Retorna o E-Mail
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                this._email = value;
            }
        }
    }
}
