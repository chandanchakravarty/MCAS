<%@ Page language="c#" Codebehind="ClaimsCountAdjusterSummary.aspx.cs" AutoEventWireup="false" Inherits="Cms.Claims.Aspx.Reports.ClaimsCountAdjusterSummary" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>Claims Count by Adjuster with Summary</title>
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

			
			function Assign(objCmb, objSelectedCmb)
			{					
			    
				var coll = document.getElementById(objCmb);
				var col2 = document.getElementById(objSelectedCmb);
				var selIndex = coll.options.selectedIndex;	
				var len = coll.options.length;
				var num=0;				
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					{
						num = i;
						var szSelectedDept = coll.options(i).value;
						var innerText = coll.options(i).text;
						col2.options[col2.length] = new Option(innerText,szSelectedDept)
						coll.remove(i);										
					}										
				}	
				
				len = coll.options.length;			
				
				if(num < len )
				{
					//col2.options(num).selected = true;
				}	
				else
				{
					if(num>0)
						col2.options(num - 1).selected = true;
				}				
			}
			
			function UnAssign(objCmb, objSelectedCmb)
			{					
				var UnassignableString = ""
				var Unassignable = UnassignableString.split(",")
				var gszAssignedString = ""				
				var Assigned = gszAssignedString.split(",")
				var coll = document.getElementById(objCmb);
				var col2 = document.getElementById(objSelectedCmb);
				var selIndex = coll.options.selectedIndex;
				var len = coll.options.length;		
				var num=0;						
				if(len==0) return;
				for (i = len- 1; i > -1 ; i--)
				{
					if((coll.options(i).selected == true) && (coll.options(i).value > 0))
					{	
							num = i;						
							var flag = true;
							var szSelectedDept = coll.options(i).value;
							var innerText = coll.options(i).text;
							for(j = 0; j < Unassignable.length ;j++)
							{
								for(k = 0; k < Assigned.length ;k++)
								{							
										if((szSelectedDept == Unassignable[j]) && (szSelectedDept == Assigned[k])) 
										{
											flag = false;
										}
								}
							}
							if(flag == true)
							{
								col2.options[col2.length] = new Option(innerText,szSelectedDept)					
								coll.remove(i);
															
							}
					}			
				}
				var len = coll.options.length;
				if(len<1) return;
				if(	num < len )
				{
					col2.options(num).selected = true;
				}	
				else
				{
					col2.options(num - 1).selected = true;
				}
			}
			
			function ResetTheForm()
			{
			/*	//ChangeColor();
				DisableValidators();	
				document.getElementById("hidRESET").value="1";				
				document.location.href = document.location.href;
				return false;*/
			}
			
			function CheckToDate(objSource , objArgs)
			{
				var FromDate=document.ADJUSTER_SUMMARY.txtFromDate.value;
				var ToDate=document.ADJUSTER_SUMMARY.txtToDate.value;
				objArgs.IsValid = DateComparer(ToDate,FromDate,jsaAppDtFormat);
			}
	
		</script>
	</HEAD>
	<BODY leftMargin="0" topMargin="0" onload="ApplyColor();">
		<FORM id="ADJUSTER_SUMMARY" method="post" runat="server">
			<TABLE cellSpacing='1' cellPadding='0' class="tableWidthHeader" border='0'>
				<tr>
					<TD class="pageHeader" width="100%" colSpan="4">Claims Count by Adjuster with 
						Summary</TD>
				</tr>
				<tr>
					<td class="midcolorc" align="center" colSpan="3"><asp:label id="lblMessage" runat="server" cssclass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolorc" align="left" width="18%"><asp:label id="capFromDate" runat="server">From :</asp:label></td>
					<td class="midcolorc" align="left" width="32%"><asp:textbox id="txtFromDate" runat="server" maxlength="10" size="12"></asp:textbox>
						<asp:hyperlink id="hlkFromDate" runat="server" CssClass="HotSpot">
							<asp:image id="imgFromDate" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink><br>
						<asp:regularexpressionvalidator id="revFromDate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic"></asp:regularexpressionvalidator>
					</td>
					<td class="midcolorc" align="left" width="18%"><asp:label id="capToDate" runat="server">To :</asp:label></td>
					<td class="midcolorc" align="left" width="32%"><asp:textbox id="txtToDate" runat="server" maxlength="10" size="12"></asp:textbox>
						<asp:hyperlink id="hlkToDate" runat="server" CssClass="HotSpot">
							<asp:image id="imgToDate" runat="server" ImageUrl="../../../CmsWeb/Images/CalendarPicker.gif"></asp:image>
						</asp:hyperlink><br>
						<asp:regularexpressionvalidator id="revToDate" runat="server" ControlToValidate="txtToDate" Display="Dynamic"></asp:regularexpressionvalidator>
						<asp:customvalidator id="csvToDate" ControlToValidate="txtToDate" Display="Dynamic" Runat="server" ClientValidationFunction="CheckToDate"></asp:customvalidator>
					</td>
				</tr>
				<tr>
					<td class="midcolorc" align="left" width="18%"><asp:label id="capPartyTypes" runat="server">Party Types</asp:label></td>
					<td class="midcolorc" align="left" width="32%" rowSpan="6"><asp:listbox id="cmbPartyTypes" Runat="server" Width="300px" Height="150px" SelectionMode="Multiple"></asp:listbox>
						<input class="clsButton" runat="server" onclick="javascript:Assign('cmbPartyTypes','cmbSelectedPartyTypes');"
							type="button" value=">>" name="btnAssignPartyTypes" id="btnAssignPartyTypes"><br>
						<br>
						<input class="clsButton" runat="server" onclick="javascript:UnAssign('cmbSelectedPartyTypes','cmbPartyTypes');"
							type="button" value="<<" name="btnUnAssignPartyTypes" id="btnUnAssignPartyTypes">
					</td>
					<td class="midcolorc" align="left" width="18%"><asp:label id="capSelectedPartyTypes" runat="server">Selected Party Types</asp:label></td>
					<td class="midcolorc" align="left" width="32%" rowSpan="6"><asp:listbox id="cmbSelectedPartyTypes" Runat="server" Width="300px" Height="150px" SelectionMode="Multiple"></asp:listbox></td>
				</tr>
			</TABLE>
			<table class="tableWidthHeader" border="0">
				<TBODY>
					<tr>
						<td class="midcolora" valign="middle" align="left" colspan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></td>
						<td class="midcolorr" vAlign="middle" align="left" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDisplay" runat="Server" text="Display"></cmsb:cmsbutton></td>
					</tr>
		</FORM>
		</TBODY></TABLE>
	</BODY>
</HTML>
