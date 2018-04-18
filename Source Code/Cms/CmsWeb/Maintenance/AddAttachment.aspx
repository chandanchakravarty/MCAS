<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Page language="c#" Codebehind="AddAttachment.aspx.cs" AutoEventWireup="false" Inherits="Cms.CmsWeb.Maintenance.AddAttachment" validateRequest=false %>
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' >
<HTML>
	<HEAD>
		<title>MNT_ATTACHMENT_LIST</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<LINK href="Common.css" type="text/css" rel="stylesheet">
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script language="javascript">		
		
	    /*if('<%=strCalledFrom%>'=='reinsurer')
	     {
			alert('xxx');
			var obj =	 document.getElementById("divback1");
			alert(obj);
			alert('style'+ obj.style.display);
			  obj.style.display="inline";
			   
		  }
		 else
			  {
					alert('zzz');
				var obj =	 document.getElementById("divback1");
				 	 document.getElementById("divback1").style.display ="none";
			 }
				*/
		
		function DoBack()
		{	
		    
			if('<%=strCalledFrom%>'=='reinsurer')
		   {
			
			 this.parent.document.location.href = "/cms/cmsweb/maintenance/AddReinsurer.aspx?REIN_COMAPANY_ID=" + document.getElementById("hidEntityId").value + "&transferdata=";		    
		   }
		   else if('<%=strCalledFrom%>'=='REINSURANCE')
		   {
			 this.parent.document.location.href = "/Cms/cmsweb/Maintenance/AddReinsuranceInfo.aspx?CONTRACT_ID=" + document.getElementById("hidEntityId").value + "&transferdata=";		    
		   }
		   else
		   {
		    
			this.parent.parent.document.location.href = "/cms/client/aspx/CustomerManagerIndex.aspx"; 			
			}
			return false;
		}	
		
		
			
		function ShowPopup(url, winname, width, height) 
		{
			var MyURL = url;
			var MyWindowName = winname;
			var MyWidth = width;
			var MyHeight = height;
			var MyScrollBars = 'Yes';
			var MyResizable = 'Yes';
			var MyMenuBar = 'No';
			var MyToolBar = 'No';
			var MyStatusBar = 'No';

			    if (document.all)
			        var xMax = screen.width, yMax = screen.height;
			    else
			        if (document.layers)
			            var xMax = window.outerWidth, yMax = window.outerHeight;
			        else
			            var xMax = 640, yMax=480;

			    var xOffset = (xMax - MyWidth)/2, yOffset = (yMax - MyHeight)/2;

				MyWin = window.open(MyURL,MyWindowName,'width=' + MyWidth + ',height=' + MyHeight + ',screenX= ' + xOffset + ',screenY=' + yOffset + ',top=' + yOffset + ',left=' + xOffset + ',scrollbars=' + MyScrollBars + ',resizable=' + MyResizable + ',menubar=' + MyMenuBar + ',toolbar=' + MyToolBar + ',status=' + MyStatusBar + '' );
				MyWin.focus();
		}
		
		function SetFileType()
		{

			if (document.getElementById("hidRowId").value == "New")
			{	
				var FileName = MNT_ATTACHMENT_LIST.txtATTACH_FILE_NAME.value;
				
				if (FileName != "")
				{
					var Index = FileName.lastIndexOf(".");
					if (Index != -1)
					{
						FileName = FileName.substring(Index + 1);
					}
				}
				document.getElementById("txtATTACH_FILE_TYPE").value = FileName;
				document.getElementById("txtATTACH_FILE_TYPE").setAttribute("readOnly",true);			
			}
		}
				
		//This function is used for refreshing the form 
		//To be called in while user clicks on Reset button 
		//and also after the form get saved
		function AddData()
		{
			
			//if(document.getElementById("txtATTACH_FILE_NAME").style.display!="none")
			//	document.getElementById("txtATTACH_FILE_NAME").focus();
				
			document.getElementById("txtATTACH_FILE_NAME").value = '';
			document.getElementById("txtATTACH_FILE_DESC").value		=	"";
			document.getElementById("txtATTACH_FILE_TYPE").value		=	"";
			document.getElementById("hidRowId").value					=	"New";
			document.getElementById("lblATTACH_FILE_NAME").innerText	=	"";
			document.getElementById("lblATTACH_FILE_TYPE").innerText	=	"";
			document.getElementById("txtATTACH_FILE_TYPE").style.display = "inline";
			document.getElementById('cmbATTACH_TYPE').options.selectedIndex = -1;			
			Page_Validators[0].enabled = true; 
			
			document.getElementById('MNT_ATTACHMENT_LIST').reset();
			
			//Disabling the validators
			DisableValidators();
			
			//Changing the colors
			ChangeColor();	
		}
		
		//Populating the data
		function populateXML()
		{
			if(document.getElementById('hidFormSaved').value == '0' || document.getElementById('hidFormSaved').value == '1')
			{
							
				var tempXML;
				
				if(this.parent.strXML != '')
				{
					tempXML = this.parent.strXML
				}
				
				if ( document.MNT_ATTACHMENT_LIST.hidOldData.value != '' )
				{
					
					tempXML = document.MNT_ATTACHMENT_LIST.hidOldData.value;		
				
				}
				
				if(tempXML != "" && tempXML != null)
				{
					//Storing the XML in hidOldData hidden fields 
					document.getElementById('hidOldData').value		=	 tempXML;
					//document.getElementById('txtATTACH_FILE_NAME').style.visibility="hidden";
					document.getElementById('txtATTACH_FILE_NAME').style.display="none";
					
					var objXmlHandler = new XMLHandler();
					var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('Table')[0]);
			
					var i=0;
					for(i=0;i<tree.childNodes.length;i++)
					{
						if(!tree.childNodes[i].firstChild) continue;
						
						var nodeName = tree.childNodes[i].nodeName;
						var nodeValue = tree.childNodes[i].firstChild.text;
						var fileName;
						switch(nodeName)
						{
							case "ATTACH_ID":
								document.getElementById("hidRowId").value = DecodeXML(nodeValue);
							case "ATTACH_FILE_NAME":
								//document.getElementById("lblATTACH_FILE_NAME").innerText = nodeValue;
								fileName = DecodeXML(nodeValue);
								
								//fileName = document.getElementById("lblATTACH_FILE_NAME").innerText;
								document.getElementById("hidAttachFileName").value=fileName;
			
								break;
							case "FILE_FULL_NAME":
							
								//added by vj to modify the link for application attachment.
								if (document.getElementById("hidEntity_Type").value == "Application")
								{
									document.getElementById("lblATTACH_FILE_NAME").innerHTML = '<a href = "' + document.getElementById("hidfileLink").value + '" target="blank">' + fileName + '</a>';
								}
								else if (document.getElementById("hidApplicationVersion").value != "")
								{
									document.getElementById("lblATTACH_FILE_NAME").innerHTML = '<a href = "' + document.getElementById("hidfileLink").value + '" target="blank">' + fileName + '</a>';
								}
								else
								{
								document.getElementById("lblATTACH_FILE_NAME").innerHTML = '<a href = "' + document.getElementById("hidfileLink").value + '" target="blank">' + fileName + '</a>';
								}
								break;
							case "ATTACH_FILE_DESC":
								document.getElementById("txtATTACH_FILE_DESC").value = DecodeXML(nodeValue);
								break;
							case "ATTACH_FILE_TYPE":
								document.getElementById("txtATTACH_FILE_TYPE").value = DecodeXML(nodeValue);
								document.getElementById("txtATTACH_FILE_TYPE").style.display = "none";
								document.getElementById("lblATTACH_FILE_TYPE").innerHTML = nodeValue;
								break;
							case "ATTACH_TYPE":
									var value = DecodeXML(nodeValue);
									SelectComboOption('cmbATTACH_TYPE',value);									
									break;								
						}
					}
					// document.getElementById("").setAttribute("disabled",true);    //  Commented by Ashwani
					Page_Validators[0].enabled = false; 
				}
				else
				{
					AddData();
				}
				
				
			}
			if (document.MNT_ATTACHMENT_LIST.hidOldData.value != "")
			{
				if(document.getElementById("btnDelete"))
				document.getElementById("btnDelete").style.display = "inline";
			}
			else
			{
				if(document.getElementById("btnDelete"))
				document.getElementById("btnDelete").style.display = "none";
			}
		}
		
		function DisplayAppDetails()
		{
			if(document.MNT_ATTACHMENT_LIST.hidApplicationNumber.value != "" && document.MNT_ATTACHMENT_LIST.hidApplicationNumber.value != "APP_VERSION")
			{
				rowAppNumber.style.display = "inline";	
			}
			else
			{
				rowAppNumber.style.display = "none";	
			}
			
			if(document.MNT_ATTACHMENT_LIST.hidApplicationVersion.value != "")
			{
				rowAppVersion.style.display = "inline";
			}
			else
			{
				rowAppVersion.style.display = "none";
			}

		}
		
		// show look up			
		function showPageLookupLayer(controlId)
		{
			var lookupMessage;						
			switch(controlId)
			{
				case "cmbATTACH_TYPE":
					lookupMessage	=	"";//According to sheet 0407
					break;
				default:
					lookupMessage	=	"Look up code not found";
					break;
					
			}
			showLookupLayer(controlId,lookupMessage);							
		}
		
		function showHide()
		{
		//Commented by swarup as Itrack issue #1932
			/*if (document.getElementById('hidCalledFrom').value == 'CLAIM')
			{
				document.getElementById('btnBack').style.display="none";
				document.getElementById('rowAttachmentType').style.display="none";
			}
			else if (document.getElementById('hidCalledFrom').value != 'REINSURANCE' && document.getElementById('hidCalledFrom').value != 'reinsurer')
			{
				document.getElementById('btnBack').style.display="inline";
				document.getElementById('rowAttachmentType').style.display="inline";
			}*/
			if (document.getElementById('hidCalledFrom').value =='MORTGAGE' || document.getElementById('hidCalledFrom').value =='InCLT'|| document.getElementById('hidCalledFrom').value =='Application')
			{
				document.getElementById('btnBack').style.display="inline";
				document.getElementById('rowAttachmentType').style.display="inline";
			}
			else if(document.getElementById('btnBack'))
			{
				document.getElementById('btnBack').style.display="none";
				//document.getElementById('rowAttachmentType').style.display="none";
			}
			
		}
			
		
		</script>
	</HEAD>
	<BODY oncontextmenu="return false;" class="bodyBackGround" leftMargin="0" topMargin="0" onload="populateXML(); ApplyColor();DisplayAppDetails();showHide();">
		<FORM onkeypress="if(event.keyCode==13){ btnSave.click();return false;}" id="MNT_ATTACHMENT_LIST"
			method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="midcolorc" align="right" colSpan="4"><asp:label id="lblDelete" runat="server" CssClass="errmsg" Visible="False"></asp:label></td>
				</tr>
				<TR id="trBody" runat="server">
					<TD>
						<TABLE width="100%" align="center" border="0">
							<tr>
								<TD class="pageHeader" colSpan="3"><asp:Label ID="capMessages" runat="server"></asp:Label>
								</TD>
							</tr>
							<tr>
								<td class="midcolorc" align="center" colSpan="3"><asp:label id="lblMessage" CssClass="errmsg" Visible="False" Runat="server"></asp:label></td>
							</tr>
							
							
							<tr>
								<TD class="midcolora" colSpan="3"><asp:label id="capATTACH_FILE_NAME" runat="server">File Name</asp:label><span class="mandatory" id="spnFileName" runat="server">*</span></br>
								<input id="txtATTACH_FILE_NAME" onchange="javascript:SetFileType();RemoveSpecialChar(document.getElementById('txtATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_NAME'));RemoveExecutableFiles(document.getElementById('txtATTACH_FILE_NAME').value,document.getElementById('revATTACH_FILE_EXT'));" 
								type="file"
										size="70" runat="server"></br>
									<asp:label id="lblATTACH_FILE_NAME" Runat="server"></asp:label><br>
									<asp:requiredfieldvalidator id="rfvATTACH_FILE_NAME" runat="server" Display="Dynamic" ControlToValidate="txtATTACH_FILE_NAME"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="revATTACH_FILE_NAME" Runat="server" ControlToValidate="txtATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
									<asp:regularexpressionvalidator id="revATTACH_FILE_EXT" Runat="server" ControlToValidate="txtATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>
									<%--<asp:regularexpressionvalidator id="revATTACH_FILE_PDF" Runat="server" ControlToValidate="txtATTACH_FILE_NAME" Display="Dynamic"></asp:regularexpressionvalidator>--%>
									</TD>
							</tr>
							<tr> 
								<TD class="midcolora" width="33%"><asp:label id="capATTACH_FILE_DESC" runat="server">File Description</asp:label>
								<br />
								<asp:textbox id="txtATTACH_FILE_DESC" runat="server" maxlength="255" size="70"></asp:textbox>
								</TD>
								<TD class="midcolora" width="33%" style="display:none">
								<asp:label id="capATTACH_FILE_TYPE" runat="server">File Type</asp:label>
								<br />
								<asp:textbox id="txtATTACH_FILE_TYPE" runat="server" maxlength="5"  Width="150px"></asp:textbox>
									<asp:label id="lblATTACH_FILE_TYPE" CssClass="LabelFont" Runat="server"></asp:label>
								</TD>
								<td class="midcolora" id="rowAttachmentType" width="33%"> 
								<asp:label id="capATTACH_TYPE" runat="server">Attachment Type</asp:label>
								<br />
								<asp:dropdownlist id="cmbATTACH_TYPE" onfocus="SelectComboIndex('cmbATTACH_TYPE')" runat="server"></asp:dropdownlist>
									<A id="ancATTACH_TYPE" runat="server" class="calcolora" href="javascript:showPageLookupLayer('cmbATTACH_TYPE')"><IMG height="16" src="/cms/cmsweb/images/info.gif" width="17" border="0"></A>
								</td>
							</tr>						
							
							<tr id="rowAppNumber">
								<TD class="midcolora">Application Number</TD>
								<TD class="midcolora"><asp:label id="lblAppNumber" CssClass="LabelFont" Runat="server"></asp:label></TD>
							</tr>
							<tr id="rowAppVersion">
								<TD class="midcolora">Application Version</TD>
								<TD class="midcolora"><asp:label id="lblAppVersion" CssClass="LabelFont" Runat="server"></asp:label></TD>
							</tr>
							<tr>
								<td class="midcolora"><cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Visible="False" Text="Back To Customer Assistant"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset" CausesValidation="False"></cmsb:cmsbutton>
									<cmsb:cmsbutton class="clsButton" id="btnback1" runat="server" Visible="False" Text="Back To Reinsurer Details"></cmsb:cmsbutton>
									<DIV>
										<cmsb:cmsbutton class="clsButton" id="btnback2" runat="server" Visible="False" Text="Back To Contract Details"></cmsb:cmsbutton></DIV>
								</td>
								
								<td class="midcolorr" colspan="2">
									<P align="right"><cmsb:cmsbutton class="clsButton" id="btnDelete" runat="server" Text="Delete"></cmsb:cmsbutton><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></P>
								</td>
							</tr>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<input id="hidFormSaved" type="hidden" value="0" runat="server"> <input id="hidRowId" type="hidden" value="New" name="RowID" runat="server">
			<input id="hidEntityId" type="hidden" name="Hidden1" runat="server"> <INPUT id="hidRootPath" type="hidden" runat="server">
			<input id="hidPostedFile" type="hidden" name="Hidden1" runat="server"> <input id="hidOldData" type="hidden" name="hidOldData" runat="server">
			<INPUT id="hidEntity_Type" type="hidden" name="hidEntity_Type" runat="server"><INPUT id="hidAttachSourceID" type="hidden" name="hidAttachSourceID" runat="server">
			<INPUT id="hidApplicationNumber" type="hidden" name="hidApplicationNumber" runat="server"><INPUT id="hidApplicationVersion" type="hidden" name="hidApplicationVersion" runat="server">
			<input id="hidCustomInfo" type="hidden" value="0" runat="server" NAME="hidCustomInfo">
			<input id="hidAttachFileName" type="hidden" value="0" runat="server" NAME="hidAttachFileName">
			<input id="hidCalledFrom" type="hidden" value="0" runat="server" NAME="hidCalledFrom">
			<INPUT id="hidfileLink" type="hidden" value="" name="hidfileLink" runat="server">
		</FORM>
		<!-- For lookup layer -->
		<div id="lookupLayerFrame" style="DISPLAY: none; Z-INDEX: 101; POSITION: absolute"><iframe id="lookupLayerIFrame" style="DISPLAY: none; Z-INDEX: 1001; FILTER: alpha(opacity=0); BACKGROUND-COLOR: #000000"
				width="0" height="0" left="0px" top="0px;"></iframe>
		</div>
		<DIV id="lookupLayer" onmouseover="javascript:refreshLookupLayer();" style="Z-INDEX: 101; VISIBILITY: hidden; POSITION: absolute">
			<table class="TabRow" cellSpacing="0" cellPadding="2" width="100%">
				<tr class="SubTabRow">
					<td><b><asp:Label runat="server" Text="Add LookUp" ID="Caplook"></asp:Label></b></td>
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
			if (document.getElementById("hidFormSaved").value == "1") {
				/*Record deleted*/
				/*Refreshing the grid and coverting the form into add mode*/
				/*Using the javascript*/
				//RefreshWebGrid("1","1"); 
				//document.getElementById("hidFormSaved").value = "0";
				//document.location.href = "AddAttachment.aspx?EntityId=" + document.getElementById("hidEntityId").value
											//+ "&EntityType=" +  document.getElementById("hidEntity_Type").value
				RefreshWebGrid(document.getElementById('hidFormSaved').value, document.getElementById('hidEntityId').value); 						//+ "&Grid=web&"
				
			}
			
		</script>
	</BODY>
</HTML>
