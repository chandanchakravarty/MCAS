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
using Cms.BusinessLayer.BlAccount;
using Cms.Model.Account;
using Cms.CmsWeb;
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon;
using Cms.BusinessLayer.BlCommon.Accounting;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for AddPlanBudget.
	/// </summary>
	public class AddPlanBudget : Cms.Account.AccountBase //System.Web.UI.Page//
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected System.Web.UI.WebControls.Label capGL_ID;
		protected System.Web.UI.WebControls.DropDownList cmbGL_ID;
		protected System.Web.UI.HtmlControls.HtmlTable tblBody;
		

		//START:*********** Local form variables *************
		string oldXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTotalAmt;
		protected System.Web.UI.WebControls.Label capBUDGET_CATEGORY_ID;
		protected System.Web.UI.WebControls.DropDownList cmbBUDGET_CATEGORY_ID;
	    //creating resource manager object (used for reading field and label mapping)
		private string strRowId;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDISTRIBUTE_AMOUNT;
		protected System.Web.UI.WebControls.TextBox txtJAN_BUDGET;
		protected System.Web.UI.WebControls.Label capJAN_BUDGET;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvJAN_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revJAN_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtFEB_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFEB_BUDGET;
		protected System.Web.UI.WebControls.Label capFEB_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtMARCH_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMARCH_BUDGET;
		protected System.Web.UI.WebControls.Label capMARCH_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtAPRIL_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAPRIL_BUDGET;
		protected System.Web.UI.WebControls.Label capAPRIL_BUDGET;
		protected System.Web.UI.WebControls.Label capMAY_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtMAY_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revMAY_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtJUNE_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revJUNE_BUDGET;
		protected System.Web.UI.WebControls.Label capJUNE_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtJULY_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revJULY_BUDGET;
		protected System.Web.UI.WebControls.Label capJULY_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtAUG_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revAUG_BUDGET;
		protected System.Web.UI.WebControls.Label capAUG_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtSEPT_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revSEPT_BUDGET;
		protected System.Web.UI.WebControls.Label capSEPT_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtOCT_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revOCT_BUDGET;
		protected System.Web.UI.WebControls.Label capOCT_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtNOV_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revNOV_BUDGET;
		protected System.Web.UI.WebControls.Label capNOV_BUDGET;
		protected System.Web.UI.WebControls.TextBox txtDEC_BUDGET;
		protected System.Web.UI.WebControls.Label capDEC_BUDGET;
		protected System.Web.UI.WebControls.RegularExpressionValidator revDEC_BUDGET;
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvGL_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBUDGET_CATEGORY_ID;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvDISTRIBUTE_AMOUNT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIDEN_ROW_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBudgetCategory;
		protected System.Web.UI.WebControls.TextBox txtDISTRIBUTE_AMOUNT;
		protected System.Web.UI.WebControls.Label capDISTRIBUTE_AMOUNT;
		protected System.Web.UI.WebControls.Label capTOTAL_AMOUNT;
		protected Cms.CmsWeb.Controls.CmsButton btnDistribute;
		protected System.Web.UI.WebControls.Label lblTOTAL_AMOUNT;
		
		
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAY_BUDGET;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		//private void SetErrorMessages()
		//{
		//	rfvGL_ID.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"978");
		//}
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvGL_ID.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"978");
			rfvBUDGET_CATEGORY_ID.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1055");
			//rfvDISTRIBUTE_AMOUNT.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1010");
			//rfvJAN_BUDGET.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			//rfvFEB_BUDGET.ErrorMessage		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
		  //revJAN_BUDGET.ValidationExpression  = aRegExpDoublePositiveNonZero;
			//revDISTRIBUTE_AMOUNT.ValidationExpression = aRegExpInteger;
			revDISTRIBUTE_AMOUNT.ValidationExpression = aRegExpIntegerPositiveWithMoreLength;
			revJAN_BUDGET.ValidationExpression	=  aRegExpIntegerPositiveWithMoreLength;
			
			//revJAN_BUDGET.ValidationExpression = aRegExpDoublePositiveNonZeroStartWithZero ;
			//revJAN_BUDGET.ValidationExpression =  aRegExpCurrencyformat;			
			//Commented and Added a new key For Itrack Issue 6366.   	
			//revFEB_BUDGET.ValidationExpression   =  aRegExpIntegerPositiveNonZero;
			revFEB_BUDGET.ValidationExpression   =  aRegExpIntegerPositiveWithMoreLength;
			revMARCH_BUDGET.ValidationExpression =  aRegExpIntegerPositiveWithMoreLength;
			revAPRIL_BUDGET.ValidationExpression =  aRegExpIntegerPositiveWithMoreLength;
			revMAY_BUDGET.ValidationExpression   =  aRegExpIntegerPositiveWithMoreLength;
			revJUNE_BUDGET.ValidationExpression  =  aRegExpIntegerPositiveWithMoreLength;
			revJULY_BUDGET.ValidationExpression  =  aRegExpIntegerPositiveWithMoreLength;
			revAUG_BUDGET.ValidationExpression   =  aRegExpIntegerPositiveWithMoreLength;
			revSEPT_BUDGET.ValidationExpression  =  aRegExpIntegerPositiveWithMoreLength;
			revOCT_BUDGET.ValidationExpression   =  aRegExpIntegerPositiveWithMoreLength;
			revNOV_BUDGET.ValidationExpression   =  aRegExpIntegerPositiveWithMoreLength;
			revDEC_BUDGET.ValidationExpression   =  aRegExpIntegerPositiveWithMoreLength;

		}
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnReset.Attributes.Add("onclick","javascript:return ResetBudgetPlan();");
			txtJAN_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtFEB_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtMARCH_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtAPRIL_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtMAY_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtJUNE_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtJULY_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtAUG_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtSEPT_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtOCT_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtNOV_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtDEC_BUDGET.Attributes.Add("onblur","javascript:FormatAmount(this);recalculated();");
			txtDISTRIBUTE_AMOUNT.Attributes.Add("onblur","javascript:FormatAmount(this);");
			btnDistribute.Attributes.Add("OnClick","javascript:if(fnDistributeAmount() == false) return false;");          			
			base.ScreenId="405_0";
			SetErrorMessages();
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			btnSave.CmsButtonClass			=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;
			btnDistribute.CmsButtonClass	=	CmsButtonType.Execute;
			btnDistribute.PermissionString	=	gstrSecurityXML;
			objResourceMgr = new System.Resources.ResourceManager("Cms.Account.Aspx.AddPlanBudget" ,System.Reflection.Assembly.GetExecutingAssembly());
			
			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{	
				//hidBudgetCategory.Value =ClsBudgetPlan.				
				Fillcombo();		
				SetFiscalYear();
				if(Request.QueryString["IDEN_ROW_ID"] != null)
					hidIDEN_ROW_ID.Value = Request.QueryString["IDEN_ROW_ID"].ToString();
				if(hidIDEN_ROW_ID.Value != "" && hidIDEN_ROW_ID.Value != "0")
					hidFormSaved.Value = "1";				
				hidOldData.Value = ClsBudgetPlan.GetXmlForPageControls(Request.QueryString["IDEN_ROW_ID"]);
				hidTotalAmt.Value = ClsCommon.FetchValueFromXML("TOTAL_CALCULATION_AMT",hidOldData.Value.ToString()); 
				SetCaptions();
         //     btnDistribute.Attributes.Add("OnClick","javascript:if(fnDistributeAmount() == false) return false;");
			}
		}
		#region FILL COMBO
		private void Fillcombo()
		{
			//Filling GL fiscal 					
			Cms.BusinessLayer.BlCommon.Accounting.ClsGeneralLedger.GetGeneralLedgerIndropDown(cmbGL_ID);
			cmbGL_ID.Items.Insert(0,new ListItem("",""));
			//Fill Drop Down List of Budget Category
			ClsBudgetCategory objBudgetCategory = new ClsBudgetCategory();
			objBudgetCategory.GetBudgetCategoryInDropdown(cmbBUDGET_CATEGORY_ID);
			
            
		}
		#endregion
		#region Set Caption from RESX
		private void SetCaptions()
		{

			capGL_ID.Text						=		objResourceMgr.GetString("cmbGL_ID");
			capBUDGET_CATEGORY_ID.Text			=		objResourceMgr.GetString("cmbBUDGET_CATEGORY_ID");
			capJAN_BUDGET.Text					=		objResourceMgr.GetString("txtJAN_BUDGET");
			capFEB_BUDGET.Text					=		objResourceMgr.GetString("txtFEB_BUDGET");
			capMARCH_BUDGET.Text				=		objResourceMgr.GetString("txtMARCH_BUDGET");
			capAPRIL_BUDGET.Text				=		objResourceMgr.GetString("txtAPRIL_BUDGET");
			capMAY_BUDGET.Text					=		objResourceMgr.GetString("txtMAY_BUDGET");
			capJUNE_BUDGET.Text					=		objResourceMgr.GetString("txtJUNE_BUDGET");
			capJULY_BUDGET.Text					=		objResourceMgr.GetString("txtJULY_BUDGET");
			capAUG_BUDGET.Text					=		objResourceMgr.GetString("txtAUG_BUDGET");
			capSEPT_BUDGET.Text					=		objResourceMgr.GetString("txtSEPT_BUDGET");
			capOCT_BUDGET.Text					=		objResourceMgr.GetString("txtOCT_BUDGET");
			capNOV_BUDGET.Text					=		objResourceMgr.GetString("txtNOV_BUDGET");
			capDEC_BUDGET.Text					=		objResourceMgr.GetString("txtDEC_BUDGET");
			capDISTRIBUTE_AMOUNT.Text			=		objResourceMgr.GetString("txtDISTRIBUTE_AMOUNT");
			capTOTAL_AMOUNT.Text				=		objResourceMgr.GetString("lblTOTAL_AMOUNT");

		}
		#endregion
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsBudgetPlanInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsBudgetPlanInfo objBudgetPlanInfo;
			objBudgetPlanInfo = new ClsBudgetPlanInfo();
			
			//if(cmbGL_ID.SelectedValue != null && cmbGL_ID.SelectedValue != "")
			string [] arrRows = cmbGL_ID.SelectedValue.Split('-');
				objBudgetPlanInfo.GL_ID = int.Parse(arrRows[0]);
				objBudgetPlanInfo.FISCAL_ID = int.Parse(arrRows[1]);
			
			//objBudgetPlanInfo.BUDGET_CATEGORY_ID = Convert.ToInt32(cmbBUDGET_CATEGORY_ID.SelectedValue);
			
			objBudgetPlanInfo.ACCOUNT_ID = Convert.ToInt32(cmbBUDGET_CATEGORY_ID.SelectedValue);

			if(txtJAN_BUDGET.Text!="")
				objBudgetPlanInfo.JAN_BUDGET   =double.Parse(txtJAN_BUDGET.Text.Replace(",",""));			
//          	else 		
//				objBudgetPlanInfo.JAN_BUDGET = double.Parse(null);
			if(txtFEB_BUDGET.Text!="")
				objBudgetPlanInfo.FEB_BUDGET   =	double.Parse(txtFEB_BUDGET.Text.Replace(",",""));
			//else 		
			//	objBudgetPlanInfo.FEB_BUDGET = -1;

			if(txtMARCH_BUDGET.Text!="")
				objBudgetPlanInfo.MARCH_BUDGET   =	double.Parse(txtMARCH_BUDGET.Text.Replace(",",""));	
//			else 		
//				objBudgetPlanInfo.MARCH_BUDGET = double.Parse(null);
			if(txtAPRIL_BUDGET.Text!="")
				objBudgetPlanInfo.APRIL_BUDGET   =	double.Parse(txtAPRIL_BUDGET.Text.Replace(",",""));
//			else 		
//				objBudgetPlanInfo.APRIL_BUDGET = double.Parse(null);
			if(txtMAY_BUDGET.Text!="")
				objBudgetPlanInfo.MAY_BUDGET   =	double.Parse(txtMAY_BUDGET.Text.Replace(",",""));
//			else 		
//				objBudgetPlanInfo.MAY_BUDGET = double.Parse(null);
			if(txtJUNE_BUDGET.Text!="")
				objBudgetPlanInfo.JUNE_BUDGET   =	double.Parse(txtJUNE_BUDGET.Text.Replace(",",""));
//			else 		
//				objBudgetPlanInfo.JUNE_BUDGET = double.Parse(null);
			if(txtJULY_BUDGET.Text!="")
				objBudgetPlanInfo.JULY_BUDGET  =	double.Parse(txtJULY_BUDGET.Text.Replace(",",""));	
//			else 		
//				objBudgetPlanInfo.JULY_BUDGET = double.Parse(null);
			if(txtAUG_BUDGET.Text!="")
				objBudgetPlanInfo.AUG_BUDGET   =	double.Parse(txtAUG_BUDGET.Text.Replace(",",""));
//			else 		
//				objBudgetPlanInfo.AUG_BUDGET = double.Parse(null);
			if(txtSEPT_BUDGET.Text!="")
				objBudgetPlanInfo.SEPT_BUDGET   =	double.Parse(txtSEPT_BUDGET.Text.Replace(",",""));
//			else 		
//				objBudgetPlanInfo.SEPT_BUDGET = double.Parse(null);
			if(txtOCT_BUDGET.Text!="")
				objBudgetPlanInfo.OCT_BUDGET   =	double.Parse(txtOCT_BUDGET.Text.Replace(",",""));
//			else 		
//				objBudgetPlanInfo.OCT_BUDGET = double.Parse(null);
			if(txtNOV_BUDGET.Text!="")
				objBudgetPlanInfo.NOV_BUDGET   =	double.Parse(txtNOV_BUDGET.Text.Replace(",",""));
//			else 		
//				objBudgetPlanInfo.NOV_BUDGET = double.Parse(null);
			if(txtDEC_BUDGET.Text!="")
				objBudgetPlanInfo.DEC_BUDGET   =	double.Parse(txtDEC_BUDGET.Text.Replace(",",""));
//			else 		
//				objBudgetPlanInfo.DEC_BUDGET = double.Parse(null);
			
			


			strRowId		=	hidGL_ID.Value;
			oldXML		   = hidOldData.Value;
			return objBudgetPlanInfo;

		}
		# endregion 

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
			this.btnDistribute.Click += new System.EventHandler(this.btnDistribute_Click);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void SetFiscalYear()
		{
			try
			{
				DateTime tranDate = DateTime.Now;
				string fdate;

				for(int ctr = 0; ctr < cmbGL_ID.Items.Count; ctr++)
				{
					fdate = cmbGL_ID.Items[ctr].Text;
				
					if (fdate.Trim() == "")
					{
						continue;
					}
			
					//Getting the financial dates, from financial year
					string d1 = fdate.Substring(fdate.IndexOf("(") + 1, 11);
					string d2 = fdate.Substring(fdate.IndexOf("-") + 1, 11);
				
					if (tranDate >= DateTime.Parse(d1) && tranDate <= DateTime.Parse(d2))		
					{
						//Transaction date is in between financial dates
						//Hence selecting this fiscal year
						cmbGL_ID.SelectedIndex = ctr;
						break;
					}	
				}
			}
			catch (Exception objExp)
			{
				lblMessage.Text = objExp.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExp);
			}
		}

			
		
		#region SAVE
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				//For retreiving the return value of business class save function
				int intRetVal=0;
				//Retreiving the form values into model class object
				ClsBudgetPlanInfo objBudgetPlanInfo;
				objBudgetPlanInfo=GetFormValue();

				ClsBudgetPlan objBudgetPlan = new ClsBudgetPlan();
				if (hidFormSaved.Value=="0") //save data
				{
					//Calling the add method of business layer class
					objBudgetPlanInfo.CREATED_BY = int.Parse(GetUserId());
					intRetVal=objBudgetPlan.Add(objBudgetPlanInfo);

					if(intRetVal>0)
					{
						hidGL_ID.Value = objBudgetPlanInfo.GL_ID.ToString();
						hidIDEN_ROW_ID.Value = objBudgetPlanInfo.IDEN_ROW_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIDEN_ROW_ID.Value=objBudgetPlanInfo.IDEN_ROW_ID.ToString();
						hidOldData.Value = ClsBudgetPlan.GetXmlForPageControls(objBudgetPlanInfo.IDEN_ROW_ID.ToString());
						hidTotalAmt.Value = ClsCommon.FetchValueFromXML("TOTAL_CALCULATION_AMT",hidOldData.Value.ToString()); 
						//CheckInvoiceStatus(hidGL_ID.Value);
					}
					else if(intRetVal==-2)
					{
						lblMessage.Text			=	"Plan Budget for this budget category & fiscal period already exists.";
						hidFormSaved.Value			=	"0";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
					
				}
				else  //update data
				{
					//Creating the Model object for holding the Old data
					if (hidIDEN_ROW_ID.Value!="" && hidIDEN_ROW_ID.Value !="0")
					{
						ClsBudgetPlanInfo objOldBudgetPlanInfo;
						objOldBudgetPlanInfo = new ClsBudgetPlanInfo();
						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldBudgetPlanInfo,hidOldData.Value);
						string GLeadger = cmbGL_ID.Items[cmbGL_ID.SelectedIndex].Text;
						string BudgetCategory = cmbBUDGET_CATEGORY_ID.Items[cmbBUDGET_CATEGORY_ID.SelectedIndex].Text;
						string GL_ID = Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("GL_ID",hidOldData.Value);
						GL_ID = GL_ID.Split('-')[0];

						objOldBudgetPlanInfo.GL_ID = int.Parse(GL_ID);
						//Setting those values into the Model object which are not in the page
						objBudgetPlanInfo.IDEN_ROW_ID = int.Parse(hidIDEN_ROW_ID.Value);
						objBudgetPlanInfo.MODIFIED_BY = int.Parse(GetUserId());
						//Updating the record using business layer class object
						intRetVal=objBudgetPlan.Update(objOldBudgetPlanInfo,objBudgetPlanInfo,GLeadger,BudgetCategory);
						if( intRetVal > 0 )			// update successfully performed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value		=	"1";
							hidOldData.Value = ClsBudgetPlan.GetXmlForPageControls(objBudgetPlanInfo.IDEN_ROW_ID.ToString());
							hidTotalAmt.Value = ClsCommon.FetchValueFromXML("TOTAL_CALCULATION_AMT",hidOldData.Value.ToString()); 
						
							//CheckInvoiceStatus(hidIDEN_ROW_ID.Value);
						}
						else if(intRetVal==-2)
						{
							lblMessage.Text			=	"Plan Budget for this budget category & fiscal period already exists.";
							hidFormSaved.Value			=	"2";
						}
						else 
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
							hidFormSaved.Value		=	"1";
						}
						hidOldData.Value = ClsBudgetPlan.GetXmlForPageControls(objBudgetPlanInfo.IDEN_ROW_ID.ToString());
						lblMessage.Visible = true;
					}
					else
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					
				}

			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//ExceptionManager.Publish(ex);
			}
			finally
			{
//				if(objBudgetPlan!= null)
//					objBudgetPlan.Dispose();
			}
			#endregion



		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
		}

		private void btnDistribute_Click(object sender, System.EventArgs e)
		{
		
		}

//		private void btnDelete_Click(object sender, System.EventArgs e)
//		{
//		}
			/*try
			{
				/*Deleting the whole record
				objBudgetPlan = new ClsBudgetPlan();
				int intRetVal = objBudgetPlan.Delete(int.Parse(hidGL_ID.Value),int.Parse(GetUserId()),txtJOURNAL_ENTRY_NO.Text);

				if (intRetVal > 0)
				{
					//Deleted successfully
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "127");
					hidIDEN_ROW_ID.Value = "";
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					tblBody.Attributes.Add("style","display:none");
				}
				else if(intRetVal == -2)
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"6");
					hidFormSaved.Value		=	"2";
				}
				else
				{
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "128");
					hidFormSaved.Value = "2";
				}
				lblMessage.Visible = true;
			}
			catch (Exception objEx)
			{
				lblMessage.Text = objEx.Message.ToString();
				lblMessage.Visible = true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objEx);
			}
		} */
	}
}
#endregion

