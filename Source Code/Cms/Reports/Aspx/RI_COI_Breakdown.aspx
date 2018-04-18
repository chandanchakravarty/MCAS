<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RI_COI_Breakdown.aspx.cs" Inherits="Cms.Reports.Aspx.RI_COI_Breakdown" %>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RI-COI-Breakdown</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
		<script type="text/javascript" language="javascript">	
		
		
		function OpenPolicyLookup() {

		    var url = '<%=URL%>';
		    var strPolicy = document.getElementById('hidPolicy').value;
		    OpenLookupWithFunction(url, 'POLICY_APP_NUMBER', 'CUSTOMER_ID_NAME', 'hidPOLICYINFO', $("#txtPOLICY_NUMBER").text().trim(), 'DBPolicy', strPolicy, '', 'splitPolicy()');
        }
        //This function splits the policyid and policy version id and put it in different controls
        function splitPolicy() {
           
            if (document.getElementById("hidPOLICYINFO").value.length > 0) {

                var arr = document.getElementById("hidPOLICYINFO").value.split("~");
                $("#hidPOLICY_ID").val(arr[0]);
                $("#hidPOLICY_VERSION_ID").val(arr[1]);
                $("#txtPOLICY_NUMBER").val(arr[2]);
                $("#hidCUSTOMER_ID").val(arr[6]);
    
            }
        }

       //This function is used for opening a pop up window(Here RIbreakdown page is used as pop up)
        function open_popup() {
            var str;
            var POLICY_NUMBER = document.getElementById('txtPOLICY_NUMBER').value; // added by shubhanshu , for itrack 1379
            str = "/cms/Reports/Aspx/RIBreakdown.aspx?CUSTOMER_ID=" + $("#hidCUSTOMER_ID").val() + "&POLICY_ID=" + $("#hidPOLICY_ID").val() + "&POLICY_VERSION_ID=" + $("#hidPOLICY_VERSION_ID").val() + "&POLICY_NUMBER=" + POLICY_NUMBER;
            window.open(str, "RIBreakdown","resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50");
           }

    </script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" onload="top.topframe.main1.mousein = false;findMouseIn();">
		<form id="Form1" method="post" runat="server">
		  <DIV><webcontrol:Menu id="bottomMenu" runat="server"></webcontrol:Menu></DIV>	
			<!-- To add bottom menu -->
			
			<!-- To add bottom menu ends here -->
			<asp:Panel ID="Panel1" Runat="server">
				<TABLE class="tableWidth" cellSpacing="1" cellPadding="0" width="100%" align="center" border="0">
					<TR>
						<TD class="pageHeader" id="tdClientTop" colSpan="4">
							<webcontrol:GridSpacer id="grdSpacer" runat="server"></webcontrol:GridSpacer></TD>
					</TR>
					<TR>
						<TD class="headereffectCenter" colSpan="4"><asp:Label runat="server" ID="lblheader_field"></asp:Label></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolorc" colSpan="4"></TD>
					</TR>
					<TR>
						<TD class="midcolora" width="36%">
							<asp:label id="lblRI" runat="server"></asp:label></TD>
						<TD class="midcolora" width="64%" colSpan="3">
							<asp:TextBox id="txtPOLICY_NUMBER" runat="server" size="40" 
                                ontextchanged="txtPOLICY_NUMBER_TextChanged"></asp:TextBox><IMG id="imgAGENCY_NAME" style="CURSOR: hand" onclick="OpenPolicyLookup();" alt="" src="../../cmsweb/images/selecticon.gif"
								runat="server"></TD>
								 <asp:RequiredFieldValidator ID="rfvPOLICY_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_NUMBER"></asp:RequiredFieldValidator>
					</TR>
					
					<TR>
						<TD class="midcolorc" colSpan="4"><cmsb:cmsbutton class="clsbutton" OnClientClick="open_popup()" id="btnReport" Runat="server" ></cmsb:cmsbutton>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
                <input id="hidHierarchySelected" type="hidden" name="hidHierarchySelected" runat="server"/>
			    <input id="hidPOLICYINFO" type="hidden" runat="server"/>
			    <input id="hidPOLICY_ID" type="hidden" value="0" name="hidPOLICY_ID" runat="server"/> 
			    <input id="hidPOLICY_VERSION_ID" type="hidden" value="0" name="hidPOLICY_VERSION_ID" runat="server"/>
			    <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>  
			    <input id="hidPOLICY_NUM" type="hidden" name="hidPOLICY_NUM" runat="server"/>  
			    <input id="hidPolicy"  name="hidPolicy" type="hidden" runat="server" />
		</form>
	</body>
</HTML>

