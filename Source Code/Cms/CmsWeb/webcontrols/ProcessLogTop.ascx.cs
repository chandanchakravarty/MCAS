/******************************************************************************************
<Author					: -		Praveash k. Chandel
<Start Date				: -		27 feb-2007
<End Date				: -	
<Description			: - 	Provide the Process Details on the Policy Process.
<Review Date			: - 
<Reviewed By			: - 	
*******************************************************************************************/ 
namespace Cms.CmsWeb.webcontrols
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Resources;
	using System.Reflection;
	using Cms.BusinessLayer;
	using System.Globalization;
	using Cms.BusinessLayer.BlApplication;
	using Cms.BusinessLayer.BlCommon;
	//using Cms.BusinessLayer.BlAccount;
	//using Cms.BusinessLayer


	/// <summary>
	///		Summary description for ProcessLogTop.
	/// </summary>
	public class ProcessLogTop : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label capName;
		protected System.Web.UI.WebControls.Label capFullName;
		protected System.Web.UI.WebControls.Label capPROCESS_STATUS;
		protected System.Web.UI.WebControls.Label capPROCESS_STATUS_DESC;
		protected System.Web.UI.WebControls.Label capCREATEDBY;
		protected System.Web.UI.WebControls.Label capCREATED_BY;
		protected System.Web.UI.WebControls.Label capCREATEDDATE;
		protected System.Web.UI.WebControls.Label capCREATED_DATETIME;
		protected System.Web.UI.WebControls.Label capCOMPLETEDBY;
		protected System.Web.UI.WebControls.Label capCOMPLETED_BY;
		protected System.Web.UI.WebControls.Label capCOMPLETEDATE;
		protected System.Web.UI.WebControls.Label capCOMPLETE_DATETIME;
		protected System.Web.UI.WebControls.Label capOLDPOLICY_VERSION_ID;
		protected System.Web.UI.WebControls.Label capPOLICY_VERSION_ID;
		protected System.Web.UI.WebControls.Label capNEWPOLICY_VERSION_ID;
		protected System.Web.UI.WebControls.Label capNEW_POLICY_VERSION_ID;
		protected System.Web.UI.WebControls.Label capPREVIOUS_POLICY_STATUS;
		protected System.Web.UI.WebControls.Label capPOLICY_PREVIOUS_STATUS;
		protected System.Web.UI.WebControls.Label capPOLICY_CUR_STATUS;
		protected System.Web.UI.WebControls.Label capPOLICY_CURRENT_STATUS;
		protected System.Web.UI.WebControls.Label capExpirationDate;
		protected System.Web.UI.WebControls.Label capPolicyExpirationDate;
		
		protected string colorScheme;
		//public string att_note="";
		protected int intCustomerID;
		protected int intPolicyID;
		protected int intPolicyVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidProcess_ID;
		protected int intProcessID; 
		protected int intBaseVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBASE_VERSION_ID;
		protected System.Web.UI.WebControls.Label capProcessExpirationDate;
		protected System.Web.UI.WebControls.Label capCOMMENTS;
		protected System.Web.UI.WebControls.Label capPRINTS_COMMENTS; 
		public bool UseRequestVariables = true;	
		/// <summary>
		/// Set the Customer ID. 
		/// </summary>
		public  int CustomerID
		{
			get
			{
				return intCustomerID;
			}
			set 
			{
				intCustomerID = value;
			}
		}

		/// <summary>
		/// Set the Policy ID.
		/// </summary>
		public  int PolicyID
		{
			
			get
			{
				return intPolicyID;
			}
			set 
			{
				intPolicyID = value;
			}
		}

		/// <summary>
		/// Set the Policy Version ID.
		/// </summary>
		public  int PolicyVersionID 
		{
			get
			{
				return intPolicyVersionID;
			}
			set
			{
				intPolicyVersionID = value; 
			}
		}
		/// <summary>
		/// Set the Process ID.
		/// </summary>
		public  int ProcessID 
		{
			get
			{
				return intProcessID;
			}
			set
			{
				intProcessID = value; 
			}
		}
		/// <summary>
		/// Set the Base Policy_Version_ID.
		/// </summary>
		public  int BasePolicyVersionID 
		{
			get
			{
				return intBaseVersionID;
			}
			set
			{
				intBaseVersionID = value; 
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			CallPageLoad();
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		/// <summary>
		/// It will set the functions in page load event.
		/// </summary>
		public void CallPageLoad()
		{
			if (UseRequestVariables)
				SetRequestVariable();	//Retreiving the values of policy , version and customer from request

			SetCaptions();
			ShowProcessHeaderDetails();
			//SetPolicyStatusInSession();
			//FillDropDownAndDisplay();

			//Setting the Image of Customer Assistant
			colorScheme =((cmsbase)this.Page).GetColorScheme();
			//CustomerDetail.ImageUrl="~/cmsweb/Images" + colorScheme  + "/Customer_Ass.gif";
		}
		/// <summary>
		/// Sets the Request Variables.
		/// </summary>
		private void SetRequestVariable()
		{
			if (Request["Customer_ID"] != null )
			{
				intCustomerID = Convert.ToInt32(Request["Customer_ID"]);
			}

			if (Request["Policy_ID"] != null )
			{
				intPolicyID = Convert.ToInt32(Request["Policy_ID"]);
			}

			if (Request["PolicyVersionID"] != null )
			{
				intPolicyVersionID = Convert.ToInt32(Request["PolicyVersionID"]);
			}
		
		}

		public void RefreshProcess()
		{
			SetCaptions();
			ShowProcessHeaderDetails();
			//SetPolicyStatusInSession();
			//FillDropDownAndDisplay();
		}
		/// <summary>
		/// Shows the details of policy on policy top control
		/// </summary>
		private void ShowProcessHeaderDetails()
		{
			
			Cms.BusinessLayer.BlClient.ClsCustomer  objProcess = new Cms.BusinessLayer.BlClient.ClsCustomer();
			DataSet ds = objProcess.GetProcessHeaderDetails(intCustomerID, intPolicyID, intPolicyVersionID);
			if ( ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				System.Data.DataRow dr = ds.Tables[0].Rows[0];
				hidProcess_ID.Value =dr["PROCESS_ID"].ToString(); 
				capFullName.Text = dr["PROCESS_FULLNAME"].ToString();
				capPOLICY_CURRENT_STATUS.Text = dr["CURRENT_POLICY_DESCRIPTION"].ToString(); 
				capPOLICY_PREVIOUS_STATUS.Text = dr["PREVIOUS_POLICY_DESCRIPTION"].ToString(); 
				capNEW_POLICY_VERSION_ID.Text =  dr["CURRENT_DISP_VERSION"].ToString();
				capPROCESS_STATUS_DESC.Text =   dr["PROCESS_STATUS"].ToString();
				capCREATED_BY.Text			=   dr["CREATED_BY"].ToString();
				capCREATED_DATETIME.Text	=	dr["CREATED_DATETIME"].ToString();	
				capCOMPLETED_BY.Text		=   dr["COMPLETED_BY"].ToString();	
				capCOMPLETE_DATETIME.Text	=	dr["COMPLETED_DATETIME"].ToString();
				capPOLICY_VERSION_ID.Text	=	dr["PREVIOUS_DISP_VERSION"].ToString();
				hidBASE_VERSION_ID.Value    =   dr["POLICY_VERSION_ID"].ToString();
				this.ProcessID				=   int.Parse(dr["PROCESS_ID"].ToString());
				this.BasePolicyVersionID	=  int.Parse(dr["POLICY_VERSION_ID"].ToString());  
				capProcessExpirationDate.Text =  dr["EXPIRY_DATE"].ToString();
				capPRINTS_COMMENTS.Text			=	dr["PRINT_COMMENTS"].ToString();
			}
		}
		/// <summary>
		/// Sets the captions of the labels.
		/// </summary>
		private void SetCaptions()
		{
			
			//creating resource manager object (used for reading field and label mapping)
			System.Resources.ResourceManager objResourceMgr;

			objResourceMgr = new System.Resources.ResourceManager("Cms.cmsweb.webcontrols.ProcessLogTop" ,System.Reflection.Assembly.GetExecutingAssembly());

			capName.Text							=		objResourceMgr.GetString("capName");
			capPOLICY_CUR_STATUS.Text				=		objResourceMgr.GetString("capPOLICY_CUR_STATUS");
			capPREVIOUS_POLICY_STATUS.Text			=		objResourceMgr.GetString("capPOLICY_PREVIOUS_STATUS");
			capPROCESS_STATUS.Text					=		objResourceMgr.GetString("capPROCESS_STATUS");
			capCREATEDBY.Text						=		objResourceMgr.GetString("capCREATEDBY");	
			capCREATEDDATE.Text						=		objResourceMgr.GetString("capCREATEDDATE");	
			capCOMPLETEDBY.Text						=		objResourceMgr.GetString("capCOMPLETEDBY");	
			capCOMPLETEDATE.Text					=		objResourceMgr.GetString("capCOMPLETEDATE");	
			capOLDPOLICY_VERSION_ID.Text			=  		objResourceMgr.GetString("capOLDPOLICY_VERSION_ID");	
			capNEWPOLICY_VERSION_ID.Text			=		objResourceMgr.GetString("capNEWPOLICY_VERSION_ID");
			capExpirationDate.Text					=		objResourceMgr.GetString("capExpirationDate");
			capCOMMENTS.Text						=		objResourceMgr.GetString("capCOMMENTS");
	
		}

	}
}
