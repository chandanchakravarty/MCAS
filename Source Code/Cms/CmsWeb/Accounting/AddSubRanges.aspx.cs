	/******************************************************************************************
	<Author				: -   Ajit Singh Chahal
	<Start Date				: -	5/12/2005 2:25:04 PM
	<End Date				: -	
	<Description				: - 	Code behind for sub ranges.
	<Review Date				: - 
	<Reviewed By			: - 	
	Modification History
	<Modified Date			: - 
	<Modified By				: - 
	<Purpose				: - Code behind for sub ranges.
	
	<Modified Date			: - 25/08/2005
	<Modified By			: - Anurag Verma
	<Purpose				: - Applying Null Check for buttons on aspx page 
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

namespace Cms.CmsWeb.Maintenance.Accounting
{
	/// <summary>
	/// Code behind for sub ranges.
	/// </summary>
	public class AddSubRanges :  Cms.CmsWeb.cmsbase
	{
			#region Page controls declaration
			protected System.Web.UI.WebControls.TextBox txtPARENT_CATEGORY_ID;
			protected System.Web.UI.WebControls.TextBox txtCATEGORY_DESC;

			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

			protected Cms.CmsWeb.Controls.CmsButton btnReset;
			protected Cms.CmsWeb.Controls.CmsButton btnSave;

			protected System.Web.UI.WebControls.RequiredFieldValidator rfvPARENT_CATEGORY_ID;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvCATEGORY_DESC;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvRANGE_FROM;
			protected System.Web.UI.WebControls.RequiredFieldValidator rfvRANGE_TO;

			protected System.Web.UI.WebControls.Label lblMessage;
            protected System.Web.UI.WebControls.Label capMessages;


			#endregion
			#region Local form variables
			//START:*********** Local form variables *************
			string oldXML;
			//creating resource manager object (used for reading field and label mapping)
			System.Resources.ResourceManager objResourceMgr;
			private string strRowId, strFormSaved;
		//	private int	intLoggedInUserID;
		protected System.Web.UI.WebControls.Label capPARENT_CATEGORY_ID;
		protected System.Web.UI.WebControls.DropDownList cmbPARENT_CATEGORY_ID;
		protected System.Web.UI.WebControls.Label capCATEGORY_DESC;
		protected System.Web.UI.WebControls.Label capRANGE_FROM;
		protected System.Web.UI.WebControls.Label capRANGE_TO;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCATEGORY_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFor;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidAnd;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRange;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRange1;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRange2;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRange3;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRange4;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRange5;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRANGE_n;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFrom;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidTo;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hid_assign;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidRangefall;
		protected System.Web.UI.WebControls.TextBox txtwholeRangeFrom;
		protected System.Web.UI.WebControls.DropDownList cmbfracRangeFrom;
		protected System.Web.UI.WebControls.TextBox txtwholeRangeTO;
		protected System.Web.UI.WebControls.DropDownList cmbfracRangeTo;
		protected System.Web.UI.WebControls.Label lblRangeLimit;
		protected System.Web.UI.WebControls.RegularExpressionValidator revwholeRangeFrom;
		protected System.Web.UI.WebControls.RegularExpressionValidator revwholeRangeTO;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidfOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidtOldData;
			//Defining the business layer class object
			ClsGLAccountRanges  objGLAccountRanges ;
		
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
				rfvPARENT_CATEGORY_ID.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"140");
				rfvCATEGORY_DESC.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
				rfvRANGE_FROM.ErrorMessage				    =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"142");
				rfvRANGE_TO.ErrorMessage					=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"143");

				revwholeRangeFrom.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				revwholeRangeTO.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");

				revwholeRangeFrom.ValidationExpression		=	aRegExpAcNumber;
				revwholeRangeTO.ValidationExpression		=	aRegExpAcNumber;           
			}
			#endregion
			#region PageLoad event
			private void Page_Load(object sender, System.EventArgs e)
			{
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
				btnSave.Attributes.Add("onclick","javascript: return CheckNewRanges();");

				// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
				base.ScreenId="124_1_0";
				lblMessage.Visible = false;
				SetErrorMessages();
                capMessages.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1168");


				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnReset.CmsButtonClass			=	CmsButtonType.Write;
				btnReset.PermissionString		=	gstrSecurityXML;

				btnDelete.CmsButtonClass		=	CmsButtonType.Delete;
				btnDelete.PermissionString		=	gstrSecurityXML;

				btnSave.CmsButtonClass			=	CmsButtonType.Write;
				btnSave.PermissionString		=	gstrSecurityXML;

				//END:*********** Setting permissions and class (Read/write/execute/delete)  *************

				//====================geting range for main type================================
				DataSet objDataSet = ClsGLAccountRanges.GetAccountRanges();
				DataTable objDataTable = objDataSet.Tables[0];
				Response.Write("<script> mainRangesFrom = new Array(); mainRangesTo = new Array();\n");
				for(int i=0;i<objDataTable.Rows.Count;i++)
				{
					Response.Write("mainRangesFrom["+objDataTable.Rows[i]["CATEGORY_ID"]+"]="+objDataTable.Rows[i]["range_from"]+";\n");
                    Response.Write("mainRangesTo["+objDataTable.Rows[i]["CATEGORY_ID"]+"]="+objDataTable.Rows[i]["range_to"]+";\n");
				}
				string var1="",var2="",var3="";
				var1 = "subRangesFrom";
				var2 = "subRangesTo";
				var3 = "categoryDesc";
				Response.Write("\n "+var1+" = new Array();\n "+var2+" = new Array();\n "+var3+" = new Array();\n");

				for(int j=1;j<objDataSet.Tables.Count;j++)
				{
					objDataTable = objDataSet.Tables[j];
					for(int i=0;i<objDataTable.Rows.Count;i++)
					{
						if(i==0)
						{
							Response.Write("\n "+var1+"["+j+"] = new Array();\n "+var2+"["+j+"] = new Array();\n "+var3+"["+j+"] = new Array();\n");
						}
						Response.Write(var1+"["+j+"]["+i+"]="+objDataTable.Rows[i]["range_from"]+";\n");
						Response.Write(var2+"["+j+"]["+i+"]="+objDataTable.Rows[i]["range_to"]+";\n");
						Response.Write(var3+"["+j+"]["+i+"]='"+objDataTable.Rows[i]["CATEGORY_DESC"]+"';\n");
					}
				}
				Response.Write("</script> ");
				//====================geting range for main type================================

				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.AddSubRanges" ,System.Reflection.Assembly.GetExecutingAssembly());
				if(!Page.IsPostBack)
				{
					string item="";
					for(int i=0;i<100;i++)
					{
						if(i<=9)
							item = ".0"+i.ToString();
						else
							item = "."+i.ToString();

						cmbfracRangeFrom.Items.Add(new ListItem(item,i<=9?"0"+i.ToString():i.ToString()));
						cmbfracRangeTo.Items.Add(new ListItem(item,i<=9?"0"+i.ToString():i.ToString()));
					}
					ClsGLAccountRanges.GetParentCategoriesInDropDown(cmbPARENT_CATEGORY_ID);
				}
					if(Request.QueryString["CATEGORY_ID"]!=null && Request.QueryString["CATEGORY_ID"].ToString().Length>0)
						hidOldData.Value = ClsGLAccountRanges.GetXmlForPageControls(Request.QueryString["CATEGORY_ID"].ToString());
										
					SetCaptions();
					#region "Loading singleton"
					#endregion//Loading singleton
				
			}//end pageload
			#endregion
			/// <summary>
			/// validate posted data from form
			/// </summary>
			/// <returns>True if posted data is valid else false</returns>
			private bool doValidationCheck()
			{
				try
				{
					return true;
				}
				catch (Exception ex)
				{
                    Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
					return false;
				}
			}
			#region GetFormValue
			/// <summary>
			/// Fetch form's value and stores into model class object and return that object.
			/// </summary>
			private Cms.Model.Maintenance.Accounting.ClsGLAccountRangesInfo GetFormValue()
			{
				//Creating the Model object for holding the New data
				ClsGLAccountRangesInfo objGLAccountRangesInfo;
				objGLAccountRangesInfo = new ClsGLAccountRangesInfo();

				objGLAccountRangesInfo.PARENT_CATEGORY_ID=	int.Parse(cmbPARENT_CATEGORY_ID.SelectedValue);
				objGLAccountRangesInfo.CATEGORY_DESC=	txtCATEGORY_DESC.Text;
				objGLAccountRangesInfo.RANGE_FROM=	double.Parse(txtwholeRangeFrom.Text+cmbfracRangeFrom.Items[cmbfracRangeFrom.SelectedIndex]);
				objGLAccountRangesInfo.RANGE_TO=	double.Parse(txtwholeRangeTO.Text+cmbfracRangeTo.Items[cmbfracRangeTo.SelectedIndex]);

				//These  assignments are common to all pages.
				strFormSaved	=	hidFormSaved.Value;
				strRowId		=	hidCATEGORY_ID.Value;
				oldXML		= hidOldData.Value;
				//Returning the model object

				return objGLAccountRangesInfo;
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
				this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
					objGLAccountRanges = new  ClsGLAccountRanges(true);

					//Retreiving the form values into model class object
					ClsGLAccountRangesInfo objGLAccountRangesInfo = GetFormValue();
					string MainType = cmbPARENT_CATEGORY_ID.Items[cmbPARENT_CATEGORY_ID.SelectedIndex].Text;

					if(strRowId.ToUpper().Equals("NEW")) //save case
					{
						objGLAccountRangesInfo.CREATED_BY = int.Parse(GetUserId());
						objGLAccountRangesInfo.CREATED_DATETIME = DateTime.Now;
						objGLAccountRangesInfo.IS_ACTIVE="Y";
						objGLAccountRangesInfo.MODIFIED_BY = objGLAccountRangesInfo.CREATED_BY;
						objGLAccountRangesInfo.LAST_UPDATED_DATETIME = objGLAccountRangesInfo.CREATED_DATETIME;

						//Calling the add method of business layer class
						intRetVal = objGLAccountRanges.Add(objGLAccountRangesInfo);

						if(intRetVal>0)
						{
							hidCATEGORY_ID.Value = objGLAccountRangesInfo.CATEGORY_ID.ToString();
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
							hidFormSaved.Value			=	"1";
							hidIS_ACTIVE.Value = "Y";
							hidOldData.Value = ClsGLAccountRanges.GetXmlForPageControls(objGLAccountRangesInfo.CATEGORY_ID.ToString());
						}
						else
						{
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
							hidFormSaved.Value			=	"2";
						}
						lblMessage.Visible = true;
					} // end save case
					else //UPDATE CASE
					{

						//Creating the Model object for holding the Old data
						ClsGLAccountRangesInfo objOldGLAccountRangesInfo;
						objOldGLAccountRangesInfo = new ClsGLAccountRangesInfo();

						//Setting  the Old Page details(XML File containing old details) into the Model Object
						base.PopulateModelObject(objOldGLAccountRangesInfo,hidOldData.Value);

						//Setting those values into the Model object which are not in the page
						objGLAccountRangesInfo.CATEGORY_ID = int.Parse(strRowId);
						objGLAccountRangesInfo.MODIFIED_BY = int.Parse(GetUserId());
						objGLAccountRangesInfo.LAST_UPDATED_DATETIME = DateTime.Now;
						objGLAccountRangesInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

						//Updating the record using business layer class object
						intRetVal	= objGLAccountRanges.Update(objOldGLAccountRangesInfo,objGLAccountRangesInfo,MainType);
						if( intRetVal > 0 )			// update successfully performed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value		=	"1";
							hidOldData.Value		=	ClsGLAccountRanges.GetXmlForPageControls(objOldGLAccountRangesInfo.CATEGORY_ID.ToString());
						}
						else if(intRetVal == -2)	// Duplicate code exist, update failed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"3");
							hidFormSaved.Value		=	"2";
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
					if(objGLAccountRanges!= null)
						objGLAccountRanges.Dispose();
				}
			}

			#endregion
			private void SetCaptions()
			{
				capPARENT_CATEGORY_ID.Text					=		objResourceMgr.GetString("cmbPARENT_CATEGORY_ID");
				capCATEGORY_DESC.Text						=		objResourceMgr.GetString("txtCATEGORY_DESC");
				capRANGE_FROM.Text							=		objResourceMgr.GetString("capRANGE_FROM");
				capRANGE_TO.Text							=		objResourceMgr.GetString("capRANGE_TO");
                hidFor.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
                hidRange.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
                hidAnd.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "7");
                hidRange1.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "8");
                hidRange2.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "9");
                hidRange3.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "10");
                hidRange4.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "11");
                hidRange5.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "12");
                hidRANGE_n.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");
                hidFrom.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");
                hidTo.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
                hid_assign.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");
                hidRangefall.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
			}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			int intRetVal = new ClsGLAccountRanges().DeleteSubRanges(hidCATEGORY_ID.Value);
			if( intRetVal > 0 )			// delete successfully performed
			{

				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"127");
				hidFormSaved.Value		=	"1";
				hidOldData.Value		=	"";
			}
			else if(intRetVal == -1)	// delete can not be performed as account range is used in to define a/c
			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
				hidFormSaved.Value		=	"2";
			}
			else 

			{
				lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"128");
				hidFormSaved.Value		=	"2";
			}
			Cms.BusinessLayer.BlApplication.ClsGeneralInformation objGen = new Cms.BusinessLayer.BlApplication.ClsGeneralInformation();
			ClsGLAccountRangesInfo objGLAccountRangesInfo = GetFormValue();
			string trans_desc ="Sub Ranges has been Deleted.";
			string MainType = cmbPARENT_CATEGORY_ID.Items[cmbPARENT_CATEGORY_ID.SelectedIndex].Text;
			string trans_custom ="; Main Type :"+ MainType +"; Sub Type :" + objGLAccountRangesInfo.CATEGORY_DESC;
			objGen.WriteTransactionLog(0, 0, 0, trans_desc, int.Parse(GetUserId()),trans_custom, "Application");

			lblMessage.Visible = true;
		}
			
		}
	}

