<%@ Page language="c#" Codebehind="CustomerIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.CustomerIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Sample for Wolvorine</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		
		<STYLE>
			.hide { OVERFLOW: hidden; TOP: 5px }
			.show { OVERFLOW: hidden; TOP: 5px }
			#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<SCRIPT src="../../cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script language="javascript">
	
		
		function onRowClicked(num,msDg )
		{
			//if(parseInt(num)==0)
			//	strXML="";
			rowNum=num;
			populateXML(num,msDg);		
			//changeTab(0, 0);
			document.location.href = "CustomerTabIndex.aspx?" + locQueryStr;
			
		}
		function addNew()
		{
			document.location.href = "CustomerTabIndex.aspx?";
		}

		</script>
	</HEAD>
	<body onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();" MS_POSITIONING="GridLayout">
		<DIV id="myTSMain" style="BEHAVIOR: url(/cms/cmsweb/htc/webservice.htc)"></DIV>
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<table class ="tableWidthHeader"  cellSpacing="0" cellPadding="0" border="0" align="center">
						
						<form id="indexForm" method="post" runat="server">
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
							<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
						<tr >
							<td id = "tdGridHolder">
								<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
								<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
							</td>							
						</tr>
						<tr>
							<td>					
								<table class ="tableWidthHeader"  cellSpacing="0" cellPadding="0" border="0" align="center">
									<tr>
										<td>
															<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
																width="100%"></iframe>
														</td>
									</tr>
								</table>
								</td>
							</tr>
							<tr>
								<td>				
									<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
								</td>
							</tr>
						</table>
					<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
								<!--//added on 27/04/2005-->
								<input type="hidden" name="hidlocQueryStr"> <input type="hidden" name="hidMode">
								<!--//added on 27/04/2005-->
					</form>
				</td>
			</tr>
		</table>
		</div>
		</div>
	</body>
</HTML>
