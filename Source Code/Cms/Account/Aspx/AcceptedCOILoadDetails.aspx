<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcceptedCOILoadDetails.aspx.cs" Inherits="Cms.Account.Aspx.AcceptedCOILoadDetails"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Accepted COI Load Details</title>
       <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
       	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
     <script language='javascript' type="text/javascript">

        
         function btnclick(BtnID) 
         {

             document.getElementById('hyper_application_Detail').style.color = '#003399';
             document.getElementById('hyper_Coverage_Detail').style.color = '#003399';
             document.getElementById('hyper_Coverage_Detail_Exc').style.color = '#003399';
             document.getElementById('hyper_application_Detail_Exc').style.color = '#003399';
             document.getElementById('hyper_Ins_Detail').style.color = '#003399';
             document.getElementById('hyper_Ins_Detail_Exc').style.color = '#003399';
             document.getElementById('hyper_Claim_Detl_Exc').style.color = '#003399';
             document.getElementById('hyper_Claim_Paid_Exc').style.color = '#003399';
             document.getElementById('hyper_Claim_Detl').style.color = '#003399';
             document.getElementById('hyper_Claim_Paid').style.color = '#003399';

             document.getElementById(BtnID).style.color = 'red';
             
             

         } 

       
     </script>
    <style type="text/css">
                
        .color1
        {
        
            color:#003399;
         
        }
       
      hr
      {
          width:125;         
      }
       
       
    </style>
    
</head>
<body>
    <form id="AccepetedCOLLoadDetails" method="post" runat="server" name="form">
    <table class="tableWidthHeader" align="center" border="0" width="100%" cellspacing="0" cellpadding="0">
     <tr>
            <td class="headereffectCenter" colspan=2><asp:Label ID="capHead" runat="server"></asp:Label></td><%--Accepted COI Load--%>
        </tr>
        <tr>
           <td class="midcolora" width="180">
             <ul class="color1" style="list-style-type:square; margin-left:10;">
                <li>
                    <b> <asp:Label ID="capExceptionDetail" runat="server">Exception Detail</asp:Label></b>
                      <ul style="list-style-type:disc;">
                         <li style="list-style-type:none"><br/></li>
                         <li>
                            <a class="color1" id="hyper_application_Detail_Exc"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                         </li>
                         <li style="list-style-type:none"><hr/></li>
                         <li>
                             <a class="color1"  id="hyper_Coverage_Detail_Exc"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                          </li>
                          <li style="list-style-type:none"><hr/></li>
                          <li>
                             <a class="color1"  id="hyper_Ins_Detail_Exc"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                          </li>
                          <li style="list-style-type:none"><hr/></li>
                          <li>
                             <a class="color1"  id="hyper_Claim_Detl_Exc" onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                          </li>
                          <li style="list-style-type:none"><hr/></li>
                          <li>
                             <a class="color1"  id="hyper_Claim_Paid_Exc" onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                          </li>
                          <li style="list-style-type:none"><hr/></li>
                       </ul>
                </li>
              </ul>
              
               <ul class="color1" style="list-style-type:square; margin-left:10;">
                 <li>
                   <b><asp:Label ID="capProcessedRecord" runat="server"></asp:Label></b><%--Processed Record--%>
                 
               <ul style="list-style-type:disc;">
                  <li style="list-style-type:none"><br/></li>
                  <li>
                      <a class="color1" id="hyper_application_Detail"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                  </li>
                  <li style="list-style-type:none"><hr/></li>
                  <li>
                      <a class="color1"  id="hyper_Coverage_Detail"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                  </li>
                  <li style="list-style-type:none"><hr/></li>
                  <li>
                     <a class="color1"  id="hyper_Ins_Detail"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                  </li>
                  <li style="list-style-type:none"><hr/></li>
                  <li>
                    <a class="color1"  id="hyper_Claim_Detl"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                  </li>
                  <li style="list-style-type:none"><hr/></li>
                  <li>
                    <a class="color1"  id="hyper_Claim_Paid"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                  </li>
                  <li style="list-style-type:none"><hr/></li>
                </ul>
               </li>
              </ul>  
              </td>         
           <td class="midcolora" >
             	<iframe class="iframsHeightLong" id="tabLayer" name="tabLayer" runat="server"  scrolling="no" frameborder="0"	width="100%"></iframe>	 
          </td>
        </tr>
    </table>
    <input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
	<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
   </form>
</body>
</html>
