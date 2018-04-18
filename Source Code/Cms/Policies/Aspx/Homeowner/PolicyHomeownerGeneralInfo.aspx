<%@ Page Language="c#" CodeBehind="PolicyHomeownerGeneralInfo.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.Policies.aspx.Homeowners.PolicyHomeownerGeneralInfo" ValidateRequest="false" %>

<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>PolicyHomeownerGeneralInfo</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="stylesheet">
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script src="/cms/cmsweb/scripts/common.js"></script>
    <script src="/cms/cmsweb/scripts/form.js"></script>
    <script src="/cms/cmsweb/scripts/Calendar.js"></script>
    <script language="javascript">
        var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
        var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
        var jsaAppDtFormat = "<%=aAppDtFormat  %>";
        function ValidateLength(objSource, objArgs) {
            if (document.getElementById('txtDESC_Location').value.length > 255)
                objArgs.IsValid = false;
            else
                objArgs.IsValid = true;
        }

        function fnHandleFARMING_BUSINESS_COND() {
            EnableDisableDesc(document.getElementById('cmbANY_FARMING_BUSINESS_COND'), document.getElementById('cmbPROVIDE_HOME_DAY_CARE'), document.getElementById('lblPROVIDE_HOME_DAY_CARE'));
            EnableDisableDesc(document.getElementById('cmbANY_FARMING_BUSINESS_COND'), document.getElementById('txtDESC_FARMING_BUSINESS_COND'), document.getElementById('lblDESC_FARMING_BUSINESS_COND'));
        }
        function EnableValidatorHome(ValidatorId, flag) {
            //Enable the validator
            if (flag) {
                if (document.getElementById(ValidatorId) != null) {
                    document.getElementById(ValidatorId).setAttribute("enabled", true);

                    if (document.getElementById(ValidatorId).isvalid == false)
                        document.getElementById(ValidatorId).style.display = "inline";
                }
            } //Disable the validator
            else {
                if (document.getElementById(ValidatorId) != null) {
                    document.getElementById(ValidatorId).setAttribute("enabled", false);
                    document.getElementById(ValidatorId).style.display = "none";
                    if (document.getElementById(ValidatorId).isvalid == false)
                        document.getElementById(ValidatorId).isvalid = true;
                }
            }
        }
        function EnableDisableDesc(cmbCombo, txtDesc, lblNA) {
            if (cmbCombo.selectedIndex > -1) {

                //Checking value only if item is selected
                if (cmbCombo.options[cmbCombo.selectedIndex].text == "Yes") {
                    //Disabling the description field, if No is selected
                    txtDesc.style.display = "inline";
                    if (lblNA.id != 'hlkDESC_INSU_TRANSFERED_AGENCY' && lblNA.id != 'imgDESC_INSU_TRANSFERED_AGENCY') {
                        lblNA.style.display = "none";
                    }
                    else {
                        hlkDESC_INSU_TRANSFERED_AGENCY.style.display = "inline";
                    }

                    //Enabling the validators
                    EnableValidatorHome("rfv" + txtDesc.id.substring(3), true);
                    EnableValidatorHome("rev" + txtDesc.id.substring(3), true);
                    EnableValidatorHome("reg" + txtDesc.id.substring(3), true);
                    /*if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
                    {
                    document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",true);
						
                    if (document.getElementById("rfv" + txtDesc.id.substring(3)).isvalid == false)
                    document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "inline";
                    }*/

                    //making the * sign visible					
                    if (document.getElementById("spn" + txtDesc.id.substring(3)) != null) {
                        document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "inline";
                    }

                }
                else {
                    //Diabling the description field, if yes is selected
                    txtDesc.style.display = "none";
                    //Do not blank the text box when no is selected
                    //txtDesc.value = "";
                    lblNA.style.display = "inline";
                    txtDesc.value = "";

                    if (lblNA.id != 'hlkDESC_INSU_TRANSFERED_AGENCY' && lblNA.id != 'imgDESC_INSU_TRANSFERED_AGENCY')
                        lblNA.innerHTML = "NA";
                    else {
                        hlkDESC_INSU_TRANSFERED_AGENCY.style.display = "none";
                    }

                    //Disabling the validators					
                    EnableValidatorHome("rfv" + txtDesc.id.substring(3), false);
                    EnableValidatorHome("rev" + txtDesc.id.substring(3), false);
                    EnableValidatorHome("reg" + txtDesc.id.substring(3), false);
                    /*if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null)
                    {
                    document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled",false);
                    document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
                    }*/

                    //making the * sign invisible					
                    if (document.getElementById("spn" + txtDesc.id.substring(3)) != null)
                        document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
                    //}
                }
            }
            else {
                //Disabling the description field, if No is selected
                txtDesc.style.display = "none";
                lblNA.style.display = "inline";
                lblNA.innerHTML = "-N.A.-";

                //Disabling the validators					
                if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null) {
                    document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled", false);
                    document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
                }

                //making the * sign invisible					
                if (document.getElementById("spn" + txtDesc.id.substring(3)) != null) {
                    document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
                }
            }

        }

        function EnableDisableNonSmokerCredit() {
            var strCalledFrom = new String("<%=strCalledFrom%>");
            //if(strCalledFrom.toUpperCase()=="REN" || strCalledFrom.toUpperCase()=="RENTAL")
            if ((document.getElementById("hidStateID").value == "22" && document.getElementById("hidCalledFrom").value == "HOME") || strCalledFrom.toUpperCase() == "REN" || strCalledFrom.toUpperCase() == "RENTAL") {
                /*document.getElementById("capNON_SMOKER_CREDIT").style.display="none";
                document.getElementById("cmbNON_SMOKER_CREDIT").style.display="none";
                document.getElementById("spnNON_SMOKER_CREDIT").style.display="none";*/
                //document.getElementById("trNON_SMOKER_CREDIT").style.display = "none";
                ValidatorEnable(document.getElementById("rfvNON_SMOKER_CREDIT"), false);
                //document.getElementById("trDOG_SURCHARGE").style.display="inline";
            }
        }
        function ShowHideLocation(flag) {
            if (flag) {
                document.getElementById('txtLocation').style.display = "inline";
                document.getElementById('capLocation').style.display = "inline";
                document.getElementById('spnLocation').style.display = "inline";
                document.getElementById('rfvLocation').setAttribute("enabled", true);
                document.getElementById('revLocation').setAttribute("enabled", true);
            }
            else {
                document.getElementById('txtLocation').style.display = "none";
                document.getElementById('capLocation').style.display = "none";
                document.getElementById('spnLocation').style.display = "none";
                document.getElementById('rfvLocation').setAttribute("enabled", false);
                document.getElementById('revLocation').setAttribute("enabled", false);
                document.getElementById('rfvLocation').style.display = "none";
                document.getElementById('revLocation').style.display = "none";
            }
        }
        function ShowHideLocationDesc(flag) {
            if (flag) {
                document.getElementById('txtDESC_Location').style.display = "inline";
                document.getElementById('capDESC_Location').style.display = "inline";
                document.getElementById('spnDESC_Location').style.display = "inline";
                document.getElementById('rfvDESC_Location').setAttribute("enabled", true);
                document.getElementById('csvDESC_Location').setAttribute("enabled", true);
            }
            else {
                document.getElementById('txtDESC_Location').style.display = "none";
                document.getElementById('capDESC_Location').style.display = "none";
                document.getElementById('spnDESC_Location').style.display = "none";
                document.getElementById('rfvDESC_Location').setAttribute("enabled", false); ;
                document.getElementById('rfvDESC_Location').style.display = "none";
                document.getElementById('csvDESC_Location').setAttribute("enabled", false); ;
                document.getElementById('csvDESC_Location').style.display = "none";
            }
        }
        /*function showPremises1()
        {
        //hidePremises();
        //document.getElementById('rowHorse').style.display="inline";
        document.getElementById('txtLocation').style.display ="inline";
        document.getElementById('capLocation').style.display ="inline";
        document.getElementById('spnLocation').style.display ="inline";
        document.getElementById('txtDESC_Location').style.display ="inline";
        document.getElementById('capDESC_Location').style.display ="inline";
        document.getElementById('spnDESC_Location').style.display ="inline";
        document.getElementById('rfvDESC_Location').setAttribute("enabled",true);
        document.getElementById('rfvDESC_Location').style.display="inline";
        document.getElementById('rfvLocation').setAttribute("enabled",true);
        document.getElementById('revLocation').setAttribute("enabled",true);			      
			       
        }*/
        /*function showPremises()
        {
        //hidePremises1();
        //document.getElementById('rowHorse').style.display="inline";
        document.getElementById('cmbIsAny_Horse').style.display ="inline";
        document.getElementById('spnIsAny_Horse').style.display ="inline";
        document.getElementById('capIsAny_Horse').style.display ="inline";
        //document.getElementById('txtOf_Acres_P').style.display ="inline";
        //document.getElementById('spnOf_Acres_P').style.display ="inline";
        document.getElementById('rfvIsAny_Horse').setAttribute("enabled",true);
        //document.getElementById('rfvOf_Acres_P').setAttribute("enabled",true);
        //document.getElementById('revOf_Acres_P').setAttribute("enabled",true);
        //document.getElementById('capOf_Acres_P').style.display ="inline";
			       
        }*/

        function cmbAny_Forming_OnChange() {

//            if (document.getElementById('cmbAny_Forming').options[document.getElementById('cmbAny_Forming').selectedIndex].value == 1) {
//                //document.getElementById('Of_Acres').style.display="inline";
//                //document.getElementById('rowPremises').style.display="inline";
//                showforming();
//                Premises_OnChange();
//                //IsAny_Horse_OnChange();
//            }
//            else {
//                //document.getElementById('rowPremises').style.display="none";
//                //document.getElementById('rowHorse').style.display="none";
//                document.getElementById('cmbPremises').options.selectedIndex = 0;
//                //document.getElementById('cmbIsAny_Horse').options.selectedIndex=0;
//                hideforming();
//                //hidePremises();
//                //hidePremises1();
//                //hideAny_Horse();			            
//            }
        }
        function showforming() {
            document.getElementById('cmbPremises').style.display = "inline";
            document.getElementById('rowPremises').style.display = "inline";
            document.getElementById('txtOf_Acres').style.display = "inline";
            document.getElementById('rfvPremises').setAttribute("enabled", true);
            document.getElementById('rfvOf_Acres').setAttribute("enabled", true);
            document.getElementById('revOf_Acres').setAttribute("enabled", true);

            document.getElementById('capPremises').style.display = "inline";
            document.getElementById('capOf_Acres').style.display = "inline";
            document.getElementById('spnPremises').style.display = "inline";
            document.getElementById('spnOf_Acres').style.display = "inline";
        }

        function Premises_OnChange() {
            if (document.getElementById('cmbPremises').options[document.getElementById('cmbPremises').selectedIndex].value == "11557") {
                document.getElementById('rowHorse').style.display = "inline";
                ShowHideLocation(false);
                ShowHideLocationDesc(true);
                //showPremises();
                //IsAny_Horse_OnChange();

            }
            else if (document.getElementById('cmbPremises').options[document.getElementById('cmbPremises').selectedIndex].value == "11558" || document.getElementById('cmbPremises').options[document.getElementById('cmbPremises').selectedIndex].value == "11559") {
                document.getElementById('rowHorse').style.display = "inline";
                ShowHideLocation(true);
                ShowHideLocationDesc(true);
                //document.getElementById('cmbIsAny_Horse').options.selectedIndex=0;							
                //showPremises1();
                //hideAny_Horse();
            }
            else {
                //document.getElementById('cmbIsAny_Horse').options.selectedIndex=0;
                document.getElementById('rowHorse').style.display = "none";
                //hidePremises();
                //hidePremises1()
                //hideAny_Horse();
                ShowHideLocation(false);
                ShowHideLocationDesc(false);
            }
        }
        function IsAny_Horse_OnChange() {
//            if (document.getElementById('cmbIsAny_Horse').options[document.getElementById('cmbIsAny_Horse').selectedIndex].value == 1) {
//                ShowAny_Horse();

//            }
//            else {
//                hideAny_Horse();

//            }
        }
        function ShowAny_Horse() {
            //document.getElementById('rowhorsetext').style.display ="inline";
            //document.getElementById('txtNo_Horses').style.display = "inline";
            document.getElementById('rfvNo_Horses').setAttribute("enabled", true);
            document.getElementById('revNo_Horses').setAttribute("enabled", true);
            //document.getElementById('capNo_Horses').style.display = "inline";
            document.getElementById('spnNo_Horses').style.display = "inline";
        }
        function hideforming() {
            document.getElementById('rowPremises').style.display = "none";
            document.getElementById('capPremises').style.display = "none";
            document.getElementById('capOf_Acres').style.display = "none";
            document.getElementById('cmbPremises').style.display = "none";
            document.getElementById('txtOf_Acres').style.display = "none";
            document.getElementById('txtOf_Acres').value = '';
            document.getElementById('revOf_Acres').setAttribute("enabled", false);
            document.getElementById('rfvPremises').setAttribute("enabled", false);
            document.getElementById('rfvPremises').style.display = "none";
            document.getElementById('rfvOf_Acres').setAttribute("enabled", false);
            document.getElementById('rfvOf_Acres').style.display = "none";
            document.getElementById('revOf_Acres').style.display = "none";
            //document.getElementById('rfvOf_Acres_p').style.display="none";
            //document.getElementById('revOf_Acres_P').style.display="none";
            document.getElementById('rfvPremises').style.display = "none";
            document.getElementById('rfvPremises').style.display = "none";
            document.getElementById('spnPremises').style.display = "none";
            document.getElementById('spnOf_Acres').style.display = "none";
            ShowHideLocation(false);
            ShowHideLocationDesc(false);
        }
        /*function hidePremises()
        {
        //alert('hello');
        //document.getElementById('rowHorse').style.display="none";
        document.getElementById('cmbIsAny_Horse').style.display="none";
        //document.getElementById('txtOf_Acres_P').style.display="none";
        //document.getElementById('txtOf_Acres_P').style.value='';
        document.getElementById('rfvIsAny_Horse').setAttribute("enabled",false);
        //document.getElementById('rfvOf_Acres_P').setAttribute("enabled",false);
        //document.getElementById('revOf_Acres_P').setAttribute("enabled",false);
        document.getElementById('rfvIsAny_Horse').style.display ="none";
        //document.getElementById('rfvOf_Acres_P').style.display ="none";
        //document.getElementById('revOf_Acres_P').style.display ="none";
        //document.getElementById('capOf_Acres_P').style.display ="none" ;
        document.getElementById('capIsAny_Horse').style.display ="none"; 
        document.getElementById('spnIsAny_Horse').style.display ="none";
        //document.getElementById('spnOf_Acres_P').style.display ="none";
        }*/
        /*function hidePremises1()
        {
        //document.getElementById('rowHorse').style.display="none";
        document.getElementById('txtLocation').style.display ="none";
        document.getElementById('capLocation').style.display ="none";
        document.getElementById('spnLocation').style.display ="none";
        document.getElementById('txtDESC_Location').style.display ="none";
        document.getElementById('capDESC_Location').style.display ="none";
        document.getElementById('spnDESC_Location').style.display ="none";
        document.getElementById('rfvDESC_Location').setAttribute("enabled",false);
        document.getElementById('rfvLocation').setAttribute("enabled",false);
        document.getElementById('revLocation').setAttribute("enabled",false);
        document.getElementById('rfvLocation').style.display="none";
        document.getElementById('revLocation').style.display="none";
        document.getElementById('rfvDESC_Location').style.display="none";
				 
        }*/
        function hideAny_Horse() {
            //document.getElementById('rowhorsetext').style.display ="none";
            //document.getElementById('txtNo_Horses').style.display = "none";
            //document.getElementById('txtNo_Horses').value = '';
            document.getElementById('rfvNo_Horses').setAttribute("enabled", false);
            document.getElementById('revNo_Horses').setAttribute("enabled", false);
            //document.getElementById('capNo_Horses').style.display = "none";
            document.getElementById('rfvNo_Horses').style.display = "none";
            document.getElementById('revNo_Horses').style.display = "none";
            document.getElementById('spnNo_Horses').style.display = "none";
        }

        //Added BY shafi
        function EnableDisableDescOther(cmbCombo, txtDesc, lblNA) {
            if (cmbCombo.selectedIndex > -1) {
                //Checking value only if item is selected
                if (cmbCombo.options[cmbCombo.selectedIndex].text != "0") {
                    //Disabling the description field, if No is selected
                    if (document.getElementById("hidCalledFrom").value == 'RENTAL') //If condition added by Charles on 9-Dec-09 for Itrack 6489
                    {
                        txtDesc.style.display = "inline";

                        //Enabling the validators
                        if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null) {
                            document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled", true);

                            if (document.getElementById("rfv" + txtDesc.id.substring(3)).isvalid == false)
                                document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "inline";
                        }
                    }
                    else //Added by Charles on 9-Dec-09 for Itrack 6489
                    {
                        document.getElementById('tabOTHER_DESCRIPTION').style.display = "inline";
                        document.getElementById('hidOTHER_DESCRIPTION').value = document.getElementById('tabOTHER_DESCRIPTION').getElementsByTagName('tr').length;
                    } //Added till here

                    lblNA.style.display = "none";

                    //making the * sign visible					
                    if (document.getElementById("spn" + txtDesc.id.substring(3)) != null) {
                        document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "inline";
                    }
                    //Added by Charles on 9-Dec-09 for Itrack 6489
                    if (document.getElementById("hidCalledFrom").value == 'HOME')
                        populateOTHER_DESCRIPTION(); 	//Added till here											
                }
                else {
                    //Enabling the description field, if yes is selected
                    if (document.getElementById("hidCalledFrom").value == 'RENTAL') //Added by Charles on 9-Dec-09 for Itrack 6489
                        txtDesc.style.display = "none";
                    //txtDesc.value="";
                    lblNA.style.display = "inline";
                    lblNA.innerHTML = "NA";

                    //Disabling the validators		
                    if (document.getElementById("hidCalledFrom").value == 'RENTAL') //If condition added by Charles on 9-Dec-09 for Itrack 6489	
                    {
                        if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null) {
                            document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled", false);
                            document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
                        }
                    }
                    else //Added by Charles on 9-Dec-09 for Itrack 6489
                    {
                        if (document.getElementById("csv" + txtDesc.id.substring(3)) != null) {
                            document.getElementById("csv" + txtDesc.id.substring(3)).style.display = "none";
                        }
                    } //Added till here

                    //making the * sign invisible					
                    if (document.getElementById("spn" + txtDesc.id.substring(3)) != null) {
                        document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
                    }

                    if (document.getElementById("hidCalledFrom").value == 'HOME') //Added by Charles on 9-Dec-09 for Itrack 6489
                    {
                        DelNewTextDescOther(false); //Delete all newly added rows
                        document.getElementById('hidOTHER_DESCRIPTION_VALUE').value = '';
                        txtDesc.selectedIndex = 0;
                        if (document.getElementById("txt" + txtDesc.id.substring(3)) != null) {
                            document.getElementById("txt" + txtDesc.id.substring(3)).value = "";
                            document.getElementById("txt" + txtDesc.id.substring(3)).style.display = "none";
                        }
                        document.getElementById('tabOTHER_DESCRIPTION').style.display = "none";
                        if (document.getElementById("lnk" + txtDesc.id.substring(3)) != null) {
                            document.getElementById("lnk" + txtDesc.id.substring(3)).style.display = "none";
                        }
                        if (document.getElementById("lnk" + txtDesc.id.substring(3) + "1") != null) {
                            document.getElementById("lnk" + txtDesc.id.substring(3) + "1").style.display = "none";
                        }
                    } //Added till here
                }
            }
            else {
                //Disabling the description field, if No is selected
                if (document.getElementById("hidCalledFrom").value == 'RENTAL') //If condition added by Charles on 9-Dec-09 for Itrack 6489
                    txtDesc.style.display = "none";
                lblNA.style.display = "inline";
                lblNA.innerHTML = "-N.A.-";

                //Disabling the validators	
                if (document.getElementById("hidCalledFrom").value == 'RENTAL') //If condition added by Charles on 9-Dec-09 for Itrack 6489	
                {
                    if (document.getElementById("rfv" + txtDesc.id.substring(3)) != null) {
                        document.getElementById("rfv" + txtDesc.id.substring(3)).setAttribute("enabled", false);
                        document.getElementById("rfv" + txtDesc.id.substring(3)).style.display = "none";
                    }
                }
                else //Added by Charles on 9-Dec-09 for Itrack 6489
                {
                    if (document.getElementById("csv" + txtDesc.id.substring(3)) != null) {
                        document.getElementById("csv" + txtDesc.id.substring(3)).style.display = "none";
                    }
                } //Added till here

                //making the * sign invisible					
                if (document.getElementById("spn" + txtDesc.id.substring(3)) != null) {
                    document.getElementById("spn" + txtDesc.id.substring(3)).style.display = "none";
                }

                if (document.getElementById("hidCalledFrom").value == 'HOME') //Added by Charles on 9-Dec-09 for Itrack 6489
                {
                    txtDesc.selectedIndex = 0;
                    document.getElementById('hidOTHER_DESCRIPTION_VALUE').value = '';
                    if (document.getElementById("txt" + txtDesc.id.substring(3)) != null) {
                        document.getElementById("txt" + txtDesc.id.substring(3)).value = "";
                        document.getElementById("txt" + txtDesc.id.substring(3)).style.display = "none";
                    }
                    document.getElementById('tabOTHER_DESCRIPTION').style.display = "none";
                    if (document.getElementById("lnk" + txtDesc.id.substring(3)) != null) {
                        document.getElementById("lnk" + txtDesc.id.substring(3)).style.display = "none";
                    }
                    if (document.getElementById("lnk" + txtDesc.id.substring(3) + "1") != null) {
                        document.getElementById("lnk" + txtDesc.id.substring(3) + "1").style.display = "none";
                    }
                } //Added till here
            }
        }

        function EnableDisableTextDescOther(cmbCombo) //Function added by Charles on 9-Dec-09 for Itrack 6489
        {
//            if (cmbCombo.selectedIndex != 0 && document.getElementById('cmbNO_OF_PETS').selectedIndex != 0) {
//                document.getElementById("txt" + cmbCombo.id.substring(3)).style.display = "inline";
//                if (document.getElementById('tabOTHER_DESCRIPTION').rows.length == 1)
//                    document.getElementById('lnkOTHER_DESCRIPTION').style.display = "inline";
//            }
//            else {
//                document.getElementById("txt" + cmbCombo.id.substring(3)).value = '';
//                document.getElementById("txt" + cmbCombo.id.substring(3)).style.display = "none";
//                if (document.getElementById('tabOTHER_DESCRIPTION').rows.length == 1)
//                    document.getElementById('lnkOTHER_DESCRIPTION').style.display = "none";
//            }
        }

        function AddNewTextDescOther() //Function added by Charles on 9-Dec-09 for Itrack 6489
        {
            var rows = document.getElementById('tabOTHER_DESCRIPTION').getElementsByTagName('tr');
            var index = rows.length;

            if (index < 10) {
                var clone = rows[index - 1].cloneNode(true);

                var inputs = clone.getElementsByTagName('input');
                var cntrl, i = 0;
                while (cntrl = inputs[i++]) {
                    cntrl.id = cntrl.id.replace(/\d/g, '') + (index + 1);
                    cntrl.name = cntrl.id;
                    cntrl.value = "";
                }
                var select = clone.getElementsByTagName('select');
                var cntrl, i = 0;
                while (cntrl = select[i++]) {
                    cntrl.id = cntrl.id.replace(/\d/g, '') + (index + 1);
                    cntrl.name = cntrl.id;
                }

                var tbo = document.getElementById('tabOTHER_DESCRIPTION').getElementsByTagName('tbody')[0];
                tbo.appendChild(clone);
                document.getElementById('lnkOTHER_DESCRIPTION1').style.display = 'inline';
                document.getElementById('hidOTHER_DESCRIPTION').value = index + 1;
                if (parseInt(document.getElementById('hidOTHER_DESCRIPTION').value) == 10) {
                    //document.getElementById('lnkOTHER_DESCRIPTION').style.display = 'none';
                }
            }
        }

        function DelNewTextDescOther(last) //Function added by Charles on 9-Dec-09 for Itrack 6489
        {
//            var tbl = document.getElementById('tabOTHER_DESCRIPTION');
//            if (tbl == null) return;

            if (last)//Delete Last Row
            {
                var lastRow = tbl.rows.length;
                if (lastRow > 1) {
                    tbl.deleteRow(lastRow - 1);
                    document.getElementById('hidOTHER_DESCRIPTION').value = parseInt(document.getElementById('hidOTHER_DESCRIPTION').value) - 1;
                    if (lastRow == 10) {
                        //document.getElementById('lnkOTHER_DESCRIPTION').style.display = 'inline';
                    }
                }
                if ((lastRow - 1) == 1)
                    document.getElementById('lnkOTHER_DESCRIPTION1').style.display = 'none';
            }
            else //Delete All Added Rows
            {
                var i = parseInt(document.getElementById('hidOTHER_DESCRIPTION').value == '' ? 0 : document.getElementById('hidOTHER_DESCRIPTION').value);
                while (i-- > 1) {
                    if (tbl.rows[i] != null)
                        tbl.deleteRow(i);
                }
                document.getElementById('hidOTHER_DESCRIPTION').value = '0';
            }
        }

        function populateOTHER_DESCRIPTION()//Function added by Charles on 9-Dec-09 for Itrack 6489
        {
            DelNewTextDescOther(false);
            if (document.getElementById('tabOTHER_DESCRIPTION').rows.length == 1)
                document.getElementById('lnkOTHER_DESCRIPTION1').style.display = 'none';

//            var cmbCombo = document.getElementById('cmbNO_OF_PETS');
//            if (cmbCombo && cmbCombo.options[cmbCombo.selectedIndex].text != "0") {
//                if (document.getElementById('hidOTHER_DESCRIPTION_VALUE') && document.getElementById('hidOTHER_DESCRIPTION_VALUE').value != '') {	//alert(document.getElementById('hidOTHER_DESCRIPTION_VALUE').value)				
//                    var oldrowscount = document.getElementById('tabOTHER_DESCRIPTION').rows.length;
//                    var rows = document.getElementById('hidOTHER_DESCRIPTION_VALUE').value.split('^');
//                    var k = 2;
//                    for (var i = 0; i < rows.length - 1; i++) {
//                        var cols = rows[i].split('~');

//                        if (i == 0) {
//                            SelectComboOption("cmbOTHER_DESCRIPTION", cols[0]);
//                            EnableDisableTextDescOther(document.getElementById('cmbOTHER_DESCRIPTION'));
//                            document.getElementById('txtOTHER_DESCRIPTION').value = cols[1];
//                        }
//                        else {
//                            if (oldrowscount != rows.length - 1) {
//                                AddNewTextDescOther();
//                                if (document.getElementById('cmbOTHER_DESCRIPTION' + k) != null) {
//                                    SelectComboOption("cmbOTHER_DESCRIPTION" + k, cols[0]);
//                                    document.getElementById('txtOTHER_DESCRIPTION' + k).value = cols[1];
//                                    k++;
//                                }
//                            }
//                        }
//                    } // end of for loop				
//                }
//            }
        }

        function ClientSideSave()//Function added by Charles on 9-Dec-09 for Itrack 6489
        {
//            var cmbCombo = document.getElementById('cmbNO_OF_PETS');
//            if (cmbCombo && cmbCombo.options[cmbCombo.selectedIndex].text != "0") {
//                var strOTHER_DESCRIPTION = ''; var strName = "OTHER_DESCRIPTION";
//                var tmpSelectID = "cmb" + strName; var tmpInputID = "txt" + strName;
//                var tmpSelect = document.getElementById(tmpSelectID); var tmpInput = document.getElementById(tmpInputID);
//                strOTHER_DESCRIPTION = tmpSelect.options[tmpSelect.selectedIndex].value + '~' + tmpInput.value + "^";

//                if (document.getElementById('hidOTHER_DESCRIPTION').value != '')
//                    for (var i = 2; i <= parseInt(document.getElementById('hidOTHER_DESCRIPTION').value); i++) {
//                        tmpSelectID = "cmb" + strName + i;
//                        tmpSelect = document.getElementById(tmpSelectID);
//                        tmpInputID = "txt" + strName + i;
//                        tmpInput = document.getElementById(tmpInputID);
//                        strOTHER_DESCRIPTION += tmpSelect.options[tmpSelect.selectedIndex].value + '~' + tmpInput.value + "^";
//                    }
//                document.getElementById('hidOTHER_DESCRIPTION_VALUE').value = strOTHER_DESCRIPTION;
//            }
        }

        function ValOTHER_DESCRIPTION()//Function added by Charles on 9-Dec-09 for Itrack 6489
        {
//            if (Page_ClientValidate() == false) return false; //Done by Sibin for Itrack Issue 6640 on 11 Dec 09	
//            var cmbCombo = document.getElementById('cmbNO_OF_PETS');
//            if (cmbCombo && cmbCombo.options[cmbCombo.selectedIndex].text != "0") {
//                var aRegExpInteger = /^[1-9]+$/;
//                var total = 0;
//                var strOTHER_DESCRIPTION = ''; var strName = "OTHER_DESCRIPTION";
//                var tmpSelectID = "cmb" + strName; var tmpInputID = "txt" + strName;
//                var tmpSelect = document.getElementById(tmpSelectID); var tmpInput = document.getElementById(tmpInputID);
//                var valid = true; var patVal = true;
//                if (tmpSelect.options[tmpSelect.selectedIndex].text == '' || tmpInput.value == '') {
//                    valid = false;
//                }
//                else if (aRegExpInteger.test(tmpInput.value) == false) {
//                    patVal = false;
//                }
//                else {
//                    total += parseInt(tmpInput.value);
//                }
//                if (document.getElementById('hidOTHER_DESCRIPTION').value != '' && valid == true) {
//                    for (var i = 2; i <= parseInt(document.getElementById('hidOTHER_DESCRIPTION').value); i++) {
//                        tmpSelectID = "cmb" + strName + i; tmpInputID = "txt" + strName + i;
//                        tmpSelect = document.getElementById(tmpSelectID); tmpInput = document.getElementById(tmpInputID);

//                        if (tmpSelect.options[tmpSelect.selectedIndex].text == '' || tmpInput.value == '') {
//                            valid = false;
//                            break;
//                        }
//                        else if (aRegExpInteger.test(tmpInput.value) == false) {
//                            patVal = false;
//                        }
//                        else {
//                            total += parseInt(tmpInput.value);
//                        }
//                    }
//                }
//                if (valid) {
//                    if (patVal) {
//                        if (total != parseInt(cmbCombo.options[cmbCombo.selectedIndex].text)) {
//                            document.getElementById('cmbOTHER_DESCRIPTION').focus();
//                            document.getElementById('csvOTHER_DESCRIPTION').style.display = 'inline';
//                            document.getElementById('csvOTHER_DESCRIPTION').innerHTML = 'Total differs from Number/Breed of dogs';
//                            return false;
//                        }
//                        else {
//                            document.getElementById('csvOTHER_DESCRIPTION').innerHTML = '';
//                            document.getElementById('csvOTHER_DESCRIPTION').style.display = 'none';
//                            ClientSideSave();
//                        }
//                    }
//                    else {
//                        document.getElementById('cmbOTHER_DESCRIPTION').focus();
//                        document.getElementById('csvOTHER_DESCRIPTION').style.display = 'inline';
//                        document.getElementById('csvOTHER_DESCRIPTION').innerHTML = 'Please enter proper numeric value.';
//                        return false;
//                    }
//                }
//                else {
//                    document.getElementById('cmbOTHER_DESCRIPTION').focus();
//                    document.getElementById('csvOTHER_DESCRIPTION').style.display = 'inline';
//                    document.getElementById('csvOTHER_DESCRIPTION').innerHTML = 'Please fill all details';
//                    return false;
//                }
//            }
        }

        function AddData() {
            ChangeColor();
            DisableValidators();
//            if (document.getElementById('trBody').style.display == 'none') return;

            //document.getElementById('cmbIS_VACENT_OCCUPY').focus();

            document.getElementById('cmbANY_FARMING_BUSINESS_COND').options.selectedIndex = 0;
            //document.getElementById('cmbDESC_FARMING_BUSINESS_COND').options.selectedIndex = 0;
            document.getElementById('txtDESC_FARMING_BUSINESS_COND').value = '';

            //document.getElementById('cmbANY_RESIDENCE_EMPLOYEE').options.selectedIndex = 0;
            //document.getElementById('txtDESC_RESIDENCE_EMPLOYEE').value = '';
            //document.getElementById('cmbANY_OTHER_RESI_OWNED').options.selectedIndex = 0;
            //document.getElementById('txtDESC_OTHER_RESIDENCE').value = '';
            //document.getElementById('cmbANY_OTH_INSU_COMP').options.selectedIndex = 0;
            //document.getElementById('txtDESC_OTHER_INSURANCE').value = '';
            //document.getElementById('cmbHAS_INSU_TRANSFERED_AGENCY').options.selectedIndex = 0;
            //document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY').value = '';
            document.getElementById('cmbANY_COV_DECLINED_CANCELED').options.selectedIndex = 0;
            document.getElementById('cmbANY_PRIOR_LOSSES').options.selectedIndex = 0;
            document.getElementById('txtDESC_COV_DECLINED_CANCELED').value = '';
            //document.getElementById('cmbANIMALS_EXO_PETS_HISTORY').options.selectedIndex = 0;
            if (document.getElementById("hidCalledFrom").value == 'RENTAL') //Added by Charles on 9-Dec-09 for Itrack 6489
                //document.getElementById('txtBREED').value = '';
            //else
                //DelNewTextDescOther(false); //Added by Charles on 9-Dec-09 for Itrack 6489

            //document.getElementById('txtLAST_INSPECTED_DATE').value = '';
            //document.getElementById('cmbCONVICTION_DEGREE_IN_PAST').options.selectedIndex = 0;
            //document.getElementById('txtDESC_CONVICTION_DEGREE_IN_PAST').value = '';

            //RP -- New Ques
            //document.getElementById('txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC').value = '';
            //document.getElementById('cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE').selectedIndex = 0;

            //document.getElementById('cmbANY_RENOVATION').options.selectedIndex = 0;
            //document.getElementById('txtDESC_RENOVATION').value = '';
            //document.getElementById('cmbTRAMPOLINE').options.selectedIndex = 0;
            //document.getElementById('txtDESC_TRAMPOLINE').value = '';
            //document.getElementById('cmbLEAD_PAINT_HAZARD').options.selectedIndex = 0;
            //document.getElementById('txtDESC_LEAD_PAINT_HAZARD').value = '';
            //document.getElementById('cmbNO_OF_PETS').options.selectedIndex = 0;
            //document.getElementById('cmbRENTERS').options.selectedIndex = 0;
            //document.getElementById('txtDESC_RENTERS').value = '';
            //document.getElementById('cmbBUILD_UNDER_CON_GEN_CONT').options.selectedIndex = 0;
            //document.getElementById('txtREMARKS').value = '';
            //document.getElementById('txtYEARS_INSU').value = '';
            //document.getElementById('txtYEARS_INSU_WOL').value = '';

            // --Added by mohit.
            //document.getElementById('cmbIS_VACENT_OCCUPY').options.selectedIndex = 0;
            //document.getElementById('cmbIS_RENTED_IN_PART').options.selectedIndex = 0;
            //document.getElementById('cmbIS_DWELLING_OWNED_BY_OTHER').options.selectedIndex = 0;
            //document.getElementById('cmbIS_PROP_NEXT_COMMERICAL').options.selectedIndex = 0;
            //document.getElementById('cmbARE_STAIRWAYS_PRESENT').options.selectedIndex = 0;
            //document.getElementById('cmbIS_OWNERS_DWELLING_CHANGED').options.selectedIndex = 0;
            //document.getElementById('txtDESC_PROPERTY').value = '';
            //document.getElementById('txtDESC_STAIRWAYS').value = '';
            //document.getElementById('txtDESC_OWNER').value = '';
            //document.getElementById('txtDESC_VACENT_OCCUPY').value = '';
            //document.getElementById('txtDESC_RENTED_IN_PART').value = '';
            //document.getElementById('txtDESC_DWELLING_OWNED_BY_OTHER').value = '';
            //document.getElementById('txtDESC_ANY_HEATING_SOURCE').value = '';

            if (document.getElementById('hidCalledFrom').value == "HOME") {
                //document.getElementById('cmbAny_Forming').options.selectedIndex = 0;
                document.getElementById('cmbPremises').options.selectedIndex = 0;
            }
            //document.getElementById('txtDESC_IS_SWIMPOLL_HOTTUB').value = '';
            //document.getElementById('txtDESC_MULTI_POLICY_DISC_APPLIED').value = '';
            //document.getElementById('txtDESC_BUILD_UNDER_CON_GEN_CONT').value = '';

            //Call for rental only---Not in HOME
            if (document.getElementById('cmbPROPERTY_ON_MORE_THAN')) {
                document.getElementById('cmbPROPERTY_ON_MORE_THAN').options.selectedIndex = 0;
                document.getElementById('txtPROPERTY_ON_MORE_THAN_DESC').value = '';
                document.getElementById('cmbDWELLING_MOBILE_HOME').options.selectedIndex = 0;
                document.getElementById('txtDWELLING_MOBILE_HOME_DESC').value = '';
                document.getElementById('cmbPROPERTY_USED_WHOLE_PART').options.selectedIndex = 0;
                document.getElementById('txtPROPERTY_USED_WHOLE_PART_DESC').value = '';
            }
            document.getElementById('cmbANY_PRIOR_LOSSES').options.selectedIndex = 0;
            document.getElementById('txtANY_PRIOR_LOSSES_DESC').value = '';

            // --End. 
        }
        function populateXML() {
            if (document.getElementById('hidFormSaved').value == '0') {
                if (document.getElementById('hidOldData').value != "") {
                    //alert(document.getElementById('hidOldData').value);
                    populateFormData(document.getElementById('hidOldData').value, POL_HOME_OWNER_GEN_INFO);
                }
                else {
                    AddData();
                }
            }
            //document.getElementById('cmbIS_VACENT_OCCUPY').focus();
            if (document.getElementById('hidCalledFrom').value == "HOME") {
                //showforming();				
                Premises_OnChange();
                IsAny_Horse_OnChange();
                cmbAny_Forming_OnChange();

            }
            SwimmingPool_OnChange();
            //EnableDisableDesc(document.getElementById('cmbANY_FARMING_BUSINESS_COND'),document.getElementById('txtDESC_BUSINESS'),document.getElementById('lblDESC_BUSINESS'));
            //EnableDisableDesc(document.getElementById('cmbANY_RESIDENCE_EMPLOYEE'), document.getElementById('txtDESC_RESIDENCE_EMPLOYEE'), document.getElementById('lblDESC_RESIDENCE_EMPLOYEE'));
            //EnableDisableDesc(document.getElementById('cmbANY_OTHER_RESI_OWNED'), document.getElementById('txtDESC_OTHER_RESIDENCE'), document.getElementById('lblDESC_OTHER_RESIDENCE'));
            //EnableDisableDesc(document.getElementById('cmbANY_OTH_INSU_COMP'), document.getElementById('txtDESC_OTHER_INSURANCE'), document.getElementById('lblDESC_OTHER_INSURANCE'));

            //EnableDisableDesc(document.getElementById('cmbHAS_INSU_TRANSFERED_AGENCY'), document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY'), document.getElementById('lblDESC_INSU_TRANSFERED_AGENCY'));

            //EnableDisableDesc(document.getElementById('cmbHAS_INSU_TRANSFERED_AGENCY'), document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY'), document.getElementById('imgDESC_INSU_TRANSFERED_AGENCY'));
            //EnableDisableDesc(document.getElementById('cmbHAS_INSU_TRANSFERED_AGENCY'), document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY'), document.getElementById('hlkDESC_INSU_TRANSFERED_AGENCY'));

            EnableDisableDesc(document.getElementById('cmbANY_COV_DECLINED_CANCELED'), document.getElementById('txtDESC_COV_DECLINED_CANCELED'), document.getElementById('lblDESC_COV_DECLINED_CANCELED'));
            EnableDisableDesc(document.getElementById('cmbANY_PRIOR_LOSSES'), document.getElementById('txtANY_PRIOR_LOSSES_DESC'), document.getElementById('lblPRIOR_LOSSES'));
            EnableDisableDesc(document.getElementById('cmbANY_FARMING_BUSINESS_COND'), document.getElementById('txtDESC_FARMING_BUSINESS_COND'), document.getElementById('lblDESC_FARMING_BUSINESS_COND'));
            EnableDisableDesc(document.getElementById('cmbANY_FARMING_BUSINESS_COND'), document.getElementById('cmbPROVIDE_HOME_DAY_CARE'), document.getElementById('lblPROVIDE_HOME_DAY_CARE'));

           
            if (document.getElementById("hidCalledFrom").value == 'RENTAL') //Added by Charles on 9-Dec-09 for Itrack 6489			
                //EnableDisableDesc(document.getElementById('cmbANIMALS_EXO_PETS_HISTORY'), document.getElementById('txtBREED'), document.getElementById('lblBREED'));
            //else
                //ShowHideNO_OF_PETS(document.getElementById('cmbANIMALS_EXO_PETS_HISTORY')); //Added by Charles on 9-Dec-09 for Itrack 6489

            //EnableDisableDesc(document.getElementById('cmbMORE_THEN_FIVE_ACRES'),document.getElementById('txtDESC_MORE_THEN_FIVE_ACRES'),document.getElementById('lblDESC_MORE_THEN_FIVE_ACRES'));
            //EnableDisableDesc(document.getElementById('cmbRETROFITTED_FOR_EARTHQUAKE'),document.getElementById('txtDESC_RETRO_FOR_EARTHQUAKE'),document.getElementById('lblDESC_RETRO_FOR_EARTHQUAKE'));
            //EnableDisableDesc(document.getElementById('cmbCONVICTION_DEGREE_IN_PAST'), document.getElementById('txtDESC_CONVICTION_DEGREE_IN_PAST'), document.getElementById('lblDESC_CONVICTION_DEGREE_IN_PAST'));
            //EnableDisableDesc(document.getElementById('cmbMANAGER_ON_PERMISES'),document.getElementById('txtDESC_MANAGER_ON_PERMISES'),document.getElementById('lblDESC_MANAGER_ON_PERMISES'));
            //EnableDisableDesc(document.getElementById('cmbSECURITY_ATTENDENT'),document.getElementById('txtDESC_SECURITY_ATTENDENT'),document.getElementById('lblDESC_SECURITY_ATTENDENT'));
            //EnableDisableDesc(document.getElementById('cmbBUILDING_ENT_LOCKED'),document.getElementById('txtDESC_BUILDING_ENT_LOCKED'),document.getElementById('lblDESC_BUILDING_ENT_LOCKED'));
            //EnableDisableDesc(document.getElementById('cmbANY_UNCORRECT_FIRE_CODE_VIOL'),document.getElementById('txtDESC_UNCORRECT_FIRE_CODE_VIOL'),document.getElementById('lblDESC_UNCORRECT_FIRE_CODE_VIOL'));
            //EnableDisableDesc(document.getElementById('cmbANY_RENOVATION'), document.getElementById('txtDESC_RENOVATION'), document.getElementById('lblDESC_RENOVATION'));
            //EnableDisableDesc(document.getElementById('cmbHOUSE_FOR_SALE'),document.getElementById('txtDESC_HOUSE_FOR_SALE'),document.getElementById('lblDESC_HOUSE_FOR_SALE'));
            //EnableDisableDesc(document.getElementById('cmbANY_NON_RESI_PROPERTY'),document.getElementById('txtDESC_NON_RESI_PROPERTY'),document.getElementById('lblDESC_NON_RESI_PROPERTY'));
            //EnableDisableDesc(document.getElementById('cmbTRAMPOLINE'), document.getElementById('txtDESC_TRAMPOLINE'), document.getElementById('lblDESC_TRAMPOLINE'));
            //EnableDisableDesc(document.getElementById('cmbSTRUCT_ORI_BUILT_FOR'),document.getElementById('txtDESC_STRUCT_ORI_BUILT_FOR'),document.getElementById('lblDESC_STRUCT_ORI_BUILT_FOR'));
            //EnableDisableDesc(document.getElementById('cmbLEAD_PAINT_HAZARD'), document.getElementById('txtDESC_LEAD_PAINT_HAZARD'), document.getElementById('lblDESC_LEAD_PAINT_HAZARD'));
            //EnableDisableDesc(document.getElementById('cmbFUEL_OIL_TANK_PERMISES'),document.getElementById('txtDESC_FUEL_OIL_TANK_PERMISES'),document.getElementById('lblDESC_FUEL_OIL_TANK_PERMISES'));
            //EnableDisableDesc(document.getElementById('cmbRENTERS'), document.getElementById('txtDESC_RENTERS'), document.getElementById('lblDESC_RENTERS'));
            //EnableDisableDesc(document.getElementById('cmbBUILD_UNDER_CON_GEN_CONT'),document.getElementById('txtREMARKS'),document.getElementById('lblREMARK'));
            

            // -----------------------Added by mohit--------------------								
            //EnableDisableDesc(document.getElementById('cmbIS_PROP_NEXT_COMMERICAL'), document.getElementById('txtDESC_PROPERTY'), document.getElementById('lblDESC_PROPERTY'));
            //EnableDisableDesc(document.getElementById('cmbARE_STAIRWAYS_PRESENT'), document.getElementById('txtDESC_STAIRWAYS'), document.getElementById('lblDESC_STAIRWAYS'));
            //EnableDisableDesc(document.getElementById('cmbIS_OWNERS_DWELLING_CHANGED'), document.getElementById('txtDESC_OWNER'), document.getElementById('lblDESC_OWNER'));

            //EnableDisableDesc(document.getElementById('cmbIS_VACENT_OCCUPY'), document.getElementById('txtDESC_VACENT_OCCUPY'), document.getElementById('lblDESC_VACENT_OCCUPY'));

            //EnableDisableDesc(document.getElementById('cmbIS_RENTED_IN_PART'), document.getElementById('txtDESC_RENTED_IN_PART'), document.getElementById('lblDESC_RENTED_IN_PART'));

            //EnableDisableDesc(document.getElementById('cmbIS_DWELLING_OWNED_BY_OTHER'), document.getElementById('txtDESC_DWELLING_OWNED_BY_OTHER'), document.getElementById('lblDESC_DWELLING_OWNED_BY_OTHER'));


            //EnableDisableDesc(document.getElementById('cmbANY_HEATING_SOURCE'), document.getElementById('txtDESC_ANY_HEATING_SOURCE'), document.getElementById('lblDESC_ANY_HEATING_SOURCE'));

            //Added By shafi 23-01-2004			
           // EnableDisableDescOther(document.getElementById('cmbNO_OF_PETS'), document.getElementById('cmbOTHER_DESCRIPTION'), document.getElementById('lblOTHER_DESCRIPTION'));

         //EnableDisableDesc(document.getElementById('cmbMODULAR_MANUFACTURED_HOME'), document.getElementById('cmbBUILT_ON_CONTINUOUS_FOUNDATION'), document.getElementById('lblBUILT_ON_CONTINUOUS_FOUNDATION'));

            //EnableDisableDesc(document.getElementById('cmbIS_SWIMPOLL_HOTTUB'), document.getElementById('txtDESC_IS_SWIMPOLL_HOTTUB'), document.getElementById('lblDESC_IS_SWIMPOLL_HOTTUB'));
            //EnableDisableDesc(document.getElementById('cmbMULTI_POLICY_DISC_APPLIED'), document.getElementById('txtDESC_MULTI_POLICY_DISC_APPLIED'), document.getElementById('lblDESC_MULTI_POLICY_DISC_APPLIED'));
            //EnableDisableDesc(document.getElementById('cmbBUILD_UNDER_CON_GEN_CONT'), document.getElementById('txtDESC_BUILD_UNDER_CON_GEN_CONT'), document.getElementById('lblDESC_BUILD_UNDER_CON_GEN_CONT'));

            //RP
            //EnableDisableDesc(document.getElementById('cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE'), document.getElementById('txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC'), document.getElementById('lblVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC'));

            //Call for rental only---Not in HOME
            if (document.getElementById('cmbPROPERTY_ON_MORE_THAN')) {
                EnableDisableDesc(document.getElementById('cmbPROPERTY_ON_MORE_THAN'), document.getElementById('txtPROPERTY_ON_MORE_THAN_DESC'), document.getElementById('lblPROPERTY_ON_MORE_THAN'));
                EnableDisableDesc(document.getElementById('cmbDWELLING_MOBILE_HOME'), document.getElementById('txtDWELLING_MOBILE_HOME_DESC'), document.getElementById('lblDWELLING_MOBILE_HOME'));
                EnableDisableDesc(document.getElementById('cmbPROPERTY_USED_WHOLE_PART'), document.getElementById('txtPROPERTY_USED_WHOLE_PART_DESC'), document.getElementById('lblPROPERTY_USED_WHOLE_PART'));
            }
            //------------------------End-------------------------------	

            return false;
        }

        function ResetForm1() {
            ResetForm('POL_HOME_OWNER_GEN_INFO');
            //EnableDisableDesc(document.getElementById('cmbANY_FARMING_BUSINESS_COND'),document.getElementById('txtDESC_BUSINESS'),document.getElementById('lblDESC_BUSINESS'));
            //EnableDisableDesc(document.getElementById('cmbANY_RESIDENCE_EMPLOYEE'), document.getElementById('txtDESC_RESIDENCE_EMPLOYEE'), document.getElementById('lblDESC_RESIDENCE_EMPLOYEE'));
            //EnableDisableDesc(document.getElementById('cmbANY_OTHER_RESI_OWNED'), document.getElementById('txtDESC_OTHER_RESIDENCE'), document.getElementById('lblDESC_OTHER_RESIDENCE'));
            //EnableDisableDesc(document.getElementById('cmbANY_OTH_INSU_COMP'), document.getElementById('txtDESC_OTHER_INSURANCE'), document.getElementById('lblDESC_OTHER_INSURANCE'));

            //EnableDisableDesc(document.getElementById('cmbHAS_INSU_TRANSFERED_AGENCY'), document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY'), document.getElementById('lblDESC_INSU_TRANSFERED_AGENCY'));

            //EnableDisableDesc(document.getElementById('cmbHAS_INSU_TRANSFERED_AGENCY'), document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY'), document.getElementById('imgDESC_INSU_TRANSFERED_AGENCY'));
            //EnableDisableDesc(document.getElementById('cmbHAS_INSU_TRANSFERED_AGENCY'), document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY'), document.getElementById('hlkDESC_INSU_TRANSFERED_AGENCY'));

            EnableDisableDesc(document.getElementById('cmbANY_COV_DECLINED_CANCELED'), document.getElementById('txtDESC_COV_DECLINED_CANCELED'), document.getElementById('lblDESC_COV_DECLINED_CANCELED'));
//            if (document.getElementById("hidCalledFrom").value == 'RENTAL') //Added by Charles on 9-Dec-09 for Itrack 6489
                //EnableDisableDesc(document.getElementById('cmbANIMALS_EXO_PETS_HISTORY'), document.getElementById('txtBREED'), document.getElementById('lblBREED'));
            //else
                //ShowHideNO_OF_PETS(document.getElementById('cmbANIMALS_EXO_PETS_HISTORY')); //Added by Charles on 9-Dec-09 for Itrack 6489

            //EnableDisableDesc(document.getElementById('cmbMORE_THEN_FIVE_ACRES'),document.getElementById('txtDESC_MORE_THEN_FIVE_ACRES'),document.getElementById('lblDESC_MORE_THEN_FIVE_ACRES'));
            //EnableDisableDesc(document.getElementById('cmbRETROFITTED_FOR_EARTHQUAKE'),document.getElementById('txtDESC_RETRO_FOR_EARTHQUAKE'),document.getElementById('lblDESC_RETRO_FOR_EARTHQUAKE'));
            //EnableDisableDesc(document.getElementById('cmbCONVICTION_DEGREE_IN_PAST'), document.getElementById('txtDESC_CONVICTION_DEGREE_IN_PAST'), document.getElementById('lblDESC_CONVICTION_DEGREE_IN_PAST'));
            //EnableDisableDesc(document.getElementById('cmbMANAGER_ON_PERMISES'),document.getElementById('txtDESC_MANAGER_ON_PERMISES'),document.getElementById('lblDESC_MANAGER_ON_PERMISES'));
            //EnableDisableDesc(document.getElementById('cmbSECURITY_ATTENDENT'),document.getElementById('txtDESC_SECURITY_ATTENDENT'),document.getElementById('lblDESC_SECURITY_ATTENDENT'));
            //EnableDisableDesc(document.getElementById('cmbBUILDING_ENT_LOCKED'),document.getElementById('txtDESC_BUILDING_ENT_LOCKED'),document.getElementById('lblDESC_BUILDING_ENT_LOCKED'));
            //EnableDisableDesc(document.getElementById('cmbANY_UNCORRECT_FIRE_CODE_VIOL'),document.getElementById('txtDESC_UNCORRECT_FIRE_CODE_VIOL'),document.getElementById('lblDESC_UNCORRECT_FIRE_CODE_VIOL'));
            //EnableDisableDesc(document.getElementById('cmbANY_RENOVATION'), document.getElementById('txtDESC_RENOVATION'), document.getElementById('lblDESC_RENOVATION'));
            //EnableDisableDesc(document.getElementById('cmbHOUSE_FOR_SALE'),document.getElementById('txtDESC_HOUSE_FOR_SALE'),document.getElementById('lblDESC_HOUSE_FOR_SALE'));
            //EnableDisableDesc(document.getElementById('cmbANY_NON_RESI_PROPERTY'),document.getElementById('txtDESC_NON_RESI_PROPERTY'),document.getElementById('lblDESC_NON_RESI_PROPERTY'));
            //EnableDisableDesc(document.getElementById('cmbTRAMPOLINE'), document.getElementById('txtDESC_TRAMPOLINE'), document.getElementById('lblDESC_TRAMPOLINE'));
            //EnableDisableDesc(document.getElementById('cmbSTRUCT_ORI_BUILT_FOR'),document.getElementById('txtDESC_STRUCT_ORI_BUILT_FOR'),document.getElementById('lblDESC_STRUCT_ORI_BUILT_FOR'));
            //EnableDisableDesc(document.getElementById('cmbLEAD_PAINT_HAZARD'), document.getElementById('txtDESC_LEAD_PAINT_HAZARD'), document.getElementById('lblDESC_LEAD_PAINT_HAZARD'));
            //EnableDisableDesc(document.getElementById('cmbFUEL_OIL_TANK_PERMISES'),document.getElementById('txtDESC_FUEL_OIL_TANK_PERMISES'),document.getElementById('lblDESC_FUEL_OIL_TANK_PERMISES'));
            //EnableDisableDesc(document.getElementById('cmbRENTERS'), document.getElementById('txtDESC_RENTERS'), document.getElementById('lblDESC_RENTERS'));

            // -----------------------Added by mohit--------------------								
            //EnableDisableDesc(document.getElementById('cmbIS_PROP_NEXT_COMMERICAL'), document.getElementById('txtDESC_PROPERTY'), document.getElementById('lblDESC_PROPERTY'));
            //EnableDisableDesc(document.getElementById('cmbARE_STAIRWAYS_PRESENT'), document.getElementById('txtDESC_STAIRWAYS'), document.getElementById('lblDESC_STAIRWAYS'));
            //EnableDisableDesc(document.getElementById('cmbIS_OWNERS_DWELLING_CHANGED'), document.getElementById('txtDESC_OWNER'), document.getElementById('lblDESC_OWNER'));
            //EnableDisableDesc(document.getElementById('cmbIS_VACENT_OCCUPY'), document.getElementById('txtDESC_VACENT_OCCUPY'), document.getElementById('lblDESC_VACENT_OCCUPY'));
            //EnableDisableDesc(document.getElementById('cmbIS_RENTED_IN_PART'), document.getElementById('txtDESC_RENTED_IN_PART'), document.getElementById('lblDESC_RENTED_IN_PART'));
            //EnableDisableDesc(document.getElementById('cmbIS_DWELLING_OWNED_BY_OTHER'), document.getElementById('txtDESC_DWELLING_OWNED_BY_OTHER'), document.getElementById('lblDESC_DWELLING_OWNED_BY_OTHER'));
            //EnableDisableDesc(document.getElementById('cmbANY_HEATING_SOURCE'), document.getElementById('txtDESC_ANY_HEATING_SOURCE'), document.getElementById('lblDESC_ANY_HEATING_SOURCE'));
            //Added by Charles on 9-Dec-09 for Itrack 6489	
            if (document.getElementById("hidCalledFrom").value == 'HOME') {
                //document.getElementById('csvOTHER_DESCRIPTION').style.display = "none";
                DelNewTextDescOther(false);
                document.getElementById('hidOTHER_DESCRIPTION_VALUE').value = '';
                //document.getElementById('cmbOTHER_DESCRIPTION').selectedIndex = 0;
                if (document.getElementById('txtOTHER_DESCRIPTION') != null) {
                    document.getElementById('txtOTHER_DESCRIPTION').value = "";
                    document.getElementById('txtOTHER_DESCRIPTION').style.display = "none";
                }
                //document.getElementById('tabOTHER_DESCRIPTION').style.display = "none";
//                if (document.getElementById('lnkOTHER_DESCRIPTION') != null) {
//                    document.getElementById('lnkOTHER_DESCRIPTION').style.display = "none";
                }
                if (document.getElementById('lnkOTHER_DESCRIPTION1') != null) {
                    document.getElementById('lnkOTHER_DESCRIPTION1').style.display = "none";
                }
                document.getElementById('hidOTHER_DESCRIPTION_VALUE').value = "<%=strOTHER_DESCRIPTION_VALUE%>";
            
            //Added till here

            //Added By Shafi 23/01/2006			
            //EnableDisableDescOther(document.getElementById('cmbNO_OF_PETS'), document.getElementById('cmbOTHER_DESCRIPTION'), document.getElementById('lblOTHER_DESCRIPTION'));
            //Added by Sumit for new desciption fields
            //EnableDisableDesc(document.getElementById('cmbIS_SWIMPOLL_HOTTUB'), document.getElementById('txtDESC_IS_SWIMPOLL_HOTTUB'), document.getElementById('lblDESC_IS_SWIMPOLL_HOTTUB'));
            //EnableDisableDesc(document.getElementById('cmbMULTI_POLICY_DISC_APPLIED'), document.getElementById('txtDESC_MULTI_POLICY_DISC_APPLIED'), document.getElementById('lblDESC_MULTI_POLICY_DISC_APPLIED'));
            //EnableDisableDesc(document.getElementById('cmbBUILD_UNDER_CON_GEN_CONT'), document.getElementById('txtDESC_BUILD_UNDER_CON_GEN_CONT'), document.getElementById('lblDESC_BUILD_UNDER_CON_GEN_CONT'));
            //------------------------End-------------------------------	
            EnableDisableDesc(document.getElementById('cmbANY_FARMING_BUSINESS_COND'), document.getElementById('txtDESC_FARMING_BUSINESS_COND'), document.getElementById('lblDESC_FARMING_BUSINESS_COND'));
            EnableDisableDesc(document.getElementById('cmbANY_FARMING_BUSINESS_COND'), document.getElementById('cmbPROVIDE_HOME_DAY_CARE'), document.getElementById('lblPROVIDE_HOME_DAY_CARE'));
            //RP
            //EnableDisableDesc(document.getElementById('cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE'), document.getElementById('txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC'), document.getElementById('lblVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC'));
            //EnableDisableDesc(document.getElementById('cmbMODULAR_MANUFACTURED_HOME'), document.getElementById('cmbBUILT_ON_CONTINUOUS_FOUNDATION'), document.getElementById('lblBUILT_ON_CONTINUOUS_FOUNDATION'));
            //EnableDisableDesc(document.getElementById('cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE'), document.getElementById('txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC'), document.getElementById('lblVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC'));

            //Call for rental only---Not in HOME
            if (document.getElementById('cmbPROPERTY_ON_MORE_THAN')) {
                EnableDisableDesc(document.getElementById('cmbPROPERTY_ON_MORE_THAN'), document.getElementById('txtPROPERTY_ON_MORE_THAN_DESC'), document.getElementById('lblPROPERTY_ON_MORE_THAN'));
                EnableDisableDesc(document.getElementById('cmbDWELLING_MOBILE_HOME'), document.getElementById('txtDWELLING_MOBILE_HOME_DESC'), document.getElementById('lblDWELLING_MOBILE_HOME'));
                EnableDisableDesc(document.getElementById('cmbPROPERTY_USED_WHOLE_PART'), document.getElementById('txtPROPERTY_USED_WHOLE_PART_DESC'), document.getElementById('lblPROPERTY_USED_WHOLE_PART'));
            }

            //return false;
        }
        //Compare years insured
        function CompareAllWolYears(source, arguments) {
            //Check For No of years
//            if (parseInt(document.POL_HOME_OWNER_GEN_INFO.txtYEARS_INSU_WOL.value) > parseInt(document.POL_HOME_OWNER_GEN_INFO.txtYEARS_INSU.value)) {
//                arguments.IsValid = false;
//                return;
//            }
        }

        function ChkTextAreaLength(source, arguments) {

//            var txtArea = arguments.Value;
//            if (txtArea.length > 255) {
//                arguments.IsValid = false;
//                return;   // invalid userName
//            }
        }
        function ChkInspectionDate(objSource, objArgs) {
//            if (document.getElementById("revLAST_INSPECTED_DATE").isvalid == true) {
//                var effdate = document.POL_HOME_OWNER_GEN_INFO.txtLAST_INSPECTED_DATE.value;
//                objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
//            }
//            else
//                objArgs.IsValid = true;

        }

        function ShowHideNO_OF_PETS(cmbCombo)//Function added by Charles on 9-Dec-09 for Itrack 6489
        {
            if (cmbCombo.options[cmbCombo.selectedIndex].text == "Yes") {
                //document.getElementById('trNO_OF_PETS').style.display = "inline";
            }
            else {
                //document.getElementById('cmbNO_OF_PETS').selectedIndex = 0;
                //EnableDisableDescOther(document.getElementById('cmbNO_OF_PETS'), document.getElementById('cmbOTHER_DESCRIPTION'), document.getElementById('lblOTHER_DESCRIPTION'));
                //document.getElementById('trNO_OF_PETS').style.display = "none";
            }
        }

        function SwimmingPool_OnChange() {
            //if ( document.getElementById('cmbSWIMMING_POOL').options.selectedIndex==1)
//            if (document.getElementById('cmbSWIMMING_POOL').options[document.getElementById('cmbSWIMMING_POOL').selectedIndex].value == 1) {
//                document.getElementById('cmbSWIMMING_POOL_TYPE').style.display = "inline";
//                document.getElementById('lblSWIMMING_POOL_TYPE').style.display = "none";
//                document.getElementById('aSwimType').style.display = "inline";
//                document.getElementById('trSwimmingPool1').style.display = "inline";
//                document.getElementById('trSwimmingPool2').style.display = "inline";
//            }
//            else {
//                document.getElementById('cmbSWIMMING_POOL_TYPE').style.display = "none";
//                document.getElementById('lblSWIMMING_POOL_TYPE').style.display = "inline";
//                document.getElementById('lblSWIMMING_POOL_TYPE').innerHTML = "-N.A-";
//                document.getElementById('aSwimType').style.display = "none";
//                document.getElementById('trSwimmingPool1').style.display = "none";
//                document.getElementById('trSwimmingPool2').style.display = "none";

//            }
//            return false;
        }
        //Added By Raghav on 22-07-2008 Itrack Issue #4506		
        function CursorFocus() {
//            if (document.getElementById('cmbIS_VACENT_OCCUPY') != null)
//                document.getElementById('cmbIS_VACENT_OCCUPY').focus();
        }

        function showPageLookupLayer(controlId) {
//            var lookupMessage;
//            switch (controlId) {
//                case "cmbSWIMMING_POOL_TYPE":
//                    lookupMessage = "SPLCD.";
//                    break;
//                default:
//                    lookupMessage = "Look up code not found";
//                    break;

//            }
//            document.getElementById("lookupLayer").style.display = "inline";
//            showLookupLayer(controlId, lookupMessage);
        }

        //Added for Itrack Issue 6640 on 11 Dec 09
        function ShowHideWeather_NonWeather_Claims_Fields() {
            if (document.getElementById("hidCalledFrom").value == "HOME") {
                if (document.getElementById('cmbANY_PRIOR_LOSSES').options.selectedIndex == 2) {
                    document.getElementById("trNON_WEATHER_CLAIMS").style.display = "inline";
                    document.getElementById("trWEATHER_CLAIMS").style.display = "inline";
                    document.getElementById('rfvNON_WEATHER_CLAIMS').setAttribute("enabled", true);
                    document.getElementById('rfvWEATHER_CLAIMS').setAttribute("enabled", true);
                }
                else {
                    document.getElementById("trNON_WEATHER_CLAIMS").style.display = "none";
                    document.getElementById("trWEATHER_CLAIMS").style.display = "none";
                    document.getElementById('rfvNON_WEATHER_CLAIMS').setAttribute("enabled", false);
                    document.getElementById('rfvNON_WEATHER_CLAIMS').style.display = "none";
                    document.getElementById('rfvWEATHER_CLAIMS').setAttribute("enabled", false);
                    document.getElementById('rfvWEATHER_CLAIMS').style.display = "none";
                }
            }
            else {
                document.getElementById("trNON_WEATHER_CLAIMS").style.display = "none";
                document.getElementById("trWEATHER_CLAIMS").style.display = "none";
                document.getElementById('rfvNON_WEATHER_CLAIMS').setAttribute("enabled", false);
                document.getElementById('rfvNON_WEATHER_CLAIMS').style.display = "none";
                document.getElementById('rfvWEATHER_CLAIMS').setAttribute("enabled", false);
                document.getElementById('rfvWEATHER_CLAIMS').style.display = "none";
            }
        }
    </script>
</head>
<body oncontextmenu="return false;" topmargin="0" leftmargin="0" rightmargin="0"
    onload="populateXML();EnableDisableNonSmokerCredit();ApplyColor();ChangeColor();showScroll();ShowHideWeather_NonWeather_Claims_Fields();CursorFocus();">
    <%--Added for Itrack Issue 6640 on 11 Dec 09--%>
    <form id="POL_HOME_OWNER_GEN_INFO" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr id="trError" runat="server">
            <td class="midcolorc">
                <asp:Label ID="lblError" runat="server" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr id="trBody" runat="server">
            <td>
                <table class="tableWidthHeader" id="TABLE1" align="center" border="0">
                    <tbody>
                        <tr>
                            <td class="pageHeader" id="tdWorkflow" colspan="4">
                                <webcontrol:workflow istop="false" id="myWorkFlow" runat="server">
                                </webcontrol:workflow>
                            </td>
                        </tr>
                        <!-- <tr>
								<TD class="pageHeader" id="tdClientTop" colSpan="4"><webcontrol:clienttop id="cltClientTop" runat="server" width="100%"></webcontrol:clienttop><webcontrol:gridspacer id="Gridspacer1" runat="server"></webcontrol:gridspacer></TD>
							</tr>
							<tr>
								<td class="headereffectCenter" colSpan="4">Underwriting Questions</td>
							</tr> -->
                        <tr>
                            <td class="pageHeader" colspan="4">
                                Please note that all fields marked with * are mandatory
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="right" colspan="4">
                                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" rowspan="2" style="vertical-align: middle">
                                <asp:Label ID="capANY_FARMING_BUSINESS_COND" runat="server">Any farming or other business conducted? (Incl. day/child care)</asp:Label><span
                                    class="mandatory" id="spnANY_FARMING_BUSINESS_COND">*</span>
                            </td>
                            <td class="midcolora" rowspan="2" style="vertical-align: middle">
                                <asp:DropDownList ID="cmbANY_FARMING_BUSINESS_COND" onfocus="SelectComboIndex('cmbANY_FARMING_BUSINESS_COND')"
                                    runat="server" onchange="fnHandleFARMING_BUSINESS_COND();">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvANY_FARMING_BUSINESS_COND" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbANY_FARMING_BUSINESS_COND"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora">
                                <asp:Label ID="capDESC_FARMING_BUSINESS_COND" runat="server">Description</asp:Label><span
                                    class="mandatory" id="spnDESC_FARMING_BUSINESS_COND">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:TextBox ID="txtDESC_FARMING_BUSINESS_COND" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_FARMING_BUSINESS_COND" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_FARMING_BUSINESS_COND" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_FARMING_BUSINESS_COND"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora">
                                <asp:Label ID="capPROVIDE_HOME_DAY_CARE" runat="server"></asp:Label><span class="mandatory"
                                    id="spnPROVIDE_HOME_DAY_CARE">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:DropDownList ID="cmbPROVIDE_HOME_DAY_CARE" runat="server">
                                </asp:DropDownList>
                                <asp:Label ID="lblPROVIDE_HOME_DAY_CARE" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" width="32%" nowrap="nowrap">
                                <asp:Label ID="capANY_COV_DECLINED_CANCELED" runat="server">Any coverage declined, cancelled or non-renewed during the last 3 years?</asp:Label><span
                                    class="mandatory" id="spnIANY_COV_DECLINED_CANCELED">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbANY_COV_DECLINED_CANCELED" onfocus="SelectComboIndex('cmbANY_COV_DECLINED_CANCELED')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_COV_DECLINED_CANCELED'),document.getElementById('lblDESC_COV_DECLINED_CANCELED'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvANY_COV_DECLINED_CANCELED" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbANY_COV_DECLINED_CANCELED" DESIGNTIMEDRAGDROP="724"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_COV_DECLINED_CANCELED" runat="server">Description</asp:Label><span
                                    class="mandatory" id="spnDESC_COV_DECLINED_CANCELED">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_COV_DECLINED_CANCELED" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_COV_DECLINED_CANCELED" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_COV_DECLINED_CANCELED" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_COV_DECLINED_CANCELED"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora">
                                <asp:Label ID="capANY_PRIOR_LOSSES" runat="server">ANY_PRIOR_LOSSES</asp:Label><span
                                    class="mandatory">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:DropDownList ID="cmbANY_PRIOR_LOSSES" onfocus="SelectComboIndex('cmbANY_PRIOR_LOSSES')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtANY_PRIOR_LOSSES_DESC'),document.getElementById('lblPRIOR_LOSSES'));ShowHideWeather_NonWeather_Claims_Fields();">
                                </asp:DropDownList>
                                <br>
                                <%--Added for Itrack Issue 6640 on 11 Dec 09--%>
                                <asp:RequiredFieldValidator ID="rfvANY_PRIOR_LOSSES" runat="server" ControlToValidate="cmbANY_PRIOR_LOSSES"
                                    ErrorMessage="ANY_PRIOR_LOSSES can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora">
                                <asp:Label ID="capANY_PRIOR_LOSSES_DESC" runat="server">ANY_PRIOR_LOSSES_DESC</asp:Label><span
                                    class="mandatory" id="spnANY_PRIOR_LOSSES_DESC">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:TextBox ID="txtANY_PRIOR_LOSSES_DESC" runat="server" size="28" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="lblPRIOR_LOSSES" runat="server" CssClass="LabelFont"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfvANY_PRIOR_LOSSES_DESC" runat="server" ControlToValidate="txtANY_PRIOR_LOSSES_DESC"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capIS_VACENT_OCCUPY" runat="server">Is dwelling currently Vacant or Unoccupied?</asp:Label><span
                                    class="mandatory" id="spnIS_VACENT_OCCUPY">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbIS_VACENT_OCCUPY" onfocus="SelectComboIndex('cmbIS_VACENT_OCCUPY')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_VACENT_OCCUPY'),document.getElementById('lblDESC_VACENT_OCCUPY'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvIS_VACENT_OCCUPY" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbIS_VACENT_OCCUPY"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_VACENT_OCCUPY" runat="server">Description</asp:Label><span
                                    class="mandatory" id="spnDESC_VACENT_OCCUPY">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_VACENT_OCCUPY" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_VACENT_OCCUPY" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_VACENT_OCCUPY" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_VACENT_OCCUPY"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capIS_RENTED_IN_PART" runat="server">Is dwelling rented in whole or part to students under age 30?</asp:Label><span
                                    class="mandatory" id="spnIS_RENTED_IN_PART">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbIS_RENTED_IN_PART" onfocus="SelectComboIndex('cmbIS_RENTED_IN_PART')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_RENTED_IN_PART'),document.getElementById('lblDESC_RENTED_IN_PART'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvIS_RENTED_IN_PART" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbIS_RENTED_IN_PART"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_RENTED_IN_PART" runat="server">Description</asp:Label><span
                                    class="mandatory" id="spnDESC_RENTED_IN_PART">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_RENTED_IN_PART" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_RENTED_IN_PART" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_RENTED_IN_PART" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_RENTED_IN_PART"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capIS_DWELLING_OWNED_BY_OTHER" runat="server">Is dwelling owned by anyone other than an individual?</asp:Label><span
                                    class="mandatory" id="spnIS_DWELLING_OWNED_BY_OTHER">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbIS_DWELLING_OWNED_BY_OTHER" onfocus="SelectComboIndex('cmbIS_DWELLING_OWNED_BY_OTHER')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_DWELLING_OWNED_BY_OTHER'),document.getElementById('lblDESC_DWELLING_OWNED_BY_OTHER'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvIS_DWELLING_OWNED_BY_OTHER" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbIS_DWELLING_OWNED_BY_OTHER"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_DWELLING_OWNED_BY_OTHER" runat="server">Description</asp:Label><span
                                    class="mandatory" id="spnDESC_DWELLING_OWNED_BY_OTHER">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_DWELLING_OWNED_BY_OTHER" runat="server" MaxLength="300"
                                    size="30"></asp:TextBox><asp:Label ID="lblDESC_DWELLING_OWNED_BY_OTHER" runat="server"
                                        CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_DWELLING_OWNED_BY_OTHER" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_DWELLING_OWNED_BY_OTHER"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr runat ="server" visible ="false">
                            <td class="midcolora">
                                <asp:Label ID="capMODULAR_MANUFACTURED_HOME" runat="server"></asp:Label><span class="mandatory"
                                    id="spnMODULAR_MANUFACTURED_HOME">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:DropDownList ID="cmbMODULAR_MANUFACTURED_HOME" onchange="javascript:EnableDisableDesc(this,document.getElementById('cmbBUILT_ON_CONTINUOUS_FOUNDATION'),document.getElementById('lblBUILT_ON_CONTINUOUS_FOUNDATION'));"
                                    runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="midcolora">
                                <asp:Label ID="capBUILT_ON_CONTINUOUS_FOUNDATION" runat="server">Other Description</asp:Label><span
                                    class="mandatory" id="spnBUILT_ON_CONTINUOUS_FOUNDATION">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:DropDownList ID="cmbBUILT_ON_CONTINUOUS_FOUNDATION" onfocus="SelectComboIndex('cmbBUILT_ON_CONTINUOUS_FOUNDATION')"
                                    runat="server">
                                </asp:DropDownList>
                                <asp:Label ID="lblBUILT_ON_CONTINUOUS_FOUNDATION" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capIS_PROP_NEXT_COMMERICAL" runat="server">Is property located next to a commercial building?</asp:Label><span
                                    class="mandatory" id="spnIS_PROP_NEXT_COMMERICAL">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbIS_PROP_NEXT_COMMERICAL" onfocus="SelectComboIndex('cmbIS_PROP_NEXT_COMMERICAL')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_PROPERTY'),document.getElementById('lblDESC_PROPERTY'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvIS_PROP_NEXT_COMMERICAL" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbIS_PROP_NEXT_COMMERICAL"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_PROPERTY" runat="server">Description Property</asp:Label><span
                                    class="mandatory" id="spnDESC_PROPERTY">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_PROPERTY" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_PROPERTY" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_PROPERTY" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_PROPERTY"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capARE_STAIRWAYS_PRESENT" runat="server">Are any outside stairways present?</asp:Label><span
                                    class="mandatory" id="spnARE_STAIRWAYS_PRESENT">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbARE_STAIRWAYS_PRESENT" onfocus="SelectComboIndex('cmbARE_STAIRWAYS_PRESENT')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_STAIRWAYS'),document.getElementById('lblDESC_STAIRWAYS'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvARE_STAIRWAYS_PRESENT" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbARE_STAIRWAYS_PRESENT"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_STAIRWAYS" runat="server">Description Stairways</asp:Label><span
                                    class="mandatory" id="spnDESC_STAIRWAYS">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_STAIRWAYS" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_STAIRWAYS" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_STAIRWAYS" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_STAIRWAYS"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capANIMALS_EXO_PETS_HISTORY" runat="server">Animals or exotic pets? (Breed & bite history)</asp:Label><span
                                    class="mandatory" id="spnANIMALS_EXO_PETS_HISTORY">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbANIMALS_EXO_PETS_HISTORY" onfocus="SelectComboIndex('cmbANIMALS_EXO_PETS_HISTORY')"
                                    runat="server" onchange="javascript:if( document.getElementById('hidCalledFrom').value=='RENTAL') { EnableDisableDesc(this,document.getElementById('txtBREED'),document.getElementById('lblBREED'));} else { ShowHideNO_OF_PETS(this); }">
                                </asp:DropDownList>
                                <br>
                                <!-- Changed onchange event, Charles (9-Dec-09), Itrack 6489 -->
                                <asp:RequiredFieldValidator ID="rfvANIMALS_EXO_PETS_HISTORY" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbANIMALS_EXO_PETS_HISTORY"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capBREED" runat="server">Description</asp:Label><span class="mandatory"
                                    id="spnBREED">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtBREED" runat="server" MaxLength="100" size="30"></asp:TextBox><asp:Label
                                    ID="lblBREED" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvBREED" runat="server" Display="Dynamic" ControlToValidate="txtBREED"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capNO_OF_PETS" runat="server"></asp:Label><span class="mandatory"
                                    id="spnNO_OF_PETS">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbNO_OF_PETS" runat="server" onchange="javascript:EnableDisableDescOther(this,document.getElementById('cmbOTHER_DESCRIPTION'),document.getElementById('lblOTHER_DESCRIPTION'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvNO_OF_PETS" runat="server" Display="Dynamic" ControlToValidate="cmbNO_OF_PETS"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2" class="midcolora">
                                <table border="0" width="100%">
                                    <tr>
                                        <td class="midcolora">
                                            <asp:Label ID="capOTHER_DESCRIPTION" runat="server">Other Description</asp:Label><span
                                                class="mandatory" id="spnOTHER_DESCRIPTION">*</span>
                                        </td>
                                        <td class="midcolora">
                                            <asp:Label ID="lblOTHER_DESCRIPTION" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
                                            <table id="tabOTHER_DESCRIPTION" border="0">
                                                <tr>
                                                    <td class="midcolora">
                                                        <select id="cmbOTHER_DESCRIPTION" runat="server" onchange="javascript: if(document.getElementById('hidCalledFrom').value=='HOME') { EnableDisableTextDescOther(this); }">
                                                        </select>
                                                    </td>
                                                    <td class="midcolora" width="25%">
                                                        <input id="txtOTHER_DESCRIPTION" type="text" value="" runat="server" maxlength="2"
                                                            size="2" style="display: none" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br>
                                            <a id="lnkOTHER_DESCRIPTION" runat="server" style="display: none" href="javascript:AddNewTextDescOther();">
                                                More</a> <a id="lnkOTHER_DESCRIPTION1" runat="server" style="display: none" href="javascript:DelNewTextDescOther(true)">
                                                    Remove</a>
                                            <asp:RequiredFieldValidator ID="rfvOTHER_DESCRIPTION" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbOTHER_DESCRIPTION"></asp:RequiredFieldValidator>
                                            <br>
                                            <span id="csvOTHER_DESCRIPTION" style="font-size: 8pt; color: red; font-family: Verdana,'MS Sans Serif',Arial;
                                                text-align: left; display: none" runat="server"></span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <!-- Moved here, added id,Charles 8-Dec-09, Itrack 6489 -->
                        <tr id="trIsAny_Horse" runat="server" visible ="false">
                            <td class="midcolora">
                                <asp:Label ID="capIsAny_Horse" runat="server"></asp:Label><span class="mandatory"
                                    id="spnIsAny_Horse">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:DropDownList ID="cmbIsAny_Horse" onfocus="SelectComboIndex('cmbIsAny_Horse')"
                                    runat="server" onChange="IsAny_Horse_OnChange();">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvIsAny_Horse" ControlToValidate="cmbIsAny_Horse"
                                    Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora">
                                <asp:Label ID="capNo_Horses" runat="server"></asp:Label><span class="mandatory" id="spnNo_Horses">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:TextBox ID="txtNo_Horses" runat="server" size="5" MaxLength="4"></asp:TextBox><br>
                                <asp:RequiredFieldValidator ID="rfvNo_Horses" ControlToValidate="txtNo_Horses" Display="Dynamic"
                                    runat="server"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revNo_Horses" ControlToValidate="txtNo_Horses"
                                    Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <!-- Moved till here -->
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capIS_SWIMPOLL_HOTTUB" runat="server">Is there an enclosed swimming pool or hot tub (not including bathroom type Jacuzzi or hot tub)?</asp:Label><span
                                    class="mandatory" id="spnIS_SWIMPOLL_HOTTUB">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbIS_SWIMPOLL_HOTTUB" onfocus="SelectComboIndex('cmbIS_SWIMPOLL_HOTTUB')"
                                    onchange="javascript:EnableDisableDesc(document.getElementById('cmbIS_SWIMPOLL_HOTTUB'),document.getElementById('txtDESC_IS_SWIMPOLL_HOTTUB'),document.getElementById('lblDESC_IS_SWIMPOLL_HOTTUB'));"
                                    runat="server">
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvIS_SWIMPOLL_HOTTUB" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbIS_SWIMPOLL_HOTTUB"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_IS_SWIMPOLL_HOTTUB" runat="server">Description</asp:Label><span
                                    class="mandatory" id="spnDESC_IS_SWIMPOLL_HOTTUB">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_IS_SWIMPOLL_HOTTUB" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_IS_SWIMPOLL_HOTTUB" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_IS_SWIMPOLL_HOTTUB" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_IS_SWIMPOLL_HOTTUB"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora">
                                <asp:Label ID="capVALUED_CUSTOMER_DISCOUNT_OVERRIDE" runat="server">Valued Customer Discount Override</asp:Label><span
                                    class="mandatory" id="spnVALUED_CUSTOMER_DISCOUNT_OVERRIDE">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:DropDownList ID="cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE" onfocus="SelectComboIndex('cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE')"
                                    onchange="javascript:EnableDisableDesc(document.getElementById('cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE'),document.getElementById('txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC'),document.getElementById('lblVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC'));"
                                    runat="server">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvVALUED_CUSTOMER_DISCOUNT_OVERRIDE" runat="server"
                                    Display="Dynamic" ControlToValidate="cmbVALUED_CUSTOMER_DISCOUNT_OVERRIDE"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora">
                                <asp:Label ID="capVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC" runat="server">Valued Customer Discount Override Description</asp:Label><span
                                    class="mandatory" id="spnVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:TextBox ID="txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC" runat="server" MaxLength="300"
                                    size="30"></asp:TextBox><asp:Label ID="lblVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC"
                                        runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC" runat="server"
                                    Display="Dynamic" ControlToValidate="txtVALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capHAS_INSU_TRANSFERED_AGENCY" runat="server">Has insurance been transferred within the agency?</asp:Label><span
                                    class="mandatory" id="spn">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbHAS_INSU_TRANSFERED_AGENCY" onfocus="SelectComboIndex('cmbHAS_INSU_TRANSFERED_AGENCY')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY'),document.getElementById('lblDESC_INSU_TRANSFERED_AGENCY'));EnableDisableDesc(this,document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY'),document.getElementById('imgDESC_INSU_TRANSFERED_AGENCY'));EnableDisableDesc(this,document.getElementById('txtDESC_INSU_TRANSFERED_AGENCY'),document.getElementById('hlkDESC_INSU_TRANSFERED_AGENCY'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvHAS_INSU_TRANSFERED_AGENCY" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbHAS_INSU_TRANSFERED_AGENCY"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_INSU_TRANSFERED_AGENCY" runat="server">Description</asp:Label><span
                                    class="mandatory" id="spnDESC_INSU_TRANSFERED_AGENCY">*</span>
                            </td>
                            <td class="midcolora">
                                <asp:TextBox ID="txtDESC_INSU_TRANSFERED_AGENCY" runat="server" MaxLength="10" size="13"
                                    onblur="FormatDate();"></asp:TextBox><asp:HyperLink ID="hlkDESC_INSU_TRANSFERED_AGENCY"
                                        runat="server" CssClass="HotSpot">
                                        <asp:Image ID="imgDESC_INSU_TRANSFERED_AGENCY" runat="server" ImageUrl="../../../cmsweb/Images/CalendarPicker.gif">
                                        </asp:Image>
                                    </asp:HyperLink><asp:Label ID="lblDESC_INSU_TRANSFERED_AGENCY" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_INSU_TRANSFERED_AGENCY" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_INSU_TRANSFERED_AGENCY"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regDESC_INSU_TRANSFERED_AGENCY" Display="Dynamic"
                                    runat="server" ErrorMessage="Date should be in 'mm/dd/yyyy' format." ControlToValidate="txtDESC_INSU_TRANSFERED_AGENCY"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capLAST_INSPECTED_DATE" runat="server">Last Inspected date</asp:Label>
                            </td>
                            <td class="midcolora" width="18%" colspan="3">
                                <asp:TextBox ID="txtLAST_INSPECTED_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox><asp:HyperLink
                                    ID="hlkLAST_INSPECTED_DATE" runat="server" CssClass="HotSpot">
                                    <asp:Image ID="imgLAST_INSPECTED_DATE" runat="server" ImageUrl="../../../cmsweb/Images/CalendarPicker.gif">
                                    </asp:Image>
                                </asp:HyperLink><br>
                                <asp:RegularExpressionValidator ID="revLAST_INSPECTED_DATE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtLAST_INSPECTED_DATE"></asp:RegularExpressionValidator><asp:CustomValidator
                                        ID="csvLAST_INSPECTED_DATE" Display="Dynamic" ControlToValidate="txtLAST_INSPECTED_DATE"
                                        runat="server" ClientValidationFunction="ChkInspectionDate"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capIS_OWNERS_DWELLING_CHANGED" runat="server">Has ownership of this dwelling changed more than one time in the last 3 years?</asp:Label><span
                                    class="mandatory" id="spnIS_OWNERS_DWELLING_CHANGED">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbIS_OWNERS_DWELLING_CHANGED" runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_OWNER'),document.getElementById('lblDESC_OWNER'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvIS_OWNERS_DWELLING_CHANGED" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbIS_OWNERS_DWELLING_CHANGED"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_OWNER" runat="server">Description ownership</asp:Label><span
                                    class="mandatory" id="spnDESC_OWNER">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_OWNER" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_OWNER" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_OWNER" runat="server" Display="Dynamic" ControlToValidate="txtDESC_OWNER"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capCONVICTION_DEGREE_IN_PAST" runat="server">Conviction of any degree of arson in past 5 (10 RI)?</asp:Label><span
                                    class="mandatory" id="spnCONVICTION_DEGREE_IN_PAST">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbCONVICTION_DEGREE_IN_PAST" onfocus="SelectComboIndex('cmbCONVICTION_DEGREE_IN_PAST')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_CONVICTION_DEGREE_IN_PAST'),document.getElementById('lblDESC_CONVICTION_DEGREE_IN_PAST'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvCONVICTION_DEGREE_IN_PAST" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbCONVICTION_DEGREE_IN_PAST"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_CONVICTION_DEGREE_IN_PAST" runat="server">Description of conviction</asp:Label><span
                                    class="mandatory" id="spnDESC_CONVICTION_DEGREE_IN_PAST">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_CONVICTION_DEGREE_IN_PAST" runat="server" MaxLength="300"
                                    size="30"></asp:TextBox><asp:Label ID="lblDESC_CONVICTION_DEGREE_IN_PAST" runat="server"
                                        CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_CONVICTION_DEGREE_IN_PAST" runat="server"
                                    Display="Dynamic" ControlToValidate="txtDESC_CONVICTION_DEGREE_IN_PAST"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capLEAD_PAINT_HAZARD" runat="server">Any lead paint hazard?</asp:Label><span
                                    class="mandatory" id="spnLEAD_PAINT_HAZARD">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbLEAD_PAINT_HAZARD" onfocus="SelectComboIndex('cmbLEAD_PAINT_HAZARD')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_LEAD_PAINT_HAZARD'),document.getElementById('lblDESC_LEAD_PAINT_HAZARD'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvLEAD_PAINT_HAZARD" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbLEAD_PAINT_HAZARD"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_LEAD_PAINT_HAZARD" runat="server">Description of lead paint hazard</asp:Label><span
                                    class="mandatory" id="spnDESC_LEAD_PAINT_HAZARD">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_LEAD_PAINT_HAZARD" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_LEAD_PAINT_HAZARD" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_LEAD_PAINT_HAZARD" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_LEAD_PAINT_HAZARD"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capMULTI_POLICY_DISC_APPLIED" runat="server"></asp:Label><span class="mandatory">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbMULTI_POLICY_DISC_APPLIED" onfocus="SelectComboIndex('cmbMULTI_POLICY_DISC_APPLIED')"
                                    onchange="javascript:EnableDisableDesc(document.getElementById('cmbMULTI_POLICY_DISC_APPLIED'),document.getElementById('txtDESC_MULTI_POLICY_DISC_APPLIED'),document.getElementById('lblDESC_MULTI_POLICY_DISC_APPLIED'));"
                                    runat="server">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvMULTI_POLICY_DISC_APPLIED" Display="Dynamic" ControlToValidate="cmbMULTI_POLICY_DISC_APPLIED"
                                    runat="server"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_MULTI_POLICY_DISC_APPLIED" runat="server"></asp:Label><span
                                    class="mandatory" id="spnDESC_MULTI_POLICY_DISC_APPLIED">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_MULTI_POLICY_DISC_APPLIED" runat="server" MaxLength="300"
                                    size="30"></asp:TextBox><asp:Label ID="lblDESC_MULTI_POLICY_DISC_APPLIED" runat="server"
                                        CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_MULTI_POLICY_DISC_APPLIED" runat="server"
                                    Display="Dynamic" ControlToValidate="txtDESC_MULTI_POLICY_DISC_APPLIED"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capANY_RESIDENCE_EMPLOYEE" runat="server">Any residence employees?</asp:Label><span
                                    class="mandatory" id="spnANY_RESIDENCE_EMPLOYEE">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbANY_RESIDENCE_EMPLOYEE" onfocus="SelectComboIndex('cmbANY_RESIDENCE_EMPLOYEE')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_RESIDENCE_EMPLOYEE'),document.getElementById('lblDESC_RESIDENCE_EMPLOYEE'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvANY_RESIDENCE_EMPLOYEE" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbANY_RESIDENCE_EMPLOYEE"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_RESIDENCE_EMPLOYEE" runat="server">Number of resident employee</asp:Label><span
                                    class="mandatory" id="spnDESC_RESIDENCE_EMPLOYEE">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_RESIDENCE_EMPLOYEE" runat="server" MaxLength="300" size="6"></asp:TextBox><asp:Label
                                    ID="lblDESC_RESIDENCE_EMPLOYEE" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RegularExpressionValidator ID="revDESC_RESIDENCE_EMPLOYEE" Display="Dynamic"
                                    ControlToValidate="txtDESC_RESIDENCE_EMPLOYEE" runat="server"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvDESC_RESIDENCE_EMPLOYEE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_RESIDENCE_EMPLOYEE"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capANY_OTHER_RESI_OWNED" runat="server">Any other residence owned, occupied or rented?</asp:Label><span
                                    class="mandatory" id="spnANY_OTHER_RESI_OWNED">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbANY_OTHER_RESI_OWNED" onfocus="SelectComboIndex('cmbANY_OTHER_RESI_OWNED')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_OTHER_RESIDENCE'),document.getElementById('lblDESC_OTHER_RESIDENCE'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvANY_OTHER_RESI_OWNED" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbANY_OTHER_RESI_OWNED"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_OTHER_RESIDENCE" runat="server">Description other residence</asp:Label><span
                                    class="mandatory" id="spnDESC_OTHER_RESIDENCE">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_OTHER_RESIDENCE" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_OTHER_RESIDENCE" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_OTHER_RESIDENCE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_OTHER_RESIDENCE"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capANY_OTH_INSU_COMP" runat="server">Any other insurance with this company? </asp:Label><span
                                    class="mandatory" id="spnANY_OTH_INSU_COMP">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbANY_OTH_INSU_COMP" onfocus="SelectComboIndex('cmbANY_OTH_INSU_COMP')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_OTHER_INSURANCE'),document.getElementById('lblDESC_OTHER_INSURANCE'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvANY_OTH_INSU_COMP" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbANY_OTH_INSU_COMP"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_OTHER_INSURANCE" runat="server">Description other insurance(List policy #s)</asp:Label><span
                                    class="mandatory" id="spnDESC_OTHER_INSURANCE">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_OTHER_INSURANCE" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_OTHER_INSURANCE" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_OTHER_INSURANCE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_OTHER_INSURANCE"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capANY_RENOVATION" runat="server">Bldg. undergoing renovation? (Est. completion date and value)</asp:Label><span
                                    class="mandatory" id="spnANY_RENOVATION">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbANY_RENOVATION" onfocus="SelectComboIndex('cmbANY_RENOVATION')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_RENOVATION'),document.getElementById('lblDESC_RENOVATION'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvANY_RENOVATION" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbANY_RENOVATION"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_RENOVATION" runat="server">Est. completion date &amp; value, if undergoing renovation</asp:Label><span
                                    class="mandatory" id="spnDESC_RENOVATION">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_RENOVATION" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_RENOVATION" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_RENOVATION" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_RENOVATION"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capTRAMPOLINE" runat="server">Trampoline?</asp:Label><span class="mandatory"
                                    id="spnTRAMPOLINE">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbTRAMPOLINE" onfocus="SelectComboIndex('cmbTRAMPOLINE')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_TRAMPOLINE'),document.getElementById('lblDESC_TRAMPOLINE'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvTRAMPOLINE" runat="server" Display="Dynamic" ControlToValidate="cmbTRAMPOLINE"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_TRAMPOLINE" runat="server">Description of trampoline</asp:Label><span
                                    class="mandatory" id="spnDESC_TRAMPOLINE">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_TRAMPOLINE" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_TRAMPOLINE" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_TRAMPOLINE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_TRAMPOLINE"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capRENTERS" runat="server">Renters?</asp:Label><span class="mandatory"
                                    id="spnRENTERS">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbRENTERS" onfocus="SelectComboIndex('cmbRENTERS')" runat="server"
                                    onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_RENTERS'),document.getElementById('lblDESC_RENTERS'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvRENTERS" runat="server" Display="Dynamic" ControlToValidate="cmbRENTERS"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_RENTERS" runat="server">If renters, then give description.</asp:Label><span
                                    class="mandatory" id="spnDESC_RENTERS">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_RENTERS" runat="server" MaxLength="3" size="3"></asp:TextBox><asp:Label
                                    ID="lblDESC_RENTERS" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_RENTERS" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_RENTERS"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revDESC_RENTERS" runat="server" ControlToValidate="txtDESC_RENTERS"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capANY_HEATING_SOURCE" runat="server">Heating Source</asp:Label><span
                                    class="mandatory" id="spnANY_HEATING_SOURCE">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbANY_HEATING_SOURCE" onfocus="SelectComboIndex('cmbANY_HEATING_SOURCE')"
                                    runat="server" onchange="javascript:EnableDisableDesc(this,document.getElementById('txtDESC_ANY_HEATING_SOURCE'),&#13;&#10;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;document.getElementById('lblDESC_ANY_HEATING_SOURCE'));">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvANY_HEATING_SOURCE" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbANY_HEATING_SOURCE"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_ANY_HEATING_SOURCE" runat="server">Wood Stove Supplement</asp:Label><span
                                    class="mandatory" id="spnDESC_ANY_HEATING_SOURCE">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_ANY_HEATING_SOURCE" runat="server" MaxLength="300" size="30"></asp:TextBox><asp:Label
                                    ID="lblDESC_ANY_HEATING_SOURCE" runat="server" CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_ANY_HEATING_SOURCE" runat="server" Display="Dynamic"
                                    ControlToValidate="txtDESC_ANY_HEATING_SOURCE"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capBUILD_UNDER_CON_GEN_CONT" runat="server">If Building is Under Construction, is the Applicant the General Contractor?</asp:Label><span
                                    class="mandatory" id="spnBUILD_UNDER_CON_GEN_CONT">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbBUILD_UNDER_CON_GEN_CONT" onfocus="SelectComboIndex('cmbBUILD_UNDER_CON_GEN_CONT')"
                                    onchange="javascript:EnableDisableDesc(document.getElementById('cmbBUILD_UNDER_CON_GEN_CONT'),document.getElementById('txtDESC_BUILD_UNDER_CON_GEN_CONT'),document.getElementById('lblDESC_BUILD_UNDER_CON_GEN_CONT'));"
                                    runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBUILD_UNDER_CON_GEN_CONT" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbBUILD_UNDER_CON_GEN_CONT"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capDESC_BUILD_UNDER_CON_GEN_CONT" runat="server">Wood Stove Supplement</asp:Label><span
                                    class="mandatory" id="spnDESC_BUILD_UNDER_CON_GEN_CONT">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:TextBox ID="txtDESC_BUILD_UNDER_CON_GEN_CONT" runat="server" MaxLength="300"
                                    size="30"></asp:TextBox><asp:Label ID="lblDESC_BUILD_UNDER_CON_GEN_CONT" runat="server"
                                        CssClass="LabelFont">-N.A.-</asp:Label><br>
                                <asp:RequiredFieldValidator ID="rfvDESC_BUILD_UNDER_CON_GEN_CONT" runat="server"
                                    Display="Dynamic" ControlToValidate="txtDESC_BUILD_UNDER_CON_GEN_CONT"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trNON_SMOKER_CREDIT" runat ="server"  visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capNON_SMOKER_CREDIT" runat="server">Non smoker credit</asp:Label><span
                                    class="mandatory" id="spnNON_SMOKER_CREDIT">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbNON_SMOKER_CREDIT" onfocus="SelectComboIndex('cmbNON_SMOKER_CREDIT')"
                                    runat="server">
                                </asp:DropDownList>
                                <br>
                                <asp:RequiredFieldValidator ID="rfvNON_SMOKER_CREDIT" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbNON_SMOKER_CREDIT"></asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2" class="midcolora">
                            </td>
                        </tr>
                        <tr runat ="server" visible ="false">
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capSWIMMING_POOL" runat="server">Swimming Pool</asp:Label><span class="mandatory"
                                    id="spnSWIMMING_POOL">*</span>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:DropDownList ID="cmbSWIMMING_POOL" onfocus="SelectComboIndex('cmbSWIMMING_POOL')"
                                    runat="server" onChange="SwimmingPool_OnChange();">
                                    <asp:ListItem Value='1'>Yes</asp:ListItem>
                                    <asp:ListItem Value='0'>No</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSWIMMING_POOL" runat="server" Display="Dynamic"
                                    ControlToValidate="cmbSWIMMING_POOL"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:Label ID="capSWIMMING_POOL_TYPE" runat="server">Type</asp:Label>
                            </td>
                            <td class="midcolora" width="18%">
                                <asp:Label ID="lblSWIMMING_POOL_TYPE" runat="server" CssClass="LabelFont">-N.A.-</asp:Label>
                                <asp:DropDownList ID="cmbSWIMMING_POOL_TYPE" runat="server">
                                </asp:DropDownList>
                                <a class="calcolora" href="javascript:showPageLookupLayer('cmbSWIMMING_POOL_TYPE')">
                                    <img src="/cms/cmsweb/images/info.gif" id="aSwimType" width="17" height="16" border="0"></a>
                            </td>
                        </tr>
                        <tr id="trSwimmingPool1" runat ="server" visible="false" >
                            <td class="midcolora">
                                <asp:Label ID="capAPPROVED_FENCE" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora">
                                <asp:DropDownList ID="cmbAPPROVED_FENCE" onfocus="SelectComboIndex('cmbAPPROVED_FENCE')"
                                    runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="midcolora">
                                <asp:Label ID="capDIVING_BOARD" runat="server"></asp:Label>
                            </td>
                            <td class="midcolora">
                                <asp:DropDownList ID="cmbDIVING_BOARD" onfocus="SelectComboIndex('cmbDIVING_BOARD')"
                                    runat="server">
                                </asp:DropDownList>
                            </td>
                            </tr>
            </td>
        </tr>
        <tr id="trSwimmingPool2" runat ="server" visible ="false">
            <td class="midcolora">
                <asp:Label ID="capSLIDE" runat="server"></asp:Label>
            </td>
            <td class="midcolora">
                <asp:DropDownList ID="cmbSLIDE" onfocus="SelectComboIndex('cmbSLIDE')" runat="server">
                </asp:DropDownList>
            </td>
            <td class="midcolora" colspan="2">
            </td>
        </tr>
        <%--// Added by Swastika on 29th Mar'06 for Pol Iss # 198--%>
        <tr runat ="server" visible ="false" >
            <td class="midcolora" width="32%">
                <asp:Label ID="capYEARS_INSU" runat="server">Years Continuously Insured</asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:TextBox ID="txtYEARS_INSU" runat="server" size="3" MaxLength="3"></asp:TextBox><br>
                <asp:RegularExpressionValidator Enabled="False" ID="revYEARS_INSU" runat="server"
                    ControlToValidate="txtYEARS_INSU" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:RangeValidator ID="rngYEARS_INSU" runat="server" Display="Dynamic" ControlToValidate="txtYEARS_INSU"
                    MaximumValue="999" MinimumValue="0" Type="Integer"></asp:RangeValidator>
            </td>
            <td class="midcolora" width="32%">
                <asp:Label ID="capYEARS_INSU_WOL" runat="server"></asp:Label>
            </td>
            <td class="midcolora" width="18%">
                <asp:TextBox ID="txtYEARS_INSU_WOL" runat="server" size="3" MaxLength="3"></asp:TextBox><br>
                <asp:RegularExpressionValidator Enabled="False" ID="revYEARS_INSU_WOL" runat="server"
                    ControlToValidate="txtYEARS_INSU_WOL" Display="Dynamic"></asp:RegularExpressionValidator>
                <asp:CustomValidator ID="csvYEARS_INSU_WOL" ControlToValidate="txtYEARS_INSU_WOL"
                    Display="Dynamic" ClientValidationFunction="CompareAllWolYears" runat="server"></asp:CustomValidator>
                <asp:RangeValidator ID="rngYEARS_INSU_WOL" runat="server" Display="Dynamic" ControlToValidate="txtYEARS_INSU_WOL"
                    MaximumValue="999" MinimumValue="0" Type="Integer"></asp:RangeValidator>
            </td>
        </tr>
        <!--Any farming control coming up-->
        <asp:Panel ID="isForming" runat="server" BorderStyle="0">
            <tr runat ="server" visible ="false">
                <td class="midcolora">
                    <asp:Label ID="capAny_Forming" runat="server"></asp:Label><span class="mandatory"
                        id="spnAny_Forming">*</span>
                </td>
                <td class="midcolora">
                    <asp:DropDownList ID="cmbAny_Forming" onfocus="SelectComboIndex('cmbAny_Forming')"
                        runat="server" onChange="cmbAny_Forming_OnChange();">
                    </asp:DropDownList>
                    <br>
                    <asp:RequiredFieldValidator ID="rfvAny_Forming" ControlToValidate="cmbAny_Forming"
                        Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                </td>
                <td class="midcolora">
                </td>
                <td class="midcolora">
                </td>
            </tr>
            <tr id="rowPremises" runat="server">
                <td class="midcolora">
                    <asp:Label ID="capPremises" runat="server"></asp:Label><span class="mandatory" id="spnPremises">*</span>
                </td>
                <td class="midcolora">
                    <asp:DropDownList ID="cmbPremises" onfocus="SelectComboIndex('cmbPremises')" runat="server"
                        onChange="Premises_OnChange();">
                    </asp:DropDownList>
                    <br>
                    <asp:RequiredFieldValidator ID="rfvPremises" ControlToValidate="cmbPremises" Display="Dynamic"
                        runat="server"></asp:RequiredFieldValidator>
                </td>
                <td class="midcolora">
                    <asp:Label ID="capOf_Acres" runat="server"></asp:Label><span class="mandatory" id="spnOf_Acres">*</span>
                </td>
                <td class="midcolora">
                    <asp:TextBox ID="txtOf_Acres" runat="server" size="10" MaxLength="4"></asp:TextBox><br>
                    <asp:RequiredFieldValidator ID="rfvOf_Acres" ControlToValidate="txtOf_Acres" Display="Dynamic"
                        runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revOf_Acres" ControlToValidate="txtOf_Acres"
                        Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="rowHorse" runat="server">
                <td class="midcolora">
                    <asp:Label ID="capLocation" runat="server"></asp:Label><span class="mandatory" id="spnLocation">*</span>
                </td>
                <td class="midcolora">
                    <asp:TextBox ID="txtLocation" size="1" MaxLength="1" runat="server"></asp:TextBox><br>
                    <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="txtLocation" Display="Dynamic"
                        runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revLocation" ControlToValidate="txtLocation"
                        Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                </td>
                <td class="midcolora">
                    <%--<asp:Label id="capOf_Acres_P" Runat="server"></asp:Label><SPAN class="mandatory" id="spnOf_Acres_P">*</SPAN>--%>
                    <asp:Label ID="capDESC_Location" runat="server"></asp:Label><span class="mandatory"
                        id="spnDESC_Location">*</span>
                </td>
                <td class="midcolora">
                    <%--<asp:textbox id="txtOf_Acres_P" runat="server" size="10" MaxLength="4"></asp:textbox>--%>
                    <asp:TextBox onkeypress="MaxLength(this,255);" ID="txtDESC_Location" runat="server"
                        MaxLength="255" TextMode="MultiLine" Columns="28" Rows="4"></asp:TextBox><br>
                    <asp:CustomValidator ID="csvDESC_Location" ControlToValidate="txtDESC_Location" Display="Dynamic"
                        runat="server" ClientValidationFunction="ValidateLength" ErrorMessage="Error"></asp:CustomValidator>
                    <asp:RequiredFieldValidator ID="rfvDESC_Location" ControlToValidate="txtDESC_Location"
                        Display="Dynamic" runat="server"></asp:RequiredFieldValidator><%--<asp:RequiredFieldValidator id="rfvOf_Acres_P" ControlToValidate="txtOf_Acres_P" Display="Dynamic" Runat="server"></asp:RequiredFieldValidator>
											<asp:regularexpressionvalidator id="revOf_Acres_P" ControlToValidate="txtOf_Acres_P" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>--%>
                </td>
            </tr>
            <!--<TR id="rowhorsetext" runat="server"><TD class="midcolora" colspan="4"></TD></TR>-->
        </asp:Panel>
        <!--any farming ends here--->
        <tr runat ="server" visible ="false">
            <td class="midcolora" colspan="4" valign="top">
                <table border="0" width="100%">
                    <tr valign="top" class="midcolora">
                        <td width="22%">
                            <asp:Label ID="capREMARKS" runat="server">Any other remark</asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtREMARKS" runat="server" MaxLength="255"
                                size="30" TextMode="MultiLine" Width="208px" Height="62px"></asp:TextBox><asp:CustomValidator
                                    ID="csvREMARKS" runat="server" Display="Dynamic" ControlToValidate="txtREMARKS"
                                    ClientValidationFunction="ChkTextAreaLength"></asp:CustomValidator><asp:Label ID="lblREMARK"
                                        runat="server" CssClass="LabelFont" Visible="false">-N.A.-</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trPROPERTY_ON_MORE_THAN" runat="server">
            <td class="midcolora">
                Is Property situated on more than 5 Acres?<span class="mandatory" id="spnPROPERTY_ON_MORE_THAN">*</span>
            </td>
            <td class="midcolora">
                <asp:DropDownList ID="cmbPROPERTY_ON_MORE_THAN" onfocus="SelectComboIndex('cmbPROPERTY_ON_MORE_THAN')"
                    onchange="javascript:EnableDisableDesc(document.getElementById('cmbPROPERTY_ON_MORE_THAN'),document.getElementById('txtPROPERTY_ON_MORE_THAN_DESC'),document.getElementById('lblPROPERTY_ON_MORE_THAN'));ApplyColor();ChangeColor();"
                    runat="server">
                </asp:DropDownList>
            </td>
            <td class="midcolora">
                Description of Land Use <span class="mandatory" id="spnPROPERTY_ON_MORE_THAN_DESC">*</span>
            </td>
            <td class="midcolora">
                <asp:TextBox ID="txtPROPERTY_ON_MORE_THAN_DESC" runat="server" MaxLength="50"></asp:TextBox><asp:Label
                    ID="lblPROPERTY_ON_MORE_THAN" runat="server" CssClass="LabelFont">
						-N.A.-
                </asp:Label>
                <br>
                <asp:RequiredFieldValidator ID="rfvPROPERTY_ON_MORE_THAN_DESC" runat="server" Display="Dynamic"
                    ControlToValidate="txtPROPERTY_ON_MORE_THAN_DESC" ErrorMessage="Please enter Description of Land Use."></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trDWELLING_MOBILE_HOME" runat="server">
            <td class="midcolora">
                Is the dwelling a mobile home or double wide?<span class="mandatory" id="spnDWELLING_MOBILE_HOME">*</span>
            </td>
            <td class="midcolora">
                <asp:DropDownList ID="cmbDWELLING_MOBILE_HOME" onfocus="SelectComboIndex('cmbDWELLING_MOBILE_HOME')"
                    onchange="javascript:EnableDisableDesc(document.getElementById('cmbDWELLING_MOBILE_HOME'),document.getElementById('txtDWELLING_MOBILE_HOME_DESC'),document.getElementById('lblDWELLING_MOBILE_HOME'));ApplyColor();ChangeColor();"
                    runat="server">
                </asp:DropDownList>
            </td>
            <td class="midcolora">
                Dwelling Description <span class="mandatory" id="spnDWELLING_MOBILE_HOME_DESC">*</span>
            </td>
            <td class="midcolora">
                <asp:TextBox ID="txtDWELLING_MOBILE_HOME_DESC" runat="server" MaxLength="50"></asp:TextBox>
                <asp:Label ID="lblDWELLING_MOBILE_HOME" runat="server" CssClass="LabelFont">
							-N.A.-
                </asp:Label>
                <br>
                <asp:RequiredFieldValidator ID="rfvDWELLING_MOBILE_HOME_DESC" runat="server" Display="Dynamic"
                    ControlToValidate="txtDWELLING_MOBILE_HOME_DESC" ErrorMessage="Please enter description."></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trPROPERTY_USED_WHOLE_PART" runat="server">
            <td class="midcolora">
                Is Property Used in whole or in part for farming, commercial, industrial, professional
                or business purposes except if use is incidental?<span class="mandatory" id="spnPROPERTY_USED_WHOLE_PART">*</span>
            </td>
            <td class="midcolora">
                <asp:DropDownList ID="cmbPROPERTY_USED_WHOLE_PART" onfocus="SelectComboIndex('cmbPROPERTY_USED_WHOLE_PART')"
                    onchange="javascript:EnableDisableDesc(document.getElementById('cmbPROPERTY_USED_WHOLE_PART'),document.getElementById('txtPROPERTY_USED_WHOLE_PART_DESC'),document.getElementById('lblPROPERTY_USED_WHOLE_PART'));ApplyColor();ChangeColor();"
                    runat="server">
                </asp:DropDownList>
            </td>
            <td class="midcolora">
                Business Description <span class="mandatory" id="spnPROPERTY_USED_WHOLE_PART_DESC">*</span>
            </td>
            <td class="midcolora">
                <asp:TextBox ID="txtPROPERTY_USED_WHOLE_PART_DESC" runat="server" MaxLength="50"></asp:TextBox>
                <asp:Label ID="lblPROPERTY_USED_WHOLE_PART" runat="server" CssClass="LabelFont">
						-N.A.-
                </asp:Label>
                <br>
                <asp:RequiredFieldValidator ID="rfvPROPERTY_USED_WHOLE_PART_DESC" runat="server"
                    Display="Dynamic" ControlToValidate="txtPROPERTY_USED_WHOLE_PART_DESC" ErrorMessage="Please enter description."></asp:RequiredFieldValidator>
            </td>
        </tr>
        <%--Added for Itrack Issue 6640 on 11 Dec 09--%>
        <tr id="trNON_WEATHER_CLAIMS">
            <td class="midcolora">
                <asp:Label ID="capNON_WEATHER_CLAIMS" runat="server">NON_WEATHER_CLAIMS</asp:Label><span
                    class="mandatory">*</span>
            </td>
            <td class="midcolora">
                <asp:TextBox ID="txtNON_WEATHER_CLAIMS" runat="server" size="3" MaxLength="2"></asp:TextBox><br>
                <asp:RequiredFieldValidator ID="rfvNON_WEATHER_CLAIMS" runat="server" ControlToValidate="txtNON_WEATHER_CLAIMS"
                    ErrorMessage="NON_WEATHER_CLAIMS" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Enabled="True" ID="revNON_WEATHER_CLAIMS" runat="server"
                    ControlToValidate="txtNON_WEATHER_CLAIMS" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" colspan="2">
            </td>
        </tr>
        <tr id="trWEATHER_CLAIMS">
            <td class="midcolora">
                <asp:Label ID="capWEATHER_CLAIMS" runat="server">WEATHER_CLAIMS</asp:Label><span
                    class="mandatory">*</span>
            </td>
            <td class="midcolora">
                <asp:TextBox ID="txtWEATHER_CLAIMS" runat="server" size="3" MaxLength="2"></asp:TextBox><br>
                <asp:RequiredFieldValidator ID="rfvWEATHER_CLAIMS" runat="server" ControlToValidate="txtWEATHER_CLAIMS"
                    ErrorMessage="WEATHER_CLAIMS" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Enabled="True" ID="revWEATHER_CLAIMS" runat="server"
                    ControlToValidate="txtWEATHER_CLAIMS" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
            <td class="midcolora" colspan="2">
            </td>
        </tr>
        <!--Added on 26 sep 2007-->
        <tr id="trBOAT_WITH_HOMEOWNER" runat="server" visible ="false">
            <td class="midcolora">
                <asp:Label ID="capBOAT_WITH_HOMEOWNER" runat="server"></asp:Label><span class="mandatory">*</span>
            </td>
            <td class="midcolora">
                <asp:DropDownList ID="cmbBOAT_WITH_HOMEOWNER" onfocus="SelectComboIndex('cmbBOAT_WITH_HOMEOWNER')"
                    runat="server">
                </asp:DropDownList>
                <br>
                <asp:RequiredFieldValidator ID="rfvBOAT_WITH_HOMEOWNER" runat="server" ControlToValidate="cmbBOAT_WITH_HOMEOWNER"
                    ErrorMessage="Boat with this Homeowner policy can't be blank." Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td class="midcolora" colspan="2">
            </td>
        </tr>
        <!--END Added on 26 sep 2007-->
        <tr>
            <td class="midcolora" colspan="2">
                <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset"></cmsb:CmsButton>
            </td>
            <td class="midcolorr" colspan="2">
                <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save" CausesValidation ="true"></cmsb:CmsButton>
            </td>
        </tr>
    </table>
    </TD></TR></TBODY></TABLE>
    <input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
    <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
    <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
    <input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
    <input id="hidCalledFrom" type="hidden" value="0" name="hidCalledFrom" runat="server">
    <input id="hidPOL_ID" type="hidden" value="0" name="hidPOL_ID" runat="server">
    <input id="hidPOL_VERSION_ID" type="hidden" value="0" name="hidPOL_VERSION_ID" runat="server">
    <input id="hidStateID" type="hidden" value="0" name="hidStateID" runat="server">
    <!-- Added by Charles on 9-Dec-09 for Itrack 6489 -->
    <input id="hidOTHER_DESCRIPTION" type="hidden" value="0" name="hidOTHER_DESCRIPTION"
        runat="server">
    <input id="hidOTHER_DESCRIPTION_VALUE" type="hidden" value="" name="hidOldData" runat="server">
    <!-- Added till here -->
    </form>
    <!-- For lookup layer -->
    <div id="lookupLayerFrame" style="display: none; z-index: 101; position: absolute">
        <iframe id="lookupLayerIFrame" style="display: none; z-index: 1001; filter: alpha(opacity=0);
            background-color: #000000" width="0" height="0" top="0px;" left="0px"></iframe>
    </div>
    <div id="lookupLayer" style="display: none; z-index: 101; position: absolute" onmouseover="javascript:refreshLookupLayer();">
        <table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
            <tr class="SubTabRow">
                <td>
                    <b>Add LookUp</b>
                </td>
                <td>
                    <p align="right">
                        <a href="javascript:void(0)" onclick="javascript:hideLookupLayer();">
                            <img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" width="16" height="14"></a></p>
                </td>
            </tr>
            <tr class="SubTabRow">
                <td colspan="2">
                    <span id="LookUpMsg"></span>
                </td>
            </tr>
        </table>
    </div>
    <!-- Added by Charles on 9-Dec-09 for Itrack 6489 -->
    <script language="javascript">
        if (document.getElementById('hidCalledFrom').value == 'HOME') {
            //document.getElementById("capBREED").style.display = "none";
            //document.getElementById("spnBREED").style.display = "none";
            //document.getElementById("txtBREED").style.display = "none";
            //document.getElementById("lblBREED").style.display = "none";
            //document.getElementById("rfvBREED").style.display = "none";
            //document.getElementById('rfvBREED').setAttribute('enabled', false);

            //document.getElementById("rfvOTHER_DESCRIPTION").style.display = "none";
            //document.getElementById('rfvOTHER_DESCRIPTION').setAttribute('enabled', false);
        }
        else {
            //document.getElementById("txtOTHER_DESCRIPTION").style.display = "none";
            //document.getElementById("lnkOTHER_DESCRIPTION").style.display = "none";
            //document.getElementById("lnkOTHER_DESCRIPTION1").style.display = "none";
            //document.getElementById("csvOTHER_DESCRIPTION").style.display = "none";
        }
    </script>
    <!-- Added till here -->
</body>
</html>
