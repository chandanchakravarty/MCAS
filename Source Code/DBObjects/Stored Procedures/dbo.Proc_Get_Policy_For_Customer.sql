IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_Policy_For_Customer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_Policy_For_Customer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_Get_Policy_For_Customer            
Created by      : Ravindra            
Date            : 03-10-2005           
Purpose         :           
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/  
--Drop PROCEDURE dbo.Proc_Get_Policy_For_Customer           
CREATE PROCEDURE dbo.Proc_Get_Policy_For_Customer          
(              
  @CUSTOMER_ID int  ,          
  @LOB_ID int = null,      
  @AGENCY_ID int =null             
)                  
AS                       
          
BEGIN                            
    
 IF NOT @LOB_ID IS NULL        
 BEGIN        
  SELECT POLICY_NUMBER + ' - ' + POLICY_DISP_VERSION + ' ' + isnull(CONVERT(VARCHAR(15),POLICY_EFFECTIVE_DATE,101),'') +   
CASE WHEN POLICY_EXPIRATION_DATE IS NOT NULL THEN ' - ' + isnull(CONVERT(VARCHAR(15),POLICY_EXPIRATION_DATE,101),'')  
ELSE '' END   
  AS POLICY_NUMBER,          
   ' - ' + CONVERT(VARCHAR(100),POLICY_ID) + ' - ' + CONVERT(VARCHAR(10),POLICY_VERSION_ID) AS POLICY_ID        
   FROM POL_CUSTOMER_POLICY_LIST           
   WHERE CUSTOMER_ID=@CUSTOMER_ID          
   AND POLICY_LOB=@LOB_ID          
   AND NOT POLICY_NUMBER IS NULL          
   AND NOT POLICY_DISP_VERSION IS NULL       
   AND ISNULL(IS_ACTIVE,'')='Y'    
  union    
  SELECT (APP_NUMBER + ' - ' + APP_VERSION + ' ' + isnull(CONVERT(VARCHAR(15),APP_EFFECTIVE_DATE,101),'') +   
CASE WHEN APP_EXPIRATION_DATE IS NOT NULL THEN ' - ' + isnull(CONVERT(VARCHAR(15),APP_EXPIRATION_DATE,101),'')  
ELSE '' END ) AS POLICY_NUMBER,          
   'APP - ' + (CONVERT(VARCHAR(100),APP_ID) + ' - ' + CONVERT(VARCHAR(10),APP_VERSION_ID)) AS POLICY_ID        
   FROM APP_LIST    
   WHERE CUSTOMER_ID=@CUSTOMER_ID          
   AND APP_LOB=@LOB_ID          
   AND NOT APP_NUMBER IS NULL          
   AND NOT APP_VERSION IS NULL     
   AND APP_STATUS = 'Incomplete'     
      AND ISNULL(IS_ACTIVE,'')='Y'    
      
 END        
 ELSE IF ISNULL(@AGENCY_ID,0) <> 0      
  BEGIN       
   SELECT POLICY_ID, CUSTOMER_ID, POLICY_VERSION_ID, POLICY_NUMBER, POLICY_DISP_VERSION        
   FROM POL_CUSTOMER_POLICY_LIST PCL        
   WHERE CUSTOMER_ID = @CUSTOMER_ID   AND AGENCY_ID=@AGENCY_ID      
   AND POLICY_STATUS = 'NORMAL' AND ISNULL(IS_ACTIVE, '') = 'Y'        
  END        
 ELSE       
 BEGIN        
  SELECT POLICY_ID, CUSTOMER_ID, POLICY_VERSION_ID, POLICY_NUMBER, POLICY_DISP_VERSION        
  FROM POL_CUSTOMER_POLICY_LIST PCL        
  WHERE CUSTOMER_ID = @CUSTOMER_ID        
  AND POLICY_STATUS = 'NORMAL' AND ISNULL(IS_ACTIVE, '') = 'Y'        
 END        
      
END        



GO

