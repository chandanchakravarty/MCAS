<%@ Page language="c#" Codebehind="AttentionNotes.aspx.cs" AutoEventWireup="false" Inherits="Cms.Client.Aspx.AttentionNotes" validateRequest = "false"%>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title><%=head%></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		 function DoBack()
			{
				var strres='<%=strResult%>';
				
				document.getElementById('txtCUSTOMER_ATTENTION_NOTE').focus();
				///this.parent.document.location.href = "customerindex.aspx";
				
				if(window.opener)
				{
					if(strres=="1")
					{
					//window.opener.document.getElementById("cltClaimTop_Image1").src = '<%=rootPathRed%>';
					
						if(window.opener.document.getElementById("cltClientTop_AspImageNote")!=null)
							window.opener.document.getElementById("cltClientTop_AspImageNote").src='<%=rootPathGrey%>';
					
						if(window.opener.document.getElementById("cltClientTop_Image1")!=null)
							window.opener.document.getElementById("cltClientTop_Image1").src='<%=rootPathGrey%>';
					
						if(window.opener.document.getElementById("cltClaimTop_AspImageNote")!=null)
							window.opener.document.getElementById("cltClaimTop_AspImageNote").src='<%=rootPathGrey%>';
					
						if(window.opener.document.getElementById("cltClaimTop_Image1")!=null)
							window.opener.document.getElementById("cltClaimTop_Image1").src='<%=rootPathGrey%>';
							
						if(window.opener.document.getElementById("AspImageNote")!=null)
							window.opener.document.getElementById("AspImageNote").src='<%=rootPathGrey%>';
					}		
					else
					{
						if(window.opener.document.getElementById("cltClientTop_AspImageNote")!=null)
							window.opener.document.getElementById("cltClientTop_AspImageNote").src='<%=rootPathRed%>';
						
						if(window.opener.document.getElementById("cltClientTop_Image1")!=null)
							window.opener.document.getElementById("cltClientTop_Image1").src='<%=rootPathRed%>';
						
						if(window.opener.document.getElementById("cltClaimTop_AspImageNote")!=null)
							window.opener.document.getElementById("cltClaimTop_AspImageNote").src='<%=rootPathRed%>';
						
						if(window.opener.document.getElementById("cltClaimTop_Image1")!=null)
							window.opener.document.getElementById("cltClaimTop_Image1").src='<%=rootPathRed%>';
						
						if(window.opener.document.getElementById("AspImageNote")!=null)
							window.opener.document.getElementById("AspImageNote").src='<%=rootPathRed%>';	
						
					}
				}
				else		
				{
					if(strres=="1")
					{
						if(parent.document.getElementById("cltClientTop_AspImageNote")!=null)
							parent.document.getElementById("cltClientTop_AspImageNote").src='<%=rootPathGrey%>';						
						
						if(parent.document.getElementById("cltClientTop_Image1")!=null)
							parent.document.getElementById("cltClientTop_Image1").src='<%=rootPathGrey%>';		
							
						if(parent.document.getElementById("cltClaimTop_AspImageNote")!=null)
							parent.document.getElementById("cltClaimTop_AspImageNote").src='<%=rootPathGrey%>';						
						
						if(parent.document.getElementById("cltClaimTop_Image1")!=null)
							parent.document.getElementById("cltClaimTop_Image1").src='<%=rootPathGrey%>';		
					}
					else
					{			
						if(parent.document.getElementById("cltClientTop_AspImageNote")!=null)
							parent.document.getElementById("cltClientTop_AspImageNote").src='<%=rootPathRed%>';	
						
						if(parent.document.getElementById("cltClientTop_Image1")!=null)
							parent.document.getElementById("cltClientTop_Image1").src='<%=rootPathRed%>';	
							
						if(parent.document.getElementById("cltClaimTop_AspImageNote")!=null)
							parent.document.getElementById("cltClaimTop_AspImageNote").src='<%=rootPathRed%>';	
						
						if(parent.document.getElementById("cltClaimTop_Image1")!=null)
							parent.document.getElementById("cltClaimTop_Image1").src='<%=rootPathRed%>';	
					}		
				}
					
				return false;
			}
			
			function BackToCustomer()
			{
			//alert('jdf');
			top.botframe.location.href="CustomerManagerIndex.aspx";
			return false;
			}	
			function ChkTextAreaLength(source, arguments)
			{
				var txtArea = arguments.Value;
				if(txtArea.length > 500 ) 
				{
					arguments.IsValid = false;
					return;   // invalid userName
				}
				else
				arguments.IsValid = true;
			}		
			
			function ResetForm()
			{
				document.forms[0].reset();
				DisableValidators();
				ChangeColor();
				return false;
				
			}
			function ChangeParentVar()
			{
				if(parent.CALLED_FROM_APPLICATION!=undefined)
					parent.CALLED_FROM_APPLICATION = 0;				
			}
			
			//Added For Itrack Issue #5510
			function GoToNewQuote()
			{	
		    if(document.getElementById("hidTYPE").value=="134_3")
			  {	
			    //if(window.opener.parent!=null) //Changed by Charles on 26-Nov-09 for Itrack 6773
			    if(window.opener!=null)
				{	
					if(typeof(window.opener.parent.opener)!='undefined')
					{	
			           window.opener.parent.opener.parent.frames[1].location = "/cms/cmsweb/aspx/quotetab.aspx?CalledFromMenu=Y";
			           window.close();			   			
			        }
			        else
			        {
			           window.opener.parent.frames[1].location = "/cms/cmsweb/aspx/quotetab.aspx?CalledFromMenu=Y";
			           window.close();
			        }
			     }
			    else
			      { //Commented by Charles on 26-Nov-09 for Itrack 6773
			        //window.opener.parent.frames[1].location = "/cms/cmsweb/aspx/quotetab.aspx?CalledFromMenu=Y";
			        //window.close();
			        parent.document.parentWindow.location.href="/cms/cmsweb/aspx/quotetab.aspx?CalledFromMenu=Y";	
			      }			    
				}
				else if(document.getElementById("hidTYPE").value=="192_3")
				{			 
					parent.document.parentWindow.location.href="/cms/cmsweb/aspx/quotetab.aspx?CalledFromMenu=Y";			 
				}
			 
 	         }
			function GoToNewApplication()
			{	
			if(document.getElementById("hidTYPE").value=="134_3")		      
			{
			    //if(window.opener.parent!=null) //Changed by Charles on 26-Nov-09 for Itrack 6773

			    var url = "/CMS/POLICIES/ASPX/POLICYTAB.aspx?CALLEDFROM=CLT";
				
				if(window.opener!=null)
				{	
					if(typeof(window.opener.parent.opener)!='undefined')
					{	
						
						window.opener.parent.opener.parent.frames[1].location = url;
						window.close();
					}
					else	
					{
					    window.opener.parent.frames[1].location = url;
						window.close();
					}
				}		
				else
				{	//Commented by Charles on 26-Nov-09 for Itrack 6773
					//window.opener.parent.frames[1].location = "/CMS/APPLICATION/ASPX/APPLICATIONTAB.aspx?CALLEDFROM=CLT";
					//window.close();
				    parent.document.parentWindow.location.href = url;
				}
				 
			}
		     else if(document.getElementById("hidTYPE").value=="192_3")
		     {
		         parent.document.parentWindow.location.href = url;
		     } 
		   // parent.document.parentWindow.location.href="/CMS/APPLICATION/ASPX/APPLICATIONTAB.aspx?CALLEDFROM=CLT";
			
		}
			
		</script>
	</HEAD>
	<body oncontextmenu="return false;" leftMargin="0" topMargin="0" onload="DoBack();ChangeParentVar();" MS_POSITIONING="GridLayout">
		<form id="CLT_CUSTOMER_ATTENTION_NOTE" method="post" runat="server">
			<webcontrol:gridspacer id="grdSpacer" runat="server"></webcontrol:gridspacer>
			<TABLE class="tableWidthHeader" align="center" border="0">
				<TR>
					<TD class="midcolorc" align="center" colSpan="2"><asp:label id="lblMessage" runat="server" Visible="False" CssClass="errmsg"></asp:label></TD>
				</TR>
				
				<TR>
					
					<TD class="midcolora"><asp:label id="capCUSTOMER_ATTENTION_NOTE" runat="server">Attention Notes(Max 500 characters)</asp:label><br /><asp:textbox onkeypress="MaxLength(this,500);" id="txtCUSTOMER_ATTENTION_NOTE" runat="server"
							size="100" maxlength="500" Columns="60" Rows="3" TextMode="MultiLine"></asp:textbox><br>
						<asp:customvalidator id="csvCUSTOMER_ATTENTION_NOTE" Display="Dynamic" Runat="server" ControlToValidate="txtCUSTOMER_ATTENTION_NOTE"
							ClientValidationFunction="ChkTextAreaLength"></asp:customvalidator></TD>
							
							<td class="midcolora"><asp:label id="capAttentionNoteUpdated" runat="server">Last Modified Date</asp:label><br />
							<asp:label id="txtAttentionNoteUpdated" runat="server" CssClass="LabelFont" size="10" maxlength="10"></asp:label>
							</td>
					
				</TR>
				<tr style="display:none">
				<td>
				<cmsb:cmsbutton class="clsButton" id="btnAddNewQuickQuote" runat="server" Text="Add New Quick Quote"  ></cmsb:cmsbutton>
				</td>
				</tr>
				
				<TR>
					<TD class="midcolora">
					<cmsb:cmsbutton class="clsButton" id="btnReset" runat="server" Text="Reset"></cmsb:cmsbutton>
					<cmsb:cmsbutton class="clsButton" id="btnBack" runat="server" Visible="False" Text="Back To Customer Assistant"></cmsb:cmsbutton>					
					
					<cmsb:cmsbutton class="clsButton" id="btnAddNewApplication" runat="server" Text="Add New Application"></cmsb:cmsbutton>
					<TD class="midcolorr"><cmsb:cmsbutton class="clsButton" id="btnSave" runat="server" Text="Save"></cmsb:cmsbutton></TD>
				</TR>
				<TR>
					<INPUT id="hidFormSaved" type="hidden" value="0" name="hidFormSaved" runat="server">
					<INPUT id="hidCUSTOMER_ID" type="hidden" name="Hidden1" runat="server"> <INPUT id="hidOldData" type="hidden" name="Hidden1" runat="server">
					<INPUT id="hidIS_ACTIVE" type="hidden" name="Hidden1" runat="server">
					<INPUT id="hidTYPE" type="hidden" name="hidTYPE" runat="server">
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
