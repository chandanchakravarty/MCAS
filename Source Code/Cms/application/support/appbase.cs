/******************************************************************************************
	<Author					: Nidhi >
	<Start Date				: April 25,2005>
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >21-02-2006
	<Modified By			: - >shafi
	<Purpose				: - >Application is not active, hence changing the security xml to read only mode
	                          - >If Policy Allready Exists For Any Version of the Application In Active session changing the security xml to read only mode
*******************************************************************************************/



using System;
using Cms;
using Cms.CmsWeb;
using System.Web.UI.WebControls;

namespace Cms.Application
{
	/// <summary>
	/// Summary description for appbase.
	/// </summary>
	public class appbase:Cms.CmsWeb.cmsbase 
	{
		//11489>Bass boat (w/Motor)
		//11369>Outboard Boat
		//11487>Outboard (w/Motor)
		//11374>Pontoon (w/Motor)
		public const string TYPE_OF_WATERCRAFT_OUTBOARD_BOAT = "11369";
		public const string TYPE_OF_WATERCRAFT_BASS_BOAT_WITHOUT_MOTOR = "11489";
		public const string TYPE_OF_WATERCRAFT_OUTBOARD_WITHOUT_MOTOR = "11487";
		public const string TYPE_OF_WATERCRAFT_PONTOON = "11374";
		public const string CALLED_FROM_GEN_INFO = "GEN_INFO";
		public const string CALLED_FROM_SHOW_DIAG = "SHOW_DIAG";

		public appbase()
		{
			
		}

		/// <summary>
		/// Writes the script, which will reload the menu for the specific lob of application
		/// </summary>
		public void ReloadApplicationMenu(string DefaultPage)
		{
			string strRefreshtopMenuScript;
			strRefreshtopMenuScript = "<script language='javascript'>top.topframe.createAppTopMenu('" + GetLOBString() + "','" + DefaultPage + "',false);top.topframe.main1.mousein = false;top.topframe.findMouseIn();</script>";
			ClientScript.RegisterStartupScript(this.GetType(),"REFRESHTOPMENU",strRefreshtopMenuScript);
		}

		//Overriding the screen id property of cmsbase class
		public new string ScreenId
		{
			get
			{
				return base.ScreenId;
			}
			set
			{
				base.ScreenId = value;
				
				
				//Here we will check whether the policy exists for the appliation in session
				//If policy exists then user can not edit any information , hence we will change
				//the security xml to view mode only
				try
				{
					string strAppId = GetAppID();
					string strAppVerId = GetAppVersionID();
					string strCustomerID = GetCustomerID();
					//string result = "";
					
					
					int RetVal = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.CheckForConverted(int.Parse(strCustomerID), int.Parse(strAppId));
					if (RetVal == 2)
					{
						//Policy exists for this particulat application, hence changing the security xml to view mode only
						gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>"; 
						base.InitializeSecuritySettings();
					}
					else
					{
						//Checking whether the application in sessoin is active or not
	
						RetVal = Cms.BusinessLayer.BlApplication.ClsGeneralInformation.CheckForApplicationStatus(int.Parse(strCustomerID), int.Parse(strAppId), int.Parse(strAppVerId));

						if (RetVal == 2)
						{
							//Application is not active, hence changing the security xml to read only mode
							gstrSecurityXML = "<Security><Read>Y</Read><Write>N</Write><Delete>N</Delete><Execute>N</Execute></Security>";
							base.InitializeSecuritySettings();
						}
					
					}
				}
				catch
				{
				}
			}
		}

		//Added by Sibin on 3 March 09 for Itrack Issue 5453
		public void OpenEndorsementDetails()
		{

			//Retreiving the active process executing on policy
			if(GetPolicyID()!=null && GetPolicyID()!="" && GetCustomerID()!=null && GetCustomerID()!="")//Condition Added for Itrack Issue 5587
			{
				Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess objProcess = new Cms.BusinessLayer.BlProcess.ClsEndorsmentProcess ();
				Cms.Model.Policy.Process.ClsProcessInfo objProcessInfo = objProcess.GetRunningProcess( int.Parse(GetCustomerID())
					, int.Parse(GetPolicyID()));

			
				if (objProcessInfo != null)
				{
					//If running process is endorsement process, opening the endorsement pop up window
					if (objProcessInfo.PROCESS_ID == Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_ENDORSEMENT_PROCESS && objProcessInfo.NEW_POLICY_VERSION_ID == int.Parse(GetPolicyVersionID ()))
					{
						//Policy is under endorsement process, hence showing the endorsement details popup window
						string strOpenEndorsementWin;

						//if (Session["TransactionId"] != null)
						if (Session["EndorsementTranIds"] != null)
							strOpenEndorsementWin = "<script>"
								+ "winEndorsement = ShowPopup('/cms/policies/processes/endorsementdetails.aspx?"
								+ "POLICY_ID=" + objProcessInfo.POLICY_ID + "&"
								//							+ "POLICY_VERSION_ID=" + objProcessInfo.POLICY_VERSION_ID + "&" 
								// Changed by Swarup(07-feb-2008) as it is taking old version id when it should take new version id
								+ "POLICY_VERSION_ID=" + objProcessInfo.NEW_POLICY_VERSION_ID + "&"
								+ "CUSTOMER_ID=" + objProcessInfo.CUSTOMER_ID + "&"
								+ "ENDORSEMENT_NO=" + objProcessInfo.ENDORSEMENT_NO + "&"
								+ "TRANS_ID=" + Session["EndorsementTranIds"].ToString() + "&', 'Endorsement',400, 200);"
								+ "</script>";
						else
							strOpenEndorsementWin = "<script>"
								+ "winEndorsement = ShowPopup('/cms/policies/processes/endorsementdetails.aspx?"
								+ "POLICY_ID=" + objProcessInfo.POLICY_ID + "&"
								//							+ "POLICY_VERSION_ID=" + objProcessInfo.POLICY_VERSION_ID + "&"
								// Changed by Swarup(07-feb-2008) as it is taking old version id when it should take new version id
								+ "POLICY_VERSION_ID=" + objProcessInfo.NEW_POLICY_VERSION_ID + "&"
								+ "CUSTOMER_ID=" + objProcessInfo.CUSTOMER_ID + "&"
								+ "ENDORSEMENT_NO=" + objProcessInfo.ENDORSEMENT_NO + "&"
								+ "TRANS_ID=0&', 'Endorsement',400, 200);"
								+ "</script>";
					

						ClientScript.RegisterStartupScript(this.GetType(),"OPENWINDOW",strOpenEndorsementWin);
					}
				}
			}
		}

	}
}
