<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Page language="c#" Codebehind="CustomerManagerIndex.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.CustomerManagerIndex" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CustomerManagerIndex</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="C#" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet" />
		<script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
			
	function ShowPopup(url)
	{
		var nuWin=window.open(url,'AttentionNotes','menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,width=600,height=200,top=286,left=240');
	}

			function showTab()
			{
				changeTab(0,'<%= LastVisitedTab %>');
				setMenu();
				//setTab();
			}
			
		function setTab()		
		{

			if (document.getElementById('hidCustomer_id').value != 'New')
			{
				var tabCount=0;
				//Added by Charles on 12-Mar-10 for Multilingual Support
				var arrCustAsst = '<%=CustAsst %>'.split(','); //Application,Policy,Claims,Attachment,To Do,Balance,AR Inquiry,Transaction Log			    
				
				// This is a wolverine user : value = 0
				if(document.getElementById('hidWolverineUser').value=="0" || document.getElementById('hidWolverineUser').value=="") //Wolverine User
				{
					if(document.getElementById('hidAgencyLogin').value=="0")
					{
						tabCount = 7;
						Url="../../cmsweb/maintenance/TransactionLogIndex.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCustomer_id').value + "&";
						DrawTab(tabCount--,top.frames[1],arrCustAsst[7],Url);
					}
					else
						tabCount = 7;
					
//					Url="/cms/account/aspx/AR_Inquiry_Info.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCUSTOMER_ID').value + "&";
//					DrawTab(tabCount--,top.frames[1],arrCustAsst[6],Url);
					
//					Url="/cms/account/aspx/CustomerBalance.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCUSTOMER_ID').value + "&";
//					DrawTab(tabCount--,top.frames[1],arrCustAsst[5],Url);
									
					Url="/cms/cmsweb/diary/index.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCustomer_id').value + "&";
					DrawTab(tabCount--,top.frames[1],arrCustAsst[4],Url);
					
					Url="../../cmsweb/maintenance/AttachmentIndex.aspx?CalledFrom=InCLT&EntityType=Customer&EntityId="+document.getElementById('hidCustomer_id').value + "&BackOption="+"Y"+"&";
					DrawTab(tabCount--,top.frames[1],arrCustAsst[3],Url);
					
					Url="/cms/Claims/aspx/SearchCustomerClaimIndex.aspx?CustomerID=" + document.getElementById('hidCustomer_id').value + "&WOLVERINE_USER=1&";
					DrawTab(tabCount--,top.frames[1],arrCustAsst[2],Url);
				}
				else //Non-wolverine user  : value = 1
				{
					if(document.getElementById('hidAgencyLogin').value=="0")
					{
						tabCount=9;
						Url="../../cmsweb/maintenance/TransactionLogIndex.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCustomer_id').value + "&";
						DrawTab(tabCount--,top.frames[1],arrCustAsst[7],Url);
					}
					else
						tabCount=6;
						
						
					
					Url="/cms/account/aspx/AR_Inquiry_Info.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCustomer_id').value + "&";
					DrawTab(tabCount--,top.frames[1],arrCustAsst[6],Url);
				//  Commented by  Swastika - iTrack #1509 (Agency login will not be shown these two tabs)
				/*	Url="/cms/account/aspx/CustomerBalance.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCUSTOMER_ID').value + "&";
					DrawTab(tabCount--,top.frames[1],'Balance',Url);
					
					Url="/cms/cmsweb/diary/index.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCUSTOMER_ID').value + "&";
					DrawTab(tabCount--,top.frames[1],'To Do',Url);
				*/	
					Url="../../cmsweb/maintenance/AttachmentIndex.aspx?CalledFrom=InCLT&EntityType=Customer&EntityId="+document.getElementById('hidCustomer_id').value + "&BackOption="+"Y"+"&";
					DrawTab(tabCount--,top.frames[1],arrCustAsst[3],Url);
					
					Url="/cms/Claims/aspx/SearchCustomerClaimIndex.aspx?CustomerID=" + document.getElementById('hidCustomer_id').value + "&WOLVERINE_USER=0&";
					DrawTab(tabCount--,top.frames[1],arrCustAsst[2],Url);
				}
				
				Url="../../application/policy/policyindex.aspx";
				DrawTab(tabCount--,top.frames[1],arrCustAsst[1],Url);				
				
				Url="../../application/aspx/applicationindex.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCustomer_id').value + "&";
				DrawTab(tabCount--,top.frames[1],arrCustAsst[0],Url);
				
			//	Url="../../cmsweb/aspx/QuickQuoteList.aspx?CalledFrom=InCLT&CUSTOMER_ID="+document.getElementById('hidCustomer_id').value + "&";
			//	DrawTab(tabCount--,top.frames[1],arrCustAsst[8],Url);
				
				
				//Added by Charles on 10-Mar-10 for Policy Page Implementation
				//RemoveTab(2,top.frames[1]);
				RemoveTab(1,top.frames[1]);				
				//Added till here
			}
			else
			{	
				if(document.getElementById('hidWolverineUser').value=="0" || document.getElementById('hidWolverineUser').value=="") //Wolverine User					
					RemoveTab(7,top.frames[1]);
				RemoveTab(6,top.frames[1]);
				RemoveTab(5,top.frames[1]);
				RemoveTab(4,top.frames[1]);
				RemoveTab(3,top.frames[1]);
				RemoveTab(2,top.frames[1]);
				
			}
		return false;
		}
		function DoBack()
		{
			document.location.href = "CustomerManagerSearch.aspx";
			return false;
		}
		function NewWin()
		{
		//alert('dfd');
		document.location.href = "/cms/client/aspx/customertabIndex.aspx?CalledFrom=Direct&CUSTOMER_ID="+document.getElementById('hidCustomer_id').value + "&";
		return false;
		}
		function setMenu()
		{
			top.topframe.enableMenu("1,1");	
			top.topframe.enableMenu("1,2");	
			
			top.topframe.enableMenu("1,4");	
			top.topframe.enableMenu("1,5");	
			//top.topframe.enableMenu("1,6");	
			top.topframe.enableMenu("1,7");	
			
			top.topframe.enableMenu("1,1,2");	
			top.topframe.enableMenu('1,1,1');
			top.topframe.enableMenu('1,1,2');
			var appID = '<%=GetAppID()%>';
			var systemID = '<%=GetSystemId()%>';//Added for Itrack Issue 6181 on 3 Aug 2009
			if (appID == "")
			{
				//Disanling the application level menus
				top.topframe.disableMenu('1,2,2');
				top.topframe.disableMenu('1,2,3');
				top.topframe.disableMenu('1,3');
				
				top.topframe.disableMenu('1,6');
				//Added for Itrack Issue 6181 on 3 Aug 2009
				if(systemID!='<%=CarrierSystemID.ToUpper() %>')//'W001')
				  top.topframe.disableMenus("1,5","ALL");
			}
		}
		
		
		</script>
	</HEAD>
	<body oncontextmenu="return false;" onload="setTab(); top.topframe.main1.activeMenuBar='1,2';top.topframe.main1.mousein = false;findMouseIn();showScroll();showTab();"
		MS_POSITIONING="GridLayout">
		<!-- To add bottom mecnu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<div class="pageContent" id="bodyHeight">
			<form id="Form1" method="post" runat="server">
				<table class="tableWidth" cellSpacing="1" cellPadding="1" align="center" border="0">
					<tr>
						<td><webcontrol:gridspacer id="Gridspacer2" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td colspan="7" class="headereffectcenter"><asp:Label id="lblHeader" runat="server">Customer Assistant</asp:Label></td>
					</tr>
					<tr>
						<td><webcontrol:gridspacer id="Gridspacer3" runat="server"></webcontrol:gridspacer></td>
					</tr>
					<tr>
						<td class="HeadRow" width="50%" colSpan="2" BACKGROUND="/cms/cmsweb/images<%=GetColorScheme()%>/blue_curve_bg.jpg"><asp:label id="lblNameAddress" Runat="server">Name & Address</asp:label></td>
						<td class="HeadRow" width="50%" colSpan="4" BACKGROUND="/cms/cmsweb/images<%=GetColorScheme()%>/blue_curve_bg.jpg"><asp:label id="lblContacts" Runat="server">Contact</asp:label></td>
					</tr>
					<tr>
						<td class="midcolora" width="15%"><asp:label id="lblNameHead" Runat="server" CssClass="LabelFont">Name:</asp:label></td>
						<td class="midcolora" width="35%"><asp:label id="lblName" Runat="server"></asp:label><A 
            href="javascript:ShowPopup('/Cms/Client/Aspx/AttentionNotes.aspx?ClientID=<%=hidCustomer_id.Value%>')">
								<asp:Image id="AspImageNote" Visible="True" Runat="server" BorderWidth="0" BorderStyle="None"
									ImageAlign="absMiddle"></asp:Image></A></td>
						<td class="midcolora" width="15%"><asp:label id="lblHomeHead" Runat="server" CssClass="LabelFont">Home:</asp:label></td>
						<td class="midcolora" width="35%" colspan="3"><asp:label id="lblHomePhone" Runat="server"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolora" width="15%"><asp:label id="lblAddressHead1" Runat="server" CssClass="LabelFont">Address 1:</asp:label></td>
						<td class="midcolora" width="35%"><asp:label id="lblAddress1" Runat="server"></asp:label></td>
						<td class="midcolora" width="15%"><asp:label id="lblWorkHead" Runat="server" CssClass="LabelFont" style="display:none">Business:</asp:label></td>
						<td class="midcolora" width="35%" colspan="3"><asp:label id="lblWorkPhone" Runat="server" style="display:none"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolora" width="15%"><asp:label id="lblAddressHead2" Runat="server" CssClass="LabelFont">Address 2:</asp:label></td>
						<td class="midcolora" width="18%"><asp:label id="lblAddress2" Runat="server"></asp:label></td>
						<td class="midcolora" width="15%"><asp:label id="lblFaxHead" Runat="server" CssClass="LabelFont">Fax:</asp:label></td>
						<td class="midcolora" width="35%" colspan="3"><asp:label id="lblFax" Runat="server"></asp:label></td>
					</tr>
					<tr>
						<td class="midcolora" width="15%"><asp:label id="lblCityHead" Runat="server" CssClass="LabelFont">City, State and Zip:</asp:label></td>
						<td class="midcolora" width="18%"><asp:label id="lblCity" Runat="server"></asp:label></td>
						<td class="midcolora" width="33%" colSpan="4"></td>
					</tr>
					<tr>
						<td class="midcolora"><cmsb:cmsbutton id="btnEdit" Runat="server" Text="Edit" CssClass="clsButton"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnBackToSearch" runat="server" Text="Back To Search"></cmsb:cmsbutton></td>
					</tr>
				</table>
				<table id="tblTabCtlTable" class="tableWidth" cellSpacing="0" cellPadding="0" align="center"
					border="0">
					<tr id="tabCtlRow">
						<td><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer>
							<webcontrol:tab id="TabCtl" runat="server" TabLength="100" TabTitles="Quick Quote" TabURLs=""></webcontrol:tab></td>
					</tr>
					<tr>
						<td>
							<table class="tableeffect" cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td><iframe class="iframsHeightExtraLong" id="tabLayer" src="" frameBorder="0" width="100%" scrolling="no"
											runat="server"></iframe>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<asp:label id="lblTemplate" Runat="server" Visible="false"></asp:label></table>
				<input id="hidCustomer_id" name="hidCustomer_id" type="hidden" runat="server"/> 
				<input id="hidApp_id" name="hidApp_id" type="hidden" runat="server"/>
				<input id="hidAppVersion_id" name="hidAppVersion_id" type="hidden" runat="server"/>
				<input id="hidWolverineUser" name="hidWolverineUser" type="hidden" runat="server"/>
				<input id="hidAgencyLogin" name="hidAgencyLogin" value="0" type="hidden" runat="server"/>
			</form>
		</div>
	</body>
</HTML>