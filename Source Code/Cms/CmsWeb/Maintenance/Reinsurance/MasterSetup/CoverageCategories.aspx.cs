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
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb; 
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlCommon.Reinsurance;
using Cms.Model.Maintenance.Reinsurance;
using Cms.ExceptionPublisher.ExceptionManagement;

namespace Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup
{
	/// <summary>
	/// Summary description for CoverageCategories.
	/// </summary>
	public class CoverageCategories : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration

		protected System.Web.UI.WebControls.Label capEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.TextBox txtEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.HyperLink hlkEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Label capLOB_ID;
		protected System.Web.UI.WebControls.DropDownList cmbLOB_ID;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLOB_ID;
		protected System.Web.UI.WebControls.Label capCATEGORY;
//		protected System.Web.UI.WebControls.TextBox txtCATEGORY;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCATEGORY;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
	
		#endregion
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOVERAGE_CATEGORY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidselcat;
        protected System.Web.UI.HtmlControls.HtmlTable tblBody;
		
		private string strRowId;
//		protected System.Web.UI.WebControls.Label capADDITIONAL;
//		protected System.Web.UI.WebControls.TextBox txtADDITIONAL;
//		protected System.Web.UI.WebControls.RegularExpressionValidator revADDITIONAL;
		protected System.Web.UI.WebControls.RegularExpressionValidator revEFFECTIVE_DATE;
		protected System.Web.UI.WebControls.Button btnSELECT;
		protected System.Web.UI.WebControls.Button btnDESELECT;
		protected System.Web.UI.WebControls.Label capRECIPIENTS1;
		protected System.Web.UI.WebControls.ListBox cmbRECIPIENTS;
		protected System.Web.UI.WebControls.CustomValidator csvRECIPIENTS;
		protected System.Web.UI.WebControls.ListBox cmbFROMCATEGORY;
		protected System.Web.UI.WebControls.Label capRECIPIENTS;
		protected System.Web.UI.WebControls.ListBox cmbCATEGORY;
		protected System.Web.UI.WebControls.CustomValidator csvCATEGORY;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCATAGORY;
        protected System.Web.UI.WebControls.Label capMessages;
		#region Local form variables
		System.Resources.ResourceManager objResourceMgr;	//creating resource manager object

		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			#region Setting the controls attribues
			//Setting the attributes of controls
			hlkEFFECTIVE_DATE.Attributes.Add("OnClick","fPopCalendar(document.COVERAGE_CATEGORY.txtEFFECTIVE_DATE,document.COVERAGE_CATEGORY.txtEFFECTIVE_DATE)");
			btnSave.Attributes.Add("onclick","javascript:return setCatagories();");   
			btnSELECT.Attributes.Add("onclick","javascript:selectCatagories();return false;");
			btnDESELECT.Attributes.Add("onclick","javascript:deselectCatagories();return false;");
//			btnReset.Attributes.Add("onclick","javascript:document.forms[0].reset();return false;");
			btnReset.Attributes.Add("onclick","javascript:Reset();");
			btnActivateDeactivate.Attributes.Add("onclick","javascript:document.COVERAGE_CATEGORY.reset();");
            capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");
			#endregion
			//Setting the screen id
			SetScreenId();
            lblMessage.Visible = false;
			
			SetErrorMessages();		//Seeting the property of validation controls

			#region Setting the properties of CmsButton 
			//START:** Setting permissions and class (Read/write/execute/delete) of Cmsbutton**********
			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;

			btnSave.CmsButtonClass		= CmsButtonType.Write;
			btnSave.PermissionString	= gstrSecurityXML;

			btnDelete.CmsButtonClass	= CmsButtonType.Delete;
			btnDelete.PermissionString	= gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	= CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;

			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			#endregion

			//Making resource manager object for reading the resource file
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Reinsurance.MasterSetup.CoverageCategories" ,System.Reflection.Assembly.GetExecutingAssembly());

			#region function to be called in Postback 
			if(!Page.IsPostBack)
			{
				GetQueryStringValues();
				GetOldDataXML();
				//Setting the caption so labels
				SetCaptions();
				fillDropDowns();
			}
			#endregion
            
		}

		/// <summary>
		/// Show the caption of labels from resource file
		/// </summary>
		private void SetCaptions()
		{
			capEFFECTIVE_DATE.Text		= objResourceMgr.GetString("txtEFFECTIVE_DATE");
			capLOB_ID.Text= objResourceMgr.GetString("cmbLOB_ID");
			capCATEGORY.Text		= objResourceMgr.GetString("cmbCATEGORY");
		}

		private void SetScreenId()
		{
			base.ScreenId = "400_1";
		}

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvEFFECTIVE_DATE.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("95");
			rfvLOB_ID.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("865");
			//rfvCATEGORY.ErrorMessage=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("194");
			revEFFECTIVE_DATE.ValidationExpression =aRegExpDate;
            revEFFECTIVE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("22.pt-BR");
            hidselcat.Value = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1407");
		}

		
		#endregion

		private void fillDropDowns()
		{
			//LOBs
			DataTable dtLOBs = Cms.CmsWeb.ClsFetcher.LOBs;
			cmbLOB_ID.DataSource			= dtLOBs;
			cmbLOB_ID.DataTextField		= "LOB_DESC";
			cmbLOB_ID.DataValueField		= "LOB_ID";
			cmbLOB_ID.DataBind();

			//Remove General Liability
			ListItem Li = cmbLOB_ID.Items.FindByValue("7");//"7" -> General Liability
			if (Li != null)	
			{
				cmbLOB_ID.Items.Remove(Li);	
			}
			cmbLOB_ID.Items.Insert(0,"");


			cmbFROMCATEGORY.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("RRB");
			cmbFROMCATEGORY.DataTextField	= "LookupDesc";
			cmbFROMCATEGORY.DataValueField	= "LookupID";
			cmbFROMCATEGORY.DataBind(); 
		
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
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReset_Click(object sender, System.EventArgs e)
		{
           
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				/*Deleting the whole record*/
				ClsCoverageCategories objCoverageCategories = new ClsCoverageCategories();

				ClsCoverageCategoriesInfo objCoverageCategoriesInfo = GetFormValue();

				int intRetVal = objCoverageCategories.Delete(objCoverageCategoriesInfo,int.Parse(hidCOVERAGE_CATEGORY_ID.Value),int.Parse(GetUserId()));

				if (intRetVal > 0)
				{
					//Deleted successfully
					lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "127");
					hidCOVERAGE_CATEGORY_ID.Value = "";
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
		
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				ClsCoverageCategories objCoverageCategories = new ClsCoverageCategories();

				//Retreiving the form values into model class object
				ClsCoverageCategoriesInfo objCoverageCategoriesInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{

					//Filling the CREATED_BY to current login user and CREATED_DATE to current date
					objCoverageCategoriesInfo.CREATED_BY = int.Parse(GetUserId());
					objCoverageCategoriesInfo.CREATED_DATETIME = DateTime.Now;

					//Calling the add method of business layer class
					intRetVal = objCoverageCategories.Add(objCoverageCategoriesInfo);

					if(intRetVal>0)				//Saved successfully
					{
						hidCOVERAGE_CATEGORY_ID.Value = objCoverageCategoriesInfo.COVERAGE_CATEGORY_ID.ToString();
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value	=	"1";
						hidIS_ACTIVE.Value	=	"Y";
                        btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315");//"Deactivate";
						GetOldDataXML();
					}
					else if(intRetVal == -1)	//Duplicate journal entry number error occured
					{
						//Showing the error message from customized message file
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value	=	"2";
					}
					else						//Some other error occured
					{
						//Showing the error message from customized message file
						lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value	=	"2";
					}
					lblMessage.Visible = true;
				}	// end save case
				else //UPDATE CASE
				{

					//Creating the Model object for holding the Old data
					ClsCoverageCategoriesInfo objOldCoverageCategoriesInfo;
					objOldCoverageCategoriesInfo = new ClsCoverageCategoriesInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldCoverageCategoriesInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objCoverageCategoriesInfo.MODIFIED_BY = int.Parse(GetUserId());
					objCoverageCategoriesInfo.LAST_UPDATED_DATETIME = DateTime.Now;
					objCoverageCategoriesInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					intRetVal	= objCoverageCategories.Update(objOldCoverageCategoriesInfo,objCoverageCategoriesInfo);
					if( intRetVal > 0 )				//update successfully performed
					{
						
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML();
					}
					
					lblMessage.Visible = true;
				}
			}
			catch(Exception ex)
			{
				//Some exception occured in code, hence showing the exception error message
				lblMessage.Text		=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				hidFormSaved.Value	=	"2";
				
				//Publishing the exception
				ExceptionManager.Publish(ex);
			}
			finally
			{
//			if(objCoverageCategoriesInfo!= null)
//				objCoverageCategoriesInfo.Dispose();
			}
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
		
			ClsCoverageCategories objCoverageCategories = new ClsCoverageCategories();
			ClsCoverageCategoriesInfo objCoverageCategoriesInfo = new ClsCoverageCategoriesInfo();
			objCoverageCategoriesInfo=GetFormValue();
			
			try
			{
				//Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo();
				
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{
					objCoverageCategories.ActivateDeactivateCoverageCatagory(objCoverageCategoriesInfo,"N");
					lblMessage.Text = ClsMessages.FetchGeneralMessage("41");
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314");//"Activate";
					hidIS_ACTIVE.Value="N";
				}
				else
				{					
					objCoverageCategories.ActivateDeactivateCoverageCatagory(objCoverageCategoriesInfo,"Y");
					lblMessage.Text = ClsMessages.FetchGeneralMessage("40");
                    btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); //"Deactivate";
					hidIS_ACTIVE.Value="Y";
				}
				hidOldData.Value = ClsCoverageCategories.GetCoverageCatagoryInfo(int.Parse(hidCOVERAGE_CATEGORY_ID.Value));
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
				if(objCoverageCategories!= null)
					objCoverageCategories.Dispose();
			}
				
		}

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsCoverageCategoriesInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			ClsCoverageCategoriesInfo objCoverageCategories;
			objCoverageCategories = new ClsCoverageCategoriesInfo();

			objCoverageCategories.CREATED_BY = objCoverageCategories.MODIFIED_BY = int.Parse(GetUserId());

			//Populating the fields values of model class object from controls
					
			if (txtEFFECTIVE_DATE.Text.Trim() != "")
				objCoverageCategories.EFFECTIVE_DATE = Convert.ToDateTime(txtEFFECTIVE_DATE.Text.Trim());
			
			string catagory=(string)hidCATAGORY.Value;
			if (catagory !="" && catagory != "0")
			{
				string[] catagories= catagory.Split(',');  
				catagory="";
				for (int i=0;i <catagories.GetLength(0)-1 ;i++)
				{
					catagory=catagory + catagories[i].ToString()  + ","; 	
				}
			}
			if (catagory =="0" ) 
				catagory="";			
			objCoverageCategories.CATEGORY = catagory;


//			if (cmbLOB_ID.SelectedValue != null && cmbLOB_ID.SelectedValue.Trim() != "")
//				objCoverageCategories.LOB_ID = int.Parse(cmbLOB_ID.SelectedValue);
//
//			objCoverageCategories.CATEGORY = txtCATEGORY.Text;
		
			if (cmbLOB_ID.SelectedValue != null && cmbLOB_ID.SelectedValue.Trim() != "")
				objCoverageCategories.LOB_ID = int.Parse(cmbLOB_ID.SelectedValue);

			//These  assignments are common to all pages.
			strRowId		=	hidCOVERAGE_CATEGORY_ID.Value;
			if(!strRowId.ToUpper().Equals("NEW"))
				objCoverageCategories.COVERAGE_CATEGORY_ID= int.Parse(strRowId);
			//Returning the model object
			return objCoverageCategories;
		}
		#endregion


		private void GetOldDataXML()
		{
			if ( hidCOVERAGE_CATEGORY_ID.Value != "" ) 
			{
				//Retreiving the information of selected journal entry in the form of XML 				
				hidOldData.Value = ClsCoverageCategories.GetCoverageCatagoryInfo(int.Parse(hidCOVERAGE_CATEGORY_ID.Value));

				if (hidOldData.Value != "")
				{
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.LoadXml(hidOldData.Value);
				
					System.Xml.XmlNode effDate = doc.SelectSingleNode("/NewDataSet/Table/EFFECTIVE_DATE");
					System.Xml.XmlNode lob = doc.SelectSingleNode("/NewDataSet/Table/LOB_ID");
					System.Xml.XmlNode catagory = doc.SelectSingleNode("/NewDataSet/Table/CATEGORY");

					if (effDate != null)
					{
						if (effDate.InnerXml.Trim() != "")
						{
							//Selecting the Date
                            txtEFFECTIVE_DATE.Text = ConvertDBDateToCulture(effDate.InnerXml.Trim().ToUpper());
						}
					}
					if (lob != null)
					{
						if (lob.InnerXml.Trim() != "")
						{
							//Selecting the Lob
							cmbLOB_ID.SelectedValue = lob.InnerXml.Trim().ToUpper();
						}
					}
					if (catagory != null)
					{
						if (catagory.InnerXml.Trim() != "")
						{
							//Selecting the type
							hidCATAGORY.Value = catagory.InnerXml.Trim();
						}
					}
					System.Xml.XmlNode IsActive = doc.SelectSingleNode("/NewDataSet/Table/IS_ACTIVE");
					if (IsActive != null)
					{
						if (IsActive.InnerXml.Trim() != "")
						{
							hidIS_ACTIVE.Value= IsActive.InnerXml.Trim().ToUpper();
                            if (IsActive.InnerXml.Trim() == "Y")
                                btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1315"); //"Deactivate";
                            else if (IsActive.InnerXml.Trim() == "N")
                                btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1314"); //"Activate";
						}
					}
					doc = null;
				}

			}
		}
					

		private void GetQueryStringValues()
		{
			if(Request.QueryString["COVERAGE_CATEGORY_ID"]!=null && Request.QueryString["COVERAGE_CATEGORY_ID"].ToString()!="")
				hidCOVERAGE_CATEGORY_ID.Value = Request.QueryString["COVERAGE_CATEGORY_ID"].ToString();

		}
	}
}
