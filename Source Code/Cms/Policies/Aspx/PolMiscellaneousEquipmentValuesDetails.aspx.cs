/******************************************************************************************
<Author				: -   Sumit Chhabra
<Start Date			: -	  08/22/2006 9:54:31 AM
<End Date				: -	
<Description				: - 	desc
<Review Date				: - 
<Reviewed By			: - 	
Modification History
<Modified Date			: - 
<Modified By				: - 
<Purpose				: - desc
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
using Cms.BusinessLayer.BlApplication;
using Cms.CmsWeb;
using Cms.ExceptionPublisher.ExceptionManagement;
using Cms.Model.Policy;
using Cms.CmsWeb.Controls;

namespace Cms.Policies.Aspx
{
	/// <summary>
	/// sdlfksdhjlkhf
	/// </summary>
	public class PolMiscellaneousEquipmentValuesDetails : Cms.Policies.policiesbase
	{
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		//private string strRowId, strFormSaved;
		
		

		#endregion
		
		#region Page controls declaration
		public const string ITEM_VALUE_TEXT = "ITEM_VALUE_";
		public const int TOTAL_ROWS = 10;
		public const string ITEM_ID_TEXT = "ITEM_ID_";
		public const string ITEM_DESCRIPTION_TEXT = "ITEM_DESCRIPTION_";		
		public string txtITEM_VALUE_ID,revITEM_VALUE_ID,capITEM_ID_TEXT,txtITEM_DESC_TEXT,txtITEM_DESCRIPTION_ID,chkITEM_ID_TEXT;//Added chkITEM_ID_TEXT by Sibin for Itrack Issue 4757 on 30 Oct 08
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capITEM_ID;
		protected System.Web.UI.WebControls.Label capITEM_DESCRIPTION;
		protected System.Web.UI.WebControls.Label capITEM_VALUE;
		protected System.Web.UI.WebControls.Label capITEM_ID_1;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_1;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_1;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_1;
		protected System.Web.UI.WebControls.Label capITEM_ID_2;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_2;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_2;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_2;
		protected System.Web.UI.WebControls.Label capITEM_ID_3;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_3;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_3;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_3;
		protected System.Web.UI.WebControls.Label capITEM_ID_4;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_4;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_4;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_4;
		protected System.Web.UI.WebControls.Label capITEM_ID_5;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_5;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_5;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_5;
		protected System.Web.UI.WebControls.Label capITEM_ID_6;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_6;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_6;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_6;
		protected System.Web.UI.WebControls.Label capITEM_ID_7;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_7;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_7;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_7;
		protected System.Web.UI.WebControls.Label capITEM_ID_8;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_8;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_8;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_8;
		protected System.Web.UI.WebControls.Label capITEM_ID_9;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_9;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_9;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_9;
		protected System.Web.UI.WebControls.Label capITEM_ID_10;
		protected System.Web.UI.WebControls.TextBox txtITEM_DESCRIPTION_10;
		protected System.Web.UI.WebControls.TextBox txtITEM_VALUE_10;
		protected System.Web.UI.WebControls.RegularExpressionValidator revITEM_VALUE_10;
		protected System.Web.UI.WebControls.Label capTOTAL_VALUE;
		protected System.Web.UI.WebControls.TextBox txtTOTAL_VALUE;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;  //Added by Sibin for Itrack Issue 4757 on 31 Oct 08
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		public bool LOAD_OLD_DATA_FLAG = true;

		//Added by Sibin for Itrack Issue 4757 on 31 Oct 08
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_1;
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_2;
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_3;
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_4;
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_5;
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_6;
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_7;
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_8;
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_9;
		protected System.Web.UI.WebControls.CheckBox chkITEM_ID_10;

		//Added by Sibin for Itrack Issue 5114 on 11 Dec 08
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_4;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_5;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_6;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_7;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_8;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_9;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidITEM_ID_10;
		#endregion

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{		
			
		}
		#endregion

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			base.ScreenId = "227_4";
			lblMessage.Visible = false;			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			
			btnSave.Attributes.Add("onclick","javascript: return FormatAll();");
			btnSave.CmsButtonClass			=	CmsButtonType.Write ;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnReset.CmsButtonClass			=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			//Added by Sibin for Itrack Issue 4757 on 31 Oct 08

			btnDelete.CmsButtonClass	=	CmsButtonType.Write;
			btnDelete.PermissionString		=	gstrSecurityXML;

			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.PolMiscellaneousEquipmentValuesDetails" ,System.Reflection.Assembly.GetExecutingAssembly());

			if(!Page.IsPostBack)
			{
				GetQueryStringValues();
				//Set formatCurrency function at value textbox
				for(int i=1;i<=TOTAL_ROWS;i++)
				{
					txtITEM_DESCRIPTION_ID = "txt" + ITEM_DESCRIPTION_TEXT + i.ToString();
					TextBox txtDescription = (TextBox)(Page.FindControl(txtITEM_DESCRIPTION_ID));
					txtITEM_VALUE_ID = "txt" + ITEM_VALUE_TEXT + i.ToString();
					TextBox txtValueID =  (TextBox)(Page.FindControl(txtITEM_VALUE_ID));
					capITEM_ID_TEXT = "cap" + ITEM_ID_TEXT + i.ToString();
					Label capItemID =  (Label)(Page.FindControl(capITEM_ID_TEXT));
					revITEM_VALUE_ID = "rev" + ITEM_VALUE_TEXT + i.ToString();
					RegularExpressionValidator revID = (RegularExpressionValidator)(Page.FindControl(revITEM_VALUE_ID));
					
					if(txtDescription!=null)
						txtDescription.Attributes.Add("onBlur","return CheckDescription(" + i.ToString() + ");");

					if(txtValueID!=null)					
						txtValueID.Attributes.Add("onBlur","this.value = formatCurrency(this.value);CheckDescription(" + i.ToString() + ");");						
					
					if(revID!=null)		
					{
						/*Modified by Asfa(11-July-2008) - iTrack #4443
						//revID.ValidationExpression	= aRegExpDoublePositiveNonZeroStartWithZero;						
						*/
						revID.ValidationExpression	= aRegExpDoublePositiveNonZero ;//aRegExpCurrencyformat;						
						revID.ErrorMessage			= Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
					}
					
					if(capItemID!=null)
						capItemID.Text = i.ToString();
					
				}
				txtTOTAL_VALUE.Attributes.Add("onBlur","this.value=formatCurrency(this.value);");				
				btnReset.Attributes.Add("onClick","javascript:return ResetPage();");
				btnSave.Attributes.Add("onClick","javascript:return CheckAllDescription();");
				SetErrorMessages();
				SetCaptions();
				GetOldData(LOAD_OLD_DATA_FLAG);
			}
		
			SetWorkFlow();
		}//end pageload
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click); //Added by Sibin for Itrack Issue 4757 on 31 Oct 08
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Utility Functions
		private void PopulateArrayList(ArrayList aList)
		{
			for(int i=1;i<=TOTAL_ROWS;i++)
			{
				Cms.Model.Policy.clsPolMiscEqptValuesInfo objPolMiscEqptValuesInfo = new Cms.Model.Policy.clsPolMiscEqptValuesInfo();

				objPolMiscEqptValuesInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
				objPolMiscEqptValuesInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
				objPolMiscEqptValuesInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
				objPolMiscEqptValuesInfo.VEHICLE_ID = int.Parse(hidVEHICLE_ID.Value);
				objPolMiscEqptValuesInfo.MODIFIED_BY = objPolMiscEqptValuesInfo.CREATED_BY = int.Parse(GetUserId());
				objPolMiscEqptValuesInfo.LAST_UPDATED_DATETIME = objPolMiscEqptValuesInfo.CREATED_DATETIME = System.DateTime.Now;

				//objPolMiscEqptValuesInfo.ITEM_ID = i;
				//Added by Sibin for Itrack Issue 5114 on 11 Dec 08
				string strHidID = "hidITEM_ID_" + i.ToString();

				HtmlInputHidden hidItemId = (HtmlInputHidden)(Page.FindControl(strHidID));
				objPolMiscEqptValuesInfo.ITEM_ID = Convert.ToInt32(hidItemId.Value);
				//Added till here

				 txtITEM_VALUE_ID = "txt" + ITEM_VALUE_TEXT + i.ToString();
				TextBox txtID =  (TextBox)(Page.FindControl(txtITEM_VALUE_ID));
				if(txtID!=null && txtID.Text.Trim()!="")
					objPolMiscEqptValuesInfo.ITEM_VALUE = Convert.ToDouble(txtID.Text.Trim());

				txtITEM_DESC_TEXT = "txt" + ITEM_DESCRIPTION_TEXT + i.ToString();
				TextBox txtDescID =  (TextBox)(Page.FindControl(txtITEM_DESC_TEXT));
				if(txtDescID!=null && txtDescID.Text.Trim()!="")
					objPolMiscEqptValuesInfo.ITEM_DESCRIPTION = txtDescID.Text;

				//Added by Sibin for Itrack Issue 4757 on 31 Oct 08

				chkITEM_ID_TEXT = "chk" + ITEM_ID_TEXT + i.ToString();
				CheckBox chkID =  (CheckBox)(Page.FindControl(chkITEM_ID_TEXT));

				
				if(chkID.Checked && txtDescID.Text!="")
				{
					if(txtID!=null && txtID.Text.Trim()!="")
					{
						objPolMiscEqptValuesInfo.ITEM_VALUE = Convert.ToDouble(txtID.Text.Trim());
						aList.Add(objPolMiscEqptValuesInfo);	
					}
				}
			}
			
		}

		private void SetCaptions()
		{
			capITEM_DESCRIPTION.Text			=		objResourceMgr.GetString("lblITEM_DESCRIPTION");
			capITEM_ID.Text						=		objResourceMgr.GetString("lblITEM_ID");
			capITEM_VALUE.Text					=		objResourceMgr.GetString("lblITEM_VALUE");
			capTOTAL_VALUE.Text					=		objResourceMgr.GetString("txtTOTAL_VALUE");
		}

		private void GetQueryStringValues()
		{
			if(Request["CUSTOMER_ID"]!=null && Request["CUSTOMER_ID"].ToString()!="")			
				hidCUSTOMER_ID.Value = Request["CUSTOMER_ID"].ToString();				
			else
				hidCUSTOMER_ID.Value = "";

			if(Request["POLICY_ID"]!=null && Request["POLICY_ID"].ToString()!="")			
				hidPOLICY_ID.Value = Request["POLICY_ID"].ToString();				
			else
				hidPOLICY_ID.Value = "";

			if(Request["POLICY_VERSION_ID"]!=null && Request["POLICY_VERSION_ID"].ToString()!="")			
				hidPOLICY_VERSION_ID.Value = Request["POLICY_VERSION_ID"].ToString();				
			else
				hidPOLICY_VERSION_ID.Value = "";

			if(Request["VEHICLE_ID"]!=null && Request["VEHICLE_ID"].ToString()!="")			
				hidVEHICLE_ID.Value = Request["VEHICLE_ID"].ToString();				
			else
				hidVEHICLE_ID.Value = "";
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
			ClsVehicleInformation objVehicleInformation = new ClsVehicleInformation();

			//Retreiving the form values into model class object
			ArrayList alMiscEquip = new ArrayList();
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				

				PopulateArrayList(alMiscEquip);				
				
				if(alMiscEquip.Count > 0)
				{
					intRetVal = objVehicleInformation.SavePolMiscEquipDetails(alMiscEquip);

					if(intRetVal>0)
					{
						if(hidOldData.Value=="")
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						else
							lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"31");
						hidFormSaved.Value			=	"1";					
						hidIS_ACTIVE.Value = "Y";
						GetOldData(LOAD_OLD_DATA_FLAG);
						SetWorkFlow();
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}
				}
				else
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"1");
				}
				lblMessage.Visible = true;				
				
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"20");
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objVehicleInformation!=null)
					objVehicleInformation.Dispose();
				if(alMiscEquip!=null)
					alMiscEquip = null;
			}			
		}



		//Added by Sibin for Itrack Issue 4757 on 30 Oct 08

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			ClsVehicleInformation objVehicleInformation = new ClsVehicleInformation();

			//Retreiving the form values into model class object
			ArrayList alMiscEquip = new ArrayList();
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				
				PopulateArrayList(alMiscEquip);		
				if(alMiscEquip.Count > 0)
				{
					intRetVal = objVehicleInformation.DeletePolMiscEquipDetails(alMiscEquip);

					if(intRetVal>0)
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"127");
						hidFormSaved.Value			=	"1";					
						GetOldData(LOAD_OLD_DATA_FLAG);
						SetWorkFlow();
						base.OpenEndorsementDetails();//Added by Sibin for Itrack Issue 5271 on 9 Jan 09
					}
					else if(intRetVal == -1)
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"1");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"1");
						hidFormSaved.Value			=	"2";
					}
				}

				else
				{
					lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"1");
					hidFormSaved.Value			=	"2";
				}

				lblMessage.Visible = true;				
				
			}	
		
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"12");
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objVehicleInformation!=null)
					objVehicleInformation.Dispose();
				if(alMiscEquip!=null)
					alMiscEquip = null;
			}			
		}
		

		
		
		#endregion
		
		#region GetOldData(), LoadData(), SetWorkFlow()
		private void GetOldData(bool LOAD_OLD_DATA)
		{
			ClsVehicleInformation objVehicleInformation = new ClsVehicleInformation();
			Cms.Model.Policy.clsPolMiscEqptValuesInfo objPolMiscEqptValuesInfo = new clsPolMiscEqptValuesInfo();
			try
			{
				objPolMiscEqptValuesInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
				objPolMiscEqptValuesInfo.POLICY_ID = int.Parse(hidPOLICY_ID.Value);
				objPolMiscEqptValuesInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
				objPolMiscEqptValuesInfo.VEHICLE_ID = int.Parse(hidVEHICLE_ID.Value);
				DataTable dtOldData = objVehicleInformation.GetPolMiscEquipOldData(objPolMiscEqptValuesInfo);
				if(dtOldData!=null && dtOldData.Rows.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
					if(LOAD_OLD_DATA)
						LoadData(dtOldData);
					//Added by Sibin for Itrack Issue 4757 on 31 Oct 08
					btnDelete.Enabled=true;
				}
				else
				{
					hidOldData.Value = "";
					//Added by Sibin for Itrack Issue 4757 on 31 Oct 08
					ClearData();
					btnDelete.Enabled=false;
				}
			}
			catch(Exception ex)
			{
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objVehicleInformation!=null)
					objVehicleInformation.Dispose();
				if(objPolMiscEqptValuesInfo!=null)
					objPolMiscEqptValuesInfo = null;
			}

		}

		//Added by Sibin for Itrack Issue 4757 on 31 Oct 08
		private void ClearData()
		{
			string strValue,strDesc,strID;
			strValue = ITEM_VALUE_TEXT.Substring(0,ITEM_VALUE_TEXT.Length-1);
			strDesc = ITEM_DESCRIPTION_TEXT.Substring(0,ITEM_DESCRIPTION_TEXT.Length-1);
			strID = ITEM_ID_TEXT.Substring(0,ITEM_ID_TEXT.Length-1);

			for(int i=1;i<=TOTAL_ROWS;i++)
			{
				txtITEM_VALUE_ID = "txt" + ITEM_VALUE_TEXT + i.ToString();
				TextBox txtID =  (TextBox)(Page.FindControl(txtITEM_VALUE_ID));					
				txtID.Text = "";
	
				txtITEM_DESC_TEXT = "txt" + ITEM_DESCRIPTION_TEXT + i.ToString();
				TextBox txtDescID =  (TextBox)(Page.FindControl(txtITEM_DESC_TEXT));
				txtDescID.Text = "";
		
				chkITEM_ID_TEXT = "chk" + ITEM_ID_TEXT + i.ToString();
				CheckBox chkID =  (CheckBox)(Page.FindControl(chkITEM_ID_TEXT));
				chkID.Checked=false;
			}
		}


		private void LoadData(DataTable dtData)
		{
			string strValue,strDesc,strID;
			if(dtData!=null && dtData.Rows.Count>0)
			{
				//Obtain column names from inititals of IDs of textboxes
				strValue = ITEM_VALUE_TEXT.Substring(0,ITEM_VALUE_TEXT.Length-1);
				strDesc = ITEM_DESCRIPTION_TEXT.Substring(0,ITEM_DESCRIPTION_TEXT.Length-1);
				strID = ITEM_ID_TEXT.Substring(0,ITEM_ID_TEXT.Length-1);

				for(int i=1;i<=TOTAL_ROWS;i++)
				{
					txtITEM_VALUE_ID = "txt" + ITEM_VALUE_TEXT + i.ToString();
					TextBox txtID =  (TextBox)(Page.FindControl(txtITEM_VALUE_ID));										
					if(i <= dtData.Rows.Count && dtData.Rows[i-1][strValue]!=null && dtData.Rows[i-1][strValue].ToString()!="" && dtData.Rows[i-1][strValue].ToString()!="0")
						txtID.Text = dtData.Rows[i-1][strValue].ToString();
					else
						txtID.Text = "";


					//Added by Sibin for Itrack Issue 5114 on 11 Dec 08
					string strHidID = "hidITEM_ID_" + i.ToString();

					HtmlInputHidden hidItemId = (HtmlInputHidden)(Page.FindControl(strHidID));

					if(i <= dtData.Rows.Count && dtData.Rows[i-1]["ITEM_ID"]!=null && dtData.Rows[i-1]["ITEM_ID"].ToString()!="" && dtData.Rows[i-1]["ITEM_ID"].ToString()!="0")
						hidItemId.Value  = dtData.Rows[i-1]["ITEM_ID"].ToString();
					else
						hidItemId.Value  = "0" ;

					//Added till here
					
					txtITEM_DESC_TEXT = "txt" + ITEM_DESCRIPTION_TEXT + i.ToString();
					TextBox txtDescID =  (TextBox)(Page.FindControl(txtITEM_DESC_TEXT));
					if(i <= dtData.Rows.Count && dtData.Rows[i-1][strDesc]!=null && dtData.Rows[i-1][strDesc].ToString()!="")
						txtDescID.Text = dtData.Rows[i-1][strDesc].ToString();
					else
						txtDescID.Text = "";

					chkITEM_ID_TEXT = "chk" + ITEM_ID_TEXT + i.ToString();
					CheckBox chkID =  (CheckBox)(Page.FindControl(chkITEM_ID_TEXT));

					//if(chkID.Checked && txtDescID.Text!="")
					//	{
					if(txtID!=null && txtID.Text.Trim()!="")
					{
						if(i <= dtData.Rows.Count && dtData.Rows[i-1][strID]!=null && dtData.Rows[i-1][strID].ToString()!="")
							chkID.Checked=true;
						else
							chkID.Checked=false;
					}
					else
						chkID.Checked=false;
				}

			}
		}

        private void SetWorkFlow()
		{
			if(base.ScreenId == "227_4")
			{ 
				//CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, VEHICLE_ID, ITEM_ID
				myWorkFlow.IsTop = false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",GetCustomerID());
				myWorkFlow.AddKeyValue("POLICY_ID",GetPolicyID());
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",GetPolicyVersionID());
				if(hidVEHICLE_ID.Value!="" && hidVEHICLE_ID.Value!="0" && hidVEHICLE_ID.Value.ToUpper()!="NEW")
					myWorkFlow.AddKeyValue("VEHICLE_ID",hidVEHICLE_ID.Value);
				myWorkFlow.WorkflowModule="POL";
				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
				
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}
		#endregion
		
	}


}
