/******************************************************************************************
<Author				: -   Mohit Gupta
<Start Date				: -	7/5/2005 11:31:45 AM
<End Date				: -	
<Description				: - 	
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - Ravindra 
<Modified By			: - 05-09-2006
<Purpose				: - 
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb.WebControls;
using Cms.Model.Maintenance;
using Cms.CmsWeb.Controls;
using System.Resources; 
using System.Reflection; 
using Cms.ExceptionPublisher; 
using System.Configuration;


namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddCoverageRange.
	/// </summary>
	public class AddCoverageRange : Cms.CmsWeb.cmsbase
	{
		#region PageControls Declaration 
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCOV_ID;
		protected System.Web.UI.WebControls.DataGrid dgrLimitRanges;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.Button btnPrevious;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidtotalPages;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCurrentPage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLineItemId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCash;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidpageDefaultsize;
		protected System.Web.UI.WebControls.CheckBox chkIncludeDeactive;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFor;
		protected System.Web.UI.WebControls.Label lblText;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLimitType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOBID;
        protected System.Web.UI.WebControls.Label headerLimit;
        protected System.Web.UI.WebControls.Label headerAdditional;
        protected System.Web.UI.WebControls.Label headerDeductible;
        ResourceManager Objresources;
		#endregion 
		
		protected Int32 _currentPageNumber = 1;
		public string isRankUnique="true";
		private string strType;
		ClsCoverageRange  ObjAddCoverageRange ;
		public DataTable dtTemp;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOLD_DATA;
		public int count=0;

        String CalledFrom = String.Empty;
		private void Page_Load(object sender, System.EventArgs e)
		{
		
			hidpageDefaultsize.Value=(ConfigurationSettings.AppSettings["CoverageRows"]).ToString();

            if (Request.QueryString["CalledFor"] != null && Request.QueryString["CalledFor"].ToString() != "")
            {
                CalledFrom = Request.QueryString["CalledFor"].ToString();
                switch (CalledFrom.ToUpper().Trim())
                {
                    case "LIMIT":
                        base.ScreenId = "492_0_1";
                        break;

                    case "DEDUCT":
                        base.ScreenId = "492_0_2";
                        break;
                    default:
                        base.ScreenId = "492_0_1";
                        break;
                }
            }
            else
            {
                base.ScreenId = "492_0_1";
            }
            

		 
			//base.ScreenId="198_0_1";
            
			lblMessage.Visible = false;
			
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass=CmsButtonType.Write;
			btnActivateDeactivate.PermissionString=gstrSecurityXML;
			
			btnActivateDeactivate1.CmsButtonClass=CmsButtonType.Write;
			btnActivateDeactivate1.PermissionString=gstrSecurityXML;
            Objresources = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddCoverageRange", System.Reflection.Assembly.GetExecutingAssembly());
			
			//Added By Ravindra(05-10-2006)
			btnActivateDeactivate.Visible=false;
			btnActivateDeactivate1.Visible=false;
			//Added By Ravindra Ends Here
			
			if(!Page.IsPostBack)
			{					
				Session["saveClick"] = "false";
				Session["ScrollBtnClick"] = "false";
				Session["Count"]="0";
				SetHiddenFields();
                SetCaptions();
				BindGridPage(1,int.Parse(hidpageDefaultsize.Value));

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
			this.dgrLimitRanges.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgrLimitRanges_ItemDataBound);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		public void BindGridPage(int currentPage,int pageSize)
		{			
			int totalRecord;
			//Double totalPages = 1;
			Int32 _totalRecords=0; 
			Double _totalPages = 1;
			Session["flagMode"]="";
			_currentPageNumber=currentPage;

			hidCurrentPage.Value=currentPage.ToString();
			

			if (hidCalledFor.Value=="Limit")
			{
				strType="Limit";
			}
			else if(hidCalledFor.Value=="Deduct")
			{
				strType="Deduct";
			}
			else if(hidCalledFor.Value=="AddDeduct" || hidCalledFor.Value=="Addded")
			{
				strType="Addded";
			}
			dtTemp=ClsCoverageRange.GetCoverageRangeXml(int.Parse(hidCOV_ID.Value),"Y",strType,currentPage,pageSize,out totalRecord);				
			
			hidOLD_DATA.Value=ClsCommon.GetXMLEncoded(dtTemp);
			if ( totalRecord > 0 )
			{			
				Session["flagMode"]="Update";	
			}
			else
			{
				Session["flagMode"]="Save";
			}	
			if (Session["flagMode"].ToString() == "Update")
			{
				if (dtTemp.Rows.Count < int.Parse(hidpageDefaultsize.Value))
				{
					int addMoreRows=int.Parse(hidpageDefaultsize.Value)-dtTemp.Rows.Count;
					for (int i=0;i < addMoreRows ; i++)
					{
						DataRow dr=dtTemp.NewRow();
						dr["LIMIT_DEDUC_ID"]=DBNull.Value;	
						dr["COV_ID"]=DBNull.Value;		
						dr["LIMIT_DEDUC_TYPE"]=DBNull.Value;	
						dr["LIMIT_DEDUC_AMOUNT"]=DBNull.Value;
						dr["LIMIT_DEDUC_AMOUNT_TEXT"]=DBNull.Value;
						dr["LIMIT_DEDUC_AMOUNT1"]=DBNull.Value;
						dr["LIMIT_DEDUC_AMOUNT1_TEXT"]=DBNull.Value;
						dr["RANK"]=DBNull.Value;		
						dr["IS_ACTIVE"]=DBNull.Value;	
						dtTemp.Rows.Add(dr);
					}
					dgrLimitRanges.DataSource=dtTemp.DefaultView;
					dgrLimitRanges.DataBind();
					_totalRecords=totalRecord;
		
				}
				else if (dtTemp.Rows.Count == int.Parse(hidpageDefaultsize.Value) || dtTemp.Rows.Count > int.Parse(hidpageDefaultsize.Value) )
				{	
					dgrLimitRanges.DataSource=dtTemp.DefaultView;
					dgrLimitRanges.DataBind();
					_totalRecords=totalRecord;
				}
			}
			else if (Session["flagMode"].ToString() == "Save")
			{
				try
				{
					for (int i=0;i<10;i++)
					{
						DataRow dr=dtTemp.NewRow();
						dr["LIMIT_DEDUC_ID"]=DBNull.Value;	
						dr["COV_ID"]=DBNull.Value;		
						dr["LIMIT_DEDUC_TYPE"]=DBNull.Value;	
						dr["LIMIT_DEDUC_AMOUNT"]=DBNull.Value;
						dr["LIMIT_DEDUC_AMOUNT1"]=DBNull.Value;
						dr["LIMIT_DEDUC_AMOUNT_TEXT"]=DBNull.Value;
						dr["LIMIT_DEDUC_AMOUNT1_TEXT"]=DBNull.Value;
						dr["RANK"]=DBNull.Value;		
						dr["IS_ACTIVE"]=DBNull.Value;	
						dtTemp.Rows.Add(dr);
					}
					dgrLimitRanges.DataSource=dtTemp.DefaultView;
					dgrLimitRanges.DataBind();
					//_totalRecords=10;
					_totalRecords=totalRecord;
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
			
			_totalPages=_totalRecords/int.Parse(hidpageDefaultsize.Value);


			if ( _currentPageNumber == 1 )
			{
				btnPrevious.Enabled=false;
				if ( _totalPages >= 1 && _totalRecords >= 10 )
				{
					btnNext.Enabled=true;					
				}
				else
				{
					btnNext.Enabled=false;					
				}
			}
			else
			{
				btnPrevious.Enabled=true;
				int p=_currentPageNumber*10;
				//_currentPageNumber == _totalPages &&
				if ( _totalRecords < p)
				{
					btnNext.Enabled=false;
				}
				else
				{
					btnNext.Enabled=true;
				}
			}

		}

   
		private void SetHiddenFields()
		{
			if (Request.QueryString["COVID"] != null && Request.QueryString["COVID"].ToString() != "")
			{ 						
				hidCOV_ID.Value=Request.QueryString["COVID"].ToString(); 
			}
			if (Request.QueryString["CalledFor"] != null && Request.QueryString["CalledFor"].ToString() != "")
			{
				hidCalledFor.Value=Request.QueryString["CalledFor"].ToString(); 
				if(hidCalledFor.Value == "AddDeduct")
					hidCalledFor.Value = "Addded";
			}					
			if (Request.QueryString["LimitType"] != null && Request.QueryString["LimitType"].ToString() != "")
			{ 						
				hidLimitType.Value=Request.QueryString["LimitType"].ToString(); 
			}
			if (Request.QueryString["LobId"] != null && Request.QueryString["LobId"].ToString() != "")
			{ 						
				hidLOBID.Value=Request.QueryString["LobId"].ToString(); 
			}
			
		}


        private void SetCaptions()
        {

            dgrLimitRanges.Columns[2].HeaderText = Objresources.GetString("txtRank");
            dgrLimitRanges.Columns[3].HeaderText = Objresources.GetString("txtAmount");
            dgrLimitRanges.Columns[4].HeaderText = Objresources.GetString("txtAmountText");
            dgrLimitRanges.Columns[6].HeaderText = Objresources.GetString("txtAmount1");
            dgrLimitRanges.Columns[7].HeaderText = Objresources.GetString("txtAmount1Text");
            dgrLimitRanges.Columns[8].HeaderText = Objresources.GetString("txtEFFECTIVE_FROM_DATE");
            dgrLimitRanges.Columns[9].HeaderText = Objresources.GetString("txtEFFECTIVE_TO_DATE");
            dgrLimitRanges.Columns[10].HeaderText = Objresources.GetString("txtDISABLED_DATE");
            btnPrevious.Text = Objresources.GetString("btnPrevious");
            btnNext.Text = Objresources.GetString("btnNext");
            headerAdditional.Text = Objresources.GetString("headerAdditional");
            headerDeductible.Text = Objresources.GetString("headerDeductible");
            headerLimit.Text = Objresources.GetString("headerLimit");
         
            
        }

		protected void Navigation_Click(Object sender,CommandEventArgs e )
		{	
				
				Session["ScrollBtnClick"] = "true";

				switch ( e.CommandName)
				{				
					case "Next":						
						_currentPageNumber =int.Parse(hidCurrentPage.Value) + 1;
						break;
					case "Previous":
						_currentPageNumber =int.Parse(hidCurrentPage.Value) - 1;
						break;
				}
				BindGridPage(_currentPageNumber,int.Parse(hidpageDefaultsize.Value));			
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			BindGridPage(int.Parse(hidCurrentPage.Value),int.Parse(hidpageDefaultsize.Value));
		}
		private void dgrLimitRanges_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				HyperLink  hlkEFFECTIVE_FROM_DATE =(HyperLink)e.Item.FindControl("hlkEFFECTIVE_FROM_DATE");
				HyperLink hlkEFFECTIVE_TO_DATE =(HyperLink)e.Item.FindControl("hlkEFFECTIVE_TO_DATE");
				HyperLink hlkDISABLED_DATE =(HyperLink)e.Item.FindControl("hlkDISABLED_DATE");

				TextBox txtEFFECTIVE_FROM_DATE=(TextBox)e.Item.FindControl("txtEFFECTIVE_FROM_DATE");
				TextBox txtEFFECTIVE_TO_DATE=(TextBox)e.Item.FindControl("txtEFFECTIVE_TO_DATE");
				TextBox txtDISABLED_DATE=(TextBox)e.Item.FindControl("txtDISABLED_DATE");

				hlkEFFECTIVE_FROM_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('" + txtEFFECTIVE_FROM_DATE.ClientID + "'), document.getElementById('" + txtEFFECTIVE_FROM_DATE.ClientID + "'))");
				hlkEFFECTIVE_TO_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('" + txtEFFECTIVE_TO_DATE.ClientID + "'), document.getElementById('" + txtEFFECTIVE_TO_DATE.ClientID + "'))");
				hlkDISABLED_DATE.Attributes.Add("OnClick","fPopCalendar(document.getElementById('" + txtDISABLED_DATE.ClientID + "'), document.getElementById('" + txtDISABLED_DATE.ClientID + "'))");
				
			
				RegularExpressionValidator revValidator1 = (RegularExpressionValidator) e.Item.FindControl("revLIMIT_DEDUC_AMOUNT");
				revValidator1.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "163");
				revValidator1.ValidationExpression = aRegExpDoublePositiveNonZero;

				RegularExpressionValidator revValidator2 = (RegularExpressionValidator) e.Item.FindControl("revAmoun1");
				revValidator2.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "163");
				revValidator2.ValidationExpression = aRegExpDouble;

				RequiredFieldValidator rfvEFFECTIVE_FROM_DATE=(RequiredFieldValidator)e.Item.FindControl("rfvEFFECTIVE_FROM_DATE"); 
				RegularExpressionValidator revEFFECTIVE_FROM_DATE =(RegularExpressionValidator)e.Item.FindControl("revEFFECTIVE_FROM_DATE");
				RegularExpressionValidator revEFFECTIVE_TO_DATE =(RegularExpressionValidator)e.Item.FindControl("revEFFECTIVE_TO_DATE");
				RegularExpressionValidator revDISABLED_DATE =(RegularExpressionValidator)e.Item.FindControl("revDISABLED_DATE");
				
				CustomValidator csvEFFECTIVE_TO_DATE =(CustomValidator)e.Item.FindControl("csvEFFECTIVE_TO_DATE");
				CustomValidator csvDISABLED_DATE =(CustomValidator)e.Item.FindControl("csvDISABLED_DATE");
				
				revEFFECTIVE_FROM_DATE.ValidationExpression=aRegExpDate;
				revEFFECTIVE_TO_DATE.ValidationExpression =aRegExpDate;
				revDISABLED_DATE.ValidationExpression =aRegExpDate;
				
				rfvEFFECTIVE_FROM_DATE.ErrorMessage="Please enter start date";
				revEFFECTIVE_FROM_DATE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2088");
				revEFFECTIVE_TO_DATE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2088");
				revDISABLED_DATE.ErrorMessage =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("2088");
				
				csvDISABLED_DATE.Attributes.Add("EndDateId",txtEFFECTIVE_TO_DATE.ClientID);
				csvDISABLED_DATE.ErrorMessage=ClsMessages.GetMessage("198_0","16");
				csvEFFECTIVE_TO_DATE.Attributes.Add("StartDateId",txtEFFECTIVE_FROM_DATE.ClientID);
				csvEFFECTIVE_TO_DATE.ErrorMessage=ClsMessages.GetMessage("198_0","15");

				HtmlInputHidden hidLIMIT_DEDUC_ID=(HtmlInputHidden)e.Item.FindControl("hidLIMIT_DEDUC_ID");
				if(hidLIMIT_DEDUC_ID.Value.ToString().Trim()!="")
				{
					TextBox txtRank=(TextBox)e.Item.FindControl("txtRank");
					TextBox txtAmount=(TextBox)e.Item.FindControl("txtAmount");
					TextBox txtAmountText=(TextBox)e.Item.FindControl("txtAmountText");
					TextBox txtAmount1=(TextBox)e.Item.FindControl("txtAmount1");
					TextBox txtAmount1Text=(TextBox)e.Item.FindControl("txtAmount1Text");
					txtRank.Attributes.Add("readOnly","true");
					txtAmount.Attributes.Add("readOnly","true");
					txtAmountText.Attributes.Add("readOnly","true");
					txtAmount1.Attributes.Add("readOnly","true");
					txtAmount1Text.Attributes.Add("readOnly","true");
					revValidator1.Enabled=false;
					revValidator2.Enabled=false;

				}
			}	
		}
		public void checkUniqueRank()
		{
			int rank=0;	
			TextBox txt;
			TextBox txt1;
			TextBox txt2;
			int count=0;
			foreach(DataGridItem dgi in dgrLimitRanges.Items)
			{	
				count=count+1;
				txt=(TextBox)dgi.Cells[3].FindControl("txtAmount");
				//lbl=(Label)dgi.Cells[2].FindControl("lblStatus");
				if(txt !=null && txt.Text != "")
				{			
					//hidLimitType.Value
					txt1=(TextBox)dgi.Cells[1].FindControl("txtRank");
					if (txt1 != null && txt1.Text !="")
					{
						rank=int.Parse(txt1.Text);
						for (int i=count;i < dgrLimitRanges.Items.Count;i++)
						{
							txt2=(TextBox)dgrLimitRanges.Items[i].Cells[1].FindControl("txtRank");					
							if (txt2 != null && txt2.Text !="")
							{
								if (rank == int.Parse(txt2.Text))
								{
                                    isRankUnique="false";
									break;
								}
							}
						}
					}					
				}						
			}
		}
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			
			ArrayList arrRangeInfo=new ArrayList ();
			foreach(DataGridItem dgi in dgrLimitRanges.Items)
			{	
				HtmlInputHidden hidLIMIT_DEDUC_ID=(HtmlInputHidden)dgi.FindControl("hidLIMIT_DEDUC_ID"); 
				TextBox txtRank=(TextBox)dgi.FindControl("txtRank");
				TextBox txtAmount=(TextBox)dgi.FindControl("txtAmount");
				TextBox txtAmountText=(TextBox)dgi.FindControl("txtAmountText");
				TextBox txtAmount1=(TextBox)dgi.FindControl("txtAmount1");
				TextBox txtAmount1Text=(TextBox)dgi.FindControl("txtAmount1Text");
				TextBox txtEFFECTIVE_FROM_DATE=(TextBox)dgi.FindControl("txtEFFECTIVE_FROM_DATE");
				TextBox txtEFFECTIVE_TO_DATE=(TextBox)dgi.FindControl("txtEFFECTIVE_TO_DATE");
				TextBox txtDISABLED_DATE=(TextBox)dgi.FindControl("txtDISABLED_DATE");
				
				if(txtAmount.Text.Trim() !="" || txtAmount1.Text.Trim() !="" ||
					txtAmountText.Text.Trim() != "" || txtAmount1Text.Text.Trim() != "")  
				{
					ClsCoverageRangeInfo objInfo =new ClsCoverageRangeInfo();

					objInfo.COV_ID= Convert.ToInt32(hidCOV_ID.Value.ToString()); 
					if(hidLIMIT_DEDUC_ID.Value==null || hidLIMIT_DEDUC_ID.Value.ToString().Trim()=="")
						objInfo.LIMIT_DEDUC_ID =0;
					else
						objInfo.LIMIT_DEDUC_ID = Convert.ToInt32(hidLIMIT_DEDUC_ID.Value.ToString());
					if(txtRank.Text.Trim() != "")
						objInfo.RANK=Convert.ToInt32(txtRank.Text.Trim());
					objInfo.LIMIT_DEDUC_TYPE =hidCalledFor.Value.ToString();
					if(txtAmount.Text.Trim() !="")
						objInfo.LIMIT_DEDUC_AMOUNT =Convert.ToDecimal (txtAmount.Text.Trim()); 
					if(txtAmount1.Text.Trim() != "")
						objInfo.LIMIT_DEDUC_AMOUNT1 =Convert.ToDecimal(txtAmount1.Text.Trim());
					objInfo.LIMIT_DEDUC_TEXT =txtAmountText.Text.Trim();
					objInfo.LIMIT_DEDUC_TEXT1 =txtAmount1Text.Text.Trim();
					
					if(txtEFFECTIVE_FROM_DATE.Text.Trim() != "")
						objInfo.EFFECTIVE_FROM_DATE=Convert.ToDateTime(txtEFFECTIVE_FROM_DATE.Text);

					if(txtEFFECTIVE_TO_DATE.Text.Trim() != "")
						objInfo.EFFECTIVE_TO_DATE=Convert.ToDateTime(txtEFFECTIVE_TO_DATE.Text);

					if(txtDISABLED_DATE.Text.Trim() != "")
						objInfo.DISABLED_DATE=Convert.ToDateTime(txtDISABLED_DATE.Text);
					objInfo.CREATED_BY=Convert.ToInt32(GetUserId());
					objInfo.MODIFIED_BY=objInfo.CREATED_BY;
					arrRangeInfo.Add(objInfo);

				}
 			}
			ClsCoverageRange objCoverageRange = new ClsCoverageRange();
			lblMessage.Visible=true;
			try
			{
				int intRetVal = objCoverageRange.Add(arrRangeInfo,hidOLD_DATA.Value );
				if(intRetVal == 1)
				{
					lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("31");
					BindGridPage(int.Parse(hidCurrentPage.Value),int.Parse(hidpageDefaultsize.Value));
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text =Cms.CmsWeb.ClsMessages.FetchGeneralMessage("21") + ex.Message;
			}
			

			
			/*
			
			TextBox txt;
			TextBox txt1;
			Label lbl;
			
			checkUniqueRank(); 
			if ( isRankUnique == "true")
			{
				int result=0;
				//	bool flagCheck=false;
				Session["saveClick"]="true";
				ObjAddCoverageRange = new  ClsCoverageRange();		
			
				DataTable dtAdd=new DataTable();
				dtAdd.Columns.Add("COV_ID",typeof(int));
				dtAdd.Columns.Add("LIMIT_DEDUC_AMOUNT",typeof(double));
				dtAdd.Columns.Add("LIMIT_DEDUC_AMOUNT_TEXT",typeof(string));
				dtAdd.Columns.Add("LIMIT_DEDUC_TYPE",typeof(string));
				dtAdd.Columns.Add("LIMIT_DEDUC_AMOUNT1",typeof(double));
				dtAdd.Columns.Add("LIMIT_DEDUC_AMOUNT1_TEXT",typeof(string));
				dtAdd.Columns.Add("RANK",typeof(int));
				dtAdd.Columns.Add("IS_ACTIVE",typeof(int));

				DataTable dtUpdate=new DataTable();
				dtUpdate.Columns.Add("LIMIT_DEDUC_ID",typeof(int));
				dtUpdate.Columns.Add("COV_ID",typeof(int));
				dtUpdate.Columns.Add("LIMIT_DEDUC_AMOUNT",typeof(double));
				dtUpdate.Columns.Add("LIMIT_DEDUC_AMOUNT_TEXT",typeof(string));
				dtUpdate.Columns.Add("LIMIT_DEDUC_TYPE",typeof(string));
				dtUpdate.Columns.Add("LIMIT_DEDUC_AMOUNT1",typeof(double));
				dtUpdate.Columns.Add("LIMIT_DEDUC_AMOUNT1_TEXT",typeof(string));
				dtUpdate.Columns.Add("RANK",typeof(int));
				dtUpdate.Columns.Add("IS_ACTIVE",typeof(int));
						
				try
				{	
					foreach(DataGridItem dgi in dgrLimitRanges.Items)
					{
						if(dgrLimitRanges.DataKeys[dgi.ItemIndex].ToString() == "")
						{
							txt=(TextBox)dgi.Cells[3].FindControl("txtAmount");
							
							TextBox txtAmountText =(TextBox)dgi.Cells[3].FindControl("txtAmountText");

							lbl=(Label)dgi.Cells[2].FindControl("lblStatus");
							if(txt !=null && txt.Text != "")
							{							  
								DataRow dr=dtAdd.NewRow();
								dr["COV_ID"]=hidCOV_ID.Value.ToString();
								dr["LIMIT_DEDUC_AMOUNT"]=txt.Text;	
								dr["LIMIT_DEDUC_TYPE"]=hidCalledFor.Value;
								if (lbl != null)
								{
									if(lbl.Text == "Active" || lbl.Text == "")
									{
										dr["IS_ACTIVE"]="1";		
									}
									else if (lbl.Text == "Deactive") 
									{
										dr["IS_ACTIVE"]="0";		
									}
								}
	
								if ( txtAmountText != null )
								{
									dr["LIMIT_DEDUC_AMOUNT_TEXT"] = txtAmountText.Text.Trim();
								}

                                //hidLimitType.Value
								txt1=(TextBox)dgi.Cells[3].FindControl("txtAmount1");
								TextBox txtAmount1Text =(TextBox)dgi.Cells[3].FindControl("txtAmount1Text");

								if(txt1 !=null && txt1.Text != "")
								{
									dr["LIMIT_DEDUC_AMOUNT1"]=txt1.Text;										
								}
								
								if ( txtAmount1Text != null )
								{
									dr["LIMIT_DEDUC_AMOUNT1_TEXT"] = txtAmount1Text.Text.Trim();										
								}

								txt1=(TextBox)dgi.Cells[1].FindControl("txtRank");
								if(txt1 !=null && txt1.Text != "")
								{
									dr["RANK"]=txt1.Text;										
								}
								dtAdd.Rows.Add(dr);
							}						
						}
						else
						{
							txt=(TextBox)dgi.Cells[3].FindControl("txtAmount");
							lbl=(Label)dgi.Cells[2].FindControl("lblStatus");
							if(txt !=null && txt.Text != "")
							{								
								DataRow dr=dtUpdate.NewRow();
								dr["LIMIT_DEDUC_ID"]=int.Parse(dgrLimitRanges.DataKeys[dgi.ItemIndex].ToString());
								dr["COV_ID"]=hidCOV_ID.Value.ToString();
								dr["LIMIT_DEDUC_AMOUNT"]=txt.Text;
								dr["LIMIT_DEDUC_TYPE"]=hidCalledFor.Value;
								if (lbl != null)
								{
									if(lbl.Text == "Active" || lbl.Text == "")
									{
										dr["IS_ACTIVE"]="1";		
									}
									else if (lbl.Text == "Deactive") 
									{
										dr["IS_ACTIVE"]="0";		
									}
								}
									
								txt1=(TextBox)dgi.Cells[3].FindControl("txtAmount1");
								TextBox txtAmountText =(TextBox)dgi.Cells[3].FindControl("txtAmountText");
								TextBox txtAmount1Text =(TextBox)dgi.Cells[3].FindControl("txtAmount1Text");

								if ( txtAmountText != null )
								{
									dr["LIMIT_DEDUC_AMOUNT_TEXT"] = txtAmountText.Text.Trim();
								}

								if(txt1 !=null && txt1.Text != "")
								{
									dr["LIMIT_DEDUC_AMOUNT1"]=txt1.Text;										
								}									
								txt1=(TextBox)dgi.Cells[1].FindControl("txtRank");
								if(txt1 !=null && txt1.Text != "")
								{
									dr["RANK"]=txt1.Text;										
								}

								if ( txtAmount1Text != null )
								{
									dr["LIMIT_DEDUC_AMOUNT1_TEXT"] = txtAmount1Text.Text.Trim();										
								}

								dtUpdate.Rows.Add(dr);
							}				
						}				
					}

					if (dtAdd.Rows.Count >0)
					{
						result=ClsCoverageRange.InsertCoverageLimit(dtAdd);
						Session["Count"]=dtAdd.Rows.Count.ToString();
						//Session["saveClick"]="true";
						BindGridPage(int.Parse(hidCurrentPage.Value),int.Parse(hidpageDefaultsize.Value));
				
					}
					else if (dtUpdate.Rows.Count >0)
					{					
						result=ClsCoverageRange.UpdateCoverageLimit(dtUpdate);
						//Session["saveClick"]="true";
						Session["Count"]=dtUpdate.Rows.Count.ToString();
						BindGridPage(int.Parse(hidCurrentPage.Value),int.Parse(hidpageDefaultsize.Value));
					
					}
					if (dtAdd.Rows.Count >0 && dtUpdate.Rows.Count <= 0)
					{
						lblMessage.Visible=true;			
						lblMessage.Text="Saved successfully.";				
					}
					else 
					{
						lblMessage.Visible=true;			
						lblMessage.Text="Updated successfully.";
					}
				}
				catch(Exception ex)
				{
					lblMessage.Visible=true;			
					lblMessage.Text=ex.Message;				
				}
			}
			else
			{
				lblMessage.Visible=true;			
				lblMessage.Text="No action performed. Please enter unique Rank.";
			}*/
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			CheckBox chkBox;
			//TextBox txt;
			//int result=0;
			ObjAddCoverageRange = new  ClsCoverageRange();		
						
			DataTable dtDeactivate=new DataTable();
			dtDeactivate.Columns.Add("LIMIT_DEDUC_ID",typeof(int));
			try
			{	
				foreach(DataGridItem dgi in dgrLimitRanges.Items)
				{
					if(dgrLimitRanges.DataKeys[dgi.ItemIndex].ToString() != "")
					{
						chkBox=(CheckBox)dgi.FindControl("chkActivate");
						if (chkBox != null && chkBox.Checked)
						{	
							DataRow dr=dtDeactivate.NewRow();
							dr["LIMIT_DEDUC_ID"]=int.Parse(dgrLimitRanges.DataKeys[dgi.ItemIndex].ToString());
							dtDeactivate.Rows.Add(dr);
						}				
					}				
				}
				if (dtDeactivate.Rows.Count >0)
				{
					ClsCoverageRange.ActivateDeactivateCoverageLimit(dtDeactivate,"0");
					BindGridPage(int.Parse(hidCurrentPage.Value),int.Parse(hidpageDefaultsize.Value));
				}
				else
				{
					lblMessage.Visible=true;			
					lblMessage.Text="No action performed. Please select Range.";	
					
				}
			}
			catch(Exception ex)
			{
				lblMessage.Visible=true;			
				lblMessage.Text=ex.Message;				
			}			
		}		

		private void btnActivateDeactivate1_Click(object sender, System.EventArgs e)
		{
			CheckBox chkBox;
			//TextBox txt;
			//int result=0;
			ObjAddCoverageRange = new  ClsCoverageRange();		
						
			DataTable dtActivate=new DataTable();
			dtActivate.Columns.Add("LIMIT_DEDUC_ID",typeof(int));
									
			try
			{	
				foreach(DataGridItem dgi in dgrLimitRanges.Items)
				{
					if(dgrLimitRanges.DataKeys[dgi.ItemIndex].ToString() != "")
					{
						chkBox=(CheckBox)dgi.FindControl("chkActivate");
						if (chkBox != null && chkBox.Checked)
						{	
							DataRow dr=dtActivate.NewRow();
							dr["LIMIT_DEDUC_ID"]=int.Parse(dgrLimitRanges.DataKeys[dgi.ItemIndex].ToString());
							dtActivate.Rows.Add(dr);
						}				
					}				
				}
				if (dtActivate.Rows.Count >0)
				{
					ClsCoverageRange.ActivateDeactivateCoverageLimit(dtActivate,"1");
					BindGridPage(int.Parse(hidCurrentPage.Value),int.Parse(hidpageDefaultsize.Value));
				}
				else
				{
					lblMessage.Visible=true;			
					lblMessage.Text="No action performed. Please select Range.";						
				}
			}
			catch(Exception ex)
			{
				lblMessage.Visible=true;			
				lblMessage.Text=ex.Message;
				
			}	
		}

	}
}