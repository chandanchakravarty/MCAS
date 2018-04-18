<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuickQuoteTab.aspx.cs" Inherits="Cms.Policies.Aspx.WindHail.QuickQuoteTab" %>

<%@ Register TagPrefix="cmsb" NameSpace="Cms.CmsWeb.Controls" Assembly="CmsButton" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
   <title>Quick Quote Tab</title>
    
     <link href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type="text/css" rel="Stylesheet"/ >
   
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
 
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery.js"></script>
		<script type="text/javascript" src="/cms/cmsweb/scripts/jquery/jquery-1.4.2.min.js"></script> 
		 
		<script language="javascript" type="text/javascript">
		    function Init() {
		        ApplyColor();
		        ChangeColor();
		        top.topframe.main1.mousein = false;
		        findMouseIn();
		      
             }
		    
		 </script>
		 
		 <script language="javascript" type="text/javascript">

		     $(document).ready(function() {

		         $("#btnSubmit").click(function() {
		             // Initialize the object, before adding data to it.
		             //  { } is declarative shorthand for new Object().
		            debugger;

		             //    <Click Handler="#{TabPanel1}.addTab(#{Tab2});" />
		             var tab=TabPanel1;
		            // TabPanel1.addTab(#{Tab2});
		             return false;

		         });
		     });

		     function CallPageMethod(fn, paramList, successFn, errorFn) {
		         var pagePath = window.location.pathname;
		         $.ajax({ type: "POST", url: pagePath + "/" + fn, contentType: "application/json; charset=utf-8",
		             data: paramList, dataType: "json", success: successFn, error: errorFn
		         });
		     }
		     function AjaxSucceeded(result) {

		         alert(result.d);

		     }
		     function AjaxFailed(result) {

		         alert(result);
		     }
		     
   </script>
</head>
<body leftmargin="0" topmargin="0"  onload="Init();" >

 <webcontrol:Menu id="bottomMenu" runat="server">
    </webcontrol:Menu>
 
    <webcontrol:GridSpacer id="grdSpacer1" runat="server"></webcontrol:GridSpacer>
    
    <div align="center" id="bodyHeight" class="pageContent" style="height:100%;vertical-align:top;overflow:scroll">
    
    <form id="WindHail" runat="server">
    <div style="width:80%;" >
     <ext:ScriptManager ID="ScriptManager1" runat="server"></ext:ScriptManager>
    
        <ext:TabPanel ID="TabPanel1" runat="server" ActiveTabIndex="0"   Height="500px" Border="true" >
         
           <Tabs >
                <ext:Tab ID="Tab1" runat="server" Title="Wind Hail Quick Quote" >
                    <Body>
                       <table id="Table1" cellspacing="2" cellpadding="0" width="100%" border="0" >
                           <tr>
                            <td  class="midcolorc" colspan="6"><asp:Label runat="server" ID="lblDelete" CssClass="errmsg"></asp:Label></td>
                           </tr>
                            <tbody runat="server" id="tbody">
                                 <tr>
                                       <td  align="right" colspan="6">
	                                     <asp:label id="lblFormLoadMessage" runat="server" Visible="False" CssClass="errmsg" ></asp:label>
	                                   </td>
	                              </tr>
	                           
                                 <tr>
                                   <td class="pageHeader" align="left" colspan="6" >		           
		                               <asp:label id="lblManHeader" Runat="server" ></asp:label>
		                           </td>
	                            </tr>
                        	
	                            <tr>
		                            <td class="midcolorc" colspan="6"><asp:label id="lblMessage" runat="server" CssClass="errmsg" ></asp:label>
		                            </td>
	                            </tr>
                	            <tr>
		                            <td class="midcolora" colspan="6" >
		                            <br />
		                             </td>
	                            </tr>
                    	        <tr>
		                            <td class="midcolora" Width="2%">
		                               </td>
		                            <td class="midcolora" Width="2%" >
		                               </td>	
		                               <td class="midcolora" Width="2%" >
		                               </td>	
		                            <td class="midcolora" Width="2%" >
		                              
                                    </td>
                                    <td class="midcolora" Width="2%" >
                                     <asp:Label ID="Label6" runat="server" Text="Date"></asp:Label>
		                              
                                    </td>
                                     
		                             <td class="midcolora" Width="2%" >
		                             <asp:TextBox ID="TextBox5" runat="server" Width="80%"></asp:TextBox>
                                    </td>
		                           	
                    		    </tr>
                	             <tr>
                                     <td class="midcolora" Width="2%">
                                         <br />
                                         <asp:Label ID="Label1" runat="server" Text="Bulding Amount"></asp:Label>
                                         
                                     </td>
                                       <td class="midcolora" Width="2%">
                                         <br />
                                         <asp:TextBox ID="TextBox12" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label2" runat="server" Text="GUA"></asp:Label>
                                         <br />
                                         <asp:TextBox ID="TextBox2" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label3" runat="server" Text="Total"></asp:Label>
                                         <br />
                                         <asp:TextBox ID="TextBox3" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label4" runat="server" Text="Premium"></asp:Label>
                                         <br />
                                         <asp:TextBox ID="TextBox6" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label5" runat="server" Text="Primary Rate"></asp:Label>
                                         <br />
                                         <asp:TextBox ID="TextBox4" runat="server" Width="80%"> </asp:TextBox>
                                     </td>
                                 </tr>
                	             <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label7" runat="server" Text="Contents Amount"></asp:Label>
                                      </td>
                                      <td class="midcolora" Width="2%">
                                            <asp:TextBox ID="TextBox1" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox8" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox9" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox10" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox11" runat="server" Width="80%"></asp:TextBox>
                                     
		                              
                                    </td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label8" runat="server" Text="Inland/Beach/Seacoa"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <ext:ComboBox ID="ComboBox1" runat="server" Width="180px">
                                         </ext:ComboBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                           <td class="midcolora" Width="2%" >
		                             
                                    </td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label9" runat="server" Text="MIT"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <ext:ComboBox ID="ComboBox2" runat="server" Width="180px">
                                         </ext:ComboBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                              <td class="midcolora" Width="2%" >
		                             
                                    </td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label10" runat="server" Text="Coins"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <ext:ComboBox ID="ComboBox3" runat="server" Width="180px">
                                         </ext:ComboBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                              <td class="midcolora" Width="2%" >
		                             
                                    </td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label11" runat="server" Text="Construction"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <ext:ComboBox ID="ComboBox4" runat="server" Width="180px">
                                         </ext:ComboBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                              <td class="midcolora" Width="2%" >
		                             
                                    </td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label12" runat="server" Text="Deductible"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <ext:ComboBox ID="ComboBox5" runat="server" Width="180px">
                                         </ext:ComboBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                              <td class="midcolora" Width="2%" >
		                             
                                    </td>
                                 </tr>
                	             <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label13" runat="server" Text="Builder Risk"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <ext:ComboBox ID="ComboBox6" runat="server" Width="180px">
                                         </ext:ComboBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label14" runat="server" Text="Builings"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox13" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label15" runat="server" Text="Contents"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox14" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label16" runat="server" Text="Business Amount"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox15" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox18" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label17" runat="server" Text="Monthly Limit"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox16" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox19" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label18" runat="server" Text="Rate Effect"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox17" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         <asp:Label ID="Label19" runat="server" Text="Annual Premium"></asp:Label>
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         <asp:TextBox ID="TextBox20" runat="server" Width="80%"></asp:TextBox>
                                     </td>
                                     <td class="midcolorr" Width="2%">
                                            <ext:Button ID="btnNext" runat="server" Text="Next">
                                          <Listeners>
                                                <Click Handler="#{TabPanel1}.addTab(#{Tab2});" />
                                            </Listeners>
                                         </ext:Button>
                                        </td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                       <cmsb:cmsbutton id="btnLogoutExit" runat="server" causesvalidation="true" 
                                             cssclass="clsButton" text="Logout/Exit" />
                                     </td>
                                     <td class="midcolora" Width="2%">
                                       &nbsp;
                                     </td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                       
                                     </td>
                                     <td class="midcolorr" Width="2%">
                                         <cmsb:cmsbutton id="btnSubmit" runat="server"  causesvalidation="true" cssclass="clsButton" text="Submit" />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                     <td class="midcolora" Width="2%">
                                         &nbsp;</td>
                                 </tr>
                	        </tbody>
	                    </table>
                    </Body>
                </ext:Tab>
                <ext:Tab ID="Tab2" runat="server" Title="Policy Information - Insured"  >
                  <Body>
                  <table id="Table2" cellspacing="2" cellpadding="0" width="100%" border="0" >
                           <tr>
                            <td  class="midcolorc" colspan="3"><asp:Label runat="server" ID="Label20" CssClass="errmsg"></asp:Label></td>
                           </tr>
                            <tbody runat="server" id="tbody1">
                                 <tr>
                                       <td  align="right" colspan="3">
	                                     <asp:label id="Label21" runat="server" Visible="False" CssClass="errmsg" ></asp:label>
	                                   </td>
	                              </tr>
                                 <tr>
                                   <td class="pageHeader" align="left" colspan="3" >		           
		                               <asp:label id="lblManHeader1" Runat="server" ></asp:label>
		                           </td>
	                            </tr>
                        	
	                            <tr>
		                            <td class="midcolorc" colspan="3"><asp:label id="Label23" runat="server" CssClass="errmsg" ></asp:label>
		                            </td>
	                            </tr>
                	            <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                             </td>
	                            </tr>
                    	         
                                  <tr>
                                     <td class="pageHeader" colspan="3">
                                          <asp:label  id="capInsured" Runat="server" Text="Insured" ></asp:label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="33%">
                                         </td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                       <asp:label id="Label45" Runat="server" Text="Date" ></asp:label>
                                       <br />
                                       <asp:TextBox ID="TextBox31" runat="server"  Width="240px"></asp:TextBox>
                                       </td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                         <asp:label id="capProducer" Runat="server" Text="Producer" ></asp:label>
                                         <br />
                                          <asp:TextBox ID="txtproducer" runat="server"  Width="240px"></asp:TextBox>
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                       <asp:label id="Label22" Runat="server" Text="ACME Insurance Agency" ></asp:label>
                                         <br />
                                         <ext:ComboBox ID="ComboBox7" runat="server"  Width="240px"></ext:ComboBox></td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     
                                 </tr>
                                  
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                         <asp:label id="Label24" Runat="server" Text="UND" ></asp:label>
                                         <br />
                                           <asp:TextBox ID="TextBox7" runat="server"  Width="240px"></asp:TextBox>
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                            <asp:label id="Label25" Runat="server" Text="Effective" ></asp:label>
                                         <br />
                                           <asp:TextBox ID="TextBox21" runat="server"  Width="240px"></asp:TextBox>
                                      </td>
                                     <td class="midcolora"  Width="33%">
                                        <asp:label id="Label26" Runat="server" Text="Expiration" ></asp:label>
                                         <br />
                                           <asp:TextBox ID="TextBox22" runat="server"  Width="240px"></asp:TextBox>   
                                     </td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                        <asp:label id="Label27" Runat="server" Text="Insured Name" ></asp:label>
                                         <br />
                                           <asp:TextBox ID="TextBox23" runat="server"  Width="240px"></asp:TextBox>  
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="pageHeader" colspan="3">
                                        <asp:label id="Label28" Runat="server"  Text="Mail Address" ></asp:label>
                                    </td>
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                       <asp:label id="Label29" Runat="server" Text="Address1" ></asp:label>
                                       <br />
                                         <asp:TextBox ID="TextBox24" runat="server"  Width="240px"></asp:TextBox>  
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                           <asp:label id="Label30" Runat="server" Text="Address2" ></asp:label>
                                       <br />
                                         <asp:TextBox ID="TextBox25" runat="server"  Width="240px"></asp:TextBox>  </td>
                                     <td class="midcolora"  Width="33%">
                                           <asp:label id="Label31" Runat="server" Text="City" ></asp:label>
                                       <br />
                                         <asp:TextBox ID="TextBox26" runat="server"  Width="240px"></asp:TextBox>  </td>
                                     
                                 </tr> 
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                          <asp:label id="Label32" Runat="server" Text="State" ></asp:label>
                                       <br />
                                        <ext:ComboBox ID="ComboBox8" runat="server"  Width="240px"></ext:ComboBox>
                                       </td>
                                     <td class="midcolora"  Width="33%">
                                       <asp:label id="Label33" Runat="server" Text="Zip" ></asp:label>
                                       <br />
                                         <asp:TextBox ID="TextBox27" runat="server"  Width="240px"></asp:TextBox>  
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                        <asp:label id="Label34" Runat="server" Text="Country" ></asp:label>
                                       <br />
                                         <ext:ComboBox ID="ComboBox9" runat="server"  Width="240px"></ext:ComboBox>
                                     </td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                         <ext:Button ID="btnPrev" runat="server" Text="Previous ">
                                          <Listeners>
                                                <Click Handler="#{TabPanel1}.addTab(#{Tab1});" />
                                            </Listeners>
                                         </ext:Button></td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolorr"  Width="33%">
                                        <ext:Button ID="btnNext1" runat="server" Text="Next">
                                          <Listeners>
                                                <Click Handler="#{TabPanel1}.addTab(#{Tab3});" />
                                            </Listeners>
                                         </ext:Button>
                                         </td>
                                     
                                 </tr>
                                 <tr>
                                     <td class="midcolora"  Width="33%">
                                        
                                         <cmsb:cmsbutton id="btnExit" runat="server" causesvalidation="true" cssclass="clsButton" text="Exit" />
                                         <cmsb:cmsbutton id="btnDiary" runat="server" causesvalidation="true" cssclass="clsButton" text="Diary" />
                                         <cmsb:cmsbutton id="btnNotepad" runat="server" causesvalidation="true" cssclass="clsButton" text="Notepad" />
                                         <cmsb:cmsbutton id="btnLetter" runat="server" causesvalidation="true" cssclass="clsButton" text="Letter" />
                                            
                                     </td>
                                        
                                     <td class="midcolora"  Width="33%">
                                       
                                     </td>
                                     <td class="midcolorr"  Width="33%">
                                         <cmsb:cmsbutton id="btnConfirm" runat="server"  causesvalidation="true" cssclass="clsButton" text="Confirm" />
                                     </td>
                                 </tr>
                                  <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
                	        </tbody>
	                    </table>
                  </Body>
                </ext:Tab>
                <ext:Tab ID="Tab3" runat="server" Title="Location/Mortgagee"  >
                    <Body>
                    <table id="Table3" cellspacing="2" cellpadding="0" width="100%" border="0" >
                           <tr>
                            <td  class="midcolorc" colspan="3"><asp:Label runat="server" ID="Label36" CssClass="errmsg"></asp:Label></td>
                           </tr>
                            <tbody runat="server" id="tbody2">
                                 <tr>
                                       <td  align="right" colspan="3">
	                                     <asp:label id="Label37" runat="server" Visible="False" CssClass="errmsg" ></asp:label>
	                                   </td>
	                              </tr>
                                 <tr>
                                   <td class="pageHeader" align="left" colspan="3" >		           
		                               <asp:label id="lblpageHeader2" Runat="server" ></asp:label>
		                           </td>
	                            </tr>
                        	
	                            <tr>
		                            <td class="midcolorc" colspan="3"><asp:label id="Label39" runat="server" CssClass="errmsg" ></asp:label>
		                            </td>
	                            </tr>
                	            <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                             </td>
	                            </tr>
                    	         
                                  <tr>
                                     <td class="pageHeader" colspan="3">
                                           <asp:label id="Label52" Runat="server" Text="Insured" ></asp:label>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td class="midcolora" Width="33%">
                                       
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                           <asp:label id="Label55" Runat="server" Text="Date" ></asp:label>
                                       <br />
                                       <asp:TextBox ID="TextBox41" runat="server"  Width="240px"></asp:TextBox></td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                        <asp:label id="Label40" Runat="server" Text="Insured" ></asp:label>
                                        <br />
                                           <asp:TextBox ID="TextBox39" runat="server"  Width="240px"></asp:TextBox>
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                       <asp:label id="Label43" Runat="server" Text="Insured Name" ></asp:label>
                                        <br />
                                           <asp:TextBox ID="TextBox30" runat="server"  Width="240px"></asp:TextBox></td>
                                     <td class="midcolora"  Width="33%">
                                           <asp:label id="Label41" Runat="server" Text="Date" ></asp:label>
                                       <br />
                                       <asp:TextBox ID="TextBox29" runat="server"  Width="240px"></asp:TextBox></td>
                                     
                                 </tr>
                                  
                                  <tr>
                                     <td class="pageHeader" colspan="3">
                                        <asp:label  id="Label44" Runat="server" Text="Location" ></asp:label>
                                     </td>
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                        <asp:label id="Label47" Runat="server" Text="Location Line 1" ></asp:label>
                                         <br />
                                           <asp:TextBox ID="TextBox34" runat="server"  Width="240px"></asp:TextBox>  
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                       <asp:label id="Label35" Runat="server" Text="Location Line 2" ></asp:label>
                                         <br />
                                           <asp:TextBox ID="TextBox28" runat="server"  Width="240px"></asp:TextBox>  </td>
                                     <td class="midcolora"  Width="33%">
                                      <asp:label id="Label42" Runat="server" Text="Location City" ></asp:label>
                                         <br />
                                           <asp:TextBox ID="TextBox32" runat="server"  Width="240px"></asp:TextBox> 
                                     </td>
                                     
                                 </tr>
                                 
                                 
                                 
                                  <tr>
                                     <td class="pageHeader" colspan="3">
                                        <asp:label  id="Label48" Runat="server"  Text="Mortgagee" ></asp:label>
                                     </td>
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                       <asp:label id="Label49" Runat="server" Text="Mortgagee Line 1" ></asp:label>
                                       <br />
                                         <asp:TextBox ID="TextBox35" runat="server"  Width="240px"></asp:TextBox>  
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                           <asp:label id="Label50" Runat="server" Text="Mortgagee Line 1" ></asp:label>
                                       <br />
                                         <asp:TextBox ID="TextBox36" runat="server"  Width="240px"></asp:TextBox>  </td>
                                     <td class="midcolora"  Width="33%">
                                           <asp:label id="Label51" Runat="server" Text="Mortgagee Line 3" ></asp:label>
                                       <br />
                                         <asp:TextBox ID="TextBox37" runat="server"  Width="240px"></asp:TextBox>  </td>
                                     
                                 </tr> 
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                        <asp:label id="Label46" Runat="server" Text="Mortgagee Line 4" ></asp:label>
                                       <br />
                                         <asp:TextBox ID="TextBox33" runat="server"  Width="240px"></asp:TextBox> 
                                       </td>
                                     <td class="midcolora"  Width="33%">
                                       
                                     </td>
                                     <td class="midcolora"  Width="33%">
                                       
                                     </td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     
                                 </tr>
                                  <tr>
                                     <td class="midcolora" Width="33%">
                                         <ext:Button ID="Button1" runat="server" Text="Previous ">
                                          <Listeners>
                                                <Click Handler="#{TabPanel1}.addTab(#{Tab2});" />
                                            </Listeners>
                                         </ext:Button></td>
                                     <td class="midcolora"  Width="33%">
                                         &nbsp;</td>
                                     <td class="midcolorr"  Width="33%">
                                        <ext:Button ID="Button2" runat="server" Text="Next">
                                          <Listeners>
                                                <Click Handler="#{TabPanel1}.addTab(#{Tab4});" />
                                            </Listeners>
                                         </ext:Button>
                                         </td>
                                     
                                 </tr>
                                 <tr>
                                     <td class="midcolora"  Width="33%">
                                        
                                         <cmsb:cmsbutton id="btnExit1" runat="server" causesvalidation="true" cssclass="clsButton" text="Exit" />
                                         <cmsb:cmsbutton id="btnPolicy" runat="server" causesvalidation="true" cssclass="clsButton" text="Policy" />
                                         
                                            
                                     </td>
                                        
                                     <td class="midcolora"  Width="33%">
                                       
                                     </td>
                                     <td class="midcolorr"  Width="33%">
                                        
                                     </td>
                                 </tr>
                                  <tr>
		                            <td class="midcolora" colspan="3" >
		                             <asp:label id="Label38" Runat="server" Text="Press F18 to see additional mortgagees" ></asp:label>
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
	                              <tr>
		                            <td class="midcolora" colspan="3" >
		                            <br />
		                           </td>
	                             </tr>
                	        </tbody>
	                    </table>
                    </Body>
                </ext:Tab>
                <ext:Tab ID="Tab4" runat="server" Title="Wind Hail Risk & Quote" >
                </ext:Tab>
                <ext:Tab ID="Tab5" runat="server" Title="DEC & Others Documents"   >
                </ext:Tab>
            </Tabs>
            
        </ext:TabPanel>
        
    </div>
     </form>
    </div>
    
   <webcontrol:Footer id="pageFooter" runat="server"></webcontrol:Footer>
</body>
</html>
