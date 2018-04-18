using System;
using Cms.Policies;

namespace Cms.Policies.Processes
{
	/// <summary>
	/// Summary description for Processbase.
	/// </summary>
	public class Processbase:policiesbase
	{
		private int intPolQuote_ID=0,intShowQuote=0;
		
		public Processbase()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		// Check the mandatory info for policy
		protected bool VerifyPolicy(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID)
		{
			Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules(CarrierSystemID);
			string strCSSNo =base.GetColorScheme();
			string polLobID = GetLOBID();
			bool valid=false;
			string strRulesStatus="0";
			string strHTML=objRules.VerifyPolicy(CUSTOMER_ID,POLICY_ID ,POLICY_VERSION_ID,polLobID,out valid,strCSSNo,out strRulesStatus,"PROCESS");			
			if(!valid || strRulesStatus !="0")
			{
				ClientScript.RegisterHiddenField("hidHTML",strHTML);
				//Page.RegisterStartupScript("ShowVerifiyDialog","<script>ShowDialogEx();</script>");
				//Javascript
				string JavascriptText = "if(parent.refSubmitWin !=null)"
					+ "{"
					+ "	parent.refSubmitWin.close();"
					+ "}"
					+ "parent.refSubmitWin=window.open('/cms/application/Aspx/ShowDialog.aspx?CALLEDFROM=POLNEWBUSINESS','BRIC','resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');"
					+ "wBody=parent.refSubmitWin.document.body;"
					+ "parent.refSubmitWin.document.write(document.getElementById('hidHTML').value);"
					+ "parent.refSubmitWin.document.title = 'BRICS - Rules Mandatory Information';";
				ClientScript.RegisterStartupScript(this.GetType(),"ShowVerifiyDialog","<script>" + JavascriptText +"</script>");				
				
				valid=false;
			}
			else if(valid && strRulesStatus == "0")
				valid=true;			
			return valid;
		}

		// Check the mandatory info for policy
		//The following method is being commented here and instead moved to cmsbase so that it can be accessed from other parts of the system
//		protected string strHTML(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID,out bool valid,out string strRulesStatus)
//		{
//			Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules objRules = new Cms.BusinessLayer.BlApplication.ClsRatingAndUnderwritingRules();
//			string strCSSNo="",polLobID="",strHTML="";
//			
//			try
//			{
//				strCSSNo =base.GetColorScheme();polLobID = GetLOBID();
//						
//				strHTML=objRules.VerifyPolicy(CUSTOMER_ID,POLICY_ID ,POLICY_VERSION_ID,polLobID,out valid,strCSSNo,out strRulesStatus);			
//				return strHTML;
//			}
//			catch(Exception ex)
//			{
//				throw ex;
//			}
//			
//		}




		/// <summary>
		///  Get the policy Effective premium		/// </summary>
		/// <param name="CUSTOMER_ID"></param>
		/// <param name="POLICY_ID"></param>
		/// <param name="POLICY_VERSION_ID"></param>
		protected void GeneratePolicyQuote(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID, bool OnlyEffective)
		{
			
			Cms.BusinessLayer.BlQuote.ClsGenerateQuote objGenerateQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(CarrierSystemID);
			string strCSSNo =base.GetColorScheme();				
			string strpolLobID = GetLOBID();

			intShowQuote = objGenerateQuote.GeneratePolicyQuote(CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,strpolLobID,strCSSNo,out intPolQuote_ID,GetUserId());				
			
			string JavascriptText="";
            if (intShowQuote == 1 || intShowQuote == 2 || intShowQuote == 3 || intShowQuote == 6) //changed by Lalit ,sep 27, 2011.tfs # 860
			{
				JavascriptText = "window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + intPolQuote_ID + "&ONLYEFFECTIVE=" + OnlyEffective.ToString() + "&SHOW=" + intShowQuote + "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";

			}
			if(intShowQuote ==5)
			{
				JavascriptText="alert('This policy has been modified. Please verify policy again.');";
			}
			ClientScript.RegisterStartupScript(this.GetType(),"ShowPolicyPremium","<script>" + JavascriptText +"</script>");						
		}
		
		/// <summary>
		///  Get the policy premium
		/// </summary>
		/// <param name="CUSTOMER_ID"></param>
		/// <param name="POLICY_ID"></param>
		/// <param name="POLICY_VERSION_ID"></param>
		protected void GeneratePolicyQuote(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID)
		{
			
			GeneratePolicyQuote(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, false);
		}

		//Added By Ravindra (07-24-2006)

		protected string GeneratePolicyQuoteJS(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID, bool OnlyEffective)
		{
			
			Cms.BusinessLayer.BlQuote.ClsGenerateQuote objGenerateQuote = new Cms.BusinessLayer.BlQuote.ClsGenerateQuote(CarrierSystemID);
			string strCSSNo =base.GetColorScheme();				
			string strpolLobID = GetLOBID();

			intShowQuote = objGenerateQuote.GeneratePolicyQuote(CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,strpolLobID,strCSSNo,out intPolQuote_ID,GetUserId());				
			
			string JavascriptText="";
			if (intShowQuote ==1 || intShowQuote==2 || intShowQuote==3)
			{
				JavascriptText = "window.open('/cms/application/Aspx/Quote/Quote.aspx?CALLEDFROM=QUOTE_POL&CUSTOMER_ID=" + CUSTOMER_ID + "&POLICY_ID=" + POLICY_ID + "&POLICY_VERSION_ID=" + POLICY_VERSION_ID + "&LOBID=" + GetLOBID() + "&QUOTE_ID=" + intPolQuote_ID + "&ONLYEFFECTIVE=" + OnlyEffective.ToString() + "&SHOW=" + intShowQuote + "','Quote','resizable=yes,scrollbars=yes,left=150,top=50,width=700,height=600');";

			}
			if(intShowQuote ==5)
			{
				JavascriptText="alert('This policy has been modified. Please verify policy again.');";
			}
			return JavascriptText;
		}
		protected string GeneratePolicyQuoteJS(int CUSTOMER_ID,int POLICY_ID,int POLICY_VERSION_ID)
		{
			
			return GeneratePolicyQuoteJS(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, false);
		}
		//Added By Ravindra Ends Here

		
	}
}
