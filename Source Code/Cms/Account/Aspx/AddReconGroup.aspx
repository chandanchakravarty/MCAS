<%@ Page language="c#" Codebehind="AddReconGroup.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AddReconGroup" validateRequest=false  %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_RECONCILIATION_GROUPS</title>
		<meta content='Microsoft Visual Studio 7.0' name='GENERATOR'>
		<meta content='C#' name='CODE_LANGUAGE'>
		<meta content='JavaScript' name='vs_defaultClientScript'>
		<meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'>
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language='javascript'>
		
		//Opens the look up window
		function OpenNewLookup()
		{
		
			var url='<%=URL%>';
			var idField,valueField,lookUpTagName,lookUpTitle;			
			cmbType = document.getElementById("cmbRECON_ENTITY_TYPE");
			if(cmbType.selectedIndex == -1)
			{
				alert("Please select entity type.");	
			}
			else
			{
				var entityType = cmbType.options[cmbType.selectedIndex].value;			
				if(cmbType.options[cmbType.selectedIndex].value == "")
				{
					alert("Please select entity type.");				
				}
				else
				{			
				
					switch(entityType)
					{
					case 'CUST':
						idField			=	'CUSTOMER_ID';
						valueField		=	'Name';
						lookUpTagName	=	'CustLookupForm';
						lookUpTitle		=	'Customer Names';
						break;
					
					case 'AGN':
						idField			=	'AGENCY_ID';
						valueField		=	'Name';
						lookUpTagName	=	'Agency';
						lookUpTitle		=	"Agency Names";
						break;
					case 'VEN':
						idField			=	'VENDOR_ID';
						valueField		=	'COMPANY_NAME';
						lookUpTagName	=	'VendorCompLookup';
						lookUpTitle		=	"Vendor Names";
						break;
					}
					
					//OpenLookup( url,idField,valueField,'hidENTITY_ID','txtRECON_ENTITY_ID',lookUpTagName,lookUpTitle,'');
					OpenLookupCenterScreen( url,idField,valueField,'hidENTITY_ID','txtRECON_ENTITY_ID',lookUpTagName,lookUpTitle,'');
				}
			}
		}
		
		function AddData()
		{
			document.getElementById('hidGROUP_ID').value	=	'New';
			document.getElementById('txtRECON_ENTITY_ID').text = "";
			document.getElementById('cmbRECON_ENTITY_TYPE').options.selectedIndex = -1;
			ChangeColor();
			DisableValidators();
			if(document.getElementById('cmbRECON_ENTITY_TYPE').getAttribute("enabled"))
				document.getElementById('cmbRECON_ENTITY_TYPE').focus();
			document.getElementById('btnDelete').style.display = 'none';
			RemoveTab(1,this.parent);
			Url="AddReconGroup.aspx?" 
				+ "&";	
			DrawTab(1,this.parent,'Reconciliation',Url);
		}
		
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0')
			{
				var tempXML = document.getElementById('hidOldData').value;
				if(tempXML != "")
				{
					populateFormData(tempXML,ACT_RECONCILIATION_GROUPS);
				}
				else
				{
					AddData();
				}
			}
			
			SetTab();			
			return false;
		}
		   function SelectEntity()
		{        
        
            if(document.getElementById('cmbRECON_ENTITY_TYPE').value=="CUST" || document.getElementById('cmbRECON_ENTITY_TYPE').value=="AGN" ||document.getElementById('cmbRECON_ENTITY_TYPE').value=="VEN" )
		    {
		          document.getElementById("txtRECON_ENTITY_ID").value="" 		   
		    }		 
		}
		
		function SetTab()
		{
		 // var left = Math.floor((screen.availWidth - 200) / 2);
         // var top = Math.floor((screen.availHeight - 200) / 2);
			if (document.getElementById("hidOldData").value != "")
			{			   
				Url="ReconDetail.aspx?ENTITY_ID=" + document.getElementById("hidENTITY_ID").value
					+ "&ENTITY_TYPE=" + document.getElementById("cmbRECON_ENTITY_TYPE").options[document.getElementById("cmbRECON_ENTITY_TYPE").selectedIndex].value
					+ "&GRP_ID=" + document.getElementById("hidGROUP_ID").value
					+ "&";				
				DrawTab(2,this.parent,'Reconciled Items',Url);
				//alert(document.getElementById("hidGROUP_ID").value)					
			}			
			else
			{
			    //alert(document.getElementById("hidGROUP_ID").value)			    	
				RemoveTab(2, this.parent);							
			}
			
		}
		
	function formReset()
		{
			populateXML();
			DisableValidators();
			ChangeColor();
		}
		function confirmCommit()
		{
		var flag=confirm("Are you sure to commit this reconciliation and adjustment with policy.");
		if (flag==false)
			return false;
		else
			return true;
		}
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<FORM id='ACT_RECONCILIATION_GROUPS' method='post' runat='server'>
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
									mandatory</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tbody id="trBody" runat="server">
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capRECON_ENTITY_TYPE" runat="server">Entity Type</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList id='cmbRECON_ENTITY_TYPE' OnFocus="SelectComboIndex('cmbRECON_ENTITY_TYPE')" onchange=SelectEntity(); runat='server'>
										<asp:ListItem Value=''></asp:ListItem>
										<asp:ListItem Value='CUST'>Customer</asp:ListItem>
										<asp:ListItem Value='AGN'>Agency</asp:ListItem>
										<asp:ListItem Value='VEN'>Vendor</asp:ListItem>
									</asp:DropDownList><BR>
									<asp:requiredfieldvalidator id="rfvRECON_ENTITY_TYPE" runat="server" ControlToValidate="cmbRECON_ENTITY_TYPE"
										ErrorMessage="RECON_ENTITY_TYPE can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capRECON_ENTITY_ID" runat="server">Entity Name</asp:Label><span class="mandatory">*</span></TD>
								<TD class='midcolora' width='32%'>
									<asp:TextBox id='txtRECON_ENTITY_ID' runat='server' ReadOnly="True"></asp:TextBox>
									<IMG id="imgENTITY_ID" style="CURSOR: hand" alt="" src="../../cmsweb/images/selecticon.gif"
										runat="server">
									<BR>
									<asp:requiredfieldvalidator id="rfvRECON_ENTITY_ID" runat="server" ControlToValidate="txtRECON_ENTITY_ID" ErrorMessage="RECON_ENTITY_ID can't be blank."
										Display="Dynamic"></asp:requiredfieldvalidator>
								</TD>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capCREATED_DATETIME" runat="server">Created Date</asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:Label id='lblCREATED_DATETIME' runat='server' CssClass="LabelFont"></asp:Label>
								</TD>
								<TD class='midcolora' ColSpan='2'></TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id="btnCommit" runat="server" Visible="False" Text="Commit" ></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text='Delete'></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
						 </tbody>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidGROUP_ID" type="hidden" value="0" name="hidGROUP_ID" runat="server">
			<input type="hidden" id="hidENTITY_ID" name="hidENTITY_ID" runat="server">
		</FORM>
		<script>
				//alert(document.getElementById('hidGROUP_ID').value);			
				//alert(document.getElementById('hidFormSaved').value);			
			if(document.getElementById('hidGROUP_ID').value != '0' && document.getElementById('hidFormSaved').value != '0')
			{
				RemoveTab(1,this.parent);
				Url="AddReconGroup.aspx?GROUP_ID_NEW=" + document.getElementById("hidGROUP_ID").value
					+ "&";	
				DrawTab(1,this.parent,'Reconciliation',Url);
			}
			else
			{
				//alert(document.getElementById('hidGROUP_ID').value);			
				//alert(document.getElementById('hidFormSaved').value);			
				RemoveTab(1,this.parent);
				Url="AddReconGroup.aspx?" 
					+ "&";	
				DrawTab(1,this.parent,'Reconciliation',Url);
			}
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidGROUP_ID').value, false);
		</script>
	</BODY>
</HTML>
