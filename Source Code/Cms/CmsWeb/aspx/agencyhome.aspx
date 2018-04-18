<%@ Page language="c#" Codebehind="agencyhome.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Aspx.agencyhome" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<HTML>
	<HEAD>
		<TITLE>BRICS</TITLE>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
			<script src="/cms/cmsweb/scripts/xmldom.js"></script>
			<script src="/cms/cmsweb/scripts/common.js"></script>
			<script src="/cms/cmsweb/scripts/form.js"></script>
			<script src="/cms/cmsweb/scripts/Calendar.js"></script>
			<LINK href="/cms/cmsweb/css/menu.css" type="text/css" rel="stylesheet">
			<STYLE>
			.hide { OVERFLOW: hidden; POSITION: absolute; TOP: 5px }
			.show { OVERFLOW: hidden; POSITION: absolute; TOP: 5px }
			#tabContents { LEFT: 60px; POSITION: absolute; TOP: 160px }
			</STYLE>
			<script language="javascript">
			
			function OpenPolicyLookup()
			{
				
				
				var url='<%=URL%>';
				var idField,valueField,lookUpTagName,lookUpTitle;	
			//	alert(document.getElementById('hidAGENCY_CODE').value);				
				OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtDecPage','AGENCYLOGIN','Policy',"@AGENCY_CODE='"+document.getElementById('hidAGENCY_CODE').value + "'",'openDecPages()');
			//	OpenLookup(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtPolicyNo','NFS','NFS','');
				
			
			}
			
			function openVehicleLookup()
			{
				
				var url='<%=URL%>';
				var idField,valueField,lookUpTagName,lookUpTitle;	
				//function OpenLookupWithFunction(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction)

				OpenLookupWithFunction(url,'POL_INFORM','POLICY_NUMBER','hidPOLICYINFO','txtDecPage','AGENCYLOGINAUTOID','Policy',"@AGENCY_CODE='"+document.getElementById('hidAGENCY_CODE').value + "'",'openAutoIdCard()');
			}
			
			function openDecPages()
			{
				var polinfo = document.getElementById('hidPOLICYINFO').value;
				var polsplit = new Array();
				polsplit = polinfo.split('^');
				var querystring = '&CUSTOMER_ID=' + polsplit[2] + '&POLICY_ID=' + polsplit[0] + '&POLICY_VER_ID=' + polsplit[1];
				var decpageurl = '../../application/aspx/DecPage.aspx?CalledFrom=POLICY&CALLEDFOR=DECPAGE&LOB_ID=' + polsplit[3] + querystring;
				var decpageselurl = 'AgencyDecSelect.aspx?CalledFrom=POLICY&CALLEDFOR=DECPAGE&LOB_ID=' + polsplit[3] + querystring;
			//	alert(decpageurl);
				window.open(decpageselurl);			
			}
			function openAutoIdCard()
			{
				var polinfo = document.getElementById('hidPOLICYINFO').value;
				var polsplit = new Array();
				polsplit = polinfo.split('^');
				var querystring = '&CUSTOMER_ID=' + polsplit[2] + '&POLICY_ID=' + polsplit[0] + '&POLICY_VER_ID=' + polsplit[1];
				var vehicleid = polsplit[6];
				var decpageurl = '../../application/aspx/DecPage.aspx?CalledFrom=POLICY&CALLEDFOR=AUTOCARD&LOB_ID=' + polsplit[3] + "&VEHICLE_ID="+ polsplit[6]  + querystring;
			//	alert(decpageurl);
				window.open(decpageurl);
			}
			function OpenPolicyPath(path,CustomerID,PolicyID,AppID,AppVersionID,PolicyLOB,PolicyVersionID)
			{
				path+= "customer_id=" + CustomerID + "&policy_id=" + PolicyID + "&App_id=" + AppID + "&app_version_id=" + AppVersionID + "&Policy_LOB=" + PolicyLOB + "&Policy_Version_ID=" + PolicyVersionID;						
				top.botframe.location.href=path;
			}
			
			function OpenClaimPath(path,CustomerID,PolicyID,ClaimID,PolicyLOB,PolicyVersionID)
			{
				top.topframe.callItemClicked('2','')
				
				path+= "CUSTOMER_ID=" + CustomerID + "&POLICY_ID=" + PolicyID + "&CLAIM_ID=" + ClaimID + "&LOB_ID=" + PolicyLOB + "&POLICY_VERSION_ID=" + PolicyVersionID;						
				top.botframe.location.href=path;
			}
			
			function openCustomerPath(path)
			{			
				top.botframe.location.href=path;
			}
			
			function OpenApplicationPath(path,customerid,appid,appversionid)
			{
				path+="customer_id=" + customerid + "&app_id=" + appid + "&app_version_id=" + appversionid			
				top.botframe.location.href=path;
			}
			
			function openQuickQuote(CustomerID,QuoteId,QuoteType,LOB_DESC,QNo,State)
			{
				top.topframe.callItemClicked('1','')
				//top.botframe.location.href = "../aspx/QuickQuoteLoad.aspx?customer_id=" + CustomerID + "&QQ_ID=" + QuoteId + "&QQ_TYPE=" + QuoteType + "&LOB_DESC=" + LOB_DESC + "&state_name1=" + State  + "&QQ_NUMBER=" + QNo ;
				top.botframe.location.href = "../aspx/QuickQuoteLoad.aspx?customer_id=" + CustomerID + "&QQ_ID=" + QuoteId + "&QQ_TYPE=" + QuoteType + "&LOB_DESC=" + LOB_DESC + "&state_name=" + State  + "&QQ_NUMBER=" + QNo ;
			}
			function NewCustomer()
			{
				top.topframe.callItemClicked('1','/cms/client/aspx/CustomerManagerSearch.aspx?');
				return false;
			}
			
			function writeCookie_1()
			{
				var cookieCustName="<%=cookieCustomerName_1%>";
				var cookieApplication="<%=cookieApplication_1%>";
				var cookiePolicy="<%=cookiePolicy_1%>";
				var cookieQQ="<%=cookieQQ_1%>";
				var cookieClaim="<%=cookieClaim_1%>";
				
				var cookieCustDate='<%=cookieCustDate_1%>';
				var cookieAppDate='<%=cookieAppDate_1%>';
				var cookiePolDate='<%=cookiePolDate_1%>';
				var cookieQQDate='<%=cookieQQDate_1%>';
				var cookieClaimDate='<%=cookieClaimDate_1%>';
								
			    if(cookieCustDate!='')
				document.getElementById('cookieDiv_1').innerHTML = "<br><li><B>Customer(s):</b></li>" + cookieCustName + " On " + cookieCustDate + "</td></tr>" ;
				
				if(cookieApplication!='')
				document.getElementById('cookieAppDiv_1').innerHTML = "<br><li><B>Application(s):</b></li>" + cookieApplication + " On " + cookieAppDate + "</td></tr>";
				if(cookiePolicy!='')
				document.getElementById('cookiePolDiv_1').innerHTML ="<br><li><B>Policies:</b></li>" + cookiePolicy + " On " + cookiePolDate + "</td></tr>";
				if(cookieQQ!='')
				document.getElementById('cookieQQDiv_1').innerHTML ="<br><li><B>Quick Quote(s):</b></li>" + cookieQQ + " On " + cookieQQDate + "</td></tr>";			
				if(cookieClaim!='')
				document.getElementById('cookieClaimDiv_1').innerHTML ="<br><li><B>Claim(s):</b></li>" + cookieClaim + " On " + cookieClaimDate + "</td></tr>";				
				
				
			}

			function writeCookie_2()
			{
				var cookieCustName="<%=cookieCustomerName_2%>";
				var cookieApplication="<%=cookieApplication_2%>";
				var cookiePolicy="<%=cookiePolicy_2%>";
				var cookieQQ="<%=cookieQQ_2%>";
				var cookieClaim="<%=cookieClaim_2%>";
				
				var cookieCustDate='<%=cookieCustDate_2%>';
				var cookieAppDate='<%=cookieAppDate_2%>';
				var cookiePolDate='<%=cookiePolDate_2%>';
				var cookieQQDate='<%=cookieQQDate_2%>';
				var cookieClaimDate='<%=cookieClaimDate_2%>';
								
			  if(cookieCustDate!='')
				document.getElementById('cookieDiv_2').innerHTML = cookieCustName + " On " + cookieCustDate + "</td></tr>" ;
				if(cookieApplication!='')
				document.getElementById('cookieAppDiv_2').innerHTML = cookieApplication + " On " + cookieAppDate + "</td></tr>";
				if(cookiePolicy!='')
				document.getElementById('cookiePolDiv_2').innerHTML =cookiePolicy + " On " + cookiePolDate + "</td></tr>";
				if(cookieQQ!='')
				document.getElementById('cookieQQDiv_2').innerHTML =cookieQQ + " On " + cookieQQDate + "</td></tr>";			
				if(cookieClaim!='')
				document.getElementById('cookieClaimDiv_2').innerHTML =cookieClaim + " On " + cookieClaimDate + "</td></tr>";					
				
				
			}

			function writeCookie_3()
			{
				var cookieCustName="<%=cookieCustomerName_3%>";
				var cookieApplication="<%=cookieApplication_3%>";
				var cookiePolicy="<%=cookiePolicy_3%>";
				var cookieQQ="<%=cookieQQ_3%>";
				var cookieClaim="<%=cookieClaim_3%>";
				
				var cookieCustDate='<%=cookieCustDate_3%>';
				var cookieAppDate='<%=cookieAppDate_3%>';
				var cookiePolDate='<%=cookiePolDate_3%>';
				var cookieQQDate='<%=cookieQQDate_3%>';
				var cookieClaimDate='<%=cookieClaimDate_3%>';
								
			   if(cookieCustDate!='')
				document.getElementById('cookieDiv_3').innerHTML = cookieCustName + " On " + cookieCustDate + "</td></tr>" ;
				if(cookieApplication!='')
				document.getElementById('cookieAppDiv_3').innerHTML = cookieApplication + " On " + cookieAppDate + "</td></tr>";
				if(cookiePolicy!='')
				document.getElementById('cookiePolDiv_3').innerHTML =cookiePolicy + " On " + cookiePolDate + "</td></tr>";
				if(cookieQQ!='')
				document.getElementById('cookieQQDiv_3').innerHTML =cookieQQ + " On " + cookieQQDate + "</td></tr>";			
				if(cookieClaim!='')
				document.getElementById('cookieClaimDiv_3').innerHTML =cookieClaim + " On " + cookieClaimDate + "</td></tr>";					
				
				
			}
			function func()
			{
				//NavigateUrl="javascript:top.topframe.callItemClicked('1','/cms/client/aspx/CustomerManagerSearcH.aspx?&CalledFor=Claim');"
				//top.topframe. ('1','/cms/client/aspx/CustomerManagerSearcH.aspx?&CalledFor=Claim');"
				
				return false;
			}
			function NewExtAgnUrl()
			{
				var url1='<%=agencyURL%>';	
				window.open(url1);
			}
			
			</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="" ms_positioning="GridLayout" marginheight="0"
		marginwidth="0">
		<webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<TABLE height="99%" cellSpacing="0" cellPadding="0" width="99.9%" border="0" class="midcolora" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="414" height="304">
					<form id="agencyhome" name="agencyHomeForm" runat="server">
						<TABLE cellSpacing="0" cellPadding="0" width="302" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="1" height="8"></TD>
								<TD width="301"></TD>
							</TR>
							<TR vAlign="top">
								<TD class="midcolora">
								<tr><br>
									<asp:Label>Switch Agency</asp:Label>
									<asp:dropdownlist id="cmbAGENCY_LIST" AutoPostBack="true" onfocus="SelectComboIndex('cmbAGENCY_LIST')" runat="server"></asp:dropdownlist>
								</tr>	
								</TD>
								<TD>
									<TABLE cellSpacing="1" cellPadding="1" width="300" border="0">
										<TR>
											<TD class="midcolorc">
												<asp:Calendar id="agencyCalendar" runat="server" Height="144px" Width="248px" ShowGridLines="True"
													BorderStyle="Inset"></asp:Calendar><br>
												<TABLE cellSpacing="0" cellPadding="0" width="248px" border="0">
													<TR>
														<TD colSpan="1" height="10"></TD>
													</TR>
													<TR>
														<TD class="midcolorc" vAlign="top" noWrap colSpan="2"><b>Recently Visited </b>
														</TD>
													</TR>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieDiv_1"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieDiv_2"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" Height="16px" noWrap colSpan="2">
															<div id="cookieDiv_3"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieAppDiv_1"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieAppDiv_2"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieAppDiv_3"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" vAlign="top" noWrap colSpan="2">
															<div id="cookiePolDiv_1"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" vAlign="top" noWrap colSpan="2">
															<div id="cookiePolDiv_2"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" vAlign="top" noWrap colSpan="2">
															<div id="cookiePolDiv_3"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieQQDiv_1"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieQQDiv_2"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieQQDiv_3"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieClaimDiv_1"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieClaimDiv_2"></div>
														</td>
													</tr>
													<tr>
														<td class="midcolora" noWrap colSpan="2">
															<div id="cookieClaimDiv_3"></div>
														</td>
													</tr>
												</TABLE>											
											</TD>
											<TD width="21"></TD>
											<TD class="midcolora">
												<TABLE class="midcolora" height="219" cellSpacing="1" cellPadding="1" width="632" border="0">
													<TR>
														<TD align=center>
															<asp:hyperlink id="hlkQuoteNew" runat="server" NavigateUrl="javascript:top.topframe.callItemClicked('1,1,0','/cms/client/aspx/customertabindex.aspx?');"
																Height="16px" Width="244px"><asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../Images/New_Customer.gif'></asp:Image><br>
																<asp:Label>Quick Quote New Customer</asp:Label></asp:hyperlink></TD>
														<TD align=center>
															<%--<asp:hyperlink id="hlkQuoteSearch" runat="server" NavigateUrl="javascript:top.topframe.callItemClicked('1','/cms/client/aspx/CustomerManagerSearch.aspx?CalledFrom=AGENCY&CalledFor=CUST');"--%>
															<asp:hyperlink id="hlkQuoteSearch" runat="server"  NavigateUrl="javascript:top.botframe.location.href = '/cms/client/aspx/CustomerManagerSearch.aspx?&CalledFor=AGENQUOTE&'"
																Height="16px" Width="244px"><asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../Images/Existing_Customer.gif'></asp:Image><br>
																<asp:Label>Quick Quote Existing Customer</asp:Label></asp:hyperlink></TD>
													</TR>
													<TR>
													</TR>
													<TR>
														<TD align=center>
															<asp:hyperlink id="hlkAppNew" runat="server" NavigateUrl="javascript:top.topframe.callItemClicked('1,1,0','/cms/client/aspx/customertabindex.aspx?');"
																Height="16px" Width="244px"><asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../Images/New_Customer.gif' ID="Image1"></asp:Image><br>
																<asp:Label>Application New Customer</asp:Label></asp:hyperlink></TD>
														<TD align=center>
															<%--<asp:hyperlink id="hlkAppSearch" runat="server" NavigateUrl="javascript:top.topframe.callItemClicked('1','/cms/client/aspx/CustomerManagerSearch.aspx?&CALLEDFROM=AGENCY&CALLEDFOR=CUST&');"--%>
															<asp:hyperlink id="hlkAppSearch" runat="server"  NavigateUrl="javascript:top.botframe.location.href = '/cms/client/aspx/CustomerManagerSearch.aspx?&CalledFor=AGENAPP&'"
																Height="16px" Width="244px"><asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../Images/Existing_Customer_1.gif'></asp:Image><br>
																<asp:Label>Application Existing Customer</asp:Label></asp:hyperlink></TD>
													</TR>
													<TR>
													</TR>
													<TR>
														<TD align=center>
															<asp:hyperlink id="hlkPolicy" runat="server" NavigateUrl="javascript:top.topframe.callItemClicked('1','/cms/client/aspx/CustomerManagerSearch.aspx?');"
																Height="16px" Width="244px"><asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../Images/Agency_System.gif'></asp:Image><br>
																<asp:Label>Policy</asp:Label></asp:hyperlink></TD>
														<TD align=center>
															<%--<asp:hyperlink id="hlkClaim" runat="server" NavigateUrl="javascript:top.topframe.callItemClicked('1','/cms/client/aspx/CustomerManagerSearch.aspx?CalledFor=Claim&');"--%>															
															<asp:hyperlink id="hlkClaim" runat="server"  NavigateUrl="javascript:top.botframe.location.href = '/cms/client/aspx/CustomerManagerSearch.aspx?&CalledFor=Claim'"
																Height="16px" Width="244px"><asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../Images/Make_PAyment.gif'></asp:Image><br>
																<asp:Label>Claim</asp:Label></asp:hyperlink></TD>
													</TR>
													<TR>
													</TR>
													<TR>
														<TD align=center>
															<asp:hyperlink id="hlkARInquiry" runat="server" NavigateUrl="javascript:top.botframe.location.href = '/cms/Account/Aspx/AR_Inquiry_Info.aspx?';"
																Height="16px" Width="244px"><asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../Images/Agency_Logout.gif'></asp:Image><br>
																<asp:Label>Account Inquiry</asp:Label></asp:hyperlink></TD>
														<TD align=center>
															<asp:hyperlink id="hlkDecPage" runat="server" NavigateUrl="javascript:OpenPolicyLookup();"
																Height="16px" Width="244px"><asp:textbox id="txtDecPage" Runat="server" Width = "0" MaxLength="8" size="10"></asp:textbox>
																<asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../images/selecticon.gif'></asp:Image><br>
																<asp:Label>Declaration Page</asp:Label></asp:hyperlink></TD>
													</TR>
													<TR>
														<TD align=center>
															<asp:hyperlink id="hlkCustPayFrmAgency" runat="server" NavigateUrl="javascript:top.botframe.location.href = '/cms/Account/Aspx/AddCustAgencyPayments.aspx?';"
																Height="16px" Width="244px"><asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../Images/Agency_Logout.gif' ID="Image2"></asp:Image><br>
																<asp:Label>Customer Payments from Agency</asp:Label></asp:hyperlink></TD>
													
														<TD align=center>
															<asp:hyperlink id="hlkAutoIdCard" runat="server" NavigateUrl="javascript:openVehicleLookup();"
																Height="16px" Width="244px"><asp:Image Width="60" Runat="Server" AlternateText='' ImageUrl='../Images/selecticon.gif' ID="Image3"></asp:Image><br>
																<asp:Label>Auto ID Card</asp:Label></asp:hyperlink></TD>
													</TR>
													<tr>
														<TD align=center>
															<asp:hyperlink id="hlkExternalAgnUrl" runat="server" NavigateUrl="javascript:NewExtAgnUrl();"
																Height="16px" Width="244px"><asp:Image Width="30" Runat="Server" AlternateText='' ImageUrl='../Images1/submit_app.gif' ID="Image4"></asp:Image><br>
																<asp:Label>Premium Destinations Travel</asp:Label></asp:hyperlink>
													  </TD>
													</tr>
													
												</TABLE>
											</TD>
										</TR>
										<TR>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
		</TABLE>
		<input id="hidPOLICYINFO" type="hidden" name="hidPOLICYINFO" runat="server">
		<input id="hidAGENCY_CODE" type="hidden" name="hidAGENCY_CODE" runat="server">
		<script>
		writeCookie_1();
		writeCookie_2();
		writeCookie_3();
		</script>
	</body>
</HTML>
