<%@ Page language="c#" Codebehind="EFTSweepHistoryDetails.aspx.cs" AutoEventWireup="false" Inherits="Cms.Account.Aspx.EFTSweepHistoryDetails" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>EFT Sweep History Details</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" >
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
		<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
		<script language="javascript">
		function showPrint()
		{
			spn_Button.style.display = "none"
			bgcolor = buttons.style.background
			buttons.style.background = "white"
			window.print()
			spn_Button.style.display = "inline"
		}
		
		function DefaultHideReport()
		{
		
				if(document.all('Customer')!=null)
				{
				
					if (typeof document.all('Customer').length == "undefined") 
        				document.all('Customer').style.display	=	'none';
					else
					{
        				for(i=0; i<document.all('Customer').length; i++)
							{
									document.all('Customer').item(i).style.display	=	'none';
							}
					}
				}
				
				if(document.all('Vendor')!=null)
				{
					
					if (typeof document.all('Vendor').length == "undefined") 
        				document.all('Vendor').style.display	=	'none';
					else
					{
        				for(i=0; i<document.all('Vendor').length; i++)
							{
									document.all('Vendor').item(i).style.display	=	'none';
							}
					}
					
				}	
				
				if(document.all('Agency')!=null)
				{
				
					
					if (typeof document.all('Agency').length == "undefined") 
        				document.all('Agency').style.display	=	'none';
					else
					{
        				for(i=0; i<document.all('Agency').length; i++)
							{
									document.all('Agency').item(i).style.display	=	'none';
							}
					}
				}	
			
		}
		function Init()
		{
			//DefaultHideReport();		
		}
		
		
		function showHideInfo_old(Type)
		{	
			if(document.getElementById('Agency_imgID')!=null)
				var path_Agn = document.getElementById('Agency_imgID').src;
			if(document.getElementById('Customer_imgID')!=null)
				var path_Cust = document.getElementById('Customer_imgID').src;
			if(document.getElementById('Vendor_imgID')!=null)
				var path_vend = document.getElementById('Vendor_imgID').src;
	
		if(Type=='CUSTOMER')
		{
		  //Customer
			if(path_Cust.indexOf('plus2') != -1)
			{
				document.getElementById('Customer_imgID').src	=	'<%=pathMinus%>';
				//show all
			if (typeof document.all('Customer').length == "undefined") 
        		document.all('Customer').style.display	=	'inline';
			else
			{
        		for(i=0; i<document.all('Customer').length; i++)
					{
							document.all('Customer').item(i).style.display	=	'inline';
					}
			}
		   
		 }  
		 
		 else
		 {	
		   //hide all
			document.getElementById('Customer_imgID').src	=	'<%=pathPlus%>';	
        
			
			if (typeof document.all('Customer').length == "undefined") 
        		document.all('Customer').style.display	=	'none';
			else
			{
        		for(i=0; i<document.all('Customer').length; i++)
					{
							document.all('Customer').item(i).style.display	=	'none';
					}
			}
			
		 }
		}
		
		else if(Type=='AGENCY')
        {
          //Agency
			if(path_Agn.indexOf('plus2') != -1)
			{
				document.getElementById('Agency_imgID').src	=	'<%=pathMinus%>';
				//show all
			if (typeof document.all('Agency').length == "undefined") 
        		document.all('Agency').style.display	=	'inline';
			else
			{
        		for(i=0; i<document.all('Agency').length; i++)
					{
							document.all('Agency').item(i).style.display	=	'inline';
						
					}
			}
		   
		 }  
		 
		 else
		 {	
		   //hide all
			document.getElementById('Agency_imgID').src	=	'<%=pathPlus%>';	
        
			
			if (typeof document.all('Agency').length == "undefined") 
        		document.all('Agency').style.display	=	'none';
			else
			{
        		for(i=0; i<document.all('Agency').length; i++)
					{
							document.all('Agency').item(i).style.display	=	'none';
					}
			}
			
		 }
		 
        }
        
        
        else if(Type=='VENDOR')
        {
         //Agency
			if(path_vend.indexOf('plus2') != -1)
			{
				document.getElementById('Vendor_imgID').src	=	'<%=pathMinus%>';
				//show all
			if (typeof document.all('Vendor').length == "undefined") 
        		document.all('Vendor').style.display	=	'inline';
			else
			{
        		for(i=0; i<document.all('Vendor').length; i++)
					{
							document.all('Vendor').item(i).style.display	=	'inline';
					}
			}
		   
		 }  
		 
		 else
		 {	
		 
		   //hide all
			document.getElementById('Vendor_imgID').src	=	'<%=pathPlus%>';	
        
			
			if (typeof document.all('Vendor').length == "undefined") 
        		document.all('Vendor').style.display	=	'none';
			else
			{
        		for(i=0; i<document.all('Vendor').length; i++)
					{
							document.all('Vendor').item(i).style.display	=	'none';
					}
			}
			
		 }
		  
        }
		
		}
		
		//New Function
		function showHideInfo(Type)
		{
				var fullpath = Type +'_imgID';		
				var path = document.getElementById(fullpath).src;
								
				if(path.indexOf('plus2') != -1)
				{
						document.getElementById(fullpath).src	=	'<%=pathMinus%>';
				//show all
					if (typeof document.all(Type).length == "undefined") 
        				document.all(Type).style.display	=	'inline';
					else
					{
        				for(i=0; i<document.all(Type).length; i++)
							{
									document.all(Type).item(i).style.display	=	'inline';
							}
					}
				   
				}  
				 
				else
				{	
				 
				//hide all
					document.getElementById(fullpath).src	=	'<%=pathPlus%>';	
		        	if (typeof document.all(Type).length == "undefined") 
        				document.all(Type).style.display	=	'none';
					else
					{
        				for(i=0; i<document.all(Type).length; i++)
							{
									document.all(Type).item(i).style.display	=	'none';
							}
					}
					
				}
		
		 
		}
		
		
		
		/*if(path.indexOf('plus2') != -1)
		{
			document.getElementById('img').src	=	'<%=pathMinus%>';
			document.getElementById('Agen').style.display	=	'inline';
			document.getElementById('Cust').style.display	=	'inline';
			document.getElementById('Ven').style.display	=	'inline';
			
			
			
		}
		else
		{
			document.getElementById('img').src	=	'<%=pathPlus%>';
			document.getElementById('Agen').style.display	=	'none';
			document.getElementById('Cust').style.display	=	'none';
			document.getElementById('Ven').style.display	=	'none';		
			
			
		}*/
		</script>
	</HEAD>
	<body oncontextmenu = "return false;" class="bodyBackGround" topmargin="0" MS_POSITIONING="GridLayout" onload="Init();">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing='1' cellPadding='0' border='0' class='tableWidth' align="center">
				<tr>
					<td>
						<webcontrol:GridSpacer id="Gridspacer1" runat="server"></webcontrol:GridSpacer>
					</td>
				</tr>
				<tr>
					<td class="headereffectCenter" colspan="2">
						EFT Sweep History
					</td>
				</tr>
				<tr>
					<td class="midcolorr" width="50%">
						Today's Date : &nbsp;
					</td>
					<td class="midcolora" width="50%">
						<%=DateTime.Now.ToString("MM/dd/yyyy")%>
					</td>
				</tr>
				<tr>
					<td class="midcolorr" width="50%">
						Time : &nbsp;
					</td>
					<td class="midcolora" width="50%">
						<%=DateTime.Now.ToString("hh:mm:ss tt")%>
					</td>
				</tr>
				<tr>
					<td class="midcolorc" colspan="2"><b>EFT Sweep History</b>
					</td>
				</tr>
				
				<tr>
					<td class="midcolora" colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td class="errmsg">
						<asp:Label ID="lblMessage" Runat="server"></asp:Label>
					</td>
				</tr>
				<tr>
					<td id="tdEFTSweepHistoryDetails" runat="server" colspan="2">
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>