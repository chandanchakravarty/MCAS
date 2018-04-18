<%@ Page language="c#" Codebehind="ReinsuranceReports.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.Reinsurance.ReinsuranceReports" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Reinsurance Reports</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">			
		
		function initialize()
		{
				if(document.getElementById("hidsp_accep").value == '1')
					document.getElementById("trSpecialAccep").style.display = 'inline';
				else
					document.getElementById("trSpecialAccep").style.display = 'none';
		}
		function CheckPageNum(objSource, objArgs)
		{	
		
			if(document.getElementById('txtGOTO').value != "" && !isNaN(document.getElementById('txtGOTO').value))
			{
				if(eval(document.getElementById('txtGOTO').value) < 0 || eval(document.getElementById('txtGOTO').value) == 0)
				{
					objArgs.IsValid = false;
					return false;
				}
				var totalPages = <%=intTotalPages%>;
				if(document.getElementById('txtGOTO').value > 0 && eval(document.getElementById('txtGOTO').value) > totalPages)
				{
					objArgs.IsValid = false;
				}
				else
				{
					objArgs.IsValid = true;
				}
			}
			else
				objArgs.IsValid = false;
		}
		
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" leftMargin="0" onload="initialize();">
		<form id="ReinsuranceReports" method="post" runat="server">
			<table class="tableWidth" cellSpacing="1" cellPadding="0" align="center" border="0" style="Z-INDEX: 101; LEFT: 32px; POSITION: absolute; TOP: 16px">
				<TBODY>
					<tr>
						<td colSpan="4" style="FONT-WEIGHT: normal; FONT-SIZE: 30px; TEXT-TRANSFORM: none" align="center">Wolverine 
							Mutual Insurance Company</td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capAccount" runat="server"><B>In Account with</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">
							<asp:Label id="lblAccount" runat="server"></asp:Label></td>
					
					</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capContractNo" runat="server"><B>Contract #</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">
							<asp:Label id="lblContractNo" runat="server"></asp:Label></td>
						
					</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capContractDates" runat="server"><B>Contract Dates</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">	
							<asp:Label id="lblContractDates" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capTypeRein" runat="server"><B>Type of Reinsurance Report</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">	
							<asp:Label id="lblTypeRein" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capDesc" runat="server"><B>Description</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">	
							<asp:Label id="lblDesc" runat="server"></asp:Label></td>
						</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capForPeriod" runat="server"><B>For Period</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">	
							<asp:Label id="lblForPeriod" runat="server"></asp:Label></td>
						</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capTransCode" runat="server"><B>Transaction Codes</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">	
							<asp:Label id="lblTransCode" runat="server"></asp:Label></td>
					</tr>
					<tr id="trSpecialAccep">
						<td class="midcolora" width="18%">
							<asp:Label id="capSpecialAccep" runat="server"><B>SPECIAL ACCEPTANCE REPORT ONLY</B></asp:Label></td>
						<td class="midcolora" width="18%" colspan="3"></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capInsuranceValue" runat="server"><B>Total Insurance Value Report</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">	
							<asp:Label id="lblInsuranceValue" runat="server"></asp:Label></td>
						
					</tr>
				</TBODY>
				<tbody id="tbDataGrid" runat="server">
							<tr>
									<td class="midcolorc" colSpan="4" ><asp:label id="lblDatagrid" Runat="server"></asp:label></td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="4">
								<asp:datagrid id="dgReinReport" Runat="server" Width="100%" ItemStyle-CssClass="datarow" HeaderStyle-CssClass="headereffectCenter"
										AllowPaging="True" PageSize="10" PagerStyle-HorizontalAlign="Right" AutoGenerateColumns="FALSE"
										PagerStyle-CssClass="datarow" PagerStyle-Mode="NumericPages" OnPageIndexChanged="dgReinReport_Paging" PagerStyle-PageButtonCount="10"> 
										<COLUMNS>
											<ASP:BOUNDCOLUMN HeaderText="Policy Number" DataField="POL_NUM"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Name & Address"  DataField="NAME_ADD"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="State"   DataField="STATE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Policy Effective Date" DataField="EFF_DATE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Policy Expiration Date" DataField="EXP_DATE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Transaction Type" DataField="TRANS_TYPE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Change/Cancel Date" DataField="CHANGE_DATE"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Rein.Gross" DataField="REIN_GROSS"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Retain" DataField="RETAIN"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Reins Ceded" DataField="REINS_CEDED"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Policy Premium" DataField="POL_PREM"></ASP:BOUNDCOLUMN>
											<ASP:BOUNDCOLUMN HeaderText="Reins Premium" DataField="REIN_PREM"></ASP:BOUNDCOLUMN>
										</COLUMNS>
									</asp:datagrid>
									</td>
							</tr>
							<tr>
									<td class="midcolorc" colSpan="4" ><asp:label id="lblCurrentPage" Runat="server"></asp:label></td>
							</tr>
							
								<TR class="AlternateDataRow" Style="display:none">
									<TD vAlign="middle" align="center" colSpan="4">
										<TABLE width="100%">
											<tr align="center">
											<td><asp:label id="capGOTO" Runat="server">Go To Page :</asp:label>
											<asp:textbox id="txtGOTO" Runat="server" size="3"></asp:textbox>
											<cmsb:cmsbutton class="clsButton" id="btnGOTO" runat="server" Text="Go"></cmsb:cmsbutton><br>
											<asp:CustomValidator ID="csvGOTO" ErrorMessage="" Runat="server" ControlToValidate="txtGOTO" ClientValidationFunction="CheckPageNum"
											Display="Dynamic"></asp:CustomValidator>
									</td>
											
								</tr>
								<TR>
									<TD align="center">
										<asp:imagebutton id="btnPrevious" Runat="server" ImageUrl="../../images/prevoff.gif"></asp:imagebutton>
										<%--<asp:label id="lblCurrentPage" Runat="server"></asp:label>--%>
										<asp:imagebutton id="btnNext" Runat="server" ImageUrl="../../images/next.gif"></asp:imagebutton>
									</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</tbody>
				
				<TBODY >
					<tr colspan="4">
						<td class="midcolora" width="18%">
							<asp:Label id="capTotals" runat="server"><B>Totals</B></asp:Label>
						</td><td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%"></td>
					</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capSum" runat="server"><B>Summary</B></asp:Label>
						</td><td class="midcolora" width="18%">
							<asp:Label id="capTotWrittenPrem" runat="server"><B>Total Written Premium</B></asp:Label></td>
							<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%"></td>
						
					</tr>
					<tr>
						<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%">
							<asp:Label id="capTotReturnPrem" runat="server"><B>Total Return Premium</B></asp:Label></td>
							<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%"></td>
						
					</tr>
					<tr>
						<td class="midcolora" width="18%"></td>
						<td class="midcolorr" width="18%">
							<asp:Label id="capSubTotal" runat="server"><B>Sub Total</B></asp:Label></td>
						<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%"></td>	
						
					</tr>
					<tr>
						<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%">
							<asp:Label id="capLessCommission" runat="server"><B>Less Commission (%)</B></asp:Label></td>
							<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%"></td>
						
					</tr>
					<tr>
						<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%">
							<asp:Label id="capNetDue" runat="server"><B>Net Due (Major Participant)</B></asp:Label></td>
						<td class="midcolora" width="18%"></td>
						<td class="midcolora" width="18%"></td>
					</tr>
				</TBODY>
				<TBODY>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capDateGenerated" runat="server"><B>Date Generated</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">	
							<asp:Label id="lblDateGenerated" runat="server"></asp:Label></td>
						
					</tr>
					<tr>
						<td class="midcolora" width="18%">
							<asp:Label id="capRequestedBy" runat="server"><B>Requested By</B></asp:Label>
						</td><td class="midcolora" width="18%" colspan="3">
							<asp:Label id="lblRequestedBy" runat="server"></asp:Label></td>
						
					</tr>
					<tr>
						<td class="midcolorr" colSpan="4">
							<cmsb:cmsbutton class="clsButton" id="btnDoGeneral" runat="server" Text=" Do General Ledger Posting" CausesValidation="false" ></cmsb:cmsbutton>
						</td>
					</tr>
				</TBODY>
				<INPUT id="hidStringValue" type="hidden" value="0" name="hidStringValue" runat="server">
				<INPUT id="hidContract_num" type="hidden" value="0" name="hidContract_num" runat="server">
				<INPUT id="hidtran_type" type="hidden" value="0" name="hidtran_type" runat="server">
				<INPUT id="hidsp_accep" type="hidden" value="0" name="hidsp_accep" runat="server">
			</table>
		</form>
	</body>
</HTML>
