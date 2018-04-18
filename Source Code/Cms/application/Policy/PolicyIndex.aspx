<%@ Page language="c#" Codebehind="PolicyIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.Policy.PolicyIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>PriorLossIndex</title>
		<%--<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">--%>
		<link rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css" />
		<script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/menubar.js" type="text/javascript"></script>
		<link href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet" />		
		<style type="text/css">
		.hide { OVERFLOW: hidden; TOP: 5px }
		.show { OVERFLOW: hidden; TOP: 5px }
		#tabContent { POSITION: absolute; TOP: 160px }
		</style>
			<script language="javascript" type="text/javascript">	
		
		function onRowClicked(num,msDg )
		{
		
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			
			populateXML(num,msDg);					
			if ( !(event.srcElement.outerHTML.indexOf("OPTION") != -1 || event.srcElement.outerHTML.indexOf("button_Click") != -1))
			{
				if(imageClick!=1)
				{
					top.botframe.location.href = "/cms/policies/aspx/policyTab.aspx?" + locQueryStr + "&" ;
				}
				else
					imageClick=0;				
			}
			//changeTab(0, 0);
        }

        //Function added by Charles on 11-Mar-10 for Policy Page Implementation
        function addNewPolicy() {
            parent.document.location.href = "/CMS/POLICIES/ASPX/POLICYTAB.aspx?CALLEDFROM=CLT";
            return false;
        }
		</script>
	</head>
	<body oncontextmenu="javascript:return false;" style="margin-left:0; margin-top:0" onload="javascript:ApplyColor();ChangeColor();" MS_POSITIONING="GridLayout">
		<%--	changes by praveer panghal for itrack no 1318--%>
		<div id="bodyHeight" class="pageContent" style='width:100%; overflow: auto;'>
			<form id="indexForm" method="post" runat="server">			
			<table class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0" width=100%>
				<tr>
					<td>
						<table  class ="tableWidthHeader" cellspacing="0" cellpadding="0" border="0" align="center">
							<tr>
								<td align=center >
									<asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
								</td>
							</tr>
							<tr>
								<TD id = "tdClientTop" class="pageHeader" colspan="4">
									<webcontrol:ClientTop id="cltClientTop" runat="server" width ="100%" ></webcontrol:ClientTop>
								</TD>
							</tr>				
							<tr>
								<td>
									<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
									<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
								</td>
							</tr>							
							<tr><td><webcontrol:GridSpacer id="Gridspacer" runat="server"></webcontrol:GridSpacer></td></tr>
							<tr>
								<td>					
									<table class ="tableWidthHeader"  cellspacing="0" cellpadding="0" border="0" align="center">
										<tr id="tabCtlRow">
											<td>
												<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="" TabTitles="Add Policy" TabLength="150"></webcontrol:Tab>
											</td>
										</tr>
										<tr>
											<td>
												<table class="tableeffect" width="100%" cellpadding="0" cellspacing="0" border="0">
													<tr>
															<td><%--	changes by praveer panghal for itrack no 1318--%>
																<iframe id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
																	width="100%" height="0px"></iframe>
															</td>
													</tr>
												</table>
											</td>
										</tr>
										<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
									</table>
								</td>
							</tr>
							<tr>
								<td>				
									<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
								</td>
							</tr>
						</table>
						<input type="hidden" name="hidTemplateID" /> 
						<input type="hidden" name="hidRowID" />
						<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr" /> 
						<input type="hidden" name="hidMode" />
						<input id="hide" type="hidden" name="ConVar" /> 
						<span id="singleRec"></span>					
				</td>
			</tr>
		</table>	
		</form>
		</div>		
	</body>
</html>
