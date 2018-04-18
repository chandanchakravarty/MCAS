<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="UserQuestion.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.UserDefined.UserQuestion" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BRICS-Preview Questions </title>
		<LINK href="../stylesheets/river.css" rel="stylesheet">
			<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
				<script src="/cms/cmsweb/scripts/common.js"></script>
				<script src="/cms/cmsweb/scripts/form.js"></script>
				<script language="javascript" src="/cms/cmsweb/Scripts/Calendar.js"></script>
				<script language="javascript">
				function duplicateRows(tblObj){
			
			var objFirstTR ;
			var objNewTR ;

			objFirstTR = tblObj.rows(tblObj.rows.length-1);
			objNewTR = objFirstTR.cloneNode(true);
			
			
			var inputColl = objNewTR.getElementsByTagName('input');
			for(i = 0 ; i < inputColl.length;i++){
				if (inputColl[i].type != 'submit'){
					inputColl[i].value = '';
				}
			}
			
			objFirstTR.cells[objFirstTR.cells.length-1].innerHTML='<input type=button class=clsbutton value="  Delete  " onclick="deleteRow(this.parentElement.parentElement.parentElement,this.parentElement.parentElement)">';
			tblObj.appendChild(objNewTR);
			return false;
		}	
		
		function deleteRow(tableobj,rowobj){
			tableobj.removeChild(rowobj);
		}
		
		function OnFormSubmit(){
			var checkboxlist = '<%=lsCheckBoxesLIst%>';
			var arraycheckbox= checkboxlist.split('#');
			for(var i=0;i<arraycheckbox.length;i++){
				var hidControlId= arraycheckbox[i];
				if (hidControlId != '') {
						var chkboxValues = document.getElementById(hidControlId).value.split(',');
						
						var lengthChkList = hidControlId.split('~')[1];
						var chkBoxName = hidControlId.split('~')[0];
						
						var selectedValues = '';
						
						for(var j=0;j<parseInt(lengthChkList);j++){
							if (document.getElementById(chkBoxName + '_' + j).checked){
								selectedValues = selectedValues + chkboxValues[j];
							}
						}
						if(selectedValues != ''){
							document.getElementById(hidControlId).value =selectedValues
						}
					}
			}
			
		}
		
		function fnShowControl(cmb,val,txtOthId){
			if (cmb.value == val){
				document.all(txtOthId).style.display='inline';
				var myControls = document.all(txtOthId).parentElement.getElementsByTagName('span');
				for(var i=0;i<myControls.length;i++){
					if(myControls[i].enabled == false){
						myControls[i].enabled=true;
						//ValidatorEnable(myControls[i],true)
					}
				}
				
			}else{
				
				var myControls = document.all(txtOthId).parentElement.getElementsByTagName('span');
				for(var i=0;i<myControls.length;i++){
					if(myControls[i].enabled == true){
						ValidatorEnable(myControls[i],false)
					}
				}
				document.all(txtOthId).style.display='none';
			}
		}
		
		function clearValues(objNewTR){

			var objInput, colObjects , selObjects;
			var lngLength;

			colObjects = objNewTR.getElementsByTagName("INPUT");
			for (lngLength = 0 ; lngLength < colObjects.length ; lngLength++){
				if(colObjects[lngLength].type != "button")
					colObjects[lngLength].value = "";
			}
		}
		function fnClose()
		{	
			parent.window.close();
		}		
				</script>
	</HEAD>
	<body class="bodyBackGround" leftMargin="0" topMargin="0" onload='ApplyColor();'>
		<form id="UserQuestion" method="post" runat="server">
			<!--<asp:Table id="Table2" runat="server"></asp:Table>-->
			<asp:table id="tblUDScreen" runat="server" Width="100%" GridLines="None" class="tableWidthHeader"
				cellSpacing="1" cellPadding="0" border="0" align="center">
				<asp:TableRow>
					<asp:TableCell class="headereffectCenter" ColumnSpan="4" Height="24px" HorizontalAlign="Center">
						<%=gstrScreenName%>
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow>
					<asp:TableCell CssClass="midcolorc" ColumnSpan="4" HorizontalAlign="Center">
						<asp:Label ID="lblErroMsg" Runat="server" Visible="False" CssClass="errmsg"></asp:Label>
					</asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td align="left" class="midcolora">
						<cmsb:CmsButton id="btnBack" runat="server" CssClass="clsbutton"></cmsb:CmsButton>
					</td>
					<td align="right" class="midcolorr">
						<cmsb:CmsButton id="btnSave" runat="server" CssClass="clsbutton"></cmsb:CmsButton>
					</td>
				</tr>
			</table>
			<asp:label id="lblComIds" Runat="server" Visible="False"></asp:label>
			<asp:label id="lblPrevQID" runat="server" Visible="False"></asp:label>
			<asp:label id="lblTabID" runat="server" Visible="False" Font-Bold="True"></asp:label>
			<asp:label id="lblhidScreenID" runat="server" Visible="False"></asp:label>
		</form>
	</body>
</HTML>
