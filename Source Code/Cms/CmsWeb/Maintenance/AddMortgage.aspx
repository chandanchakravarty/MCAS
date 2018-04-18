<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddMortgage.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddMortgage" validateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_HOLDER_INTEREST_LIST</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script> 
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script language="javascript">
		function AddData() {
		   
			document.getElementById('hidHOLDER_ID').value	=	'New';
			document.getElementById('txtHOLDER_NAME').value  = '';
			document.getElementById('txtHOLDER_CODE').value  = '';
			document.getElementById('txtHOLDER_ADD1').value  = '';
			document.getElementById('txtHOLDER_ADD2').value  = '';
			document.getElementById('txtHOLDER_CITY').value  = '';
			//document.getElementById('cmbHOLDER_COUNTRY').options.selectedIndex = 0;
			document.getElementById('cmbHOLDER_STATE').options.selectedIndex = -1;
			document.getElementById('txtHOLDER_ZIP').value  = '';
			document.getElementById('txtHOLDER_MAIN_PHONE_NO').value  = '';
			document.getElementById('txtHOLDER_EXT').value  = '';
			document.getElementById('txtHOLDER_MOBILE').value  = '';
			document.getElementById('txtHOLDER_FAX').value  = '';
			document.getElementById('txtHOLDER_EMAIL').value  = '';
			document.getElementById('cmbHOLDER_LEGAL_ENTITY').options.selectedIndex = -1;
			document.getElementById('cmbHOLDER_TYPE').options.selectedIndex = -1;
			document.getElementById('txtHOLDER_MEMO').value  = '';
			if(document.getElementById('btnActivateDeactivate'))
			document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);
			DisableValidators();
			ChangeColor();
			//document.getElementById('txtHOLDER_NAME').focus();

}

//Shikha
function HOLDERCountryChanged() {
    GlobalError = true;
    var CountryID = document.getElementById('cmbHOLDER_COUNTRY').options[document.getElementById('cmbHOLDER_COUNTRY').selectedIndex].value;
    AddMortgage.AjaxFillState(CountryID, FillState);
    if (GlobalError) {
        return false;
    }
    else {
        return true;
    }
}

function FillState(Result) {
    //var strXML;
    if (Result.error) {
        var xfaultcode = Result.errorDetail.code;
        var xfaultstring = Result.errorDetail.string;
        var xfaultsoap = Result.errorDetail.raw;
    }
    else {
        var statesList = document.getElementById("cmbHOLDER_STATE");
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
        if (statesList.options.length > 0) {
            //statesList.remove(0);
            document.getElementById('hidHOLDER_STATE').value = statesList.options[0].value;
        }
        document.getElementById("cmbHOLDER_STATE").value = document.getElementById("cmbHOLDER_STATE").value;
    }

    return false;
}

function FormatZipcodeformat(vr) {


    var vr = new String(vr.toString());
    if (vr != "" && (document.getElementById('cmbHOLDER_COUNTRY').options[document.getElementById('cmbHOLDER_COUNTRY').options.selectedIndex].value == '5')) {

        vr = vr.replace(/[-]/g, "");
        num = vr.length;
        if (num == 8 && (document.getElementById('cmbHOLDER_COUNTRY').options[document.getElementById('cmbHOLDER_COUNTRY').options.selectedIndex].value == '5')) {
            vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
            //document.getElementById('revCHK_MAIL_ZIP').setAttribute('enabled', false);
            document.getElementById('revHOLDER_ZIP').setAttribute('enabled', false);

        }


    }

    return vr;
}
		
		function SetTab()
		{
			if (document.getElementById('hidOldData').value	!= '') {
			    var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
			    var tabtitles = TAB_TITLES.split(',');
			
				Url="AttachmentIndex.aspx?calledfrom=mortgage&EntityType=Mortgage&EntityId="+document.getElementById('hidHOLDER_ID').value + "&";	
				var entityTyp = '';
				if(document.getElementById('txtHOLDER_NAME') != null)
					entityTyp += document.getElementById('txtHOLDER_NAME').value;
				if(document.getElementById('txtHOLDER_CODE') != null)
					entityTyp += '~' + document.getElementById('txtHOLDER_CODE').value;
				
				
				
				Url="AttachmentIndex.aspx?calledfrom=mortgage&EntityType=Mortgage&EntityName=" + entityTyp + "&EntityId="+document.getElementById('hidHOLDER_ID').value + "&";
				DrawTab(2, top.frames[1], tabtitles[0], Url);	
			
			
				//Url="AttachmentIndex.aspx?calledfrom=mortgage&EntityType=Mortgage&EntityId="+document.getElementById('hidHOLDER_ID').value + "&";
				//DrawTab(2,top.frames[1],'Attachment',Url);
			}
			else
			{							
				RemoveTab(2,top.frames[1]);
			}			
		}
		
		function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 50) 
				{
				
					//event.returnValue=false;
					arguments.IsValid = false;
					return false;   // invalid userName
				}
			}
		
		function populateXML()
		{
		    var tempXML;
		  
				tempXML=document.getElementById("hidOldData").value;
				//if(document.getElementById('hidOldData').value	!= '')
				//{
				
				if (tempXML != "0" && tempXML!="")
					{
						//document.getElementById('btnDelete').style.display='none';
						populateFormData(tempXML,MNT_HOLDER_INTEREST_LIST);		
						//if(top.frames[1].strXML!="")
						//{
						//document.getElementById('btnReset').style.display='none';
						//	tempXML=top.frames[1].strXML;
						//Enabling the activate deactivate button
						if(document.getElementById('btnActivateDeactivate'))
						document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
						//Storing the XML in hidRowId hidden fields 
						//document.getElementById('hidOldData').value		=	 tempXML;

						//tempXML = document.getElementById('hidOldData').value;
						//populateFormData(tempXML,MNT_HOLDER_INTEREST_LIST);				
					   ChangeColor();

					}
			 else
			 {
				AddData();
			 }
		SetTab();
		return false;
		}
		function generateCode()
			{
			var strname=new String();
				strname=document.getElementById("txtHOLDER_NAME").value; 
				
				if(document.getElementById('hidHOLDER_ID').value=='New')
				{
					if(strname.length>6)
						document.getElementById("txtHOLDER_CODE").value=strname.substring(0,6);
					else
						document.getElementById("txtHOLDER_CODE").value=strname;					
				}
		}
		function showPageLookupLayer(controlId)
			{
				var lookupMessage;						
				switch(controlId)
				{
					case "cmbHOLDER_TYPE":
						lookupMessage	=	"%HLDTY.";
						break;
					default:
						lookupMessage	=	"Look up code not found";
						break;
						
				}
				showLookupLayer(controlId,lookupMessage);							
			}
		// Added by Swarup For checking zip code for LOB: Start
	
		function GetZipForState_OLD()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbHOLDER_STATE').value==14 ||document.getElementById('cmbHOLDER_STATE').value==22||document.getElementById('cmbHOLDER_STATE').value==49)
			{ 
				if(document.getElementById('txtHOLDER_ZIP').value!="")
				{
					var intStateID = document.getElementById('cmbHOLDER_STATE').options[document.getElementById('cmbHOLDER_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtHOLDER_ZIP').value;	
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
		
			/////ZIP AJAX CALL///
		function GetZipForState()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbHOLDER_STATE').value==14 ||document.getElementById('cmbHOLDER_STATE').value==22||document.getElementById('cmbHOLDER_STATE').value==49)
			{ 
				if(document.getElementById('txtHOLDER_ZIP').value!="")
				{
					var intStateID = document.getElementById('cmbHOLDER_STATE').options[document.getElementById('cmbHOLDER_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtHOLDER_ZIP').value;	
					var result = AddMortgage.AjaxFetchZipForState(intStateID,strZipID);
					return AjaxCallFunction_CallBack_Zip(result);
					
				}	
				return false;
			}
			else 
				return true;
				
		}
		function AjaxCallFunction_CallBack_Zip(response)
		{		
		  if(document.getElementById('cmbHOLDER_STATE').value==14 ||document.getElementById('cmbHOLDER_STATE').value==22||document.getElementById('cmbHOLDER_STATE').value==49)
			{ 
				if(document.getElementById('txtHOLDER_ZIP').value!="")
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
		
		//////AJAX END/////	
			
		
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
					var strZip=document.getElementById('hidZip').value;
						objArgs.IsValid = true;
						if(objArgs.IsValid == true)
						{
							objArgs.IsValid = GetZipForState();
							if (objArgs.IsValid == false)
							    document.getElementById('csvHOLDER_ZIP').innerHTML = strZip;  //"The zip code does not belong to the state";				
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
						document.getElementById("btnSave").click();
					}
			
		// Added by Swarup For checking zip code for LOB: End
		</script>
		<script language="javascript"  type="text/javascript" >

		    $(document).ready(function() {
		    $("#cmbHOLDER_STATE").change(function() {
		    $("#hidHOLDER_STATE_2").val($("#cmbHOLDER_STATE option:selected").val());
		        });
		    });
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();">
		<FORM id="MNT_HOLDER_INTEREST_LIST" method="post" runat="server">		
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_NAME" Runat="server">Name</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_NAME" runat="server" size="30" maxlength="70"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvHOLDER_NAME" runat="server" ControlToValidate="txtHOLDER_NAME" Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revHOLDER_NAME" runat="server" ControlToValidate="txtHOLDER_NAME" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_CODE" Runat="server">Code</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_CODE" runat="server" size="10" maxlength="8"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvHOLDER_CODE" runat="server" ControlToValidate="txtHOLDER_CODE" Display="Dynamic"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revHOLDER_CODE" runat="server" ControlToValidate="txtHOLDER_CODE" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_ADD1" Runat="server">Address1</asp:label><span class="mandatory">*</span>
								</TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_ADD1" runat="server" size="30" maxlength="70"></asp:textbox><BR>
									<asp:requiredfieldvalidator id="rfvHOLDER_ADD1" runat="server" ControlToValidate="txtHOLDER_ADD1" Display="Dynamic"></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_ADD2" Runat="server">Address2</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_ADD2" runat="server" size="30" maxlength="70"></asp:textbox></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_CITY" Runat="server">City</asp:label><span id="spnHOLDER_CITY" runat="server" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_CITY" runat="server" size="30" maxlength="40"></asp:textbox><br>
									<asp:requiredfieldvalidator id="rfvHOLDER_CITY" runat="server" ControlToValidate="txtHOLDER_CITY" Display="Dynamic"
										ErrorMessage="HOLDER_CITY can't be blank."></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_COUNTRY" Runat="server">Country</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHOLDER_COUNTRY" onfocus="SelectComboIndex('cmbHOLDER_COUNTRY')" onchange="javascript:HOLDERCountryChanged();" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvHOLDER_COUNTRY" runat="server" ControlToValidate="cmbHOLDER_COUNTRY" Display="Dynamic"
										ErrorMessage="HOLDER_COUNTRY can't be blank."></asp:requiredfieldvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_STATE" Runat="server">State</asp:label><span id="spnHOLDER_STATE" runat="server" class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHOLDER_STATE" onfocus="SelectComboIndex('cmbHOLDER_STATE')" runat="server">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR>
									<asp:requiredfieldvalidator id="rfvHOLDER_STATE" runat="server" ControlToValidate="cmbHOLDER_STATE" Display="Dynamic"
										ErrorMessage="HOLDER_STATE can't be blank."></asp:requiredfieldvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_ZIP" Runat="server">Zip</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_ZIP" runat="server" size="13" maxlength="8" OnBlur="GetZipForState();this.value=FormatZipcodeformat(this.value);ValidatorOnChange();"></asp:textbox>
								<%-- Added by Swarup on 30-mar-2007 --%>
									<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
									<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
									</asp:hyperlink>
									<BR>
									<asp:customvalidator id="csvHOLDER_ZIP" Runat="server" ClientValidationFunction="ChkResult" ErrorMessage=" "
												Display="Dynamic" ControlToValidate="txtHOLDER_ZIP"></asp:customvalidator>
									<asp:requiredfieldvalidator id="rfvHOLDER_ZIP" runat="server" ControlToValidate="txtHOLDER_ZIP" Display="Dynamic"
										ErrorMessage="HOLDER_ZIP can't be blank."></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revHOLDER_ZIP" runat="server" ControlToValidate="txtHOLDER_ZIP" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_MAIN_PHONE_NO" Runat="server">Phone No</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_MAIN_PHONE_NO" runat="server" size="17" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revHOLDER_MAIN_PHONE_NO" runat="server" ControlToValidate="txtHOLDER_MAIN_PHONE_NO"
										Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_EXT" Runat="server">Ext</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_EXT" runat="server" size="5" maxlength="4"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revHOLDER_EXT" runat="server" ControlToValidate="txtHOLDER_EXT" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_MOBILE" Runat="server">Mobile No</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_MOBILE" runat="server" size="17"  maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revHOLDER_MOBILE" runat="server" ControlToValidate="txtHOLDER_MOBILE" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_FAX" Runat="server">Fax</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_FAX" runat="server" size="17"  onblur="ValidatorOnChange()" maxlength="13"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revHOLDER_FAX" runat="server" ControlToValidate="txtHOLDER_FAX" Display="Dynamic"></asp:regularexpressionvalidator></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_EMAIL" Runat="server">Email</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:textbox id="txtHOLDER_EMAIL" runat="server" size="30" maxlength="50"></asp:textbox><BR>
									<asp:regularexpressionvalidator id="revHOLDER_EMAIL" runat="server" ControlToValidate="txtHOLDER_EMAIL" Display="Dynamic"></asp:regularexpressionvalidator></TD>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_LEGAL_ENTITY" Runat="server">Legal Entity</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHOLDER_LEGAL_ENTITY" onfocus="SelectComboIndex('cmbHOLDER_LEGAL_ENTITY')"
										runat="server">
										<asp:ListItem Value=''></asp:ListItem>
									</asp:dropdownlist></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_TYPE" Runat="server">Holder Type</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbHOLDER_TYPE" onfocus="SelectComboIndex('cmbHOLDER_TYPE')" runat="server"></asp:dropdownlist><A class="calcolora" href="javascript:showPageLookupLayer('cmbHOLDER_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								</TD>
								<TD class="midcolora" width="50%" colSpan="2"></TD>
							</tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capHOLDER_MEMO" Runat="server">Memo</asp:label></TD>
								<TD class="midcolora" width="82%" colSpan="3"><asp:textbox onkeypress="MaxLength(this,50);" id="txtHOLDER_MEMO" runat="server" size="55" maxlength="50"
										TextMode="MultiLine"></asp:textbox><br><asp:customvalidator id="csvHOLDER_MEMO" Runat="server" ControlToValidate="txtHOLDER_MEMO" Display="Dynamic"
										ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
							</tr>
							<tr>
								<td class="midcolora" width="50%" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
								<td class="midcolorr" width="50%" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidHOLDER_ID" type="hidden" value="0" name="hidHOLDER_ID" runat="server">
			<input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/> 
			<input type="hidden" runat="server" id="hidZip" />
			<input type="hidden" runat="server" id="hidHOLDER_STATE" value="" name="hidHOLDER_STATE" />
			<input type="hidden" runat="server" id="hidHOLDER_STATE_2" value="" name="hidHOLDER_STATE_2" />
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" left="0px" top="0px;"></iframe>
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
					<td colSpan="2">
						<span id="LookUpMsg"></span>
					</td>
				</tr>
			</table>
		</DIV>
		<!-- For lookup layer ends here-->
		<script>
		RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidHOLDER_ID').value,true);
		</script>
	</BODY>
</HTML>
