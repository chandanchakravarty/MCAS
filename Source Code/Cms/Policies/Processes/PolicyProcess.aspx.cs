using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Policies.Processes
{
	/// <summary>
	/// Summary description for PolicyProcess.
	/// </summary>
	public class PolicyProcess : Cms.Policies.policiesbase
	{
		
		private string policyProcess = "";
		private string customerID = "";
		private string policyID = "";
		private string policyVersionID = "";
		protected System.Web.UI.WebControls.Label lblMessage;
		private string url = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.ScreenId = "5000_31";
			policyProcess = Request["PROCESS"];
			customerID = Request["Customer_ID"];
			policyID = Request["Policy_ID"];
			policyVersionID = Request["Policy_Version_ID"];
			
			//Selecting the page based on process passed in query string
            string QueryStr = Cms.CmsWeb.QueryStringModule.EncriptQueryString("Customer_ID="
                        + customerID + "&Policy_ID="
                        + policyID + "&process="
                        + policyProcess + "&policyVersionID="
                        + policyVersionID + "&"); 			  
			switch(policyProcess.ToUpper())
			{
                case "RCANCEL":	
				case "CCANCEL":	
				case "CANCEL":	
					url = "CancellationProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;
				case "ENDORSE":
				case "CENDORSE":				
				case "RENDORSE":
				case "SENDORSE":
					url = "EndorsementProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;
				case "CREINST":
				case "RREINST":
				case "REINST":
					url = "ReinstateProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;
				case "CRENEWAL":
				case "RRENEWAL":
				case "RENEWAL":
					url = "RenewalProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;
				case "RNRENEW":
				case "CNRENEW":
				case "NRENEW":
					url = "NonRenewProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;

				case "RNEGT":
				case "CNEGT":
				case "NEGT":
					url = "NegateProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;
				case "CNBUS":
				case "RNBUS":
				case "NBUS":
					url = "NewBusinessProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;					
				case "CDECPOL":
				case "RDECPOL":
				case "DECPOL":
					url = "RescindProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;

				case "CCORUSER":
				case "RCORUSER":
				case "CORUSER":
					url = "CorrectiveUserProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;
				case "REWRTE":
				case "CREWRTE":
				case "RREWRTE":
					url = "RewriteProcess.aspx" + QueryStr ;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                         */ 
					break;
				case "REVERT":
				case "CREVERT":
				case "RREVERT":
                    url = "RollbackAfterCommit.aspx" + QueryStr;
                        /*?Customer_ID=" 
						+ customerID + "&Policy_ID=" 
						+ policyID + "&process=" 
						+ policyProcess + "&policyVersionID=" 
						+ policyVersionID + "&";
                        */
					break;
				default:
					url = "";
					break;
			}

			if (url != "")
			{
				//Redirecting to the process specific page
				Response.Redirect(url);
			}
			else
			{
				//Invalid process has been passed in URL
				lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("561");
			}
			
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
