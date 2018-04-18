IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyWaterCraftTrailer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyWaterCraftTrailer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name        : dbo.Proc_FetchPolicyWaterCraftTrailer    
Created by        : Vijay Arora    
Date              : 28-11-2005    
Purpose         : Retrieving data from POL_WATERCRAFT_TRAILER_INFO      
Revison History  :      
Used In          : Wolverine      
      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/    
-- DROP  PROC dbo.Proc_FetchPolicyWaterCraftTrailer       
CREATE  PROC dbo.Proc_FetchPolicyWaterCraftTrailer      
@CUSTOMER_ID INT,      
@POLICY_ID INT,      
@POLICY_VERSION_ID INT,      
@TRAILER_ID INT      
AS      
      
BEGIN      
SELECT       
TRAILER_ID,      
TRAILER_NO,      
[YEAR],  
MODEL,      
MANUFACTURER,      
SERIAL_NO,      
INSURED_VALUE,      
ASSOCIATED_BOAT,      
IS_ACTIVE,  
TRAILER_TYPE,  
TRAILER_DED,
TRAILER_DED_ID,
TRAILER_DED_AMOUNT_TEXT  
       
FROM POL_WATERCRAFT_TRAILER_INFO       
WHERE POLICY_ID=@POLICY_ID     
AND  POLICY_VERSION_ID=@POLICY_VERSION_ID      
AND  CUSTOMER_ID=@CUSTOMER_ID     
AND  TRAILER_ID=@TRAILER_ID      
END      
      
    
  
  
  
  
  



GO

