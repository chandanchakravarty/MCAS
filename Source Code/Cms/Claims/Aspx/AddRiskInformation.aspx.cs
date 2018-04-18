using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cms.Model.Claims;
using Cms.CmsWeb.Controls;
using Cms.CmsWeb;
using Cms.ExceptionPublisher;
using Cms.BusinessLayer.BLClaims;
using Cms.BusinessLayer.BlCommon;
using System.Data;
using System.Globalization;


namespace Cms.Claims.Aspx
{
    public partial class AddRiskInformation : Cms.Claims.ClaimBase
    {

      

        ClsRiskInformation objRiskInformation = new ClsRiskInformation();
       
        System.Resources.ResourceManager objResourceMgr;
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AddRiskInformation)); 		
            base.ScreenId = "306_12";
            lblMessage.Visible = false;

            //START:*********** Setting permissions and class (Read/write/execute/delete)  *************
            btnReset.CmsButtonClass = CmsButtonType.Write;
            btnReset.PermissionString = gstrSecurityXML;

            btnSave.CmsButtonClass = CmsButtonType.Write;
            btnSave.PermissionString = gstrSecurityXML;

            //END:*********** Setting permissions and class (Read/write/execute/delete)  *************
            objResourceMgr = new System.Resources.ResourceManager("Cms.Claims.Aspx.AddRiskInformation", System.Reflection.Assembly.GetExecutingAssembly());
          

            if (!Page.IsPostBack)
            {
                
                InitializePage();               

            }

           
        }

        private void InitializePage()
        {

            
            hlkEFFECTIVE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtEFFECTIVE_DATE,txtEFFECTIVE_DATE)");
            hlkEXPIRE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtEXPIRE_DATE,txtEXPIRE_DATE)");
            hlkVOYAGE_DEPARTURE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtVOYAGE_DEPARTURE_DATE,txtVOYAGE_DEPARTURE_DATE)");
            hlkVOYAGE_ARRIVAL_DATE.Attributes.Add("OnClick", "fPopCalendar(txtVOYAGE_ARRIVAL_DATE,txtVOYAGE_ARRIVAL_DATE)");
            hlkVOYAGE_SURVEY_DATE.Attributes.Add("OnClick", "fPopCalendar(txtVOYAGE_SURVEY_DATE,txtVOYAGE_SURVEY_DATE)");
            hlkPERSON_DOB.Attributes.Add("OnClick", "fPopCalendar(txtPERSON_DOB,txtPERSON_DOB)");
            hlkPERSON_DISEASE_DATE.Attributes.Add("OnClick", "fPopCalendar(txtPERSON_DISEASE_DATE,txtPERSON_DISEASE_DATE)");
            hlkPA_START_DATE.Attributes.Add("OnClick", "fPopCalendar(txtPA_START_DATE,txtPA_START_DATE)");
            hlkPA_END_DATE.Attributes.Add("OnClick", "fPopCalendar(txtPA_END_DATE,txtPA_END_DATE)");


            btnReset.Attributes.Add("onclick", "javascript:return ResetTheForm();");
            
            SetCaptions();
            SetErrorMessages();
            GetQueryStringValues();
            ManageScreenControls();            
            GetOldDataObject();


        }
        private void ManageScreenControls()
        {
            if (!string.IsNullOrEmpty(hidLOB_ID.Value))
            {
                string LobID = hidLOB_ID.Value;
                if (LobID == ((int)enumLOB.ARPERIL).ToString() || LobID == ((int)enumLOB.ERISK).ToString() || LobID == ((int)enumLOB.GLBANK).ToString() || LobID == ((int)enumLOB.COMPCONDO).ToString() || LobID == ((int)enumLOB.COMPCOMPY).ToString() || LobID == ((int)enumLOB.DWELLING).ToString() || LobID == ((int)enumLOB.GENCVLLIB).ToString() || LobID == ((int)enumLOB.DRISK).ToString() || LobID == ((int)enumLOB.ROBBERY).ToString() || LobID == ((int)enumLOB.TFIRE).ToString() || LobID == ((int)enumLOB.JDLGR).ToString() || LobID == ((int)enumLOB.HOME).ToString())
                {
                    rfvPOL_PERSON_ID.Enabled = false;               
                    rfvVehicle_ID.Enabled = false;
                    rfvPOL_VOYAGE_ID.Enabled = false;
                    rfvPOL_VESSEL_ID.Enabled = false;
                    rfvDPVAT.Enabled = false;
                    rfvRURAL_LIEN.Enabled = false;
                    rfvPERSONAL_ACCIDENT.Enabled = false;

                    TblLocation.Visible = true;



                    rfvPOL_PERSON_ID.Enabled = false;
                    rfvVehicle_ID.Enabled = false;
                    rfvPOL_VOYAGE_ID.Enabled = false;
                    rfvPOL_VESSEL_ID.Enabled = false;
                    rfvDPVAT.Enabled = false;
                    rfvRURAL_LIEN.Enabled = false;
                    rfvPERSONAL_ACCIDENT.Enabled = false;

                    TblLocation.Visible = true;

                    //Added by santosh kumar gautam itrack:851(see comment)
                    if (LobID == ((int)enumLOB.ERISK).ToString() || LobID == ((int)enumLOB.GLBANK).ToString() || LobID == ((int)enumLOB.GENCVLLIB).ToString() || LobID == ((int)enumLOB.DRISK).ToString() || LobID == ((int)enumLOB.ROBBERY).ToString() || LobID == ((int)enumLOB.HOME).ToString())
                    {
                        txtACTUAL_INSURED_OBJECT.Visible = true;
                        capACTUAL_INSURED_OBJECT.Visible = true;
                        txtLOCATION_ITEM_NUMBER.Visible = true;
                        capLOCATION_ITEM_NUMBER.Visible = true;
                        revLOCATION_ITEM_NUMBER.Enabled = true;
                    }
                    else
                    {
                        txtACTUAL_INSURED_OBJECT.Visible = false;
                        capACTUAL_INSURED_OBJECT.Visible = false;
                        txtLOCATION_ITEM_NUMBER.Visible = false;
                        capLOCATION_ITEM_NUMBER.Visible = false;
                        revLOCATION_ITEM_NUMBER.Enabled = false;
                    }

                 
                }

                if (LobID == ((int)enumLOB.INDPA).ToString() || LobID == ((int)enumLOB.CPCACC).ToString() || LobID == ((int)enumLOB.MRTG).ToString() || LobID == ((int)enumLOB.GRPLF).ToString())
                {
                    rfvPOL_LOCATION_ID.Enabled = false;
                    rfvVehicle_ID.Enabled = false;
                    rfvPOL_VOYAGE_ID.Enabled = false;
                    rfvPOL_VESSEL_ID.Enabled = false;
                    rfvDPVAT.Enabled = false;
                    rfvRURAL_LIEN.Enabled = false;
                    rfvPERSONAL_ACCIDENT.Enabled = false;
                  
                    TblPerson.Visible = true;
                   //capHEADING.Text = objResourceMgr.GetString("lblPERSON_HEADING");
                }

                if (LobID == ((int)enumLOB.MTIME).ToString())
                {
                    rfvPOL_LOCATION_ID.Enabled = false;
                    rfvVehicle_ID.Enabled = false;
                    rfvPOL_VOYAGE_ID.Enabled = false;
                    rfvPOL_PERSON_ID.Enabled = false;
                    rfvDPVAT.Enabled = false;
                    rfvRURAL_LIEN.Enabled = false;
                    rfvPERSONAL_ACCIDENT.Enabled = false;

                    TblVessel.Visible = true;
                    //capHEADING.Text = objResourceMgr.GetString("lblVESSEL_HEADING");
                }

                if (LobID == ((int)enumLOB.CVLIABTR).ToString() || LobID == ((int)enumLOB.FACLIAB).ToString() || LobID == ((int)enumLOB.AERO).ToString() || LobID == ((int)enumLOB.MTOR).ToString() || LobID == ((int)enumLOB.CTCL).ToString() || LobID == ((int)enumLOB.MOT).ToString())
                {

                    rfvPOL_LOCATION_ID.Enabled = false;
                    rfvPOL_VESSEL_ID.Enabled = false;
                    rfvPOL_VOYAGE_ID.Enabled = false;
                    rfvPOL_PERSON_ID.Enabled = false;
                    rfvDPVAT.Enabled = false;
                    rfvRURAL_LIEN.Enabled = false;
                    rfvPERSONAL_ACCIDENT.Enabled = false;

                    TblVehicle.Visible = true;
                    //capHEADING.Text = objResourceMgr.GetString("lblVEHICLE_HEADING");
                }               

                if (LobID == ((int)enumLOB.NATNTR).ToString() || LobID == ((int)enumLOB.INTERN).ToString())
                {

                    rfvPOL_PERSON_ID.Enabled = false;
                    rfvVehicle_ID.Enabled = false;
                    rfvPOL_LOCATION_ID.Enabled = false;
                    rfvPOL_VESSEL_ID.Enabled = false;
                    rfvDPVAT.Enabled = false;
                    rfvRURAL_LIEN.Enabled = false;
                    rfvPERSONAL_ACCIDENT.Enabled = false;

                    TblVoyage.Visible = true;
                  // capHEADING.Text = objResourceMgr.GetString("lblVOYAGE_HEADING");
                }

                if (LobID == ((int)enumLOB.DPVA).ToString() || LobID == ((int)enumLOB.DPVAT2).ToString())
                {
                    rfvPOL_VOYAGE_ID.Enabled = false;
                    rfvPOL_PERSON_ID.Enabled = false;
                    rfvVehicle_ID.Enabled = false;
                    rfvPOL_LOCATION_ID.Enabled = false;
                    rfvPOL_VESSEL_ID.Enabled = false;                   
                    rfvRURAL_LIEN.Enabled = false;
                    rfvPERSONAL_ACCIDENT.Enabled = false;

                    TblDpvat.Visible = true;

                    // NEW SECTION NEED TO INTRODUCE
                   
                }

                if (LobID == ((int)enumLOB.RLLE).ToString() )
                {
                    rfvPOL_VOYAGE_ID.Enabled = false;
                    rfvPOL_PERSON_ID.Enabled = false;
                    rfvVehicle_ID.Enabled = false;
                    rfvPOL_LOCATION_ID.Enabled = false;
                    rfvPOL_VESSEL_ID.Enabled = false; 
                    rfvDPVAT.Enabled = false;                    
                    rfvPERSONAL_ACCIDENT.Enabled = false;

                    TblRuralLien.Visible = true;

                   

                }

                if (LobID == ((int)enumLOB.PAPEACC).ToString())
                {

                    rfvPOL_VOYAGE_ID.Enabled = false;
                    rfvPOL_PERSON_ID.Enabled = false;
                    rfvVehicle_ID.Enabled = false;
                    rfvPOL_LOCATION_ID.Enabled = false;
                    rfvPOL_VESSEL_ID.Enabled = false;
                    rfvDPVAT.Enabled = false;
                    rfvRURAL_LIEN.Enabled = false;


                    TblPersonalAccident.Visible = true;

                   

                }

                 if (LobID == ((int)enumLOB.RETSURTY).ToString())
                 {

                     rfvPOL_VOYAGE_ID.Enabled = false;
                     rfvPOL_PERSON_ID.Enabled = false;
                     rfvVehicle_ID.Enabled = false;
                     rfvPOL_LOCATION_ID.Enabled = false;
                     rfvPOL_VESSEL_ID.Enabled = false;
                     rfvDPVAT.Enabled = false;
                     rfvRURAL_LIEN.Enabled = false;
                     rfvPERSONAL_ACCIDENT.Enabled = false;                


                     TblRentalSecurity.Visible = true;

                 }
            }
                
        }
   
        private void FillDropdowns()
        {
            DataTable dt;
            
            dt = objRiskInformation.GetRiskTypes(int.Parse(hidLOB_ID.Value), int.Parse(hidCLAIM_ID.Value), int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));


            string LobID = hidLOB_ID.Value;
           
                if (dt != null && dt.Rows.Count > 0)
                {

                    if (LobID == ((int)enumLOB.ARPERIL).ToString() || LobID == ((int)enumLOB.GLBANK).ToString() || LobID == ((int)enumLOB.ERISK).ToString() || LobID == ((int)enumLOB.COMPCONDO).ToString() || LobID == ((int)enumLOB.COMPCOMPY).ToString() || LobID == ((int)enumLOB.DWELLING).ToString() || LobID == ((int)enumLOB.GENCVLLIB).ToString() || LobID == ((int)enumLOB.DRISK).ToString() || LobID == ((int)enumLOB.ROBBERY).ToString() || LobID == ((int)enumLOB.TFIRE).ToString() || LobID == ((int)enumLOB.JDLGR).ToString() || LobID == ((int)enumLOB.HOME).ToString())
                    {
                        cmbPOL_LOCATION_ID.DataSource = dt;
                        cmbPOL_LOCATION_ID.DataTextField = "ITEM_VALUE";
                        cmbPOL_LOCATION_ID.DataValueField = "ITEM_ID";
                        cmbPOL_LOCATION_ID.DataBind();
                        cmbPOL_LOCATION_ID.Items.Insert(0, "");



                        DataTable dtCountry = Cms.CmsWeb.ClsFetcher.AllCountry;
                        cmbLOCATION_COUNTRY.DataSource = dtCountry;
                        cmbLOCATION_COUNTRY.DataTextField = COUNTRY_NAME;
                        cmbLOCATION_COUNTRY.DataValueField = COUNTRY_ID;
                        cmbLOCATION_COUNTRY.DataBind();


                        if (!string.IsNullOrEmpty(hidLOCATION_COUNTRY.Value) && hidLOCATION_COUNTRY.Value != "0")
                            cmbLOCATION_COUNTRY.SelectedValue = hidLOCATION_COUNTRY.Value;
                        else
                            cmbLOCATION_COUNTRY.SelectedValue = Convert.ToString(5); // FOR BRAZIL


                        if (!string.IsNullOrEmpty(cmbLOCATION_COUNTRY.SelectedValue))
                        {
                            FillState(int.Parse(cmbLOCATION_COUNTRY.SelectedValue), ref cmbLOCATION_STATE);

                            if (!string.IsNullOrEmpty(hidLOCATION_STATE.Value) && hidLOCATION_STATE.Value != "0")
                            {
                                if (cmbLOCATION_STATE.Items.FindByValue(hidLOCATION_STATE.Value) != null)
                                    cmbLOCATION_STATE.SelectedValue = hidLOCATION_STATE.Value;
                                else
                                    hidLOCATION_STATE.Value = "";
                            }
                            else
                                hidLOCATION_STATE.Value = cmbLOCATION_STATE.SelectedValue;
                        }
                      


                    }

                    if (LobID == ((int)enumLOB.INDPA).ToString() || LobID == ((int)enumLOB.CPCACC).ToString() || LobID == ((int)enumLOB.MRTG).ToString() || LobID == ((int)enumLOB.GRPLF).ToString())
                    {
                        cmbPOL_PERSON_ID.DataSource = dt;
                        cmbPOL_PERSON_ID.DataTextField = "ITEM_VALUE";
                        cmbPOL_PERSON_ID.DataValueField = "ITEM_ID";
                        cmbPOL_PERSON_ID.DataBind();
                        cmbPOL_PERSON_ID.Items.Insert(0, "");
                    }

                    if (LobID == ((int)enumLOB.MTIME).ToString())
                    {
                        cmbPOL_VESSEL_ID.DataSource = dt;
                        cmbPOL_VESSEL_ID.DataTextField = "ITEM_VALUE";
                        cmbPOL_VESSEL_ID.DataValueField = "ITEM_ID";
                        cmbPOL_VESSEL_ID.DataBind();
                        cmbPOL_VESSEL_ID.Items.Insert(0, "");
                    }

                    if (LobID == ((int)enumLOB.CVLIABTR).ToString() || LobID == ((int)enumLOB.FACLIAB).ToString() || LobID == ((int)enumLOB.AERO).ToString() || LobID == ((int)enumLOB.MTOR).ToString() || LobID == ((int)enumLOB.CTCL).ToString() || LobID == ((int)enumLOB.MOT).ToString())
                    {
                        cmbPOL_VEHICLE_ID.DataSource = dt;
                        cmbPOL_VEHICLE_ID.DataTextField = "ITEM_VALUE";
                        cmbPOL_VEHICLE_ID.DataValueField = "ITEM_ID";
                        cmbPOL_VEHICLE_ID.DataBind();
                        cmbPOL_VEHICLE_ID.Items.Insert(0, "");
                        if (Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("DAMTYP").Select("", "LookupDesc").Length > 0)
                        {
                            cmbVEHICLE_DAMAGE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("DAMTYP").Select("", "LookupDesc").CopyToDataTable<DataRow>();
                        }
                        else
                        {
                            cmbVEHICLE_DAMAGE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookupTable("DAMTYP");
                        }
                        //cmbVEHICLE_DAMAGE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DAMTYP");
                        cmbVEHICLE_DAMAGE_TYPE.DataValueField = "LookupID";
                        cmbVEHICLE_DAMAGE_TYPE.DataTextField = "LookupDesc";
                        cmbVEHICLE_DAMAGE_TYPE.DataBind();
                        cmbVEHICLE_DAMAGE_TYPE.Items.Insert(0, "");
                        cmbVEHICLE_DAMAGE_TYPE.SelectedIndex = 0;
                    }
                   

                    if (LobID == ((int)enumLOB.NATNTR).ToString() || LobID == ((int)enumLOB.INTERN).ToString())
                    {
                        ClsStates objStates = new ClsStates();

                        cmbPOL_VOYAGE_ID.DataSource = dt;
                        cmbPOL_VOYAGE_ID.DataTextField = "ITEM_VALUE";
                        cmbPOL_VOYAGE_ID.DataValueField = "ITEM_ID";
                        cmbPOL_VOYAGE_ID.DataBind();
                        cmbPOL_VOYAGE_ID.Items.Insert(0, "");

                        cmbVOYAGE_CONVEYENCE_TYPE.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("COVTPE");
                        cmbVOYAGE_CONVEYENCE_TYPE.DataTextField = "LookupDesc";
                        cmbVOYAGE_CONVEYENCE_TYPE.DataValueField = "LookupID";
                        cmbVOYAGE_CONVEYENCE_TYPE.DataBind();
                        cmbVOYAGE_CONVEYENCE_TYPE.Items.Insert(0, "");

                        //DataTable dtCountry = Cms.CmsWeb.ClsFetcher.AllCountry;
                        //cmbVOYAGE_DESTINATION_COUNTRY.DataSource = dtCountry;
                        //cmbVOYAGE_DESTINATION_COUNTRY.DataTextField = COUNTRY_NAME;
                        //cmbVOYAGE_DESTINATION_COUNTRY.DataValueField = COUNTRY_ID;
                        //cmbVOYAGE_DESTINATION_COUNTRY.DataBind();

                        //cmbVOYAGE_ORIGIN_COUNTRY.DataSource = dtCountry;
                        //cmbVOYAGE_ORIGIN_COUNTRY.DataTextField = COUNTRY_NAME;
                        //cmbVOYAGE_ORIGIN_COUNTRY.DataValueField = COUNTRY_ID;
                        //cmbVOYAGE_ORIGIN_COUNTRY.DataBind();

                        //if (!string.IsNullOrEmpty(hidVOYAGE_DESTINATION_COUNTRY.Value) && hidVOYAGE_DESTINATION_COUNTRY.Value != "0")
                        //    cmbVOYAGE_DESTINATION_COUNTRY.SelectedValue = hidVOYAGE_DESTINATION_COUNTRY.Value;
                        //else
                        //    cmbVOYAGE_DESTINATION_COUNTRY.SelectedValue = Convert.ToString(5); // FOR BRAZIL

                        //if (!string.IsNullOrEmpty(hidVOYAGE_ORIGIN_COUNTRY.Value) && hidVOYAGE_ORIGIN_COUNTRY.Value != "0")
                        //    cmbVOYAGE_ORIGIN_COUNTRY.SelectedValue = hidVOYAGE_ORIGIN_COUNTRY.Value;
                        //else
                        //    cmbVOYAGE_ORIGIN_COUNTRY.SelectedValue = Convert.ToString(5); // FOR BRAZIL

                        
                        //if (!string.IsNullOrEmpty(cmbVOYAGE_DESTINATION_COUNTRY.SelectedValue))
                        //{
                        //    FillState(int.Parse(cmbVOYAGE_DESTINATION_COUNTRY.SelectedValue), ref cmbVOYAGE_DESTINATION_STATE);
                           
                        //    // FILL STATE AS PER CONTRY SELECTED
                        //    if (!string.IsNullOrEmpty(hidVOYAGE_DESTINATION_STATE.Value) && hidVOYAGE_DESTINATION_STATE.Value != "0")
                        //        cmbVOYAGE_DESTINATION_STATE.SelectedValue = hidVOYAGE_DESTINATION_STATE.Value;
                        //    else
                        //        hidVOYAGE_DESTINATION_STATE.Value = cmbVOYAGE_DESTINATION_STATE.SelectedValue;

                        //}
                      
                        //if (!string.IsNullOrEmpty(cmbVOYAGE_ORIGIN_COUNTRY.SelectedValue))
                        //{
                        //    FillState(int.Parse(cmbVOYAGE_ORIGIN_COUNTRY.SelectedValue), ref cmbVOYAGE_ORIGIN_STATE);                           

                        //    if (!string.IsNullOrEmpty(hidVOYAGE_ORIGIN_STATE.Value) && hidVOYAGE_ORIGIN_STATE.Value != "0")
                        //        cmbVOYAGE_ORIGIN_STATE.SelectedValue = hidVOYAGE_ORIGIN_STATE.Value;
                        //    else
                        //        hidVOYAGE_ORIGIN_STATE.Value = cmbVOYAGE_ORIGIN_STATE.SelectedValue;

                        //}
                    }

                    if (LobID == ((int)enumLOB.DPVA).ToString() || LobID == ((int)enumLOB.DPVAT2).ToString())
                    {
                        cmbDPVAT.DataSource = dt;
                        cmbDPVAT.DataTextField = "ITEM_VALUE";
                        cmbDPVAT.DataValueField = "ITEM_ID";
                        cmbDPVAT.DataBind();
                        cmbDPVAT.Items.Insert(0, "");

                        cmbDP_CATEGORY.Items.Clear();
                        cmbDP_CATEGORY.DataSource = Cms.BusinessLayer.BlCommon.ClsCommon.GetLookup("DPVCTE");//DPVAT (Cat. 3 e 4) Category
                        cmbDP_CATEGORY.DataTextField = "LookupDesc";
                        cmbDP_CATEGORY.DataValueField = "LookupID";
                        cmbDP_CATEGORY.DataBind();
                        cmbDP_CATEGORY.Items.Insert(0, "");


                        ClsStates objStates = new ClsStates();
                        int COUNTRY_ID = 5;
                        DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);

                        if (dtStates != null && dtStates.Rows.Count > 0)
                        {
                            cmbDP_STATE_ID.Items.Clear();
                            cmbDP_STATE_ID.DataSource = dtStates;
                            cmbDP_STATE_ID.DataTextField = "STATE_NAME";
                            cmbDP_STATE_ID.DataValueField = "STATE_ID";
                            cmbDP_STATE_ID.DataBind();
                            cmbDP_STATE_ID.Items.Insert(0, "");

                        }

                    }
                    if (LobID == ((int)enumLOB.RLLE).ToString())
                    {
                        cmbRURAL_LIEN.DataSource = dt;
                        cmbRURAL_LIEN.DataTextField = "ITEM_VALUE";
                        cmbRURAL_LIEN.DataValueField = "ITEM_ID";
                        cmbRURAL_LIEN.DataBind();
                        cmbRURAL_LIEN.Items.Insert(0, "");

                        cmbRURAL_MODE.Items.Clear();
                        cmbRURAL_MODE.DataSource = ClsCommon.GetLookup("PNMODE");//Penhor Rural Product Mode
                        cmbRURAL_MODE.DataTextField = "LookupDesc";
                        cmbRURAL_MODE.DataValueField = "LookupID";
                        cmbRURAL_MODE.DataBind();
                        cmbRURAL_MODE.Items.Insert(0, "");

                        cmbRURAL_PROPERTY.Items.Clear();
                        cmbRURAL_PROPERTY.DataSource = ClsCommon.GetLookup("PNPRPT");//Penhor Rural Product Property
                        cmbRURAL_PROPERTY.DataTextField = "LookupDesc";
                        cmbRURAL_PROPERTY.DataValueField = "LookupID";
                        cmbRURAL_PROPERTY.DataBind();
                        cmbRURAL_PROPERTY.Items.Insert(0, "");


                        cmbRURAL_CULTIVATION.Items.Clear();
                        cmbRURAL_CULTIVATION.DataSource = ClsCommon.GetLookup("PNCUTI");//Penhor Rural Product Cultivation
                        cmbRURAL_CULTIVATION.DataTextField = "LookupDesc";
                        cmbRURAL_CULTIVATION.DataValueField = "LookupID";
                        cmbRURAL_CULTIVATION.DataBind();
                        cmbRURAL_CULTIVATION.Items.Insert(0, "");


                        ClsStates objStates = new ClsStates();
                        int COUNTRY_ID = 5;// for brasil
                        DataTable dtStates = objStates.GetStatesForCountry(COUNTRY_ID);

                        if (dtStates != null && dtStates.Rows.Count > 0)
                        {
                            cmbRURAL_STATE_ID.Items.Clear();
                            cmbRURAL_STATE_ID.DataSource = dtStates;
                            cmbRURAL_STATE_ID.DataTextField = "STATE_NAME";
                            cmbRURAL_STATE_ID.DataValueField = "STATE_ID";
                            cmbRURAL_STATE_ID.DataBind();
                            cmbRURAL_STATE_ID.Items.Insert(0, "");

                            cmbRURAL_SUBSIDY_STATE.Items.Clear();
                            cmbRURAL_SUBSIDY_STATE.DataSource = dtStates;
                            cmbRURAL_SUBSIDY_STATE.DataTextField = "STATE_NAME";
                            cmbRURAL_SUBSIDY_STATE.DataValueField = "STATE_ID";
                            cmbRURAL_SUBSIDY_STATE.DataBind();
                            cmbRURAL_SUBSIDY_STATE.Items.Insert(0, "");

                        }

                    }
                    if (LobID == ((int)enumLOB.PAPEACC).ToString())
                    {
                        cmbPERSONAL_ACCIDENT.DataSource = dt;
                        cmbPERSONAL_ACCIDENT.DataTextField = "ITEM_VALUE";
                        cmbPERSONAL_ACCIDENT.DataValueField = "ITEM_ID";
                        cmbPERSONAL_ACCIDENT.DataBind();
                        cmbPERSONAL_ACCIDENT.Items.Insert(0, "");

                    }

                    if (LobID == ((int)enumLOB.RETSURTY).ToString())
                    {
                        cmbRENTAL_SECURITY.DataSource = dt;
                        cmbRENTAL_SECURITY.DataTextField = "ITEM_VALUE";
                        cmbRENTAL_SECURITY.DataValueField = "ITEM_ID";
                        cmbRENTAL_SECURITY.DataBind();
                        cmbRENTAL_SECURITY.Items.Insert(0, "");
                    }

                    dt.Clear();
                }
               
        }

        [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        public DataSet AjaxFillState(string CountryID)
        {
            try
            {
                CmsWeb.webservices.ClsWebServiceCommon obj = new CmsWeb.webservices.ClsWebServiceCommon();
                string result = "";
                result = obj.FillState(CountryID);
                DataSet ds = new DataSet();
                ds.ReadXml(new System.IO.StringReader(result));
               
                return ds;
            }
            catch (Exception ex)
            {
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                return null;
            }
        }


        private void SetCaptions()
        {
            capVEHICLE_INSURED_PLEADED_GUILTY.Text = objResourceMgr.GetString("chkVEHICLE_INSURED_PLEADED_GUILTY");
            capPOL_LOCATION_ID.Text = objResourceMgr.GetString("cmbPOL_LOCATION_ID");
            capLOCATION_COUNTRY.Text = objResourceMgr.GetString("cmbLOCATION_COUNTRY");
            capLOCATION_STATE.Text = objResourceMgr.GetString("cmbLOCATION_STATE");
            capPOL_PERSON_ID.Text = objResourceMgr.GetString("cmbPOL_PERSON_ID");
            capPOL_VEHICLE_ID.Text = objResourceMgr.GetString("cmbPOL_VEHICLE_ID");
            capPOL_VESSEL_ID.Text = objResourceMgr.GetString("cmbPOL_VESSEL_ID");
            capPOL_VOYAGE_ID.Text = objResourceMgr.GetString("cmbPOL_VOYAGE_ID");
            capVOYAGE_DESTINATION_COUNTRY.Text = objResourceMgr.GetString("cmbVOYAGE_DESTINATION_COUNTRY");
            capVOYAGE_DESTINATION_STATE.Text = objResourceMgr.GetString("cmbVOYAGE_DESTINATION_STATE");
            capVOYAGE_ORIGIN_COUNTRY.Text = objResourceMgr.GetString("cmbVOYAGE_ORIGIN_COUNTRY");
            capVOYAGE_ORIGIN_STATE.Text = objResourceMgr.GetString("cmbVOYAGE_ORIGIN_STATE");
            capDAMAGE_DESCRIPTION.Text = objResourceMgr.GetString("txtDAMAGE_DESCRIPTION");
            capEFFECTIVE_DATE.Text = objResourceMgr.GetString("txtEFFECTIVE_DATE");
            capEXPIRE_DATE.Text = objResourceMgr.GetString("txtEXPIRE_DATE");
            capLOCATION_ADDRESS.Text = objResourceMgr.GetString("txtLOCATION_ADDRESS");
            capLOCATION_COMPLIMENT.Text = objResourceMgr.GetString("txtLOCATION_COMPLIMENT");
            capLOCATION_DISTRICT.Text = objResourceMgr.GetString("txtLOCATION_DISTRICT");
            capLOCATION_ZIPCODE.Text = objResourceMgr.GetString("txtLOCATION_ZIPCODE");
            capINSURED_NAME.Text = objResourceMgr.GetString("txtINSURED_NAME");
            capVEHICLE_MAKER.Text = objResourceMgr.GetString("txtVEHICLE_MAKER");
            capVEHICLE_MODEL.Text = objResourceMgr.GetString("txtVEHICLE_MODEL");
            capVEHICLE_YEAR.Text = objResourceMgr.GetString("txtVEHICLE_YEAR");
            capVESSEL_MANUFACTURED_YEAR.Text = objResourceMgr.GetString("txtVESSEL_MANUFACTURED_YEAR");
            capVESSEL_MANUFACTURER.Text = objResourceMgr.GetString("txtVESSEL_MANUFACTURER");
            capVESSEL_NAME.Text = objResourceMgr.GetString("txtVESSEL_NAME");
            capVESSEL_TYPE.Text = objResourceMgr.GetString("txtVESSEL_TYPE");
            capVEHICLE_VIN.Text = objResourceMgr.GetString("txtVEHICLE_VIN");
            capVOYAGE_CONVEYENCE_TYPE.Text = objResourceMgr.GetString("cmbVOYAGE_CONVEYENCE_TYPE");
            capVOYAGE_DEPARTURE_DATE.Text = objResourceMgr.GetString("txtVOYAGE_DEPARTURE_DATE");
            capVOYAGE_DESTINATION_CITY.Text = objResourceMgr.GetString("txtVOYAGE_DESTINATION_CITY");
            capVOYAGE_ORIGIN_CITY.Text = objResourceMgr.GetString("txtVOYAGE_ORIGIN_CITY");

            capVEHICLE_LIC_PT_NUMBER.Text = objResourceMgr.GetString("txtVEHICLE_LICENSE_PLATE_NUMBER");
            capVEHICLE_DAMAGE_TYPE.Text = objResourceMgr.GetString("cmbVEHICLE_DAMAGE_TYPE");
            capPERSON_DOB.Text = objResourceMgr.GetString("txtPERSON_DATE_OF_BIRTH");
            capVOYAGE_CERT_NUMBER.Text = objResourceMgr.GetString("txtVOYAGE_CERTIFICATE_NUMBER");
            capVOYAGE_LIC_PT_NUMBER.Text = objResourceMgr.GetString("txtVOYAGE_LICENSE_PLATE");
            capVOYAGE_PREFIX.Text = objResourceMgr.GetString("txtVOYAGE_PREFIX");
            capVESSEL_NUMBER.Text = objResourceMgr.GetString("txtVESSEL_NUMBER");
            capVOYAGE_TRAN_COMPANY.Text = objResourceMgr.GetString("txtVOYAGE_TRANSPORTATION_COMPANY");
            capVOYAGE_IO_DESC.Text = objResourceMgr.GetString("txtVOYAGE_INSURED_OBJECT_DESCRIPTION");
            capVOYAGE_ARRIVAL_DATE.Text = objResourceMgr.GetString("txtVOYAGE_ARRIVAL_DATE");
            capVOYAGE_SURVEY_DATE.Text = objResourceMgr.GetString("txtVOYAGE_SURVEY_DATE");
            capPERSON_DISEASE_DATE.Text = objResourceMgr.GetString("txtPERSON_DISEASE_DATE");

            capDP_CATEGORY.Text = objResourceMgr.GetString("cmbDP_CATEGORY");
            capDP_STATE_ID.Text = objResourceMgr.GetString("cmbDP_STATE_ID");
            capDP_TICKET_NUMBER.Text = objResourceMgr.GetString("txtDP_TICKET_NUMBER");
            capDPVAT.Text = objResourceMgr.GetString("cmbDPVAT");

            capPA_END_DATE.Text = objResourceMgr.GetString("txtPA_END_DATE");
            capPA_START_DATE.Text = objResourceMgr.GetString("txtPA_START_DATE");
            capPA_NUM_OF_PASS.Text = objResourceMgr.GetString("txtPA_NUM_OF_PASS");
            capPERSONAL_ACCIDENT.Text = objResourceMgr.GetString("cmbPERSONAL_ACCIDENT");

            capRURAL_CITY.Text = objResourceMgr.GetString("txtRURAL_CITY");
            capRURAL_STATE_ID.Text = objResourceMgr.GetString("cmbRURAL_STATE_ID");
            capRURAL_SUBSIDY_STATE.Text = objResourceMgr.GetString("cmbRURAL_SUBSIDY_STATE");
            capRURAL_SUBSIDY_PREMIUM.Text = objResourceMgr.GetString("txtRURAL_SUBSIDY_PREMIUM");
            capRURAL_CULTIVATION.Text = objResourceMgr.GetString("cmbRURAL_CULTIVATION");
            capRURAL_FESR_COVERAGE.Text = objResourceMgr.GetString("chkRURAL_FESR_COVERAGE");
            capRURAL_INSURED_AREA.Text = objResourceMgr.GetString("txtRURAL_INSURED_AREA");
            capRURAL_ITEM_NUMBER.Text = objResourceMgr.GetString("txtRURAL_ITEM_NUMBER");
            capRURAL_MODE.Text = objResourceMgr.GetString("cmbRURAL_MODE");
            capRURAL_PROPERTY.Text = objResourceMgr.GetString("cmbRURAL_PROPERTY");
            capRURAL_LIEN.Text = objResourceMgr.GetString("cmbRURAL_LIEN");

            capLOCATION_ITEM_NUMBER.Text = objResourceMgr.GetString("txtLOCATION_ITEM_NUMBER");
            capACTUAL_INSURED_OBJECT.Text = objResourceMgr.GetString("txtACTUAL_INSURED_OBJECT");

            capRENTAL_SECURITY.Text = objResourceMgr.GetString("cmbRENTAL_SECURITY");
            capRENTAL_INSURED_OBJECT.Text = objResourceMgr.GetString("txtRENTAL_INSURED_OBJECT");
            capITEM_NUMBER.Text = objResourceMgr.GetString("txtITEM_NUMBER");


            lblRequiredFieldsInformation.Text = objResourceMgr.GetString("lblRequiredFieldsInformation");


            hidFUTURE_DATE_MSG.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "40");
            hidPOLICY_DATE_MSG.Value = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "41");
            capYES.Text = objResourceMgr.GetString("capYES");
            
        }

        private void GetQueryStringValues()
        {        

            if (Request.QueryString["CLAIM_ID"] != null && Request.QueryString["CLAIM_ID"].ToString() != "")
                hidCLAIM_ID.Value = Request.QueryString["CLAIM_ID"].ToString();

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                hidCUSTOMER_ID.Value = Request.QueryString["CUSTOMER_ID"].ToString();

            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                hidPOLICY_ID.Value = Request.QueryString["POLICY_ID"].ToString();

            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                hidPOLICY_VERSION_ID.Value = Request.QueryString["POLICY_VERSION_ID"].ToString();

            if (Request.QueryString["LOB_ID"] != null && Request.QueryString["LOB_ID"].ToString() != "")
                hidLOB_ID.Value = Request.QueryString["LOB_ID"].ToString();

            if (Request.QueryString["POLICY_EFFECTIVE_DATE"] != null && Request.QueryString["POLICY_EFFECTIVE_DATE"].ToString() != "")
                hidPOLICY_EFFECTIVE_DATE.Value = Request.QueryString["POLICY_EFFECTIVE_DATE"].ToString();

            if (Request.QueryString["POLICY_EXPIRATION_DATE"] != null && Request.QueryString["POLICY_EXPIRATION_DATE"].ToString() != "")
                hidPOLICY_EXPIRATION_DATE.Value = Request.QueryString["POLICY_EXPIRATION_DATE"].ToString();

        }

        private void SetErrorMessages()
        {
            revZIP_CODE.ValidationExpression = aRegExpZip;
            revEFFECTIVE_DATE.ValidationExpression = aRegExpDate;
            revEXPIRE_DATE.ValidationExpression = aRegExpDate;

            revPERSON_DOB.ValidationExpression = aRegExpDate;
            revPERSON_DISEASE_DATE.ValidationExpression = aRegExpDate;
            revVOYAGE_ARRIVAL_DATE.ValidationExpression = aRegExpDate;
            revVOYAGE_SURVEY_DATE.ValidationExpression = aRegExpDate;

            revVOYAGE_DEPARTURE_DATE.ValidationExpression = aRegExpDate;


            revLOCATION_ITEM_NUMBER.ValidationExpression = aRegExpInteger;
            revLOCATION_ITEM_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("492");

            rfvPOL_PERSON_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "9");
            rfvVehicle_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "10");
            rfvPOL_VOYAGE_ID.ErrorMessage =ClsMessages.GetMessage(base.ScreenId, "8");
            rfvPOL_VESSEL_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "7");
            rfvPOL_LOCATION_ID.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "6");
            csvVESSEL_MANUFACTURED_YEAR.ErrorMessage =ClsMessages.GetMessage(base.ScreenId, "5");
            csvVEHICLE_YEAR.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "5");

            csvPERSON_DiSEASE_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "12");
            csvRISK_EXPIRE_DATE.ErrorMessage = ClsMessages.GetMessage(base.ScreenId, "11");
            revEXPIRE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            revZIP_CODE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("1084");
            revVOYAGE_DEPARTURE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            revEFFECTIVE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");

            revPERSON_DOB.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            revPERSON_DISEASE_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            revVOYAGE_ARRIVAL_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");
            revVOYAGE_SURVEY_DATE.ErrorMessage = ClsMessages.FetchGeneralMessage("22");

            // RURAL LIEN
            revRURAL_ITEM_NUMBER.ValidationExpression = aRegExpInteger;
            revRURAL_ITEM_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");
            rfvRURAL_ITEM_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "13");

            cvRURAL_FESR_COVERAGE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "14");

            rfvRURAL_MODE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "15");

            rfvRURAL_PROPERTY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "16");
            rfvRURAL_CULTIVATION.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "17");
            rfvRURAL_STATE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "19");

            revRURAL_CITY.ValidationExpression = aRegExpTextArea255;
            revRURAL_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "23");
            rfvRURAL_CITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "18");

            revRURAL_INSURED_AREA.ValidationExpression = aRegExpInteger;
            revRURAL_INSURED_AREA.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "24");
            rfvRURAL_INSURED_AREA.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "20");

            revRURAL_SUBSIDY_PREMIUM.ValidationExpression = aRegExpCurrencyformat;
            revRURAL_SUBSIDY_PREMIUM.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "21");

            // DPVAT
            revDP_TICKET_NUMBER.ValidationExpression = aRegExpInteger;
            revDP_TICKET_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "26");
            rfvDP_TICKET_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "25");
            rfvDP_CATEGORY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "27");
            rfvDP_STATE_ID.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "28");

            //Personal Accident for Passengers
            revPA_START_DATE.ValidationExpression = aRegExpDate;
            revPA_START_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "30");
            rfvPA_START_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "29");

            revPA_END_DATE.ValidationExpression = aRegExpDate;
            revPA_END_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "32");
            rfvPA_END_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "31");

            revPA_NUM_OF_PASS.ValidationExpression = aRegExpIntegerPositiveNonZero;
            revPA_NUM_OF_PASS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "34");
            rfvPA_NUM_OF_PASS.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "33");
            cpvPA_END_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.FetchGeneralMessage("733");

            rfvPERSONAL_ACCIDENT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "37");
            rfvDPVAT.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "35");
            rfvRURAL_LIEN.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "36");

            rfvRENTAL_SECURITY.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "39");
            revITEM_NUMBER.ValidationExpression = aRegExpInteger;
            revITEM_NUMBER.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "22");

            csvDISEASE_DATE.ErrorMessage = Cms.CmsWeb.ClsMessages.GetMessage(base.ScreenId, "40");

        }

        private void GetOldDataObject()
        {
            ClsRiskInfo objRiskInfo = new ClsRiskInfo();

            // FILL STATE AS PER THE COUNTRY BECAUSE STATE IS FILL BY USING AJAX WHICH IS CLEAR ON THE POSTBACK          

            objRiskInfo.CLAIM_ID.CurrentValue = int.Parse(hidCLAIM_ID.Value);
            DataTable dt = objRiskInformation.FetchData(ref objRiskInfo);

            if (dt != null && dt.Rows.Count > 0)
            {

                //---------------------------------------------------------------------
                // DISABLE FIELDS THOSE ARE COPIED FROM POLICY SIDE (ITRACK : 959)
                //---------------------------------------------------------------------

                // ARPERIL,GLBANK,ERISK,COMPCONDO,COMPCOMPY,DWELLING,GENCVLLIB,DRISK,ROBBERY,TFIRE,JDLGR

                cmbLOCATION_COUNTRY.Enabled = false;
                cmbLOCATION_STATE.Enabled = false;
                txtACTUAL_INSURED_OBJECT.ReadOnly = true;
                txtLOCATION_ITEM_NUMBER.ReadOnly = true;

                txtLOCATION_ADDRESS.ReadOnly = true;
                txtLOCATION_COMPLIMENT.ReadOnly = true;
                txtLOCATION_DISTRICT.ReadOnly = true;
                txtLOCATION_ZIPCODE.ReadOnly = true;

                //INDPA CPCACC, MRTG GRPLF
                txtINSURED_NAME.ReadOnly = true;
                txtPERSON_DOB.ReadOnly = true;
                //------------- ITrack Issue - 971 by Shikha --------------
                txtEFFECTIVE_DATE.ReadOnly = true;
                txtEFFECTIVE_DATE.Enabled = false;
                txtEXPIRE_DATE.ReadOnly = true;
                txtEXPIRE_DATE.Enabled = false;

                //MTIME
                txtVESSEL_NAME.ReadOnly = true;
                txtVESSEL_TYPE.ReadOnly = true;
                txtVESSEL_MANUFACTURED_YEAR.ReadOnly = true;
                txtVESSEL_MANUFACTURER.ReadOnly = true;
                txtVESSEL_NUMBER.ReadOnly = true;

                //  CVLIABTR,FACLIAB,AERO,MTOR,CTCL

                txtVEHICLE_VIN.ReadOnly = true;
                txtVEHICLE_YEAR.ReadOnly = true;
                txtVEHICLE_MAKER.ReadOnly = true;
                txtVEHICLE_MODEL.ReadOnly = true;
                txtVEHICLE_LIC_PT_NUMBER.ReadOnly = true;

                // NATNTR,enumLOB.INTERN

                cmbVOYAGE_CONVEYENCE_TYPE.Enabled = false;
                txtVOYAGE_DEPARTURE_DATE.ReadOnly = true;
                txtVOYAGE_DESTINATION_CITY.ReadOnly = true;
                txtVOYAGE_DESTINATION_COUNTRY.Enabled = false;
                txtVOYAGE_DESTINATION_STATE.Enabled = false;
                txtVOYAGE_ORIGIN_CITY.ReadOnly = true;
                txtVOYAGE_ORIGIN_STATE.Enabled = false;
                txtVOYAGE_ORIGIN_COUNTRY.Enabled = false;

                //DPVA,DPVAT2
                cmbDP_CATEGORY.Enabled = false;
                cmbDP_STATE_ID.Enabled = false;
                txtDP_TICKET_NUMBER.ReadOnly = true;

                //PAPEACC
                txtPA_START_DATE.ReadOnly = true;
                txtPA_END_DATE.ReadOnly = true;
                txtPA_NUM_OF_PASS.ReadOnly = true;
                //RLLE

                chkRURAL_FESR_COVERAGE.Enabled = false;
                cmbRURAL_CULTIVATION.Enabled = false;
                cmbRURAL_MODE.Enabled = false;
                cmbRURAL_PROPERTY.Enabled = false;
                cmbRURAL_STATE_ID.Enabled = false;
                cmbRURAL_SUBSIDY_STATE.Enabled = false;
                txtRURAL_ITEM_NUMBER.ReadOnly = true;
                txtRURAL_CITY.ReadOnly = true;
                txtRURAL_INSURED_AREA.ReadOnly = true;
                txtRURAL_SUBSIDY_PREMIUM.ReadOnly = true;


                //RSECURITY
                txtRENTAL_INSURED_OBJECT.ReadOnly = true;
                txtITEM_NUMBER.ReadOnly = true;
              

                //---------------------------------------------------------------------


                hidINSURED_PRODUCT_ID.Value = dt.Rows[0]["INSURED_PRODUCT_ID"].ToString();
                hidLOCATION_COUNTRY.Value = dt.Rows[0]["COUNTRY1"].ToString();
                txtVOYAGE_DESTINATION_COUNTRY.Text = dt.Rows[0]["COUNTRY2"].ToString();
                txtVOYAGE_ORIGIN_COUNTRY.Text = dt.Rows[0]["COUNTRY1"].ToString();

                hidLOCATION_STATE.Value = dt.Rows[0]["STATE1"].ToString();
                txtVOYAGE_DESTINATION_STATE.Text = dt.Rows[0]["STATE2"].ToString();
                txtVOYAGE_ORIGIN_STATE.Text = dt.Rows[0]["STATE1"].ToString();

                FillDropdowns();
                PopulatePageFromEbixModelObject(this.Page, objRiskInfo);
                base.SetPageModelObject(objRiskInfo);

                if (TblLocation.Visible == true)
                {
                    cmbPOL_LOCATION_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                    if( objRiskInfo.ITEM_NUMBER.CurrentValue>0)
                    txtLOCATION_ITEM_NUMBER.Text = objRiskInfo.ITEM_NUMBER.CurrentValue.ToString();
                }

                else if (TblPerson.Visible == true)
                    cmbPOL_PERSON_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                else if (TblVehicle.Visible == true)
                {
                    cmbPOL_VEHICLE_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();
                    if (objRiskInfo.DAMAGE_TYPE.CurrentValue > 0)
                        cmbVEHICLE_DAMAGE_TYPE.SelectedValue = objRiskInfo.DAMAGE_TYPE.CurrentValue.ToString();

                    txtVEHICLE_LIC_PT_NUMBER.Text = objRiskInfo.LICENCE_PLATE_NUMBER.CurrentValue.ToString();

                    if (objRiskInfo.YEAR.CurrentValue > 0)
                    txtVEHICLE_YEAR.Text = objRiskInfo.YEAR.CurrentValue.ToString();
                }

                else if (TblVessel.Visible == true)
                {
                    cmbPOL_VESSEL_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                    if (objRiskInfo.YEAR.CurrentValue > 0)
                     txtVESSEL_MANUFACTURED_YEAR.Text = objRiskInfo.YEAR.CurrentValue.ToString();

                }
                else if (TblVoyage.Visible == true)
                {
                    cmbPOL_VOYAGE_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();
                    txtVOYAGE_LIC_PT_NUMBER.Text = objRiskInfo.LICENCE_PLATE_NUMBER.CurrentValue.ToString();

                    txtVOYAGE_ORIGIN_CITY.Text = objRiskInfo.CITY1.CurrentValue.ToString();
                    txtVOYAGE_DESTINATION_CITY.Text = objRiskInfo.CITY2.CurrentValue.ToString();

                }

                else if (TblDpvat.Visible == true)
                {
                    cmbDPVAT.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();
                    if (!string.IsNullOrEmpty(objRiskInfo.STATE1.CurrentValue.ToString()))
                        cmbDP_STATE_ID.SelectedValue = objRiskInfo.STATE1.CurrentValue.ToString();
                }

                else if (TblPersonalAccident.Visible == true)
                {
                    cmbPERSONAL_ACCIDENT.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                    string strPA_START_DATE = objRiskInfo.EFFECTIVE_DATE.CurrentValue.ToString();
                    string strPA_END_DATE = objRiskInfo.EFFECTIVE_DATE.CurrentValue.ToString();

                    if (!string.IsNullOrEmpty(strPA_START_DATE))
                        txtPA_START_DATE.Text = Convert.ToDateTime(strPA_START_DATE).ToShortDateString();

                    if (!string.IsNullOrEmpty(strPA_END_DATE))
                        txtPA_END_DATE.Text = Convert.ToDateTime(strPA_END_DATE).ToShortDateString();

                    txtPA_NUM_OF_PASS.Text = objRiskInfo.PA_NUM_OF_PASS.CurrentValue.ToString();



                }

                else if (TblRuralLien.Visible == true)
                {
                    cmbRURAL_LIEN.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                    txtRURAL_CITY.Text = objRiskInfo.CITY1.CurrentValue.ToString();
                    if (objRiskInfo.STATE1.CurrentValue != "0" && objRiskInfo.STATE1.CurrentValue != "")
                        cmbRURAL_STATE_ID.SelectedValue = objRiskInfo.STATE1.CurrentValue.ToString();

                    if (objRiskInfo.STATE2.CurrentValue != "0" && objRiskInfo.STATE2.CurrentValue != "")
                        cmbRURAL_SUBSIDY_STATE.SelectedValue = objRiskInfo.STATE2.CurrentValue.ToString();

                    if (objRiskInfo.ITEM_NUMBER.CurrentValue > 0)
                        txtRURAL_ITEM_NUMBER.Text = objRiskInfo.ITEM_NUMBER.CurrentValue.ToString();



                }

                else if (TblRentalSecurity.Visible == true)
                {
                    cmbRENTAL_SECURITY.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();
                    txtRENTAL_INSURED_OBJECT.Text = objRiskInfo.ACTUAL_INSURED_OBJECT.CurrentValue.ToString();
                    txtITEM_NUMBER.Text = objRiskInfo.ITEM_NUMBER.CurrentValue.ToString();     

                }



                cmbPOL_LOCATION_ID.Enabled = false;
                cmbPOL_PERSON_ID.Enabled = false;
                cmbPOL_VEHICLE_ID.Enabled = false;
                cmbPOL_VESSEL_ID.Enabled = false;
                cmbPOL_VOYAGE_ID.Enabled = false;
                cmbDPVAT.Enabled = false;
                cmbPERSONAL_ACCIDENT.Enabled = false;
                cmbRURAL_LIEN.Enabled = false;
                cmbRENTAL_SECURITY.Enabled = false;
               // btnReset.Enabled = false;
            }
            else
            {
                FillDropdowns();
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            int intRetval;
            
            ClsRiskInfo objRiskInfo;
            try
            {
                //For new item to add
                if (hidINSURED_PRODUCT_ID.Value=="0")
                {

                    objRiskInfo = new ClsRiskInfo();
                    this.getFormValues(objRiskInfo);

                    objRiskInfo.CLAIM_ID.CurrentValue = int.Parse( hidCLAIM_ID.Value);//int.Parse(ViewStateClaimID);

                    objRiskInfo.CUSTOMER_ID.CurrentValue =int.Parse( hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                    objRiskInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                    objRiskInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                    objRiskInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objRiskInfo.CREATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());
                    objRiskInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;


                    intRetval = objRiskInformation.AddRiskInformation(objRiskInfo);

                    if (intRetval > 0)
                    {
                        
                        this.GetOldDataObject();
                        //hidPeril_Id.Value = objNamedPerilsinfo.PERIL_ID.CurrentValue.ToString();
                        //btnActivateDeactivate.Enabled = true;
                        //btnDelete.Enabled = true;
                       lblMessage.Text = ClsMessages.FetchGeneralMessage("29");

                       if (TblLocation.Visible == true)
                           cmbPOL_LOCATION_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                       else if (TblPerson.Visible == true)
                           cmbPOL_PERSON_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                       else if (TblVehicle.Visible == true)
                           cmbPOL_VEHICLE_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                       else if (TblVessel.Visible == true)
                           cmbPOL_VESSEL_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                       else if (TblVoyage.Visible == true)
                           cmbPOL_VOYAGE_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                       else if (TblDpvat.Visible == true)
                           cmbDPVAT.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                       else if (TblPersonalAccident.Visible == true)
                           cmbPERSONAL_ACCIDENT.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                       else if (TblRuralLien.Visible == true)
                           cmbRURAL_LIEN.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                        //hidFormSaved.Value = "1";
                       // base.OpenEndorsementDetails();

                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                        //hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                       // hidFormSaved.Value = "2";
                    }
                    //lblDelete.Visible = false;
                    lblMessage.Visible = true;
                } 
                else //For The Update cse
                {
                    
                    objRiskInfo = (ClsRiskInfo)base.GetPageModelObject();
                    this.getFormValues(objRiskInfo);
                    objRiskInfo.IS_ACTIVE.CurrentValue = "Y"; //hidIS_ACTIVE.Value;

                    objRiskInfo.CUSTOMER_ID.CurrentValue = int.Parse(hidCUSTOMER_ID.Value); //int.Parse(GetCustomerID());
                    objRiskInfo.POLICY_ID.CurrentValue = int.Parse(hidPOLICY_ID.Value);                  // int.Parse(GetPolicyID());
                    objRiskInfo.POLICY_VERSION_ID.CurrentValue = int.Parse(hidPOLICY_VERSION_ID.Value);               //int.Parse(GetPolicyVersionID());

                    objRiskInfo.CREATED_BY.CurrentValue = int.Parse(GetUserId());
                    objRiskInfo.MODIFIED_BY.CurrentValue = int.Parse(GetUserId());
                    objRiskInfo.LAST_UPDATED_DATETIME.CurrentValue = ConvertToDate(DateTime.Now.ToString());

                    intRetval = objRiskInformation.UpdateRiskInformation(objRiskInfo);

                    if (intRetval > 0)
                    {
                        this.GetOldDataObject();                      
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("31");

                        if (TblLocation.Visible == true)
                            cmbPOL_LOCATION_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                        else if (TblPerson.Visible == true)
                            cmbPOL_PERSON_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                        else if (TblVehicle.Visible == true)
                            cmbPOL_VEHICLE_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                        else if (TblVessel.Visible == true)
                            cmbPOL_VESSEL_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                        else if (TblVoyage.Visible == true)
                            cmbPOL_VOYAGE_ID.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                        else if (TblDpvat.Visible == true)
                            cmbDPVAT.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                        else if (TblPersonalAccident.Visible == true)
                            cmbPERSONAL_ACCIDENT.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();

                        else if (TblRuralLien.Visible == true)
                            cmbRURAL_LIEN.SelectedValue = objRiskInfo.POL_RISK_ID.CurrentValue.ToString();


                        //hidFormSaved.Value = "1";
                      //  base.OpenEndorsementDetails();

                    }
                    else if (intRetval == -1)
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("18");
                      //  hidFormSaved.Value = "2";
                    }
                    else if (intRetval == -4)
                    {
                        lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "38");
                        //  hidFormSaved.Value = "2";
                    }

                    else
                    {
                        lblMessage.Text = ClsMessages.FetchGeneralMessage("20");
                        //hidFormSaved.Value = "2";
                    }
                    //lblDelete.Visible = false;
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ClsMessages.GetMessage(base.ScreenId, "21") + " - " + ex.Message + " Try again!";
                lblMessage.Visible = true;
                Cms.ExceptionPublisher.ExceptionManagement.ExceptionManager.Publish(ex);
                //hidFormSaved.Value = "2";

            }
            finally
            {
                //if (objNamedPerilsinfo != null)
                //    objNamedPerilsinfo.Dispose();
            }
        }

       
        private void getFormValues(ClsRiskInfo objRiskInfo)
        {
            int NumValue=0;          

            try
            {
                // DAMAGE DESC IS COMMON TO ALL 
                if (!string.IsNullOrEmpty(txtDAMAGE_DESCRIPTION.Text))
                     objRiskInfo.DAMAGE_DESCRIPTION.CurrentValue = txtDAMAGE_DESCRIPTION.Text.Trim();

                if (!string.IsNullOrEmpty(hidRISK_CO_APP_ID.Value) && hidRISK_CO_APP_ID.Value != "0")
                    objRiskInfo.RISK_CO_APP_ID.CurrentValue = int.Parse(hidRISK_CO_APP_ID.Value);

                if(TblVehicle.Visible)
                {
                    if(cmbPOL_VEHICLE_ID.SelectedIndex>0)
                    {
                        objRiskInfo.POL_RISK_ID.CurrentValue = int.Parse(cmbPOL_VEHICLE_ID.SelectedValue);
                        
                         if(int.TryParse(txtVEHICLE_YEAR.Text.Trim(), out NumValue))
                         {                             
                            objRiskInfo.YEAR.CurrentValue=NumValue;
                         }
                        else
                             objRiskInfo.YEAR.CurrentValue = 0;


                         objRiskInfo.VEHICLE_VIN.CurrentValue = txtVEHICLE_VIN.Text.Trim();
                         objRiskInfo.VEHICLE_MAKER.CurrentValue = txtVEHICLE_MAKER.Text.Trim();
                         objRiskInfo.VEHICLE_MODEL.CurrentValue=txtVEHICLE_MODEL.Text.Trim();
                         objRiskInfo.LICENCE_PLATE_NUMBER.CurrentValue = txtVEHICLE_LIC_PT_NUMBER.Text.Trim();
                         if (!string.IsNullOrEmpty(cmbVEHICLE_DAMAGE_TYPE.SelectedValue))
                             objRiskInfo.DAMAGE_TYPE.CurrentValue = int.Parse(cmbVEHICLE_DAMAGE_TYPE.SelectedValue);
                         else
                             objRiskInfo.DAMAGE_TYPE.CurrentValue = 0;


                        if(chkVEHICLE_INSURED_PLEADED_GUILTY.Checked==true)
                           objRiskInfo.VEHICLE_INSURED_PLEADED_GUILTY.CurrentValue="Y";
                        else
                           objRiskInfo.VEHICLE_INSURED_PLEADED_GUILTY.CurrentValue="N";

                    }
                }
                else if(TblLocation.Visible)
                {

                    if(cmbPOL_LOCATION_ID.SelectedIndex>0)
                    {
                        objRiskInfo.POL_RISK_ID.CurrentValue = int.Parse(cmbPOL_LOCATION_ID.SelectedValue);

                       
                           objRiskInfo.LOCATION_ADDRESS.CurrentValue=txtLOCATION_ADDRESS.Text.Trim();
                           objRiskInfo.LOCATION_COMPLIMENT.CurrentValue=txtLOCATION_COMPLIMENT.Text.Trim();
                           objRiskInfo.LOCATION_DISTRICT.CurrentValue=txtLOCATION_DISTRICT.Text.Trim();
                           objRiskInfo.LOCATION_ZIPCODE.CurrentValue=txtLOCATION_ZIPCODE.Text.Trim();

                           if (!string.IsNullOrEmpty(cmbLOCATION_COUNTRY.SelectedValue))
                           {
                               objRiskInfo.COUNTRY1.CurrentValue = cmbLOCATION_COUNTRY.SelectedValue;

                               //if (!string.IsNullOrEmpty(cmbLOCATION_STATE.SelectedValue))
                               //    objRiskInfo.LOCATION_STATE.CurrentValue = int.Parse(cmbLOCATION_STATE.SelectedValue);
                               if (!string.IsNullOrEmpty(hidLOCATION_STATE.Value) && hidLOCATION_STATE.Value != "0")
                                   objRiskInfo.STATE1.CurrentValue = hidLOCATION_STATE.Value;
                           }
                           else
                           {
                               objRiskInfo.COUNTRY1.CurrentValue = "0";
                               objRiskInfo.STATE1.CurrentValue = "0";
                           }

                         if (!string.IsNullOrEmpty(txtLOCATION_ITEM_NUMBER.Text))
                             objRiskInfo.ITEM_NUMBER.CurrentValue = int.Parse(txtLOCATION_ITEM_NUMBER.Text.Trim());
                         else
                             objRiskInfo.ITEM_NUMBER.CurrentValue = 0;

                         
                         objRiskInfo.ACTUAL_INSURED_OBJECT.CurrentValue = txtACTUAL_INSURED_OBJECT.Text.Trim();
                        

                      }
                }
                else if(TblVessel.Visible)
                {
                    if(cmbPOL_VESSEL_ID.SelectedIndex>0)
                    {
                        objRiskInfo.POL_RISK_ID.CurrentValue = int.Parse(cmbPOL_VESSEL_ID.SelectedValue);

                      
                           objRiskInfo.VESSEL_MANUFACTURER.CurrentValue=txtVESSEL_MANUFACTURER.Text.Trim();                       
                           objRiskInfo.VESSEL_NAME.CurrentValue=txtVESSEL_NAME.Text.Trim();
                           objRiskInfo.VESSEL_TYPE.CurrentValue=txtVESSEL_TYPE.Text.Trim();                        
                           objRiskInfo.VESSEL_NUMBER.CurrentValue = txtVESSEL_NUMBER.Text.Trim();
                           if(int.TryParse(txtVESSEL_MANUFACTURED_YEAR.Text.Trim(), out NumValue))
                           {                             
                            objRiskInfo.YEAR.CurrentValue=NumValue;
                           }
                          else
                            objRiskInfo.YEAR.CurrentValue = 0;

                      }
                }             
                

                else if(TblVoyage.Visible)
                {
                   if(cmbPOL_VOYAGE_ID.SelectedIndex>0)
                    {
                        objRiskInfo.POL_RISK_ID.CurrentValue = int.Parse(cmbPOL_VOYAGE_ID.SelectedValue);

                        if(cmbVOYAGE_CONVEYENCE_TYPE.SelectedValue!="")
                            objRiskInfo.VOYAGE_CONVEYENCE_TYPE.CurrentValue = cmbVOYAGE_CONVEYENCE_TYPE.SelectedValue;

                          
                           objRiskInfo.CITY1.CurrentValue=txtVOYAGE_ORIGIN_CITY.Text.Trim();


                         
                              objRiskInfo.CITY2.CurrentValue = txtVOYAGE_DESTINATION_CITY.Text.Trim();
                              objRiskInfo.STATE1.CurrentValue = txtVOYAGE_ORIGIN_STATE.Text.Trim();
                              objRiskInfo.STATE2.CurrentValue = txtVOYAGE_DESTINATION_STATE.Text.Trim();
                              objRiskInfo.COUNTRY2.CurrentValue = txtVOYAGE_DESTINATION_COUNTRY.Text.Trim();
                              objRiskInfo.COUNTRY1.CurrentValue = txtVOYAGE_ORIGIN_COUNTRY.Text.Trim();

                          
                          if (!string.IsNullOrEmpty(txtVOYAGE_DEPARTURE_DATE.Text))
                              objRiskInfo.VOYAGE_DEPARTURE_DATE.CurrentValue = ConvertToDate(txtVOYAGE_DEPARTURE_DATE.Text.Trim());
                          else
                              objRiskInfo.VOYAGE_DEPARTURE_DATE.CurrentValue = DateTime.MinValue;

                         
                         
                              objRiskInfo.VOYAGE_CERT_NUMBER.CurrentValue = txtVOYAGE_CERT_NUMBER.Text.Trim();
                              objRiskInfo.LICENCE_PLATE_NUMBER.CurrentValue = txtVOYAGE_LIC_PT_NUMBER.Text.Trim();                         
                              objRiskInfo.VOYAGE_PREFIX.CurrentValue = txtVOYAGE_PREFIX.Text.Trim();                         
                              objRiskInfo.VOYAGE_TRAN_COMPANY.CurrentValue = txtVOYAGE_TRAN_COMPANY.Text.Trim();

                          
                              objRiskInfo.VOYAGE_IO_DESC.CurrentValue = txtVOYAGE_IO_DESC.Text.Trim();

                              if (!string.IsNullOrEmpty(txtVOYAGE_ARRIVAL_DATE.Text))
                                  objRiskInfo.VOYAGE_ARRIVAL_DATE.CurrentValue = ConvertToDate(txtVOYAGE_ARRIVAL_DATE.Text.Trim());
                              else
                                  objRiskInfo.VOYAGE_ARRIVAL_DATE.CurrentValue = DateTime.MinValue;


                          if (!string.IsNullOrEmpty(txtVOYAGE_SURVEY_DATE.Text))
                              objRiskInfo.VOYAGE_SURVEY_DATE.CurrentValue = ConvertToDate(txtVOYAGE_SURVEY_DATE.Text.Trim());
                          else
                              objRiskInfo.VOYAGE_SURVEY_DATE.CurrentValue = DateTime.MinValue;

                         
                            

                      }
                }
                else if(TblPerson.Visible)
                {
                    if (cmbPOL_PERSON_ID.SelectedIndex > 0)
                    {
                        objRiskInfo.POL_RISK_ID.CurrentValue = int.Parse(cmbPOL_PERSON_ID.SelectedValue);

                        
                          objRiskInfo.INSURED_NAME.CurrentValue = txtINSURED_NAME.Text.Trim();

                        if (!string.IsNullOrEmpty(txtEFFECTIVE_DATE.Text))
                           objRiskInfo.EFFECTIVE_DATE.CurrentValue = ConvertToDate(txtEFFECTIVE_DATE.Text.Trim());
                        else
                            objRiskInfo.EFFECTIVE_DATE.CurrentValue = DateTime.MinValue;

                        if (!string.IsNullOrEmpty(txtEXPIRE_DATE.Text))
                              objRiskInfo.EXPIRE_DATE.CurrentValue = ConvertToDate(txtEXPIRE_DATE.Text.Trim());
                        else
                            objRiskInfo.EXPIRE_DATE.CurrentValue = DateTime.MinValue;


                        if (!string.IsNullOrEmpty(txtPERSON_DOB.Text))
                            objRiskInfo.PERSON_DOB.CurrentValue = ConvertToDate(txtPERSON_DOB.Text.Trim());
                        else
                            objRiskInfo.PERSON_DOB.CurrentValue = DateTime.MinValue;

                        if (!string.IsNullOrEmpty(txtPERSON_DISEASE_DATE.Text))
                            objRiskInfo.PERSON_DISEASE_DATE.CurrentValue = ConvertToDate(txtPERSON_DISEASE_DATE.Text.Trim());
                        else
                            objRiskInfo.PERSON_DISEASE_DATE.CurrentValue = DateTime.MinValue;

                    }

                }

                else if (TblDpvat.Visible)
                {
                    if (cmbDPVAT.SelectedIndex > 0)
                    {
                        objRiskInfo.POL_RISK_ID.CurrentValue = int.Parse(cmbDPVAT.SelectedValue);

                        if (!string.IsNullOrEmpty(cmbDP_CATEGORY.SelectedValue))
                            objRiskInfo.DP_CATEGORY.CurrentValue = int.Parse(cmbDP_CATEGORY.SelectedValue);
                        else
                            objRiskInfo.DP_CATEGORY.CurrentValue = 0;

                        if (!string.IsNullOrEmpty(cmbDP_STATE_ID.SelectedValue))
                            objRiskInfo.STATE1.CurrentValue = cmbDP_STATE_ID.SelectedValue;
                        else
                            objRiskInfo.STATE1.CurrentValue = "0";

                        if(!string.IsNullOrEmpty(txtDP_TICKET_NUMBER.Text ))
                            objRiskInfo.DP_TICKET_NUMBER.CurrentValue = int.Parse(txtDP_TICKET_NUMBER.Text.Trim());
                        else
                            objRiskInfo.DP_TICKET_NUMBER.CurrentValue = 0;
                    }

                }

                else if (TblPersonalAccident.Visible)
                {
                    if (cmbPERSONAL_ACCIDENT.SelectedIndex > 0)
                    {
                        objRiskInfo.POL_RISK_ID.CurrentValue = int.Parse(cmbPERSONAL_ACCIDENT.SelectedValue);
                                              
                       if (!string.IsNullOrEmpty(txtPA_START_DATE.Text))
                          objRiskInfo.EFFECTIVE_DATE.CurrentValue = ConvertToDate(txtPA_START_DATE.Text.Trim());
                       else
                           objRiskInfo.EFFECTIVE_DATE.CurrentValue = DateTime.MinValue;

                       if (!string.IsNullOrEmpty(txtPA_END_DATE.Text))
                           objRiskInfo.EXPIRE_DATE.CurrentValue = ConvertToDate(txtPA_END_DATE.Text.Trim());
                       else
                           objRiskInfo.EXPIRE_DATE.CurrentValue = DateTime.MinValue;

                       if (!string.IsNullOrEmpty(txtPA_NUM_OF_PASS.Text))
                           objRiskInfo.PA_NUM_OF_PASS.CurrentValue = double.Parse(txtPA_NUM_OF_PASS.Text);
                       else
                           objRiskInfo.PA_NUM_OF_PASS.CurrentValue = base.GetEbixDoubleDefaultValue();


                    }

                }

                else if (TblRuralLien.Visible)
                {
                    if (cmbRURAL_LIEN.SelectedIndex > 0)
                    {
                        objRiskInfo.POL_RISK_ID.CurrentValue = int.Parse(cmbRURAL_LIEN.SelectedValue);

                           objRiskInfo.CITY1.CurrentValue = txtRURAL_CITY.Text;
                           if (!string.IsNullOrEmpty(txtRURAL_ITEM_NUMBER.Text))
                               objRiskInfo.ITEM_NUMBER.CurrentValue = int.Parse(txtRURAL_ITEM_NUMBER.Text.Trim());
                           else
                               objRiskInfo.ITEM_NUMBER.CurrentValue = 0;

                           if (!string.IsNullOrEmpty(txtRURAL_INSURED_AREA.Text))
                               objRiskInfo.RURAL_INSURED_AREA.CurrentValue = int.Parse(txtRURAL_INSURED_AREA.Text.Trim());
                           else
                               objRiskInfo.RURAL_INSURED_AREA.CurrentValue = 0;

                        if (!string.IsNullOrEmpty(txtRURAL_SUBSIDY_PREMIUM.Text))
                            objRiskInfo.RURAL_SUBSIDY_PREMIUM.CurrentValue = Convert.ToDouble(txtRURAL_SUBSIDY_PREMIUM.Text.Trim(), numberFormatInfo);
                        else
                            objRiskInfo.RURAL_SUBSIDY_PREMIUM.CurrentValue = base.GetEbixDoubleDefaultValue();
                        
                        if (chkRURAL_FESR_COVERAGE.Checked)
                            objRiskInfo.RURAL_FESR_COVERAGE.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES);// 10963;
                        else
                            objRiskInfo.RURAL_FESR_COVERAGE.CurrentValue = Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.NO);// 10964;


                        if (!string.IsNullOrEmpty(cmbRURAL_CULTIVATION.SelectedValue))
                            objRiskInfo.RURAL_CULTIVATION.CurrentValue = int.Parse(cmbRURAL_CULTIVATION.SelectedValue);
                        else
                            objRiskInfo.RURAL_CULTIVATION.CurrentValue = 0;

                        if (!string.IsNullOrEmpty(cmbRURAL_MODE.SelectedValue))
                            objRiskInfo.RURAL_MODE.CurrentValue = int.Parse(cmbRURAL_MODE.SelectedValue);
                        else
                            objRiskInfo.RURAL_MODE.CurrentValue = 0;

                        if (!string.IsNullOrEmpty(cmbRURAL_PROPERTY.SelectedValue))
                            objRiskInfo.RURAL_PROPERTY.CurrentValue = int.Parse(cmbRURAL_PROPERTY.SelectedValue);
                        else
                            objRiskInfo.RURAL_PROPERTY.CurrentValue = 0;

                        if (!string.IsNullOrEmpty(cmbRURAL_STATE_ID.SelectedValue))
                            objRiskInfo.STATE1.CurrentValue = cmbRURAL_STATE_ID.SelectedValue;
                        else
                            objRiskInfo.STATE1.CurrentValue = "0";
                           

                        if (!string.IsNullOrEmpty(cmbRURAL_SUBSIDY_STATE.SelectedValue))
                            objRiskInfo.STATE2.CurrentValue = cmbRURAL_SUBSIDY_STATE.SelectedValue;
                        else
                            objRiskInfo.STATE2.CurrentValue = "0";

                       
                        if(objRiskInfo.RURAL_FESR_COVERAGE.CurrentValue == Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES))// 10963;
                            chkRURAL_FESR_COVERAGE.Checked=true;
                        else
                            chkRURAL_FESR_COVERAGE.Checked=false;

                    }

                }
                else if (TblRentalSecurity.Visible)
                {
                    if (cmbRENTAL_SECURITY.SelectedIndex > 0)
                    {
                        objRiskInfo.POL_RISK_ID.CurrentValue = int.Parse(cmbRENTAL_SECURITY.SelectedValue);
                        objRiskInfo.ACTUAL_INSURED_OBJECT.CurrentValue = txtRENTAL_INSURED_OBJECT.Text.Trim();
                        objRiskInfo.ITEM_NUMBER.CurrentValue =int.Parse(txtITEM_NUMBER.Text.Trim());
                    }

                }

               
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

        protected void cmbPOL_VEHICLE_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPOL_VEHICLE_ID.SelectedValue))
            {
                GetRiskTypeDetails(int.Parse(cmbPOL_VEHICLE_ID.SelectedValue));
            }
        }

        protected void cmbPOL_VESSEL_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPOL_VESSEL_ID.SelectedValue))
            {
                GetRiskTypeDetails(int.Parse(cmbPOL_VESSEL_ID.SelectedValue));
            }
        }

        protected void cmbPOL_LOCATION_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPOL_LOCATION_ID.SelectedValue))
            {
                GetRiskTypeDetails(int.Parse(cmbPOL_LOCATION_ID.SelectedValue));
            }
        }

        protected void cmbPOL_VOYAGE_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPOL_VOYAGE_ID.SelectedValue))
            {
                GetRiskTypeDetails(int.Parse(cmbPOL_VOYAGE_ID.SelectedValue));
            }
        }

        protected void cmbPOL_PERSON_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPOL_PERSON_ID.SelectedValue))
            {
                GetRiskTypeDetails(int.Parse(cmbPOL_PERSON_ID.SelectedValue));
            }
        }

        protected void cmbRENTAL_SECURITY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbRENTAL_SECURITY.SelectedValue))
            {
                GetRiskTypeDetails(int.Parse(cmbRENTAL_SECURITY.SelectedValue));
            }
        }

        private void FillState(int CountryID, ref DropDownList cmbState)
        {
            ClsStates objStates = new ClsStates();
            DataSet ds = objStates.GetStatesCountry(CountryID);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cmbState.DataSource = ds;
                cmbState.DataTextField = STATE_NAME;
                cmbState.DataValueField = STATE_ID;
                cmbState.DataBind();
            }

        }

      

        private void GetRiskTypeDetails(int RiskID)
        {
          
            DataTable dt;
            string LobID = hidLOB_ID.Value;            

            dt = objRiskInformation.GetRiskTypeDetails(int.Parse(hidCLAIM_ID.Value),int.Parse(LobID), RiskID, int.Parse(hidCUSTOMER_ID.Value), int.Parse(hidPOLICY_ID.Value), int.Parse(hidPOLICY_VERSION_ID.Value));

            if (dt != null && dt.Rows.Count > 0)
            {
                if (LobID == ((int)enumLOB.ARPERIL).ToString() || LobID == ((int)enumLOB.GLBANK).ToString() || LobID == ((int)enumLOB.ERISK).ToString() || LobID == ((int)enumLOB.COMPCONDO).ToString() || LobID == ((int)enumLOB.COMPCOMPY).ToString() || LobID == ((int)enumLOB.DWELLING).ToString() || LobID == ((int)enumLOB.GENCVLLIB).ToString() || LobID == ((int)enumLOB.DRISK).ToString() || LobID == ((int)enumLOB.ROBBERY).ToString() || LobID == ((int)enumLOB.TFIRE).ToString() || LobID == ((int)enumLOB.JDLGR).ToString() || LobID == ((int)enumLOB.HOME).ToString())
                {
                    txtLOCATION_ADDRESS.Text = dt.Rows[0]["ADDRESS"].ToString();
                    txtLOCATION_COMPLIMENT.Text = dt.Rows[0]["COMPLIMENT"].ToString();
                    txtLOCATION_DISTRICT.Text = dt.Rows[0]["DISTRICT"].ToString();
                    txtLOCATION_ZIPCODE.Text = dt.Rows[0]["ZIP_CODE"].ToString();
                    hidRISK_CO_APP_ID.Value = dt.Rows[0]["CO_APPLICANT_ID"].ToString();
                     string Country= dt.Rows[0]["COUNTRY"].ToString();
                     if (cmbLOCATION_COUNTRY.Items.FindByValue(Country) != null)
                     {
                         cmbLOCATION_COUNTRY.SelectedValue = Country;
                     }
                   

                    if (!string.IsNullOrEmpty(cmbLOCATION_COUNTRY.SelectedValue))
                    {
                        FillState(int.Parse(cmbLOCATION_COUNTRY.SelectedValue), ref cmbLOCATION_STATE);
                    }
                    string LocationState = dt.Rows[0]["STATE"].ToString();

                    if (cmbLOCATION_STATE.Items.FindByValue(LocationState) != null)
                    {
                        cmbLOCATION_STATE.SelectedValue = dt.Rows[0]["STATE"].ToString();

                        hidLOCATION_STATE.Value = cmbLOCATION_STATE.SelectedValue;
                    }


                    
                     txtACTUAL_INSURED_OBJECT.Text = dt.Rows[0]["ACTUAL_INSURED_OBJECT"].ToString();
                     
                  
                     txtLOCATION_ITEM_NUMBER.Text  = dt.Rows[0]["LOCATION_ITEM_NUMBER"].ToString();


                     cmbLOCATION_COUNTRY.Enabled = false;
                     cmbLOCATION_STATE.Enabled = false;
                     txtACTUAL_INSURED_OBJECT.ReadOnly = true;
                     txtLOCATION_ITEM_NUMBER.ReadOnly = true;

                     txtLOCATION_ADDRESS.ReadOnly = true;
                     txtLOCATION_COMPLIMENT.ReadOnly = true;
                     txtLOCATION_DISTRICT.ReadOnly = true;
                     txtLOCATION_ZIPCODE.ReadOnly = true;

                }

                if (LobID == ((int)enumLOB.INDPA).ToString() || LobID == ((int)enumLOB.CPCACC).ToString() || LobID == ((int)enumLOB.MRTG).ToString() || LobID == ((int)enumLOB.GRPLF).ToString())
                {
                    txtINSURED_NAME.Text = dt.Rows[0]["INSURED_NAME"].ToString();
                    string Dob = dt.Rows[0]["DATE_OF_BIRTH"].ToString();
                    string EFFECTIVE_DATETIME = dt.Rows[0]["EFFECTIVE_DATETIME"].ToString();
                    string EXPIRY_DATE = dt.Rows[0]["EXPIRY_DATE"].ToString();
                    hidRISK_CO_APP_ID.Value = dt.Rows[0]["CO_APPLICANT_ID"].ToString();

                    if(!string.IsNullOrEmpty(Dob))
                        txtPERSON_DOB.Text = Convert.ToDateTime(Dob).ToShortDateString();// ConvertDBDateToCulture(Dob);

                    if (!string.IsNullOrEmpty(EFFECTIVE_DATETIME))
                    {
                        txtEFFECTIVE_DATE.Text = Convert.ToDateTime(EFFECTIVE_DATETIME).ToShortDateString();// ConvertDBDateToCulture(Dob);
                        txtEFFECTIVE_DATE.Enabled = false;
                    }

                    if (!string.IsNullOrEmpty(EXPIRY_DATE))
                    {
                        txtEXPIRE_DATE.Text = Convert.ToDateTime(EXPIRY_DATE).ToShortDateString();// ConvertDBDateToCulture(Dob);
                        txtEXPIRE_DATE.Enabled = false;
                    }


                    txtINSURED_NAME.ReadOnly = true;
                    txtPERSON_DOB.ReadOnly = true;
                   
                }

                if (LobID == ((int)enumLOB.MTIME).ToString())
                {
                    txtVESSEL_NAME.Text = dt.Rows[0]["NAME_OF_VESSEL"].ToString();
                    txtVESSEL_TYPE.Text = dt.Rows[0]["TYPE_OF_VESSEL"].ToString();
                    txtVESSEL_MANUFACTURED_YEAR.Text = dt.Rows[0]["MANUFACTURE_YEAR"].ToString();
                    txtVESSEL_MANUFACTURER.Text = dt.Rows[0]["MANUFACTURER"].ToString();
                    txtVESSEL_NUMBER.Text = dt.Rows[0]["VESSEL_NUMBER"].ToString();
                    hidRISK_CO_APP_ID.Value = dt.Rows[0]["CO_APPLICANT_ID"].ToString();

                    txtVESSEL_NAME.ReadOnly = true;
                    txtVESSEL_TYPE.ReadOnly = true;
                    txtVESSEL_MANUFACTURED_YEAR.ReadOnly = true;
                    txtVESSEL_MANUFACTURER.ReadOnly = true;
                    txtVESSEL_NUMBER.ReadOnly = true;

                    
                }

                if (LobID == ((int)enumLOB.CVLIABTR).ToString() || LobID == ((int)enumLOB.FACLIAB).ToString() || LobID == ((int)enumLOB.AERO).ToString() || LobID == ((int)enumLOB.MTOR).ToString() || LobID == ((int)enumLOB.CTCL).ToString() || LobID == ((int)enumLOB.MOT).ToString())
                {

                    txtVEHICLE_VIN.Text = dt.Rows[0]["VEHICLE_VIN"].ToString();
                    txtVEHICLE_YEAR.Text = dt.Rows[0]["MANUFACTURED_YEAR"].ToString();
                    txtVEHICLE_MAKER.Text = dt.Rows[0]["MANUFACTURER"].ToString();
                    txtVEHICLE_MODEL.Text = dt.Rows[0]["MODEL"].ToString();
                    hidRISK_CO_APP_ID.Value = dt.Rows[0]["CO_APPLICANT_ID"].ToString();

                    txtVEHICLE_LIC_PT_NUMBER.Text = dt.Rows[0]["LICENSE_PLATE"].ToString();


                    txtVEHICLE_VIN.ReadOnly = true;
                    txtVEHICLE_YEAR.ReadOnly = true;
                    txtVEHICLE_MAKER.ReadOnly = true;
                    txtVEHICLE_MODEL.ReadOnly = true;
                    txtVEHICLE_LIC_PT_NUMBER.ReadOnly = true;

                }


                if (LobID == ((int)enumLOB.NATNTR).ToString() || LobID == ((int)enumLOB.INTERN).ToString())
                {
                    string DepartureDate=dt.Rows[0]["DEPARTING_DATE"].ToString();
                    if (!string.IsNullOrEmpty(DepartureDate))
                        txtVOYAGE_DEPARTURE_DATE.Text = Convert.ToDateTime(DepartureDate).ToShortDateString(); //ConvertDBDateToCulture(DepartureDate);

                    if (dt.Rows[0]["CONVEYANCE_TYPE"] != null && dt.Rows[0]["CONVEYANCE_TYPE"].ToString() != "")
                    {
                        if(cmbVOYAGE_CONVEYENCE_TYPE.Items.FindByValue(dt.Rows[0]["CONVEYANCE_TYPE"].ToString())!=null)
                           cmbVOYAGE_CONVEYENCE_TYPE.SelectedValue = dt.Rows[0]["CONVEYANCE_TYPE"].ToString();
                    }

                    txtVOYAGE_DESTINATION_CITY.Text = dt.Rows[0]["DESTINATION_CITY"].ToString();

                    txtVOYAGE_DESTINATION_COUNTRY.Text = dt.Rows[0]["DESTINATION_COUNTRY"].ToString(); 
                    txtVOYAGE_DESTINATION_STATE.Text = dt.Rows[0]["DESTINATION_STATE"].ToString();
                    txtVOYAGE_ORIGIN_COUNTRY.Text = dt.Rows[0]["ORIGIN_COUNTRY"].ToString();
                    txtVOYAGE_ORIGIN_STATE.Text = dt.Rows[0]["ORIGIN_STATE"].ToString();
                    txtVOYAGE_ORIGIN_CITY.Text = dt.Rows[0]["ORIGIN_CITY"].ToString();
                    hidRISK_CO_APP_ID.Value = dt.Rows[0]["CO_APPLICANT_ID"].ToString();
                    
                  


                    cmbVOYAGE_CONVEYENCE_TYPE.Enabled = false;
                    txtVOYAGE_DEPARTURE_DATE.ReadOnly = true;
                    txtVOYAGE_DESTINATION_CITY.ReadOnly = true;
                    txtVOYAGE_DESTINATION_COUNTRY.Enabled = false;
                    txtVOYAGE_DESTINATION_STATE.Enabled = false;
                    txtVOYAGE_ORIGIN_CITY.ReadOnly = true;
                    txtVOYAGE_ORIGIN_STATE.Enabled = false;
                    txtVOYAGE_ORIGIN_COUNTRY.Enabled = false;

                }

                if (LobID == ((int)enumLOB.DPVA).ToString() || LobID == ((int)enumLOB.DPVAT2).ToString())
                {
                    string DP_CATEGORY = dt.Rows[0]["DP_CATEGORY"].ToString();

                    if (cmbDP_CATEGORY.Items.FindByValue(DP_CATEGORY) != null)
                        cmbDP_CATEGORY.SelectedValue = DP_CATEGORY;

                    string DP_STATE_ID = dt.Rows[0]["DP_STATE_ID"].ToString();

                    if (cmbDP_STATE_ID.Items.FindByValue(DP_STATE_ID) != null)
                        cmbDP_STATE_ID.SelectedValue = DP_STATE_ID;

                    txtDP_TICKET_NUMBER.Text = dt.Rows[0]["DP_TICKET_NUMBER"].ToString();
                    hidRISK_CO_APP_ID.Value = dt.Rows[0]["CO_APPLICANT_ID"].ToString();
                    cmbDP_CATEGORY.Enabled = false;
                    cmbDP_STATE_ID.Enabled = false;
                    txtDP_TICKET_NUMBER.ReadOnly = true;
                }
                                 
                if (LobID == ((int)enumLOB.PAPEACC).ToString())
                {
                    txtPA_NUM_OF_PASS.Text = dt.Rows[0]["PA_NUM_OF_PASS"].ToString();
                    hidRISK_CO_APP_ID.Value = dt.Rows[0]["CO_APPLICANT_ID"].ToString();
                    string PA_START_DATE = dt.Rows[0]["PA_START_DATE"].ToString();
                    string PA_END_DATE = dt.Rows[0]["PA_END_DATE"].ToString();

                    if (!string.IsNullOrEmpty(PA_START_DATE))
                        txtPA_START_DATE.Text = Convert.ToDateTime(PA_START_DATE).ToShortDateString();

                    if (!string.IsNullOrEmpty(PA_END_DATE))
                        txtPA_END_DATE.Text = Convert.ToDateTime(PA_END_DATE).ToShortDateString();

                    txtPA_START_DATE.ReadOnly = true;
                    txtPA_END_DATE.ReadOnly = true;
                    txtPA_NUM_OF_PASS.ReadOnly = true;
                }

                if (LobID == ((int)enumLOB.RLLE).ToString())
                {

                    txtRURAL_ITEM_NUMBER.Text = dt.Rows[0]["RURAL_ITEM_NUMBER"].ToString();
                    txtRURAL_CITY.Text = dt.Rows[0]["RURAL_CITY"].ToString();
                    txtRURAL_INSURED_AREA.Text = dt.Rows[0]["RURAL_INSURED_AREA"].ToString();
                    txtRURAL_SUBSIDY_PREMIUM.Text = dt.Rows[0]["RURAL_SUBSIDY_PREMIUM"].ToString();
                    hidRISK_CO_APP_ID.Value = dt.Rows[0]["CO_APPLICANT_ID"].ToString();

                    string strRURAL_FESR_COVERAGE = dt.Rows[0]["RURAL_FESR_COVERAGE"].ToString();
                    string strRURAL_CULTIVATION = dt.Rows[0]["RURAL_CULTIVATION"].ToString();
                    string strRURAL_MODE = dt.Rows[0]["RURAL_MODE"].ToString();
                    string strRURAL_PROPERTY = dt.Rows[0]["RURAL_PROPERTY"].ToString();
                    string strRURAL_STATE_ID = dt.Rows[0]["RURAL_STATE_ID"].ToString();
                    string strRURAL_SUBSIDY_STATE = dt.Rows[0]["RURAL_SUBSIDY_STATE"].ToString();


                   if(strRURAL_FESR_COVERAGE !="" && int.Parse(strRURAL_FESR_COVERAGE) == Convert.ToInt32(enumYESNO_LOOKUP_UNIQUE_ID.YES))// 10963;
                        chkRURAL_FESR_COVERAGE.Checked=true;
                    else
                        chkRURAL_FESR_COVERAGE.Checked=false;



                    if (!string.IsNullOrEmpty(strRURAL_CULTIVATION))
                        cmbRURAL_CULTIVATION.SelectedValue = strRURAL_CULTIVATION;

                    if (!string.IsNullOrEmpty(strRURAL_MODE))
                        cmbRURAL_MODE.SelectedValue = strRURAL_MODE;

                    if (!string.IsNullOrEmpty(strRURAL_PROPERTY))
                        cmbRURAL_PROPERTY.SelectedValue = strRURAL_PROPERTY;

                    if (strRURAL_STATE_ID!="-1" &&!string.IsNullOrEmpty(strRURAL_STATE_ID))
                        cmbRURAL_STATE_ID.SelectedValue = strRURAL_STATE_ID;

                    if (strRURAL_SUBSIDY_STATE !="-1" && !string.IsNullOrEmpty(strRURAL_SUBSIDY_STATE))
                        cmbRURAL_SUBSIDY_STATE.SelectedValue = strRURAL_SUBSIDY_STATE;

                    chkRURAL_FESR_COVERAGE.Enabled = false;
                    cmbRURAL_CULTIVATION.Enabled = false;
                    cmbRURAL_MODE.Enabled = false;
                    cmbRURAL_PROPERTY.Enabled = false;
                    cmbRURAL_STATE_ID.Enabled = false;
                    cmbRURAL_SUBSIDY_STATE.Enabled = false;
                    txtRURAL_ITEM_NUMBER.ReadOnly = true;
                    txtRURAL_CITY.ReadOnly = true;
                    txtRURAL_INSURED_AREA.ReadOnly = true;
                    txtRURAL_SUBSIDY_PREMIUM.ReadOnly = true;

                }

                if (LobID == ((int)enumLOB.RETSURTY).ToString())
                {
                    txtRENTAL_INSURED_OBJECT.Text = dt.Rows[0]["ACTUAL_INSURED_OBJECT"].ToString();                   
                    txtITEM_NUMBER.Text = dt.Rows[0]["ITEM_NUMBER"].ToString();
                    hidRISK_CO_APP_ID.Value = dt.Rows[0]["CO_APPLICANT_ID"].ToString();

                    txtRENTAL_INSURED_OBJECT.ReadOnly = true;
                    txtITEM_NUMBER.ReadOnly = true;
                }


               
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtDAMAGE_DESCRIPTION.Text = string.Empty;

            if (TblLocation.Visible)
            {
                cmbPOL_LOCATION_ID.SelectedIndex = 0;
                txtLOCATION_ADDRESS.Text=string.Empty;
                txtLOCATION_COMPLIMENT.Text = string.Empty;
                txtLOCATION_DISTRICT.Text = string.Empty;
                txtLOCATION_ZIPCODE.Text = string.Empty;
            }
            if (TblVehicle.Visible)
            {
                cmbPOL_VEHICLE_ID.SelectedIndex = 0;
                txtVEHICLE_MAKER.Text = string.Empty;
                txtVEHICLE_MODEL.Text = string.Empty;
                txtVEHICLE_VIN.Text = string.Empty;
                txtVEHICLE_YEAR.Text = string.Empty;
               
            }

            if (TblVessel.Visible)
            {
                cmbPOL_VESSEL_ID.SelectedIndex = 0;
                txtVESSEL_MANUFACTURED_YEAR.Text = string.Empty;
                txtVESSEL_MANUFACTURER.Text = string.Empty;
                txtVESSEL_NAME.Text = string.Empty;
                txtVESSEL_TYPE.Text = string.Empty;

            }
            if (TblVoyage.Visible)
            {
                cmbPOL_VOYAGE_ID.SelectedIndex = 0;
                cmbVOYAGE_CONVEYENCE_TYPE.SelectedIndex = 0;
                txtVOYAGE_DEPARTURE_DATE.Text = string.Empty;
                txtVOYAGE_DESTINATION_CITY.Text = string.Empty;
                txtVOYAGE_ORIGIN_CITY.Text = string.Empty;
            }
            if (TblPerson.Visible)
            {
                cmbPOL_PERSON_ID.SelectedIndex = 0;
                txtINSURED_NAME.Text = string.Empty;
                txtEFFECTIVE_DATE.Text = string.Empty;
                txtEXPIRE_DATE.Text = string.Empty;
                
            }
        }

        protected void cmbDPVAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbDPVAT.SelectedValue))
            {
                GetRiskTypeDetails(int.Parse(cmbDPVAT.SelectedValue));
            }
        }

        protected void cmbRURAL_LIEN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbRURAL_LIEN.SelectedValue))
            {
                GetRiskTypeDetails(int.Parse(cmbRURAL_LIEN.SelectedValue));
            }
        }

        protected void cmbPERSONAL_ACCIDENT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbPERSONAL_ACCIDENT.SelectedValue))
            {
                GetRiskTypeDetails(int.Parse(cmbPERSONAL_ACCIDENT.SelectedValue));
            }
        }

      
        

       
        
    }
}
