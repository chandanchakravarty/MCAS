IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUmbLetterData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUmbLetterData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                              
Proc Name       : dbo.Proc_GetUmbLetterData                                                              
Created by      :  Mohit Agarwal                                                              
Date            :  22-Jun-2007                                                              
Purpose         :  To get Umbrella Letter data                                                              
Revison History :                                                              
Used In         :    Wolverine                                                              
                                                              
Modified By:                                                            
Modified Date :                                                             
                                                              
------------------------------------------------------------                                                              
Date     Review By          Comments                                                              
------   ------------       -------------------------*/                                                              
--drop PROC Dbo.Proc_GetUmbLetterData                  
                                             
CREATE PROC dbo.Proc_GetUmbLetterData                                                             
(                                                                    
@CUSTOMER_ID int,                                                    
@POLICY_ID int,                                                    
@POLICY_VERSION_ID int,                                
@CALLED_FROM varchar(20)=null              
)                                                                    
AS                                                                    
BEGIN               
 SELECT                                                    
  ISNULL(CCL.CUSTOMER_FIRST_NAME,'') + ' ' + CASE WHEN CCL.CUSTOMER_MIDDLE_NAME IS NULL THEN '' ELSE CCL.CUSTOMER_MIDDLE_NAME + ' ' END                          
  + ISNULL(CCL.CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME,                                                                                                    
  MAL.AGENCY_DISPLAY_NAME,                                                    
  PCPL.POLICY_NUMBER AS POLICY_NUMBER,  PCPL.APP_EFFECTIVE_DATE, PCPL.APP_EXPIRATION_DATE, MLM.LOB_CODE, MLM.LOB_DESC AS LOB_DESC,                                         
  DateAdd(day, -45, APP_EFFECTIVE_DATE) AS DUE_DATE, CCL1.CUSTOMER_BUSINESS_PHONE AS UNDERWRITER_PHONE,  
   CCL1.CUSTOMER_EXT AS UNDERWRITER_EXT, CCL1.CUSTOMER_Email AS UNDERWRITER_EMAIL,  
   ISNULL(CCL1.CUSTOMER_FIRST_NAME,'') + ' ' + CASE WHEN CCL1.CUSTOMER_MIDDLE_NAME IS NULL THEN '' ELSE CCL1.CUSTOMER_MIDDLE_NAME + ' ' END                          
  + ISNULL(CCL1.CUSTOMER_LAST_NAME,'') AS UNDERWRITER_NAME    
                                                     
 FROM POL_CUSTOMER_POLICY_LIST  PCPL  with(nolock)                                                   
                                  
   INNER JOIN CLT_CUSTOMER_LIST CCL  with(nolock)  ON                                                    
    CCL.CUSTOMER_ID=PCPL.CUSTOMER_ID                            
                                        
   LEFT OUTER JOIN CLT_CUSTOMER_LIST CCL1 with(nolock) ON  
    CCL1.CUSTOMER_ID=PCPL.UNDERWRITER     
             
   LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MCSL   with(nolock) ON                                                    
   CCL.CUSTOMER_STATE = MCSL.STATE_ID                                                    
                                                     
   LEFT OUTER JOIN MNT_LOB_MASTER MLM   with(nolock) ON                                     
   PCPL.POLICY_LOB = MLM.LOB_ID                                                    
                                      
   LEFT OUTER JOIN MNT_AGENCY_LIST MAL  with(nolock) ON                                                    
   PCPL.AGENCY_ID = MAL.AGENCY_ID                                            
                                       
 WHERE PCPL.CUSTOMER_ID=@CUSTOMER_ID AND PCPL.POLICY_ID=@POLICY_ID AND PCPL.POLICY_VERSION_ID=@POLICY_VERSION_ID                   
       
                           
END                                               
                                  
                              
                            
                          
                        
                      
                    
                  
                
              
              
              
            
          
        
      
    
  



GO

