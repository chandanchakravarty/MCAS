IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLPOL_UMBRELLA_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLPOL_UMBRELLA_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name         : dbo.Proc_GetXMLPOL_UMBRELLA_MVR_INFORMATION                           
Created by          :       Sumit Chhabra
Date                 :       22-03-2006        
Purpose            :       Get the table information 
Revison History   :                         
Used In              :       Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
CREATE PROC Dbo.Proc_GetXMLPOL_UMBRELLA_MVR_INFORMATION          
(                          
                
 @CUSTOMER_ID INT,          
 @POLICY_ID INT,          
 @POLICY_VERSION_ID INT,          
 @DRIVER_ID INT,        
 @POL_UMB_MVR_ID INT                                
)                          
AS                          
BEGIN                          
 SELECT                          
 P.CUSTOMER_ID,                          
 P.POLICY_ID,                          
 P.POLICY_VERSION_ID,                          
 P.DRIVER_ID,                      
 P.POL_UMB_MVR_ID,        
 P.VIOLATION_ID,                          
 P.DRIVER_ID,                          
 P.MVR_AMOUNT,                          
 P.MVR_DEATH,        
 P.VERIFIED,      
 P.VIOLATION_TYPE,  
 CONVERT(CHAR,P.MVR_DATE,101) MVR_DATE,            
 P.IS_ACTIVE,      
  (M.VIOLATION_DES + ' (' + CAST(M.MVR_POINTS AS VARCHAR) +  ')' )AS VIOLATION_DES      
 FROM  POL_UMBRELLA_MVR_INFORMATION P JOIN MNT_VIOLATIONS M       
 ON P.VIOLATION_ID=M.VIOLATION_ID      
 WHERE          
 P.CUSTOMER_ID=@CUSTOMER_ID AND          
 P.POLICY_ID= @POLICY_ID AND          
 P.POLICY_VERSION_ID= @POLICY_VERSION_ID AND          
 P.DRIVER_ID= @DRIVER_ID AND                                                             
 P.POL_UMB_MVR_ID=@POL_UMB_MVR_ID         
         
END          
          
          
          
        
      
    
  



GO

