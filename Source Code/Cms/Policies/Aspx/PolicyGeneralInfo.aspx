<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%--<%@ Register TagPrefix="webcontrol" TagName="CmsTimer" Src="/cms/cmsweb/webcontrols/CmsTimePicker.ascx" %>--%>

<%@ Page Language="c#" CodeBehind="PolicyGeneralInfo.aspx.cs" AutoEventWireup="false"
    ValidateRequest="false" Inherits="Cms.Policies.Aspx.PolicyGeneralInfo" %>

<%@ Register TagPrefix="cmsb" Namespace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>APP_LIST</title>
    <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet" />
    <%--<meta http-equiv="Page-Enter" content="revealtrans(duration=0.0)" />
		<meta http-equiv="Page-Exit" content="revealtrans(duration=0.0)" />--%>
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <script type="text/javascript" src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/common.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/form.js"></script>
    <script language="javascript" type="text/javascript" src="/cms/cmsweb/Scripts/Calendar.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
    <%--		 
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/JQCommon.js"></script>	--%>
    <script language="javascript" type="text/javascript">	
		var refWindow;		
		//shows the submit anyway input messages
		var refSubmitWin;
		var mortgageeBill = '11276';
		var InsuredMortgageeBill = '11278';	
		//Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
            //		function cmbAPP_TERMS_Change()
            //		{
            //			if(document.getElementById("cmbAPP_TERMS").selectedIndex!="-1" && document.getElementById("cmbAPP_TERMS").options[document.getElementById("cmbAPP_TERMS").selectedIndex].value!="")
            //				__doPostBack("cmbAPP_TERMS_Change","1");				
            //			else
            //				return false;
            //		}		
		//Commented till here 
		//Formats the amount and convert 111 into 1.11	
		
		
			
		function FormatAmount(txtAmount)
		{		

       
			if (txtAmount.value != "")
			{
				amt = txtAmount.value;
                
				if(sDecimalSep == "," && sGroupSep == ".")
				{
				    amt = ReplaceAll(amt,".","~");
				    amt = ReplaceAll(amt,",",".");
				    amt = ReplaceAll(amt,"~",",");
				}
				
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
		
		function VerifyPolicy()
	    {		    
		    var url;  
		    
		    if(refWindow != null)
			{
				refWindow.close();				
			}
			
		    url="/cms/application/Aspx/ShowDialog.aspx?CALLEDFROM=<%=Cms.CmsWeb.cmsbase.CALLED_FROM_PROCESS_POLICY%>&CUSTOMER_ID="
		     + document.getElementById('hidCustomerID').value + "&POLICY_ID=" + document.getElementById('hidPOLICY_ID').value + "&POLICY_VERSION_ID=" 
		     + document.getElementById('hidPOLICY_VERSION_ID').value + "&LOBID=" + document.getElementById('hidLOBID').value ;
		    refWindow=window.open(url,"Quote","resizable=yes,scrollbars=yes,width=800,height=500,left=150,top=50");	
	    }	
		
		// Shows policy mandatory information
		function ShowPolicyMsg()
		{
			if(parent.refSubmitWin !=null)
			{
				parent.refSubmitWin.close();
			}
			parent.refSubmitWin=window.open('/cms/application/Aspx/ShowDialog.aspx?CALLEDFROM=POLNEWBUSINESS','BRICS','resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500');
			wBody=parent.refSubmitWin.document.body;			
			parent.refSubmitWin.document.write(document.getElementById("hidPOLHTML").value);
			parent.refSubmitWin.document.title = document.getElementById("hidRulesMessage").value; // "EBIX Advantage - Rules Mandatory Information";
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
		<%}	%>
		
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
		<%}	%>
		
		<% if(gIntShowQuote==4)	{%>			
			alert('<%=gIntShowQuote4 %>');
		<%}%>
		
		
		function ShowQuote()
		{				
			ShowPopup('/cms/application/Aspx/Quote/Quote.aspx?CUSTOMER_ID='+ <%= gIntCUSTOMER_ID %> +"&APP_ID="+<%= gIntAPP_ID %>+"&APP_VERSION_ID="+ <%= gIntAPP_VERSION_ID %>  +    "&LOBID="+ <%= gstrLobID %>  + "&QUOTE_ID="+ <%= gIntQuoteID %>+"&SHOW="+ <%= gIntShowQuote %> ,'Quote', 700, 600);
		}
		
		function AddData()
		{ 		
			document.getElementById('hidCustomerID').value	=	'NEW';
			document.getElementById('hidAppID').value	=	'NEW';			
			document.getElementById('txtAPP_NUMBER').value  = '';
			document.getElementById('txtPOLICY_DISP_VERSION').value  = '1.0';			
			document.getElementById('cmbAPP_TERMS').options.selectedIndex = -1;
			document.getElementById('txtAPP_INCEPTION_DATE').value  = '';
			document.getElementById('txtAPP_EFFECTIVE_DATE').value  = DateAdd();
			document.getElementById('txtAPP_EXPIRATION_DATE').value  = '';
			document.getElementById('cmbAPP_LOB').options.selectedIndex = -1;
			document.getElementById('cmbPOLICY_SUBLOB').options.selectedIndex = -1;		
			document.getElementById('cmbCSR').options.selectedIndex = -1;
			document.getElementById('cmbPRODUCER').options.selectedIndex=-1;
			document.getElementById('hidSUB_LOB').value='';			
			document.getElementById('cmbBILL_TYPE').options.selectedIndex=-1;
           			
			//document.getElementById('txtYEAR_AT_CURR_RESI').value  = '';
			//document.getElementById('txtYEARS_AT_PREV_ADD').value  = '';
			//document.getElementById('txtRECEIVED_PRMIUM').value='';
			//document.getElementById('cmbPROXY_SIGN_OBTAINED').options.selectedIndex=-1;
			//document.getElementById('cmbPIC_OF_LOC').options.selectedIndex=-1;	
				
			ChangeColor();
			DisableValidators();			
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
		        
		    }
		    else
		    {
		    EnableValidator('rfvState_ID',false);
		    }
		   
		}
		function populateXML()
		{	
             var varSysID = "<%=GetSystemId()%>"; 
             document.getElementById('hidBILL_TYPE_ID').value = "<%=strBillType%>";
		//alert(document.getElementById('cmbAGENCY_ID').value)
			SetValidationControl();		
				
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
						    //GetCSRProducer(document.getElementById('hidAPP_AGENCY_ID').value, document.getElementById('hidLOBID').value);				
							populateFormData(tempXML,APP_LIST);	
							document.getElementById('txtPOLICY_LEVEL_COMISSION').value=formatAmount(document.getElementById('txtPOLICY_LEVEL_COMISSION').value,2);
							if(document.getElementById('txtAPP_EFFECTIVE_DATE'))
							{
							    if(document.getElementById('txtAPP_EFFECTIVE_DATE').value != "")
							    {
							        document.getElementById('rfvAPP_EFFECTIVE_DATE').style.display = 'none'
							    }
							}
							//set the  hidden sublob
							document.getElementById('hidSUB_LOB').value = document.APP_LIST.cmbPOLICY_SUBLOB.value;										
						
						     if (document.getElementById('hidDOWN_PAY_MODE').value !='')
							{
								for(i=0;i<document.getElementById('cmbDOWN_PAY_MODE').options.length;i++)
								{
									if(document.getElementById('cmbDOWN_PAY_MODE').options[i].value==document.getElementById('hidDOWN_PAY_MODE').value)	
										document.getElementById('cmbDOWN_PAY_MODE').options[i].selected=true;
								}
							}		
							
							//if(document.getElementById('txtPREFERENCE_DAY').value=="0")
    							//document.getElementById('txtPREFERENCE_DAY').value="";	
								
//							if(document.getElementById('txtRECEIVED_PRMIUM').value=="0.00")
//								document.getElementById('txtRECEIVED_PRMIUM').value="";			
						}						
					}
				}
				
//				else
//				{			   
//					if (document.getElementById('hidCustomerID') != null && document.getElementById('hidCustomerID').value != 'NEW')
//					{					   
//						if (document.getElementById('hidFormSaved')!= null &&(document.getElementById('hidFormSaved').value!=1 && document.getElementById('hidFormSaved').value!=3) )
//						{								
//							//document.getElementById('txtCUSTOMER_NAME').style.display ="none";	 
//							//document.getElementById('imgSelect').style.display ="none";	
//							/* 	
//			 				if (document.getElementById('txtCUSTOMERNAME')!=null)
//							{			 
//								document.getElementById('txtCUSTOMERNAME').style.display ="inline";		
//								document.getElementById('cmbAPP_LOB').style.display ="inline";													
//							}
//							else
//							{ 
//								document.getElementById('txtCUSTOMER_NAME').style.display ="inline";	 
//								document.getElementById('imgSelect').style.display ="inline";	 	
//								
//								document.getElementById('cmbAPP_LOB').style.display ="none";			 									
//							}
//							*/			
//						}
//						else
//						{		
//						    /*				 
//							if (document.getElementById('hidAppID').value !='NEW')
//							{  							
//								
//							}
//							else
//							{  		
//							    //changed from inline ,  to handle error on save, Charles, 5-Mar-10				  
//								document.getElementById('txtCUSTOMER_NAME').style.display ="none";	 
//								document.getElementById('imgSelect').style.display ="none";	 	
//								//document.getElementById('txtCUSTOMERNAME').value ='';
//								
//								document.getElementById('cmbAPP_LOB').style.display ="inline";																				
//							}
//							*/
//						}				 
//					} 
//					else
//					{					 
//						// control is coming from the customer section
//						/*
//						if(document.getElementById('hidFormSaved')!= null  && document.getElementById('hidFormSaved').value=='4')
//						{
//							document.getElementById('txtCUSTOMERNAME').style.display ="inline";
//							document.getElementById('txtCUSTOMER_NAME').style.display ="none";	 
//							document.getElementById('imgSelect').style.display ="none";	 	
//							
//							document.getElementById('cmbAPP_LOB').style.display ="none";							
//							document.getElementById('txtCUSTOMERNAME').style.display ="inline";
//							
//						}
//						else
//						{					 
//							document.getElementById('txtCUSTOMER_NAME').style.display ="inline";	 
//							document.getElementById('imgSelect').style.display ="inline";														
//							document.getElementById('cmbAPP_LOB').style.display="none";							
//						}
//						*/
//					}
//				}

//                if(document.getElementById('cmbAPP_LOB').selectedIndex > 0 && document.getElementById('hidAppID').value=='NEW')
//                {
                    GetCSRProducer(document.getElementById('hidAPP_AGENCY_ID').value, document.getElementById('hidLOBID').value);
//                }
				
				if(document.getElementById("hidSUB_LOB")!=null)
				{
					var SUBLOBID =document.getElementById("hidSUB_LOB").value;
					SelectComboOption("cmbPOLICY_SUBLOB",SUBLOBID);		
				}
				if(document.getElementById("hidCSR")!=null)
				{
					var CSR = document.getElementById("hidCSR").value;
					SelectComboOption("cmbCSR",CSR);		 
				}				 
				if(document.getElementById("hidProducer")!=null)
				{
					var Producer= document.getElementById("hidProducer").value;
					SelectComboOption("cmbProducer",Producer);
				}
				if(document.getElementById("hidSTATE_ID")!=null)
				{
					var STATE_ID= document.getElementById("hidSTATE_ID").value;
					
					SelectComboOption("cmbState_ID",STATE_ID);
				}
				
				if (document.getElementById("hidPOLICY_TYPE")!=null)
				{
				var POLICY_TYPE= document.getElementById("hidPOLICY_TYPE").value;
					
					SelectComboOption("cmbPOLICY_TYPE",POLICY_TYPE);
				}
								 
				if (document.getElementById('hidLOBID')!=null)
				{
					SelectedLOBID=document.getElementById('hidLOBID').value;	
				
//					if(SelectedLOBID!="1" || SelectedLOBID!="6") // Home Or Rental
//					{					
//						document.getElementById('trPropInspCr').style.display='none';
//					}
//					else
//					{					
//					 	//document.getElementById('trPropInspCr').style.display='inline';
//					 	//mandPropInspCredit();
//					}	
				}
				if (document.getElementById('hidINSTALL_PLAN_ID')!=null)
				{
				SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidINSTALL_PLAN_ID").value); 
				}
				//setDefaultPlan();
			   if (document.getElementById('hidBILL_TYPE_ID')!=null)
			   {
			   SelectComboOption('cmbBILL_TYPE',document.getElementById("hidBILL_TYPE_ID").value); 
			   }
				
				
//				var LOBID	= document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].value;
//				if (LOBID=="1" || LOBID=="6") // Home or Rental
//				{	
//					//document.getElementById('trPropInspCr').style.display='inline';
//					//mandPropInspCredit();
//				}
//				else
//				{
//				  	document.getElementById('trPropInspCr').style.display='none';
//				}

				 if (document.getElementById('hidDOWN_PAY_MODE').value !=='')
					{
                         if (varSysID != 'S001' && varSysID != 'SUAT')
                         {
                            fillDownPayMode();
                         }
					        
							for(i=0;i<document.getElementById('cmbDOWN_PAY_MODE').options.length;i++)
							{
								if(document.getElementById('cmbDOWN_PAY_MODE').options[i].value==document.getElementById('hidDOWN_PAY_MODE').value)	
									document.getElementById('cmbDOWN_PAY_MODE').options[i].selected=true;
							}
					}
					
				
			}
			
			//Display the value of underwriter only when policy has been created and underwriter saved			
			/*
			if(document.getElementById('hidPOLICY_ID')==null || document.getElementById('hidPOLICY_ID').value=="")
			{
				if(document.getElementById('lblUNDERWRITER').innerText=="" || document.getElementById('lblUNDERWRITER').innerText=="0")
				{
					if(document.getElementById('hidLangCulture').value!="" && document.getElementById('hidLangCulture').value=="pt-BR")
					{
						document.getElementById('lblUNDERWRITER').innerText="A ser atribuído";
					}
					else
					{
						document.getElementById('lblUNDERWRITER').innerText="To be Assigned";
					}
				}
			}
			*/
			return false;
//			var temp;
//			temp =1;



		}

	 	function FillSubLOB()
		{   		
		if (document.getElementById('trDETAILS')!=null  && (document.getElementById('trDETAILS').style.display == "inline" || document.getElementById('trDETAILS').style.display == "") )
		{
			document.getElementById('cmbPOLICY_SUBLOB').innerHTML = '';
			var Xml = document.getElementById('hidLOBXML').value;
			
			var LOBId ="";
			//var stID="";
			
			if(document.getElementById('cmbAPP_LOB').style.display=="inline")
			{
				if (document.getElementById('cmbAPP_LOB').selectedIndex!= -1)
				{
					LOBId	= document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].value;
					//stID	= document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
				}
			}
			else
			{
					LOBId	= document.getElementById('hidLOBId').value;
					//stID	= document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;
			} 
			
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
				document.getElementById('cmbPOLICY_SUBLOB').add(oOption);
				 
				for(i=0; i<tree.childNodes.length; i++)
				{
					nodValue = tree.childNodes[i].getElementsByTagName('LOB_ID');
					//stateValue = tree.childNodes[i].getElementsByTagName('STATE_ID');
					if (nodValue != null)
					{
						if (nodValue[0].firstChild == null)
							continue
						//alert(nodValue[0].firstChild.text + "----" + stateValue[0].firstChild.text)						
						if ((nodValue[0].firstChild.text == LOBId)) /*&& stateValue[0].firstChild.text==stID*/
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
									document.getElementById('cmbPOLICY_SUBLOB').add(oOption);
								}
							}
						}
					}
				}
			}
			document.getElementById('cmbPOLICY_SUBLOB').selectedIndex=-1;	
				 
			// hide and display the checkbox as per the selection of LOB		
			/*
			if(LOBId!="1" || LOBId!="6")  // Is not Home
			{				
				document.getElementById('trPropInspCr').style.display='none';				
			}	
			else
			{	
				document.getElementById('trPropInspCr').style.display='inline';
				//mandPropInspCredit();
			}*/	 
		}		
	}
	
	function fillBillingPlan()
	
	{
        //Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
        //		if (document.getElementById('cmbAPP_TERMS').options.selectedIndex==-1)
        //			return false;
        //Commented till here
        
        if (document.getElementById('txtAPP_TERMS').value=="")
            return false;
		var ctrl = document.getElementById('cmbBILL_TYPE');
		var nodValueColor;
		
		if (ctrl == null)
			return;
		// Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
    	//var policyTerm =document.getElementById('cmbAPP_TERMS').options[document.getElementById('cmbAPP_TERMS').options.selectedIndex].value;
    	//Commented till here 
    	


    	var policyTerm =document.getElementById('txtAPP_TERMS').value;
    	
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
	 
	return false;
	} 
	
	function SetBillTypeFlag()
	{ 
	   document.getElementById('hidBILL_TYPE_FLAG').value	=	'1';
	   if(!IsProperDate(document.getElementById('txtAPP_EFFECTIVE_DATE'))) return false;
	   if(!IsProperDate(document.getElementById('txtAPP_INCEPTION_DATE'))) return false;
	   //ChangeDefaultDate();
	   
	}
	
		function setHidSubLob()
		{
			document.getElementById('hidSUB_LOB').value = document.getElementById('cmbPOLICY_SUBLOB').options[document.getElementById('cmbPOLICY_SUBLOB').selectedIndex].value;			
		}
		
		function setHidAgencyID()
		{
		    if(document.getElementById('cmbAGENCY_ID').options[document.getElementById('cmbAGENCY_ID').selectedIndex].value != "")
		    {
			    document.getElementById('hidAPP_AGENCY_ID').value = document.getElementById('cmbAGENCY_ID').options[document.getElementById('cmbAGENCY_ID').selectedIndex].value;						   
			    
			    GetCSRProducer(document.getElementById('hidAPP_AGENCY_ID').value, document.getElementById('hidLOBID').value);
			}
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
		/*
		function sethidStateID()
		{
			document.getElementById('hidStateID').value = document.getElementById('cmbSTATE_ID').options[document.getElementById('cmbSTATE_ID').selectedIndex].value;			 
		}	
		*/		
		
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
			 
			sTerm = document.APP_LIST.txtAPP_TERMS.value;//Addeb by pradeep 
			 
		  
			
			if(sTerm == "0" || sTerm == "")
			{
			    return;
				 //document.APP_LIST.txtAPP_EXPIRATION_DATE.value="";				
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
					if(sDay==30 || sDay==31 || sDay==28 || sDay==29) 
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
			if (top.topframe.main1.menuXmlReady == false)
				setTimeout("setMenu();",1000);
			top.topframe.disableMenu("1,3");
			//No need to make menus , if record has been deleted,
			//Hence checking whether record deleted or not
			if (document.getElementById("hidFormSaved") != null && document.getElementById("hidFormSaved").value != "5")
			{
				top.topframe.main1.activeMenuBar = '1';
				top.topframe.createActiveMenu();
				//top.topframe.enableMenus("1","ALL");
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
						//if policy status is rejected can not go to policy information page;
						//i-track # 862
						top.topframe.enableMenus("1","ALL");
						if(document.getElementById("hidPolicyStatus").value!= "" 
						&& document.getElementById("hidPolicyStatus").value!= '<%=  Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_REJECT.ToString()%>'
						&& document.getElementById("hidPolicyStatus").value!= '<%=  Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_ISSUE.ToString()%>'  
						&& document.getElementById("hidPolicyStatus").value!= '<%=  Cms.BusinessLayer.BlCommon.ClsCommon.POLICY_STATUS_UNDER_RENEW.ToString()%>'  

						)
						{
						    top.topframe.enableMenu("1,2,3");
						}						
						    
						//parent.parent.location.href = "..\client\aspx\CustomerManagerIndex.aspx?SubmitApp=1";
						//Hide the images in the ClientTop after the policy has been created successfully
						parentObj = parent.document;
						/*
						if(parentObj.getElementById("cltClientTop_imgSubmitAnyway"))
							parentObj.getElementById("cltClientTop_imgSubmitAnyway").style.display = "none";
							
						if(parentObj.getElementById("cltClientTop_imgSubmitApp"))
							parentObj.getElementById("cltClientTop_imgSubmitApp").style.display = "none";
						if(parentObj.getElementById("cltClientTop_imgVerifyApp"))
							parentObj.getElementById("cltClientTop_imgVerifyApp").style.display = "none";							
						if(parentObj.getElementById("cltClientTop_imgQuote"))
							parentObj.getElementById("cltClientTop_imgQuote").style.display = "none";
						*/
					}
					
					/*
					if (document.getElementById("txtPOLICY_STATUS").value != "" && document.getElementById("txtPOLICY_STATUS").value == "Unconfirmed")
					{
						parentObj = parent.document;
						if(parentObj.getElementById("cltClientTop_imgSubmitAnyway"))
							parentObj.getElementById("cltClientTop_imgSubmitAnyway").style.display = "none";
						if(parentObj.getElementById("cltClientTop_imgVerifyApp"))
							parentObj.getElementById("cltClientTop_imgVerifyApp").style.display = "none";
					}
					*/
				}
				else
				{
					//Disabling the Risk information menu, as it is not req if application is not selected
					top.topframe.disableMenu("1,3");
					//disable application detail menu till application not added
					if(document.getElementById("hidPOLICY_ID")!=null ||  document.getElementById("hidPOLICY_ID").value!="" || document.getElementById("hidPOLICY_ID").value!='NEW' || document.getElementById("hidPOLICY_ID").value!='0')
					top.topframe.disableMenu("1,2,2");
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
		
			function CheckBillType(source,arguments)
			{
				if (document.getElementById('cmbBILL_TYPE').options.selectedIndex==-1)
				{
					arguments.IsValid = false;
					return;				
				}
			}				
			
			function BillType()
			{
                var varSysID = "<%=GetSystemId()%>"; 
			
				if(document.getElementById('cmbBILL_TYPE'))
				{
					var ctrl = document.getElementById('cmbBILL_TYPE');
					if (ctrl == null)
						return;
						
					if (ctrl.selectedIndex == -1 )
					{
						//By default n.a. should be visible
						document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',true);
					
						SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidFULL_PAY_PLAN_ID").value);
						
						//document.getElementById('trBILL_MORTAGAGEE').style.display="inline";
						return;
					}									
				
//					if(ctrl.options[ctrl.selectedIndex].value == "11150" ||ctrl.options[ctrl.selectedIndex].value == "8460" || ctrl.options[ctrl.selectedIndex].value == InsuredMortgageeBill)
					if(ctrl.options[ctrl.selectedIndex].value == "114329")
                    {
						document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',false);
						document.getElementById('cmbINSTALL_PLAN_ID').style.display = "inline";
						//SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidINSTALL_PLAN_ID").value); 
						
					}				
					else
					{
					
						document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',true);
						SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidINSTALL_PLAN_ID").value); 
						
					}
					var LOB = document.getElementById('hidLOBID').value;
			
					if (document.getElementById("hidInstall").value=="Y")
					{
					document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',true);
					}
					else
					{
					document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',false);
					}
//					if((LOB==<%=((int)enumLOB.REDW).ToString()%> || LOB==<%=((int)enumLOB.HOME).ToString()%>) && (ctrl.options[ctrl.selectedIndex].value=='11276' || ctrl.options[ctrl.selectedIndex].value=='11277' || ctrl.options[ctrl.selectedIndex].value=='11278'))
//						document.getElementById('trBILL_MORTAGAGEE').style.display="inline";
//					else
//						document.getElementById('trBILL_MORTAGAGEE').style.display="none";	
				HideShowBillingInfo();	

                
                if (varSysID.toUpperCase() != 'S001' && varSysID.toUpperCase() != 'SUAT')	
                {	     
				    fillDownPayMode();
                }
				SelectComboOption('cmbDOWN_PAY_MODE',document.getElementById("hidDOWN_PAY_MODE").value);
                document.getElementById('hidBILL_TYPE_ID').value=ctrl.options[ctrl.selectedIndex].value;
//                if(document.getElementById('cmbBILL_TYPE').options.selectedIndex != 0)   
//                             document.getElementById('hidBILL_TYPE_ID').value = "";
//                else
//				document.getElementById('hidBILL_TYPE_ID').value=ctrl.options[ctrl.selectedIndex].value; 
				
				}

                
			}
			
			function HideShowBillingInfo()
			{ 
				var ctrl = document.getElementById('cmbBILL_TYPE');
                
                var varSysID = "<%=GetSystemId()%>"; //Added by RC for TFS 1000

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


//				if(ctrl.options[ctrl.selectedIndex].value == "8460" || ctrl.options[ctrl.selectedIndex].value == "11150" || ctrl.options[ctrl.selectedIndex].value == "11278" || ctrl.options[ctrl.selectedIndex].value == mortgageeBill || ctrl.options[ctrl.selectedIndex].value == InsuredMortgageeBill)
                if(ctrl.options[ctrl.selectedIndex].value == "114329" )
				{
					document.getElementById('lblDOWN_PAY_MODE').style.display="none";
					document.getElementById('lblINSTALL_PLAN_ID').style.display="inline";
                    document.getElementById('lblINSTALL_PLAN_ID').innerHTML = "Billing Plan";
					//document.getElementById('cmbDOWN_PAY_MODE').style.display="inline";     
					document.getElementById('cmbINSTALL_PLAN_ID').style.display="inline";
					document.getElementById('spnINSTALL_PLAN_ID').style.display="inline";
					//document.getElementById('spnDOWN_PAY_MODE').style.display="inline";

                    //Added by RC
                    if(varSysID.toUpperCase()=="S001" || varSysID.toUpperCase()=="SUAT")
                    {
                        EnableValidator('rfvDOWN_PAY_MODE',false);
                    }
                    else
                    {
                        EnableValidator('rfvDOWN_PAY_MODE',true);
                    }
					
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
                    
                    //Added by A. Goel
//                    alert(ctrl.options[ctrl.selectedIndex].value);
//                    document.getElementById('cmbBILL_TYPE').options.selectedIndex = -1;			
                    
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
					return true;					
				}
				return false;			
			}
			
			/*
			function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 250 ) 
				{
					arguments.IsValid = false;
					return;   // invalid userName
				}
			}*/			
			
			// function for finding number of days between two applications.
			function CheckDate(objSource , objArgs)
			{	
			    objArgs.IsValid=true; //commnted by lalit,March 16,2011.For brazil implimentation only.
			    return;		 
			  var inceptiondate=document.APP_LIST.txtAPP_EFFECTIVE_DATE.value;
			  
			  //Added for Multilingual Support
			  if (sCultureDateFormat == 'DD/MM/YYYY') 			  
			  {
			    aDateArr = inceptiondate.split('/');
					    					    
			    strDay = aDateArr[0];
			    strMonth = aDateArr[1];
			    strYear = aDateArr[2];						
					    
			    inceptiondate = strMonth + '/' + strDay + '/' + strYear
			    var d1=new Date("<%=System.DateTime.Now.Month%>" + '/' + "<%=System.DateTime.Now.Day%>" + '/' + "<%=System.DateTime.Now.Year%>");
			    
			  }
			  else
			  {
			    var d1=new Date("<%=System.DateTime.Now%>"); 			    
			  }
			  
			  var d2= new Date(inceptiondate); 
			  
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
			    //Changed by Lalit Feb 24,2011
	            //the Inception Date is greater than policy Effective Date since policy could be risk elapsed, which means
                //that the period of insurance of the policy is in the past and the application/ policy was done in a future date
                //i-track # 644

                //Added by RC
                var varSysID = "<%=GetSystemId()%>";
                if(varSysID.toUpperCase() != 'S001' && varSysID.toUpperCase() != 'SUAT')
                {
		            setExpDate();	
                }

		        return;
			 var msg= '<%=InceptionMsg %>'// 'Inception date can not be greater then Effective date';
			//If Inception date is greater then Effective date  : Set Effective date in Inception date
				
				if(document.getElementById('revAPP_EFFECTIVE_DATE').isvalid == true)
				{
				
					if(document.getElementById('txtAPP_INCEPTION_DATE').value == "")
					{
						 document.getElementById('txtAPP_INCEPTION_DATE').value = document.getElementById('txtAPP_EFFECTIVE_DATE').value
					}
					
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
					    alert(msg);
						document.getElementById('txtAPP_INCEPTION_DATE').value = document.getElementById('txtAPP_EFFECTIVE_DATE').value
					}
					 
					if(dtIncep < dtEff)
					{
					 //DONT CHANGE INCEPTION DATE
					}
					
					//ShowExpirationDate();//Commented by Pradeep 
									
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
				
				if (CheckDelete() == false)
				{
				    //document.getElementById('cmbAPP_LOB').focus();				    
					
					//populateXML();
					//FillSubLOB();
				
					setValues();
					populateXML();	
					BillType();
					//populateXML();
					HideShowBillingInfo();						
					//DisplayPreviousYearDesc();					
					CommChange();	
					fillPayor();			
					ChangeColor();
					//DisplaySubLOB();
                    CalculateTerm();
					
			}		
			
					
							
				if(document.getElementById("hidFocusFlag")!=null)
				{
					if(document.getElementById("hidFocusFlag").value=="1")
					{
						if(document.getElementById("cmbAPP_LOB"))
						{
							if(document.getElementById("cmbAPP_LOB").style.display!='none')
							{
								//document.getElementById("cmbAPP_LOB").focus();
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
				
				/*
				var strBill = new String(document.getElementById('lblBILL_MORTAGAGEE').innerHTML);
				strBill = replaceAll(strBill," ","");
				if(strBill=="")
					document.getElementById('lblBILL_MORTAGAGEE').innerHTML = "N.A.";
			    */	
			   DisablePolicyLevelCommission();	
			   SubmitAnyway();
			   
			   
			   RenewalOldPolicyNO();		
			}
			
			//Added by Lalit for submit anyway button, only user equivalent to supervisor can submit anyway
				//i-track # 1090
				function SubmitAnyway()
				{
				
                   var UserEquivalent_SuperVisor = document.getElementById('hidUSER_SUPERVISOR').value;
				    if(UserEquivalent_SuperVisor=='N')
				    {
				        if(document.getElementById('btnSubmitAnyway')!=null)
				           document.getElementById('btnSubmitAnyway').style.display='none' ;
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
                           else 
                            {
                                document.getElementById('tdBrokerName').colSpan = 1;
                                document.getElementById('tdChkPolicyCommission').style.display = 'inline';
                                document.getElementById('tdtxtPolicyCommission').style.display = 'inline';
                                //document.getElementById('tdChkPolicyCommission').disabled=true;
                                //document.getElementById('tdtxtPolicyCommission').disabled=true;
                            }
                        }

			/*
			function DisplaySubLOB()
			{
				var LOB = document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].value;
				var CalledFrom = document.getElementById('hidCallefroms').value;
				var State="0";
				if(document.getElementById("cmbSTATE_ID").selectedIndex>0)
					State = document.getElementById("cmbSTATE_ID").options[document.getElementById("cmbSTATE_ID").selectedIndex].value;

				if( LOB == "8" || LOB == "3" || LOB == "4" || LOB == "5" || LOB == "1" || LOB == "6" || LOB == "7" || CalledFrom == "MOT" || CalledFrom == "WAT" || CalledFrom == "UMB" || CalledFrom == "HOME" || CalledFrom == "REDW" || CalledFrom == "GEN" || (State=="22" && (CalledFrom == "AUTO" || LOB == "2")))
				 { 
					document.getElementById('capPOLICY_SUBLOB').style.display='none';
					document.getElementById('cmbPOLICY_SUBLOB').style.display='none';
				 }				
			}
						
			function DisplayPreviousYearDesc()
			{
				if (parseInt(document.getElementById('txtYEAR_AT_CURR_RESI').value) < 3 && document.getElementById('txtYEAR_AT_CURR_RESI').value != "" && document.getElementById('txtYEAR_AT_CURR_RESI').value != "0" && document.getElementById('txtYEAR_AT_CURR_RESI').value != "00")
				{ 
					document.getElementById('txtYEARS_AT_PREV_ADD').style.display='inline';
					document.getElementById('capYEARS_AT_PREV_ADD').style.display='inline';
					document.getElementById("rfvYEARS_AT_PREV_ADD").setAttribute('enabled',true);
					document.getElementById('spnYEARS_AT_PREV_ADD').style.display='inline';	
					//document.getElementById('txtYEARS_AT_PREV_ADD').focus();
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
			}*/
			
			function ChkBillingPlan(objSource , objArgs)
			{ 
				objArgs.IsValid=true;
					return;				
			}
			
			function fillDownPayMode()
			{
		 
				//added by pravesh
				//Split to get ID 
				//var PlanId = document.getElementById('cmbINSTALL_PLAN_ID').options[document.getElementById('cmbINSTALL_PLAN_ID').selectedIndex].value;
				var PlanId =document.getElementById('hidINSTALL_PLAN_ID').value;
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
							
							var ListDOWN_PAY_MODE=new Array();
							 
							if (nodeDOWN_PAY_MODE != null && nodeDOWN_PAY_MODE_decs != null)
							{
								if (nodeDOWN_PAY_MODE[0] != null &&  nodeDOWN_PAY_MODE_decs[0] != null )
								{
//									oOption = document.createElement("option");
//									oOption.value = nodeDOWN_PAY_MODE[0].firstChild.text;
//									oOption.text = nodeDOWN_PAY_MODE_decs[0].firstChild.text;
									
									ListDOWN_PAY_MODE[0]=new Array() 
									ListDOWN_PAY_MODE[0][0]=nodeDOWN_PAY_MODE[0].firstChild.text;
									ListDOWN_PAY_MODE[0][1]=nodeDOWN_PAY_MODE_decs[0].firstChild.text;
									
//									if(oOption.value==document.getElementById('hidPOLDOWN_PAY_MODE').value)
//								        oOption.style.color='#ff0000'; 
								    
									//document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
								}
							} 
							if (nodeDOWN_PAY_MODE1 != null && nodeDOWN_PAY_MODE_decs1 != null)
							{
								if (nodeDOWN_PAY_MODE1[0] != null &&  nodeDOWN_PAY_MODE_decs1[0] != null && nodeDOWN_PAY_MODE_decs1[0].firstChild.text!="null")
								{
//									oOption = document.createElement("option");
//									oOption.value = nodeDOWN_PAY_MODE1[0].firstChild.text;
									ListDOWN_PAY_MODE[1]=new Array()
									ListDOWN_PAY_MODE[1][0]= nodeDOWN_PAY_MODE1[0].firstChild.text;
									ListDOWN_PAY_MODE[1][1]=nodeDOWN_PAY_MODE_decs1[0].firstChild.text;
									
//									if(oOption.value==document.getElementById('hidPOLDOWN_PAY_MODE').value)
//									 oOption.style.color='#ff0000'; 
//								   
//									oOption.text = nodeDOWN_PAY_MODE_decs1[0].firstChild.text;
//									document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
								}
							} 
							if (nodeDOWN_PAY_MODE2 != null && nodeDOWN_PAY_MODE_decs2 != null)
							{
								if (nodeDOWN_PAY_MODE2[0] != null &&  nodeDOWN_PAY_MODE_decs2[0] != null && nodeDOWN_PAY_MODE_decs2[0].firstChild.text!="null")
								{
								 
//									oOption = document.createElement("option");
//									oOption.value = nodeDOWN_PAY_MODE2[0].firstChild.text;
//									oOption.text = nodeDOWN_PAY_MODE_decs2[0].firstChild.text;
									
									ListDOWN_PAY_MODE[2]=new Array()
									ListDOWN_PAY_MODE[2][0]= nodeDOWN_PAY_MODE2[0].firstChild.text;
									ListDOWN_PAY_MODE[2][1]=nodeDOWN_PAY_MODE_decs2[0].firstChild.text;
									
									//if(oOption.value==document.getElementById('hidPOLDOWN_PAY_MODE').value)
									 //oOption.style.color='#ff0000'; 
								     
									//document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
								 
								}
								
							}
				            
						    
				
							ListDOWN_PAY_MODE.sort(sortMultiDimensional); 
							 
							for(Count=0; Count<ListDOWN_PAY_MODE.length; Count++)
							{
							    oOption = document.createElement("option");
								oOption.value = ListDOWN_PAY_MODE[Count][0];
								oOption.text = ListDOWN_PAY_MODE[Count][1];
								
								if(oOption.value==document.getElementById('hidPOLDOWN_PAY_MODE').value)
									oOption.style.color='#ff0000'; 
								document.getElementById('cmbDOWN_PAY_MODE').add(oOption);
                             
                            }
                            //Added by Pradeep Kushwaha on 09-03-2011 -iTrack 920 (“DOWN PAYMENT METHOD” SHOULD BE “BOLETO” AS DEFAULT.)
                            
                            if($("#hidDOWN_PAY_MODE").val()=="")
                            {
                                $("#cmbDOWN_PAY_MODE option[value='14558']").attr("selected", "selected");
                                $("#hidDOWN_PAY_MODE").val( $("#cmbDOWN_PAY_MODE option:selected").val());
                            }
                            else{
                                $("#cmbDOWN_PAY_MODE option[value='"+$("#hidDOWN_PAY_MODE").val()+"']").attr("selected", "selected");
                                $("#hidDOWN_PAY_MODE").val( $("#cmbDOWN_PAY_MODE option:selected").val());
                                
                            }
                            
                            
                            //Added till here 
						}				
					}			
				}   	
				return;	
					///end here
			}	
	 
		function sortMultiDimensional(a,b)
        {
            // this sorts the array using the second element    
            return ((a[1] < b[1]) ? -1 : ((a[1] > b[1]) ? 1 : 0));
        }
 
	function fillDownPay()
	{		
		if (document.getElementById('cmbDOWN_PAY_MODE').options.selectedIndex !=-1)
			document.getElementById('hidDOWN_PAY_MODE').value=document.getElementById('cmbDOWN_PAY_MODE').options[document.getElementById('cmbDOWN_PAY_MODE').options.selectedIndex].value;
	}
	function ResetForm()
	{
		//temp=1;
		document.APP_LIST.reset();
		//DisplayPreviousYearDesc();
		populateXML();
		BillType();
		CommChange();	
		fillPayor()	
		DisableValidators();
		ChangeColor();
		
		return false;
	}	
	
	function ProcessKeypress() 
	{
		if (event.keyCode == 13) 
		{    
			//ChangeDefaultDate();
		}
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

	function fillPayor()
	{
	    var combo = document.getElementById("cmbCO_INSURANCE");
	    var comboPayor = document.getElementById("cmbPAYOR");
	    
	    if(combo.options[combo.options.selectedIndex].value =="")
	    {
	        return;
	    }
	    
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
	        document.getElementById('spnBILLTO').style.display = 'none';
	    }
	    else
	    {
	       document.getElementById('txtBILLTO').value = document.getElementById('txtCUSTOMERNAME').value; 
//	       document.getElementById('spnBILLTO').style.display = 'inline';
            document.getElementById('spnBILLTO').setAttribute("display",'inline');
	    }
	    
	    document.getElementById('hidPAYOR').value = comboPayor.options[comboPayor.options.selectedIndex].value;
	    
	    comboPayor.disabled = true;
	}
	//Added by Pradeep on 22-July-2011
	//Function to format policy level commission based on policy currency selected 
	//iTrack-1411,1370 
	function fnformatCommission(value) {
		    var DecimalSep=".";
		    if($("#cmbPOLICY_CURRENCY option:selected").val()=="2")
		        DecimalSep=",";
		    if(DecimalSep != 'undefined')
		        value = ReplaceAll(value, DecimalSep, ".");
		        
            if (isNaN(value)) return value;
            
		    if (value != "")
		        value = parseFloat(value).toFixed(2);
		    value = ReplaceAll(value, ".", DecimalSep)
		    
		    return value;
	}//function fnformatCommission(value) {
	//till here 
	//Modified by Pradeep on 22-July-2011 iTrack-1411,1370
	function FormatAmountForSum(num) {
	           num = ReplaceAll(num, ',', '.');   
		       return num;
	}
	//Till here 
		    function Validate(objSource, objArgs) {
		       
               var comm = parseFloat(FormatAmountForSum(document.getElementById(objSource.controltovalidate).value));

		       var varSysID = "<%=GetSystemId()%>"; //Added by Ruchika Chauhan on 7-Dec-2011 for TFS# 1211
               if (varSysID == 'S001' || varSysID == 'SUAT')	
               {	     
			        if (comm <= 0 || comm > 100) 
                    {
		                document.getElementById(objSource.controltovalidate).select();
		                objArgs.IsValid = false;
		            }
		            else
		                objArgs.IsValid = true;
                }
                else
                {
                    if (comm < 0 || comm > 100) 
                    {
		                document.getElementById(objSource.controltovalidate).select();
		                objArgs.IsValid = false;
		            }
		            else
		                objArgs.IsValid = true;
               
                }
		        
		    } 
	
	function ShowAlertMessageForDelete()
	{
	    var r=confirm(document.getElementById('lblDelete').value);
        return r;
	}
	
	function showHideAppDefMsg(flag)
	{
	    if(document.getElementById('hidAppID').value == 'NEW') 
	    {    
	        var msg = '<%=TEMP_APP_NUMBER %>';
	        var txt = document.getElementById('txtAPP_NUMBER').value 
	          
	        if(flag)
	        {
	            if(msg == txt)
	            document.getElementById('txtAPP_NUMBER').value = '';
	        }
	        else
	        {           
	            if(txt == "")
	                document.getElementById('txtAPP_NUMBER').value = msg;
	        }        
	    }
	}
	
	function GetCSRProducer(hidAPP_AGENCY_ID, hidLOBID)
	{  
		GlobalError=true;

		if(hidAPP_AGENCY_ID!="" && hidAPP_AGENCY_ID!="0" && hidLOBID!="" && hidLOBID!="0" )
		{
        
				var result = PolicyGeneralInfo.AjaxFetchCSRProducer(hidAPP_AGENCY_ID, hidLOBID);				
				return AjaxCallFunction_CallBack(result);	
		}	
	}
	function AjaxCallFunction_CallBack(response)
	{	 
	  if(document.getElementById('hidAPP_AGENCY_ID').value!="" && document.getElementById('hidAPP_AGENCY_ID').value!="0" )
		{ 		    
			handleResult(response);
			if(GlobalError)
			{
			    document.getElementById('cmbCSR').length=0;
		        document.getElementById('cmbPRODUCER').length=0;
		        
				return false;
			}
			else
			{
				return true;
			}
		}
		else 
			return true;		
	}
	function handleResult(res) 
	{
    	    
		if(!res.error)
		{		
		    if (res.value!="" && res.value!=null ) 
		    {
			    GlobalError=false;
    			
			    fillCSRproducer(res.value)
		    }
		    else
		    {		        
		        document.getElementById('cmbCSR').length=0;
		        document.getElementById('cmbPRODUCER').length=0;
		        document.getElementById('hidCSR').value = '-1';
		        document.getElementById('hidProducer').value = '0';
		        	        
			    GlobalError=true;
		    }
		}
		else
		{
			GlobalError=true;		
		}
	}
	function fillCSRproducer(strCSRXML)
	{	
        var flag=0;
		var objXmlHandler = new XMLHandler();
		var tree1 = objXmlHandler.quickParseXML(strCSRXML);
		if(tree1.childNodes.length==0)
		{
		document.getElementById('cmbCSR').length=0;
		document.getElementById('cmbPRODUCER').length=0;
			return;
		}	
		var tree = objXmlHandler.quickParseXML(strCSRXML).childNodes[0];
		
			oOption = document.createElement("option");
			oOption.value = "";
			oOption.text = "";
			oOptionP = document.createElement("option");
			oOptionP.value = "";
			oOptionP.text = "";
			document.getElementById('cmbCSR').length=0;
			document.getElementById('cmbCSR').add(oOption);
			document.getElementById('cmbPRODUCER').length=0;
			document.getElementById('cmbPRODUCER').add(oOptionP);
			 
			for(i=0; i<tree.childNodes.length; i++)
			{
				nodValue = tree.childNodes[i].getElementsByTagName('Table');
				flag=1;
				if (nodValue != null)
				{
								
					if (nodValue[0].firstChild == null)
						continue;					
							
					userID = tree.childNodes[i].getElementsByTagName('USER_ID');
					userDesc = tree.childNodes[i].getElementsByTagName('USER_NAME_ID');
					userIsAcrive = tree.childNodes[i].getElementsByTagName('IS_ACTIVE');
					
					if (userID != null && userDesc != null)
					{
						if (userID[0] != null &&  userDesc[0] != null )
						{
							oOption = document.createElement("option");
							oOption.value = userID[0].firstChild.text;
							oOption.text = userDesc[0].firstChild.text;
							if(userIsAcrive[0].firstChild.text=='N'){
								oOption.style.color='#ff0000'; 
								oOption.removeAttribute;}
							else{	
							document.getElementById('cmbCSR').add(oOption);
							}									
							oOptionP = document.createElement("option");
							oOptionP.value = userID[0].firstChild.text;
							oOptionP.text = userDesc[0].firstChild.text;
							if(userIsAcrive[0].firstChild.text=='N')
								oOptionP.style.color='#ff0000'; 
							document.getElementById('cmbPRODUCER').add(oOptionP);
						}
					}					
				}//End of 'If'
			}//End of for loop

            //Added by Ruchika Chauhan on 18-Jan-2012 for TFS # 1211      
            if(flag==1)
            {
                EnableValidator('rfvPRODUCER',true);
                document.getElementById("spnPRODUCER").style.display = 'inline'; 
            }
            else
            {
                EnableValidator('rfvPRODUCER',false);
                document.getElementById("spnPRODUCER").style.display = 'none'; 
            }

	}
	// added by sonal to convert changes from cs code into ajax
	
	 function setValues() {
            document.getElementById('hidLOBId').value = document.getElementById('cmbAPP_LOB').value;
           var varSysID = "<%=GetSystemId()%>";

            if (document.getElementById('cmbAPP_LOB').value == "" || document.getElementById('cmbAPP_LOB').value == "0") {
                  document.getElementById('cmbPOLICY_SUBLOB').innerHTML = "";
                //document.getElementById('cmbINSTALL_PLAN_ID').innerHTML = "";
                //document.getElementById('cmbAPP_TERMS').innerHTML = "";//Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                document.getElementById('cmbBILL_TYPE').innerHTML = "";                          
            }
            else {
                GetValues(document.getElementById('cmbAPP_LOB').value);
                GetCSRProducer(document.getElementById('cmbAGENCY_ID').value,document.getElementById('cmbAPP_LOB').value)
            }

            //document.getElementById('txtAPP_EFFECTIVE_DATE').value = "";
            //document.getElementById('txtAPP_EXPIRATION_DATE').value = "";    
            if(document.getElementById('cmbAPP_LOB').value!='')
                setTransactiontype();

            if(document.getElementById('cmbAPP_LOB').value == "13")
            {
                //document.getElementById("tdterm").style.visibility  = 'hidden';
                document.getElementById("capAPP_TERMS").style.visibility  = 'hidden';
                document.getElementById("spnAPP_TERMS").style.visibility  = 'hidden';
                document.getElementById("txtAPP_TERMS").style.visibility  = 'hidden';
                document.getElementById("capAPP_INCEPTION_DATE").style.visibility  = 'hidden';
                document.getElementById("txtAPP_INCEPTION_DATE").style.visibility  = 'hidden';
                document.getElementById("imgAPP_INCEPTION_DATE").style.visibility  = 'hidden';
                document.getElementById("spnInception").style.visibility  = 'hidden';
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
                document.getElementById("imgAPP_INCEPTION_DATE").style.visibility  = 'visible';
                document.getElementById("spnInception").style.visibility  = 'visible';
                document.getElementById("capAPP_EFFECTIVE_DATE").innerText = "Effective Date";
                document.getElementById("capAPP_INCEPTION_DATE").innerText = "Inception Date";
                document.getElementById("capAPP_EXPIRATION_DATE").innerText = "Expiry Date";

            }

            if(document.getElementById("hidCustomerType").value != "11109" && document.getElementById("hidLOBID").value == "13")
            {
                document.getElementById("lblMessage").style.display = "inline";
                document.getElementById("lblMessage").innerHTML = "Sorry !! this product is implimented only for Commerical type Customer.Please change Customer Type";
                    if (document.getElementById('btnSave') != null)
                        document.getElementById('btnSave').disabled = true;
                    if (document.getElementById('btnReset') != null)
                        document.getElementById('btnReset').disabled = true;
                    if (document.getElementById('btnCreateNewVersion') != null)
                        document.getElementById('btnCreateNewVersion').disabled = true;
                    if (document.getElementById('btnActivateDeactivate') != null)
                        document.getElementById('btnActivateDeactivate').disabled = true;
                    if (document.getElementById('btnCopy') != null)
                        document.getElementById('btnCopy').disabled = true;
                    if (document.getElementById('btnReject') != null)
                        document.getElementById('btnReject').disabled = true;
                    if (document.getElementById('btnSubmitAnyway') != null)
                        document.getElementById('btnSubmitAnyway').disabled = true;
                    if (document.getElementById('btnConvertAppToPolicy') != null)
                        document.getElementById('btnConvertAppToPolicy').disabled = true;
                    if (document.getElementById('btnVerifyApplication') != null)
                        document.getElementById('btnVerifyApplication').disabled = true;
                    if (document.getElementById('btnBack') != null)
                        document.getElementById('btnBack').disabled = true;
                    if (document.getElementById('btnDelete') != null)
                        document.getElementById('btnDelete').disabled = true;
                        if (document.getElementById('btnCustomerAssistant') != null)
                        document.getElementById('btnCustomerAssistant').disabled = true;

            }
            else
            {
                if(document.getElementById("hidSUB_LOB").value == "0")
                {
                document.getElementById("lblMessage").style.display = "none";
                document.getElementById("lblMessage").innerHTML = "";
                }
               if (document.getElementById('btnSave') != null)
                        document.getElementById('btnSave').disabled = false;
                    if (document.getElementById('btnReset') != null)
                        document.getElementById('btnReset').disabled = false;
                    if (document.getElementById('btnCreateNewVersion') != null)
                        document.getElementById('btnCreateNewVersion').disabled = false;
                    if (document.getElementById('btnActivateDeactivate') != null)
                        document.getElementById('btnActivateDeactivate').disabled = false;
                    if (document.getElementById('btnCopy') != null)
                        document.getElementById('btnCopy').disabled = false;
                    if (document.getElementById('btnReject') != null)
                        document.getElementById('btnReject').disabled = false;
                    if (document.getElementById('btnSubmitAnyway') != null)
                        document.getElementById('btnSubmitAnyway').disabled = false;
                    if (document.getElementById('btnConvertAppToPolicy') != null)
                        document.getElementById('btnConvertAppToPolicy').disabled = false;
                    if (document.getElementById('btnVerifyApplication') != null)
                        document.getElementById('btnVerifyApplication').disabled = false;
                    if (document.getElementById('btnBack') != null)
                        document.getElementById('btnBack').disabled = false;
                    if (document.getElementById('btnDelete') != null)
                        document.getElementById('btnDelete').disabled = false;
                        if (document.getElementById('btnCustomerAssistant') != null)
                        document.getElementById('btnCustomerAssistant').disabled = false;
            }



         }
        //Added By Lalit For master policy implimentation        
        function setTransactiontype()
        {
           document.getElementById('cmbTRANSACTION_TYPE').innerHTML='';
           var result = PolicyGeneralInfo.AjaxTranType();
           fillDTCombo(result.value, document.getElementById('cmbTRANSACTION_TYPE'), 'LookupID', 'LookupDesc', 0);
           var LobId = document.getElementById('cmbAPP_LOB').value ;
           if(document.getElementById("cmbDOWN_PAY_MODE").disabled==false)
           document.getElementById("cmbDOWN_PAY_MODE").disabled = false;
           var resultt = PolicyGeneralInfo.GetProductType(LobId);
           var ProductType=resultt.value;  
            //if Product Type is not master policy then 'open policy' 
           //transaction type should not available
            if( !(ProductType == '14680'))                                            
            {
            
           //if(!(LobId == 17 || LobId == 18 || LobId== 21 || LobId== 34))
            //{
               var MasterPolicyTranType = '<%= MASTER_POLICY %>';
               var length = document.getElementById('cmbTRANSACTION_TYPE').length ;
               for (i=0 ; i<length-1; i++)
               {
                   if(document.getElementById('cmbTRANSACTION_TYPE').options[i].value == MasterPolicyTranType) 
                   {
                    document.getElementById('cmbTRANSACTION_TYPE').remove(i);
                   }
               }
              }
        }
        


        //Added by Ruchika Chauhan on 7-Dec-2011 for TFS # 1211
        function CalculateTerm()
        {
      
            var incepDate = document.getElementById('txtAPP_EFFECTIVE_DATE').value;
            var expireDate = document.getElementById('txtAPP_EXPIRATION_DATE').value;           
            
                if (incepDate != "" && expireDate != "") 
                {                                                  
                    var result = PolicyGeneralInfo.GetTerm(incepDate, expireDate); 
                 
                    if ((result != null) && (result.value > 0))
                    {
                        document.getElementById('txtAPP_TERMS').value = parseInt(result.value)+1;        
                        document.getElementById('spnAPP_EXPIRATION_DATE').style.display='none';   
                        document.getElementById('rfvAPP_TERMS').style.display='none'; 
                        SetAPP_TERMS();
                    }
                    else if(result.value == 0) //Added by Ruchika on 18-Jan-2012
                    {
                        document.getElementById('txtAPP_TERMS').value = ""; 
                        document.getElementById('txtAPP_EXPIRATION_DATE').value = '';
                    } 
                    else
                    {
                        document.getElementById('txtAPP_TERMS').value = "";
                        document.getElementById('spnAPP_EXPIRATION_DATE').innerHTML="Expiry date should be greater than the Inception date.";//Added by Ruchika Chauhan on 22-Dec-2011 for TFS # 1211
                        document.getElementById('spnAPP_EXPIRATION_DATE').style.color='red';
                        document.getElementById('spnAPP_EXPIRATION_DATE').style.display='inline';  
                    }                                 
                }
                else 
                {           
                    document.getElementById('txtAPP_TERMS').value = "";                    
                }
        }


       function GetValues(iLOB_ID) {

        var varSysID = "<%=GetSystemId()%>";
            if (iLOB_ID != "" && iLOB_ID != "0" ) { 
                var result = PolicyGeneralInfo.AjaxFetchInfo(iLOB_ID);

                fillDTCombo(result.value, document.getElementById('cmbPOLICY_SUBLOB'), 'SUB_LOB_ID', 'SUB_LOB_DESC', 0);
                fillDTCombo(result.value, document.getElementById('cmbBILL_TYPE'), 'LOOKUP_UNIQUE_ID', 'LOOKUP_VALUE_DESC', 1);
                //Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                //fillDTCombo(result.value, document.getElementById('cmbAPP_TERMS'), 'LOOKUP_VALUE_CODE', 'LOOKUP_VALUE_DESC', 2);
               
                if (iLOB_ID<=8 && iLOB_ID != 1)
                {
              
                fillDTCombo(result.value, document.getElementById('cmbState_ID'), 'STATE_ID', 'STATE_NAME', 3);
               
                document.getElementById('tblstate').style.display= 'block';
                EnableValidator('rfvState_ID',true);
                
                }
                else
                {
                document.getElementById('tblstate').style.display ='none' 
                EnableValidator('rfvState_ID',false);
             
                }
                //Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                    //                 appoption = document.getElementById('cmbAPP_TERMS');
                    //                
                    //                if (iLOB_ID=="3")
                    //                {
                    //                
                    //                appoption.selectedIndex=2;
                    //                }
                    //                else
                    //                {
                    //                
                    //                appoption.selectedIndex=1;
                    //                }
                    //if (document.getElementById('cmbAPP_TERMS').selectedIndex !=0)
                    //{
                    //document.getElementById("rfvAPP_TERMS").style.display='none';
                    //}
                //Commented till here
                if (document.getElementById('txtAPP_TERMS').value !="")
                {
                 document.getElementById("rfvAPP_TERMS").style.display='none';
                }
                if (document.getElementById('cmbBILL_TYPE').selectedIndex !=0)
                {
                document.getElementById("rfvBILL_TYPE").style.display='none';
                }
               
                if (iLOB_ID == "1" )
                {
                  EnableValidator('rfvPOLICY_TYPE',false); 
                 
                 document.getElementById('div_poltype').style.display= 'none';
                
                PolicyType("HOPTYP")
                
                }
                else if (iLOB_ID == "6" )
                {
                  EnableValidator('rfvPOLICY_TYPE',true);
                   
                   document.getElementById('div_poltype').style.display= 'block';
                 
                  PolicyType("RTPTYI")
                }
                
                else
                {
                  EnableValidator('rfvPOLICY_TYPE',false);
                  
                   document.getElementById('div_poltype').style.display= 'none';
                  
                }
                
                //Added by RC                 
                if(varSysID.toUpperCase() != 'S001')
                {
		            setExpDate();	
                }
               
              
              

                //document.getElementById('cmbINSTALL_PLAN_ID').innerHTML = "";               
            }
        }
        
        function fillDTCombo(objDT, combo,valID, txtDesc, tabIndex) {            
            combo.innerHTML = "";
            if (objDT != null) {

                for (i = 0; i < objDT.Tables[tabIndex].Rows.length; i++) {

                    if (i == 0) {
                        oOption = document.createElement("option");
                        oOption.value = "";
                        oOption.text = "";
                        combo.add(oOption);
                    }
                    oOption = document.createElement("option");
                    oOption.value = objDT.Tables[tabIndex].Rows[i][valID];
                    oOption.text = objDT.Tables[tabIndex].Rows[i][txtDesc];
                    combo.add(oOption);
                }
            }
        }
         function SetAPP_TERMS() {
                 
            if (document.getElementById('txtAPP_TERMS').value != "") {
                document.getElementById('hidAPP_TERMS').value = document.getElementById('txtAPP_TERMS').value;
            }

          
            //Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
                //            if (document.getElementById('cmbAPP_TERMS').value != "") {
                //                document.getElementById('hidAPP_TERMS').value = document.getElementById('cmbAPP_TERMS').value;
                //            }
            //Commented till here  
        }
        
        
        
        function fillBillPlan() {
  
            if (document.getElementById('txtAPP_TERMS').value != "") {
            var lobid=document.getElementById('cmbAPP_LOB').value ;
                 //Commented By Lalit  
               
                var result = PolicyGeneralInfo.GetInstallPlan(document.getElementById('txtAPP_TERMS').value,lobid,document.getElementById('cmbTRANSACTION_TYPE').value);
                fillDTCombo(result.value, document.getElementById('cmbINSTALL_PLAN_ID'), 'INSTALL_PLAN_ID', 'BILLING_PLAN', 0);
            }
            else {
                document.getElementById('cmbINSTALL_PLAN_ID').innerHTML = "";
            }
             //            if (document.getElementById('cmbAPP_TERMS').value != "") {
//                var result = PolicyGeneralInfo.GetInstallPlan(document.getElementById('cmbAPP_TERMS').value);
//                fillDTCombo(result.value, document.getElementById('cmbINSTALL_PLAN_ID'), 'INSTALL_PLAN_ID', 'BILLING_PLAN', 0);
//            }
//            else {
//                document.getElementById('cmbINSTALL_PLAN_ID').innerHTML = "";
//            }
        }
        

        

         function setExpDate() {
         if (document.getElementById('txtAPP_TERMS').value != "" && document.getElementById('txtAPP_EFFECTIVE_DATE').value != "") {
                var result = PolicyGeneralInfo.GetExpDate(document.getElementById('txtAPP_TERMS').value, document.getElementById('txtAPP_EFFECTIVE_DATE').value);
              
                if (result != null) {
                    document.getElementById('txtAPP_EXPIRATION_DATE').value = result.value;
                   //Added by Pradeep Kushwaha on 09-03-2011 -iTrack 920 (BILLING TYPE SHOULD COME AS DEFAULT “INSURED”)
                    $("#cmbBILL_TYPE option[value='8460']").attr("selected", "selected");
                    document.getElementById('cmbDOWN_PAY_MODE').innerHTML = '';
                    $("#cmbBILL_TYPE").change(); 
                   //Added till here 
                }
                else {
                    document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
                }
            }
            else {
           
                document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
            }
//         
//            if (document.getElementById('cmbAPP_TERMS').value != "" && document.getElementById('txtAPP_EFFECTIVE_DATE').value != "") {
//                var result = PolicyGeneralInfo.GetExpDate(document.getElementById('cmbAPP_TERMS').value, document.getElementById('txtAPP_EFFECTIVE_DATE').value);
//              
//                if (result != null) {
//                    document.getElementById('txtAPP_EXPIRATION_DATE').value = result.value;
//                  
//                }
//                else {
//                    document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
//                }
//            }
//            else {
//           
//                document.getElementById('txtAPP_EXPIRATION_DATE').value = "";
//            }
            SetAPP_TERMS();
           
        }
        
         function setDefaultPlan() {
         
           // alert(document.getElementById('cmbAPP_TERMS').value);
           fillBillPlan();
           //Commented by Pradeep Kushwaha(20-oct-2010) for implementation of APP_TERMS on Textbox
            //            if (document.getElementById('cmbAPP_TERMS').value != "") {
            //                var result = PolicyGeneralInfo.GetDefaultInstallmentPlanAjax(document.getElementById('cmbAPP_TERMS').value,document.getElementById('cmbAPP_LOB').value);
            //          
            //                if (result != null) {
            //                    SelectComboOption("cmbINSTALL_PLAN_ID", result.value);
            //                    SetINSTALL_PLAN_ID();
            //                    document.getElementById('cmbBILL_TYPE').selectedIndex = 2;
            //                    BillType();
            //                    
            //                }
            //                else
            //                {
            //                SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidFULL_PAY_PLAN_ID").value);
            //                }
            //            }
            //Commented  till here   
            if (document.getElementById('txtAPP_TERMS').value != "") {
                var result = PolicyGeneralInfo.GetDefaultInstallmentPlanAjax(document.getElementById('txtAPP_TERMS').value,document.getElementById('cmbAPP_LOB').value);
          
                if (result != null) {
                    SelectComboOption("cmbINSTALL_PLAN_ID", result.value);
                    SetINSTALL_PLAN_ID();
                    document.getElementById('cmbBILL_TYPE').selectedIndex = 2;
                    BillType();
                    
                }
                else
                {
                SelectComboOption('cmbINSTALL_PLAN_ID',document.getElementById("hidFULL_PAY_PLAN_ID").value);
                }
            }
        }
         function SetINSTALL_PLAN_ID() {
            if (document.getElementById('cmbINSTALL_PLAN_ID').value != "") {
                document.getElementById('hidINSTALL_PLAN_ID').value = document.getElementById('cmbINSTALL_PLAN_ID').value;
            }
        }
        
        function setHidState()
        {
         if (document.getElementById('cmbState_ID').value != "") {
         
         var iLOB_ID = document.getElementById('hidLOBId').value
                document.getElementById('hidSTATE_ID').value = document.getElementById('cmbState_ID').value;
                
                if (iLOB_ID=="1" )
                {
                document.getElementById('div_poltype').style.display= 'block';
               
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
                document.getElementById('div_poltype').style.display= 'block';
                
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
           
            PolicyGeneralInfo.AjaxFillPolicyType(SelectType,FillPolicyType);
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

        
     function setHidPolicyType()
     {
      if (document.getElementById('cmbPOLICY_TYPE').value != "") {
                document.getElementById('hidPOLICY_TYPE').value = document.getElementById('cmbPOLICY_TYPE').value;
            }
      
     }
     
     function setHidPolicyCurrency()
     {
      if (document.getElementById('cmbPOLICY_CURRENCY').value != "") {
                document.getElementById('hidPOLICY_CURRENCY').value = document.getElementById('cmbPOLICY_CURRENCY').value;
            }
     }
     //Added By Lalit chauhan,Nov 09
     function GetMaster_PolicyBillingPlans()
     {
       //if(document.getElementById('cmbTRANSACTION_TYPE').value == '14560'){            
            var LobId= document.getElementById('cmbAPP_LOB').value
            var result = PolicyGeneralInfo.GetInstallPlan('',LobId,document.getElementById('cmbTRANSACTION_TYPE').value);
            fillDTCombo(result.value, document.getElementById('cmbINSTALL_PLAN_ID'), 'INSTALL_PLAN_ID', 'BILLING_PLAN', 0);
            CreateXML_DS(result.value);
            document.getElementById('cmbINSTALL_PLAN_ID').setAttribute('disabled',false);
            document.getElementById('hidPOL_TRAN_TYPE').value= document.getElementById('cmbTRANSACTION_TYPE').value;
            DisablePolicyLevelCommission();	
            //Added by Pradeep Kushwaha on 09-03-2011 -iTrack 920 (BILLING TYPE SHOULD COME AS DEFAULT “INSURED”)
            //Modified by Abhishek Goel on dated 23/12/2011 for TFS 1211
            var singleValues = $("#cmbBILL_TYPE").val(); 
            if(singleValues == '114329')
            $("#cmbBILL_TYPE option[value='114329']").attr("selected", "selected");
            //Added till here 
       //}
     }
     //Added By Lalit chauhan,Nov 09
     function  CreateXML_DS(ds)
     {
        var alertmsg= document.getElementById('hidPOL_BILL_PLANMSG').value
       try
        {
           var xmlstring= '';
           
            var startRoot ='<NewDataSet>';
            var EndRoot ='</NewDataSet>';            
            $.each(ds.Tables, function(index, table) {    
                xmlstring = xmlstring + startRoot;      
            $.each(table.Rows, function(index, row) { 
            
               var xml = '';
                xml += '<Table>';
                xml += '<INSTALL_PLAN_ID>'+row.INSTALL_PLAN_ID+'</INSTALL_PLAN_ID>';
                xml += '<BILLING_PLAN>'+row.BILLING_PLAN+'</BILLING_PLAN>';
                xml += '<APPLABLE_POLTERM>'+row.APPLABLE_POLTERM+'</APPLABLE_POLTERM>';
                xml += '<MODE_OF_DOWNPAY>'+row.MODE_OF_DOWNPAY+'</MODE_OF_DOWNPAY>';
                xml += '<MODE_OF_DOWNPAY1>'+row.MODE_OF_DOWNPAY1+'</MODE_OF_DOWNPAY1>';
                xml += '<MODE_OF_DOWNPAY2>'+row.MODE_OF_DOWNPAY2+'</MODE_OF_DOWNPAY2>';
                xml += '<LOOKUP_VALUE_DESC>'+row.LOOKUP_VALUE_DESC+'</LOOKUP_VALUE_DESC>';
                xml += '<LOOKUP_VALUE_DESC1>'+row.LOOKUP_VALUE_DESC1+'</LOOKUP_VALUE_DESC1>';
                xml += '<LOOKUP_VALUE_DESC2>'+row.LOOKUP_VALUE_DESC2+'</LOOKUP_VALUE_DESC2>';
                xml += '<DEFAULT_PLAN>'+row.DEFAULT_PLAN+'</DEFAULT_PLAN>';
                xml += '<IS_ACTIVE>'+row.IS_ACTIVE+'</IS_ACTIVE>';
                xml = xml + '</Table>';
                xmlstring +=xml ;      
                }); 
               xmlstring += EndRoot;  
            });
            document.getElementById('hidBillingPlan').value = xmlstring;
        }
        catch(err)
        {
           alert(alertmsg);
        }

     }
     function DateConvert(Date, dateFormate) {
      
        if (Date == "" || Date.length < 8) return "";
        var returnDate = '';
        var saperator = '/';
        var firstDate, secDate;

        var strDateFirst = Date.split("/");
        //var strDateSec = DateSec.split("/");

        if (dateFormate.toLowerCase() == "dd/mm/yyyy") {
            //alert("dd/mm/yyyy")
            returnDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0]) + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);

        }
        if (dateFormate.toLowerCase() == "mm/dd/yyyy") {
            //alert("mm/dd/yyyy")
            returnDate = Date
            //secDate = DateSec;
        }

        return returnDate;
    }
     function ReadOnly()
     {//itrack #712
        if(event.keyCode==8)
            return false;
     }
     	
        // Added by aditya, for itrack - 1284
       
      $(document).ready(function() {
        $("#cmbAGENCY_ID").change(function() {        
        $("#hidAGENCY_ID").val($("#cmbAGENCY_ID option:selected").val());
            });
        });
       //$(document).ready(function() { $("input#txtAPP_EFFECTIVE_DATE").bind("change", FillAgency); }); 
                	
     	function FillAgency() {
     	
     	    var App_Effective_Date = document.getElementById("txtAPP_EFFECTIVE_DATE").value;
     	    var App_Effective_Date_new = DateConvert(App_Effective_Date, jsaAppDtFormat);

		    document.getElementById("hidAPP_EFFECTIVE_DATE").value = App_Effective_Date;
		    var result = PolicyGeneralInfo.AjaxFillAgency(App_Effective_Date_new, FillBroker);
	    }

	    function FillBroker(Result) { 
	            if (Result.error) {
	            var xfaultcode = Result.errorDetail.code;
	            var xfaultstring = Result.errorDetail.string;
	            var xfaultsoap = Result.errorDetail.raw;	            
	             }
	        else {
	            var AgencyList = document.getElementById("cmbAGENCY_ID");
	            AgencyList.options.length = 0;
	            oOption = document.createElement("option");
	            oOption.value = "";
	            oOption.text = "";
	            AgencyList.add(oOption);           
	           
	            ds = Result.value;
	            if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
	                for (var i = 0; i < ds.Tables[0].Rows.length; ++i) {	                   
	                    AgencyList.options[AgencyList.options.length] = new Option(ds.Tables[0].Rows[i]["AGENCY_NAME_ACTIVE_STATUS"], ds.Tables[0].Rows[i]["AGENCY_ID"]);
	                }

	            }
	            if (AgencyList.options.length > 0) {
	                document.getElementById('hidAGENCY_ID').value = AgencyList.options[0].value;
	            }
	            document.getElementById("cmbAGENCY_ID").value = document.getElementById("cmbAGENCY_ID").value;
	        }

	        return false;
	    }
			
    </script>
</head>
<body style="margin-left: 0; margin-top: 0" onload="Init();ApplyColor();RefreshClient();"
    onkeydown="ProcessKeypress();" oncontextmenu="return true;">
    <form id="APP_LIST" name="APP_LIST" method="post" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr id="trFORMMESSAGE" runat="server">
            <td class="midcolorc" align="right" colspan="4">
                <asp:Label ID="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg"></asp:Label>
            </td>
        </tr>
        <tr id="trDETAILS" runat="server">
            <td>
                <table id="Table2" width="100%" align="center" border="0">
                    <tbody>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="headereffectCenter" colspan="4">
                                <asp:Label ID="lblHeader" runat="server">Application Information</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="pageHeader" colspan="4">
                                <webcontrol:workflow id="myWorkFlow" runat="server">
                                </webcontrol:workflow>
                                <br />
                                <asp:Label ID="lblManHeader" runat="server">Please note that all fields marked with * are mandatory</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolorc" align="right" colspan="4">
                                <asp:Label ID="lblMessage" runat="server" CssClass="errmsg" ></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="midcolora" width="18%">
                                <asp:Label ID="capCUSTOMER_ID" runat="server">ClientID</asp:Label>
                            </td>
                            <td class="midcolora" width="32%">
                                <asp:TextBox ID="txtCUSTOMER_NAME" runat="server" ReadOnly="true"></asp:TextBox>
                                <img id="imgSelect" style="cursor: hand" src="../../cmsweb/images/selecticon.gif"
                                    alt="" runat="server" />
                                <asp:TextBox ID="txtCUSTOMERNAME" runat="server" CssClass="midcolora" BorderStyle="None"
                                    MaxLength="10" size="30"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvCUSTOMER_ID" runat="server" Enabled="False" Display="Dynamic"
                                    ControlToValidate="txtCUSTOMER_NAME"></asp:RequiredFieldValidator>
                            </td>
                            <td class="midcolora" width="16%">
                                <asp:Label ID="capAPP_STATUS" runat="server">Status</asp:Label>
                            </td>
                            <td class="midcolora" width="34%">
                                <asp:TextBox ID="txtPOLICY_STATUS" runat="server" CssClass="midcolora" ReadOnly="true"
                                    BorderStyle="None" MaxLength="10" size="30"></asp:TextBox>
                            </td>
                        </tr>
                        <%--<tr style="display:none">
									<td class="midcolora" width="18%">
									    <asp:label id="capSTATE_ID" runat="server">State</asp:label><span class="mandatory">*</span>
									</td>
									<td class="midcolora" width="32%" colspan="3">									
									    <asp:dropdownlist id="cmbSTATE_ID" runat="server" onfocus="SelectComboIndex('cmbSTATE_ID')" AutoPostBack="true"></asp:dropdownlist>
									    <br />
									    <asp:requiredfieldvalidator id="rfvSTATE_ID" runat="server" Enabled="false" Display="Dynamic" ControlToValidate="cmbSTATE_ID"></asp:requiredfieldvalidator>
									</td>
								</tr>--%>
                        <tr>
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="lblProduct" runat="server">Product</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <table style="width: 100%" border="0">
                                                <tr>
                                                    <td class="midcolora">
                                                        <asp:Label ID="capAPP_LOB" runat="server">Product</asp:Label><span class="mandatory">*</span>
                                                        <br />
                                                        <asp:DropDownList ID="cmbAPP_LOB" onfocus="SelectComboIndex('cmbAPP_LOB')" runat="server"
                                                            onchange="setValues();setDefaultPlan();">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvAPP_LOB" runat="server" Display="Dynamic" ControlToValidate="cmbAPP_LOB"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="left" style="width: 40%">
                                                        <table id="tblstate" style="display: block" runat="server">
                                                            <tr>
                                                                <td class="midcolora">
                                                                    <asp:Label ID="capState" runat="server">State</asp:Label><span class="mandatory">*</span><br />
                                                                    <asp:DropDownList ID="cmbState_ID" onfocus="SelectComboIndex('cmbState_ID')" runat="server"
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
                                                <tr id="trTransactionType" runat="server" visible="true">
                                                    <td colspan="2" class="midcolora">
                                                        <asp:Label ID="capTRANSACTION_TYPE" runat="server">Transaction Type</asp:Label><span
                                                            class="mandatory">*</span>
                                                        <br />
                                                        <asp:DropDownList ID="cmbTRANSACTION_TYPE" onfocus="SelectComboIndex('cmbTRANSACTION_TYPE')"
                                                            runat="server" onchange="GetMaster_PolicyBillingPlans();">
                                                        </asp:DropDownList>
                                                        <br />
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
                                                        <asp:Label ID="capPOLICY_SUBLOB" runat="server">Line of Business</asp:Label><span
                                                            id="spnPOLICY_SUBLOB" runat="server" class="mandatory">*</span>
                                                        <br />
                                                        <asp:DropDownList ID="cmbPOLICY_SUBLOB" onfocus="SelectComboIndex('cmbPOLICY_SUBLOB')"
                                                            runat="server" onchange="setHidSubLob();">
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
                                                <tr id="trModality" runat="server" visible="true">
                                                    <td colspan="2" class="midcolora">
                                                        <asp:Label ID="capMODALITY" runat="server">Modality</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="cmbMODALITY" onfocus="SelectComboIndex('cmbMODALITY')" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capPOLICY_CURRENCY" runat="server">Policy Currency</asp:Label><span
                                                class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbPOLICY_CURRENCY" runat="server" onfocus="SelectComboIndex('cmbPOLICY_CURRENCY')"
                                                onchange="setHidPolicyCurrency();">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvPOLICY_CURRENCY" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbPOLICY_CURRENCY"></asp:RequiredFieldValidator>
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
                                <asp:Label ID="lblAppHeader" runat="server">Application</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capAPP_NUMBER" runat="server">Application Number</asp:Label><span
                                                class="mandatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtAPP_NUMBER" runat="server" MaxLength="25" size="50" onfocus="showHideAppDefMsg(true);"
                                                onclick="showHideAppDefMsg(true);" onblur="showHideAppDefMsg(false);"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="revAPP_NUMBER" runat="server" Display="Dynamic"
                                                ControlToValidate="txtAPP_NUMBER" ErrorMessage="Allow aonly alphanumeric"></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="midcolora" colspan="1" id="tdAPP_VERSION">
                                            <asp:Label ID="capAPP_VERSION" runat="server">Version</asp:Label><span class="mandatory">*</span>
                                            <br />
                                            <asp:TextBox ID="txtPOLICY_DISP_VERSION" runat="server" CssClass="midcolora" ReadOnly="true"
                                                BorderStyle="None" MaxLength="10" size="30"></asp:TextBox>
                                        </td>
                                        <td class="midcolora" colspan="1" id="tdOLD_POLICY_NUMBER">
                                            <asp:Label ID="capOLD_POLICY_NUMBER" runat="server">Old Policy Number</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtOLD_POLICY_NUMBER" runat="server" CssClass="midcolora" ReadOnly="true"
                                                BorderStyle="None" MaxLength="10" size="30" onfocus="blur()"></asp:TextBox>
                                            <input type="hidden" runat="server" id="hidOLD_POLICY_NUMBER" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                                <table id="tblTerm" width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td class="headerEffectSystemParams" colspan="4">
                                            <asp:Label ID="lblTermHeader" runat="server">Term</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td class="midcolora" style="width: 40%" id="tdterm">
                                                        <asp:Label ID="capAPP_TERMS" runat="server">Policy Term</asp:Label><span class="mandatory"
                                                            id="spnAPP_TERMS" runat="server">*</span>
                                                        <br />
                                                        <asp:TextBox ID="txtAPP_TERMS" MaxLength="4" runat="server" Width="73px"></asp:TextBox>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvAPP_TERMS" runat="server" Display="Dynamic" ControlToValidate="txtAPP_TERMS"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revAPP_TERMS" runat="server" Display="Dynamic"
                                                            ControlToValidate="txtAPP_TERMS"></asp:RegularExpressionValidator>
                                                        <%-- <br />
                                            <asp:DropDownList ID="cmbAPP_TERMS" onfocus="SelectComboIndex('cmbAPP_TERMS')" runat="server" onchange="javascript:SetAPP_TERMS();fillBillPlan();setDefaultPlan();setExpDate();">
                                            </asp:DropDownList>--%>
                                                    </td>
                                                    <td class="midcolora" style="width: 30%">
                                                        <asp:Label ID="capAPP_EFFECTIVE_DATE" runat="server">Effective Date</asp:Label><span
                                                            class="mandatory">*</span>
                                                        <br />
                                                        <asp:TextBox ID="txtAPP_EFFECTIVE_DATE" runat="server" MaxLength="10" size="12" Display="Dynamic"
                                                            onChange="FillAgency();"></asp:TextBox>
                                                        <asp:HyperLink ID="hlkCalandarDate" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                            <asp:Image ID="imgAPP_EFFECTIVE_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                                valign="middle"></asp:Image>
                                                        </asp:HyperLink>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvAPP_EFFECTIVE_DATE" runat="server" Display="Dynamic"
                                                            ControlToValidate="txtAPP_EFFECTIVE_DATE"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revAPP_EFFECTIVE_DATE" runat="server" Display="Dynamic"
                                                            ControlToValidate="txtAPP_EFFECTIVE_DATE"></asp:RegularExpressionValidator>
                                                        <asp:CustomValidator ID="csvAPP_EFFECTIVE_DATE" Display="Dynamic" ControlToValidate="txtAPP_EFFECTIVE_DATE"
                                                            runat="server" ClientValidationFunction="CheckDate"></asp:CustomValidator>
                                                    </td>
                                                    <td class="midcolora" style="width: 30%">
                                                        <asp:Label ID="capAPP_INCEPTION_DATE" runat="server">Inception Date</asp:Label><span
                                                            id="spnInception" class="mandatory">*</span>
                                                        <br />
                                                        <asp:TextBox ID="txtAPP_INCEPTION_DATE" runat="server" MaxLength="10" size="12"></asp:TextBox>
                                                        <asp:HyperLink ID="hlkInceptionDate" runat="server" CssClass="HotSpot">
                                                            <asp:Image ID="imgAPP_INCEPTION_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                                valign="middle"></asp:Image>
                                                        </asp:HyperLink>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvAPP_INCEPTION_DATE" runat="server" Display="Dynamic"
                                                            ControlToValidate="txtAPP_INCEPTION_DATE"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revAPP_INCEPTION_DATE" runat="server" Display="Dynamic"
                                                            ControlToValidate="txtAPP_INCEPTION_DATE"></asp:RegularExpressionValidator>
                                                        <asp:RangeValidator ID="rngAPP_INCEPTION_DATE" runat="server" ControlToValidate="txtAPP_INCEPTION_DATE"
                                                            MinimumValue="1/1/1900" MaximumValue='<%# DateTime.Now.ToShortDateString() %>'
                                                            ErrorMessage="" Display="Dynamic" Type="Date"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="midcolora" style="width: 40%">
                                                        &nbsp;
                                                    </td>
                                                    <td class="midcolora" style="width: 30%">
                                                        <asp:Label ID="capAPP_EXPIRATION_DATE" runat="server">Expiration Date</asp:Label><span
                                                            class="mandatory">*</span>
                                                        <br />
                                                        <asp:TextBox ID="txtAPP_EXPIRATION_DATE" runat="server" ReadOnly="true" MaxLength="10"
                                                            size="12" Display="Dynamic" onblur="javascript:CalculateTerm()"></asp:TextBox>
                                                        <asp:HyperLink ID="hlkAPP_EXPIRATION_DATE" runat="server" CssClass="HotSpot" Display="Dynamic">
                                                            <asp:Image ID="imgAPP_EXPIRATION_DATE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif"
                                                                valign="middle"></asp:Image>
                                                        </asp:HyperLink>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfvAPP_EXPIRATION_DATE" Enabled="false" runat="server"
                                                            Display="Dynamic" ControlToValidate="txtAPP_EXPIRATION_DATE"></asp:RequiredFieldValidator>
                                                        <span id="spnAPP_EXPIRATION_DATE"></span>
                                                    </td>
                                                    <td class="midcolora" style="width: 30%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
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
                                            <asp:Label ID="capBILL_TYPE_ID" runat="server">Bill Type</asp:Label><span class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbBILL_TYPE" onfocus="SelectComboIndex('cmbBILL_TYPE')" runat="server"
                                                onchange="BillType();GetMaster_PolicyBillingPlans();">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvBILL_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbBILL_TYPE"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:Label ID="capDIV_ID_DEPT_ID_PC_ID" runat="server">Div/Dept/PC</asp:Label><span
                                                id="spnDIV_ID_DEPT_ID_PC_ID" runat="server" class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbDIV_ID_DEPT_ID_PC_ID" onfocus="SelectComboIndex('cmbDIV_ID_DEPT_ID_PC_ID')"
                                                runat="server">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvDIV_ID_DEPT_ID_PC_ID" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbDIV_ID_DEPT_ID_PC_ID"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <table>
                                                <tr>
                                                    <td class="midcolora" style="width: 30%">
                                                        <asp:Label ID="capINSTALL_PLAN_ID" runat="server">Billing Plan</asp:Label>
                                                        <asp:Label ID="lblINSTALL_PLAN_ID" runat="server">N.A.</asp:Label><span class="mandatory"
                                                            id="spnINSTALL_PLAN_ID" runat="server">*</span>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:DropDownList ID="cmbINSTALL_PLAN_ID" onfocus="SelectComboIndex('cmbINSTALL_PLAN_ID')"
                                                runat="server">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvINSTALL_PLAN_ID" Display="Dynamic" ControlToValidate="cmbINSTALL_PLAN_ID"
                                                runat="server"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="csvINSTALL_PLAN_ID" Display="Dynamic" ControlToValidate="cmbINSTALL_PLAN_ID"
                                                runat="server" ClientValidationFunction="ChkBillingPlan"></asp:CustomValidator>
                                            <!-- Added by Agniswar for Singapore -->
                                            <br />
                                            <asp:Label ID="capFUND_TYPE" runat="server">Fund Type</asp:Label>
                                            <span id="spnFUND_TYPE" runat="server" class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbFUND_TYPE" onfocus="SelectComboIndex('cmbFUND_TYPE')" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvFUND_TYPE" runat="server" Display="Dynamic" ControlToValidate="cmbFUND_TYPE"></asp:RequiredFieldValidator>
                                            <!-- Till Here -->
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capPREFERENCE_DAY" runat="server">Preferred Billing Day</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtPREFERENCE_DAY" runat="server" MaxLength="2" size="6" Style="text-align: right"></asp:TextBox>
                                            <br />
                                            <asp:RangeValidator ID="rngPREFERENCE_DAY" ControlToValidate="txtPREFERENCE_DAY"
                                                Display="Dynamic" runat="server" MinimumValue="1" MaximumValue="28" Type="Integer"></asp:RangeValidator>
                                            <!-- Added by Agniswar for Singapore -->
                                            <br />
                                            <br />
                                            <asp:Label ID="capBILLING_CURRENCY" runat="server">Billing Currency</asp:Label>
                                            <span id="spnBILLING_CURRENCY" runat="server" class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbBILLING_CURRENCY" onfocus="SelectComboIndex('cmbBILLING_CURRENCY')"
                                                runat="server">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvBILLING_CURRENCY" runat="server" Display="Dynamic"
                                                ControlToValidate="cmbBILLING_CURRENCY"></asp:RequiredFieldValidator>
                                            <!-- Till Here -->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capDOWN_PAY_MODE" runat="server">Down Payment</asp:Label><span class="mandatory"
                                                id="spnDOWN_PAY_MODE" runat="server">*</span>
                                            <br />
                                            <asp:Label ID="lblDOWN_PAY_MODE" runat="server" CssClass="LabelFont">N.A.</asp:Label>
                                            <asp:DropDownList ID="cmbDOWN_PAY_MODE" onchange="fillDownPay()" runat="server">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvDOWN_PAY_MODE" ControlToValidate="cmbDOWN_PAY_MODE"
                                                Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <%--<asp:label id="capRECEIVED_PRMIUM" runat="server">Received</asp:label>
								                <br />
								                <asp:textbox id="txtRECEIVED_PRMIUM" style="TEXT-ALIGN: right"  onblur="FormatAmount(this);" size="16" Runat="server" MaxLength="9"></asp:textbox>
										        <br />
										        <asp:regularexpressionvalidator id="revRECEIVED_PRMIUM" ControlToValidate="txtRECEIVED_PRMIUM" Display="Dynamic" Runat="server"></asp:regularexpressionvalidator>--%>
                                            <asp:Label ID="capCO_INSURANCE" runat="server">Co-Insurance</asp:Label><span class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbCO_INSURANCE" runat="server" onchange="javascript:fillPayor();">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvCO_INSURANCE" Display="Dynamic" ControlToValidate="cmbCO_INSURANCE"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capUNDERWRITER" runat="server">Underwriter</asp:Label>
                                            <br />
                                            <label id="lblUNDERWRITER_UN_ASG">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" style="width: 40%">
                                            <asp:Label ID="capPAYOR" runat="server">Payor</asp:Label><span class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbPAYOR" runat="server" onfocus="SelectComboIndex('cmbPAYOR')">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvPAYOR" Display="Dynamic" Enabled="false" ControlToValidate="cmbPAYOR"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capBILLTO" runat="server">Bill To</asp:Label><span id="spnBILLTO"
                                                class="mandatory" runat="server">*</span>
                                            <br />
                                            <asp:TextBox ID="txtBILLTO" runat="server" size="50" ReadOnly="true" onkeydown="javascript:return ReadOnly();"></asp:TextBox>
                                        </td>
                                        <td class="midcolora">
                                        </td>
                                        <%-- <td  class="midcolora" style="width: 30%" id="tdCSR" runat="server">
                                            <asp:Label ID="capCSR" runat="server">CSR Name</asp:Label><span id="spnCSR" runat="server" class="mandatory">*</span>
                                            <br />
                                            <select id="cmbCSR" onfocus="SelectComboIndex('cmbCSR')" runat="server" onchange="sethidCSR();">
                                            </select>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvCSR" Display="Dynamic" ControlToValidate="cmbCSR"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>--%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAgencyHeader" runat="server" display="dynamic">
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="lblAgencyHeader" Text="Broker" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <%-- tr id="trAgencyHeader1" added by Ruchika Chauhan on 9-Dec-2011 for tfs # 1211--%>
                        <tr id="trAgencyHeader1" runat="server" display="dynamic">
                            <td class="headerEffectSystemParams" colspan="4">
                                <asp:Label ID="lblAgencyHeader1" Text="Intermediary" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" width="100%">
                                <table width="100%">
                                    <tr>
                                        <td class="midcolora" style="width: 40%" id="tdBrokerName">
                                            <asp:Label ID="capAGENCY_ID" runat="server">Broker</asp:Label><span class="mandatory">*</span>
                                            <br />
                                            <asp:DropDownList ID="cmbAGENCY_ID" onfocus="SelectComboIndex('cmbAGENCY_ID');" runat="server"
                                                onchange="javascript:setHidAgencyID();">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblAGENCY_DISPLAY_NAME" Style="display: none" CssClass="LabelFont"
                                                runat="server"></asp:Label>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvAGENCY_ID" runat="server" ControlToValidate="cmbAGENCY_ID"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%" id="tdChkPolicyCommission">
                                            <asp:Label ID="capPOLICY_LEVEL_COMM_APPLIES" runat="server">Policy Level Commission Applies</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="chkPOLICY_LEVEL_COMM_APPLIES" runat="server" onclick="javascript:CommChange();">
                                            </asp:CheckBox>
                                            <%--<asp:label id="capCOMPLETE_APP" runat="server">Policy Level Commission Applies</asp:label>
									            <br />
									            <asp:checkbox id="chkCOMPLETE_APP" runat="server" onclick="javascript:CommChange();"></asp:checkbox>--%>
                                        </td>
                                        <td class="midcolora" style="width: 30%" id="tdtxtPolicyCommission">
                                            <asp:Label ID="capPOLICY_LEVEL_COMISSION" runat="server">Policy Level Commission</asp:Label><span
                                                id="spnPOLICY_LEVEL_COMISSION" class="mandatory" style="display: none">*</span>
                                            <br />
                                            <%--Change currency format function by Pradeep- Itrack 1411,1370--%>
                                            <asp:TextBox ID="txtPOLICY_LEVEL_COMISSION" runat="server" MaxLength="6" size="10"
                                                Style="text-align: right;" disabled="true" CausesValidation="true" CssClass="INPUTCURRENCY"
                                                onChange="this.value=fnformatCommission(this.value)" onblur="this.value=fnformatCommission(this.value)"></asp:TextBox>
                                            <%--till here    --%>
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
                                        <td class="midcolora" style="width: 30%" id="td1" runat="server">
                                            <asp:Label ID="capPRODUCER" runat="server">Servicer</asp:Label><span id="spnPRODUCER"
                                                runat="server" class="mandatory">*</span>
                                            <br />
                                            <select id="cmbPRODUCER" onfocus="SelectComboIndex('cmbPRODUCER')" runat="server"
                                                onchange="sethidProducer();">
                                            </select>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvPRODUCER" Display="Dynamic" ControlToValidate="cmbPRODUCER"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capBROKER_REQUEST_NO" runat="server">Broker Request #</asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtBROKER_REQUEST_NO" MaxLength="50" runat="server" Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td class="midcolora" style="width: 30%">
                                            <asp:Label ID="capBROKER_COMM_FIRST_INSTM" runat="server">Pay Broker Commission on First Installment</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="chkBROKER_COMM_FIRST_INSTM" Enabled="false" runat="server"></asp:CheckBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" style="width: 30%" id="tdCSR" runat="server">
                                            <asp:Label ID="capCSR" runat="server">CSR Name</asp:Label><span id="spnCSR" runat="server"
                                                class="mandatory">*</span>
                                            <br />
                                            <select id="cmbCSR" onfocus="SelectComboIndex('cmbCSR')" runat="server" onchange="sethidCSR();">
                                            </select>
                                            <br />
                                            <asp:RequiredFieldValidator ID="rfvCSR" Display="Dynamic" ControlToValidate="cmbCSR"
                                                runat="server"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="midcolora">
                                        </td>
                                        <td class="midcolora">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="headerEffectSystemParams" colspan="4">
                                            <asp:Label ID="lblRemarksHeader" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="midcolora" style="width: 60%" id="tdRemarks">
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
                        <tr>
                            <td class="midcolora" align="left" style="width: 40%">
                                <cmsb:CmsButton class="clsButton" ID="btnReset" runat="server" Text="Reset" CausesValidation="false">
                                </cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnCreateNewVersion" runat="server" Visible="false"
                                    Text="Create New Version"></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnActivateDeactivate" Visible="false" runat="server"
                                    Text="" CausesValidation="false"></cmsb:CmsButton>
                            </td>
                            <td class="midcolorr" align="right" style="width: 60%">
                                <cmsb:CmsButton class="clsButton" ID="btnCopy" runat="server" Visible="false" CausesValidation="false"
                                    Text="Copy"></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnReject" runat="server" CausesValidation="false"
                                    Visible="false" Text="Reject"></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnSubmitAnyway" runat="server" Text="Submit Anyway"
                                    CausesValidation="false"></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnConvertAppToPolicy" Visible="false" runat="server"
                                    Text="Submit" causeValidation="false"></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnVerifyApplication" Visible="false" runat="server"
                                    Text="Verify App"></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnSave" runat="server" Text="Save"></cmsb:CmsButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="midcolora" align="left" style="width: 40%">
                                <cmsb:CmsButton class="clsButton" ID="btnBack" runat="server" Text="Back To Search"
                                    CausesValidation="false"></cmsb:CmsButton>
                                <cmsb:CmsButton class="clsButton" ID="btnDelete" Visible="false" runat="server" Text="Delete"
                                    CausesValidation="false"></cmsb:CmsButton>
                            </td>
                            <td class="midcolorr" align="right" style="width: 60%">
                                <cmsb:CmsButton class="clsButton" ID="btnCustomerAssistant" runat="server" Text="Back To Customer Assistant"
                                    CausesValidation="false"></cmsb:CmsButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <!-- END OF trDETAILS -->
        <%--							    <asp:panel id="hideForBrazil" Visible="false" runat="server">
							        <tr>									
									    <td class="midcolora" width="16%" colspan="2">
									        <asp:label id="capPROXY_SIGN_OBTAINED" Runat="server">Proxy Signature Obtained?</asp:label><span class="mandatory" >*</span>
									    </td>
									    <td class="midcolora" width="34%">
									        <asp:dropdownlist id="cmbPROXY_SIGN_OBTAINED" onfocus="SelectComboIndex('cmbPROXY_SIGN_OBTAINED')" runat="server"></asp:dropdownlist>
									        <br />
									        <asp:requiredfieldvalidator id="rfvPROXY_SIGN_OBTAINED" Enabled="false" runat="server" Display="Dynamic" ControlToValidate="cmbPROXY_SIGN_OBTAINED"></asp:requiredfieldvalidator>
									    </td>
								    </tr>								
								    <tr>
									    <td class="midcolora" width="18%">
									        <asp:label id="capCHARGE_OFF_PRMIUM" runat="server">Charge Of Premium</asp:label>
									    </td>
									    <td class="midcolora" width="32%">
									        <asp:dropdownlist id="cmbCHARGE_OFF_PRMIUM" Runat="server"></asp:dropdownlist>
									    </td>						
								    </tr>
								    <tr id="trPropInspCr">
									    <td class="midcolora" width="18%">
									        <asp:label id="capPROPRTY_INSP_CREDIT" runat="server">Property Inspection/Cost Estimator </asp:label><span class="mandatory" id="spnPROPRTY_INSP_CREDIT">*</span>
									    </td>
									    <td class="midcolora" width="32%">
									        <asp:dropdownlist id="cmbPROPRTY_INSP_CREDIT" onfocus="SelectComboIndex('cmbPROPRTY_INSP_CREDIT')" Runat="server"></asp:dropdownlist>
									        <br />
										    <asp:requiredfieldvalidator id="rfvPROPRTY_INSP_CREDIT" ControlToValidate="cmbPROPRTY_INSP_CREDIT" Display="Dynamic" Enabled="False" Runat="server"></asp:requiredfieldvalidator>
								        </td>
									    <td class="midcolora" width="16%">
									        <asp:label id="capPIC_OF_LOC" Runat="server"></asp:label><span class="mandatory" id="spnPIC_OF_LOC">*</span>
									    </td>
									    <td class="midcolora" width="34%">
									        <asp:dropdownlist id="cmbPIC_OF_LOC" onfocus="SelectComboIndex('cmbPIC_OF_LOC')" Runat="server"></asp:dropdownlist>
									        <br />
										    <asp:requiredfieldvalidator id="rfvPIC_OF_LOC" ControlToValidate="cmbPIC_OF_LOC" Display="Dynamic" Enabled="False" Runat="server"></asp:requiredfieldvalidator>
									    </td>
								    </tr>								
								    <tr>
									    <td class="midcolora" width="18%">
									        <asp:label id="capYEAR_AT_CURR_RESI" runat="server">Year at Current Residence</asp:label>
									    </td>
									    <td class="midcolora" width="32%">
									        <asp:textbox id="txtYEAR_AT_CURR_RESI" runat="server" size="4" maxlength="2"></asp:textbox>
									        <br />
										    <asp:regularexpressionvalidator Enabled="False" id="revYEAR_AT_CURR_RESI" runat="server" ControlToValidate="txtYEAR_AT_CURR_RESI" Display="Dynamic"></asp:regularexpressionvalidator>
										    <asp:rangevalidator id="rngYEAR_AT_CURR_RESI" runat="server" Enabled="false" Display="Dynamic" ControlToValidate="txtYEAR_AT_CURR_RESI" MaximumValue="99" MinimumValue="1" Type="Integer"></asp:rangevalidator>
									    </td>
									    <td class="midcolora" width="16%">
									        <asp:label id="capYEARS_AT_PREV_ADD" runat="server">Previous Address if less than 3 years(Max 250 characters)</asp:label><span class="mandatory" id="spnYEARS_AT_PREV_ADD">*</span>
									    </td>
									    <td class="midcolora" width="34%">
									        <asp:textbox id="txtYEARS_AT_PREV_ADD" runat="server" Columns="40" Rows="5" TextMode="MultiLine"></asp:textbox>
									        <br />
										    <asp:customvalidator id="csvYEARS_AT_PREV_ADD" Enabled="false" ControlToValidate="txtYEARS_AT_PREV_ADD" Display="Dynamic" Runat="server" ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator>
										    <asp:requiredfieldvalidator id="rfvYEARS_AT_PREV_ADD" runat="server" Enabled="false" ControlToValidate="txtYEARS_AT_PREV_ADD" Display="Dynamic"></asp:requiredfieldvalidator>
										</td>
								    </tr>
								    <tr id="trBILL_MORTAGAGEE">
									    <td class="midcolora" width="18%">
									        <asp:label id="capBILL_MORTAGAGEE" Runat="server">Mortgagee</asp:label>
									    </td>
									    <td class="midcolora" width="32%">
									        <asp:label id="lblBILL_MORTAGAGEE" Runat="server">N.A.</asp:label>
									    </td>
									    <td class="midcolora" colspan="2"></td>
								    </tr>								    
								    <tr>
									    <td class="midcolora">
									        <asp:label id="capProducer" Runat="server">Producer</asp:label>
									    </td>
									    <td class="midcolora">
									        <select id="cmbProducer" onfocus="SelectComboIndex('cmbProducer')" onchange="sethidProducer();"	Runat="server"></select>
									    </td>						
								    </tr>
								    <tr>
									    <td class="headerEffectSystemParams" colspan="4">
									        <asp:label id="lblAllPoliciesHeader" runat="server">Any other policy with Wolverine</asp:label>
									    </td>
								    </tr>
								    <tr>
									    <td class="midcolora" width="18%">
									        <asp:label id="lblPolicies" runat="server">All the policies with Wolverine</asp:label>
									    </td>
									    <td class="midcolora" width="32%">
									        <asp:label id="lblAllPolicy" runat="server"></asp:label>
									    </td>
									    <td class="midcolora" width="16%">
									        <asp:label id="lblPoliciesDiscount" runat="server">Policies eligible for discount</asp:label>
									    </td>
									    <td class="midcolora" width="34%">
									        <asp:label id="lblEligbilePolicy" runat="server"></asp:label>
									    </td>
								    </tr>
								</asp:panel>
        --%>
        <tr>
            <td>
                <input id="hidFormSaved" type="hidden" name="hidFormSaved" runat="server" />
                <input id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server" />
                <input id="hidOldData" type="hidden" value="0" name="hidOldData" runat="server" />
                <input id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server" />
                <input id="hidAPP_AGENCY_ID" type="hidden" value="0" name="hidAPP_AGENCY_ID" runat="server" />
                <input id="hidCSR" type="hidden" value="0" name="hidCSR" runat="server" />
                <input id="hidPAYOR" type="hidden" value="0" name="hidPAYOR" runat="server" />
                <input id="hidAppID" type="hidden" value="0" name="hidAppID" runat="server" />
                <input id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server" />
                <input id="hidLOBID" type="hidden" value="0" name="hidLOBID" runat="server" />
                <input id="hidSUB_LOB" type="hidden" value="0" name="hidSUB_LOB" runat="server" />
                <input id="hidLOBXML" type="hidden" value="0" name="hidLOBXML" runat="server" />
                <input id="hidStateID" type="hidden" value="0" name="hidStateID" runat="server" />
                <input id="hidUnderwriter" type="hidden" name="hidUnderwriter" runat="server" />
                <input id="hidDepartmentXml" type="hidden" name="hidDepartmentXml" runat="server" />
                <input id="hidProfitCenterXml" type="hidden" name="hidProfitCenterXml" runat="server" />
                <input id="hidDEPT_ID" type="hidden" name="hidDEPT_ID" runat="server" />
                <input id="hidPC_ID" type="hidden" name="hidPC_ID" runat="server" />
                <input id="hidReset" type="hidden" name="hidReset" runat="server" />
                <input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server" />
                <input id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server" value="0" />
                <input id="hidRuleVerify" type="hidden" value="0" name="hidRuleVerify" runat="server" />
                <input id="hidIs_Agency" type="hidden" value="N" name="hidIs_Agency" runat="server" />
                <input id="hidPOLICY_ID" type="hidden" name="hidPOLICY_ID" runat="server" />
                <input id="hidCallefroms" type="hidden" name="hidCallefroms" runat="server" />
                <input id="hidPOLICY_TYPE" type="hidden" name="Hidden1" runat="server" value="0" />
                <input id="hidPolicyNumber" type="hidden" name="hidPolicyNumber" runat="server" />
                <input id="hidPOLICY_VERSION_ID" type="hidden" name="hidPOLICY_VERSION_ID" runat="server" />
                <input id="hidFULL_PAY_PLAN_ID" type="hidden" name="hidFULL_PAY_PLAN_ID" runat="server" />
                <input id="hidBillingPlan" type="hidden" name="hidBillingPlan" runat="server" />
                <input id="hidAlert" type="hidden" value="0" name="hidAlert" runat="server" />
                <input id="hidFocusFlag" type="hidden" name="hidFocusFlag" runat="server" />
                <input id="hidProducer" type="hidden" name="hidProducer" runat="server" />
                <input id="hidDOWN_PAY_MODE" type="hidden" name="hidDOWN_PAY_MODE" runat="server" />
                <input id="hidBILL_TYPE_ID" type="hidden" name="hidBILL_TYPE_ID" runat="server" />
                <input id="hidINSTALL_PLAN_ID" type="hidden" name="hidINSTALL_PLAN_ID" runat="server" />
                <input id="hidDEACTIVE_INSTALL_PLAN_ID" type="hidden" name="hidDEACTIVE_INSTALL_PLAN_ID"
                    runat="server" />
                <input id="hidBILL_TYPE_FLAG" type="hidden" name="hidBILL_TYPE_FLAG" runat="server"
                    value="0" />
                <input id="hidIsTerminated" type="hidden" name="hidIsTerminated" runat="server" />
                <input id="hidLangCulture" type="hidden" name="hidLangCulture" runat="server" />
                <input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server" />
                <input id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server" />
                <input id="hidPolicyStatus" type="hidden" name="hidPolicyStatus" runat="server" />
                <input id="lblDelete" type="hidden" runat="server" />
                <input id="hidDIV_ID_DEPT_ID_PC_ID" type="hidden" runat="server" />
                <input id="hidRefresh" type="hidden" value="0" name="" runat="server" />
                <input id="hidRulesMessage" type="hidden" runat="server" value="" />
                <input id="hidApplicationStatus" type="hidden" runat="server" />
                <input id="hidAPP_TERMS" runat="server" type="hidden" value="" />
                <input id="hidInstall" runat="server" type="hidden" value="N" />
                <input id="hidPOLDOWN_PAY_MODE" runat="server" type="hidden" value="0" />
                <input id="hidPOLICY_CURRENCY" runat="server" type="hidden" value="0" />
                <input id="hidPOL_BILL_PLANMSG" runat="server" type="hidden" value="0" />
                <input id="hidPOL_TRAN_TYPE" name="hidPOL_TRAN_TYPE" runat="server" type="hidden"
                    value="0" />
                <input id="hidPOL_POLICY_STATUS" runat="server" type="hidden" value="" />
                <input id="hidUSER_SUPERVISOR" runat="server" name="hidUSER_SUPERVISOR" type="hidden"
                    value="" />
                <input id="hidAGENCY_ID" runat="server" name="hidAGENCY_ID" type="hidden" value="" />
                <input id="hidAPP_EFFECTIVE_DATE" runat="server" name="hidAPP_EFFECTIVE_DATE" type="hidden"
                    value="" />
                <input id="hidCustomerType" runat="server" name="hidCustomerType" type="hidden"
                    value="" />

            </td>
        </tr>
    </table>
    </form>
    <!-- For lookup layer -->
    <div id="lookupLayerFrame" style="display: none; z-index: 101; position: absolute">
        <iframe id="lookupLayerIFrame" style="display: none; z-index: 1001; background-color: #000000;
            margin-top: 0px; margin-left: 0px;" width="0" height="0"></iframe>
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
                            <img height="14" alt="Close" src="/cms/cmsweb/images/cross.gif" width="16" border="0" /></a></p>
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
        function showInputVerification() {
            parent.refWindow = window.open('ShowInputVerificationXml.aspx?CUSTOMER_ID=<%= gIntCUSTOMER_ID %>&APP_ID=<%= gIntAPP_ID %>&APP_VERSION_ID=<%= gIntAPP_VERSION_ID %>&LOBID=<%= gstrLobID %>&QUOTE_ID=<%= gIntQuoteID %>', 'Quote', " left=150,top=50,resizable=yes, scrollbars=yes,width=800,height=500");
        }
        function setDelayMenu() {
            setMenu();
        }
        window.setTimeout('setDelayMenu()', 1);

        if (document.getElementById('lblUNDERWRITER_UN_ASG')) {
            document.getElementById('lblUNDERWRITER_UN_ASG').innerText = document.getElementById('hidUnderwriter').value;
        }
        //alert(document.getElementById('lblUNDERWRITER_UN_ASG').innerText)



        function RefreshClient() {
            if (document.getElementById('hidRefresh').value == 'R') {
                var doc = this.parent.document;
                var str = '';

                str = document.getElementById('hidPolicyNumber').value + "-" +
				document.getElementById('hidApplicationStatus').value + " (" +
				 document.getElementById('cmbAPP_LOB').options[document.getElementById('cmbAPP_LOB').selectedIndex].innerText + ":" +
				document.getElementById("txtAPP_EFFECTIVE_DATE").value + "-" +
				document.getElementById("txtAPP_EXPIRATION_DATE").value + ")"
                doc.getElementById("cltClientTop_lblSAppNo").innerHTML = str;

                document.getElementById('hidRefresh').value = '';
            }
        }	
    </script>
</body>
</html>
