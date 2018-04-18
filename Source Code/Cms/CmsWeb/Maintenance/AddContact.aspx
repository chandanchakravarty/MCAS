<%@ Page validateRequest="false" language="c#" Codebehind="AddContact.aspx.cs" AutoEventWireup="false" EnableViewState="true"  Inherits="Cms.CmsWeb.Maintenance.AddContact"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ADDCONTACT</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet" >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
	    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
	    <script type="text/javascript" src="/cms/cmsweb/scripts/CmsHelpScript/jQueryPageHelpFile.js"></script>
	    <script src="/cms/cmsweb/scripts/Calendar.js"></script> 
		<script language="javascript">
			function ResetTheForm()
			{
//				var url;						
//				var strContactID = new String();
////				strContactID = document.getElementById('hidCONTACT_ID').value;
////				
////				if(strContactID!="" && strContactID.toUpperCase()!="NEW")
////					url="AddContact.aspx?CONTACTTYPEID=&EntityId=&CONTACT_ID=" + document.getElementById("hidCONTACT_ID").value + "&CONTACT_TYPE_ID=" + document.getElementById("cmbCONTACT_TYPE_ID").options[document.getElementById("cmbCONTACT_TYPE_ID").selectedIndex].value + "&transferdata=";
////				else
////					url="AddContact.aspx?CONTACTTYPEID=&EntityId=&&transferdata=";
//				//				window.location.href=url;
				document.ADDCONTACT.reset();
				return false;
				
			}	
			function DoBack()
			{
				this.parent.parent.document.location.href = "/cms/client/aspx/CustomerManagerIndex.aspx";
				return false;
			}				
			function AddData()
			{
			
				{
				   if(document.getElementById('cmbCONTACT_TYPE_ID').disabled)
						document.getElementById('txtCONTACT_CODE').focus();
					else
						document.getElementById('cmbCONTACT_TYPE_ID').focus();
					
					document.getElementById('hidCONTACT_ID').value	=	'New';
					document.getElementById('txtCONTACT_CODE').value  = '';
					document.getElementById('txtCONTACT_FNAME').value  = '';
					document.getElementById('txtCONTACT_MNAME').value  = '';
					document.getElementById('txtCONTACT_LNAME').value  = '';
					document.getElementById('txtCONTACT_ADD1').value  = '';
					document.getElementById('txtCONTACT_ADD2').value = '';
					document.getElementById('txtDISTRICT').value = '';
					document.getElementById('txtCONTACT_CITY').value  = '';
					document.getElementById('txtCONTACT_ZIP').value  = '';
					document.getElementById('txtCONTACT_EXT').value  = '';
					document.getElementById('txtCONTACT_BUSINESS_PHONE').value  = '';
					document.getElementById('txtCONTACT_EMAIL').value  = '';
					document.getElementById('txtCONTACT_FAX').value  = '';
					document.getElementById('txtCONTACT_MOBILE').value  = '';
					document.getElementById('txtCONTACT_PAGER').value  = '';
					document.getElementById('txtCONTACT_HOME_PHONE').value  = '';
					document.getElementById('txtCONTACT_TOLL_FREE').value  = '';
					document.getElementById('txtCONTACT_NOTE').value  = '';
					
//					document.getElementById('cmbCONTACT_POS').options.selectedIndex = -1;
//					document.getElementById('cmbCONTACT_COUNTRY').options.selectedIndex = 0;
//					document.getElementById('cmbCONTACT_SALUTATION').options.selectedIndex = -1;
//					document.getElementById('cmbCONTACT_STATE').options.selectedIndex = -1;
					//document.getElementById('cmbCONTACT_TYPE_ID').options.selectedIndex = -1;

					if (document.getElementById('CCcmbINDIVIDUAL_CONTACT_ID') != null) {
					   
					  var callfrom='<%=Request.QueryString["CallFrom"] %>'
					  if (callfrom == "CUSTOMER")
					  { }
					  else { document.getElementById('CCcmbINDIVIDUAL_CONTACT_ID').options.selectedIndex = -1; }
					  
						//if(parent.document.getElementById("hidMode").value == "Edit")
							//document.getElementById('cmbINDIVIDUAL_CONTACT_ID').options.selectedIndex = -1;
					}
					else
						document.getElementById('spnIndContactID').style.display="none";
					 			
					if(document.getElementById('btnActivateDeactivate'))				
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);	
					
					// Commented by mohit on 14/10/2005.
					if(document.getElementById('btnDelete'))
						document.getElementById('btnDelete').setAttribute('disabled',true); 
					 
					//document.getElementById('txtCONTACT_EXT').setAttribute('readOnly',true);
				}		
			
				ChangeColor();
				DisableValidators();		
			}   
					
			function populateXML()
			{
				 
				// Added by mohit on 14/10/2005.   	
				if(document.getElementById('hidCONTACT_ID').value=="" || document.getElementById('hidCONTACT_ID').value=="NEW" || document.getElementById('hidCONTACT_ID').value=="New")			
					document.getElementById('btnDelete').setAttribute('disabled',true); 
					
				//setting xml of top frame generated by the page itself
	var tempXML = document.getElementById('hidOldData').value;
	//alert(tempXML);
	//Commented to implement Coolite Combobox control -itrack 1557
//				if(document.getElementById('cmbINDIVIDUAL_CONTACT_ID')==null)
	//				    document.getElementById('spnIndContactID').style.display = "none";
	//Commented till here 
	if (document.getElementById('CCcmbINDIVIDUAL_CONTACT_ID') == null)  //Changed by Aditya for TFS bug # 923
				    document.getElementById('spnIndContactID').style.display = "none";
                    	
				if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
				{											
					if(tempXML!="")
					{
					    							
						//Enabling the activate deactivate button
						if(document.getElementById('btnActivateDeactivate'))
						document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
						
						if(document.getElementById('btnDelete'))
						document.getElementById('btnDelete').setAttribute('disabled',false);  
						                        
						//Start: ****default customer case hiding individual contact id if contact type is personal(6)
						var objXmlHandler = new XMLHandler();
						var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('Table')[0]);
						//document.getElementById("hidSTATE_ID").value=
						var i=0;
						for(i=0;i<tree.childNodes.length;i++)
						{
							if(!tree.childNodes[i].firstChild) continue;
							var nodeName = tree.childNodes[i].nodeName;
							if(nodeName!="CONTACT_TYPE_ID")
								continue;
							else
								{
									var nodeValue = tree.childNodes[i].firstChild.text;
									if(nodeValue=="6")	
									{
										document.getElementById('spnIndContactID').style.display="none";
									}
										
								}
						}
						//End:**** default customer case hiding individual contact id if contact type is personal(6)
						
						//populateFormData(tempXML,AddContact);
						
					}
					else
					{
						//AddData();
					}
				}
				else
				{
					ChangeColor();
				}
				if(document.getElementById("cmbCONTACT_TYPE_ID").value == 2)
				{
					//Customer type is customer , hence visible pullcustomer button row if hidCustAddXml contains some value
					if (document.forms[0].hidCustAddXml.value != "")
					{
						document.getElementById("trPullCustomerAddress").style.display = "inline";
					}
					else
					{
						document.getElementById("trPullCustomerAddress").style.display = "none";
					}
				}
				else
				{
					document.getElementById("trPullCustomerAddress").style.display = "none";
				}
				
				return false;
			}
			function ComboIndexChanged()
			{
				document.getElementById('hidFormSaved').value=3;
			}
			function CheckIfPhoneEmpty() {
			    if (document.getElementById('txtCONTACT_BUSINESS_PHONE').value == "") {
			        document.getElementById('txtCONTACT_EXT').value = ""
			    }
			}


	function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbCONTACT_SALUTATION":
						lookupMessage	=	"";
						break;
					default:
						lookupMessage	=	"";
						break;
						
				}
				showLookupLayer(controlId, lookupMessage);
				//populateXML();
}
function fillstateFromCountry() {
   
    GlobalError = true;
    //var CmbState=document.getElementById('cmbCUSTOMER_STATE');
    var CountryID = document.getElementById('cmbCONTACT_COUNTRY').options[document.getElementById('cmbCONTACT_COUNTRY').selectedIndex].value;
    //var oResult='';
    AddContact.AjaxFillState(CountryID, fillState);

    //fillState(oResult);
    if (GlobalError) {
        return false;
    }
    else {
        return true;
    }
}

function fillState(Result) {
    //var strXML;
    if (Result.error) {
        var xfaultcode = Result.errorDetail.code;
        var xfaultstring = Result.errorDetail.string;
        var xfaultsoap = Result.errorDetail.raw;
    }
    else {
        var statesList = document.getElementById("cmbCONTACT_STATE");
        statesList.options.length = 0;
        var len = statesList.options.length;
        for (i = len - 1; i >= 0; i--) {
            statesList.options.remove(i);
        }
        oOption = document.createElement("option");
        oOption.value = "";
        oOption.text = "";
        statesList.add(oOption);
        ds = Result.value;
        if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
            for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {
                statesList.options[statesList.options.length] = new Option(ds.Tables[0].Rows[i]["STATE_NAME"], ds.Tables[0].Rows[i]["STATE_ID"]);
            }
        }
        if (document.getElementById('hidSTATE_ID')) {
            if (document.getElementById('hidSTATE_ID').value != "") {
                for (var i = 0; i < statesList.options.length; i++) {
                    if (statesList.options[i].value == document.getElementById('hidSTATE_ID').value) {
                        statesList.options[i].selected = true;
                    }
                }
             }
         }
    }  

    return false;
}
function setStateId() {
    if (document.getElementById("cmbCONTACT_STATE") != null) {
        document.getElementById('hidSTATE_ID').value = document.getElementById('cmbCONTACT_STATE').value;
        
    }
 }
 function GenerateCustomerCode(fncntrl, lnamecntrl, codecntrl) {
     if (document.getElementById('hidCONTACT_ID').value == "" || document.getElementById('hidCONTACT_ID').value.toUpperCase() == 'NEW' || document.getElementById('hidCONTACT_ID').value == '0') {
         if (document.getElementById(fncntrl).value != "") {
             if (document.getElementById(lnamecntrl).value != "") {
                 document.getElementById(codecntrl).value = (GenerateRandomCode(ReplaceAll(document.getElementById(fncntrl).value, " ", ""), ReplaceAll(document.getElementById(lnamecntrl).value," ","")));
             } else {
             document.getElementById(codecntrl).value = (GenerateRandomCode(ReplaceAll(document.getElementById(fncntrl).value, " ", ""), ''));

             }

         }
     } 
}


function FormatZipCode(vr) {
    //debugger;
    var vr = new String(vr.toString());
    if (vr != "" && (document.getElementById('cmbCONTACT_COUNTRY').options[document.getElementById('cmbCONTACT_COUNTRY').options.selectedIndex].value == '5')) {

        vr = vr.replace(/[-]/g, "");
        num = vr.length;
        if (num == 8 && (document.getElementById('cmbCONTACT_COUNTRY').options[document.getElementById('cmbCONTACT_COUNTRY').options.selectedIndex].value == '5')) {
            vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
            document.getElementById('revCONTACT_ZIP').setAttribute('enabled', false); //Changed by aditya
           
        }

    }

    return vr;
}

function validatCPF_CNPJ(objSource, objArgs) {

    var cpferrormsg = '<%=javasciptCPFmsg %>';
    var CPF_invalid = '<%=CPF_invalid %>';
    var valid = false;
    var idd = objSource.id;
    var rfvid = idd.replace('csv', 'rev');
    if (document.getElementById(rfvid) != null)
        if (document.getElementById(rfvid).isvalid == true) {
        var theCPF = document.getElementById(objSource.controltovalidate)
        var len = theCPF.value.length;
        if (len == '14') {
            valid = validar(objSource, objArgs);
            if (valid) { objSource.innerText = ''; } else { objSource.innerText = CPF_invalid; }
        }
        else {
            if (document.getElementById(rfvid) != null) {
                if (document.getElementById(rfvid).isvalid == true) {
                    objArgs.IsValid = false;
                    objSource.innerHTML = cpferrormsg; 
                } else { objSource.innerHTML = ''; }
            }
        }
    }
}

function validar(objSource, objArgs) {
 
    var theCPF = document.getElementById(objSource.controltovalidate)
    var errormsg = '<%=javasciptmsg  %>'
    var ermsg = errormsg.split(',');
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
        objSource.innerHTML = ermsg[1];
        return (false);
    }

    if (!((realval.length == 11) || (realval.length == 14))) {
        objSource.innerHTML = ermsg[1];
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
        objSource.innerHTML = ermsg[2];
        objArgs.IsValid = false;
        return (false);
    }

    var chkVal = allNum;
    var prsVal = parseFloat(allNum);
    if (chkVal != "" && !(prsVal > "0")) {
        objSource.innerHTML = ermsg[3];
        objArgs.IsValid = false;
        return (false);
    }

    if (realval.length == 11) {
        var tot = 0;
        for (i = 2; i <= 10; i++)
            tot += i * parseInt(checkStr.charAt(10 - i));

        if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(9))) {
            objSource.innerHTML = ermsg[1];
            objArgs.IsValid = false;
            return (false);
        }

        tot = 0;

        for (i = 2; i <= 11; i++)
            tot += i * parseInt(checkStr.charAt(11 - i));
        if ((tot * 10 % 11 % 10) != parseInt(checkStr.charAt(10))) {
            objSource.innerHTML = ermsg[1];
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
            objSource.innerHTML = ermsg[1];
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
            objSource.innerHTML = ermsg[1];
            objArgs.IsValid = false;
            return (false);
        }
    }
    return (true);
}
		</script>
		  <%--Populate the Address based on the ZipeCode using jQuery ------- Added by Pradeep Kushwaha on 15 june 2010--%>
        <script type="text/javascript" language="javascript">
            $(document).ready(function() { 
                $("#txtCONTACT_ZIP").change(function() {
                    if (trim($('#txtCONTACT_ZIP').val()) != '') {
                        var ZIPCODE = $("#txtCONTACT_ZIP").val();
                        var COUNTRYID = "5";
                        ZIPCODE = ZIPCODE.replace(/[-]/g, "");
                        PageMethod("GetCustomerAddressDetailsUsingZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters
                    }
                    else {
                        $("#txtCONTACT_ADD1").val('');
                        $("#txtCONTACT_ADD2").val('');
                        $("#txtCONTACT_CITY").val('');
                    }
                });
            });
            function PageMethod(fn, paramArray, successFn, errorFn) {
                var pagePath = window.location.pathname;
                var paramList = '';
                if (paramArray.length > 0) {
                    for (var i = 0; i < paramArray.length; i += 2) {
                        if (paramList.length > 0) paramList += ',';
                        paramList += '"' + paramArray[i] + '":"' + paramArray[i + 1] + '"';
                    }
                }
                paramList = '{' + paramList + '}';
                //Call the page method  
                $.ajax({
                    type: "POST",
                    url: pagePath + "/" + fn,
                    contentType: "application/json; charset=utf-8",
                    data: paramList,
                    dataType: "json",
                    success: successFn,
                    error: errorFn});
            }
            function AjaxSucceeded(result) {
            
                var Addresses = result.d;
                var Addresse = Addresses.split('^');
                if (result.d != "" && Addresse[1] != 'undefined') {
                    $("#cmbCONTACT_STATE").val(Addresse[1]);
                    $("#hidTEST_STATE_ID").val(Addresse[1]);
                    $("#hidSTATE_ID").val(Addresse[1]);                    
                  //  $("#txtCONTACT_ZIP").val(Addresse[2]);
                    $("#txtCONTACT_ADD1").val(Addresse[3] + ' ' + Addresse[4]);
                    $("#txtDISTRICT").val(Addresse[5]);
                    $("#txtCONTACT_CITY").val(Addresse[6]);}
               // else {alert($("#hidZipeCodeVerificationMsg").val());
//                    $("#txtCONTACT_ZIP").val('');
//                    $("#txtCONTACT_ADD1").val('');
//                    $("#txtCONTACT_ADD2").val('');
//                    $("#txtCONTACT_CITY").val('');
                   else if (document.getElementById('cmbCONTACT_COUNTRY').options[document.getElementById('cmbCONTACT_COUNTRY').options.selectedIndex].value == '5') {
                        alert($("#hidZipeCodeVerificationMsg").val());
                    }

                //}
            }


            function zipcodeval() {

                if (document.getElementById('cmbCONTACT_COUNTRY').options[document.getElementById('cmbCONTACT_COUNTRY').options.selectedIndex].value == '6') {
                    document.getElementById('revCONTACT_ZIP').setAttribute('enabled', false); //Changed by aditya
                }
            }
            



            function AjaxFailed(result) {
                alert(result.d);
            }

// in  case when country is selected  'other',system have no format for zip. only numeric value will be accepted
            $(document).ready(function() {                
                $("#cmbCONTACT_COUNTRY").change(function() {
                    if ($("#cmbCONTACT_COUNTRY option:selected").val() == 6) {
                        document.getElementById('revCONTACT_ZIP').setAttribute('enabled', false); //Changed by aditya
                        document.getElementById('revCONTACT_ZIP').style.display = "none";  //Changed by aditya
                    }
                });
                if (trim($('#txtCONTACT_ZIP').val()) != '' && $("#cmbCONTACT_COUNTRY option:selected").val() == 6){
                    document.getElementById('revCONTACT_ZIP').setAttribute('enabled', false); //Changed by aditya
                    document.getElementById('revCONTACT_ZIP').style.display = "none";  //Changed by aditya
                    };
               

            });
        </script>
        <%-- End jQuery Implimentation for ZipeCode --%>
	</HEAD>
	<BODY class="bodyBackGround" leftMargin="0" topMargin="0" onload="ApplyColor();">
		<FORM id="ADDCONTACT" method="post" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server"></ext:ScriptManager>
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4">
						<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
					</td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0" DESIGNTIMEDRAGDROP="582">
							<tr>
								<TD class="pageHeader" colSpan="3"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="3"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
							</tr>
							<tr>
								<TD width="33%" class="midcolora"><asp:Label runat="server" id="capCONTACT_TYPE">Contact Type</asp:Label><span class="mandatory">*</span>
								<br />
								<asp:dropdownlist id="cmbCONTACT_TYPE_ID" runat="server" AutoPostBack="True" onfocus="SelectComboIndex('cmbCONTACT_TYPE_ID')"></asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCONTACT_TYPE_ID" runat="server" Display="Dynamic" ErrorMessage="Contact Type can't be blank."
										ControlToValidate="cmbCONTACT_TYPE_ID"></asp:requiredfieldvalidator>
								
								</TD>
                                <%--Modified for itrack - 1557 to implement Coolite combobox--%>
								<TD class="midcolora" width="34%" runat ="server" >
                                <asp:Label id="capINDIVIDUAL_CONTACT_ID" runat="server">Individual Contact Id</asp:Label><span id="spnIndContactID" class="mandatory" runat="server" >*</span>
								<br />
                                <%--<asp:dropdownlist id="cmbINDIVIDUAL_CONTACT_ID" onfocus="SelectComboIndex('cmbINDIVIDUAL_CONTACT_ID')"  runat="server"></asp:dropdownlist>--%>
                                <ext:Store ID="StoreINDIVIDUAL_CONTACT_ID" runat="server" >
                                   
                                  <Listeners>
                                        <Load Handler="#{CCcmbINDIVIDUAL_CONTACT_ID}.setValue(this.getAt(0).data[#{CCcmbINDIVIDUAL_CONTACT_ID}.valueField]);#{CCcmbINDIVIDUAL_CONTACT_ID}.fireEvent('select', #{StoreINDIVIDUAL_CONTACT_ID}.getAt(0), 0);" Single="false" /> <%--Changed by Aditya for TFS bug # 923--%>
                                  </Listeners>      
                               </ext:Store>
                               <ext:ComboBox ID="CCcmbINDIVIDUAL_CONTACT_ID" AllowBlank="false"  ForceSelection="true" CausesValidation="true" ItemCls="required" FieldClass="midcolora"  Resizable="true" Editable="false" runat="server" Width="420px" ></ext:ComboBox> <%--Changed by Aditya for TFS bug # 923--%>
								<asp:Label id="lblINDIVIDUAL_CONTACT_NAME" runat="server"  Visible="False"></asp:Label><BR>
								<asp:requiredfieldvalidator id="rfvIndContactId" runat="server" Display="Dynamic" ErrorMessage="Individual Contact Id can't be blank." ControlToValidate="CCcmbINDIVIDUAL_CONTACT_ID"></asp:requiredfieldvalidator> <%--Changed by Aditya for TFS bug # 923--%>
                                </TD> 
                                <%--Modified till here --%>
							    <TD class="midcolora" width="33%">
							    <asp:Label runat="server" id="capContact_Code">Contact Code</asp:Label><span class="mandatory">*</span>
							    <br />
							    <asp:textbox id="txtCONTACT_CODE" runat="server" maxlength="7" size="20"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revContactCode" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_CODE"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvCONTACT_CODE" runat="server" Display="Dynamic" ErrorMessage="Contact Code can not be balnk."
										ControlToValidate="txtCONTACT_CODE"></asp:requiredfieldvalidator>
							    </TD>
							</tr>
						
							<tr>
								<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capTITLE"></asp:Label><span class="mandatory">*</span><br />
								<asp:dropdownlist id="cmbCONTACT_SALUTATION" onfocus="SelectComboIndex('cmbCONTACT_SALUTATION')" runat="server"></asp:dropdownlist><a id="ancTITLE" runat="server" class="calcolora" href="javascript:showPageLookupLayer('cmbCONTACT_SALUTATION')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
									<BR>
									<asp:requiredfieldvalidator id="rfvCONTACT_SALUTATION" runat="server" Display="Dynamic" ErrorMessage="Salutation  can't be blank."
										ControlToValidate="cmbCONTACT_SALUTATION"></asp:requiredfieldvalidator>
								</TD>
								<TD class="midcolora"  colspan="2"><asp:Label runat="server" ID="capCONTACT_POSITION"></asp:Label><span id="spnCONTACT_POS" runat="server" class="mandatory">*</span>
								<br />
							    <asp:dropdownlist id="cmbCONTACT_POS" onfocus="SelectComboIndex('cmbCONTACT_POS')" runat="server">
								</asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvCONTACT_POS" runat="server" Display="Dynamic" ErrorMessage="Contact Position can't be blank."
											ControlToValidate="cmbCONTACT_POS"></asp:requiredfieldvalidator>
								</TD>
								
							</tr>							
							<tr>
								
										
							<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capCONTACT_FNAME"></asp:Label><span class="mandatory">*</span><br />
							<asp:textbox id="txtCONTACT_FNAME" runat="server" maxlength="75" size="35"></asp:textbox><br>
								<asp:requiredfieldvalidator id="rfvCONTACT_FNAME" runat="server" Display="Dynamic" ErrorMessage="First Name can't be blank."
									ControlToValidate="txtCONTACT_FNAME"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revFirstName" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
									ControlToValidate="txtCONTACT_FNAME"></asp:regularexpressionvalidator></TD>
							<TD class="midcolora" width="34%" >
								<asp:Label runat="server" ID="capCONTACT_MNAME"></asp:Label><br />
							<asp:textbox id="txtCONTACT_MNAME" runat="server" maxlength="15" size="20"></asp:textbox><BR>
								<asp:regularexpressionvalidator id="revCONTACT_MNAME" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
									ControlToValidate="txtCONTACT_MNAME"></asp:regularexpressionvalidator></TD>
							<TD class="midcolora" width="33%"><asp:Label runat="server" ID="capCONTACT_LNAME"></asp:Label>
							<br />
							<asp:textbox id="txtCONTACT_LNAME" runat="server" maxlength="75" size="35"></asp:textbox>
								<%--<asp:requiredfieldvalidator id="rfvLastName" runat="server" Display="Dynamic" ErrorMessage="Last Name can't be blank."
									ControlToValidate="txtCONTACT_LNAME">--%>
									<asp:regularexpressionvalidator id="revLastName" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
									ControlToValidate="txtCONTACT_LNAME"></asp:regularexpressionvalidator></TD>
									
								
							</tr>
							<tr style="DISPLAY: none">
								<TD class="midcolora">Address Same as Agent</TD>
								<TD class="midcolora"><asp:checkbox id="chkAddSameAsAgent" runat="server"></asp:checkbox></TD>
								<TD class="midcolora" colSpan="1"></TD>
							</tr>
							<tr id="trPullCustomerAddress" runat="server" visible="true">
								<td class="midcolora">
									<asp:label id="capPullCustomerAddress" runat="server" ></asp:label><%--Would you like to pull customer address--%>
								</td>
								<td colspan="2" class="midcolora">
									<cmsb:cmsbutton class="clsButton" id="btnPullCustomerAddress" runat="server" Text="Pull Customer Address"></cmsb:cmsbutton>
								</td>
							</tr>
							<tr>
								<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capADDRESS1">Address1</asp:Label><span class="mandatory">*</span>
								<br /><asp:textbox id="txtCONTACT_ADD1" runat="server" maxlength="70" size="35"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvCONTACT_ADD1" runat="server" Display="Dynamic" ErrorMessage="Address can't be blank."
										ControlToValidate="txtCONTACT_ADD1"></asp:requiredfieldvalidator></TD>
								<TD width="34%" class="midcolora"><asp:Label runat="server" ID="capADDRESS2">Address2</asp:Label>
								<br /><asp:textbox id="txtCONTACT_ADD2" runat="server" maxlength="70" size="35"></asp:textbox></TD>
								<td width="33%" class="midcolora"><asp:Label runat="server" ID="capCONTACT_CITY">City</asp:Label><span id="spnCONTACT_CITY" runat="server" class="mandatory">*</span>
								<br />
								<asp:textbox id="txtCONTACT_CITY" runat="server" maxlength="40" size="35"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvCONTACT_CITY" runat="server" Display="Dynamic" ErrorMessage="City can't be blank."
										ControlToValidate="txtCONTACT_CITY"></asp:requiredfieldvalidator>
								</td>
								
							</tr>
							<tr><td width="33%" class="midcolora">
                                <asp:Label ID="capNUMBER" runat="server" >Number</asp:Label><br/>
                                <asp:TextBox ID="txtNUMBER" runat="server"></asp:TextBox>
                                 
							
							</td>
							<td width="33%" class="midcolora">
                                <asp:Label ID="capDISTRICT" runat="server" >District</asp:Label><br />
                                <asp:TextBox ID="txtDISTRICT" runat="server"></asp:TextBox>
                                 
							
							</td>
							<td width="33%" class="midcolora"> </td>
							
							
							</tr>
							<tr>
								<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capCOUNTRY">Country</asp:Label><SPAN class="mandatory">*</SPAN><br />
								<asp:dropdownlist id="cmbCONTACT_COUNTRY" onfocus="SelectComboIndex('cmbCONTACT_COUNTRY')" runat="server">
										
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCountry" runat="server" Display="Dynamic" ErrorMessage="Country can't be blank."
										ControlToValidate="cmbCONTACT_COUNTRY"></asp:requiredfieldvalidator></TD>
							
								<TD width="34%" class="midcolora"><asp:Label runat="server" ID="capCONTACT_STATE">State
								</asp:Label><span id="spnCONTACT_STATE" runat="server" class="mandatory">*</span><br />
								<asp:dropdownlist id="cmbCONTACT_STATE"  runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvCONTACT_STATE" runat="server" Display="Dynamic" ErrorMessage="State can't be blank."
										ControlToValidate="cmbCONTACT_STATE"></asp:requiredfieldvalidator></TD>
								<TD width="18%" class="midcolora"><asp:Label runat="server" ID="capCONTACT_ZIP">Zip Code</asp:Label> <SPAN class="mandatory" id="spnCONTACT_ZIP" runat="server">*</SPAN>
								<br /><asp:textbox id="txtCONTACT_ZIP"  
                                        OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();zipcodeval();" 
                                        runat="server" maxlength="8" size="12" 
                                       ></asp:textbox>
								<%-- Added by Swarup on 30-mar-2007 --%>
									<asp:hyperlink id="hlkZipLookup" runat="server" Visible="true" CssClass="HotSpot">
									<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" Visible="true" ImageAlign="Bottom"></asp:image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvCONTACT_ZIP" runat="server" Display="Dynamic" ErrorMessage="Zipcode can't be blank."
										ControlToValidate="txtCONTACT_ZIP"></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revCONTACT_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_ZIP"></asp:regularexpressionvalidator>
										<asp:regularexpressionvalidator id="revCONTACT_ZIP_NUMERIC" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_ZIP"></asp:regularexpressionvalidator><br /> <%--Changed by Aditya for TFS bug # 923--%>
										</TD>
							</tr>
							<tr>
							
							<td class="midcolora" width="18%"><asp:Label ID="capCPF_CNPJ" runat="server"></asp:Label>
								<br /><asp:TextBox ID="txtCPF_CNPJ" runat="server" MaxLength = "14" OnBlur="this.value=FormatCPFCNPJ(this.value);ValidatorOnChange();"></asp:TextBox>
								<br/><asp:RegularExpressionValidator runat="server" ID="revCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_CNPJ"></asp:RegularExpressionValidator>
								     <asp:CustomValidator runat="server" ID="csvCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_CNPJ" ClientValidationFunction="validatCPF_CNPJ"></asp:CustomValidator>
								</td>
							
							
							 <td class="midcolora" width="34%" id="tdID_TYPE">
                                    <asp:Label ID="CapREGIONAL_ID_TYPE" runat="server">Id Type</asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtREGIONAL_ID_TYPE" runat="server" size="30" MaxLength="20" AutoCompleteType="Disabled"></asp:TextBox><br>
                                   <%-- <asp:RequiredFieldValidator ID="rfvID_TYPE" runat="server" ControlToValidate="txtID_TYPE"
                                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                </td>
							<td class="midcolora" width="33%"><asp:Label ID="capREGIONAL_ID" runat="server"></asp:Label>
								<br /><asp:TextBox ID="txtREGIONAL_IDENTIFICATION" runat="server"></asp:TextBox>
								</td>
							
							
							</tr>
							
							<tr>
							
							 <td class="midcolora" width="33%">
                                 <asp:Label runat="server" ID="capREGIONAL_ID_ISSUE_DATE">Regional ID Issue Date</asp:Label>
                                 <br /><asp:TextBox runat="server" ID="txtREG_ID_ISSUE_DATE"  size="12" AutoCompleteType="Disabled" CausesValidation="true"></asp:TextBox><asp:HyperLink ID="hlkREG_ID_ISSUE_DATE" runat="server" CssClass="HotSpot">
                                 <asp:Image ID="imgREG_ID_ISSUE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" ></asp:Image></asp:HyperLink><br/>
                                <asp:RegularExpressionValidator runat="server" ID="revREGIONAL_ID_ISSUE_DATE" Display="Dynamic" ControlToValidate="txtREG_ID_ISSUE_DATE"></asp:RegularExpressionValidator>
                                <asp:comparevalidator id="cpvREGIONAL_ID_ISSUE_DATE2" ControlToValidate="txtREG_ID_ISSUE_DATE" Display="Dynamic" Runat="server" Type="Date"
					             Operator="LessThanEqual" 
					             ValueToCompare="<%#DateTime.Today.ToShortDateString()%>"></asp:comparevalidator><br>	
					            <asp:comparevalidator id="cpvREGIONAL_ID_ISSUE_DATE"  
					             ControlToValidate="txtREG_ID_ISSUE_DATE" Display="Dynamic" Runat="server" ControlToCompare="txtDATE_OF_BIRTH" Type="Date"
			                     Operator="GreaterThan"></asp:comparevalidator>
                                </td>
								<td class="midcolora" width="33%"><asp:Label ID="capREGIONAL_ID_ISSUE" runat="server"></asp:Label>
								<br /><asp:TextBox ID="txtREG_ID_ISSUE" runat="server"></asp:TextBox></td>
							<td class="midcolora" width="33%">
							</td>
							
							
							</tr>
							
							<tr>
								<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capBusiness_Phone">Business Phone</asp:Label> <%--<span class="mandatory">*</span>--%><br />
								<asp:textbox id="txtCONTACT_BUSINESS_PHONE"  runat="server" maxlength="15" size="20" ></asp:textbox>
										<br />
										<asp:regularexpressionvalidator id="revCONTACT_BUSINESS_PHONE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_BUSINESS_PHONE"></asp:regularexpressionvalidator><%--<asp:requiredfieldvalidator id="rfvbusPhone" runat="server" Display="Dynamic" ErrorMessage="Business Phone can't be blank."
										ControlToValidate="txtCONTACT_BUSINESS_PHONE"></asp:requiredfieldvalidator>--%>
										</td>
										<td  width="34%" class="midcolora"><asp:Label runat="server"  ID="capCONTACT_EXT"></asp:Label><br />
										
									<asp:textbox id="txtCONTACT_EXT"  runat="server" size="8"
										maxlength="6" ReadOnly="False" onblur="CheckIfPhoneEmpty();"></asp:textbox><BR>
									
									<asp:regularexpressionvalidator id="revExt" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_EXT"></asp:regularexpressionvalidator></td>
								<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capMOBILE">Mobile</asp:Label> <%--<SPAN class="mandatory">*</SPAN>--%>
								<br /><asp:textbox id="txtCONTACT_MOBILE" runat="server" maxlength="15" size="20" onblur="FormatBrazilPhone()"></asp:textbox>
									<br>
									<%--<asp:requiredfieldvalidator id="rfvMobile" runat="server" Display="Dynamic" ErrorMessage="Mobile can't be blank."
										ControlToValidate="txtCONTACT_MOBILE"></asp:requiredfieldvalidator>--%><asp:regularexpressionvalidator id="revCONTACT_MOBILE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_MOBILE"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capFAX">Fax </asp:Label> 
								<br />
									<asp:textbox id="txtCONTACT_FAX" runat="server" maxlength="15" size="20" onblur="FormatBrazilPhone()"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCONTACT_FAX" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_FAX"></asp:regularexpressionvalidator>
								</TD>
								<TD width="34%" class="midcolora"><asp:Label runat="server" ID="capCONTACT_PAGER">Pager </asp:Label>
								<br /><asp:textbox id="txtCONTACT_PAGER" runat="server" maxlength="15" size="20" onblur="FormatBrazilPhone()"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCONTACT_PAGER" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_PAGER"></asp:regularexpressionvalidator>
								</TD>
								<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capHome_Phone" >Home Phone</asp:Label> <%--<span class="mandatory">*</span>--%>
								<br />
								<asp:textbox id="txtCONTACT_HOME_PHONE" runat="server" maxlength="15" size="20" onblur="FormatBrazilPhone()"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revCONTACT_HOME_PHONE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_HOME_PHONE"></asp:regularexpressionvalidator><%--<asp:requiredfieldvalidator id="rfvHomePhone" runat="server" Display="Dynamic" ErrorMessage="Home Phone can't be blank."
										ControlToValidate="txtCONTACT_HOME_PHONE"></asp:requiredfieldvalidator>--%>
								</TD>
							</tr>
							<tr>
								<%--<td class="midcolora" width="18%"><asp:Label ID="capCPF_CNPJ" runat="server"></asp:Label>
								<br /><asp:TextBox ID="txtCPF_CNPJ" runat="server"></asp:TextBox>
								<br/><asp:RegularExpressionValidator runat="server" ID="revCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF_CNPJ"></asp:RegularExpressionValidator>
								</td>--%>
								<td class="midcolora" width="33%">
                                <asp:Label ID="capDATE_OF_BIRTH" runat="server" ></asp:Label>  </br>
                               
                                <asp:TextBox ID="txtDATE_OF_BIRTH" runat="server" size="12"  MaxLength="10"></asp:TextBox>
                                <asp:HyperLink ID="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot">
                                <asp:Image runat="server" ID="imgDATE_OF_BIRTH" Border="0" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" ></asp:Image></asp:HyperLink><br/>
                                 <asp:RegularExpressionValidator  ID="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:RegularExpressionValidator>
                                </td>
                                <td class="midcolora" width="18%"><asp:Label ID="capACTIVITY" runat="server"></asp:Label>
								<br />
								<asp:DropDownList ID="cmbACTIVITY" runat="server" Width="200px"></asp:DropDownList>
								</td>
								<td class="midcolora" width="33%">
							</td>
								</tr>
								<%--<tr>--%>
								
								<%--<td class="midcolora" width="33%"><asp:Label ID="capREGIONAL_ID" runat="server"></asp:Label>
								<br /><asp:TextBox ID="txtREGIONAL_IDENTIFICATION" runat="server"></asp:TextBox>
								</td>--%>
								<%-- <td class="midcolora" width="33%">
                                 <asp:Label runat="server" ID="capREGIONAL_ID_ISSUE_DATE">Regional ID Issue Date</asp:Label>
                                 <br /><asp:TextBox runat="server" ID="txtREG_ID_ISSUE_DATE"  size="12" AutoCompleteType="Disabled" CausesValidation="true"></asp:TextBox><asp:HyperLink ID="hlkREG_ID_ISSUE_DATE" runat="server" CssClass="HotSpot">
                                 <asp:Image ID="imgREG_ID_ISSUE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" ></asp:Image></asp:HyperLink><br/>
                                <asp:RegularExpressionValidator runat="server" ID="revREGIONAL_ID_ISSUE_DATE" Display="Dynamic" ControlToValidate="txtREG_ID_ISSUE_DATE"></asp:RegularExpressionValidator>
                                </td>
								<td class="midcolora" width="33%"><asp:Label ID="capREGIONAL_ID_ISSUE" runat="server"></asp:Label>
								<br /><asp:TextBox ID="txtREG_ID_ISSUE" runat="server"></asp:TextBox></td>--%>
								<%--</tr>--%>
								
							    <tr>
								<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capTOLL_FREE_NO" >Toll Free No. </asp:Label>
								<br /><asp:textbox id="txtCONTACT_TOLL_FREE" runat="server" maxlength="11" size="20"></asp:textbox>
									<br>
									<asp:regularexpressionvalidator id="revCONTACT_TOLL_FREE" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_TOLL_FREE"></asp:regularexpressionvalidator></TD>
										
								<TD width="34%" class="midcolora"><asp:Label runat="server" ID="capEMAIL" >Email</asp:Label><%--<span class="mandatory">*</span>--%><br />
								<asp:textbox id="txtCONTACT_EMAIL" runat="server" maxlength="50" size="35"></asp:textbox><BR>
									<%--<asp:requiredfieldvalidator id="rfvEmail" runat="server" Display="Dynamic" ErrorMessage="Email can't be blank."
										ControlToValidate="txtCONTACT_EMAIL"></asp:requiredfieldvalidator>--%><asp:regularexpressionvalidator id="revEmail" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
										ControlToValidate="txtCONTACT_EMAIL"></asp:regularexpressionvalidator></TD>
								<TD width="33%" class="midcolora"><asp:Label runat="server" ID="capNOTE" >Note</asp:Label>
								<br /><asp:textbox id="txtCONTACT_NOTE" runat="server" OnKeyPress="MaxLength(this,500);" maxlength="500"
										TextMode="MultiLine" Width="250" Rows="3"></asp:textbox></TD>
								
							</tr>
							
							<tr>
							 <td class="midcolora" width="33%" colspan="3">
                            <asp:Label id="capNATIONALITY" runat="server">Nationality</asp:Label><br />
                            <asp:TextBox ID="txtNATIONALITY" runat="server"></asp:TextBox><br /></td>
							</tr>
							<tr>
								<td class="midcolora" colSpan="2">
									<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back To Customer Assistant"
										Visible="False"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" tabIndex="2" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
								<TD class="midcolora" colSpan="1">
									<P align="right">
										<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave"  runat="server" Text="Save"></cmsb:cmsbutton></P>
								</TD>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidCONTACT_ID" type="hidden" name="hidCONTACT_ID" runat="server"> <INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server">&nbsp;
			<INPUT id="hidINDIVIDUAL_CONTACT_ID" type="hidden" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidSTATE_ID_OLD" type="hidden" name="hidSTATE_ID_OLD" runat="server">
			<input type="hidden" id="hidSTATE_ID" name="hidSTATE_ID" runat="server" />
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="DISPLAY: none;Z-INDEX: 1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" style="Z-INDEX: 101;VISIBILITY: hidden;POSITION: absolute" onmouseover="javascript:refreshLookupLayer();">
			<table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b><asp:Label runat="server" Text="Add LookUp" ID="Caplook"></asp:Label></b></td>
					<td><p align="right"><a href="javascript:void(0)" onclick="javascript:hideLookupLayer();"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></a></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colspan="2">
						<span id="LookUpMsg"></span>
					</td>
					<td>
					<input type="hidden" id="hidCALLEDFROM" name="hidCALLEDFROM" runat="server" />
					<input id="hidZipeCodeVerificationMsg" type="hidden" name="hidZipeCodeVerificationMsg"  runat="server"> 
					<input id="hidCUSTOMER_TYPE" type="hidden" name="hidCUSTOMER_TYPE"  runat="server"> 
					 
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
		   
			RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidCONTACT_ID').value);
		//RemoveTab(2,top.frames[1]);
				//RefreshWebGrid("1","1");//Commented by Pradeep Kushwaha on 27-07-2010
				//document.getElementById('hidCONTACT_ID').value = "NEW";
		</script>
	</BODY>
</HTML>


