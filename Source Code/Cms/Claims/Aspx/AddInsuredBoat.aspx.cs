/******************************************************************************************
	<Author					: - > Sumit Chhabra
	<Start Date				: -	> April 20,2006
	<End Date				: - >
	<Description			: - > Page is used to assign limits to authority
	<Review Date			: - >
	<Reviewed By			: - >
	
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
using Cms.Model.Claims;
using Cms.CmsWeb.Controls; 
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BLClaims;
using Cms.ExceptionPublisher.ExceptionManagement;


namespace Cms.Claims.Aspx
{
	/// <summary>
	/// 
	/// </summary>
	public class AddInsuredBoat : Cms.Claims.ClaimBase
	{
		#region Page controls declaration
		
		
		#endregion
		#region Local form variables
		//START:*********** Local form variables *************
		//string oldXML;		
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capSTATE;
		protected System.Web.UI.WebControls.Label capIncludeTrailer;
		protected System.Web.UI.WebControls.DropDownList cmbSTATE;
		protected System.Web.UI.WebControls.DropDownList cmbIncludeTrailer;
		protected System.Web.UI.WebControls.Label capPOLICY_BOAT;
		protected System.Web.UI.WebControls.DropDownList cmbPOLICY_BOAT;		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvIncludeTrailer;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvHORSE_POWER;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODEL;
		protected System.Web.UI.WebControls.Label capSERIAL_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtSERIAL_NUMBER;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvSERIAL_NUMBER;
		protected System.Web.UI.WebControls.Label capYEAR;
		protected System.Web.UI.WebControls.TextBox txtYEAR;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvYEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.Label capBODY_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbBODY_TYPE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvBODY_TYPE;		
		protected System.Web.UI.WebControls.Label capLENGTH;
		protected System.Web.UI.WebControls.TextBox txtLENGTH;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvLENGTH;
		protected System.Web.UI.WebControls.Label capWEIGHT;
		protected System.Web.UI.WebControls.TextBox txtWEIGHT;
		protected System.Web.UI.WebControls.Label capHORSE_POWER;
		protected System.Web.UI.WebControls.TextBox txtHORSE_POWER;		
		protected System.Web.UI.WebControls.Label capOTHER_HULL_TYPE;
		protected System.Web.UI.WebControls.TextBox txtOTHER_HULL_TYPE;		
		protected System.Web.UI.WebControls.Label capPLATE_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtPLATE_NUMBER;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvPLATE_NUMBER;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidLOB_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_BOAT_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCLAIM_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBOAT_ID;
		private string strRowId;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvWEIGHT;
		//protected System.Web.UI.WebControls.RequiredFieldValidator rfvOTHER_HULL_TYPE;
		protected System.Web.UI.WebControls.RangeValidator rngYEAR;
		protected System.Web.UI.WebControls.RegularExpressionValidator revHORSE_POWER;
		protected System.Web.UI.WebControls.RangeValidator rngWEIGHT;
		protected System.Web.UI.WebControls.RangeValidator rngHORSE_POWER;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPOLICY_BOAT;
		protected System.Web.UI.WebControls.Label capWHERE_BOAT_SEEN;
		protected System.Web.UI.WebControls.TextBox txtWHERE_BOAT_SEEN;
		//Added for Itrack Issue 5833 on 20 July 2009
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		//Added till here
		private bool LOAD_OLD_DATA=true;
		

		#endregion
	

		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{						
			base.ScreenId="306_4_0";
			
			lblMessage.Visible = false;
			

			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			//Added for Itrack Issue 5833 on 20 July 2009
			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	CmsButtonType.Execute;
			btnDelete.PermissionString=	gstrSecurityXML;
			//Added till here
			
			//END:*********** Setting permissions and class (Read/write/execute/delete)  *************
			objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddInsuredBoat" ,System.Reflection.Assembly.GetExecutingAssembly());
			

			if(!Page.IsPostBack)
			{			
				GetQueryStringValues();
				
				GetOldDataXML(LOAD_OLD_DATA);
				
				LoadDropDowns();

				//Added for Itrack Issue 5833 on 20 July 2009
				if(hidOldData.Value!="")
				{
					btnActivateDeactivate.Enabled = true;
					btnReset.Enabled = false;
				}
				else
				{
					btnActivateDeactivate.Enabled = false;
					btnReset.Enabled = true;
				}

				ClsInsuredBoat objInsuredBoat = new ClsInsuredBoat();
				if(hidBOAT_ID.Value.ToUpper()!= "0")
				{
					int intReserve = objInsuredBoat.CheckClaimActivityReserve(int.Parse(GetClaimID()),int.Parse(hidBOAT_ID.Value),"WATERCRAFT");
					if(intReserve == 1)
					{
						btnDelete.Visible=false;
					}
					else
					{
						btnDelete.Visible= true;
					}
				}
				else
				{
					btnDelete.Visible= true;
					btnDelete.Enabled = false;
				}
				//Added till here
				//cmbPOLICY_BOAT.Attributes.Add("onChange","javascript: return FetchPolicyBoat();");
				btnReset.Attributes.Add("onclick","javascript:return ResetTheForm();");
				SetCaptions();
				SetErrorMessages();								
			}
		}
		#endregion

		#region GetOldDataXML
		private void GetOldDataXML(bool flag)
		{
			DataTable dtOldData;
			if(hidBOAT_ID.Value!="" && hidBOAT_ID.Value!="0")
			{
				dtOldData	=	ClsInsuredBoat.GetOldDataForPageControls(hidCLAIM_ID.Value,hidBOAT_ID.Value);
				if(dtOldData!=null && dtOldData.Rows.Count>0)
				{
					hidOldData.Value = Cms.BusinessLayer.BlCommon.ClsCommon.GetXMLEncoded(dtOldData);
					trPOLICY_BOAT.Attributes.Add("style","display:none");
					if(LOAD_OLD_DATA)
						LoadData(dtOldData);
				}
				else
					hidOldData.Value	=	"";
			}
			else
				hidOldData.Value	=	"";
		}
		#endregion

		private void LoadData(DataTable dtOldData)
		{
			if(dtOldData!=null && dtOldData.Rows.Count>0)
			{
				if(dtOldData.Rows[0]["STATE"]!=null && dtOldData.Rows[0]["STATE"].ToString()!="" && dtOldData.Rows[0]["STATE"].ToString()!="0")
					cmbSTATE.SelectedValue		=		dtOldData.Rows[0]["STATE"].ToString();
				cmbIncludeTrailer.SelectedValue	=		dtOldData.Rows[0]["INCLUDE_TRAILER"].ToString();
				txtSERIAL_NUMBER.Text			=		dtOldData.Rows[0]["SERIAL_NUMBER"].ToString();	
				txtYEAR.Text					=		dtOldData.Rows[0]["YEAR"].ToString();
				txtMAKE.Text					=		dtOldData.Rows[0]["MAKE"].ToString();
				txtMODEL.Text					=		dtOldData.Rows[0]["MODEL"].ToString();				
				if(dtOldData.Rows[0]["BODY_TYPE"]!=null && dtOldData.Rows[0]["BODY_TYPE"].ToString()!="" && dtOldData.Rows[0]["BODY_TYPE"].ToString()!="0")
					cmbBODY_TYPE.SelectedValue		=		dtOldData.Rows[0]["BODY_TYPE"].ToString();				
				txtLENGTH.Text					=		dtOldData.Rows[0]["LENGTH"].ToString();
				txtWEIGHT.Text					=		dtOldData.Rows[0]["WEIGHT"].ToString();
				txtHORSE_POWER.Text				=		dtOldData.Rows[0]["HORSE_POWER"].ToString();
				txtOTHER_HULL_TYPE.Text			=		dtOldData.Rows[0]["OTHER_HULL_TYPE"].ToString();
				txtPLATE_NUMBER.Text			=		dtOldData.Rows[0]["PLATE_NUMBER"].ToString();
				if(dtOldData.Rows[0]["POLICY_BOAT_ID"]!=null && dtOldData.Rows[0]["POLICY_BOAT_ID"].ToString()!="" && dtOldData.Rows[0]["POLICY_BOAT_ID"].ToString()!="0")
					hidPOLICY_BOAT_ID.Value = dtOldData.Rows[0]["POLICY_BOAT_ID"].ToString();
				txtWHERE_BOAT_SEEN.Text			=		dtOldData.Rows[0]["WHERE_BOAT_SEEN"].ToString();
			}

		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{			
			this.cmbPOLICY_BOAT.SelectedIndexChanged += new System.EventHandler(this.cmbPOLICY_BOAT_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);//Added for Itrack Issue 5833 on 21 July 2009
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);//Done for Itrack Issue 5833 on 21 July 2009
			this.Load += new System.EventHandler(this.Page_Load);

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

			//rfvSERIAL_NUMBER.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"1");
			//rfvYEAR.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"2");
			//rfvMAKE.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"3");
			//rfvMODEL.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"4");
			//rfvBODY_TYPE.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"5");											
			
			//rfvLENGTH.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"6");
			//rfvWEIGHT.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"7");
			//rfvHORSE_POWER.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"8");			
			//rfvOTHER_HULL_TYPE.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"9");
			//rfvPLATE_NUMBER.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"10");
			//rfvSTATE.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"11");
			rfvIncludeTrailer.ErrorMessage			=		  Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"12");
			rngHORSE_POWER.ErrorMessage				=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");			
			rngYEAR.MaximumValue					=		 (DateTime.Now.Year+1).ToString();
			rngYEAR.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("673");
			rngWEIGHT.ErrorMessage					=		  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("216");
			revHORSE_POWER.ValidationExpression = aRegExpDoublePositiveNonZero;
			//revHORSE_POWER.ValidationExpression = aRegExpInteger;
			//revHORSE_POWER.ValidationExpression = aRegExpDoublePositiveWithZeroNonDollar;//Done for Itrack Issue 7619(Note 1) on 21 July
			revHORSE_POWER.ErrorMessage	  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1046");
			
			
		}

		#endregion

		private void GetQueryStringValues()
		{
			if(Request.QueryString["CLAIM_ID"]!=null && Request.QueryString["CLAIM_ID"].ToString()!="")
				hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();
			if(Request.QueryString["BOAT_ID"]!=null && Request.QueryString["BOAT_ID"].ToString()!="")
				hidBOAT_ID.Value = Request.QueryString["BOAT_ID"].ToString();
			if(Request.QueryString["LOB_ID"]!=null && Request.QueryString["LOB_ID"].ToString()!="")
				hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();			
		}

		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				//For retreiving the return value of business class save function
				ClsInsuredBoat objInsuredBoat = new ClsInsuredBoat();				

				//Retreiving the form values into model class object
				ClsInsuredBoatInfo objInsuredBoatInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objInsuredBoatInfo.CREATED_BY = int.Parse(GetUserId());
					objInsuredBoatInfo.CREATED_DATETIME = DateTime.Now;
					objInsuredBoatInfo.IS_ACTIVE="Y"; 
					
					//Calling the add method of business layer class
					intRetVal = objInsuredBoat.Add(objInsuredBoatInfo);

					if(intRetVal>0)
					{
						hidBOAT_ID.Value = objInsuredBoatInfo.BOAT_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML(!LOAD_OLD_DATA);
					}
					else if(intRetVal == -1) //Duplicate Authority Limit
					{
						lblMessage.Text				=		ClsMessages.GetMessage(base.ScreenId,"18");
						hidFormSaved.Value			=		"2";
					}
					else
					{
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"20");
						hidFormSaved.Value			=	"2";
					}					
				} // end save case
				else //UPDATE CASE
				{
					//Creating the Model object for holding the Old data
					ClsInsuredBoatInfo objOldInsuredBoatInfo = new ClsInsuredBoatInfo();

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldInsuredBoatInfo,hidOldData.Value);

					//Setting those values into the Model object which are not in the page					
					objInsuredBoatInfo.MODIFIED_BY = int.Parse(GetUserId());
					objInsuredBoatInfo.LAST_UPDATED_DATETIME = DateTime.Now;                    

					//Updating the record using business layer class object
					intRetVal	= objInsuredBoat.Update(objOldInsuredBoatInfo,objInsuredBoatInfo);					
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						GetOldDataXML(!LOAD_OLD_DATA);
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"2";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}					
				}
				lblMessage.Visible = true;
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
			    
			}
			finally
			{
				//				if(objAgency!= null)
				//					objAgency.Dispose();
			}
		}

		//Added for Itrack Issue 5833 on 20 July 2009
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			ClsInsuredBoat objInsuredBoat = new ClsInsuredBoat();
			//Done for Itrack Issue 6932 on 3 June 2010
			ClsInsuredBoatInfo objInsuredBoatInfo = GetFormValue();
			objInsuredBoatInfo.CREATED_BY = int.Parse(GetUserId());
			try
			{
				int intRetVal;	//For retreiving the return value of business class save function
				
				intRetVal = objInsuredBoat.DeleteBoat(objInsuredBoatInfo,"WATERCRAFT");//Done for Itrack Issue 6932 on 3 June 2010
				//Retreiving the form values into model class object
				if(intRetVal>0)
				{
					hidFormSaved.Value		=	"1";
					lblDelete.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
					trBody.Attributes.Add("style","display:none");
				}
				else if(intRetVal == 0)
				{
					lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
				}
				lblDelete.Visible = true;
				
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objInsuredBoat!= null)
					objInsuredBoat.Dispose();
			}
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			ClsInsuredBoat objInsuredBoat = new ClsInsuredBoat();
			//Done for Itrack Issue 6932 on 3 June 2010
			ClsInsuredBoatInfo objInsuredBoatInfo = GetFormValue();
			objInsuredBoatInfo.CREATED_BY = int.Parse(GetUserId());

			try
			{
				if(Cms.BusinessLayer.BlCommon.ClsCommon.FetchValueFromXML("IS_ACTIVE",hidOldData.Value) == "Y")
				{
					objInsuredBoat.ActivateDeactivateBoat(objInsuredBoatInfo,"N","WATERCRAFT");//Done for Itrack Issue 6932 on 3 June 2010
					lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"41");
					lblMessage.Visible=true;
					btnActivateDeactivate.Text = "Activate";
				}
				else
				{
					objInsuredBoat.ActivateDeactivateBoat(objInsuredBoatInfo,"Y","WATERCRAFT");//Done for Itrack Issue 6932 on 3 June 2010
					lblMessage.Text		= ClsMessages.GetMessage(base.ScreenId,"40");
					lblMessage.Visible=true;
					btnActivateDeactivate.Text = "Deactivate";
				}
				GetOldDataXML(!LOAD_OLD_DATA);
                ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>RefreshWebGrid(1," + hidBOAT_ID.Value + ");</script>");
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objInsuredBoat!= null)
					objInsuredBoat.Dispose();
			}
		}
		//Added till here
		private void SetCaptions()
		{
			capSTATE.Text				=		objResourceMgr.GetString("cmbSTATE");
			capIncludeTrailer.Text		=		objResourceMgr.GetString("cmbINCLUDE_TRAILER");//Done for Itrack Issue 6932 on 3 June 2010
			capSERIAL_NUMBER.Text		=		objResourceMgr.GetString("txtSERIAL_NUMBER");
			capYEAR.Text				=		objResourceMgr.GetString("txtYEAR");
			capMAKE.Text				=		objResourceMgr.GetString("txtMAKE");
			capMODEL.Text				=		objResourceMgr.GetString("txtMODEL");			
			capBODY_TYPE.Text			=		objResourceMgr.GetString("cmbBODY_TYPE");
			capLENGTH.Text				=		objResourceMgr.GetString("txtLENGTH");
			capWEIGHT.Text				=		objResourceMgr.GetString("txtWEIGHT");
			capHORSE_POWER.Text			=		objResourceMgr.GetString("txtHORSE_POWER");
			capOTHER_HULL_TYPE.Text		=		objResourceMgr.GetString("txtOTHER_HULL_TYPE");
			capPLATE_NUMBER.Text		=		objResourceMgr.GetString("txtPLATE_NUMBER");
			capPOLICY_BOAT.Text			=		objResourceMgr.GetString("cmbPOLICY_BOAT");
			capWHERE_BOAT_SEEN.Text	=		objResourceMgr.GetString("txtWHERE_BOAT_SEEN");			
		}
	

		#region GetFormValue
		private ClsInsuredBoatInfo GetFormValue()
		{
			ClsInsuredBoatInfo objInsuredBoatInfo		=		 new ClsInsuredBoatInfo();
			objInsuredBoatInfo.SERIAL_NUMBER			=		 txtSERIAL_NUMBER.Text.Trim();
			if(txtYEAR.Text.Trim()!="")
				objInsuredBoatInfo.YEAR					=		 int.Parse(txtYEAR.Text.Trim());
			objInsuredBoatInfo.MAKE						=	     txtMAKE.Text.Trim();
			objInsuredBoatInfo.MODEL					=		 txtMODEL.Text.Trim();
			if(cmbBODY_TYPE.SelectedItem!=null && cmbBODY_TYPE.SelectedItem.Value!="")
				objInsuredBoatInfo.BODY_TYPE			=		 int.Parse(cmbBODY_TYPE.SelectedItem.Value);
			objInsuredBoatInfo.LENGTH					=		 txtLENGTH.Text.Trim();
			objInsuredBoatInfo.WEIGHT					=		 txtWEIGHT.Text.Trim();
			if(txtHORSE_POWER.Text.Trim()!="")
				objInsuredBoatInfo.HORSE_POWER			=		 Double.Parse(txtHORSE_POWER.Text.Trim());
			objInsuredBoatInfo.OTHER_HULL_TYPE			=		 txtOTHER_HULL_TYPE.Text.Trim();
			objInsuredBoatInfo.PLATE_NUMBER				=		 txtPLATE_NUMBER.Text.Trim();
			if(cmbSTATE.SelectedItem!=null && cmbSTATE.SelectedItem.Value!="")
				objInsuredBoatInfo.STATE	=	int.Parse(cmbSTATE.SelectedItem.Value);
			objInsuredBoatInfo.INCLUDE_TRAILER			=		int.Parse(cmbIncludeTrailer.SelectedItem.Value);
			objInsuredBoatInfo.CLAIM_ID					=		int.Parse(hidCLAIM_ID.Value);
			//if(hidPOLICY_BOAT_ID.Value!="" && hidPOLICY_BOAT_ID.Value!="0")
			if(cmbPOLICY_BOAT.SelectedItem!=null && cmbPOLICY_BOAT.SelectedItem.Value!="")
				objInsuredBoatInfo.POLICY_BOAT_ID	= int.Parse(cmbPOLICY_BOAT.SelectedItem.Value);
			objInsuredBoatInfo.WHERE_BOAT_SEEN		= txtWHERE_BOAT_SEEN.Text.Trim();
			
			if(hidBOAT_ID.Value.ToUpper()=="NEW" || hidBOAT_ID.Value=="0")
				strRowId="NEW";
			else
			{
				strRowId=hidBOAT_ID.Value;
				objInsuredBoatInfo.BOAT_ID		=	int.Parse(hidBOAT_ID.Value);
			}
			return objInsuredBoatInfo;
		}
		#endregion

		#region LoadDropDowns
		private void LoadDropDowns()
		{
			cmbSTATE.DataSource				= Cms.CmsWeb.ClsFetcher.State;
			cmbSTATE.DataTextField			= "State_Name";
			cmbSTATE.DataValueField			= "State_Id";
			cmbSTATE.DataBind();
			cmbSTATE.Items.Insert(0,"");

			cmbBODY_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("%HULL");
			cmbBODY_TYPE.DataTextField	= "LookupDesc";
			cmbBODY_TYPE.DataValueField	= "LookupID";
			cmbBODY_TYPE.DataBind();
			cmbBODY_TYPE.Items.Insert(0,"");

			cmbIncludeTrailer.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
			cmbIncludeTrailer.DataTextField="LookupDesc";
			cmbIncludeTrailer.DataValueField="LookupID";
			cmbIncludeTrailer.DataBind();
			cmbIncludeTrailer.Items.Insert(0,"");
			//cmbIncludeTrailer.SelectedIndex=1;

			if(hidOldData.Value!="" && hidOldData.Value!="0") 
			{
				return;
			}

			DataTable dtTemp = ClsInsuredBoat.GetPolicyBoats(hidCLAIM_ID.Value,"0");
			if(dtTemp!=null && dtTemp.Rows.Count>0)
			{
				string sVal="",sText="";
				cmbPOLICY_BOAT.Items.Insert(0,"");
				for(int i=0;i<dtTemp.Rows.Count;i++)
				{
					sText = dtTemp.Rows[i]["SERIAL_NUMBER"] + "-" + dtTemp.Rows[i]["YEAR"] + "-" + dtTemp.Rows[i]["MAKE"] + "-" + dtTemp.Rows[i]["MODEL"];
					sVal = dtTemp.Rows[i]["BOAT_ID"].ToString();
					ListItem lItem = new ListItem(sText,sVal);				
					cmbPOLICY_BOAT.Items.Add(lItem);
				}
			}
			else
				trPOLICY_BOAT.Attributes.Add("style","display:none");
			/*cmbPOLICY_BOAT.Items.Add("");
			
			foreach(DataRow dtRow in dtTemp.Rows)
			{
				string sVal = "";
				for(int i=0;i<dtTemp.Columns.Count;i++)
					sVal+= dtRow[i].ToString() + "^";

				sVal=sVal.Substring(0,sVal.Length-1).Trim();

				ListItem lItem = new ListItem(dtRow["BOAT_NO"].ToString(),sVal);
				
				cmbPOLICY_BOAT.Items.Add(lItem);
			}*/

					
		}
		#endregion

		private void cmbPOLICY_BOAT_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtVehicle = new DataTable();
			try
			{
				if(cmbPOLICY_BOAT.SelectedItem!=null && cmbPOLICY_BOAT.SelectedItem.Value!="")
				{
					dtVehicle = ClsInsuredBoat.GetPolicyBoats(hidCLAIM_ID.Value,cmbPOLICY_BOAT.SelectedItem.Value);
					if(dtVehicle!=null && dtVehicle.Rows.Count>0)
					{
						DataRow drTemp = dtVehicle.Rows[0];
						if(drTemp["SERIAL_NUMBER"]!=null && drTemp["SERIAL_NUMBER"].ToString()!="")
						{
							txtSERIAL_NUMBER.Text = drTemp["SERIAL_NUMBER"].ToString();
							txtSERIAL_NUMBER.Enabled = false;
						}
						if(drTemp["YEAR"]!=null && drTemp["YEAR"].ToString()!="")
						{
							txtYEAR.Text = drTemp["YEAR"].ToString();
							txtYEAR.Enabled = false;
						}
						if(drTemp["MAKE"]!=null && drTemp["MAKE"].ToString()!="")
						{
							txtMAKE.Text = drTemp["MAKE"].ToString();
							txtMAKE.Enabled = false;
						}
						if(drTemp["MODEL"]!=null && drTemp["MODEL"].ToString()!="")
						{
							txtMODEL.Text = drTemp["MODEL"].ToString();
							txtMODEL.Enabled = false;
						}
						if(drTemp["BODY_TYPE"]!=null && drTemp["BODY_TYPE"].ToString()!="" && drTemp["BODY_TYPE"].ToString()!="0")
						{
							cmbBODY_TYPE.SelectedValue = drTemp["BODY_TYPE"].ToString();
							cmbBODY_TYPE.Enabled = false;
						}
						if(drTemp["LENGTH"]!=null && drTemp["LENGTH"].ToString()!="")
						{
							txtLENGTH.Text = drTemp["LENGTH"].ToString();
							txtLENGTH.Enabled = false;
						}
						if(drTemp["WATERCRAFT_HORSE_POWER"]!=null && drTemp["WATERCRAFT_HORSE_POWER"].ToString()!="")
						{
							txtHORSE_POWER.Text = drTemp["WATERCRAFT_HORSE_POWER"].ToString();
							txtHORSE_POWER.Enabled = false;
						}
						if(drTemp["STATE"]!=null && drTemp["STATE"].ToString()!="" && drTemp["STATE"].ToString()!="0")
						{
							cmbSTATE.SelectedValue = drTemp["STATE"].ToString();
							cmbSTATE.Enabled = false;
						}						
						

						/*txtSERIAL_NUMBER.Enabled = false;
						txtYEAR.Enabled = false;
						txtMAKE.Enabled = false;
						txtMODEL.Enabled = false;
						cmbBODY_TYPE.Enabled = false;
						txtLENGTH.Enabled = false;
						//txtWEIGHT.Enabled = false;
						txtHORSE_POWER.Enabled = false;
						//txtOTHER_HULL_TYPE.Enabled = false;
						//txtPLATE_NUMBER.Enabled = false;
						cmbSTATE.Enabled = false;*/
					}					
				}
				else
				{
					txtSERIAL_NUMBER.Enabled = true;
					txtYEAR.Enabled = true;
					txtMAKE.Enabled = true;
					txtMODEL.Enabled = true;
					cmbBODY_TYPE.Enabled = true;
					txtLENGTH.Enabled = true;
					//txtWEIGHT.Enabled = true;
					txtHORSE_POWER.Enabled = true;
					//txtOTHER_HULL_TYPE.Enabled = true;
					//txtPLATE_NUMBER.Enabled = true;
					cmbSTATE.Enabled = true;
				}
			}
			catch(Exception ex)
			{
				lblMessage.Text = ex.Message;
				lblMessage.Visible = true;
			}
			finally
			{
				if(dtVehicle!=null)
					dtVehicle.Dispose();				 
			}
		}
	}
}
