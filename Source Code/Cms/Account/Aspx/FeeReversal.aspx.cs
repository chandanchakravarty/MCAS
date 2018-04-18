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
using Cms.CmsWeb.Controls;
using Cms.BusinessLayer.BlAccount;
using Cms.BusinessLayer.BlCommon;
using Cms.Model.Account;

namespace Cms.Account.Aspx
{
	/// <summary>
	/// Summary description for FeeReversal.
	/// </summary>
	public class FeeReversal : Cms.Account.AccountBase
	{
		protected System.Web.UI.WebControls.TextBox txtFromDate;
		protected System.Web.UI.WebControls.HyperLink hlkFromDate;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvFromDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revFromDate;
		protected System.Web.UI.WebControls.CustomValidator csvFromDate;
		protected System.Web.UI.WebControls.TextBox txtToDate;
		protected System.Web.UI.WebControls.HyperLink hlkToDate;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvFEETYPE;
		protected System.Web.UI.WebControls.CustomValidator csvToDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator revToDate;		
		protected System.Web.UI.WebControls.RegularExpressionValidator revPOLICY_ID;
		protected System.Web.UI.WebControls.Label capPOLICY_ID;
		protected System.Web.UI.WebControls.TextBox txtPOLICY_ID;
		protected System.Web.UI.WebControls.Label capFEE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbFEETYPE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnFind;
		protected System.Web.UI.WebControls.CompareValidator cmpToDate;		
		protected System.Web.UI.HtmlControls.HtmlImage imgSelect;
		protected System.Web.UI.WebControls.DataGrid grdFeesReversal;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolicyID;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		string url="";
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvPOLICY_ID;
		private int NoOfRows=0;
		private ArrayList arrSavedRecords;
		protected Cms.CmsWeb.Controls.CmsButton btnReverse;
		protected Cms.CmsWeb.Controls.CmsButton btnReverseInProgress;	//Added by Shikha #5534(10/03/09).
		public static int numberDiv;
		public const string FEETYPE = "INSF";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			
			base.ScreenId = "319";
			/*********** Setting permissions and class (Read/write/execute/delete)  *************/
			SetPermissions();
			setErrorMessages();
			hlkFromDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtFromDate,document.forms[0].txtFromDate)");
			hlkToDate.Attributes.Add("OnClick","fPopCalendar(document.forms[0].txtToDate,document.forms[0].txtToDate)");
			url = ClsCommon.GetLookupURL();			
			imgSelect.Attributes.Add("onclick","javascript:OpenLookupWithFunction('" + url + "','POLICY_ID','POLICY_NUMBER','hidPolicyID','txtPOLICY_ID','PolicyForFeeRev','Policy','','');");		
			btnReset.Attributes.Add("onclick","javascript:return formReset();");
			btnDelete.Attributes.Add("onclick","javascript:return CheckDelete();");			
			btnSave.Attributes.Add("onclick","if (CheckDelete() == false) return false;");			
			btnReverse.Attributes.Add("onclick","javascript:HideShowReverse();if (CheckDelete() == false) return false;if(!Page_IsValid) return false;");	//Changed by Shikha #5534, 10/03/09.				
			
			if(!IsPostBack)
			{
				BindGrid(null,null,null,null);
			}
		}

		private void setErrorMessages()
		{
			revFromDate.ValidationExpression =aRegExpDate;
			revToDate.ValidationExpression =  aRegExpDate;
			revPOLICY_ID.ValidationExpression = aRegExpAlphaNum;

			revFromDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revToDate.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("179");
			revPOLICY_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("102");

			//			rfvFEETYPE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
			//rfvToDate.ErrorMessage  = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1"); 		

			cmpToDate.ErrorMessage  = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2"); 	
		
		}
		
		/// <summary>
		/// 
		/// </summary>
		private void SetPermissions()
		{
			btnFind.CmsButtonClass		= CmsButtonType.Read;
			btnFind.PermissionString	= gstrSecurityXML;

			btnReset.CmsButtonClass		= CmsButtonType.Write;
			btnReset.PermissionString	= gstrSecurityXML;
			
			btnSave.CmsButtonClass		= CmsButtonType.Write;
			btnSave.PermissionString	= gstrSecurityXML;
			
			btnDelete.CmsButtonClass		= CmsButtonType.Delete;
			btnDelete.PermissionString	= gstrSecurityXML;

			btnReverse.CmsButtonClass		= CmsButtonType.Write;
			btnReverse.PermissionString	= gstrSecurityXML;

			//By Shikha #5534(10/03/09).
			btnReverseInProgress.CmsButtonClass = CmsButtonType.Write;
			btnReverseInProgress.PermissionString = gstrSecurityXML;
			//End
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
			this.cmbFEETYPE.SelectedIndexChanged += new System.EventHandler(this.cmbFEETYPE_SelectedIndexChanged);
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			this.grdFeesReversal.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdFeesReversal_ItemCommand);
			this.grdFeesReversal.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdFeesReversal_ItemDataBound);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			grdFeesReversal.CurrentPageIndex =0;
			BindGrid(txtPOLICY_ID.Text.ToString(),cmbFEETYPE.SelectedValue.ToString(),txtFromDate.Text.ToString(),txtToDate.Text.ToString());			
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="policyNo"></param>
		/// <param name="feeType"></param>
		/// <param name="fromDate"></param>
		/// <param name="toDate"></param>
		private void BindGrid(string policyNo,string feeType,string fromDate,string toDate)
		{
			lblMessage.Visible=false;
			try
			{
				DataSet objDataSet = Cms.BusinessLayer.BlAccount.ClsFeeReversal.GetFeesDetail(policyNo,feeType,fromDate,toDate);
				grdFeesReversal.DataSource =  objDataSet.Tables[0];
				grdFeesReversal.DataBind();	
			
				NoOfRows = objDataSet.Tables[0].Rows.Count;
				if(NoOfRows>0)
				{
					btnSave.Enabled =  true;
					btnDelete.Enabled= true;
					btnReverse.Enabled = true;
					btnReset.Enabled =true;
				}
				else
				{
					btnSave.Enabled =  false;
					btnDelete.Enabled= false;
					btnReverse.Enabled = false;
					btnReset.Enabled =false;
				}
			}
			catch (Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"8") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}

			
		}
		/// <summary>
		/// Sorting Coilumns
		/// </summary>
		/// <param name="policyNo"></param>
		/// <param name="feeType"></param>
		/// <param name="fromDate"></param>
		/// <param name="toDate"></param>
		private void BindGridSort(string policyNo,string feeType,string fromDate,string toDate,string sortExpr)
		{
			lblMessage.Visible=false;
			try
			{
				DataSet objDataSet = Cms.BusinessLayer.BlAccount.ClsFeeReversal.GetFeesDetail(policyNo,feeType,fromDate,toDate);
				//DataView for sorting:
				DataView dvSortView = new DataView(objDataSet.Tables[0]);
				
				if( (numberDiv%2) == 0)
					dvSortView.Sort = sortExpr + " " + "ASC";
				else
					dvSortView.Sort = sortExpr + " " + "DESC";
				numberDiv++;
				grdFeesReversal.DataSource = dvSortView;
				grdFeesReversal.DataBind();	
			
				NoOfRows = objDataSet.Tables[0].Rows.Count;
				if(NoOfRows>0)
				{
					btnSave.Enabled =  true;
					btnDelete.Enabled= true;
					btnReverse.Enabled = true;
					btnReset.Enabled =true;
				}
				else
				{
					btnSave.Enabled =  false;
					btnDelete.Enabled= false;
					btnReverse.Enabled = false;
					btnReset.Enabled =false;
				}
			}
			catch (Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"8") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);

			}

			
		}
		protected void grdFeesReversal_Paging(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs  e)
		{
			lblMessage.Visible=false;
			grdFeesReversal.CurrentPageIndex = e.NewPageIndex; 
			//BindGrid(null,null,null,null);
			BindGrid(txtPOLICY_ID.Text.ToString(),cmbFEETYPE.SelectedValue.ToString(),txtFromDate.Text.ToString(),txtToDate.Text.ToString());			
		}

		
		private bool save_feeReversal()
		{
			ArrayList arrLST_FEES_RECORDS= new ArrayList();
			int[] check = new int[grdFeesReversal.Items.Count];
			int indexGrid = -1;
			try
			{
				foreach(DataGridItem dgi in grdFeesReversal.Items)
				{		
					CheckBox objCheckBox=null;
					Label objLabel=null;
					TextBox objTextBox= null;
					objCheckBox = (CheckBox)dgi.FindControl("chkSelect");
					HtmlInputHidden hidITEM_CODE ;
					indexGrid++;
					if(objCheckBox.Checked)
					{
						check[indexGrid] = 1;
						if (grdFeesReversal.DataKeys[dgi.ItemIndex].ToString() !="")
						{
							ClsFeeReversalInfo objFeeReversalInfo	=	new ClsFeeReversalInfo();

							if (((HtmlInputHidden)dgi.FindControl("hidPOLICY_ID_GRID")).Value != "")
							{
								objFeeReversalInfo.POLICY_ID =int.Parse(((HtmlInputHidden) dgi.FindControl("hidPOLICY_ID_GRID")).Value);								   
							}							
							if (((HtmlInputHidden)dgi.FindControl("hidPOLICY_VERSION_ID")).Value != "")
							{
								objFeeReversalInfo.POLICY_VERSION_ID =int.Parse(((HtmlInputHidden) dgi.FindControl("hidPOLICY_VERSION_ID")).Value);								   
							}
							if (((HtmlInputHidden)dgi.FindControl("hidCUSTOMER_ID")).Value != "")
							{
								objFeeReversalInfo.CUSTOMER_ID =int.Parse(((HtmlInputHidden) dgi.FindControl("hidCUSTOMER_ID")).Value);								   
							}								
			
							hidITEM_CODE = (HtmlInputHidden)dgi.FindControl("hidITEM_CODE");
							if(hidITEM_CODE != null && hidITEM_CODE.Value !="")
							{
								objFeeReversalInfo.FEE_TYPE = hidITEM_CODE.Value;
							}
							
							//							objLabel				=	(Label)dgi.FindControl("lblFeeType");
							//							if(objLabel.Text.ToString() !="")
							//							{
							//								if(objLabel.Text.ToString().ToUpper()=="INSTALLMENT")
							//									objFeeReversalInfo.FEE_TYPE		=	"INSF";
							//								if(objLabel.Text.ToString().ToUpper()== "RE-INSTATEMENT")
							//									objFeeReversalInfo.FEE_TYPE		=	"RNSF";
							//							}

							objLabel				=	(Label)dgi.FindControl("lblFeeAmount");
							
							if(objLabel.Text.ToString() !="")
								objFeeReversalInfo.FEES_AMOUNT	=	double.Parse(objLabel.Text);

							objTextBox	=	(TextBox)dgi.FindControl("txtFeeAmtReversed");
							
							if(objTextBox.Text.ToString() !="")					
								objFeeReversalInfo.FEES_REVERSE	=	double.Parse(objTextBox.Text);					

							objFeeReversalInfo.CREATED_BY		= int.Parse(GetUserId());
							objFeeReversalInfo.CREATED_DATETIME = DateTime.Now;							
							objFeeReversalInfo.CUSTOMER_OPEN_ITEM_ID = int.Parse(((HtmlInputHidden) dgi.FindControl("hidCUSTOMER_OPEN_ITEM_ID")).Value);   			
							
							
							if (((HtmlInputHidden)dgi.FindControl("hidAFR_IDEN_ROW_ID")).Value != "")
							{						
								objFeeReversalInfo.IDEN_ROW_ID = int.Parse(((HtmlInputHidden)dgi.FindControl("hidAFR_IDEN_ROW_ID")).Value);
								objFeeReversalInfo.MODIFIED_BY = int.Parse(GetUserId());
								objFeeReversalInfo.LAST_UPDATED_DATETIME = DateTime.Now;
							}
							else
							{
								objFeeReversalInfo.IDEN_ROW_ID = -1;
							}
							objFeeReversalInfo.IS_ACTIVE = "Y";							
							arrLST_FEES_RECORDS.Add(objFeeReversalInfo);	
						}											
					}
					else
						check[indexGrid] = 0;
				}

				ClsFeeReversal objFeeReversal =new ClsFeeReversal();		
				if (objFeeReversal.AddFeeReversalRecords(arrLST_FEES_RECORDS,out arrSavedRecords)>0)
				{
					return true;
				}
				
				return false;
			}
			catch (Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				throw(ex);
			}
		}
		/// <summary>
		///  Save the records in ACT_FEE_REVERSAL table 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			save_feeReversal();
		}

		
		private void grdFeesReversal_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			try
			{
				if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
				{
					//Setting properties of valdator control
					CheckBox chkBox = (CheckBox) e.Item.FindControl("chkSelect");
					DataRowView drvItem = (DataRowView)e.Item.DataItem;
					if ( drvItem["AFR_IDEN_ROW_ID"] != System.DBNull.Value )						
					{
						chkBox.Checked = true;
					}
					
					//Added on 26 March 2008
					HtmlInputHidden hidMaxAmt = null;
					hidMaxAmt = (HtmlInputHidden)e.Item.FindControl("hidMAX_REVERSE");
					if(hidMaxAmt.Value == "0")
					{
						chkBox.Checked = false;
						chkBox.Enabled = false;
					}
					//Added on 30 may 2008
					HtmlInputHidden hidTotalDue = null;
					hidTotalDue = (HtmlInputHidden)e.Item.FindControl("hidTOTAL_DUE");

					HtmlInputHidden hidFeeType = null;
					hidFeeType = (HtmlInputHidden)e.Item.FindControl("hidITEM_CODE");

					if(hidTotalDue.Value == "0")
					{
						Button btnReverseAmt = null;
						btnReverseAmt = (Button)e.Item.FindControl("btnReverseAmt");
						if(hidFeeType.Value == FEETYPE)
							btnReverseAmt.Visible = true;
						
					}
					
					//Added on 8 June 2009 for #5919 by Shikha Dixit.
					HtmlInputHidden hidDATERECD = null;
					HtmlInputHidden hidRecvdAmt = null;
					hidDATERECD = (HtmlInputHidden)e.Item.FindControl("hidDATERECD");
					hidRecvdAmt = (HtmlInputHidden)e.Item.FindControl("hidRecvdAmt");
					if (hidRecvdAmt.Value=="0")
					{
						e.Item.Cells[4].Text = "";
					}

					//End of Addition.

					SetValidators(e,drvItem);
					System.Web.UI.WebControls.WebControl ctrl= null;
					ctrl = (System.Web.UI.WebControls.WebControl)e.Item.FindControl("chkSelect");
					ctrl.Attributes.Add("OnClick","javascript:  EnableValidators('" + (e.Item.ItemIndex + 3).ToString() + "');onChange(" + (e.Item.ItemIndex + 3).ToString() + ");");

					ctrl = (System.Web.UI.WebControls.WebControl)e.Item.FindControl("txtFeeAmtReversed");
					ctrl.Attributes.Add("enabled","false");
					ctrl.Attributes.Add("onblur","javascript:FormatAmount(this);EnableValidators('" + (e.Item.ItemIndex + 3).ToString() + "');ValidateFeeAmtForReversal('" + ctrl.ClientID + "');");
					
				}				
			}
			catch (Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
		}

		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetValidators(System.Web.UI.WebControls.DataGridItemEventArgs e,DataRowView drvItem)
		{
			try
			{
				
				RequiredFieldValidator rfvValidator = (RequiredFieldValidator) e.Item.FindControl("rfvFeeAmtReversed");
				rfvValidator.ErrorMessage	=  "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4"); // change
				
				RegularExpressionValidator revValidator = (RegularExpressionValidator) e.Item.FindControl("revFeeAmtReversed");
				revValidator.ErrorMessage =  "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"9"); // change
				revValidator.ValidationExpression = aRegExpDoublePositiveNonZero;		

				//RangeValidator rnvValidator = (RangeValidator) e.Item.FindControl("rnvFeeAmtReversed");
				//				rnvValidator.ErrorMessage  =  "<br>" + Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5"); // change
				//				
				//				if ( drvItem["MAX_REVERSE"] != System.DBNull.Value )						
				//				{
				//						rnvValidator.MaximumValue=drvItem["MAX_REVERSE"].ToString();
				//				}
				//				rnvValidator.MinimumValue="0"; 

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbFEETYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindGrid(txtPOLICY_ID.Text.ToString(),cmbFEETYPE.SelectedValue.ToString(),txtFromDate.Text.ToString(),txtToDate.Text.ToString());			
		}

		/// <summary>
		/// delete 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
		
			ClsFeeReversal objFeeReversal	=	new ClsFeeReversal();
			CheckBox chkBox;
			DataTable dt=new DataTable();
			dt.Columns.Add("RowID",typeof(int));
			save_feeReversal();
			try
			{				
				foreach(DataGridItem dgi in grdFeesReversal.Items)
				{
					chkBox=(CheckBox)dgi.FindControl("chkSelect");
					if (chkBox != null && chkBox.Checked)
					{
						
						if (((HtmlInputHidden)dgi.FindControl("hidAFR_IDEN_ROW_ID")).Value != "")
						{
							DataRow dr=dt.NewRow();
							dr["RowID"]=int.Parse(((HtmlInputHidden)dgi.FindControl("hidAFR_IDEN_ROW_ID")).Value);	
							dt.Rows.Add(dr);
						}
						else
						{
							lblMessage.Text = "Record could not be saved first.";
							lblMessage.Visible=true;			
						}

						
					}			
				}
				if (dt.Rows.Count > 0)	
				{	
					
					objFeeReversal.Delete(dt);	
					BindGrid(txtPOLICY_ID.Text.ToString(),cmbFEETYPE.SelectedValue.ToString(),txtFromDate.Text.ToString(),txtToDate.Text.ToString());			
					lblMessage.Text=Cms.CmsWeb.ClsMessages.FetchGeneralMessage("127");
					lblMessage.Visible=true;
				}									
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;			
			}				
		}

		private void btnReverse_Click(object sender, System.EventArgs e)
		{
			ClsFeeReversal objFeeReversal	=	new ClsFeeReversal();
			//CheckBox chkBox;
			DataTable dt=new DataTable();
			dt.Columns.Add("RowID",typeof(int));
			int result=0;
			
			try
			{				
				if(save_feeReversal())
				{
					string strPolNum = txtPOLICY_ID.Text.Trim().ToUpper();
					result = objFeeReversal.Reverse(arrSavedRecords,strPolNum);	
					if(result>0)
					{
						BindGrid(txtPOLICY_ID.Text.ToString(),cmbFEETYPE.SelectedValue.ToString(),txtFromDate.Text.ToString(),txtToDate.Text.ToString());			
						lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6");
					}
					else
					{
						lblMessage.Text=Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"7");
					}
					lblMessage.Visible=true;
				}									
			}
			catch(Exception ex)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				lblMessage.Text=ex.Message;
				lblMessage.Visible=true;			
			}					
		}
		//Sorting
		public void Sort_Grid(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs  e)
		{
			BindGridSort(txtPOLICY_ID.Text.ToString(),cmbFEETYPE.SelectedValue.ToString(),txtFromDate.Text.ToString(),txtToDate.Text.ToString(),e.SortExpression.ToString());			
		}

		private void grdFeesReversal_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(e.CommandName.Equals("UpdateReverseAmount"))
				{
					ClsFeeReversal objFeeReversal	=	new ClsFeeReversal();
					int idenRow =0;
					double insFee = 0.0;
                    int userId = int.Parse(GetUserId());
					int retVal = 0;
					idenRow = int.Parse(grdFeesReversal.DataKeys[e.Item.ItemIndex].ToString());
					HtmlInputHidden hidINSTALLMENT_FEES = null;
					hidINSTALLMENT_FEES = (HtmlInputHidden)e.Item.FindControl("hidINSTALLMENT_FEES");
					if(hidINSTALLMENT_FEES.Value != "")
					{
						insFee = Double.Parse(hidINSTALLMENT_FEES.Value.ToString());
						retVal = objFeeReversal.Recharge(idenRow,insFee,txtPOLICY_ID.Text.ToString().Trim(),userId);
						if(retVal > 0)
						{
							lblMessage.Text= "Fee Recharged Successfully";
							lblMessage.Visible = true;
							BindGrid(txtPOLICY_ID.Text.ToString(),cmbFEETYPE.SelectedValue.ToString(),txtFromDate.Text.ToString(),txtToDate.Text.ToString());			
						}
					}

				}
			}
			catch(Exception ex)
			{
				lblMessage.Text= ex.Message.ToString();
				lblMessage.Visible = true;
			}
            		
		}
		
		
		
	}
}
