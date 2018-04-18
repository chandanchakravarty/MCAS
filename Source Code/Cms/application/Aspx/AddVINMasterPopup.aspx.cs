/******************************************************************************************
<Author				: -   
<Start Date			: -	
<End Date			: -	
<Description		: -
<Review Date		: - 
<Reviewed By		: - 	
Modification History

<Modified Date			: -  30/08/2005	
<Modified By				: - Anurag Verma
<Purpose				: - chainging message id according to screen id
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
using Cms.BusinessLayer.BlApplication;
namespace Cms.Application.Aspx
{
	/// <summary>
	/// Summary description for AddVINMasterPopup.
	/// </summary>
	public class AddVINMasterPopup :Cms.Application.appbase
	{
		protected System.Web.UI.WebControls.Label capVEHICLE_YEAR;
		protected System.Web.UI.WebControls.DropDownList cmbVEHICLE_YEAR;		
		
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvVEHICLE_YEAR;
		protected System.Web.UI.WebControls.Label capMAKE;
		protected System.Web.UI.WebControls.DropDownList cmbMAKE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvMAKE;
		protected System.Web.UI.WebControls.Label capMODEL;
		protected System.Web.UI.WebControls.DropDownList cmbMODEL;
	
	 
		protected System.Web.UI.WebControls.Label capBODY_TYPE;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvBODY_TYPE;
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected System.Web.UI.WebControls.Label lblVIN;
		//protected System.Web.UI.WebControls.TextBox txtVIN;
		protected System.Web.UI.WebControls.DropDownList cmbBODY_TYPE;
		public string gStrCalledFrom="-1",gStrPageTitle="",gStrMake="",gStrYear="",gStrMakeCode="",gStrModel="",gStrBodyType="", gStrVIN="",gStrAntiLock="", gStrSymbol="",gStrAirBag="";
		private const string CALLED_FROM_APP="APP";
		private const string CALLED_FROM_MOTORCYCLE="MOT";
		protected System.Web.UI.WebControls.DropDownList cmbVIN;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnAddNew;
		System.Resources.ResourceManager objResourceMgr;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAntiLockBrakes;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVIN;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidAirBags;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidSymbol;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidModel;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidBodyType;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVIN_INDEX; 
		protected System.Web.UI.WebControls.Label lblMessage;//Added for Itrack Issue 5680 on 14 April 2009
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidVINYear;
        protected System.Web.UI.WebControls.Label lblVehicleInfo;//Added for Itrack Issue 5680 on 14 April 2009
		
		public string strCalledFrom ="";
		


		private void Page_Load(object sender, System.EventArgs e)
		{
			#region setting screen id
			if (Request.QueryString["CALLEDFROM"]!=null && Request.QueryString["CALLEDFROM"].ToString().Trim()!="")
			{
				strCalledFrom = Request.QueryString["CALLEDFROM"].ToString().Trim();					
				
			}
			switch(strCalledFrom)
			{
				case "ppa" :
				case "PPA" :
					base.ScreenId	=	"44_0_0";
					break;
				case "umb" :
				case "UMB" :
					base.ScreenId	=	"81_0_0";
					break;
				default :
					base.ScreenId	=	"44_0_0";
					break;
			}
			#endregion
			// Put user code to initialize the page here
			string colorScheme=GetColorScheme();
			
			objResourceMgr = new System.Resources.ResourceManager("Cms.Application.Aspx.AddVinMasterPopup" ,System.Reflection.Assembly.GetExecutingAssembly());


			if (!Page.IsPostBack)
			{
                
				if (Request.QueryString["VIN"]!=null && Request.QueryString["VIN"].ToString().Trim()!="")
				{
					hidVIN.Value = Request.QueryString["VIN"].ToString().Trim();					
					//Replace ^ with & to get correct vin number
					hidVIN.Value =  hidVIN.Value.Replace("^","&");
					int returnValue = GetVINDetails();
					if(returnValue==1)
					{
						gStrCalledFrom = strCalledFrom.ToUpper();
						return;
					}					
				}
                SetCaptions();
				SetErrorMessages();

				// populate dropdowns
				cmbVEHICLE_YEAR.DataSource = ClsVehicleInformation.FetchYearsFromVINMASTER();
				cmbVEHICLE_YEAR.DataTextField	= "MODEL_YEAR";
				cmbVEHICLE_YEAR.DataValueField	= "MODEL_YEAR";
				cmbVEHICLE_YEAR.DataBind();

				cmbVEHICLE_YEAR.Items.Insert(0,new ListItem("",""));
				cmbVEHICLE_YEAR.SelectedIndex=0;
				if(hidVINYear.Value!="" || hidVINYear.Value!=null)
				{
					cmbVEHICLE_YEAR.SelectedValue=hidVINYear.Value;
					cmbVEHICLE_YEAR_SelectedIndexChanged(null,null);
				}
                
				SetRecords();
				//Added for Itrack Issue 5680 on 14 April 2009
				if(Request.QueryString["Msg"]== "Y")
				{
				  lblMessage.Visible = true;
                  lblMessage.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "3");//"This VIN number has more than one corresponding records. Please select proper Model and other details.";
				}
 
			}
		 
		}
		private int GetVINDetails()
		{
			if  (hidVIN.Value.Trim()!="" &&  hidVIN.Value.Length>=10)
			{
				//Dataset dsTemp = new DataSet();
				DataSet dsTemp = ClsVehicleInformation.FetchVINMasterDetailsFromVIN(hidVIN.Value.Substring(0,10));
				if (dsTemp!=null)
				{
					//Added for Itrack Issue 5680 on 14 April 2009
					if(dsTemp.Tables[0]!=null && dsTemp.Tables[0].Rows.Count>1)
					{
						hidVINYear.Value = dsTemp.Tables[0].Rows[0]["Model_year"].ToString();
						return -1;

					}
					else if(	dsTemp.Tables[0]!=null && dsTemp.Tables[0].Rows.Count>0)
					{
						if(dsTemp.Tables[0].Rows[0]["RESULT"].ToString() != "-1")	
						{
							// Show values in controls.
							gStrYear= dsTemp.Tables[0].Rows[0]["Model_year"].ToString(); 
							//gStrMakeCode= dsTemp.Tables[0].Rows[0]["MAKE_CODE"].ToString() + "-" + dsTemp.Tables[0].Rows[0]["Make_Name"].ToString();
							//gStrMake = dsTemp.Tables[0].Rows[0]["MAKE_CODE"].ToString() + "-" + dsTemp.Tables[0].Rows[0]["Make_Name"].ToString();
							gStrMakeCode= dsTemp.Tables[0].Rows[0]["MAKE_CODE"].ToString();
							gStrMake = dsTemp.Tables[0].Rows[0]["MAKE_CODE"].ToString();
							gStrBodyType= dsTemp.Tables[0].Rows[0]["Body_Type"].ToString();
							gStrModel= dsTemp.Tables[0].Rows[0]["Series_Name"].ToString();
							gStrSymbol= dsTemp.Tables[0].Rows[0]["Symbol"].ToString();
							gStrAntiLock=dsTemp.Tables[0].Rows[0]["ANTI_LCK_BRAKES"].ToString(); 
							gStrAirBag=dsTemp.Tables[0].Rows[0]["AIRBAG"].ToString(); 
							gStrVIN = hidVIN.Value;
						}
						return -1;
					}
				}				
			}
			return -1;
			/*gStrYear = cmbVEHICLE_YEAR.SelectedItem.Text ;
			gStrMakeCode =cmbMAKE.SelectedItem.Value;
			gStrMake = cmbMAKE.SelectedItem.Text;
			gStrAirBag = strArray[0];
			gStrSymbol = strArray[1];
			gStrAntiLock=strArray[2];
			gStrModel	=strArray[3];
			gStrBodyType=strArray[4];*/
			/*hidAntiLockBrakes.Value=dsVIN.Tables[0].Rows[0]["Anti_Lck_Brakes"].ToString();
			hidSymbol.Value=dsVIN.Tables[0].Rows[0]["SYMBOL"].ToString();
			hidAirBags.Value=dsVIN.Tables[0].Rows[0]["AIRBAG"].ToString();					
			hidBodyType.Value=dsVIN.Tables[0].Rows[0]["BODY_TYPE"].ToString();
			hidModel.Value=dsVIN.Tables[0].Rows[0]["SERIES_NAME"].ToString();*/
		}

		private void SetRecords()
		{
			ListItem li=null;
			if(Request.QueryString["year"]!=null )
			{
				cmbVEHICLE_YEAR.ClearSelection(); 
				li=cmbVEHICLE_YEAR.Items.FindByValue(Request.QueryString["year"].ToString()); 
				if(li!=null)
				{
					li.Selected=true;
					cmbVEHICLE_YEAR_SelectedIndexChanged(null,null);
				}
                
				li=null;     
			}

			if(Request.QueryString["make"]!=null )
			{
				cmbMAKE.ClearSelection(); 
				li=cmbMAKE.Items.FindByValue(Request.QueryString["make"].ToString()); 
				if(li!=null)
				{
					li.Selected=true;
					cmbMAKE_SelectedIndexChanged(null,null);
				}

				li=null;     
			}

			if(Request.QueryString["model"]!=null )
			{
				li=cmbMODEL.Items.FindByValue(Request.QueryString["model"].ToString()); 
				if(li!=null)
				{
					cmbMODEL.ClearSelection(); 
					li.Selected=true;
					cmbMODEL_SelectedIndexChanged(null,null);
				}

				li=null;     
			}

            

			if(Request.QueryString["bodytype"]!=null )
			{
				li=cmbBODY_TYPE.Items.FindByValue(Request.QueryString["bodytype"].ToString()); 
				if(li!=null)
				{
					cmbBODY_TYPE.ClearSelection(); 
					li.Selected=true;            
				}

				li=null;     
			}

			if(Request.QueryString["addvin"]!=null )
			{
				cmbVIN.ClearSelection();  
				li=cmbVIN.Items.FindByValue(Request.QueryString["addvin"].ToString()); 
				if(li!=null)
					li.Selected=true;

				li=null;     
			}



		}

		#region Set Validators ErrorMessages
		/// <summary>
		/// Method to set validation control error masessages.
		/// Parameters: none
		/// Return Type: none
		/// </summary>
		private void SetErrorMessages()
		{
			rfvVEHICLE_YEAR.ErrorMessage 		=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
			rfvMAKE.ErrorMessage				=  Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
			 
		}
		#endregion

		#region SetCaptions
		private void SetCaptions()
		{
            //=============================================================
            // UPDATED BY SANTOSH KR. GAUTAM ON 07 APRIL 2011 (ITRACK:553)
            //=============================================================
            capVEHICLE_YEAR.Text = objResourceMgr.GetString("cmbVEHICLE_YEAR");
            capMAKE.Text = objResourceMgr.GetString("cmbMAKE");
            capMODEL.Text = objResourceMgr.GetString("cmbMODEL");
            capBODY_TYPE.Text = objResourceMgr.GetString("cmbBODY_TYPE");
            lblVIN.Text = objResourceMgr.GetString("cmbVIN");
            lblVehicleInfo.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "4");
            btnSubmit.Text = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");
            btnAddNew.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "6");
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
			this.cmbVEHICLE_YEAR.SelectedIndexChanged += new System.EventHandler(this.cmbVEHICLE_YEAR_SelectedIndexChanged);
			this.cmbMAKE.SelectedIndexChanged += new System.EventHandler(this.cmbMAKE_SelectedIndexChanged);
			this.cmbMODEL.SelectedIndexChanged += new System.EventHandler(this.cmbMODEL_SelectedIndexChanged);
			this.cmbBODY_TYPE.SelectedIndexChanged += new System.EventHandler(this.cmbBODY_TYPE_SelectedIndexChanged);//Added for Itrack Issue 5831 on 13 May 2009
			//this.cmbVIN.SelectedIndexChanged += new System.EventHandler(this.cmbVIN_SelectedIndexChanged);
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
		private void cmbVEHICLE_YEAR_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//show make
			if (cmbVEHICLE_YEAR.SelectedItem !=null )
			{
					 
				DataSet dsMake = ClsVehicleInformation.FetchMakeFromVINMASTER(cmbVEHICLE_YEAR.SelectedItem.Value);
				cmbMAKE.DataSource			= dsMake ;
				cmbMAKE.DataTextField			= "MAKE";
				cmbMAKE.DataValueField		= "MAKE_CODE";
				cmbMAKE.DataBind();
				cmbMAKE.Items.Insert(0,new ListItem("",""));
				cmbMAKE.SelectedIndex=0;
			}
		 
			cmbMODEL.Items.Clear();
			cmbBODY_TYPE.Items.Clear();
			cmbVIN.Items.Clear();
		}

		private void cmbMAKE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//show model + body type+ vin
			if (cmbMAKE.SelectedItem !=null )
			{
					 
				DataSet dsModel = ClsVehicleInformation.FetchModelFromVINMASTER(cmbMAKE.SelectedItem.Value,cmbVEHICLE_YEAR.SelectedItem.Value);
				cmbMODEL.DataSource			= dsModel ;
				cmbMODEL.DataTextField		= "SERIES_NAME";
				cmbMODEL.DataValueField		= "SERIES_NAME";
				cmbMODEL.DataBind();
				cmbMODEL.Items.Insert(0,new ListItem("",""));
				cmbMODEL.SelectedIndex=0;
				
				//fill body type					 
				cmbBODY_TYPE.Items.Clear();
				DataSet dsBodyType = ClsVehicleInformation.FetchBodyTypeFromVINMASTER(cmbMAKE.SelectedItem.Value,cmbVEHICLE_YEAR.SelectedItem.Value);
				cmbBODY_TYPE.DataSource			= dsBodyType ;
				cmbBODY_TYPE.DataTextField		= "BODY_TYPE";
				cmbBODY_TYPE.DataValueField		= "BODY_TYPE";
				cmbBODY_TYPE.DataBind();
				cmbBODY_TYPE.Items.Insert(0,new ListItem("",""));
				cmbBODY_TYPE.SelectedIndex=0;

				//fill vin
				cmbVIN.Items.Clear();
				DataSet dsVIN  = ClsVehicleInformation.FetchVINFromVINMASTER(cmbMAKE.SelectedItem.Value,cmbVEHICLE_YEAR.SelectedItem.Value);
				cmbVIN.DataSource			= dsVIN ;
				cmbVIN.DataTextField		= "VIN";
				//cmbVIN.DataValueField		= "VIN"; //cmbVIN has been assigned the field vin+symbol as it will be used for fetching symbol value corresponding to VIN. 														
				cmbVIN.DataValueField		= "VINSYMBOL";
				cmbVIN.DataBind();
				cmbVIN.Items.Insert(0,new ListItem("",""));
				cmbVIN.SelectedIndex=0;

				//if(dsVIN.Tables[0].Rows.Count>0 )
				//	hidAntiLockBrakes.Value = dsVIN.Tables[0].Rows[0]["Anti_Lck_Brakes"].ToString(); 
			}
		}

		private void cmbMODEL_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//show body
			if (cmbMODEL.SelectedItem !=null )
			{
				//fill body type	
				cmbBODY_TYPE.Items.Clear(); 
				DataSet dsBodyType = ClsVehicleInformation.FetchBodyTypeFromVINMASTER(cmbMODEL.SelectedItem.Text,cmbMAKE.SelectedItem.Value,cmbVEHICLE_YEAR.SelectedItem.Value);
				cmbBODY_TYPE.DataSource			= dsBodyType ;
				cmbBODY_TYPE.DataTextField		= "BODY_TYPE";
				cmbBODY_TYPE.DataValueField		= "BODY_TYPE";
				cmbBODY_TYPE.DataBind();
				cmbBODY_TYPE.Items.Insert(0,new ListItem("",""));
				cmbBODY_TYPE.SelectedIndex=0;

				//fill vin
				cmbVIN.Items.Clear();
				DataSet dsVIN  = ClsVehicleInformation.FetchVINFromVINMASTER(cmbMODEL.SelectedItem.Text,cmbMAKE.SelectedItem.Value,cmbVEHICLE_YEAR.SelectedItem.Value);
				
				
				cmbVIN.DataSource			= dsVIN ;
				cmbVIN.DataTextField		= "VIN";
				//cmbVIN.DataValueField		= "VIN"; //cmbVIN has been assigned the field vin+symbol as it will be used for fetching symbol value corresponding to VIN. 														
				cmbVIN.DataValueField		= "VINSYMBOL";
				cmbVIN.DataBind();
				cmbVIN.Items.Insert(0,new ListItem("",""));
				cmbVIN.SelectedIndex=0;
			}
		}
		//Added for Itrack Issue 5831 on 13 May 2009
		private void cmbBODY_TYPE_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//show vin 
			if (cmbBODY_TYPE.SelectedItem !=null )
			{
				//fill vin
				cmbVIN.Items.Clear();
				DataSet dsVIN  = ClsVehicleInformation.FetchVINFromVINMASTER(cmbMODEL.SelectedItem.Text,cmbMAKE.SelectedItem.Value,cmbVEHICLE_YEAR.SelectedItem.Value,cmbBODY_TYPE.SelectedItem.Value);
				
				
				cmbVIN.DataSource			= dsVIN ;
				cmbVIN.DataTextField		= "VIN";
				//cmbVIN.DataValueField		= "VIN"; //cmbVIN has been assigned the field vin+symbol as it will be used for fetching symbol value corresponding to VIN. 														
				cmbVIN.DataValueField		= "VINSYMBOL";
				cmbVIN.DataBind();
				cmbVIN.Items.Insert(0,new ListItem("",""));
				cmbVIN.SelectedIndex=0;
			}
		}

		private void cmbVIN_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(cmbVIN.SelectedIndex!=0)
				{
					DataSet dsVIN=ClsVehicleInformation.FetchVINDataFromVINMASTER(cmbVIN.SelectedItem.Text);
					if(dsVIN!=null)
					{
						hidAntiLockBrakes.Value=dsVIN.Tables[0].Rows[0]["Anti_Lck_Brakes"].ToString();
						hidSymbol.Value=dsVIN.Tables[0].Rows[0]["SYMBOL"].ToString();
						hidAirBags.Value=dsVIN.Tables[0].Rows[0]["AIRBAG"].ToString();					
						hidBodyType.Value=dsVIN.Tables[0].Rows[0]["BODY_TYPE"].ToString();
						hidModel.Value=dsVIN.Tables[0].Rows[0]["SERIES_NAME"].ToString();
					}
				}
				
			}
			catch(Exception ex)
			{
				throw(ex);
			}

		}

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			string[] strArray=null;
			// check and send the control to the prev  page.
			if (Request.QueryString["CalledFrom"]!=null )
			{
				gStrYear = cmbVEHICLE_YEAR.SelectedItem.Text ;
				gStrMakeCode =cmbMAKE.SelectedItem.Value;
				gStrMake = cmbMAKE.SelectedItem.Text;
				//gStrModel = cmbMODEL.SelectedItem.Text;
				//gStrBodyType = cmbBODY_TYPE.SelectedItem.Text;
				//gStrAntiLock=hidAntiLockBrakes.Value; 
//				if(cmbVIN.SelectedIndex!=0)
//				{
//					gStrVIN = cmbVIN.SelectedItem.Text;
//					/*strArray=cmbVIN.SelectedItem.Value.Split('~');
//					if(strArray.Length>0)
//						gStrSymbol = strArray[1];*/
//
//					gStrAirBag=hidAirBags.Value;
//					gStrSymbol=hidSymbol.Value;
//					gStrAntiLock=hidAntiLockBrakes.Value;
//					gStrModel = hidModel.Value;
//					gStrBodyType = hidBodyType.Value;
//				}				
				if(cmbVIN.SelectedIndex!=0)
				{
					string vinIndex = "0";
					if(hidVIN_INDEX.Value!="")
					{

						vinIndex = hidVIN_INDEX.Value.ToString();	
						gStrVIN = cmbVIN.Items[int.Parse(vinIndex)].Text;
						strArray = cmbVIN.Items[int.Parse(vinIndex)].Value.Split('~');
					}

					//gStrVIN = cmbVIN.SelectedItem.Text;
					//strArray=cmbVIN.SelectedItem.Value.Split('~');
					if(strArray.Length>0)
					{
						gStrAirBag = strArray[0];
						gStrSymbol = strArray[1];
						gStrAntiLock=strArray[2];
						gStrModel	=strArray[3];
						gStrBodyType=strArray[4];
					}

					//gStrAirBag=hidAirBags.Value;
					//gStrSymbol=hidSymbol.Value;
					//gStrAntiLock=hidAntiLockBrakes.Value;
					//gStrModel = hidModel.Value;
					//gStrBodyType = hidBodyType.Value;
				}		
				else
				{
					gStrModel = cmbMODEL.SelectedItem.Text;
					gStrBodyType = cmbBODY_TYPE.SelectedItem.Text;
				}

				gStrCalledFrom =Request.QueryString["CalledFrom"].ToString(); 

				
			}
		}
	}
}
