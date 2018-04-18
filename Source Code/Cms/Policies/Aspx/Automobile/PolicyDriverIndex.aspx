<%@ Page language="c#" Codebehind="PolicyDriverIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.DriverDetailsIndex" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Policy Driver Index</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK rel="stylesheet" type="text/css" href = "/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>
			.hide { OVERFLOW: hidden; TOP: 5px }
			.show { OVERFLOW: hidden; TOP: 5px }
			#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
	
		function SetTabs(pretab,loadPage)
		{
			if (strSelectedRecordXML != "")		//If record xml exists then adding the tabs
			{
				var CalledFrom	= '<%=strCalledFrom%>';	//Stores the refrence , where the form is called from				
				var CalledFor	= '<%=strCalledFor%>';	
				
				var objXmlHandler = new XMLHandler();
				var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);				
				if (tree != null)
				{
					//Fetching the values from xml to be passed in url as query string					
					var strPolicyId			= tree.getElementsByTagName('POLICY_ID')[0].firstChild.text;
					var strPolicyVersionId	= tree.getElementsByTagName('POLICY_VERSION_ID')[0].firstChild.text;
					var strCustomerId		= tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
					var strDriverID			= tree.getElementsByTagName('DRIVER_ID')[0].firstChild.text;								
					var Url="";
					if(CalledFor=='WAT' && CalledFrom=="UMB")										
						Url="PolicyAutoMVRIndex.aspx?CalledFrom=" + CalledFrom + "&CUSTOMER_ID="+strCustomerId+"&POLICY_ID="+strPolicyId+"&Operator=1&POLICY_VERSION_ID="+strPolicyVersionId+"&CALLEDFOR=" + CalledFor + "&"; 											
					else					
						Url="PolicyAutoMVRIndex.aspx?CalledFrom=" + CalledFrom + "&CUSTOMER_ID="+strCustomerId+"&POLICY_ID="+strPolicyId+"&Operator=0&POLICY_VERSION_ID="+strPolicyVersionId+"&CALLEDFOR=" + CalledFor + "&"; 											
						
					DrawTab(2,this,"MVR Information",Url,null,pretab,! loadPage); 
					
					if(CalledFrom=="UMB")
						RemoveTab(2,this);
					return;
				}
			}
			//Record not exists , hence only one tab should come
			//Hence removing other tabs if exists
			RemoveTab(2,this);	
			changeTab(0,0);
		}
	
			function copyApplicant()
			{
				window.open('/cms/policies/aspx/PolicyCopyApplicantDriver.aspx?CalledFrom=<%=strCalledFrom%>&CalledFor=<%=strCalledFor%>&CUSTOMER_ID=<%=strCustomerID%>&POLICY_ID=<%=strPolicyID%>&POLICY_VERSION_ID=<%=strPolicyVersionID%>' ,'CopyApplicants',600,300,'Yes','Yes','No','No','No');	
				//window.open(url);			
			}

			function copyApplicationDrivers()
			{	
				window.open('/cms/policies/aspx/AddCurrentPolExistingDriver.aspx?CalledFrom=<%=strCalledFrom%>&CalledFor=<%=strCalledFor%>&CUSTOMER_ID=<%=strCustomerID%>&POLICY_ID=<%=strPolicyID%>&POLICY_VERSION_ID=<%=strPolicyVersionID%>','CopyApplicants',null,"width=600,height=600,scrollbars=1,menubar=0,resizable=0");
			}
			
			//Added By Swarup on 15-Mar-2007 
			//for fetching MVR and Fetching UnDisclosed Drivers at pol level :Start
			
			function FetchMVR()
			{
				window.open('/cms/Policies/Aspx/PolMvrForm.aspx?LOB_ID=2&STATE_ID=14&CUSTOMER_ID=<%=strCustomerID%>&POL_ID=<%=strPolicyID%>&POLICY_VERSION_ID=<%=strPolicyVersionID%>&CalledFor=MVR',null,"width=600,height=600,scrollbars=0,menubar=0,resizable=1");
			}
			
			function FetchLossReport()
			{
				window.open('/cms/Policies/Aspx/PolicyLossReport.aspx?LOB_ID=2&STATE_ID=14&CUSTOMER_ID=<%=strCustomerID%>&POL_ID=<%=strPolicyID%>&POLICY_VERSION_ID=<%=strPolicyVersionID%>&CalledFor=MVR',null,"width=600,height=600,scrollbars=0,menubar=0,resizable=1");
			
			}
			
			function PriorLossTab()
			{
				//window.open('/cms/Application/priorloss/priorlossindex.aspx',null,"width=800,height=600,scrollbars=1,menubar=0,resizable=1");
				//window.open('/cms/Application/priorloss/priorlossindex.aspx',null,"width=900,height=800,scrollbars=1,menubar=0,resizable=1"); //width,height,scrollbar values changed by Sibin on 5 Dec 08 for Itrack Issue 5059
				window.open('/cms/Application/priorloss/priorlossindex.aspx',null,"width=900,height=800,scrollbars=0,menubar=0,resizable=1"); //scrollbar value changed by Sibin on 29 Dec 08 for Itrack Issue 5059
			}

			function FetchUnDiscloseDriver()
			{
				window.open('/cms/Policies/Aspx/PolMvrForm.aspx?LOB_ID=2&STATE_ID=14&CUSTOMER_ID=<%=strCustomerID%>&POLICY_ID=<%=strPolicyID%>&POLICY_VERSION_ID=<%=strPolicyVersionID%>&CalledFor=UDI',null,"width=600,height=600,scrollbars=0,menubar=0,resizable=0");				
			}

			// :End
			function CopySchRecords()
			{
				window.open('/cms/policies/aspx/PolCopyUmbSchRecords.aspx?CalledFrom=<%=strCalledFrom%>&CalledFor=DRIVERS&CUSTOMER_ID=<%=strCustomerID%>&POLICY_ID=<%=strPolicyID%>&POLICY_VERSION_ID=<%=strPolicyVersionID%>' ,'Copy',600,300,'Yes','Yes','No','No','No');	
			}

			function onRowClicked(num,msDg )
			{
				rowNum = num;
				rowNum=num;
				if(parseInt(num)==0)
					strXML="";
				populateXML(num,msDg);					
			}
		</script>
	</HEAD>
	<body leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
		MS_POSITIONING="GridLayout">
		
		<!-- To add bottom menu -->
		<webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu>
		<!-- To add bottom menu ends here -->
		<div id="bodyHeight" class="pageContent">
			<form id="indexForm" method="post" runat="server">
				<table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td>
							<table class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
								<tr>
									<td align="center"><asp:Label ID="capMessage" Runat="server" Visible="False" CssClass="errmsg"></asp:Label></td>
								</tr>
								<tr>
									<td id="tdWorkflow" class="pageHeader" colspan="4">
										<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
									</td>
								</tr>
								<tr>
									<TD id="tdClientTop" class="pageHeader" colSpan="4">
										<webcontrol:ClientTop id="cltClientTop" runat="server" width="100%"></webcontrol:ClientTop>
									</TD>
								</tr>
								<tr>
									<td id="tdGridHolder">
										<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer>
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
													<webcontrol:Tab ID="TabCtl" runat="server" TabURLs="AddDriverDetails.aspx?" TabTitles="Driver Details"
														TabLength="225"></webcontrol:Tab>
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
							<input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
							<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
							<input id="hide" type="hidden" name="ConVar"> <span id="singleRec"></span>
						</td>
					</tr>
				</table>
			</form>
		</div>
		<DIV></DIV>
	</body>
</HTML>

