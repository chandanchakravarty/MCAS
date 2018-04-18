<%@ Page Language="c#" CodeBehind="AddReinsurer.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.CmsWeb.Maintenance.AddReinsurer" ValidateRequest="false" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<html>
<head>
    <title>MNT_REIN_COMPANY_LIST</title>
    <meta content="False" name="vs_showGrid">
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">

    <script src="/cms/cmsweb/scripts/xmldom.js"></script>

    <script src="/cms/cmsweb/scripts/common.js"></script>

    <script src="/cms/cmsweb/scripts/form.js"></script>

    <script src="/cms/cmsweb/scripts/Calendar.js"></script>

    <script language="javascript">

        var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
        var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";

        function AddData() {
            ChangeColor();
            DisableValidators();
            //return;
            document.getElementById('hidREIN_COMAPANY_ID').value = 'New';
            document.getElementById('txtREIN_COMAPANY_CODE').value = '';
            document.getElementById('txtREIN_COMAPANY_NAME').focus();
            document.getElementById('cmbREIN_COMAPANY_TYPE').options.selectedIndex = 0;
            document.getElementById('txtREIN_COMAPANY_ADD1').value = '';
            document.getElementById('txtREIN_COMAPANY_ADD2').value = '';
            document.getElementById('txtREIN_COMAPANY_CITY').value = '';
            //document.getElementById('cmbREIN_COMAPANY_COUNTRY').options.selectedIndex = -1;
            //document.getElementById('cmbREIN_COMAPANY_STATE').options.selectedIndex = -1;
            document.getElementById('txtREIN_COMAPANY_STATE').value = '';
            document.getElementById('txtREIN_COMAPANY_ZIP').value = '';
            document.getElementById('txtREIN_COMAPANY_PHONE').value = '';
            document.getElementById('txtREIN_COMAPANY_EXT').value = '';
            document.getElementById('txtREIN_COMAPANY_FAX').value = '';
            document.getElementById('txtREIN_COMAPANY_MOBILE').value = '';
            document.getElementById('txtREIN_COMAPANY_EMAIL').value = '';

            document.getElementById('txtREIN_COMAPANY_NOTE').value = '';
            document.getElementById('txtREIN_COMAPANY_ACC_NUMBER').value = '';

            document.getElementById('txtM_REIN_COMPANY_ADD_1').value = '';
            document.getElementById('txtM_RREIN_COMPANY_ADD_2').value = '';
            document.getElementById('txtM_REIN_COMPANY_CITY').value = '';
            //document.getElementById('cmbM_REIN_COMPANY_COUNTRY').options.selectedIndex = 0;
            //document.getElementById('cmbM_REIN_COMPANY_STATE').options.selectedIndex = 0;
            document.getElementById('txtM_REIN_COMPANY_STATE').value = '';
            document.getElementById('txtM_REIN_COMPANY_ZIP').value = '';
            document.getElementById('txtM_REIN_COMPANY_PHONE').value = '';
            document.getElementById('txtM_REIN_COMPANY_FAX').value = '';
            document.getElementById('txtM_REIN_COMPANY_EXT').value = '';

            document.getElementById('txtREIN_COMPANY_WEBSITE').value = '';
            document.getElementById('cmbREIN_COMPANY_IS_BROKER').options.selectedIndex = 0;
            //document.getElementById('cmbNAIC_CODE').options.selectedIndex = 0;
            document.getElementById('txtNAIC_CODE').value = '';
            document.getElementById('txtPRINCIPAL_CONTACT').value = '';
            document.getElementById('txtOTHER_CONTACT').value = '';
            document.getElementById('txtFEDERAL_ID').value = '';
            document.getElementById('txtDOMICILED_STATE').value = '';
            document.getElementById('txtAM_BEST_RATING').value = '';
            //document.getElementById('txtTERMINATION_DATE').value  = '';
            //document.getElementById('txtTERMINATION_DATE').value  = '';
            //document.getElementById('txtTERMINATION_DATE').value  = '';
            document.getElementById('hidOldData').value = '';
            document.getElementById('txtEFFECTIVE_DATE').value = '';
            document.getElementById('txtTERMINATION_REASON').value = '';
            document.getElementById('txtREIN_COMPANY_SPEED_DIAL').value = '';

            if (document.getElementById('btnActivateDeactivate'))
                document.getElementById('btnActivateDeactivate').setAttribute('disabled', true);
            if (document.getElementById('btnDelete'))
                document.getElementById('btnDelete').style.display = "none";

            FEDERAL_ID_change();

            if ('<%=GetSystemId().ToUpper()%>'=='S001' ||'<%=GetSystemId().ToUpper()%>'=='SUAT')
            {
        
         document.getElementById('cmbREIN_COMAPANY_COUNTRY_SIN') .value='Singapore'
         document.getElementById('cmbM_REIN_COMPANY_COUNTRY_SIN').value='Singapore';  
         document.getElementById('txtREIN_COMAPANY_COUNTRY') .value='Singapore'
         document.getElementById('txtM_REIN_COMPANY_COUNTRY').value='Singapore';  
          }
        }


        function populateXML() {
            //ResetAfterActivateDeactivate();
            var tempXML;
            //tempXML=document.getElementById("hidOldData").value;
            //alert(document.getElementById("hidOldData").value);
            if (document.getElementById('hidFormSaved').value == '0') {
                if (top.frames[1].strXML != "") {
                    tempXML = top.frames[1].strXML;

                    //Storing the XML in hidRowId hidden fields 
                   // document.getElementById('hidOldData').value = tempXML;
                    //ChkCountry_physical();ChkCountry_mailing();						
                    //populateFormData(tempXML,"MNT_REIN_COMAPANY_LIST");						
                    FEDERAL_ID_hide();
                }
                else {
                    AddData();
                }
            }
            else {
                if (document.getElementById('hidFEDERAL_ID').value != '')
                    FEDERAL_ID_hide();
                else
                    FEDERAL_ID_change();
            }

            SetTab();
            //window.MNT_REIN_COMPANY_LIST.scrollIntoView(true);
            return false;
        }

        function SetTab() {     
     
           if (document.getElementById('hidOldData').value != "") {

               var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
               var tabtitles = TAB_TITLES.split(',');
            
                Url = "Reinsurance/ReinsuranceContactIndex.aspx?calledfrom=reinsurer&EntityType=Reinsurer&EntityId=" + document.getElementById('hidREIN_COMAPANY_ID').value + "&";
                DrawTab(2, top.frames[1], tabtitles[0], Url);

                Url = "AttachmentIndex.aspx?calledfrom=reinsurer&EntityType=Reinsurer&EntityId=" + document.getElementById('hidREIN_COMAPANY_ID').value + "&";
                DrawTab(3, top.frames[1], tabtitles[1], Url);
             
              //Added by amit
                if(<%=GetLanguageID()%>=="3")
               { 
                Url = "AddRating.aspx?calledfrom=reinsurer&EntityType=Reinsurer&EntityId=" + document.getElementById('hidREIN_COMAPANY_ID').value + "&";
                DrawTab(4, top.frames[1],tabtitles[2] , Url);
               }
                
            }
            else {
                
                RemoveTab(4, top.frames[1]);
                RemoveTab(3, top.frames[1]);
                RemoveTab(2, top.frames[1]); 
               
               
            }
           }

        //	function ResetAfterActivateDeactivate()
        //	{
        //		if (document.getElementById('hidReset').value == "1")
        //		{				
        //			document.MNT_REIN_COMAPANY_LIST.reset();			
        //		}
        //	}

        function Reset() {
            DisableValidators();
            document.MNT_REIN_COMPANY_LIST.reset();
            //window.MNT_REIN_COMPANY_LIST.scrollIntoView(1);
            ChangeColor();
            return false;
        }

        function CopyPhysicalAddress() {

            //if(document.getElementById('txtM_REIN_COMPANY_ADD_1').value=="")
            document.getElementById('txtM_REIN_COMPANY_ADD_1').value = document.getElementById('txtREIN_COMAPANY_ADD1').value;
            //if(document.getElementById('txtM_RREIN_COMPANY_ADD_2').value=="")
            document.getElementById('txtM_RREIN_COMPANY_ADD_2').value = document.getElementById('txtREIN_COMAPANY_ADD2').value;
            //if(document.getElementById('txtM_REIN_COMPANY_CITY').value=="")
            document.getElementById('txtM_REIN_COMPANY_CITY').value = document.getElementById('txtREIN_COMAPANY_CITY').value;
            //if(document.getElementById('txtM_REIN_COMPANY_COUNTRY').value=="")
           document.getElementById('txtM_REIN_COMPANY_COUNTRY').value = document.getElementById('txtREIN_COMAPANY_COUNTRY').value;

           // if(document.getElementById('cmbM_REIN_COMPANY_COUNTRY').value=="")
            document.getElementById('cmbM_REIN_COMPANY_COUNTRY_SIN').value =document.getElementById('cmbREIN_COMAPANY_COUNTRY_SIN').value ;
            //if(document.getElementById('txtM_REIN_COMPANY_STATE').value=="")
            document.getElementById('txtM_REIN_COMPANY_STATE').value = document.getElementById('txtREIN_COMAPANY_STATE').value;
            //if(document.getElementById('txtM_REIN_COMPANY_ZIP').value=="")
            document.getElementById('txtM_REIN_COMPANY_ZIP').value = document.getElementById('txtREIN_COMAPANY_ZIP').value;
            //if(document.getElementById('txtM_REIN_COMPANY_PHONE').value=="")
            document.getElementById('txtM_REIN_COMPANY_PHONE').value = document.getElementById('txtREIN_COMAPANY_PHONE').value;
            EnableValidator('rfvM_REIN_COMPANY_PHONE', false);
            //if(document.getElementById('txtM_REIN_COMPANY_EXT').value=="")
            document.getElementById('txtM_REIN_COMPANY_EXT').value = document.getElementById('txtREIN_COMAPANY_EXT').value;
            //if(document.getElementById('txtM_REIN_COMPANY_FAX').value=="")
            document.getElementById('txtM_REIN_COMPANY_FAX').value = document.getElementById('txtREIN_COMAPANY_FAX').value;
            EnableValidator('rfvM_REIN_COMPANY_ADD_1', false);
            ChangeColor();
            return false;
        }


        function ValidateControl(ControlToValidate,ValidatorID)
		{ 
			if(ValidatorID==null) return;
			var str=document.getElementById(ControlToValidate).value;
			if(str!="" &&  str!=null && str!=undefined) //Enable the validator
			{
				document.getElementById(ValidatorID).setAttribute('enabled',false);
				document.getElementById(ValidatorID).style.display = "none";
				document.getElementById(ValidatorID).setAttribute('isValid',false);
			}
			else
			{				
                document.getElementById(ValidatorID).setAttribute('enabled',true);
                document.getElementById(ValidatorID).style.display = "inline";
				document.getElementById(ValidatorID).setAttribute('isValid',true);
			}
		}
		
        function ChkDate(objSource, objArgs) {
            var effdate = document.MNT_REIN_COMAPANY_LIST.txtORIGINAL_CONTRACT_DATE.value;
            objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>", effdate, jsaAppDtFormat);
        }


        function GenerateCustomerCode(Ctrl) {
            if (Ctrl == "txtREIN_COMAPANY_NAME") {
                if (document.getElementById("txtREIN_COMAPANY_NAME").value != "") {
                    if (document.getElementById("txtREIN_COMAPANY_CODE").value == "") {
                        document.getElementById('txtREIN_COMAPANY_CODE').value = (GenerateRandomCode(document.getElementById('txtREIN_COMAPANY_NAME').value, ''));
                        /*document.getElementById("txtCUSTOMER_CODE").value = document.getElementById("txtCUSTOMER_FIRST_NAME").value.substring(0,4) +
                        "000001";*/
                        EnableValidator('rfvREIN_COMAPANY_CODE', false);
                    }
                }
            }
        }

        function FEDERAL_ID_change() {
            document.getElementById('txtFEDERAL_ID').value = '';
            document.getElementById('txtFEDERAL_ID').style.display = 'inline';
//            document.getElementById("rfvFEDERAL_ID").setAttribute('enabled', true);
            document.getElementById("revFEDERAL_ID").setAttribute('enabled', true);
            if (document.getElementById("btnFEDERAL_ID").value == 'Edit')
                document.getElementById("btnFEDERAL_ID").value = 'Cancel';
            else if (document.getElementById("btnFEDERAL_ID").value == 'Cancel')
                FEDERAL_ID_hide();
            else
                document.getElementById("btnFEDERAL_ID").style.display = 'none';

        }

        function FEDERAL_ID_hide() {
            document.getElementById("btnFEDERAL_ID").style.display = 'inline';
            document.getElementById('txtFEDERAL_ID').value = '';
            document.getElementById('txtFEDERAL_ID').style.display = 'none';
//            document.getElementById("rfvFEDERAL_ID").style.display = 'none';
//            document.getElementById("rfvFEDERAL_ID").setAttribute('enabled', false);
            document.getElementById("revFEDERAL_ID").style.display = 'none';
            document.getElementById("revFEDERAL_ID").setAttribute('enabled', false);
            document.getElementById("btnFEDERAL_ID").value = 'Edit';
        }

        function FormatBankBranch(vr) {

            var vr = new String(vr.toString());
            if (vr != "") {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (num == 7) {
                    vr = vr.substr(0, 6) + '-' + vr.substr(6, 1);
                }

            }
            return vr;
        }


        function validatCNPJ(objSource, objArgs) {

            
           
                
           var cnpjerrormsg = '<%=javasciptCNPJmsg %>';
           var CNPJ_invalid = '<%=CNPJ_invalid %>';
            

            var valid = false;
            var idd = objSource.id;
            var rfvid = idd.replace('csv', 'rev');
            if (document.getElementById(rfvid) != null)
                if (document.getElementById(rfvid).isvalid == true) {
                var theCPF = document.getElementById(objSource.controltovalidate)
                var len = theCPF.value.length;              
                if (len == '18') {
                    valid = validar(objSource, objArgs);
                    if (valid) { objSource.innerText = ''; } else { objSource.innerText = CNPJ_invalid; }
                }
                else {

                    if (document.getElementById(rfvid) != null) {
                        if (document.getElementById(rfvid).isvalid == true) {
                            objArgs.IsValid = false;
                            objSource.innerHTML = cnpjerrormsg; //'Please enter 18 digit CPF No';
                        } else { objSource.innerHTML = ''; }
                    }
                }
                //for CNPJ # in if customer type is commercial
                //it check CNPJ format & valdate bia validar() function CNPJ is valid or not
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
     


     function SetValue()
     {
    
     document.getElementById('txtREIN_COMAPANY_COUNTRY').value=document.getElementById('cmbREIN_COMAPANY_COUNTRY_SIN').value;

       
     }


     function SetcmbMValue()
     {
      
     document.getElementById('txtM_REIN_COMPANY_COUNTRY').value=document.getElementById('cmbM_REIN_COMPANY_COUNTRY_SIN').value;
     
     }

     


        /*Commented by Swarup as Ashish send mail 
        "Country drop down on Reinsurance Broker Add/Edit screen should allow all 
        countries. Remove the drop down and let users type the name of the country."*/

        /*function ChkCountry_physical()
        {
        if(document.getElementById("cmbREIN_COMAPANY_COUNTRY").options[document.getElementById("cmbREIN_COMAPANY_COUNTRY").selectedIndex].value != "1")
        {
        EnableValidator('revREIN_COMAPANY_ZIP',false);
        EnableValidator('rfvREIN_COMAPANY_STATE',false);
        EnableValidator('rfvREIN_COMAPANY_ZIP',false);
        document.getElementById("spnREIN_COMAPANY_STATE").style.display = "none";
        document.getElementById("spnREIN_COMAPANY_ZIP").style.display = "none";
        document.getElementById("cmbREIN_COMAPANY_STATE").style.backgroundColor="white";
        document.getElementById("txtREIN_COMAPANY_ZIP").style.backgroundColor="white";

					
        }
        else
        {
        EnableValidator('revREIN_COMAPANY_ZIP',true);
        EnableValidator('rfvREIN_COMAPANY_STATE',true);
        EnableValidator('rfvREIN_COMAPANY_ZIP',true);
        document.getElementById("spnREIN_COMAPANY_STATE").style.display = "inline";
        document.getElementById("spnREIN_COMAPANY_ZIP").style.display = "inline";
        document.getElementById("cmbREIN_COMAPANY_STATE").style.backgroundColor="#FFFFD1";
        document.getElementById("txtREIN_COMAPANY_ZIP").style.backgroundColor="#FFFFD1";

			}
			if(document.getElementById("cmbREIN_COMAPANY_COUNTRY").options[document.getElementById("cmbREIN_COMAPANY_COUNTRY").selectedIndex].value != "1"
        && document.getElementById("cmbREIN_COMAPANY_COUNTRY").options[document.getElementById("cmbREIN_COMAPANY_COUNTRY").selectedIndex].value != "2")
        {
        EnableValidator('revREIN_COMAPANY_PHONE',false);
        EnableValidator('revREIN_COMAPANY_EXT',false);
        EnableValidator('revREIN_COMAPANY_FAX',false);
        EnableValidator('revREIN_COMAPANY_PHONE_CHECK',true);
        EnableValidator('revREIN_COMAPANY_EXT_CHECK',true);
        EnableValidator('revREIN_COMAPANY_FAX_CHECK',true);
        }
        else 
        {
        EnableValidator('revREIN_COMAPANY_PHONE',true);
        EnableValidator('revREIN_COMAPANY_EXT',true);
        EnableValidator('revREIN_COMAPANY_FAX',true);
        EnableValidator('revREIN_COMAPANY_PHONE_CHECK',false);
        EnableValidator('revREIN_COMAPANY_EXT_CHECK',false);
        EnableValidator('revREIN_COMAPANY_FAX_CHECK',false);
        }
			
			if(document.getElementById("cmbREIN_COMAPANY_COUNTRY").options[document.getElementById("cmbREIN_COMAPANY_COUNTRY").selectedIndex].value != "1" 
        || document.getElementById("cmbM_REIN_COMPANY_COUNTRY").options[document.getElementById("cmbM_REIN_COMPANY_COUNTRY").selectedIndex].value !="1")
        {
        EnableValidator('revREIN_COMAPANY_MOBILE',false);
        EnableValidator('revREIN_COMAPANY_MOBILE_CHECK',true);
        }
        else
        {
        EnableValidator('revREIN_COMAPANY_MOBILE',true);
        EnableValidator('revREIN_COMAPANY_MOBILE_CHECK',false);
        }
        }
        function ChkCountry_mailing()
        {
        if(document.getElementById("cmbM_REIN_COMPANY_COUNTRY").options[document.getElementById("cmbM_REIN_COMPANY_COUNTRY").selectedIndex].value !="1")
        {
        EnableValidator('revM_REIN_COMPANY_ZIP',false);
        EnableValidator('rfvM_REIN_COMPANY_STATE',false);
        EnableValidator('rfvM_REIN_COMPANY_ZIP',false);
        document.getElementById("spnM_REIN_COMPANY_STATE").style.display = "none";
        document.getElementById("spnM_REIN_COMPANY_ZIP").style.display = "none";
        document.getElementById("cmbM_REIN_COMPANY_STATE").style.backgroundColor="white";
        document.getElementById("txtM_REIN_COMPANY_ZIP").style.backgroundColor="white";
        }
        else
        {
        EnableValidator('revM_REIN_COMPANY_ZIP',true);
        EnableValidator('rfvM_REIN_COMPANY_STATE',true);
        EnableValidator('rfvM_REIN_COMPANY_ZIP',true);
        document.getElementById("spnM_REIN_COMPANY_STATE").style.display = "inline";
        document.getElementById("spnM_REIN_COMPANY_ZIP").style.display = "inline";
        document.getElementById("cmbM_REIN_COMPANY_STATE").style.backgroundColor="#FFFFD1";
        document.getElementById("txtM_REIN_COMPANY_ZIP").style.backgroundColor="#FFFFD1";

			}
			if(document.getElementById("cmbM_REIN_COMPANY_COUNTRY").options[document.getElementById("cmbM_REIN_COMPANY_COUNTRY").selectedIndex].value !="1"
        && document.getElementById("cmbM_REIN_COMPANY_COUNTRY").options[document.getElementById("cmbM_REIN_COMPANY_COUNTRY").selectedIndex].value !="2")
        {
        EnableValidator('revM_REIN_COMPANY_PHONE',false);
        EnableValidator('revM_REIN_COMPANY_EXT',false);
        EnableValidator('revM_REIN_COMPANY_FAX',false);
        EnableValidator('revM_REIN_COMPANY_PHONE_CHECK',true);
        EnableValidator('revM_REIN_COMPANY_EXT_CHECK',true);
        EnableValidator('revM_REIN_COMPANY_FAX_CHECK',true);
        }
        else 
        {
        EnableValidator('revM_REIN_COMPANY_PHONE',true);
        EnableValidator('revM_REIN_COMPANY_EXT',true);
        EnableValidator('revM_REIN_COMPANY_FAX',true);
        EnableValidator('revM_REIN_COMPANY_PHONE_CHECK',false);
        EnableValidator('revM_REIN_COMPANY_EXT_CHECK',false);
        EnableValidator('revM_REIN_COMPANY_FAX_CHECK',false);

			}
			
			
			if(document.getElementById("cmbREIN_COMAPANY_COUNTRY").options[document.getElementById("cmbREIN_COMAPANY_COUNTRY").selectedIndex].value != "1"
        ||document.getElementById("cmbM_REIN_COMPANY_COUNTRY").options[document.getElementById("cmbM_REIN_COMPANY_COUNTRY").selectedIndex].value !="1")
        {
        EnableValidator('revREIN_COMAPANY_MOBILE',false);
        EnableValidator('revREIN_COMAPANY_MOBILE_CHECK',true);
        }
        else
        {
        EnableValidator('revREIN_COMAPANY_MOBILE',true);
        EnableValidator('revREIN_COMAPANY_MOBILE_CHECK',false);
        }
        ChangeColor();
        }*/
		
    </script>

</head>
<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="populateXML();ApplyColor();ChangeColor();">
    <form id="MNT_REIN_COMPANY_LIST" method="post" runat="server">
    <p>
        <uc1:addressverification id="AddressVerification1" runat="server">
        </uc1:addressverification></p>
    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr id="trBody" runat="server">
            <td>
                <table width="100%" align="center" border="0">
                    <tbody>
                        <tr>
                            <td class="pageHeader" colspan="4">
                                <asp:Label ID="capMessages" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="right" colspan="4">
                                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <!--START April 09 2007 Harmanjeet-->
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_NAME" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_NAME" runat="server" size="30" MaxLength="75"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_NAME" runat="server" ControlToValidate="txtREIN_COMAPANY_NAME"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_CODE" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_CODE" runat="server" size="12" MaxLength="6"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_CODE" runat="server" ControlToValidate="txtREIN_COMAPANY_CODE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <!--END Harmanjeet-->
                        </tr>
                        <!-- START April 10 2007 Harmanjeet-->
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_TYPE" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:DropDownList ID="cmbREIN_COMAPANY_TYPE" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_TYPE" runat="server" ControlToValidate="cmbREIN_COMAPANY_TYPE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" colspan="2">
                            </td>
                        </tr>
                        <!--END Harmanjeet-->
                        <tr>
                            <td class="headerEffectSystemParams" colspan="4">
                               <asp:Label ID="capphysical" runat="server"></asp:Label>
                            </td> <%--Physical Address--%>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_ADD1" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_ADD1" runat="server" MaxLength="70"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_ADD1" runat="server" ControlToValidate="txtREIN_COMAPANY_ADD1"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_ADD2" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_ADD2" runat="server" MaxLength="70"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="ttrREIN_COMAPANY_COUNTRY" runat="server" >
                            <td class="midcolora" width="18%" >
                                <asp:Label ID="capREIN_COMAPANY_CITY" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_CITY" runat="server" MaxLength="40"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revREIN_COMAPANY_CITY" ControlToValidate="txtREIN_COMAPANY_CITY"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%" >
                                <asp:Label ID="capREIN_COMAPANY_COUNTRY" runat="server"></asp:Label><asp:Label ID="capREIN_COMAPANY_COUNTRY_SIN" runat="server" ></asp:Label><span id="spnREIN_COMAPANY_COUNTRY" runat="server" class="mandatory">*</span>
                            </td>
                            <%--<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbREIN_COMAPANY_COUNTRY" onfocus="SelectComboIndex('cmbREIN_COMAPANY_COUNTRY')"
											onChange = "ChkCountry_physical();" runat="server" ></asp:dropdownlist>--%>                            <%-- Commented by Swarup as Ashish send mail 
									"Country drop down on Reinsurance Broker Add/Edit screen should allow all 
									countries. Remove the drop down and let users type the name of the country."--%>
                            <td class="midcolora" width="32%" id="ttdREIN_COMAPANY_COUNTRY" runat="server">
                                <asp:TextBox ID="txtREIN_COMAPANY_COUNTRY" runat="server" MaxLength="50"></asp:TextBox> 
                            
                                <asp:DropDownList ID="cmbREIN_COMAPANY_COUNTRY_SIN" runat="server"  onChange= "SetValue();" style="display:none">
                                </asp:DropDownList>
                              
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_COUNTRY" runat="server" ControlToValidate="txtREIN_COMAPANY_COUNTRY"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                        
                         <td class="midcolora" width="18%">
                           <asp:label id="capDISTRICT" runat="server">DISTRICT</asp:label>
                        </td>
                        
                        <td class="midcolora" width="32%">
                        <asp:textbox id="txtDISTRICT" runat="server"  MaxLength="20" size="20"></asp:textbox>
                          </td>
                          
                           <td class="midcolora" width="18%">
                         <asp:label id="capCARRIER_CNPJ" runat="server">CARRIER_CNPJ</asp:label>
                         
                        </td>
                        
                        <td class="midcolora" width="32%">
                       <asp:textbox id="txtCARRIER_CNPJ" runat="server" MaxLength="18"  OnBlur="this.value=FormatCPFCNPJ(this.value); ValidatorOnChange();" size="20"></asp:textbox><br />
                          <asp:RegularExpressionValidator runat="server" ID="revCARRIER_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCARRIER_CNPJ"></asp:RegularExpressionValidator>
                    <asp:CustomValidator runat="server" ID="csvCARRIER_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCARRIER_CNPJ" ClientValidationFunction="validatCNPJ"></asp:CustomValidator>
                          </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_STATE" runat="server"></asp:Label><span id="spnREIN_COMAPANY_STATE" runat="server"
                                    class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_STATE" MaxLength="30" runat="server"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_STATE" runat="server" ControlToValidate="txtREIN_COMAPANY_STATE"
                                    Display="Dynamic"></asp:RequiredFieldValidator><br>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_ZIP" runat="server"></asp:Label><span id="spnREIN_COMAPANY_ZIP"
                                    class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_ZIP" runat="server" size="13" onkeypress="MaxLength(this,8)"
                                    onpaste="MaxLength(this,8)"></asp:TextBox><br />
                                <%-- Added by Swarup on 30-mar-2007 --%>                                <%--<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
											<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
										</asp:hyperlink><br>--%>
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_ZIP" runat="server" ControlToValidate="txtREIN_COMAPANY_ZIP"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revREIN_COMAPANY_ZIP" ControlToValidate="txtREIN_COMAPANY_ZIP"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_PHONE" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_PHONE" runat="server" size="17" onkeypress="MaxLength(this,15)"
                                    onpaste="MaxLength(this,15)"></asp:TextBox><br>
                                <%--<asp:regularexpressionvalidator id="revREIN_COMAPANY_PHONE" ControlToValidate="txtREIN_COMAPANY_PHONE" Display="Dynamic"
											Runat="server"></asp:regularexpressionvalidator>--%>
                                <asp:RegularExpressionValidator ID="revREIN_COMAPANY_PHONE" Display="Dynamic"
                                    ControlToValidate="txtREIN_COMAPANY_PHONE" runat="server"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_PHONE" runat="server" ControlToValidate="txtREIN_COMAPANY_PHONE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_EXT" runat="server"></asp:Label><span ID="spnREIN_COMAPANY_EXT" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_EXT" runat="server" size="17" onkeypress="MaxLength(this,15)"
                                    onpaste="MaxLength(this,15)"></asp:TextBox><br>
                                <%--<asp:regularexpressionvalidator id="revREIN_COMAPANY_EXT" ControlToValidate="txtREIN_COMAPANY_EXT" Display="Dynamic"
											Runat="server"></asp:regularexpressionvalidator>--%>
                                <asp:RegularExpressionValidator ID="revREIN_COMAPANY_EXT" Display="Dynamic"
                                    ControlToValidate="txtREIN_COMAPANY_EXT" runat="server"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_EXT" runat="server" ControlToValidate="txtREIN_COMAPANY_EXT"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>                       
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_FAX" runat="server"></asp:Label><span ID="spnREIN_COMAPANY_FAX" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_FAX" runat="server" size="17" MaxLength="15"></asp:TextBox><br />
                                <%--<asp:regularexpressionvalidator id="revREIN_COMAPANY_FAX" ControlToValidate="txtREIN_COMAPANY_FAX" Display="Dynamic"
											Runat="server"></asp:regularexpressionvalidator>--%>
                                <asp:RegularExpressionValidator ID="revREIN_COMAPANY_FAX" Display="Dynamic"
                                    ControlToValidate="txtREIN_COMAPANY_FAX" runat="server"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_FAX" runat="server" ControlToValidate="txtREIN_COMAPANY_FAX"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <!-- START April 10 2007 Harmanjeet-->
                            <!--<td class="midcolora" colSpan="2"></td>-->
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMPANY_SPEED_DIAL" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMPANY_SPEED_DIAL" runat="server" MaxLength="4"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revREIN_COMPANY_SPEED_DIAL" ControlToValidate="txtREIN_COMPANY_SPEED_DIAL"
                                    Display="Dynamic" runat="server" ValidationExpression="^[0-9]{4}"></asp:RegularExpressionValidator>
                            </td>
                            <!--END Harmanjeet-->
                        </tr> 
                        </tr>                        
                        <tr>
                            <td class="headerEffectSystemParams" style="height: 21px" colspan="4">
                              <asp:Label ID="capMAILING" runat="server"></asp:Label>
                            </td>  <%--Mailing Address--%>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="lblCopy_Address" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <cmsb:CmsButton class="clsButton" ID="btnCopyPhysicalAddress" runat="server" Text="Copy Physical Address">
                                </cmsb:CmsButton>
                            </td>
                            <td class="midcolora" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capM_REIN_COMPANY_ADD_1" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtM_REIN_COMPANY_ADD_1" runat="server" MaxLength="70"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvM_REIN_COMPANY_ADD_1" runat="server" ControlToValidate="txtM_REIN_COMPANY_ADD_1"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capM_RREIN_COMPANY_ADD_2" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtM_RREIN_COMPANY_ADD_2" runat="server" MaxLength="70"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capM_REIN_COMPANY_CITY" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtM_REIN_COMPANY_CITY" runat="server" MaxLength="40"></asp:TextBox><br>
                                <%--<asp:requiredfieldvalidator id="rfvM_REIN_COMPANY_CITY" runat="server" ControlToValidate="txtM_REIN_COMPANY_CITY"
							Display="Dynamic"></asp:requiredfieldvalidator><br>--%>
                                <asp:RegularExpressionValidator ID="revM_REIN_COMPANY_CITY" Display="Dynamic" ControlToValidate="txtM_REIN_COMPANY_CITY"
                                    runat="server"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capM_REIN_COMPANY_COUNTRY" runat="server"></asp:Label><asp:Label ID="capM_REIN_COMPANY_COUNTRY_SIN" runat="server"></asp:Label>
                            </td>
                            <%--<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbM_REIN_COMPANY_COUNTRY" onfocus="SelectComboIndex('cmbM_REIN_COMPANY_COUNTRY')"
							onChange= "ChkCountry_mailing();" runat="server" ></asp:dropdownlist><BR>--%>                            <%-- Commented by Swarup as Ashish send mail 
									"Country drop down on Reinsurance Broker Add/Edit screen should allow all 
									countries. Remove the drop down and let users type the name of the country."--%>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtM_REIN_COMPANY_COUNTRY" runat="server" MaxLength="40"></asp:TextBox>
                                
                                 <asp:DropDownList ID="cmbM_REIN_COMPANY_COUNTRY_SIN" runat="server"  onChange= "SetcmbMValue();" style="display:none" >
                                </asp:DropDownList>
                                <br>


                                <%--<asp:requiredfieldvalidator id="rfvM_REIN_COMPANY_COUNTRY" runat="server" ControlToValidate="txtM_REIN_COMPANY_COUNTRY"
							Display="Dynamic"></asp:requiredfieldvalidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capM_REIN_COMPANY_STATE" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtM_REIN_COMPANY_STATE" MaxLength="30" runat="server"></asp:TextBox><br>
                                <%--<asp:requiredfieldvalidator id="rfvM_REIN_COMPANY_STATE" runat="server" ControlToValidate="cmbM_REIN_COMPANY_STATE"
							Display="Dynamic"></asp:requiredfieldvalidator><br>--%>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capM_REIN_COMPANY_ZIP" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtM_REIN_COMPANY_ZIP" runat="server" onkeypress="MaxLength(this,8)"
                                    onpaste="MaxLength(this,8)" size="13"></asp:TextBox><br />
                                <%-- Added by Swarup on 30-mar-2007 --%>                                <%--<asp:hyperlink id="hlkMZipLookup" runat="server" CssClass="HotSpot">
							<asp:image id="imgMZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
						</asp:hyperlink><br>--%>                                <%--<asp:requiredfieldvalidator id="rfvM_REIN_COMPANY_ZIP" runat="server" ControlToValidate="txtM_REIN_COMPANY_ZIP"
							Display="Dynamic"></asp:requiredfieldvalidator>--%>
                                <asp:RegularExpressionValidator ID="revM_REIN_COMPANY_ZIP" Display="Dynamic" ControlToValidate="txtM_REIN_COMPANY_ZIP"
                                    runat="server"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capM_REIN_COMPANY_PHONE" runat="server"></asp:Label><span ID="spnM_REIN_COMPANY_PHONE" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtM_REIN_COMPANY_PHONE" runat="server" onkeypress="MaxLength(this,15)"
                                    onpaste="MaxLength(this,15)" size="17"></asp:TextBox><br>
                                <%--<asp:regularexpressionvalidator id="revM_REIN_COMPANY_PHONE" Display="Dynamic" ControlToValidate="txtM_REIN_COMPANY_PHONE"
							Runat="server"></asp:regularexpressionvalidator>--%>
                                <asp:RegularExpressionValidator ID="revM_REIN_COMPANY_PHONE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtM_REIN_COMPANY_PHONE"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvM_REIN_COMPANY_PHONE" runat="server" ControlToValidate="txtM_REIN_COMPANY_PHONE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capM_REIN_COMPANY_EXT" runat="server"></asp:Label><span ID="spnM_REIN_COMPANY_EXT" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtM_REIN_COMPANY_EXT" runat="server" onkeypress="MaxLength(this,15)"
                                    onpaste="MaxLength(this,15)" size="17"></asp:TextBox><br>
                                <%--<asp:regularexpressionvalidator id="revM_REIN_COMPANY_EXT" Display="Dynamic" ControlToValidate="txtM_REIN_COMPANY_EXT"
							Runat="server"></asp:regularexpressionvalidator>--%>
                                <asp:RegularExpressionValidator ID="revM_REIN_COMPANY_EXT" Display="Dynamic"
                                    ControlToValidate="txtM_REIN_COMPANY_EXT" runat="server"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvM_REIN_COMPANY_EXT" runat="server" ControlToValidate="txtM_REIN_COMPANY_EXT"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capM_REIN_COMPANY_FAX" runat="server"></asp:Label><span ID="spnM_REIN_COMPANY_FAX" runat="server" class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtM_REIN_COMPANY_FAX" runat="server" onkeypress="MaxLength(this,15)"
                                    onpaste="MaxLength(this,15)" size="17"></asp:TextBox><br>
                                <%--<asp:regularexpressionvalidator id="revM_REIN_COMPANY_FAX" Display="Dynamic" ControlToValidate="txtM_REIN_COMPANY_FAX"
							Runat="server"></asp:regularexpressionvalidator>--%>
                                <asp:RegularExpressionValidator ID="revM_REIN_COMPANY_FAX" Display="Dynamic"
                                    ControlToValidate="txtM_REIN_COMPANY_FAX" runat="server"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvM_REIN_COMPANY_FAX" runat="server" ControlToValidate="txtM_REIN_COMPANY_FAX"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <!--START April 09 2007 Harmanjeet-->
                            <td class="midcolora" colspan="2">
                            </td>
                            <!--END Harmanjeet-->
                        </tr>
                        <tr>
                            <td class="headerEffectSystemParams" colspan="4" style="height: 21px">
                               <asp:Label ID="capOTHER" runat="server"></asp:Label>
                            </td> <%--Other Information--%>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_MOBILE" runat="server"></asp:Label><%--<span class="mandatory">*</span>--%>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_MOBILE" runat="server" MaxLength="15" size="20"></asp:TextBox><br>
                                <%--<asp:regularexpressionvalidator id="revREIN_COMAPANY_MOBILE" Display="Dynamic" ControlToValidate="txtREIN_COMAPANY_MOBILE"
							Runat="server"></asp:regularexpressionvalidator>--%>
                                <asp:RegularExpressionValidator ID="revREIN_COMAPANY_MOBILE" Display="Dynamic"
                                    ControlToValidate="txtREIN_COMAPANY_MOBILE" runat="server"></asp:RegularExpressionValidator>
                                <%-- <asp:RequiredFieldValidator ID="rfvREIN_COMAPANY_MOBILE" runat="server" ControlToValidate="txtREIN_COMAPANY_MOBILE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMAPANY_EMAIL" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtREIN_COMAPANY_EMAIL" runat="server" MaxLength="50"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revREIN_COMAPANY_EMAIL" Display="Dynamic" ControlToValidate="txtREIN_COMAPANY_EMAIL"
                                    runat="server"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capREIN_COMPANY_WEBSITE" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%" colspan="3">
                                <asp:TextBox ID="txtREIN_COMPANY_WEBSITE" runat="server" MaxLength="70"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revREIN_COMPANY_WEBSITE" Display="Dynamic" ControlToValidate="txtREIN_COMPANY_WEBSITE"
                                    runat="server"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%" style="display: none">
                                <asp:Label ID="capREIN_COMPANY_IS_BROKER" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%" style="display: none">
                                <asp:DropDownList ID="cmbREIN_COMPANY_IS_BROKER" onfocus="SelectComboIndex('cmbREIN_COMPANY_IS_BROKER')"
                                    runat="server">
                                </asp:DropDownList>
                                <br>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capPRINCIPAL_CONTACT" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtPRINCIPAL_CONTACT" runat="server" MaxLength="15" size="15"></asp:TextBox><br>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capOTHER_CONTACT" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtOTHER_CONTACT" runat="server" MaxLength="15" size="15"></asp:TextBox><br>
                            </td>
                        </tr>
                        <!--START- NEW FIELDS ADDED-->
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capFEDERAL_ID" runat="server"></asp:Label><%--<span class="mandatory">*</span>--%>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capFEDERAL_ID_HID" runat="server" size="10" maxlength="9"></asp:Label><input
                                    class="clsButton" id="btnFEDERAL_ID" text="Edit" onclick="FEDERAL_ID_change();"
                                    type="button"></input><asp:TextBox ID="txtFEDERAL_ID" runat="server" MaxLength="9"
                                        size="10" Width="110px"></asp:TextBox><br>
                                <%--<asp:RequiredFieldValidator ID="rfvFEDERAL_ID" runat="server" Display="Dynamic" ControlToValidate="txtFEDERAL_ID"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="revFEDERAL_ID" runat="server" ControlToValidate="txtFEDERAL_ID"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capDOMICILED_STATE" runat="server"></asp:Label><%--<span class="mandatory">*</span>--%>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtDOMICILED_STATE" runat="server" MaxLength="25" size="10"></asp:TextBox><br>
                                <%--<asp:RequiredFieldValidator ID="rfvDOMICILED_STATE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDOMICILED_STATE"></asp:RequiredFieldValidator>--%>
                            </td>
                            <!--<td class="midcolora" colSpan="2"></td>-->
                        </tr>
                        <!--START-April 09, 2007 Harmanjeet-->
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capNAIC_CODE" runat="server"></asp:Label><%--<span class="mandatory">*</span>--%>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtNAIC_CODE" runat="server" MaxLength="10" size="11" Width="110px"></asp:TextBox><br>
                                <%--<asp:dropdownlist id="cmbNAIC_CODE" runat="server" ></asp:dropdownlist><br>--%>                                <%--<asp:RequiredFieldValidator ID="rfvNAIC_CODE" runat="server" Display="Dynamic" ControlToValidate="txtNAIC_CODE"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator ID="revNAIC_CODE" runat="server" ControlToValidate="txtNAIC_CODE"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capAM_BEST_RATING" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtAM_BEST_RATING" runat="server" MaxLength="5"></asp:TextBox><br>
                                <asp:RegularExpressionValidator ID="revAM_BEST_RATING" ControlToValidate="txtAM_BEST_RATING"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capEFFECTIVE_DATE" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtEFFECTIVE_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox>
                                <asp:HyperLink ID="hlkEFFECTIVE_DATE" runat="server" CssClass="HotSpot">
                                    <asp:Image ID="imgEFFECTIVE_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                                    </asp:Image>
                                </asp:HyperLink>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvEFFECTIVE_DATE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtEFFECTIVE_DATE"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEFFECTIVE_DATE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtEFFECTIVE_DATE"></asp:RegularExpressionValidator>
                            </td>
                            <td class="midcolora" colspan="2">
                            </td>
                        </tr>
        <!--END-Harmanjeet-->
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capTERMINATION_DATE" runat="server"></asp:Label><span class="mandatory">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtTERMINATION_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox><asp:HyperLink
                    ID="hlkTERMINATION_DATE" runat="server" CssClass="HotSpot">
                    <asp:Image ID="imgTERMINATION_DATE" runat="server" ImageUrl="/cms/CmsWeb/Images/CalendarPicker.gif">
                    </asp:Image>
                </asp:HyperLink><br />
                <asp:RequiredFieldValidator ID="rfvTERMINATION_DATE" runat="server" Display="Dynamic"
                    ControlToValidate="txtTERMINATION_DATE"></asp:RequiredFieldValidator>
              
                <asp:RegularExpressionValidator ID="revTERMINATION_DATE" runat="server" Display="Dynamic"
                    ControlToValidate="txtTERMINATION_DATE"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="cpvTERMINATION_DATE" ControlToValidate="txtTERMINATION_DATE"
                    Display="Dynamic" runat="server" ControlToCompare="txtEFFECTIVE_DATE" Type="Date"
                    Operator="GreaterThanEqual"></asp:CompareValidator>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capTERMINATION_REASON" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtTERMINATION_REASON" runat="server" MaxLength="30" size="12"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capREIN_COMAPANY_NOTE" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtREIN_COMAPANY_NOTE" runat="server" MaxLength="70"></asp:TextBox><br>
            </td>
            <td class="midcolora" width="18%">
                <asp:Label ID="capREIN_COMAPANY_ACC_NUMBER" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtREIN_COMAPANY_ACC_NUMBER" runat="server" MaxLength="20"></asp:TextBox><br>
                <asp:RegularExpressionValidator ID="revACCOUNT_NUMBER" ControlToValidate="txtREIN_COMAPANY_ACC_NUMBER" runat="server" Display="Dynamic" ></asp:RegularExpressionValidator>
            </td>
        </tr>
        <!--START April 09 2007 Harmanjeet-->
        <tr>
            <td class="midcolora" width="18%">
                <asp:Label ID="capCOMMENTS" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtCOMMENTS" runat="server" MaxLength="255" TextMode="MultiLine"></asp:TextBox><br>
                <%--<asp:regularexpressionvalidator id="revCOMMENTS" ControlToValidate="txtCOMMENTS" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>--%>
            </td>
            <td class="midcolora" colspan="2">
            </td>
        </tr>
        <!--End Harmanjeet-->
        <tr>
            <td width="18%" class="midcolora">
                <asp:Label ID="capSUSEP_NUM" runat="server"></asp:Label><span ID="spnSUSEP_NUM" runat="server" class="mandatory">*</span>
            </td>
            <td class="midcolora" width="32%">
                <asp:TextBox ID="txtSUSEP_NUM" runat="server" MaxLength="5"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvSUSEP_NUM" runat="server" Display="Dynamic" ControlToValidate="txtSUSEP_NUM"
                    ErrorMessage=""></asp:RequiredFieldValidator> <%--Changed by Aditya for TFS bug # 923--%>
                <asp:RegularExpressionValidator ID="revSUSEP_NUM" runat="server" Display="Dynamic"
                    ControlToValidate="txtSUSEP_NUM"></asp:RegularExpressionValidator>
            </td><%--Please enter the susep number--%>
            <td width="18%" class="midcolora">
                <asp:Label ID="capCOM_TYPE" runat="server"></asp:Label>
            </td>
            <td width="32%" class="midcolora">
                <asp:DropDownList ID="cmbCOM_TYPE" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        
        
        <tr>
                        <td class="midcolora" width="18%">
                        <asp:label id="capBANK_NUMBER" runat="server">BANK_NUMBER</asp:label><span class="mandatory">*</span>
                        </td>
                        
                        <td class="midcolora" width="32%">
                          <asp:textbox id="txtBANK_NUMBER" runat="server"  MaxLength="10" size="20"></asp:textbox><br />
                           <asp:RequiredFieldValidator ID="rfvBANK_NUMBER" runat="server" Display="Dynamic" ControlToValidate="txtBANK_NUMBER"
                    ErrorMessage=""></asp:RequiredFieldValidator>
                          </td>
                          
                          
                         <td class="midcolora" width="18%">
                       <asp:label id="capACCOUNT_TYPE" runat="server">ACCOUNT_TYPE</asp:label><span class="mandatory">*</span>
                        </td>
                        
                        <td class="midcolora" width="32%">
                          <asp:DropDownList ID="cmbACCOUNT_TYPE" runat="server"></asp:DropDownList><br/>
                          <asp:RequiredFieldValidator ID="rfvACCOUNT_TYPE" ControlToValidate="cmbACCOUNT_TYPE" runat="server" Display="dynamic"></asp:RequiredFieldValidator>
                          </td>
                         
                        
                         
                        </tr>
        <tr>
                         <td class="midcolora" width="18%">
                        <asp:label id="capBANK_BRANCH" runat="server">BANK_BRANCH</asp:label><%--<span class="mandatory">*</span>--%>
                        </td>
                        
                        <td class="midcolora" width="32%">
                          <asp:textbox id="txtBANK_BRANCH" runat="server"  size="20" MaxLength="20"></asp:textbox><br />
                          <asp:RegularExpressionValidator ID="revBANK_BRANCH" ControlToValidate="txtBANK_BRANCH" runat="server" Display="Dynamic"></asp:RegularExpressionValidator>
                          </td>
                          <td class="midcolora" width="18%">
                           <asp:label id="capPAYMENT_METHOD" runat="server">PAYMENT_METHOD</asp:label><span class="mandatory">*</span>
                        </td>
                        
                        <td class="midcolora" width="32%">
                         <asp:DropDownList ID="cmbPAYMENT_METHOD" runat="server"></asp:DropDownList><br/>
                          <asp:RequiredFieldValidator ID="rfvPAYMENT_METHOD" ControlToValidate="cmbPAYMENT_METHOD" runat="server" Display="dynamic"></asp:RequiredFieldValidator>
                          </td>
                        
                          </tr>
                          <tr>
                          <td class="midcolora" width="18%">
                          <asp:Label ID="capAGENCY_CLASSIFICATION" runat="server"></asp:Label>
                          </td>
                           <td class="midcolora" width="32%">
                           <asp:TextBox ID="txtAGENCY_CLASSIFICATION" runat="server" MaxLength="10"></asp:TextBox>
                          </td>
                           <td class="midcolora" width="18%">
                           <asp:Label ID="capRISK_CLASSIFICATION" runat="server"></asp:Label>
                          </td>
                           <td class="midcolora" width="32%">
                            <asp:TextBox ID="txtRISK_CLASSIFICATION" runat="server" MaxLength="10"></asp:TextBox>
                          </td>
                          </tr>
                         
        <tr>
            <td class="midcolora" colspan="2">
                <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" CausesValidation="False">
                </cmsb:CmsButton>&nbsp;
                <cmsb:CmsButton class="clsButton" ID="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
                    Visible="True" CausesValidation="False"></cmsb:CmsButton>
            </td>
            <td class="midcolorr" colspan="2">
                <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server"></cmsb:CmsButton>
            </td>
        </tr>
        </TBODY>
    </table>
    </TD></TR></TABLE>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
    <input id="hidREIN_COMAPANY_ID" type="hidden" value="0" name="hidREIN_COMAPANY_ID"
        runat="server">
    <input id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
    <input id="hidFEDERAL_ID" type="hidden" value="0" name="hidFEDERAL_ID" runat="server">
     <input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>  
    </form>

    <script>
   
            RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidREIN_COMAPANY_ID').value);


    </script>

</body>
</html>
