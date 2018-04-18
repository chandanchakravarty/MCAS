<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="InitialLoadApplicationIndex.aspx.cs" Inherits="Cms.Account.Aspx.InitialLoadApplicationIndex" %>

<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head >
    <title>Initial Load Deatils</title>
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

         function onRowClicked(num, msDg) 
         {
             rowNum = num;
             populateXML(num, msDg);
             populateFormData(strXML, indexForm);

             if (document.getElementById('hidPageMode').value == '14936_P' || document.getElementById('hidPageMode').value == '14937_P' || document.getElementById('hidPageMode').value == '14938_P')
              {
                 var objXmlHandler = new XMLHandler();
                 var tree1 = objXmlHandler.quickParseXML(strXML);
                 var tree = objXmlHandler.quickParseXML(strXML).childNodes[0];

                 var customer_id = tree.firstChild.childNodes[2].attributes.firstChild.text;
                 var strcustomer_id = tree.firstChild.childNodes[2].nodeName;
                 if (strcustomer_id == "CUSTOMER_ID") 
                 {
                     var Query = "customer_id="+customer_id+"&"
                     window.parent.opener.parent.location = "/Cms/client/Aspx/CustomerTabIndex.aspx?" + Query; 
                     window.parent.close();
                 }
                 else 
                 {
                     var strmsg = document.getElementById('hidErrormsg').value;
                     alert(strmsg);
                 }
             }

             else if (document.getElementById('hidPageMode').value == '14939_P' || document.getElementById('hidPageMode').value == '14960_P' || document.getElementById('hidPageMode').value == '14942_P' || document.getElementById('hidPageMode').value == '14941_P' || document.getElementById('hidPageMode').value == '14940_P' || document.getElementById('hidPageMode').value == '14962_P' || document.getElementById('hidPageMode').value == '14963_P' || document.getElementById('hidPageMode').value == '14961_P' || document.getElementById('hidPageMode').value == '14966_P' || document.getElementById('hidPageMode').value == '14967_P' ||
                      document.getElementById('hidPageMode').value == '14969_P' || document.getElementById('hidPageMode').value == '15008_P' || document.getElementById('hidPageMode').value == '14968_P')
             {
                 var objXmlHandler = new XMLHandler();
                 var tree1 = objXmlHandler.quickParseXML(strXML);
                 var tree = objXmlHandler.quickParseXML(strXML).childNodes[0];

                 var customer_id = tree.firstChild.childNodes[2].attributes.firstChild.text;
                 var strcustomer_id = tree.firstChild.childNodes[2].nodeName;
                 var customer_id = tree.firstChild.childNodes[2].attributes.firstChild.text;
                 var policy_id = tree.firstChild.childNodes[3].attributes.firstChild.text;
                 var policy_ver_id = tree.firstChild.childNodes[4].attributes.firstChild.text;

                 var strcustomer_id = tree.firstChild.childNodes[2].nodeName;
                 var strpolicy_id = tree.firstChild.childNodes[3].nodeName;
                 var strpolicy_ver_id = tree.firstChild.childNodes[4].nodeName;

                 if (strcustomer_id == "CUSTOMER_ID " || strpolicy_id == "POLICY_ID" || strpolicy_ver_id == "POLICY_VERSION_ID" )
                 {
                     var Query = "customer_id=" + customer_id + "&policy_id=" + policy_id + "&policy_version_id=" + policy_ver_id + "&app_id=" + policy_id + "&app_version_id=" + policy_ver_id + "&"
                     window.parent.opener.parent.location = "/cms/Policies/aspx/policytab.aspx?" + Query;
                     window.parent.close();
                 }
                 else
                 {
                     var strmsg = document.getElementById('hidErrormsg').value;
                     alert(strmsg);
                 }
             }
             else if (document.getElementById('hidPageMode').value == '14943_P' || document.getElementById('hidPageMode').value == '14997_P' || document.getElementById('hidPageMode').value == '14998_P' || document.getElementById('hidPageMode').value == '14999_P'
                    || document.getElementById('hidPageMode').value == '15000_P' || document.getElementById('hidPageMode').value == '14944_P' || document.getElementById('hidPageMode').value == '15001_P' || document.getElementById('hidPageMode').value == '15002_P'
                    || document.getElementById('hidPageMode').value == '15003_P' || document.getElementById('hidPageMode').value == '15004_P' || document.getElementById('hidPageMode').value == '15005_P' || document.getElementById('hidPageMode').value == '15006_P' 
                    || document.getElementById('hidPageMode').value == '15007_P')
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

                 if (strcustomer_id == "CUSTOMER_ID " || strpolicy_id == "POLICY_ID" || strpolicy_ver_id == "POLICY_VERSION_ID" || strClaim_ID == "CLAIM_ID") {
                     var Query = "CUSTOMER_ID=" + customer_id + "&POLICY_ID=" + policy_id + "&CLAIM_ID=" + claim_ID + "&LOB_ID=" + lob_id + "&POLICY_VERSION_ID=" + policy_ver_id + "&"
                     window.parent.opener.parent.location = "/cms/Claims/Aspx/ClaimsTab.aspx?" + Query;
                     window.parent.close();
                 }
                 else {
                     var strmsg = document.getElementById('hidErrormsg').value;
                     alert(strmsg);
                 }
             }
             else if (document.getElementById('hidPageMode').value == '14936_E' || document.getElementById('hidPageMode').value == '14937_E' || document.getElementById('hidPageMode').value == '14938_E' || document.getElementById('hidPageMode').value == '14939_E' || document.getElementById('hidPageMode').value == '14960_E' || document.getElementById('hidPageMode').value == '14942_E' || document.getElementById('hidPageMode').value == '14941_E' || document.getElementById('hidPageMode').value == '14940_E' || document.getElementById('hidPageMode').value == '14962_E'
                      || document.getElementById('hidPageMode').value == '14963_E' || document.getElementById('hidPageMode').value == '14961_E' || document.getElementById('hidPageMode').value == '14969_E' || document.getElementById('hidPageMode').value == '14966_E' || document.getElementById('hidPageMode').value == '14967_E' || document.getElementById('hidPageMode').value == '14943_E' || document.getElementById('hidPageMode').value == '15008_E' || document.getElementById('hidPageMode').value == '14944_E' || document.getElementById('hidPageMode').value == '14997_E'
                      || document.getElementById('hidPageMode').value == '14998_E' || document.getElementById('hidPageMode').value == '14999_E' || document.getElementById('hidPageMode').value == '15000_E' || document.getElementById('hidPageMode').value == '15001_E' || document.getElementById('hidPageMode').value == '15002_E' || document.getElementById('hidPageMode').value == '15003_E' || document.getElementById('hidPageMode').value == '15004_E' || document.getElementById('hidPageMode').value == '15005_E' || document.getElementById('hidPageMode').value == '15006_E'
                      || document.getElementById('hidPageMode').value == '15007_E' || document.getElementById('hidPageMode').value == '14968_E')

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

