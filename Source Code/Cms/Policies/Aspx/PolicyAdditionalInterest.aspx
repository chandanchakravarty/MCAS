<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page language="c#" Codebehind="PolicyAddlInterest.aspx.cs" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.PolicyAddInterest" ValidateRequest="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<TITLE>Policy Vehicle Additional Interest</TITLE>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
			
		function PostFromLookup()
		{
			/*Post back the form to show the details of holder*/
			document.getElementById("hidLOOKUP").value = "Y";
			__doPostBack('hidLOOKUP','')
		}
		
		function OpenLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args)
		{
			
			var win = window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
								DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
								'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
								'&Args=' + Args,
								'review','height=500, width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no' )
		}
		
		function AddData()
		{
			
			//document.getElementById('hidCUSTOMER_ID').value	=	'New';			
			document.getElementById('txtHOLDER_ID').value = "";
			document.getElementById('cmbNATURE_OF_INTEREST').value  = -1;
			document.getElementById('cmbHOLDER_STATE').options.selectedIndex = -1;	

			if(document.getElementById('btnActivateDeactivate'))	
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			DisableValidators();
			ChangeColor();
			
		}
		function populateXML() {
		    //debugger;
			if(document.getElementById('hidFormSaved').value == '0')
			{				
				if(document.getElementById('hidOldData').value!="")
				{						
					
					if(document.getElementById('btnActivateDeactivate'))	
						document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					
				}
				else
				{
					AddData(); 
				} 
			}						
			return false;
		}
	
	function ResetScreen()
	{
//		if(document.getElementById("imgSelect").style.display = "none")
//		{		
//			this.parent.changeTab(0,0);
//		}
//		else
//		{
//			document.AdditionalInterest.reset();
//		}
		
		DisableValidators();		 
		
		return false;
	
	}
	
	function ChkTextAreaLength(source, arguments)
	{
		var txtArea = arguments.Value;
		if(txtArea.length > 250 ) 
		{
			arguments.IsValid = false;
			return;   
		}
	}
	
	//Called on bodyload to enable od disable the selected holder combobox
	function Init() {
	    //debugger;
		if(document.getElementById('hidFormSaved').value != 5)
		{
			if(document.getElementById('txtHOLDER_ID'))
			{
			try{
				document.getElementById('txtHOLDER_ID').focus();
				}catch(err){}
			}
		}
		if ( document.getElementById('hidMode').value == "Update")
		{
		    document.getElementById("txtHOLDER_ID").style.display = "none";
            //comment by kuldeep at the time of insurors
			//document.getElementById("imgSelect").style.display = "none";
			document.getElementById("spnHOLDER_ID").style.display = "inline";
			document.getElementById("spnHOLDER_ID").innerHTML = document.getElementById("txtHOLDER_ID").value
			
			if(document.getElementById('txtRANK') && document.getElementById('hidFormSaved').value != 5)
			{
				try{
					document.getElementById('txtRANK').focus();
				}catch(err){}
			}
		}
		else
		{
		    document.getElementById("txtHOLDER_ID").style.display = "inline";
		    //comment by kuldeep at the time of insurors
			//document.getElementById("imgSelect").style.display = "inline";
			document.getElementById("spnHOLDER_ID").style.display = "none";
		}
	}
	
	function txtHOLDER_ID_OnBlur()
	{
	    //debugger;
		if ( document.getElementById('txtHOLDER_ID').value != document.getElementById('hidHOLDER_NAME').value )
		{
			document.getElementById('hidHolderID').value = '';
		}
	}
	
		</script>
		<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="Init();populateXML();ApplyColor();ChangeColor();">
			<FORM id="AdditionalInterest" method="post" runat="server">
			<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
				<TABLE class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td class="midcolorc" align="right" colSpan="4">
							<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
						</td>
					</tr>
					<TR>
					<TD class="pageHeader" colSpan="4">
						<%--<webcontrol:WorkFlow id="myWorkFlow" runat="server"></webcontrol:WorkFlow>--%>
					</TD>
				</TR>
					<TR id="trBody" runat="server">
						<TD>
							<TABLE class="tableWidthHeader" align="center" border="0">
								<TBODY>
                                    <tr>
                                        <td class="headereffectCenter" colspan="3">
                                            <asp:Label ID="lblHeader" runat="server">Additional Interest</asp:Label>
                                        </td>
                                    </tr>
									<tr>
										<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are 
											mandatory
										</TD>
									</tr>
									<tr>
										<td class="midcolorc" align="right" colSpan="3"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
									</tr>
									<TR>
										<TD class="headerEffectSystemParams" colSpan="3">Additional Interest</TD>
									</TR>
									<tr>
										<TD class="midcolora" width="33%"><asp:label id="capHOLDER_ID" runat="server">Name</asp:label><span class="mandatory">*</span>
										<br /><asp:textbox id="txtHOLDER_ID" onblur="txtHOLDER_ID_OnBlur();" MaxLength="70" size="30" Runat="server"></asp:textbox><span class="labelfont" id="spnHOLDER_ID" runat="server"></span><%--<IMG id="imgSelect" style="CURSOR: hand" alt="" src="../../../cmsweb/images/selecticon.gif"
												runat="server">--%>
											<br>
											<asp:requiredfieldvalidator id="rfvHOLDER_ID" Runat="server" Display="Dynamic" ControlToValidate="txtHOLDER_ID"></asp:requiredfieldvalidator></td>
										<td class="midcolora" width="33%"><asp:Label ID="capRANK" Runat="server"></asp:Label><span class="mandatory">*</span><br /><asp:TextBox ID="txtRANK" Runat="server" size="2" MaxLength="2"></asp:TextBox><br>
											<asp:RequiredFieldValidator ID="rfvRANK" Runat="server" ControlToValidate="txtRANK" Display="Dynamic" ErrorMessage="RANK can't be blank"></asp:RequiredFieldValidator>
											<asp:RegularExpressionValidator ID="revRANK" Runat="server" ControlToValidate="txtRANK" Display="Dynamic"></asp:RegularExpressionValidator>
											<asp:RangeValidator ID="rngRANK" Runat="server" ControlToValidate="txtRANK" Display="Dynamic" MaximumValue="99" MinimumValue="1" Type="Integer"></asp:RangeValidator>
											</td>
                                            <td class="midcolora" width="33%">
                                                <asp:label id="capHOLDER_ADD1" runat="server">Holder Address11</asp:label><SPAN class="mandatory">*</SPAN><br /><asp:textbox id="txtHOLDER_ADD1" runat="server" size="35" maxlength="70"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvHOLDER_ADD1" runat="server" Display="Dynamic" ControlToValidate="txtHOLDER_ADD1"
												ErrorMessage="NAME can't be blank."></asp:requiredfieldvalidator>
                                            </td>
									</tr>
									<TR>
										<TD class="midcolora" width="33%"><asp:label id="capHOLDER_ADD2" runat="server" visible="false">Holder Address222</asp:label><br /><asp:textbox id="txtHOLDER_ADD2" visible="false" runat="server" size="35" maxlength="70"></asp:textbox></TD>
										<TD class="midcolora" width="33%">
                                            <asp:label id="capHOLDER_CITY" runat="server"></asp:label><SPAN class="mandatory">*</SPAN><br /><asp:textbox id="txtHOLDER_CITY" runat="server" size="35" maxlength="35"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvHOLDER_CITY" runat="server" Display="Dynamic" ControlToValidate="txtHOLDER_CITY"
												ErrorMessage="NAME can't be blank."></asp:requiredfieldvalidator>
                                                
                                        </TD>
									    
                                        <td class="midcolora" width="33%">
                                            <asp:label id="capHOLDER_COUNTRY" runat="server">Holder Countery1</asp:label><br /><asp:dropdownlist id="cmbHOLDER_COUNTRY" runat="server"></asp:dropdownlist>
                                        </td>

                                    </TR>
									<TR>
										<TD class="midcolora" width="33%">
                                            <asp:label id="capHOLDER_STATE" runat="server">Holder City1</asp:label><SPAN class="mandatory">*</SPAN><br /><asp:dropdownlist id="cmbHOLDER_STATE" runat="server" OnFocus="SelectComboIndex('cmbHOLDER_STATE')"></asp:dropdownlist><br>
											<asp:requiredfieldvalidator id="rfvHOLDER_STATE" runat="server" Display="Dynamic" ControlToValidate="cmbHOLDER_STATE"
												ErrorMessage="NAME can't be blank."></asp:requiredfieldvalidator>
                                        </TD>
										<TD class="midcolora" width="33%">
                                            <asp:label id="capHOLDER_ZIP" runat="server">Holder Zip11</asp:label><span class="mandatory">*</span><br /><asp:textbox id="txtHOLDER_ZIP" runat="server" size="13" maxlength="10"></asp:textbox>
										<%-- Added by Swarup on 30-mar-2007 --%>
												<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
												<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
												</asp:hyperlink>
												<br>
											<asp:requiredfieldvalidator id="rfvHOLDER_ZIP" Runat="server" Display="Dynamic" ControlToValidate="txtHOLDER_ZIP"
												ErrorMessage="Zip can not be black"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revHOLDER_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtHOLDER_ZIP"
												ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator>
										</TD>
									
                                        <td class="midcolora" width="33%"></td>

                                   
									<TR>
										<TD class="headerEffectSystemParams" colSpan="3">Additional Interest Details</TD>
									</TR>
									<TR>
										<TD class="midcolora" width="33%"><asp:label id="capMEMO" runat="server">Memo</asp:label><br /><asp:textbox onkeypress="MaxLength(this,250)" id="txtMEMO" runat="server" size="200" maxlength="250"
												TextMode="MultiLine" Width="240px" Height="88px"></asp:textbox><br>
											<asp:customvalidator id="csvMEMO" Runat="server" Display="Dynamic" ControlToValidate="txtMEMO" ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
										<TD class="midcolora" width="33%">
                                        <asp:label id="capNATURE_OF_INTEREST" runat="server" >Nature Of Interest</asp:label><span class="mandatory">*</span><br />
											<asp:DropDownList id="cmbNATURE_OF_INTEREST" runat="server" OnFocus="SelectComboIndex('cmbNATURE_OF_INTEREST')"></asp:DropDownList>
												<br>
												<asp:RequiredFieldValidator id="rfvNATURE_OF_INTEREST" runat="server" Display="Dynamic" ControlToValidate="cmbNATURE_OF_INTEREST"></asp:RequiredFieldValidator>
                                                <p>
                                                    <asp:label id="capADD_INT_TYPE" runat="server" >Additional Insured/WOS Type</asp:label><span class="mandatory">*</span><br />
											<asp:DropDownList id="cmbADD_INT_TYPE" runat="server" OnFocus="SelectComboIndex('cmbADD_INT_TYPE')"></asp:DropDownList>
												<br>
												<asp:RequiredFieldValidator id="rfvADD_INT_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbADD_INT_TYPE"></asp:RequiredFieldValidator>
                                                </p>
										</TD>
                                        <td class="midcolora" width="33%">
                                            <asp:label id="capLOAN_REF_NUMBER" runat="server">Loan/Reference No</asp:label>
                                        <br />
                                        <asp:textbox id="txtLOAN_REF_NUMBER" runat="server" size="30" maxlength="75"></asp:textbox>
                                        <p>
                                        
                                        <asp:label id="capADD_INT_PREMIUM" runat="server">Additional Insured/WOS Premium</asp:label>
                                        <br />
                                        <asp:textbox id="txtADD_INT_PREMIUM" runat="server" size="30" maxlength="75"></asp:textbox>
                                        <asp:label id="capBILL_MORTAGAGEE" runat="server">Bill this mortgagee</asp:label>
										<br />
                                        <asp:dropdownlist id="cmbBILL_MORTAGAGEE" onfocus="SelectComboIndex('cmbBILL_MORTAGAGEE')" runat="server" >											
											</asp:dropdownlist>
                                        
                                        </p>
                                        </td>

									</TR>
									<tr>
										<TD class="midcolora" width="33%">
                                            
                                        </TD>
										<td class="midcolora" width="33%">
                                            
										</td>
                                        <td class="midcolora" width="33%"></td>
									</tr>
									<TR>
										<TD class="midcolora" width="33%" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" CausesValidation="False" Text="Reset"></cmsb:cmsbutton>&nbsp;
											<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" CausesValidation="false"
												Text="Activate/Deactivate"></cmsb:cmsbutton></TD>
										<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" visible="false"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
									</TR>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
				<INPUT id="hidHolderXML" type="hidden" value="0" name="hidHolderXML" runat="server">
				<INPUT id="hidMode" type="hidden" value="0" name="hidMode" runat="server"> <INPUT id="hidAPP_ID" type="hidden" name="hidAPP_ID" runat="server">
				<INPUT id="hidAPP_VERSION_ID" type="hidden" name="hidAPP_VERSION_ID" runat="server">
				<INPUT id="hidVEHICLE_ID" type="hidden" name="hidVEHICLE_ID" runat="server"> <INPUT id="hidDWELLING_ID" type="hidden" name="hidDWELLING_ID" runat="server">
				<INPUT id="hidBOAT_ID" type="hidden" name="hidBOAT_ID" runat="server"> <INPUT id="hidTRAILER_ID" type="hidden" name="hidTRAILER_ID" runat="server">
				<INPUT id="hidENGINE_ID" type="hidden" name="hidENGINE_ID" runat="server"> <INPUT id="hidHolderID" type="hidden" name="hidHolderID" runat="server">
				<input id="hidLOOKUP" type="hidden" value="0" name="hidLOOKUP" runat="server"> <input id="hidHOLDER_NAME" type="hidden" runat="server" NAME="hidHOLDER_NAME">
				<input id="hidADD_INT_ID" type="hidden" runat="server" NAME="hidADD_INT_ID" value="0"> <input id="hidPOLICY_ID" type="hidden" runat="server" NAME="hidPOLICY_ID">
				<input id="hidPOLICY_VERSION_ID" type="hidden" runat="server" NAME="hidPOLICY_VERSION_ID">
				<INPUT id="hidLOB_ID" type="hidden" value="" name="hidLOB_ID" runat="server">
				<INPUT id="hidBILL_MORTAGAGEE" type="hidden" value="" name="hidBILL_MORTAGAGEE" runat="server">			
			</FORM>
			<script>
			RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidADD_INT_ID').value,false);
			</script>
		</BODY>
