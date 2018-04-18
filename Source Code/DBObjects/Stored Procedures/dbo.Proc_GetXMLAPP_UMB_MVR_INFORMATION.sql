IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLAPP_UMB_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLAPP_UMB_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
Proc Name        :            dbo.Proc_GetXMLAPP_UMB_MVR_INFORMATION                               
Created by         :           Sumit Chhabra                              
Date                :           22/03/2006                              
Purpose           :           Get the table information FOR APP_UMBRELLA_MVR_INFORMATION
Revison History  :                              
Used In             :           Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
CREATE PROC Dbo.Proc_GetXMLAPP_UMB_MVR_INFORMATION              
(                              
 @CUSTOMER_ID INT,          
 @APP_ID INT,          
 @APP_VERSION_ID INT,          
 @DRIVER_ID INT,                   
 @APP_UMB_MVR_ID  INT                         
)                              
AS                              
BEGIN                              
 SELECT A.APP_UMB_MVR_ID,                              
 A.CUSTOMER_ID,                              
 A.APP_ID,                              
 A.APP_VERSION_ID,                              
 A.DRIVER_ID,                          
 A.VIOLATION_ID,                              
 A.DRIVER_ID,                              
 A.MVR_AMOUNT,                              
 A.MVR_DEATH,      
 A.VERIFIED,      
 A.VIOLATION_TYPE,                            
 convert(char,A.mvr_date,101) MVR_DATE,                
-- (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +  '/' +  CAST(isnull(M.SD_POINTS,'') AS VARCHAR)  +  ')' )AS VIOLATION_DES,          
 (isnull(M.VIOLATION_DES,'') + ' (' + CAST(isnull(M.MVR_POINTS,'') AS VARCHAR) +    ')' )AS VIOLATION_DES,          
 A.IS_ACTIVE                 
 FROM  APP_UMBRELLA_MVR_INFORMATION A JOIN MNT_VIOLATIONS M        
 ON A.VIOLATION_ID=M.VIOLATION_ID        
 WHERE          
 A.CUSTOMER_ID=@CUSTOMER_ID AND           
 A.APP_ID=@APP_ID AND          
 A.APP_VERSION_ID=@APP_VERSION_ID AND          
 A.DRIVER_ID=@DRIVER_ID AND           
 A.APP_UMB_MVR_ID=@APP_UMB_MVR_ID                                  
END              
              
            


GO

