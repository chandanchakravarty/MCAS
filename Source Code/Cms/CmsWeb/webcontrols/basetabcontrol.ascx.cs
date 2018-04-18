namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Text;
    using Cms.BusinessLayer.BlCommon;
    using System.Collections;
	/// <summary>
	///		Summary description for TabControl.
	/// </summary>
	public class BaseTabControl : System.Web.UI.UserControl
	{
		private string sTabTitles; //should be comma separated
		private string sTabURLs; //should be comma separated
        private string sTabScreenIDs=""; //should be comma separated
		private string[] sSubTabTitles; //the string should be comma separated among main tabs and ^ separated among sub tabs
		private string[] sSubTabURLs; //the string should be comma separated among main tabs and ^ separated among sub tabs
		private int iTabLength = 150;
		private int iSubTabLength = 150;
		protected System.Web.UI.WebControls.Literal firstTabScript;
		protected System.Web.UI.WebControls.Panel mainTabTbl;
		private bool bNoWrap = false;
		private string sTabDataFilled="";
		protected string sCtlId="";
		protected System.Web.UI.WebControls.Label textlabel;
		private string doAutoSubmit="1";
        protected string strMessage;
		
		public string TabTitles 
		{
			get {return sTabTitles; }
			set {sTabTitles = value; }
		}

		public string TabURLs
		{
			get {return sTabURLs; }
			set {sTabURLs = value; }
		}
        public string TabScreenIDs
        {
            get { return sTabScreenIDs; }
            set { sTabScreenIDs = value; }
        }
		public string SubTabTitles 
		{
			set { sSubTabTitles = value.Split(new Char[]{','}); }
		}

		public string SubTabURLs
		{
			set { sSubTabURLs = value.Split(new Char[]{','}); }
		}
		public int TabLength
		{
			get {return iTabLength; }
			set {iTabLength = value; }
		}
		public int SubTabLength
		{
			get {return iSubTabLength; }
			set {iSubTabLength = value; }
		}
		public bool NoWrap 
		{
			get {return bNoWrap; }
			set {bNoWrap = value; }
		}
		public string TabDataFilled 
		{
			get {return sTabDataFilled; }
			set {sTabDataFilled = value; }
		}	

		public string RequireAutoSubmit
		{
			get 
			{
				return doAutoSubmit=="1"?"Yes":"No";
			}
			set
			{
				if (value.ToLower()=="no")
					doAutoSubmit = "0";
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
            Ajax.Utility.RegisterTypeForAjax(typeof(BaseTabControl));
            int blnDataFlg = 0;
			string[] sArrTabTitles = sTabTitles.Split(new Char[]{','});
			string[] sArrTabURLs = sTabURLs.Split(new Char[]{','});
            string[] sArrTabScreenIds=null;
            if (TabScreenIDs != "")
            {
                sArrTabScreenIds = TabScreenIDs.Split(new Char[] { ',' });
                TabDataFilled = getScreenStatus(TabScreenIDs);
            }
			if (sTabDataFilled!="")
			{
				blnDataFlg=1;				
			}
			string[] sArrTabDataFilled = sTabDataFilled.Split(new Char[]{','});
			//start the javascript string building. This only generates the variables required for running the javascript in the .ascx file
			StringBuilder tmpScript = new StringBuilder("<script type=\"text/javascript\" language=\"JavaScript\">\n<!--\n");
		
			int iTabsPerRow = 0;
			int iRowsPerTbl = 1;
			if (!bNoWrap) //i.e. more than one tab row can be created
			{
				iTabsPerRow = (int) Math.Floor((double) 1000/iTabLength);
				iRowsPerTbl = (int) Math.Ceiling((double) sArrTabTitles.Length/iTabsPerRow);
			}
			else //only one tab row
				iTabsPerRow = sArrTabTitles.Length;
            string strScreenIds = "";
			tmpScript.Append("var iRowsPerTbl = " + iRowsPerTbl + ";\n");
			tmpScript.Append("var iTabsPerRow = " + iTabsPerRow + ";\n");
			tmpScript.Append("var iTabWidth = " + iTabLength + ";\n");
			tmpScript.Append("var iSubTabWidth = " + iSubTabLength + ";\n");
            tmpScript.Append("\nvar arrScreenIds = new Array()" + ";\n");
			tmpScript.Append("\nvar arrMainTab = new Array("); //this would write the js array to create the main tabs
			for (int i=0; i< sArrTabTitles.Length; i++)
			{
                if (blnDataFlg==1)
				{
					tmpScript.Append("\n\"" + sArrTabTitles[i].Trim() + "\", \"" + sArrTabURLs[i].Trim() + "\", " + ((sSubTabURLs == null || sSubTabURLs[i].Trim() == "") ? "0" : "1")+ ", \"" + sArrTabDataFilled[i].Trim()  + "\",");
				}
				else
				{
					tmpScript.Append("\n\"" + sArrTabTitles[i].Trim() + "\", \"" + sArrTabURLs[i].Trim() + "\", " + ((sSubTabURLs == null || sSubTabURLs[i].Trim() == "") ? "0" : "1")+ ", \"N\",");
				}
                if (sArrTabScreenIds!=null && sArrTabScreenIds[i] != null)
                    strScreenIds = strScreenIds + "\n arrScreenIds[" + i.ToString() + "] = \"" + sArrTabScreenIds[i].Trim() + "\";";
                else
                    strScreenIds = strScreenIds + "\n arrScreenIds[" + i.ToString() + "] = \"\";";
			}
			tmpScript.Remove(tmpScript.Length-1,1);
			tmpScript.Append(")\n");
            tmpScript.Append(strScreenIds + "\n"); 
			if (sSubTabTitles != null) 
			{
				string[] sArrSubTabTitles;
				string[] sArrSubTabURLs;
				for (int i=0; i<sSubTabTitles.Length; i++)
				{
					if (sSubTabTitles[i] != null && sSubTabTitles[i].Trim() != "") 
					{
						sArrSubTabTitles = sSubTabTitles[i].Split(new Char[]{'^'}); //split the sub tab title string
						sArrSubTabURLs = sSubTabURLs[i].Split(new Char[]{'^'}); //split the sub tab URL string. it is assumed that the programmer would pass the correct parameters
						tmpScript.Append("\nvar arrSubTab_" + (i+1) + " = new Array("); //this would write the js array to create the sub tabs of main tab (i+1)
						for (int k=0; k<sArrSubTabTitles.Length; k++)
							tmpScript.Append("\n\"" + sArrSubTabTitles[k].Trim() + "\", \"" + sArrSubTabURLs[k].Trim() + "\",");
						tmpScript.Remove(tmpScript.Length-1,1);
						tmpScript.Append(")\n");
					}
				}
			}
			tmpScript.Append("//-->\n</script>\n");
			firstTabScript.Text = tmpScript.ToString(); //write the js string to the client browser
			sCtlId = this.ID; //this call ensures that the ID of the loaded control is available to the js written in the .ascx file
            strMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1453");
		}
        private string getScreenStatus(string strScreenId)
        {
            string screenStatus = "N"; string strRetString = "";
            string[] sArrTabScreenIds = strScreenId.Split(new Char[] { ',' });
            for (int i = 0; i < sArrTabScreenIds.Length; i++)
            {
                 screenStatus = "N";
                string ScreenId = sArrTabScreenIds[i].Trim();
                DataSet objScreenDetails = ClsCommon.GetScreenDetails(ScreenId);
                if (objScreenDetails.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sqlQuery = new StringBuilder();
                    sqlQuery.Append("Select ");
                    sqlQuery.Append(objScreenDetails.Tables[0].Rows[0]["PRIMARY_KEYS"].ToString());
                    sqlQuery.Append(" From ");
                    sqlQuery.Append(objScreenDetails.Tables[0].Rows[0]["TABLE_NAME"].ToString());
                    sqlQuery.Append(" Where 1=1");
                    string[] primary_keys = objScreenDetails.Tables[0].Rows[0]["PRIMARY_KEYS"].ToString().Split(',');
                    string CustomerID = ((cmsbase)this.Page).GetCustomerID();
                    string PolicyID = ((cmsbase)this.Page).GetPolicyID();
                    string PolicyVersionID = ((cmsbase)this.Page).GetPolicyVersionID();
                    string strWhereClause = "";
                    if (CustomerID == "") break;
                    foreach (string key in primary_keys)
                    {
                        if (key == "CUSTOMER_ID" || key.IndexOf("CUSTOMER")> -1 )
                            strWhereClause += " AND " + key + " = " + CustomerID;
                        if (key == "POLICY_ID" || key.IndexOf("POLICY_ID")>-1)
                            strWhereClause += " AND " + key + " = " + PolicyID;
                        if (key == "POLICY_VERSION_ID" || key.IndexOf("POLICY_VER")>-1)
                            strWhereClause += " AND " + key + " = " + PolicyVersionID;
                        
                    }
                    if (strWhereClause != "")
                    {
                        sqlQuery.Append(strWhereClause);
                        if (ClsCommon.GetCountSql(sqlQuery.ToString()) > 0)
                            screenStatus = "Y";
                    }
                }
                if (strRetString == "")
                    strRetString = screenStatus;
                else
                    strRetString += "," + screenStatus;
            }
            return strRetString.TrimEnd(',');
        }
        [Ajax.AjaxMethod()]
        public string GetQValue(string str,string addStr)
        {
            string strRet="";
            if (addStr == "")
                strRet = Cms.CmsWeb.QueryStringModule.EncriptQueryString(str);
            else
            {
                if (str.StartsWith(Cms.CmsWeb.QueryStringModule.PARAMETER_NAME, StringComparison.OrdinalIgnoreCase))
                {
                    strRet = str.Replace(Cms.CmsWeb.QueryStringModule.PARAMETER_NAME, string.Empty);
                    strRet = Cms.CmsWeb.QueryStringModule.Decrypt(strRet);
                    strRet = Cms.CmsWeb.QueryStringModule.EncriptQueryString(strRet + addStr);
                }
                else
                {
                    strRet = Cms.CmsWeb.QueryStringModule.EncriptQueryString(str + addStr);
                }
                
            }
            return strRet;
        }
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
