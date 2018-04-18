/******************************************************************************************
<Author					: -		Ajit Singh Chahal
<Start Date				: -		5/16/2005 3:34:53 PM
<End Date				: -	
<Description			: - 	Code behind for Add General Ledger.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: -		Code behind for Add General Ledger.
*******************************************************************************************/ 
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
using Cms.CmsWeb;
using System.Resources; 
using System.Reflection; 
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher.ExceptionManagement; 



namespace  Cms.CmsWeb.Maintenance.Accounting
{
		/// <summary>
		/// Code behind for Add General Ledger.
		/// </summary>
		public class AddGeneralLedgers : Cms.CmsWeb.cmsbase 
		{
			#region Page controls declaration
			protected System.Web.UI.WebControls.TextBox txtLEDGER_NAME;
			protected System.Web.UI.WebControls.CheckBox chkFORBID_POSTING;
			protected System.Web.UI.WebControls.TextBox txtSMALL_BALANCE;

			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
            protected System.Web.UI.HtmlControls.HtmlInputHidden hiddentab; 
            protected System.Web.UI.HtmlControls.HtmlInputHidden hdientab_LockPosting;
			protected Cms.CmsWeb.Controls.CmsButton btnReset;
			protected Cms.CmsWeb.Controls.CmsButton btnSave;
			protected Cms.CmsWeb.Controls.CmsButton btnCreateNewFiscalYear;

			protected System.Web.UI.WebControls.RequiredFieldValidator rfvLEDGER_NAME;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvFISCAL_BEGIN_DATE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvFISCAL_END_DATE;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvMONTH_BEGINING;
			protected System.Web.UI.WebControls.RegularExpressionValidator revSMALL_BALANCE;
			protected System.Web.UI.WebControls.Label lblMessage;
            protected System.Web.UI.WebControls.Label capCHATACC;  
            protected System.Web.UI.WebControls.Label capMANDATORY; 


			#endregion
			#region Local form variables
			//START:*********** Local form variables *************
			string oldXML;
			//creating resource manager object (used for reading field and label mapping)
			System.Resources.ResourceManager objResourceMgr;
			private string strRowId, strFormSaved;
			//private int	intLoggedInUserID;
			protected System.Web.UI.WebControls.Label capLEDGER_NAME;
			protected System.Web.UI.WebControls.Label capFISCAL_BEGIN_DATE;
			protected System.Web.UI.WebControls.Label capFISCAL_END_DATE;
			protected System.Web.UI.WebControls.Label capMONTH_BEGINING;
			protected System.Web.UI.WebControls.Label capSMALL_BALANCE;
			protected System.Web.UI.WebControls.Label capFORBID_POSTING;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFISCAL_ID;
			protected System.Web.UI.WebControls.Label Label1;
			protected System.Web.UI.WebControls.DropDownList cmbFiscalYearFrom;
			protected System.Web.UI.WebControls.DropDownList cmbMONTH_BEGINING;
			protected System.Web.UI.WebControls.DropDownList cmbFISCAL_BEGIN_MONTH;
			protected System.Web.UI.WebControls.DropDownList cmbFISCAL_END_MONTH;
			protected System.Web.UI.WebControls.TextBox txtFiscalYearFrom;
			protected System.Web.UI.HtmlControls.HtmlForm ACT_GENERAL_LEDGER;
			protected System.Web.UI.WebControls.Label lblFISCAL_BEGIN_DATE;
			protected System.Web.UI.WebControls.Label lblFISCAL_END_DATE;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFISCAL_END_DATE;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFISCAL_BEGIN_DATE;
			protected System.Web.UI.WebControls.CustomValidator csvMONTH_BEGINING;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvFiscalYearFrom;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidGL_ID;
			protected Cms.CmsWeb.Controls.CmsButton btnPrint;
			//Defining the business layer class object
			ClsGeneralLedger  objGeneralLedger ;
			//END:*********** Local variables *************

			#endregion
			#region Set Validators ErrorMessages
			/// <summary>
			/// Method to set validation control error masessages.
			/// Parameters: none
			/// Return Type: none
			/// </summary>
			private void SetErrorMessages()
			{
				rfvLEDGER_NAME.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				rfvFISCAL_BEGIN_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
				rfvFISCAL_END_DATE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
				rfvMONTH_BEGINING.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
				rfvFiscalYearFrom.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");

				//revSMALL_BALANCE.ValidationExpression		=	aRegExpCurrencyformat;

                revSMALL_BALANCE.ValidationExpression = aRegExpDoublePositiveNonZero;  //Changed by Aditya on 17-oct-2011 for TFS Bug # 1844
				revSMALL_BALANCE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
               
				csvMONTH_BEGINING.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			}
			#endregion
			#region PageLoad event
			private void Page_Load(object sender, System.EventArgs e)
			{
				
				if(Request.QueryString["mode"]!=null && Request.QueryString["mode"].ToString().ToUpper().Equals("NEWFISCALYEAR"))
				{//*****************add new fiscal year mode********************
					base.ScreenId	=	"125_0_0";
				}
				else
				{
					  base.ScreenId	=	"125_0";
				}
				btnReset.Attributes.Add("onclick","javascript:document.getElementById('hidFormSaved').value = '0';return ResetScreen();");
				btnCreateNewFiscalYear.Attributes.Add("onclick","javascript:return ShowCreateNewFiscalYear();");
				btnPrint.Attributes.Add("onclick","javascript: return ShowPrint();");
                if (GetLanguageID() == "2")   //Added by Aditya on 17-oct-2011 for TFS Bug # 1844
                {
                    txtSMALL_BALANCE.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,2);formatRateBase(this.value);ValidatorOnChange();");
                }
                else
                {
                    txtSMALL_BALANCE.Attributes.Add("onblur", "javascript:this.value=formatRateTextValue(this.value,1);formatCurrencyRate(this.value);ValidatorOnChange();");
                }
          
				lblMessage.Visible = false;
               	SetErrorMessages();

               	//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass						=	CmsButtonType.Write;
				btnReset.PermissionString					=	gstrSecurityXML;

				btnCreateNewFiscalYear.CmsButtonClass		=	CmsButtonType.Write;
				btnCreateNewFiscalYear.PermissionString		=	gstrSecurityXML;

				btnPrint.CmsButtonClass						=	CmsButtonType.Execute;
				btnPrint.PermissionString					=	gstrSecurityXML;

				btnSave.CmsButtonClass						=	CmsButtonType.Write;
				btnSave.PermissionString					=	gstrSecurityXML;

				//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.AddGeneralLedgers" ,System.Reflection.Assembly.GetExecutingAssembly());
				if(!Page.IsPostBack)
				{	
			  
					//added by uday to select fiscal year	
					string fiscalYear="";
				//	string CurYear = DateTime.Now.Year.ToString();
					
					fiscalYear = "<script> " + 
						" var CurDate  = new Date(); "+
						" var CurYear  = CurDate.getUTCFullYear(); "+
						" var cmbFisYr = document.getElementById('cmbFiscalYearFrom'); "+
						" if(cmbFisYr != null && cmbFisYr.options.length>0)" + 
						"{ "+
						" SelectComboOptionByText('cmbFiscalYearFrom',CurYear); "+
						" var cmbLastFiscalYr = document.getElementById('cmbFiscalYearFrom').options.lastChild.innerText; "+
						" if(cmbLastFiscalYr != CurYear) "+
						" document.getElementById('cmbFISCAL_END_MONTH').setAttribute('disabled',true); "+
						" } "+
						"</script> ";
                    ClientScript.RegisterStartupScript(this.GetType(),"Test", fiscalYear);
					
					//RegisterStartupScript("Test","<script>CheckIfPopup();</script>");
					//uday
					if(Request.QueryString["mode"]!=null && Request.QueryString["mode"].ToString().ToUpper().Equals("NEWFISCALYEAR"))
					{//*****************add new fiscal year mode********************
						
						cmbFiscalYearFrom.Visible = false;
						txtFiscalYearFrom.Visible = true;
						hidOldData.Value = ClsGeneralLedger.GetXmlForPageControlsForNewFiscalYear();
						txtLEDGER_NAME.Text = Request.QueryString["LEDGER_NAME"];
						txtLEDGER_NAME.ReadOnly = true;
						cmbFISCAL_BEGIN_MONTH.Enabled = false;
						btnCreateNewFiscalYear.Visible = false;
					}
					else
					{
						
						fiscalYear = DateTime.Now.Year.ToString();//current year
						string GL_ID="",FISCAL_ID="";
						ClsGeneralLedger.GetFiscalYearsIndropDown(cmbFiscalYearFrom);
						hidOldData.Value = ClsGeneralLedger.GetXmlForPageControls(fiscalYear,ref GL_ID,ref FISCAL_ID);
						if(hidOldData.Value=="" && cmbFiscalYearFrom.Items.Count<=0)
						{
							cmbFiscalYearFrom.Items.Add(fiscalYear);
						}
						else
						{
							Session["GL_ID"] = GL_ID;
							Session["FISCAL_ID"] = FISCAL_ID;
						}
													
					}
                    hiddentab.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1365");
                    hdientab_LockPosting.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1368");
					FillMonths();
					SetCaptions();					
				}
				else
				{
					
				}
               
			}//end pageload
			#endregion
            private void FillMonths()
            {
                if (ClsCommon.BL_LANG_ID == 2)
                {
                    string[] months = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "October", "Novembro ", "Dezembro" };
                    //	cmbFISCAL_BEGIN_MONTH.Items.Add("");//adding blank
                    for (int i = 0; i < 12; i++)
                    {
                        cmbFISCAL_BEGIN_MONTH.Items.Add(new ListItem(months[i], (i + 1).ToString()));
                        cmbMONTH_BEGINING.Items.Add(new ListItem(months[i], (i + 1).ToString()));
                    }
                    //	cmbFISCAL_END_MONTH.Items.Add("");//adding blank

                    for (int i = 3; i <= 12; i += 3)
                    {
                        cmbFISCAL_END_MONTH.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }

                   
                }
                else
                {
                    string[] months = { "January", "Febuary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                    //	cmbFISCAL_BEGIN_MONTH.Items.Add("");//adding blank
                    for (int i = 0; i < 12; i++)
                    {
                        cmbFISCAL_BEGIN_MONTH.Items.Add(new ListItem(months[i], (i + 1).ToString()));
                        cmbMONTH_BEGINING.Items.Add(new ListItem(months[i], (i + 1).ToString()));
                    }
                    //	cmbFISCAL_END_MONTH.Items.Add("");//adding blank

                    for (int i = 3; i <= 12; i += 3)
                    {
                        cmbFISCAL_END_MONTH.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                }
            }

			#region GetFormValue
			/// <summary>
			/// Fetch form's value and stores into model class object and return that object.
			/// </summary>
			private ClsGeneralLedgerInfo GetFormValue()
			{
				//Creating the Model object for holding the New data
				ClsGeneralLedgerInfo objGeneralLedgerInfo;
				objGeneralLedgerInfo = new ClsGeneralLedgerInfo();
                SetCultureThread(GetLanguageCode());
				objGeneralLedgerInfo.LEDGER_NAME			=	txtLEDGER_NAME.Text;
				objGeneralLedgerInfo.FISCAL_BEGIN_DATE		=   ConvertToDate(hidFISCAL_BEGIN_DATE.Value);//DateTime.Parse(month+"/1/"+year);
				objGeneralLedgerInfo.FISCAL_END_DATE		=	ConvertToDate(hidFISCAL_END_DATE.Value);//DateTime.Parse(month+"/"+days+"/"+(year+1));
	

				objGeneralLedgerInfo.MONTH_BEGINING			=	int.Parse(cmbMONTH_BEGINING.SelectedValue);
				if(chkFORBID_POSTING.Checked)
					objGeneralLedgerInfo.FORBID_POSTING		=	"Y";
				else
					objGeneralLedgerInfo.FORBID_POSTING		=	"N";

                if (txtSMALL_BALANCE.Text != "")   //Changed by Aditya on 17-oct-2011 for TFS Bug # 1844
                {
                    if (GetLanguageID() == "2")
                    {
                        objGeneralLedgerInfo.SMALL_BALANCE = Convert.ToDouble(txtSMALL_BALANCE.Text, NfiBaseCurrency);
                    }
                    else
                    {
                        objGeneralLedgerInfo.SMALL_BALANCE = Convert.ToDouble(txtSMALL_BALANCE.Text);
                    }
                }

				//These  assignments are common to all pages.
				strFormSaved	=	hidFormSaved.Value;
				strRowId		=	hidFISCAL_ID.Value;
				oldXML			=	hidOldData.Value;
				//Returning the model object

				return objGeneralLedgerInfo;
			}
			
			#endregion

			#region Web Form Designer generated code
			override protected void OnInit(EventArgs e)
			{
				InitializeComponent();
				base.OnInit(e);
			}
			private void InitializeComponent()
			{
				this.cmbFiscalYearFrom.SelectedIndexChanged += new System.EventHandler(this.cmbFiscalYearFrom_SelectedIndexChanged);
				this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
				this.Load += new System.EventHandler(this.Page_Load);

			}
			#endregion


			#region "Web Event Handlers"
			/// <summary>
			/// If form is posted back then add entry in database using the BL object
			/// </summary>
			/// <param name="sender"></param>
			/// <param name="e"></param>
			private void btnSave_Click(object sender, System.EventArgs e)
			{
				try
				{
			
						int intRetVal;	//For retreiving the return value of business class save function
						objGeneralLedger = new  ClsGeneralLedger(true);

						//Retreiving the form values into model class object
						ClsGeneralLedgerInfo objGeneralLedgerInfo = GetFormValue();

						if(strRowId.ToUpper().Equals("NEW")) //save case
						{
							objGeneralLedgerInfo.CREATED_BY			   = int.Parse(GetUserId());
							objGeneralLedgerInfo.CREATED_DATETIME	   = DateTime.Now;
							objGeneralLedgerInfo.MODIFIED_BY		   = objGeneralLedgerInfo.CREATED_BY;
							objGeneralLedgerInfo.LAST_UPDATED_DATETIME = objGeneralLedgerInfo.CREATED_DATETIME;
							objGeneralLedgerInfo.IS_ACTIVE			   = "Y";

							//Calling the add method of business layer class
							intRetVal = objGeneralLedger.Add(objGeneralLedgerInfo);

							if(intRetVal>0)
							{
								hidFISCAL_ID.Value			=	objGeneralLedgerInfo.GL_ID.ToString();
								lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
								hidFormSaved.Value		=	"1";
								GetRefreshedData();	
								Response.Write("<script>if(window.opener!=null)window.opener.location=window.opener.location;</script>");
							}
							else
							{
								lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
								hidFormSaved.Value		=	"2";
							}
							lblMessage.Visible = true;
						} //end save case
						else //UPDATE CASE
						{

							//Creating the Model object for holding the Old data
							ClsGeneralLedgerInfo objOldGeneralLedgerInfo;
							objOldGeneralLedgerInfo = new ClsGeneralLedgerInfo();

							//Setting  the Old Page details(XML File containing old details) into the Model Object
							base.PopulateModelObject(objOldGeneralLedgerInfo,hidOldData.Value);

							//Setting those values into the Model object which are not in the page
							objGeneralLedgerInfo.FISCAL_ID = int.Parse(strRowId);
							objGeneralLedgerInfo.GL_ID = int.Parse(hidGL_ID.Value);
							objGeneralLedgerInfo.MODIFIED_BY = int.Parse(GetUserId());
							objGeneralLedgerInfo.LAST_UPDATED_DATETIME = DateTime.Now;
							//Updating the record using business layer class object
							intRetVal	= objGeneralLedger.Update(objOldGeneralLedgerInfo,objGeneralLedgerInfo);
							if( intRetVal > 0 )			// update successfully performed
							{
								lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
								hidFormSaved.Value		=	"1";
								GetRefreshedData();	
								Response.Write("<script>if(window.opener!=null)window.opener.location=window.opener.location;</script>");
							}
							else 
							{
                                lblMessage.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1531");//"Fiscal Period could not be saved.Transactions exist beyond the given end date.";
								lblMessage.Visible		=   true;
								hidFormSaved.Value		=	"2";
							}
							lblMessage.Visible = true;
						}
					}
				catch(Exception ex)
				{
					lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
					lblMessage.Visible	=	true;
					hidFormSaved.Value			=	"2";
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				}
				finally
				{
					if(objGeneralLedger!= null)
						objGeneralLedger.Dispose();
				}
			}

			#endregion
			private void GetRefreshedData()
			{
				string fiscalYear="";
				if(Request.Form["cmbFiscalYearFrom"]==null)
					fiscalYear = Request.Form["txtFiscalYearFrom"].ToString();
				else
					fiscalYear = cmbFiscalYearFrom.SelectedItem.Text;
				//ClsGeneralLedger.GetFiscalYearsIndropDown(cmbFiscalYearFrom);
				hidOldData.Value = ClsGeneralLedger.GetXmlForPageControls(fiscalYear);

			}
			private void SetCaptions()
			{

				capLEDGER_NAME.Text							=		objResourceMgr.GetString("txtLEDGER_NAME");
				capFISCAL_BEGIN_DATE.Text					=		objResourceMgr.GetString("txtFISCAL_BEGIN_DATE");
				capFISCAL_END_DATE.Text						=		objResourceMgr.GetString("txtFISCAL_END_DATE");
				capMONTH_BEGINING.Text						=		objResourceMgr.GetString("txtMONTH_BEGINING");
				capFORBID_POSTING.Text						=		objResourceMgr.GetString("chkFORBID_POSTING");
				capSMALL_BALANCE.Text						=		objResourceMgr.GetString("txtSMALL_BALANCE");
                capCHATACC.Text                             =       objResourceMgr.GetString("capCHATACC"); 
                btnCreateNewFiscalYear.Text                 =       Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1337"); 
                capMANDATORY.Text                           =       Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
                Label1.Text = objResourceMgr.GetString("Label1");
			}

			private void cmbFiscalYearFrom_SelectedIndexChanged(object sender, System.EventArgs e)
			{
				//string fiscalYear	 = Request.Form["cmbFiscalYearFrom"];
				hidOldData.Value	 = ClsGeneralLedger.GetXmlForPageControls(cmbFiscalYearFrom.SelectedItem.Text);
				hidFormSaved.Value	 = "0";
				Session["FISCAL_ID"] = cmbFiscalYearFrom.SelectedValue;
			}
		}
	}
