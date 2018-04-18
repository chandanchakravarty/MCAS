<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="PolicyVehicleCoverageDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Aviation.PolicyVehicleCoverageDetails" EnableEventValidation="false" ValidateRequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Avaition Policy Coverages Details</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../../cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="../../../cmsweb/scripts/common.js"></script>
		<script src="../../../cmsweb/scripts/form.js"></script>
		<script src="../../../cmsweb/scripts/Coverages.js"></script>
		<script language="javascript">
			var prefix = "dgCoverages__ctl";
			var policyPrefix = "dgPolicyCoverages__ctl";
			var ShowSaveMsgAlways = true;
			
			function onButtonClick(chk,rowCount)
			{
				var span = chk.parentElement;
				var covID = 0;
				if ( span == null ) return;
				
				SetHiddenField(chk.id);
				var covID = span.getAttribute("COV_ID");
				var covCode = span.getAttribute("COV_CODE");
				
				var lastIndex1 = chk.id.lastIndexOf('__');
				var dgPrefix = chk.id.substring(0,lastIndex1);
				//var lobState = document.getElementById('hidLOBState').value;
				if ( chk.checked == true )
				{		
					var toDisable = GetControlInGridFromCode(covCode,'_hidCHECKDDISABLE');
					//alert(toDisable);
					if(toDisable==null) return;
					var lastIndex = toDisable.id.lastIndexOf('_');
					var prefix = toDisable.id.substring(0,lastIndex);
					var toEnable =  document.getElementById(prefix + '_hidCHECKDENABLE');
					var toUncheck = document.getElementById(prefix + '_hidCHECKDDSELECT');
					var toCheck =document.getElementById(prefix +'_hidCHECKDSELECT');
					
				}
				else if ( chk.checked == false )
				{
					var toDisable = GetControlInGridFromCode(covCode,'_hidUNCHECKDDISABLE');
					//alert(toDisable);
					if(toDisable==null)return;
					var lastIndex = toDisable.id.lastIndexOf('_');
					var prefix = toDisable.id.substring(0,lastIndex);
					var toEnable =  document.getElementById(prefix + '_hidUNCHECKDENABLE');
					var toUncheck = document.getElementById(prefix + '_hidUNCHECKDDSELECT');
					var toCheck =document.getElementById(prefix +'_hidUNCHECKDSELECT');
				}
				
				if(trim(toDisable.value) != "")
				{
					var toDisableArray =toDisable.value.split(",");
					for(i=0;i < toDisableArray.length ;i++)
					{
						var cbCTRL = GetControlInGridFromCode(toDisableArray[i],'_cbDelete');
						//alert(toDisableArray[i]);
						if (toDisableArray[i]=='UMPD')
						{
							var cbPUNCS = GetControlInGridFromCode('PUNCS','_cbDelete');
							if((cbPUNCS!=null && cbPUNCS.checked==true) || (cbPUMSP!=null && cbPUMSP.checked==true))
								continue;
						}
						if ( cbCTRL != null )
						{
							//cbCTRL.parentElement.parentElement.parentElement.disabled = true;
							//EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, true);
							cbCTRL.disabled = true;
						}
					}
				}
				if(trim(toEnable.value) != "")
				{
					var toEnableArray = toEnable.value.split(",");
					for(i=0;i < toEnableArray.length ;i++)
					{
						var cbCTRL = GetControlInGridFromCode(toEnableArray[i],'_cbDelete');
						if ( cbCTRL != null )
						{
							//cbCTRL.parentElement.parentElement.parentElement.disabled = false;
							//EnableDisableRow(cbCTRL.parentElement.parentElement.parentElement, false);
							cbCTRL.disabled = false;
						}
					}
				}
				if(trim(toUncheck.value) != "")
				{
				  	var toUnCheckArray =toUncheck.value.split(",");
					for(i=0;i < toUnCheckArray.length ;i++)
					{
						var cbCTRL = GetControlInGridFromCode(toUnCheckArray[i],'_cbDelete');
						if ( cbCTRL != null )
						{
							cbCTRL.checked = false;
							DisableControls(cbCTRL.id);
							SetHiddenField(cbCTRL.id);
						}
					}
				}
				if(trim(toCheck.value) != "")
				{
					var toCheckArray =toCheck.value.split(",");
					for(i=0;i < toCheckArray.length ;i++)
					{
						var cbCTRL = GetControlInGridFromCode(toCheckArray[i],'_cbDelete');
						var ddlCTRL = GetControlInGridFromCode(toCheckArray[i],'_ddlLIMIT');
						if ( cbCTRL != null )
						{
							if(firstTime==false)
								cbCTRL.checked = true;
							DisableControls(cbCTRL.id);
							if(firstTime==false)
								OnDDLChange(ddlCTRL);
							SetHiddenField(cbCTRL.id);
						}
					}
				}
				SetHiddenField(chk.id);
			}
			function DisableControls(strcbDelete)
			{
				var lastIndex = strcbDelete.lastIndexOf('_');
				var prefix = strcbDelete.substring(0,lastIndex);
							
				strddlLimit = prefix + '_ddlLIMIT';
				strddlDed = prefix + '_ddlDEDUCTIBLE';
				strlblLimit = prefix + '_lblLIMIT';
				strlblDed = prefix + '_lblDEDUCTIBLE';
				strtxtLimit = prefix + '_txtLIMIT';
				strtxtDed = prefix + '_txtDEDUCTIBLE';
				strCovTypeName = prefix + '_lblLIMIT_TYPE';
				strDedTypeName = prefix + '_lblDEDUCTIBLE_TYPE';
				strLimitApplyName = prefix + '_lblIS_LIMIT_APPLICABLE';
				strDedApplyName = prefix + '_lblIS_DEDUCT_APPLICABLE';
				strAddDedApplyName = prefix + '_lblAdd_IS_DEDUCT_APPLICABLE'; 
				
				strtxtRate = prefix + '_txtRATE';
				strtxtPremium = prefix + '_txtPREMIUM';
				strtxtDescription = prefix + '_txtDESCRIPTION';
				strlblRate = prefix + '_lblRATE';
				strlblPremium = prefix + '_lblPREMIUM';
				strlblDescription = prefix + '_lblDESCRIPTION';
				
				strCovType = document.getElementById(strCovTypeName).innerText;
				strDedType = document.getElementById(strDedTypeName).innerText;
				strLimitApply = document.getElementById(strLimitApplyName).innerText;
				strDedApply = document.getElementById(strDedApplyName).innerText;
				strAddDedApply = document.getElementById(strAddDedApplyName).innerText;
				
				ddlLimit = document.getElementById(strddlLimit);
				ddlDed = document.getElementById(strddlDed);
				lblLimit = document.getElementById(strlblLimit);
				lblDed = document.getElementById(strlblDed);	
				cbDelete = document.getElementById(strcbDelete);	
				txtLimit = document.getElementById(strtxtLimit);	
				txtDed = document.getElementById(strtxtDed);

				lblRate = document.getElementById(strlblRate);
				txtRate = document.getElementById(strtxtRate);
				lblPremium = document.getElementById(strlblPremium);
				txtPremium = document.getElementById(strtxtPremium);
				lblDescription = document.getElementById(strlblDescription);
				txtDescription = document.getElementById(strtxtDescription);
				
				strrevLimit = prefix + '_revLIMIT';
				strrevDed = prefix + '_revDEDUCTIBLE';

				revLimit = document.getElementById(strrevLimit);
				revDeductible = document.getElementById(strrevDed);	
				
				if ( cbDelete.checked == true )
				{
					if(strLimitApply=="0" && strDedApply=="0" && strAddDedApply=="0")
					{
						lblRate.style.display = "inline";
						txtRate.style.display = "none";
						lblRate.text='';
						lblLimit.style.display = "inline";
						lblLimit.text = "";
						txtLimit.style.display = "none";
						if(ddlLimit != null)
							ddlLimit.style.display = "none";
							
						lblDed.style.display = "inline";
						lblDed.innerText = '';
						if(ddlDed != null)
						ddlDed.style.display = "none";
						lblDescription.style.display = "inline";
						lblDescription.innerText = '';
						if(txtDescription != null)
								txtDescription.style.display = "none";
						
						lblPremium.style.display = "inline";
						lblPremium.text='';
						txtPremium.style.display = "none";		
						return;
					}
					if(strAddDedApply=="1")
					{
					lblRate.style.display = "none";
					txtRate.style.display = "inline";
					}
					else
					{
					lblRate.style.display = "inline";
					txtRate.style.display = "none";
					}
					lblPremium.style.display = "none";
					txtPremium.style.display = "inline";
					lblDescription.style.display = "none";
					txtDescription.style.display = "inline";
					switch(strCovType)
					{
						case '0':
							//Show "No Coverages.
							SetEmptyLimits(prefix);
							break;
						case '1':
							//Flat
							//Show Dropdown
						case '2':
							//Split
								if ( ddlLimit.options.length == 0 )
								{
									lblLimit.style.display = "inline";
									lblLimit.innerText = '';
									if(ddlLimit != null)
										ddlLimit.style.display = "none";
								}
								else
								{
									lblLimit.style.display = "none";
									lblLimit.innerText = '';
									if(ddlLimit != null)
										ddlLimit.style.display = "inline";
								}
								if ( txtLimit != null )
								{
									txtLimit.style.display = "none";
								}
								break;	
							case '3':
								//Open
								//Show Textbox
								lblLimit.style.display = "none";
								if(ddlLimit!=null)
									ddlLimit.style.display = "none";
								if(txtLimit!=null)
								{
									txtLimit.style.display = "inline";
									ValidatorEnable(revLimit,true);
								}
								break;		
								
					}
					switch(strDedType)
					{
						case '0':
							//Show "No Coverages.
							SetEmptyDeds(prefix);
							break;
						case '1':
							//Flat
						case '2':
							//Split
							//Show Dropdown
							if ( ddlDed.options.length == 0 )
							{
								lblDed.style.display = "inline";
								lblDed.innerText = '';
								ddlDed.style.display = "none";
							}
							else
							{
								lblDed.style.display = "none";
								lblDed.innerText = '';
								ddlDed.style.display = "inline";
							}
							if(txtDed != null)
								txtDed.style.display = "none";
							break;
						case '3':
							//Open
							//Show Textbox
								lblDed.style.display = "none";
								if(ddlDed!=null)
									ddlDed.style.display = "none";
								txtDed.style.display = "inline";
								ValidatorEnable(revDeductible,true);
								
								if(strDedApply=="0")
								{
									lblDed.style.display = "inline";
									lblDed.innerText = '';
									if(ddlDed != null)
										ddlDed.style.display = "none";
									if(txtDed != null)
										txtDed.style.display = "none";
									if ( revDeductible != null )
									{
										ValidatorEnable(revDeductible,false);
									}
									//alert(strDedApply)
								}
							break;
					}
				}
				else if ( cbDelete.checked == false )	//checked == false
				{
					
					lblRate.style.display = "inline";
					txtRate.style.display = "none";
					lblPremium.style.display = "inline";
					txtPremium.style.display = "none";
					lblDescription.style.display = "inline";
					txtDescription.style.display = "none";
					
					DisableLimits(prefix);
					DisableDeds(prefix);
				}
			}
			
			//Sets the visibility fo Limits dropdowns
			function DisableLimits(prefix)
			{
					lblLimit.style.display = "inline";
					lblLimit.innerText = '';
					if(ddlLimit != null)
						ddlLimit.style.display = "none";
					if(txtLimit != null)
						txtLimit.style.display = "none";		
					if ( revLimit != null )
					{
						ValidatorEnable(revLimit,false);
					}
					
			}
			////Sets the visibility of Deductibles dropdowns
			function DisableDeds(prefix)
			{
					lblDed.style.display = "inline";
					lblDed.innerText = '';
					if(ddlDed != null)
						ddlDed.style.display = "none";
					if(txtDed != null)
						txtDed.style.display = "none";
					if ( revDeductible != null )
					{
						ValidatorEnable(revDeductible,false);
					}
			}
			function calculatePremium(strcbDelete)
			{
				/*Cover Type: Hull All Risks, Hull War Risks, Hull Spares Parts and Hull War Spares
						Annual Gross Premium = (SI * Basic Rate) / (1 – commission)
							*Basic Rate is going to input manually in the system
  
				Cover Type: TP Liability, TP Liability Additional Coverage, Hull Additional Coverage
						Annual Gross Premium = Flat Premium/ (1 – commission)
							*Flat premium is going to input manually in the system
				*/
				var lastIndex = strcbDelete.lastIndexOf('_');
				var prefix = strcbDelete.substring(0,lastIndex);
				strtxtLimit = prefix + '_txtLIMIT';
				strtxtRate = prefix + '_txtRATE';
				strtxtPremium = prefix + '_txtPREMIUM';

				txtLimit = document.getElementById(strtxtLimit);	
				txtRate = document.getElementById(strtxtRate);
				txtPremium = document.getElementById(strtxtPremium);
				var varRate=ReplaceAll(txtRate.value,',','');
		    	var varLimit =ReplaceAll(txtLimit.value,',','');
		    	 
				//alert(txtLimit.value + '-' + txtRate.value + '-' + txtPremium.value)
				if(varRate!="" && varLimit!="")
				   txtPremium.value= Math.round((varLimit * varRate* 0.01)/(1-0.15),2);
				else
				   txtPremium.value= '';
				   
			}
			function formatRate(ctlRate)
			{
			if(ctlRate!="")
			ctlRate = parseFloat(ctlRate).toFixed(4);
			return ctlRate; 
			}
			function init()
			{
			ChangeColor();
			ApplyColor();
			}
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" onload="init();" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td align="center"><asp:label id="lblMessage" runat="server" CssClass="errmsg" EnableViewState="False"></asp:label></td>
				</tr>
				<tr>
					<td class="headerEffectSystemParams"><asp:label id="lblPolicyCaption" Runat="server">Policy Coverages</asp:label></td>
				</tr>
				<tr id="trPOLICY_LEVEL_GRID" runat="server">
					<td class="midcolora"><asp:datagrid id="dgPolicyCoverages" runat="server" DataKeyField="COVERAGE_ID" AutoGenerateColumns="False"
							Width="100%">
							<HeaderStyle CssClass="headereffectWebGrid"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText=" Required /Optional">
									<ItemStyle Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblCOV_ID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
										</asp:Label>
										<asp:CheckBox ID="cbDelete" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' COV_ID='<%# DataBinder.Eval(Container, "DataItem.COV_ID") %>'>
										</asp:CheckBox>
										<input type="hidden" id="hidcbDelete" name="hidcbDelete" runat="server">
										<asp:Label ID="lblLIMIT_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LIMIT_TYPE") %>'>
										</asp:Label>
										<asp:Label ID="lblDEDUCTIBLE_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DEDUCTIBLE_TYPE") %>'>
										</asp:Label>
										<asp:Label ID="lblIS_LIMIT_APPLICABLE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IS_LIMIT_APPLICABLE") %>'>
										</asp:Label>
										<asp:Label ID="lblIS_DEDUCT_APPLICABLE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IS_DEDUCT_APPLICABLE") %>'>
										</asp:Label>
										<asp:Label ID="lblAdd_IS_DEDUCT_APPLICABLE" style="display:none" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ISADDDEDUCTIBLE_APP") %>'>
										</asp:Label>
										<input type="hidden" id="hidCHECKDDISABLE"  runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDDISABLE">
										<input type="hidden" id="hidCHECKDENABLE" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDENABLE">
										<input type="hidden" id="hidCHECKDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDSELECT">
										<input type="hidden" id="hidCHECKDDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidCHECKDDSELECT">
										<input type="hidden" id="hidUNCHECKDDISABLE" runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDDISABLE">
										<input type="hidden" id="hidUNCHECKDENABLE" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDENABLE">
										<input type="hidden" id="hidUNCHECKDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDSELECT">
										<input type="hidden" id="hidUNCHECKDDSELECT" runat="server"  COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' NAME="hidUNCHECKDDSELECT">
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Coverage Type">
									<ItemStyle Width="20%"></ItemStyle>
									<ItemTemplate>
										<asp:Label runat="server" ID="lblCOVG_TYPE" Text='<%# DataBinder.Eval(Container, "DataItem.COV_TYPE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Coverage Type/Interest">
									<ItemStyle Width="20%"></ItemStyle>
									<ItemTemplate>
										<select id="ddlCOV_DESC" Visible="True" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'  NAME="ddlCOV_DESC">
										</select>
										<asp:Label runat="server" ID="lblCOV_DESC" Text='<%# DataBinder.Eval(Container, "DataItem.COV_DESC") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Sum Insured">
									<ItemStyle HorizontalAlign="Right" Width="15%"></ItemStyle>
									<ItemTemplate>
										<select id="ddlLIMIT" Visible="True" Runat="server" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>' onchange="javascript:OnDDLChange(this);" NAME="ddlLIMIT">
										</select>
										<asp:label id="lblLIMIT" CssClass="labelfont" Runat="server">
											<%# DataBinder.Eval(Container, "DataItem.INCLUDED_TEXT","{0:,#,###}") %>
										</asp:label>
										<asp:TextBox ID="txtLIMIT" Runat="server" CssClass="INPUTCURRENCY" MaxLength="7" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
										</asp:TextBox>
										<BR>
										<asp:RegularExpressionValidator ID="revLIMIT" Enabled="False" Runat="server" ControlToValidate="txtLIMIT" Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Rate">
									<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
									<ItemTemplate>
										<asp:label id="lblRATE" CssClass="labelfont" Runat="server"></asp:label>
										<asp:TextBox ID="txtRATE" Runat="server" CssClass="INPUTCURRENCY" MaxLength="7" COV_CODE='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
										</asp:TextBox>
										<BR>
										<asp:RegularExpressionValidator ID="revRATE" Enabled="False" Runat="server" ControlToValidate="txtRATE" Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Deductible">
									<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
									<ItemTemplate>
										<select id="ddlDEDUCTIBLE" Visible="True" Runat="server" NAME="ddlDEDUCTIBLE">
										</select>
										<asp:Label id="lblDEDUCTIBLE" CssClass="labelfont" Runat="server"></asp:Label>
										<asp:TextBox ID="txtDEDUCTIBLE" Runat="server" CssClass="INPUTCURRENCY" MaxLength="10"></asp:TextBox>
										<BR>
										<asp:RegularExpressionValidator ID="revDEDUCTIBLE" Enabled="False" Runat="server" ControlToValidate="txtDEDUCTIBLE"
											Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Description">
									<ItemStyle HorizontalAlign="Right" Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblDESCRIPTION" runat="server" CssClass="labelfont" Text=''></asp:Label>
										<asp:TextBox ID="txtDESCRIPTION" Runat="server" MaxLength="250"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Premium">
									<ItemStyle HorizontalAlign="Right" Width="5%"></ItemStyle>
									<ItemTemplate>
										<asp:Label ID="lblPREMIUM" runat="server" CssClass="labelfont" Text=''></asp:Label>
										<asp:TextBox ID="txtPREMIUM" Runat="server" CssClass="INPUTCURRENCY" MaxLength="12"></asp:TextBox>
										<BR>
										<asp:RegularExpressionValidator ID="revPREMIUM" Enabled="False" Runat="server" ControlToValidate="txtPREMIUM" Display="Dynamic"></asp:RegularExpressionValidator>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:Label ID="lblCOV_CODE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COV_CODE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<ItemTemplate>
										<asp:Label ID="lblCOV_TYPE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COVERAGE_TYPE") %>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td>
						<table width="100%">
							<tr>
								<td class="midcolora" colSpan="3"></td>
								<td class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidPolID" type="hidden" name="hidPolID" runat="server">
						<INPUT id="hidPolVersionID" type="hidden" name="hidPolVersionID" runat="server">
						<INPUT id="hidVEHICLE_ID" type="hidden" name="hidVEHICLE_ID" runat="server"> <INPUT id="hidCustomerID" type="hidden" name="hidCustomerID" runat="server">
						<INPUT id="hidROW_COUNT" type="hidden" value="0" name="hidROW_COUNT" runat="server">
						<INPUT id="hidPOLICY_ROW_COUNT" type="hidden" value="0" name="hidPOLICY_ROW_COUNT" runat="server">
						<INPUT id="hidCoverageXML" type="hidden" name="hidCoverageXML" runat="server"> <INPUT id="hidLOBState" type="hidden" name="hidLOBState" runat="server">
						<INPUT id="hidControlXML" type="hidden" name="hidControlXML" runat="server">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
