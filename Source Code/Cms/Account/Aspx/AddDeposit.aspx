<%@ Page language="c#" Codebehind="AddDeposit.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.AddDeposit" validateRequest=false %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>ACT_CURRENT_DEPOSITS</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta http-equiv="Cache-Control" content="no-cache">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"><LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/calendar.js"></script>
		<script src="/cms/cmsweb/scripts/AJAXcommon.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		<script language="javascript" type="text/javascript">
		    var jsaAppDtFormat = "<%=aAppDtFormat  %>";
		//This function activates the details tab
		function OnNextClick()
		{
			this.parent.parent.changeTab(0,1);
			return false;
			
		}
	
		//Client validation function checking trans date
		function ChkFiscalDate(objSource , objArgs) {
		    
			if(document.getElementById("revDEPOSIT_TRAN_DATE").isvalid==true)
			{
				if (document.getElementById("cmbFISCAL_ID").selectedIndex >= 0)
				{ 
				   var effdate = document.forms[0].txtDEPOSIT_TRAN_DATE.value;
				    //var effdate = document.getElementById('hidDEPOSIT_TRAN_DATE').value;
					var fdate = document.getElementById("cmbFISCAL_ID") .options[document.getElementById("cmbFISCAL_ID").selectedIndex].text;
					
					d1 = fdate.substring(fdate.indexOf("(") + 1, fdate.indexOf("(") + 11);
					d2 = fdate.substring(fdate.indexOf("-") + 1,fdate.indexOf("-") + 12);
					 tranDate = document.forms[0].txtDEPOSIT_TRAN_DATE.value;
					 //tranDate = document.getElementById('hidDEPOSIT_TRAN_DATE').value;
					d1 = trim(d1);
					d2 = trim(d2);
					
					d1 = DateConvert(d1, jsaAppDtFormat);
					d2 = DateConvert(d2, jsaAppDtFormat);
					tranDate = DateConvert(tranDate, jsaAppDtFormat);
					
					d1 = new Date(d1);
					d2 = new Date(d2);
					
					tranDate = new Date(tranDate);
					
					d1 = Date.parse(d1);
					d2 = Date.parse(d2);
					tranDate = Date.parse(tranDate);
					
					if (tranDate < d1 || tranDate > d2)
						objArgs.IsValid = false;
					else
						objArgs.IsValid = true;
					
				}
			}
			else
				objArgs.IsValid = true;
}
function ChkFutureDate(objSource, objArgs) {
 
    if (document.getElementById("revDEPOSIT_TRAN_DATE").isvalid == true) {

        var effdate = document.getElementById("txtDEPOSIT_TRAN_DATE").value;
        objArgs.IsValid = DateComparer("<%=DateTime.Now%>", effdate, jsaAppDtFormat);
    }
    else
        objArgs.IsValid = true;
}
		function DateConvert(Date, dateFormate) {
		    // debugger;
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
	
		function SetTab()
		{
		    
		    if (document.getElementById("hidOldData").value != "") {



		        var depNum = document.getElementById("txtDEPOSIT_NUMBER").value
		        //Modified by Pradeep to Add on param RECEIPT_MODE - itrack - 1495
				var QueryStringParams = "DEPOSIT_ID=" + document.getElementById("hidDEPOSIT_ID").value + "&DEPOSIT_NUM=" + depNum + "&DEPOSIT_TYPE=" + $("#cmbDEPOSIT_TYPE option:selected").val() + "&RECEIPT_MODE=" + $("#cmbRECEIPT_MODE option:selected").val();
				//Url="DepositDetails.aspx?" + QueryStringParams + "CUST&"; //Commented By Pradeep Kushwaha on 22-oct-2010
				Url = "DepositDetailsIndex.aspx?" + QueryStringParams + "&CUST&"; //Added By Pradeep Kushwaha on 22-oct-2010
				var TAB_TITLES = document.getElementById('hidTAB_TITLES').value;
				var tabtitles = TAB_TITLES.split(',');
				var TabTitleName = '';
				$("#hidDEPOSIT_TYPE").val($("#cmbDEPOSIT_TYPE option:selected").val());
				
				if ($("#cmbDEPOSIT_TYPE option:selected").val() == "14831")//Normal
				    TabTitleName = tabtitles[0];
				else if ($("#cmbDEPOSIT_TYPE option:selected").val() == "14832")//Co-Insurance
				    TabTitleName = tabtitles[1];
				else if ($("#cmbDEPOSIT_TYPE option:selected").val() == "14916")//Broker Refund
				    TabTitleName = tabtitles[2];
				else if ($("#cmbDEPOSIT_TYPE option:selected").val() == "14917")//Reinsurance Refund
				    TabTitleName = tabtitles[3];
				else if ($("#cmbDEPOSIT_TYPE option:selected").val() == "14918")//Ceded CO Refund
				    TabTitleName = tabtitles[4];    
				
                DrawTab(2, this.parent.parent, TabTitleName, Url);
				//Commented by Pradeep Kushwaha on 24-Feb-24-Feb-2011 (itrack - 846)
                //Url="DepositAgency.aspx?" + QueryStringParams + "AGN&";
                //DrawTab(3,this.parent.parent,tabtitles[2],Url);//Agency Receipts
                //				
                //Url="DepositOthers.aspx?" + QueryStringParams + "CLAM&";
                //DrawTab(4,this.parent.parent,tabtitles[3],Url);//Claim & Reinsurance Receipts
                //				
                //Url="DepositOthers.aspx?" + QueryStringParams + "MISC&";
                //DrawTab(5, this.parent.parent, tabtitles[4], Url); //Miscellaneous Receipts
				//Commented till here 
				
				//showing the copy, next and delete button
				if (document.getElementById("btnCopy") != null)
					document.getElementById("btnCopy").style.display = "inline";
					
				if (document.getElementById("btnNext") != null)
					document.getElementById("btnNext").style.display = "inline";
				
				if (document.getElementById("btnDelete") != null)
					document.getElementById("btnDelete").style.display = "inline";
									
				if (document.getElementById("btnCommit") != null)
					document.getElementById("btnCommit").style.display = "inline";
				if(parent.parent.totalRecords!='undefined' && parent.parent.totalRecords!=null)
				parent.parent.JournalId = document.getElementById("hidDEPOSIT_ID").value;
				//parent.parent.ClickRowNo
				
			}
			else
			{
			    //Commented by Pradeep Kushwaha on 24-Feb-24-Feb-2011 (itrack - 846)
				//RemoveTab(5,this.parent.parent);
				//RemoveTab(4,this.parent.parent);
			    //RemoveTab(3,this.parent.parent);
			    //Commented till here 
				RemoveTab(2,this.parent.parent);
			
				if(document.getElementById("btnCopy") != null)
					document.getElementById("btnCopy").style.display = "none";
					
				if (document.getElementById("btnNext") != null)
					document.getElementById("btnNext").style.display = "none";
				
				if (document.getElementById("btnDelete") != null)
					document.getElementById("btnDelete").style.display = "none";
				
				if (document.getElementById("btnCommit") != null)
					document.getElementById("btnCommit").style.display = "none";
					
				
			}
		}
		
		//This function activates the first tab
		function OnBackClick()
		{
			this.parent.parent.changeTab(0,0);
			return false;
		}
		
		function ResetForm()
		{
			document.ACT_CURRENT_DEPOSITS.reset();
			DisableValidators();
			populateXML();
			ChangeColor();			
			return false;
		}
		
		function OnDeleteClick()
		{
		    var ans;
			var str1=document.getElementById('hidVal').value 
			ans=window.confirm(str1); //alert (ans); 
			if (ans==true) { 
			//alert('Yes'); 
				document.getElementById("hidDelete").value='Yes'; 
			} 
			else { //alert('No'); 
				document.getElementById("hidDelete").value='No';
			} 
		}
	
		//Defaulting the financial year (General Leder)
		function SetFiscalYear()
		{
		    tranDate = document.getElementById("txtDEPOSIT_TRAN_DATE").value;
		    //tranDate = document.getElementById('hidDEPOSIT_TRAN_DATE').value;
			tranDate = new Date(tranDate);
			tranDate = Date.parse(tranDate);
			
			cmbFiscal = document.getElementById("cmbFISCAL_ID");
			for(ctr = 0; ctr < cmbFiscal.options.length; ctr++)
			{
				fdate = cmbFiscal.options[ctr].text;
				
				if (fdate.trim() == "")
				{
					continue;
				}
			
				//Getting the financial dates, from financial year
				d1 = fdate.substring(fdate.indexOf("(") + 1, fdate.indexOf("(") + 11);
				d2 = fdate.substring(fdate.indexOf("-") + 1,fdate.indexOf("-") + 12);
				
				d1 = new Date(d1);
				d2 = new Date(d2);
				
				d1 = Date.parse(d1);
				d2 = Date.parse(d2);
			
				if (tranDate >= d1 && tranDate <= d2)		
				{
					//Transaction date is in between financial dates
					//Hence selecting this fiscal year
					cmbFiscal.selectedIndex = ctr;
					break;
				}	
			}


}
//Show and hide function
function fnShowAndHideValidator(validatorId, flag) {
    if (flag == true)//show
        document.getElementById(validatorId.id).setAttribute('enabled', true);
    else if (flag == false)//hide
        document.getElementById(validatorId.id).setAttribute('enabled', false);
}

$(document).ready(function () {
    //Hide Account Balance field - I-track- 1045 (Pradeep Kushwaha on 29-March-2011)
    $("#capACCOUNT_BALANCE").hide();
    $("#lblACCOUNT_BALANCE").hide();
    //Till Hire

    //Hide Deposit date(DEPOSIT_TRAN_DATE) field - iTrack 1323 Notes By Paula Dated 18-July-2011
    $("#tdDEPOSIT_TRAN_DATE").hide();
    $("#tdDEPOSIT_TRAN_DATE1").hide();
    fnShowAndHideValidator(rfvDEPOSIT_TRAN_DATE, false);
    fnShowAndHideValidator(csvDEPOSIT_TRAN_DATE, false);
    fnShowAndHideValidator(csvDEPOSIT_TRAN_DATE2, false);
    fnShowAndHideValidator(rfvDEPOSIT_TRAN_DATE, false);
    //Till Hire

    
    $("#cmbDEPOSIT_TYPE").change(function () {
        $("#hidDEPOSIT_TYPE").val($("#cmbDEPOSIT_TYPE option:selected").val());
    });

});
		function AddData() {
		    
			document.getElementById('hidDEPOSIT_ID').value	=	'New';
			document.getElementById('txtDEPOSIT_TRAN_DATE').value = '<%=DateTime.Now.ToShortDateString()%>';
			document.getElementById('hidDEPOSIT_TRAN_DATE').value = '<%=DateTime.Now.ToShortDateString()%>';
		 
			$("#lblCREATED_DATETIME").text('<%=DateTime.Now.ToShortDateString()%>');
			        
		 
			document.getElementById('lblTOTAL_DEPOSIT_AMOUNT').innerHTML = "";
			document.getElementById('txtDEPOSIT_NOTE').value  = '';
			document.getElementById('cmbRECEIPT_MODE').selectedIndex = 1;
			document.getElementById('cmbDEPOSIT_TYPE').selectedIndex = 1;

			$("#hidDEPOSIT_TYPE").val($("#cmbDEPOSIT_TYPE option:selected").val());
			
			if(document.getElementById('btnImport')!=null)
			document.getElementById('btnImport').setAttribute('disabled',true);
			//Defaulting the fiscal year
			SetFiscalYear();
			
			ChangeColor();
			DisableValidators();
			if (document.getElementById('cmbFISCAL_ID').enabled)
				document.getElementById('cmbFISCAL_ID').focus();
			document.getElementById('hidFormSaved').value = 0;	
			document.getElementById("lblRTL_FILE").style.display='inline';
			document.getElementById("hidRTL_FILE").value="Y";	
			FormatAmount(document.getElementById('lblACCOUNT_BALANCE'));	
		}
		
				
		//Thi function shows or hide the delete button
		function ShowDeleteButton()
		{
			if(document.getElementById('hidOldData').value == "")
			{
				document.getElementById('btnDelete').style.display = "none";
			}
			else
			{
				document.getElementById('btnDelete').style.display = "inline";
			}
		}
		
		function populateXML()
		{	
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{
				if(document.getElementById('hidOldData').value != "") {

				     
				    var DEPOSIT_TRAN_DATE = $("#hidDEPOSIT_TRAN_DATE").val();
				    var CREATED_DATETIME = $("#hidCREATED_DATETIME").val();
					
				    populateFormData(document.getElementById('hidOldData').value, ACT_CURRENT_DEPOSITS);
				  
					
					var fileName1 = document.getElementById("lblRTL_FILE").innerText;
					var RootPath = document.getElementById('hidRootPath').value;
					var DepID = document.getElementById('hidDEPOSIT_ID').value;

			 
					$("#txtDEPOSIT_TRAN_DATE").val(DEPOSIT_TRAN_DATE);
					$("#lblCREATED_DATETIME").text(CREATED_DATETIME);
					$("#hidDEPOSIT_TRAN_DATE").val(DEPOSIT_TRAN_DATE)
					$("#hidCREATED_DATETIME").val(CREATED_DATETIME)
					if(fileName1 != "")
					{
						document.getElementById("txtRTL_FILE").style.display='none';
						document.getElementById("hidRTL_FILE").value="N";
					}
					else
						document.getElementById("hidRTL_FILE").value="Y";
						document.getElementById("lblRTL_FILE").style.display='inline';
						//document.getElementById("lblRTL_FILE").innerHTML ="<a href = '" + RootPath +  "/" + DepID + "_" + fileName1 + "' target='blank'>" + fileName1 + "</a>";
						document.getElementById("lblRTL_FILE").innerHTML ="<a href = '" + document.getElementById("hidfileLink").value + "' target='blank'>" + fileName1 + "</a>";
					//	ChangeAccountBalance(); //added to display amt and Deposit Number:
				}
				else
				{
					AddData();
				}
			}
		
			SetTab();
			return false;
		}
		
		/////AJAX Function Added By Raghav #5013 For Itrack 
		//TO MOVE TO LOCAL VSS
		function FetchAjaxResponse()
		{
		    
		   var acct_id =  document.getElementById('cmbACCOUNT_ID').options[document.getElementById('cmbACCOUNT_ID').options.selectedIndex].value;
		   var Fiscal_id =  document.getElementById('cmbFISCAL_ID').options[document.getElementById('cmbFISCAL_ID').options.selectedIndex].value;
		   //alert(acct_id + ' : ' +  Fiscal_id)
		   AddDeposit.AjaxFunction(acct_id,Fiscal_id,AjaxCallFunction_CallBack);
		}
		
		function AjaxCallFunction_CallBack(response)
		{
			//alert(response.value)
			var tmp = response.value.split(";");
			document.getElementById("txtDEPOSIT_NUMBER").value = tmp[0];
			document.getElementById("hidDEPOSIT_NUMBER").value = tmp[0];
			document.getElementById("lblACCOUNT_BALANCE").innerText = tmp[1];	
			document.getElementById('hidAccountBalance').value = tmp[1];
			document.getElementById('cmbACCOUNT_ID').disabled = false;
			//Formating the Amount
			FormatAmount(document.getElementById("lblACCOUNT_BALANCE"));		
		}
		
		
		/////END AJAX
		
		function CallbackFun(AJAXREsponse) //not calling
		{
			//alert(AJAXREsponse)
			if(AJAXREsponse != "")
			{				
				var newval = document.getElementById("txtDEPOSIT_NUMBER").value;
				
				var tmp = AJAXREsponse.split(";");				
				document.getElementById("txtDEPOSIT_NUMBER").value = tmp[0];
				document.getElementById("hidDEPOSIT_NUMBER").value = tmp[0];
				document.getElementById("lblACCOUNT_BALANCE").innerText = tmp[1];	
				document.getElementById('hidAccountBalance').value = tmp[1];
				document.getElementById('cmbACCOUNT_ID').disabled = false;
				//Formating the Amount
				FormatAmount(document.getElementById("lblACCOUNT_BALANCE"));
				
			}	
		}
	
		function ChangeAccountBalance()
		{
		DepositNo();
		//Callin Ajax Function 
		//FetchAjaxResponse();		
			
			/*var ParamArray = new Array();
			obj1=new Parameter('ACC_ID',document.getElementById('cmbACCOUNT_ID').value);
			alert(document.getElementById('cmbACCOUNT_ID').value)			
			obj2=new Parameter('FISC_ID',document.getElementById('cmbFISCAL_ID').value);
			ParamArray.push(obj1);
			ParamArray.push(obj2);
			var objRequest = _CreateXMLHTTPObject();
			var Action = 'ACC_BAL';
			_SendAJAXRequest(objRequest,'ACC_BAL',ParamArray,CallbackFun);*/
		}
		function DepositNo()
		{ 
		   var Account_id = document.getElementById('cmbACCOUNT_ID').options[document.getElementById('cmbACCOUNT_ID').options.selectedIndex].value;
		   var Deposit_no = document.getElementById("txtDEPOSIT_NUMBER").value;
		   var Deposit_no = document.getElementById("hidDEPOSIT_NUMBER").value;
		    		   
		   var oldDeposit_no = document.getElementById('hidDEPOSIT_NO').value;		    
		   var oldAccount_id = document.getElementById('hidACCOUNT_ID').value;		     
		   var oldAccount_bal =  document.getElementById('hidBalance').value;    

		   if(Account_id == oldAccount_id)
		   {  
		       document.getElementById("txtDEPOSIT_NUMBER").value = oldDeposit_no;
		       document.getElementById("lblACCOUNT_BALANCE").innerHTML = oldAccount_bal;
		       
		   }
		   else
			FetchAjaxResponse();
	 
		}
		
		//This function calls on change of fiscal id
		function cmbFISCAL_ID_Change()
		{
			if 	(document.getElementById("cmbFISCAL_ID").selectedIndex == 0)
			{
				return false;
			}

}

        // function to display RTL only if Receipt type mode is 'Check'
		function fxnShowHide()
		{
			var modeType = document.getElementById('cmbRECEIPT_MODE').value;
			if (modeType == 11975 || modeType == 14919) // Check
			{
				document.getElementById('trRTL_FILE').style.display = 'inline';
			}
			else
			{
				document.getElementById('trRTL_FILE').style.display = 'none';
			}
			
		}
		// confirms user action to commit the selected deposit or not.
        //this function modified for itrack - 1049 - by pradeep kushwaha on 18-aug-2011
		function confirmCommit() {
        
            var str = document.getElementById('hidMessage').value;
		    //var confirmAction = confirm("Do you really want to commit this deposit?");
		    
            var DEPOSIT_ID = document.getElementById("hidDEPOSIT_ID").value;
		    var DEPOSIT_TYPE = $("#cmbDEPOSIT_TYPE option:selected").val();
		    if (DEPOSIT_TYPE == "14831" || DEPOSIT_TYPE == "14832")//Normal or Co-Insurance
             {
		        var result = AddDeposit.AjaxGetDepositDetails(DEPOSIT_ID, DEPOSIT_TYPE);

		        if (result.value == "true") {

		            var str = "/cms/Account/Aspx/DepositMessageDetails.aspx?DEPOSIT_ID=" + DEPOSIT_ID + "&DEPOSIT_TYPE=" + DEPOSIT_TYPE;
		            var retval = window.showModalDialog(str, "DepositMessageDetails", "dialogWidth:800px;dialogHeight:500px", window)
		            if (retval == "SubmitAnyway")
		                return true;
		            else
		                return false;
		        }
		        else {

		            var confirmAction = confirm(str); // confirm("Do you really want to Send this deposit to Commit?");
		            if (confirmAction) {
		                return true;
		            }
		            else
		                return false;
		        }
		    }
            else {
		        var confirmAction = confirm(str); // confirm("Do you really want to Send this deposit to Commit?");
		        if (confirmAction) {
		            return true;
		        }
		        else
		            return false;
		    }

		}	
		//till here 
		//Show Hide
		function HideImportProgress()
		{
			document.getElementById('btnImportProgress').style.display="none";
		}
		function HideShowImportInProgress()
		{
			document.getElementById('btnImportProgress').style.display="inline";
			document.getElementById('btnImportProgress').disabled = true;
			document.getElementById('btnImport').style.display="none";
			
		}
		//Amount Balance Formating on ADD NEW
		//Formats the amount and convert 111 into 1.11
		function FormatAmount(lblAmount)
		{
			if (lblAmount.innerText != "")
			{
				amt = lblAmount.innerText;
				amt = ReplaceAll(amt,".","");
				if (amt.length == 1)
					amt = amt + "0";
				if ( ! isNaN(amt))
				{
					DollarPart = amt.substring(0, amt.length - 2);
					CentPart = amt.substring(amt.length - 2);
					lblAmount.innerText = InsertDecimal(amt);
				}
			}
		}
		function ShowExceptionReport() {
            //Modified for Accepted Co-Insurance deposit to show the exception By Pradeep Kushwaha on 03-Aug-2011 1363/1148
		    var str = "/cms/Account/Aspx/ExceptionItemsDetails.aspx?DEPOSIT_ID=" + document.getElementById("hidDEPOSIT_ID").value + "&DEPOSIT_TYPE=" + $("#cmbDEPOSIT_TYPE option:selected").val();
            		   
		    window.open(str, "ExceptionReport", "resizable=yes,toolbar=0,location=0, directories=0, status=0, menubar=0,scrollbars=yes,width=800,height=500,left=150,top=50")
		   
		  return false;
		}
		
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="populateXML();ApplyColor();fxnShowHide();HideImportProgress();">
		<FORM id="ACT_CURRENT_DEPOSITS" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<TD class="pageHeader" colSpan="4"><asp:Label ID="capMandatoryNotes" runat="server"></asp:Label></TD></tr>
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></td></tr>
				<TR>
					<TD>
						<TABLE id="tblBody" width="100%" align="center" border="0" runat="server">
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capFISCAL_ID" runat="server">General Ledger</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%" ><asp:dropdownlist id="cmbFISCAL_ID" runat="server" onchange="javascript:if (cmbFISCAL_ID_Change() == false) return false;" AutoPostBack="true">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR><asp:requiredfieldvalidator id="rfvFISCAL_ID" runat="server" Display="Dynamic" ErrorMessage="FISCAL_ID can't be blank." ControlToValidate="cmbFISCAL_ID"></asp:requiredfieldvalidator></TD>
									<TD class="midcolora" width="18%"><asp:label id="capDEPOSIT_TYPE" runat="server">Deposit Type</asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%" colspan=3><asp:dropdownlist id="cmbDEPOSIT_TYPE" 
                                        runat="server" >
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR></TD>
								</tr>
								<tr>
									<TD class="midcolora" width="18%">
									<asp:label id="capACCOUNT_ID" runat="server">Bank Account</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%" colspan=3>
									<asp:dropdownlist id="cmbACCOUNT_ID" runat="server" 
                                            onchange="ChangeAccountBalance();" Height="16px" Width="770px">
										<asp:ListItem Value='0'></asp:ListItem>
									</asp:dropdownlist><BR><asp:requiredfieldvalidator id="rfvACCOUNT_ID" runat="server" Display="Dynamic" ErrorMessage="ACCOUNT_ID can't be blank." ControlToValidate="cmbACCOUNT_ID"></asp:requiredfieldvalidator></TD>
								</tr>
								<tr >
									<TD class="midcolora" width="18%">
									<asp:label id="capDEPOSIT_NUMBER" runat="server">Deposit Number</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" width="32%"><asp:textbox id="txtDEPOSIT_NUMBER" runat="server" ReadOnly="True" maxlength="4" size="10"></asp:textbox><BR><asp:requiredfieldvalidator id="rfvDEPOSIT_NUMBER" runat="server" Display="Dynamic" ErrorMessage="DEPOSIT_NUMBER can't be blank." ControlToValidate="txtDEPOSIT_NUMBER"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="revDEPOSIT_NUMBER" runat="server" Display="Dynamic" ErrorMessage="RegularExpressionValidator" ControlToValidate="txtDEPOSIT_NUMBER"></asp:regularexpressionvalidator>
									</TD>
									<%--Hide Deposit date(DEPOSIT_TRAN_DATE) field - iTrack 1323 Notes By Paula Dated 18-July-2011--%>
									<TD class="midcolora" id="td1" width="18%">
									</TD>
									<TD class="midcolora" id="td2" width="32%">
									</TD>
									<TD class="midcolora" id="tdDEPOSIT_TRAN_DATE" width="18%"><asp:label id="capDEPOSIT_TRAN_DATE" runat="server">Deposit Date</asp:label><span class="mandatory">*</span></TD>
									<TD class="midcolora" id="tdDEPOSIT_TRAN_DATE1" width="32%">
									<asp:textbox id="txtDEPOSIT_TRAN_DATE" runat="server" maxlength="10" size="12" ></asp:textbox>
									 <asp:hyperlink id="hlkDEPOSIT_TRAN_DATE" runat="server" CssClass="HotSpot">
										<asp:image id="imgDEPOSIT_TRAN_DATE" runat="server" ImageUrl="../../cmsweb/Images/CalendarPicker.gif"></asp:image>
									</asp:hyperlink><BR>
									<asp:requiredfieldvalidator id="rfvDEPOSIT_TRAN_DATE" runat="server"  Display="Dynamic" ErrorMessage="" ControlToValidate="txtDEPOSIT_TRAN_DATE"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revDEPOSIT_TRAN_DATE" runat="server" Display="Dynamic" ErrorMessage="" ControlToValidate="txtDEPOSIT_TRAN_DATE"></asp:regularexpressionvalidator>
									<asp:customvalidator id="csvDEPOSIT_TRAN_DATE" runat="server" Display="Dynamic" ErrorMessage="TRANS_DATE can't be blank." ControlToValidate="txtDEPOSIT_TRAN_DATE" ClientValidationFunction="ChkFiscalDate"></asp:customvalidator>
									<asp:customvalidator id="csvDEPOSIT_TRAN_DATE2" runat="server" Display="Dynamic"  ControlToValidate="txtDEPOSIT_TRAN_DATE" ClientValidationFunction="ChkFutureDate"></asp:customvalidator>
									<%--till here --%>
									
									</TD></tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capRECEIPT_MODE" runat="server"></asp:label><span class="mandatory">*</span></TD>
								<TD class="midcolora" width="32%"><asp:dropdownlist id="cmbRECEIPT_MODE" runat="server" OnChange="fxnShowHide();"></asp:dropdownlist><BR><asp:requiredfieldvalidator id="rfvRECEIPT_MODE" runat="server" Display="Dynamic" ControlToValidate="cmbRECEIPT_MODE"></asp:requiredfieldvalidator></TD>
								<td class="midcolora" colSpan="2"></td></tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capCREATED_DATETIME" runat="server">Deposit Created Date</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblCREATED_DATETIME" runat="server" CssClass="LabelFont"></asp:label></TD>
								<td class="midcolora"><asp:label id="capACCOUNT_BALANCE" runat="server" >Account Balance</asp:label></td>
								<td class="midcolora"><asp:label id="lblACCOUNT_BALANCE" CssClass="LabelFont" Runat="server"></asp:label></td></tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTOTAL_DEPOSIT_AMOUNT" runat="server">Total Deposit Amount</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblTOTAL_DEPOSIT_AMOUNT" runat="server" CssClass="LabelFont"></asp:label></TD>
								<TD class="midcolora" width="18%"><asp:label id="capTapeTotalCust" runat="server">Total Customer Receipts Amount</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblTAPE_TOTAL_CUST" runat="server" CssClass="LabelFont"></asp:label></TD></tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTapeTotalAgency" runat="server">Total Agency Receipts Amount</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblTOTAL_AGENCY" runat="server" CssClass="LabelFont"></asp:label></TD>
								<TD class="midcolora" width="18%"><asp:label id="capTapeTotalClaim" runat="server">Total Claim & Reinsurance Receipts Amount</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblTAPE_TOTAL_CLM" runat="server" CssClass="LabelFont"></asp:label></TD></tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capTotalMisc" runat="server">Total Miscellaneous Receipts Amount</asp:label></TD>
								<TD class="midcolora" width="32%"><asp:label id="lblTAPE_TOTAL_MISC" runat="server" CssClass="LabelFont"></asp:label></TD>
								<TD class="midcolora" colSpan="3"></TD></tr>
							<tr>
								<TD class="midcolora" width="18%"><asp:label id="capDEPOSIT_NOTE" runat="server">Note</asp:label></TD>
								<TD class="midcolora" colSpan="3"><asp:textbox id="txtDEPOSIT_NOTE" runat="server" maxlength="250" size="100"></asp:textbox></TD></tr>
							<tr id="trRTL_FILE">
								<td class="midcolora" colSpan="1"><asp:label id="capRTL_FILE" Runat="server">Import customer deposits from RTL output file</asp:label></td>
								<td class="midcolora" colSpan="3"><input id="txtRTL_FILE" type="file" size="65" runat="server"> <asp:label id="lblRTL_FILE" Runat="server"></asp:label>&nbsp;&nbsp;
									<cmsb:cmsbutton class="clsButton" id="btnImport" runat="server" Text="Import File"></cmsb:cmsbutton>
									<asp:Button class="clsButton" id="btnImportProgress" runat="server" Text="Importing..."></asp:Button>
								</td></tr>
							<tr>
								<td class="midcolora" colSpan="2"><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete" CausesValidation="False"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnCopy" runat="server" Visible="false" Text="Copy"></cmsb:cmsbutton></td>
								<td class="midcolorr" colSpan="2">
								<cmsb:cmsbutton class="clsButton" id="btnShowExceptionItems" runat="server" Text="Exception Items Report" OnClientClick="javascript:return ShowExceptionReport()"></cmsb:cmsbutton>
								<cmsb:cmsbutton class="clsButton" id="btnCommit" runat="server" Text="Send To Commit"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnNext" runat="server" Text="Next"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton>
								</td></tr></TABLE></TD></TR></TABLE>
			<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
			<INPUT id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidDEPOSIT_ID" type="hidden" name="hidDEPOSIT_ID" runat="server">
			<input id="hidCashAccountXML" type="hidden" name="hidCashAccountXML" runat="server">
			<input id="hidDEPOSIT_TYPE" type="hidden" name="hidDEPOSIT_TYPE" runat="server">
			<input id="hidDelete" type="hidden" name="hidDelete" runat="server">
			<input id="hidRTL_FILE" type="hidden" name="hidRTL_FILE" runat="server">
			<INPUT id="hidRootPath" type="hidden" name="hidRootPath" runat="server">
			<INPUT id="hidAccountBalance" type="hidden" name="hidAccountBalance" runat="server">
			<INPUT id="hidACCOUNT_ID" type="hidden" value="" name="hidACCOUNT_ID" runat="server">
			<INPUT id="hidDEPOSIT_NO" type="hidden" value="" name="hidDEPOSIT_NO" runat="server">
			<INPUT id="hidBalance" type="hidden" value="" name="hidBalance" runat="server">
			<INPUT id="hidfileLink" type="hidden" value="" name="hidfileLink" runat="server">
			<INPUT id="hidDEPOSIT_TRAN_DATE" type="hidden" value="" name="hidDEPOSIT_TRAN_DATE" runat="server">
			<input id="hidDEPOSIT_NUMBER" type="hidden" value="" name="hidDEPOSIT_NUMBER" runat="server">
			<input id="hidTAB_TITLES" type="hidden" value="0" name="hidTAB_TITLES" runat="server"/>         
			<input id="hidMessage" type="hidden" runat="server" />
			<input id="hidCREATED_DATETIME" type="hidden"  name="hidCREATED_DATETIME" runat="server"/> 
			<input id="hidVal" type="hidden" runat="server" />        
			
		</FORM>
		<script>
				RefreshWebGrid(document.getElementById('hidFormSaved').value,document.getElementById('hidDEPOSIT_ID').value, false);
		</script>

	</BODY>
</HTML>
