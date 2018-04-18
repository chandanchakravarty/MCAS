<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalDetailQQ.aspx.cs" Inherits="Cms.CmsWeb.aspx.PersonalDetailQQ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Personal Particulars</title>  
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script> 
	
	<script language="javascript" type="text/javascript">

	    function ValCheck() {

	        //if (Page_ClientValidate()) {
	            //document.getElementById('lblManHeader').style.display = 'none';
	            return true;
	       // }
	       // else {
	            //document.getElementById('lblManHeader').style.display = 'inline';
	           // return false;
	      //  }
	        }

	        function HideShowGenderField() {
	           
	            if ((document.getElementById('cmbCUSTOMER_TYPE').value == '11109') || (document.getElementById('cmbCUSTOMER_TYPE').value == '14725')) {
                
                    document.getElementById('rfvGENDER').enabled=false;
                    document.getElementById('spnGENDER').style.display = 'none';
                    document.getElementById('capGENDER').style.display = 'none';
                    document.getElementById('cmbGENDER').style.display = 'none';
                    document.getElementById('rfvGENDER').style.display = 'none';
                    
                }
                else
                {
                     document.getElementById('rfvGENDER').enabled=true;
                     document.getElementById('spnGENDER').style.display = 'inline';
                     document.getElementById('capGENDER').style.display = 'inline';
                     document.getElementById('cmbGENDER').style.display = 'inline';
                     
                }
	        }

	    function HideShowMerit() {
	        var NCD = document.getElementById('cmbEXISTING_NCD').options[document.getElementById('cmbEXISTING_NCD').options.selectedIndex].value;
	        NCD = NCD.replace("%", "");
	        //alert(NCD);	 
	        var eNCD = 0;
	        eNCD = NCD.valueOf();	               
	        //alert(eNCD);
	        if (eNCD >= 30){
//	            alert(eNCD);
//	            alert("IF");
//	            document.getElementById("lblDemerit").style.display = "inline";
//	            document.getElementById("rbDEMERIT_YES").style.visibility="visible";
//	            document.getElementById("rbDEMERIT_NO").style.visibility = "visible";
//	            document.getElementById("lblDEMERIT_YES").style.visibility = "visible";
//	            document.getElementById("lblDEMERIT_NO").style.visibility = "visible";
//	            document.getElementById("lblNCDMerit").style.display = "inline";
	            document.getElementById("rbDEMERIT_YES").disabled = false;
	            document.getElementById("rbDEMERIT_NO").disabled = false;
	            
	        }
	        else {
//	            alert(eNCD);
//	            alert("ELSE");
//	            document.getElementById("lblDemerit").style.display = "none";
//	            document.getElementById("rbDEMERIT_YES").style.visibility = "hidden";
//	            document.getElementById("rbDEMERIT_NO").style.visibility = "hidden";
//	            document.getElementById("lblDEMERIT_YES").style.visibility = "hidden";
//	            document.getElementById("lblDEMERIT_NO").style.visibility = "hidden";
//	            document.getElementById("lblNCDMerit").style.display = "none";
	            document.getElementById("rbDEMERIT_YES").disabled = true;
	            document.getElementById("rbDEMERIT_NO").disabled = true;
	            document.getElementById("rbDEMERIT_NO").checked = true;

	        }
	    }

	    function ChkDateOfBirth(objSource, objArgs) {
	        if (document.getElementById("revDATE_OF_BIRTH").isValid == true) {
	            var effdate = document.QQ_PERSONAL_DETAIL.txtDATE_OF_BIRTH.value;
	            objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
	        }
	        else
	            objArgs.IsValid = true;
	    }

	    function FormatDateofBirth() {
	        if (document.QQ_PERSONAL_DETAIL.txtDATE_OF_BIRTH.value != "") {
	            var dt = document.QQ_PERSONAL_DETAIL.txtDATE_OF_BIRTH.value.split('/');
	            document.CLT_CUSTOMER_LIST.txtDATE_OF_BIRTH.value = dt[1] + "/" + dt[0] + "/" + dt[2];

	        }
	    }

//	    function check() {

//	        if (document.getElementById("btnSave") != null) {
//	            //alert("True");
//	        }
//	        else {
//	            //alert("False");
//	        }
//	    }

	    function AddData() {
	        //document.forms[0].reset();
	        document.getElementById('hid_ID').value = 'New';
//	        document.getElementById('cmbCUSTOMER_TYPE').options.selectedIndex = -1;
//	       
//	        document.getElementById('txtCUSTOMER_FIRST_NAME').value = '';
//	        document.getElementById('txtCUSTOMER_MIDDLE_NAME').value = '';
//	        document.getElementById('txtCUSTOMER_LAST_NAME').value = '';       
//	        
//	        document.getElementById('txtDATE_OF_BIRTH').value = '';	        
//	        document.getElementById('cmbGENDER').options.selectedIndex = -1;
//	        document.getElementById('cmbAPPLICANT_OCCU').options.selectedIndex = 0;
	        
	        //DisableValidators();

	        //Changing the color of mandatory controls
	        //ChangeColor();
	        //OnCustomerTypeChange();
	        //SSN_change();

	    }

	    function Initialize() {
	       // setTimeout("GetColorAgency()", 300);	        
	        if (document.getElementById('hidFormSaved') != null && (document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')) {
	            var tempXML = document.getElementById('hidOldXML').value;
	            if (tempXML != "" && tempXML != 0 && tempXML != "<NewDataSet />") {
	                setMenu();
	                
	                //Storing the XML in hidCUSTOMER_ID hidden fields 
	                if (tempXML != undefined)//Done for Itrack Issue 5454 on 20 April 09
	                    document.getElementById('hidOldData').value = tempXML;

	                        
	                

	            }
	            else {
	                AddData();
	                //document.getElementById('hidCUSTOMER_ID').value = 'New';
	            }
	        }

	        //setTab();
//	        Check();
	        //This function will enable or disable the middle and last name depending upon
	        //the type of customer
	        //OnCustomerTypeChange();

	        //setEncryptFields();

	    }

	    function initPage() {
	        // alert();
	        Initialize();
//	        InsuranceScoreChange();
//	        ApplyColor();
//	        showButtons();
//	        LoadTitles();
//	        chkScore_clicked();
//	        RefreshClientTop();
	    }

	    function ResetForm() {
//	        temp = 1;
	        document.QQ_PERSONAL_DETAIL.reset();
//	        DisplayPreviousYearDesc();
//	        populateXML();
//	        BillType();
//	        DisableValidators();
//	        ChangeColor();


	        return false;
	    }
	</script>
	
</head>
<BODY  oncontextmenu="return true;" leftMargin="0" topMargin="0" onload="initPage();HideShowGenderField();">
    <form id="QQ_PERSONAL_DETAIL" runat="server">
        <table align="center" width="99%">
            <tr>
                <td colspan="4" align="center">           
                <asp:Label ID="capMessage" runat="server" 
                        Text="Motor Insurance Quotation - Private Vehicle" style="font-weight: 700; text-align:center"></asp:Label>
                </td>                
            </tr>
            <tr>
                <td class="pageHeader" colspan="3">
                    <asp:Label ID="lblManHeader" runat="server" Style="display: none">Please note that all fields marked with * are mandatory</asp:Label>
                </td>
            </tr>

			<tr>
				<td class="midcolorc" align="right" colSpan="4">
				    <asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label>
				</td>
			</tr>
			<tr>
				<TD class="pageHeader" colSpan="4">Please note that all fields marked with * are mandatory</TD>
			</tr>
            <tr>
                <td colspan="4">
                    <table width = "100%">
                        <tr>
                            <td class="midcolora" colspan="2" style="width: 50%" >
                                <asp:Label ID="capDOQ" runat="server" Text="Date of Quotation"></asp:Label>
                            </td>
                            <td class="midcolora" colspan="2" style="width: 50%" >
                                <asp:Label ID="lblDOQ" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" colspan="2" style="width: 50%" >
                                <asp:Label ID="capAGENT" runat="server" Text="Agent/Broker"></asp:Label>
                            </td>
                            <td class="midcolora" colspan="2" style="width: 50%" >
                                <asp:Label ID="lblAGENT" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="20%" ></td>
                            <td class="midcolora" width="30%" ></td>
                            <td class="midcolora" width="20%" ></td>
                            <td class="midcolora" width="30%" ></td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="20%" ></td>
                            <td class="midcolora" width="30%" ></td>
                            <td class="midcolora" width="20%" ></td>
                            <td class="midcolora" width="30%" ></td>
                        </tr>                       
                    </table>
                </td>
            </tr>
            <tr>
                <td class="headerEffectSystemParams" colspan="4" >           
                    <asp:Label ID="capPOP" runat="server" Text="Particulars of Proposer"></asp:Label>
                </td>  
            </tr>            
            <tr>
                <td class="midcolora" width="20%" >                  
                </td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >                    
                </td>
                <td class="midcolora" width="30%" ></td>
            </tr>
            <tr id="trClientCode" runat="server" visible="false">
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capCLIENT_CODE" runat="server" Text="eProfessional Client Code"></asp:Label>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:TextBox ID="txtCUSTOMER_CODE" runat="server"></asp:TextBox>
                </td>
                <td class="midcolora" width="20%" ></td>
                <td class="midcolora" width="30%" ></td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capSTATUS" runat="server" Text="Type"></asp:Label><SPAN class="mandatory">*</SPAN>
                </td>
                <td class="midcolora" width="30%" > 
                    <asp:DropDownList ID="cmbCUSTOMER_TYPE" runat="server" onchange="HideShowGenderField();" onblur="HideShowGenderField();">
                    </asp:DropDownList>
                    <asp:requiredfieldvalidator id="rfvCUSTOMER_TYPE" Runat="server" ControlToValidate="cmbCUSTOMER_TYPE" Display="Dynamic" ErrorMessage="Please select Customer Type."></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" ></td>
                <td class="midcolora" width="30%" ></td>
            </tr>

            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capCUSTOMER_FIRST_NAME" runat="server" Text="Name"></asp:Label><SPAN class="mandatory">*</SPAN>
                </td>
                <td class="midcolora" width="30%" > 
                    <asp:textbox id="txtCUSTOMER_FIRST_NAME" runat="server" size="25" maxlength="75"></asp:textbox><br />
								<asp:requiredfieldvalidator id="rfvCUSTOMER_FIRST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
									Display="Dynamic" ErrorMessage="Please enter Customer Name."></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:textbox id="txtCUSTOMER_MIDDLE_NAME" runat="server" size="12" maxlength="10" Visible="false"></asp:textbox>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:textbox id="txtCUSTOMER_LAST_NAME" runat="server" size="12" maxlength="10" Visible="false"></asp:textbox>
                </td>
            </tr>
            <%--<tr>
                <td colSpan="4">
					<table cellSpacing="0" cellPadding="0" width="100%" border="0">
						<TR>
							<TD class="midcolora" width="20%"><asp:label id="capCUSTOMER_FIRST_NAME" runat="server" Text="First Name"></asp:label><SPAN class="mandatory">*</SPAN>
							</TD>
							<TD class="midcolora" width="18%" id='tdFname'><asp:textbox id="txtCUSTOMER_FIRST_NAME" runat="server" size="25" maxlength="75"></asp:textbox><br>
								<asp:requiredfieldvalidator id="rfvCUSTOMER_FIRST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_FIRST_NAME"
									Display="Dynamic" ErrorMessage="Please enter First Name."></asp:requiredfieldvalidator></TD>
							<TD class="midcolora" width="13%"><asp:label id="capCUSTOMER_MIDDLE_NAME" runat="server" Text="Middle Name"></asp:label></TD>
							<TD class="midcolora" width="16%"><asp:textbox id="txtCUSTOMER_MIDDLE_NAME" runat="server" size="12" maxlength="10"></asp:textbox></TD>
							<TD class="midcolora" width="8%"><asp:label id="capCUSTOMER_LAST_NAME" runat="server" Text="Last Name"></asp:label><SPAN class="mandatory" id="spnMandatory">*</SPAN>
							</TD>
							<TD class="midcolora" width="25%"><asp:textbox id="txtCUSTOMER_LAST_NAME" runat="server" size="25" maxlength="25"></asp:textbox><br>
								<asp:requiredfieldvalidator id="rfvCUSTOMER_LAST_NAME" Runat="server" ControlToValidate="txtCUSTOMER_LAST_NAME"
									Display="Dynamic" ErrorMessage="Please enter Last Name."></asp:requiredfieldvalidator></TD>
						</TR>
					</table>
				</td>
            </tr>--%>
            <tr>
                <td class="midcolora" width="20%" >  
                    <asp:Label ID="capGENDER" runat="server" Text="Sex"></asp:Label>
                    <SPAN class="mandatory" id="spnGENDER">*</SPAN>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:DropDownList ID="cmbGENDER" runat="server" Height="16px">
                        <asp:ListItem Value="">-Select-</asp:ListItem>
						<asp:ListItem Value="F">Female</asp:ListItem>
						<asp:ListItem Value="M">Male</asp:ListItem>
                    </asp:DropDownList></br>
                    <asp:requiredfieldvalidator id="rfvGENDER" Runat="server" ControlToValidate="cmbGENDER"
									Display="Dynamic" ErrorMessage="Please select Sex." ></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capDOB" runat="server" Text="Date of Birth (dd/mm/yyyy)"></asp:Label>
                    <SPAN class="mandatory" id="SPAN2">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <%--<asp:TextBox ID="txtDOB_DAY" runat="server" Height="17px" Width="25px"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtDOB_MONTH" runat="server" Height="17px" Width="25px"></asp:TextBox>
                    &nbsp;/
                    <asp:TextBox ID="txtDOB_YEAR" runat="server" Height="17px" Width="32px"></asp:TextBox>--%>
                     <asp:textbox id="txtDATE_OF_BIRTH" runat="server" onBlur="FormatDate();" size="12" maxlength="10">
                     </asp:textbox>
                     <asp:hyperlink id="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot">
					 <asp:Image runat="server" ID="imgDATE_OF_BIRTH" Border="0" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:Image>
					</asp:hyperlink><BR />
					<asp:requiredfieldvalidator id="rfvDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"	ErrorMessage="Date of Birth can't be blank."></asp:requiredfieldvalidator>
					<%--<asp:regularexpressionvalidator id="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:regularexpressionvalidator>--%><br>
					<%--<asp:customvalidator id="csvDATE_OF_BIRTH" Runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic" ClientValidationFunction="ChkDateOfBirth"></asp:customvalidator>--%>
                </td>
            </tr>
            <tr>
               <td class="midcolora" width="20%" >  
                    <asp:Label ID="capNATIONALITY" runat="server">
                        Nationality<br>(Exclude Foreigner)
                    </asp:Label>
                    <SPAN class="mandatory" id="SPAN3">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbCUSTOMER_COUNTRY" runat="server" Height="16px" Width="130px">
                    </asp:DropDownList>
                    </br>
                    <asp:requiredfieldvalidator id="rfv" runat="server" ControlToValidate="cmbCUSTOMER_COUNTRY" Display="Dynamic"	ErrorMessage="Please select Nationality."></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capOCCUPATION" runat="server" Text="Occupation"></asp:Label>
                    <SPAN class="mandatory" id="SPAN4">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:RadioButton ID="rbOCCU_TYPE_INDOOR" GroupName="Occupation" runat="server" Text="Indoor" Checked="true" />
                    <asp:RadioButton ID="rbOCCU_TYPE_OUTDOOR" GroupName="Occupation" runat="server" Text="Outdoor" />
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="capOCCUPATION_LIST" runat="server" Text="Occupation List"></asp:Label>
                    <SPAN class="mandatory" id="SPAN5">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbAPPLICANT_OCCU" runat="server" Height="16px" Width="200px">
                    </asp:DropDownList></br>
                    <asp:requiredfieldvalidator id="rfvAPPLICANT_OCCU" runat="server" ControlToValidate="cmbAPPLICANT_OCCU" Display="Dynamic"	ErrorMessage="Please select Occupation."></asp:requiredfieldvalidator>
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="Label16" runat="server" Text="Driving Experience(years)"></asp:Label>
                    <SPAN class="mandatory" id="SPAN6">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbDRIVING_EXP" runat="server" Height="16px" Width="75px">
                    </asp:DropDownList></br>
                    <asp:requiredfieldvalidator id="rfvDRIVING_EXP" runat="server" ControlToValidate="cmbDRIVING_EXP" Display="Dynamic"	ErrorMessage="Please select Driving Experience."></asp:requiredfieldvalidator>
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="Label17" runat="server" Text="Any Claim made for this year?"></asp:Label>
                    <SPAN class="mandatory" id="SPAN7">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:RadioButton ID="rbCLAIM_YES" GroupName="CLAIM" runat="server" Text="Yes" />
                    <asp:RadioButton ID="rbCLAIM_NO" GroupName="CLAIM" runat="server" Text="No" Checked="true" />
                </td>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="Label18" runat="server" 
                        Text="Is your existing NCD entitlement less than 10 months"></asp:Label>
                        <SPAN class="mandatory" id="SPAN8">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:RadioButton ID="rbNCD_YES" GroupName="NCD" runat="server" Text="Yes" />
                    <asp:RadioButton ID="rbNCD_NO" GroupName="NCD" runat="server" Text="No" Checked="true" />
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >
                    <asp:Label ID="Label19" runat="server" Text="Existing NCD"></asp:Label>
                    <SPAN class="mandatory" id="SPAN9">*</SPAN>
                </td>
                <td class="midcolora" width="30%" >
                    <asp:DropDownList ID="cmbEXISTING_NCD" runat="server" Width="75px" onchange="javascript:HideShowMerit();">
                    </asp:DropDownList>
                            &nbsp;<asp:DropDownList ID="DropDownList5" runat="server" Width="75px" Visible="false">
                    </asp:DropDownList></br>
                    <asp:requiredfieldvalidator id="rfvEXISTING_NCD" runat="server" ControlToValidate="cmbEXISTING_NCD" Display="Dynamic"	ErrorMessage="Please select existing NCD."></asp:requiredfieldvalidator>
                </td>
                <td id=lbMerit class="midcolora" width="20%" >
                    <asp:Label ID="lblDemerit" runat="server" Text="Demerit Points Free Discount"></asp:Label>
                </td>
                <td id="tdMerit" class="midcolora" width="30%" >
                    <asp:RadioButton ID="rbDEMERIT_YES" GroupName="DEMERIT" runat="server" />
                     <asp:Label ID="lblDEMERIT_YES" runat="server" Text="Yes"></asp:Label>
                    <asp:RadioButton ID="rbDEMERIT_NO" GroupName="DEMERIT" runat="server" Checked="true" />
                     <asp:Label ID="lblDEMERIT_NO" runat="server" Text="No"></asp:Label>
                </td>
            </tr>
            <tr>
               <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td id = "tdNCDMerit" class="midcolora" colspan="2" style="width: 50%" >
                   <asp:Label ID="lblNCDMerit" runat="server" Text="(The discount is applicable for NCD 30% and above)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
                <td class="midcolora" width="20%" >
                    &nbsp;</td>
                <td class="midcolora" width="30%" >&nbsp;</td>
            </tr>
            
             <tr>
                <td class="midcolora" colspan="2">
                    <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton>
                </td>
                <td class="midcolora" colspan="1">
                </td>
                <td class="midcolora" colspan="1">       
                    <table width="100%">
                        <tr>
                            <td align="right" width="100%">
                              <%-- <cmsb:CmsButton ID="btnSave" CssClass="clsbutton" Text="Save" runat="server" CausesValidation="true"
                            OnClick="btnSave_Click" />--%>
                            <cmsb:CmsButton class="clsButton" ID="btnSave" CausesValidation="true" runat="server"
                    Text="Save" OnClick="btnSave_Click"></cmsb:CmsButton>
                            
                            </td>
                        </tr>
                    </table>              
                    
                </td>
            </tr>
            <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server" />
			<input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server"/>
			<input id="hid_ID" type="hidden" value="0" name="hid_ID" runat="server"/> 
			<input id="hidCUST_PART_ID" type="hidden" value="0" name="hidCUST_PART_ID" runat="server"/> 
			<input id="hid_QUOTEID" type="hidden" value="0" name="hid_QUOTEID" runat="server"/> 
			<input id="hidCUSTOMER_PARENT" type="hidden" name="hidCUSTOMER_PARENT" runat="server"/>
			<input id="hidOldData" type="hidden" name="Hidden1" runat="server"/> 
			<input id="hidIS_ACTIVE" type="hidden" name="Hidden1" runat="server"/>
			<input id="Hidden1" type="hidden" name="Hidden1" runat="server"/> 
			<INPUT id="hidOldXML" type="hidden" name="hidOldXML" runat="server"/>
			<INPUT id="hidRefreshTabIndex" type="hidden" name="hidRefreshTabIndex" runat="server"/>
			<INPUT id="hidSaveMsg" type="hidden" name="hidSaveMsg" runat="server"/> 
			<input id="hidCarrierId" type="hidden" name="hidCarrierId" runat="server"/>
			<INPUT id="hidMsg" type="hidden" name="hidMsg" runat="server"/>
			<INPUT id="hidCust_Type" type="hidden" name="hidCust_Type" runat="server"/> 
			<INPUT id="hidBackToApplication" type="hidden" value="0" name="hidBackToApplication" runat="server"/>
			<INPUT id="hidCustomer_AGENCY_ID" type="hidden" name="hidCustomer_AGENCY_ID" runat="server"/>		
           
        </table>
    </form>
</body>
</html>
