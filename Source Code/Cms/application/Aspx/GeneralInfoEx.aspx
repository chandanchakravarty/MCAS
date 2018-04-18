<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="CmsTimer" Src="/cms/cmsweb/webcontrols/CmsTimePicker.ascx" %>
<%@ Page language="c#" Codebehind="GeneralInfoEx.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.Application.Aspx.GeneralInfoEx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>APP_LIST</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)">
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript" src="/cms/cmsweb/Scripts/Calendar.js"></script>
		<script language="vbscript">
			Function getUserConfirmationForDelete				
				getUserConfirmationForDelete= msgbox("Are you sure you want to delete this application?",35,"CMS")
			End function
		</script>
		<script language="javascript"> 
		var refWindow;
		
		//shows the submit anyway input messages
		var refSubmitWin;
		var mortgageeBill = '11276';
		var InsuredMortgageeBill = '11278';	
		
		function cmbAPP_TERMS_Change()
		{
			if(document.getElementById("cmbAPP_TERMS").selectedIndex!="-1" && document.getElementById("cmbAPP_TERMS").options[document.getElementById("cmbAPP_TERMS").selectedIndex].value!="")
				__doPostBack("cmbAPP_TERMS_Change","1");				
			else
				return false;
		}
		
		function ShowAlertMessageForDelete()
		{
		    var returnValue;			
			returnValue= getUserConfirmationForDelete();
			if(returnValue==6) 
     			return true;
			else 
	     		return false;
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
		
		function ShowDialogEx()
		{
		
			if(parent.refSubmitWin !=null)
			{
				parent.refSubmitWin.close();
			}
			parent.refSubmitWin=window.open('/cms/application/Aspx/ShowDialog.aspx','BRICS','resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');
			wBody=parent.refSubmitWin.document.body;			
			parent.refSubmitWin.document.write(document.getElementById("hidHTML").value);			
			parent.refSubmitWin.document.title = "BRICS - Rules Mandatory Information";
		}
		// Shows policy mandatory information
		function ShowPolicyMsg()
		{
			if(parent.refSubmitWin !=null)
			{
				parent.refSubmitWin.close();
			}
			parent.refSubmitWin=window.open('/cms/application/Aspx/ShowDialog.aspx?CALLEDFROM=POLNEWBUSINESS','BRIC','resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');
			wBody=parent.refSubmitWin.document.body;			
			parent.refSubmitWin.document.write(document.getElementById("hidPOLHTML").value);
			parent.refSubmitWin.document.title = "BRICS - Rules Mandatory Information";
		}		
		
		var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		var lobMenuArr	= new Array();
		lobMenuArr[1]	= 2;
		lobMenuArr[2]	= 0;
		lobMenuArr[3]	= 1;
		lobMenuArr[4]	= 4;
		lobMenuArr[5]	= 5;
		lobMenuArr[6]	= 3;		
		lobMenuArr[7]	= 6;
		
		<% if(gIntShowVerificationResult==1 )	{%>			
			ShowDialog();			
		<%}
		%>
		
		function ShowDialog()
		{		
			if(parent.refWindow != null)
			{
				parent.refWindow.close();				
			}				
			
			parent.refWindow = window.open('/cms/application/Aspx/ShowDialog.aspx?CUSTOMER_ID=' + document.getElementById('hidCustomerID').value +"&APP_ID=" + document.getElementById('hidAppID').value +"&APP_VERSION_ID="+ document.getElementById('hidAppVersionID').value  + "&LOBID="+ document.getElementById('hidLOBID').value  + "&QUOTE_ID="+ <%= gIntQuoteID %>+"&SHOW="+ <%= gIntShowQuote %> ,'Quote'," left=150,top=50,resizable=yes, scrollbars=yes,width=800,height=500");
		}		
		<% if(gIntShowQuote==1 ||gIntShowQuote==2 || gIntShowQuote==3)	{%>			
			ShowQuote();		
		<%}
		%>
		// <start> added by ashwani on 01 Mar 2006
		<% if(gIntShowQuote==4)	{%>			
			alert('Application is rejected based on certain rules, so quote cannot be generated.');
		<%}
		%>
		// <end>
		
		function ShowQuote()
		{				
				ShowPopup('/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID='+<%= gIntCUSTOMER_ID %> +"&APP_ID="+<%= gIntAPP_ID %>+"&APP_VERSION_ID="+ <%= gIntAPP_VERSION_ID %>  +    "&LOBID="+ <%= gstrLobID %>  + "&QUOTE_ID="+ <%= gIntQuoteID %>+"&SHOW="+ <%= gIntShowQuote %> ,'Quote', 700, 600);
		}
		
		function AddData()
		{ 
			document.getElementById('hidCustomerID').value	=	'NEW';
			document.getElementById('hidAppID').value	=	'NEW';
			document.getElementById('txtAPP_STATUS').innerText = 'Incomplete';
			document.getElementById('txtAPP_NUMBER').value  = '';
			document.getElementById('txtAPP_VERSION').value  = '1.0';
			document.getElementById('txtYEAR_AT_CURR_RESI').value  = '';
			document.getElementById('txtYEARS_AT_PREV_ADD').value  = '';
			document.getElementById('cmbAPP_TERMS').options.selectedIndex = -1;
			document.getElementById('txtAPP_INCEPTION_DATE').value  = '';//DateAdd();
			document.getElementById('txtAPP_EFFECTIVE_DATE').value  = DateAdd();
			document.getElementById('txtAPP_EXPIRATION_DATE').value  = DateAdd();
			document.getElementById('cmbAPP_LOB').options.selectedIndex = -1;
			document.getElementById('cmbAPP_SUBLOB').options.selectedIndex = '';
			document.getElementById('cmbAPP_SUBLOB').options.text = '';
			document.getElementById('cmbCSR').options.selectedIndex = -1;
			document.getElementById('cmbProducer').options.selectedIndex=-1;
			//document.getElementById('cmbAPP_AGENCY_ID').options.selectedIndex = -1; //Commented by Charles on 21-Aug-09 for APP/POL Optimization
			document.getElementById('hidSUB_LOB').value='';
			document.getElementById('txtAPP_NUMBER').value  ='To be generated';
			document.getElementById('cmbBILL_TYPE_ID').options.selectedIndex=-1;
			document.getElementById('txtRECEIVED_PRMIUM').value='';
			document.getElementById('cmbPROXY_SIGN_OBTAINED').options.selectedIndex=-1;
			document.getElementById('cmbPIC_OF_LOC').options.selectedIndex=-1;
			
			if(document.getElementById('hidCultureName').value!="" && document.getElementById('hidCultureName').value=="pt-BR")
			{
				document.getElementById('lblUNDERWRITER').innerText="A ser atribuído";
			}
			else
			{
				document.getElementById('lblUNDERWRITER').innerText="To be Assigned";
			}
			ChangeColor();
			DisableValidators();
			 
			
		}

function setTab()
{
	 	  
	if (document.getElementById('hidFormSaved')!=null && document.getElementById('hidOldData')!= null)
	{ 
		if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != ""))
		{
			var SelectedLOBID;
			
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
			
			
			URL = "GeneralInfoEx.aspx?CUSTOMER_ID=" + CustomerID  
				+ "&APP_ID= " + AppID  
				+ "&APP_VERSION_ID=" + AppVersionID
				+ "&CALLEDFROM=" + document.getElementById("hidCalledFrom").value
				+ "&transferdata="
			RemoveTab(1,top.frames[1]);		
			
			var AppInfo = ""
			var CoApp = ""
			var Attach = ""
			var BillInfo = ""
			
			switch(cultureName)
			{
				case "pt-BR":
					AppInfo = "Aplicativo Informação"
					CoApp = "Co-Recorrente Detalhes"
					Attach = "Ligação"
					BillInfo = "Faturamento Informação" 
					break;
				case "en-US":
				default: 
					AppInfo = "Application Information";
					CoApp = "Co-Applicant Details";
					Attach = "Attachment";
					BillInfo = "Billing Info";
					break;
			}
			
			DrawTab(1,top.frames[1],AppInfo,URL);		
			
			if (document.getElementById('hidLOBID')!=null)
			{			
				SelectedLOBID=document.getElementById('hidLOBID').value;	
				if(SelectedLOBID =="5")
				{	
				
					Url="AddApplicantInsured.aspx?CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&LOB_ID="+LobID+"&"
					DrawTab(2,top.frames[1],CoApp,Url);		
					
						//added by vj				
						var customerID = '<%=gIntCUSTOMER_ID%>'
						var ApplicationID = '<%=gIntAPP_ID%>'
						var ApplicationVerID = '<%=gIntAPP_VERSION_ID %>'
						
						if(AppID =='NEW')
						{	 
							var ApplicationID = '<%=gIntAPP_ID%>'
							var ApplicationVerID = '<%=gIntAPP_VERSION_ID %>'							
							Url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=Application&EntityType=Application&EntityId=0&CUSTOMER_ID=" + CustomerID +"&APP_ID="+ ApplicationID + "&APP_VERSION_ID="+ ApplicationVerID  + "&";
						}
						else
						{
							Url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=Application&EntityType=Application&EntityId=0&CUSTOMER_ID=" + CustomerID +"&APP_ID="+ AppID + "&APP_VERSION_ID="+ AppVersionID  + "&";
						}
						//Url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=Application&EntityType=Application&EntityId=0&CUSTOMER_ID=" + customerID +"&APP_ID="+ ApplicationID + "&APP_VERSION_ID="+ ApplicationVerID  + "&";
						DrawTab(3,top.frames[1],Attach,Url);
						//Commented By Manoj Rathore Itrack No. 3776
						
						//DrawTab(4,top.frames[1],'Declaration Page','DecPage.aspx');
						RemoveTab(4,top.frames[1]);
						Url="InstallmentInfo.aspx?CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&LOB_ID="+LobID+"&";
						//Commented By Manoj Rathore Itrack No. 3776
						//DrawTab(5,top.frames[1],'Billing Info',Url);
						DrawTab(4,top.frames[1],BillInfo,Url);
						//DrawTab(5,top.frames[1],'InstallmentInfo',Url);
																					
				}
				else
				{
					if(SelectedLOBID != "5" && SelectedLOBID != "0" )
					{
						Url="AddApplicantInsured.aspx?CUSTOMER_ID="+CustomerID+"&APP_ID="+AppID+"&APP_VERSION_ID="+AppVersionID+"&LOB_ID="+LobID+"&"
						DrawTab(2,top.frames[1],CoApp,Url);							
						
						//added by vj				
						var ApplicationID = '<%=gIntAPP_ID%>'
						var ApplicationVerID = '<%=gIntAPP_VERSION_ID %>'
						
						if(AppID =='NEW')
						{	
														
							Url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=Application&EntityType=Application&EntityId=0&CUSTOMER_ID=" + CustomerID +"&APP_ID="+ ApplicationID + "&APP_VERSION_ID="+ ApplicationVerID  + "&";
						}
						else
						{
							Url="/cms/cmsweb/Maintenance/AttachmentIndex.aspx?calledfrom=Application&EntityType=Application&EntityId=0&CUSTOMER_ID=" + CustomerID +"&APP_ID="+ AppID + "&APP_VERSION_ID="+ AppVersionID  + "&";
						}
						DrawTab(3,top.frames[1],Attach,Url);						
											
						RemoveTab(4,top.frames[1]);

						Url="InstallmentInfo.aspx?CUSTOMER_ID=" + CustomerID +"&APP_ID="+ AppID + "&APP_VERSION_ID="+ AppVersionID  + "&";
												
						DrawTab(4,top.frames[1],BillInfo,Url);
					
					}
					else
					{
						RemoveTab(3,top.frames[1]);
						RemoveTab(2,top.frames[1]);
					}					
				}
			}
		}
		else
		{				
			RemoveTab(2,top.frames[1]);
			RemoveTab(3,top.frames[1]);
			RemoveTab(4,top.frames[1]);
		}
	}
}

		function DoBack()
		{
			this.parent.document.location.href = "/Cms/Client/Aspx/CustomerManagerSearch.aspx";
			return false;
		}
		function DoBackToAssistant()
		{
			this.parent.document.location.href = "/Cms/Client/Aspx/CustomerManagerIndex.aspx";
			return false;
		}
		function SetValidationControl()
		{
		    if(document.getElementById('hidIs_Agency') != null && document.getElementById('hidIs_Agency').value == "1")
		    {
				document.getElementById("csvAPP_EFFECTIVE_DATE").setAttribute('enabled',true); 
				document.getElementById("csvAPP_EFFECTIVE_DATE").setAttribute('isValid',true); 
			}
		    else
		    {
				document.getElementById("csvAPP_EFFECTIVE_DATE").setAttribute('isValid',false); 
				document.getElementById("csvAPP_EFFECTIVE_DATE").style.display='none'; 
				document.getElementById("csvAPP_EFFECTIVE_DATE").setAttribute('enabled',false); 
		    }	
		}
		function populateXML()
		{	
			SetValidationControl();
	
			//Disabling the policy type in edit mode
			if (document.getElementById('hidOldData').value != "" && document.getElementById('hidOldData').value != "0")
			{ 
			
				if(document.getElementById('hidCallefroms').value=="HOME" || document.getElementById('hidCallefroms').value=="RENT")
				{
					document.getElementById('cmbPOLICY_TYPE').disabled="true";
					
					for(i=0;i<document.getElementById('cmbPOLICY_TYPE').options.length;i++)
					{
						if(document.getElementById('cmbPOLICY_TYPE').options[i].value==document.getElementById('hidPOLICY_TYPE').value)	
							document.getElementById('cmbPOLICY_TYPE').options[i].selected=true;
					}
				}
			}
			
							
			if (document.getElementById('trDETAILS')!=null  && (document.getElementById('trDETAILS').style.display == "inline" || document.getElementById('trDETAILS').style.display == "") )
			{ 
				
				if(document.getElementById('hidFormSaved')!=null &&( document.getElementById('hidFormSaved').value == '0'||document.getElementById('hidFormSaved').value == '1'))
				{	
					
					// control  is coming either at formload or after saving 
					
					var tempXML;
					if(document.getElementById('hidOldData')!=null)
					{		
						tempXML=document.getElementById('hidOldData').value;		
						//alert(tempXML);
						if(tempXML!="" && tempXML!=0)
						{
							//update mode
							populateFormData(tempXML,APP_LIST);								
							 
							//set the  hidden sublob
							document.getElementById('hidSUB_LOB').value = document.APP_LIST.cmbAPP_SUBLOB.value;				 
							//show the label for LOB since it is in update mode. combo box not required.
							document.getElementById('txtLOB').style.display ="inline";						
							document.getElementById('cmbAPP_LOB').style.display ="none";						
							document.getElementById('cmbSTATE_ID').disabled="true";						   
							
							//show the label for customer.lookup not required. 						
							document.getElementById('txtCUSTOMER_NAME').style.display ="none";	 
							document.getElementById('imgSelect').style.display ="none";								
						
						     if (document.getElementById('hidDOWN_PAY_MODE').value !=='')
							{
								for(i=0;i<document.getElementById('cmbDOWN_PAY_MODE').options.length;i++)
								{
									if(document.getElementById('cmbDOWN_PAY_MODE').options[i].value==document.getElementById('hidDOWN_PAY_MODE').value)	
										document.getElementById('cmbDOWN_PAY_MODE').options[i].selected=true;
								}
							}			
								
							if(document.getElementById('txtRECEIVED_PRMIUM').value=="0.00")
								document.getElementById('txtRECEIVED_PRMIUM').value="";	
								
						}
						else
						{ 			 	
							
							document.getElementById('txtCUSTOMER_NAME').style.display ="none";	 
							document.getElementById('imgSelect').style.display ="none";
							document.getElementById('txtCUSTOMERNAME').style.display ="inline";								
							document.getElementById('txtLOB').style.display ="none";						
							document.getElementById('cmbAPP_LOB').style.display ="inline";			 	
							document.getElementById('cmbSTATE_ID').disabled="false";
							if(document.getElementById('hidCallefroms').value=="HOME" || document.getElementById('hidCallefroms').value=="RENT")
							{
							document.getElementById('cmbPOLICY_TYPE').disabled="false";
							
							}	 
							 
						}
					}
				}
				else
				{ 						
				
					if (document.getElementById('hidCustomerID') != null && document.getElementById('hidCustomerID').value != 'NEW')
					{  	
						if (document.getElementById('hidFormSaved')!= null &&(document.getElementById('hidFormSaved').value!=1 && document.getElementById('hidFormSaved').value!=3) )
						{				
							
							document.getElementById('txtCUSTOMER_NAME').style.display ="none";	 
							document.getElementById('imgSelect').style.display ="none";	 	
			 				if (document.getElementById('txtCUSTOMERNAME')!=null)
							{			 
								document.getElementById('txtCUSTOMERNAME').style.display ="inline";		
								document.getElementById('cmbAPP_LOB').style.display ="inline";
								
								document.getElementById('txtLOB').style.display ="none";			 		 	
								
							}
							else
							{ 
								document.getElementById('txtCUSTOMER_NAME').style.display ="inline";	 
								document.getElementById('imgSelect').style.display ="inline";	 	
								
								document.getElementById('cmbAPP_LOB').style.display ="none";
								
			 					if (document.getElementById('txtLOB')!=null)
								{			 
									document.getElementById('txtLOB').style.display ="inline";
									
								}
								else
								{
									document.getElementById('cmbAPP_LOB').style.display ="inline";									
								}					
							}			
						}
						else
						{	     
							
							if (document.getElementById('hidAppID').value !='NEW')
							{
								document.getElementById('txtCUSTOMERNAME').style.display ="inline";
								document.getElementById('txtCUSTOMER_NAME').style.display ="none";	 
								document.getElementById('imgSelect').style.display ="none";	 	
								
								document.getElementById('cmbAPP_LOB').style.display ="none";
								document.getElementById('txtLOB').style.display ="inline";
								for(i=0;i<document.getElementById('cmbSTATE_ID').options.length;i++)
								{
									if(document.getElementById('cmbSTATE_ID').options[i].value==document.getElementById('hidSTATE_ID').value)	
										document.getElementById('cmbSTATE_ID').options[i].selected=true;
								}
								document.getElementById('cmbSTATE_ID').disabled="true";								
								
							}
							else
							{
								document.getElementById('txtCUSTOMER_NAME').style.display ="inline";	 
								document.getElementById('imgSelect').style.display ="inline";	 	
								document.getElementById('txtCUSTOMERNAME').value ='';
								
								document.getElementById('cmbAPP_LOB').style.display ="inline";
								document.getElementById('txtLOB').value ='';
								document.getElementById('cmbSTATE_ID').disabled="false";								
							}
						}				 
					} 
					else
					{						 
						// control is coming from the customer section
						if(document.getElementById('hidFormSaved')!= null  && document.getElementById('hidFormSaved').value=='4')
						{
							document.getElementById('txtCUSTOMERNAME').style.display ="inline";
							document.getElementById('txtCUSTOMER_NAME').style.display ="none";	 
							document.getElementById('imgSelect').style.display ="none";	 	
							
							document.getElementById('cmbAPP_LOB').style.display ="none";
							document.getElementById('cmbSTATE_ID').disabled="true";
							document.getElementById('txtCUSTOMERNAME').style.display ="inline";
							
						}
						else
						{						 
							document.getElementById('txtCUSTOMER_NAME').style.display ="inline";	 
							document.getElementById('imgSelect').style.display ="inline";	 	
							document.getElementById('cmbSTATE_ID').disabled="false";
							document.getElementById('txtLOB').style.display ="inline";
							document.getElementById('cmbAPP_LOB').style.display="none";
							
						}
					}
				}
				
				if(document.getElementById("hidSUB_LOB")!=null)
				{
					var SUBLOBID =document.getElementById("hidSUB_LOB").value;
					SelectComboOption("cmbAPP_SUBLOB",SUBLOBID);		
				}
				if(document.getElementById("hidCSR")!=null)
				{
					var CSR = document.getElementById("hidCSR").value;
					SelectComboOption("cmbCSR",CSR);		 
				}
				//Added at 30 Nov 2006 
				if(document.getElementById("hidProducer")!=null)
				{
					var Producer= document.getElementById("hidProducer").value;
					SelectComboOption("cmbProducer",Producer);
				}				 
				if (document.getElementById('hidLOBID')!=null)
				{
					SelectedLOBID=document.getElementById('hidLOBID').value;	
					//alert(SelectedLOBID);
					if(SelectedLOBID!="1" || SelectedLOBID!="6") // Home Or Rental
					{
					
							document.getElementById('trPropInspCr').style.display='none';
					}
					else
					{
					
					 	document.getElementById('trPropInspCr').style.display='inline';
					 	mandPropInspCredit();
					}								
						
				}
				
				if ( document.getElementById("hidOldData").value == "0")
				{
					document.getElementById('txtLOB').style.display ="none";
					document.getElementById('cmbAPP_LOB').style.display="inline";
					
				}
				else
				{
					document.getElementById('txtLOB').style.display ="inline";
					document.getElementById('cmbAPP_LOB').style.display="none";
				}
				
				var LOBID	= document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].value;
				if (LOBID=="1" || LOBID=="6") // Home or Rental
				{	
					document.getElementById('trPropInspCr').style.display='inline';
					mandPropInspCredit();
				}
				else
				{
				  	document.getElementById('trPropInspCr').style.display='none';
				}
				 if (document.getElementById('hidDOWN_PAY_MODE').value !=='')
					{
							for(i=0;i<document.getElementById('cmbDOWN_PAY_MODE').options.length;i++)
							{
								if(document.getElementById('cmbDOWN_PAY_MODE').options[i].value==document.getElementById('hidDOWN_PAY_MODE').value)	
									document.getElementById('cmbDOWN_PAY_MODE').options[i].selected=true;
							}
					}
			}
			
			//Display the value of underwriter only when policy has been created and underwriter saved			
			
			if(document.getElementById('hidPOLICY_ID')==null || document.getElementById('hidPOLICY_ID').value=="")
			{
				if(document.getElementById('lblUNDERWRITER').innerText=="" || document.getElementById('lblUNDERWRITER').innerText=="0")
				{
					if(document.getElementById('hidCultureName').value!="" && document.getElementById('hidCultureName').value=="pt-BR")
					{
						document.getElementById('lblUNDERWRITER').innerText="A ser atribuído";
					}
					else
					{
						document.getElementById('lblUNDERWRITER').innerText="To be Assigned";
					}
				}
			}
			
			return false;
			var temp;
			temp =1;
		}

	 	function FillSubLOB()
		{   		//if (document.getElementById('hidFormSaved')!= null  && document.getElementById('hidFormSaved').value=='3')

		if (document.getElementById('trDETAILS')!=null  && (document.getElementById('trDETAILS').style.display == "inline" || document.getElementById('trDETAILS').style.display == "") )
		{
			document.getElementById('cmbAPP_SUBLOB').innerHTML = '';
			var Xml = document.getElementById('hidLOBXML').value;
			
			var LOBId ="";
			var stID="";
			
			if(document.getElementById('cmbAPP_LOB').style.display=="inline")
			{
				if (document.getElementById('cmbAPP_LOB').selectedIndex!= -1)
				{
					LOBId	= document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].value;
					stID	= document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
				}
			}
			else
			{
					LOBId	= document.getElementById('hidLOBId').value;
					stID	= document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
			}	
			 
			//alert(LOBId + "-----" + stID)
			//LOB id is not selected then returning 
			if(document.getElementById('cmbAPP_LOB').selectedIndex == -1)
			{
				document.getElementById('hidLOBId').value = '';
				return false;
			}
			
			//Inserting the lobid in hidden control
			//document.getElementById('hidLOBId').value = document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].value;
			
			var objXmlHandler = new XMLHandler();
			if(Xml!="")
			{
				var tree = objXmlHandler.quickParseXML(Xml).childNodes[0];
				
				//adding a blank option
				oOption = document.createElement("option");
				oOption.value = "";
				oOption.text = "";
				document.getElementById('cmbAPP_SUBLOB').add(oOption);
				 
				for(i=0; i<tree.childNodes.length; i++)
				{
					nodValue = tree.childNodes[i].getElementsByTagName('LOB_ID');
					stateValue = tree.childNodes[i].getElementsByTagName('STATE_ID');
					if (nodValue != null)
					{
						if (nodValue[0].firstChild == null)
							continue
						//alert(nodValue[0].firstChild.text + "----" + stateValue[0].firstChild.text)						
						if ((nodValue[0].firstChild.text == LOBId) && stateValue[0].firstChild.text==stID)
						{
							
							SubLobId = tree.childNodes[i].getElementsByTagName('SUB_LOB_ID');
							SubLobDesc = tree.childNodes[i].getElementsByTagName('SUB_LOB_DESC');
							
							if (SubLobId != null && SubLobDesc != null)
							{
								if ((SubLobId[0] != null || SubLobId[0] == 'undefined' ) 
									&&  (SubLobDesc[0] != null || SubLobDesc[0] == 'undefined'))
								{
									oOption = document.createElement("option");
									oOption.value = SubLobId[0].firstChild.text;
									oOption.text = SubLobDesc[0].firstChild.text;
									document.getElementById('cmbAPP_SUBLOB').add(oOption);
								}
							}
						}
					}
				}
			}
			document.getElementById('cmbAPP_SUBLOB').selectedIndex=-1;			
			 
			// hide and display the checkbox as per the selection of LOB	 
			
			
			if(LOBId!="1" || LOBId!="6")  // Is not Home
			{
				
				document.getElementById('trPropInspCr').style.display='none';
				
			}	
			else
			{	
				document.getElementById('trPropInspCr').style.display='inline';
				//mandPropInspCredit();
			}			 
			 
		}
		
	}
	
	function fillBillingPlan()
	{
		if (document.getElementById('cmbAPP_TERMS').options.selectedIndex==-1)
			return false;
		var ctrl = document.getElementById('cmbBILL_TYPE_ID');
		var nodValueColor;
		
		if (ctrl == null)
						return;
			
    	var policyTerm =document.getElementById('cmbAPP_TERMS').options[document.getElementById('cmbAPP_TERMS').options.selectedIndex].value;
    	var termXML=document.getElementById('hidBillingPlan').value;
    	
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
						//IsActive = tree.childNodes[i].getElementsByTagName('IS_ACTIVE');
						if (PlanID != null && PlanDesc != null)
						{
							if (PlanID[0] != null &&  PlanDesc[0] != null )
							{
								oOption = document.createElement("option");
								oOption.value = PlanID[0].firstChild.text;// + '^' +  IsActive[0].firstChild.text;
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
				}
			}
		BillType();
	// }	
	 
	return false;
	} 
	
	function SetBillTypeFlag()
	{ 
	   document.getElementById('hidBILL_TYPE_FLAG').value	=	'1';
	   if(!IsProperDate(document.getElementById('txtAPP_EFFECTIVE_DATE'))) return false;
	   if(!IsProperDate(document.getElementById('txtAPP_INCEPTION_DATE'))) return false;
	   ChangeDefaultDate();
	   
	}
	
		function setHidSubLob()
		{
			document.getElementById('hidSUB_LOB').value = document.getElementById('cmbAPP_SUBLOB').options[document.getElementById('cmbAPP_SUBLOB').selectedIndex].value;
			
		}
		
		function sethidCSR()
		{
			document.getElementById('hidCSR').value = document.getElementById('cmbCSR').options[document.getElementById('cmbCSR').selectedIndex].value;
		}
		function sethidProducer()
		{
			document.getElementById('hidProducer').value = document.getElementById('cmbProducer').options[document.getElementById('cmbProducer').selectedIndex].value;
			 
		}
		function sethidCustomerID()
		{
			document.getElementById('hidCustomerID').value = document.getElementById('cmbCUSTOMER_ID').options[document.getElementById('cmbCUSTOMER_ID').selectedIndex].value;
			 
		}
		function sethidStateID()
		{
			document.getElementById('hidStateID').value = document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
			 
		}
		
		function fnTest()
		{
			var customerid = '';
			var appid='';
			var appversionid='';
			if (document.getElementById('hidCustomerID')!=null)
			{
				
				customerid = document.getElementById('hidCustomerID').value;
			}
			if (document.getElementById('hidAppID')!=null)
			{
				appid = document.getElementById('hidAppID').value;
			}
			if (document.getElementById('hidAppVersionID')!=null)
			{
				appversionid = document.getElementById('hidAppVersionID').value;
			}
			
			if(document.getElementById('hidLOBID')!= null && document.getElementById('hidLOBID').value =='1')
			{
				var dwellingid='2';
			 
				ShowPopup('/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID='+<%= gIntCUSTOMER_ID %> +'&APP_ID='+appid+'&APP_VERSION_ID='+appversionid+'&DWELLING_ID='+dwellingid,'Quote', 950, 400);
				
			} 
			return false;
		}
		
		function fnTest1()
		{
		return false;
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

			sTerm = document.APP_LIST.cmbAPP_TERMS.value;
			
			if(sTerm == "0" || sTerm == "")
			{
				 document.APP_LIST.txtAPP_EXPIRATION_DATE.value="";							
				
			}
			else
			{
				
				//document.APP_LIST.txtAPP_EXPIRATION_DATE.disabled = true;				
				
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
					{
						sEffDate = ReplaceDateSeparator(sEffDate);
						 
					}
					sDate = new Date(sEffDate);
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
						//sposfix = new String(sDate.getMonth() + 1) + dtDSep + new String(sDate.getDate()-1) + dtDSep + new String(sNewYear)		
					
					var newDate = new Date(new String(sDate.getMonth() + 1) + "/" + new String(sDate.getDate()) + "/" + new String(sNewYear));
					
					//alert(newDate);
					
					newDate.setDate(newDate.getDate());	
				
					
					//sposfix = new String(sDate.getMonth() + 1) + "/" + new String(sDate.getDate()) + "/" + new String(sNewYear)						
					
					sposfix = new String(newDate.getMonth() + 1) + "/" + new String(newDate.getDate()) + "/" + new String(sNewYear)						
					
					document.APP_LIST.txtAPP_EXPIRATION_DATE.value = sposfix;
					
					if(sTerm=="70")
					{
						document.APP_LIST.txtAPP_EXPIRATION_DATE.value ="";
					}
					
				 
				}
			}
		}
		
		function setMenu()
		{
			if (top.topframe.main1.menuXmlReady == false)
				setTimeout("setMenu();",1000);
			//alert(document.getElementById("hidFormSaved").value);	
			//No need to make menus , if record has been deleted,
			//Hence checking whether record deleted or not
			if (document.getElementById("hidFormSaved") != null && document.getElementById("hidFormSaved").value != "5")
			{
				top.topframe.main1.activeMenuBar = '1';
				top.topframe.createActiveMenu();
				top.topframe.enableMenus("1","ALL");
				top.topframe.enableMenu("1,1,1");			
				top.topframe.enableMenu("1,1,2"); //display the Add Quote Menu Link
				selectedLOB = lobMenuArr[document.getElementById('hidLOBID').value];
				
				
				if (document.getElementById("hidOldData").value != "" && document.getElementById("hidOldData").value != "0")
				{
					//Enabling the application details and risk information menus, if there is a record
					top.topframe.enableMenu("1,2,2");	
					top.topframe.enableMenu("1,3," + selectedLOB);
					
					if (document.getElementById("hidPOLICY_ID").value != "")
					{
						//Enablig the polic details menu						
						top.topframe.enableMenu("1,2,3");
						//parent.parent.location.href = "..\client\aspx\CustomerManagerIndex.aspx?SubmitApp=1";
						//Hide the images in the ClientTop after the policy has been created successfully
						parentObj = parent.document;
						if(parentObj.getElementById("cltClientTop_imgSubmitAnyway"))
							parentObj.getElementById("cltClientTop_imgSubmitAnyway").style.display = "none";
						if(parentObj.getElementById("cltClientTop_imgSubmitApp"))
							parentObj.getElementById("cltClientTop_imgSubmitApp").style.display = "none";
						if(parentObj.getElementById("cltClientTop_imgVerifyApp"))
							parentObj.getElementById("cltClientTop_imgVerifyApp").style.display = "none";
						if(parentObj.getElementById("cltClientTop_imgQuote"))
							parentObj.getElementById("cltClientTop_imgQuote").style.display = "none";
					}
					//Added by Asfa (24-June-2008) - iTrack #4045
					if (document.getElementById("txtAPP_STATUS").value != "" && document.getElementById("txtAPP_STATUS").value == "Unconfirmed")
					{
						parentObj = parent.document;
						if(parentObj.getElementById("cltClientTop_imgSubmitAnyway"))
							parentObj.getElementById("cltClientTop_imgSubmitAnyway").style.display = "none";
						if(parentObj.getElementById("cltClientTop_imgVerifyApp"))
							parentObj.getElementById("cltClientTop_imgVerifyApp").style.display = "none";
					}
				}
				else
				{
					//Disabling the Risk information menu, as it is not req if application is not selected
					top.topframe.disableMenu("1,3");
				}
							
			}
			
		}
		
		function SetLookupValues()
		{

			var customerIDAndStateID= document.getElementById('hidCustomerID').value ;			  
			document.getElementById('hidCustomerID').value = customerIDAndStateID.substring(0,customerIDAndStateID.indexOf('^'));
			var StateID= customerIDAndStateID.substring(customerIDAndStateID.indexOf('^')+1,customerIDAndStateID.length);
			SelectComboOption("cmbSTATE_ID",StateID);
			document.getElementById('hidStateID').value = StateID;
		}
		
			function CheckBillType(source,arguments)
			{
				if (document.getElementById('cmbBILL_TYPE_ID').options.selectedIndex==-1)
				{
					arguments.IsValid = false;
					return;				
				}
			}
			
			function ResetForm1()
			{
				document.getElementById('hidReset').value=1; 
				var loc=document.location.href;
				var custID=document.getElementById('hidCustomerID').value;
				var appID=document.getElementById('hidAppID').value;
				var appVersionID=document.getElementById('hidAppVersionID').value;
				var calledFrom=document.getElementById('hidCalledFrom').value;
				
				if(appID=="NEW")
					appID="";
					
				document.location.href="GeneralInfoEx.aspx?CUSTOMER_ID="+custID+"&APP_ID="+appID+"&APP_VERSION_ID="+appVersionID+"&CALLEDFROM="+ calledFrom + "&transferdata=";
				return false; 
			}
			function SetCreateNewVersion()
			{
				return;
				if(document.getElementById('hidIS_ACTIVE'))
				
				if(document.getElementById('hidIS_ACTIVE').value=="" || document.getElementById('hidIS_ACTIVE').value=='N')			
				{
					if(document.getElementById('btnCopy'))
					document.getElementById('btnCopy').disabled=true;	
					}
					
			}
			function checkNewVersion()
			{
				if(document.getElementById('hidCustomerID'))
				{
					var strVersion='<%=strNewVersion%>';
					var custID=document.getElementById('hidCustomerID').value;
					var appID=document.getElementById('hidAppID').value;
					var appVersionID='<%=strVersionID%>'
					var calledFrom=document.getElementById('hidCalledFrom').value;;
					//alert(appVersionID);
					if(strVersion!="")
					{
						var con=confirm("Do you wish to move to new version?");
						if(con)
						{
							//parent.location.href="ApplicationTab.aspx?CUSTOMER_ID="+custID+"&APP_ID="+appID+"&APP_VERSION_ID="+appVersionID+"&CALLEDFROM="+ calledFrom + "&transferdata=";
							document.location.href="GeneralInfoEx.aspx?CUSTOMER_ID="+custID+"&APP_ID="+appID+"&APP_VERSION_ID="+appVersionID+"&CALLEDFROM="+ calledFrom + "&transferdata=";							
							
							RefreshClientTop();
							parent.location.href="ApplicationTab.aspx?CUSTOMER_ID="+custID+"&APP_ID="+appID+"&APP_VERSION_ID="+appVersionID+"&CALLEDFROM="+ calledFrom + "&transferdata=";
							//window.parent.parent.frames[0].refresh;
						}
					}
				}
			}
			
			function enableState()
			{
				document.getElementById('cmbSTATE_ID').disabled="false";				
			}
			
			
			function BillType()
			{
				if(document.getElementById('cmbBILL_TYPE_ID'))
				{
					var ctrl = document.getElementById('cmbBILL_TYPE_ID');
					if (ctrl == null)
						return;
						
					if (ctrl.selectedIndex == -1)
					{
						//By default n.a. should be visible
						document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',true);
						
						SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidFULL_PAY_PLAN_ID").value);
						
						document.getElementById('trBILL_MORTAGAGEE').style.display="inline";
						return;
					}
					/*8460
						11150
						11276
						11278
					*/
									
					//if(ctrl.options[ctrl.selectedIndex].value == "11150" ||ctrl.options[ctrl.selectedIndex].value == "8460" ||ctrl.options[ctrl.selectedIndex].value == mortgageeBill || ctrl.options[ctrl.selectedIndex].value == InsuredMortgageeBill)
					if(ctrl.options[ctrl.selectedIndex].value == "11150" ||ctrl.options[ctrl.selectedIndex].value == "8460" || ctrl.options[ctrl.selectedIndex].value == InsuredMortgageeBill)
					{
						document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',false);
						document.getElementById('cmbINSTALL_PLAN_ID').style.display = "inline";
					}				
					else
					{
						document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',true);
						SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidFULL_PAY_PLAN_ID").value); 
					}
					var LOB = document.getElementById('hidLOBID').value;
					if((LOB==<%=((int)enumLOB.REDW).ToString()%> || LOB==<%=((int)enumLOB.HOME).ToString()%>) && (ctrl.options[ctrl.selectedIndex].value=='11276' || ctrl.options[ctrl.selectedIndex].value=='11277' || ctrl.options[ctrl.selectedIndex].value=='11278'))
						document.getElementById('trBILL_MORTAGAGEE').style.display="inline";
					else
						document.getElementById('trBILL_MORTAGAGEE').style.display="none";	
				HideShowBillingInfo();	
				fillDownPayMode();
				SelectComboOption('cmbDOWN_PAY_MODE',document.getElementById("hidDOWN_PAY_MODE").value);
				
				}
			}
			function HideShowBillingInfo()
			{
				var ctrl = document.getElementById('cmbBILL_TYPE_ID');
				if (ctrl == null || ctrl.selectedIndex == -1)
				{
					document.getElementById('lblDOWN_PAY_MODE').style.display="none";
					document.getElementById('lblINSTALL_PLAN_ID').style.display="none";
					document.getElementById('spnINSTALL_PLAN_ID').style.display="none";
					document.getElementById('spnDOWN_PAY_MODE').style.display="none";
					EnableValidator('rfvDOWN_PAY_MODE',false);
					EnableValidator('rfvINSTALL_PLAN_ID',false);
					//var myVal = document.getElementById('rfvINSTALL_PLAN_ID'); 
					//ValidatorEnable(myVal, false); 
					return;
				}
				if(ctrl.options[ctrl.selectedIndex].value == "8460" || ctrl.options[ctrl.selectedIndex].value == "11150" || ctrl.options[ctrl.selectedIndex].value == "11278" || ctrl.options[ctrl.selectedIndex].value == mortgageeBill || ctrl.options[ctrl.selectedIndex].value == InsuredMortgageeBill)
				{
					document.getElementById('lblDOWN_PAY_MODE').style.display="none";
					document.getElementById('lblINSTALL_PLAN_ID').style.display="none";
					document.getElementById('cmbDOWN_PAY_MODE').style.display="inline";     
					document.getElementById('cmbINSTALL_PLAN_ID').style.display="inline";
					document.getElementById('spnINSTALL_PLAN_ID').style.display="inline";
					document.getElementById('spnDOWN_PAY_MODE').style.display="inline";
					EnableValidator('rfvDOWN_PAY_MODE',true);
					EnableValidator('rfvINSTALL_PLAN_ID',true);
					//var myVal = document.getElementById('rfvINSTALL_PLAN_ID'); 
				//ValidatorEnable(myVal, true); 
			 
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
				//var myVal = document.getElementById('rfvINSTALL_PLAN_ID'); 
				//ValidatorEnable(myVal, false); 
				} 
			}
			
			
			function CheckDelete()
			{
				var delStr='<%=delStr%>';
					
				if(delStr=="1")
				{
					top.topframe.disableMenu('1,3');
					top.topframe.disableMenu('1,2,2');
					RemoveTab(4,top.frames[1]);
					RemoveTab(3,top.frames[1]);
					RemoveTab(2,top.frames[1]);
					//parent.document.getElementById('divWorkFlow').innerHTML		=	"";
					//parent.document.getElementById('workflowOptions').innerHTML	=	"";
					return true;
					/*top.topframe.enableMenu('1,0');
					top.topframe.enableMenu('1,1');
					top.topframe.disableMenus('1,1','ALL');
					top.topframe.enableMenu('1,1,0');
					top.topframe.enableMenu('1,1,1');*/
				}
				return false;
			
			}
			function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbBILL_TYPE_ID":
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
					return;   // invalid userName
				}
			}	
			
			// Added by mohit.
			// function for finding number of days between two applications.
			function CheckDate(objSource , objArgs)
			{
			  var inceptiondate=document.APP_LIST.txtAPP_EFFECTIVE_DATE.value;
			  var d2= new Date(inceptiondate);
			  var d1=new Date("<%=System.DateTime.Now%>");	 
			  var t1= 0;
			  			 
			  t1 = d1.getTime() - d2.getTime();
			  v1= Math.floor(t1 / (24 * 60 * 60 * 1000));	
			   
			  if (v1 > 15)
			  {
				//alert("Agency can not create 15 days old application.");
				objArgs.IsValid=false;
			  }
			  else
			  {
				objArgs.IsValid=true;
			  }
			}
			function ChangeDefaultDate()
			{		
			
			//If Inception date is greater then Effective date  : Set Effective date in Inception date
				
				if(document.getElementById('revAPP_EFFECTIVE_DATE').isvalid == true)
				{
				
					if(document.getElementById('txtAPP_INCEPTION_DATE').value == "")
					{
						 document.getElementById('txtAPP_INCEPTION_DATE').value = document.getElementById('txtAPP_EFFECTIVE_DATE').value
					}
					
					var effDate=document.getElementById('txtAPP_EFFECTIVE_DATE').value;				
					var incDate=document.getElementById('txtAPP_INCEPTION_DATE').value;
					
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
					
					ShowExpirationDate(); 	
				
				}				
			}			
			
			/*Function to be called in onload, initilize the page*/
			function Init()
			{
			  // If customer is InActive : Do not display Page
			  if(document.getElementById('trDETAILS')==null)
			  {
			   CheckDelete();
			   return false;
			  }
				ApplyColor();
				ChangeColor();		
				//setTimeout("GetColorAgency()",300); //Commented by Charles on 24-Aug-09 for APP/POL Optimization
				if (CheckDelete() == false)
				{	
				//alert();
					document.getElementById('cmbSTATE_ID').focus();				    
					populateXML();
					FillSubLOB();
					populateXML();	
					BillType();
					populateXML();
					//fillBillingPlan(); //Commented by Charles on 14-Sep-09 for APP/POL Optimization
					populateXML();
					HideShowBillingInfo();
					setTab();
					SetCreateNewVersion();
					checkNewVersion();
					DisplayPreviousYearDesc();					
					ChangeColor();
					DisplaySubLOB();
				}			
				if(document.getElementById('btnSubmitInProgress')!=null)
					document.getElementById('btnSubmitInProgress').style.display="none";
				if(document.getElementById('btnSubmitAnywayInProgress')!=null)
					document.getElementById('btnSubmitAnywayInProgress').style.display="none";
				
				//alert(document.getElementById("hidFocusFlag").value)
				if(document.getElementById("hidFocusFlag")!=null)
				{
					if(document.getElementById("hidFocusFlag").value=="1")
					{
						if(document.getElementById("cmbAPP_LOB"))
						{
							if(document.getElementById("cmbAPP_LOB").style.display!='none')
							{
								document.getElementById("cmbAPP_LOB").focus();
								document.getElementById("hidFocusFlag").value="0"
							}
						}
					}
					
				}				
				if(document.getElementById("hidCalledFrom")!=null && document.getElementById("hidCalledFrom").value=="QQ")
				{
					if(document.getElementById("cmbCSR"))
					{
						if(document.getElementById("cmbCSR").style.display!='none')
						{
							document.getElementById("cmbCSR").focus();						
						}
					}
				
				}
				var strBill = new String(document.getElementById('lblBILL_MORTAGAGEE').innerHTML);
				strBill = replaceAll(strBill," ","");
				if(strBill=="")
					document.getElementById('lblBILL_MORTAGAGEE').innerHTML = "N.A.";
				//added by pravesh on 3 june 08 as agency can not be changed for a customer/ to change agency copy client and change agency of copied client and then add app
				//if(document.getElementById("cmbAPP_AGENCY_ID"))
				//	document.getElementById("cmbAPP_AGENCY_ID").setAttribute("disabled",true);
			}
			function DisplaySubLOB()
			{
				var LOB = document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].value;
				var CalledFrom = document.getElementById('hidCallefroms').value;
				var State="0";
				if(document.getElementById("cmbSTATE_ID").selectedIndex>0)
					State = document.getElementById("cmbSTATE_ID").options[document.getElementById("cmbSTATE_ID").selectedIndex].value;
				//if( LOB == "3" || LOB == "4" || LOB == "5" || CalledFrom == "MOT" || CalledFrom == "WAT" || CalledFrom == "UMB")
				if( LOB == "3" || LOB == "4" || LOB == "5" || LOB == "1" || LOB == "6" || LOB == "7" || CalledFrom == "MOT" || CalledFrom == "WAT" || CalledFrom == "UMB" || CalledFrom == "HOME" || CalledFrom == "REDW" || CalledFrom == "GEN" || (State=="22" && (CalledFrom == "AUTO" || LOB == "2")))
				 { 
					document.getElementById('capAPP_SUBLOB').style.display='none';
					document.getElementById('cmbAPP_SUBLOB').style.display='none';
				 }
				
			}
			// added by mohit on 13/10/2005.
			function DisplayPreviousYearDesc()
			{
				if (parseInt(document.getElementById('txtYEAR_AT_CURR_RESI').value) < 3 && document.getElementById('txtYEAR_AT_CURR_RESI').value != "" && document.getElementById('txtYEAR_AT_CURR_RESI').value != "0" && document.getElementById('txtYEAR_AT_CURR_RESI').value != "00")
				{ 
					document.getElementById('txtYEARS_AT_PREV_ADD').style.display='inline';
					document.getElementById('capYEARS_AT_PREV_ADD').style.display='inline';
					document.getElementById("rfvYEARS_AT_PREV_ADD").setAttribute('enabled',true);
					document.getElementById('spnYEARS_AT_PREV_ADD').style.display='inline';	
					document.getElementById('txtYEARS_AT_PREV_ADD').focus();
				}
				else
				{
					document.getElementById('txtYEARS_AT_PREV_ADD').style.display='none';
				    document.getElementById('capYEARS_AT_PREV_ADD').style.display='none';
				    document.getElementById('spnYEARS_AT_PREV_ADD').style.display='none';
				    document.getElementById("rfvYEARS_AT_PREV_ADD").setAttribute('enabled',false);
				    document.getElementById("rfvYEARS_AT_PREV_ADD").style.display = "none";
					document.getElementById("rfvYEARS_AT_PREV_ADD").setAttribute('isValid',false);			
				}
			}
			
			function ChkBillingPlan(objSource , objArgs)
			{ 
				objArgs.IsValid=true;
					return;
				
			}
			function	fillDownPayMode()
			{
				//added by pravesh
				//Split to get ID 
				var PlanId = document.getElementById('cmbINSTALL_PLAN_ID').options[document.getElementById('cmbINSTALL_PLAN_ID').selectedIndex].value;
				
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
									document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
								}
							} 
							if (nodeDOWN_PAY_MODE1 != null && nodeDOWN_PAY_MODE_decs1 != null)
							{
								if (nodeDOWN_PAY_MODE1[0] != null &&  nodeDOWN_PAY_MODE_decs1[0] != null )
								{
									oOption = document.createElement("option");
									oOption.value = nodeDOWN_PAY_MODE1[0].firstChild.text;
									oOption.text = nodeDOWN_PAY_MODE_decs1[0].firstChild.text;
									document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
								}
							} 
							if (nodeDOWN_PAY_MODE2 != null && nodeDOWN_PAY_MODE_decs2 != null)
							{
								if (nodeDOWN_PAY_MODE2[0] != null &&  nodeDOWN_PAY_MODE_decs2[0] != null )
								{
									oOption = document.createElement("option");
									oOption.value = nodeDOWN_PAY_MODE2[0].firstChild.text;
									oOption.text = nodeDOWN_PAY_MODE_decs2[0].firstChild.text;
									document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
								}
							} 
							
							
						} 
						
					} 			
					
				}   	
				return;	
					///end here
			}
			// Makes cmbPROPRTY_INSP_CREDIT ques mandatory in case policy type is : HO-2 Replacement, HO-3 Replacement, HO-5 Replacement or HO-3/HO-5 Premier
			function mandPropInspCredit()
			{
			//if(document.getElementById('hidLOBID').value == '1')
			if(document.getElementById('cmbAPP_LOB').value == '1' || document.getElementById('cmbAPP_LOB').value == '6')
			{
				var policyType;
				policyType = document.getElementById('cmbPOLICY_TYPE').options[document.getElementById('cmbPOLICY_TYPE').selectedIndex].value;				
				
				/*
				Michigan  : 
					11402 :  HO-2 Replacement
					11400 :  HO-3 Replacement
					11401 :	 HO-5 Replacement
					11409 :  HO-3 Premier
					11410 :  HO-5 Premier
				Indiana   :
					11192 :  HO-2 Replacement
					11148 :  HO-3 Replacement
					11149 :	 HO-5 Replacement
					//Itrack 6485 : Praveen
					11193 :  HO-2 Repair
					11194 :  HO-3 Repair

				Rental Michigan  :	
					11290 : DP-2 Repair
					11289 : DP-2 Replacement
					11458 : DP-3 Premier
					11292 : DP-3 Repair
					11291 : DP-3 Replacement
				Rental Indiana  :
					11480 : DP-2 Repair
					11479 : DP-2 Replacement
					11482 : DP-3 Repair
					11481 : DP-3 Replacement
						
				*/
				
					//if (policyType == '11402' || policyType == '11400' || policyType == '11401' ||policyType == '11409' || policyType == '11410' || policyType == '11192' || policyType == '11148' || policyType == '11149' )
					if (policyType == '11402' || policyType == '11403' || policyType == '11404' || policyType == '11400' || policyType == '11401' ||policyType == '11409' || policyType == '11410' || policyType == '11192' || policyType == '11148' || policyType == '11149'
					|| policyType == '11290' || policyType == '11289' || policyType == '11458' || policyType == '11292' || policyType == '11291' 
					|| policyType == '11480' || policyType == '11479' || policyType == '11482' || policyType == '11481' || policyType == '11193' || policyType == '11194' )
					{
						document.getElementById("rfvPROPRTY_INSP_CREDIT").setAttribute('enabled',true); 
						document.getElementById("rfvPROPRTY_INSP_CREDIT").setAttribute('isValid',true); 
						document.getElementById("spnPROPRTY_INSP_CREDIT").style.display = "inline";
						document.getElementById("cmbPROPRTY_INSP_CREDIT").className = "mandatoryColor";
						
						document.getElementById("rfvPIC_OF_LOC").setAttribute('enabled',true); 
						document.getElementById("rfvPIC_OF_LOC").setAttribute('isValid',true); 
						document.getElementById("spnPIC_OF_LOC").style.display = "inline";
						document.getElementById("cmbPIC_OF_LOC").className = "mandatoryColor";
					}
					else
					{
						document.getElementById("rfvPROPRTY_INSP_CREDIT").setAttribute('enabled',false); 
						document.getElementById("rfvPROPRTY_INSP_CREDIT").setAttribute('isValid',false); 
						document.getElementById("rfvPROPRTY_INSP_CREDIT").style.display = "none";
						document.getElementById("spnPROPRTY_INSP_CREDIT").style.display = "none";
						document.getElementById("cmbPROPRTY_INSP_CREDIT").className = "none";
						
						document.getElementById("rfvPIC_OF_LOC").setAttribute('enabled',false); 
						document.getElementById("rfvPIC_OF_LOC").setAttribute('isValid',false); 
						document.getElementById("rfvPIC_OF_LOC").style.display = "none";
						document.getElementById("spnPIC_OF_LOC").style.display = "none";
						document.getElementById("cmbPIC_OF_LOC").className = "none";
					}
				}
				//else if(document.getElementById('hidLOBID').value == '6')
				
			}
			
	function fillDownPay()
	{		
		if (document.getElementById('cmbDOWN_PAY_MODE').options.selectedIndex !=-1)
			document.getElementById('hidDOWN_PAY_MODE').value=document.getElementById('cmbDOWN_PAY_MODE').options[document.getElementById('cmbDOWN_PAY_MODE').options.selectedIndex].value;
			//alert(' mod value='+ document.getElementById('hidDOWN_PAY_MODE').value);
	}
	function ResetForm()
	{
		temp=1;
		document.APP_LIST.reset();
		DisplayPreviousYearDesc();
		populateXML();
		BillType();
		DisableValidators();
		ChangeColor();
		
		return false;
	}
	/*This Function Refresh ClientTopPage at Pageload */
   function RefreshClientTop()
	{
	// If customer is InActive : Do not display Page
	 if(document.getElementById('trDETAILS')==null)
	 {
	  return false;
	 }
	
		var doc = this.parent.document;
		var custID=document.getElementById('hidCustomerID').value;
		var appID=document.getElementById('hidAppID').value;
		var calledFrom=document.getElementById('hidCalledFrom').value;;
	
		var newstate = (document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].text).substring(0,2).toUpperCase();
			if (doc.getElementById("cltClientTop_PanelClient") == null)
				{	
					if(document.getElementById("txtLOB").value == 'Homeowners' || document.getElementById("txtLOB").value == 'Rental' )
					{
					
						var add = "";
								add = document.getElementById("txtAPP_NUMBER").value + " (" + newstate + ")" + "-"
									+ document.getElementById("txtAPP_STATUS").value + " ("
									+ document.getElementById("txtLOB").value + ": "
									+ document.getElementById("cmbPOLICY_TYPE").options[document.getElementById('cmbPOLICY_TYPE').selectedIndex].text + ", "
									+ document.getElementById("txtAPP_EFFECTIVE_DATE").value + "-"
									+ document.getElementById("txtAPP_EXPIRATION_DATE").value + ")" ;
												
								doc.getElementById("cltClientTop_lblSAppNo1").innerHTML = add;
								//appVersion ='<%=strVersionID%>'
								appVersion = document.getElementById('txtAPP_VERSION').value;
								doc.getElementById("cltClientTop_lblSAppVersion").innerHTML =appVersion;
					}
					else
					{
					
					var add = "";
								add = document.getElementById("txtAPP_NUMBER").value + " (" + newstate + ")" + "-"
									+ document.getElementById("txtAPP_STATUS").value + " ("
									+ document.getElementById("txtLOB").value + ": "
									//+ document.getElementById("cmbPOLICY_TYPE").options[document.getElementById('cmbPOLICY_TYPE').selectedIndex].text + ","
									+ document.getElementById("txtAPP_EFFECTIVE_DATE").value + "-"
									+ document.getElementById("txtAPP_EXPIRATION_DATE").value + ")" ;
												
								doc.getElementById("cltClientTop_lblSAppNo1").innerHTML = add;
								appVersion = document.getElementById('txtAPP_VERSION').value;
								//parent.location.href="ApplicationTab.aspx?CUSTOMER_ID="+custID+"&APP_ID="+appID+"&APP_VERSION_ID="+appVersion+"&CALLEDFROM="+ calledFrom + "&transferdata=";
								doc.getElementById("cltClientTop_lblSAppVersion").innerHTML =appVersion;
					}	
									
				}
	}
	
	function RetunrType()
	{
		return false;
	}
	function HideShowSubmit()
	{
	
		if(document.getElementById('btnSubmitInProgress')!=null)
			document.getElementById('btnSubmitInProgress').style.display="inline";
			document.getElementById('btnSubmitInProgress').disabled="true";
		if(document.getElementById('btnConvertAppToPolicy')!=null)
			document.getElementById('btnConvertAppToPolicy').style.display="none";
		if(document.getElementById('btnSubmitAnyway')!=null)
			document.getElementById('btnSubmitAnyway').style.display="none";			
	}
	
	function HideShowSubmitAnyway()
	{
	if(document.getElementById('btnSubmitAnywayInProgress')!=null)
			document.getElementById('btnSubmitAnywayInProgress').style.display="inline";
			document.getElementById('btnSubmitAnywayInProgress').disabled="true";
		if(document.getElementById('btnSubmitAnyway')!=null)
			document.getElementById('btnSubmitAnyway').style.display="none";
		if(document.getElementById('btnConvertAppToPolicy')!=null)
			document.getElementById('btnConvertAppToPolicy').style.display="none";
	}
	function ProcessKeypress() 
	{
		if (event.keyCode == 13) 
		{       
			ChangeDefaultDate();
		}  
	}

		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="Init();ApplyColor(); RefreshClientTop();" onkeydown="ProcessKeypress();">
		<FORM id="APP_LIST" name="APP_LIST" onsubmit="enableState();" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<br>
				<tr id="trFORMMESSAGE" runat="server">
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td>
				</tr>
				<TR id="trDETAILS" runat="server">
					<TD>
						<TABLE id="Table2" width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="midcolorr" colSpan="4">
									<asp:Label ID="lblChooseLang" Runat="server">Select Language</asp:Label>								
									<asp:DropDownList ID="cmbChooseLang" Runat="server" AutoPostBack="True">				
										<asp:ListItem Value="en-US">English</asp:ListItem>
										<asp:ListItem Value="pt-BR">Portuguese (Brazil)</asp:ListItem>
									</asp:DropDownList>
									</TD>									
								</tr>
								<tr>
									<TD class="pageHeader" colSpan="4"><webcontrol:workflow id="myWorkFlow" runat="server"></webcontrol:workflow>
										<asp:label id="lblManHeader" Runat="server">Please note that all fields marked with * are mandatory</asp:label>
									</TD>
								</tr>
								<tr>
									<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg"></asp:label></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCUSTOMER_ID" runat="server">ClientID</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtCUSTOMER_NAME" runat="server" ReadOnly="true"></asp:textbox><IMG id="imgSelect" style="CURSOR: hand" src="../../cmsweb/images/selecticon.gif" runat="server">
										<asp:textbox id="txtCUSTOMERNAME" runat="server" CssClass="midcolora" ReadOnly="true" BorderStyle="None"
											maxlength="10" size="30"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvCUSTOMER_ID" runat="server" Enabled="False" Display="Dynamic" ControlToValidate="txtCUSTOMER_NAME"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="16%"><asp:label id="capAPP_STATUS" runat="server">Status</asp:label></TD>
									<TD class="midcolora" width="34%"><asp:textbox id="txtAPP_STATUS" runat="server" CssClass="midcolora" ReadOnly="true" BorderStyle="None"
											maxlength="10" size="30"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4">LOB</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capSTATE_ID" runat="server">State</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%" colSpan="3"><asp:dropdownlist id="cmbSTATE_ID" onfocus="SelectComboIndex('cmbSTATE_ID')" runat="server" AutoPostBack="True"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvSTATE_ID" runat="server" Display="Dynamic" ControlToValidate="cmbSTATE_ID"
											ErrorMessage="Please select state."></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora"><asp:label id="capAPP_LOB" runat="server">Line of Business</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora"><asp:dropdownlist id="cmbAPP_LOB" onfocus="SelectComboIndex('cmbAPP_LOB')" runat="server" AutoPostBack="True"
											onchange="FillSubLOB();BillType();"></asp:dropdownlist><asp:textbox id="txtLOB" runat="server" CssClass="midcolora" ReadOnly="true" BorderStyle="None"
											maxlength="10" size="30"></asp:textbox><BR> <!-- BillType() added by Charles on 14-Sep-09 for APP/POL Optimization -->
										<asp:requiredfieldvalidator id="rfvAPP_LOB" runat="server" Display="Dynamic" ControlToValidate="cmbAPP_LOB"
											ErrorMessage="APP_LOB can't be blank."></asp:requiredfieldvalidator></TD>
									<TD class="midcolora"><asp:label id="capAPP_SUBLOB" runat="server">Sub Line of Business</asp:label></TD>
									<TD class="midcolora"><asp:dropdownlist id="cmbAPP_SUBLOB" onfocus="SelectComboIndex('cmbAPP_SUBLOB')" runat="server" onchange="setHidSubLob();"></asp:dropdownlist><BR>
									</TD>
								</tr>
								<tr id="policyTR" runat="server">
									<td class="midcolora" colSpan="1"><asp:label id="capAPP_POLICY_TYPE" runat="server">Policy Type</asp:label><span class="mandatory">*</span>
									</td>
									<td class="midcolora" colSpan="3"><asp:dropdownlist id="cmbPOLICY_TYPE" onblur="mandPropInspCredit();" onfocus="SelectComboIndex('cmbPOLICY_TYPE');"
											runat="server" onchange="mandPropInspCredit();"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvPOLICY_TYPE" Display="Dynamic" ControlToValidate="cmbPOLICY_TYPE" Runat="server"></asp:requiredfieldvalidator></td>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:label id ="lblAppHeader" runat="server">Application</asp:label></TD>
								</tr>
								<tr>
									<TD class="midcolora" colSpan="1"><asp:label id="capAPP_NUMBER" runat="server">Application Number</asp:label></TD>
									<TD class="midcolora" colSpan="1"><asp:textbox id="txtAPP_NUMBER" runat="server" CssClass="midcolora" ReadOnly="true" BorderStyle="None"
											maxlength="150" size="50"></asp:textbox></TD>
									<TD class="midcolora" colSpan="1"><asp:label id="capAPP_VERSION" runat="server">Version</asp:label></TD>
									<TD class="midcolora" colSpan="1"><asp:textbox id="txtAPP_VERSION" runat="server" CssClass="midcolora" ReadOnly="true" BorderStyle="None"
											maxlength="10" size="30"></asp:textbox></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:label id ="lblTermHeader" runat="server">Term</asp:label></TD>
								</tr>
								<tr>
									<TD class="midcolora"><asp:label id="capAPP_TERMS" runat="server">Policy Term Months</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora"><asp:dropdownlist id="cmbAPP_TERMS" onfocus="SelectComboIndex('cmbAPP_TERMS')" runat="server" 
										 AutoPostBack="true"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvAPP_TERMS" runat="server" Display="Dynamic" ControlToValidate="cmbAPP_TERMS"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora"><asp:label id="capAPP_EFFECTIVE_DATE" runat="server">Effective Date</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" vAlign="middle"><asp:textbox id="txtAPP_EFFECTIVE_DATE" runat="server" maxlength="10" size="12" Display="Dynamic"></asp:textbox><asp:hyperlink id="hlkCalandarDate" runat="server" CssClass="HotSpot" Display="Dynamic">
											<asp:image id="imgCalenderExp" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
												valign="middle" Display="Dynamic"></asp:image>
										</asp:hyperlink><BR>
										<asp:requiredfieldvalidator id="rfvAPP_EFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtAPP_EFFECTIVE_DATE"
											ErrorMessage="APP_EFFECTIVE_DATE can't be blank."></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revAPP_EFFECTIVE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtAPP_EFFECTIVE_DATE"
											ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator><asp:customvalidator id="csvAPP_EFFECTIVE_DATE" Display="Dynamic" ControlToValidate="txtAPP_EFFECTIVE_DATE"
											Runat="server" ClientValidationFunction="CheckDate"></asp:customvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora"><asp:label id="capAPP_EXPIRATION_DATE" runat="server">Expiration Date</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora"><asp:textbox id="txtAPP_EXPIRATION_DATE" runat="server" ReadOnly="True" maxlength="10" size="12"
											Display="Dynamic"></asp:textbox><BR>
										<asp:requiredfieldvalidator id="rfvAPP_EXPIRATION_DATE" runat="server" Display="Dynamic" ControlToValidate="txtAPP_EXPIRATION_DATE"
											ErrorMessage="APP_EXPIRATION_DATE can't be blank."></asp:requiredfieldvalidator></TD>
									<TD class="midcolora"><asp:label id="capAPP_INCEPTION_DATE" runat="server">Inception Date</asp:label></TD>
									<TD class="midcolora" vAlign="middle"><asp:textbox id="txtAPP_INCEPTION_DATE" runat="server" maxlength="10" size="12"></asp:textbox><asp:hyperlink id="hlkInceptionDate" runat="server" CssClass="HotSpot">
											<asp:image id="imgInceptionExp" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
												valign="middle"></asp:image>
										</asp:hyperlink><br>
										<asp:regularexpressionvalidator id="revAPP_INCEPTION_DATE" runat="server" Display="Dynamic" ControlToValidate="txtAPP_INCEPTION_DATE"
											ErrorMessage="RegularExpressionValidator"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:label id ="lblAgencyHeader" runat="server">Agency</asp:label></TD>
								</tr>
								<tr>
									<TD class="midcolora"><asp:label id="capAPP_AGENCY_ID" runat="server">Agency</asp:label><span class="mandatory">*</span></TD>						
									<TD class="midcolora" colspan="3"><!-- Added by Charles on 21-Aug-09 for APP/POL OPTIMISATION --><asp:label id="lblAGENCY_DISPLAY_NAME" CssClass="LabelFont" Runat="server"></asp:label>
									</TD>	
									</tr>
								<tr>
									<TD class="midcolora"><asp:label id="capProducer" Runat="server">Producer </asp:label></TD>
									<TD class="midcolora"><SELECT id="cmbProducer" onfocus="SelectComboIndex('cmbProducer')" onchange="sethidProducer();"
											Runat="server" NAME="cmbProducer"></SELECT></TD>
									<TD class="midcolora"><asp:label id="capCSR" runat="server">CSR Name</asp:label></TD>
									<TD class="midcolora"><SELECT id="cmbCSR" onfocus="SelectComboIndex('cmbCSR')" runat="server" onchange="sethidCSR();" NAME="cmbCSR"></SELECT></TD>
								
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:label id ="lblBillingHeader" runat="server">Billing Information</asp:label></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capBILL_TYPE_ID" runat="server">Bill Type</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbBILL_TYPE_ID" onfocus="SelectComboIndex('cmbBILL_TYPE_ID')" runat="server"
											onchange="BillType();fillBillingPlan();"></asp:dropdownlist> 
										<br>
										<asp:requiredfieldvalidator id="rfvBILL_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbBILL_TYPE_ID"
											ErrorMessage="BILL TYPE can't be blank."></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="16%"><asp:label id="capINSTALL_PLAN_ID" runat="server">Billing Plan</asp:label><span class="mandatory" id="spnINSTALL_PLAN_ID">*</span></TD>
									<TD class="midcolora" width="34%">
										<asp:Label ID="lblINSTALL_PLAN_ID" Runat="server" CssClass="LabelFont">N.A.</asp:Label>
										<asp:dropdownlist id="cmbINSTALL_PLAN_ID" onfocus="SelectComboIndex('cmbINSTALL_PLAN_ID')" runat="server"
											onchange="fillDownPayMode();"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvINSTALL_PLAN_ID" Display="Dynamic" ControlToValidate="cmbINSTALL_PLAN_ID"
											ErrorMessage="Please select billing plan." Runat="server"></asp:requiredfieldvalidator><asp:customvalidator id="csvINSTALL_PLAN_ID" Display="Dynamic" ControlToValidate="cmbINSTALL_PLAN_ID"
											ErrorMessage="Please select billing plan according to policy term." Runat="server" ClientValidationFunction="ChkBillingPlan"></asp:customvalidator></TD>
								</tr>
								<tr>
									<td class="midcolora" width="18%"><asp:label id="capDOWN_PAY_MODE" Runat="server">Down Payment Mode</asp:label><span class="mandatory" id="spnDOWN_PAY_MODE">*</span></td>
									<td class="midcolora" width="34%">
										<asp:Label ID="lblDOWN_PAY_MODE" Runat="server" CssClass="LabelFont">N.A.</asp:Label>
										<asp:dropdownlist id="cmbDOWN_PAY_MODE" onchange="fillDownPay()" Runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvDOWN_PAY_MODE" ControlToValidate="cmbDOWN_PAY_MODE" Display="Dynamic" ErrorMessage="Please select Down Payment Mode."
											Runat="server"></asp:requiredfieldvalidator></td>
									<TD class="midcolora" width="16%"><asp:label id="capPROXY_SIGN_OBTAINED" Runat="server">Proxy Signature Obtained?</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="34%"><asp:dropdownlist id="cmbPROXY_SIGN_OBTAINED" onfocus="SelectComboIndex('cmbPROXY_SIGN_OBTAINED')"
											runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvPROXY_SIGN_OBTAINED" runat="server" Display="Dynamic" ControlToValidate="cmbPROXY_SIGN_OBTAINED"
											ErrorMessage="PROXY SIGN OBTAINED can't be blank."></asp:requiredfieldvalidator></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capUNDERWRITER" runat="server">Underwriter</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblUNDERWRITER" runat="server"></asp:label></TD>
									<td class="midcolora" colspan="2"></td>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capCHARGE_OFF_PRMIUM" runat="server">Charge Of Premium</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbCHARGE_OFF_PRMIUM" Runat="server"></asp:dropdownlist></TD>
									<TD class="midcolora" width="16%"><asp:label id="capRECEIVED_PRMIUM" runat="server">Received Premium</asp:label></TD>
									<TD class="midcolora" width="34%"><asp:textbox id="txtRECEIVED_PRMIUM" style="TEXT-ALIGN: right"  onblur="FormatAmount(this);" size="16" Runat="server" MaxLength="9"></asp:textbox><br>
										<asp:regularexpressionvalidator id="revRECEIVED_PRMIUM" ControlToValidate="txtRECEIVED_PRMIUM" Display="Dynamic"
											Runat="server"></asp:regularexpressionvalidator></TD>
								</tr>
								<tr id="trPropInspCr">
									<TD class="midcolora" width="18%"><asp:label id="capPROPRTY_INSP_CREDIT" runat="server">Property Inspection/Cost Estimator </asp:label><span class="mandatory" id="spnPROPRTY_INSP_CREDIT">*</span></TD>
									<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbPROPRTY_INSP_CREDIT" onfocus="SelectComboIndex('cmbPROPRTY_INSP_CREDIT')"
											Runat="server"></asp:dropdownlist><BR>
										<asp:requiredfieldvalidator id="rfvPROPRTY_INSP_CREDIT" ControlToValidate="cmbPROPRTY_INSP_CREDIT" Display="Dynamic"
											Enabled="False" Runat="server"></asp:requiredfieldvalidator></TD>
									<td class="midcolora" width="16%"><asp:label id="capPIC_OF_LOC" Runat="server"></asp:label><span class="mandatory" id="spnPIC_OF_LOC">*</span></td>
									<td class="midcolora" width="34%"><asp:dropdownlist id="cmbPIC_OF_LOC" onfocus="SelectComboIndex('cmbPIC_OF_LOC')" Runat="server"></asp:dropdownlist><br>
										<asp:requiredfieldvalidator id="rfvPIC_OF_LOC" ControlToValidate="cmbPIC_OF_LOC" Display="Dynamic" Enabled="False"
											Runat="server"></asp:requiredfieldvalidator></td>
								</tr>
								<tr id="trCOMPLETE_APP" runat="server">
									<TD class="midcolora" width="18%"><asp:label id="capCOMPLETE_APP" runat="server">Complete Application Bonus Applies </asp:label></TD>
									<TD class="midcolora" colSpan="3"><asp:checkbox id="chkCOMPLETE_APP" runat="server"></asp:checkbox><BR>
									</TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="capYEAR_AT_CURR_RESI" runat="server">Year at Current Residence</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtYEAR_AT_CURR_RESI" runat="server" size="4" maxlength="2"></asp:textbox><br>
										<asp:regularexpressionvalidator Enabled="False" id="revYEAR_AT_CURR_RESI" runat="server" ControlToValidate="txtYEAR_AT_CURR_RESI" Display="Dynamic"></asp:regularexpressionvalidator>
										<asp:rangevalidator id="rngYEAR_AT_CURR_RESI" runat="server" Display="Dynamic" ControlToValidate="txtYEAR_AT_CURR_RESI" MaximumValue="99" MinimumValue="1" Type="Integer"></asp:rangevalidator>
									</TD>
									<TD class="midcolora" width="16%"><asp:label id="capYEARS_AT_PREV_ADD" runat="server">Previous Address if less than 3 years(Max 250 characters)</asp:label><span class="mandatory" id="spnYEARS_AT_PREV_ADD">*</span></TD>
									<TD class="midcolora" width="34%"><asp:textbox id="txtYEARS_AT_PREV_ADD" runat="server" Columns="40" Rows="5" TextMode="MultiLine"></asp:textbox><BR>
										<asp:customvalidator id="csvYEARS_AT_PREV_ADD" ControlToValidate="txtYEARS_AT_PREV_ADD" Display="Dynamic"
											Runat="server" ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator><asp:requiredfieldvalidator id="rfvYEARS_AT_PREV_ADD" runat="server" ControlToValidate="txtYEARS_AT_PREV_ADD"
											Display="Dynamic"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr id="trBILL_MORTAGAGEE">
									<td class="midcolora" width="18%"><asp:label id="capBILL_MORTAGAGEE" Runat="server">Mortgagee</asp:label></td>
									<td class="midcolora" width="32%"><asp:label id="lblBILL_MORTAGAGEE" Runat="server">N.A.</asp:label></td>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<tr>
								</tr>
								<tr>
									<TD class="headerEffectSystemParams" colSpan="4"><asp:label id="lblAllPoliciesHeader" runat="server">Any other policy with Wolverine</asp:label></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%"><asp:label id="lblPolicies" runat="server">All the policies with Wolverine</asp:label></TD>
									<TD class="midcolora" width="32%"><asp:label id="lblAllPolicy" runat="server"></asp:label></TD>
									<TD class="midcolora" width="16%"><asp:label id="lblPoliciesDiscount" runat="server">Policies eligible for discount</asp:label></TD>
									<TD class="midcolora" width="34%"><asp:label id="lblEligbilePolicy" runat="server"></asp:label></TD>
								</tr>
								<tr id="trbutton" runat="server">
									<td class="midcolora" align="left" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" Text="Create New Version" causevalidation="false"></cmsb:cmsbutton>
										<cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="" CausesValidation="False"></cmsb:cmsbutton>
									</td>
									<td class="midcolorr" align="right" colSpan="2">
										<cmsb:cmsbutton class="clsButton" id="btnSubmitAnyway" runat="server" Text="Submit Anyway" causesValidation="false"
											causeValidation="false"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnSubmitAnywayInProgress" runat="server" Text="Submit Anyway In Progress" CausesValidation="False"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnConvertAppToPolicy" style="DISPLAY: none" runat="server"
											Text="Submit" causesValidation="false" causeValidation="false"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnSubmitInProgress" runat="server" Text="Submit In Progress" CausesValidation="False"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnVerifyApplication" runat="server" Text="Verify App" causesValidation="false"
											causeValidation="false"></cmsb:cmsbutton>&nbsp;
										<cmsb:cmsbutton class="clsButton" id="btnQuote" style="DISPLAY: none" runat="server" Text="Quote"
											causesValidation="false" causeValidation="false"></cmsb:cmsbutton>
										&nbsp;<cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
									</td>
								</tr>
								<INPUT id="hidFormSaved" type="hidden" name="hidFormSaved" runat="server"> <INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
								<INPUT id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server">
								<INPUT id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
								<INPUT id="hidAPP_AGENCY_ID" type="hidden" value="0" name="hidAPP_AGENCY_ID" runat="server">
								<INPUT id="hidCSR" type="hidden" value="0" name="hidCSR" runat="server"> <INPUT id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server">
								<INPUT id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
								<INPUT id="hidLOBID" type="hidden" value="0" name="hidLOBID" runat="server"> <INPUT id="hidSUB_LOB" type="hidden" value="0" name="hidSUB_LOB" runat="server">
								<INPUT id="hidLOBXML" type="hidden" value="0" name="hidLOBXML" runat="server"> <INPUT id="hidStateID" type="hidden" value="0" name="hidStateID" runat="server">
								<INPUT id="hidUnderwriter" type="hidden" value="0" name="hidUnderwriter" runat="server">
								<INPUT id="hidDepartmentXml" type="hidden" name="hidDepartmentXml" runat="server">
								<INPUT id="hidProfitCenterXml" type="hidden" name="hidProfitCenterXml" runat="server">
								<INPUT id="hidDEPT_ID" type="hidden" name="hidDEPT_ID" runat="server"> <INPUT id="hidPC_ID" type="hidden" name="hidPC_ID" runat="server">
								<input id="hidReset" type="hidden" name="hidReset" runat="server"> <input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">
								<input id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server"> <INPUT id="hidRuleVerify" type="hidden" value="0" name="hidRuleVerify" runat="server">
								<input id="hidIs_Agency" type="hidden" value="N" name="hidIs_Agency" runat="server">
								<input id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server"> <INPUT id="hidCallefroms" type="hidden" name="hidCallefroms" runat="server">
								<input id="hidPOLICY_TYPE" type="hidden" name="Hidden1" runat="server"> <input id="hidPolicyNumber" type="hidden" name="hidPolicyNumber" runat="server">
								<input id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server">
								<input id="hidFULL_PAY_PLAN_ID" type="hidden" name="hidFULL_PAY_PLAN_ID" runat="server">
								<input id="hidBillingPlan" type="hidden" name="hidBillingPlan" runat="server"> <INPUT id="hidAlert" type="hidden" value="0" name="hidAlert" runat="server">
								<input id="hidFocusFlag" type="hidden" name="hidFocusFlag" runat="server"> <input id="hidProducer" type="hidden" name="hidProducer" runat="server">
								<input id="hidDOWN_PAY_MODE" type="hidden" name="hidDOWN_PAY_MODE" runat="server">
								<input id="hidBILL_TYPE_ID" type="hidden" name="hidBILL_TYPE_ID" runat="server">
								<input id="hidINSTALL_PLAN_ID" type="hidden" name="hidINSTALL_PLAN_ID" runat="server">
								<input id="hidDEACTIVE_INSTALL_PLAN_ID" type="hidden" name="hidDEACTIVE_INSTALL_PLAN_ID"
									runat="server">
								<input id="hidBILL_TYPE_FLAG" type="hidden" name="hidBILL_TYPE_FLAG" runat="server" value="0">								
								<input id="hidIsTerminated" type="hidden" name="hidIsTerminated" runat="server"><!-- Added by Charles on 21-Aug-09 for APP/POL Optimization -->
								<input id="hidCultureName" type="hidden" name="hidCultureName" runat="server">
							</TBODY>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<TD>
						<TABLE id="Table3" width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<td class="midcolora" colSpan="1"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Text="Back To Search"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" causesValidation="false"></cmsb:cmsbutton></td>
									<td class="midcolorr" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnCustomerAssistant" runat="server" Text="Back To Customer Assistant"></cmsb:cmsbutton></ASP:BUTTON>										
									</td>
								</tr>
							</TBODY>
						</TABLE>
					</TD>
				</tr>
			</TABLE>
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td>
						<p align="right"><A onclick="javascript:hideLookupLayer();" href="javascript:void(0)"><IMG height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0"></A></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colSpan="2"><span id="LookUpMsg"></span></td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
		function showInputVerification()
		{			
			parent.refWindow = window.open('ShowInputVerificationXml.aspx?CUSTOMER_ID=<%= gIntCUSTOMER_ID %>&APP_ID=<%= gIntAPP_ID %>&APP_VERSION_ID=<%= gIntAPP_VERSION_ID %>&LOBID=<%= gstrLobID %>&QUOTE_ID=<%= gIntQuoteID %>' ,'Quote'," left=150,top=50,resizable=yes, scrollbars=yes,width=800,height=500");					
		}
		
		function setDelayMenu()
		{		
			setMenu();
		}
		window.setTimeout('setDelayMenu()', 1);
		
		var cultureName ="";
		var parentDoc = parent.document;
		if(document.getElementById('hidCultureName').value!="")
		{
			cultureName=document.getElementById('hidCultureName').value;			
		}
		else
		{
			cultureName="en-US"	;
		}
		
		if(parentDoc != null)
		{		
			switch(cultureName)
			{
				case "pt-BR":
					if(parentDoc.getElementById('cltClientTop_lblCustomerName')!=null)
					{
						parentDoc.getElementById('cltClientTop_lblCustomerName').innerText ="Nome do cliente";
						parentDoc.getElementById('cltClientTop_lblCustomerAddress').innerText ="Endereço";
						parentDoc.getElementById('cltClientTop_lblCustomerType').innerText ="Tipo de cliente";
						parentDoc.getElementById('cltClientTop_lblCustomerPhone').innerText ="Telefone";
						parentDoc.getElementById('cltClientTop_lblAppNo').innerText ="Aplicativo Número";
						parentDoc.getElementById('cltClientTop_lblAppVersion').innerText ="Aplicativo Versão";
					}
					else
					{
						parentDoc.getElementById('cltClientTop_lblName').innerText = "Nome";
						parentDoc.getElementById('cltClientTop_lblType').innerText = "Tipo de cliente";
						parentDoc.getElementById('cltClientTop_lblAddress').innerText = "Endereço";
						parentDoc.getElementById('cltClientTop_lblPhone').innerText = "Início Telefone";
						parentDoc.getElementById('cltClientTop_lblStatus').innerText ="Estado";
						parentDoc.getElementById('cltClientTop_lblTitle').innerText = "Título";
					}
					break;
				case "en-US":
				default:
					if(parentDoc.getElementById('cltClientTop_lblCustomerName')!=null)
					{
						parentDoc.getElementById('cltClientTop_lblCustomerName').innerText ="Customer Name";
						parentDoc.getElementById('cltClientTop_lblCustomerAddress').innerText ="Address";
						parentDoc.getElementById('cltClientTop_lblCustomerType').innerText ="Customer Type";
						parentDoc.getElementById('cltClientTop_lblCustomerPhone').innerText ="Phone";
						parentDoc.getElementById('cltClientTop_lblAppNo').innerText ="App No.";
						parentDoc.getElementById('cltClientTop_lblAppVersion').innerText ="App Version";
					}
					else
					{
						parentDoc.getElementById('cltClientTop_lblName').innerText = "Name";
						parentDoc.getElementById('cltClientTop_lblType').innerText = "Customer Type";
						parentDoc.getElementById('cltClientTop_lblAddress').innerText = "Address";
						parentDoc.getElementById('cltClientTop_lblPhone').innerText = "Home Phone";
						parentDoc.getElementById('cltClientTop_lblStatus').innerText ="Status";
						parentDoc.getElementById('cltClientTop_lblTitle').innerText = "Title";
					}
					break;				
			}
		}		
		</script>
	</BODY>
</HTML>