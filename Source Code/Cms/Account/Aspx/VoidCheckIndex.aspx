<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="VoidCheckIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.VoidCheckIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS-Define Sub Ranges</title>
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
		<script language="javascript">
		var defaultMode = false;
		
		function onRowClicked(num,msDg )
		{
			rowNum=num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);		
			changeTab(0, 0);
			
			var Mode = '<%=Request.QueryString["Mode"]%>';
			if(typeof(Mode)!="undefined" && Mode !='Register')//i.e. execute following code in void check screen.
			{
				var CheckTypeID = document.getElementById('hidlocQueryStr').value.split('&')[1];
				
				if(typeof(CheckTypeID)=="undefined")
				{
					document.getElementById('ExtraButton0').setAttribute("disabled",true)
				}
				else
				{
					CheckTypeID=CheckTypeID.split('=')[1];
					CheckTypeID = parseInt(CheckTypeID);
			/* -- commented as per issue #2646		
					if(CheckTypeID==9940 || CheckTypeID==9941 || CheckTypeID==9942 || CheckTypeID==9943 || CheckTypeID==9937 || CheckTypeID==9939)
					{//other types + Tax type:9939
						document.getElementById('ExtraButton0').setAttribute("disabled",true)
					}
					else
					{
						document.getElementById('ExtraButton0').setAttribute("disabled",false)
					}
			*/
				}
				document.getElementById('trCheckInfo').style.display='none';
			}
			else
			{
				document.getElementById('trCheckInfo').style.display='inline';
			}
		}
		//by pravesh
	function getAllSelected() { //public
	
	var str = "";
	//
	if (document.all("cbxSelectRow")) 
	{		
		if (typeof document.all("cbxSelectRow").checked != "undefined") 
		{
			if (document.all("cbxSelectRow").checked) str = SEPARATOR + document.all("cbxSelectRow").value.substr(1);
		}
		else 
		{
			for(i=0; i<document.all("cbxSelectRow").length; i++) 
			{
				if (document.all("cbxSelectRow").item(i).checked) str += SEPARATOR + document.all("cbxSelectRow").item(i).value.substr(1);
			}
		}
		return str.substr(1);
		
	}
	else
		return "";
	}//end here
		function ChangeToPrevTab()
		{
			parent.changeTab(0,0);
		}
		
		function VoidCheck() 
		{
			var SelectedIDs="";
			for(i=0; i<document.forms[0].elements.length; i++)
			{
				if (document.forms[0].elements[i].type == "checkbox" && document.forms[0].elements[i].checked == true)
				{
					SelectedIDs = SelectedIDs + "," + document.forms[0].elements[i].id
				}
			}
			if(SelectedIDs == "" || SelectedIDs == ",")
			{
				alert("Please select atleast one check for voiding.");
				return false;
			}
			else
				var flag=confirm('Are you sure you want to void the selected check(s)?');
				if (flag==false)
				return false;
				VoidChecks();
		}
		function ShowItemsPaid()
		{
			if(IfRowSelected())
			{
				//alert();
				var url="CheckRegisterItemsPaidPopup.aspx?"+document.getElementById('hidlocQueryStr').value+"&Mode=Register";
				ShowPopup(url,'DefineSubRanges',900,600);	
			}
		}
		function ShowPrint()
		{
			//if(IfRowSelected())
			{
				var url="CheckRegisterItemsPrintPopup.aspx";	
				ShowPopup(url,'DefineSubRanges',900,600);	
			}
		}
		function IfRowSelected()
		{
		/*	if (document.getElementById("hidlocQueryStr").value == "")
			{
				alert("Please select a check from the grid.");
				return false;
			}
			else
				return true;*/
		}
		


	
		</script>
	</HEAD>
	<body oncontextmenu="return false;" onload="setfirstTime();top.topframe.main1.mousein = false;showScroll();findMouseIn();"
		MS_POSITIONING="GridLayout">
		<form id="indexForm" method="post" runat="server">			
		<DIV><webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu></DIV>
			<!-- To add bottom menu -->			
			<!-- To add bottom menu ends here -->
			<div id="bodyHeight" class="pageContent">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
								<p align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></p>
								<tr>
									<td>
										<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
									</td>
								</tr>
								<tr>
									<td>
										<table border="0" cellpadding="0" cellspacing="0" width="100%">
											<tr>
											<tr>
												<td class="midcolorc" align="right" colSpan="2"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
											</tr>
											<TR>
												<TD class="midcolora" width="18%"><asp:label id="capCHECK_TYPE" runat="server">Select Check Type</asp:label><span class="mandatory">*</span></TD>
												<TD class="midcolora" width="82%"><asp:dropdownlist id="cmbCHECK_TYPE" onfocus="SelectComboIndex('cmbCHECK_TYPE')" runat="server" AutoPostBack="True"></asp:dropdownlist><BR>
													<asp:requiredfieldvalidator id="rfvCHECK_TYPE" runat="server" Display="Dynamic" ErrorMessage="Please Select check Type"
														ControlToValidate="cmbCHECK_TYPE"></asp:requiredfieldvalidator></TD>
											</TR>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<asp:placeholder id="GridHolder" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td>&nbsp;</td>
								</tr>
								<tr id="trCheckInfo" >
									<td>
										<TABLE class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
											<tr id="tabCtlRow">
												<td>
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddCheck.aspx?CalledFrom=Register" TabTitles="Check Information"
														TabLength="150"></webcontrol:Tab>
												</td>
											</tr>
											<tr>
												<td>
													<table class="tableeffect" width="100%" cellpadding="0" cellspacing="0" border="0">
														<tr>
															<td>
																<iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no" frameborder="0"
																	width="100%"></iframe>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<asp:Label ID="lblTemplate" Runat="server" Visible="false"></asp:Label>
										</TABLE>
									</td>
								</tr>
								<tr>
									<td>
										<webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
									</td>
								</tr>
							</table>
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID"><INPUT id="hidKeyValues" type="hidden" runat="server" NAME="hidKeyValues">
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
							<input type="hidden" name="hidVoidClicked" runat="server" id="hidVoidClicked"> <input type="hidden" id="hidDelString" name="hidDelString" runat="server">
							<input id="hidCheckedRowIDs" type="hidden" runat="server" NAME="hidCheckedRowIDs">
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
							
						</td>
					</tr>
				</table>
			</div>
			<DIV>&nbsp;</DIV>
		</form>
	</body>
</HTML>
