<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page validateRequest=false language="c#" Codebehind="AG_CheckTypeSelect.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AG_CheckTypeSelect" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_CHECK_INFORMATION</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">
		 function init()
		 {
			document.getElementById('trClaimsChecks').style.display='none';
		 }
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="init();setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();">
		<FORM id="ACT_CHECK_INFORMATION" method="post" runat="server">
		 <DIV><webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu></DIV>	
			<!-- To add bottom menu -->			
			<!-- To add bottom menu ends here -->
			<div id="bodyHeight" class="pageContent">
				<TABLE class="tableWidth" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<TABLE width="100%" align="center" border="0">
								<tr>
									<td class="midcolorc" align="center"><asp:label id="lblCommitStatus" CssClass="errmsg" Runat="server"></asp:label></td>
								</tr>
								<tr>
									<td Class="HeadRow"><asp:label id="lblMessage" runat="server">General Checks</asp:label></td>
								</tr>
								
								<%--
								<tr>
									<TD class="midcolora" width="18%"><a href='AG_CheckListingIframe.aspx?TypeID=2'>Premium 
											Refund Checks for Suspense Amount</a></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><a href='AG_CheckListingIframe.aspx?TypeID=3'>Premium 
											Refund Checks for Return Premium Payment</a></TD>
								</tr>
								--%>
								<tr>
									<TD class="midcolora" width="18%"><a href='AG_AgencyCommissionChecks.aspx?TypeID=4' href1='AG_CheckListingIframe.aspx?TypeID=4'>Agency 
											Checks</a></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><a href='AG_CheckListingMisc.aspx?TypeID=8'>Miscellaneous 
											(Other) Checks</a></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><a href='AG_CheckListingIframe.aspx'>Premium Refund 
											Checks</a></TD>
								</tr>
								<tr id="trClaimsChecks">
									<TD class="midcolora" width="18%"><a href="../../cmsweb/Construction.html" href1='AG_CheckListingIframe.aspx?TypeID=5'>Claim 
											Checks</a></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><a href='AG_CheckListingReinsurance.aspx?TypeID=6'>Reinsurance 
											Premium Checks</a></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><a href='AG_CheckListingVandor.aspx?TypeID=7'>Vendor 
											Checks</a></TD>
								</tr>
								
								<tr>
									<TD class="midcolorc" width="18%">
										<cmsb:cmsbutton class="clsButton" id="btnCommitOnAllPages" runat="server" Text="Commit On All Pages"></cmsb:cmsbutton></TD>
								</tr>
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</div>
		</FORM>
	</BODY>
</HTML>
