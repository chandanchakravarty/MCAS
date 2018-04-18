<%@ Page language="c#" Codebehind="AddAccountingEntity.aspx.cs" AutoEventWireup="false" validateRequest=false  Inherits="Cms.CmsWeb.Maintenance.AddAccountingEntity" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddAccountingEntity</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
				if(document.getElementById('btnDelete'))
				    document.getElementById('btnDelete').setAttribute('disabled', true);
				if (document.getElementById('btnActivateDeactivate'))
				    document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);

				DisableValidators();
				//alert("AddData"+document.getElementById('hidREC_ID').value);
				
				
				
			}
		/*
		function populateXML()
			{
				
				if(document.getElementById('hidFormSaved').value == '0')
				{
				
					//alert(top.frames[1].frames[0].strXML);	
					//var tempXML;			
					if(top.frames[1].frames[0].strXML!="")
					{
						tempXML=top.frames[1].frames[0].strXML;
						//alert(tempXML);
						
						var objXmlHandler = new XMLHandler();
						var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('Table')[0]);
						
						document.getElementById('btnActivateDeactivate').setAttribute('value','Deactivate'); 
						//Enabling the activate deactivate button
						document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
						//document.getElementById('btnReset').setAttribute('disabled',true);
						
						//Removing the visiblity of reset button when in update mode
						//document.userType.btnReset.style.display = "none";
						document.getElementById('hidOldData').value		=	 top.frames[1].frames[0].strXML;
						
						//FillDepartment();
					//	FillProfitCenter();
						//alert(tempXML);									
						populateFormData(tempXML,AddAccountingEntity);
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
			}
			*/
function populateXML()
{
	var tempXML = document.getElementById('hidOldData').value;
	//alert(tempXML);
	if(document.getElementById('hidFormSaved').value == '0') {
	  
		//var tempXML;
		//Enabling the activate deactivate button
		if(tempXML!="") {
		   
			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
			if(document.getElementById('btnDelete'))
			document.getElementById('btnDelete').setAttribute('disabled',false);
			populateFormData(tempXML,AddAccountingEntity);
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
				var str = document.getElementById('hidDEPT').value;
				//alert(divId);
				var objXmlHandler = new XMLHandler();
				var tree = (objXmlHandler.quickParseXML(deptXml).getElementsByTagName('deptxml')[0]);
				var oOption;
				var i=0;
				//document.getElementById('cmbdivision').options[0].text="   Select Division    ";
								
								// Adding Heading to drop down							
								oOption=document.createElement("option");
								oOption.value = -1;
								oOption.text = str;  //"Select Department";
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
				var str = document.getElementById('hidPROFIT_CENTER').value;
				// Adding Heading to drop down
				oOption=document.createElement("option");
								oOption.value = -1;
								oOption.text = str;
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
	<body oncontextmenu = "return false;" class="bodyBackGround" leftMargin="0" topMargin="0" onload="populateXML(); ApplyColor();">
		<form id="AddAccountingEntity" method="post" runat="server">
			<TABLE width="100%" align="center" border="0">
				<tr>
					<TD class="pageHeader" width="100%" colSpan="4"><asp:Label ID="capMARKEDFIELD" runat="server"></asp:Label></TD><%--Please note that all fields marked 
						with * are mandatory--%>
				</tr>
				<tr>
					<td class="midcolorc" width="100%" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<tr>
					<TD class="midcolora" style="HEIGHT: 22px" width="18%"><asp:label id="capEntityName" runat="server"></asp:label><SPAN class="mandatory">*</SPAN></TD>
					<TD class="midcolora" style="HEIGHT: 22px" width="32%"><asp:label id="lblEntityName" runat="server"></asp:label></TD>
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
					<td class="midcolora" height="100%"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" Runat="server" CausesValidation="False"
							text="Activate/Deactivate"></cmsb:cmsbutton></td>
					<td class="midcolorr" align="right" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</TD></tr>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidDepartmentXml" type="hidden" name="hidDepartmentXml" runat="server">
			<INPUT id="hidProfitCenterXml" type="hidden" name="hidProfitCenterXml" runat="server">
			<INPUT id="hidDeptId" type="hidden" name="hidDeptId" runat="server"> <INPUT id="hidProfitCenterId" type="hidden" name="hidProfitCenterId" runat="server">
			<INPUT id="hidREC_ID" type="hidden" name="hidREC_ID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<input id="hidPROFIT_CENTER" type="hidden" name="hidDEPT" runat="server"/>
			<input id="hidDEPT" type="hidden" name="hidPROFIT_CENTER" runat="server"/>
			<!--Following hidden field holds IS_Active field value--><INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">
			<script>
			//alert(document.getElementById('hidFormSaved').value);
		
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidREC_ID').value,true);	
			//alert(document.getElementById('hidFormSaved').value);
			if(document.getElementById('hidFormSaved').value == '1')
			{
				FillDepartment();
				SelectComboOption('cmbDepartment',document.getElementById('hidDeptId').value)
				FillProfitCenter();
				SelectComboOption('cmbProfitCenter',document.getElementById('hidProfitCenterId').value)
			}
			if(document.getElementById('hidFormSaved').value == '2')
			{
				FillDepartment();
				//SelectComboOption('cmbDepartment',document.getElementById('hidDeptId').value)
				FillProfitCenter();
				//SelectComboOption('cmbProfitCenter',document.getElementById('hidProfitCenterId').value)
			}
			if (document.getElementById("hidFormSaved").value == "5")
			{
				/*Record deleted*/
				/*Refreshing the grid and coverting the form into add mode*/
				/*Using the javascript*/
				RefreshWebGrid("1","1"); 
				document.getElementById("hidFormSaved").value = "0";
				AddData();
				
			}
			</script>
		</form>
	</body>
</HTML>
