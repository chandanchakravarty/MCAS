<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="ClaimsCheckListingCriteriaPage.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.Reports.ClaimsCheckListingCriteriaPage" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>CLM_CLAIM_REPORT</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/Calendar.js"></script>
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		
		function Init()
		{
			ApplyColor();
			ChangeColor();
			setfirstTime();
			setfirstTime();
			top.topframe.main1.mousein = false;
			findMouseIn();			
		}
		function OpenReportsPopUp()
		{
			var QueryStringParams,Url;
			CompareEndDateWithStartDate();
			if(Page_IsValid)
			{
				// Get Order By						
				var LstLen = document.getElementById('lstSelOrderBy').options.length;
				var strOrderBy = new String();
				//var strOrderBy;				
				for(var i = 0;i<LstLen;i++)
					{					
					    strOrderBy = strOrderBy + document.getElementById('lstSelOrderBy').options[i].value + ',';
						
					}				
				OrderBy = strOrderBy.substr(0,strOrderBy.length-1); // remove ',' from last
				//alert(OrderBy);
				//Commemted For Itrack Issue #6386.
				/*if(OrderBy == '')
				{
					OrderBy="Check_Date"
				}*/
				
				QueryStringParams = "PageName=ClaimCheckListing&StartDate=" + document.getElementById("txtSTART_DATE").value;
				QueryStringParams+=  "&EndDate=" + document.getElementById("txtEND_DATE").value;				
				QueryStringParams+=  "&Check_From_Amount=" + replaceAll(document.getElementById("txtFROM_AMOUNT").value,",","");
				QueryStringParams+=  "&Check_To_Amount=" + replaceAll(document.getElementById("txtTO_AMOUNT").value,",","");
				QueryStringParams+= "&RptType=" + document.getElementById("hidReportType").value;
				QueryStringParams+= "&ORDERBY=" + OrderBy;		
				
				
				//alert(strOrderBy.substr(0,strOrderBy.length-1));
				//--------------
				url = "/cms/Reports/Aspx/CustomReport.aspx?" + QueryStringParams;
				//alert(url);
				//ShowPopup(Url,'ClaimsChecksListingReport', 600, 400);
				var windowobj = window.open(url,'mywindow','resizable=yes,scrollbar=yes,top=100,left=50')
				windowobj.focus();	
			}
				
		}
		function CompareEndDateWithStartDate()
		{
			document.getElementById("txtEND_DATE").value = FormatDateForGrid(document.getElementById("txtEND_DATE"),'');
			document.getElementById("txtSTART_DATE").value = FormatDateForGrid(document.getElementById("txtSTART_DATE"),'');
			var dob=document.getElementById("txtSTART_DATE").value;
			var expdate=document.getElementById("txtEND_DATE").value;
			var Result = true;
			var GreaterThanEqualComparision = 1;
			if (dob != "")
			{
				if(expdate=="")
					Result=true;
				else
					Result = CompareTwoDate(expdate,dob,jsaAppDtFormat,GreaterThanEqualComparision);
			}
			else
			{
				Result = true;
			}					
			if(Result)
				document.getElementById("spnEND_DATE").style.display="none";
			else
				document.getElementById("spnEND_DATE").style.display="inline";
						
			Page_IsValid = (Page_IsValid && Result);			
		}	
		function ChkEndDate(objSource , objArgs)
		{
			
			var expdate=document.CLM_CLAIM_REPORT.txtEND_DATE.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
			
		}			
		function ChkStartDate(objSource , objArgs)
		{
			
			var expdate=document.CLM_CLAIM_REPORT.txtSTART_DATE.value;
			objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",expdate,jsaAppDtFormat);
			
		}			
		function funcValidateSelOrderBy()
		{
			return true;
		}		
		function funcSetOrderBy()
		{
			var lstOrderBy = document.getElementById('lstOrderBy');	// ListBx 1
			var lstSelOrderBy = document.getElementById('lstSelOrderBy');// ListBx 2
			var i;
		
			for (i=lstOrderBy.options.length-1;i>=0;i--)
			{
				if(lstOrderBy.options[i].selected == true)
				{
					lstSelOrderBy.options.length = lstSelOrderBy.length + 1;
					lstSelOrderBy.options[lstSelOrderBy.length-1].value = lstOrderBy.options[i].value;
					lstSelOrderBy.options[lstSelOrderBy.length-1].text = lstOrderBy.options[i].text;
					lstOrderBy.options[i] = null;
				}
			}
		}
		function funcRemoveOrderBy()
		{
			var lstOrderBy = document.getElementById('lstOrderBy');	// ListBx 1
			var lstSelOrderBy = document.getElementById('lstSelOrderBy');// ListBx 2
			var i;
			
			for (i=lstSelOrderBy.options.length-1;i>=0;i--)
			{
			
				if (lstSelOrderBy.options[i].selected == true)
				{
					lstOrderBy.options.length=lstOrderBy.length+1;
					lstOrderBy.options[lstOrderBy.length-1].value=lstSelOrderBy.options[i].value;
					lstOrderBy.options[lstOrderBy.length-1].text=lstSelOrderBy.options[i].text;
					lstSelOrderBy.options[i] = null;
				}
			}
		}	
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="Init();">
		<!-- To add bottom menu --><webcontrol:menu id="bottomMenu" runat="server"></webcontrol:menu>
		<!-- To add bottom menu ends here -->
		<FORM id="CLM_CLAIM_REPORT" method="post" runat="server">
			<TABLE class="tableWidth" cellSpacing="0" cellPadding="0" width="100%"  align="center" border="0">
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="headereffectCenter" align="center" colSpan="4">
										<asp:Label ID="lblHeader" Runat="server"></asp:Label>
									</TD>
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="4"><!--Please note that all fields marked with * are 
										mandatory--></TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSTART_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtSTART_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkSTART_DATE" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgSTART_DATE" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:regularexpressionvalidator id="revSTART_DATE" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtSTART_DATE"
											Display="Dynamic"></asp:regularexpressionvalidator>			
										<asp:customvalidator id="csvSTART_DATE" Display="Dynamic" ControlToValidate="txtSTART_DATE" Runat="server"
											ClientValidationFunction="ChkStartDate"></asp:customvalidator>							
										</TD>
									<TD class="midcolora" width="18%"><asp:label id="capEND_DATE" runat="server"></asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtEND_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkEND_DATE" runat="server" CssClass="HotSpot">
											<ASP:IMAGE id="imgEND_DATE" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></ASP:IMAGE>
										</asp:hyperlink><br>
										<asp:regularexpressionvalidator id="revEND_DATE" runat="server" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtEND_DATE"
											Display="Dynamic"></asp:regularexpressionvalidator>
										<span id="spnEND_DATE" style="DISPLAY: none; COLOR: red" runat="server"></span>
										<asp:customvalidator id="csvEND_DATE" Display="Dynamic" ControlToValidate="txtEND_DATE" Runat="server"
											ClientValidationFunction="ChkEndDate"></asp:customvalidator>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capFROM_AMOUNT" runat="server">Amount</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtFROM_AMOUNT" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revFROM_AMOUNT" ControlToValidate="txtFROM_AMOUNT" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capTO_AMOUNT" runat="server">Amount</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtTO_AMOUNT" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revTO_AMOUNT" ControlToValidate="txtTO_AMOUNT" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
										<asp:CompareValidator ID="cmpTO_AMOUNT" Runat="server" ControlToValidate="txtTO_AMOUNT" ControlToCompare="txtFROM_AMOUNT" Display="Dynamic" 
											Operator="GreaterThanEqual" Type="Currency"></asp:CompareValidator>
									</TD>
								</tr>
								<!-- order by fields -->
								<!--Check_date has been change to Posting_date For Sorting For Itrack Issue 6893 -->
							<TR>
								<TD class="midcolora" colSpan="1"><asp:label id="lblOrderBy" runat="server">Order By</asp:label></TD>
								<TD class="midcolora" colSpan="1">
								<asp:ListBox id="lstOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="110px" AutoPostBack="false">
										<asp:ListItem Value="POSTING_DATE">Date</asp:ListItem>
										<asp:ListItem Value="CHECK_NUMBER">Check Number</asp:ListItem>
										<asp:ListItem Value="CHECK_AMOUNT">Amount</asp:ListItem>
										<asp:ListItem Value="CLAIM_NUMBER">Claim Number</asp:ListItem>
										<asp:ListItem Value="PAYMENT_TYPE">Payee Name</asp:ListItem>
										<asp:ListItem Value="MANUAL_CHECK">Manual Check</asp:ListItem>
								</asp:ListBox>
								</TD>
								<TD class="midcolora" vAlign="middle" colSpan="1">&nbsp;<asp:Button id="btnSel" Runat="server" Text=">>"></asp:Button><BR><BR>
									&nbsp;<asp:Button id="btnDeSel" Runat="server" Text="<<"></asp:Button>
								</TD>
								<TD class="midcolora" colSpan="1"><asp:ListBox id="lstSelOrderBy" onblur="funcValidateSelOrderBy" runat="server" Height="65px" SelectionMode="Multiple" Width="110px" AutoPostBack="False" onchange="funcValidateSelOrderBy">
									</asp:ListBox><br><asp:customvalidator id="csvSelOrderBy" Runat="server" ControlToValidate="lstSelOrderBy" Display="Dynamic" ClientValidationFunction="funcValidateSelOrderBy" Enabled="False"></asp:customvalidator><SPAN id="spnSelOrderBy" style="DISPLAY: none; COLOR: red">Please 
										select Order
										By.</SPAN> 
								</TD>
							</TR>
								<%--<tr>
									<TD class="midcolora" width="18%"><asp:label id="capLISTING_TYPE" runat="server">Amount</asp:label></TD>
									<TD class="midcolora" width="32%">
										<asp:RadioButtonList ID="rblLISTING_TYPE" Runat="server" CssClass="midcolora">
											<asp:ListItem>Clear</asp:ListItem>
											<asp:ListItem>Outstanding</asp:ListItem>
											<asp:ListItem>Issued</asp:ListItem>
										</asp:RadioButtonList>
									</TD>
								</tr>--%>
								<tr>
									<TD class="midcolorr" colSpan="4"><cmsb:cmsbutton class="clsButton" id="btnGenerateReport" runat="server" CausesValidation="true"
											Text="Generate Report"></cmsb:cmsbutton></TD>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidReportType" type="hidden" value="0" name="hidReportType" runat="server">
			<INPUT language="javascript" id="hidOldData" type="hidden" name="hidOldData" runat="server">
		</FORM>
	</BODY>
</HTML>
