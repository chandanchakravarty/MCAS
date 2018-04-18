<%@ Page language="c#" Codebehind="AddDefaultHierarchy.aspx.cs" validateRequest=false  AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddDefaultHierarchy" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddDefaultHierarchy</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<SCRIPT src="/cms/cmsweb/scripts/common.js"></SCRIPT>
		<SCRIPT src="/cms/cmsweb/scripts/form.js"></SCRIPT>
		<script language="javascript">
		function AddData()
			{
				ChangeColor();
				
				FillDepartment();
				FillProfitCenter();
				
				document.getElementById('cmbDivision').options.selectedIndex	= 0;
				//document.getElementById('cmbDivision').options[].text	="Select Division";
				document.getElementById('cmbDepartment').options.selectedIndex	= 0;
				document.getElementById('cmbProfitCenter').options.selectedIndex	= 0;
				
												
				document.getElementById('hidREC_ID').value							='New';
				document.getElementById('hidFormSaved').value						='0';
											
				DisableValidators();
			
			}
		function populateXML()
			{
				var tempXML = document.getElementById('hidOldData').value;
				
				if(document.getElementById('hidFormSaved').value == '0')
				{
					//var tempXML;
					//Enabling the activate deactivate button
					if(tempXML!="" && tempXML!='<NewDataSet />')
					{
						if(document.getElementById('btnActivateDeactivate'))
						document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
						if(document.getElementById('btnDelete'))
						document.getElementById('btnDelete').setAttribute('disabled',false);
						populateFormData(tempXML,DefaultHierarchy);
						//alert(tempXML);
						FillDepartment();
						SelectComboOption('cmbDepartment',document.getElementById('hidDeptId').value)
						FillProfitCenter();
						SelectComboOption('cmbProfitCenter',document.getElementById('hidProfitCenterId').value)
									
					}
					else
					{
						AddData();
					}
				}
				return false;
		}

			function FillDepartment()
			{
				document.getElementById('cmbDepartment').innerHTML='';
				var str1 = "";
				var deptXml = document.getElementById('hidDepartmentXml').value;
				//alert(deptXml);
				
				var divId=document.getElementById('cmbDivision').options[document.getElementById('cmbDivision').selectedIndex].value ;
				
				//alert(divId);
				var objXmlHandler = new XMLHandler();
				var tree = (objXmlHandler.quickParseXML(deptXml).getElementsByTagName('deptxml')[0]);
				var oOption;
				var i=0;
				//document.getElementById('cmbdivision').options[0].text="   Select Division    ";
								
								// Adding Heading to drop down							
								oOption=document.createElement("option");
								oOption.value = -1;
								//oOption.text = "Select Department";
								var kr = document.getElementById('hidDepartment').value;
								oOption.text = kr.toString();
								
								
								
						document.getElementById('cmbDepartment').add(oOption);
								
				for(i=0;i<tree.childNodes.length;i++)
				{
					if(!tree.childNodes[i].firstChild) continue;
					
						var nodeName = tree.childNodes[i];//.getAttribute("idd");
						var checkDiv = tree.childNodes[i].getAttribute("idd");
						
						
						for(j=0;j<nodeName.childNodes.length;j++)
						{	
							//document.getElementById('cmbdepartment').options[0].text="   Select Division    ";
							if(divId==checkDiv)
							{						
								//alert(nodeName.childNodes[j].getAttribute("idd"));
								//alert(nodeName.childNodes[j].getAttribute("deptname"));
								//str1 = str1 + " <OPTION value=" + nodeName.childNodes[j].getAttribute("idd") + ">" + nodeName.childNodes[j].getAttribute("deptname") + "</OPTION>";
								
								oOption=document.createElement("option");
								
								oOption.value = nodeName.childNodes[j].getAttribute("idd");
								oOption.text = nodeName.childNodes[j].getAttribute("deptname");
								document.getElementById('cmbDepartment').add(oOption);
																
							}
						}
						
						
				}		
			}
			
			function FillProfitCenter()
			{
				
				document.getElementById('cmbProfitCenter').innerHTML='';
				var pcXml = document.getElementById('hidProfitCenterXml').value ;
				//alert(pcXml);
				var Deptid=document.getElementById('cmbDepartment').options[document.getElementById('cmbDepartment').selectedIndex].value;
					
				document.getElementById('hidDeptId').value = Deptid;
				
				// Adding Heading to drop down
				oOption=document.createElement("option");
								oOption.value = -1;
								//oOption.text = "Select Profit Center";
								var Pr = document.getElementById('hidProfit').value;
								oOption.text = Pr.toString();
								document.getElementById('cmbProfitCenter').add(oOption);
				if(document.getElementById('cmbDepartment').selectedIndex ==	0)
				{
				
				return false;
				}
				//alert(document.getElementById('hidDeptId').value);
				//alert(hidProfitCenterId);
				
				var objXmlHandler = new XMLHandler();
				var tree = (objXmlHandler.quickParseXML(pcXml).getElementsByTagName('profitxml')[0]);
				
				
				var oOption;
				var i=0;
				
								
								
				for(i=0;i<tree.childNodes.length;i++)
				{
					if(!tree.childNodes[i].firstChild) continue;
					var nodeName = tree.childNodes[i];//.getAttribute("idd");
					var checkDept = tree.childNodes[i].getAttribute("idd");
					
					for(j=0;j<nodeName.childNodes.length;j++)
					{
						if(Deptid == checkDept)
						{
							
								oOption=document.createElement("option");
								oOption.value = nodeName.childNodes[j].getAttribute("idd");
								oOption.text = nodeName.childNodes[j].getAttribute("pcname");
								document.getElementById('cmbProfitCenter').add(oOption);
						}
					}
				}
				
			}
			
			function GetProfitCenterId()
			{
				var pcid=document.getElementById('cmbProfitCenter').options[document.getElementById('cmbProfitCenter').selectedIndex].value;
				document.getElementById('hidProfitCenterId').value = pcid;
								//alert(document.getElementById('hidProfitCenterId').value);
			}
			
			function CheckDepartment(source,arguments)
			{			
				if (document.getElementById('cmbDepartment').options.selectedIndex==0)
				{
					arguments.IsValid = false;
					return;				
				}	
			}
			function CheckProfitCenter(source,arguments)
			{
				if (document.getElementById('cmbProfitCenter').options.selectedIndex==0)
				{
					arguments.IsValid = false;
					return;				
				}			
			}
			function CheckDivision(source,arguments)
			{
				if (document.getElementById('cmbDivision').options.selectedIndex==0)
				{
					arguments.IsValid = false;
					return;				
				}			
			}
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout" onload="populateXML(); ApplyColor();">
		<form id="DefaultHierarchy" method="post" runat="server">
			<TABLE width="100%" align="center" border="0">
				<tr>
					<TD class="pageHeader" width="100%" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label> </TD>
				</tr>
				<tr>
					<td class="midcolorc" width="100%" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capDivison" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDivision" runat="server"></asp:dropdownlist><BR>
						<asp:customvalidator id="cvDivision" ClientValidationFunction="CheckDivision" Display="Dynamic" ControlToValidate="cmbDivision"
							Runat="server"></asp:customvalidator></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capDepartment" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDepartment" runat="server"></asp:dropdownlist><BR>
						<asp:customvalidator id="cvDepartment" ClientValidationFunction="CheckDepartment" Display="Dynamic" ControlToValidate="cmbDepartment"
							Runat="server"></asp:customvalidator></TD>
				</tr>
				<tr>
					<TD class="midcolora" width="18%"><asp:label id="capProfitCenter" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbProfitCenter" runat="server"></asp:dropdownlist><BR>
						<asp:customvalidator id="cvProfitCenter" ClientValidationFunction="CheckProfitCenter" Display="Dynamic"
							ControlToValidate="cmbProfitCenter" Runat="server"></asp:customvalidator></TD>
				</tr>
				<tr>
					<td class="midcolora" height="100%"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton></td>
					<td class="midcolorr" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</TD></tr>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidDepartmentXml" type="hidden" name="hidDepartmentXml" runat="server">
			<INPUT id="hidProfitCenterXml" type="hidden" name="hidProfitCenterXml" runat="server">
			<INPUT id="hidDeptId" type="hidden" name="hidDeptId" runat="server"> <INPUT id="hidProfitCenterId" type="hidden" name="hidProfitCenterId" runat="server">
			<INPUT id="hidREC_ID" type="hidden" name="hidREC_ID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
			<input id="hidDepartment" type="hidden" name="hidDepartment" runat="server" />
			<asp:Label ID="capDepartmentid" runat="server"></asp:Label>
			<input id="hidDivision" type="hidden" name="hidDivision" runat="server" />
			<asp:Label ID="capDivisionid" runat="server"></asp:Label>
			<input id="hidProfit" type="hidden" name="hidProfit" runat="server" />
			<asp:Label ID="capProfit" runat="server"></asp:Label>
		
		<script>
		if(document.getElementById('hidFormSaved').value == '1')
			{
				FillDepartment();
				SelectComboOption('cmbDepartment',document.getElementById('hidDeptId').value)
				FillProfitCenter();
				SelectComboOption('cmbProfitCenter',document.getElementById('hidProfitCenterId').value)
			}
		</script>
		</form>
	</body>
</HTML>
