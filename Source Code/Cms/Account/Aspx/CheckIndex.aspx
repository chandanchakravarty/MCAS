<%@ Page language="c#" Codebehind="CheckIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.CheckIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS-Define Sub Ranges</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
		var defaultMode = false;
		
		function onRowClicked(num,msDg )
		{
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
			var CheckTypeID = document.getElementById('hidlocQueryStr').value.split('&')[1];
			if(typeof(CheckTypeID)=="undefined")
			{
				if(document.getElementById('ExtraButton1'))
					document.getElementById('ExtraButton1').setAttribute("disabled",true)
			}
			else
			{
				CheckTypeID=CheckTypeID.split('=')[1];
				CheckTypeID = parseInt(CheckTypeID);
				if(CheckTypeID==9940 || CheckTypeID==9941 || CheckTypeID==9942 || CheckTypeID==9943 || CheckTypeID==9937 || CheckTypeID==9939)
				{//other types + Tax type:9939
					if(document.getElementById('ExtraButton1'))
						document.getElementById('ExtraButton1').setAttribute("disabled",true)
				}
				else
				{
					if(document.getElementById('ExtraButton1'))
						document.getElementById('ExtraButton1').setAttribute("disabled",false)
				}
			}
		}
		function ChangeToPrevTab()
		{
			parent.changeTab(0,0);
		}
		
		function ShowItemsToBePaid() 
		{
			if(IfRowSelected())
			{
				
					var url="CheckRegisterItemsPaidPopup.aspx?"+document.getElementById('hidlocQueryStr').value;	
					ShowPopup(url,'ItemsToBePaid',900,600);	
				
				
			}
		}
		function ShowPreviewItemList()
		{
			if(IfRowSelected())
			{
				//var url="PrintChartOfAccountRanges.aspx";	
				//ShowPopup(url,'DefineSubRanges',900,600);	
			}
		}
		function ShowPrintItwmList() 
		{
			if(IfRowSelected())
			{
				//var url="PrintChartOfAccountRanges.aspx";	
				//ShowPopup(url,'DefineSubRanges',900,600);	
			}
		}
		function ShowPreviewCheck()
		{
			if(IfRowSelected())
			{
				//var url="PrintChartOfAccountRanges.aspx";	
				//ShowPopup(url,'DefineSubRanges',900,600);	
			}
		}
		
		function CommitCheck()
		{
			
			if (document.getElementById("hidlocQueryStr").value == "")
			{
				alert("Please select record.");
				return;
			}	
		}
		
		function IfRowSelected()
		{
			if (document.getElementById("hidlocQueryStr").value == "")
			{
				alert("Please select a check from the grid.");
				return false;
			}
			else
				return true;
		}
		</script>
	</HEAD>
	<body oncontextmenu="return false;" onload="setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();" MS_POSITIONING="GridLayout">
		<form id="indexForm" method="post" runat="server">
			<DIV><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu></DIV>
			<!-- To add bottom menu -->
			<!-- To add bottom menu ends here -->
			<div class="pageContent" id="bodyHeight">
				<table class="tableWidth" cellSpacing="0" cellPadding="0" border="0">

					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:label id="capMessage" CssClass="errmsg" Visible="False" Runat="server"></asp:label></p>

								<tr>
									<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
											</tr>
											<tr>
												<TD class="midcolora" width="18%"><br><asp:label id="capCHECK_TYPE" runat="server">Select Check Type</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora" width="82%"><asp:dropdownlist id="cmbCHECK_TYPE" onfocus="SelectComboIndex('cmbCHECK_TYPE')" runat="server" AutoPostBack="True"></asp:dropdownlist><BR>
													<asp:requiredfieldvalidator id="rfvCHECK_TYPE" runat="server" ControlToValidate="cmbCHECK_TYPE" ErrorMessage="Please Select check Type" Display="Dynamic"></asp:requiredfieldvalidator></TD>
											</tr>
										
										</table>
									</td>
								</tr>
								<tr>
									<td><asp:placeholder id="GridHolder" runat="server"></asp:placeholder></td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr>
									<td>
										<TABLE class="tableWidthHeader" cellSpacing="0" cellPadding="0" align="center" border="0">
											<tr id="tabCtlRow">
												<td><webcontrol:tab id="TabCtl" runat="server" TabLength="150" TabTitles="Check Information" TabURLs="AddCheck.aspx?"></webcontrol:tab></td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<tr>
															<td><iframe class="iframsHeightLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no" runat="server"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<asp:label id="lblTemplate" Visible="false" Runat="server"></asp:label></TABLE>
									</td>
								</tr>
								<tr>
									<td><webcontrol:footer id="pageFooter" runat="server"></webcontrol:footer></td>
								</tr>
							</table>
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
							<INPUT id="hidKeyValues" type="hidden" runat="server"> <input id="hidlocQueryStr" type="hidden" name="hidlocQueryStr">
							<input type="hidden" name="hidMode"> <input id="hidCheckedRowIDs" type="hidden" runat="server">
						</td>
					</tr>
				</table>
			</div>
			<DIV>&nbsp;</DIV>
		</form>
	</body>
</HTML>
