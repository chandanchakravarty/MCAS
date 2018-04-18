<%@ Page language="c#" Codebehind="AddPriorLoss.aspx.cs" AutoEventWireup="false" Inherits="Cms.Application.PriorLoss.AddPriorLoss" validateRequest=false%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddPriorLoss</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<LINK href="Common.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/Jquery/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		
		<script language="javascript">
		
			var jsaAppWebDtFormat = "<%=aAppWebDtFormat  %>";
			var jsaAppWebSepFormat = "<%=aAppWebDtSep  %>";
			var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		
		
			//This function is used for refreshing the form 
			//To be called in while user clicks on Reset button 
			//and also after the form get saved
			function AddData()
			{
				ChangeColor();
				DisableValidators();
				document.getElementById('hidLOSS_ID').value	=	'New';
				//document.getElementById('txtOCCURENCE_DATE').focus();
				document.getElementById('txtOCCURENCE_DATE').value  = '';
			//	document.getElementById('txtCLAIM_DATE').value  = '';
				document.getElementById('cmbLOB').options.selectedIndex =0;
				document.getElementById('cmbLOSS_TYPE').options.selectedIndex = -1;
				//SelectComboOption('cmbLOSS_TYPE','All Other');
				document.getElementById('txtAMOUNT_PAID').value  = '';
			//	document.getElementById('txtAMOUNT_RESERVED').value  = '';
				document.getElementById('cmbCLAIM_STATUS').options.selectedIndex = -1;
				document.getElementById('cmbRELATIONSHIP').options.selectedIndex = -1;				
				document.getElementById('cmbCLAIMS_TYPE').options.selectedIndex = -1;				
				document.getElementById('cmbCHARGEABLE').options.selectedIndex = -1;				
				document.getElementById('cmbAT_FAULT').options.selectedIndex = -1;		
				document.getElementById('cmbWaterBackup_SumpPump_Loss').options.selectedIndex = -1;	 //Added by Charles on 30-Nov-09 for Itrack 6647
				document.getElementById('cmbWeather_Related_Loss').options.selectedIndex = -1;	 //Added for Itrack 6640 on 9 Dec 09			
				//document.getElementById('txtLOSS_DESC').value  = '';			
				//document.getElementById('txtLOSS_DESC').value  = '';
				document.getElementById('txtDESC_OF_LOSS_AND_REMARKS').value  = '';
			//	document.getElementById('txtMOD').value  = '';
			//	document.getElementById('txtCAT_NO').value  = '';
			//	document.getElementById('chkLOSS_RUN').checked=false;
				if(document.getElementById('btnDelete'))
					document.getElementById('btnDelete').setAttribute('disabled',true);
				if(document.getElementById('lblLOSS'))
				document.getElementById('lblLOSS').innerHTML ='';
					
			}
			function SetChargeableField()
			{
				comboLOB = document.getElementById("cmbLOB");
				if(comboLOB==null || comboLOB.selectedIndex==-1 || comboLOB.options[comboLOB.selectedIndex].value!="2")
				{
					//document.getElementById("cmbCHARGEABLE").selectedIndex = 0;
					if(comboLOB.options[comboLOB.selectedIndex].value!="3")
						return;
				}
				//We have auto LOB as selected
				
				comboFault = document.getElementById("cmbAT_FAULT"); 
				if(comboFault==null || comboFault.selectedIndex==-1 || comboFault.options[comboFault.selectedIndex].value!="10963")
				{
					//document.getElementById("cmbCHARGEABLE").selectedIndex = 0;
					return;
				}				
				
				amt = new String(document.getElementById("txtAMOUNT_PAID").value);
				amt = replaceAll(amt,",","");
				//if(amt!="" && !isNaN(amt))
				//{
					if(amt>=1000 && comboLOB.options[comboLOB.selectedIndex].value=="2")
						document.getElementById("cmbCHARGEABLE").selectedIndex = 1;
					else if(amt>=500 && comboLOB.options[comboLOB.selectedIndex].value=="3")
						document.getElementById("cmbCHARGEABLE").selectedIndex = 1;
					else
						document.getElementById("cmbCHARGEABLE").selectedIndex = 2;
					
				//}
				//else
				//{
					//document.getElementById("cmbCHARGEABLE").selectedIndex = -1;
				//	return;
				//}
				
			}			
			
			function populateXML()
			{
				var tempXML;
				tempXML=document.getElementById("hidOldData").value; 
				if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
				{	
					if(tempXML!="")
					{
						if(document.getElementById('btnDelete'))
							document.getElementById('btnDelete').setAttribute('disabled',false); 
						populateFormData(tempXML,"APP_PRIOR_LOSS_INFO");
						$('#txtAMOUNT_PAID').blur();
					}
					else
					{
						AddData();
					}
					try
					{
						document.getElementById('txtOCCURENCE_DATE').focus();		
					}
					catch(e)
					{}
				}
				ShowHideFields();
				if(document.getElementById("cmbLOSS_TYPE").selectedIndex!=-1 && document.getElementById("cmbLOSS_TYPE").selectedIndex!=null)
				{
					Loss_type();
				}
				else
				{
					document.getElementById('capOTHER_DESC').style.display ="none";
					document.getElementById('txtOTHER_DESC').style.display ="none";	
				}	
				
							
				return false;			
				
			}
			
			function SetTab()
			{
				if((document.getElementById('hidFormSaved').value == '1') || (top.frames[1].strXML != ""))
				{
					Url="AttachmentIndex.aspx?EntityType=Agency&EntityId="+document.getElementById('hidRowId').value;
					DrawTab(2,top.frames[1],'Attachment',Url);
				}
				else
				{							
					RemoveTab(2,top.frames[1]);
				}			
			}
			
			function PopulateDriverCombo(LOB_ID)
			{
				if(document.getElementById("hidDRIVER_XML").value=="" || document.getElementById("hidDRIVER_XML").value=="0") return;
				
				strXML = document.getElementById("hidDRIVER_XML").value;
				var xmlDoc = new ActiveXObject("Microsoft.XMLDOM") ;
				xmlDoc.async=false;
				xmlDoc.loadXML(strXML);
				xmlTableNodes = xmlDoc.selectNodes("/NewDataSet/Table[LOB='" + LOB_ID + "']");
				
				document.getElementById('cmbDRIVER_ID').length=0;
				document.getElementById('cmbDRIVER_ID').options[document.getElementById('cmbDRIVER_ID').length]= new Option("","0");
								
				for(var i = 0; i < xmlTableNodes.length; i++ )
				{
					var text = 	xmlTableNodes[i].selectSingleNode('DRIVER_NAME').text;
					var value = 	xmlTableNodes[i].selectSingleNode('DRIVER_ID').text;
					document.getElementById('cmbDRIVER_ID').options[document.getElementById('cmbDRIVER_ID').length]= new Option(text,value);
				}		
				document.getElementById('cmbDRIVER_ID').options[document.getElementById('cmbDRIVER_ID').length]= new Option("Other","-1");
				
			}		
		
		//This function will be called from the grid object while user double clicks
			//on any record in Grid
			
			/*function populateXML()
			{
					//alert(document.getElementById('hidFormSaved').value)
				if(document.getElementById('hidFormSaved').value == '0')
				{
					DisableValidators();
					var tempXML;			
					//alert(top.frames[1].strXML)
					if(top.frames[1].strXML!="")
					{
						//document.getElementById('btnReset').style.display='none';
						
						tempXML=top.frames[1].strXML;
						//alert(tempXML);
						
						//Enabling the activate deactivate button
						document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
						                        
						//Storing the XML in hidOldData hidden fields 
						document.getElementById('hidOldData').value		=	 tempXML;
						populateFormData(tempXML,Form1);				
				
					}
					else
					{
						AddData();
					}
				}
				else
				{
					//DisableValidators();
					//document.getElementById('btnReset').style.display='none';
				}
				
				SetTab();
				return false;
			}*/
			
			function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 50 ) 
				{
					//event.returnValue=false;
					arguments.IsValid = false;
					return;   // invalid userName
				}
			}
			
			function ChkTextAreaLengthLoc(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 70 ) 
				{
					//event.returnValue=false;
					arguments.IsValid = false;
					return;   // invalid userName
				}
			}

			function DisplayDriverFields(flag)
			{
				combo = document.getElementById("cmbDRIVER_ID");								
				if(combo==null && combo.selectedIndex==-1)
					return false;
				
				if(flag && combo.options[combo.selectedIndex].value=="-1")
				{
					document.getElementById("trRow2").style.display="inline";
					document.getElementById("spnDRIVER_NAME").style.display="inline";					
					document.getElementById("spnRELATIONSHIP").style.display="inline";					
					document.getElementById("rfvDRIVER_NAME").setAttribute("enabled",true);
					document.getElementById("rfvDRIVER_NAME").setAttribute("isValid",true);
					document.getElementById("rfvRELATIONSHIP").setAttribute("enabled",true);					
					document.getElementById("rfvRELATIONSHIP").setAttribute("isValid",true);										
				}
				else
				{
					document.getElementById("trRow2").style.display="none";
					document.getElementById("rfvRELATIONSHIP").setAttribute("enabled",false);
					document.getElementById("rfvRELATIONSHIP").setAttribute("isValid",false);
					document.getElementById("rfvRELATIONSHIP").style.display="none";
					document.getElementById("rfvDRIVER_NAME").style.display="none";
					document.getElementById("rfvDRIVER_NAME").setAttribute("enabled",false);
					document.getElementById("rfvDRIVER_NAME").setAttribute("isValid",false);
					document.getElementById("spnDRIVER_NAME").style.display="none";					
					document.getElementById("spnRELATIONSHIP").style.display="none";					
				}				
				ChangeColor();
				return false;
			}
			function ShowHideFields()
			{
				
				combo = document.getElementById("cmbLOB");
				if(combo==null || combo.selectedIndex==-1)
					return;				
				//alert(combo.options[combo.selectedIndex].value);
				if (combo.options[combo.selectedIndex].value=="1" || combo.options[combo.selectedIndex].value=="6")
					{
						document.getElementById("Home_rental_loss").style.display = "inline";
						if(combo.options[combo.selectedIndex].value=='1') //Added by Charles on 30-Nov-09 for Itrack 6647
						{
							document.getElementById("trWaterBackup_SumpPump_Loss").style.display = "inline";
							document.getElementById("trWeather_Related_Loss").style.display = "inline";
						}//Added till here
						else//Added by Charles on 14-Dec-09 for Itrack 6647
						{
							document.getElementById("Home_rental_loss").style.display = "none";
							document.getElementById('cmbWaterBackup_SumpPump_Loss').options.selectedIndex = -1; 
							document.getElementById("trWaterBackup_SumpPump_Loss").style.display = "none";
							document.getElementById('cmbWeather_Related_Loss').options.selectedIndex = -1; 
							document.getElementById("trWeather_Related_Loss").style.display = "none";
						}//Added till here
					}
				else
					{
						document.getElementById("Home_rental_loss").style.display = "none";
						document.getElementById('cmbWaterBackup_SumpPump_Loss').options.selectedIndex = -1; //Added by Charles on 30-Nov-09 for Itrack 6647
						document.getElementById("trWaterBackup_SumpPump_Loss").style.display = "none"; //Added by Charles on 30-Nov-09 for Itrack 6647
						//Added for Itrack 6640 on 9 Dec 09
						document.getElementById('cmbWeather_Related_Loss').options.selectedIndex = -1; 
						document.getElementById("trWeather_Related_Loss").style.display = "none";
					}
				if(combo.options[combo.selectedIndex].value=="2" || combo.options[combo.selectedIndex].value=="3")
				{
					document.getElementById("trRow1").style.display = "inline";
					document.getElementById("trRow2").style.display = "inline";
					document.getElementById("trRow3").style.display = "inline";
					document.getElementById("trRow4").style.display = "inline";							
					if(combo.options[combo.selectedIndex].value=="2" || combo.options[combo.selectedIndex].value=="3")
					{
						PopulateDriverCombo(combo.options[combo.selectedIndex].value);
						if(document.getElementById("hidDRIVER_ID").value!="" && document.getElementById("hidDRIVER_ID").value!="-1" && !isNaN(document.getElementById("hidDRIVER_ID").value))
						{
							//var str = new String(document.getElementById("txtDRIVER_NAME").value);
							//str = str.split('^')[3];							
							//SetComboValueForConcatenatedString("cmbDRIVER_ID",str,'^',3);
							SelectComboOption("cmbDRIVER_ID",document.getElementById("txtDRIVER_NAME").value);
							document.getElementById("txtDRIVER_NAME").value = "";							
						}
						else
						{
							SelectComboOption("cmbDRIVER_ID",document.getElementById("hidDRIVER_ID").value);
						}						
					}
					DisplayDriverFields(true);					
				}
				else
				{
					document.getElementById("trRow1").style.display = "none";
					document.getElementById("trRow2").style.display = "none";
					document.getElementById("trRow3").style.display = "none";
					document.getElementById("trRow4").style.display = "none";
					DisplayDriverFields(false);
				}				
				return false;
			}
			function ResetTheForm()
			{
				DisableValidators();
				populateXML();
				return false;
			}			
			
			function CheckQueryString()
			{
				var qStr=new String();
				qStr=self.location.toString() ;
				
				var arrQStr=qStr.split("&"); 
				

				if(arrQStr.length<3)
					document.getElementById("newRow").style.display="none";  
				else
					document.getElementById("newRow").style.display="inline"  ;		
				
			}
			
			
	/*function ChkDate(objSource , objArgs)
	{
		var effdate=document.Form1.txtOCCURENCE_DATE.value;
		var expdate=document.Form1.txtCLAIM_DATE.value;
		objArgs.IsValid = DateComparer(expdate, effdate, jsaAppDtFormat);
	}	
		 */
	function ChkOccurenceDate(objSource , objArgs)
	{
		var effdate=document.Form1.txtOCCURENCE_DATE.value;
		objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
	}		
	function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbLOSS_TYPE":
						lookupMessage	=	"CLOSS.";
						break;
					case "cmbLOB":
						lookupMessage	=	"LOBCD.";
						break;
					case "cmbCLAIM_STATUS":
						lookupMessage	=	"CLAIM.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
		function Loss_type()
		{
			if(document.getElementById("cmbLOSS_TYPE").options[document.getElementById("cmbLOSS_TYPE").selectedIndex].value=='14246')
			{
				document.getElementById('capOTHER_DESC').style.display ="inline";
				document.getElementById('txtOTHER_DESC').style.display ="inline";	
			}
			else
			{
				document.getElementById('capOTHER_DESC').style.display ="none";
				document.getElementById('txtOTHER_DESC').style.display ="none";	
			}			
		}
		//Done for Itrack Issue 6640 on 10 Dec 09
		function Weather_Related_Loss()
		{//debugger
			combo = document.getElementById("cmbLOB");
			comboWeather_Related_Loss = document.getElementById("cmbWeather_Related_Loss");
			if(combo.options[combo.selectedIndex].value =='1')
			{
				if(comboWeather_Related_Loss.selectedIndex !=-1 && comboWeather_Related_Loss.selectedIndex !=0)
				{
					document.getElementById("rfvWeather_Related_Loss").setAttribute("enabled",false);
					document.getElementById("rfvWeather_Related_Loss").setAttribute("isValid",false);
					document.getElementById('rfvWeather_Related_Loss').style.display ="none";	
					return true;
				}
				else
				{
					document.getElementById("rfvWeather_Related_Loss").setAttribute("enabled",true);
					document.getElementById("rfvWeather_Related_Loss").setAttribute("isValid",true);
					document.getElementById('rfvWeather_Related_Loss').style.display ="inline";	
					return false;
				}			
			}
			else
			{
				document.getElementById("rfvWeather_Related_Loss").setAttribute("enabled",false);
				document.getElementById("rfvWeather_Related_Loss").setAttribute("isValid",false);
				document.getElementById('rfvWeather_Related_Loss').style.display ="none";	
				return true;
			}
				
		}
		</script>
	</HEAD>
	<body class="bodyBackGround" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();"
		MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<table width="100%" align="center" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<td>
						<table width="100%" align="center" border="0">
							<TBODY>
								<tr>
									<TD class="pageHeader" width="100%" colSpan="4"><asp:label ID="capMessage" runat="server"></asp:label>
									</TD><%--Please note that all fields marked with * are mandatory--%> 
								</tr>
								<tr>
									<td class="midcolorc" align="center" width="100%" colSpan="4"><asp:label class="errmsg" id="lblMessage" Visible="False" Runat="server"></asp:label></td>
								</tr>
								<tr id="newRow" height="20" runat="server">
									<td class="midcolora" width="33%"><asp:label id="capLOSS" Runat="server"></asp:label></td>
									<td class="midcolora" width="33%"><asp:label id="lblLOSS" Runat="server"></asp:label></td>
									<td class="midcolora" colSpan="2"></td>
								</tr>
								<tr>
									<td class="midcolora" width="33%"><asp:label id="capOCCURENCE_DATE" Runat="server"></asp:label><span class="mandatory">*</span>
                                    <br /><asp:textbox id="txtOCCURENCE_DATE" Runat="server" Width="70" MaxLength="10"></asp:textbox><asp:hyperlink id="hlkOCCURENCE_DATE" runat="server" CssClass="HotSpot">
											<asp:image id="imgOCCURENCE_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
										</asp:hyperlink><br>
										<asp:requiredfieldvalidator id="rfvOCCURENCE_DATE" runat="server" Display="Dynamic" ControlToValidate="txtOCCURENCE_DATE"
											ErrorMessage="OCCURANCE_DATE can't be blank."></asp:requiredfieldvalidator>
										<asp:regularexpressionvalidator id="revOCCURENCE_DATE" Runat="server" ControlToValidate="txtOCCURENCE_DATE" Display="Dynamic"></asp:regularexpressionvalidator><asp:customvalidator id="csvOccurence_date" Runat="server" ControlToValidate="txtOCCURENCE_DATE" Display="Dynamic"
											ClientValidationFunction="ChkOccurenceDate"></asp:customvalidator></td>
					
					<td class="midcolora" width="33%"><asp:label id="capLOB" Runat="server"></asp:label><br />
                    <asp:dropdownlist id="cmbLOB" Runat="server"></asp:dropdownlist>
						<a class="calcolora" href="javascript:showPageLookupLayer('cmbLOB')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
					</td>
                    <td class="midcolora" width="33%"><asp:label id="capLOSS_TYPE" Runat="server"></asp:label><br />
                    <asp:dropdownlist id="cmbLOSS_TYPE" Runat="server" onchange="Loss_type();" Width="200px" ></asp:dropdownlist>
						<a class="calcolora" href="javascript:showPageLookupLayer('cmbLOSS_TYPE')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
					</td>
				</TR>
				<tr>
					
					<!--<td class="midcolora" width="33%"><asp:label id="capLOSS_DESC" Runat="server"></asp:label></td>
					<td class="midcolora" width="33%"><asp:textbox id="txtLOSS_DESC" Runat="server" MaxLength="200"></asp:textbox></td>-->
					<td class="midcolora" width="33%"><asp:label id="capAMOUNT_PAID" Runat="server"></asp:label><br />
                    <asp:textbox id="txtAMOUNT_PAID" CssClass="INPUTCURRENCY" Runat="server" Width="90" MaxLength="9"></asp:textbox><br>
						<asp:regularexpressionvalidator id="revAMOUNT_PAID" Runat="server" ControlToValidate="txtAMOUNT_PAID" Display="Dynamic"
							Enabled="true"></asp:regularexpressionvalidator></td>

                            <td class="midcolora" width="33%"><asp:label id="capCLAIM_STATUS" Runat="server"></asp:label>
                    <br /><asp:dropdownlist id="cmbCLAIM_STATUS" Runat="server"></asp:dropdownlist>
						<a class="calcolora" href="javascript:showPageLookupLayer('cmbCLAIM_STATUS')"><img src="/cms/cmsweb/images/info.gif" width="17" height="16" border="0"></a>
					</td>

                    <td class="midcolora" width="33%"><asp:label id="capDESC_OF_LOSS_AND_REMARKS" Runat="server"></asp:label><br />
                    <asp:textbox onkeypress="MaxLength(this,50);" id="txtDESC_OF_LOSS_AND_REMARKS" Runat="server"
							MaxLength="50" TextMode="MultiLine"></asp:textbox><br>
						<asp:customvalidator id="csvREMARKS" Runat="server" ControlToValidate="txtDESC_OF_LOSS_AND_REMARKS" Display="Dynamic"
							ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></td>
				</tr>
				
				<tr>
					<td class="midcolora" width="33%"><asp:label id="capLOSS_LOCATION" Runat="server"></asp:label>
                    <br /><asp:textbox onkeypress="MaxLength(this,70);" id="txtLOSS_LOCATION" Runat="server"
							MaxLength="50" width="200" TextMode="MultiLine"></asp:textbox><br>
						<asp:customvalidator id="csvLOSS_LOCATION" Runat="server" ControlToValidate="txtLOSS_LOCATION" Display="Dynamic"
							ClientValidationFunction="ChkTextAreaLengthLoc"></asp:customvalidator></td>
					<td class="midcolora" width="33%"><asp:label id="capCAUSE_OF_LOSS" Runat="server"></asp:label>
                    <br /><asp:textbox onkeypress="MaxLength(this,50);" id="txtCAUSE_OF_LOSS" Runat="server"
							MaxLength="50" TextMode="MultiLine"></asp:textbox><br>
						<asp:customvalidator id="csvCAUSE_OF_LOSS" Runat="server" ControlToValidate="txtCAUSE_OF_LOSS" Display="Dynamic"
							ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></td>

                    	<td class="midcolora" width="33%"><asp:label id="capPOLICY_NUM" Runat="server"></asp:label>
                    <br /><asp:textbox onkeypress="MaxLength(this,70);" id="txtPOLICY_NUM" Runat="server"
							MaxLength="50" TextMode="MultiLine"></asp:textbox><br>
						<asp:customvalidator id="csvPOLICY_NUM" Runat="server" ControlToValidate="txtPOLICY_NUM" Display="Dynamic"
							ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></td>
				</tr>
				<tr>				
					<td class="midcolora" width="33%"><asp:label id="capLOSS_CARRIER" Runat="server"></asp:label>
                    <br /><asp:textbox onkeypress="MaxLength(this,50);" id="txtLOSS_CARRIER" Runat="server"
							MaxLength="50" TextMode="MultiLine"></asp:textbox><br>
						<asp:customvalidator id="csvLOSS_CARRIER" Runat="server" ControlToValidate="txtLOSS_CARRIER" Display="Dynamic"
							ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></td>

                    <td class="midcolora" width="33%"><asp:label id="capAPLUS_REPORT_ORDERED" Runat="server"></asp:label>
                    <br /><asp:dropdownlist id="cmbAPLUS_REPORT_ORDERED" Runat="server"></asp:dropdownlist></td>

                    <td class="midcolora" width="33%"><asp:label id="capNAME_MATCH" Runat="server"></asp:label>
                    <br /><asp:dropdownlist id="cmbNAME_MATCH" Runat="server"></asp:dropdownlist></td>
					
				</tr>
				
				<tr>
                    <td class="midcolora" width="33%"><asp:label id="capOTHER_DESC" Runat="server"></asp:label>
                    <br /><asp:textbox onkeypress="MaxLength(this,50);" id="txtOTHER_DESC" Runat="server"
							MaxLength="50" TextMode="MultiLine"></asp:textbox></td>
					
					<td class="midcolora" colspan="2" width="33%">
                    <br /></td>
				</tr>
				<tr id="trRow1">
					<td class="midcolora" width="33%" style="HEIGHT: 15px"><asp:label id="capDRIVER_ID" Runat="server"></asp:label>
                    <br /><asp:dropdownlist id="cmbDRIVER_ID" Runat="server"></asp:dropdownlist></td>
					<td class="midcolora" colspan="2" style="HEIGHT: 15px"></td>
				</tr>
				<tr id="trRow2">
					<td class="midcolora" width="33%"><asp:label id="capDRIVER_NAME" Runat="server"></asp:label><span id="spnDRIVER_NAME" class="mandatory">*</span>
                    <br /><asp:TextBox ID="txtDRIVER_NAME" Runat="server" size="20" MaxLength="50"></asp:TextBox><br>
						<asp:RequiredFieldValidator ID="rfvDRIVER_NAME" Runat="server" ControlToValidate="txtDRIVER_NAME" Display="Dynamic"></asp:RequiredFieldValidator>
					</td>
					<td class="midcolora" width="33%"><asp:label id="capRELATIONSHIP" Runat="server"></asp:label><span id="spnRELATIONSHIP" class="mandatory">*</span>
                    <br /><asp:dropdownlist id="cmbRELATIONSHIP" Runat="server"></asp:dropdownlist><br>
						<asp:RequiredFieldValidator ID="rfvRELATIONSHIP" Runat="server" ControlToValidate="cmbRELATIONSHIP" Display="Dynamic"></asp:RequiredFieldValidator></td>
				<tr id="trRow3">
					<td class="midcolora" width="33%"><asp:label id="capCLAIMS_TYPE" Runat="server"></asp:label>
                    <br /><asp:dropdownlist id="cmbCLAIMS_TYPE" Runat="server"></asp:dropdownlist></td>
					<td class="midcolora" width="33%"><asp:label id="capAT_FAULT" Runat="server"></asp:label>
                    <br /><asp:dropdownlist id="cmbAT_FAULT" Runat="server"></asp:dropdownlist></td>
				</tr>
				<tr id="trRow4">
					<td class="midcolora" width="33%"><asp:label id="capCHARGEABLE" Runat="server"></asp:label>
                    <br /><asp:dropdownlist id="cmbCHARGEABLE" Runat="server"></asp:dropdownlist></td>
					<td class="midcolora" colspan="2"></td>
				</tr>	
				<!-- Added by Charles on 30-Nov-09 for Itrack 6647 -->			
				<tr id=trWaterBackup_SumpPump_Loss runat="server">
				<td class="midcolora" width="33%"><asp:label id="capWaterBackup_SumpPump_Loss" Runat="server"></asp:label>
                <br /><asp:dropdownlist id="cmbWaterBackup_SumpPump_Loss" Runat="server"></asp:dropdownlist></td>
				<td class="midcolora" colspan="2"></td>				
				</tr>
				<!-- Added till here -->
				
				<!-- Added for Itrack 6640 on 9 Dec 09 Is this a Weather Related Loss-->			
				<tr id=trWeather_Related_Loss runat="server">
				  <td class="midcolora" width="33%"><asp:label id="capWeather_Related_Loss" Runat="server"></asp:label><span id="spnWeather_Related_Loss" class="mandatory">*</span>
                  <br /><asp:dropdownlist id="cmbWeather_Related_Loss" Runat="server" onChange="Weather_Related_Loss()"></asp:dropdownlist><br/>
					<asp:requiredfieldvalidator id="rfvWeather_Related_Loss" runat="server" Display="Dynamic" ControlToValidate="cmbWeather_Related_Loss" ErrorMessage="Weather_Related_Loss."></asp:requiredfieldvalidator>
				  </td>
				  <td class="midcolora" colspan="2"></td>
				</tr>
				<td class="midcolora" colspan="2"></td>				
				</tr>
				<!-- Added till here -->
				<!-- added by pravesh   -->
				<tr id="Home_rental_loss">
					<td colspan ="4">
						<table width="100%" align="center" border="0">
						<tr>
						<td class="pageHeader" width="100%" colSpan="4">Loss Location</td>
						</tr>
						<tr>
							<td class=midcolora width="33%"><asp:label id="capLOSS_ADD1" Runat="server">Address1</asp:label></td>
							<td class="midcolora" width="33%"><asp:TextBox ID="txtLOSS_ADD1" Runat="server" size="40" MaxLength="50"></asp:TextBox></td>
							<td class=midcolora width="33%"><asp:label id="capLOSS_ADD2" Runat="server">Address2</asp:label></td>
							<td class="midcolora" width="33%"><asp:TextBox ID="txtLOSS_ADD2" Runat="server" size="40" MaxLength="50"></asp:TextBox></td>
						</tr>
						<tr>
							<td class=midcolora width="33%"><asp:label id="capLOSS_CITY" Runat="server">City</asp:label></td>
							<td class="midcolora" width="33%"><asp:TextBox ID="txtLOSS_CITY" Runat="server" size="20" MaxLength="50"></asp:TextBox></td>
							<td class=midcolora width="33%"><asp:label id="capLOSS_STATE" Runat="server">State</asp:label></td>
							<td class="midcolora" width="33%">
							<asp:dropdownlist id="cmbLOSS_STATE" Runat="server"></asp:dropdownlist></td>
							</td>
						</tr>
						<tr>
							<td class=midcolora width="33%"><asp:label id="capLOSS_ZIP" Runat="server">Zip</asp:label></td>
							<td class="midcolora" width="33%"><asp:TextBox ID="txtLOSS_ZIP" Runat="server" size="12" MaxLength="11"></asp:TextBox>
							<%-- Added by Swarup on 05-apr-2007 --%>
							<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
							<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
							</asp:hyperlink><br>
							<asp:regularexpressionvalidator id="revLOSS_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtLOSS_ZIP"></asp:regularexpressionvalidator>
							</td>
							<td colspan="2" class=midcolora ></td>
						</tr>
						<tr>
						<td class="pageHeader" width="100%" colSpan="4">Current Location</td>
						</tr>
						<tr>
							<td class=midcolora width="33%"><asp:label id="capCURRENT_ADD1" Runat="server">Address1</asp:label></td>
							<td class="midcolora" width="33%"><asp:TextBox ID="txtCURRENT_ADD1" Runat="server" size="40" MaxLength="50"></asp:TextBox></td>
							<td class=midcolora width="33%"><asp:label id="capCURRENT_ADD2" Runat="server">Address2</asp:label></td>
							<td class="midcolora" width="33%"><asp:TextBox ID="txtCURRENT_ADD2" Runat="server" size="40" MaxLength="50"></asp:TextBox></td>
						</tr>
						<tr>
							<td class=midcolora width="33%"><asp:label id="capCURRENT_CITY" Runat="server">City</asp:label></td>
							<td class="midcolora" width="33%"><asp:TextBox ID="txtCURRENT_CITY" Runat="server" size="20" MaxLength="50"></asp:TextBox></td>
							<td class=midcolora width="33%"><asp:label id="capCURRENT_STATE" Runat="server">State</asp:label></td>
							<td class="midcolora" width="33%">
							<asp:dropdownlist id="cmbCURRENT_STATE" Runat="server"></asp:dropdownlist></td>
							</td>
						</tr>
						<tr>
							<td class=midcolora width="33%"><asp:label id="capCURRENT_ZIP" Runat="server">Zip</asp:label></td>
							<td class="midcolora" width="33%"><asp:TextBox ID="txtCURRENT_ZIP" Runat="server" size="12" MaxLength="11"></asp:TextBox>
							<%-- Added by Swarup on 05-apr-2007 --%>
							<asp:hyperlink id="hlkCurZipLookup" runat="server" CssClass="HotSpot">
							<asp:image id="imgCurZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
							</asp:hyperlink><br>
							<asp:regularexpressionvalidator id="revCURRENT_ZIP" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator"
											ControlToValidate="txtCURRENT_ZIP"></asp:regularexpressionvalidator>
							</td>
							<td colspan="2" class=midcolora ></td>
						</tr>
						
						</table>
					</td>
				</tr>
				<!-- end here  --> 
				<tr>
					<td class="midcolora" width="33%"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" causesValidation="false"></cmsb:cmsbutton>&nbsp;<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton></td>
					<td class="midcolorr" width="33%" colspan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" Runat="server" text="Save"></cmsb:cmsbutton></td>
				</tr>
			</table>
			<input id="hidFormSaved" type="hidden" name="hidFormSaved" value="0" runat="server">
			<input id="hidDRIVER_ID" type="hidden" name="hidDRIVER_ID" value="0" runat="server">
			<input id="hidRowId" type="hidden" name="hidRowId" runat="server"> <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<input id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> <input id="hidLOSS_ID" type="hidden" value="0" name="hidLOSS_ID" runat="server">
			<input id="hidCUSTOMER_ID" type="hidden" value="0" name="hidCUSTOMER_ID" runat="server">
			<input id="hidDRIVER_XML" type="hidden" name="hidDRIVER_XML" runat="server"> </TD></TR></TBODY></TABLE></form>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none;Z-INDEX: 101;POSITION: absolute">
			<iframe id="lookupLayerIFrame" style="DISPLAY: none;Z-INDEX: 1001;FILTER: alpha(opacity=0);BACKGROUND-COLOR: #000000"
				width="0" height="0" top="0px;" left="0px"></iframe>
		</div>
		<DIV id="lookupLayer" style="Z-INDEX: 101;VISIBILITY: hidden;POSITION: absolute" onmouseover="javascript:refreshLookupLayer();">
			<table class="TabRow" cellspacing="0" cellpadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b>Add LookUp</b></td>
					<td><p align="right"><a href="javascript:void(0)" onclick="javascript:hideLookupLayer();"><img src="/cms/cmsweb/images/cross.gif" border="0" alt="Close" WIDTH="16" HEIGHT="14"></a></p>
					</td>
				</tr>
				<tr class="SubTabRow">
					<td colspan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
		/*if (document.getElementById('hidFormSaved').value == "5")
		{
			document.getElementById('hidFormSaved').value = 1;
			RefreshWebGrid(document.getElementById('hidFormSaved').value,"<%=primaryKeyValues%>",false);					
			document.getElementById('hidFormSaved').value = 0;
		}
		else
		{*/
			RefreshWebGrid(document.getElementById('hidFormSaved').value,"<%=primaryKeyValues%>",false);					
		//}
	
			
		</script>
	</body>
</HTML>
