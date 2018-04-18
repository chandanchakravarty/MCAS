<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="AcceptedCOILoadApplicationIndex.aspx.cs" Inherits="Cms.Account.Aspx.AcceptedCOILoadApplicationIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Application Deatils</title>
     <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
     <LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet" />          
	 
	 	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
    <style type="text/css">
        
        .style1
        {
            width: 240px;
        }
        
       
     </style>
     <script language="javascript">

         function onRowClicked(num, msDg) {
             
             rowNum = num;
             populateXML(num, msDg);
             populateFormData(strXML, indexForm);

             if (document.getElementById('hidPageMode').value == 'APP_P' || document.getElementById('hidPageMode').value == 'IST_P' || document.getElementById('hidPageMode').value == 'COV_P')
             {

                     var objXmlHandler = new XMLHandler();
                     var tree1 = objXmlHandler.quickParseXML(strXML);
                     var tree = objXmlHandler.quickParseXML(strXML).childNodes[0];

                     var customer_id = tree.firstChild.childNodes[2].attributes.firstChild.text;
                     var policy_id = tree.firstChild.childNodes[3].attributes.firstChild.text;
                     var policy_ver_id = tree.firstChild.childNodes[4].attributes.firstChild.text;

                     var strcustomer_id = tree.firstChild.childNodes[2].nodeName;
                     var strpolicy_id = tree.firstChild.childNodes[3].nodeName;
                     var strpolicy_ver_id = tree.firstChild.childNodes[4].nodeName;

                     if (strcustomer_id == "CUSTOMER_ID " || strpolicy_id == "POLICY_ID" || strpolicy_ver_id == "POLICY_VERSION_ID")
                      {
                         var Query = "customer_id=" + customer_id + "&policy_id=" + policy_id + "&policy_version_id=" + policy_ver_id + "&app_id=" + policy_id + "&app_version_id=" + policy_ver_id + "&"
                         window.parent.opener.parent.location = "/cms/Policies/aspx/policytab.aspx?" + Query;
                         window.parent.close();
                      }
                     else {
                         var strmsg = document.getElementById('hidErrormsg').value;
                         alert(strmsg);
                      }
            }

            else if (document.getElementById('hidPageMode').value == 'CLM_D_P' || document.getElementById('hidPageMode').value == 'CLM_P') 
            {
                 var objXmlHandler = new XMLHandler();
                 var tree1 = objXmlHandler.quickParseXML(strXML);
                 var tree = objXmlHandler.quickParseXML(strXML).childNodes[0];

                 var customer_id = tree.firstChild.childNodes[2].attributes.firstChild.text;
                 var policy_id = tree.firstChild.childNodes[3].attributes.firstChild.text;
                 var policy_ver_id = tree.firstChild.childNodes[4].attributes.firstChild.text;
                 var claim_ID = tree.firstChild.childNodes[5].attributes.firstChild.text;
                 var lob_id = tree.firstChild.childNodes[6].attributes.firstChild.text;
                 
                 var strcustomer_id = tree.firstChild.childNodes[2].nodeName;
                 var strpolicy_id = tree.firstChild.childNodes[3].nodeName;
                 var strpolicy_ver_id = tree.firstChild.childNodes[4].nodeName;
                 var strClaim_ID = tree.firstChild.childNodes[5].attributes.nodeName;

                 if (strcustomer_id == "CUSTOMER_ID " || strpolicy_id == "POLICY_ID" || strpolicy_ver_id == "POLICY_VERSION_ID" || strClaim_ID == "CLAIM_ID") 
                 {
                     var Query = "CUSTOMER_ID=" + customer_id + "&POLICY_ID=" + policy_id    + "&CLAIM_ID=" + claim_ID + "&LOB_ID=" + lob_id + "&POLICY_VERSION_ID=" + policy_ver_id + "&"
                     window.parent.opener.parent.location = "/cms/Claims/Aspx/ClaimsTab.aspx?" + Query;
                     window.parent.close();
                 }
                 else
                 {
                     var strmsg = document.getElementById('hidErrormsg').value;
                     alert(strmsg);
                 }
            }
            else if (document.getElementById('hidPageMode').value == 'APP_E' || document.getElementById('hidPageMode').value == 'COV_E' || document.getElementById('hidPageMode').value == 'IST_E' || document.getElementById('hidPageMode').value == 'CLM_D_E' || document.getElementById('hidPageMode').value == 'CLM_E') 
             {
                 changeTab(0, 0);
             }
            
         }
       
     </script>
</head>
	<body   MS_POSITIONING="GridLayout">
     
    <form id="indexForm" method="post" runat="server">
    
    <table width="100%" >
       
        <tr>
            <td class="midcolorc" colspan="5">
            
               <webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
            </td>
        </tr>
        <tr>
			<td width="100%" >
				<table class ="tableWidthHeader" cellSpacing="" cellPadding="0" border="0" align="left">
					<tr id="tabCtlRow">
						<td>
							<webcontrol:Tab ID="TabCtl" runat="server" ></webcontrol:Tab>
						</td>
					</tr>
					<tr>
					 <td>
						<table class="tableeffect" width="100%" cellpadding="0" cellspacing="0" border="0">
						 <tr>
							<td>
							 <iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0" width="100%"></iframe>
							</td>
						 </tr>
						</table>
					 </td>
				   </tr>
				</table>
			</td>
		</tr>
		<input type="hidden" id="hidPageMode" name="hidPageMode" runat="server" />
		<input type="hidden" id="hidErrormsg" name="hidErrormsg" runat="server" />
    </table>
    	 	 <input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
		     <input type="hidden" runat="server" name="hidlocQueryStr" id="hidlocQueryStr">
		      <input runat="server" type="hidden" name="hidMode" id="hidMode">
		     <input runat="server" type="hidden" name="hidGridRowClickNumber" id="hidGridRowClickNumber">
			 <input runat="server" type="hidden" name="hidGridRowClickMsg" id="hidGridRowClickMsg">
                 
							
    </form>
</body>
</html>

