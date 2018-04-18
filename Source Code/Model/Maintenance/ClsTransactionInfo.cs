/******************************************************************************************
<Author					: - Anshuman
<Start Date				: -	Apr 14, 2005 10:28:03 AM
<End Date				: -	
<Description			: - Model for Transcation Log Table.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 21-Apr-2010
<Modified By			: - Charles Gomes
<Purpose				: - Multilingual Additional Info
*******************************************************************************************/ 
using System;
using System.Data;
using Cms.Model;
using System.Resources;
using System.Reflection;

namespace Cms.Model.Maintenance
{
	/// <summary>
	/// Summary description for ClsTransactionInfo.
	/// </summary>
	public class ClsTransactionInfo
	{
		int intTransId;

        // Added by Charles on 21-Apr-2010 for Multilingual Implementation
        //private ResourceManager objResourceManager = null;
        //private int APPLICATION_CONVERT_CHECK = -1;

		public ClsTransactionInfo()
		{             
            //objResourceManager = new ResourceManager("Cms.Model.Maintenance.ClsTransactionInfo", Assembly.GetExecutingAssembly());
		}
		public int TransID
		{
			get
			{
				return intTransId;
			}
			set
			{
				intTransId = value;
			}
		}
		#region Database schema details
		
		// model for database field TRANS_TYPE_ID(int)
		public int TRANS_TYPE_ID
		{
			get
			{
				return intTRANS_TYPE_ID;
			}
			set
			{
				intTRANS_TYPE_ID = value;
			}
		}
		
		/// <summary>
		/// model for database field CLIENT_ID(int)
		/// </summary>
		public int CLIENT_ID
		{
			get
			{
				return intCLIENT_ID;
			}
			set
			{
				intCLIENT_ID = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_TYPE(nvarchar 25)
		/// </summary>
		public int POLICY_ID
		{
			get
			{
				return intPOLICY_ID;
			}
			set
			{
				intPOLICY_ID = value;
			}
		}

		/// <summary>
		/// model for database field POLICY_VER_TRACKING_ID(int)
		/// </summary>
		public int POLICY_VER_TRACKING_ID
		{
			get
			{
				return intPOLICY_VER_TRACKING_ID;
			}
			set
			{
				intPOLICY_VER_TRACKING_ID = value;
			}
		}

		/// <summary>
		/// model for database field RECORDED_BY(int)
		/// </summary>
		public int RECORDED_BY
		{
			get
			{
				return intRECORDED_BY;
			}
			set
			{
				intRECORDED_BY = value;
			}
		}

		/// <summary>
		/// model for database field RECORDED_BY_NAME(nvarchar 75)
		/// </summary>
		public string RECORDED_BY_NAME
		{
			get
			{
				return strRECORDED_BY_NAME;
			}
			set
			{
				strRECORDED_BY_NAME = value;
			}
		}

		/// <summary>
		/// model for database field CUSTOMER_INSURANCE_RECEIVED_DATE(datetime)
		/// </summary>
		public DateTime RECORD_DATE_TIME
		{
			get
			{
				return dateRECORD_DATE_TIME;
			}
			set
			{
				dateRECORD_DATE_TIME = value;
			}
		}

		/// <summary>
		/// model for database field TRANS_DESC(text)
		/// </summary>
		public string TRANS_DESC
		{
			get
			{
				return strTRANS_DESC;
			}
			set
			{
				strTRANS_DESC = value;
			}
		}

		/// <summary>
		/// model for database field ENTITY_ID(int)
		/// </summary>
		public int ENTITY_ID
		{
			get
			{
				return intENTITY_ID;
			}
			set
			{
				intENTITY_ID = value;
			}
		}

		/// <summary>
		/// model for database field ENTITY_TYPE(nvarchar 5)
		/// </summary>
		public string ENTITY_TYPE
		{
			get
			{
				return strENTITY_TYPE;
			}
			set
			{
				strENTITY_TYPE = value;
			}
		}

		/// <summary>
		/// model for database field IS_COMPLETED(nchar 1)
		/// </summary>
		public string IS_COMPLETED
		{
			get
			{
				return strIS_COMPLETED;
			}
			set
			{
				strIS_COMPLETED = value;
			}
		}
		
		/// <summary>
		/// model for database field CHANGE_XML(text)
		/// </summary>
		public string CHANGE_XML
		{
			get
			{
				return strCHANGE_XML;
			}
			set
			{
				strCHANGE_XML = value;
			}
		}
		public int APP_ID
		{
			get
			{
				return intAPP_ID;
			}
			set
			{
				intAPP_ID = value;
			}
		}
		public int APP_VERSION_ID
		{
			get
			{
				return intAPP_VERSION_ID;
			}
			set
			{
				intAPP_VERSION_ID = value;
			}
		}
		public int QUOTE_ID
		{
			get
			{
                // Added by Charles on 21-Apr-2010 for Multilingual Implementation
                if (intQUOTE_ID == null)
                {
                    intQUOTE_ID = 0;
                }
				return intQUOTE_ID;
			}
			set
			{
				intQUOTE_ID = value;
			}
		}
		public int QUOTE_VERSION_ID
		{
			get
			{
				return intQUOTE_VERSION_ID;
			}
			set
			{
				intQUOTE_VERSION_ID = value;
			}
		}

		/// <summary>
		/// model for database field strCustomInfo(nvarchar 5)
		/// </summary>
		public string CUSTOM_INFO
		{
			get
			{
				return strCustomInfo;
			}
			set
			{
				strCustomInfo	=	 value;
			}
		}

        /// <summary>
        /// Additional Information for Transaction
        /// </summary>
        /// Added by Charles on 21-Apr-2010 for Multilingual Implementation
        public string ADDITIONAL_INFO
        {
            get
            {
                if (strAdditionalInfo == null)
                {
                    strAdditionalInfo = "";

                }
                //else
                //{
                //    strAdditionalInfo = GetAdditionalInfo(APP_ID,POLICY_ID,QUOTE_ID);
                //}
                return strAdditionalInfo;
            }
            set
            {
                strAdditionalInfo = value;
            }
        }
		#endregion

        /// <summary>
        /// Fetches Additional Information for Transaction Log
        /// </summary>
        /// <param name="APP_ID">App ID</param>
        /// <param name="POLICY_ID">Policy ID</param>
        /// <param name="QUOTE_ID">Quote ID</param>
        /// <returns>ADDITIONAL_INFO</returns>
        /// Added by Charles on 21-Apr-10 
        //public string GetAdditionalInfo(int APP_ID, int POLICY_ID,int QUOTE_ID)
        //{
        //    return "";
        //}

	
		private int			intTRANS_TYPE_ID;
		private int			intCLIENT_ID;
		private int			intPOLICY_ID;
		private int			intPOLICY_VER_TRACKING_ID;
		private int			intRECORDED_BY;
		private string		strRECORDED_BY_NAME;
		private DateTime	dateRECORD_DATE_TIME;
		private string		strTRANS_DESC;
		private int			intENTITY_ID;
		private string		strENTITY_TYPE;
		private string		strIS_COMPLETED;
		private int			intAPP_ID;
		private int			intAPP_VERSION_ID;
		private int			intQUOTE_ID;
		private int			intQUOTE_VERSION_ID;
		private string		strCHANGE_XML;
		private string		strCustomInfo;
        private string      strAdditionalInfo = ""; //Added by Charles on 21-Apr-2010 for Multilingual Implementation
	}
}
