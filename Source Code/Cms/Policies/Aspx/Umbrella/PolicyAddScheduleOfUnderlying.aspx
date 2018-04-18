<%@ Page validateRequest="false" CodeBehind="PolicyAddScheduleOfUnderlying.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Cms.Policies.Aspx.Umbrella.PolicyAddScheduleOfUnderlying" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ScheduleOfUnderlying</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<STYLE>.hide { OVERFLOW: hidden; TOP: 5px }
	.show { OVERFLOW: hidden; TOP: 5px }
	#tabContent { POSITION: absolute; TOP: 160px }
		</STYLE>
		<SCRIPT src="/cms/cmsweb/scripts/xmldom.js"></SCRIPT>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		/*function ValidateLength(objSource , objArgs)
		{
			if(document.getElementById('txtEXPLAIN').value.length>125)
				objArgs.IsValid = false;
			else
				objArgs.IsValid = true;
		}*/
		function ResetForm()
		{
			DisableValidators();
			document.SCHEDULE_OF_UNDERLYING.reset ();
			if(document.SCHEDULE_OF_UNDERLYING.hidPolicyNumber.value == "0" || document.SCHEDULE_OF_UNDERLYING.hidPolicyNumber.value == "" )
			{
				document.getElementById('cmbPOLICY_LOB').selectedIndex=-1;
				document.getElementById('cmbPOLICY_COMPANY').selectedIndex=0;
				document.getElementById('cmbPOLICY_NUMBER').selectedIndex=-1;
				//document.getElementById('txtEXPLAIN').innerText="";
				//document.getElementById('cmbQUESTION').selectedIndex=-1;
								
				var lob = document.SCHEDULE_OF_UNDERLYING.hidLobId.value ;
				//document.getElementById("txtPOLICY_PREMIUM").innerText ="";
				document.getElementById("txtPOLICY_START_DATE").innerText ="";
				document.getElementById("txtPOLICY_EXPIRATION_DATE").innerText ="";
				if(lob == "1" || lob =="6")
				{
					//document.getElementById("trHOME_COVERAGES1").style.display="none";
					//document.getElementById("trHOME_COVERAGES2").style.display="none";
				if(document.getElementById("trHOME_COVERAGES3") != null)
					document.getElementById("trHOME_COVERAGES3").style.display="none";
					
				}
				if(lob == "2")
				{
				if(document.getElementById("trAUTO_COVERAGES1") != null)
					document.getElementById("trAUTO_COVERAGES1").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES2") != null)
					document.getElementById("trAUTO_COVERAGES2").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES3") != null)
					document.getElementById("trAUTO_COVERAGES3").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES4") != null)
					document.getElementById("trAUTO_COVERAGES4").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES5") != null)
					document.getElementById("trAUTO_COVERAGES5").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES6") != null)
					document.getElementById("trAUTO_COVERAGES6").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES7") != null)
					document.getElementById("trAUTO_COVERAGES7").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES8") != null)
					document.getElementById("trAUTO_COVERAGES8").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES9") != null)
					document.getElementById("trAUTO_COVERAGES9").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES10") != null)
					document.getElementById("trAUTO_COVERAGES10").style.display="none";
				if(document.getElementById("trAUTO_COVERAGES11") != null)
					document.getElementById("trAUTO_COVERAGES11").style.display="none";	
					//document.getElementById("trAUTO_COVERAGES5").style.display="none";
					document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="none";
					document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="none";
					
				}
				if(lob == "3")
				{
				if(document.getElementById("trMOT_COVERAGES1") != null)
					document.getElementById("trMOT_COVERAGES1").style.display="none";
				if(document.getElementById("trMOT_COVERAGES2") != null)
					document.getElementById("trMOT_COVERAGES2").style.display="none";
				if(document.getElementById("trMOT_COVERAGES3") != null)
					document.getElementById("trMOT_COVERAGES3").style.display="none";
				if(document.getElementById("trMOT_COVERAGES4") != null)
					document.getElementById("trMOT_COVERAGES4").style.display="none";
				if(document.getElementById("trMOT_COVERAGES5") != null)
					document.getElementById("trMOT_COVERAGES5").style.display="none";
				if(document.getElementById("trMOT_COVERAGES6") != null)
					document.getElementById("trMOT_COVERAGES6").style.display="none";
				if(document.getElementById("trMOT_COVERAGES7") != null)
					document.getElementById("trMOT_COVERAGES7").style.display="none";
				if(document.getElementById("trMOT_COVERAGES8") != null)
					document.getElementById("trMOT_COVERAGES8").style.display="none";
				if(document.getElementById("trMOT_COVERAGES9") != null)
					document.getElementById("trMOT_COVERAGES9").style.display="none";
				if(document.getElementById("trMOT_COVERAGES10") != null)
					document.getElementById("trMOT_COVERAGES10").style.display="none";	
					document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="none";
					document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="none";			
				}
				if(lob == "4")
				{
				if(document.getElementById("trWATERCRAFT_COVERAGES1") != null)
					document.getElementById("trWATERCRAFT_COVERAGES1").style.display="none";
					//document.getElementById("trWATERCRAFT_COVERAGES2").style.display="none";
				}
			if(document.getElementById("trCOVERAGES") != null)
			{
				document.getElementById("trCOVERAGES").style.display="none";
				document.getElementById("trCHECKBUTTONS").style.display="none";
			}
			if(document.getElementById("capHAS_MOTORIST_PROTECTION") != null)
			{
				document.getElementById("capHAS_MOTORIST_PROTECTION").style.display="none";
				document.getElementById("spnHAS_MOTORIST_PROTECTION").style.display="none";
				document.getElementById("cmbHAS_MOTORIST_PROTECTION").style.display="none";
				document.getElementById("capHAS_SIGNED_A9").style.display="none";
				document.getElementById("spnHAS_SIGNED_A9").style.display="none";
				document.getElementById("cmbHAS_SIGNED_A9").style.display="none";
			}
				
			if(document.getElementById("imgOpenDecPage"))
			{	
				document.getElementById("imgOpenDecPage").style.display ="none";
				document.getElementById("lblDecPage").style.display ="none";
			}
			if(document.getElementById("cmbPOLICY_LOB"))
						document.getElementById("cmbPOLICY_LOB").focus();						
			ChangeColor();
			return false;			
			}
		}
		
		function ResetCoverages()
		{
			

			var lob = document.SCHEDULE_OF_UNDERLYING.hidLobId.value ;			
			document.getElementById("txtPOLICY_START_DATE").innerText ="";
			document.getElementById("txtPOLICY_EXPIRATION_DATE").innerText ="";
			
			// Reset Coverages only in case of Other company				
			var varCompanyType = document.getElementById("cmbPOLICY_COMPANY").options[document.getElementById("cmbPOLICY_COMPANY").selectedIndex].value;
			if(varCompanyType == "1")
			{
				// Enable the coverages 
				EnableCoverages();			
				if(lob == "1" || lob =="6")
				{
					if(document.getElementById("txtHOME_COV5") != null)
						document.getElementById("txtHOME_COV5").innerText ="";
					//if(document.getElementById("txtHOME_COV6") != null)
						//document.getElementById("txtHOME_COV6").innerText ="";
						
				}
				if(lob == "2")
				{
				
					
					if(document.getElementById("txtAUTO_COV2") != null)
						document.getElementById("txtAUTO_COV2").innerText ="";
					if(document.getElementById("txtAUTO_COV3") != null)
						document.getElementById("txtAUTO_COV3").innerText ="";
					if(document.getElementById("txtAUTO_COV4") != null)
						document.getElementById("txtAUTO_COV4").innerText ="";
					if(document.getElementById("txtAUTO_COV5") != null)
						document.getElementById("txtAUTO_COV5").innerText ="";
					if(document.getElementById("txtAUTO_COV6") != null)
						document.getElementById("txtAUTO_COV6").innerText ="";
					if(document.getElementById("txtAUTO_COV7") != null)
						document.getElementById("txtAUTO_COV7").innerText ="";
					
				}
				if(lob=="3")
				{
					if(document.getElementById("txtMOT_COV2") != null)
						document.getElementById("txtMOT_COV2").innerText ="";
					if(document.getElementById("txtMOT_COV3") != null)
						document.getElementById("txtMOT_COV3").innerText ="";
					if(document.getElementById("txtMOT_COV4") != null)
						document.getElementById("txtMOT_COV4").innerText ="";
					if(document.getElementById("txtMOT_COV5") != null)
						document.getElementById("txtMOT_COV5").innerText ="";
					if(document.getElementById("txtMOT_COV6") != null)
						document.getElementById("txtMOT_COV6").innerText ="";
					if(document.getElementById("txtMOT_COV7") != null)
						document.getElementById("txtMOT_COV7").innerText ="";
					if(document.getElementById("txtMOT_COV9") != null)
						document.getElementById("txtMOT_COV9").innerText ="";
					if(document.getElementById("txtMOT_COV11") != null)
						document.getElementById("txtMOT_COV11").innerText ="";	
					if(document.getElementById("txtMOT_COV13") != null)
						document.getElementById("txtMOT_COV13").innerText ="";					
				}
				if(lob == "4")
				{
					if(document.getElementById("txtWAT_COV1") != null)
						document.getElementById("txtWAT_COV1").innerText ="";				
					if(document.getElementById("txtWAT_COV3") != null)
						document.getElementById("txtWAT_COV3").innerText ="";
					
				}
			}
			else if (varCompanyType == "0")
			{
				if(document.getElementById("trCHECKBUTTONS") != null)
					document.getElementById("trCHECKBUTTONS").style.display ="none";
				if(document.getElementById("trCOVERAGES") != null)
					document.getElementById("trCOVERAGES").style.display ="none";
				if(document.getElementById("trAUTO_COVERAGES1") != null)
					document.getElementById("trAUTO_COVERAGES1").style.display ="none";
				if(document.getElementById("trAUTO_COVERAGES2") != null)
					document.getElementById("trAUTO_COVERAGES2").style.display ="none";
				if(document.getElementById("trAUTO_COVERAGES3") != null)
					document.getElementById("trAUTO_COVERAGES3").style.display ="none";
				if(document.getElementById("trAUTO_COVERAGES4") != null)
					document.getElementById("trAUTO_COVERAGES4").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES1") != null)
					document.getElementById("trMOT_COVERAGES1").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES2") != null)
					document.getElementById("trMOT_COVERAGES2").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES3") != null)
					document.getElementById("trMOT_COVERAGES3").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES4") != null)
					document.getElementById("trMOT_COVERAGES4").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES5") != null)
					document.getElementById("trMOT_COVERAGES5").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES6") != null)
					document.getElementById("trMOT_COVERAGES6").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES7") != null)
					document.getElementById("trMOT_COVERAGES7").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES8") != null)
					document.getElementById("trMOT_COVERAGES8").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES9") != null)
					document.getElementById("trMOT_COVERAGES9").style.display ="none";
				if(document.getElementById("trMOT_COVERAGES10") != null)
					document.getElementById("trMOT_COVERAGES10").style.display ="none";
				if(document.getElementById("trWATERCRAFT_COVERAGES1") != null)
					document.getElementById("trWATERCRAFT_COVERAGES1").style.display ="none";
				if(document.getElementById("trHOME_COVERAGES3") != null)
					document.getElementById("trHOME_COVERAGES3").style.display ="none";			
			}
		}
	
	
		
				
		function EnableCoverages()
		{

				var lob = document.SCHEDULE_OF_UNDERLYING.hidLobId.value ;												
				
				if(document.getElementById("trCOVERAGES") != null)			
					document.getElementById("trCOVERAGES").style.display ="inline";
					
				if(lob == "2") //auto
				{	
				if(document.getElementById("trCHECKBUTTONS") != null)
					document.getElementById("trCHECKBUTTONS").style.display ="inline";
					if(document.getElementById("rdoCSL").checked==true)
					{
						//CSL TR
						if(document.getElementById("trAUTO_COVERAGES1") != null)
							document.getElementById("trAUTO_COVERAGES1").style.display ="inline";
						if(document.getElementById("trAUTO_COVERAGES4") != null)
							document.getElementById("trAUTO_COVERAGES4").style.display ="inline";
						if(document.getElementById("trAUTO_COVERAGES5") != null)
							document.getElementById("trAUTO_COVERAGES5").style.display ="inline";
						//SPLIT TR	
						
						if(document.getElementById("trAUTO_COVERAGES6") != null)
							document.getElementById("trAUTO_COVERAGES6").style.display ="none";
						if(document.getElementById("trAUTO_COVERAGES2") != null)
							document.getElementById("trAUTO_COVERAGES2").style.display ="none";
						if(document.getElementById("trAUTO_COVERAGES7") != null)
							document.getElementById("trAUTO_COVERAGES7").style.display ="none";
						if(document.getElementById("trAUTO_COVERAGES8") != null)
							document.getElementById("trAUTO_COVERAGES8").style.display ="none";
						if(document.getElementById("trAUTO_COVERAGES3") != null)
							document.getElementById("trAUTO_COVERAGES3").style.display ="none";
						if(document.getElementById("trAUTO_COVERAGES9") != null)
							document.getElementById("trAUTO_COVERAGES9").style.display ="none";
						if(document.getElementById("trAUTO_COVERAGES10") != null)
							document.getElementById("trAUTO_COVERAGES10").style.display ="none";
							
						
							
					}
					else if(document.getElementById("rdoSPLIT").checked==true)
					{
						//CSL TR
						if(document.getElementById("trAUTO_COVERAGES1") != null)
							document.getElementById("trAUTO_COVERAGES1").style.display ="none";
						if(document.getElementById("trAUTO_COVERAGES4") != null)
							document.getElementById("trAUTO_COVERAGES4").style.display ="none";
						if(document.getElementById("trAUTO_COVERAGES5") != null)
							document.getElementById("trAUTO_COVERAGES5").style.display ="none";
						//SPLIT TR	
						
						if(document.getElementById("trAUTO_COVERAGES6") != null)
							document.getElementById("trAUTO_COVERAGES6").style.display ="inline";
						if(document.getElementById("trAUTO_COVERAGES2") != null)
							document.getElementById("trAUTO_COVERAGES2").style.display ="inline";
						if(document.getElementById("trAUTO_COVERAGES7") != null)
							document.getElementById("trAUTO_COVERAGES7").style.display ="inline";
						if(document.getElementById("trAUTO_COVERAGES8") != null)
							document.getElementById("trAUTO_COVERAGES8").style.display ="inline";
						if(document.getElementById("trAUTO_COVERAGES3") != null)
							document.getElementById("trAUTO_COVERAGES3").style.display ="inline";
						if(document.getElementById("trAUTO_COVERAGES9") != null)
							document.getElementById("trAUTO_COVERAGES9").style.display ="inline";
						if(document.getElementById("trAUTO_COVERAGES10") != null)
							document.getElementById("trAUTO_COVERAGES10").style.display ="inline";
							
						
					}
					if(document.getElementById("trMOT_COVERAGES1") != null)
						document.getElementById("trMOT_COVERAGES1").style.display ="none";
					if(document.getElementById("trMOT_COVERAGES2") != null)
						document.getElementById("trMOT_COVERAGES2").style.display ="none";
					if(document.getElementById("trMOT_COVERAGES3") != null)
						document.getElementById("trMOT_COVERAGES3").style.display ="none";
					if(document.getElementById("trMOT_COVERAGES4") != null)
						document.getElementById("trMOT_COVERAGES4").style.display ="none";
					if(document.getElementById("trMOT_COVERAGES5") != null)
						document.getElementById("trMOT_COVERAGES5").style.display ="none";
					
					if(document.getElementById("trMOT_COVERAGES6") != null)
						document.getElementById("trMOT_COVERAGES6").style.display ="none";
					if(document.getElementById("trMOT_COVERAGES7") != null)
						document.getElementById("trMOT_COVERAGES7").style.display ="none";
					if(document.getElementById("trMOT_COVERAGES8") != null)
						document.getElementById("trMOT_COVERAGES8").style.display ="none";
					if(document.getElementById("trMOT_COVERAGES9") != null)
						document.getElementById("trMOT_COVERAGES9").style.display ="none";
					if(document.getElementById("trMOT_COVERAGES10") != null)
						document.getElementById("trMOT_COVERAGES10").style.display ="none";
					if(("<%=strState%>") != "IN")
					{	
						document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="inline";
						document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="inline";
					}
					//document.getElementById("cmbRENTAL_DWELLINGS_UNIT").style.display="inline";
				}
				if(lob == "3") // mc
				{
					if(document.getElementById("trCHECKBUTTONS") != null)
						document.getElementById("trCHECKBUTTONS").style.display ="inline";
					if(document.getElementById("rdoCSL").checked==true)
					{
					    // CSL 
					    if(document.getElementById("trMOT_COVERAGES4") != null)
							document.getElementById("trMOT_COVERAGES4").style.display ="inline";
						if(document.getElementById("trMOT_COVERAGES5") != null)
							document.getElementById("trMOT_COVERAGES5").style.display ="inline";
						if(document.getElementById("trMOT_COVERAGES1") != null)
							document.getElementById("trMOT_COVERAGES1").style.display ="inline";
						
						//SPLIT
						if(document.getElementById("trMOT_COVERAGES2") != null)
							document.getElementById("trMOT_COVERAGES2").style.display ="none";
						if(document.getElementById("trMOT_COVERAGES3") != null)
							document.getElementById("trMOT_COVERAGES3").style.display ="none";
						
						if(document.getElementById("trMOT_COVERAGES6") != null)
							document.getElementById("trMOT_COVERAGES6").style.display ="none";
						
						if(document.getElementById("trMOT_COVERAGES7") != null)
							document.getElementById("trMOT_COVERAGES7").style.display ="none";
						if(document.getElementById("trMOT_COVERAGES8") != null)
							document.getElementById("trMOT_COVERAGES8").style.display ="none";
						if(document.getElementById("trMOT_COVERAGES9") != null)
							document.getElementById("trMOT_COVERAGES9").style.display ="none";
						if(document.getElementById("trMOT_COVERAGES10") != null)
							document.getElementById("trMOT_COVERAGES10").style.display ="none";
					}
					else if(document.getElementById("rdoSPLIT").checked==true)
					{
					 // CSL 
					    if(document.getElementById("trMOT_COVERAGES4") != null)
							document.getElementById("trMOT_COVERAGES4").style.display ="none";
						if(document.getElementById("trMOT_COVERAGES5") != null)
							document.getElementById("trMOT_COVERAGES5").style.display ="none";
						if(document.getElementById("trMOT_COVERAGES1") != null)
							document.getElementById("trMOT_COVERAGES1").style.display ="none";
						
						//SPLIT
						if(document.getElementById("trMOT_COVERAGES2") != null)
							document.getElementById("trMOT_COVERAGES2").style.display ="inline";
						if(document.getElementById("trMOT_COVERAGES3") != null)
							document.getElementById("trMOT_COVERAGES3").style.display ="inline";
						
						if(document.getElementById("trMOT_COVERAGES6") != null)
							document.getElementById("trMOT_COVERAGES6").style.display ="inline";
						
						if(document.getElementById("trMOT_COVERAGES7") != null)
							document.getElementById("trMOT_COVERAGES7").style.display ="inline";
						if(document.getElementById("trMOT_COVERAGES8") != null)
							document.getElementById("trMOT_COVERAGES8").style.display ="inline";
						if(document.getElementById("trMOT_COVERAGES9") != null)
							document.getElementById("trMOT_COVERAGES9").style.display ="inline";
						if(document.getElementById("trMOT_COVERAGES10") != null)
							document.getElementById("trMOT_COVERAGES10").style.display ="inline";
					}
						//document.getElementById("cmbRENTAL_DWELLINGS_UNIT").style.display="inline";
						if(("<%=strState%>") != "IN")
						{
							document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="inline";
							document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="inline";
						}
				}
				if(lob == "4")	//WC
				{
					if(document.getElementById("trWATERCRAFT_COVERAGES1") != null)
						document.getElementById("trWATERCRAFT_COVERAGES1").style.display ="inline";
						//document.getElementById("cmbRENTAL_DWELLINGS_UNIT").style.display="none";
						document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="none";
						document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="none";
				}
				if(lob =="1" || lob =="6") // HO & Rental
				{
					if(document.getElementById("trHOME_COVERAGES3") != null)
						document.getElementById("trHOME_COVERAGES3").style.display ="inline";
						//document.getElementById("cmbRENTAL_DWELLINGS_UNIT").style.display="none";
						document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="none";
						document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="none";
					}
		
		}
		
		function DisableCoverages()
		{
								
			if(document.getElementById("trCHECKBUTTONS") != null)			
			document.getElementById("trCHECKBUTTONS").style.display ="none";
			
			if(document.getElementById("trCOVERAGES") != null)			
			document.getElementById("trCOVERAGES").style.display ="none";
							
			if(document.getElementById("trAUTO_COVERAGES1") != null)
			document.getElementById("trAUTO_COVERAGES1").style.display ="none";
			if(document.getElementById("trAUTO_COVERAGES2") != null)
			document.getElementById("trAUTO_COVERAGES2").style.display ="none";
			if(document.getElementById("trAUTO_COVERAGES3") != null)
			document.getElementById("trAUTO_COVERAGES3").style.display ="none";
			if(document.getElementById("trAUTO_COVERAGES4") != null)
			document.getElementById("trAUTO_COVERAGES4").style.display ="none";
			if(document.getElementById("trAUTO_COVERAGES5") != null)
			document.getElementById("trAUTO_COVERAGES5").style.display ="none";
			if(document.getElementById("trAUTO_COVERAGES6") != null)
			document.getElementById("trAUTO_COVERAGES6").style.display ="none";
			if(document.getElementById("trAUTO_COVERAGES7") != null)
			document.getElementById("trAUTO_COVERAGES7").style.display ="none";
			if(document.getElementById("trAUTO_COVERAGES8") != null)
			document.getElementById("trAUTO_COVERAGES8").style.display ="none";
			if(document.getElementById("trAUTO_COVERAGES9") != null)
			document.getElementById("trAUTO_COVERAGES9").style.display ="none";
			if(document.getElementById("trAUTO_COVERAGES10") != null)
			document.getElementById("trAUTO_COVERAGES10").style.display ="none";

			if(document.getElementById("trMOT_COVERAGES1") != null)
			document.getElementById("trMOT_COVERAGES1").style.display ="none";
			if(document.getElementById("trMOT_COVERAGES2") != null)
			document.getElementById("trMOT_COVERAGES2").style.display ="none";
			if(document.getElementById("trMOT_COVERAGES3") != null)
			document.getElementById("trMOT_COVERAGES3").style.display ="none";
			if(document.getElementById("trMOT_COVERAGES4") != null)
			document.getElementById("trMOT_COVERAGES4").style.display ="none";
			if(document.getElementById("trMOT_COVERAGES5") != null)
			document.getElementById("trMOT_COVERAGES5").style.display ="none";
			if(document.getElementById("trMOT_COVERAGES6") != null)
			document.getElementById("trMOT_COVERAGES6").style.display ="none";
			if(document.getElementById("trMOT_COVERAGES7") != null)
			document.getElementById("trMOT_COVERAGES7").style.display ="none";
			if(document.getElementById("trMOT_COVERAGES8") != null)
			document.getElementById("trMOT_COVERAGES8").style.display ="none";
			if(document.getElementById("trMOT_COVERAGES9") != null)
			document.getElementById("trMOT_COVERAGES9").style.display ="none";
			if(document.getElementById("trMOT_COVERAGES10") != null)
			document.getElementById("trMOT_COVERAGES10").style.display ="none";
			
			
			if(document.getElementById("trWATERCRAFT_COVERAGES1") != null)
			document.getElementById("trWATERCRAFT_COVERAGES1").style.display ="none";
			if(document.getElementById("trHOME_COVERAGES3") != null)
			document.getElementById("trHOME_COVERAGES3").style.display ="none";
		}
				
				function ShowHidesigned()
				{
					if(("<%=strState%>") == "IN" && document.getElementById("cmbPOLICY_LOB").value == "2")
					{
						document.getElementById("cmbHAS_MOTORIST_PROTECTION").style.display = "inline";
						document.getElementById("cmbLOWER_LIMITS").style.display = "inline";
						if (document.getElementById("cmbHAS_MOTORIST_PROTECTION").value == "1" || document.getElementById("cmbLOWER_LIMITS").value == "1")
						{
						
							document.getElementById ("capHAS_SIGNED_A9").style.display = "inline";
							document.getElementById ("cmbHAS_SIGNED_A9").style.display = "inline";
							document.getElementById ("spnHAS_SIGNED_A9").style.display = "inline";
						}
						else if(document.getElementById("cmbHAS_MOTORIST_PROTECTION").value == "0" || document.getElementById("cmbLOWER_LIMITS").value == "0")
						{
							document.getElementById ("capHAS_SIGNED_A9").style.display = "none";
							document.getElementById ("cmbHAS_SIGNED_A9").style.display = "none";
							document.getElementById ("spnHAS_SIGNED_A9").style.display = "none";
						}
					}
					else
					{
						document.getElementById("capHAS_MOTORIST_PROTECTION").style.display = "none";
						document.getElementById("spnHAS_MOTORIST_PROTECTION").style.display = "none";
						document.getElementById("cmbHAS_MOTORIST_PROTECTION").style.display = "none";
						document.getElementById("capLOWER_LIMITS").style.display = "none";
						document.getElementById("spnLOWER_LIMITS").style.display = "none";
						document.getElementById("cmbLOWER_LIMITS").style.display = "none";
						document.getElementById ("capHAS_SIGNED_A9").style.display = "none";
						document.getElementById ("cmbHAS_SIGNED_A9").style.display = "none";
						document.getElementById ("spnHAS_SIGNED_A9").style.display = "none";
					}
				
				}	
				function CompanyComboChange(comboBox,txtDesc,lblNA,cmbPN,txtPN,calledFrom)
				{
					
					
					
					var lob = document.SCHEDULE_OF_UNDERLYING.hidLobId.value ;
					var combo = document.getElementById(comboBox);
					var index = combo.selectedIndex;
					var selectedText;
				
					document.SCHEDULE_OF_UNDERLYING.setAttribute('isValid',true);
					document.getElementById("rfvPOLICY_NUMBER").style.display ="none";
					document.getElementById("rfvTXT_POLICY_NUMBER").style.display ="none";
					document.getElementById("rfvCOMPANY_OTHER").style.display ="none";
					document.getElementById("rfvPOLICY_START_DATE").style.display ="none";
					document.getElementById("rfvPOLICY_EXPIRATION_DATE").style.display ="none";
									
					
					
					if(document.getElementById("cmbPOLICY_NUMBER").options[document.getElementById("cmbPOLICY_NUMBER").selectedIndex].value != ""
					&& document.getElementById("cmbPOLICY_COMPANY").options[document.getElementById("cmbPOLICY_COMPANY").selectedIndex].value == "0")
					{
						document.getElementById("imgOpenDecPage").style.display ="inline";
						document.getElementById("lblDecPage").style.display ="inline";
						
					}
					else
					{
						document.getElementById("imgOpenDecPage").style.display ="none";
						document.getElementById("lblDecPage").style.display ="none";
					}
					
					
					if(calledFrom == "initialise")
					{
						document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="none";
						document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="none";
					}
					
					document.getElementById(lblNA).style.display = "inline";
					
					//document.getElementById("rfvEXPLAIN").setAttribute('enabled',false);
					//document.getElementById("rfvEXPLAIN").setAttribute('isvalid',true);
					//document.getElementById("rfvEXPLAIN").style.display ="none";
					//document.getElementById("csvEXPLAIN").setAttribute('enabled',false);
					//document.getElementById("csvEXPLAIN").setAttribute('isvalid',true);
					//document.getElementById("csvEXPLAIN").style.display ="none";
					
					if ( index > -1)
					{
						selectedText = combo.options[index].text;
					}
					if(selectedText == '')
						return;
					if (selectedText == "Other")					
					{
						document.getElementById("hidAppStateID").value = "";
						document.getElementById(lblNA).style.display = "none";
						document.getElementById(txtDesc).style.display = "inline";
						document.getElementById(txtPN).style.display = "inline";
						document.getElementById(cmbPN).style.display  = "none";
						document.getElementById("spnCOMPANY_OTHER").style.display  = "inline";
						document.getElementById("capCOMPANY_OTHER").style.display  = "inline";
						document.getElementById("lbl_NA_COMPANY_OTHER").style.display  = "none";
						
											
						//Enabling Require filed validators 
						document.getElementById("rfvTXT_POLICY_NUMBER").setAttribute('enabled',true);
						document.getElementById("rfvCOMPANY_OTHER").setAttribute('enabled',true);
						//added by Manoj Rathore
						//document.getElementById("cmbOFFICE_PREMISES").style.display="inline";
						if(("<%=strState%>") != "IN")
						{
							document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="inline";
							document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="inline";
						}
						//document.getElementById("capOFFICE_PREMISES").style.display="inline";
			
						
											
						//Disabling Require field validator for Policy Number DropDown
						document.getElementById("rfvPOLICY_NUMBER").setAttribute('enabled',false);
						
						//Enabling text boxes
						
						document.getElementById("txtPOLICY_START_DATE").readOnly=false;
						document.getElementById("txtPOLICY_EXPIRATION_DATE").readOnly=false;
						document.getElementById("imgPOLICY_START_DATE").style.display="inline";
						document.getElementById("imgPOLICY_EXPIRATION_DATE").style.display="inline";				
						// Enable the Coverages 
						EnableCoverages();
						
						document.getElementById ("cmbHAS_MOTORIST_PROTECTION").disabled=false;
						document.getElementById ("cmbHAS_SIGNED_A9").disabled=false;
						document.getElementById ("cmbLOWER_LIMITS").disabled =false;

						//document.getElementById("spnHAS_MOTORIST_PROTECTION").style.display ="inline";	
						document.getElementById("rfvHAS_MOTORIST_PROTECTION").disabled = false;
						document.getElementById ("spnHAS_SIGNED_A9").style.display ="none";	
						document.getElementById ("rfvHAS_SIGNED_A9").disabled = false;
						if(("<%=strState%>") == "IN" && document.getElementById("cmbPOLICY_LOB").value == "2")
						{
							document.getElementById("spnHAS_MOTORIST_PROTECTION").style.display ="inline";	
							document.getElementById("rfvHAS_MOTORIST_PROTECTION").disabled = false;
							document.getElementById ("spnHAS_SIGNED_A9").style.display ="none";	
							document.getElementById ("rfvHAS_SIGNED_A9").disabled = false;
						}
						
						//auto coverage
						document.getElementById ("txtAUTO_COV2").readOnly=false;
						document.getElementById ("txtAUTO_COV3").readOnly=false;
						document.getElementById ("txtAUTO_COV4").readOnly=false;
						document.getElementById ("txtAUTO_COV5").readOnly=false;
						document.getElementById ("txtAUTO_COV6").readOnly=false;
						document.getElementById ("txtAUTO_COV7").readOnly=false;
						document.getElementById ("txtAUTO_COV8").readOnly=false;
						document.getElementById ("txtAUTO_COV10").readOnly=false;
						document.getElementById ("txtAUTO_COV12").readOnly=false;
						document.getElementById ("txtAUTO_COV14").readOnly=false;
						//home
						document.getElementById ("txtHOME_COV5").readOnly=false;
						//motor
						document.getElementById ("txtMOT_COV1").readOnly=false;
						document.getElementById ("txtMOT_COV2").readOnly=false;
						document.getElementById ("txtMOT_COV3").readOnly=false;
						document.getElementById ("txtMOT_COV4").readOnly=false;
						document.getElementById ("txtMOT_COV5").readOnly=false;
						document.getElementById ("txtMOT_COV6").readOnly=false;
						document.getElementById ("txtMOT_COV7").readOnly=false;
						document.getElementById ("txtMOT_COV11").readOnly=false;
						//document.getElementById ("txtMOT_COV12").readOnly=false;
						document.getElementById ("txtMOT_COV13").readOnly=false;
						
						//water
						document.getElementById ("txtWAT_COV1").readOnly=false;
						document.getElementById ("txtWAT_COV3").readOnly=false;
						document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="none";
						document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="none";
												
					}
					else if (selectedText != "")
					{
						document.getElementById(lblNA).style.display = "inline";
						document.getElementById(txtDesc).style.display = "none";
						document.getElementById(txtPN).style.display = "none";
						document.getElementById(cmbPN).style.display  = "inline";
						document.getElementById("spnCOMPANY_OTHER").style.display  = "none";
						document.getElementById("capCOMPANY_OTHER").style.display  = "none";
						document.getElementById("lbl_NA_COMPANY_OTHER").style.display  = "none";
						//added by Manoj Rathore on 8 th jan 2007
						//document.getElementById("cmbOFFICE_PREMISES").style.display="none";
						document.getElementById("chkEXCLUDE_UNINSURED_MOTORIST").style.display="none";
						document.getElementById("capEXCLUDE_UNINSURED_MOTORIST").style.display="none";
						//document.getElementById("capOFFICE_PREMISES").style.display="none";
						
						//Disabling Require filed validators 
						document.getElementById("rfvTXT_POLICY_NUMBER").setAttribute('enabled',false);
						document.getElementById("rfvCOMPANY_OTHER").setAttribute('enabled',false);
						//document.getElementById("rfvPOLICY_START_DATE").setAttribute('enabled',false);
						//document.getElementById("rfvPOLICY_EXPIRATION_DATE").setAttribute('enabled',false);
						//document.getElementById("rfvPOLICY_START_DATE").setAttribute('enabled',false);
						
						//Enabling Require field validator for Policy Number DropDown
						document.getElementById("rfvPOLICY_NUMBER").setAttribute('enabled',true);
						
						//Disabling textboxes
						//document.getElementById("txtPOLICY_PREMIUM").readOnly = true;
						document.getElementById("txtPOLICY_START_DATE").readOnly = true;
						document.getElementById("txtPOLICY_EXPIRATION_DATE").readOnly = true;
						document.getElementById("imgPOLICY_START_DATE").style.display="none";
						document.getElementById("imgPOLICY_EXPIRATION_DATE").style.display="none";
						//DisableCoverages();	
						
						document.getElementById ("cmbHAS_MOTORIST_PROTECTION").disabled=true;
						document.getElementById ("cmbHAS_SIGNED_A9").disabled=true;
						document.getElementById("spnHAS_MOTORIST_PROTECTION").style.display ="none";	
						document.getElementById("rfvHAS_MOTORIST_PROTECTION").disabled = true;
						document.getElementById ("spnHAS_SIGNED_A9").style.display ="none";	
						document.getElementById ("rfvHAS_SIGNED_A9").disabled = true;
						
						document.getElementById ("cmbLOWER_LIMITS").disabled =true;
				
						
						//auto coverage
						
						document.getElementById ("txtAUTO_COV2").readOnly=true;
						document.getElementById ("txtAUTO_COV3").readOnly=true;
						document.getElementById ("txtAUTO_COV4").readOnly=true;
						document.getElementById ("txtAUTO_COV5").readOnly=true;
						document.getElementById ("txtAUTO_COV6").readOnly=true;
						document.getElementById ("txtAUTO_COV7").readOnly=true;
						document.getElementById ("txtAUTO_COV8").readOnly=true;
						document.getElementById ("txtAUTO_COV10").readOnly=true;
						document.getElementById ("txtAUTO_COV12").readOnly=true;
						document.getElementById ("txtAUTO_COV14").readOnly=true;
						//home
						document.getElementById ("txtHOME_COV5").readOnly=true;
						//motor
						document.getElementById ("txtMOT_COV1").readOnly=true;
						document.getElementById ("txtMOT_COV2").readOnly=true;
						document.getElementById ("txtMOT_COV3").readOnly=true;
						document.getElementById ("txtMOT_COV4").readOnly=true;
						document.getElementById ("txtMOT_COV5").readOnly=true;
						document.getElementById ("txtMOT_COV6").readOnly=true;
						document.getElementById ("txtMOT_COV7").readOnly=true;
						document.getElementById ("txtMOT_COV11").readOnly=true;
						//document.getElementById ("txtMOT_COV12").readOnly=true;
						document.getElementById ("txtMOT_COV13").readOnly=true;
						
						//water
						document.getElementById ("txtWAT_COV1").readOnly=true;
						document.getElementById ("txtWAT_COV3").readOnly=true;					
					}
					// Enable the Coverages 
					if (selectedText != "0" && document.SCHEDULE_OF_UNDERLYING.hidLobId.value != "0")				
							EnableCoverages();
					
					ChangeColor();			
					
				}
				
				function OpenDecPage()
				{																						
					var LOBID=document.getElementById("hidLobId").value;
					var url ="/cms/application/Aspx/DecPage.aspx?CalledFrom=UNDERLAYING_POL&CALLEDFOR=DECPAGE&LOB_ID="+LOBID+"&CUSTOMER_ID=<%=intCustomerID%>&POLICY_ID=<%=gintPolicy_ID%>&POLICY_VER_ID=<%=gintPolicy_Ver_ID%>&APP_ID=<%=strAPP%>";
					window.open(url,'BRICS', 1200, 1200);						
				}			
				
				function Initialise()
				{					
					
					CompanyComboChange('cmbPOLICY_COMPANY','txtCOMPANY_OTHER','lbl_NA_COMPANY_OTHER','cmbPOLICY_NUMBER','txtPOLICY_NUMBER','initialise');
					//combo_OnChange('cmbQUESTION','txtEXPLAIN','lblNA_EXP');
					if(document.getElementById("cmbPOLICY_LOB"))
						document.getElementById("cmbPOLICY_LOB").focus();
						ShowHidesigned();
					ApplyColor();
					ChangeColor();
				}
		</script>
	</HEAD>
	<body leftMargin="0" onload="Initialise();" rightMargin="0" topmargin="0" MS_POSITIONING="GridLayout">
		<div class="pageContent" id="bodyHeight">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td>
							<form id="SCHEDULE_OF_UNDERLYING" method="post" runat="server">
								<table width="100%">
									<TBODY>
										<tr>
											<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>Please 
												note that all fields marked with * are mandatory</TD>
										</tr>
										<tr>
											<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
										</tr>
										<tr id="trPolicy" runat="server">
											<TD class="pageHeader" colSpan="4">Policy Information
											</TD>
										</tr>
										<tr>
											<td class="midcolora" width="30%"><asp:label id="capPOLICY_LOB" Runat="server"></asp:label><span class="mandatory" id="spnLOB">*</span></td>
											<td class="midcolora" width="20%" colSpan="3"><asp:dropdownlist id="cmbPOLICY_LOB" Runat="server" OnChange="ResetCoverages();" Width="120" AutoPostBack="True"></asp:dropdownlist><br>
												<asp:requiredfieldvalidator id="rfvLOB" Runat="server" Display="Dynamic" ControlToValidate="cmbPOLICY_LOB"></asp:requiredfieldvalidator></td>
										</tr>
										<tr>
											<td class="midcolora" width="30%"><asp:label id="capPOLICY_COMPANY" Runat="server"></asp:label><span class="mandatory" id="spnCOMPANY">*</span></td>
											<td class="midcolora" width="20%"><asp:dropdownlist id="cmbPOLICY_COMPANY" Runat="server" OnChange="CompanyComboChange('cmbPOLICY_COMPANY','txtCOMPANY_OTHER','lbl_NA_COMPANY_OTHER','cmbPOLICY_NUMBER','txtPOLICY_NUMBER','change');ResetCoverages();">
													<asp:ListItem Value='0'>Wolverine</asp:ListItem>
													<asp:ListItem Value='1'>Other</asp:ListItem>
												</asp:dropdownlist><br>
												<asp:requiredfieldvalidator id="rfvCOMPANY" Runat="server" Display="Dynamic" ControlToValidate="cmbPOLICY_COMPANY"></asp:requiredfieldvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capCOMPANY_OTHER" Runat="server"></asp:label><span class="mandatory" id="spnCOMPANY_OTHER">*</span></td>
											<td class="midcolora" width="20%"><asp:label id="lbl_NA_COMPANY_OTHER" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:textbox id="txtCOMPANY_OTHER" Runat="server"></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvCOMPANY_OTHER" Runat="server" Display="Dynamic" ControlToValidate="txtCOMPANY_OTHER"></asp:requiredfieldvalidator></td>
										</tr>
										<tr>
											<td class="midcolora" width="30%"><asp:label id="capPOLICY_NUMBER" Runat="server"></asp:label><span class="mandatory" id="spnPOLICY_NUMBER">*</span></td>
											<td class="midcolora" width="20%"><asp:dropdownlist id="cmbPOLICY_NUMBER" Runat="server" AutoPostBack="True"></asp:dropdownlist><asp:textbox id="txtPOLICY_NUMBER" Runat="server"></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvPOLICY_NUMBER" Runat="server" Display="Dynamic" ControlToValidate="cmbPOLICY_NUMBER"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="rfvTXT_POLICY_NUMBER" Runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_NUMBER"></asp:requiredfieldvalidator></td>
											<%--<td class="midcolora" width="30%"><asp:label id="capPOLICY_PREMIUM" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtPOLICY_PREMIUM" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revPOLICY_PREMIUM" runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_PREMIUM"></asp:regularexpressionvalidator></td>
											--%>
											<td class="midcolora" width="30%"><asp:label id="lblDecPage" runat="server" CssClass="midcolora">View Declaration Page</asp:label></td>
											<td class="midcolora" width="20%">
												<IMG id="imgOpenDecPage" alt="" src="../../../cmsweb/images/selecticon.gif" runat="server"
													style="CURSOR: hand">
											</td>
										</tr>
										<tr>
											<td class="midcolora" width="30%"><asp:label id="capPOLICY_START_DATE" Runat="server"></asp:label><span class="mandatory" id="spnPOLICY_START_DATE">*</span></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtPOLICY_START_DATE" Runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkPOLICY_START_DATE" runat="server" CssClass="HotSpot">
													<ASP:IMAGE id="imgPOLICY_START_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
												</asp:hyperlink><br>
												<asp:regularexpressionvalidator id="revPOLICY_START_DATE" runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_START_DATE"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="rfvPOLICY_START_DATE" Runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_START_DATE"></asp:requiredfieldvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capPOLICY_EXPIRATION_DATE" Runat="server"></asp:label><span class="mandatory" id="spnPOLICY_EXPIRATION_DATE">*</span></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtPOLICY_EXPIRATION_DATE" Runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkPOLICY_EXPIRATION_DATE" runat="server" CssClass="HotSpot">
													<ASP:IMAGE id="imgPOLICY_EXPIRATION_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
												</asp:hyperlink><br>
												<asp:regularexpressionvalidator id="revPOLICY_EXPIRATION_DATE" runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_EXPIRATION_DATE"></asp:regularexpressionvalidator>
												<asp:requiredfieldvalidator id="rfvPOLICY_EXPIRATION_DATE" Runat="server" Display="Dynamic" ControlToValidate="txtPOLICY_EXPIRATION_DATE"></asp:requiredfieldvalidator>
												<asp:comparevalidator id="cmpToDate" Runat="server" ControlToValidate="txtPOLICY_EXPIRATION_DATE" ErrorMessage="Expiration date can not be less than Effective Date."
												Display="Dynamic" ControlToCompare="txtPOLICY_START_DATE" Type="Date" Operator="GreaterThanEqual"></asp:comparevalidator>
												</td>
										</tr>
										<tr>
											<td class="midcolora" width="30%"><asp:label id="capEXCLUDE_UNINSURED_MOTORIST" Runat="server">Exclude Uninsured Motorist</asp:label></td>
											<td class="midcolora" width="30%" colspan="3"><asp:CheckBox ID="chkEXCLUDE_UNINSURED_MOTORIST" Runat="server" Checked="True" Enabled="False"></asp:CheckBox></td>
										</tr>
										<tr>
											<td class="midcolora" width="30%"><asp:label id="capHAS_MOTORIST_PROTECTION" Runat="server"></asp:label><span class="mandatory" id="spnHAS_MOTORIST_PROTECTION">*</span></td>
											<td class="midcolora" width="20%"><asp:dropdownlist id="cmbHAS_MOTORIST_PROTECTION" onfocus="SelectComboIndex('cmbHAS_MOTORIST_PROTECTION')"
													onchange="ShowHidesigned();" Runat="server"></asp:dropdownlist>
												<asp:requiredfieldvalidator id="rfvHAS_MOTORIST_PROTECTION" Runat="server" Display="Dynamic" ControlToValidate="cmbHAS_MOTORIST_PROTECTION"></asp:requiredfieldvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capHAS_SIGNED_A9" Runat="server"></asp:label><span class="mandatory" id="spnHAS_SIGNED_A9">*</span></td>
											<td class="midcolora" width="20%"><asp:dropdownlist id="cmbHAS_SIGNED_A9" onfocus="SelectComboIndex('cmbHAS_SIGNED_A9')" Runat="server"></asp:dropdownlist>
												<asp:requiredfieldvalidator id="rfvHAS_SIGNED_A9" Runat="server" Display="Dynamic" ControlToValidate="cmbHAS_SIGNED_A9"></asp:requiredfieldvalidator></td>
										</tr>
										<tr>
														<td class="midcolora" width="30%"><asp:label id="capLOWER_LIMITS" Runat="server">Has Uninsured/Underinsured Motorist Protection limits been reduced (lower limits)</asp:label><span class="mandatory" id="spnLOWER_LIMITS">*</span></td>
														<td class="midcolora" width="20%"><asp:dropdownlist id="cmbLOWER_LIMITS" onfocus="SelectComboIndex('cmbLOWER_LIMITS')" onchange="ShowHidesigned();"
																Runat="server"></asp:dropdownlist>
															<asp:requiredfieldvalidator id="rfvLOWER_LIMITS" Runat="server" Display="Dynamic" ControlToValidate="cmbLOWER_LIMITS"></asp:requiredfieldvalidator></td>
															<TD class="midcolora" colSpan="2"></TD>
										</tr>
										<tr id="trCOVERAGES" runat="server">
											<TD class="pageHeader" colSpan="4">Coverages For Policy
											</TD>
										</tr>
										<tr id="trCHECKBUTTONS" runat="server">
											<td class="midcolora"><asp:label id="capCHECK" Runat="server">Check policy coverage type</asp:label></td>
											<td class="midcolora" colSpan="3">
												<asp:radiobutton id="rdoCSL" Runat="server" Text="CSL" Checked="True" GroupName="F" value="0"></asp:radiobutton>
												<asp:radiobutton id="rdoSPLIT" Runat="server" Text="SPLIT" GroupName="F" value="1"></asp:radiobutton></td>
										</tr>
										<tr id="trWATERCRAFT_COVERAGES1" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capWAT_COV1" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtWAT_COV1" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revWAT_COV1" runat="server" Display="Dynamic" ControlToValidate="txtWAT_COV1"></asp:regularexpressionvalidator></td>
											<%--<td class="midcolora" width="30%"><asp:label id="capWAT_COV2" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtWAT_COV2" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revWAT_COV2" runat="server" Display="Dynamic" ControlToValidate="txtWAT_COV2"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trWATERCRAFT_COVERAGES2" runat="server">--%>
											<td class="midcolora" width="30%"><asp:label id="capWAT_COV3" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtWAT_COV3" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revWAT_COV3" runat="server" Display="Dynamic" ControlToValidate="txtWAT_COV3"></asp:regularexpressionvalidator></td>
											<%--<td class="midcolora" width="30%"><asp:label id="capWAT_COV4" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtWAT_COV4" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revWAT_COV4" runat="server" Display="Dynamic" ControlToValidate="txtWAT_COV4"></asp:regularexpressionvalidator></td>--%>
										</tr>
										<tr id="trAUTO_COVERAGES1" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV4" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%" colspan="3"><asp:label id="lblAUTO_COV4" Runat="server"></asp:label><asp:textbox id="txtAUTO_COV4" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV4" runat="server" ControlToValidate="txtAUTO_COV4" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trAUTO_COVERAGES4" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV5" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%" colspan="3"><asp:label id="lblAUTO_COV5" Runat="server"></asp:label><asp:textbox id="txtAUTO_COV5" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV5" runat="server" ControlToValidate="txtAUTO_COV5" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<%--<td class="midcolora" width="30%"><asp:label id="capAUTO_COV1" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox CssClass="INPUTCURRENCY"  id="txtAUTO_COV1" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV1" runat="server" ControlToValidate="txtAUTO_COV1" Display="Dynamic"></asp:regularexpressionvalidator></td>--%>
										</tr>
										<tr id="trAUTO_COVERAGES5" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV8" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox CssClass="INPUTCURRENCY" id="txtAUTO_COV8" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV8" runat="server" ControlToValidate="txtAUTO_COV8" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<td colspan="4" class="midcolora"></td>
										</tr>
										<tr id="trAUTO_COVERAGES6" runat="server">
											<td class="midcolora" colspan="4"><asp:label id="capAUTO_COV9" Runat="server"></asp:label></td>
										</tr>
										<tr id="trAUTO_COVERAGES2" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV2" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtAUTO_COV2" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV2" runat="server" ControlToValidate="txtAUTO_COV2" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV10" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtAUTO_COV10" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV10" runat="server" ControlToValidate="txtAUTO_COV10" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trAUTO_COVERAGES7" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV3" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%" colspan="3"><asp:textbox id="txtAUTO_COV3" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV3" runat="server" ControlToValidate="txtAUTO_COV3" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trAUTO_COVERAGES8" runat="server">
											<td class="midcolora" colspan="4"><asp:label id="capAUTO_COV11" Runat="server"></asp:label></td>
										</tr>
										<tr id="trAUTO_COVERAGES3" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV6" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:label id="lblAUTO_COV6" Runat="server"></asp:label><asp:textbox id="txtAUTO_COV6" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV6" runat="server" ControlToValidate="txtAUTO_COV6" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV12" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtAUTO_COV12" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV12" runat="server" ControlToValidate="txtAUTO_COV6" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trAUTO_COVERAGES9" runat="server">
											<td class="midcolora" colspan="4"><asp:label id="capAUTO_COV13" Runat="server"></asp:label></td>
										</tr>
										<tr id="trAUTO_COVERAGES10" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV7" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:label id="lblAUTO_COV7" Runat="server"></asp:label><asp:textbox id="txtAUTO_COV7" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV7" runat="server" ControlToValidate="txtAUTO_COV7" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV14" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtAUTO_COV14" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV14" runat="server" ControlToValidate="txtAUTO_COV14" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<%--<tr id="trAUTO_COVERAGES4" runat="server" style="display:none">
											
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV8" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox CssClass="INPUTCURRENCY"  id="txtAUTO_COV8" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV8" runat="server" ControlToValidate="txtAUTO_COV8" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<td colspan="4" class="midcolora"></td>
										</tr>
										<%--<tr id="trAUTO_COVERAGES5" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capAUTO_COV9" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%" colSpan="3"><asp:textbox CssClass="INPUTCURRENCY"  id="txtAUTO_COV9" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revAUTO_COV9" runat="server" ControlToValidate="txtAUTO_COV9" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>--%>
										<tr id="trMOT_COVERAGES4" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV1" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtMOT_COV1" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV1" runat="server" ControlToValidate="txtMOT_COV1" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<td colspan="2" class="midcolora"></td>
										</tr>
										<tr id="trMOT_COVERAGES1" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV4" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%" colspan="3"><asp:label id="lblMOT_COV4" Runat="server"></asp:label><asp:textbox id="txtMOT_COV4" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV4" runat="server" ControlToValidate="txtMOT_COV4" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trMOT_COVERAGES5" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV6" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%" colspan="3"><asp:label id="lblMOT_COV6" Runat="server"></asp:label><asp:textbox id="txtMOT_COV6" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV6" runat="server" ControlToValidate="txtMOT_COV6" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										
										<tr id="trMOT_COVERAGES8" runat="server">
											<td class="midcolora" width="30%" colspan="4"><asp:label id="capMOT_COV8" Runat="server"></asp:label></td>
										</tr>
										<tr id="trMOT_COVERAGES2" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV2" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtMOT_COV2" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV2" runat="server" ControlToValidate="txtMOT_COV2" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV9" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtMOT_COV9" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV9" runat="server" ControlToValidate="txtMOT_COV2" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trMOT_COVERAGES6" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV3" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%" colspan="3"><asp:textbox id="txtMOT_COV3" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV3" runat="server" ControlToValidate="txtMOT_COV3" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trMOT_COVERAGES9" runat="server">
											<td class="midcolora" width="30%" colspan="4"><asp:label id="capMOT_COV10" Runat="server"></asp:label></td>
										</tr>
										<tr id="trMOT_COVERAGES3" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV5" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:label id="lblMOT_COV5" Runat="server"></asp:label><asp:textbox id="txtMOT_COV5" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV5" runat="server" ControlToValidate="txtMOT_COV5" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV11" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtMOT_COV11" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV11" runat="server" ControlToValidate="txtMOT_COV11" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trMOT_COVERAGES10" runat="server">
											<td class="midcolora" width="30%" colspan="4"><asp:label id="capMOT_COV12" Runat="server"></asp:label></td>
										</tr>
										<tr id="trMOT_COVERAGES7" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV7" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:label id="lblMOT_COV7" Runat="server"></asp:label><asp:textbox id="txtMOT_COV7" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV7" runat="server" ControlToValidate="txtMOT_COV7" Display="Dynamic"></asp:regularexpressionvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capMOT_COV13" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtMOT_COV13" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revMOT_COV13" runat="server" ControlToValidate="txtMOT_COV2" Display="Dynamic"></asp:regularexpressionvalidator></td>
										</tr>
										<%--<tr id="trHOME_COVERAGES1" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capHOME_COV1" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtHOME_COV1" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revHOME_COV1" runat="server" Display="Dynamic" ControlToValidate="txtHOME_COV1"></asp:regularexpressionvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capHOME_COV2" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtHOME_COV2" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revHOME_COV2" runat="server" Display="Dynamic" ControlToValidate="txtHOME_COV2"></asp:regularexpressionvalidator></td>
										</tr>
										<tr id="trHOME_COVERAGES2" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capHOME_COV3" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtHOME_COV3" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revHOME_COV3" runat="server" Display="Dynamic" ControlToValidate="txtHOME_COV3"></asp:regularexpressionvalidator></td>
											<td class="midcolora" width="30%"><asp:label id="capHOME_COV4" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtHOME_COV4" Runat="server"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revHOME_COV4" runat="server" Display="Dynamic" ControlToValidate="txtHOME_COV4"></asp:regularexpressionvalidator></td>
										</tr>--%>
										<tr id="trHOME_COVERAGES3" runat="server">
											<td class="midcolora" width="30%"><asp:label id="capHOME_COV5" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%" colspan='3'><asp:textbox id="txtHOME_COV5" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>
												<asp:regularexpressionvalidator id="revHOME_COV5" runat="server" Display="Dynamic" ControlToValidate="txtHOME_COV5"></asp:regularexpressionvalidator></td>
											<%--<td class="midcolora" width="30%"><asp:label id="capHOME_COV6" Runat="server"></asp:label></td>
											<td class="midcolora" width="20%"><asp:textbox id="txtHOME_COV6" runat="server" CssClass="INPUTCURRENCY" maxlength="9" size="19"></asp:textbox><br>											
												<asp:regularexpressionvalidator id="revHOME_COV6" runat="server" Display="Dynamic" ControlToValidate="txtHOME_COV6"></asp:regularexpressionvalidator></td>--%>
										</tr>
									</TBODY>
								</table>
					</tr>
				</TBODY>
			</table>
			<table width="100%">
				<%--<tr id="Tr1" runat="server">
					<TD class="pageHeader" colSpan="4">Names Insured</TD>
				</tr>
				<tr>
					<TD class="midcolora" width="30%"><asp:label id="capQUESTION" Runat="server"></asp:label></TD>
					<TD class="midcolora" width="20%"><asp:dropdownlist id="cmbQUESTION" runat="server" OnChange="combo_OnChange('cmbQUESTION','txtEXPLAIN','lblNA_EXP');">
							<asp:ListItem Value=''></asp:ListItem>
							<asp:ListItem Value='0'>No</asp:ListItem>
							<asp:ListItem Value='1'>Yes</asp:ListItem>
						</asp:dropdownlist><br>
					</TD>
					<TD class="midcolora" width="30%"><asp:label id="capEXPLAIN" runat="server"></asp:label><span class="mandatory" id="spnExplain">*</span></TD>
					<TD class="midcolora" width="20%"><asp:label id="lblNA_EXP" runat="server" CssClass="LabelFont">-N.A.-</asp:label><asp:textbox onkeypress="MaxLength(this,125);" id="txtEXPLAIN" runat="server" TextMode="MultiLine"
							Columns="25" Rows="5" MaxLength="100"></asp:textbox><br>
						<asp:requiredfieldvalidator id="rfvEXPLAIN" Runat="server" Display="Dynamic" ControlToValidate="txtEXPLAIN"
							></asp:requiredfieldvalidator>
						<asp:customvalidator id="csvEXPLAIN" Runat="server" ControlToValidate="txtEXPLAIN" Display="Dynamic"
										ClientValidationFunction="ValidateLength" ErrorMessage="Error"></asp:customvalidator>
						</TD>
				</tr>--%>
				<TR>
					<TD class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton></TD>
					<TD class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
				</TR>
			</table>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidPolicyNumber" type="hidden" value="0" name="hidPolicyNumber" runat="server">
			<INPUT id="hidPolicyCompany" type="hidden" value="0" name="hidPolicyCompany" runat="server">
			<input id="hidHAS_MOTORIST_PROTECTION" type="hidden" value="0" name="hidHAS_MOTORIST_PROTECTION" runat="server">
			<input id="hidLOWER_LIMITS" type="hidden" value="0" name="hidLOWER_LIMITS" runat="server">
											<input id="hidHAS_SIGNED_A9" type="hidden" value="0" name="hidHAS_SIGNED_A9" runat="server">
			<INPUT id="hidAppStateID" type="hidden" name="hidAppStateID" runat="server"> <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<input id="hidLobId" type="hidden" value="0" name="hidLobId" runat="server"> </FORM></TD></TR></TBODY></TABLE></div>
		<script>
			RefreshWebGrid(document.SCHEDULE_OF_UNDERLYING.hidFormSaved.value ,document.SCHEDULE_OF_UNDERLYING.hidPolicyNumber.value ,false);
		</script>
		</TR></TBODY></TABLE></FORM></TR></TBODY>
	</body>
</HTML>
