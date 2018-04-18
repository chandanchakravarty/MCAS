/******************************************************************************************
<Author				: - Pradeep
<Start Date			: -	April 25, 2005
<End Date			: -	April 25, 2005
<Description		: - Used to store the data from look up tables
<Review Date		: - 
<Reviewed By		: - 	
Modification History
<Modified Date		: - 
<Modified By		: - 
<Purpose			: - 
*******************************************************************************************/

using System;

namespace Cms.Model
{
	/// <summary>
	/// Summary description for ClsLookupInfo.
	/// </summary>
	public class ClsLookupInfo
	{
		private int LOOKUP_UNIQUE_ID;
		private string LOOKUP_VALUE_DESC;
        private string LOOKUP_VALUE_DESC1;
		private string LOOKUP_VALUE_CODE;

		public ClsLookupInfo()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public int LookupID
		{
			get { return LOOKUP_UNIQUE_ID; }
			set { LOOKUP_UNIQUE_ID = value; }
		}

		public string LookupDesc
		{
			get { return LOOKUP_VALUE_DESC;}
			set { LOOKUP_VALUE_DESC = value; }
		}

        public string LookupDesc1
        {
            get { return LOOKUP_VALUE_DESC1; }
            set { LOOKUP_VALUE_DESC1 = value; }
        }
		public string LookupCode
		{
			get { return LOOKUP_VALUE_CODE;}
			set { LOOKUP_VALUE_CODE = value; }
		}
	}
		
}
