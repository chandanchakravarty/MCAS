/******************************************************************************************
<Author					: -   Nidhi
<Start Date				: -	  5/11/2005  
<End Date				: -	
<Description			: - 	Motorcycle Information screen
<Review Date			: - 
<Reviewed By			: - 	
Modification History
<Modified Date			:  Sumit Chhabra
<Modified By			:  11/10/2005
<Purpose				:  Added the feature to delete the records

<Modified Date			:  Vijay Arora
<Modified By			:  17/10/2005
<Purpose				:  Added the field named APP_VEHICLE_CLASS

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
using System.Xml;
using Cms.Model.Policy; 

namespace Cms.Policies.Aspx.Umbrella
{
	/// <summary>
	/// Summary description for AddPolUmbVehicleInfo.
	/// </summary>
	public class AddPolUmbVehicleInfo : Cms.Policies.policiesbase 
	{
		
	
		#region Page controls declaration
		
		#endregion

		#region Local form variables
		//START:*********** Local form variables *************
		string oldXML;
		//creating resource manager object (used for reading field and label mapping)
		System.Resources.ResourceManager objResourceMgr;
		private string strRowId, strFormSaved;
		//private int	intLoggedInUserID;
		
		//Defining the business layer class object
		Cms.BusinessLayer.BlApplication.ClsVehicleInformation  objVehicleInformation ;
		protected System.Web.UI.WebControls.Label lblDelete;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected System.Web.UI.WebControls.Label capMOTORCYCLE_TYPE;
		protected System.Web.UI.WebControls.DropDownList cmbMOTORCYCLE_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMOTORCYCLE_TYPE;
		protected System.Web.UI.WebControls.Label capVEHICLE_YEAR;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_YEAR;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_YEAR;
		protected System.Web.UI.WebControls.Label capREGN_PLATE_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtREGN_PLATE_NUMBER;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvREGN_PLATE_NUMBER;
		protected System.Web.UI.WebControls.RangeValidator rngVEHICLE_YEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.TextBox txtMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.TextBox txtMODEL;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMODEL;
		protected Cms.CmsWeb.Controls.CmsButton btnReset;
		protected Cms.CmsWeb.Controls.CmsButton btnCopy;
		protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
		//		protected Cms.CmsWeb.Controls.CmsButton btnDelete;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBody;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelectForVehicleMake;
		protected System.Web.UI.HtmlControls.HtmlImage imgSelectVehicleModel;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCustomInfo;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVEHICLE_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidPOLICY_VERSION_ID;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidCalledFrom;
		protected Cms.CmsWeb.WebControls.WorkFlow myWorkFlow;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAPP_VERSION_ID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUMB_VEHICLE_TYPE_OTHER;	
		protected System.Web.UI.WebControls.Label capIS_EXCLUDED;
		protected System.Web.UI.WebControls.DropDownList cmbIS_EXCLUDED;
		protected System.Web.UI.WebControls.Label capUSE_VEHICLE;
		protected System.Web.UI.WebControls.DropDownList cmbUSE_VEHICLE;
		protected System.Web.UI.WebControls.Label capOTHER_POLICY;
		protected System.Web.UI.WebControls.DropDownList cmbOTHER_POLICY;
		protected System.Web.UI.WebControls.Label capVEHICLE_NUMBER;
		protected System.Web.UI.WebControls.TextBox txtVEHICLE_NUMBER;

				
		private string strCalledFrom="";
		

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
			rfvVEHICLE_YEAR.ErrorMessage	=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"166");
			rfvMAKE.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"168");
			rfvMODEL.ErrorMessage			=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"169");						
			rngVEHICLE_YEAR.MaximumValue = (DateTime.Now.Year+1).ToString();
			rngVEHICLE_YEAR.MinimumValue = aAppMinYear  ;			
			rngVEHICLE_YEAR.ErrorMessage =  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"673");
			rfvMOTORCYCLE_TYPE.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("456");
			rfvREGN_PLATE_NUMBER.ErrorMessage =  Cms.CmsWeb.ClsMessages.FetchGeneralMessage("861");
			
		}
		#endregion
		
		#region PageLoad event
		private void Page_Load(object sender, System.EventArgs e)
		{	
			
			base.ScreenId="275_0";
            btnActivateDeactivate.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1333");   
			lblMessage.Visible = false;
			//START:*********** Setting permissions and class (Read/write/execute/delete)  *************
			btnReset.CmsButtonClass	=	CmsButtonType.Write;
			btnReset.PermissionString		=	gstrSecurityXML;			
			
			btnSave.CmsButtonClass	=	CmsButtonType.Write;
			btnSave.PermissionString		=	gstrSecurityXML;

			btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
			btnActivateDeactivate.PermissionString	=	gstrSecurityXML;

			//			btnDelete.CmsButtonClass	=	CmsButtonType.Delete;
			//			btnDelete.PermissionString		=	gstrSecurityXML;
			objResourceMgr = new System.Resources.ResourceManager("Cms.Policies.Aspx.Umbrella.AddPolUmbVehicleInfo" ,System.Reflection.Assembly.GetExecutingAssembly());
			if(!Page.IsPostBack)
			{
				hidUMB_VEHICLE_TYPE_OTHER.Value = ClsVehicleInformation.UMB_VEHICLE_TYPE_OTHER;
				if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
				{
					strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().ToUpper();
				}				
				cmbMOTORCYCLE_TYPE.Attributes.Add("onChange","javascript:cmbMOTORCYCLE_TYPE_Change();");
				btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");			
				hidCalledFrom.Value =strCalledFrom;
				SetErrorMessages();
				GetQueryStringValues();				
				SetCaptions();												
				FillDropdowns();
				FillData();
				SetWorkflow();
				if (hidVEHICLE_ID.Value == "NEW")
				{
					//Set the next VehicleID number
					int intVehicleNumber = ClsVehicleInformation.GetNextPolCompanyIDNumber
						(Convert.ToInt32(hidCUSTOMER_ID.Value),
						Convert.ToInt32(this.hidPOLICY_ID.Value),
						Convert.ToInt32(this.hidPOLICY_VERSION_ID.Value)
						);
						
					if ( intVehicleNumber == -1 )
					{
						lblMessage.Visible = true;
						lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"7");
						return;
					}
					else
					{
						this.txtVEHICLE_NUMBER.Text = intVehicleNumber.ToString();
					}
				}
			}		
		}//end pageload
		#endregion

		private void FillData()
		{
			if (hidVEHICLE_ID.Value == "NEW")
				return;
			hidOldData.Value = @ClsVehicleInformation.FetchPolicyUmbrellaVehicleXML(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidVEHICLE_ID.Value));
			if (hidOldData.Value!="")
			{
				string strOldXml=hidOldData.Value;
				System.Xml.XmlDocument objXmlDoc = new System.Xml.XmlDocument();
				string strMotorType= "";//ClsCommon.GetNodeValue(objXmlDoc,"MOTORCYCLE_TYPE");
				string strOtherPol ="" ;//ClsCommon.GetNodeValue(objXmlDoc,"OTHER_POLICY");
				objXmlDoc.LoadXml(strOldXml);
				System.Xml.XmlNodeList nodList ;
				nodList= objXmlDoc.GetElementsByTagName("MOTORCYCLE_TYPE");
				if (nodList.Count >0)
				{
					strMotorType=nodList.Item(0).InnerText;
				}
				nodList = objXmlDoc.GetElementsByTagName("OTHER_POLICY");
				if (nodList.Count >0)
				{
					strOtherPol=nodList.Item(0).InnerText;
				}
				nodList= objXmlDoc.GetElementsByTagName("INSURED_VEH_NUMBER");
				if (nodList.Count >0)
				{
					txtVEHICLE_NUMBER.Text=nodList.Item(0).InnerText;
				}
				cmbMOTORCYCLE_TYPE.SelectedIndex  = cmbMOTORCYCLE_TYPE.Items.IndexOf(cmbMOTORCYCLE_TYPE.Items.FindByValue(strMotorType));
				cmbMOTORCYCLE_TYPE_SelectedIndexChanged(null,null);
				hidFormSaved.Value ="0";
				cmbOTHER_POLICY.SelectedIndex  = cmbOTHER_POLICY.Items.IndexOf(cmbOTHER_POLICY.Items.FindByValue(strOtherPol));
			}
		}
		
		private void FillDropdowns()
		{
			try 
			{
				DataTable dt = Cms.CmsWeb.ClsFetcher.State;
				cmbMOTORCYCLE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("UVEHTP");
				cmbMOTORCYCLE_TYPE.DataTextField	= "LookupDesc";
				cmbMOTORCYCLE_TYPE.DataValueField	= "LookupID";
				cmbMOTORCYCLE_TYPE.DataBind();
				cmbMOTORCYCLE_TYPE.Items.Insert(0,"");

				cmbIS_EXCLUDED.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("YESNO");
				cmbIS_EXCLUDED.DataTextField="LookupDesc"; 
				cmbIS_EXCLUDED.DataValueField="LookupCode";
				cmbIS_EXCLUDED.DataBind();

				cmbUSE_VEHICLE.DataSource =Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("VHUCP");
				cmbUSE_VEHICLE.DataTextField	= "LookupDesc";
				cmbUSE_VEHICLE.DataValueField	= "LookupID";
				cmbUSE_VEHICLE.DataBind();
				cmbUSE_VEHICLE.Items.Insert(0,"");

				ClsUmbrellaRecrVeh objUmbrellaRecrVeh = new ClsUmbrellaRecrVeh();
				//dt = objUmbrellaRecrVeh.GetPolSelectedOtherPolicies(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value));
				dt = objUmbrellaRecrVeh.GetPolSelectedOtherPolicies(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),ClsUmbSchRecords.CALLED_FROM_VEHICLES,cmbMOTORCYCLE_TYPE.SelectedValue);

				if(cmbMOTORCYCLE_TYPE.SelectedValue != "")
				{
					if(dt!=null && dt.Rows.Count>0)
					{
						cmbOTHER_POLICY.DataSource = dt;
						cmbOTHER_POLICY.DataTextField = "POLICY_NUMBER_LOB";
						cmbOTHER_POLICY.DataValueField = "POLICY_NUMBER";
						cmbOTHER_POLICY.DataBind();
						cmbOTHER_POLICY.Items.Insert(0,new ListItem("",""));

					}
				}
			}
			catch(Exception exc)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(exc);
			}
			finally
			{}
		
		}

		#region GetFormValue
		/// <summary>
		/// Fetch form's value and stores into model class object and return that object.
		/// </summary>
		private Cms.Model.Policy.Umbrella.ClsVehicleInfo GetFormValue()
		{
			//Creating the Model object for holding the New data
			Cms.Model.Policy.Umbrella.ClsVehicleInfo objVehicleInfo = new Cms.Model.Policy.Umbrella.ClsVehicleInfo();
			
			
			objVehicleInfo.VEHICLE_YEAR=	txtVEHICLE_YEAR.Text;
			objVehicleInfo.MAKE=	txtMAKE.Text ;
			objVehicleInfo.MODEL=	txtMODEL.Text ;
			objVehicleInfo.MODIFIED_BY	=	Int32.Parse(GetUserId());
			objVehicleInfo.VEHICLE_ID = Convert.ToInt32(this.txtVEHICLE_NUMBER.Text.Trim());
			objVehicleInfo.CUSTOMER_ID = int.Parse(hidCUSTOMER_ID.Value);
		
			if (cmbMOTORCYCLE_TYPE.SelectedItem!=null && cmbMOTORCYCLE_TYPE.SelectedItem.Value!="")
			{
				objVehicleInfo.MOTORCYCLE_TYPE =int.Parse (cmbMOTORCYCLE_TYPE.SelectedItem.Value);
			}			

			if(cmbIS_EXCLUDED.SelectedItem!=null && cmbIS_EXCLUDED.SelectedItem.Value!="")
				objVehicleInfo.IS_EXCLUDED =int.Parse (cmbIS_EXCLUDED.SelectedItem.Value); 
			if(cmbUSE_VEHICLE.SelectedItem!=null && cmbUSE_VEHICLE.SelectedItem.Value!="")
				objVehicleInfo.USE_VEHICLE =int.Parse (cmbUSE_VEHICLE.SelectedItem.Value); 

			objVehicleInfo.POLICY_ID =   int.Parse(hidPOLICY_ID.Value); 
			objVehicleInfo.POLICY_VERSION_ID = int.Parse(hidPOLICY_VERSION_ID.Value);
			if(objVehicleInfo.MOTORCYCLE_TYPE==11959 && txtREGN_PLATE_NUMBER.Text.Trim()!="") 
				objVehicleInfo.REGN_PLATE_NUMBER = txtREGN_PLATE_NUMBER.Text.Trim();

			if(cmbOTHER_POLICY.SelectedItem!=null)
				objVehicleInfo.OTHER_POLICY = cmbOTHER_POLICY.SelectedItem.Value.ToString();
			if (hidVEHICLE_ID.Value !=null )
			{
				if (hidVEHICLE_ID.Value.ToString()=="NEW")
				{
					objVehicleInfo.VEHICLE_ID = 0;}
				else
				{
					objVehicleInfo.VEHICLE_ID = int.Parse (hidVEHICLE_ID.Value.ToString());					
				}
			}		
		
			
			//These  assignments are common to all pages.
			strFormSaved	=	hidFormSaved.Value;
			strRowId		=	hidVEHICLE_ID.Value;
			oldXML		= @hidOldData.Value;
			//Returning the model object

			return objVehicleInfo;
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
			//			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.cmbMOTORCYCLE_TYPE.SelectedIndexChanged += new System.EventHandler(this.cmbMOTORCYCLE_TYPE_SelectedIndexChanged);


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
				int intRetVal=0;	//For retreiving the return value of business class save function
				objVehicleInformation = new  ClsVehicleInformation();

				//Retreiving the form values into model class object
				Cms.Model.Policy.Umbrella.ClsVehicleInfo objVehicleInfo = GetFormValue();
				
				if(strRowId.ToUpper().Equals("NEW")) //save case
				{
					objVehicleInfo.CREATED_BY = int.Parse(GetUserId());
					objVehicleInfo.CREATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
	
					if(cmbMOTORCYCLE_TYPE.SelectedValue != "11957")
						intRetVal = objVehicleInformation.AddUmbrellaPolicyVehicle(objVehicleInfo,"MOT");					
					else
						intRetVal = objVehicleInformation.AddUmbrellaPolicyVehicle(objVehicleInfo,"AUTO");			
//					intRetVal = objVehicleInformation.AddUmbrellaPolicyVehicle(objVehicleInfo);					
					if(intRetVal>0)
					{
						strRowId = intRetVal.ToString() ;//objVehicleInfo.VEHICLE_ID.ToString();
						hidVEHICLE_ID.Value =  intRetVal.ToString() ;//objVehicleInfo.VEHICLE_ID.ToString()
						hidCUSTOMER_ID.Value  = objVehicleInfo.CUSTOMER_ID.ToString();
						lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
						hidFormSaved.Value			=	"1";
						hidIS_ACTIVE.Value = "Y";
						GetOldDataXML();
						btnActivateDeactivate.Attributes.Add("style","display:inline"); 
						base.OpenEndorsementDetails();
						SetWorkflow();

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
					Cms.Model.Policy.Umbrella.ClsVehicleInfo objOldVehicleInfo = new Cms.Model.Policy.Umbrella.ClsVehicleInfo();					

					//Setting  the Old Page details(XML File containing old details) into the Model Object
					base.PopulateModelObject(objOldVehicleInfo,@hidOldData.Value);

					//Setting those values into the Model object which are not in the page
					objVehicleInfo.MODIFIED_BY = int.Parse(GetUserId());
					objVehicleInfo.LAST_UPDATED_DATETIME = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
					objVehicleInfo.IS_ACTIVE = hidIS_ACTIVE.Value;

					intRetVal	= objVehicleInformation.UpdatePolicyUmbrellaVehicle(objOldVehicleInfo,objVehicleInfo);					
				
					if( intRetVal > 0 )			// update successfully performed
					{
						lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
						hidFormSaved.Value		=	"1";
						btnActivateDeactivate.Attributes.Add("style","display:inline");  
						GetOldDataXML();
						base.OpenEndorsementDetails();
						SetWorkflow();
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

		
		#endregion

		//		#region Handling Delete Event
		//		private void btnDelete_Click(object sender, System.EventArgs e)
		//		{
		//			int intRetVal;		
		//			objVehicleInformation = new  ClsVehicleInformation();
		//
		//			//Retreiving the form values into model class object
		//			Cms.Model.Policy.Umbrella.ClsVehicleInfo objVehicleInfo = GetFormValue();
		//			
		//			intRetVal = objVehicleInformation.DeletePolicyUmbrellaVehicle(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidVEHICLE_ID.Value));
		//
		//			if(intRetVal>0)
		//			{
		//				//following single line has been commented
		//				lblDelete.Text = Cms.CmsWeb.ClsMessages.GetMessage("G","127");
		//				//lblMessage.Text		 =	ClsMessages.GetMessage(base.ScreenId,"127");
		//				hidFormSaved.Value = "3";
		//				hidOldData.Value = "";
		//				trBody.Attributes.Add("style","display:none");
		//				SetWorkflow();
		//				
		//			}
		//			else if(intRetVal == -1)
		//			{
		//				//following single line has been commented
		//				lblDelete.Text		=	ClsMessages.GetMessage(base.ScreenId,"128");
		//				hidFormSaved.Value		=	"2";
		//			}
		//			lblDelete.Visible = true;
		//		}
		//		#endregion
		private void SetCaptions()
		{
			capVEHICLE_YEAR.Text				=		objResourceMgr.GetString("cmbVEHICLE_YEAR");
			capMAKE.Text						=		objResourceMgr.GetString("cmbMAKE");
			capMODEL.Text						=		objResourceMgr.GetString("cmbMODEL");			
			capMOTORCYCLE_TYPE.Text				= 		objResourceMgr.GetString("cmbMOTORCYCLE_TYPE");			
			capREGN_PLATE_NUMBER.Text			= 		objResourceMgr.GetString("txtREGN_PLATE_NUMBER");	
			capIS_EXCLUDED.Text					=		objResourceMgr.GetString("cmbIS_EXCLUDED");
			capUSE_VEHICLE.Text					=		objResourceMgr.GetString("cmbUSE_VEHICLE");
			capOTHER_POLICY.Text				=		objResourceMgr.GetString("cmbOTHER_POLICY");
			capVEHICLE_NUMBER.Text				=		objResourceMgr.GetString("txtVEHICLE_NUMBER");
		}
		
		private void GetQueryStringValues()
		{
			
			if(Request.QueryString["CUSTOMER_ID"]!=null && Request.QueryString["CUSTOMER_ID"].ToString()!="")
				hidCUSTOMER_ID.Value =Request.QueryString["CUSTOMER_ID"].ToString();
			if(Request.QueryString["POLICY_ID"]!=null && Request.QueryString["POLICY_ID"].ToString()!="")
				hidPOLICY_ID.Value =Request.QueryString["POLICY_ID"].ToString();
			if(Request.QueryString["POLICY_VERSION_ID"]!=null && Request.QueryString["POLICY_VERSION_ID"].ToString()!="")
				hidPOLICY_VERSION_ID.Value =Request.QueryString["POLICY_VERSION_ID"].ToString();

			if (Request.QueryString["VH_ID"]!=null && Request.QueryString["VH_ID"].ToString()!="") // UPDATE MODE
			{
				hidVEHICLE_ID.Value = Request.QueryString["VH_ID"].ToString();		
				btnActivateDeactivate.Attributes.Add("style","display:inline"); 
				GetOldDataXML();
			}
			else
			{
				hidVEHICLE_ID.Value = "NEW";
				//btnActivateDeactivate.Attributes.Add("style","display:none"); 
			}					
		
		}
		private void GetOldDataXML()
		{
			hidOldData.Value = @ClsVehicleInformation.FetchPolicyUmbrellaVehicleXML(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),int.Parse(hidVEHICLE_ID.Value));
		}
		
		private void SetWorkflow()
		{
			if(base.ScreenId == "275_0")
			{
				myWorkFlow.IsTop	=	false;
				myWorkFlow.ScreenID	=	base.ScreenId;
				myWorkFlow.AddKeyValue("CUSTOMER_ID",hidCUSTOMER_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_ID",hidPOLICY_ID.Value);
				myWorkFlow.AddKeyValue("POLICY_VERSION_ID",hidPOLICY_VERSION_ID.Value);

				if (hidVEHICLE_ID.Value != "0" && hidVEHICLE_ID.Value != ""  && hidVEHICLE_ID.Value.ToUpper() != "NEW")
				{
					myWorkFlow.AddKeyValue("VEHICLE_ID",hidVEHICLE_ID.Value);
				}

				myWorkFlow.GetScreenStatus();
				myWorkFlow.SetScreenStatus();
			}
			else
			{
				myWorkFlow.Display	=	false;
			}
		}

		private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
		{
			Cms.BusinessLayer.BlApplication.ClsVehicleInformation objVehicle	=  new ClsVehicleInformation();
			try
			{

				Cms.Model.Policy.Umbrella.ClsVehicleInfo objVehicleInfo= new Cms.Model.Policy.Umbrella.ClsVehicleInfo();
				

				if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
				{	
					objVehicleInfo=GetFormValue();
					//objVehicle.ActivateDeactivateAutoMotorVehicle(objVehicleInfo,"N",hidCustomInfo.Value);
					//objVehicle.ActivateDeactivateAutoMotorVehicle(objVehicleInfo,"N",hidCustomInfo.Value,hidCalledFrom.Value.ToUpper());
					objVehicle.ActivateDeactivateUmbrellaVehiclePolicy(objVehicleInfo ,"N");
					//btnActivateDeactivate.Text="Activate";	
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
					hidIS_ACTIVE.Value="N";
				}
				else
				{
					objVehicleInfo=GetFormValue();					
					//objVehicle.ActivateDeactivateAutoMotorVehicle(objVehicleInfo,"Y",hidCustomInfo.Value,hidCalledFrom.Value.ToUpper());					
					objVehicle.ActivateDeactivateUmbrellaVehiclePolicy(objVehicleInfo ,"Y");
					lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
					hidIS_ACTIVE.Value="Y";
				}				
				hidFormSaved.Value			=	"0";	
				base.OpenEndorsementDetails();
				GetOldDataXML();
				ClientScript.RegisterStartupScript(this.GetType(),"REFRESHGRID","<script>RefreshWebGrid(1," + hidVEHICLE_ID.Value + ");</script>");
			}
			catch(Exception ex)
			{
				lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
				lblMessage.Visible	=	true;				
			}
			finally
			{
				lblMessage.Visible = true;
				if(objVehicle!=null)
					objVehicle = null;
				
			}
		}


		#region Event Handler cmbMOTORCYCLE_TYPE_SelectedIndexChanged
		private void cmbMOTORCYCLE_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dt = Cms.CmsWeb.ClsFetcher.State;
			ClsUmbrellaRecrVeh objUmbrellaRecrVeh = new ClsUmbrellaRecrVeh();
			//dt = objUmbrellaRecrVeh.GetSelectedOtherPolicies(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidAPP_ID.Value),int.Parse(hidAPP_VERSION_ID.Value),ClsUmbSchRecords.CALLED_FROM_VEHICLES,cmbMOTORCYCLE_TYPE.SelectedValue);
			dt = objUmbrellaRecrVeh.GetPolSelectedOtherPolicies(int.Parse(hidCUSTOMER_ID.Value),int.Parse(hidPOLICY_ID.Value),int.Parse(hidPOLICY_VERSION_ID.Value),Cms.BusinessLayer.BlApplication.ClsUmbSchRecords.CALLED_FROM_VEHICLES,cmbMOTORCYCLE_TYPE.SelectedValue);

			if(dt!=null && dt.Rows.Count>0)
			{
				cmbOTHER_POLICY.DataSource = dt;
				cmbOTHER_POLICY.DataTextField = "POLICY_NUMBER_LOB";
				cmbOTHER_POLICY.DataValueField = "POLICY_NUMBER";
				cmbOTHER_POLICY.DataBind();
				cmbOTHER_POLICY.Items.Insert(0,new ListItem("",""));
				hidFormSaved.Value ="2";

			}
			else
			{
				cmbOTHER_POLICY.Items.Clear();
			}
		}
		#endregion
	}
}
