<%@ Page language="c#" Codebehind="AddVINMasterPopup.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.Aspx.AddVINMasterPopup" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS -Search Vehicle Information</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js">
		</script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/webcommon.js"></script>
		<script>
		
		if(("<%=gStrCalledFrom%>")=="PPA"||("<%=gStrCalledFrom%>")=="MOT" ||("<%=gStrCalledFrom%>")=="UMB")
		{
			var Year="<%=gStrYear%>";
			
			if(window.opener.document.getElementById("txtVEHICLE_YEAR"))
				window.opener.document.getElementById("txtVEHICLE_YEAR").value = Year ;
			 
			if(window.opener.document.getElementById("txtMAKE"))
				window.opener.document.getElementById("txtMAKE").value = "<%=gStrMake%>" ;
			 
			if(window.opener.document.getElementById("txtMODEL"))
				window.opener.document.getElementById("txtMODEL").value = "<%=gStrModel%>" ;
			
			if(window.opener.document.getElementById("txtBODY_TYPE"))
				window.opener.document.getElementById("txtBODY_TYPE").value = "<%=gStrBodyType%>" ;
			 
			if(window.opener.document.getElementById("txtVIN"))
				window.opener.document.getElementById("txtVIN").value = "<%=gStrVIN%>" ;						
			
			if(window.opener.document.getElementById("txtSYMBOL"))
				window.opener.document.getElementById("txtSYMBOL").value = "<%=gStrSymbol%>";
				
			if(window.opener.document.getElementById("hidMakeCode"))
				window.opener.document.getElementById("hidMakeCode").value = "<%=gStrMakeCode%>" ;
			
			if((window.opener.document.getElementById("cmbVehicle")!='undefined') &&  (window.opener.document.getElementById("cmbVehicle")!=null))
				window.opener.document.getElementById("cmbVehicle").selectedIndex = 0;
			 
			
				
			
			var lStrAntiLock='<%=gStrAntiLock%>';
			if(window.opener.document.getElementById("cmbANTI_LOCK_BRAKES"))
				window.opener.document.getElementById("cmbANTI_LOCK_BRAKES").selectedIndex=-1;
			if(lStrAntiLock!='' && window.opener.document.getElementById("cmbANTI_LOCK_BRAKES"))
			{
				if(lStrAntiLock=="N" || lStrAntiLock=="n")
				{
					for(i=0;i<window.opener.document.getElementById("cmbANTI_LOCK_BRAKES").options.length;i++)
					{
						if(window.opener.document.getElementById("cmbANTI_LOCK_BRAKES").options[i].text=="No")
						{
							window.opener.document.getElementById("cmbANTI_LOCK_BRAKES").selectedIndex=i;
							break;	
						}		
					}		
				}
				else
				{
					for(i=0;i<window.opener.document.getElementById("cmbANTI_LOCK_BRAKES").options.length;i++)
					{
						if(window.opener.document.getElementById("cmbANTI_LOCK_BRAKES").options[i].text=="Yes")
						{
							window.opener.document.getElementById("cmbANTI_LOCK_BRAKES").selectedIndex=i;
							break;	
						}		
					}		
				}			
			}			
			var lStrAirBag='<%=gStrAirBag%>';			
			if(window.opener.document.getElementById("cmbAIR_BAG"))
			{
				window.opener.document.getElementById("cmbAIR_BAG").selectedIndex=-1;
			
				for(i=0;i<window.opener.document.getElementById("cmbAIR_BAG").options.length;i++)
				{	
					if(window.opener.document.getElementById("cmbAIR_BAG").options[i].value==lStrAirBag)
					{				
						window.opener.document.getElementById("cmbAIR_BAG").selectedIndex=i;											
						break;
					}
				}
			}
			
			if(isNaN(Year))
			{
				if(window.opener.document.getElementById("txtVEHICLE_AGE"))
					window.opener.document.getElementById("txtVEHICLE_AGE").value = '0' ;			 
			}
			else
			{
				var today = new Date();			
				var age = today.getYear()-Year;	
				/*if(parseInt(age)>6)
				{
				//document.getElementById('hidAge').value='Rated as 6 Year Old';
				window.opener.document.getElementById("txtVEHICLE_AGE").value = document.getElementById('hidAge').value ;			 
				}
				else*/
				if(window.opener.document.getElementById("txtVEHICLE_AGE"))
				{
					window.opener.document.getElementById("txtVEHICLE_AGE").value = age ;			 				
					window.opener.GetAgeOfVehicle();
				}
			}
			window.opener.DisableValidators();
			window.opener.ChangeColor();
			window.close();
			
		}
		
		function AddVinNo()
		{
			var clfrom='<%=strCalledFrom%>'
			document.location.href="AddVinNo.aspx?CalledFrom="+clfrom;
		}		
		
		
		function SetVinIndex()
		{
			var index = document.getElementById("cmbVIN").selectedIndex;
			document.getElementById("hidVIN_INDEX").value = index;
		
		}
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" leftMargin='0' topMargin='0' onload='ApplyColor();ChangeColor();'>
		<form id='VINMasterPopup' method='post' runat='server'>
			<table cellSpacing='1' cellPadding='1' width='100%' border='0'>
				<br>
				<tr>
					<TD class="headerEffectSystemParams" colSpan="4" align="center">
                        <asp:label id="lblVehicleInfo" runat="server"></asp:label></TD>
				</tr>
				<%--Added for Itrack Issue 5680 on 14 April 2009--%>
				<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capVEHICLE_YEAR" runat="server"></asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:DropDownList id='cmbVEHICLE_YEAR' OnFocus="SelectComboIndex('cmbVEHICLE_YEAR')" runat='server'
							AutoPostBack="true"></asp:DropDownList><BR>
						<asp:requiredfieldvalidator id="rfvVEHICLE_YEAR" runat="server" ControlToValidate="cmbVEHICLE_YEAR" ErrorMessage="VEHICLE_YEAR can't be blank."
							Display="Dynamic"></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capMAKE" runat="server"></asp:Label><span class="mandatory">*</span></TD>
					<TD class='midcolora' width='32%'>
						<asp:DropDownList id='cmbMAKE' OnFocus="SelectComboIndex('cmbMAKE')" runat='server' AutoPostBack="true"></asp:DropDownList><BR>
						<asp:requiredfieldvalidator id="rfvMAKE" runat="server" ControlToValidate="cmbMAKE" ErrorMessage="MAKE can't be blank."
							Display="Dynamic"></asp:requiredfieldvalidator>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capMODEL" runat="server"></asp:Label></TD>
					<TD class='midcolora' width='32%'>
						<asp:DropDownList id='cmbMODEL' OnFocus="SelectComboIndex('cmbMODEL')" runat='server' AutoPostBack="true"></asp:DropDownList>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="capBODY_TYPE" runat="server"></asp:Label></TD>
					<TD class='midcolora' width='32%'>
						<asp:DropDownList id='cmbBODY_TYPE' OnFocus="SelectComboIndex('cmbBODY_TYPE')" runat='server' AutoPostBack="true"></asp:DropDownList>
					</TD>
				</tr>
				<tr>
					<TD class='midcolora' width='18%'>
						<asp:Label id="lblVIN" runat="server"></asp:Label></TD>
					<TD class='midcolora' width='32%'>
						<asp:DropDownList id="cmbVIN" OnFocus="SelectComboIndex('cmbVIN')" runat='server' onchange="SetVinIndex();" AutoPostBack="false"></asp:DropDownList>
					</TD>
				</tr>
				<tr>
					<td class='midcolora' align="left">
						<input type='button' id="btnAddNew" runat="server" onclick="AddVinNo()" value="Add New"
							class="clsButton" NAME="btnAddNew">
					</td>
					<td class='midcolorr' align="left">
						<asp:Button class="clsButton" id='btnSubmit' runat="server"></asp:Button>
					</td>
				</tr>
			</table>
			<input type="hidden" name="hidAge" id="hidAge"> <input type="hidden" name="hidAntiLockBrakes" runat="server" id="hidAntiLockBrakes">
			<input type="hidden" name="hidAirBags" runat="server" id="hidAirBags"> <input type="hidden" name="hidSymbol" runat="server" id="hidSymbol">
			<input type="hidden" name="hidModel" runat="server" id="hidModel"><input type="hidden" name="hidBodyType" runat="server" id="hidBodyType">
			<input type="hidden" name="hidVIN" runat="server" id="hidVIN">
			<input type="hidden" name="hidVIN_INDEX" runat="server" id="hidVIN_INDEX">
			<input type="hidden" name="hidVINYear" runat="server" id="hidVINYear">
			
		</form>
	</body>
</HTML>
