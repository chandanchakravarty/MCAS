IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillVendorDropDown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillVendorDropDown]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------          
Proc Name       : dbo.Proc_FillVendorDropDown          
Created by      :  Ajit Singh Chahal          
Date                :  04/20/2005          
Purpose         :  To fill drop down of finance company names          
Revison History :          
Used In         :    Wolverine          
      
Reviewed By : Anurag Verma      
Reviewed On : 06-07-2007      
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
-- drop proc dbo.Proc_FillVendorDropDown        
CREATE PROCEDURE [dbo].[Proc_FillVendorDropDown]         
AS        
begin          
 select CAST(ISNULL(VENDOR_ID,'') AS VARCHAR) + '~' + ISNULL(COMPANY_NAME,'') + '~' + ISNULL(CHK_MAIL_ADD1,'') + '~' + ISNULL(CHK_MAIL_ADD2,'') + '~' +         
 ISNULL(CHK_MAIL_CITY,'') + '~' + ISNULL(SL.STATE_name,'') + '~' +      
--CAST(ISNULL(ALLOWS_EFT,'10964') AS VARCHAR)     
 CAST( CASE     
 WHEN ISNULL(VL.ACCOUNT_ISVERIFIED,'10964') = 10963     
 AND  ISNULL(VL.ALLOWS_EFT,'10964')  = 10963    
 THEN 10963    
 ELSE 10964 END    
 AS VARCHAR    
      ) + '~'   
+ CAST(ISNULL(REQ_SPECIAL_HANDLING,10964)AS VARCHAR)  
AS COMPANY_NAME_DATA,     
--+ ISNULL(CHK_MAIL_STATE,'')AS COMPANY_NAME_DATA,        
 COMPANY_NAME + ' - '  + isnull(VENDOR_ACC_NUMBER,'') as COMPANY_NAME,        
 VENDOR_FNAME+' '+VENDOR_LNAME as VENDOR_FNAME,VENDOR_ID        
 from MNT_VENDOR_LIST VL        
 left outer join MNT_COUNTRY_STATE_LIST SL ON     
--SL.STATE_ID =  VL.VENDOR_STATE and      
vl.VENDOR_COUNTRY=sl.COUNTRY_ID     
and  VL.CHK_MAIL_STATE=SL.STATE_ID     
 where VL.IS_ACTIVE = 'Y'         
 order by COMPANY_NAME          
End      
    
     
 
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
  
GO

