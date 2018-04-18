IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_GetNextPolicyDwelling_Number]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_GetNextPolicyDwelling_Number]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_UM_GetNextPolicyDwelling_Number            
Created by      : Ravindra
Date            : 03-21-2006

------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/   


 CREATE      PROCEDURE Proc_UM_GetNextPolicyDwelling_Number  
(  
   
 @CUSTOMER_ID Int,  
 @POLICY_ID Int,  
 @POLICY_VERSION_ID SmallInt  
)  
  
As  
  
DECLARE @MAX BigInt  
  
   
 SELECT @MAX = ISNULL(MAX(DWELLING_NUMBER),0)  
 FROM POL_UMBRELLA_DWELLINGS_INFO  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND   
  POLICY_VERSION_ID = @POLICY_VERSION_ID   
   
   
 IF @MAX = 2147483647  
 BEGIN  
  SELECT -1  
  RETURN    
 END  
  
 SELECT @MAX + 1  
  
  
  



GO

