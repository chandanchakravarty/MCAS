IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNextDwelling_NumberForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNextDwelling_NumberForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_GetNextDwelling_Number         
Created by  :shafi          
Date        :17 feb 2006       
Purpose     : Returns the next DWELLING_NUMBER for the   
       CUSTOMERID, POLID and POLVersionID           
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/    
  
CREATE      PROCEDURE Proc_GetNextDwelling_NumberForPolicy  
(  
   
 @CUSTOMER_ID Int,  
 @POL_ID Int,  
 @POL_VERSION_ID SmallInt  
)  
  
As  
  
DECLARE @MAX BigInt  
  
   
 SELECT @MAX = ISNULL(MAX(DWELLING_NUMBER),0)  
 FROM POL_DWELLINGS_INFO  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POL_ID AND   
  POLICY_VERSION_ID = @POL_VERSION_ID   
   
   
 IF @MAX = 2147483647  
 BEGIN  
  SELECT -1  
  RETURN    
 END  
  
 SELECT @MAX + 1  
  
  
  
  



GO

