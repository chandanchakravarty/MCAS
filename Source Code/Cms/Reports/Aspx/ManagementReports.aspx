<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagementReports.aspx.cs" Inherits="Cms.Reports.Aspx.ManagementReports" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<HTML  xmlns="http://www.w3.org/1999/xhtml">
	<HEAD>
		<title></title>
	
       <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script language="javascript">


    </script>
    <script language="javascript" type="text/javascript" >
        
        function ResetForm() {
            document.MANAGEMENT_REPORT.reset();
             return false;
        }
		
    </script>
	</HEAD>
	<body oncontextmenu = "return false;" scroll="yes" onload="top.topframe.main1.mousein = false;findMouseIn();ApplyColor();"
		MS_POSITIONING="GridLayout">
		<form id="MANAGEMENT_REPORT"  runat="server">
		  <DIV><webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu></DIV>	
			<!-- To add bottom menu -->
			
			<!-- To add bottom menu ends here -->
			
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					
                    <TR>
                  
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							&nbsp;</TD>
					</TR>
                    <TR>
						<TD class="headereffectCenter" colSpan="4"><asp:Label  runat="server" ID="lblheader_field"></asp:Label></TD>
					</TR>
                    <tr> <td class="midcolora" colspan="4"   > <pre> </pre></td> </tr>
                     
					<TR>
						<TD class="midcolora" >
                        <asp:Label runat="server" ID="lblreportname"  Text="Report Name" ></asp:Label><span id="spnReport_Name" class="mandatory">*</span>
                         </TD>
                        <TD class="midcolora"> 
                        <asp:DropDownList runat="server" ID="ddlReportName"                                
                                AutoPostBack="false" ></asp:DropDownList>
                        <asp:requiredfieldvalidator id="rfvddlReportName" runat="server" ControlToValidate="ddlReportName" Display="Dynamic"></asp:requiredfieldvalidator>
                        </TD>
                        <TD class="midcolora"> 
                         <asp:Label runat="server" ID="lblGroupBy" Text="Group By" ></asp:Label><span id="spngroupby" class="mandatory">*</span>
                        </TD>
                         <TD class="midcolora">
                          <asp:DropDownList runat="server" ID="ddlGroupby" ></asp:DropDownList>
                           <asp:requiredfieldvalidator id="rfvGroupby" runat="server" ControlToValidate="ddlGroupby" Display="Dynamic"></asp:requiredfieldvalidator>
                          </TD>
					</TR>
					<TR>
						<TD class="midcolora">
                          <asp:Label runat="server" ID="lblinitialdate" Text="Initial Date" ></asp:Label><span id="spn_initialdate" class="mandatory">*</span>
                         </TD>
                        <TD class="midcolora">
                        <asp:TextBox runat="server" ID="txtinitialDate" ></asp:TextBox><asp:hyperlink id="hlkInitialDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="ImgInitialDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink>
                            <asp:regularexpressionvalidator id="revinitialDate" Runat="server" Display="Dynamic" ErrorMessage=""
								ControlToValidate="txtinitialDate"></asp:regularexpressionvalidator>
                           <asp:requiredfieldvalidator id="rfvinitialdate" runat="server" ControlToValidate="txtinitialDate" Display="Dynamic"></asp:requiredfieldvalidator>
                         </TD>
                        <TD class="midcolora"> 
                          <asp:Label runat="server" ID="lblenddate" Text="End Date" ></asp:Label><span id="spn_enddate" class="mandatory">*</span>
                        </TD>
                         <TD class="midcolora"> 
                          <asp:TextBox runat="server" ID="txtenddate" ></asp:TextBox><asp:hyperlink id="hlkEndDate" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgEndDate" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink>
                            <asp:regularexpressionvalidator id="revenddate" Runat="server" Display="Dynamic" ErrorMessage="Please enter Date in 'mm/dd/yyyy' format."
								ControlToValidate="txtenddate"></asp:regularexpressionvalidator>
                                <asp:requiredfieldvalidator id="rfvinenddate" runat="server" ControlToValidate="txtenddate" Display="Dynamic"></asp:requiredfieldvalidator>

                                 <asp:comparevalidator id="cpvEND_Date"  
					             ControlToValidate="txtenddate" Display="Dynamic" Runat="server" 
                                 ControlToCompare="txtinitialDate" Type="Date" ErrorMessage=""
			                     Operator="GreaterThanEqual"  ></asp:comparevalidator>
                         </TD>
					</TR>
					<TR>
						<TD class="midcolora">
                         <asp:Label runat="server" ID="lbldivision" Text="Division" ></asp:Label>
                         </TD>
                        <TD class="midcolora">
                         <asp:DropDownList runat="server" ID="ddlDivision" ></asp:DropDownList>
                         </TD>
                        <TD class="midcolora"> </TD>
                         <TD class="midcolora"> </TD>
					</TR>
					<TR id="trDate">
						
					   <TD class="midcolora">
                         <asp:Label runat="server" ID="lblsusepAccLOB" Text="SUSEP Accounting LOB" ></asp:Label>
                         </TD>
                        <TD class="midcolora">
                         <asp:DropDownList runat="server" ID="ddlsusepAccLOB" ></asp:DropDownList>
                         </TD>
                        <TD class="midcolora"> </TD>
                         <TD class="midcolora"> </TD>
					</TR>
					<TR>
						 <TD class="midcolora">
                         <asp:Label runat="server" ID="lblPolicyNumber" Text="Policy No" ></asp:Label>
                         </TD>
                        <TD class="midcolora">
                         <asp:TextBox runat="server" ID="txtpolicynumber" MaxLength="20" ></asp:TextBox>
                         </TD>
                        <TD class="midcolora">
                         <asp:Label runat="server" ID="llbClaimNUmber" Text="Claim No" ></asp:Label>
                         </TD>
                        <TD class="midcolora">
                         <asp:TextBox runat="server" ID="txtClaimNo" MaxLength="8"  ></asp:TextBox>
                         </TD>
					</TR>
					<tr>
                        <TD class="midcolora">
                         <asp:Label runat="server" ID="lblbrokercode" Text="Broker Code" ></asp:Label>
                         </TD>
                        <TD class="midcolora">
                         <asp:TextBox runat="server" ID="txtbrokerCode" MaxLength="6"  ></asp:TextBox>
                         </TD>
                        <TD class="midcolora"> </TD>
                         <TD class="midcolora"> </TD>         
                    </tr>
					<TR>
						<TD class="midcolora">
                        
                         <CMSB:CMSBUTTON class="clsButton" id="btnResetReport"  runat="server" Text="Reset"></CMSB:CMSBUTTON>
                         </TD>
                        <TD class="midcolora">
                       <CMSB:CMSBUTTON class="clsButton" id="btnRunReport"  runat="server" Text="Run Report"></CMSB:CMSBUTTON>
							
                         </TD>
                        <TD class="midcolora"> </TD>

                         <TD class="midcolora"> </TD>
					</TR>
					<tr>
                      <TD class="midcolora"> </TD>
                        <TD class="midcolora"> </TD>
                        <TD class="midcolora"> </TD>
                         <TD class="midcolora"> </TD>
                    
                     </tr>

                     	<tr>
                        <td class="midcolora" colspan="4" align="center"><asp:Label ID ="LblErrorMsg" runat ="server" Visible ="false"></asp:Label></td> </tr>
				</TABLE>
               <div>
		   
               <asp:GridView ID="Gv_General" runat="server" AutoGenerateColumns="false">
       <Columns>
       </Columns>
       </asp:GridView>
			  </div>
		</form>
	</body>
</HTML>

