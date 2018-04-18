<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitialLoadCommitedPolicyList.aspx.cs" Inherits="Cms.Account.Aspx.InitialLoadCommitedPolicyList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Initial Details</title>
       <LINK href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css" type=text/css rel=stylesheet >
       	<script src="/cms/cmsweb/scripts/xmldom.js"></script>
		<script src="/cms/cmsweb/scripts/common.js"></script>
		<script src="/cms/cmsweb/scripts/form.js"></script>
     <script language='javascript' type="text/javascript">


         function btnclick(BtnID) {
                 document.getElementById('hyper_ExceptionDetail').style.color = '#003399';
                 document.getElementById('hyper_ProcessedRecord').style.color = '#003399';
                 document.getElementById(BtnID).style.color = 'red';
              
         } 

       
     </script>
    <style type="text/css">
                
        .color1
        {
        
            color:#003399;
            font-size:13.5px;
         
        }
       
      hr
      {
          width:135;         
      }
       
       
    </style>
    
</head>
<body>
    <form id="AccepetedCOLLoadDetails" method="post" runat="server" name="form">
    <table class="tableWidthHeader" align="center" border="0" width="100%" cellspacing="0" cellpadding="0">
     <tr>
            <td class="headereffectCenter" colspan=2><asp:Label ID="capHead" runat="server"></asp:Label></td> 
        </tr>
        <tr>
           <td class="midcolora" width="180">
             <ul class="color1" style="list-style-type:square; margin-left:10; margin-top:20px;">
                <li>
                     <a class="color1" id="hyper_ExceptionDetail"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                     <hr />
                 </li>
                 <li id="TdProccessedRecordLink" runat="server">
                      <a class="color1" id="hyper_ProcessedRecord"  onclick="btnclick(this.id);"  target="tabLayer" runat="server"></a>
                     <hr />
                  </li>
              </ul>
            </td>         
           <td class="midcolora" >
             	<iframe class="iframsHeightLong" id="tabLayer" name="tabLayer" runat="server"  scrolling="no" frameborder="0"	width="100%"></iframe>	 
          </td>
        </tr>
        <input type="hidden" id="hidIMPORT_FILE_TYPE_NAME" runat="server" />
    </table>
    
    <input type="hidden" name="hidTemplateID"> <input type="hidden" name="hidRowID">
	<input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr"> <input type="hidden" name="hidMode">
    
   </form>
</body>
</html>
