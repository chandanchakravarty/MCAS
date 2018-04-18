/******************************************************************************************
	<Author					:  Aditya Goel
	<Start Date				:   07/01/2011
	<End Date				: - >
	<Description			: - >
	<Review Date			: - >
	<Reviewed By			: - >
	
	Modification History

	<Modified Date			: - >
	<Modified By			: - >
	<Purpose				: - >
*******************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Cms.DataLayer;
using Cms.CmsWeb;
using System.Resources;
using System.Reflection;
//using Cms.BusinessLayer.BlCommon;

namespace Cms.Reports.Aspx
{
    public partial class RIBreakdown : Cms.CmsWeb.cmsbase
    {
        ResourceManager Objresources;
        ResourceManager Objresources1;
        Int32 CUSTOMER_ID = 0;
        Int32 POLICY_ID = 0;
        Int32 POLICY_VERSION_ID = 0;
        string POLICY_NUMBER = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SetCultureThread(GetLanguageCode());
            Objresources = new System.Resources.ResourceManager("Cms.Reports.Aspx.RIBreakdown", System.Reflection.Assembly.GetExecutingAssembly());
            Objresources1 = new System.Resources.ResourceManager("Cms.Reports.Aspx.RIBreakdown", System.Reflection.Assembly.GetExecutingAssembly());

            if (Request.QueryString["CUSTOMER_ID"] != null && Request.QueryString["CUSTOMER_ID"].ToString() != "")
                CUSTOMER_ID = Convert.ToInt32(Request.QueryString["CUSTOMER_ID"].ToString());
            if (Request.QueryString["POLICY_ID"] != null && Request.QueryString["POLICY_ID"].ToString() != "")
                POLICY_ID = Convert.ToInt32(Request.QueryString["POLICY_ID"].ToString());
            if (Request.QueryString["POLICY_VERSION_ID"] != null && Request.QueryString["POLICY_VERSION_ID"].ToString() != "")
                POLICY_VERSION_ID = Convert.ToInt32(Request.QueryString["POLICY_VERSION_ID"].ToString());
            if (Request.QueryString["POLICY_NUMBER"] != null && Request.QueryString["POLICY_NUMBER"].ToString() != "")
                POLICY_NUMBER = Request.QueryString["POLICY_NUMBER"].ToString();// added by shubhanshu , for itrack 1379
            bindrptr(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, POLICY_NUMBER);

        }

        private void bindrptr(int CUSTOMER_ID, int POLICY_ID, int POLICY_VERSION_ID,string  POLICY_NUMBER)
        {
            Cms.DataLayer.DataWrapper objDataWrapper = new DataWrapper(Cms.BusinessLayer.BlCommon.ClsCommon.ConnStr, CommandType.StoredProcedure, DataWrapper.MaintainTransaction.NO, DataWrapper.SetAutoCommit.OFF);
            objDataWrapper.AddParameter("@CUSTOMER_ID", CUSTOMER_ID);
            objDataWrapper.AddParameter("@POLICY_ID", POLICY_ID);
            objDataWrapper.AddParameter("@POLICY_VERSION_ID", POLICY_VERSION_ID);
            objDataWrapper.AddParameter("@LANG_ID", int.Parse(GetLanguageID()));
            objDataWrapper.AddParameter("@POLICY_NUMBER", POLICY_NUMBER); // added by shubhanshu , for itrack 1379
            DataSet ds = new DataSet();
            ds = objDataWrapper.ExecuteDataSet("Proc_GetReinPolicyId");
            DataTable dt = ds.Tables[0];
            DataTable dt1 = ds.Tables[1];
            rptrein.DataSource = dt;
            rptrein.DataBind();
            rptrein1.DataSource = dt1;
            rptrein1.DataBind();
        }

        protected void rptrein_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Header)
            {
                //e.Item.Cells[7].Text = Objresources.GetString("ZIP_CODE");
                Label l21 = (Label)e.Item.FindControl("lblPOLICY_DISP_VERSION");
                l21.Text = Objresources1.GetString("lblPOLICY_DISP_VERSION");
                Label l1 = (Label)e.Item.FindControl("lblPOLICY_NUMBER");
                l1.Text = Objresources.GetString("lblPOLICY_NUMBER");
                Label l2 = (Label)e.Item.FindControl("lblREINSURER_NAME");
                l2.Text = Objresources.GetString("lblREINSURER_NAME");
                Label l3 = (Label)e.Item.FindControl("lblCONTRACT_NUMBER");
                l3.Text = Objresources.GetString("lblCONTRACT_NUMBER");
                Label l4 = (Label)e.Item.FindControl("lblLAYER");
                l4.Text = Objresources.GetString("lblLAYER");
                Label l5 = (Label)e.Item.FindControl("lblLAYER_AMOUNT");
                l5.Text = Objresources.GetString("lblLAYER_AMOUNT");
                Label l6 = (Label)e.Item.FindControl("lblRETENTION_PER");
                l6.Text = Objresources.GetString("lblRETENTION_PER");
                Label l7 = (Label)e.Item.FindControl("lblTRAN_PREMIUM");
                l7.Text = Objresources.GetString("lblTRAN_PREMIUM");
                Label l8 = (Label)e.Item.FindControl("lblREIN_PREMIUM");
                l8.Text = Objresources.GetString("lblREIN_PREMIUM");
                Label l9 = (Label)e.Item.FindControl("lblCOMM_AMOUNT");
                l9.Text = Objresources.GetString("lblCOMM_AMOUNT");
                Label l10 = (Label)e.Item.FindControl("lblCOMM_PER");
                l10.Text = Objresources.GetString("lblCOMM_PER");
                Label l11 = (Label)e.Item.FindControl("lblRISK_ID");
                l11.Text = Objresources.GetString("lblRISK_ID");
                //Label l12 = (Label)e.Item.FindControl("lblREIN_CEDED");
                //l12.Text = Objresources.GetString("lblREIN_CEDED");
                Label l13 = (Label)e.Item.FindControl("lblTIV");
                l13.Text = Objresources.GetString("lblTIV");
                Label l14 = (Label)e.Item.FindControl("lblRI_BREAKDOWN");
                l14.Text = Objresources.GetString("lblRI_BREAKDOWN");

                Label l15 = (Label)e.Item.FindControl("lblRATE");
                l15.Text = Objresources.GetString("lblRATE");
                hdtitle.Text = Objresources.GetString("hdtitle"); //Added by aditya for itrack - 1379
                
            }

        }

        protected void rptrein1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           
        }

        protected void rptrein1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                
                Label l8 = (Label)e.Item.FindControl("lblPOLICY_VERSION_ID");
                l8.Text = Objresources1.GetString("lblPOLICY_VERSION_ID");
                Label l1 = (Label)e.Item.FindControl("lblREIN_COM_CODE");
                l1.Text = Objresources1.GetString("lblREIN_COM_CODE");
                Label l2 = (Label)e.Item.FindControl("lblREIN_COMPANY_NAME");
                l2.Text = Objresources1.GetString("lblREIN_COMPANY_NAME");
                Label l3 = (Label)e.Item.FindControl("lblPREMIUM");
                l3.Text = Objresources1.GetString("lblPREMIUM");
                Label l4 = (Label)e.Item.FindControl("lblINTEREST");
                l4.Text = Objresources1.GetString("lblINTEREST");
                Label l5 = (Label)e.Item.FindControl("lblTOTAL_AMOUNT");
                l5.Text = Objresources1.GetString("lblTOTAL_AMOUNT");
                Label l6 = (Label)e.Item.FindControl("lblCOMM_AMOUNT");
                l6.Text = Objresources1.GetString("lblCOMM_AMOUNT");
                Label l7 = (Label)e.Item.FindControl("lblCOI_BREAKDOWN");
                l7.Text = Objresources1.GetString("lblCOI_BREAKDOWN");
            }
        }
    }
}
