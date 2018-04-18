<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Page language="c#" Codebehind="ErrorDetail.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.Aspx.ErrorDetail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ErrorDetail</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<script language="javascript">
	
		function onRowClicked(num,msDg )
		{
			rowNum = num;
			if(parseInt(num)==0)
				strXML="";
			populateXML(num,msDg);	
			changeTab(0, 0);
			
		}
		
		function ShowHideTable()
		{
			//alert(document.getElementById('tblErrorDetail').style.display);
			//document.getElementById('tblErrorDetail').style.display="none";
		}
		</script>
	</HEAD>
	<body  oncontextmenu = "return false;" leftmargin="0" topmargin="0" MS_POSITIONING="GridLayout">
		<FORM id="ERROR_DETAIL" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<TR>
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<TR>
										<TD class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></TD>
									</TR>
									<TBODY id="tblErrorDetail" runat="server">
										<TR id="trOne">
											<TD class="midcolora" width="10%"><asp:label id="capEXCEPTION_ID" runat="server"><b>Exception Id</b></asp:label></TD>
											<TD class="midcolora" width="10%"><asp:label id="lblEXCEPTION_ID" runat="server"></asp:label></TD>
											<TD class="midcolora" width="10%"><asp:label id="capEXCEPTION_DATE" runat="server"><b>Exception Date</b></asp:label></TD>
											<TD class="midcolora" width="10%"><asp:label id="lblEXCEPTION_DATE" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="midcolora" width="10%"><asp:label id="capERRORDETAIL" runat="server"><b>Error Detail</b></asp:label></TD>
											<TD class="midcolora" width="10%" colspan="3"><asp:label id="lblERRORDETAIL" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="midcolora" width="10%"><asp:label id="capEXCEPTION_TYPE" runat="server"><b>Exception Type</b></asp:label></TD>
											<TD class="midcolora" width="10%" colspan="3"><asp:label id="lblEXCEPTION_TYPE" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="headerEffectSystemParams" colSpan="4">Details</TD>
										</TR>
										<TR>
											<TD class="midcolora" width="10%"><asp:label id="capCUSTOMER_ID" runat="server"><b>Customer Id</b></asp:label></TD>
											<TD class="midcolora" width="10%" colspan="3"><asp:label id="lblCUSTOMER_ID" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="midcolora" width="10%"><asp:label id="capAPP_ID" runat="server"><b>Application Id</b></asp:label></TD>
											<TD class="midcolora" width="10%"><asp:label id="lblAPP_ID" runat="server"></asp:label></TD>
											<TD class="midcolora" width="10%"><asp:label id="capAPP_VER_ID" runat="server"><b>Application Version Id</b></asp:label></TD>
											<TD class="midcolora" width="10%"><asp:label id="lblAPP_VER_ID" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="midcolora" width="10%"><asp:label id="capPOL_ID" runat="server"><b>Policy Id</b></asp:label></TD>
											<TD class="midcolora" width="10%"><asp:label id="lblPOL_ID" runat="server"></asp:label></TD>
											<TD class="midcolora" width="10%"><asp:label id="capPOL_VER_ID" runat="server"><b>Policy Version Id</b></asp:label></TD>
											<TD class="midcolora" width="10%"><asp:label id="lblPOL_VER_ID" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="midcolora" width="10%"><asp:label id="capCLAIM_ID" runat="server"><b>Claim Id</b></asp:label></TD>
											<TD class="midcolora" width="10%" colspan="3"><asp:label id="lblCLAIM_ID" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="midcolora" width="10%"><asp:label id="capSOURCE" runat="server"><b>Source</b></asp:label></TD>
											<TD class="midcolora" width="10%" colspan="3"><asp:label id="lblSOURCE" runat="server"></asp:label></TD>
										</TR>
										<TR>
											<TD class="midcolora" width="10%"><asp:label id="capDETAIL" runat="server"><b>Complete Details</b></asp:label></TD>
											<TD class="midcolora" width="10%" colspan="3"><asp:label id="lblDETAIL" runat="server"></asp:label></TD>
										</TR>
										<TR id="trBody" runat="server">
											<TD class="midcolora" colspan="4"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesvalidation="false"></cmsb:cmsbutton></TD>
											<%--<TD class="midcolora" align="center" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></TD>
										<TD class="midcolorr" align="center" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>--%>
										</TR>
										<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
										<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
										<INPUT id="hidEXCEPTIONID" type="hidden" name="hidEXCEPTIONID" runat="server"> <input type="hidden" name="hidErrMsg" id="hidErrMsg" runat="server">
										<TR>
										</TR>
									</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</FORM>
		<script>
		//if(document.getElementById('hidEXCEPTIONID').value!=" ")
			//RefreshWebGrid("1",document.getElementById('hidEXCEPTIONID').value);
			RefreshWebGrid("1","");
		</script>
	</body>
</HTML>
