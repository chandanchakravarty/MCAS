<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitialLoadCommitedPolicyDetail.aspx.cs" Inherits="Cms.Account.Aspx.InitialLoadCommitedPolicyDetail" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head >
    <title>Initial Load Commited Policy Details Index Page</title>
    	<link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css"/>
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
        <script type="text/javascript" type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
        <STYLE>
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">

		    function onRowClicked(num, msDg) {
		       
		        rowNum = num;
		        if (parseInt(num) == 0)
		            strXML = "";

		        populateXML(num, msDg);
		        
		         if (document.getElementById('hidPageMode').value == 'PR') {

		            var objXmlHandler = new XMLHandler();
		            var tree1 = objXmlHandler.quickParseXML(strXML);
		            var tree = objXmlHandler.quickParseXML(strXML).childNodes[0];

		            var customer_id = tree.firstChild.childNodes[0].attributes.firstChild.text;
		            var policy_id = tree.firstChild.childNodes[1].attributes.firstChild.text;
		            var policy_ver_id = tree.firstChild.childNodes[2].attributes.firstChild.text;

		            var strcustomer_id = tree.firstChild.childNodes[0].nodeName;
		            var strpolicy_id = tree.firstChild.childNodes[1].nodeName;
		            var strpolicy_ver_id = tree.firstChild.childNodes[2].nodeName;

		            if (strcustomer_id == "CUSTOMER_ID " || strpolicy_id == "POLICY_ID" || strpolicy_ver_id == "POLICY_VERSION_ID") {
		                var Query = "customer_id=" + customer_id + "&policy_id=" + policy_id + "&policy_version_id=" + policy_ver_id + "&app_id=" + policy_id + "&app_version_id=" + policy_ver_id + "&"
		                window.parent.opener.parent.location = "/cms/Policies/aspx/policytab.aspx?" + Query;
		                window.parent.close();
		            }
		            else {
		                var strmsg = document.getElementById('hidErrormsg').value;
		                alert(strmsg);
		            }
		        }
		        else if (document.getElementById('hidPageMode').value == 'EX') {
		            changeTab(0, 0);
		        }

		    }


		    function DeleteAllRecords() {
		        document.getElementById('hidCALLED_FOR').value = "DEL_ALL";
		        __doPostBack('__Page', 'MyCustomArgument');
		    }
		  

	
		</script>
</head>
<body oncontextmenu="return false;" leftmargin="0" rightmargin="0" onload="setfirstTime();" MS_POSITIONING="GridLayout">
		<form id="indexForm" method="post" runat="server">
		<div id="bodyHeight" class="pageContent">
			<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class="tableWidthHeader" width="100%" cellSpacing="0" cellPadding="0" border="0"
							align="center">
							
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								 
								<tr>
									<td id="tdGridHolder">
                                        <asp:Label ID="lblMessage" Runat="server" CssClass="errmsg" ></asp:Label>    
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" ></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td>
																<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
																	width="100%"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<asp:Label ID="lblTemplate"  Runat="server" Visible="false"></asp:Label>
                                    
										</table>
									</td>
								</tr>
                                <tr>
                                <td>
             
                                </td>
                                </tr>
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
                                       
									</td>
								</tr>
                                 
                               
						</table>
                                
						<input type="hidden" name="hidTemplateID" id="hidTemplateID"/>
                        <input type="hidden" name="hidRowID" id="hidRowID"/>
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"/>
                        <input type="hidden" name="hidMode" id="hidMode"/>
						<input type="hidden" name="hidIMPORT_REQUEST_ID" id="hidIMPORT_REQUEST_ID" value="0"  runat="server"/>  
                        <input type="hidden" name="hidIMPORT_SERIAL_NO" id="hidIMPORT_SERIAL_NO" value="0"  runat="server"/>  
                        <input type="hidden" name="hidCALLED_FOR" id="hidCALLED_FOR" runat="server"/>  
						<input type="hidden" id="hidDelString" name="hidDelString" runat="server" />   
						<input type="hidden" id="hidErrMsg" name="hidDelString" runat="server" />   
                        <input id="hidKeyValues" type="hidden" runat="server" />
                        <input type="hidden" id="hidPageMode" name="hidPageMode" runat="server" />
		                <input type="hidden" id="hidErrormsg" name="hidErrormsg" runat="server" />
					</td>
				</tr>
                
			</table>
		</div>
        </form>
	</body>
</html>
