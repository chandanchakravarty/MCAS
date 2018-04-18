<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyEndorsement.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyEndorsement" validateRequest="false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Endorsements</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../cmsweb/scripts/xmldom.js"></script>
		<script src="../../cmsweb/scripts/common.js"></script>
		<script src="../../cmsweb/scripts/form.js"></script>
		<script>
			//This variable will be used by tab control, for checking ,whether save msg should be shown to user
			//while changing the tabs
			//True means Want to save msg should always be shown to user
			var ShowSaveMsgAlways = true;
			
			function ResetForm()
			{
				document.Form1.reset();
				return false;
			}
				//New function being added
			function DisableControls(strcbDelete,strRemarks)
			{
				 
				var lastIndex = strcbDelete.lastIndexOf('_');
				var prefix = strcbDelete.substring(0,lastIndex);
				var   editionDate=document.getElementById(prefix+'_ddlEDITIONDATE')
				//document.getElementById(strRemarks).style.display="none";
					
				//alert(strRemarks);
				if (document.getElementById(strcbDelete).checked == false )
				{
					document.getElementById(strRemarks).value="";
					document.getElementById(strRemarks).disabled=true;
						editionDate.disabled=true;
					/*lblLimit.style.display = "inline";
					lblLimit.innerText = 'N.A';
					ddlLimit.style.display = "none";
					alert("checked");*/
				}
				else
				{
					document.getElementById(strRemarks).disabled=false;
						editionDate.disabled=false;
					/*lblLimit.style.display = "none";
					lblLimit.innerText = 'N.A';
					ddlLimit.style.display = "inline";*/
				}
					
			}
			///////////////////////
			
			
		function populateAdditionalInfo(NodeName)
		{
			var tempXML=parent.strSelectedRecordXML;	
			var objXmlHandler = new XMLHandler();
			var unqID;
			if(tempXML==null || tempXML=="")
				return;
			var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('Table')[0]);					
			
			if(tree)
			{				
				for(i=0;i<tree.childNodes.length;i++)
				{
					if(tree.childNodes[i].nodeName==NodeName)
					{
						if(tree.childNodes[i].firstChild)
							unqID=tree.childNodes[i].firstChild.text;
					}					
				}
			}
			return unqID;
		}
		function populateInfo()
		{
			if (this.parent.strSelectedRecordXML == "-1")
			{
				setTimeout('populateInfo();',100);
				return;
			}
			var pageFrom = new String('<%=pageFrom%>');			
			
			//if(pageFrom.toUpperCase()=='HWAT' || pageFrom.toUpperCase()=='WWAT' || pageFrom.toUpperCase()=='UWAT')
			if(pageFrom.toUpperCase()=='WWAT')
			{
				document.getElementById('hidDataValue1').value =populateAdditionalInfo("MAKE");
				document.getElementById('hidDataValue2').value =populateAdditionalInfo("MODEL");					
				if(document.getElementById('hidDataValue1').value=='undefined')
					document.getElementById('hidDataValue1').value="";
				if(document.getElementById('hidDataValue2').value=='undefined')
					document.getElementById('hidDataValue2').value="";
				document.getElementById('hidCustomInfo').value=";Boat # = " + populateAdditionalInfo("BOAT_NO") + ";Boat Make = " + document.getElementById('hidDataValue1').value + ";Boat Model = " + document.getElementById('hidDataValue2').value;
			}
			else
			{
				document.getElementById('hidCustomInfo').value="";
				return;					
			}
			
			
		}
			
		</script>
		<script id="clientEventHandlersJS" language="javascript">
<!--

function hidREC_VEH_ID_onbeforeeditfocus() {

}

//-->
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" MS_POSITIONING="GridLayout" leftMargin="0" rightMargin="0" onload="populateInfo();">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<TD>
						<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>
					</TD>
				</tr>
				<tr>
					<td class="headereffectCenter"><asp:label id="lblTitle" runat="server">Endorsements</asp:label></td>
				</tr>
				<tr>
					<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False" EnableViewState="False"></asp:label></td>
				</tr>
				<tr>
					<td class="midcolora"><asp:datagrid id="dgEndorsements" runat="server" DataKeyField="VEHICLE_ENDORSEMENT_ID" AutoGenerateColumns="False"
							Width="100%">
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText=" Required /Optional">
									<ItemTemplate>
										<asp:Label ID="lblEND_ID" Visible="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ENDORSMENT_ID") %>'>
										</asp:Label>
										<asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Endorsement">
									<ItemTemplate>
										<asp:Label ID="lblENDORSEMENT" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ENDORSEMENT") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Remarks">
									<ItemTemplate>
										<asp:TextBox id=txtREMARKS Width="200px" onkeypress="MaxLength(this,500)" maxlength="500" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks") %>'>
										</asp:TextBox>&nbsp;<br>(Max 500 characters)
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Edition Date">
									<ItemStyle HorizontalAlign="Right" Width="20%"></ItemStyle>
									<ItemTemplate>
										<select id="ddlEDITIONDATE" Visible="True" Runat="server" ENDORSMENT_ID='<%# DataBinder.Eval(Container, "DataItem.ENDORSMENT_ID") %>' NAME="ddlEDITIONDATE">
										</select>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Selected" Visible="False"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidREC_VEH_ID" type="hidden" value="0" name="hidREC_VEH_ID" runat="server" language="javascript"
							onbeforeeditfocus="return hidREC_VEH_ID_onbeforeeditfocus()"> <INPUT id="hidPolID" type="hidden" name="hidPolID" runat="server">
						<INPUT id="hidPolVersionID" type="hidden" name="hidPolVersionID" runat="server">
						<INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server"> <INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidAPP_LOB" runat="server">
						<INPUT id="hidCoverageXML" type="hidden" name="hidCoverageXML" runat="server"> <INPUT id="hidCustomInfo" type="hidden" name="hidCustomInfo" runat="server">
						<INPUT id="hidDataValue1" type="hidden" name="hidDataValue1" runat="server"> <INPUT id="hidDataValue2" type="hidden" name="hidDataValue2" runat="server">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>











