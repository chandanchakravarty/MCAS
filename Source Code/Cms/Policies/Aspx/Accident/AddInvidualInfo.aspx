<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddInvidualInfo.aspx.cs" ValidateRequest="false" Inherits="Cms.Policies.Aspx.Accident.AddInvidualInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>POL_PERSONAL_ACCIDENT_INFO</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR"/>
    <meta content="C#" name="CODE_LANGUAGE"/>
    <meta content="JavaScript" name="vs_defaultClientScript"/>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
	<script src="/cms/cmsweb/scripts/common.js"></script>
	<script src="/cms/cmsweb/scripts/form.js"></script>
	<script src="/cms/cmsweb/scripts/calendar.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
    <script type="text/javascript">

        $(document).ready(function() {

            $("#cmbMAIN_INSURED").change(function() {
                if ($("#cmbMAIN_INSURED option:selected").val() != "") {
                    var PERSONAL_INFO_ID = $("#cmbMAIN_INSURED option:selected").val();
                    var CUSTOMER_ID = $("#hidCUSTOMER_ID").val();
                    var POLICY_ID = $("#hidPOL_ID").val();
                    var POLICY_VERSION_ID = $("#hidPOL_VERSION_ID").val();

                    PageMethod("GetInsuredObjectDetailsUsingInsuredId", ["PERSONAL_INFO_ID", PERSONAL_INFO_ID, "CUSTOMER_ID", CUSTOMER_ID, "POLICY_ID", POLICY_ID, "POLICY_VERSION_ID", POLICY_VERSION_ID], AjaxSucceeded, AjaxFailed);
                }
            });
            function PageMethod(fn, paramArray, successFn, errorFn) {
                var pagePath = window.location.pathname;
                //Create list of parameters in the form:  
                var paramList = '';
                if (paramArray.length > 0) {
                    for (var i = 0; i < paramArray.length; i += 2) {
                        if (paramList.length > 0) paramList += ',';
                        paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                    }
                }
                paramList = '{' + paramList + '}';
                //Call the page method  
                $.ajax({ type: "POST", url: pagePath + "/" + fn, contentType: "application/json; charset=utf-8",
                    data: paramList, dataType: "json", success: successFn, error: errorFn
                });

            }
            function AjaxSucceeded(result) {
                $("#txtCODE").val(result.d);
            }
            function AjaxFailed(result) { }

            var CalledFrom = $("#hidCALLED_FROM").val();

            if (CalledFrom == "MRTG") { $("#InsuredObject").hide(); }
            if (CalledFrom != "MRTG") {
                $("#chkIS_SPOUSE_OR_CHILD").click(function() {

                    if ($("#chkIS_SPOUSE_OR_CHILD").attr("checked") == true) {
                        $("#cmbMAIN_INSURED").show();
                        $("#capMAIN_INSURED").show();
                    }
                    else {
                        $("#cmbMAIN_INSURED").hide();
                        $("#capMAIN_INSURED").hide();
                    }
                });
            }
        });
        function HideValidator(id) {
            document.getElementById(id).style.display = "none";
            document.getElementById(id).setAttribute('isValid', false);
            document.getElementById(id).setAttribute('enabled', false);
            document.getElementById('cstREG_ID_ISSUE').setAttribute('enabled', true); //FOR PERSONAL
            document.getElementById('cstREG_ID_ISSUE').setAttribute('enabled', false);//GOVERMENT AND COMMERCIAL
            document.getElementById('cstREG_ID_ISSUE').setAttribute('enabled', false);
        }
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";
        function ResetTheForm() {
            document.POL_PERSONAL_ACCIDENT_INFO.reset();
        }
      
        function ChkDateOfBirth(objSource, objArgs) {

            if (document.getElementById("revDATE_OF_BIRTH").isvalid == true) {
            
                var effdate = document.POL_PERSONAL_ACCIDENT_INFO.txtDATE_OF_BIRTH.value;
                objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
            }
            else
                objArgs.IsValid = true;

        }
        function ChkREG_ID_ISSUES(objSource, objArgs) {
            if (document.getElementById("revREG_ID_ISSUES").isvalid == true) {

                var effdate = document.POL_PERSONAL_ACCIDENT_INFO.txtREG_ID_ISSUES.value;
                objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
            }
            else
                objArgs.IsValid = true;
        }

        function setTab() {
           
            var pagefrom = '<%=PAGEFROM %>'
            var firsttab = '<%=FIRSTTAB %>'
            if (pagefrom == 'QAPP') {
                EnableValidator('rfvAPPLICANT', false);
                document.getElementById("spn_mandatory").style.display = 'none';
                document.getElementById("rfvAPPLICANT").style.display = 'none'
            }
            else {
                EnableValidator('rfvAPPLICANT', true);

            }
            if (document.getElementById('hidPERSONAL_INFO_ID') != null && document.getElementById('hidPERSONAL_INFO_ID').value != "NEW" && document.getElementById('hidPERSONAL_INFO_ID').value != "") {
                if (document.getElementById('hidPERSONAL_INFO_ID') != null)
                    riskId = document.getElementById('hidPERSONAL_INFO_ID').value;
                
                var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
                var tabtitles = TAB_TITLES.split(',');
                var CalledFrom = document.getElementById('hidCALLED_FROM').value;

                if (pagefrom == "QAPP") {// changed by sonal to implemt this in quick app

                    Url = parent.document.getElementById('hidFrameUrl').value + riskId + "&";
                    DrawTab(1, top.frames[1], firsttab, Url);
                }
                var CO_APPLICANT_ID = "0";
                if (document.getElementById('hidAPPLICANT_ID') != null && document.getElementById('hidAPPLICANT_ID').value != "")
                    CO_APPLICANT_ID = document.getElementById('hidAPPLICANT_ID').value;
                    
                if (CalledFrom != '' && CalledFrom == "INDPA" || CalledFrom == "CPCACC" || CalledFrom == "GRPLF" || CalledFrom == "MRTG") 
                {

                    Url = "/Cms/Policies/aspx/AddDiscountSurcharge.aspx?CalledFrom=" + CalledFrom + "&pageTitle=" + tabtitles[0] + "&RISK_ID=" + riskId + "&CO_APPLICANT_ID=" + CO_APPLICANT_ID + "&";
                    DrawTab(2, top.frames[1], tabtitles[0], Url);
                  

                    if (CalledFrom == "INDPA" || CalledFrom == "CPCACC" || CalledFrom == "GRPLF")
                    {

                        Url = "/Cms/Policies/aspx/BeneficiaryIndex.aspx?CalledFrom=" + CalledFrom + "&pageTitle=" + tabtitles[1] + "&RISK_ID=" + riskId + "&CO_APPLICANT_ID=" + CO_APPLICANT_ID + "&";
                        DrawTab(3, top.frames[1], tabtitles[1], Url);

                        Url = "/Cms/Policies/aspx/AddpolicyCoverages.aspx?CalledFrom=" + CalledFrom + "&pageTitle=" + tabtitles[1] + "&RISK_ID=" + riskId + "&CO_APPLICANT_ID=" + CO_APPLICANT_ID + "&";
                        DrawTab(4, top.frames[1], tabtitles[2], Url);

                    }
                    else
                    {

                    Url = "/Cms/Policies/aspx/AddpolicyCoverages.aspx?CalledFrom=" + CalledFrom + "&pageTitle=" + tabtitles[1] + "&RISK_ID=" + riskId + "&";
                    DrawTab(3, top.frames[1], tabtitles[1], Url);
                    }



                }

                
               
            }
            else {
                RemoveTab(4, top.frames[1]);
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]);
            }
            return false;
        }
        function InitPage() {
            ApplyColor(); 
            ChangeColor();
            setTab();
        }
    </script>
    <script language="javascript" type="text/javascript">
    
    
        //validate CPf 
//            function validatCPF_CNPJ(objSource, objArgs) {
//            //get error message for xml on culture base. 
//            var cpferrormsg = '<%=javasciptCPFmsg %>';  
//            var CPF_invalid = '<%=CPF_invalid %>';     

//            var valid = false;
//            var idd = objSource.id;
//            var rfvid = idd.replace('csv', 'rev');
//            if (document.getElementById(rfvid) != null)
//                if (document.getElementById(rfvid).isvalid == true) {
//                var theCPF = document.getElementById(objSource.controltovalidate)
//                var len = theCPF.value.length;

//                //To check cpf format & valdate through validar() function, CPF is valid or not
//                if (len == '14') {
//                    valid = validar(objSource, objArgs);
//                    if (valid) { objSource.innerText = ''; } else { objSource.innerText = CPF_invalid; }
//                }
//                else {
//                    if (document.getElementById(rfvid) != null) {
//                        if (document.getElementById(rfvid).isvalid == true) {
//                            objArgs.IsValid = false;
//                            objSource.innerHTML = cpferrormsg; //'Please enter 14 digit CPF No';
//                        } else {
//                            objSource.innerHTML = '';
//                        }
//                    }
//                }
//            }

//        }
        //function validate CPF/CNPJ # .
        //created by Brazil team

        // itrackno 839 praveer panghal


        function validatCPF_CNPJ(objSource, objArgs) {
            //get error message for xml on culture base. 
            var cpferrormsg = '<%=javasciptCPFmsg %>';
            var cnpjerrormsg = '<%=javasciptCNPJmsg %>';
            var CPF_invalid = '<%=CPF_invalid %>';
            var CNPJ_invalid = '<%=CNPJ_invalid %>';

            var valid = false;
            var idd = objSource.id;
            var rfvid = idd.replace('csv', 'rev');
            if (document.getElementById(rfvid) != null)
                if (document.getElementById(rfvid).isvalid == true) {
                var theCPF = document.getElementById(objSource.controltovalidate)
                var len = theCPF.value.length;
                if (document.getElementById('hidTYPE').value == '11110') {

                    //for CPF # in if customer type is personal
                    //it check cpf format & valdate bia validar() function, CPF is valid or not
                    if (len == '14') {
                        valid = validar(objSource, objArgs);
                        if (valid) { objSource.innerText = ''; } else { objSource.innerText = CPF_invalid; }
                    }
                    else {

                        if (document.getElementById(rfvid) != null) {
                            if (document.getElementById(rfvid).isvalid == true) {
                                objArgs.IsValid = false;
                                objSource.innerHTML = cpferrormsg; //'Please enter 14 digit CPF No';
                            } else {
                                objSource.innerHTML = '';
                            }
                        }
                    }
                } //for CNPJ # in if customer type is commercial
                //it check CNPJ format & valdate bia validar() function CNPJ is valid or not
                else if (document.getElementById('hidTYPE').value == '11109' || document.getElementById('hidTYPE').value == '14725') {
                    if (len == '18') {
                        valid = validar(objSource, objArgs);
                        if (!valid) {
                            objSource.innerText = CNPJ_invalid;
                        } else { objSource.innerText = ''; }
                    }
                    else {
                        if (document.getElementById(rfvid) != null) {
                            if (document.getElementById(rfvid).isvalid == true) {
                                objArgs.IsValid = false;
                                objSource.innerHTML = cnpjerrormsg; //'validate';
                            } else { objSource.innerHTML = ''; }
                        }
                    }
                }
            } else { objSource.innerHTML = ''; }
        }

        
        function validar(objSource, objArgs) {

            var theCPF = document.getElementById(objSource.controltovalidate)
           
            var intval = "0123456789";
            var val = theCPF.value;
            var flag = false;
            var realval = "";
            for (l = 0; l < val.length; l++) {
                ch = val.charAt(l);
                flag = false;
                for (m = 0; m < intval.length; m++) {
                    if (ch == intval.charAt(m)) {
                        flag = true;
                        break;
                    }
                } if (flag)
                    realval += val.charAt(l);
            }
            if (((realval.length == 11) && (realval == 11111111111) || (realval == 22222222222) || (realval == 33333333333) || (realval == 44444444444) || (realval == 55555555555) || (realval == 66666666666) || (realval == 77777777777) || (realval == 88888888888) || (realval == 99999999999) || (realval == 00000000000))) {
                objArgs.IsValid = false;
                return (false);
            }
            if (!((realval.length == 11) || (realval.length == 14))) {
                objArgs.IsValid = false;
                return (false);
            }
            var checkOK = "0123456789";
            var checkStr = realval;
            var allValid = true;
            var allNum = "";
            for (i = 0; i < checkStr.length; i++) {
                ch = checkStr.charAt(i);
                for (j = 0; j < checkOK.length; j++)
                    if (ch == checkOK.charAt(j))
                    break;
                if (j == checkOK.length) {
                    allValid = false;
                    break;
                }
                allNum += ch;
            }
            if (!allValid) {
                objArgs.IsValid = false;
                return (false);
            }
            var chkVal = allNum;
            var prsVal = parseFloat(allNum);
            if (chkVal != "" && !(prsVal > "0")) {
                objArgs.IsValid = false;
                return (false);
            }
            if (realval.length == 11) {
                var tot = 0;
                for (i = 2; i <= 10; i++)
                    tot += i * parseInt(checkStr.charAt(10 - i));

                if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(9))) {
                    objArgs.IsValid = false;
                    return (false);
                }
                tot = 0;
                for (i = 2; i <= 11; i++)
                    tot += i * parseInt(checkStr.charAt(11 - i));

                if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(10))) {
                    objArgs.IsValid = false;
                    return (false);
                }
            }
            else {
                var tot = 0;
                var peso = 2;
                for (i = 0; i <= 11; i++) {
                    tot += peso * parseInt(checkStr.charAt(11 - i));
                    peso++;
                    if (peso == 10) {
                        peso = 2;
                    }
                }
                if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(12))) {
                    objArgs.IsValid = false;
                    return (false);
                }
                tot = 0;
                peso = 2;
                for (i = 0; i <= 12; i++) {
                    tot += peso * parseInt(checkStr.charAt(12 - i));
                    peso++;
                    if (peso == 10) {
                        peso = 2;
                    }
                }
                if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(13))) {
                    objArgs.IsValid = false;
                    return (false);
                }
            }
        }
        
 
    </script>
</head>
<body  leftMargin="0" topMargin="0" onload="InitPage();"><%--HideShowTDBasedOnCalledFrom();">--%>
    <form id="POL_PERSONAL_ACCIDENT_INFO" runat="server"  method="post">
        <table width="100%"  class="tableWidthHeader" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table width="100%" class="tableWidthHeader" cellSpacing="0" cellPadding="0" border="0" align="center">
                        <tr>
                             <td class="midcolorc" align="right" colSpan="3">
                                <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                             </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="midcolorc" align="right">
                    <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="pageHeader" class="midcolora">
                    &nbsp;</td>
            </tr>
           
           
           
            <tr id="trBody" runat="server">
                
                <td >
                    <table width="100%" class="tableWidthHeader" cellspacing="2" cellpadding="2" border="0">
                     <tr>
                <td class="pageHeader" class="midcolora">
                 <asp:Label runat="server" ID="capMAN_MSG"></asp:Label>
                </td>
            </tr>
                    <tr>
                    <td id="td1" class="midcolora" colspan="3">
                    <asp:label ID="capAPPLICANT" runat="server" ></asp:label><span class="mandatory" id="spn_mandatory">*</span>
                    
                    <br />
                    <asp:DropDownList ID="cmbAPPLICANT_ID" runat="server"></asp:DropDownList>
                    
                    
                    <cmsb:cmsbutton class="clsButton" id="btnSelect" runat="server" Text="Select" CausesValidation="false"  onclick="btnSelect_Click" 
                         /><br />
                        <asp:requiredfieldvalidator id="rfvAPPLICANT" runat="server" ControlToValidate="cmbAPPLICANT_ID" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                </td>
                    </tr>
                         <tr id="InsuredObject">
                             <td width="33%" class="midcolora">
                                 <asp:Label ID="capIS_SPOUSE_OR_CHILD" runat="server"  ></asp:Label> <br />
                                 <asp:CheckBox ID="chkIS_SPOUSE_OR_CHILD" runat="server" /></td>
                                 
                             <td width="33%" class="midcolora">
                                 <asp:Label ID="capMAIN_INSURED" runat="server" ></asp:Label><br />
                                 <asp:DropDownList ID="cmbMAIN_INSURED" runat="server">
                                 </asp:DropDownList>
                                 </td>
                             
                             <td width="33%" class="midcolora">
                             
                                 </td>
                            
                             
                         </tr>
                         <tr>
                             <td width="33%" class="midcolora">
                                 <asp:Label ID="capINDIVIDUAL_NAME" runat="server"> </asp:Label><span class="mandatory">*</span>
                                 <br />
                                 <asp:TextBox ID="txtINDIVIDUAL_NAME" runat="server" Width="191px" 
                                     onkeypress="MaxLength(this,30)" onpaste="MaxLength(this,30)" MaxLength="30"></asp:TextBox><br />
                                 <asp:requiredfieldvalidator id="rfvINDIVIDUAL_NAME" runat="server" ControlToValidate="txtINDIVIDUAL_NAME" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                             </td>
                             <td width="33%" class="midcolora">
                                <asp:Label ID="capCODE" runat="server"> </asp:Label><span class="mandatory">*</span>
                                 <br />
                                 <asp:TextBox ID="txtCODE" runat="server" Width="191px" 
                                     onkeypress="MaxLength(this,10)" onpaste="MaxLength(this,10)" MaxLength="10"></asp:TextBox><br />
                                 <asp:requiredfieldvalidator id="rfvCODE" runat="server" ControlToValidate="txtCODE" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                             </td>
                             
                             <td width="33%" class="midcolora">
                                <asp:Label ID="capGENDER" runat="server"> </asp:Label><span id="spnGENDER" runat ="server" class="mandatory">*</span>
                                 <br />
                                 <asp:DropDownList ID="cmbGENDER" runat="server" Height="16px" Width="191px">
                                 </asp:DropDownList><br />
                                  <asp:requiredfieldvalidator id="rfvGENDER" runat="server" ControlToValidate="cmbGENDER" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                                 
                             </td>
                            
                             
                         </tr>
                         <tr>
                             <td width="33%" class="midcolora">
                                <asp:Label ID="capCPF_NUM" runat="server"> </asp:Label><span class="mandatory">*</span>
                                 <br />
                                 <asp:TextBox ID="txtCPF_NUM" runat="server" OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();"  Width="191px" onkeypress="MaxLength(this,18)" onpaste="MaxLength(this,18)"></asp:TextBox>
                                 <br />
                                 <asp:requiredfieldvalidator id="rfvCPF_NUM" runat="server" ControlToValidate="txtCPF_NUM" Display="Dynamic"></asp:requiredfieldvalidator>
                                 <asp:RegularExpressionValidator runat="server" ID="revCPF_NUM" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_NUM"></asp:RegularExpressionValidator>
                                 <asp:CustomValidator runat="server" ID="csvCPF_NUM" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_NUM" ClientValidationFunction="validatCPF_CNPJ"></asp:CustomValidator>
                             </td>
                             <td width="33%" class="midcolora">
                                <asp:Label ID="capSTATE_ID" runat="server"> </asp:Label><span id="spnSTATE_ID"  runat ="server" class="mandatory">*</span> 
                                 <br />
                                 <asp:DropDownList ID="cmbSTATE_ID" runat="server" Height="16px" Width="191px">
                                 </asp:DropDownList><br />
                               <asp:requiredfieldvalidator id="rfvSTATE_ID" runat="server" ControlToValidate="cmbSTATE_ID" 
								 Display="Dynamic"> </asp:requiredfieldvalidator>
								                              </td>
                             <td width="34%" class="midcolora">
                                <asp:Label ID="capDATE_OF_BIRTH" runat="server"></asp:Label><span class="mandatory">*</span> </asp:Label>
                                 <br />
                                 <asp:TextBox ID="txtDATE_OF_BIRTH" runat="server" SIZE="15"> </asp:TextBox>
                                 <asp:hyperlink id="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot"> <asp:Image runat="server" ID="imgDATE_OF_BIRTH" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
				                 </asp:hyperlink><br />
	                                <asp:requiredfieldvalidator id="rfvDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
			                     <asp:regularexpressionvalidator id="revDATE_OF_BIRTH" Runat="server" ControlToValidate="txtDATE_OF_BIRTH"
								 Display="Dynamic"></asp:regularexpressionvalidator>
								
								 <asp:customvalidator id="csvDATE_OF_BIRTH" Runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"
								ClientValidationFunction="ChkDateOfBirth"></asp:customvalidator>
								
                             </td>
                         </tr>
                         <tr>
                         <td width="34%" class="midcolora" colspan="1">
                                <asp:Label ID="capREG_ID_ORG" runat="server"> </asp:Label><span id="spnREG_ID_ORG" runat ="server" class="mandatory">*</span>
                                 <br />
                                 <asp:TextBox ID="txtREG_ID_ORG" runat="server" Width="191px" 
                                     onkeypress="MaxLength(this,12)" onpaste="MaxLength(this,12)" MaxLength="12"></asp:TextBox><br />
                                      <asp:requiredfieldvalidator id="rfvREG_ID_ORG" runat="server" ControlToValidate="txtREG_ID_ORG" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                             </td>
                             <td width="33%" class="midcolora">
                                <asp:Label ID="capREG_IDEN" runat="server"> </asp:Label><span class="mandatory">*</span>
                                 <br />
                                 <asp:TextBox ID="txtREG_IDEN" runat="server" Width="191px" 
                                     onkeypress="MaxLength(this,12)" onpaste="MaxLength(this,12)" MaxLength="12"></asp:TextBox><br />
                                 <asp:requiredfieldvalidator id="rfvREG_IDEN" runat="server" ControlToValidate="txtREG_IDEN" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                             </td>
                                 
                             <td width="34%" class="midcolora">
                                <asp:Label ID="capREG_ID_ISSUES" runat="server"> </asp:Label><span class="mandatory">*</span>
                                 <br />
                                 <asp:TextBox ID="txtREG_ID_ISSUES" runat="server" SIZE="15"> </asp:TextBox>
                                
                                 <asp:hyperlink id="hlkREG_ID_ISSUES" runat="server" CssClass="HotSpot"> <asp:Image runat="server" ID="imgREG_ID_ISSUES" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></asp:Image>
				                 </asp:hyperlink><br />
				                
				                  <asp:requiredfieldvalidator id="rfvREG_ID_ISSUES" runat="server" ControlToValidate="txtREG_ID_ISSUES" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
				                 <asp:regularexpressionvalidator id="revREG_ID_ISSUES" Runat="server" ControlToValidate="txtREG_ID_ISSUES"
								 Display="Dynamic"></asp:regularexpressionvalidator>
								
					            <asp:comparevalidator id="cpvREG_ID_ISSUES" ControlToValidate="txtREG_ID_ISSUES" Display="Dynamic" Runat="server" ControlToCompare="txtDATE_OF_BIRTH" Type="Date" Operator="GreaterThan"></asp:comparevalidator>
					            <asp:customvalidator id="csvREG_ID_ISSUES" Runat="server" ControlToValidate="txtREG_ID_ISSUES" Display="Dynamic" ClientValidationFunction="ChkREG_ID_ISSUES"></asp:customvalidator>
			                     
								
                             </td>
                         </tr>
                         <tr>
                            <td width="33%" class="midcolora" colspan="3">
                                <asp:Label ID="capPOSITION_ID" runat="server"> </asp:Label><span class="mandatory">*</span>
                                 <br />
                                 <asp:DropDownList ID="cmbPOSITION_ID" runat="server" Height="16px" 
                                     Width="90%">
                                 </asp:DropDownList><br />
                                 <asp:requiredfieldvalidator id="rfvPOSITION_ID" runat="server" ControlToValidate="cmbPOSITION_ID" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                             </td> 
                             </tr>
                         <tr>
                            <td width="33%" class="midcolora" colspan="1">
                            <asp:Label ID="capCITY_OF_BIRTH" runat="server"> </asp:Label><span class="mandatory">*</span><br />
                            <asp:TextBox ID="txtCITY_OF_BIRTH" runat="server" MaxLength="100" Width="271px" 
                                    AutoCompleteType="Disabled"></asp:TextBox><br />
                              <asp:requiredfieldvalidator id="rfvCITY_OF_BIRTH" runat="server" ControlToValidate="txtCITY_OF_BIRTH" 
								 Display="Dynamic"></asp:requiredfieldvalidator>
                             </td> 
                             
                             <td  width="33%" class="midcolora">
                             
                             
                              <asp:Label ID="capMARITAL_STATUS" runat="server"></asp:Label><span id="spnMARITAL_STATUS" runat="server" class="mandatory">*</span>
            
                 <br />
             <asp:DropDownList ID="cmbMARITAL_STATUS" runat="server"  onfocus="SelectComboIndex('cmbMARITAL_STATUS')">
            </asp:DropDownList>       <br />
           <asp:RequiredFieldValidator ID="rfvMARITAL_STATUS" runat="server"  ControlToValidate="cmbMARITAL_STATUS" Display="Dynamic" 
                ErrorMessage=""></asp:RequiredFieldValidator></td>

                <td class="midcolora" Width="32%" >
      <asp:Label ID="capExceededPremium" runat="server" Text="" ></asp:Label><br />
      <asp:DropDownList ID="cmbExceeded_Premium" runat="server">
      </asp:DropDownList>
        </td>
                             </tr>
                             <tr>
                             <td width="34%" class="midcolora" colspan="3">
                                <asp:Label ID="capREMARKS" runat="server"> </asp:Label>
                                 <br />
                                 <asp:TextBox ID="txtREMARKS" runat="server" TextMode="MultiLine" Wrap="true" 
                                     Height="139px" Width="270px" MaxLength= "4000" onkeypress="MaxLength(this,4000)" onpaste="MaxLength(this,4000)" ></asp:TextBox>
                             </td>
                         </tr>
                         <tr> 
                            <td class="midcolora" colspan="3">
                             <table width="100%" class="tableWidthHeader">
                                 <tr>
                                     <td class="midcolora" width="5%">
                                        <cmsb:cmsbutton class="clsButton" id="btnReset" runat="server"></cmsb:cmsbutton>
                                     </td>
                                     <td align="left" width="45%">
                                        <cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" CausesValidation="false" Text="ActivateDeactivate" onclick="btnActivateDeactivate_Click" />
                                     </td>
                                     <td width="45%" align="right">
                                        <cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server"  causesvalidation="false" align="right" onclick="btnDelete_Click"></cmsb:cmsbutton>
                                     </td>
                                     <td align="right" width="5%"> 
                                        <cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" onclick="btnSave_Click"></cmsb:cmsbutton>
                                     </td>
                                 </tr>
                             </table>
                             </td>
                         </tr>
                    </table>
                </td>
            </tr>
        </table>
     <input id="hidPOL_ID" type="hidden" name="hidPOL_ID" runat="server"/>
     <input id="hidPOL_VERSION_ID" type="hidden" name="hidPOL_VERSION_ID" runat="server"/>
	 <input id="hidCUSTOMER_ID" type="hidden" name="hidCUSTOMER_ID" runat="server" />
	 <input id="hidPERSONAL_INFO_ID" type="hidden" value="" name="hidPERSONAL_INFO_ID" runat="server"/>
	 <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server"/>
	 <input  type="hidden" runat="server" ID="hidCALLED_FROM"  value=""  name="hidCALLED_FROM"/>   
	 <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/> 	 
	 <input  type="hidden" runat="server" ID="hidTYPE"  value=""  name="hidTYPE"/> 	
	  <input  type="hidden" runat="server" ID="hidAPPLICANT_ID"  value=""  name="hidAPPLICANT_ID"/> 	
    </form>
    <script type="text/javascript" >
        if (document.getElementById('hidFormSaved').value == "1") {
            var pagefrom = '<%=PAGEFROM %>'
            if (pagefrom == "QAPP") {

                parent.BindRisk();
                for (i = 0; i < parent.document.getElementById('cmbRisk').options.length; i++) {

                    if (parent.document.getElementById('cmbRisk').options[i].value == document.getElementById('hidPERSONAL_INFO_ID').value)
                        parent.document.getElementById('cmbRisk').options[i].selected = true;


                }
            }
            else {
                try {
                    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidPERSONAL_INFO_ID').value);
                }
                catch (err) {

                }
            }
        }
	</script>
</body>
</html>
