/******************************************************************************************
<Author					: - Pravesh Chandel
<Start Date				: -	27/10/2006 5:11:42 PM
<End Date				: -	
<Description			: - Code behind for Minimum Premium
<Review Date			: - 
<Reviewed By			: - 	
Modification History
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
using Cms.Model.Maintenance;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.BusinessLayer.BlCommon;  
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher.ExceptionManagement; 

namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Summary description for AddMinimumPremAmt.
	/// </summary>
	public class AddMinimumPremAmt :   Cms.CmsWeb.cmsbase
	{
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		string oldXML;
        //Constants 
        //Changed by Amit mishra for Tfs Bug #836
		string COUNTRYID = "1";//countryid hard coded for USA for save case 
		string COUNTRY_NAME="USA";
        protected System.Web.UI.WebControls.Label capMessages;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label lblCOUNTRY_NAME;
		protected System.Web.UI.WebControls.Label capSTATE_ID;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSTATE_ID;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		protected System.Web.UI.WebControls.Label capSUB_LOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbSUB_LOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvSUB_LOB_ID;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkFROM_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_FROM_DATE;
		protected System.Web.UI.WebControls.Label capEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkTO_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_TO_DATE;
		protected System.Web.UI.WebControls.CustomValidator csvCHECK_DATE;
		protected System.Web.UI.WebControls.Label capPREMIUM_AMT;
        protected System.Web.UI.WebControls.Label capCOUNTRY;
		protected System.Web.UI.WebControls.TextBox txtPREMIUM_AMT;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPREMIUM_AMT;
		protected System.Web.UI.WebControls.RegularExpressionValidator revPREMIUM_AMT;
		protected System.Web.UI.WebControls.CustomValidator csvPREMIUM_AMT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOMM_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOUNTRY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBXML;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSUB_LOB_ID;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidState;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidROW_ID;
		 ClsGeneralLedger objGeneralLedger;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here		
            Ajax.Utility.RegisterTypeForAjax(typeof(AddMinimumPremAmt));
			// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
			base.ScreenId="364_0";
			
			lblMessage.Visible = false;
			SetErrorMessages();
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass					=	CmsButtonType.Write;
			btnReset.PermissionString				=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass					=	CmsButtonType.Write;
			btnSave.PermissionString				=	gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Accounting.AddMinimumPremAmt" ,System.Reflection.Assembly.GetExecutingAssembly());

			
		
			if(!Page.IsPostBack)
			{
                // Commenetd by Ruchika Chauhan on 19-Jan-2012 for TFS # 836
				//btnReset.Attributes.Add("onclick","javascript:return formReset();"); 
                //Calling Form.js function for btnReset
                btnReset.Attributes.Add("onclick", "javascript:return ResetForm('" + Page.Controls[0].ID + "' );");

                txtPREMIUM_AMT.Attributes.Add("onBlur", "this.value=formatBaseCurrencyAmount(this.value);");
				//btnSave.Attributes.Add("onclick","return Validate();");
				hlkTO_DATE.Attributes.Add("OnClick","fPopCalendar(document.ACT_MINIMUM_PREM_CANCEL.txtEFFECTIVE_TO_DATE,document.ACT_MINIMUM_PREM_CANCEL.txtEFFECTIVE_TO_DATE)");
				hlkFROM_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('txtEFFECTIVE_FROM_DATE'),document.getElementById('txtEFFECTIVE_FROM_DATE'))");

				#region "Loading singleton"
				DataTable dt = Cms.CmsWeb.ClsFetcher.ActiveState;
				cmbSTATE_ID.DataSource		= dt;
				DataRow row = dt.NewRow();
				row["State_Name"] = "All";
				row["State_Id"] = "0";
				dt.Rows.InsertAt(row,0);
				cmbSTATE_ID.DataTextField	= "State_Name";
				cmbSTATE_ID.DataValueField	= "State_Id";
				cmbSTATE_ID.DataBind();
				cmbSTATE_ID.Items.Insert(0,"");
				#endregion//Loading singleton

                //Added by Amit mishra for Tfs Bug #836
                if (GetLanguageID() == "3")
                {
                    COUNTRYID = "7";
                    COUNTRY_NAME = "Singapore";
                }
				SetCaptions();
               
                string strSysID = GetSystemId();
                if (ClsCommon.IsXMLResourceExists(Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID, "AddMinimumPremAmt.xml"))
                    setPageControls(Page, Request.PhysicalApplicationPath + "/CmsWeb/support/PageXml/" + strSysID + "/AddMinimumPremAmt.xml");
                GetLobByStateId();
				if(Request.QueryString["ROW_ID"]!=null && Request.QueryString["ROW_ID"].ToString()!="")
				{
					hidROW_ID.Value = Request.QueryString["ROW_ID"].ToString();
					hidOldData.Value =  ClsGeneralLedger.GetXmlForPageControlsMinimumPremium(hidROW_ID.Value);

					if (hidOldData.Value != "")
					{
						//Selecting the state id and then filling the associated line of busieness for that state
						System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
						doc.LoadXml(hidOldData.Value);
						
						System.Xml.XmlNode nod = doc.SelectSingleNode("/NewDataSet/Table/STATE_ID");
						if (nod != null)
						{
							cmbSTATE_ID.SelectedValue = nod.InnerText;
							cmbSTATE_ID_SelectedIndexChanged(null, null);
							//hidFormSaved.Value = "1"; // Commented by Ruchika on 24-Jan-2012 for TFS Bug # 836
						}
						nod = null;
						doc = null;
					}
				}
				//GetLobsInDropDown(cmbLOB_ID);
				//hidLOBXML.Value = Cms.BusinessLayer.BlApplication.clsPkgLobDetails.GetXmlForLobByState();
				hidLOBXML.Value = ClsCommon.GetXmlForLobByState();
				//Get The Value Of Class Field
				/*
				if(hidOldData.Value != "")
				{ 
					hidSelectedClass.Value =Cms.BusinessLayer.BlApplication.ClsGeneralInformation.FetchValueFromXML("CLASS_RISK",hidOldData.Value);
				}*/
               
				hidCOUNTRY_ID.Value = COUNTRYID;
				lblCOUNTRY_NAME.Text = COUNTRY_NAME;
				
			}
            
		}//end pageload
		#region  /setcaptions
		private void SetCaptions()
		{
			
			capSTATE_ID.Text								=		objResourceMgr.GetString("cmbSTATE_ID");
			capLOB_ID.Text									=		objResourceMgr.GetString("cmbLOB_ID");
			capSUB_LOB_ID.Text								=		objResourceMgr.GetString("cmbSUB_LOB_ID");
			capEFFECTIVE_FROM_DATE.Text						=		objResourceMgr.GetString("txtEFFECTIVE_FROM_DATE");
			capEFFECTIVE_TO_DATE.Text						=		objResourceMgr.GetString("txtEFFECTIVE_TO_DATE");
			//capPREMIUM_AMT.Text								=		objResourceMgr.GetString("txtPREMIUM_AMT");
            capPREMIUM_AMT.Text = objResourceMgr.GetString("capPREMIUM_AMT");
            capCOUNTRY.Text = objResourceMgr.GetString("capCOUNTRY");
			
		}

		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvSTATE_ID.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			rfvLOB_ID.ErrorMessage						=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvSUB_LOB_ID.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			rfvEFFECTIVE_FROM_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5");
			rfvEFFECTIVE_TO_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
			rfvPREMIUM_AMT.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"13");
			csvPREMIUM_AMT.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"8");
										
			revEFFECTIVE_FROM_DATE.ValidationExpression	=	aRegExpDate;
			revEFFECTIVE_TO_DATE.ValidationExpression	=	aRegExpDate;
			revPREMIUM_AMT.ValidationExpression			=	aRegExpBaseCurrencyformat;
													
			revEFFECTIVE_FROM_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revEFFECTIVE_TO_DATE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revPREMIUM_AMT.ErrorMessage					=  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");

			csvCHECK_DATE.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9");
			//rfvAGENCY_ID.ErrorMessage					=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"10");
		}
		#endregion
		#region "Fill DropDowns"
		public static void GetLobsInDropDown(DropDownList objDropDownList, string selectedValue)
		{
			
			DataTable  objDataTable =Cms.CmsWeb.ClsFetcher.LOBs;
			objDropDownList.Items.Clear();
			objDropDownList.Items.Add("");
			objDropDownList.Items.Add(new ListItem("ALL","0"));
			for(int i=0;i<objDataTable.DefaultView.Count;i++)
			{
				objDropDownList.Items.Add(new ListItem(objDataTable.DefaultView[i]["LOB_DESC"].ToString(),objDataTable.DefaultView[i]["LOB_ID"].ToString()));
				if(selectedValue!=null && selectedValue.Length>0 && objDataTable.DefaultView[i]["LOB_ID"].ToString().Equals(selectedValue))
					objDropDownList.SelectedIndex = i;
			}
		}      


        //Added by Ruchika Chauhan on 31-Jan-2012 for TFS Bug # 836
        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxGetSubLobByLob(string selectedLOB)
        {
            try
            {                
                DataSet dsSubLob = new DataSet();
                if ((selectedLOB != "") || (selectedLOB != null))
                {
                    dsSubLob = ClsCommon.GetSubLOBs(int.Parse(selectedLOB));                
                }
                return dsSubLob;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

      
		private void cmbSTATE_ID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
                GetLobByStateId();
				
//				if(cmbSTATE_ID.SelectedIndex!=3)
//				{
//					rfvSUB_LOB_ID.Enabled=false;
//				}
//				else
//					rfvSUB_LOB_ID.Enabled=true;
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
		}




        private void GetLobByStateId()
        {
            
            int stateID;

            Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGenInfo = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
            DataSet dsLOB = new DataSet();
            if (cmbSTATE_ID.SelectedValue != "0" && cmbSTATE_ID.SelectedValue != "")
            {
                stateID = cmbSTATE_ID.SelectedItem == null ? -1 : int.Parse(cmbSTATE_ID.SelectedItem.Value);
                if (stateID != -1)
                {

                    dsLOB = objGenInfo.GetLOBBYSTATEID(stateID);
                    cmbLOB_ID.DataSource = dsLOB;
                    cmbLOB_ID.DataTextField = "LOB_DESC";
                    cmbLOB_ID.DataValueField = "LOB_ID";
                    cmbLOB_ID.DataBind();
                    cmbLOB_ID.Items.Insert(0, "");
                    //					if(stateID!=49)
                    //					{
                    //						cmbLOB_ID.Items.Insert(0,new ListItem("All","0"));					
                    //						cmbLOB_ID.Items.Insert(0,"");
                    //					}					

                    hidState.Value = stateID.ToString();
                }
            }
        }


	
		public static void GetLobsInDropDown(DropDownList objDropDownList)
		{
			GetLobsInDropDown(objDropDownList,null);
		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				objGeneralLedger  = new  ClsGeneralLedger();

				//Retreiving the form values into model class object
				ClsMinimumPremAmt  objMinimumPremAmt = GetFormValue();
				objMinimumPremAmt.CREATED_BY = int.Parse(GetUserId());
				objMinimumPremAmt.CREATED_DATETIME = DateTime.Now;
				objMinimumPremAmt.MODIFIED_BY = objMinimumPremAmt.CREATED_BY;
				objMinimumPremAmt.LAST_UPDATED_DATETIME = objMinimumPremAmt.CREATED_DATETIME;

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					/*objMinimumPremAmt.CREATED_BY = int.Parse(GetUserId());
					objMinimumPremAmt.CREATED_DATETIME = DateTime.Now;
					objMinimumPremAmt.MODIFIED_BY = objMinimumPremAmt.CREATED_BY;
					objMinimumPremAmt.LAST_UPDATED_DATETIME = objMinimumPremAmt.CREATED_DATETIME;*/
					objMinimumPremAmt.IS_ACTIVE = "Y";
					//Calling the add method of business layer class
					intRetVal = objGeneralLedger.SaveMinimumPremium(objMinimumPremAmt);

					if(intRetVal>0)
					{
						//hidCOMM_ID.Value = objMinimumPremAmt.COMM_ID.ToString();
						hidROW_ID.Value = objMinimumPremAmt.ROW_ID.ToString();
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						hidOldData.Value =  ClsGeneralLedger.GetXmlForPageControlsMinimumPremium(objMinimumPremAmt.ROW_ID.ToString());
						//GetClassData();
						
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"11");
						hidFormSaved.Value			=		"2";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"12");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text				=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
					lblMessage.Visible = true;
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsMinimumPremAmt  objOldobjMinimumPremAmt;
					objOldobjMinimumPremAmt = new ClsMinimumPremAmt();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldobjMinimumPremAmt,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objMinimumPremAmt.ROW_ID = int.Parse(strRowId);
					//objMinimumPremAmt.MODIFIED_BY = int.Parse(GetUserId());
					//objMinimumPremAmt.LAST_UPDATED_DATETIME = DateTime.Now;
					//Updating the record using business layer class object
					//intRetVal	= objGeneralLedger.SaveMinimumPremium(objMinimumPremAmt);
					intRetVal	= objGeneralLedger.UpdateMinimumPremium(objOldobjMinimumPremAmt,objMinimumPremAmt);
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						hidOldData.Value		=   ClsGeneralLedger.GetXmlForPageControlsMinimumPremium(objMinimumPremAmt.ROW_ID.ToString());
						//GetClassData();
						
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"11");
						hidFormSaved.Value		=	"2";
					}
					else if(intRetVal == -2)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"12");
						hidFormSaved.Value			=		"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"2";
					}
					lblMessage.Visible = true;
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
				if(objGeneralLedger!= null)
					objGeneralLedger.Dispose();
			}
		}


		#region
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsMinimumPremAmt GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsMinimumPremAmt objMinimumPremAmt;
			objMinimumPremAmt = new ClsMinimumPremAmt();

			objMinimumPremAmt.COUNTRY_ID			=	int.Parse(hidCOUNTRY_ID.Value);
			objMinimumPremAmt.STATE_ID				=	int.Parse(cmbSTATE_ID.SelectedValue);
			objMinimumPremAmt.LOB_ID				=	int.Parse(cmbLOB_ID.SelectedValue);
			objMinimumPremAmt.SUB_LOB_ID	=-1;

			//Get The Sub Lob
			//int sublobId=0;
			//sublobId= int.Parse(Request["cmbSUB_LOB_ID"]);
			//objRegCommSetup_AgencyInfo.SUB_LOB_ID =sublobId;
			if(hidSUB_LOB_ID.Value!=null && hidSUB_LOB_ID.Value.Length>0)
				objMinimumPremAmt.SUB_LOB_ID			=	int.Parse(hidSUB_LOB_ID.Value);
			objMinimumPremAmt.EFFECTIVE_FROM_DATE	=	ConvertToDate(txtEFFECTIVE_FROM_DATE.Text);
            objMinimumPremAmt.EFFECTIVE_TO_DATE = ConvertToDate(txtEFFECTIVE_TO_DATE.Text);
            objMinimumPremAmt.PREMIUM_AMT = double.Parse(txtPREMIUM_AMT.Text, NfiBaseCurrency);
		
			/*if(Request.QueryString["COMMISSION_TYPE"]!=null && Request.QueryString["COMMISSION_TYPE"]=="A")
			{
				objRegCommSetup_AgencyInfo.AGENCY_ID = int.Parse(cmbAGENCY_ID.SelectedValue);
			} */

			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId 		=	hidROW_ID.Value;
			oldXML 			=	hidOldData.Value;
			//Returning the model object

			return objMinimumPremAmt;
		}
		#endregion 

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
				objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
				objStuTransactionInfo.loggedInUserName = GetUserName();
				objGeneralLedger =  new ClsGeneralLedger();
				

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
					objGeneralLedger.TransactionInfoParams  = objStuTransactionInfo;
					objGeneralLedger.ActivateDeactivate(hidROW_ID.Value,"N");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
					objGeneralLedger.TransactionInfoParams = objStuTransactionInfo;
					objGeneralLedger.ActivateDeactivate(hidROW_ID.Value,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}
				hidOldData.Value =  ClsGeneralLedger.GetXmlForPageControlsMinimumPremium(hidROW_ID.Value);  
					//objGeneralLedger.GetXmlForPageControls(hidCOMM_ID.Value);
				hidFormSaved.Value			=	"1";
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
				if(objGeneralLedger!= null)
					objGeneralLedger.Dispose();
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
			this.cmbSTATE_ID.SelectedIndexChanged += new System.EventHandler(this.cmbSTATE_ID_SelectedIndexChanged);            
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
            
            //formReset
		}
		#endregion
	}
}
