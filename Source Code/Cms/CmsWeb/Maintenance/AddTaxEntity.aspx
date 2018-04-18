<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddTaxEntity.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddTaxEntity" validateRequest = false %>
<%@ Register TagPrefix="uc1" TagName="AddressVerification" Src="/cms/cmsweb/webcontrols/AddressVerification.ascx" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_TAX_ENTITY_LIST</title>
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
 function AddData()
{
	document.getElementById('txtTAX_NAME').value  = '';
	document.getElementById('txtTAX_CODE').value  = '';
	document.getElementById('txtTAX_ADDRESS1').value  = '';
	document.getElementById('txtTAX_ADDRESS2').value  = '';
	document.getElementById('txtTAX_CITY').value  = '';
	document.getElementById('cmbTAX_COUNTRY').value  =-1;
	document.getElementById('cmbTAX_STATE').value  = -1;
	document.getElementById('txtTAX_ZIP').value  = '';
	document.getElementById('txtTAX_PHONE').value  = '';
	document.getElementById('txtTAX_EXT').value  = '';
	document.getElementById('txtTAX_FAX').value  = '';
	document.getElementById('txtTAX_EMAIL').value  = '';
	document.getElementById('txtTAX_WEBSITE').value  = '';
	document.getElementById('hidTAX_ID').value	= 'New';
    document.getElementById('hidFormSaved').value = '0';
	//document.getElementById('btnActivateDeactivate').setAttribute('disabled',true);	
	if(document.getElementById('btnDelete'))
	document.getElementById('btnDelete').setAttribute('disabled',true);
	DisableValidators();
	ChangeColor();
	//document.getElementById('txtTAX_NAME').focus();
				
	}
	//shikha

	function TAX_CountryChanged() {
	    GlobalError = true;
	    var CountryID = document.getElementById('cmbTAX_COUNTRY').options[document.getElementById('cmbTAX_COUNTRY').selectedIndex].value;
	    AddTaxEntity.AjaxFillState(CountryID, FillState);
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
	        var statesList = document.getElementById("cmbTAX_STATE");
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
	            document.getElementById('hidTAX_STATE').value = statesList.options[0].value;
	        }
	        document.getElementById("cmbTAX_STATE").value = document.getElementById("cmbTAX_STATE").value;
	    }

	    return false;
	}
	
		function setTab()
		{
		    if (document.getElementById('hidOldData').value != '') 
			{
			    var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
			    var tabtitles = TAB_TITLES.split(',');
         
				
				Url="AttachmentIndex.aspx?calledfrom=tax&EntityType=Tax&EntityId="+document.getElementById('hidTAX_ID').value + "&";
				DrawTab(3,top.frames[1],tabtitles[0],Url);
				
				Url="AccountingEntityIndex.aspx?calledfrom=tax&EntityType=Tax&EntityId="+document.getElementById('hidTAX_ID').value + "&EntityName="+document.getElementById('hidTAX_Name').value + "&";
				DrawTab(2, top.frames[1],tabtitles[1], Url);															
			}
			else
			{							
				RemoveTab(3,top.frames[1]);
				RemoveTab(2,top.frames[1]);
			}
		
		}
	
	function populateXML()
	{
		if(document.getElementById('hidOldData').value	!= '' && document.MNT_TAX_ENTITY_LIST.hidFormSaved.value == "1")
			{
				var tempXML;
				
					//Enabling the activate deactivate button
					if(document.getElementById('btnActivateDeactivate'))
					document.getElementById('btnActivateDeactivate').setAttribute('disabled',false); 
					if(document.getElementById('btnDelete'))
					document.getElementById('btnDelete').setAttribute('disabled',false);
					//Storing the XML in hidRowId hidden fields 
					tempXML = document.getElementById('hidOldData').value;
					populateFormData(tempXML,MNT_TAX_ENTITY_LIST);	
			 }			 
			 if ( document.MNT_TAX_ENTITY_LIST.hidTAX_ID.value == "New" && document.MNT_TAX_ENTITY_LIST.hidFormSaved.value == "0")
			 {
				AddData();
			 }
			setTab();
		return false;
	}
	
	
	function generateCode()
    {
		var strname=new String();
		strname=document.getElementById("txtTAX_NAME").value; 
				
		 if(document.getElementById('hidTAX_ID').value=='New')
			{
				if(strname.length>6 )
					document.getElementById("txtTAX_CODE").value=strname.substring(0,6);
				else
					document.getElementById("txtTAX_CODE").value=strname;					
			}
					  
	}
	// Added by Swarup For checking zip code for LOB: Start
	function GetZipForState_OLD()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbTAX_STATE').value==14 ||document.getElementById('cmbTAX_STATE').value==22||document.getElementById('cmbTAX_STATE').value==49)
			{ 
			if(document.getElementById('txtTAX_ZIP').value!="")
			{
				var intStateID = document.getElementById('cmbTAX_STATE').options[document.getElementById('cmbTAX_STATE').options.selectedIndex].value;
				var strZipID = document.getElementById('txtTAX_ZIP').value;	
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
}

function FormatZipcodeformat(vr) {


    var vr = new String(vr.toString());
    if (vr != "" && (document.getElementById('cmbTAX_COUNTRY').options[document.getElementById('cmbTAX_COUNTRY').options.selectedIndex].value == '5')) {

        vr = vr.replace(/[-]/g, "");
        num = vr.length;
        if (num == 8 && (document.getElementById('cmbTAX_COUNTRY').options[document.getElementById('cmbTAX_COUNTRY').options.selectedIndex].value == '5')) {
            vr = vr.substr(0, 5) + '-' + vr.substr(5, 3);
            //document.getElementById('revCHK_MAIL_ZIP').setAttribute('enabled', false);
            document.getElementById('revTAX_ZIP').setAttribute('enabled', false);

        }


    }

    return vr;
}
	    
	  /////ZIP AJAX CALL///
		function GetZipForState()
		{		    
			GlobalError=true;
			if(document.getElementById('cmbTAX_STATE').value==14 ||document.getElementById('cmbTAX_STATE').value==22||document.getElementById('cmbTAX_STATE').value==49)
			{ 
				if(document.getElementById('txtTAX_ZIP').value!="")
				{
					var intStateID = document.getElementById('cmbTAX_STATE').options[document.getElementById('cmbTAX_STATE').options.selectedIndex].value;
					var strZipID = document.getElementById('txtTAX_ZIP').value;	
					var result = AddTaxEntity.AjaxFetchZipForState(intStateID,strZipID);
					return AjaxCallFunction_CallBack_Zip(result);
					
				}	
				return false;
			}
			else 
				return true;
				
		}
		function AjaxCallFunction_CallBack_Zip(response)
		{		
		  if(document.getElementById('cmbTAX_STATE').value==14 ||document.getElementById('cmbTAX_STATE').value==22||document.getElementById('cmbTAX_STATE').value==49)
			{ 
				if(document.getElementById('txtTAX_ZIP').value!="")
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
		function ChkResult(objSource, objArgs) 
		{
		   var str = document.getElementById('hidZipCode').value;
			objArgs.IsValid = true;
			if(objArgs.IsValid == true)
			{
				objArgs.IsValid = GetZipForState();
				if (objArgs.IsValid == false)
				    document.getElementById('csvTAX_ZIP').innerHTML = str; //"The zip code does not belong to the state";				
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
		    $("#cmbTAX_STATE").change(function() {
		    $("#hidTAX_STATE_2").val($("#cmbTAX_STATE option:selected").val());
		        });
		    });
		</script>
	</HEAD>
	<BODY oncontextmenu = "return false;" leftMargin="0" topMargin="0" onload=" populateXML();ApplyColor();ChangeColor();">
		<FORM id="MNT_TAX_ENTITY_LIST" method="post" runat="server">		
		<P><uc1:addressverification id="AddressVerification1" runat="server"></uc1:addressverification></P>
			<P>
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td class="midcolorc" align="right" colSpan="4">
							<asp:label id="lblDelete" runat="server" Visible="False" CssClass="errmsg"></asp:label>
						</td>
					</tr>
					<TR id="trBody" runat="server">
						<TD>
							<TABLE width="100%" align="center" border="0">
								<TBODY>
									<TR>
										<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
									</TR>
									<TR>
										<TD class="midcolorc" align="center" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></TD>
									</TR>
									<TR>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_NAME" runat="server">Name</asp:Label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%">
											<P><asp:textbox id="txtTAX_NAME" runat="server" maxlength="70" size="30"></asp:textbox><br>
												<asp:requiredfieldvalidator id="rfvTAX_NAME" runat="server" Display="Dynamic" ControlToValidate="txtTAX_NAME"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTAX_NAME" runat="server" Display="Dynamic" ControlToValidate="txtTAX_NAME"></asp:regularexpressionvalidator></P>
										</TD>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_CODE" runat="server">Code</asp:Label><SPAN class="mandatory">*</SPAN>
										</TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_CODE" runat="server" maxlength="6" size="9"></asp:textbox><br>
											<asp:requiredfieldvalidator id="rfvTAX_CODE" runat="server" Display="Dynamic" ControlToValidate="txtTAX_CODE"></asp:requiredfieldvalidator>
											<asp:regularexpressionvalidator id="revTAX_CODE" runat="server" ControlToValidate="txtTAX_CODE" Display="Dynamic"></asp:regularexpressionvalidator>
										</TD>
									</TR>
									<TR>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_ADDRESS1" runat="server">Address1</asp:Label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_ADDRESS1" runat="server" maxlength="70" size="30"></asp:textbox><BR>
											<asp:requiredfieldvalidator id="rfvTAX_ADDRESS1" runat="server" Display="Dynamic" ControlToValidate="txtTAX_ADDRESS1"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_ADDRESS2" runat="server">Address2</asp:Label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_ADDRESS2" runat="server" maxlength="70" size="30"></asp:textbox></TD>
									</TR>
									<TR>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_CITY" runat="server">City</asp:Label><SPAN id="spnTAX_CITY" runat="server" class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_CITY" runat="server" maxlength="40" size="30"></asp:textbox><BR>
											<asp:requiredfieldvalidator id="rfvTAX_CITY" runat="server" Display="Dynamic" ControlToValidate="txtTAX_CITY"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_COUNTRY" runat="server">Country</asp:Label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTAX_COUNTRY" OnFocus="SelectComboIndex('cmbTAX_COUNTRY');" onchange="javascript:TAX_CountryChanged();" runat="server"></asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvTAX_COUNTRY" runat="server" Display="Dynamic" ControlToValidate="cmbTAX_COUNTRY"></asp:requiredfieldvalidator>
											</TD>
									</TR>
									<TR>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_STATE" runat="server">State</asp:Label><SPAN id="spnTAX_STATE"  runat="server" class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbTAX_STATE" onfocus="SelectComboIndex('cmbTAX_STATE');" runat="server"></asp:dropdownlist><BR>
											<asp:requiredfieldvalidator id="rfvTAX_STATE" runat="server" Display="Dynamic" ControlToValidate="cmbTAX_STATE"></asp:requiredfieldvalidator></TD>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_ZIP" runat="server">Zip</asp:Label><SPAN class="mandatory">*</SPAN></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_ZIP" runat="server" maxlength="8" size="13" OnBlur="GetZipForState();this.value=FormatZipcodeformat(this.value);ValidatorOnChange();"></asp:textbox>
										<%-- Added by Swarup on 30-mar-2007 --%>
											<asp:hyperlink id="hlkZipLookup" runat="server" CssClass="HotSpot">
											<asp:image id="imgZipLookup" runat="server" ImageUrl="/cms/cmsweb/images/info.gif" ImageAlign="Bottom"></asp:image>
											</asp:hyperlink><BR>
											<asp:customvalidator id="csvTAX_ZIP" Runat="server" ClientValidationFunction="ChkResult" ErrorMessage=" "
												Display="Dynamic" ControlToValidate="txtTAX_ZIP"></asp:customvalidator>
											<asp:requiredfieldvalidator id="rfvTAX_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtTAX_ZIP"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revTAX_ZIP" runat="server" Display="Dynamic" ControlToValidate="txtTAX_ZIP"></asp:regularexpressionvalidator></TD>
									</TR>
									<TR>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_PHONE" runat="server">Phone</asp:Label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_PHONE" runat="server" maxlength="13" size="17"></asp:textbox><BR>
											<asp:regularexpressionvalidator id="revTAX_PHONE" runat="server" Display="Dynamic" ControlToValidate="txtTAX_PHONE"></asp:regularexpressionvalidator></TD>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_EXT" runat="server">Ext</asp:Label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_EXT" runat="server" maxlength="4" size="5"></asp:textbox><BR>
											<asp:regularexpressionvalidator id="revTAX_EXT" runat="server" Display="Dynamic" ControlToValidate="txtTAX_EXT"></asp:regularexpressionvalidator></TD>
									</TR>
									<TR>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_FAX" runat="server">Fax</asp:Label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_FAX"  onblur="ValidatorOnChange()" runat="server" maxlength="13" size="17"></asp:textbox><BR>
											<asp:regularexpressionvalidator id="revTAX_FAX" runat="server" Display="Dynamic" ControlToValidate="txtTAX_FAX"></asp:regularexpressionvalidator></TD>
										<TD class="midcolora" width="18%">
											<asp:Label id="capTAX_EMAIL" runat="server">Email</asp:Label></TD>
										<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_EMAIL" runat="server" maxlength="50" size="30"></asp:textbox><BR>
											<asp:regularexpressionvalidator id="revTAX_EMAIL" runat="server" Display="Dynamic" ControlToValidate="txtTAX_EMAIL"
												ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></TD>
						</TD>
					</TR>
					<TR>
						<TD class="midcolora" width="18%">
							<asp:Label id="capTAX_WEBSITE" runat="server">Website</asp:Label></TD>
						<TD class="midcolora" width="32%"><asp:textbox id="txtTAX_WEBSITE" runat="server" maxlength="50" size="30"></asp:textbox><BR>
							<asp:RegularExpressionValidator id="revTAX_WEBSITE" runat="server" ControlToValidate="txtTAX_WEBSITE" Display="Dynamic"></asp:RegularExpressionValidator>
						</TD>
						<td class="midcolora" colSpan="2"></td>
					</TR>
					<tr>
						<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnActivateDeactivate" runat="server" Text="Activate/Deactivate"></cmsb:cmsbutton></td>
						<td class="midcolorr" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></td>
					</tr>
				</TABLE>
				</TD></TR></TBODY></TABLE><INPUT id="hidFormSaved" type="hidden" value="1" name="hidFormSaved" runat="server">
				<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
				<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server"> <INPUT id="hidTAX_ID" type="hidden" name="hidTAX_ID" runat="server">
				<INPUT id="hidTAX_Name" type="hidden" name="hidTAX_Name" runat="server">
				<input  type="hidden" runat="server" ID="hidTAB_TITLES"  value=""  name="hidTAB_TITLES"/> 
				<input type ="hidden" id="hidZipCode" runat="server" />
				<input type="hidden" runat="server" id="hidTAX_STATE" value="" name="hidTAX_STATE" />
				<input type="hidden" runat="server" id="hidTAX_STATE_2" value="" name="hidTAX_STATE_2" />
				<script>
				if (document.getElementById("hidFormSaved").value == "5")
				{
					/*Record deleted*/
					/*Refreshing the grid and coverting the form into add mode*/
					/*Using the javascript*/
					RefreshWebGrid("1","1"); 
					document.getElementById("hidFormSaved").value = "0";
					AddData();
				
			    }
			    
			    if(document.getElementById('hidFormSaved').value=='2')
			    {
			    document.getElementById('txtTAX_CODE').value='';
			    document.getElementById('txtTAX_CODE').focus();
			    
			    }
				</script>
		</FORM>
		</TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></P>
		<P>&nbsp;</P>
		<P>&nbsp;</P>
		</FORM>
		<script>
		    RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidTAX_ID').value, true);
		</script>
	</BODY>
</HTML>
