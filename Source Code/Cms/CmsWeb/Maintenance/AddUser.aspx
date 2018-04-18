<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddUser.aspx.cs" AutoEventWireup="false" validateRequest="false" Inherits="Cms.CmsWeb.Maintenance.AddUser" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>MNT_USER_LIST</title>	
		<link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet" />
		<script src="/cms/cmsweb/scripts/xmldom.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/common.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/form.js" type="text/javascript"></script>
		<script src="/cms/cmsweb/scripts/calendar.js" type="text/javascript"></script>
		
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
	<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
	<script type="text/javascript" src="/cms/cmsweb/scripts/CmsHelpScript/jQueryPageHelpFile.js"></script> 
	
		<script type="text/javascript" language="javascript">
			function AddData()
			{
				var clFrom='<%=strCalledFrom%>';
				document.getElementById('hidUSER_ID').value	=	'New';
				/*if(clFrom=='AGENCY')
					{
					document.getElementById('txtUSER_LOGIN_ID').focus();
					}
				else
					{
					document.getElementById('txtUSER_FNAME').focus();
					}*/
					
				//document.getElementById('txtUSER_TITLE').focus();
				document.getElementById('txtUSER_LOGIN_ID').value  = '';
				//document.getElementById('cmbUSER_TYPE_ID').value  = '';
				document.getElementById('txtUSER_PWD').value  = '';
				document.getElementById('txtUSER_CONFIRM_PWD').value  = '';
				document.getElementById('txtUSER_TITLE').value  = '';
				document.getElementById('txtUSER_FNAME').value  = '';
				document.getElementById('txtUSER_LNAME').value  = '';
				document.getElementById('txtUSER_INITIALS').value  = '';
				document.getElementById('txtUSER_ADD1').value  = '';
				document.getElementById('txtUSER_ADD2').value  = '';
				document.getElementById('txtUSER_CITY').value  = '';
				document.getElementById('cmbUSER_STATE').options.selectedIndex = -1;
				//document.getElementById('cmbUSER_COUNTRY').options.selectedIndex = -1;
				document.getElementById('txtUSER_ZIP').value  = '';
				document.getElementById('txtUSER_PHONE').value  = '';
				document.getElementById('txtUSER_EXT').value  = '';
				document.getElementById('txtUSER_FAX').value  = '';
				document.getElementById('txtUSER_MOBILE').value  = '';
				document.getElementById('txtUSER_EMAIL').value  = '';
				//document.getElementById('txtSSN_NO').value  = '';
				document.getElementById('txtDATE_OF_BIRTH').value  = '';
				document.getElementById('txtDRIVER_LIC_NO').value  = '';
				document.getElementById('txtDATE_EXPIRY').value  = '';
				document.getElementById('cmbLICENSE_STATUS').options.selectedIndex = -1;
				document.getElementById('txtNON_RESI_LICENSE_NO').value  = '';
				document.getElementById('cmbNON_RESI_LICENSE_STATE').options.selectedIndex = -1;

				document.getElementById('txtNON_RESI_LICENSE_NO2').value  = '';
				document.getElementById('cmbNON_RESI_LICENSE_STATE2').options.selectedIndex = -1;
				//document.getElementById('cmb_BRICS_USER').options.selectedIndex = -1;

				document.getElementById('cmbUSER_MGR_ID').options.selectedIndex = -1;
				//document.getElementById('cmbDefault_Hierarchy').options.selectedIndex = -1;
				document.getElementById('cmbUSER_TYPE_ID').options.selectedIndex = -1;
				//document.getElementById('cmbUSER_DEF_PC_ID').options.selectedIndex = -1;
				document.getElementById('cmbUSER_TIME_ZONE').options.selectedIndex = -1;
				//document.getElementById('cmbLIC_BRICS_USER').options.selectedIndex = 1;
				//document.getElementById('txtIS_ACTIVE').value  = '';
				//document.getElementById('cmbUSER_COLOR_SCHEME').options.selectedIndex = -1;
				if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
				document.getElementById('chkUSER_SPR').checked	= false;
				//Added by Sibin for Itrack Issue 4994 on 10 Dec 08
				document.getElementById('cmbCHANGE_PWD_NEXT_LOGIN').options.selectedIndex = 1;
				document.getElementById('cmbUSER_LOCKED').options.selectedIndex = 1;
				//Added till here
				
				//Added by Sibin for Itrack Issue 4173 on 15 Jan 09
				document.getElementById('txtNON_RESI_LICENSE_EXP_DATE').value  = '';
				document.getElementById('txtNON_RESI_LICENSE_EXP_DATE2').value = '';
				document.getElementById('txtREG_ID_ISSUE_DATE').value = '';
				document.getElementById('cmbNON_RESI_LICENSE_STATUS').options.selectedIndex = -1;
				document.getElementById('cmbNON_RESI_LICENSE_STATUS2').options.selectedIndex = -1;
				//Added till here
				ChangeColor();
				DisableValidators();
//				SSN_change(0);
			}
		  function populateXML()
			{	
				ResetAfterActivateDeactivate();			
				var tempXML = document.getElementById('hidOldData').value;				
					if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
					{
					//var tempXML;
					//Enabling the activate deactivate button
						if(tempXML!="")
						{
							//if(document.getElementById('hidFormSaved').value == '0')
							if(document.getElementById('btnActivateDeactivate'))	
								document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
						
							//Added by Sibin for Itrack Issue 5212 to remove USER_PWD and USER_CONFIRM_PWD from View Source - Done on 7 Jan 08
							document.getElementById('txtUSER_PWD').value= document.getElementById('hidPWD').value;
							document.getElementById('txtUSER_CONFIRM_PWD').value= document.getElementById('hidCONFIRM_PWD').value;							
							//Added till here					
							populateFormData(tempXML, MNT_USER_LIST); 
						    // for itrack no:677
							document.getElementById('hidUSER_TYPE_ID').value = document.getElementById('cmbUSER_TYPE_ID').value; //.options[document.getElementById('cmbUSER_TYPE_ID').options.selectedIndex].value;
							document.getElementById('txtDATE_OF_BIRTH').value = document.getElementById('hidDATE_OF_BIRTH_NEW').value;
							document.getElementById('txtDATE_EXPIRY').value = document.getElementById('hidDATE_EXPIRY_NEW').value;
							document.getElementById('txtNON_RESI_LICENSE_EXP_DATE').value = document.getElementById('hidNON_RESI_LICENSE_EXP_DATE_NEW').value;
							document.getElementById('txtNON_RESI_LICENSE_EXP_DATE2').value = document.getElementById('hidNON_RESI_LICENSE_EXP_DATE2_NEW').value;
							document.getElementById('txtREG_ID_ISSUE_DATE').value = document.getElementById('hidREG_ID_ISSUE_DATE_NEW').value;															
							
							SetActive();
							setStatusLabel();
							//SSN_hide();
						}
						else
						{
							AddData();
						}						
					}
					else
					{
						//if(document.getElementById('hidSSN_NO').value != '')
							//SSN_hide();
						//else
							//SSN_change(1);
							
					}
			  //setEncryptFields();			  
			  return false;
			  
			}
			
			function ResetAfterActivateDeactivate()
			{
				if (document.getElementById('hidReset').value == "1")
				{				
					document.MNT_USER_LIST.reset();			
				}
			}
			// Added by mohit on 23/05/2005.
			function setTab() {
			   if ((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != "")) 
				{
				    var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
				    var tabtitles = TAB_TITLES.split(',');
         
					combo = document.getElementById("cmbLIC_BRICS_USER");	
					
					if(document.getElementById('hidAGENCY_CODE').value == null || document.getElementById('hidAGENCY_CODE').value =="")
					{	
						if(combo !=null  && combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value!='10964')
						{
							
							Url="AddUserPreferences.aspx?USERID="+ document.getElementById('hidUSER_ID').value + "&";
							DrawTab(2, top.frames[1], tabtitles[0], Url);
							if(document.getElementById('hidAGENCY_LOGIN').value != null && document.getElementById('hidAGENCY_LOGIN').value != "1")
							{
								Url="AddSecurityRights.aspx?UserId="+ document.getElementById('hidUSER_ID').value +"&UserTypeId=" + document.getElementById('hidUSER_TYPE_ID').value + "&CALLED_FOR=" + document.getElementById('hidAGENCY_LOGIN').value + "&";
								DrawTab(3, top.frames[1], tabtitles[1], Url);
							}
							else
							{
								Url="AddSecurityRights.aspx?UserId="+ document.getElementById('hidUSER_ID').value +"&UserTypeId=" + document.getElementById('hidUSER_TYPE_ID').value +  "&";
								DrawTab(3, top.frames[1], tabtitles[1], Url);
							}							
						}
						else
						{
							RemoveTab(3,top.frames[1]);
							RemoveTab(2,top.frames[1]);
						}							
					}
					else
					{
					
						if(combo !=null  && combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value!='10964' && document.getElementById('hidLIC_BRICS_USER').value !='10964')
						{
							Url="AddUserPreferences.aspx?USERID="+ document.getElementById('hidUSER_ID').value + "&CALLAGENCY=" + document.getElementById('hidAGENCY').value + "&";
							DrawTab(2, parent, tabtitles[0], Url);
							if(document.getElementById('hidAGENCY_LOGIN').value != null && document.getElementById('hidAGENCY_LOGIN').value != "1")
							{
								Url="AddSecurityRights.aspx?UserId="+ document.getElementById('hidUSER_ID').value +"&UserTypeId=" + document.getElementById('hidUSER_TYPE_ID').value + "&CALLED_FOR=" + document.getElementById('hidAGENCY_LOGIN').value + "&";
                                RemoveTab(4,this.parent);
								DrawTab(3, parent, tabtitles[1], Url);
							}
							else
							{
								Url="AddSecurityRights.aspx?UserId="+ document.getElementById('hidUSER_ID').value +"&UserTypeId=" + document.getElementById('hidUSER_TYPE_ID').value + "&";
                                RemoveTab(4,this.parent);
								DrawTab(3, parent, tabtitles[1], Url);
            				}
				           
							//ADDED BY PRAVEEN KUMAR
							if(document.getElementById('hidAGENCY').value == "AGENCY")
							{
								Url="AssignAgency.aspx?UserId="+ document.getElementById('hidUSER_ID').value + "&CALLAGENCY=" + document.getElementById('hidAGENCY').value + "&USER_SUB_CODE=" + document.getElementById('txtSUB_CODE').value + "&";
								DrawTab(4, parent, tabtitles[2], Url);
							}
                            else if( document.getElementById('hidUSER_TYPE_ID').value=="13" && <%=GetLanguageID().ToString() %>=="3")
                            {
                             Url="UnderWritingAuthorityIndex.aspx?UserId="+ document.getElementById('hidUSER_ID').value +"&UserTypeId=" + document.getElementById('hidUSER_TYPE_ID').value + "&"+ "CalledFrom=User" + "&";
							 DrawTab(4, parent, 'UnderWriter Authority', Url);            
                            }
							//END PRAVEEN							
						}
						else
						{
							RemoveTab(4,this.parent);
							RemoveTab(3,this.parent);
							RemoveTab(2,this.parent);
						}					
					}
				}
				else
				{	
					if(document.getElementById('hidAGENCY_CODE').value == null || document.getElementById('hidAGENCY_CODE').value =="")
					{
						RemoveTab(3,top.frames[1]);
						RemoveTab(2,top.frames[1]);//
					}
					else
					{
						RemoveTab(4,this.parent);
						RemoveTab(3,this.parent);
						RemoveTab(2,this.parent);
					}
				}
			}
			//**********************Added by Manoj Rathore********
			/*function setTab2()
			{
				if((document.getElementById('hidFormSaved').value == '1') || (document.getElementById('hidOldData').value != ""))
				{
					combo = document.getElementById("cmb_BRICS_USER");			
					if(document.getElementById('hidAGENCY_CODE').value == null || document.getElementById('hidAGENCY_CODE').value =="")
					{		
						if(combo !=null  && combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value!='10964')
						{
							Url="AddUserPreferences.aspx?USERID="+ document.getElementById('hidUSER_ID').value +"&";
							DrawTab(2,top.frames[1],'Preferences',Url);
							Url="AddSecurityRights.aspx?UserId="+ document.getElementById('hidUSER_ID').value +"&UserTypeId=" + document.getElementById('hidUSER_TYPE_ID').value + "&";
							DrawTab(3,top.frames[1],'Assign Rights',Url);
						}
						else
						{
							RemoveTab(3,top.frames[1]);
							RemoveTab(2,top.frames[1]);
						}
					}
					else
					{
						if(combo !=null  && combo.selectedIndex!=-1 && combo.options[combo.selectedIndex].value!='10964')
						{
							Url="AddUserPreferences.aspx?USERID="+ document.getElementById('hidUSER_ID').value +"&";
							DrawTab(2,parent,'Preferences',Url);
							Url="AddSecurityRights.aspx?UserId="+ document.getElementById('hidUSER_ID').value +"&UserTypeId=" + document.getElementById('hidUSER_TYPE_ID').value + "&";
							DrawTab(3,parent,'Assign Rights',Url);
						}
						else
						{
							RemoveTab(3,this.parent);
							RemoveTab(2,this.parent);
						}
					}
				}
				else
				{	
					if(document.getElementById('hidAGENCY_CODE').value == null || document.getElementById('hidAGENCY_CODE').value =="")
					{
						RemoveTab(3,top.frames[1]);
						RemoveTab(2,top.frames[1]);
					}
					else
					{
						RemoveTab(3,this.parent);
						RemoveTab(2,this.parent);
					}
				}
			}*/			 
			
					function EnableDisableSubCode()
					{ 
						var clFrom='<%=strCalledFrom%>';
						 
						if(clFrom=='AGENCY')
						{						 
						//document.getElementById("capSUB_CODE").style.display = "true"  ;
						document.getElementById("capSUB_CODE").style.display = "inline"	 ;
						document.getElementById("txtSUB_CODE").style.display = "inline";
						document.getElementById("spnSUB_CODE").style.display = "inline"	;
						document.getElementById("rd1").style.display = "inline";
						document.getElementById("rfvSUB_CODE").setAttribute('enabled',true);
						document.getElementById("rfvSUB_CODE").setAttribute('isValid',true);					
							 
						//   document.getElementById("spnsub").style.display = "inline"
						}
						else	   
					{	   
						document.getElementById("txtSUB_CODE").style.display = "none";	
						document.getElementById("spnSUB_CODE").style.display = "none";
						document.getElementById("capSUB_CODE").style.display = "none"	;
						document.getElementById("rd1").style.display = "none";
						document.getElementById("rfvSUB_CODE").setAttribute('isValid',false);	
						document.getElementById("rfvSUB_CODE").style.display='none';
						document.getElementById("rfvSUB_CODE").setAttribute('enabled',false);							
					}				   
					  
					}
					 
					function EnableDisable()
					{
						var clFrom='<%=strCalledFrom%>';
						//alert(clFrom) 
						if(clFrom=='AGENCY')
						{
							//document.getElementById('rfvUSER_EMAIL').setAttribute('enabled',false);
							//document.getElementById('rfvUSER_EMAIL').style.display="none";
							//document.getElementById('rfvUSER_EMAIL').setAttribute('isValid',false);				
							//document.getElementById('revEmail').setAttribute('enabled',false);
							//document.getElementById('revEmail').setAttribute('isValid',false);
							//document.getElementById('spnUSER_EMAIL').style.display="none";
							
							document.getElementById('capDefault_Hierarchy').style.display="none";			
							document.getElementById('cmbDefault_Hierarchy').style.display="none";								
							document.getElementById('trDefault_Hierarchy').style.display="none";												
							document.getElementById('rfvDefault_Hierarchy').setAttribute('enabled',false);
							document.getElementById('rfvDefault_Hierarchy').setAttribute('isValid',false);
							document.getElementById('spnDefault_Hierarchy').style.display="none";			
							document.getElementById('txtUSER_EMAIL').className="";							
						}
						else
						{			
							//document.getElementById('rfvUSER_EMAIL').enabled=true;
							//document.getElementById('rfvUSER_EMAIL').setAttribute('isValid',true);
					
		  					//document.getElementById('revEmail').setAttribute('enabled',true);
							//document.getElementById('revEmail').setAttribute('isValid',true);
							//document.getElementById('spnUSER_EMAIL').style.display="inline";
							if(document.getElementById('txtUSER_EMAIL').value=="")
							document.getElementById('txtUSER_EMAIL').className="MandatoryControl";
							
							
                            var strSystemID = '<%=GetSystemId() %>';                            
                            if(strSystemID=="I001" || strSystemID=="IUAT")
                            {
							
							document.getElementById('capDefault_Hierarchy').style.display="none";			
							document.getElementById('cmbDefault_Hierarchy').style.display="none";			
							document.getElementById('trDefault_Hierarchy').style.display="none";
							document.getElementById('rfvDefault_Hierarchy').setAttribute('enabled',false);
							document.getElementById('rfvDefault_Hierarchy').setAttribute('isValid',false);
							document.getElementById('spnDefault_Hierarchy').style.display="none";		
                            }	
                            else
                            {
                            document.getElementById('capDefault_Hierarchy').style.display="inline";			
							document.getElementById('cmbDefault_Hierarchy').style.display="inline";			
							document.getElementById('trDefault_Hierarchy').style.display="inline";
							document.getElementById('rfvDefault_Hierarchy').setAttribute('enabled',true);
							document.getElementById('rfvDefault_Hierarchy').setAttribute('isValid',true);
							document.getElementById('spnDefault_Hierarchy').style.display="inline";	
                            }			
							
						}		
						//ApplyColor(); 	
						// ChangeColor();	
					}
					 
					function copyAgencyAddress()
					{
		 				if(document.getElementById('txtUSER_ADD1').value=="")
						document.getElementById('txtUSER_ADD1').value=document.getElementById('hidAGENCY_ADD1').value;
						if(document.getElementById('txtUSER_ADD2').value=="")
						document.getElementById('txtUSER_ADD2').value=document.getElementById('hidAGENCY_ADD2').value;
						if(document.getElementById('txtUSER_CITY').value=="")
						document.getElementById('txtUSER_CITY').value=document.getElementById('hidAGENCY_CITY').value;
						if(document.getElementById('cmbUSER_COUNTRY').value=="")
						document.getElementById('cmbUSER_COUNTRY').options.selectedIndex=parseInt(document.getElementById('hidAGENCY_COUNTRY').value)-1;
                        if('<%=strCarrierSIN%>' == 'S001' || '<%=strCarrierSIN%>' == 'SUAT')
                        document.getElementById('cmbUSER_STATE').options.selectedIndex = -1;
                        else
                        {				
						if(document.getElementById('cmbUSER_STATE').value=="")
						document.getElementById('cmbUSER_STATE').options.selectedIndex=parseInt(document.getElementById('hidAGENCY_STATE').value)-1;						
                        }
						
						if(document.getElementById('txtUSER_FAX').value=="")
						document.getElementById('txtUSER_FAX').value=document.getElementById('hidAGENCY_FAX').value;						
											
						/*for(i=0;i<document.getElementById('cmbUSER_STATE').options.length;i++)
						{
						
							if(document.getElementById('cmbUSER_STATE').options[i].value==document.getElementById('hidAGENCY_STATE').value)
							{
								document.getElementById('cmbUSER_STATE').options.selectedIndex=;					
								break;
							}					
						}*/	
						if(document.getElementById('txtUSER_ZIP').value=="")		
						document.getElementById('txtUSER_ZIP').value=document.getElementById('hidAGENCY_ZIP').value;
						
						/*for(i=0;i<document.getElementById('cmbUSER_COUNTRY').options.length;i++)
						{
							if(document.getElementById('cmbUSER_COUNTRY').options[i].value==document.getElementById('hidAGENCY_COUNTRY').value)
							{
								document.getElementById('cmbUSER_COUNTRY').options[i].selectedIndex=true;					
								break;
						    }
						}*/
						if(document.getElementById('txtUSER_PHONE').value=="")		
						document.getElementById('txtUSER_PHONE').value=document.getElementById('hidAGENCY_PHONE').value;
						ChangeColor();	

                        if (document.getElementById('txtUSER_ADD1').value != "") 
                        {
				            document.getElementById('rfvUSER_ADD1').setAttribute('enabled', false);
				            document.getElementById('rfvUSER_ADD1').style.display = 'none';
				        }			
                        if (document.getElementById('txtUSER_CITY').value != "") 
                        {
				            document.getElementById('rfvUSER_CITY').setAttribute('enabled', false);
				            document.getElementById('rfvUSER_CITY').style.display = 'none';
				        }				
                        if (document.getElementById('txtUSER_ZIP').value != "") 
                        {
				            document.getElementById('rfvUSER_ZIP').setAttribute('enabled', false);
				            document.getElementById('rfvUSER_ZIP').style.display = 'none';
				        }			
                        if (document.getElementById('cmbUSER_STATE').value != "") 
                        {
				            document.getElementById('rfvUSER_STATE').setAttribute('enabled', false);
				            document.getElementById('rfvUSER_STATE').style.display = 'none';
				        }					
						return false;
					}

                    function EnableDisableRfv()
                     {
			            if (document.getElementById('txtUSER_ADD1').value == "") 
                        {
				            document.getElementById('rfvUSER_ADD1').setAttribute('enabled', true);
				            document.getElementById('rfvUSER_ADD1').style.display = 'inline';
				        }			
//                        if (document.getElementById('txtUSER_CITY').value == "") //Commented by Ruchika for TFS # 3120
//                        {
//				            document.getElementById('rfvUSER_CITY').setAttribute('enabled', true);
//				            document.getElementById('rfvUSER_CITY').style.display = 'inline';
//				        }		
//                        if (document.getElementById('txtUSER_ZIP').value == "") 
//                        {
//				            document.getElementById('rfvUSER_ZIP').setAttribute('enabled', true);
//				            document.getElementById('rfvUSER_ZIP').style.display = 'inline';
//				        }		//Commented till here
//                        if (document.getElementById('cmbUSER_STATE').value == "") 
//                        {
//				            document.getElementById('rfvUSER_STATE').setAttribute('enabled', true);
//				            document.getElementById('rfvUSER_STATE').style.display = 'inline';
//				        }		
			        }	
					
					var jsaAppDtFormat = "<%=aAppDtFormat  %>";
					// DOB can't be future date
					function ChkDOB(objSource , objArgs)
					{
						var dob=document.MNT_USER_LIST.txtDATE_OF_BIRTH.value;
						objArgs.IsValid = DateComparer("<%=System.DateTime.Now%>",dob,jsaAppDtFormat);			
					}
					
					function displayLoginData()
					{
					if (document.getElementById('cmbLIC_BRICS_USER').selectedIndex == 0) // No
						{
							document.getElementById('trLoginPwd').style.display="none";
							document.getElementById('trConfirmPwd').style.display="none";
							document.getElementById('trSupervisor').style.display="none";
							document.getElementById("trUser").style.display="none";
							document.getElementById('trTimeZone').style.display="none";
							document.getElementById('trPasswordChange').style.display="none";//Added by Sibin for Itrack Issue 4994 on 10 Dec 08
							//added by swarup
							//document.getElementById('txtUSER_LOGIN_ID').value="";
							//document.getElementById('txtUSER_PWD').value="";
							//document.getElementById('txtUSER_CONFIRM_PWD').value="";
							//document.getElementById('cmbUSER_MGR_ID').options.selectedIndex = -1;
							//document.getElementById('chkUSER_SPR').checked=false;
							//document.getElementById('cmbUSER_TYPE_ID').options.selectedIndex = -1;
							//document.getElementById('cmbUSER_TIME_ZONE').options.selectedIndex = -1; 
							EnableValidator("cvPassword",false);
							EnableValidator("rfvUSER_CONFIRM_PWD",false);
							EnableValidator("rfvUSER_LOGIN_ID",false);
							EnableValidator("rfvUSER_PWD",false);
							EnableValidator("rfvUSER_TYPE_ID",false);				
							EnableValidator("rfvUSER_TIME_ZONE",false);				
						}
						else
						{
							document.getElementById('trLoginPwd').style.display="inline";
							document.getElementById('trConfirmPwd').style.display="inline";
							document.getElementById('trSupervisor').style.display="inline";
							document.getElementById("trUser").style.display="inline";
							document.getElementById('trTimeZone').style.display="inline";
							document.getElementById('trPasswordChange').style.display="inline";//Added by Sibin for Itrack Issue 4994 on 10 Dec 08
							EnableValidator("cvPassword",true);
							EnableValidator("rfvUSER_CONFIRM_PWD",true);
							EnableValidator("rfvUSER_LOGIN_ID",true);
							EnableValidator("rfvUSER_PWD",true);
							EnableValidator("rfvUSER_TYPE_ID",true);
							//EnableValidator("rfvUSER_TIME_ZONE",true);Commented by Amit mishra as per singapore requirement for tfs bug #836 on 09 Nov,2011
						    document.getElementById("rfvUSER_TIME_ZONE").setAttribute('Enabled',true);
                        }
						ChangeColor();
					}
					// Added by Swarup For checking zip code for LOB: Start
				function GetZipForState_OLD()
				{		    
					GlobalError=true;
					if(document.getElementById('cmbUSER_STATE').value==14 ||document.getElementById('cmbUSER_STATE').value==22||document.getElementById('cmbUSER_STATE').value==49)
					{ 
						if(document.getElementById('txtUSER_ZIP').value!="")
						{
							var intStateID = document.getElementById('cmbUSER_STATE').options[document.getElementById('cmbUSER_STATE').options.selectedIndex].value;
							var strZipID = document.getElementById('txtUSER_ZIP').value;	
							var co=myTSMain1.createCallOptions();
							co.funcName = "FetchZipForState";
							co.async = false;
							co.SOAPHeader= new Object();
							var oResult = myTSMain1.FetchZip.callService(co,intStateID,strZipID);
							handleResult(oResult);	
							if(GlobalError)
							{
								return false;
							}
							else
							{
								return true;
							}
						}	
						return false;
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
							}
							else
							{
								GlobalError=true;
							}
						}
						else
						{
							GlobalError=true;		
						}
						
					}
													
			      function ChkResult(objSource , objArgs)
					{
					    var strState = document.getElementById('hidState').value;	
						objArgs.IsValid = true;
						if(objArgs.IsValid == true)
						{
							objArgs.IsValid = GetZipForState();
							if (objArgs.IsValid == false)
							    document.getElementById('csvUSER_ZIP').innerHTML = strState;  //"The zip code does not belong to the state";				
						}
						return;
						if(GlobalError==true)
						{							
							Page_IsValid = false;
							objArgs.IsValid = false;
						}
						else
						{   						    
							objArgs.IsValid = true;				
						}
						if(document.getElementById("btnSave"))
						document.getElementById("btnSave").click();
						
					}
			////////////////////////////////////AJAX CALLS FOR ZIP///////////////////////////
		    function GetZipForState()
			{	
			  GlobalError=true;
			  if(document.getElementById('cmbUSER_STATE').value==14 ||document.getElementById('cmbUSER_STATE').value==22||document.getElementById('cmbUSER_STATE').value==49)
			  { 
				if(document.getElementById('txtUSER_ZIP').value!="")
			  	{	
					var intStateID = document.getElementById('cmbUSER_STATE').options[document.getElementById('cmbUSER_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtUSER_ZIP').value;	
					var result = AddUser.AjaxFetchZipForState(intStateID,strZipID);
					return AjaxCallFunction_CallBack(result);
									
				}			
				return false;
		      }
			else 
				return true;				
		   }
		
		function AjaxCallFunction_CallBack(response)
		{		
			if(document.getElementById('cmbUSER_STATE').value==14 ||document.getElementById('cmbUSER_STATE').value==22||document.getElementById('cmbUSER_STATE').value==49)
			{ 
				if(document.getElementById('txtUSER_ZIP').value!="")
				{
					
					handleResult(response);
					if(GlobalError)
					{						
						return false;
					}
					else
					{
						return true;
					}
				}	
				return false;
			}
			else 
				return true;		
		}
		/////EMP ZIP AJAX////////////////						

			</script>
			<script language="javascript" type="text/javascript" id="clientEventHandlersJS">			

			function hidAGENCY_ADD1_onbeforeeditfocus() {

			}
			function cmbUSER_TYPE_ID_Change()
			{
			    //alert();
				if(document.getElementById("cmbUSER_TYPE_ID").selectedIndex!=-1 && document.getElementById("cmbUSER_TYPE_ID").options[document.getElementById("cmbUSER_TYPE_ID").selectedIndex].value=="46")
				{
					EnableValidator("rfvADJUSTER_CODE",true);
					document.getElementById("txtADJUSTER_CODE").style.display="inline";
					document.getElementById("capADJUSTER_CODE").style.display="inline";
					document.getElementById("spnADJUSTER_CODE").style.display="inline";
				}
				else
				{
					EnableValidator("rfvADJUSTER_CODE",false);
					document.getElementById("txtADJUSTER_CODE").style.display="none";
					document.getElementById("capADJUSTER_CODE").style.display="none";
					document.getElementById("spnADJUSTER_CODE").style.display="none";
				}
				ChangeColor();
				return false;
			}
			function Focus() {
			
			    try
			    {
				    document.getElementById('txtUSER_TITLE').focus();
				}
				catch(err)
				{ }
			}

//			function SSN_change(calledfor)
//			{
//				//document.getElementById('txtSSN_NO').value = document.getElementById('txtSSN_NO_HID').value;
//				document.getElementById('txtSSN_NO').value = '';
//				document.getElementById('txtSSN_NO').style.display = 'inline';
//				//document.getElementById("rfvSSN_NO").style.display='none';
//				document.getElementById("rfvSSN_NO").setAttribute('enabled',true);
//				//document.getElementById("revSSN_NO").style.display='none';
//				document.getElementById("revSSN_NO").setAttribute('enabled',true);
//				//if(calledfor == 1)
//				if(document.getElementById("btnSSN_NO"))
//				{
//					if(document.getElementById("btnSSN_NO").value == 'Edit')
//						document.getElementById("btnSSN_NO").value = 'Cancel';
//					else if(document.getElementById("btnSSN_NO").value == 'Cancel')
//						SSN_hide();
//					else
//						document.getElementById("btnSSN_NO").style.display = 'none';
//				}
//					
//				//document.getElementById('txtSSN_NO_HID').style.display = 'none';			
//			}
			
//			function SSN_hide()
//			{
//				if(document.getElementById("btnSSN_NO"))
//				document.getElementById("btnSSN_NO").style.display = 'inline';
//				//document.getElementById('txtSSN_NO').value = document.getElementById('txtSSN_NO_HID').value;
//				document.getElementById('txtSSN_NO').value = '';
//				document.getElementById('txtSSN_NO').style.display = 'none';
//				document.getElementById("rfvSSN_NO").style.display='none';
//				document.getElementById("rfvSSN_NO").setAttribute('enabled',false);
//				document.getElementById("revSSN_NO").style.display='none';
//				document.getElementById("revSSN_NO").setAttribute('enabled',false);
//				if(document.getElementById("btnSSN_NO"))
//				document.getElementById("btnSSN_NO").value = 'Edit';
//				//document.getElementById('txtSSN_NO_HID').style.display = 'none';			
//			}
		
		//Added by Sibin for Itrack Issue 5212 to remove USER_PWD and USER_CONFIRM_PWD from View Source - Done on 7 Jan 08
		function Password_Change()
		{
		  if(document.getElementById("hidPWD").value != document.getElementById("txtUSER_PWD").value)
		  {
		    document.getElementById("hidPWD").value='';
		    document.getElementById("hidCONFIRM_PWD").value='';
		    //Added by Sibin on 21 Jan 09 for Itrack Issue 4994
		    document.getElementById("revUSER_PWD").setAttribute('enabled',true);
		  }
		  else
		  {
			 document.getElementById("hidPWD").value= document.getElementById("txtUSER_PWD").value;
			 document.getElementById("hidCONFIRM_PWD").value= document.getElementById("txtUSER_CONFIRM_PWD").value;
			 //Added by Sibin on 21 Jan 09 for Itrack Issue 4994
			 document.getElementById("revUSER_PWD").setAttribute('enabled',false);
		  }
		}
		
	function RemoveSingleQuote(objEvent) 
	{
	    objEvent = (objEvent) ? objEvent : event;
	    var charCode = (objEvent.charCode) ? objEvent.charCode : ((objEvent.keyCode) ? objEvent.keyCode : ((objEvent.which) ? objEvent.which : 0));
	  
	    if (charCode == '39') {
	       return false;
	    }
	    return true;
	}
	
	function replaceQuote(obj)
	{
		chr = obj.value;
		if(chr.indexOf("'") != -1)
		{	
			obj.value = ReplaceAll(chr,"'",'')
		}
        }


        function zipcodeval() {

            if (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '6') {
                document.getElementById('revUSER_ZIP').setAttribute('enabled', false);
            }
        }

        function zipcodeval1() {

            if (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '5') {
                document.getElementById('revUSER_ZIP').setAttribute('enabled', true);
            }
        }

        function FormatZipCode(vr) { //Changes done by Aditya for TFS BUG # 1832

            var vr = new String(vr.toString());
            num = vr.length;
            if (vr != "" && (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '5') || (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '6') || (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '1')) {
                //|| (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {

                vr = vr.replace(/[-]/g, "");
                num = vr.length;
                if (iLangID == '2') //If the language id is 2 then
                {
                    if (num == 8 && (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '5')) {
                        //|| (num == 8 && (document.getElementById('cmbMAIL_1099_COUNTRY').options[document.getElementById('cmbMAIL_1099_COUNTRY').options.selectedIndex].value == '5'))) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
                        document.getElementById('revUSER_ZIP').setAttribute('enabled', false);
                        //document.getElementById('revMAIL_1099_ZIP').setAttribute('enabled', false);

                    }
                }
                if (iLangID.toString() == "1") //If the language id is 1 then
                {
                    if (num == '9' && (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '1') || (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '5') || (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '6')) {
                        vr = vr.substr(0, 5) + '-' + vr.substr(5, 4);
                        document.getElementById('revUSER_ZIP').setAttribute('enabled', false);
                    }
                    document.getElementById('revUSER_ZIP').setAttribute('enabled', true);
                }
                
            }

            return vr;
        }            
        

//-------------------------------------------------------------------------------------------------------------
//*****************************Added by SANTOSH KUMAR GAUTAM for Itrack Issue 1028 on 29 MARCH 2011******************

function fillstateFromCountry() {

    GlobalError = true;
    //var CmbState=document.getElementById('cmbCUSTOMER_STATE');
    var CountryID = document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').selectedIndex].value;
    //var oResult='';
    AddUser.AjaxFillState(CountryID, fillState);


    if (GlobalError) {
        return false;
    }
    else {
        return true;
    }
}
function setStateID() {

    var CmbState = document.getElementById('cmbUSER_STATE');
    if (CmbState == null)
        return;
    if (CmbState.selectedIndex != -1) {
        document.getElementById('hidSTATE_ID').value = CmbState.options[CmbState.selectedIndex].value;

    }

}
function fillState(Result) {

    if (Result.error) {
        var xfaultcode = Result.errorDetail.code;
        var xfaultstring = Result.errorDetail.string;
        var xfaultsoap = Result.errorDetail.raw;
    }
    else {
        var statesList = document.getElementById("cmbUSER_STATE");
        statesList.options.length = 0;
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
        // setStateID()';
        //document.getElementById("cmbCUSTOMER_STATE").value = document.getElementById("hidSTATE_ID_OLD").value;



    }

    return false;
}

    function allnumeric(objSource, objArgs) {//debugger
    var numbers = /\d{1,2}\/\d\d?\/\d{4}/;
    var inputxt = document.getElementById('txtREG_ID_ISSUE_DATE').value;
    if (document.getElementById(objSource.controltovalidate).value.match(numbers)) {
        document.getElementById('revREGIONAL_ID_ISSUE_DATE').setAttribute('enabled',true);
        document.getElementById('cpv2REG_ID_ISSUE_DATE_FUTURE').setAttribute('enabled',true);
        document.getElementById('cpvREG_ID_ISSUE_DATE').setAttribute('enabled',true);

        objArgs.IsValid = true;
    }
    else {
        document.getElementById('revREGIONAL_ID_ISSUE_DATE').setAttribute('enabled',false);
        document.getElementById('revREGIONAL_ID_ISSUE_DATE').style.display = 'none';
        document.getElementById('cpv2REG_ID_ISSUE_DATE_FUTURE').setAttribute('enabled',false);
        document.getElementById('cpv2REG_ID_ISSUE_DATE_FUTURE').style.display = 'none';
        document.getElementById('cpvREG_ID_ISSUE_DATE').setAttribute('enabled',false);
        document.getElementById('cpvREG_ID_ISSUE_DATE').style.display = 'none';
        document.getElementById('csvREG_ID_ISSUE_DATE').setAttribute('enabled',true);
        document.getElementById('csvREG_ID_ISSUE_DATE').style.display = 'inline';
        objArgs.IsValid = false;
    }
} 



	</script> 

     <script language="javascript" type="text/javascript">// changed by praveer for itrack no 1553/TFS# 626 strating from here

        
         $(document).ready(function () {

             $("#txtUSER_ZIP").change(function () {
                 if (trim($('#txtUSER_ZIP').val()) != '') {
                     var ZIPCODE = $("#txtUSER_ZIP").val();
                     $("#hidZipCodeCalledfor").val("USER_ZIP");
                     var COUNTRYID = $("#cmbUSER_COUNTRY").val(); //"5";
                     ZIPCODE = ZIPCODE.replace(/[-]/g, "");
                     PageMethod("GetUserAddressDetailsUsingZipeCode", ["ZIPCODE", ZIPCODE, "COUNTRYID", COUNTRYID], AjaxSucceeded, AjaxFailed); //With parameters

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
                 error: errorFn
             });

         }
         function AjaxSucceeded(result) {
           
             var Addresses = result.d;
             var strSystemId='<%=GetSystemId().ToUpper().Trim()%>';
            
             var Addresse = Addresses.split('^');
             if ($("#hidZipCodeCalledfor").val() == "USER_ZIP") {
                 if (result.d != "" && Addresse[1] != 'undefined') {
                     $("#cmbUSER_STATE").val(Addresse[1]);
                     $("#txtUSER_ADD1").val(Addresse[3] + ' ' + Addresse[4]);
                     $("#txtUSER_ADD2").val(Addresse[4]);
                     //  $("#txtDISTRICT").val(Addresse[5]);
                     $("#txtUSER_CITY").val(Addresse[6]);
                     var ZipeCode = $("#txtUSER_ZIP").val();
                     ZipeCode = ZipeCode.replace(/[-]/g, "");

                     if (strSystemId != 'S001' && strSystemId != 'SUAT') {
                         if (ZipeCode == "00000000")
                             alert($("#hidZipeCodeVerificationMsg").val());
                     }
                 }
                 else if (document.getElementById('cmbUSER_COUNTRY').options[document.getElementById('cmbUSER_COUNTRY').options.selectedIndex].value == '5') {

                     if (strSystemId != 'S001' && strSystemId != ' SUAT') {
                         alert($("#hidZipeCodeVerificationMsg").val());

                     } 
                 }
             }
         }
         function AjaxFailed(result) {

             //alert(result.d);
         } // changed by praveer for itrack no 1553/TFS# 626 end here .
         
                    </script>
	</head>
	<body style="margin-left:0; margin-top:0" onload="populateXML();ApplyColor();setTab();EnableDisableSubCode();EnableDisable();displayLoginData();cmbUSER_TYPE_ID_Change();setTimeout('Focus()',500);">
		<form id="MNT_USER_LIST" method="post" runat="server">			
			<p><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></p>
			<table  cellPadding="0" width="100%" border="0">
				<tbody>	
                
					<tr>
						<td class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
					</tr>
					<tr>
						<TD class="midcolora" style="WIDTH: 191px; HEIGHT: 25px" width="191"><asp:label id="capUSER_TYPE" runat="server">User Type Status</asp:label></TD>
						<TD class="midcolora" style="HEIGHT: 25px" width="32%"><asp:label id="lblStatus" CssClass="LabelFont" Runat="server"></asp:label></TD>
						<TD class="midcolora" style="HEIGHT: 25px" width="18%"><asp:label id="capUSER_TITLE" runat="server">User Title</asp:label></TD>
						<TD class="midcolora" style="HEIGHT: 25px" width="32%"><asp:textbox id="txtUSER_TITLE" runat="server" size="35" maxlength="35"></asp:textbox></TD>
					</tr>
					<tr id="rd1">
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capSUB_CODE" runat="server">Sub Code</asp:label><span class="mandatory" id="spnSUB_CODE" runat="server">*</span></TD>
						<TD class="midcolora" width="32%">
						<!-- Changed SUB CODE size and maxlength to 3 from 2 for Itrack 6128 on 17-Jul-09, Charles -->
						<asp:textbox id="txtSUB_CODE" runat="server" size="3" maxlength="3"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvSUB_CODE" runat="server" ControlToValidate="txtSUB_CODE" ErrorMessage="Sub Code can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revSUB_CODE" runat="server" ControlToValidate="txtSUB_CODE" Display="Dynamic"></asp:regularexpressionvalidator></TD>
						<td class="midcolora" colSpan="2"></td>
					</tr>
					<tr id="ttrUSER_FNAME" runat="server">
						<TD class="midcolora" id="ttdUSER_FNAME" runat="server" style="WIDTH: 191px" width="191"><asp:label id="capUSER_FNAME" runat="server">First Name</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora"  width="32%"><asp:textbox id="txtUSER_FNAME" runat="server" size="35" maxlength="65"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvUSER_FNAME" runat="server" ControlToValidate="txtUSER_FNAME" ErrorMessage="USER_FNAME can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revUSER_FNAME" runat="server" ControlToValidate="txtUSER_FNAME" Display="Dynamic"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capUSER_LNAME" runat="server">Last Name</asp:label><%--<span class="mandatory">*</span>--%></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_LNAME" runat="server" size="20" maxlength="15"></asp:textbox><BR>
							<%--<asp:requiredfieldvalidator id="rfvUSER_LNAME" runat="server" ControlToValidate="txtUSER_LNAME" ErrorMessage="USER_LNAME can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator>--%><asp:regularexpressionvalidator id="revLastName" runat="server" ControlToValidate="txtUSER_LNAME" Display="Dynamic"></asp:regularexpressionvalidator></TD>
					</tr>
					<!-- USER OTHER INFO :S -->
					<tr>
						<%--<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capSSN_NO" runat="server"></asp:label>
							<!--Added By Swarup Kumar Pal(04-12-2006)-->
							<span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:label id="capSSN_NO_HID" runat="server" size="14" maxlength="11"></asp:label><input class="clsButton" id="btnSSN_NO" text="Edit" onclick="SSN_change(0);" type="button"></input><asp:textbox id="txtSSN_NO" runat="server" size="14" maxlength="11" AutoComlete = "Off"></asp:textbox><BR>
							<!--Added By Swarup Kumar Pal(04-12-2006)			-->
							<asp:requiredfieldvalidator id="rfvSSN_NO" runat="server" ControlToValidate="txtSSN_NO" ErrorMessage="Please enter Social Security No."
								Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revSSN_NO" runat="server" ControlToValidate="txtSSN_NO" Display="Dynamic"></asp:regularexpressionvalidator></TD>--%>
						<TD class="midcolora" width="18%"></TD>
						<TD class="midcolora" width="32%"></TD>		
								
						<TD class="midcolora" width="18%"><asp:label id="capDATE_OF_BIRTH" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtDATE_OF_BIRTH" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkDATE_OF_BIRTH" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgDATE_OF_BIRTH" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><BR>
							
							<asp:regularexpressionvalidator id="revDATE_OF_BIRTH" runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"></asp:regularexpressionvalidator><br>
							<asp:customvalidator id="csvDATE_OF_BIRTH" Runat="server" ControlToValidate="txtDATE_OF_BIRTH" Display="Dynamic"
								ClientValidationFunction="ChkDOB"></asp:customvalidator></TD>
					</tr>
					<!-- License No. , License Expiry Date -->
					<tr>
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capDRIVER_LIC_NO" runat="server">License Number</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtDRIVER_LIC_NO" runat="server" size="30" maxlength="30" AutoComlete = "Off"></asp:textbox></TD>
						<TD class="midcolora" width="18%"><asp:label id="capDATE_EXPIRY" runat="server">Expiry Date</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtDATE_EXPIRY" runat="server" size="12" maxlength="10"></asp:textbox><asp:hyperlink id="hlkDATE_EXPIRY" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgDATE_EXPIRY" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><br>
							<asp:regularexpressionvalidator id="revDATE_EXPIRY" runat="server" ControlToValidate="txtDATE_EXPIRY" ErrorMessage="RegularExpressionValidator"
								Display="Dynamic"></asp:regularexpressionvalidator></TD>
					</tr>
					<!-- License Status -->
					<tr>
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capLICENSE_STATUS" runat="server">License Status </asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbLICENSE_STATUS" onfocus="SelectComboIndex('cmbLICENSE_STATUS')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbLICENSE_STATUS')"></A></TD>
						
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capPINK_SLIP_NOTIFY" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:checkbox id="chkPINK_SLIP_NOTIFY" runat="server"></asp:checkbox></TD>
					</tr>
					
					<tr id="ttrNON_RESI_LICENSE_NO" runat="server">
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capNON_RESI_LICENSE_NO" runat="server">NON_RESI_LICENSE_#</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtNON_RESI_LICENSE_NO" runat="server" size="30" maxlength="30"></asp:textbox></TD>
						<%--Added by Sibin for Itrack Issue 4173 on 15 Jan 09--%>
						<TD class="midcolora" width="18%"><asp:label id="capNON_RESI_LICENSE_EXP_DATE" runat="server">1st Non Resident Expiry Date</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtNON_RESI_LICENSE_EXP_DATE" runat="server" size="12" maxlength="10"></asp:textbox>
						<asp:hyperlink id="hlkNON_RESI_LICENSE_EXP_DATE" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgNON_RESI_LICENSE_EXP_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><br>
							<asp:regularexpressionvalidator id="revNON_RESI_LICENSE_EXP_DATE" runat="server" ControlToValidate="txtNON_RESI_LICENSE_EXP_DATE" ErrorMessage="RegularExpressionValidator"
								Display="Dynamic"></asp:regularexpressionvalidator></TD>
					</tr>

					
                     
                    <tr id="ttrNON_RESI_LICENSE_STATE" runat="server">
					  <TD class="midcolora" width="18%"><asp:label id="capNON_RESI_LICENSE_STATE" runat="server">LICENSE_STATE</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbNON_RESI_LICENSE_STATE" onfocus="SelectComboIndex('cmbNON_RESI_LICENSE_STATE')"
								runat="server"></asp:dropdownlist></TD>
					<%--Added by Sibin for Itrack Issue 4173 on 15 Jan 09--%>			
					<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capNON_RESI_LICENSE_STATUS" runat="server">1st Non Resident License Status</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbNON_RESI_LICENSE_STATUS" onfocus="SelectComboIndex('cmbNON_RESI_LICENSE_STATUS')" runat="server"></asp:dropdownlist>
						  <A class="calcolora" href="javascript:showPageLookupLayer('cmbNON_RESI_LICENSE_STATUS')"></A>
						</TD>
					</tr>


                   
					<tr  id="ttrNON_RESI_LICENSE_NO2" runat="server" >
						<%-- Added By Manoj Rathore(11-06-2006)	%>	--%>	
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capNON_RESI_LICENSE_NO2" runat="server">NON_RESI_LICENSE_State</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtNON_RESI_LICENSE_NO2" runat="server" size="30" MaxLength="30"></asp:textbox></TD>						
						
						<%--Added by Sibin for Itrack Issue 4173 on 15 Jan 09--%>
						<TD class="midcolora" width="18%"><asp:label id="capNON_RESI_LICENSE_EXP_DATE2" runat="server">Expiry Date</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtNON_RESI_LICENSE_EXP_DATE2" runat="server" size="12" maxlength="10"></asp:textbox>
						<asp:hyperlink id="hlkNON_RESI_LICENSE_EXP_DATE2" runat="server" CssClass="HotSpot">
								<ASP:IMAGE id="imgNON_RESI_LICENSE_EXP_DATE2" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></ASP:IMAGE>
							</asp:hyperlink><br>
							<asp:regularexpressionvalidator id="revNON_RESI_LICENSE_EXP_DATE2" runat="server" ControlToValidate="txtNON_RESI_LICENSE_EXP_DATE2" ErrorMessage="RegularExpressionValidator"
								Display="Dynamic"></asp:regularexpressionvalidator></TD>
					</tr>


                   
					<tr id="ttrNON_RESI_LICENSE_STATE2" runat="server">
						<TD class="midcolora" width="18%"><asp:label id="capNON_RESI_LICENSE_STATE2" runat="server"></asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbNON_RESI_LICENSE_STATE2" onfocus="SelectComboIndex('cmbNON_RESI_LICENSE_STATE2')"
								runat="server"></asp:dropdownlist></TD>
						
						<%--Added by Sibin for Itrack Issue 4173 on 15 Jan 09--%>		
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capNON_RESI_LICENSE_STATUS2" runat="server">License Status </asp:label></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbNON_RESI_LICENSE_STATUS2" onfocus="SelectComboIndex('cmbNON_RESI_LICENSE_STATUS2')" runat="server"></asp:dropdownlist>
						  <A class="calcolora" href="javascript:showPageLookupLayer('cmbNON_RESI_LICENSE_STATUS2')"></A>
						</TD>
					</tr>

					<!-- USER OTHER INFO  -->
				<%-- if(strCalledFrom=="AGENCY")
				{--%>
				
					<tr id="ttrCopyAddress" runat="server">
						<td class="midcolora" style="WIDTH: 191px" colSpan="1">Copy Agency Address
						</td>
						<td class="midcolora" colSpan="3"><cmsb:cmsbutton class="clsButton" id="btnCopyAddress" runat="server" Text=""
								causesValidation="false"></cmsb:cmsbutton></td>
					</tr>
					
			<%--	}--%>
				
					<tr>
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_ADD1" runat="server">Address1</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_ADD1" runat="server" size="35" maxlength="70"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvUSER_ADD1" runat="server" ControlToValidate="txtUSER_ADD1" ErrorMessage="USER_ADD1 can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capUSER_ADD2" runat="server">Address2</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_ADD2" runat="server" size="35" maxlength="70"></asp:textbox></TD>
					</tr>
					<tr>
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_CITY" runat="server">City</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_CITY" runat="server" size="35" maxlength="80"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvUSER_CITY" runat="server" ControlToValidate="txtUSER_CITY" ErrorMessage="USER_CITY can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capUSER_COUNTRY" Runat="server">Country</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSER_COUNTRY" onfocus="SelectComboIndex('cmbUSER_COUNTRY')" runat="server" onchange="javascript:fillstateFromCountry();" >
								<ASP:LISTITEM Value="0"></ASP:LISTITEM>
							</asp:dropdownlist><BR>
							<asp:requiredfieldvalidator id="rfvUSER_COUNTRY" runat="server" ControlToValidate="cmbUSER_COUNTRY" ErrorMessage="USER_COUNTRY can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
					</tr>
					<tr>
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_STATE" runat="server">State</asp:label><span id="spnUSER_STATE" runat="server" class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSER_STATE" onfocus="SelectComboIndex('cmbUSER_STATE')" runat="server">
								<ASP:LISTITEM Value="0"></ASP:LISTITEM>
							</asp:dropdownlist><BR>
							<asp:requiredfieldvalidator id="rfvUSER_STATE" runat="server" ControlToValidate="cmbUSER_STATE" ErrorMessage="USER_STATE can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capUSER_ZIP" runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
                        <%--changed by praveer for itrack no 1553/TFS# 626 --%>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_ZIP" runat="server" size="13" maxlength="9" OnBlur="this.value=FormatZipCode(this.value);ValidatorOnChange();GetZipForState();zipcodeval();zipcodeval1();"></asp:textbox>
							<asp:hyperlink id="hlkUSER_ZIP" runat="server" CssClass="HotSpot">
								<asp:image id="imgUSER_ZIP" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
							</asp:hyperlink>
							<BR>
							<asp:customvalidator id="csvUSER_ZIP" Runat="server" ClientValidationFunction="ChkResult" ErrorMessage=" "
								Display="Dynamic" ControlToValidate="txtUSER_ZIP"></asp:customvalidator>
							<asp:requiredfieldvalidator id="rfvUSER_ZIP" runat="server" ControlToValidate="txtUSER_ZIP" ErrorMessage="USER_ZIP can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revUSER_ZIP" runat="server" ControlToValidate="txtUSER_ZIP" Display="Dynamic"></asp:regularexpressionvalidator></TD>
					</tr>
					<tr>
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_INITIALS" runat="server">User Initials</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_INITIALS" runat="server" size="5" maxlength="3"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvUSER_INITIALS" runat="server" ControlToValidate="txtUSER_INITIALS" ErrorMessage="USER_INITIALS can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revUserInitials" runat="server" ControlToValidate="txtUSER_INITIALS" Display="Dynamic"
								ValidationExpression="[a-zA-Z]*"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capUSER_PHONE" runat="server">Phone</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_PHONE" runat="server"  onblur="FormatBrazilPhone()" size="19" maxlength="15"></asp:textbox><br>
							<asp:regularexpressionvalidator id="revUSER_PHONE" runat="server" ControlToValidate="txtUSER_PHONE" Display="Dynamic"></asp:regularexpressionvalidator></TD>
					</tr>
					<tr>
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_EXT" runat="server">Extn</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_EXT" runat="server" size="5" maxlength="4" ReadOnly="True"></asp:textbox><br>
							<asp:regularexpressionvalidator id="revExtn" runat="server" ControlToValidate="txtUSER_EXT" Display="Dynamic"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capUSER_FAX" runat="server">Fax</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_FAX" runat="server" onblur="FormatBrazilPhone();ValidatorOnChange();" size="19" maxlength="15"></asp:textbox><br>
							<asp:regularexpressionvalidator id="revUSER_FAX" runat="server" ControlToValidate="txtUSER_FAX" Display="Dynamic"></asp:regularexpressionvalidator></TD>
					</tr>
					<tr>
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_MOBILE" runat="server">Mobile</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_MOBILE" runat="server" size="19" onblur="FormatBrazilPhone();ValidatorOnChange()" maxlength="15"></asp:textbox><br>
							<asp:regularexpressionvalidator id="revUSER_MOBILE" runat="server" ControlToValidate="txtUSER_MOBILE" Display="Dynamic"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capUSER_EMAIL" runat="server">Email</asp:label><span class="mandatory" id="spnUSER_EMAIL">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_EMAIL" runat="server" size="35" maxlength="50"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvUSER_EMAIL" runat="server" ControlToValidate="txtUSER_EMAIL" ErrorMessage="USER_EMAIL can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revEmail" runat="server" ControlToValidate="txtUSER_EMAIL" Display="Dynamic"></asp:regularexpressionvalidator></TD>
					</tr>
					<tr>
						<td class="midcolora" width="191"><asp:label id="capLIC_BRICS_USER" runat="server">Brics User</asp:label></td>
						<td class="midcolora" width="32%" colSpan="3"><asp:dropdownlist id="cmbLIC_BRICS_USER" onfocus="SelectComboIndex('cmbLIC_BRICS_USER')" Runat="server"
								onchange="displayLoginData();"></asp:dropdownlist></td>
						<!--td class="midcolora" width="18%"><asp:label id="cap_BRICS_USER" runat="server">Bric's User</asp:label></td>
						<td class="midcolora" width="32%"><asp:DropDownList ID="cmb_BRICS_USER" Runat="server" onfocus="SelectComboIndex('cmb_BRICS_USER')"
								onchange="displayLoginData();"></asp:DropDownList></td-->
					</tr>
					<tr id="trLoginPwd">
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_LOGIN_ID" runat="server">Login ID</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_LOGIN_ID" runat="server" size="14" maxlength="10"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvUSER_LOGIN_ID" runat="server" ControlToValidate="txtUSER_LOGIN_ID" ErrorMessage="USER_LOGIN_ID can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revSignOnId" runat="server" ControlToValidate="txtUSER_LOGIN_ID" Display="Dynamic"></asp:regularexpressionvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capUSER_PWD" runat="server">Password</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_PWD" runat="server" onkeypress="return RemoveSingleQuote(event);" size="19" maxlength="15" AutoComlete = "Off" TextMode="Password" onBlur="Password_Change();replaceQuote(this);"></asp:textbox><BR><%--Added Password_Change() by Sibin for Itrack Issue 5212 to remove USER_PWD and USER_CONFIRM_PWD from View Source - Done on 7 Jan 08--%>
							<asp:requiredfieldvalidator id="rfvUSER_PWD" runat="server" ControlToValidate="txtUSER_PWD" ErrorMessage="USER_PWD can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator>
							<%--Added by Sibin for Itrack Issue 4994 on 21 Jan 09--%>	
							<asp:regularexpressionvalidator id="revUSER_PWD" runat="server" Display="Dynamic" ControlToValidate="txtUSER_PWD"
							ErrorMessage="Please Enter a valid password Containing at least one numeric, one Upper case and One Lower Case and a special Character."></asp:regularexpressionvalidator>		
						</TD>
					</tr>
					<tr id="trConfirmPwd">
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_CONFIRM_PWD" runat="server">Confirm Password</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtUSER_CONFIRM_PWD" runat="server" size="19" maxlength="15" AutoComlete = "Off" TextMode="Password" onkeypress="return RemoveSingleQuote(event);" onBlur="Password_Change();replaceQuote(this);"></asp:textbox><BR><%--Added Password_Change() by Sibin for Itrack Issue 5212 to remove USER_PWD and USER_CONFIRM_PWD from View Source - Done on 7 Jan 08--%>
							<asp:requiredfieldvalidator id="rfvUSER_CONFIRM_PWD" runat="server" ControlToValidate="txtUSER_CONFIRM_PWD"
								ErrorMessage="USER_CONFIRM_PWD can't be blank." Display="Dynamic"></asp:requiredfieldvalidator>
							<asp:comparevalidator id="cvPassword" runat="server" ControlToValidate="txtUSER_CONFIRM_PWD" ErrorMessage="Password does not match." Display="Dynamic" ControlToCompare="txtUSER_PWD"></asp:comparevalidator>
							<%--Added by Sibin for Itrack Issue 4994 on 21 Jan 09--%>
							<asp:regularexpressionvalidator id="revUSER_CONFIRM_PWD" runat="server" Display="Dynamic" ControlToValidate="txtUSER_CONFIRM_PWD" ErrorMessage="Please Enter a valid password Containing at least one numeric, one Upper case and One Lower Case and a special Character."></asp:regularexpressionvalidator>		
						</TD>									
		                <TD class="midcolora" width="18%"><asp:label id="capUSER_MGR_ID" runat="server">Manager</asp:label></TD>
		                <TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSER_MGR_ID" onfocus="SelectComboIndex('cmbUSER_MGR_ID')" runat="server">
				                <ASP:LISTITEM Value="0"></ASP:LISTITEM>
			                </asp:dropdownlist></TD>
	                </tr>
					<%--Added by Sibin for Itrack Issue 4994 on 10 Dec 08--%>
					<tr id="trPasswordChange">
						<td class="midcolora" width="191"><asp:label id="capCHANGE_PWD_NEXT_LOGIN" runat="server"></asp:label></td>
						<td class="midcolora" width="32%" colSpan="1"><asp:dropdownlist id="cmbCHANGE_PWD_NEXT_LOGIN" onfocus="SelectComboIndex('cmbCHANGE_PWD_NEXT_LOGIN')" runat="server"></asp:dropdownlist></td>
						
						<td class="midcolora" width="191"><asp:label id="capUSER_LOCKED" runat="server"></asp:label></td>
						<td class="midcolora" width="32%" colSpan="1"><asp:dropdownlist id="cmbUSER_LOCKED" onfocus="SelectComboIndex('cmbUSER_LOCKED')" runat="server"></asp:dropdownlist></td>
					</tr>
					<%--Added till here--%>
					<tr id="trSupervisor">
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_SPR" runat="server">Supervisor Equivalent</asp:label></TD>
						<TD class="midcolora" width="32%"><asp:checkbox id="chkUSER_SPR" runat="server"></asp:checkbox></TD>
						<td class="midcolora" colSpan="2"></td>
					</tr>
					<tr id="trUser">
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_TYPE_ID" runat="server">User Type</asp:label><span class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSER_TYPE_ID" onfocus="SelectComboIndex('cmbUSER_TYPE_ID')" runat="server">
								<ASP:LISTITEM Value="0"></ASP:LISTITEM>
							</asp:dropdownlist><BR>
							<asp:requiredfieldvalidator id="rfvUSER_TYPE_ID" runat="server" ControlToValidate="cmbUSER_TYPE_ID" ErrorMessage="USER_TYPE_ID can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<TD class="midcolora" width="18%"><asp:label id="capADJUSTER_CODE" runat="server">Adjuster Code</asp:label><span class="mandatory" id="spnADJUSTER_CODE">*</span></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtADJUSTER_CODE" runat="server" size="10" maxlength="2"></asp:textbox><BR>
							<asp:requiredfieldvalidator id="rfvADJUSTER_CODE" runat="server" ControlToValidate="txtADJUSTER_CODE" ErrorMessage="ADJUSTER_CODE can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
					</tr>
					<tr id="trDefault_Hierarchy" runat="server">
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capDefault_Hierarchy" runat="server">Default Hierarchy</asp:label><span class="mandatory" id="spnDefault_Hierarchy">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbDefault_Hierarchy" onfocus="SelectComboIndex('cmbDefault_Hierarchy')" runat="server">
								<ASP:LISTITEM Value="0"></ASP:LISTITEM>
							</asp:dropdownlist><BR>
							<asp:requiredfieldvalidator id="rfvDefault_Hierarchy" runat="server" ControlToValidate="cmbDefault_Hierarchy"
								ErrorMessage="Default_Hierarchy can't be blank." Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<td class="midcolora" colSpan="2"></td>
					</tr>
					<tr id="trTimeZone" runat="server">
						<TD class="midcolora" style="WIDTH: 191px" width="191"><asp:label id="capUSER_TIME_ZONE" runat="server">Time Zone</asp:label><span id="spnUSER_TIME_ZONE" runat="server" class="mandatory">*</span></TD>
						<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbUSER_TIME_ZONE" onfocus="SelectComboIndex('cmbUSER_TIME_ZONE')" runat="server">
								<ASP:LISTITEM Value="0"></ASP:LISTITEM>
							</asp:dropdownlist><BR>
							<asp:requiredfieldvalidator id="rfvUSER_TIME_ZONE" runat="server" ControlToValidate="cmbUSER_TIME_ZONE" ErrorMessage="USER_TIME_ZONE can't be blank."
								Display="Dynamic"></asp:requiredfieldvalidator></TD>
						<%--*************** A New Field(USER_NOTES)Added by Manoj Rathore ( 7th Nov 2006)*****************--%>
						<td class="midcolora" width="18%"><asp:label id="capUSER_NOTES" runat="server"></asp:label></td>
						<td class="midcolora" width="32%"><asp:textbox id="txtUSER_NOTES" runat="server" size="23" TextMode="MultiLine"></asp:textbox><BR>
						</td>
					</tr>
					<tr id="trCPF" runat="server">
								<td class="midcolora" width="18%"><asp:Label ID="capCPF" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtCPF" runat="server"></asp:TextBox>
								</br><asp:RegularExpressionValidator runat="server" ID="revCPF_CNPJ" ErrorMessage="" Display="Dynamic" ControlToValidate="txtCPF"></asp:RegularExpressionValidator>
								</td>
								<td class="midcolora" width="18%"></td>
								<td class="midcolora" width="32%"></td>
								</tr>
								
								<tr id="ttrACTIVITY" runat="server">
								<td class="midcolora" width="18%"><asp:Label ID="capACTIVITY" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:DropDownList ID="cmbACTIVITY" runat="server" Width="400px"></asp:DropDownList>
								</td>
								<td class="midcolora" width="18%"><asp:Label ID="capREGIONAL_ID" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtREGIONAL_IDENTIFICATION" runat="server"></asp:TextBox>
								</td>
								</tr>
								
								<tr id="ttrREGIONAL_ID_ISSUE_DATE" runat="server">
								 <td class="midcolora" width="18%">
                                 <asp:Label runat="server" ID="capREGIONAL_ID_ISSUE_DATE">Regional ID Issue Date</asp:Label></td>
                                 <td class="midcolora" width="32%"><asp:TextBox runat="server" ID="txtREG_ID_ISSUE_DATE" AutoCompleteType="Disabled" CausesValidation="true"></asp:TextBox><asp:HyperLink ID="hlkREG_ID_ISSUE_DATE" runat="server" CssClass="HotSpot">
                                 <asp:Image ID="imgREG_ID_ISSUE" runat="server" ImageUrl="/cms/cmsweb/Images/CalendarPicker.gif" ></asp:Image></asp:HyperLink><br/>
                                <asp:RegularExpressionValidator runat="server" ID="revREGIONAL_ID_ISSUE_DATE" Display="Dynamic" ControlToValidate="txtREG_ID_ISSUE_DATE"></asp:RegularExpressionValidator>
                                 
                                 <asp:comparevalidator id="cpv2REG_ID_ISSUE_DATE_FUTURE" ControlToValidate="txtREG_ID_ISSUE_DATE" Display="Dynamic" Runat="server" Type="Date"
					             Operator="LessThanEqual" 
					             ValueToCompare="<%# DateTime.Today.ToShortDateString()%>"  ></asp:comparevalidator><br>	
					            <asp:comparevalidator id="cpvREG_ID_ISSUE_DATE"  
					             ControlToValidate="txtREG_ID_ISSUE_DATE" Display="Dynamic" Runat="server" ControlToCompare="txtDATE_OF_BIRTH" Type="Date"
			                     Operator="GreaterThan"></asp:comparevalidator>
                                 <asp:CustomValidator ID="csvREG_ID_ISSUE_DATE" Display="Dynamic" runat="server" ControlToValidate="txtREG_ID_ISSUE_DATE" ClientValidationFunction="allnumeric"></asp:CustomValidator> 
                                </td>
								<td class="midcolora" width="18%"><asp:Label ID="capREGIONAL_ID_ISSUE" runat="server"></asp:Label>
								</td>
								<td class="midcolora" width="32%"><asp:TextBox ID="txtREG_ID_ISSUE" runat="server"></asp:TextBox>
								</td>
								</tr>
					
					
					<tr>
						<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"
								CausesValidation="false"></cmsb:cmsbutton></td>
						<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</tr>
					<input id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
					<input id="hidIS_ACTIVE" type="hidden" name="hidIS_ACTIVE" runat="server"> 
					<input id="hidOldData" type="hidden" name="hidOldData" runat="server">
					<input id="hidUSER_ID" type="hidden" value="0" name="hidUSER_ID" runat="server">
					<input id="hidLoginId" type="hidden" value="0" name="hidLoginId" runat="server">
					<input id="hidUSER_TYPE_ID" type="hidden" value="0" name="hidUSER_TYPE_ID" runat="server">
					<input id="hidAGENCY_CODE" type="hidden" name="hidAGENCY_CODE" runat="server"> 
					<input language="javascript" id="hidAGENCY_ADD1" onbeforeeditfocus="return hidAGENCY_ADD1_onbeforeeditfocus()" type="hidden" name="hidAGENCY_ADD1" runat="server" /> 
					<input id="hidAGENCY_ADD2" type="hidden" name="hidAGENCY_ADD2" runat="server">
					<input id="hidAGENCY_CITY" type="hidden" name="hidAGENCY_CITY" runat="server"> 
					<input id="hidAGENCY_STATE" type="hidden" name="hidAGENCY_STATE" runat="server">
					<input id="hidAGENCY_ZIP" type="hidden" name="hidAGENCY_ZIP" runat="server"> 
					<input id="hidAGENCY_COUNTRY" type="hidden" name="hidAGENCY_COUNTRY" runat="server">
					<input id="hidAGENCY_PHONE" type="hidden" name="hidAGENCY_PHONE" runat="server">
					<input id="hidAGENCY_FAX" type="hidden" name="hidAGENCY_FAX" runat="server"> 
					<input id="hidReset" type="hidden" value="0" name="hidReset" runat="server">
					<input id="hidAGENCY_LOGIN" type="hidden" name="hidAGENCY_LOGIN" runat="server">
					<input id="hidAGENCY" type="hidden" name="hidAGENCY_LOGIN" runat="server">
					<%--<input id="hidSSN_NO" type="hidden" name="hidSSN_NO" runat="server">--%>
					<%--Added by Sibin for Itrack Issue 5212 to remove USER_PWD and USER_CONFIRM_PWD from View Source - Done on 7 Jan 08--%>
					<input id="hidPWD" type="hidden" name="hidPWD" runat="server">
					<input id="hidCONFIRM_PWD" type="hidden" name="hidCONFIRM_PWD" runat="server">
					<input id="hidLIC_BRICS_USER" type="hidden" name="hidLIC_BRICS_USER" runat="server">
					<input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/>  	
					<input id="hidDATE_OF_BIRTH_NEW" type="hidden" name="hidDATE_OF_BIRTH_NEW" runat="server">	
					<input id="hidDATE_EXPIRY_NEW" type="hidden" name="hidDATE_EXPIRY_NEW" runat="server">
					<input id="hidNON_RESI_LICENSE_EXP_DATE_NEW" type="hidden" name="hidNON_RESI_LICENSE_EXP_DATE_NEW" runat="server">
					<input id="hidNON_RESI_LICENSE_EXP_DATE2_NEW" type="hidden" name="hidNON_RESI_LICENSE_EXP_DATE2_NEW" runat="server">
					<input id="hidREG_ID_ISSUE_DATE_NEW" type="hidden" name="hidREG_ID_ISSUE_DATE_NEW" runat="server">
                    <input id="hidUSER_COUNTRY" type="hidden" name="hidUSER_COUNTRY" runat="server" />
					<input id="hidactivate" type="hidden" name="hidactivate" runat="server" />
					<input id="hidDeactivate" type="hidden" name="hidactivate" runat="server" /> 
					<input id="hidinactive" type="hidden" name="hidinactive" runat="server" />
					<input id="hidactive" type="hidden" name="hidactive" runat="server" /> 
								 <input id="hidSTATE_ID" type="hidden" name="hidSTATE_ID" runat="server">
								 <input type="hidden" id="hidState" runat="server" />
                    <input type="hidden" runat="server" id="hidZipCodeCalledfor" name="hidZipCodeCalledfor" /> <%--changed by praveer for itrack no 1553/TFS# 626 --%>
                    <input type="hidden" runat="server" id="hidZipeCodeVerificationMsg" name="hidZipeCodeVerificationMsg" /> <%--changed by praveer for itrack no 1553/TFS# 626 --%>
				</tbody>
			</table>		
			</form>
		<script type="text/javascript" language="javascript">
		    RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidUSER_ID').value,true);
		    //RefreshWindowsGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidUSER_ID').value);
		if(document.getElementById('hidFormSaved').value == '1')
			{
			//***********************************
			setStatusLabel();
		
			//*********************************
			
				//Form get saved refreshing the grid
				//top.frames[1].document.gridObject.refreshcompletegrid();
				//document.userType.btnReset.style.display = "none"	
				//Enabling the activate deactivate button
				if(document.getElementById('btnActivateDeactivate'))
				document.getElementById('btnActivateDeactivate').setAttribute('disabled',false);
				SetActive(); 
			}
			else if(document.getElementById('hidFormSaved').value == '2')
			{
				//Reset should not be visible
				//document.userType.btnReset.style.display = "none"	
				//Enabling the activate deactivate button
				//document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
			}
			function setStatusLabel()
			{
				var xml		=	document.getElementById('hidOldData').value;
				//alert(xml+"   aaaa");
				if(xml!="")
				{
					//alert(xml);	
					//alert(document.getElementById('hidSystemUser').value);						
					var objXmlHandler = new XMLHandler();
					var tree = (objXmlHandler.quickParseXML(xml).getElementsByTagName('Table')[0]);
					var i=0;
					for(i=0;i<tree.childNodes.length;i++)
					{
						if(!tree.childNodes[i].firstChild) continue;
						var nodeName = tree.childNodes[i].nodeName;
						var nodeValue = tree.childNodes[i].firstChild.text;
						//alert(nodeName.toUpperCase());
						
						/*if(nodeName.toUpperCase() == "SYSTEMUSER")
						{
							if(nodeValue.toUpperCase() == "Y" || nodeValue == "1")
							{
							document.getElementById('btnActivateDeactivate').setAttribute('disabled',false);				
							}
							else
							{
							document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);				
							}
						}
						*/
						if(nodeName.toUpperCase() == "IS_ACTIVE") {
						    var activate = document.getElementById("hidactivate").value;
						    var Deactivate = document.getElementById("hidDeactivate").value;
						    var inactive = document.getElementById("hidinactive").value;
						    var active = document.getElementById("hidactive").value;
						//alert(nodeValue.toUpperCase());
							if(nodeValue.toUpperCase() == "Y" || nodeValue == "1")
							{
							    document.getElementById("lblStatus").innerText = active;
								if(document.getElementById('btnActivateDeactivate'))
								    document.getElementById('btnActivateDeactivate').setAttribute('value',Deactivate); 
								//document.getElementById('btnActivateDeactivate').value = "Deactivate";
								}
							else
							{
							    document.getElementById("lblStatus").innerText = inactive;
								document.getElementById('lblStatus').style.color = 'Red';
								//document.getElementById('btnActivateDeactivate').value = "Activate";
								if(document.getElementById('btnActivateDeactivate'))
								    document.getElementById('btnActivateDeactivate').setAttribute('value',activate); 
							}
							continue;
						}
					}
				}
			}
			
			function SetActive() {//alert(document.getElementById("hidIS_ACTIVE").value);
			    var inactive = document.getElementById("hidinactive").value;
			    var active = document.getElementById("hidactive").value;
				if (document.getElementById("hidIS_ACTIVE").value == "Y")
				{
				    document.getElementById("lblStatus").innerText = active ;
//						if(document.getElementById('btnActivateDeactivate'))
//					document.getElementById('btnActivateDeactivate').setAttribute('value','Deactivate');
				}
				else
				{
				    document.getElementById("lblStatus").innerText = inactive ;
//					if(document.getElementById('btnActivateDeactivate'))
//					document.getElementById('btnActivateDeactivate').setAttribute('value','Activate');
				}
			}
			
		</script>
		<%--</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>--%>
	</body>
</html>
