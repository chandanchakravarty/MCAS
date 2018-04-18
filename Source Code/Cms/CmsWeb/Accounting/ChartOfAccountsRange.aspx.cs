/******************************************************************************************
<Author					: -   Ajit Singh Chahal
<Start Date				: -	5/11/2005 12:45:11 PM
<End Date				: -	
<Description			: - 	Code Behind for  GLAccountRanges
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By			: - 
<Purpose				: - Code Behind for  GLAccountRanges
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
using Cms.Model.Maintenance.Accounting;
using Cms.BusinessLayer.BlCommon.Accounting;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.CmsWeb;
using Cms.CmsWeb.WebControls;
using Cms.CmsWeb.Controls;
using System.Text;





	namespace Cms.CmsWeb.Maintenance.Accounting
	{
		/// <summary>
		/// Code Behind for  GLAccountRanges
		/// </summary>
		public class ChartOfAccountsRange : Cms.CmsWeb.cmsbase
		{
			#region Page controls declaration
			protected System.Web.UI.WebControls.Label lblMessage;
			
			protected System.Web.UI.WebControls.Label capRANGE_FROM;
			protected System.Web.UI.WebControls.TextBox txtRANGE_FROM;
			protected System.Web.UI.WebControls.Label capRANGE_TO;
			protected System.Web.UI.WebControls.TextBox txtRANGE_TO;
			protected System.Web.UI.WebControls.Label lblCATEGORY_DESC1;
			protected System.Web.UI.WebControls.DropDownList DropDownList1;
			protected Cms.CmsWeb.Controls.CmsButton btnSave;
			protected Cms.CmsWeb.Controls.CmsButton btnDefine_Ranges;

			protected System.Web.UI.HtmlControls.HtmlInputHidden hidCATEGORY_ID;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
			protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;

		

			#endregion
			#region Local form variables
			//START:*********** Local form variables *************
			string oldXML;
			//creating resource manager object (used for reading field and label mapping)
			System.Resources.ResourceManager objResourceMgr;
			private string strRowId, strFormSaved;
			//private int	intLoggedInUserID;
			protected System.Web.UI.WebControls.Label lblCATEGORY_DESC_Asset;
			protected System.Web.UI.WebControls.Label capRANGE_FROM_Asset;
			protected System.Web.UI.WebControls.TextBox txtRANGE_FROM_Asset;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_FROM_Asset1;
			protected System.Web.UI.WebControls.Label capRANGE_TO_Asset;
			protected System.Web.UI.WebControls.TextBox txtRANGE_TO_Asset;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_TO_Asset1;
			protected System.Web.UI.WebControls.Label Label1;
			protected System.Web.UI.WebControls.Label Label4;
			protected System.Web.UI.WebControls.Label Label7;
			protected System.Web.UI.WebControls.Label Label10;
			protected System.Web.UI.WebControls.Label capRANGE_FROM_Liability;
			protected System.Web.UI.WebControls.TextBox txtRANGE_FROM_Liability;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_FROM_Liability1;
			protected System.Web.UI.WebControls.Label capRANGE_FROM_Equity;
            protected System.Web.UI.WebControls.Label capChartofaccountrange; //sneha
			protected System.Web.UI.WebControls.TextBox txtRANGE_FROM_Equity;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_FROM_Equity1;
			protected System.Web.UI.WebControls.Label capRANGE_FROM_Income;
			protected System.Web.UI.WebControls.TextBox txtRANGE_FROM_Income;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_FROM_Income1;
			protected System.Web.UI.WebControls.Label capRANGE_FROM_Expense;
			protected System.Web.UI.WebControls.TextBox txtRANGE_FROM_Expense;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_FROM_Expense1;
			protected System.Web.UI.WebControls.Label capRANGE_TO_Liability;
			protected System.Web.UI.WebControls.TextBox txtRANGE_TO_Liability;
			protected System.Web.UI.WebControls.Label capRANGE_TO_Equity;
			protected System.Web.UI.WebControls.TextBox txtRANGE_TO_Equity;
			protected System.Web.UI.WebControls.Label capRANGE_TO_Income;
			protected System.Web.UI.WebControls.TextBox txtRANGE_TO_Income;
			protected System.Web.UI.WebControls.Label capRANGE_TO_Expense;
			protected System.Web.UI.WebControls.TextBox txtRANGE_TO_Expense;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_TO_Liability1;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_TO_Equity1;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_TO_Income1;
			protected System.Web.UI.WebControls.DropDownList cmbRANGE_TO_Expense1;
			protected Cms.CmsWeb.Controls.CmsButton btnPrintRanges;
			protected System.Web.UI.HtmlControls.HtmlForm ACT_GL_ACCOUNT_RANGES;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_FROM_Asset;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_TO_Asset;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_FROM_Liability;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_TO_Liability;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_FROM_Equity;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_TO_Equity;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_FROM_Income;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_TO_Income;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_FROM_Expense;
			protected System.Web.UI.WebControls.RegularExpressionValidator revRANGE_TO_Expense;
			protected Cms.CmsWeb.Controls.CmsButton btnReset;
			//Defining the business layer class object

			ClsGLAccountRanges objGLAccountRanges ;
			
			
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
			
				revRANGE_FROM_Asset.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				revRANGE_FROM_Liability.ErrorMessage			=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				revRANGE_FROM_Equity.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				revRANGE_FROM_Income.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				revRANGE_FROM_Expense.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");

				revRANGE_TO_Asset.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				revRANGE_TO_Liability.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				revRANGE_TO_Equity.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				revRANGE_TO_Income.ErrorMessage					=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
				revRANGE_TO_Expense.ErrorMessage				=	Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");

				revRANGE_FROM_Asset.ValidationExpression		=	aRegExpAcNumber;
				revRANGE_FROM_Liability.ValidationExpression	=	aRegExpAcNumber;
				revRANGE_FROM_Equity.ValidationExpression		=	aRegExpAcNumber;
				revRANGE_FROM_Expense.ValidationExpression		=	aRegExpAcNumber;
				revRANGE_FROM_Income.ValidationExpression		=	aRegExpAcNumber;

				revRANGE_TO_Asset.ValidationExpression			=	aRegExpAcNumber;
				revRANGE_TO_Liability.ValidationExpression		=	aRegExpAcNumber;
				revRANGE_TO_Equity.ValidationExpression			=	aRegExpAcNumber;
				revRANGE_TO_Expense.ValidationExpression		=	aRegExpAcNumber;
				revRANGE_TO_Income.ValidationExpression			=	aRegExpAcNumber;
				
			}
			#endregion
			#region PageLoad event
			private void Page_Load(object sender, System.EventArgs e)
			{
				btnSave.Attributes.Add("onclick","javascript: return CheckForOverlapping();");
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('ACT_GL_ACCOUNT_RANGES' );");
                btnDefine_Ranges.Attributes.Add("onclick","javascript: return ShowDefineSubRanges();");
				btnPrintRanges.Attributes.Add("onclick","javascript: return ShowPrintRanges();");

				// phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
				base.ScreenId="124_0";
				lblMessage.Visible = false;
				SetErrorMessages();

				//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
				btnPrintRanges.CmsButtonClass		=	CmsButtonType.Execute;
				btnPrintRanges.PermissionString		=	gstrSecurityXML;

				btnDefine_Ranges.CmsButtonClass		=	CmsButtonType.Read;
				btnDefine_Ranges.PermissionString	=	gstrSecurityXML;

				
				btnReset.CmsButtonClass				=	CmsButtonType.Write;
				btnReset.PermissionString			=	gstrSecurityXML;


				btnSave.CmsButtonClass				=	CmsButtonType.Write;
				btnSave.PermissionString			=	gstrSecurityXML;

				//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
				objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.Accounting.ChartOfAccountsRange" ,System.Reflection.Assembly.GetExecutingAssembly());
				if(!Page.IsPostBack)
				{
					string item="";
					for(int i=0;i<100;i++)
					{
						if(i<=9)
							item = ".0"+i.ToString();
						else
							item = "."+i.ToString();

						cmbRANGE_FROM_Asset1.Items.Add(item);
						cmbRANGE_TO_Asset1.Items.Add(item);

						cmbRANGE_FROM_Liability1.Items.Add(item);
						cmbRANGE_TO_Liability1.Items.Add(item);

						cmbRANGE_FROM_Equity1.Items.Add(item);
						cmbRANGE_TO_Equity1.Items.Add(item);

						cmbRANGE_FROM_Income1.Items.Add(item);
						cmbRANGE_TO_Income1.Items.Add(item);

						cmbRANGE_FROM_Expense1.Items.Add(item);
						cmbRANGE_TO_Expense1.Items.Add(item);
					}
/*						Asset                 
          Liability   
          Equity      
          Income    
          Expense  */
					#region "Loading Data"
					DataTable objDataTable = ClsGLAccountRanges.GetData();
					CreateXML(objDataTable);
					
						//1:=============================================================================================================
						string[] strRangeFrom = objDataTable.Rows[0]["RANGE_FROM"].ToString().Split('.');
						string[] strRangeTo = objDataTable.Rows[0]["RANGE_TO"].ToString().Split('.');
						if(strRangeFrom[0].Length==0)
							btnDefine_Ranges.Enabled = false;
						else
							btnDefine_Ranges.Enabled = true;
						txtRANGE_FROM_Asset.Text = strRangeFrom[0].Length==0?"0":strRangeFrom[0];
						cmbRANGE_FROM_Asset1.SelectedIndex = strRangeFrom.Length==1?-1:int.Parse(strRangeFrom[1]);

						txtRANGE_TO_Asset.Text = strRangeTo[0].Length==0?"0":strRangeTo[0];
						cmbRANGE_TO_Asset1.SelectedIndex =  strRangeFrom.Length==1?-1:int.Parse(strRangeTo[1]);
						//=============================================================================================================

						//1:=============================================================================================================
						strRangeFrom = objDataTable.Rows[1]["RANGE_FROM"].ToString().Split('.');
						strRangeTo = objDataTable.Rows[1]["RANGE_TO"].ToString().Split('.');
						txtRANGE_FROM_Liability.Text = strRangeFrom[0].Length==0?"0":strRangeFrom[0];
						cmbRANGE_FROM_Liability1.SelectedIndex =  strRangeFrom.Length==1?-1:int.Parse(strRangeFrom[1]);

						txtRANGE_TO_Liability.Text = strRangeTo[0].Length==0?"0":strRangeTo[0];
						cmbRANGE_TO_Liability1.SelectedIndex =  strRangeFrom.Length==1?-1:int.Parse(strRangeTo[1]);
						//=============================================================================================================

						//2:=============================================================================================================
						strRangeFrom = objDataTable.Rows[2]["RANGE_FROM"].ToString().Split('.');
						strRangeTo = objDataTable.Rows[2]["RANGE_TO"].ToString().Split('.');
						txtRANGE_FROM_Equity.Text = strRangeFrom[0].Length==0?"0":strRangeFrom[0];
						cmbRANGE_FROM_Equity1.SelectedIndex =  strRangeFrom.Length==1?-1:int.Parse(strRangeFrom[1]);

						txtRANGE_TO_Equity.Text = strRangeTo[0].Length==0?"0":strRangeTo[0];
						cmbRANGE_TO_Equity1.SelectedIndex =  strRangeFrom.Length==1?-1:int.Parse(strRangeTo[1]);
						//=============================================================================================================

						//3:=============================================================================================================
						strRangeFrom = objDataTable.Rows[3]["RANGE_FROM"].ToString().Split('.');
						strRangeTo = objDataTable.Rows[3]["RANGE_TO"].ToString().Split('.');
						txtRANGE_FROM_Income.Text = strRangeFrom[0].Length==0?"0":strRangeFrom[0];
						cmbRANGE_FROM_Income1.SelectedIndex =  strRangeFrom.Length==1?-1:int.Parse(strRangeFrom[1]);

						txtRANGE_TO_Income.Text = strRangeTo[0].Length==0?"0":strRangeTo[0];
						cmbRANGE_TO_Income1.SelectedIndex =  strRangeFrom.Length==1?-1:int.Parse(strRangeTo[1]);
						//=============================================================================================================

						//4:=============================================================================================================
						strRangeFrom = objDataTable.Rows[4]["RANGE_FROM"].ToString().Split('.');
						strRangeTo = objDataTable.Rows[4]["RANGE_TO"].ToString().Split('.');
						txtRANGE_FROM_Expense.Text = strRangeFrom[0].Length==0?"0":strRangeFrom[0];
						cmbRANGE_FROM_Expense1.SelectedIndex =  strRangeFrom.Length==1?-1:int.Parse(strRangeFrom[1]);

						txtRANGE_TO_Expense.Text = strRangeTo[0].Length==0?"0":strRangeTo[0];
						cmbRANGE_TO_Expense1.SelectedIndex =  strRangeFrom.Length==1?-1:int.Parse(strRangeTo[1]);
						//=============================================================================================================
					
					#endregion

			
					SetCaptions();
					
				}
			}//end pageload
			private void CreateXML(DataTable objDataTable)
			{
				string[] xml = new string[5];
				string[] captions = {"_Asset","_Liability","_Equity","_Income","_Expense"};
				StringBuilder str;
				Response.Write("<script>xmlArray = new Array();</script>");
				string value1;
				for(int i=0;i<objDataTable.Rows.Count;i++)
				{
					str = new StringBuilder();
					str.Append("<NewDataSet><Table>");
					for(int j=0;j<objDataTable.Columns.Count;j++)
					{
						if(objDataTable.Columns[j].ColumnName.Equals("RANGE_FROM") || objDataTable.Columns[j].ColumnName.Equals("RANGE_TO"))
						{
							value1 = objDataTable.Rows[i][j]==DBNull.Value || objDataTable.Rows[i][j].ToString().Length<=0?"0":objDataTable.Rows[i][j].ToString().Length==1?objDataTable.Rows[i][j].ToString():objDataTable.Rows[i][j].ToString().Split('.')[0];
							str.Append("<"+objDataTable.Columns[j].ColumnName+captions[i]+">" +value1+"</"+objDataTable.Columns[j].ColumnName+captions[i]+">");
							value1 = objDataTable.Rows[i][j]==DBNull.Value || objDataTable.Rows[i][j].ToString().IndexOf('.')==-1?".00":objDataTable.Rows[i][j].ToString().Split('.')[1];
							str.Append("<"+objDataTable.Columns[j].ColumnName+captions[i]+"1>." +value1+"</"+objDataTable.Columns[j].ColumnName+captions[i]+"1>");
						}
						else
							str.Append("<"+objDataTable.Columns[j].ColumnName+captions[i]+">" +objDataTable.Rows[i][j]+"</"+objDataTable.Columns[j].ColumnName+captions[i]+">");
					}
					str.Append("</Table></NewDataSet>");
					xml[i] = str.ToString();
					Response.Write("<script>\nxmlArray["+i.ToString()+"]='"+xml[i]+"';\n</script>\n");
				}
				
			}
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
			private ClsGLAccountRangesInfo[] GetFormValue()
			{
				//Creating the Model object for holding the New data
				ClsGLAccountRangesInfo[] objGLAccountRangesInfo = new ClsGLAccountRangesInfo[5];
				
				//Setting those values into the Model object which are not in the page
				for(int i=0;i<5;i++)
				{
					objGLAccountRangesInfo[i]					     =	 new ClsGLAccountRangesInfo();
					objGLAccountRangesInfo[i].CATEGORY_ID			 =	 i+1;
					objGLAccountRangesInfo[i].MODIFIED_BY			 =   int.Parse(GetUserId());
					objGLAccountRangesInfo[i].LAST_UPDATED_DATETIME  =   DateTime.Now;
				}

				//================================================================================
				objGLAccountRangesInfo[0].RANGE_FROM	=	double.Parse(txtRANGE_FROM_Asset.Text+cmbRANGE_FROM_Asset1.Items[cmbRANGE_FROM_Asset1.SelectedIndex].Text);
				objGLAccountRangesInfo[0].RANGE_TO		=	double.Parse(txtRANGE_TO_Asset.Text+cmbRANGE_TO_Asset1.Items[cmbRANGE_TO_Asset1.SelectedIndex].Text);
				//=================================================================================

				//================================================================================
				objGLAccountRangesInfo[1].RANGE_FROM	=	double.Parse(txtRANGE_FROM_Liability.Text+cmbRANGE_FROM_Liability1.Items[cmbRANGE_FROM_Liability1.SelectedIndex].Text);
				objGLAccountRangesInfo[1].RANGE_TO		=	double.Parse(txtRANGE_TO_Liability.Text+cmbRANGE_TO_Liability1.Items[cmbRANGE_TO_Liability1.SelectedIndex].Text);
				//=================================================================================

				//================================================================================
				objGLAccountRangesInfo[2].RANGE_FROM	=	double.Parse(txtRANGE_FROM_Equity.Text+cmbRANGE_FROM_Equity1.Items[cmbRANGE_FROM_Equity1.SelectedIndex].Text);
				objGLAccountRangesInfo[2].RANGE_TO		=	double.Parse(txtRANGE_TO_Equity.Text+cmbRANGE_TO_Equity1.Items[cmbRANGE_TO_Equity1.SelectedIndex].Text);
				//=================================================================================

				//================================================================================
				objGLAccountRangesInfo[3].RANGE_FROM	=	double.Parse(txtRANGE_FROM_Income.Text+cmbRANGE_FROM_Income1.Items[cmbRANGE_FROM_Income1.SelectedIndex].Text);
				objGLAccountRangesInfo[3].RANGE_TO		=	double.Parse(txtRANGE_TO_Income.Text+cmbRANGE_TO_Income1.Items[cmbRANGE_TO_Income1.SelectedIndex].Text);
				//=================================================================================

				//================================================================================
				objGLAccountRangesInfo[4].RANGE_FROM	=	double.Parse(txtRANGE_FROM_Expense.Text+cmbRANGE_FROM_Expense1.Items[cmbRANGE_FROM_Expense1.SelectedIndex].Text);
				objGLAccountRangesInfo[4].RANGE_TO		=	double.Parse(txtRANGE_TO_Expense.Text+cmbRANGE_TO_Expense1.Items[cmbRANGE_TO_Expense1.SelectedIndex].Text);
				//=================================================================================

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
					ClsGLAccountRangesInfo[] objGLAccountRangesInfo		=	GetFormValue();
					//Creating the Model object for holding the Old data
					ClsGLAccountRangesInfo[] objOldGLAccountRangesInfo  =   GetOldData();
					
					 //UPDATE CASE
					{
						//Updating the record using business layer class object
						intRetVal	= objGLAccountRanges.Update(objOldGLAccountRangesInfo,objGLAccountRangesInfo);
						if( intRetVal > 0 )			// update successfully performed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
							hidFormSaved.Value		=	"1";
							btnDefine_Ranges.Enabled = true;
						}
						else if(intRetVal == -1)	// Duplicate code exist, update failed
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
							hidFormSaved.Value		=	"2";
						}
						else 
						{
							lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
							hidFormSaved.Value		=	"2";
						}
						lblMessage.Visible = true;
					}
					CreateXML(ClsGLAccountRanges.GetData());
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
			private ClsGLAccountRangesInfo[] GetOldData()
			{
				DataTable objDataTable = ClsGLAccountRanges.GetData();
				ClsGLAccountRangesInfo[] objGLAccountRangesInfo = new ClsGLAccountRangesInfo[5];
				for(int i=0;i<objDataTable.Rows.Count;i++)
				{
					objGLAccountRangesInfo[i] = new ClsGLAccountRangesInfo();
					
					objGLAccountRangesInfo[i].RANGE_FROM	=	objDataTable.Rows[i]["RANGE_FROM"]==DBNull.Value?0.00:double.Parse(objDataTable.Rows[i]["RANGE_FROM"].ToString());
					objGLAccountRangesInfo[i].RANGE_TO		=	objDataTable.Rows[i]["RANGE_TO"]==DBNull.Value?0.00:double.Parse(objDataTable.Rows[i]["RANGE_TO"].ToString());
				}
				return objGLAccountRangesInfo;
			}
			private void SetCaptions()
			{
				capRANGE_FROM_Asset.Text						=		objResourceMgr.GetString("capRANGE_FROM");
				capRANGE_TO_Asset.Text							=		objResourceMgr.GetString("capRANGE_TO");
				
				capRANGE_FROM_Liability.Text					=		objResourceMgr.GetString("capRANGE_FROM");
				capRANGE_TO_Liability.Text						=		objResourceMgr.GetString("capRANGE_TO");
				
				capRANGE_FROM_Equity.Text						=		objResourceMgr.GetString("capRANGE_FROM");
				capRANGE_TO_Equity.Text							=		objResourceMgr.GetString("capRANGE_TO");

				capRANGE_FROM_Income.Text						=		objResourceMgr.GetString("capRANGE_FROM");
				capRANGE_TO_Income.Text							=		objResourceMgr.GetString("capRANGE_TO");

				capRANGE_FROM_Expense.Text						=		objResourceMgr.GetString("capRANGE_FROM");
				capRANGE_TO_Expense.Text						=		objResourceMgr.GetString("capRANGE_TO");
                lblCATEGORY_DESC_Asset.Text = objResourceMgr.GetString("capCATEGORY_DESC_Asset");
                Label1.Text = objResourceMgr.GetString("capLabel1");
                Label4.Text = objResourceMgr.GetString("capLabel4");
                Label7.Text = objResourceMgr.GetString("capLabel7");
                Label10.Text = objResourceMgr.GetString("capLabel10");
                capChartofaccountrange.Text = objResourceMgr.GetString("capChartofaccountrange");
                btnPrintRanges.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");
                btnDefine_Ranges.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1336");
			}
		}
	}
