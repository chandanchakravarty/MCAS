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

namespace Cms.CmsWeb.Maintenance
{
	/// <summary>
	/// Summary description for AddUnderwriterAssignment.
	/// </summary>
	public class AddUnderwriterAssignment : Cms.CmsWeb.cmsbase
	{
		protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capAssignedUnderwriter;
		protected Cms.CmsWeb.Controls.CmsButton btnSave;
		protected System.Web.UI.HtmlControls.HtmlForm AssignLOBUnderwriter;
		protected System.Web.UI.WebControls.ListBox lbUnAssignUnderwriter;
		protected System.Web.UI.WebControls.ListBox lbAssignUnderwriter;
		private Cms.BusinessLayer.BlCommon.ClsAgency objAgency;
		protected System.Web.UI.WebControls.DropDownList cmbLOB;
        protected System.Web.UI.WebControls.Label capheader;
        protected System.Web.UI.WebControls.Label capMessages;
        protected System.Web.UI.WebControls.Label capProduct;
	
		private int intAgencyId=0;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvlbAssignUnderwriter;
		protected System.Web.UI.WebControls.RequiredFieldValidator rfvLob;
		protected System.Web.UI.HtmlControls.HtmlInputButton AssignUnderwriters;
		protected System.Web.UI.HtmlControls.HtmlInputButton UnAssignUnderwriters;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.ListBox Listbox1;
		protected System.Web.UI.WebControls.ListBox Listbox2;
		protected System.Web.UI.HtmlControls.HtmlInputButton Button1;
		protected System.Web.UI.HtmlControls.HtmlInputButton Button2;
		protected System.Web.UI.WebControls.Label capUnassignMarketeer;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidMarketeerID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidUnderwriterNames;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidMarketeerNames;
		protected System.Web.UI.WebControls.Label capUnassignUnderwriter;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidUnderwriterID;
		protected System.Web.UI.WebControls.Label capAssignedMarketeer;
		protected System.Web.UI.WebControls.ListBox lbUnAssignMarketeer;
		protected System.Web.UI.WebControls.ListBox lbAssignMarketeer;
		protected System.Web.UI.HtmlControls.HtmlInputButton AssignMarketeers;
		protected System.Web.UI.HtmlControls.HtmlInputButton UnAssignMarketeers;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidunassgnuw;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidunassgnuwnm;
		private int intLobId =0;
        public string Unassignalert = "";
        System.Resources.ResourceManager objResourceMgr;
        public string markeeter = "";
        public string underwriter = "";

		private void Page_Load(object sender, System.EventArgs e)
		{			
			// Put user code to initialize the page here
           
			if(Request.QueryString.HasKeys())
				intAgencyId=Convert.ToInt32(Request.QueryString["EntityId"].ToString());
			base.ScreenId = "10_3";
			lbUnAssignUnderwriter.Attributes.Add("ondblclick","javascript:AssignUnderwriter();");
			lbAssignUnderwriter.Attributes.Add("ondblclick","javascript:UnAssignUnderwriter();");
			lbUnAssignMarketeer.Attributes.Add("ondblclick","javascript:AssignMarketeer();");
			lbAssignMarketeer.Attributes.Add("ondblclick","javascript:UnAssignMarketeer();");
			btnSave.CmsButtonClass		=	Cms.CmsWeb.Controls.CmsButtonType.Write;
			btnSave.PermissionString	=	gstrSecurityXML;
			btnSave.Attributes.Add("onClick","CountAssignUnderwriter();");
			//btnSave.Attributes.Add("onClick","CountAssignUnderwriter();");
			lblMessage.Text = "";
			string strCarrierSystemID = Cms.CmsWeb.cmsbase.CarrierSystemID;
			string strAgencyID = GetSystemId();
			if(strCarrierSystemID.ToUpper()!=strAgencyID.ToUpper())
				btnSave.Enabled = false;
            objResourceMgr = new System.Resources.ResourceManager("Cms.CmsWeb.Maintenance.AddUnderwriterAssignment", System.Reflection.Assembly.GetExecutingAssembly());
            rfvlbAssignUnderwriter.Text = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1186");
            rfvlbAssignUnderwriter.IsValid = true;
            rfvlbAssignUnderwriter.Enabled = false;

            SetCaptions();	
			if(!Page.IsPostBack)
            {
                		
				DataSet dsRecieve;
				try
				{
					dsRecieve =new DataSet();
					Cms.BusinessLayer.BlCommon.ClsStates objState = new 
						Cms.BusinessLayer.BlCommon.ClsStates();
					dsRecieve = objState.PoplateLob();
					
					this.cmbLOB.DataSource =	dsRecieve.Tables[0];
					this.cmbLOB.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
					this.cmbLOB.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
					this.cmbLOB.DataBind();
					this.cmbLOB.Items.Insert(0,"");
					this.cmbLOB.SelectedIndex=0;

					
					//Disable the JavaScript function also
					//this.btnSave.Attributes.Add("onClick","javascript:CountAssignUnderwriter();");					
					ShowHideControls(false);                   
				}
				catch(Exception objExcep)
				{
					Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
				}
			}
			else
			{
                
				PopulateAssignedUnderwriter();
				PopulateUnassignedUnderwriter();
				PopulateAssignedMarketeer();
				PopulateUnassignedMarketeer();

				
			}
           // Unassignalert = objResourceMgr.GetString("lblUnassignalert");
															
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
			this.cmbLOB.SelectedIndexChanged += new System.EventHandler(this.cmbLOB_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void PopulateUnassignedMarketeer()
		{	
			DataSet dsRecieve ;
			try
			{
				objAgency =new Cms.BusinessLayer.BlCommon.ClsAgency();

				if(Convert.ToInt32(this.cmbLOB.SelectedIndex) >= 0)
				{
                    intLobId = Convert.ToInt32(cmbLOB.SelectedItem.Value);
					lbUnAssignMarketeer.Items.Clear();
					dsRecieve = new DataSet();
                    // Changed because we hve to show all marketers which are not assigned for particular agency and lobID
					//dsRecieve = objAgency.PopulateUnassignedMarketeer();
                    dsRecieve = objAgency.PopulateAgency_MrkUW(intAgencyId, intLobId,"UM");
					for (int ctr=0;ctr < dsRecieve.Tables[0].Rows.Count; ctr++)
					{
						//if (lbAssignUnderwriter.Items.FindByValue(dsRecieve.Tables[0].Columns[0].ToString()) == null)
						if (lbAssignMarketeer.Items.FindByValue(dsRecieve.Tables[0].Rows[ctr][0].ToString()) == null)
						{
							lbUnAssignMarketeer.Items.Add (new ListItem(dsRecieve.Tables[0].Rows[ctr][1].ToString(), dsRecieve.Tables[0].Rows[ctr][0].ToString()));
						}

					}

                        this.lbUnAssignMarketeer.Items.Insert(0, markeeter);
                        this.lbUnAssignMarketeer.SelectedIndex = 0;
                   
				}
				
			}
               
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}
        private void SetCaptions()
        {
            capAssignedMarketeer.Text = objResourceMgr.GetString("lbAssignMarketeer");
            capAssignedUnderwriter.Text = objResourceMgr.GetString("lbAssignUnderwriter");
            capMessages.Text = objResourceMgr.GetString("capMessages");
            capUnassignUnderwriter.Text = objResourceMgr.GetString("lbUnAssignUnderwriter");
            capheader.Text = objResourceMgr.GetString("capheader");
            capUnassignMarketeer.Text = objResourceMgr.GetString("lbUnAssignMarketeer");
            Unassignalert = objResourceMgr.GetString("lblUnassignalert");
            capProduct.Text = objResourceMgr.GetString("cmbLOB");
            markeeter = objResourceMgr.GetString("markeeter");
            underwriter = objResourceMgr.GetString("underwriter");


        }
                	

		private void PopulateUnassignedUnderwriter()
		{	
			DataSet dsRecieve ;
			try
			{
				objAgency =new Cms.BusinessLayer.BlCommon.ClsAgency();

				if(Convert.ToInt32(this.cmbLOB.SelectedIndex) >= 0)
				{
                    intLobId = Convert.ToInt32(cmbLOB.SelectedItem.Value);
					lbUnAssignUnderwriter.Items.Clear();
					dsRecieve = new DataSet();
					
                    // Changed because we hve to show all underwriters which are not assigned for particular agency and lobID
                    //dsRecieve = objAgency.PopulateUnassignedUnderwriter();
                    dsRecieve = objAgency.PopulateAgency_MrkUW(intAgencyId, intLobId, "UUW");
					for (int ctr=0;ctr < dsRecieve.Tables[0].Rows.Count; ctr++)
					{
						//if (lbAssignUnderwriter.Items.FindByValue(dsRecieve.Tables[0].Columns[0].ToString()) == null)
						if (lbAssignUnderwriter.Items.FindByValue(dsRecieve.Tables[0].Rows[ctr][0].ToString()) == null)
						{
							lbUnAssignUnderwriter.Items.Add (new ListItem(dsRecieve.Tables[0].Rows[ctr][1].ToString(), dsRecieve.Tables[0].Rows[ctr][0].ToString()));
						}

					}

                    this.lbUnAssignUnderwriter.Items.Insert(0,underwriter);
					this.lbUnAssignUnderwriter.SelectedIndex =0;
				}
				
			}
                	
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}

		/// <summary>
		/// fill the listbox with all the Assigned departments
		/// </summary>
		private void PopulateAssignedMarketeer()
		{
			objAgency =new Cms.BusinessLayer.BlCommon.ClsAgency();
			DataSet dsRecieve ;
			try
			{
				this.lbAssignMarketeer.Items.Clear();
				if(Convert.ToInt32(this.cmbLOB.SelectedIndex)>=0)
				{	
					dsRecieve =new DataSet();
					intLobId = Convert.ToInt32(cmbLOB.SelectedItem.Value);
                    dsRecieve = objAgency.PopulateAgency_MrkUW(intAgencyId, intLobId, "AM");
					if(dsRecieve!=null && dsRecieve.Tables.Count>0)
					{
						this.lbAssignMarketeer.DataSource = dsRecieve.Tables[0];
						this.lbAssignMarketeer.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
						this.lbAssignMarketeer.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
						this.lbAssignMarketeer.DataBind();
						//this.lbAssignMarketeer.Items.Insert(0,">>...Select Marketeer");
						if (this.lbAssignMarketeer.Items.Count > 0)
							this.lbAssignMarketeer.SelectedIndex =0;
						btnSave.Attributes.Add("style","display:inline");					
					}
						
				}
							
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}
       
		private void PopulateAssignedUnderwriter()
		{
			objAgency =new Cms.BusinessLayer.BlCommon.ClsAgency();
			DataSet dsRecieve ;
			try
			{
				this.lbAssignUnderwriter.Items.Clear();
				if(Convert.ToInt32(this.cmbLOB.SelectedIndex)>=0)
				{	
					dsRecieve =new DataSet();
					intLobId = Convert.ToInt32(cmbLOB.SelectedItem.Value);
                    dsRecieve = objAgency.PopulateAgency_MrkUW(intAgencyId, intLobId, "AUW");
                    if (dsRecieve != null && dsRecieve.Tables.Count > 0)
                    {
                        this.lbAssignUnderwriter.DataSource = dsRecieve.Tables[0];
                        this.lbAssignUnderwriter.DataValueField = dsRecieve.Tables[0].Columns[0].ToString();
                        this.lbAssignUnderwriter.DataTextField = dsRecieve.Tables[0].Columns[1].ToString();
                        this.lbAssignUnderwriter.DataBind();
                        //this.lbAssignUnderwriter.Items.Insert(0,">>...Select Underwriter");
                       // if (this.lbAssignUnderwriter.Items.Count > 0)
                       //     this.lbAssignUnderwriter.SelectedIndex = 0;
                      //  rfvlbAssignUnderwriter.Enabled = true;                        
                        btnSave.Attributes.Add("style", "display:inline");

                        DataSet dsunderwriter = new DataSet();
                        string AssignUDW="";
                        dsunderwriter = objAgency.PopulateAgency_MrkUW(intAgencyId, intLobId, "POL");
                        if (dsunderwriter != null && dsunderwriter.Tables.Count > 0)
                        {
                            foreach (DataRow dr in dsunderwriter.Tables[0].Rows)
                            {
                                if (dr["UNDERWRITER"] != null)
                                {
                                    AssignUDW = AssignUDW + dr["UNDERWRITER"]+",";
                                }
                            }


                            
                        }
                        if (AssignUDW != "")
                        {
                            AssignUDW = AssignUDW.Substring(0, AssignUDW.Length-1);
                        }
                        hidunassgnuw.Value = AssignUDW; 
                                           
                    }
                   // else
                    //{
                   //     rfvlbAssignUnderwriter.Enabled = false;
                  //  }
                   		
				}
							
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}
  

		private void btnSave_Click(object sender, System.EventArgs e)
		{          
			try
			{
				objAgency =new Cms.BusinessLayer.BlCommon.ClsAgency();
				
				intLobId = Convert.ToInt32(cmbLOB.SelectedItem.Value);
				char[] arrDelimeter={','};
				string [] arrUnderwriterId ;
				string [] arrMarketeerId;
				arrUnderwriterId = hidUnderwriterID.Value.ToString().Split(',');
				arrMarketeerId   = hidMarketeerID.Value.ToString().Split(',');
				int i;
				string intUnderwriter="";
				for (i=0;i<arrUnderwriterId.Length;i++)
				{
					if (intUnderwriter == "")
					{
						intUnderwriter = arrUnderwriterId[i].ToString();
					}
					else
					{
						intUnderwriter = intUnderwriter  + "," +arrUnderwriterId[i].ToString();
					}
				}




			
				string strMarketeer ="";
				for (i=0;i<arrMarketeerId.Length;i++)
				{
					if (strMarketeer == "")
					{
						strMarketeer = arrMarketeerId[i].ToString();
					}
					else
					{
						strMarketeer = strMarketeer  + "," +arrMarketeerId[i].ToString();
					}
				}
                   
                int intResult;
                intResult = objAgency.Save(intLobId,intUnderwriter,intAgencyId,strMarketeer,hidUnderwriterNames.Value,hidMarketeerNames.Value,cmbLOB.SelectedItem.Text);	
	
				if(intResult >= 1)
				{
					this.lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"1");
				}
				else
				{
					this.lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"2");
				}
				this.lblMessage.Visible=true;

				PopulateAssignedUnderwriter();
				PopulateUnassignedUnderwriter();
				PopulateAssignedMarketeer();
				PopulateUnassignedMarketeer();
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		}

		private void cmbLOB_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if(cmbLOB.SelectedIndex<1) 
				{
					ShowHideControls(false);
				}
				else
				{				
					ShowHideControls(true);
					PopulateAssignedUnderwriter();
					PopulateUnassignedUnderwriter();
				}
			}
			catch(Exception objExcep)
			{
				Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(objExcep);
			}
		
		}

		private void ShowHideControls(bool flag)
		{
			lbUnAssignUnderwriter.Visible = flag;
			lbAssignUnderwriter.Visible	= 	flag;
			capUnassignUnderwriter.Visible = flag;
			capAssignedUnderwriter.Visible = flag;
			AssignUnderwriters.Visible =flag;
			UnAssignUnderwriters.Visible = flag;
			//Marketeer
			lbUnAssignMarketeer.Visible = flag;
			lbAssignMarketeer.Visible	= 	flag;
			capUnassignMarketeer.Visible = flag;
			capAssignedMarketeer.Visible = flag;
			AssignMarketeers.Visible =flag;
			UnAssignMarketeers.Visible = flag;
			
			btnSave.Visible = flag;
		}

        

    
       

       
          
		

		
	}
}
