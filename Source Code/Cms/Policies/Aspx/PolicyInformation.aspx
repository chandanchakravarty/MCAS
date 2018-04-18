<%@ Page Language="c#" CodeBehind="PolicyInformation.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.Policies.Aspx.PolicyInformation" ValidateRequest="false" %>

<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="CmsTimer" Src="/cms/cmsweb/webcontrols/CmsTimePicker.ascx" %>
<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>APP_LIST</title>
    <meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)" />
    <meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)" />
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" rel="Stylesheet" type="text/css" />
    <script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
    <script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
    <script language="javascript" src="/cms/cmsweb/scripts/Calendar.js" type="text/javascript"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script>
    <script language="javascript" type="text/javascript">


		var mortgageeBill = '11276';
		var InsuredMortgageeBill = '11278';
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		var lobMenuArr	= new Array();
		lobMenuArr[1]	= 2;
		lobMenuArr[2]	= 0;
		lobMenuArr[3]	= 1;
		lobMenuArr[4]	= 4;
		lobMenuArr[5]	= 5;
		lobMenuArr[6]	= 3;		
		lobMenuArr[7]	= 6;
	    //Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
            //		function cmbAPP_TERMS_Change()//Function added by Charles on 17-Sep-09 for APP/POL Optimization
            //		{
            //			if(document.getElementById("cmbAPP_TERMS").selectedIndex!="-1" && document.getElementById("cmbAPP_TERMS").options[document.getElementById("cmbAPP_TERMS").selectedIndex].value!="")
            //				__doPostBack("cmbAPP_TERMS_Change","1");				
            //			else
            //				return false;
            //		}
	    //Commented Till here 	
	    
	    //Added By Lalit for set expiry date on press enter for save
	    //March 07,2011
	    $(document).ready(function() {
		        $('body').bind('keydown', function(event) {
		            if (event.keyCode == '13') {
		                $("#txtAPP_TERMS").blur();
		                $("#btnSave").focus();

		            }
		        });

		    });
	    
		function ShowDialog()
		{				
				window.open('/cms/application/Aspx/ShowDialog.aspx?CUSTOMER_ID='+<%= gIntCUSTOMER_ID %> +"&APP_ID="+<%= gIntAPP_ID %>+"&APP_VERSION_ID="+ <%= gIntAPP_VERSION_ID %> + "&LOBID="+ <%= gstrLobID %>  ,'Quote',"resizable=yes, scrollbars=yes,width=750,height=500");
		}
		
		<% if(gIntShowQuote==1 ||gIntShowQuote==2 || gIntShowQuote==3)	{%>			
		<%}
		%>
		
		function ShowQuote()
		{				
			ShowPopup('/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID='+<%= gIntCUSTOMER_ID %> +"&APP_ID="+<%= gIntAPP_ID %>+"&APP_VERSION_ID="+ <%= gIntAPP_VERSION_ID %>  +    "&LOBID="+ <%= gstrLobID %>  + "&QUOTE_ID="+ <%= gIntQuoteID %>+"&SHOW="+ <%= gIntShowQuote %> ,'Quote', 950, 400);
		}		
		
		function ShowPolicyQuote()
		{				
			ShowPopup('/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID='+<%= gIntCUSTOMER_ID %> +"&POLICY_ID="+<%= gIntPOLICY_ID %>+"&POLICY_VERSION_ID="+ <%= gIntPOLICY_VERSION_ID %>  +    "&LOBID="+ <%= gstrLobID %>  + "&QUOTE_ID="+ <%= gIntQuoteID %>+"&SHOW="+ <%= gIntShowPolicyQuote %> ,'Quote', 950, 400);
		}

	  //Formats the amount and convert 111 into 1.11
		function FormatAmount(txtAmount)
		{		
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
				
				amt = ReplaceAll(amt,".","");
				
				if (amt.length == 1)
					amt = amt + "0";
				
				if ( ! isNaN(amt))
				{
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					txtAmount.value = InsertDecimal(amt);
				}
			}
		}
		
		function RefreshClient()
		{  
		     
			if(document.getElementById('hidRefresh').value=='R')
			{ 
				var doc = this.parent.document;
				var str
				var policy_type
				policy_type=''
				//Only Of Home
//				if(document.getElementById('hidLOBID').value == '1')
//				{
//				    policy_type= document.getElementById('cmbPOLICY_TYPE').options[document.getElementById('cmbPOLICY_TYPE').selectedIndex].text + "," 
//				}
				str=document.getElementById('txtPOLICY_NUMBER').value + "-" +
				//document.getElementById('txtPOLICY_STATUS').value + "(" +
				//Done for Itrack Issue 5966 on 24 June 2009
				document.getElementById('hidPolicyStatus').value + " (" +
				document.getElementById('txtPOLICY_LOB').value + ":" +
				policy_type +
				document.getElementById("txtAPP_EFFECTIVE_DATE").value + "-" +
				document.getElementById("txtAPP_EXPIRATION_DATE").value + ")"
				doc.getElementById("cltClientTop_lblSAppNo").innerHTML=str;    
			}
		}
		
		/*
		function setTab()
		{
		if (document.getElementById('hidFormSaved')!=null && document.getElementById('hidOldData')!= null)
		{ 
			if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != ""))
			{
				var CustomerID='';
				if (document.getElementById('hidCustomerID')!=null)
				{
					CustomerID=document.getElementById('hidCustomerID').value;
				}
				var AppID='';
				if (document.getElementById('hidAPPID')!=null)
				{
					AppID=document.getElementById('hidAPPID').value;
				}
				var AppVersionID='';
				if (document.getElementById('hidAppVersionID')!=null)
				{
					AppVersionID=document.getElementById('hidAppVersionID').value;
				}
				var LobID='';
				if (document.getElementById('hidLOBID')!=null)
				{
					LobID=document.getElementById('hidLOBID').value;
				}			
			
				Url="PolicyNameIns.aspx?CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&POLICY_LOB="+LobID+"&";
				DrawTab(2,top.frames[1],'Named Insured',Url);		
				Url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=POLICY&EntityType=Application&EntityId=0&CUSTOMER_ID=" + CustomerID +"&APP_ID="+ AppID + "&APP_VERSION_ID="+ AppVersionID+"&POLICY_LOB="+LobID+"&";
				DrawTab(3,top.frames[1],'Attachment',Url);		
				Url="PolicyProcessLogIndex.aspx";
				DrawTab(4,top.frames[1],'Process Log',Url);		
				Url="PolicyEndorsementLogIndex.aspx";
				DrawTab(5,top.frames[1],'Endorsement Log',Url);
				//DrawTab(7,top.frames[1],'Billing Info',Url);
				DrawTab(6,top.frames[1],'Billing Info',Url);								
				//DrawTab(7,top.frames[1],'InstallmentInfo',Url);		
				Url="InstallmentInfo.aspx";
		}
		else
		{	
			//RemoveTab(7,top.frames[1]);				
			RemoveTab(6,top.frames[1]);	
			RemoveTab(5,top.frames[1]);
			RemoveTab(4,top.frames[1]);
			RemoveTab(3,top.frames[1]);
			RemoveTab(2,top.frames[1]);			
		}

	}
}*/			
		function sethidCSR()
		{
			document.getElementById('hidCSR').value = document.getElementById('cmbCSR').options[document.getElementById('cmbCSR').selectedIndex].value;			 
		}		
		function sethidPRODUCER()
		{
			document.getElementById('hidPRODUCER').value = document.getElementById('cmbPRODUCER').options[document.getElementById('cmbPRODUCER').selectedIndex].value;			 
		}		
		function sethidUnderwriter()
		{
			document.getElementById('hidUnderwriter').value = document.getElementById('cmbUNDERWRITER').options[document.getElementById('cmbUNDERWRITER').selectedIndex].value;			 
		}
		function sethidCustomerID()
		{
			document.getElementById('hidCustomerID').value = document.getElementById('cmbCUSTOMER_ID').options[document.getElementById('cmbCUSTOMER_ID').selectedIndex].value;			 
		}
		/*
		function sethidStateID()
		{
			document.getElementById('hidStateID').value = document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;			
		}
		*/	
		//Added by Pradeep Kushwaha on 21-Oct-2010		
		 function setExpDate() {
         if (document.getElementById('txtAPP_TERMS').value != "" && document.getElementById('txtAPP_EFFECTIVE_DATE').value != "") {
                var result = PolicyInformation.GetExpDateFromDays(document.getElementById('txtAPP_TERMS').value, document.getElementById('txtAPP_EFFECTIVE_DATE').value);
              
                if (result != null) {
                    document.getElementById('txtAPP_EXPIRATION_DATE').value = result.value;
                  
                }
                else {
                    document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
                }
            }
            else {
           
                document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
            }
 
            //SetAPP_TERMS();
           
        }
		function ShowExpirationDate()
		{
			var sTerm = "";
			var sposfix = "";
			var sDate;
			var sEffDate = "";
			var sNewMonth = 0;
			var sNewYear = 0;
			var sNY = "";
			var dtDSep;

			//sTerm = document.APP_LIST.cmbAPP_TERMS.value;//Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
			sTerm = document.APP_LIST.txtAPP_TERMS.value;//Added by Pradeep Kushwaha
			if(sTerm == "0" || sTerm == "")
			{
				 document.APP_LIST.txtAPP_EXPIRATION_DATE.value="";				
			}
			else
			{				
				sEffDate = TrimTheString(document.APP_LIST.txtAPP_EFFECTIVE_DATE.value);
	   			dtDSep = GetDateSeparator(sEffDate);
	   			if(dtDSep =="")
	   				dtDSep = "/"
	   			else(dtDSep ==" ")
	   			{
	   				dtDSep = "/"
	   				sEffDate = ReplaceString(sEffDate," ", "")
	   			}
	   			
				if(sEffDate != "")
				{
					sEffDate = ReplaceDateSeparator(sEffDate);
				
						//Added for Multilingual Support
					if (sCultureDateFormat == 'DD/MM/YYYY')					
					{
					    aDateArr = sEffDate.split(dtDSep);
					    					    
					    strDay = aDateArr[0];
					    strMonth = aDateArr[1];
					    strYear = aDateArr[2];		
					    
					    sEffDate = strMonth + '/' + strDay + '/' + strYear					    
					    
					    sDate = new Date(sEffDate);										
					}
					else
					{
					   sDate = new Date(sEffDate);	 			
					}
					
						sNewMonth = sDate.getMonth() + parseInt(sTerm);
						sDay=sDate.getDate();
						if(sNewMonth >= 12)
						{
							sNewYear = sNewMonth / 12;
							sNewYear = sNewYear + sDate.getYear();
							sNewMonth = sNewMonth % 12;
						}
						else
						{
							sNewYear = sDate.getYear();
						}
		
						sDate.setMonth(sNewMonth);
						sDate.setYear(sNewYear);
						if(sDay==30 || sDay==31 || sDay==28 || sDay==29) // added by Pravesh on 17 aug 09 itrack 6265
						{
						var dd = new Date(sDate.getYear(), sNewMonth+1, 0);
							if(dd.getDate()<=sDay)
							{
								sDate.setDate(dd.getDate());
								sDate.setMonth(sNewMonth);
							}
						}
						sNewYear = sDate.getYear();
						if (sNewYear < 1000)
						{
							sNewYear = sNewYear + 1900;
						} 
						
						if ('U' == 'E')		//Date in UK format
							sposfix = new String(sDate.getDate()-1) + dtDSep + new String(sDate.getMonth() + 1) + dtDSep + new String(sNewYear)
						else
						var newDate = new Date(new String(sDate.getMonth() + 1) + "/" + new String(sDate.getDate()) + "/" + new String(sNewYear));
						newDate.setDate(newDate.getDate());
						
						//Added for Multilingual Support
					if (sCultureDateFormat == 'DD/MM/YYYY')					
					{
					    sposfix = new String(newDate.getDate()) + "/" + new String(newDate.getMonth() + 1) + "/" + new String(sNewYear)
					}
					else
					{
					    sposfix = new String(newDate.getMonth() + 1) + "/" + new String(newDate.getDate()) + "/" + new String(sNewYear)								
					}						
												
						document.APP_LIST.txtAPP_EXPIRATION_DATE.value = sposfix;						
				}
			}
		}
		
		function setMenu()
		{
			
            if(top.topframe!=null && top.topframe != undefined){
                    if (top.topframe.main1.menuXmlReady == false)
				        setTimeout("setMenu();",1000);
				
			        //No need to make menus , if record has been deleted,
			        //Hence checking whether record deleted or not
			        if (document.getElementById("hidFormSaved") != null && document.getElementById("hidFormSaved").value != "5")
			        {
				        top.topframe.main1.activeMenuBar = '1';
				        top.topframe.createActiveMenu();
				        top.topframe.enableMenus("1","ALL");
				        top.topframe.enableMenu("1,1,1");			
				         top.topframe.enableMenu("1,2,3");
				         top.topframe.enableMenu("1,1,2");
				        //top.topframe.disableMenus("1,3","ALL");
				        selectedLOB = lobMenuArr[document.getElementById('hidLOBID').value];
				        top.topframe.enableMenu("1,3," + selectedLOB);
			        }		
            }	
		}
		
		function SetLookupValues()
		{
			var customerIDAndStateID= document.getElementById('hidCustomerID').value ;			  
			document.getElementById('hidCustomerID').value = customerIDAndStateID.substring(0,customerIDAndStateID.indexOf('^'));
			//var StateID= customerIDAndStateID.substring(customerIDAndStateID.indexOf('^')+1,customerIDAndStateID.length);
			//SelectComboOption("cmbSTATE_ID",StateID);
			//document.getElementById('hidStateID').value = StateID;
		}		
			function ResetForm1()
			{				
				document.getElementById('hidReset').value=1; 
				
				var CustomerID='';
				if (document.getElementById('hidCustomerID')!=null)
				{
					CustomerID=document.getElementById('hidCustomerID').value;
				}
				
				var AppID='';
				if (document.getElementById('hidAPPID')!=null)
				{
					AppID=document.getElementById('hidAPPID').value;
				}
				
				var AppVersionID='';
				if (document.getElementById('hidAppVersionID')!=null)
				{
					AppVersionID=document.getElementById('hidAppVersionID').value;
				}
				
				var LobID='';
				if (document.getElementById('hidLOBID')!=null)
				{
					LobID=document.getElementById('hidLOBID').value;
				}
				
				var PolicyID = '';
				var PolicyVersionID = '';
				
				if (document.getElementById('hidPolicyID')!=null)
				{
					PolicyID=document.getElementById('hidPolicyID').value;
				}
				
							
				if (document.getElementById('hidPolicyVersionID')!=null)
				{
					PolicyVersionID=document.getElementById('hidPolicyVersionID').value;
				}
				
				CommChange();	
		        fillPayor();	
													
				document.location.href="PolicyInformation.aspx?CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&POLICY_LOB="+ LobID  +"&POLICY_ID="+ PolicyID + "&POLICY_VERSION_ID="+ PolicyVersionID +  "&transferdata=";
				return false; 				
			}
		//added by pravesh for billing plan
		function fillBillingPlan()
		{
		   //Commented by Pradeep Kushwaha(21-oct-2010) for implementation of APP_TERMS on Textbox
            //if (document.getElementById('cmbAPP_TERMS').options.selectedIndex==-1)
            //return false;
		    //Commented till here
             
             if (document.getElementById('txtAPP_TERMS').value=="")
                return false;
        
			var ctrl = document.getElementById('cmbBILL_TYPE');
			if (ctrl == null)
						return;
			// Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox			
			//var policyTerm =document.getElementById('cmbAPP_TERMS').options[document.getElementById('cmbAPP_TERMS').options.selectedIndex].value;
			//Commented till here 
			
			var policyTerm =document.getElementById('txtAPP_TERMS').value;//Added by Pradeep Kushwaha 
			
			var termXML=document.getElementById('hidBillingPlan').value;
			//alert(termXML);
			var objXmlHandler = new XMLHandler();
			var tree = objXmlHandler.quickParseXML(termXML).childNodes[0];
			//adding a blank option
				oOption = document.createElement("option");
				oOption.value = "";
				oOption.text = "";
				document.getElementById('cmbINSTALL_PLAN_ID').length=0;
				document.getElementById('cmbINSTALL_PLAN_ID').add(oOption);
				 
				for(i=0; i<tree.childNodes.length; i++)
				{
					nodValue = tree.childNodes[i].getElementsByTagName('APPLABLE_POLTERM');
					
					if (nodValue != null)
					{
									
						if (nodValue[0].firstChild == null)
							continue
						
						if (nodValue[0].firstChild.text == policyTerm || nodValue[0].firstChild.text == 0 )
						{
								
							PlanID = tree.childNodes[i].getElementsByTagName('INSTALL_PLAN_ID');
							PlanDesc = tree.childNodes[i].getElementsByTagName('BILLING_PLAN');
							
							if (PlanID != null && PlanDesc != null)
							{
								if (PlanID[0] != null &&  PlanDesc[0] != null )
								{
									oOption = document.createElement("option");
									oOption.value = PlanID[0].firstChild.text;
									oOption.text = PlanDesc[0].firstChild.text;
									document.getElementById('cmbINSTALL_PLAN_ID').add(oOption);									
								}
							} 
						} 
					} 
				}   	
				
			for (cntr=0;cntr<document.getElementById('cmbINSTALL_PLAN_ID').length;cntr++)
			{
				if (document.getElementById('hidINSTALL_PLAN_ID').value ==document.getElementById('cmbINSTALL_PLAN_ID').options[cntr].value) 	
				{
					//Added PK
					var deactiveId = document.getElementById('hidDEACTIVE_INSTALL_PLAN_ID').value 
					if(deactiveId!="")
					{
						if(document.getElementById('hidINSTALL_PLAN_ID').value  = deactiveId)
						{
							document.getElementById('cmbINSTALL_PLAN_ID').options[cntr].className ="DeactivatedInstallmentPlan";
							
						}
					}
					//End
					document.getElementById('cmbINSTALL_PLAN_ID').options.selectedIndex=cntr;
					document.getElementById("rfvINSTALL_PLAN_ID").style.display = "none";
					break;
				}
			}	
			BillType();
		//}	
		return false;
		}	
				
		function fillDownPayMode()
			{			
				//added by pravesh
				//var PlanId = document.getElementById('cmbINSTALL_PLAN_ID').options[document.getElementById('cmbINSTALL_PLAN_ID').selectedIndex].value;
			    document.getElementById('hidINSTALL_PLAN_ID').value = document.getElementById('cmbINSTALL_PLAN_ID').value;
				var PlanId =document.getElementById('hidINSTALL_PLAN_ID').value;
				document.getElementById('hidINSTALL_PLAN_ID').value=PlanId;
			  	// Policy term value <= Month_Between * no_of_payments , get the same from XML 
				var strXML = document.getElementById('hidBillingPlan').value;		
				if (strXML=='')
					return;
				var objXmlHandler1 = new XMLHandler();
				var tree = objXmlHandler1.quickParseXML(strXML).childNodes[0];
				//adding a blank option
				oOption = document.createElement("option");
				oOption.value = "";
				oOption.text = "";
				 //alert(' in strxml');
				document.getElementById('cmbDOWN_PAY_MODE').length=0;
				document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
			
				for(i=0; i<tree.childNodes.length; i++)
				{
					nodValue = tree.childNodes[i].getElementsByTagName('INSTALL_PLAN_ID');
					
					if (nodValue != null)
					{									
						if (nodValue[0].firstChild == null)
							continue
						
						if (nodValue[0].firstChild.text == PlanId)
						{								
							nodeDOWN_PAY_MODE = tree.childNodes[i].getElementsByTagName('MODE_OF_DOWNPAY');
							nodeDOWN_PAY_MODE_decs = tree.childNodes[i].getElementsByTagName('LOOKUP_VALUE_DESC');
							nodeDOWN_PAY_MODE1 = tree.childNodes[i].getElementsByTagName('MODE_OF_DOWNPAY1');
							nodeDOWN_PAY_MODE_decs1 = tree.childNodes[i].getElementsByTagName('LOOKUP_VALUE_DESC1');
							nodeDOWN_PAY_MODE2 = tree.childNodes[i].getElementsByTagName('MODE_OF_DOWNPAY2');
							nodeDOWN_PAY_MODE_decs2 = tree.childNodes[i].getElementsByTagName('LOOKUP_VALUE_DESC2');
							if (nodeDOWN_PAY_MODE != null && nodeDOWN_PAY_MODE_decs != null)
							{
								if (nodeDOWN_PAY_MODE[0] != null &&  nodeDOWN_PAY_MODE_decs[0] != null )
								{
									oOption = document.createElement("option");
									oOption.value = nodeDOWN_PAY_MODE[0].firstChild.text;
									oOption.text = nodeDOWN_PAY_MODE_decs[0].firstChild.text;
										if(oOption.value==document.getElementById('hidPOLDOWN_PAY_MODE').value)
								    oOption.style.color='#ff0000'; 
									document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
									//document.getElementById('hidDOWN_PAY_MODE').value = oOption.value;
								}
							} 
							if (nodeDOWN_PAY_MODE1 != null && nodeDOWN_PAY_MODE_decs1 != null)
							{
								if (nodeDOWN_PAY_MODE1[0] != null &&  nodeDOWN_PAY_MODE_decs1[0] != null )
								{
									oOption = document.createElement("option");
									oOption.value = nodeDOWN_PAY_MODE1[0].firstChild.text;
									oOption.text = nodeDOWN_PAY_MODE_decs1[0].firstChild.text;
										if(oOption.value==document.getElementById('hidPOLDOWN_PAY_MODE').value)
								    oOption.style.color='#ff0000'; 
									document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
									//document.getElementById('hidDOWN_PAY_MODE').value = oOption.value;	
								}
							} 
							if (nodeDOWN_PAY_MODE2 != null && nodeDOWN_PAY_MODE_decs2 != null)
							{
								if (nodeDOWN_PAY_MODE2[0] != null &&  nodeDOWN_PAY_MODE_decs2[0] != null )
								{
									oOption = document.createElement("option");
									oOption.value = nodeDOWN_PAY_MODE2[0].firstChild.text;
									oOption.text = nodeDOWN_PAY_MODE_decs2[0].firstChild.text;
										if(oOption.value==document.getElementById('hidPOLDOWN_PAY_MODE').value)
								    oOption.style.color='#ff0000'; 
									document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
								    //document.getElementById('hidDOWN_PAY_MODE').value = oOption.value;
								}
							} 
								SelectComboOption('cmbDOWN_PAY_MODE',document.getElementById("hidDOWN_PAY_MODE").value);							
						} 						
					} 				
				}
				
				return;						
			}			
			
			function BillType()
			{		  
				if(document.getElementById('cmbBILL_TYPE'))
				{
					var ctrl = document.getElementById('cmbBILL_TYPE');
					
					if (ctrl == null)
						return;
					
					if (ctrl.selectedIndex == -1)
					{
						//By default n.a. should be visible
						document.getElementById('cmbINSTALL_PLAN_ID').style.display="block";
						SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidFULL_PAY_PLAN_ID").value);
						//document.getElementById('trBILL_MORTAGAGEE').style.display="inline";
					
						return;
					}
					var LOB = document.getElementById('hidLOBID').value;
					//if(ctrl.options[ctrl.selectedIndex].value == "11150" ||ctrl.options[ctrl.selectedIndex].value == "8460" ||ctrl.options[ctrl.selectedIndex].value == mortgageeBill || ctrl.options[ctrl.selectedIndex].value == InsuredMortgageeBill)
					if(ctrl.options[ctrl.selectedIndex].value == "11150" ||ctrl.options[ctrl.selectedIndex].value == "8460" || ctrl.options[ctrl.selectedIndex].value == InsuredMortgageeBill)
					{
						hidPOLICY_STATUS_CODE1=document.getElementById('hidPOLICY_STATUS_CODE').value;
						hidPOLICY_STATUS_CODE1=hidPOLICY_STATUS_CODE1.toUpperCase();
						
						if ( hidPOLICY_STATUS_CODE1 == <%= "'" + Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENDED + "'" %>  ||
							hidPOLICY_STATUS_CODE1 ==  <%=  "'" + Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_REWRITE + "'" %>  ||
							hidPOLICY_STATUS_CODE1 ==  <%=  "'" + Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_RENEW + "'" %>  ||
							hidPOLICY_STATUS_CODE1 ==  <%=  "'" + Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_ISSUE + "'" %>  ||
							hidPOLICY_STATUS_CODE1 ==  <%=  "'" + Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_RENEWAL_SUSPENSE + "'" %>  ||
							hidPOLICY_STATUS_CODE1 ==  <%=  "'" + Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENSE_REWRITE + "'" %>)
						{
							document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',false);
							if(document.getElementById('hidAgencyCode').value.toUpperCase() != <%=  "'" + CarrierSystemID.ToUpper() + "'" %> )							
								document.getElementById('cmbBILL_TYPE').setAttribute('disabled',false);
						}
						else
						{
							document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',true);							
							document.getElementById('cmbINSTALL_PLAN_ID').style.display = "inline";	
							document.getElementById('cmbBILL_TYPE').setAttribute('disabled',true);	
							document.getElementById('cmbBILL_TYPE').style.display = "inline";					 
						} 
					}
					else
					{
						document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',true);
						SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidFULL_PAY_PLAN_ID").value);
					}	
					
					//var LOB = document.getElementById('hidLOBID').value;
//					if((LOB==<%=((int)enumLOB.REDW).ToString()%> || LOB==<%=((int)enumLOB.HOME).ToString()%>) && (ctrl.options[ctrl.selectedIndex].value=='11276' || ctrl.options[ctrl.selectedIndex].value=='11277' || ctrl.options[ctrl.selectedIndex].value=='11278'))
//						document.getElementById('trBILL_MORTAGAGEE').style.display="inline";
//					else
//						document.getElementById('trBILL_MORTAGAGEE').style.display="none";	

				HideShowBillingInfo();						
				fillDownPayMode();		
				}				
			}			
					
			function HideShowBillingInfo()
			{

				var ctrl = document.getElementById('cmbBILL_TYPE');
				
				if (ctrl == null || ctrl.selectedIndex == -1)
				{
					document.getElementById('lblDOWN_PAY_MODE').style.display="none";
					document.getElementById('lblINSTALL_PLAN_ID').style.display="none";
					document.getElementById('spnINSTALL_PLAN_ID').style.display="none";
					document.getElementById('spnDOWN_PAY_MODE').style.display="none";
					EnableValidator('rfvDOWN_PAY_MODE',false);
					EnableValidator('rfvINSTALL_PLAN_ID',false);
					return;
				}
				
				//if(ctrl.options[ctrl.selectedIndex].value == "8460" || ctrl.options[ctrl.selectedIndex].value == "11150" || ctrl.options[ctrl.selectedIndex].value == "11278" || ctrl.options[ctrl.selectedIndex].value == mortgageeBill || ctrl.options[ctrl.selectedIndex].value == InsuredMortgageeBill)
                if(ctrl.options[ctrl.selectedIndex].value == "114329" )
				{
					document.getElementById('lblDOWN_PAY_MODE').style.display="none";
					document.getElementById('lblINSTALL_PLAN_ID').style.display="none";
					document.getElementById('cmbDOWN_PAY_MODE').style.display="inline";					
					document.getElementById('cmbINSTALL_PLAN_ID').style.display="inline";
					document.getElementById('spnINSTALL_PLAN_ID').style.display="inline";
					document.getElementById('spnDOWN_PAY_MODE').style.display="inline";
					EnableValidator('rfvDOWN_PAY_MODE',false);					
					EnableValidator('rfvINSTALL_PLAN_ID',true);					
				}
				else
				{
					document.getElementById('lblDOWN_PAY_MODE').innerText="N.A.";
					document.getElementById('lblDOWN_PAY_MODE').style.display="inline";
					document.getElementById('lblINSTALL_PLAN_ID').innerText="N.A.";
					document.getElementById('lblINSTALL_PLAN_ID').style.display="inline";
					document.getElementById('cmbDOWN_PAY_MODE').style.display="none";					
					document.getElementById('cmbINSTALL_PLAN_ID').style.display="none";
					document.getElementById('spnINSTALL_PLAN_ID').style.display="none";
					document.getElementById('spnDOWN_PAY_MODE').style.display="none";
					EnableValidator('rfvDOWN_PAY_MODE',false);
					EnableValidator('rfvINSTALL_PLAN_ID',false);
				}	
				//fillDownPayMode();							
			}			
		
			function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbBILL_TYPE":
						lookupMessage	=	"BLCODE.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;						
				}
				showLookupLayer(controlId,lookupMessage);							
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
			
			function ChangeDefaultDate()
			{		   
				if(document.getElementById('revAPP_EFFECTIVE_DATE').isvalid == true)
				{			
				    //Changed by Lalit Feb 24,2011
				    //the Inception Date is greater than policy Effective Date since policy could be risk elapsed, which means
                    //that the period of insurance of the policy is in the past and the application/ policy was done in a future date
			        //i-track # -644
					
                    //setExpDate() commented by Ruchika Chauhan for Policy Screen
                    //setExpDate();	

					return;
					var strPolicyStatusCode = new String(document.getElementById('hidPOLICY_STATUS_CODE').value);					
					
					if (
						 strPolicyStatusCode.toUpperCase() == "<%=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENDED%>" 
						|| strPolicyStatusCode.toUpperCase() == "<%=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_REWRITE%>"
						|| strPolicyStatusCode.toUpperCase() == "<%=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENSE_REWRITE%>"
						|| strPolicyStatusCode.toUpperCase() == "<%=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_ISSUE%>"						
						)					
					{						
						var effDate=document.getElementById('txtAPP_EFFECTIVE_DATE').value;				
						var incDate=document.getElementById('txtAPP_INCEPTION_DATE').value;
						
						//Added for Multilingual Support
					    if (sCultureDateFormat == 'DD/MM/YYYY')			        
			            {			
			                aDateArr = effDate.split('/');
    					    					    
			                strDay = aDateArr[0];
			                strMonth = aDateArr[1];
			                strYear = aDateArr[2];					    	
    					    
			                effDate = strMonth + '/' + strDay + '/' + strYear
    			            
			                aDateArr = incDate.split('/');
    					    					    
			                strDay = aDateArr[0];
			                strMonth = aDateArr[1];
			                strYear = aDateArr[2];			            		
    					    
			                incDate = strMonth + '/' + strDay + '/' + strYear
					    }
						
						var dtEff=new Date(effDate);						
						var dtIncep=new Date(incDate);						
											
						if(dtIncep > dtEff)
						{
							document.getElementById('txtAPP_INCEPTION_DATE').value = document.getElementById('txtAPP_EFFECTIVE_DATE').value
						}						 
						if(dtIncep < dtEff)
						{
						//DONT CHANGE INCEPTION DATE
						}
						if(document.getElementById('txtAPP_INCEPTION_DATE').value == "")
						{
							document.getElementById('txtAPP_INCEPTION_DATE').value = document.getElementById('txtAPP_EFFECTIVE_DATE').value
						}
						//document.getElementById('txtAPP_INCEPTION_DATE').value=document.getElementById('txtAPP_EFFECTIVE_DATE').value
					}						
						//ShowExpirationDate();  
					
				}
			}			
		
			/*Function to be called in onload, initilize the page*/
			function Init()
			{
			   // fillBillingPlan();
				BillType();					
				CommChange();	
				fillPayor();					
				//setTab();						
				//DisplayPreviousYearDesc();
				HideShowBillingInfo();
				//DisplaySubLOB();
				//DisplaySubLobonEffDatChg();	
				//DisplaySubLobonPolVerEffDatChg();			
				ApplyColor();
				ChangeColor();
				
			   if (document.getElementById('hidLOBID.Value')!="0" && document.getElementById('hidLOBID.Value')!=null)	
		        {
		      
		            if (document.getElementById('hidLOBID.Value')<=8)
		            {
		                EnableValidator('rfvState_ID',true);
		            }
		            else
		            {
		                EnableValidator('rfvState_ID',false);
		            }
		            
		            if (document.getElementById('hidLOBID.Value')=="1" || document.getElementById('hidLOBID.Value')=="6")
		            {
		             EnableValidator('rfvPOLICY_TYPE',true);
		            }
		            else
		            {
		             EnableValidator('rfvPOLICY_TYPE',false);
		            }
		        
		        }
		        else
		        {
		            EnableValidator('rfvState_ID',false);
		        }
		        
		         DisablePolicyLevelCommission();
		        //show reject button in suspended mode
		        //ShowReject();
		        RenewalOldPolicyNO();
                 if(document.getElementById('hidLOBID').value == "13")
            {
                //document.getElementById("tdterm").style.visibility  = 'hidden';
                document.getElementById("capAPP_TERMS").style.visibility  = 'hidden';
                document.getElementById("spnAPP_TERMS").style.visibility  = 'hidden';
                document.getElementById("txtAPP_TERMS").style.visibility  = 'hidden';
                document.getElementById("capAPP_INCEPTION_DATE").style.visibility  = 'hidden';
                document.getElementById("txtAPP_INCEPTION_DATE").style.visibility  = 'hidden';
                document.getElementById("imgInceptionExp").style.visibility  = 'hidden';
                //document.getElementById("spnInception").style.visibility  = 'hidden';
                document.getElementById("capAPP_EFFECTIVE_DATE").innerText = "Voyage From Date";
                document.getElementById("capAPP_EXPIRATION_DATE").innerText = "Voyage To Date";

            }
            else
            {
                //document.getElementById("tdterm").style.visibility  = 'visible';
                document.getElementById("capAPP_TERMS").style.visibility  = 'visible';
                document.getElementById("spnAPP_TERMS").style.visibility  = 'visible';
                document.getElementById("txtAPP_TERMS").style.visibility  = 'visible';
                document.getElementById("capAPP_INCEPTION_DATE").style.visibility  = 'visible';
                document.getElementById("txtAPP_INCEPTION_DATE").style.visibility  = 'visible';
                document.getElementById("imgInceptionExp").style.visibility  = 'visible';
                //document.getElementById("spnInception").style.visibility  = 'visible';
                document.getElementById("capAPP_EFFECTIVE_DATE").innerText = "Effective Datee";
                document.getElementById("capAPP_INCEPTION_DATE").innerText = "Inception Date";
                document.getElementById("capAPP_EXPIRATION_DATE").innerText = "Expiry Date";

            }
			}			
//                function ShowReject()
//                {
//                    if (document.getElementById('lblPOLICY_STATUS').innerHTML =='Suspended')
//                    {
//                        document.getElementById('btnReject').style.display='inline';
//                    }else
//                    {
//                        document.getElementById('btnReject').style.display='none';
//                    }
//                }


                        function DisablePolicyLevelCommission()
                        {
                            if(document.getElementById('hidPOL_TRAN_TYPE').value=='14560')
                            {
                                document.getElementById('tdBrokerName').colSpan = 3;
                                document.getElementById('tdChkPolicyCommission').style.display = 'none';
                                document.getElementById('tdtxtPolicyCommission').style.display = 'none';
                                //document.getElementById('tdChkPolicyCommission').disabled=true;
                                //document.getElementById('tdtxtPolicyCommission').disabled=true;
                            }
                        }



            function RenewalOldPolicyNO()
			{
			    if(document.getElementById('hidOLD_POLICY_NUMBER') != null && document.getElementById('hidOLD_POLICY_NUMBER').value=='')
			    {
			      document.getElementById('tdOLD_POLICY_NUMBER').style.display = 'none';
			      document.getElementById('tdAPP_VERSION').colSpan = 2;
			    }
			   else
			    {
			      document.getElementById('tdOLD_POLICY_NUMBER').style.display = 'inline';
			      document.getElementById('tdAPP_VERSION').colSpan = 1;
			    }
			}
			function DoBackToAssistant()
			{
				this.parent.document.location.href = "/Cms/Client/Aspx/CustomerManagerIndex.aspx";
				return false;
			}
			
			function DoBack()
			{
				this.parent.document.location.href = "/Cms/Client/Aspx/CustomerManagerSearch.aspx";
				return false;
			}
			
			function CheckPolicyStatus()
			{
				if (document.getElementById('hidPolicyStatus').value == 'Suspended')
				{
					document.getElementById('txtAPP_INCEPTION_DATE').setAttribute('readOnly',false);
				}
				else
				{
					document.getElementById('txtAPP_INCEPTION_DATE').setAttribute('readOnly',true);
				}	
			}
			function ChkBillingPlan(objSource , objArgs)
			{
			    objArgs.IsValid=true;
			    return;				
			}			
			function fillDownPay()
			{				
				if (document.getElementById('cmbDOWN_PAY_MODE').options.selectedIndex !=-1)
					document.getElementById('hidDOWN_PAY_MODE').value=document.getElementById('cmbDOWN_PAY_MODE').options[document.getElementById('cmbDOWN_PAY_MODE').options.selectedIndex].value;
						document.getElementById("rfvDOWN_PAY_MODE").setAttribute('enabled',false); 
										
			}
			function setDownPay()
			{			
			    if (document.getElementById('hidDOWN_PAY_MODE').value !='0' && document.getElementById('hidDOWN_PAY_MODE').value !='')   	
				{
					for (i=0;i<document.getElementById('cmbDOWN_PAY_MODE').options.length;i++)
					{					 					
					if (document.getElementById('cmbDOWN_PAY_MODE').options[i].value==document.getElementById('hidDOWN_PAY_MODE').value)
						document.getElementById('cmbDOWN_PAY_MODE').selectedIndex=i;
						
					//ApplyColor();
					ChangeColor();
					}
				}				
			}
			
			function showHideRenewal()
			{				
				var strPolicyStatusCode = new String(document.getElementById('hidPOLICY_STATUS_CODE').value);
				
				if ( strPolicyStatusCode.toUpperCase() != "<%=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_SUSPENDED%>" && strPolicyStatusCode.toUpperCase() != "<%=Cms.BusinessLayer.BlProcess.ClsPolicyProcess.POLICY_STATUS_UNDER_ISSUE%>")
				{
					document.getElementById('trRenewalInfo0').style.display ="inline";  
					document.getElementById('trRenewalInfo1').style.display ="inline";  
					document.getElementById('trRenewalInfo2').style.display ="inline";					
				}
				else
				{
					document.getElementById('trRenewalInfo0').style.display ="none";  
					document.getElementById('trRenewalInfo1').style.display ="none";  
					document.getElementById('trRenewalInfo2').style.display ="none";					
				}
			}		
			/*
			function PostFromLookup()
		    {
			    //Post back the form to show the details of holder
			    //document.getElementById("hidLOOKUP").value = "Y";
			    __doPostBack('','')
		    }
			function CurrencyFormat()
			{
				if (document.getElementById('txtRECEIVED_PRMIUM').value != null && document.getElementById('txtRECEIVED_PRMIUM').value  != '')
					document.getElementById('txtRECEIVED_PRMIUM').value = formatCurrency(document.getElementById('txtRECEIVED_PRMIUM').value)
			} 
			
			function showhideStateDDL()
			{
				if (document.getElementById('hidCalledFrom').value=='REWRITE' || document.getElementById('hidPOLICY_STATUS_CODE').value=='UREWRITE' || document.getElementById('hidPOLICY_STATUS_CODE').value=='REWRTSUSP')  
				{
				document.getElementById('cmbSTATE_ID').style.display ="inline" 
				//Commented by Charles on 21-Aug-09 for APP/POL OPTIMISATION
//				document.getElementById('cmbAGENCY_ID').style.display ="inline" 
//				document.getElementById('cmbAGENCY_ID').setAttribute("disabled",true);
//				document.getElementById('txtAGENCY_DISPLAY_NAME').style.display ="none" 
//				document.getElementById('rfvAGENCY_ID').setAttribute("enabled",true);
				
				//document.getElementById('cmbCSR').style.display ="inline" 
				//document.getElementById('cmbPRODUCER').style.display ="inline"
				//document.getElementById('txtCSRNAME').style.display ="none" 
				//document.getElementById('txtPRODUCER').style.display ="none"										
				document.getElementById('txtSTATE_NAME').style.display ="none" 				
				document.getElementById('rfvSTATE_ID').setAttribute("enabled",true); 				
				}
				else
				{
				document.getElementById('txtSTATE_NAME').style.display ="inline" 				
				//document.getElementById('txtCSRNAME').style.display ="inline"  
				//document.getElementById('txtPRODUCER').style.display ="inline" 
				document.getElementById('cmbSTATE_ID').style.display ="none" 				
				//document.getElementById('cmbCSR').style.display ="none" 
				//document.getElementById('cmbPRODUCER').style.display ="none" 			
				document.getElementById('rfvSTATE_ID').setAttribute("enabled",false);
				document.getElementById('rfvSTATE_ID').style.display ='none';
				 
				//Commented by Charles on 21-Aug-09 for APP/POL OPTIMISATION
//				document.getElementById('cmbAGENCY_ID').style.display ="none"
//				document.getElementById('rfvAGENCY_ID').setAttribute("enabled",false);
//				document.getElementById('rfvAGENCY_ID').style.display ='none'; 
				}				
			}*/
			
			function HideUnhideReason()
			{
			var chkNOTRENEW=document.getElementById('chkNOT_RENEW');
			if (chkNOTRENEW.checked==true)
				{
				document.getElementById('cmbNOT_RENEW_REASON').style.display ="inline"; 
				document.getElementById('capNOT_RENEW_REASON').style.display ="inline"; 
				document.getElementById('capNOT_RENEW_REASON').style.display ="inline"; 
				document.getElementById('chkREFER_UNDERWRITER').setAttribute("checked",false); 
				document.getElementById('capREFERAL_INSTRUCTIONS').style.display ="none";
				document.getElementById('txtREFERAL_INSTRUCTIONS').style.display ="none";
				//document.getElementById('txtREFERAL_INSTRUCTIONS').value ="";
				}
			else
				{
				document.getElementById('cmbNOT_RENEW_REASON').style.display ="none";
				document.getElementById('capNOT_RENEW_REASON').style.display ="none";
				//document.getElementById('chkREFER_UNDERWRITER').setAttribute("checked",true); 
				}
			}
			
			function CheckUncheckReferToUnderWriter()
			{
			var chkReferUnderWriter=document.getElementById('chkREFER_UNDERWRITER');
			if (chkReferUnderWriter.checked==true)
				{
				document.getElementById('cmbNOT_RENEW_REASON').style.display ="none"; 
				document.getElementById('capNOT_RENEW_REASON').style.display ="none"; 
				//document.getElementById('capNOT_RENEW_REASON').style.display ="none"; 
				document.getElementById('chkNOT_RENEW').setAttribute("checked",false);
				document.getElementById('capREFERAL_INSTRUCTIONS').style.display ="inline";
				document.getElementById('txtREFERAL_INSTRUCTIONS').style.display ="inline";
				}
			else
				{
				document.getElementById('capREFERAL_INSTRUCTIONS').style.display ="none";
				document.getElementById('txtREFERAL_INSTRUCTIONS').style.display ="none";
				//document.getElementById('txtREFERAL_INSTRUCTIONS').value ="";
				var chkNOTRENEW=document.getElementById('chkNOT_RENEW');
					if (chkNOTRENEW.checked==true)
					{
					document.getElementById('cmbNOT_RENEW_REASON').style.display ="inline";
					document.getElementById('capNOT_RENEW_REASON').style.display ="inline";
					}
				}
			}
		    function Validate_ChangeDefaultDate()
			{			   
				 if(!IsProperDate(document.getElementById('txtAPP_EFFECTIVE_DATE'))) return false;
	             if(!IsProperDate(document.getElementById('txtAPP_INCEPTION_DATE'))) return false;
	             //ChangeDefaultDate();				
			}
			function ProcessKeypress() 
			{
				if (event.keyCode == 13) 
				{       
					//__doPostBack('ibtnAdd','click');
					
					//ChangeDefaultDate();
					//if (document.getElementById('txtRECEIVED_PRMIUM').value != null && document.getElementById('txtRECEIVED_PRMIUM').value  != '')
					//document.getElementById('txtRECEIVED_PRMIUM').value = formatCurrency(document.getElementById('txtRECEIVED_PRMIUM').value)
				}    
		    }
		    
			function TransaferPolicyToNewClient()
			{
				var AppID='';
				if (document.getElementById('hidAPPID')!=null)
				{
					AppID=document.getElementById('hidAPPID').value;
				}
				
				var AppVersionID='';
				if (document.getElementById('hidAppVersionID')!=null)   
				{
					AppVersionID=document.getElementById('hidAppVersionID').value;
				}
				window.open('CopyPolicyToNewClient.aspx?CUSTOMER_ID='+ document.getElementById('hidCustomerID').value +"&POLICY_ID=" + document.getElementById('hidPolicyID').value + "&POLICY_VERSION_ID="+ document.getElementById('hidPolicyVersionID').value + "&LOB_ID="+ document.getElementById('hidLOBID').value + "&APP_ID="+ AppID + "&APP_VERSION_ID="+ AppVersionID ,'Customer',"resizable=no, scrollbars=yes,width=750,height=500");
				return false;
				//window.open('CopyPolicyToNewClient.aspx')
			}
			
	function CommChange()
	{	    
	    if(document.getElementById("chkPOLICY_LEVEL_COMM_APPLIES").checked)
	    {      
	        document.getElementById("txtPOLICY_LEVEL_COMISSION").disabled = false;
	        document.getElementById("revPOLICY_LEVEL_COMISSION").setAttribute("enabled",true);
	        document.getElementById("csvPOLICY_LEVEL_COMISSION").setAttribute("enabled",true); 
	        document.getElementById("spnPOLICY_LEVEL_COMISSION").style.display = 'inline'; 
	        document.getElementById("rfvPOLICY_LEVEL_COMISSION").setAttribute("enabled",true);   
	        document.getElementById("rfvPOLICY_LEVEL_COMISSION").setAttribute("IsValid",false);               
	    }
	    else
	    {
	        document.getElementById("spnPOLICY_LEVEL_COMISSION").style.display = 'none';  
	        document.getElementById("txtPOLICY_LEVEL_COMISSION").value = ''	   
	        document.getElementById("revPOLICY_LEVEL_COMISSION").style.display = 'none'     
	        document.getElementById("revPOLICY_LEVEL_COMISSION").setAttribute("enabled",false);	
	        document.getElementById("csvPOLICY_LEVEL_COMISSION").style.display = 'none'      
	        document.getElementById("csvPOLICY_LEVEL_COMISSION").setAttribute("enabled",false); 	
	        document.getElementById("rfvPOLICY_LEVEL_COMISSION").style.display = 'none'      
	        document.getElementById("rfvPOLICY_LEVEL_COMISSION").setAttribute("enabled",false);     
	        
	        document.getElementById("txtPOLICY_LEVEL_COMISSION").disabled = true;      
	    }
	}

//    //Added by Ruchika on 13-Jan-2-12 for Policy screen
//     function CalculateTerm()
//        {
//       
//            var incepDate = document.getElementById('txtAPP_INCEPTION_DATE').value;
//            var expireDate = document.getElementById('txtAPP_EXPIRATION_DATE').value;           
//            
//                if (incepDate != "" && expireDate != "") 
//                {                                 
//                                 
//                    var result = PolicyInformation.GetTerm(incepDate, expireDate);              
//                    if ((result != null) && (result.value > 0))
//                    {
//                        document.getElementById('txtAPP_TERMS').value = result.value;        
//                        document.getElementById('spnAPP_EXPIRATION_DATE').style.display='none';   
//                        document.getElementById('rfvAPP_TERMS').style.display='none'; 
//                                            }
//                    else 
//                    {
//                        document.getElementById('txtAPP_TERMS').value = "";
//                        document.getElementById('spnAPP_EXPIRATION_DATE').innerHTML="Expiry date should be greater than the Inception date.";//Added by Ruchika Chauhan on 22-Dec-2011 for TFS # 1211
//                        document.getElementById('spnAPP_EXPIRATION_DATE').style.color='red';
//                        document.getElementById('spnAPP_EXPIRATION_DATE').style.display='inline';                         

//                    }                                   
//                }
//                else 
//                {           
//                    document.getElementById('txtAPP_TERMS').value = "";                    
//                }
//        }
	
	function fillPayor()
	{
	    var combo = document.getElementById("cmbCO_INSURANCE");
	    var comboPayor = document.getElementById("cmbPAYOR");
	    
	    comboPayor.disabled = false;
	    
	    switch (combo.options[combo.options.selectedIndex].value)
	    {
	        case "14547": SelectDropdownOptionByValue(comboPayor,"14542"); break;   //Direct - Insured
	        case "14549": SelectDropdownOptionByValue(comboPayor,"14544"); break;   //Follower - Leader                   
	        case "14548": SelectDropdownOptionByValue(comboPayor,"14542"); break;   //Leader - Insured                   
	        case "": break;
	    }	 
	    
	    if(comboPayor.options[comboPayor.options.selectedIndex].value == "14544" ) //Leader
	    {
	        document.getElementById('txtBILLTO').value="";
	        //document.getElementById('spnBILLTO').style.display = 'none';
            document.getElementById('rfvBILLTO').style.display = 'none';
	    }
	    else
	    {
	       document.getElementById('txtBILLTO').value = document.getElementById('txtCUSTOMERNAME').value; 
	       //document.getElementById('spnBILLTO').style.display = 'inline';
           document.getElementById('rfvBILLTO').style.display = 'inline';
	    }
	    
	    document.getElementById('hidPAYOR').value = comboPayor.options[comboPayor.options.selectedIndex].value;	    
	    comboPayor.disabled = true;
	}
	
	function Validate(objSource , objArgs)
	{	
		var comm = parseFloat(document.getElementById('txtPOLICY_LEVEL_COMISSION').value);
		if(comm < 0 || comm > 100)
		{		
			document.getElementById('txtPOLICY_LEVEL_COMISSION').select();
			objArgs.IsValid=false;
		}
		else
			objArgs.IsValid=true;
	}
	//Added by Pradeep Kushwaha on 06-07-2010
 
	function ShowAlertMessageWhileReject()
	{
	    var r=confirm($('#hidRejectMsg').val());
        return r;
	}
	//Added till here 
	
	  function setHidState()
        {
         if (document.getElementById('cmbState_ID').value != "") {
         
         var iLOB_ID = document.getElementById('hidLOBId').value
                document.getElementById('hidSTATE_ID').value = document.getElementById('cmbState_ID').value;
                
                if (iLOB_ID=="1" )
                {
                document.getElementById('div_poltype').style.display= 'none';
               
                   if (document.getElementById('cmbState_ID').value == "22")
                   {
                     PolicyType("HOPTPM")
                     }
                     
                     else
                     {
                      PolicyType("HOPTYP")
                     }
                }
                
                else if (iLOB_ID=="6")
                {
                document.getElementById('div_poltype').style.display= 'none';
                
                 if (document.getElementById('cmbState_ID').value == "22")
                   {
                     PolicyType("RTPTYP")
                     }
                     
                     else
                     {
                      PolicyType("RTPTYI")
                     }
                }
                else
                {
                 document.getElementById('div_poltype').style.display= 'none';
               
                }
               
            }
            
        }
        
      

        //added by sonal to implemet policy type for Rental and homeowners products
        function PolicyType(SelectType) {

            GlobalError = true;
           
            PolicyInformation.AjaxFillPolicyType(SelectType,FillPolicyType);
            if (GlobalError) {
                return false;
            }
            else {
                return true;
            }


        }

        function FillPolicyType(Result) {


            //var strXML;
            if (Result.error) {
                var xfaultcode = Result.errorDetail.code;
                var xfaultstring = Result.errorDetail.string;
                var xfaultsoap = Result.errorDetail.raw;
            }
            else {
                var TypeList = document.getElementById("cmbPOLICY_SUBLOB");
                TypeList.options.length = 0;
                oOption = document.createElement("option");
                oOption.value = "";
                oOption.text = "";
                TypeList.add(oOption);
               
                if (Result.value != null ) {
                    for (var i = 0; i < Result.value.Rows.length; ++i) {
                        TypeList.options[TypeList.options.length] = new Option(Result.value.Rows[i]["LookupDesc"], Result.value.Rows[i]["LookupID"]);
                    }
                }

                // setStateID();
                //document.getElementById('cmbPC_STATE').value = document.getElementById('hidSTATE_ID_OLD').value;
            }

            return false;
        }
         function ShowMsg()
         {
             alert("<%=str%>");
             
         }
         function ReadOnly()
        {
        //itrack #712
        if(event.keyCode==8) //for back space 
            return false;
        }
    </script>
</head>
<body style="margin-left: 0; margin-top: 0" onload="Init();RefreshClient();CheckPolicyStatus();setDownPay();showHideRenewal();HideUnhideReason();CheckUncheckReferToUnderWriter();"
    onkeydown="ProcessKeypress();">
    <form id="APP_LIST" name="APP_LIST" method="post" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr id="trFORMMESSAGE" runat="server">
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblFormLoadMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" width="100%" align="center" border="0">
                    <tbody>
                        <%--<tr>    Commented by Agniswar on 15 Nov 2011
                            <td>
                                &nbsp;
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="headereffectCenter" colspan="4">
                                <asp:Label ID="lblHeader" runat="server">Policy Information</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="pageHeader" colspan="4">
                                <asp:Label ID="lblManHeader" runat="server">Please note that all fields marked with * are mandatory</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="right" colspan="4">
                                <asp:Label ID="lblCUATION" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="right" colspan="4">
                                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capCUSTOMERNAME" runat="server">Customer Name</asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtCUSTOMERNAME" runat="server" CssClass="midcolora" size="30" BorderStyle="None"></asp:TextBox>
                            </td>
                            <td class="midcolora" width="16%">
                                <asp:Label ID="capPOLICY_STATUS" runat="server">Status</asp:Label>
                            </td>
                            <td class="midcolora" width="34%">
                                <asp:Label ID="lblPOLICY_STATUS" runat="server"></asp:Label>
                                <img id="imgPolicy" style="cursor: hand; display: none" onclick="ShowDialog();" height="15"
                                    alt="" src="/cms/cmsweb/images<%=GetColorScheme()%>/PolicyStatus.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="capHeader" runat="server"></asp:Label><%-- Product--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <table>
                                                <tr>
                                                    <td class="midcolora">
                                                        <asp:Label ID="capPOLICY_LOB" runat="server">Product</asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtPOLICY_LOB" runat="server" CssClass="midcolora" size="50%" BorderStyle="None"
                                                            ReadOnly="true"></asp:TextBox>
                                                        <asp:DropDownList ID="cmbAPP_LOB" onfocus="SelectComboIndex('cmbAPP_LOB')" Style="display: none"
                                                            runat="server" AutoPostBack="True" onchange="BillType();">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvAPP_LOB" Enabled="false" runat="server" Display="Dynamic"
                                                            ControlToValidate="cmbAPP_LOB"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="left" style="width: 40%">
                                                        <table id="tblstate" style="display: block" runat="server">
                                                            <tr>
                                                                <td class="midcolora">
                                                                    <asp:Label ID="capState" runat="server"></asp:Label><span class="mandatory">*</span><br />
                                                                    <asp:DropDownList ID="cmbState_ID" onfocus="SelectComboIndex('cmbAPP_LOB')" runat="server"
                                                                        onchange="setHidState();">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="rfvState_ID" runat="server" Display="Dynamic" ControlToValidate="cmbState_ID"
                                                                        ErrorMessage="state can't be blank" Enabled="false"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="midcolora">
                                                        <asp:Label ID="capTRANSACTION_TYPE" runat="server">Transaction Type</asp:Label><span
                                                            class="mandatory" id="spnTRANSACTION_TYPE" runat="server">*</span>
                                                        <br />
                                                        <asp:DropDownList ID="cmbTRANSACTION_TYPE" onfocus="SelectComboIndex('cmbTRANSACTION_TYPE')"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator runat="server" ID="rfvTRANSACTION_TYPE" Display="Dynamic"
                                                            ControlToValidate="cmbTRANSACTION_TYPE"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <table style="width: 100%" border="0">
                                                <tr>
                                                    <td class="midcolora">
                                                        <asp:Label ID="capSUB_LOB_DESC" runat="server">Line of Business</asp:Label><span
                                                            class="mandatory" id="spnPOLICY_SUBLOB" runat="server"></span>
                                                        <br />
                                                        <asp:DropDownList ID="cmbPOLICY_SUBLOB" Enabled="true" onfocus="SelectComboIndex('cmbPOLICY_SUBLOB')"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvPOLICY_SUBLOB" runat="server" Display="Dynamic"
                                                            ControlToValidate="cmbPOLICY_SUBLOB"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="left" style="width: 40%" class="midcolora" align="left">
                                                        <div style="display: none" id="div_poltype" runat="server">
                                                            <asp:Label ID="capAPP_POLICY_TYPE" runat="server">Policy Type</asp:Label><span class="mandatory">*</span>
                                                            <br />
                                                            <asp:DropDownList ID="cmbPOLICY_TYPE" onfocus="SelectComboIndex('cmbPOLICY_TYPE');"
                                                                runat="server" onchange="setHidPolicyType();">
                                                                <%-- onchange="mandPropInspCredit();"  onblur="mandPropInspCredit();" --%>
                                                            </asp:DropDownList>
                                                            <br />
                                                            <asp:RequiredFieldValidator ID="rfvPOLICY_TYPE" Display="Dynamic" ControlToValidate="cmbPOLICY_TYPE"
                                                                runat="server" Enabled="false"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="midcolora">
                                                        <asp:Label ID="capMODALITY" runat="server">Modality</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="cmbMODALITY" onfocus="SelectComboIndex('cmbMODALITY')" runat="server">
                                                        </asp:DropDownList>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capPOLICY_CURRENCY" runat="server">Policy Currency</asp:Label><span
                                                class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbPOLICY_CURRENCY" Enabled="false" runat="server" onfocus="SelectComboIndex('cmbPOLICY_CURRENCY')">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvPOLICY_CURRENCY" Enabled="false" runat="server"
                                                Display="Dynamic" ControlToValidate="cmbPOLICY_CURRENCY"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:Label ID="capACTIVITY" runat="server">Activity</asp:Label>
                                            <br />
                                            <asp:DropDownList ID="cmbACTIVITY" onfocus="SelectComboIndex('cmbACTIVITY')" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="lblAppHeader" runat="server">Policy</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capPOLICY_NUMBER" runat="server">Policy Number</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtPOLICY_NUMBER" runat="server" CssClass="midcolora" ReadOnly="true"
                                                BorderStyle="None" MaxLength="150" size="50"></asp:TextBox>
                                        </td>
                                        <td class="midcolora" colspan="1" id="tdAPP_VERSION">
                                            <asp:Label ID="capPOLICY_DISP_VERSION" runat="server">Version</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtPOLICY_DISP_VERSION" runat="server" CssClass="midcolora" ReadOnly="true"
                                                BorderStyle="None" MaxLength="10" size="30"></asp:TextBox>
                                        </td>
                                        <td class="midcolora" colspan="1" id="tdOLD_POLICY_NUMBER">
                                            <asp:Label ID="capOLD_POLICY_NUMBER" runat="server">Old Policy Number</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtOLD_POLICY_NUMBER" runat="server" CssClass="midcolora" ReadOnly="true"
                                                BorderStyle="None" MaxLength="10" size="30" onfocus="blur()"></asp:TextBox>
                                            <input type="hidden" id="hidOLD_POLICY_NUMBER" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trIsRewritePolicy" runat="server">
                            <td class="midcolora" colspan="1">
                                <asp:Label ID="capIS_REWRITE_POLICY" runat="server">Rewritten Policy</asp:Label>
                            </td>
                            <td class="midcolora" colspan="3">
                                <asp:Label ID="lblIS_REWRITE_POLICY" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="lblTermHeader" runat="server">Term</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capAPP_TERMS" runat="server">Policy Term</asp:Label><span class="mandatory"
                                                id="spnAPP_TERMS">*</span>
                                            <br />
                                            <asp:TextBox ID="txtAPP_TERMS" MaxLength="4" runat="server" Width="73px" onchange="javascript:setExpDate();"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvAPP_TERMS" runat="server" Display="Dynamic" ControlToValidate="txtAPP_TERMS"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revAPP_TERMS" runat="server" Display="Dynamic"
                                                ControlToValidate="txtAPP_TERMS"></asp:RegularExpressionValidator>
                                            <%-- <br />
                                            <asp:DropDownList ID="cmbAPP_TERMS" onfocus="SelectComboIndex('cmbAPP_TERMS')" runat="server" onchange="ChangeDefaultDate();">
                                            </asp:DropDownList>
                                            <br />--%>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capAPP_EFFECTIVE_DATE" runat="server">Effective Date</asp:Label><span
                                                class="mandatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtAPP_EFFECTIVE_DATE" runat="server" MaxLength="10" size="12" Display="Dynamic"></asp:TextBox>
                                            <asp:HyperLink ID="hlkCalandarDate" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                <asp:Image ID="imgAPP_EFFECTIVE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvAPP_EFFECTIVE_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtAPP_EFFECTIVE_DATE"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revAPP_EFFECTIVE_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtAPP_EFFECTIVE_DATE"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capAPP_INCEPTION_DATE" runat="server">Inception Date</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtAPP_INCEPTION_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox>
                                            <asp:HyperLink ID="hlkInceptionDate" runat="server" CssClass="HotSpot">
                                                <asp:Image ID="imgInceptionExp" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>
                                            <br />
                                            <asp:RegularExpressionValidator ID="revAPP_INCEPTION_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtAPP_INCEPTION_DATE"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            &nbsp;
                                        </td>
                                        <td class="midcolora" colspan="2">
                                            <asp:Label ID="capAPP_EXPIRATION_DATE" runat="server">Expiration Date</asp:Label><span
                                                class="mandatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtAPP_EXPIRATION_DATE" runat="server" ReadOnly="True" MaxLength="10"
                                                size="12" Display="Dynamic"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvAPP_EXPIRATION_DATE" runat="server" Display="Dynamic"
                                                ControlToValidate="txtAPP_EXPIRATION_DATE"></asp:RequiredFieldValidator>
                                        </td>
                                        <%--
                                          <td class="midcolora"style="width: 30%">
                                            <asp:Label ID="capAPP_EXPIRATION_DATE" runat="server">Expiration Date</asp:Label><span
                                                class="mandatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtAPP_EXPIRATION_DATE" runat="server" ReadOnly="true" MaxLength="10"
                                                size="12" Display="Dynamic" onblur="javascript:CalculateTerm()"></asp:TextBox>
                                                 <asp:HyperLink ID="hlkAPP_EXPIRATION_DATE" runat="server" CssClass="HotSpot" Display="Dynamic" >
                                                <asp:Image ID="imgAPP_EXPIRATION_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                    valign="middle"></asp:Image>
                                            </asp:HyperLink>                                            
                                            <br />      
                                            <asp:RequiredFieldValidator ID="rfvAPP_EXPIRATION_DATE" Enabled="false" runat="server"
                                                Display="Dynamic" ControlToValidate="txtAPP_EXPIRATION_DATE"></asp:RequiredFieldValidator>
                                          
                                             <span id="spnAPP_EXPIRATION_DATE"></span>
                                        </td>
                                        <td class="midcolora" style="width: 30%"></td>--%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="lblBillingHeader" runat="server">Billing Information</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capBILL_TYPE" runat="server">Bill Type</asp:Label><span class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbBILL_TYPE" onfocus="SelectComboIndex('cmbBILL_TYPE')" runat="server"
                                                onchange="BillType();fillBillingPlan();">
                                            </asp:DropDownList>
                                            <cmsb:CmsButton class="clsButton" ID="btnChangeBillType" runat="server" Text="Change To Mortgagee"
                                                Style="display: inline" Width="180"></cmsb:CmsButton>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvBILL_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbBILL_TYPE"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:Label ID="capDIV_ID_DEPT_ID_PC_ID" runat="server">Div/Dept/PC</asp:Label><span
                                                class="mandatory" id="spnDIV_ID_DEPT_ID_PC_ID" runat="server">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbDIV_ID_DEPT_ID_PC_ID" onfocus="SelectComboIndex('cmbDIV_ID_DEPT_ID_PC_ID')"
                                                runat="server">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvDIV_ID_DEPT_ID_PC_ID" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbDIV_ID_DEPT_ID_PC_ID"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capINSTALL_PLAN_ID" runat="server">Billing Plan</asp:Label><span class="mandatory"
                                                id="spnINSTALL_PLAN_ID">*</span>
                                            <br />
                                            <asp:Label ID="lblINSTALL_PLAN_ID" runat="server" CssClass="LabelFont">N.A.</asp:Label>
                                            <asp:DropDownList ID="cmbINSTALL_PLAN_ID" onfocus="SelectComboIndex('cmbINSTALL_PLAN_ID')"
                                                runat="server" onchange="fillDownPayMode();">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvINSTALL_PLAN_ID" Display="Dynamic" ControlToValidate="cmbINSTALL_PLAN_ID"
                                                runat="server"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="csvINSTALL_PLAN_ID" Display="Dynamic" ControlToValidate="cmbINSTALL_PLAN_ID"
                                                runat="server" ClientValidationFunction="ChkBillingPlan"></asp:CustomValidator>
                                            <br />
                                            <%-- Added by Ruchika--%>
                                            <asp:Label ID="capFUND_TYPE" runat="server">Fund Type</asp:Label>
                                            <span id="spnFUND_TYPE" runat="server" class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbFUND_TYPE" onfocus="SelectComboIndex('cmbFUND_TYPE')" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvFUND_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbFUND_TYPE"></asp:RequiredFieldValidator>
                                            <%--Added till here--%>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capPREFERENCE_DAY" runat="server">Preferred Billing Day</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtPREFERENCE_DAY" runat="server" MaxLength="2" size="6" Style="text-align: right"></asp:TextBox>
                                            <br />
                                            <asp:RangeValidator ID="rngPREFERENCE_DAY" ControlToValidate="txtPREFERENCE_DAY"
                                                Display="Dynamic" runat="server" MinimumValue="1" MaximumValue="28" Type="Integer"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capDOWN_PAY_MODE" runat="server">Down Payment Mode</asp:Label>
                                            <span class="mandatory" id="spnDOWN_PAY_MODE" runat="server" style="display: none">
                                            </span>
                                            <br />
                                            <asp:Label ID="lblDOWN_PAY_MODE" runat="server" CssClass="LabelFont">N.A.</asp:Label>
                                            <asp:DropDownList ID="cmbDOWN_PAY_MODE" onchange="fillDownPay()" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvDOWN_PAY_MODE" ControlToValidate="cmbDOWN_PAY_MODE"
                                                Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capCO_INSURANCE" runat="server">Co-Insurance</asp:Label><span class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbCO_INSURANCE" runat="server" onchange="javascript:fillPayor();">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvCO_INSURANCE" Display="Dynamic" ControlToValidate="cmbCO_INSURANCE"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capUNDERWRITER" runat="server">Underwriter</asp:Label><span class="mandatory">*</span>
                                            <br />
                                            <select id="cmbUNDERWRITER" onfocus="SelectComboIndex('cmbUNDERWRITER')" runat="server"
                                                onchange="sethidUnderwriter();">
                                            </select>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvUNDERWRITER" ControlToValidate="cmbUNDERWRITER"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capPAYOR" runat="server">Payor</asp:Label><span class="mandatory"
                                                style="display: none">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbPAYOR" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvPAYOR" Display="Dynamic" Enabled="false" ControlToValidate="cmbPAYOR"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capBillTo" runat="server">Bill To</asp:Label><span id="spnBILLTO"
                                                class="mandatory" runat="server">*</span>
                                            <br />
                                            <asp:TextBox ID="txtBillTo" runat="server" size="50" ReadOnly="true" onkeydown="javascript:returnReadOnly();"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvBillTo" Display="Dynamic" ControlToValidate="txtBillTo"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capCSRNAME" runat="server">CSR Name</asp:Label><span class="mandatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtCSRNAME" runat="server" CssClass="midcolora" size="30" BorderStyle="None"
                                                ReadOnly="true" Visible="false"></asp:TextBox>
                                            <select id="cmbCSR" runat="server" onfocus="SelectComboIndex('cmbCSR')" onchange="sethidCSR();">
                                            </select>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvCSR" Display="Dynamic" ControlToValidate="cmbCSR"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="lblAgencyHeader" runat="server">Broker</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%" id="tdBrokerName">
                                            <asp:Label ID="capAGENCY_DISPLAY_NAME" runat="server">Broker</asp:Label>
                                            <br />
                                            <asp:Label ID="lblAGENCY_DISPLAY_NAME" CssClass="LabelFont" runat="server"></asp:Label>
                                        </td>
                                        <td class="midcolora" style="width: 30%" id="tdChkPolicyCommission">
                                            <asp:Label ID="capPOLICY_LEVEL_COMM_APPLIES" runat="server">Policy Level Commission Applies</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="chkPOLICY_LEVEL_COMM_APPLIES" runat="server" onclick="javascript:CommChange();">
                                            </asp:CheckBox>
                                        </td>
                                        <td class="midcolora" style="width: 30%" id="tdtxtPolicyCommission">
                                            <asp:Label ID="capPOLICY_LEVEL_COMISSION" runat="server">Policy Level Commission</asp:Label><span
                                                id="spnPOLICY_LEVEL_COMISSION" class="mandatory" style="display: none">*</span>
                                            <br />
                                            <asp:TextBox ID="txtPOLICY_LEVEL_COMISSION" runat="server" MaxLength="6" size="10"
                                                Style="text-align: right;" disabled="true"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvPOLICY_LEVEL_COMISSION" runat="server" Display="Dynamic"
                                                ControlToValidate="txtPOLICY_LEVEL_COMISSION"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revPOLICY_LEVEL_COMISSION" ControlToValidate="txtPOLICY_LEVEL_COMISSION"
                                                Display="Dynamic" runat="server"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="csvPOLICY_LEVEL_COMISSION" Display="Dynamic" ControlToValidate="txtPOLICY_LEVEL_COMISSION"
                                                ClientValidationFunction="Validate" runat="server"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capPRODUCER" runat="server">Producer Code</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtPRODUCER" runat="server" CssClass="midcolora" size="30" BorderStyle="None"
                                                ReadOnly="true" Visible="false"></asp:TextBox>
                                            <select id="cmbPRODUCER" onfocus="SelectComboIndex('cmbPRODUCER')" runat="server"
                                                onchange="sethidPRODUCER();">
                                            </select>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capBROKER_REQUEST_NO" runat="server">Broker Request #</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtBROKER_REQUEST_NO" runat="server" MaxLength="50" Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capBROKER_COMM_FIRST_INSTM" runat="server">Pay Broker Commission on First Installment</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="chkBROKER_COMM_FIRST_INSTM" Enabled="false" runat="server"></asp:CheckBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trRenewalInfo0" runat="server">
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="lblRenewalHeader" runat="server">Renewal Information</asp:Label>
                            </td>
                        </tr>
                        <tr id="trRenewalInfo1" runat="server">
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capNOT_RENEW" runat="server">Do not Renew</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="chkNOT_RENEW" onclick="HideUnhideReason();" runat="server"></asp:CheckBox>
                                        </td>
                                        <td class="midcolora" style="width: 60%">
                                            <asp:Label ID="capNOT_RENEW_REASON" runat="server">Reason</asp:Label>
                                            <br />
                                            <asp:DropDownList ID="cmbNOT_RENEW_REASON" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trRenewalInfo2" runat="server">
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capREFER_UNDERWRITER" runat="server">Refer to underwriter</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="chkREFER_UNDERWRITER" onclick="CheckUncheckReferToUnderWriter();"
                                                runat="server"></asp:CheckBox>
                                        </td>
                                        <td class="midcolora" style="width: 60%">
                                            <asp:Label ID="capREFERAL_INSTRUCTIONS" runat="server">Referel Instractions(Max 250 characters)</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtREFERAL_INSTRUCTIONS" runat="server" MaxLength="250" TextMode="MultiLine"
                                                Rows="5" Columns="40"></asp:TextBox><br />
                                            <asp:CustomValidator ID="csvREFERAL_INSTRUCTIONS" Display="Dynamic" ControlToValidate="txtREFERAL_INSTRUCTIONS"
                                                runat="server" ClientValidationFunction="ChkTextAreaLength"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="headerEffectSystemParams" colspan="4">
                                            <asp:Label ID="lblRemarksHeader" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" style="width: 40%" id="tdRemarks">
                                            <asp:Label ID="capREMARKS" runat="server"></asp:Label>
                                            <br />
                                            <%--   by praveer panghal for itrack no 1430--%>
                                            <asp:TextBox ID="txtPOLICY_DESCRIPTION" runat="server" MaxLength="4000" TextMode="MultiLine"
                                                Width="550px" Rows="4"></asp:TextBox>
                                        </td>
                                        <td class="midcolora" colspan="2" style="width: 40%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%--<tr id="policyTR" runat="server" style="display:none">
					<td class="midcolora" colspan="1"><asp:label id="capPOLICY_TYPE" runat="server">Policy Type</asp:label><span class="mandatory">*</span>
					</td>
					<td class="midcolora" colspan="3"><asp:dropdownlist id="cmbPOLICY_TYPE" onfocus="SelectComboIndex('cmbPOLICY_TYPE')" runat="server"
							onchange="mandPropInspCredit();"></asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvPOLICY_TYPE" Display="Dynamic" ControlToValidate="cmbPOLICY_TYPE" Runat="server"></asp:requiredfieldvalidator></td>
				</tr>				
                <asp:label id="capRECEIVED_PRMIUM" runat="server">Received</asp:label>
                <br />
                <asp:textbox id="txtRECEIVED_PRMIUM" style="TEXT-ALIGN: right"  onblur="FormatAmount(this);" size="16" Runat="server" MaxLength="9"></asp:textbox>
		        <br />
		        <asp:regularexpressionvalidator id="revRECEIVED_PRMIUM" ControlToValidate="txtRECEIVED_PRMIUM" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>
				<tr style="display:none">
					<TD class="midcolora" width="18%"><asp:label id="capSTATE_NAME" runat="server">State</asp:label></TD>
					<TD class="midcolora" width="32%" colspan="3"><asp:textbox id="txtSTATE_NAME" runat="server" CssClass="midcolora" size="30" BorderStyle="None"
							ReadOnly="true"></asp:textbox><asp:dropdownlist id="cmbSTATE_ID" onfocus="SelectComboIndex('cmbSTATE_ID')" runat="server" AutoPostBack="True"></asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvSTATE_ID" runat="server" Display="Dynamic" ControlToValidate="cmbSTATE_ID" Enabled="false"
							ErrorMessage="State cannot be blank."></asp:requiredfieldvalidator></TD>
			    </tr>
				<tr style="display:none">
					<TD class="midcolora" width="16%"><asp:label id="capPROXY_SIGN_OBTAINED" Runat="server">Proxy Signature Obtained?</asp:label><span class="mandatory">*</span></TD>
					<TD class="midcolora" width="34%"><asp:dropdownlist id="cmbPROXY_SIGN_OBTAINED" onfocus="SelectComboIndex('cmbPROXY_SIGN_OBTAINED')"
							runat="server"></asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvPROXY_SIGN_OBTAINED" runat="server" Display="Dynamic" ControlToValidate="cmbPROXY_SIGN_OBTAINED" Enabled="false"
							ErrorMessage="PROXY SIGN OBTAINED can't be blank."></asp:requiredfieldvalidator></TD>
				</tr>				
				<tr style="display:none">
					<TD class="midcolora" width="18%"><asp:label id="capCHARGE_OFF_PRMIUM" runat="server">Charge Of Premium</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCHARGE_OFF_PRMIUM" Runat="server"></asp:dropdownlist></TD>					
				</tr>
				<tr id="trPropInspCr" runat="server" style="display:none">
					<TD class="midcolora" width="18%"><asp:label id="capPROPRTY_INSP_CREDIT" runat="server">Property Inspection/Cost Estimator </asp:label><span class="mandatory" id="spnPROPRTY_INSP_CREDIT" runat="server">*</span></TD>
					<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPROPRTY_INSP_CREDIT" onfocus="SelectComboIndex('cmbPROPRTY_INSP_CREDIT')"
							Runat="server"></asp:dropdownlist><BR>
						<asp:requiredfieldvalidator id="rfvPROPRTY_INSP_CREDIT" Display="Dynamic" ControlToValidate="cmbPROPRTY_INSP_CREDIT" Enabled="false"
							Runat="server"></asp:requiredfieldvalidator></TD>
					<td class="midcolora" width="16%"><asp:label id="capPIC_OF_LOC" Runat="server"></asp:label><span class="mandatory" id="spnPIC_OF_LOC" runat="server">*</span></td>
					<td class="midcolora" width="34%"><asp:dropdownlist id="cmbPIC_OF_LOC" onfocus="SelectComboIndex('cmbPIC_OF_LOC')" Runat="server"></asp:dropdownlist><br>
						<asp:requiredfieldvalidator id="rfvPIC_OF_LOC" Display="Dynamic" Enabled="false" ControlToValidate="cmbPIC_OF_LOC" Runat="server"></asp:requiredfieldvalidator></td>
				</tr>
				<tr id="trCOMPLETE_APP" runat="server" style="display:none">
					<TD class="midcolora" width="18%"><asp:label id="capCOMPLETE_APP" runat="server">Complete Application Bonus Applies </asp:label></TD>
					<TD class="midcolora" colspan="3"><asp:checkbox id="chkCOMPLETE_APP" runat="server"></asp:checkbox><BR>
					</TD>
				</tr>
				<tr style="display:none">
					<TD class="midcolora" width="18%"><asp:label id="capYEAR_AT_CURR_RESI" runat="server">Year at Current Residence</asp:label></TD>
					<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR_AT_CURR_RESI" runat="server" size="4" maxlength="2"></asp:textbox><br>
						<asp:regularexpressionvalidator Enabled="False" id="revYEAR_AT_CURR_RESI" runat="server" Display="Dynamic" ControlToValidate="txtYEAR_AT_CURR_RESI"></asp:regularexpressionvalidator>
						<asp:rangevalidator id="rngYEAR_AT_CURR_RESI" runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtYEAR_AT_CURR_RESI" MaximumValue="99" MinimumValue="1" Type="Integer"></asp:rangevalidator>
					</TD>
					<TD class="midcolora" width="16%"><asp:label id="capYEARS_AT_PREV_ADD" runat="server">Previous Address if less than 3 years(Max 250 characters)</asp:label><span class="mandatory" id="spnYEARS_AT_PREV_ADD">*</span></TD>
					<TD class="midcolora" width="34%"><asp:textbox id="txtYEARS_AT_PREV_ADD" runat="server" TextMode="MultiLine" Rows="5" Columns="40"></asp:textbox><BR>
						<asp:customvalidator id="csvYEARS_AT_PREV_ADD" Display="Dynamic" ControlToValidate="txtYEARS_AT_PREV_ADD" Enabled="false"
							Runat="server" ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator><asp:requiredfieldvalidator id="rfvYEARS_AT_PREV_ADD" runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtYEARS_AT_PREV_ADD"></asp:requiredfieldvalidator></TD>
				</tr>
				<tr id="trBILL_MORTAGAGEE" runat="server" style="display:none">
					<td class="midcolora" width="18%"><asp:label id="capBILL_MORTAGAGEE" Runat="server">Mortgagee</asp:label></td>
					<td class="midcolora" width="32%"><asp:label id="lblBILL_MORTAGAGEE" Runat="server">N.A.</asp:label></td>
					<td class="midcolora" colspan="2"></td>
				</tr>
				<tr id="trREINSURANCE" runat="server" style="display:none">
					<td class="midcolora" width="18%"><asp:label id="capREINS_SPECIAL_ACPT" Runat="server">Reinsurance Special Acceptances</asp:label></td>
					<td class="midcolora" width="32%"><asp:dropdownlist id="cmbREINS_SPECIAL_ACPT" runat="server"></asp:dropdownlist></td>
					<td class="midcolora" colspan="2"></td>
				</tr>
				<asp:label id="capCOMPLETE_APP" runat="server">Policy Level Comission Applies</asp:label>
		            <br />
		            <asp:checkbox id="chkCOMPLETE_APP" runat="server" onclick="javascript:CommChange();"></asp:checkbox>
		        <td class="midcolora" colspan="3">
		            <asp:label id="capContact_Person" runat="server">Contact Person</asp:label>
		            <br />
		             <asp:DropDownList ID="cmbContact_Person" runat="server"></asp:DropDownList>
		        </td>
				<tr style="display:none">
					<td class="headerEffectSystemParams" colspan="4"><asp:label id="lblAllPoliciesHeader" runat="server">Any other policy with Wolverine</asp:label></td>
				</tr>
				<tr style="display:none">
					<td class="midcolora" width="18%"><asp:label id="lblPolicies" runat="server">All the policies with Wolverine</asp:label></td>
					<td class="midcolora" width="32%"><asp:label id="lblAllPolicy" runat="server"></asp:label></td>
					<td class="midcolora" width="16%"><asp:label id="lblPoliciesDiscount" runat="server">Policies eligible for discount</asp:label></td>
					<td class="midcolora" width="34%"><asp:label id="lblEligbilePolicy" runat="server"></asp:label></td>
				</tr>--%>
                        <tr>
                            <td class="midcolora" align="left" colspan="2">
                                <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" CausesValidation="false"
                                    Text="Reset"></cmsb:CmsButton>&nbsp;
                                <cmsb:CmsButton class="clsButton" ID="btnAppQuote" runat="server" CausesValidation="false"
                                    Text="Application Quote" Visible="false"></cmsb:CmsButton><cmsb:CmsButton class="clsButton"
                                        ID="btnPolicyQuote" runat="server" CausesValidation="false" Text="Policy Quote"
                                        Visible="false"></cmsb:CmsButton>
                            </td>
                            <td class="midcolorr" align="right" colspan="2">
                                <cmsb:CmsButton class="clsButton" ID="btnSendCancelNotice" runat="server" OnClientClick="ShowMsg();return false"
                                    Visible="false" CausesValidation="false" Text="Send Cancellation Notice" OnClick="btnSendCancelNotice_Click">
                                </cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnCopy" runat="server" Visible="false" CausesValidation="false"
                                    Text="Copy"></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnReject" runat="server" CausesValidation="false"
                                    Visible="false" Text="Reject" />
                                <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save"></cmsb:CmsButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" colspan="2">
                                <cmsb:CmsButton class="clsButton" ID="btnBack" runat="server" Text="Back To Search">
                                </cmsb:CmsButton>
                            </td>
                            <td class="midcolorr" colspan="2">
                                <cmsb:CmsButton class="clsButton" ID="btnCopyPolicy" runat="server" Text="Copy Policy">
                                </cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnCustomerAssistant" runat="server" Text="Back To Customer Assistant">
                                </cmsb:CmsButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <input id="hidFormSaved" type="hidden" name="hidFormSaved" runat="server" />
                <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server" />
                <input id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server" />
                <input id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server" />
                <input id="hidAPP_AGENCY_ID" type="hidden" value="0" name="hidAPP_AGENCY_ID" runat="server" />
                <input id="hidCSR" type="hidden" value="0" name="hidCSR" runat="server" />
                <input id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server" />
                <input id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server" />
                <input id="hidLOBID" type="hidden" value="0" name="hidLOBID" runat="server" />
                <input id="hidSUB_LOB" type="hidden" value="0" name="hidSUB_LOB" runat="server" />
                <input id="hidLOBXML" type="hidden" value="0" name="hidLOBXML" runat="server" />
                <input id="hidStateID" type="hidden" value="0" name="hidStateID" runat="server" />
                <input id="hidUnderwriter" type="hidden" value="0" name="hidUnderwriter" runat="server" />
                <input id="hidDepartmentXml" type="hidden" name="hidDepartmentXml" runat="server" />
                <input id="hidProfitCenterXml" type="hidden" name="hidProfitCenterXml" runat="server" />
                <input id="hidDEPT_ID" type="hidden" name="hidDEPT_ID" runat="server" />
                <input id="hidPC_ID" type="hidden" name="hidPC_ID" runat="server" />
                <input id="hidReset" type="hidden" name="hidReset" runat="server" />
                <input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server" />
                <input id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server" />
                <input id="hidIs_Agency" type="hidden" value="0" name="hidIs_Agency" runat="server" />
                <input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server" />
                <input id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server" />
                <input id="hidRefresh" type="hidden" value="0" name="" runat="server" />
                <input id="hidPolicyStatus" type="hidden" value="0" name="hidPolicyStatus" runat="server" />
                <input id="hidAPP_INCEPTION_DATE" type="hidden" value="0" name="hidAPP_INCEPTION_DATE"
                    runat="server" />
                <input id="hidFULL_PAY_PLAN_ID" type="hidden" name="hidFULL_PAY_PLAN_ID" runat="server" />
                <input id="hidBillingPlan" type="hidden" name="hidBillingPlan" runat="server" />
                <input id="hidPRODUCER" type="hidden" value="0" name="hidPRODUCER" runat="server" />
                <input id="hidDOWN_PAY_MODE" type="hidden" name="hidDOWN_PAY_MODE" runat="server" />
                <input id="hidINSTALL_PLAN_ID" type="hidden" name="hidINSTALL_PLAN_ID" runat="server" />
                <input id="hidPOLICY_STATUS" type="hidden" name="hidPOLICY_STATUS" runat="server" />
                <input id="hidAgencyCode" type="hidden" name="hidAgencyCode" runat="server" />
                <input id="hidPOLICY_STATUS_CODE" type="hidden" name="hidPOLICY_STATUS_CODE" runat="server" />
                <input id="hidDEACTIVE_INSTALL_PLAN_ID" type="hidden" name="hidDEACTIVE_INSTALL_PLAN_ID"
                    runat="server" />
                <input id="hidBILLTYPE" type="hidden" name="hidBILLTYPE" runat="server" />
                <input id="hidPolTermEffDate" type="hidden" value="0" name="hidPolTermEffDate" runat="server" />
                <input id="hidPAYOR" type="hidden" value="0" name="hidPAYOR" runat="server" />
                <input id="hidRejectMsg" type="hidden" value="" name="hidRejectMsg" runat="server" />
                <input id="hidREJECT_REASON_ID" type="hidden" value="" name="hidREJECT_REASON_ID"
                    runat="server" />
                <input id="hidPOLDOWN_PAY_MODE" runat="server" type="hidden" value="0" />
                <input id="hidPOL_TRAN_TYPE" name="hidPOL_TRAN_TYPE" runat="server" type="hidden"
                    value="0" />
                <input id="hidFUND_TYPE" runat="server" type="hidden" value="0" />
            </td>
        </tr>
    </table>
    </form>
    <!-- For lookup layer -->
    <div id="lookupLayerFrame" style="display: none; z-index: 101; position: absolute">
        <iframe id="lookupLayerIFrame" style="display: none; z-index: 1001; background-color: #000000"
            width="0" height="0" top="0px;" left="0px"></iframe>
    </div>
    <div id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="z-index: 101;
        visibility: hidden; position: absolute">
        <table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
            <tr class="SubTabRow">
                <td>
                    <b>Add LookUp</b>
                </td>
                <td>
                    <p align="right">
                        <a onclick="javascript:hideLookupLayer();" href="javascript:void(0)">
                            <img height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></a></p>
                </td>
            </tr>
            <tr class="SubTabRow">
                <td colspan="2">
                    <span id="LookUpMsg"></span>
                </td>
            </tr>
        </table>
    </div>
    <!-- For lookup layer ends here-->
    <script type="text/javascript" language="javascript">
        function setDelayMenu() {
            setMenu();
        }
        window.setTimeout('setDelayMenu()', 1);
    </script>
</body>
</html>
