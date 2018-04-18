
/******************************************************************************************
<Author				: - Pradeep Kushwaha
<Start Date			: -	22-03-2010
<End Date			: -	
<Description		: - 
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 06 April 2010
<Modified By		: - Pravesh  Chandel
<Purpose			: - Review and optimization 
*******************************************************************************************/



using System;
using System.Text;

namespace Cms.EbixDataTypes
{
    
    /// <summary>
    /// Represents the EbixInt32 Datatype
    /// </summary>
    [Serializable]
    public class EbixInt32
    {
        private  Int32 _CurrentValue;
        private  Int32 _PrevValue;
        private  Boolean _IsChanged;
        private  String _ParamName;
        private Boolean _IsDBParam;
        /// <summary>
        /// Use to Initialize the default value of EbixInt32
        /// </summary>
        public EbixInt32(string strParamName)
        {
            _PrevValue=_CurrentValue = Int32.MinValue;
            ParamName = "@"+strParamName.Replace("@","");
            IsDBParam = true;
        }//public Int32 CurrentValue 

        public EbixInt32()
        {
            //CurrentValue = Convert.ToInt32(null);
            ParamName = String.Empty;
            IsDBParam = true;
            _CurrentValue = Convert.ToInt32(null);
            _PrevValue = Convert.ToInt32(null);
        }

        /// <summary>
        /// Get and Set the EbixInt32's CurrentValue 
        /// </summary>
        public  Int32 CurrentValue
        {
            get
            {
                return _CurrentValue;
            }
            set
            {
                 
                //Check  _PrevValue is null then  assign the current value to the _PrevValue
                if (Convert.ToInt32(_PrevValue) == Int32.MinValue)
                {
                    _PrevValue = value;

                }//if (_PrevValue == null)
                else if (Convert.ToInt32(_PrevValue) != Convert.ToInt32(value))//Check if the _PrevValueis not equal to value the _IsChanged is true
                {
                    _IsChanged = true;
                }
                _CurrentValue = value;
            }

        }//public Int32 CurrentValue 
        /// <summary>
        /// Get and Set the EbixInt32's PrevValue 
        /// </summary>
        public  Int32 PrevValue
        {
            get
            {
                return _PrevValue;
            }

        }//public Int32 PrevValue 
        /// <summary>
        /// Get and Set the EbixInt32's IsChanged Value 
        /// </summary>
        public  Boolean IsChanged
        {
            get
            {
                return _IsChanged;
            }
            set { _IsChanged = value; }

        }//public Boolean IsChanged
        /// <summary>
        /// Get and Set the EbixInt32's ParamName Value 
        /// </summary>
        public  String ParamName
        {
            get
            {
                return _ParamName;
            }
            set
            {
                _ParamName = value;
            }
        }//public String ParamName 
        public Boolean IsDBParam
        {
            get
            {
                return _IsDBParam;
            }
            set
            {
                _IsDBParam = value;
            }
        }
    }
    /// <summary>
    /// Represents the EbixString Datatype
    /// </summary>
    [Serializable]
    public class EbixString
    {
        private String _CurrentValue;
        private String _PrevValue;
        private Boolean _IsChanged;
        private String _ParamName;
        private Boolean _IsDBParam;
        /// <summary>
        ///  Use to Initialize the default value of EbixString
        /// </summary>
        public EbixString(string strParamName)
        {
            //CurrentValue = String.Empty;
            ParamName = "@" + strParamName.Replace("@", "");
            
            IsDBParam = true;
        }//public EbixString()
        public EbixString()
        {
            _CurrentValue = String.Empty;
            ParamName = String.Empty;
            _IsDBParam = true;
        }

        /// <summary>
        /// Get and Set the EbixString's CurrentValue 
        /// </summary>
        public String CurrentValue
        {
            get
            {
                return _CurrentValue;
            }
            set
            {
                //Check  _PrevValue is null then  assign the current value to the _PrevValue
                if (Convert.ToString(_PrevValue) == Convert.ToString(null))
                {
                    //if(_IsChanged!=true)
                    _PrevValue = value;

                }//if (_PrevValue == null)
                else if (Convert.ToString(_PrevValue) !=Convert.ToString(value))//Check if the _PrevValueis not equal to value the _IsChanged is true
                {
                    _IsChanged = true;
                }//else if (value != _PrevValue)
              

                _CurrentValue = value;
                
            }
        }//public String CurrentValue
        /// <summary>
        /// Get and Set the EbixString's PrevValue 
        /// </summary>
        public String PrevValue
        {
            get
            {
                return _PrevValue;
            }
             
        }//public String PrevValue
        /// <summary>
        ///  Get and Set the EbixString's IsChanged Value 
        /// </summary>
        public Boolean IsChanged
        {
            get
            {
                return _IsChanged;
            }
             set
            {
                _IsChanged = value;
            }


        }// public Boolean IsChanged
        /// <summary>
        /// Get and Set the EbixString's ParamName Value
        /// </summary>
        public String ParamName
        {
            get
            {
                return _ParamName;
            }
            set
            {
                _ParamName = value;
            }
        }// public Boolean IsChanged
        public Boolean IsDBParam
        {
            get
            {
                return _IsDBParam;
            }
            set
            {
                _IsDBParam = value;
            }
        }
    }
    /// <summary>
    /// Represents the EbixBoolean Datatype
    /// </summary>
    [Serializable]
    public class EbixBoolean
    {
        private Boolean _CurrentValue;
        private Boolean _PrevValue;
        private Boolean _IsChanged;
        private String _ParamName;
        private Boolean _IsDBParam;
        /// <summary>
        ///  Use to Initialize the default value of EbixBoolean
        /// </summary>
        public EbixBoolean(string strParamName)
        {
            _CurrentValue = _PrevValue = false;
            ParamName = "@" + strParamName.Replace("@", ""); 
            IsDBParam = true;
        }
        public EbixBoolean()
        {
            CurrentValue = false;
            ParamName = String.Empty;
            IsDBParam = true;
        }
        /// <summary>
        /// Get and Set the EbixBoolean's CurrentValue 
        /// </summary>
        public Boolean CurrentValue
        {
            get
            {
                return _CurrentValue;
            }
            set
            {
                //Check  _PrevValue is null then  assign the current value to the _PrevValue
                if (Convert.ToBoolean(_PrevValue) == Convert.ToBoolean(null))
                {
                    _PrevValue = value;

                }//if (_PrevValue == null)
                else if (Convert.ToBoolean(_PrevValue) !=Convert.ToBoolean( value))//Check if the _PrevValueis not equal to value the _IsChanged is true
                {
                    _IsChanged = true;
                }//else if (value != _PrevValue)

                _CurrentValue = value;
            }
        }//public Boolean CurrentValue
        /// <summary>
        /// Get and Set the EbixBoolean's PrevValue 
        /// </summary>
        public Boolean PrevValue
        {
            get
            {
                return _PrevValue;
            }

        }// public Boolean PrevValue
        /// <summary>
        ///  Get and Set the EbixBoolean's IsChanged Value 
        /// </summary>
        public Boolean IsChanged
        {
            get
            {
                return _IsChanged;
            }
             set
            {
                 _IsChanged = value;
            }

        }// public Boolean IsChanged
        /// <summary>
        /// Get and Set the EbixBoolean's ParamName Value
        /// </summary>
        public String ParamName
        {
            get
            {
                return _ParamName;
            }
            set
            {
                _ParamName = value;
            }
        }// public String ParamName
        public Boolean IsDBParam
        {
            get
            {
                return _IsDBParam;
            }
            set
            {
                _IsDBParam = value;
            }
        }
    }
    /// <summary>
    /// Represents the EbixDateTime Datatype
    /// </summary>
    [Serializable]
    public class EbixDateTime
    {
        private DateTime _CurrentValue;
        private DateTime _PrevValue;
        private Boolean _IsChanged;
        private String _ParamName;
        private Boolean _IsDBParam;
        private Boolean _IsTimePartRequired;
        /// <summary>
        ///  Use to Initialize the default value of EbixDateTime
        /// </summary>
        public EbixDateTime(string strParamName)
        {
            _PrevValue=_CurrentValue = Convert.ToDateTime(null);
            ParamName = "@" + strParamName.Replace("@", ""); 
            IsDBParam = true;
            _IsTimePartRequired = true;
        }
        public EbixDateTime()
        {
           // CurrentValue = Convert.ToDateTime(null);
            ParamName = String.Empty;
            IsDBParam = true;
            _IsTimePartRequired = true;
        }
        /// <summary>
        /// Get and Set the EbixDateTime's CurrentValue 
        /// </summary>
        public DateTime CurrentValue
        {
            get
            {
                return _CurrentValue;
            }
            set
            {
                //Check  _PrevValue is null then  assign the current value to the _PrevValue
                if (Convert.ToDateTime(_PrevValue) == Convert.ToDateTime(null))
                {
                    _PrevValue = value;

                }//if (_PrevValue == null)
                else if (Convert.ToDateTime(_PrevValue) != value)//Check if the _PrevValueis not equal to value the _IsChanged is true
                {
                    _IsChanged = true;
                }//else if (value != _PrevValue)

                _CurrentValue = value;
            }
        }// public DateTime CurrentValue
        /// <summary>
        /// Get and Set the EbixDateTime's PrevValue 
        /// </summary>
        public DateTime PrevValue
        {
            get
            {
                return _PrevValue;
            }

        }//public DateTime PrevValue
        /// <summary>
        ///  Get and Set the EbixDateTime's IsChanged Value 
        /// </summary>
        public Boolean IsChanged
        {
            get
            {
                return _IsChanged;
            }
            set
            {
                 _IsChanged = value;
            }

        }// public Boolean IsChanged
        /// <summary>
        /// Get and Set the EbixDateTime's ParamName Value
        /// </summary>
        public String ParamName
        {
            get
            {
                return _ParamName;
            }
            set
            {
                _ParamName = value;
            }
        }//public String ParamName
        public Boolean IsDBParam
        {
            get
            {
                return _IsDBParam;
            }
            set
            {
                _IsDBParam = value;
            }
        }
        public Boolean IsTimePartRequired
        {
            get
            {
                return _IsTimePartRequired;
            }
            set
            {
                _IsTimePartRequired = value;
            }
        }
        
    }

    /// <summary>
    /// Represents the EbixDouble Datatype
    /// </summary>
    [Serializable]
    public class EbixDouble
    {
        private Double _CurrentValue;
        private Double _PrevValue;
        private Boolean _IsChanged;
        private String _ParamName;
        private Boolean _IsDBParam;

        /// <summary>
        ///  Use to Initialize the default value of EbixDouble
        /// </summary>
        public EbixDouble(string strParamName)
        {
            _CurrentValue = _PrevValue = Double.MinValue;
            ParamName = "@" + strParamName.Replace("@", ""); 
            IsDBParam = true;
        }
        public EbixDouble()
        {
           // CurrentValue = Convert.ToDouble(null);
            ParamName = String.Empty;
            IsDBParam = true;
        }
        /// <summary>
        /// Get and Set the EbixDouble's CurrentValue 
        /// </summary>
        public Double CurrentValue
        {
            get
            {
                return _CurrentValue;
            }
            set
            {

                //Check  _PrevValue is null then  assign the current value to the _PrevValue
                if (Convert.ToDouble(_PrevValue) == Double.MinValue)
                {
                    _PrevValue = value;

                }//if (_PrevValue == null)
                else if (Convert.ToDouble(_PrevValue) != Convert.ToDouble(value))//Check if the _PrevValueis not equal to value the _IsChanged is true
                {
                    _IsChanged = true;
                }//else if (value != _PrevValue)

                _CurrentValue = value;
            }
        }// public DateTime CurrentValue
        /// <summary>
        /// Get and Set the EbixDouble's PrevValue 
        /// </summary>
        public Double PrevValue
        {
            get
            {
                return _PrevValue;
            }

        }//public DateTime PrevValue
        /// <summary>
        ///  Get and Set the EbixDouble's IsChanged Value 
        /// </summary>
        public Boolean IsChanged
        {
            get
            {
                return _IsChanged;
            }
            set
            {
                 _IsChanged = value;
            }

        }// public Boolean IsChanged
        /// <summary>
        /// Get and Set the EbixDouble's ParamName Value
        /// </summary>
        public String ParamName
        {
            get
            {
                return _ParamName;
            }
            set
            {
                _ParamName = value;
            }
        }//public String ParamName
        public Boolean IsDBParam
        {
            get
            {
                return _IsDBParam;
            }
            set
            {
                _IsDBParam = value;
            }
        }
    }
    /// <summary>
    /// Represents the EbixDecimal Datatype
    /// </summary>
    [Serializable]
    public class EbixDecimal
    {
        private Decimal _CurrentValue;
        private Decimal _PrevValue;
        private Boolean _IsChanged;
        private String _ParamName;
        private Boolean _IsDBParam;

        /// <summary>
        ///  Use to Initialize the default value of EbixDouble
        /// </summary>
        public EbixDecimal(string strParamName)
        {
            _CurrentValue = _PrevValue = Decimal.MinValue;
            ParamName = "@" + strParamName.Replace("@", "");
            IsDBParam = true;
        }
        public EbixDecimal()
        {
            
            ParamName = String.Empty;
            IsDBParam = true;
        }
        /// <summary>
        /// Get and Set the EbixDouble's CurrentValue 
        /// </summary>
        public Decimal CurrentValue
        {
            get
            {
                return _CurrentValue;
            }
            set
            {

                //Check  _PrevValue is null then  assign the current value to the _PrevValue
                if (Convert.ToDecimal(_PrevValue) == Decimal.MinValue)
                {
                    _PrevValue = value;

                }//if (_PrevValue == null)
                else if (Convert.ToDecimal(_PrevValue) != Convert.ToDecimal(value))//Check if the _PrevValueis not equal to value the _IsChanged is true
                {
                    _IsChanged = true;
                }//else if (value != _PrevValue)

                _CurrentValue = value;
            }
        }// public DateTime CurrentValue
        /// <summary>
        /// Get and Set the EbixDouble's PrevValue 
        /// </summary>
        public Decimal PrevValue
        {
            get
            {
                return _PrevValue;
            }

        }//public DateTime PrevValue
        /// <summary>
        ///  Get and Set the EbixDouble's IsChanged Value 
        /// </summary>
        public Boolean IsChanged
        {
            get
            {
                return _IsChanged;
            }
            set
            {
                _IsChanged = value;
            }

        }// public Boolean IsChanged
        /// <summary>
        /// Get and Set the EbixDouble's ParamName Value
        /// </summary>
        public String ParamName
        {
            get
            {
                return _ParamName;
            }
            set
            {
                _ParamName = value;
            }
        }//public String ParamName
        public Boolean IsDBParam
        {
            get
            {
                return _IsDBParam;
            }
            set
            {
                _IsDBParam = value;
            }
        }
    }
    public class ClsEbixBaseDataTypes
    { 
       
        /// <summary>
        /// Use to Initialize the default value of EixDataType
        /// </summary>
        public ClsEbixBaseDataTypes()
        {
        
             
        }//public ClsEbixBaseDataTypes()

        
       
    }


   
}
