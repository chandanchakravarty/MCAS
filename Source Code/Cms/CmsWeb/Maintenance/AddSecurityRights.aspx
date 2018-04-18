<%@ Page language="c#" Codebehind="AddSecurityRights.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddSecurityRights" validateRequest="false"%>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AddRightsUserType</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type='text/css' rel='stylesheet'>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		
		<script language="javascript">
		/*function onRowClicked(num,msDg )
			{
				rowNum=num;
				if(parseInt(num)==0)
					strXML="";
				populateXML(num,msDg);		
				changeTab(0, 0);
			}*/
		function AddData()
		{
			ChangeColor();
			DisableValidators();
		}
		function populateXML()
		{
			//FillComboSection();
			if(document.getElementById('hidFormSaved').value == '0')
			{
				if(document.getElementById('hidOldDatas').value == "")
				{
					AddData();
				}
				else
				{
					populateData();
				}
			}
			if(document.getElementById('screenRights').innerText == "")
				document.getElementById('screenButtons').style.display = 'none';
			return false;
		}
		var treeMenu;
		// menu data is passed to create menu on the 2 frames
		function createMenuDataSecurity()
		{
			var tree = document.getElementById('hidMENU_XML').value;
			//alert(tree);
			if (tree == null || tree == 'undefined') return;
			var objXmlHandler = new XMLHandler();
			treeMenu = objXmlHandler.quickParseXML(tree).getElementsByTagName('button');
		}
		

		function FillComboSection() {
			for(var i=0; i<treeMenu.length; i++)
			{
				AddComboOption("cmbSECTION",treeMenu[i].menu_id,treeMenu[i].name);
			}
			
			if(document.getElementById('hidSECTION').value != 0)
			{
			
				SelectComboOption('cmbSECTION',document.getElementById('hidSECTION').value);
				FillComboSubsection();
				if(document.getElementById('hidSUBSECTION').value != 0)
				{
					SelectComboOption('cmbSUBSECTION',document.getElementById('hidSUBSECTION').value);
				}
			}
			else
			{
				FillComboSubsection();
			}
		}
		
		function FillComboSubsection()
		{
			document.getElementById('cmbSUBSECTION').options.length = 0;
			var i;
			var menuNumber	=	-1;
			var menu_id		=	document.getElementById('cmbSECTION').options[document.getElementById('cmbSECTION').options.selectedIndex].value;
			var menu_text = document.getElementById('cmbSECTION').options[document.getElementById('cmbSECTION').options.selectedIndex].text;
			var All = document.getElementById('hidAll').value;
			AddComboOption("cmbSUBSECTION", menu_id, All);
			if(menu_text != 'Agent Home')
			{
				for(i=0; i<treeMenu.length; i++)
				{
					if(treeMenu[i].menu_id == menu_id)
						menuNumber = i;
				}
				var menuLength = treeMenu[menuNumber].childNodes.length;
				for(i=0; i<menuLength; i++)
				{
					AddComboOption("cmbSUBSECTION",treeMenu[menuNumber].childNodes[i].menu_id,treeMenu[menuNumber].childNodes[i].name);				
				}
			}
			else
			{
				AddComboOption("cmbSUBSECTION",menu_id,menu_text);				
			}
			/*if(i == 0)
			{
				AddComboOption("cmbSUBSECTION",menu_id,menu_text);
			}*/
            EnableDisableAppPolCombo();
            

		}
		
		function EnableDisableAppPolCombo()
		{
						
			combo=document.getElementById('cmbSUBSECTION');				
			
			if((combo.options[combo.selectedIndex].value=="116" ||  document.getElementById('hidSUBSECTION').value=="116") && document.getElementById('cmbSECTION').selectedIndex=="1" )
			{				
				document.getElementById("spnAPPPOL").style.display="inline";
				document.getElementById("cmbAPPPOL").style.display="inline";
				document.getElementById("capAppPol").style.display="inline";
				document.getElementById("rfvAPPPOL").style.display="inline";
				document.getElementById("rfvAPPPOL").setAttribute("enabled",true);
				//document.getElementById("rfvAPPPOL").setAttribute('isValid',true);				
				if(document.getElementById("cmbAPPPOL").options.length==0)
				{
					//AddComboOption("cmbAPPPOL",0,'');				
					//AddComboOption("cmbAPPPOL",1,'Application');				
					//AddComboOption("cmbAPPPOL",2,'Policy');
					if(document.getElementById('hidAppPol').value!="0")
						document.getElementById("cmbAPPPOL").selectedIndex=(document.getElementById('hidAppPol').value - 1);		
					else
						document.getElementById("cmbAPPPOL").selectedIndex				
				
				}		
			}
			else				
			{
				//document.getElementById("rfvAPPPOL").setAttribute('isValid',false);
				document.getElementById("rfvAPPPOL").setAttribute("enabled",false);
				document.getElementById("rfvAPPPOL").style.display="none";
				document.getElementById("cmbAPPPOL").style.display="none";
				document.getElementById("capAppPol").style.display="none";
				document.getElementById("spnAPPPOL").style.display="none";
}
		}
		function populateData() {
		   
			var tree			=	document.getElementById('hidOldDatas').value;
			//document.write (tree);
			//alert(tree)
			if (tree == null || tree == 'undefined')return;
			var objXmlHandler	=	new XMLHandler();
			var screenList		=	{};
			
			screenList			=	objXmlHandler.quickParseXML(tree).getElementsByTagName('screen');
			populateScreenData(screenList);
		}
		function populateScreenData(screenList)
		{
			var objXmlHandler	=	new XMLHandler();
			var permissionXML	=	{};
			var strHTML			=	'<TABLE cellSpacing=0 cellPadding=0 width=100% border=0>';
			var header, prevHeader, secHeader, subSecHeader, subSubSecHeader, subSubSubSecHeader, screenIds, typeId;
			var read = document.getElementById('hidread').value;
			var write = document.getElementById('hidwrite').value;
			var delet = document.getElementById('hiddelete').value;
			var execute = document.getElementById('hidexecute').value;
			header = '';
			prevHeader = '';
			screenIds = '';
			var i,j,k,l;
			
			for(i = 0; i < screenList.length; i++)
			{
				secHeader = '';
				subSecHeader = '';
				subSubSecHeader = '';
				subSubSubSecHeader = '';
				//New header added for policy
				subPolicyHeader = '';
				
				screenIds += screenList[i].screen_id + '~';
				if(screenList[i].level1_id)
				{
					if(secHeader != screenList[i].level1)
						secHeader	=	screenList[i].level1;
				}
				if(screenList[i].level2_id)
				{
					if(subSecHeader != screenList[i].level2)
						subSecHeader	=	screenList[i].level2;
				}
				if(screenList[i].level3_id)
				{
					if(subSubSecHeader != screenList[i].level3)
						subSubSecHeader	=	screenList[i].level3;
				}
				if(screenList[i].level4_id)
				{
					if(subSubSubSecHeader != screenList[i].level4)
						subSubSubSecHeader	=	screenList[i].level4;
				}
				//Header for policy
				if(screenList[i].module_name)
				{
					if(subPolicyHeader != screenList[i].module_name)
						subPolicyHeader	=	screenList[i].module_name;
				}
				//////////
				header = secHeader;
				if(subSecHeader != "")	header += "&nbsp;&gt;&gt;&nbsp;" + subSecHeader;
				if(subSubSecHeader != "")	header += "&nbsp;&gt;&gt;&nbsp;" + subSubSecHeader;
				if(subSubSubSecHeader != "")	header += "&nbsp;&gt;&gt;&nbsp;" + subSubSubSecHeader;
				
				//Policy header				
				//if(subPolicyHeader != "")	header += "&nbsp;(" + subPolicyHeader + ")";
				//alert(header);
				//
				if(header != prevHeader)
				{
					prevHeader		=	header;
					
					if(secHeader != "")			typeId = screenList[i].level1_id;
					if(subSecHeader != "")		typeId = screenList[i].level2_id;
					if(subSubSecHeader != "")	typeId = screenList[i].level3_id;
					if(subSubSubSecHeader != "")typeId = screenList[i].level4_id;
					
					//Done for Itrack Issue 6539 on 9 Oct 09 -- To show menu heading only when there is screen_id associated with it
					if(screenList[i].screen_id !="")
					{
						strHTML			+=	'<TR><TD class="pageHeader" width="60%">' + header + '</TD>';
						strHTML         += '<TD class="pageHeader" width="10%"><input type=checkbox name=like' + typeId + '_R onclick="checkAll(this)">'+ read +'</TD>'
						strHTML			+=	'<TD class="pageHeader" width="10%"><input type=checkbox name=like' + typeId + '_W onclick="checkAll(this)">'+ write +'</TD>';
						strHTML			+=	'<TD class="pageHeader" width="10%"><input type=checkbox name=like' + typeId + '_D onclick="checkAll(this)">'+ delet +'</TD>';
						strHTML			+=	'<TD class="pageHeader" width="10%"><input type=checkbox name=like' + typeId + '_E onclick="checkAll(this)">'+ execute +'</TD>';
						strHTML			+=	'</TR>';
					}
				}
				strHTML			+=	'<TR><TD class=midcolora style="padding-left:40px">' + screenList[i].screen_desc + '</TD>';
				if(screenList[i].screen_read == "True")
				{
					if(screenList[i].permission_read == 'Y')
					    strHTML += '<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_read checked onClick="checkSelf(this)">' + read + '</TD>';
					else
					    strHTML += '<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_read onClick="checkSelf(this)">' + read + '</TD>';
				}
				else
				{
					strHTML		+=	'<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_read style="display:none"> </TD>';
				}
				if(screenList[i].screen_write == "True")
				{
					if(screenList[i].permission_write == 'Y')
					    strHTML += '<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_write checked onClick="checkRead(this)">' + write + '</TD>';
					else
					    strHTML += '<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_write onClick="checkRead(this)">' + write + '</TD>';
				}
				else
				{
					strHTML		+=	'<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_write style="display:none"> </TD>';
				}
				if(screenList[i].screen_delete == "True")
				{
					if(screenList[i].permission_delete == 'Y')
					    strHTML += '<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_delete checked onClick="checkRead(this)">' + delet + '</TD>';
					else
					    strHTML += '<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_delete onClick="checkRead(this)">' + delet + '</TD>';
				}
				else
				{
					strHTML		+=	'<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_delete style="display:none"> </TD>';
				}
				if(screenList[i].screen_execute == "True")
				{
					if(screenList[i].permission_execute == 'Y')
					    strHTML += '<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_execute checked onClick="checkRead(this)">' + execute + '</TD>';
					else
					    strHTML += '<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_execute onClick="checkRead(this)">' + execute + '</TD>';
				}
				else
				{
					strHTML		+=	'<TD class=midcolora><input type=checkbox name=' + screenList[i].screen_id + '_execute style="display:none"> </TD>';
				}
				strHTML			+=	'</TR>';
			}
			strHTML				+=	'</TABLE>';
			
			document.getElementById('hidSCREENLIST').value	=	screenIds.substring(0,screenIds.length-1);
			document.getElementById('screenRights').innerHTML = strHTML;
			if(this.parent.parent.document.getElementById('tabLayer'))
			{
				this.parent.parent.document.getElementById('tabLayer').style.height = 1000 + document.getElementById('screenRights').offsetHeight + 250;//((i-1)*40 < 1000 ? 1000 : ((i-1)*40));
			}
			this.parent.document.getElementById('tabLayer').style.height = 1000 + document.getElementById('screenRights').offsetHeight + 250;//((i-1)*40 < 1000 ? 1000 : ((i-1)*40));

        }
        //Modified by Pradeep Kushwaha on 13-August-2010
		function checkAll(obj) {
			var checkBoxName	=	obj.name.substring(4,obj.name.length-1);
			var checkBoxType	=	obj.name.substring(obj.name.length-1,obj.name.length);
			
			var loop = new Array();

			if (checkBoxType == 'R') {
			    checkBoxType = '_read';

			    for (var count = 0; count < document.forms[0].elements.length; count++) {
		            var e = document.forms[0].elements[count];

		            if ((e.name.indexOf(checkBoxName) == 0) && (e.name.indexOf(checkBoxType) != -1) && (e.type == 'checkbox')) {
		        
		                if (obj.checked == true && checkBoxType == '_read')
		                    e.checked = true;  
		                else if (obj.checked == false && checkBoxType == '_read'){
                             var ids = e.name;
                             var writeid = ReplaceAll(ids, '_read', '_write');
		                     var deleteid = ReplaceAll(ids, '_read', '_delete');
		                     var executeid = ReplaceAll(ids, '_read', '_execute');

		                     if (document.getElementById(writeid).checked == false && document.getElementById(deleteid).checked == false && document.getElementById(executeid).checked == false) {
		                         e.checked = false; 
		                      }
		                }
		            }
		        }
			}
			if (checkBoxType == 'W') {
			   
			    for (var i = 0; i < document.forms[0].elements.length; i++) {
    	            var e = document.forms[0].elements[i];
    	            
    	            var checkBoxType = e.name.substring(e.name.lastIndexOf("_") + 1);
    	            if((e.name.indexOf(checkBoxName) == 0) && (e.type=='checkbox') && (checkBoxType=='read')){
    	                if(obj.checked)
    	           	    e.checked	=	true;
    	            }
		            checkBoxType = '_write';
		            if ((e.name.indexOf(checkBoxName) == 0) && (e.name.indexOf(checkBoxType) != -1) && (e.type == 'checkbox')) {
		                if (obj.checked) {
		                    e.checked = true;
		                    }
		                else if (obj.checked == false && checkBoxType == '_write') {
		                    var ids = e.name;
		                    var readid = ReplaceAll(ids, '_write', '_read');
		                    var deleteid = ReplaceAll(ids, '_write', '_delete');
		                    var executeid = ReplaceAll(ids, '_write', '_execute');
		                    if (document.getElementById(readid).checked == false && document.getElementById(deleteid).checked == false && document.getElementById(executeid).checked == false) {
		                        e.checked = false;

		                    }
		                }
		            }
			  }
			}
			if (checkBoxType == 'D') {
			    
			    for (var i = 0; i < document.forms[0].elements.length; i++) {
			        var e = document.forms[0].elements[i];
			        var checkBoxType = e.name.substring(e.name.lastIndexOf("_") + 1);
			        if ((e.name.indexOf(checkBoxName) == 0) && (e.type == 'checkbox') && (checkBoxType == 'read')) {
			            if (obj.checked)
			                e.checked = true;

			        }
			        checkBoxType = '_delete';    
			        if ((e.name.indexOf(checkBoxName) == 0) && (e.name.indexOf(checkBoxType) != -1) && (e.type == 'checkbox')) {
			            if (obj.checked == true && checkBoxType == '_delete')
			                e.checked = obj.checked;
			            else if (obj.checked == false && checkBoxType == '_delete') {
			                
			                var ids = e.name;
			                var readid = ReplaceAll(ids, '_delete', '_read');
			                var writeid = ReplaceAll(ids, '_delete', '_write');
			                var executeid = ReplaceAll(ids, '_delete', '_execute');

			                if (document.getElementById(readid).checked == false && document.getElementById(writeid).checked == false && document.getElementById(executeid).checked == false) {
			                    e.checked = false;
			                }

			            }
			        }
			    }
			}
			if (checkBoxType == 'E') 
			{
			   
			    for (var i = 0; i < document.forms[0].elements.length; i++) {
			        var e = document.forms[0].elements[i];
			        var checkBoxType = e.name.substring(e.name.lastIndexOf("_") + 1);
			        if ((e.name.indexOf(checkBoxName) == 0) && (e.type == 'checkbox') && (checkBoxType == 'read')) {
			            if (obj.checked)
			                e.checked = true;

			        }
			        checkBoxType = '_execute';
			        if ((e.name.indexOf(checkBoxName) == 0) && (e.name.indexOf(checkBoxType) != -1) && (e.type == 'checkbox')) {
			            if (obj.checked == true && checkBoxType == '_execute')
			                e.checked = obj.checked;
			            else if (obj.checked == false && checkBoxType == '_execute') {

			                var ids = e.name;
			                var readid = ReplaceAll(ids, '_execute', '_read');
			                var writeid = ReplaceAll(ids, '_execute', '_write');
			                var deleteid = ReplaceAll(ids, '_execute', '_delete');

			                if (document.getElementById(readid).checked == false && document.getElementById(writeid).checked == false && document.getElementById(deleteid).checked == false) {
			                    e.checked = false;
			                }

			            }
			        }
			    }
			} 

        }
		//Till here
		function checkRead(obj) {
		    
			var checkBoxName	=	obj.name.substring(0,obj.name.lastIndexOf("_"));
			
			for (var i=0;i<document.forms[0].elements.length;i++)
			{
				var e				=	document.forms[0].elements[i];
				var checkBoxType	=	e.name.substring(e.name.lastIndexOf("_")+1);
				
				
				if((e.name.indexOf(checkBoxName) == 0) && (e.type=='checkbox') && (checkBoxType=='read'))
				{
					if(obj.checked)
						e.checked	=	true;
					break;
				}
			}			
		}
 
		function checkSelf(obj) {
		   
		    var checkBoxName = obj.name.substring(0, obj.name.lastIndexOf("_"));

		    for (var i = 0; i < document.forms[0].elements.length; i++) {
		        var e = document.forms[0].elements[i];
		        var checkBoxType = e.name.substring(e.name.lastIndexOf("_") + 1);

		        if ((e.name.indexOf(checkBoxName) == 0) && (e.type == 'checkbox') && (checkBoxType != 'write' || checkBoxType != 'delete' || checkBoxType != 'execute')) {
		            if (e.checked) {
		                obj.checked = true;
		                break;
		            }
		        }
		    }
		}
		
		function checkComplete(obj)
		{
			for (var i=0;i<document.forms[0].elements.length;i++)
			{
				var e				=	document.forms[0].elements[i];
				if(e.type=='checkbox')		
					e.checked=obj.checked;
			}
}
function fireAutoSelection() {
    __doPostBack('btnGetScreen', 'OnClick');

}
		</script>
	</HEAD>
	<BODY  leftMargin='0' topMargin='0' onload='populateXML();ApplyColor();'>
		<form id="MNT_USER_TYPE_PERMISSION" method="post" runat="server">
			<TABLE cellSpacing='0' cellPadding='0' width='100%' border='0'>
				<TR>
					<TD>
						<TABLE width='100%' border='0' align='center'>
							<tr>
								<TD class="pageHeader" colSpan="4"><asp:Label ID="capMessages" runat="server"></asp:Label></TD>
							</tr>
							<tr>
								<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblMessage" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
							</tr>
							<tr>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capSECTION" runat="server">Select Section</asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList ID="cmbSECTION" runat="server" 
                                        onchange="FillComboSubsection();EnableDisableAppPolCombo();fireAutoSelection();"></asp:DropDownList>
								</TD>
								<TD class='midcolora' width='18%'>
									<asp:Label id="capSUBSECTION" runat="server">Subsection</asp:Label></TD>
								<TD class='midcolora' width='32%'>
									<asp:DropDownList ID="cmbSUBSECTION" runat="server" onChange="EnableDisableAppPolCombo();fireAutoSelection();"></asp:DropDownList>
								</TD>
							</tr>
							<tr> 
								<td class='midcolora'>
									<asp:Label ID="capSELECTALL" Runat="server">Select All</asp:Label>
								</td>
								<td class='midcolora'>
									<asp:CheckBox ID="chkSELECTALL" Runat="server"></asp:CheckBox>
								</td>
								<TD class='midcolorr' colspan="2">
									<table border=0>
										<tr>
											<TD class='midcolora' width='37%'>
												<asp:Label id="capAppPol" runat="server" Visible="True">Select App/Pol</asp:Label><span class="mandatory" id="spnAPPPOL">*</span></TD>
											<TD class='midcolora' width='38%'>
												<asp:DropDownList ID="cmbAPPPOL" Visible="True" runat="server"></asp:DropDownList><br>
												<asp:requiredfieldvalidator id="rfvAPPPOL" runat="server" ControlToValidate="cmbAPPPOL" ErrorMessage="Select Application/Policy."
																	Display="Static"></asp:requiredfieldvalidator>
											</TD>
											<TD class='midcolora' width='25%' align="right">
												<cmsb:cmsbutton class="clsButton" id='btnGetScreen' runat="server" text='Show Rights' ></cmsb:cmsbutton>
											</TD>
										</tr>
									</table>
									
								</TD>
							</tr>
							<tr>
								<td class='midcolora' colspan='4'>
									<div id="screenRights"></div>
								</td>
							</tr>
							<tr id="screenButtons">
								<td class='midcolora' colspan='2'>
									<cmsb:cmsbutton class="clsButton" id='btnReset' runat="server" Text='Reset'></cmsb:cmsbutton>
								</td>
								<td class='midcolorr' colspan="2">
									<cmsb:cmsbutton class="clsButton" id='btnSave' runat="server" Text='Save'></cmsb:cmsbutton>
								</td>
							</tr>
							<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
							<INPUT id="hidIS_ACTIVE" type="hidden" value="0" name="hidIS_ACTIVE" runat="server">
							<INPUT id="hidOldDatas" type="hidden" name="hidOldDatas" runat="server"> <INPUT id="hidUSER_TYPE_ID" type="hidden" value="0" name="hidUSER_TYPE_ID" runat="server">
							<INPUT id="hidUSER_ID" type="hidden" value="0" name="hidUSER_ID" runat="server">
							<INPUT id="hidSECTION" type="hidden" value="0" name="hidSECTION" runat="server">
							<INPUT id="hidSUBSECTION" type="hidden" value="0" name="hidSUBSECTION" runat="server">
							<INPUT id="hidSCREENLIST" type="hidden" name="hidSCREENLIST" runat="server">
							<input id="hidAppPol" runat="server" type="hidden" name="hidAppPol" value="-1">
							<input id="hidCALLED_FOR" runat="server" type="hidden" name="hidCALLED_FOR" value="">
							<input id="hidMENU_XML" runat="server" type="hidden" name="hidMENU_XML" value="">
                            <input id="hidAll" runat="server" type="hidden" name="hidAll" value="">
                            <input id="hidread" runat="server" type="hidden" name="hidread" value="" /> 
                            <input id="hidwrite" runat="server" type="hidden" name="hidwrite" value="" /> 
                            <input id="hidexecute" runat="server" type="hidden" name="hidexecute" value="" />
                            <input id="hiddelete" runat="server" type="hidden" name="hiddelete" value="" />
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script>
		createMenuDataSecurity();
		FillComboSection();
		</script>
	</BODY>
</HTML>
