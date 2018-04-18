/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	  6/9/2005 1:10:15 PM
<End Date				: -	
<Description			: -   Code behind for Transaction code details.
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: -   Code behind for Transaction code details.

<Modified Date			: - 26/08/2005
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
using System.Configuration;
namespace Cms.CmsWeb.Accounting
{
	/// <summary>
	/// Code behind for Transaction code details.
	/// </summary>
	public class AddTransactionCodeGroupDetails : Cms.CmsWeb.cmsbase
	{
		#region Page controls declaration

		protected System.Web.UI.WebControls.TextBox txtDEF_SEQ;

		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;

		protected System.Web.UI.WebControls.RequiredFieldValidator rfvDEF_SEQ;

		protected System.Web.UI.WebControls.RegularExpressionValidator revDEF_SEQ;
		protected System.Web.UI.WebControls.Label lblMessage;

		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		public string url="";
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidDETAIL_ID;
		protected System.Web.UI.WebControls.Label lblDISPLAY_DESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAN_ID;
		protected System.Web.UI.WebControls.TextBox txtDISPLAY_DESCRIPTION;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMaxRows;
		protected System.Web.UI.WebControls.TextBox TRAN_ID;
		protected System.Web.UI.WebControls.TextBox txtTRAN_CODE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCurrentPage;
		protected Cms.CmsWeb.Controls.CmsButton btnPrevious;
		protected Cms.CmsWeb.Controls.CmsButton btnNext;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.DataGrid dgTranCodeGrpDetails;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidTRAN_GROUP_ID;
		protected Int32 _currentPageNumber = 1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidpageDefaultsize;
		//Defining the business layer class object
		//ClsTransactionCodeGroupDetails  objTransactionCodeGroupDetails ;
										
		//END:*********** Local variables *************

		#endregion
		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetValidators(System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				
				RequiredFieldValidator rfvValidator = (RequiredFieldValidator) e.Item.FindControl("rfvDEF_SEQ");
				rfvValidator.ErrorMessage			= Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
				
				RegularExpressionValidator revValidator = (RegularExpressionValidator) e.Item.FindControl("revDEF_SEQ");
				revValidator.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("163");
				revValidator.ValidationExpression = aRegExpInteger;
				revValidator.Attributes.Add("RowNo",(e.Item.ItemIndex + 2).ToString());
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
		#endregion
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			base.ScreenId	=	"186_0";
			//btnReset.Attributes.Add("onClick","javascript:return Reset();");	
			lblMessage.Visible = false;
			
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************		
			#region SetbtnSecurity
			btnReset.CmsButtonClass	=	CmsButtonType.Execute;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Execute;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
			btnDelete.PermissionString		=	gstrSecurityXML;

			btnNext.CmsButtonClass	=	CmsButtonType.Execute;
			btnNext.PermissionString		=	gstrSecurityXML;

			btnPrevious.CmsButtonClass	=	CmsButtonType.Execute;
			btnPrevious.PermissionString		=	gstrSecurityXML;
			#endregion 
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Accounting.AddTransactionCodeGroupDetails" ,System.Reflection.Assembly.GetExecutingAssembly());

			//******************lookup
			//string rootPath = Cms.BusinessLayer.BlCommon.ClsCommon.GetApplicationPath();				
			//url = "'"+rootPath + @"/cmsweb/aspx/LookupForm.aspx"+"'";

			url = ClsCommon.GetLookupWindowURL();			
			btnDelete.Attributes.Add("onclick","javascript:return CheckDelete();");

			btnDelete.Enabled = false;

			hidpageDefaultsize.Value=(ConfigurationSettings.AppSettings["CoverageRows"]).ToString(); 
			

			if(!Page.IsPostBack)
			{
				
				hidMaxRows.Value = ConfigurationManager.AppSettings.Get("CoverageRows");
				//SetCaptions();
				GetQueryString();
				hidCurrentPage.Value="0";
				ViewState["CurrentPageIndex"] = 1;
				if(Request.QueryString["TRAN_GROUP_ID"]!=null)
				{				
					try
					{
						//BindGridPage(1,dgrCashReceipt.PageSize);

						BindGrid(1,dgTranCodeGrpDetails.PageSize );					
					}
					catch(Exception ex)
					{
						Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
						return;
					}					
				}
			}			
		}//end pageload
		#endregion
			
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private ClsTransactionCodeGroupDetailsInfo[] GetFormValue()
		{
			int noOfRows;
			if(txtTRAN_CODE.Text.Length>0)
				noOfRows=1;
			else
				noOfRows=0;

			for(int i=0;i<int.Parse(hidMaxRows.Value);i++)
			{
				if(Request.Form["txtTRAN_CODE"+i.ToString()]==null || Request.Form["txtTRAN_CODE"+i.ToString()].ToString().Length <=0)
					continue;
				noOfRows++;
			}
			ClsTransactionCodeGroupDetailsInfo[] objTransactionCodeGroupDetailsInfo = new ClsTransactionCodeGroupDetailsInfo[noOfRows];
			for(int i=0;i<objTransactionCodeGroupDetailsInfo.Length;i++)
			{
				objTransactionCodeGroupDetailsInfo[i] = new ClsTransactionCodeGroupDetailsInfo();				
			}
			
			int init=1;

			if(hidDETAIL_ID.Value!="")
			{
				if(hidDETAIL_ID.Value.ToUpper() != "NEW")
					objTransactionCodeGroupDetailsInfo[0].DETAIL_ID		 =	int.Parse(hidDETAIL_ID.Value);
				objTransactionCodeGroupDetailsInfo[0].DEF_SEQ		 =	int.Parse(txtDEF_SEQ.Text);
				objTransactionCodeGroupDetailsInfo[0].TRAN_ID        =  int.Parse(hidTRAN_ID.Value);
				objTransactionCodeGroupDetailsInfo[0].TRAN_GROUP_ID  =  int.Parse(Request.QueryString["TRAN_GROUP_ID"].ToString());
				objTransactionCodeGroupDetailsInfo[0].MODIFIED_BY = int.Parse(GetUserId());
				objTransactionCodeGroupDetailsInfo[0].LAST_UPDATED_DATETIME = DateTime.Now;
				
			}
			else
				init = 0;
			for(int i=1,j=init;j<objTransactionCodeGroupDetailsInfo.Length;j++)
			{
				//Start: skiping blank rows in between
				i=j;
				for(int k=1;k<int.Parse(hidMaxRows.Value);k++)
				{
					if(Request.Form["txtTRAN_CODE"+i.ToString()]==null || Request.Form["txtTRAN_CODE"+i.ToString()].ToString().Length <=0)
						i++;
				}
				//End: skiping blank rows in between

				if(Request.Form["hidDETAIL_ID"+i.ToString()]!=null && Request.Form["hidDETAIL_ID"+i.ToString()].ToString().Length>0 )
					objTransactionCodeGroupDetailsInfo[j].DETAIL_ID		=	int.Parse(Request.Form["hidDETAIL_ID"+i.ToString()]);
				objTransactionCodeGroupDetailsInfo[j].DEF_SEQ		 =	int.Parse(Request.Form["txtDEF_SEQ"+i.ToString()]);
				objTransactionCodeGroupDetailsInfo[j].TRAN_ID        =  int.Parse(Request.Form["hidTRAN_ID"+i.ToString()]); 
				objTransactionCodeGroupDetailsInfo[j].TRAN_GROUP_ID  =  int.Parse(Request.QueryString["TRAN_GROUP_ID"]);
				objTransactionCodeGroupDetailsInfo[j].MODIFIED_BY = int.Parse(GetUserId());
				objTransactionCodeGroupDetailsInfo[j].LAST_UPDATED_DATETIME = DateTime.Now;			
			}
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidDETAIL_ID.Value;
			oldXML			=	hidOldData.Value;
			//Returning the model object

			return objTransactionCodeGroupDetailsInfo;
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
			this.dgTranCodeGrpDetails.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgTranCodeGrpDetails_ItemDataBound);
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
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
			ArrayList alRecr = new ArrayList();
			try
			{
			
				foreach(DataGridItem dgi in dgTranCodeGrpDetails.Items)
				{
					if ( dgi.ItemType == ListItemType.Item || dgi.ItemType == ListItemType.AlternatingItem )
					{
						TextBox txtText;
						txtText = (TextBox)dgi.FindControl("txtDEF_SEQ");

						if (txtText.Text == "")
							continue;

						ClsTransactionCodeGroupDetailsInfo objInfo = new ClsTransactionCodeGroupDetailsInfo();
 
						//Saving the policy no and version no
					
						if (((HtmlInputHidden)dgi.FindControl("hidDETAIL_ID")).Value != "")
						{
							objInfo.DETAIL_ID = int.Parse(((HtmlInputHidden)dgi.FindControl("hidDETAIL_ID")).Value);						
						}

						objInfo.TRAN_GROUP_ID = int.Parse((hidTRAN_GROUP_ID).Value);						
						
						if (((HtmlInputHidden)dgi.FindControl("hidTRAN_ID")).Value != "")
						{
							objInfo.TRAN_ID = int.Parse(((HtmlInputHidden)dgi.FindControl("hidTRAN_ID")).Value.Split(',')[0]);						
						}
						
						if (((TextBox)dgi.FindControl("txtDEF_SEQ")).Text != "")
						{
							objInfo.DEF_SEQ = int.Parse(((TextBox)dgi.FindControl("txtDEF_SEQ")).Text);
						}
						objInfo.IS_ACTIVE = "Y";	

						objInfo.CREATED_BY = int.Parse(GetUserId());
						objInfo.CREATED_DATETIME = DateTime.Now;

						if ( dgTranCodeGrpDetails.DataKeys[dgi.ItemIndex] != System.DBNull.Value )
						{
							objInfo.DETAIL_ID = Convert.ToInt32(dgTranCodeGrpDetails.DataKeys[dgi.ItemIndex]);
							objInfo.MODIFIED_BY = int.Parse(GetUserId());
							objInfo.LAST_UPDATED_DATETIME = DateTime.Now;
						}
						else
						{
							objInfo.DETAIL_ID = -1;
						}

						alRecr.Add(objInfo);
					}
				}
			
				//ClsDepositDetails objDeposit = new ClsDepositDetails();
				ClsTransactionCodeGroupDetails objDeposit =new ClsTransactionCodeGroupDetails();
				ArrayList alStatus;

				if ( objDeposit.AddTransactionCodeGroupDetail(alRecr, out alStatus)>0)
				{
					//saved successfully
					lblMessage.Text = ClsMessages.FetchGeneralMessage("29");
					//BindGrid();
					BindGrid(int.Parse(hidCurrentPage.Value),int.Parse(hidpageDefaultsize.Value));
				}
				else
				{
					//error occured, showing the error message
					lblMessage.Text = ClsMessages.FetchGeneralMessage("128");
					//ShowLineItemStatus(alStatus);
				}
				
				lblMessage.Visible = true;
				
			}
			catch (Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		protected void Navigation_Click(Object sender,CommandEventArgs e )
		{			
			switch ( e.CommandName)
			{				
				case "Next":
					
					_currentPageNumber =int.Parse(hidCurrentPage.Value) + 1;
					btnSave_Click(sender,e);		
					break;
				case "Previous":
					if (int.Parse(hidCurrentPage.Value) > 1)
					{
						_currentPageNumber =int.Parse(hidCurrentPage.Value) - 1;
						btnSave_Click(sender,e);
					}
					else
						_currentPageNumber =1;

					break;
			}			
			BindGrid(_currentPageNumber,dgTranCodeGrpDetails.PageSize);

		}


		#endregion
		/// <summary>
		/// Get query string from url into hidden controls
		/// </summary>
		private void GetQueryString()
		{
			hidTRAN_GROUP_ID.Value		= Request.Params["TRAN_GROUP_ID"];
		}

		

		private void btnDelete_Click(object sender, System.EventArgs e)
		{

			ClsTransactionCodeGroupDetails objTransactionCodeGroupDetails= new ClsTransactionCodeGroupDetails();
			CheckBox chkBox;
			DataTable dt=new DataTable();
			dt.Columns.Add("RowID",typeof(int));
			try
			{				
				foreach(DataGridItem dgi in dgTranCodeGrpDetails.Items)
				{
					chkBox=(CheckBox)dgi.FindControl("chkDelete");
					if (chkBox != null && chkBox.Checked)
					{
						if (dgTranCodeGrpDetails.DataKeys[dgi.ItemIndex].ToString() !="")
						{
							DataRow dr=dt.NewRow();
							dr["RowID"]=int.Parse(dgTranCodeGrpDetails.DataKeys[dgi.ItemIndex].ToString());	
							dt.Rows.Add(dr);
						}
					}			
				}
				if (dt.Rows.Count > 0)	
				{	
					objTransactionCodeGroupDetails.Delete(dt);
					//BindGrid();	
					BindGrid(int.Parse(hidCurrentPage.Value),dgTranCodeGrpDetails.PageSize);				
					lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("127");
					lblMessage.Visible=true;
				}									
			}
			catch(Exception ex)
			{
				//gIntSaved=0;
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;			
			}		
		}
		



		/// <summary>
		/// Binds the data set with the data grid
		/// </summary>
		private void BindGrid(int currentPage,int pageSize )
		{
			
			ClsTransactionCodeGroupDetails objTransactionCodeGroupDetails = new ClsTransactionCodeGroupDetails();
			hidCurrentPage.Value=currentPage.ToString();			
			
			if (int.Parse(hidCurrentPage.Value) > 1)
				btnPrevious.Enabled = true;
			else
				btnPrevious.Enabled = false;
			

			DataSet ds;//			
			ds = objTransactionCodeGroupDetails.GetTransactionCodeDetails(hidTRAN_GROUP_ID.Value.ToString(),int.Parse(hidCurrentPage.Value),pageSize);			

			if (ds.Tables[1].Rows.Count > 0)
				btnDelete.Enabled = true;
			else
				btnDelete.Enabled = false;
			
			int totRecords = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
			int TotalPages = 0;			
			
			if( totRecords % pageSize  == 0 ) 
				TotalPages = totRecords / pageSize;
			else
				TotalPages = (totRecords / pageSize) + 1;			
			int currentRowCount = ds.Tables[1].Rows.Count;

			if  ( currentRowCount < pageSize )
			{
				for(int i = 0; i < pageSize - currentRowCount; i++ )
				{
					DataTable dt = ds.Tables[1];
					DataRow dr = dt.NewRow();					
					dt.Rows.Add(dr);
				}
				btnNext.Enabled = false;
			}
			else
				btnNext.Enabled = true;
			

			dgTranCodeGrpDetails.DataSource = ds.Tables[1];
			dgTranCodeGrpDetails.DataBind();			
		}

		private void dgTranCodeGrpDetails_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
				{
					System.Web.UI.WebControls.WebControl ctrl = (System.Web.UI.WebControls.WebControl)e.Item.FindControl("imgSelect");
					ctrl.Attributes.Add("onclick","OpenNewLookupEx('" + (e.Item.ItemIndex + 2).ToString() + "');");

					//Setting properties of valdator control
					SetValidators(e);

					if ( dgTranCodeGrpDetails.DataKeys[e.Item.ItemIndex] == System.DBNull.Value )
					{
						ctrl = (System.Web.UI.WebControls.WebControl)e.Item.FindControl("chkDelete");
						ctrl.Visible = false;
					}
				}
				
			}
			catch (Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			BindGrid(int.Parse(hidCurrentPage.Value),dgTranCodeGrpDetails.PageSize);
		}

	}

			
}

