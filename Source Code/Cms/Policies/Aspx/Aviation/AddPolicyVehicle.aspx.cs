/******************************************************************************************
<Author				: -  Pravesh K Chandel
<Start Date			: -	 14 Jan 2010
<End Date			: -	 
<Description		: -  Contains details for Aviation Vehicle Information. 
<Review Date		: - 
<Reviewed By		: - 	
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
using Cms.BusinessLayer.BlCommon;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.CmsWeb.Controls; 
//using System.Xml;
using Cms.Model.Policy; 

namespace  Cms.Policies.Aspx.Aviation
{
	/// <summary>
	/// Summary description for AddPolicyVehicle.
	/// </summary>
	public class AddPolicyVehicle : Cms.Policies.policiesbase
	{
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capUSE_VEHICLE;
		protected System.Web.UI.WebControls.DropDownList cmbUSE_VEHICLE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvUSE_VEHICLE;
		protected System.Web.UI.WebControls.Label capCOVG_PERIMETER;
		protected System.Web.UI.WebControls.DropDownList cmbCOVG_PERIMETER;
		protected System.Web.UI.WebControls.Label capREG_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtREG_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREG_NUMBER;
		protected System.Web.UI.WebControls.Label capSERIAL_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtSERIAL_NUMBER;
		protected System.Web.UI.WebControls.Label capVEHICLE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RangeValidator rngVEHICLE_YEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.DropDownList cmbMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.Label capMAKE_OTHER;
		protected System.Web.UI.WebControls.TextBox txtMAKE_OTHER;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.DropDownList cmbMODEL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODEL;
		protected System.Web.UI.WebControls.Label capMODEL_OTHER;
		protected System.Web.UI.WebControls.TextBox txtMODEL_OTHER;
		protected System.Web.UI.WebControls.Label capCERTIFICATION;
		protected System.Web.UI.WebControls.TextBox txtCERTIFICATION;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvCERTIFICATION;
		protected System.Web.UI.WebControls.Label capREGISTER;
		protected System.Web.UI.WebControls.TextBox txtREGISTER;
		protected System.Web.UI.WebControls.Label capENGINE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbENGINE_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvENGINE_TYPE;
		protected System.Web.UI.WebControls.Label capWING_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbWING_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvWING_TYPE;
		protected System.Web.UI.WebControls.Label capCREW;
		protected System.Web.UI.WebControls.TextBox txtCREW;
		protected System.Web.UI.WebControls.Label capPAX;
		protected System.Web.UI.WebControls.TextBox txtPAX;
		protected System.Web.UI.WebControls.Label capREMARKS;
		protected System.Web.UI.WebControls.TextBox txtREMARKS;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVehicleID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPolVersionID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_EFFECTIVE_YEAR;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_LOB;

		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			#region setting screen id
			base.ScreenId	=	"449_0";
			#endregion
			#region setting security Xml 
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;

			btnDelete.CmsButtonClass	=	Cms.CmsWeb.Controls.CmsButtonType.Delete;
			btnDelete.PermissionString	=	gstrSecurityXML;

			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	= Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	= gstrSecurityXML;
			#endregion
			objResourceMgr = new System.Resources.ResourceManager("Cms.Application.Aspx.Aviation.AddVehicle" ,System.Reflection.Assembly.GetExecutingAssembly());
			if ( !Page.IsPostBack)
			{
				//SetCaptions();
				FillDropdown();
				GetOldDataXML();
			}
		}
		private void GetOldDataXML()
		{
			// If VEHICLE_ID is passed then it is a case of update else it is a case of add
			if (Request.QueryString["VEHICLE_ID"]!=null && Request.QueryString["VEHICLE_ID"].ToString()!="") // UPDATE MODE
			{
				hidCustomerID.Value =GetCustomerID();
				hidVehicleID.Value =Request.QueryString["VEHICLE_ID"].ToString();
				hidPOLICY_ID.Value =Request.QueryString["POLICY_ID"].ToString();
				hidPolVersionID.Value =Request.QueryString["POLICY_VERSION_ID"].ToString();
				hidOldData.Value = @ClsVehicleInformation.FetchPolAviationVehicleXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPolVersionID.Value==""?"0":hidPolVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
				btnActivateDeactivate.Attributes.Add("style","display:inline"); 
			}
			else
			{
				// IN ADD NEW CASE - we will take the customerid from the session		
				hidCustomerID.Value		= GetCustomerID();
				hidPOLICY_ID.Value			= GetPolicyID();
				hidPolVersionID.Value	= GetPolicyVersionID();;
				//txtINSURED_VEH_NUMBER.Text = "To be generated";
				hidVehicleID.Value = "NEW";
				//save the lobid
			}		
		}
		private void SetCaptions()
		{
			//capINSURED_VEH_NUMBER.Text		=		objResourceMgr.GetString("txtINSURED_VEH_NUMBER");
			capVEHICLE_YEAR.Text				=		objResourceMgr.GetString("cmbVEHICLE_YEAR");
			capMAKE.Text						=		objResourceMgr.GetString("cmbMAKE");
			capMODEL.Text						=		objResourceMgr.GetString("cmbMODEL");
			capENGINE_TYPE.Text					=		objResourceMgr.GetString("cmbENGINE_TYPE");
			capUSE_VEHICLE.Text					=		objResourceMgr.GetString("cmbUSE_VEHICLE");
			capCOVG_PERIMETER.Text				=		objResourceMgr.GetString("cmbCOVG_PERIMETER");
			capREG_NUMBER.Text					=		objResourceMgr.GetString("txtREG_NUMBER");
			capSERIAL_NUMBER.Text				=		objResourceMgr.GetString("txtSERIAL_NUMBER");
			capMAKE_OTHER.Text					=		objResourceMgr.GetString("txtMAKE_OTHER");
			capMODEL_OTHER.Text					=		objResourceMgr.GetString("txtMODEL_OTHER");
			capREGISTER.Text					=		objResourceMgr.GetString("txtREGISTER");
			capCERTIFICATION.Text				=		objResourceMgr.GetString("txtCERTIFICATION");
			capENGINE_TYPE.Text					=		objResourceMgr.GetString("cmbENGINE_TYPE");
			capWING_TYPE.Text					=		objResourceMgr.GetString("cmbWING_TYPE");
			capCREW.Text						=		objResourceMgr.GetString("txtCREW");
			capPAX.Text							=		objResourceMgr.GetString("txtPAX");
			capREMARKS.Text						=		objResourceMgr.GetString("txtREMARKS");
		}
		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objVehicleInfo;
			objVehicleInfo = new Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo();

			hidCustomerID.Value =GetCustomerID();
			hidPOLICY_ID.Value=GetPolicyID();
			hidPolVersionID.Value=GetPolicyVersionID();

			objVehicleInfo.CUSTOMER_ID = int.Parse(hidCustomerID.Value);
			objVehicleInfo.POLICY_ID =   int.Parse(hidPOLICY_ID.Value); 
			objVehicleInfo.POLICY_VERSION_ID = int.Parse(hidPolVersionID.Value);
			if (hidVehicleID.Value !=null )
			{
				if (hidVehicleID.Value.ToString()=="NEW")
				{
					objVehicleInfo.VEHICLE_ID = 0;
				}
				else
				{
					objVehicleInfo.VEHICLE_ID = int.Parse (hidVehicleID.Value.ToString());
					//objVehicleInfo.INSURED_VEH_NUMBER=int.Parse(txtINSURED_VEH_NUMBER.Text);
				}
			}

			objVehicleInfo.MODIFIED_BY=int.Parse(GetUserId());
			objVehicleInfo.INSURED_VEH_NUMBER=	int.Parse("0");
			
			if(cmbUSE_VEHICLE.SelectedValue != null  && cmbUSE_VEHICLE.SelectedValue!="")
				objVehicleInfo.USE_VEHICLE		=	int.Parse(cmbUSE_VEHICLE.SelectedValue);
			if(cmbCOVG_PERIMETER.SelectedValue != null  && cmbCOVG_PERIMETER.SelectedValue!="")
				objVehicleInfo.COVG_PERIMETER	=	int.Parse(cmbCOVG_PERIMETER.SelectedValue);
			objVehicleInfo.REG_NUMBER		=	txtREG_NUMBER.Text;
			objVehicleInfo.SERIAL_NUMBER	=	txtSERIAL_NUMBER.Text;
			objVehicleInfo.VEHICLE_YEAR		=	txtVEHICLE_YEAR.Text ==null?"":txtVEHICLE_YEAR.Text ;
			if(cmbMAKE.SelectedValue != null && cmbMAKE.SelectedValue!="")
				objVehicleInfo.MAKE				=	cmbMAKE.SelectedValue;
			objVehicleInfo.MAKE_OTHER		=	txtMAKE_OTHER.Text;
			if(cmbMODEL.SelectedValue != null && cmbMODEL.SelectedValue!="")
				objVehicleInfo.MODEL			=	cmbMODEL.SelectedValue;
			objVehicleInfo.MODEL_OTHER		=	txtMODEL_OTHER.Text;
			objVehicleInfo.CERTIFICATION	=	txtCERTIFICATION.Text;
			objVehicleInfo.REGISTER			=	txtREGISTER.Text;
			objVehicleInfo.REG_NUMBER		=	txtREG_NUMBER.Text;
			if(cmbENGINE_TYPE.SelectedValue != null && cmbENGINE_TYPE.SelectedValue!="")
				objVehicleInfo.ENGINE_TYPE		=	cmbENGINE_TYPE.SelectedValue;
			if(cmbWING_TYPE.SelectedValue != null && cmbWING_TYPE.SelectedValue!="")
				objVehicleInfo.WING_TYPE		=	cmbWING_TYPE.SelectedValue;
			objVehicleInfo.CREW				=	txtCREW.Text;
			objVehicleInfo.PAX				=	txtPAX.Text;
			objVehicleInfo.REMARKS			=	txtREMARKS.Text;
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidVehicleID.Value;
			//oldXML			=	@hidOldData.Value;
			//Returning the model object

			return objVehicleInfo;
		}
		#endregion
		private void btnSave_Click(object sender, System.EventArgs e)
		{
			int intRetVal;	//For retreiving the return value of business class save function
			ClsVehicleInformation objVehicleInformation = new  ClsVehicleInformation();

			try
			{
				//Retreiving the form values into model class object
				Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objVehicleInfo = GetFormValue();

				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objVehicleInfo.CREATED_BY = int.Parse(GetUserId());
					//objVehicleInfo.CREATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                    objVehicleInfo.CREATED_DATETIME = DateTime.Now;
                    

					objVehicleInfo.IS_ACTIVE = hidIS_ACTIVE.Value;
					//Calling the add method of business layer class
					intRetVal = objVehicleInformation.AddAviationPolicyVehicle(objVehicleInfo,"");

					strRowId = intRetVal.ToString() ;//objVehicleInfo.VEHICLE_ID.ToString();
					hidVehicleID.Value =  intRetVal.ToString() ;//objVehicleInfo.VEHICLE_ID.ToString();
					if(intRetVal>0)
					{
						hidCustomerID.Value = objVehicleInfo.CUSTOMER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						btnActivateDeactivate.Attributes.Add("style","display:inline"); 
						//Opening the endorsement details page
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
					lblMessage.Visible = true;
					
				} // end save case
				else //UPDATE CASE
				{
					
					//Creating the Model object for holding the Old data
					Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objOldVehicleInfo;
					objOldVehicleInfo = new Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo();
					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldVehicleInfo,@hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objVehicleInfo.MODIFIED_BY = int.Parse(GetUserId());
					//objVehicleInfo.LAST_UPDATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                    objVehicleInfo.LAST_UPDATED_DATETIME = DateTime.Now;

					objVehicleInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					//Updating the record using business layer class object
					//intRetVal	= objVehicleInformation.Update(objOldVehicleInfo,objVehicleInfo);
					intRetVal	= objVehicleInformation.UpdateAviationPolicyVehicle(objOldVehicleInfo,objVehicleInfo,""); //hidCustomInfo.Value);

					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						btnActivateDeactivate.Attributes.Add("style","display:inline"); 
						//Opening the endorsement details page
						base.OpenEndorsementDetails();
					}
					else if(intRetVal == -1)	// Duplicate code exist, update failed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"18");
						hidFormSaved.Value		=	"1";
					}
					else 
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"20");
						hidFormSaved.Value		=	"1";
					}
					lblMessage.Visible = true;
				}
				hidOldData.Value = @ClsVehicleInformation.FetchPolAviationVehicleXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPolVersionID.Value==""?"0":hidPolVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21") + " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
				hidFormSaved.Value			=	"2";
				//Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
			}
			finally
			{
				if(objVehicleInformation!= null)
					objVehicleInformation.Dispose();
			}
		}
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				int intRetVal;	
				int intCustomerID = int.Parse(hidCustomerID.Value);
				int intPolId=  int.Parse(hidPOLICY_ID.Value);
				int intPolVerId	= int.Parse(hidPolVersionID.Value);
				int intVehicleId = int.Parse(hidVehicleID.Value);

				Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objVehicleInfo = new Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo();
				base.PopulateModelObject(objVehicleInfo,hidOldData.Value); 
				objVehicleInfo.CUSTOMER_ID =intCustomerID;
				objVehicleInfo.POLICY_ID=intPolId;
				objVehicleInfo.POLICY_VERSION_ID=intPolVerId;
				objVehicleInfo.VEHICLE_ID=intVehicleId;
				objVehicleInfo.MODIFIED_BY=int.Parse(GetUserId());

				ClsVehicleInformation objVehicleInformation = new ClsVehicleInformation();
			
				objVehicleInformation = new Cms.BusinessLayer.BlApplication.ClsVehicleInformation();

				intRetVal = objVehicleInformation.DeleteAviationPolicyVehicle(objVehicleInfo,"");
				if(intRetVal>0)
				{
					lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
					hidFormSaved.Value = "5";
					hidOldData.Value = "";
					trBody.Attributes.Add("style","display:none");
					//Opening the endorsement details page
					base.OpenEndorsementDetails();
				}
				else if(intRetVal == -1)
				{
					lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
					hidFormSaved.Value		=	"2";
				}
				lblDelete.Visible = true;
				lblMessage.Visible=false;
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				//lblMessage.Visible = true;
			
			}
		}
		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			try
			{
				Cms.BusinessLayer.BlApplication.ClsVehicleInformation objVehicle	= new ClsVehicleInformation();
				Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo objVehicleInfo=new Cms.Model.Policy.Aviation.ClsPolicyAviationVehicleInfo();
				//int intResult=0;				
				base.PopulateModelObject(objVehicleInfo,hidOldData.Value); 
				objVehicleInfo.CUSTOMER_ID	=int.Parse(hidCustomerID.Value);
				objVehicleInfo.POLICY_ID	=int.Parse(hidPOLICY_ID.Value);
				objVehicleInfo.POLICY_VERSION_ID=int.Parse(hidPolVersionID.Value);
				objVehicleInfo.VEHICLE_ID=int.Parse(hidVehicleID.Value);
				objVehicleInfo.MODIFIED_BY=int.Parse(GetUserId());
				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{		 			
					objVehicle.ActivateDeactivateAviationPolicyVehicle(objVehicleInfo,"N","");//hidCustomInfo.Value);						
					btnActivateDeactivate.Text="Activate";	
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
					trBody.Attributes.Add("style","display:none");
                    ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID", "<script>parent.strSelectedRecordXML='';parent.RemoveTab(5,parent);parent.RemoveTab(4,parent);parent.RemoveTab(3,parent);parent.RemoveTab(2,parent);RefreshWebGrid('5','1',true,true); </script>");
						
				}
				else
				{									
					objVehicle.ActivateDeactivateAviationPolicyVehicle(objVehicleInfo,"Y","");//hidCustomInfo.Value);
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
					btnActivateDeactivate.Text="Deactivate";
				}
				hidFormSaved.Value			=	"1";
				hidOldData.Value = ClsVehicleInformation.FetchPolAviationVehicleXML(int.Parse(hidCustomerID.Value==""?"0": hidCustomerID.Value) ,int.Parse (hidPOLICY_ID.Value ==""?"0":hidPOLICY_ID.Value),int.Parse (hidPolVersionID.Value==""?"0":hidPolVersionID.Value),int.Parse (hidVehicleID.Value==""?"0":hidVehicleID.Value));
				//RegisterStartupScript("REFRESHGRID","<script>RefreshWebGrid(1," + hidVehicleID.Value + ");</script>");
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;
				//ExceptionManager.Publish(ex);
			}
			finally
			{
				lblMessage.Visible = true;
			
			}
		}
		#region fill Drop Downs
		private void FillDropdown()
		{

			cmbUSE_VEHICLE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("AVUSE");
			cmbUSE_VEHICLE.DataTextField	= "LookupDesc";
			cmbUSE_VEHICLE.DataValueField	= "LookupID";
			cmbUSE_VEHICLE.DataBind();
			cmbUSE_VEHICLE.Items.Insert(0,"");

			cmbCOVG_PERIMETER.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("COVPER");
			cmbCOVG_PERIMETER.DataTextField	= "LookupDesc";
			cmbCOVG_PERIMETER.DataValueField	= "LookupID";
			cmbCOVG_PERIMETER.DataBind();
			cmbCOVG_PERIMETER.Items.Insert(0,"");

			
			cmbMAKE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("AVMAKE");
			cmbMAKE.DataTextField	= "LookupDesc";
			cmbMAKE.DataValueField	= "LookupID";
			cmbMAKE.DataBind();
			cmbMAKE.Items.Insert(0,"");

			cmbMODEL.DataSource =ClsVehicleInformation.FetchAviationVehicleModel();
			cmbMODEL.DataTextField	= "MODEL";
			cmbMODEL.DataValueField	= "ID";
			cmbMODEL.DataBind();
			cmbMODEL.Items.Insert(0,"");
			
			cmbENGINE_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("ENGTYP");
			cmbENGINE_TYPE.DataTextField	= "LookupDesc";
			cmbENGINE_TYPE.DataValueField	= "LookupID";
			cmbENGINE_TYPE.DataBind();
			cmbENGINE_TYPE.Items.Insert(0,"");

			cmbWING_TYPE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("WTYP");
			cmbWING_TYPE.DataTextField	= "LookupDesc";
			cmbWING_TYPE.DataValueField	= "LookupID";
			cmbWING_TYPE.DataBind();
			cmbWING_TYPE.Items.Insert(0,"");



		}
		#endregion
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnSave.Click+= new System.EventHandler(this.btnSave_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);

		}
		#endregion
	}
}
