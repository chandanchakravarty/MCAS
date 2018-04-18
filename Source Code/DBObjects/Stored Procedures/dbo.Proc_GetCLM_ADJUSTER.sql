IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ADJUSTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ADJUSTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name       : Dbo.Proc_GetCLM_ADJUSTER                  
Created by      : Amar                  
Date            : 4/21/2006                  
Purpose       :Evaluation                  
Revison History :                  
Used In        : Wolverine                  
------------------------------------------------------------                  
Modified By  : Asfa Praveen      
Date   : 29/Aug/2007      
Purpose  : To add USER_ID column      
------------------------------------------------------------                                                                      
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--DROP PROC dbo.Proc_GetCLM_ADJUSTER                  
CREATE PROC [dbo].[Proc_GetCLM_ADJUSTER]             
(                    
@ADJUSTER_ID     int                    
)                    
AS                    
BEGIN                    
select ADJUSTER_ID,                    
ADJUSTER_TYPE,                    
ADJUSTER_NAME,                    
ADJUSTER_CODE,                    
SUB_ADJUSTER,                    
SUB_ADJUSTER_LEGAL_NAME,                    
SUB_ADJUSTER_ADDRESS1,                    
SUB_ADJUSTER_ADDRESS2,                    
SUB_ADJUSTER_CITY,                    
SUB_ADJUSTER_STATE,                    
SUB_ADJUSTER_ZIP,                    
SUB_ADJUSTER_PHONE,                    
SUB_ADJUSTER_FAX,                    
SUB_ADJUSTER_EMAIL,                    
SUB_ADJUSTER_WEBSITE,                    
SUB_ADJUSTER_NOTES,                
SUB_ADJUSTER_COUNTRY,                  
SUB_ADJUSTER_CONTACT_NAME,       
CA.IS_ACTIVE,            
SA_ADDRESS1,            
SA_ADDRESS2,            
SA_CITY,            
SA_COUNTRY,            
SA_STATE,            
SA_ZIPCODE,            
SA_PHONE,            
SA_FAX,          
ISNULL(LOB_ID,'') AS LOB_ID,        
CA.USER_ID      ,  
CA.DISPLAY_ON_CLAIM, -- ADDED BY SANTOSH KUMAR GAUTAM ON 13 APRIL 2011 

-- Added by Agniswar on 16 SEPTEMBER 2011
CA.SUB_ADJUSTER_GST, 
CA.SUB_ADJUSTER_GST_REG_NO,
CA.SUB_ADJUSTER_MOBILE,
CA.SUB_ADJUSTER_CLASSIFICATION            
 
from  CLM_ADJUSTER  CA                  
--LEFT JOIN MNT_USER_LIST MUL ON CA.USER_ID= MUL.USER_ID      
where  ADJUSTER_ID = @ADJUSTER_ID                    
                    
END          
      
    
  
  
GO

