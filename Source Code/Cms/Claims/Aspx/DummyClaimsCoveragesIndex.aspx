<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="DummyClaimsCoveragesIndex.aspx.cs" AutoEventWireup="false" Inherits="Claims.Aspx.DummyClaimsCoveragesIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DummyClaimsCoveragesIndex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		function addCoverage()
		{			
			//Url="AddDummyClaimsCoverage.aspx?";
			//alert(Url);
			//DrawTab(0,top.frames[1],'Add Coverages',Url);
			addRecord();		
		}
		
		function onRowClicked(num,msDg )
		{
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
			var i;
			for(i=1;i<=iPs;i++)		
			{
				if(eval("document.getElementById('Row_" + i + "')") == null)
				{
					i = i-1;
					break;
				}
			}
			document.getElementById('hidCOV_ID_CLAIM').value = num ;
		}
		function Init()
		{
			setfirstTime();
		}
		</script>
	</HEAD>
	<body onload="Init();" leftMargin="0" topMargin="0" rightMargin="0" MS_POSITIONING="GridLayout">
		
		<div class="pageContent" id="bodyHeight">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
								<tr>
									<td align="center">
										<asp:label id="capMessage" CssClass="errmsg" Visible="False" Runat="server"></asp:label>
									</td>
								</tr>
								<tr>
									<td id="tdGridHolder">
										<webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td>
										<webcontrol:gridspacer id="Gridspacer" runat="server"></webcontrol:gridspacer>
									</td>
								</tr>
								<tr>
									<td>
										<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:tab id="TabCtl" runat="server" TabLength="150"  TabTitles="Add Coverages" TabURLs="AddDummyClaimsCoverage.aspx?"></webcontrol:tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellSpacing="0" cellPadding="0" border="0">
														<tr>
															<td><iframe class="iframsHeightLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no"
																	runat="server"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<INPUT id="hidCOV_ID_CLAIM" type="hidden" value="0" name="hidCOV_ID_CLAIM" runat="server">
			</form>
		</div>
	</body>
</HTML>
