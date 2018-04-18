namespace Cms.CmsWeb.WebControls
{
	using System;
	using System.Data;
	using System.Collections;
	using System.Text;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Cms.BusinessLayer.BlCommon;
    using System.Reflection;
    using System.Resources;
	
	/// <summary>
	///	Summary description for WorkFlow.
	/// </summary>
	public class WorkFlow : System.Web.UI.UserControl
	{
		private Hashtable	PrimaryKeys = new Hashtable();
		private string		strWorkFlowScreens;
		private string		strscreenID = null;
		public Hashtable	ScreenStatus = new Hashtable();
		public string		WorkflowHeader = "Work Status";
		public	string		workflowXML;
		private bool		isTop = true;
		private bool		boolDisplay = true;
		private string		strColorScheme = "1";
		protected System.Web.UI.WebControls.Panel divWorkFlow;

        public string CustomerId = "";
        public string PolicyId = "";
        public string PolicyVersionId = "";
        //Added by Charles on 11-Mar-10 for Multilingual Implementation
        private ResourceManager objResourceMgr;
        public string strGoTo, strPrevious, strNext, strLast, strFirst, strSelectAdd;
        //Added till here
        public string CustMgrEnQuery;
        public string TranLogEnQuery;
		public string WorkflowModule = "APP";
        public String strVerify = String.Empty;
        public String strCustomer = String.Empty;
        public String strTrans = String.Empty;
        public String strAccount = String.Empty;
        public String strQuote = String.Empty;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            Ajax.Utility.RegisterTypeForAjax(typeof(WorkFlow)); 
			ColorScheme		=	((cmsbase)this.Page).GetColorScheme();
            strVerify = ClsMessages.GetMessage("G", "1391");
            strCustomer = ClsMessages.GetMessage("G", "1392");
            strTrans = ClsMessages.GetMessage("G", "1393");
            strAccount = ClsMessages.GetMessage("G", "1394");
            strQuote = ClsMessages.GetMessage("G", "1989");
            SetCaptions();

			if (WorkflowModule == "APP")
				WorkflowHeader = ClsMessages.GetMessage("workflow","1");
			else
				WorkflowHeader = ClsMessages.GetMessage("workflow","2");
			
			if(Display && (ScreenID != null))
			{
				GetScreenStatus();
				SetScreenStatus();
			}
            EnQueryString();
		}
		
		#region public functions
		public void AddKeyValue(string strName, object objValue)
		{
			if(PrimaryKeys.ContainsKey(strName))
				PrimaryKeys.Remove(strName);
			PrimaryKeys.Add(strName,objValue);
		}

        public void GetScreenStatus()
        {
            DataSet objScreenDetails;
            StringBuilder sqlQuery;
            strWorkFlowScreens = ClsCommon.GetWorkflowScreens(ScreenID);
            string[] arrScreens = strWorkFlowScreens.Split(',');
            string[] primary_keys;
            bool anded;
            foreach (string aString in arrScreens)
            {
                objScreenDetails = ClsCommon.GetScreenDetails(aString);
                if (objScreenDetails.Tables[0].Rows.Count > 0)
                {
                    anded = false;
                    sqlQuery = new StringBuilder();
                    sqlQuery.Append("Select ");
                    sqlQuery.Append(objScreenDetails.Tables[0].Rows[0]["PRIMARY_KEYS"].ToString());
                    sqlQuery.Append(" From ");
                    sqlQuery.Append(objScreenDetails.Tables[0].Rows[0]["TABLE_NAME"].ToString());
                    sqlQuery.Append(" Where ");
                    primary_keys = objScreenDetails.Tables[0].Rows[0]["PRIMARY_KEYS"].ToString().Split(',');
                    string strWhereClause = "";
                    foreach (string key in primary_keys)
                    {
                        if (PrimaryKeys.ContainsKey(key.Trim()))
                        {
                            if (!anded)
                            {
                                strWhereClause = key.Trim() + " = " + PrimaryKeys[key.Trim()];
                                //sqlQuery.Append(key.Trim() + " = " + PrimaryKeys[key.Trim()]);
                                anded = true;
                            }
                            else
                                strWhereClause += " AND " + key.Trim() + " = " + PrimaryKeys[key.Trim()];
                            //sqlQuery.Append(" AND " + key.Trim() + " = " + PrimaryKeys[key.Trim()]);
                        }
                    }
                    if (strWhereClause != "")
                        sqlQuery.Append(strWhereClause);
                    if (strWhereClause != "" && ClsCommon.GetCountSql(sqlQuery.ToString()) > 0)
                    {
                        if (ScreenStatus.ContainsKey(objScreenDetails.Tables[0].Rows[0]["SCREEN_ID"].ToString()))
                        {
                            ScreenStatus.Remove(objScreenDetails.Tables[0].Rows[0]["SCREEN_ID"].ToString());
                        }
                        ScreenStatus.Add(objScreenDetails.Tables[0].Rows[0]["SCREEN_ID"].ToString(),
                            objScreenDetails.Tables[0].Rows[0]["SCREEN_DESC"].ToString() + "~" +
                            "true~" + objScreenDetails.Tables[0].Rows[0]["WORKFLOW_ORDER"].ToString()
                            + "~" + objScreenDetails.Tables[0].Rows[0]["MENU_LINK"].ToString()
                            + "~" + objScreenDetails.Tables[0].Rows[0]["TAB_NUMBER"].ToString());
                    }
                    else
                    {
                        if (ScreenStatus.ContainsKey(objScreenDetails.Tables[0].Rows[0]["SCREEN_ID"].ToString()))
                        {
                            ScreenStatus.Remove(objScreenDetails.Tables[0].Rows[0]["SCREEN_ID"].ToString());
                        }
                        ScreenStatus.Add(objScreenDetails.Tables[0].Rows[0]["SCREEN_ID"].ToString(),
                            objScreenDetails.Tables[0].Rows[0]["SCREEN_DESC"].ToString() + "~" +
                            "false~" + objScreenDetails.Tables[0].Rows[0]["WORKFLOW_ORDER"].ToString()
                            + "~" + objScreenDetails.Tables[0].Rows[0]["MENU_LINK"].ToString()
                            + "~" + objScreenDetails.Tables[0].Rows[0]["TAB_NUMBER"].ToString());
                    }
                }
            }
            //AddedBy Lalit
            if (PrimaryKeys.Keys.Count > 0)
            {
                ICollection ColumnsNameKey = PrimaryKeys.Keys;
                IDictionaryEnumerator _Enumerator = PrimaryKeys.GetEnumerator();
                while (_Enumerator.MoveNext())
                {
                    if (_Enumerator.Key.ToString() == "CUSTOMER_ID")
                        CustomerId = _Enumerator.Value.ToString();
                    if (_Enumerator.Key.ToString() == "POLICY_ID")
                        PolicyId = _Enumerator.Value.ToString();
                    if (_Enumerator.Key.ToString() == "POLICY_VERSION_ID")
                        PolicyVersionId = _Enumerator.Value.ToString();
                }
            }

        }
        /// <summary>
        /// Set Captions according to culture
        /// </summary>
        /// Added by Charles on 11-Mar-2010 for Multilingual Implementation
        private void SetCaptions()
        {
            objResourceMgr = new ResourceManager("Cms.CmsWeb.WebControls.WorkFlow", Assembly.GetExecutingAssembly());
            strGoTo = objResourceMgr.GetString("strGoTo");
            strPrevious = objResourceMgr.GetString("strPrevious");
            strNext = objResourceMgr.GetString("strNext");
            strLast = objResourceMgr.GetString("strLast");
            strFirst = objResourceMgr.GetString("strFirst");
            strSelectAdd = objResourceMgr.GetString("strSelectAdd");
        }

		public void SetScreenStatus()
		{
			string strId		=	"";
			string strDesc		=	"";
			string strValue		=	"";
			string strOrder		=	"";
			string strLink		=	"";
			string val			=	"";
			string strTabNumber	=	"";


			string[] screens	=	new string[3];

			StringBuilder workFlow = new StringBuilder();
			workFlow.Append("<workflow>");

			string i="";
			foreach(string key in ScreenStatus.Keys)
			{
				val				=	ScreenStatus[key].ToString();
				strId			=	key.ToString();
				screens			=	val.Split('~');
				strDesc			=	screens[0];
				strValue		=	screens[1];
				strOrder		=	screens[2];
				strLink			=	screens[3];
				strTabNumber	=	screens[4];
				
				
				//workFlow.Append("<screen id=\"" + strId + "\" desc=\"" + strDesc + "\" value=\"" + strValue + "\" order=\"" + strOrder + "\" menu_link=\"" + strLink + "\"/>");
				workFlow.Append("<screen id=\"" + strId + "\" desc=\"" + strDesc + "\" value=\"" + strValue + "\" order=\"" + strOrder + "\" menu_link=\"" + strLink + "\" tabNumber=\"" + strTabNumber + "\"/>");
				
			}
			workFlow.Append("</workflow>");
			workflowXML = workFlow.ToString();
		}

		/// <summary>
		/// Removes the specified screen from workflow
		/// </summary>
		/// <param name="ScreenID"></param>
		public void RemoveScreen(string ScreenID)
		{
			if (workflowXML != null & workflowXML != "")
			{
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				workflowXML = workflowXML.Replace("&", "&amp;");
				doc.LoadXml(workflowXML);

				System.Xml.XmlNode nd = doc.SelectSingleNode("/workflow/screen[@id='" + ScreenID + "']");
				if (nd != null)
				{
					doc.FirstChild.RemoveChild(nd);
				}

				workflowXML = doc.InnerXml;
			}
		}

        public void EnQueryString()
        {
            //Encrypt Query for customer assistance redirect
            CustMgrEnQuery = "customer_id = " + CustomerId;
            CustMgrEnQuery = QueryStringModule.EncriptQueryString(CustMgrEnQuery);

            //Encrypt Query for transaction log
            TranLogEnQuery = "CUSTOMER_ID=" + CustomerId + "&CalledFrom=InCLT&CALLEDFOR=WORKFLOW";
            TranLogEnQuery = QueryStringModule.EncriptQueryString(TranLogEnQuery);
            
        }

		#endregion

		#region Public Properties
		public string ScreenID
		{
			set
			{
				strscreenID			=	value;
			}
			get
			{
				return strscreenID;
			}
		}

		public bool IsTop
		{
			set
			{
				isTop	=	value;
			}
			get
			{
				return isTop;
			}
		}

		public bool Display
		{
			set
			{
				boolDisplay = value;
			}
			get
			{
				return boolDisplay;
			}
		}
		public string ColorScheme
		{
			set
			{
				strColorScheme = value;
			}
			get
			{
				return strColorScheme;
			}
		}
		#endregion


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        [Ajax.AjaxMethod()]
        public static string jsEncriptQueryString(string strUrl)
        {
            return QueryStringModule.EncriptQueryString(strUrl);
        }
	}
}
