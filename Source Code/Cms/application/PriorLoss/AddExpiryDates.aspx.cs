/******************************************************************************************
	<Author					: - > Anurag Verma
 	<Start Date				: -	> 28/04/2005    
	<End Date				: - > 
	<Description			: - > This is code behind file for adding and updating expiry date information
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - 25/08/2005
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
using System.Reflection;
using System.Resources;  
using Cms.Model.Application.PriorLoss;  
using Cms.BusinessLayer.BlApplication;  
using Cms.CmsWeb; 
using Cms.CmsWeb.Controls;
using Cms.ExceptionPublisher; 
using Cms.BusinessLayer.BlCommon;  

namespace Cms.Application.PriorLoss
{
	/// <summary>
	/// This class is used to give functionality to Expiry Date module
	/// </summary>
	public class AddExpiryDates : Cms.Application.appbase  
	{
        protected System.Web.UI.WebControls.Label lblMessage;
        protected System.Web.UI.WebControls.Label capEXPDT_LOB;
        protected System.Web.UI.WebControls.DropDownList cmbEXPDT_LOB;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPDT_LOB;
        protected System.Web.UI.WebControls.Label capEXPDT_CARR;
        protected System.Web.UI.WebControls.TextBox txtEXPDT_CARR;
        protected System.Web.UI.WebControls.Label capEXPDT_DATE;
        protected System.Web.UI.WebControls.TextBox txtEXPDT_DATE;
        protected System.Web.UI.WebControls.Label capEXPDT_PREM;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPDT_PREM;
        protected System.Web.UI.WebControls.Label capEXPDT_CONT_DATE;
        protected System.Web.UI.WebControls.TextBox txtEXPDT_CONT_DATE;
        protected System.Web.UI.WebControls.Label capEXPDT_CSR;
        protected System.Web.UI.WebControls.DropDownList cmbEXPDT_CSR;
        protected System.Web.UI.WebControls.Label capEXPDT_PROD;
        protected System.Web.UI.WebControls.DropDownList cmbEXPDT_PROD;
        protected System.Web.UI.WebControls.Label capEXPDT_NOTES;
        protected System.Web.UI.WebControls.TextBox txtEXPDT_NOTES;
        protected System.Web.UI.WebControls.TextBox txtEXPDT_PREM;
        
        protected System.Web.UI.WebControls.Label capPOLICY_NUMBER;
        protected System.Web.UI.WebControls.TextBox txtPOLICY_NUMBER;  
        
        protected Cms.CmsWeb.Controls.CmsButton btnReset;
        protected Cms.CmsWeb.Controls.CmsButton btnActivateDeactivate;
        protected Cms.CmsWeb.Controls.CmsButton btnSave;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidFormSaved;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidIS_ACTIVE;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidOldData;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidCUSTOMER_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidEXPDT_ID;
        
    


        protected System.Web.UI.HtmlControls.HtmlInputHidden hidhidCUSTOMER_ID;
        protected System.Web.UI.HtmlControls.HtmlInputHidden hidhidEXPDT_ID;
        
        protected System.Web.UI.WebControls.HyperLink hlkEXPDT_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPDT_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEXPDT_DATE;
        protected System.Web.UI.WebControls.HyperLink hlkEXPDT_CONT_DATE;
        protected System.Web.UI.WebControls.RequiredFieldValidator rfvEXPDT_CONT_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEXPDT_CONT_DATE;
        protected System.Web.UI.WebControls.RegularExpressionValidator revEXPDT_PREM;
        protected System.Web.UI.WebControls.CustomValidator csvEXPDT_NOTES;

        public string primaryKeyValues="";
        


        #region Local form variables
        //START:*********** Local form variables *************
        string oldXML;
        //creating resource manager object (used for reading field and label mapping)
        System.Resources.ResourceManager objResourceMgr;
        private string strRowId, strFormSaved;
        //private int	intLoggedInUserID;
        protected System.Web.UI.WebControls.TextBox reEXPDT_PREM;
        
        //Defining the business layer class object
        ClsExpiryDates  objAddExpiryDates ;
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
            rfvEXPDT_LOB.ErrorMessage			        = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"1");
            rfvEXPDT_DATE.ErrorMessage			        = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"2");
           // rfvEXPDT_CSR.ErrorMessage			        = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"4");
           // rfvEXPDT_PROD.ErrorMessage			        = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"3");
            revEXPDT_DATE.ErrorMessage                  = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5"); 
            revEXPDT_CONT_DATE.ErrorMessage             = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"5"); 

            revEXPDT_DATE.ValidationExpression          = aRegExpDate;
            revEXPDT_CONT_DATE.ValidationExpression     = aRegExpDate; 

            csvEXPDT_NOTES.ErrorMessage                 = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId,"6"); 
            revEXPDT_PREM.ValidationExpression          = aRegExpCurrencyformat;  
            revEXPDT_PREM.ErrorMessage                  = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("117");   
          }
        #endregion

        #region PageLoad event
        private void Page_Load(object sender, System.EventArgs e)
        {
            btnReset.Attributes.Add("onclick","javascript:return ResetForm('" + Page.Controls[0].ID + "' );");
            txtEXPDT_PREM.Attributes.Add("onBlur","this.value=formatCurrency(this.value);"); 
            cmbEXPDT_CSR.Attributes.Add("onFocus","SelectComboIndex('cmbEXPDT_CSR');");  
            cmbEXPDT_LOB.Attributes.Add("onFocus","SelectComboIndex('cmbEXPDT_LOB');");     
            cmbEXPDT_PROD.Attributes.Add("onFocus","SelectComboIndex('cmbEXPDT_PROD');");     
            

            // phone and extension control names: txtPHONE.Attributes.Add("OnBlur","Javascript:DisableExt('txtPHONE','txtEXT');");
            base.ScreenId="119_0";
            lblMessage.Visible = false;
            SetErrorMessages();

            #region code for calendar picker
                hlkEXPDT_DATE.Attributes.Add("OnClick","fPopCalendar(document.APP_EXPIRY_DATES.txtEXPDT_DATE,document.APP_EXPIRY_DATES.txtEXPDT_DATE)"); //Javascript Implementation for Calender				
                hlkEXPDT_CONT_DATE.Attributes.Add("OnClick","fPopCalendar(document.APP_EXPIRY_DATES.txtEXPDT_CONT_DATE,document.APP_EXPIRY_DATES.txtEXPDT_CONT_DATE)"); //Javascript Implementation for Calender				
            #endregion

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass	=	  CmsButtonType.Write;
            btnReset.PermissionString		=	gstrSecurityXML;

            btnActivateDeactivate.CmsButtonClass	=	CmsButtonType.Write;
            btnActivateDeactivate.PermissionString		=	gstrSecurityXML;

            btnSave.CmsButtonClass	=	CmsButtonType.Write;
            btnSave.PermissionString		=	gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Application.PriorLoss.AddExpiryDates" ,System.Reflection.Assembly.GetExecutingAssembly());
            if(!Page.IsPostBack)
            {
                PopulateHiddenFields();
                SetCaptions();
                FillCombo();
                if(Request.QueryString["EXPDT_ID"]!=null)
                    GenerateXML(Request.QueryString["EXPDT_ID"].ToString());
            }
        }//end pageload
        #endregion

        /// <summary>
        /// Populating combo box from lookup tables 
        /// </summary>
        private void FillCombo()
        {
            DataTable dtLob = Cms.CmsWeb.ClsFetcher.LOBs;
			//cmbEXPDT_LOB.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("LOBCD");
            cmbEXPDT_LOB.DataSource=dtLob.DefaultView;
			cmbEXPDT_LOB.DataTextField="Lob_Desc"; 
            cmbEXPDT_LOB.DataValueField="Lob_ID"; 
            cmbEXPDT_LOB.DataBind(); 

            DataTable dt=new DataTable();        
            //if(hidAPP_ID.Value!="0")
                dt=ClsUser.GetAgencyUsers(int.Parse(hidCUSTOMER_ID.Value),"Y");    
            //else
            //    dt=ClsUser.GetAgencyUsers(int.Parse(GetCustomerID()));    

            cmbEXPDT_CSR.DataSource=dt; 
            cmbEXPDT_CSR.DataTextField=dt.Columns[1].ToString() ;
            cmbEXPDT_CSR.DataValueField= dt.Columns[0].ToString() ;
            cmbEXPDT_CSR.DataBind(); 
			cmbEXPDT_CSR.Items.Insert(0,"");

            DataTable dtProd=ClsUser.GetAllProducers();
            cmbEXPDT_PROD.DataSource =dtProd;     
            cmbEXPDT_PROD.DataTextField=dtProd.Columns[1].ToString() ;  
            cmbEXPDT_PROD.DataValueField=dtProd.Columns[0].ToString() ;
            cmbEXPDT_PROD.DataBind(); 
			cmbEXPDT_PROD.Items.Insert(0,"");
        }


        /// <summary>
        /// fetching data based on query string values
        /// </summary>
        private void GenerateXML(string expdID)
        {
            string strEXPDTId=expdID;
                //Request.QueryString["EXPDT_ID"].ToString();
            objAddExpiryDates = new  ClsExpiryDates();
  
            if(strEXPDTId!="" && strEXPDTId!=null)
            {
                try
                {
                    hidOldData.Value=objAddExpiryDates.FetchData(int.Parse(strEXPDTId),int.Parse(hidCUSTOMER_ID.Value));
//                    hidFormSaved.Value="0"; 
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
                    if(objAddExpiryDates!= null)
                        objAddExpiryDates.Dispose();
                }  
                
            }
                
        }


        /// <summary>
        /// populating hidden fields
        /// </summary>
        private void PopulateHiddenFields()
        {
            hidCUSTOMER_ID.Value=GetCustomerID();            
        }
    
        
        #region GetFormValue
        /// <summary>
        /// Fetch form's value and stores into model class object and return that object.
        /// </summary>
        private ClsExpiryDatesInfo GetFormValue()
        {
            string expDate=txtEXPDT_DATE.Text; 
            string contDate=txtEXPDT_CONT_DATE.Text; 
            


            //Creating the Model object for holding the New data
            ClsExpiryDatesInfo objExpiryDatesModel;
            objExpiryDatesModel = new ClsExpiryDatesInfo();

            objExpiryDatesModel.EXPDT_LOB=	cmbEXPDT_LOB.SelectedValue =="" ? 0 : int.Parse(cmbEXPDT_LOB.SelectedValue) ;
            objExpiryDatesModel.EXPDT_CARR=	txtEXPDT_CARR.Text;

            if(expDate!="")
                objExpiryDatesModel.EXPDT_DATE=	Convert.ToDateTime(expDate);
            else
                objExpiryDatesModel.EXPDT_DATE=	Convert.ToDateTime("1/1/1900");

            objExpiryDatesModel.EXPDT_PREM=	txtEXPDT_PREM.Text==""?0.0 : double.Parse(txtEXPDT_PREM.Text);

            if(contDate!="")
                objExpiryDatesModel.EXPDT_CONT_DATE=Convert.ToDateTime(contDate);
            else
                 objExpiryDatesModel.EXPDT_CONT_DATE=Convert.ToDateTime("1/1/1900");    

             objExpiryDatesModel.EXPDT_CSR=	cmbEXPDT_CSR.SelectedValue==""? 0: int.Parse(cmbEXPDT_CSR.SelectedValue); 
            objExpiryDatesModel.EXPDT_PROD=	cmbEXPDT_PROD.SelectedValue==""?0:int.Parse(cmbEXPDT_PROD.SelectedValue); ;
            objExpiryDatesModel.EXPDT_NOTES=	txtEXPDT_NOTES.Text;
            objExpiryDatesModel.POLICY_NUMBER=	txtPOLICY_NUMBER.Text;
        
            objExpiryDatesModel.CUSTOMER_ID = hidCUSTOMER_ID.Value=="" ? 0 : int.Parse(hidCUSTOMER_ID.Value);
        

            //These  assignments are common to all pages.
            strFormSaved	=	hidFormSaved.Value;
            strRowId		=	hidEXPDT_ID.Value;
            oldXML		= hidOldData.Value;
            //Returning the model object

            return objExpiryDatesModel;
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
			this.btnActivateDeactivate.Click += new System.EventHandler(this.btnActivateDeactivate_Click);
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
                objAddExpiryDates = new  ClsExpiryDates();

                //Retreiving the form values into model class object
                ClsExpiryDatesInfo objExpiryDatesModel = GetFormValue();

          

                if(strRowId.ToUpper().Equals("NEW")) //save case
                {
                    objExpiryDatesModel.CREATED_BY = int.Parse(GetUserId());
                    objExpiryDatesModel.CREATED_DATETIME = DateTime.Now;
                    objExpiryDatesModel.MODIFIED_BY=int.Parse(GetUserId());
                    objExpiryDatesModel.IS_ACTIVE = "Y";
                    objExpiryDatesModel.LAST_UPDATED_DATETIME = DateTime.Now; 

                    //Calling the add method of business layer class
                    intRetVal = objAddExpiryDates.Add(objExpiryDatesModel);

                    if(intRetVal>0)
                    {
                        hidEXPDT_ID.Value = objExpiryDatesModel.EXPDT_ID.ToString();
                        primaryKeyValues=hidEXPDT_ID.Value + "^" + hidCUSTOMER_ID.Value; 
                        lblMessage.Text			=	ClsMessages.GetMessage(base.ScreenId,"29");
                        hidFormSaved.Value			=	"1";
                        hidIS_ACTIVE.Value = "Y";
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
                    ClsExpiryDatesInfo objOldExpiryDatesModel;
                    objOldExpiryDatesModel = new ClsExpiryDatesInfo();

                    //Setting  the Old Page details(XML File containing old details) into the Model Object
                    base.PopulateModelObject(objOldExpiryDatesModel,hidOldData.Value);

                    //Setting those values into the Model object which are not in the page
                    objExpiryDatesModel.EXPDT_ID = int.Parse(strRowId);
                    objExpiryDatesModel.MODIFIED_BY = int.Parse(GetUserId());
                    objExpiryDatesModel.LAST_UPDATED_DATETIME = DateTime.Now;                   
                    

                    //Updating the record using business layer class object
                    intRetVal	= objAddExpiryDates.Update(objOldExpiryDatesModel,objExpiryDatesModel);
                    if( intRetVal > 0 )			// update successfully performed
                    {
                        primaryKeyValues=hidEXPDT_ID.Value + "^" + hidCUSTOMER_ID.Value;
                        lblMessage.Text			=	Cms.CmsWeb.ClsMessages.GetMessage(ScreenId,"31");
                        hidFormSaved.Value		=	"1";
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
                GenerateXML(hidEXPDT_ID.Value);
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
                if(objAddExpiryDates!= null)
                    objAddExpiryDates.Dispose();
            }
        }

        /// <summary>
        /// Activates and deactivates  .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActivateDeactivate_Click(object sender, System.EventArgs e)
        {
           
            try
            {
                Cms.BusinessLayer.BlCommon.stuTransactionInfo  objStuTransactionInfo = new  Cms.BusinessLayer.BlCommon.stuTransactionInfo ();
                objStuTransactionInfo.loggedInUserId = int.Parse(GetUserId());
                objStuTransactionInfo.loggedInUserName = GetUserName();
                objAddExpiryDates =  new ClsExpiryDates();

                if(hidIS_ACTIVE.Value.ToString().ToUpper().Equals("Y"))
                {
                    objStuTransactionInfo.transactionDescription = "Deactivated Succesfully.";
                    objAddExpiryDates.TransactionInfoParams = objStuTransactionInfo;
                    objAddExpiryDates.ActivateDeactivate(hidEXPDT_ID.Value,"N");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"41");
                    hidIS_ACTIVE.Value="N";
                }
                else
                {
                    objStuTransactionInfo.transactionDescription = "Activated Succesfully.";
                    objAddExpiryDates.TransactionInfoParams = objStuTransactionInfo;
                    objAddExpiryDates.ActivateDeactivate(hidEXPDT_ID.Value,"Y");
                    lblMessage.Text = ClsMessages.GetMessage(base.ScreenId,"40");
                    hidIS_ACTIVE.Value="Y";
                }
                GenerateXML(hidEXPDT_ID.Value);
                hidFormSaved.Value			=	"1";
            }
            catch(Exception ex)
            {
                lblMessage.Text	=	ClsMessages.GetMessage(base.ScreenId,"21")+ " - " + ex.Message + " Try again!";
                lblMessage.Visible	=	true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
            }
            finally
            {
                lblMessage.Visible = true;
                if(objAddExpiryDates!= null)
                    objAddExpiryDates.Dispose();
            }
        }
        #endregion
        
        /// <summary>
        /// setting captions of the label fields
        /// </summary>
        private void SetCaptions()
        {
            capEXPDT_LOB.Text						=		objResourceMgr.GetString("cmbEXPDT_LOB");
            capEXPDT_CARR.Text						=		objResourceMgr.GetString("txtEXPDT_CARR");
            capEXPDT_DATE.Text						=		objResourceMgr.GetString("txtEXPDT_DATE");
            capEXPDT_PREM.Text						=		objResourceMgr.GetString("txtEXPDT_PREM");
            capEXPDT_CONT_DATE.Text						=		objResourceMgr.GetString("txtEXPDT_CONT_DATE");
            capEXPDT_CSR.Text						=		objResourceMgr.GetString("cmbEXPDT_CSR");
            capEXPDT_PROD.Text						=		objResourceMgr.GetString("cmbEXPDT_PROD");
            capEXPDT_NOTES.Text						=		objResourceMgr.GetString("txtEXPDT_NOTES");
            capPOLICY_NUMBER.Text						=		objResourceMgr.GetString("txtPOLICY_NUMBER");
            
        }
    }
}
