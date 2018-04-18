IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNextPolUmbrellaRecrCompany_ID_Number]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNextPolUmbrellaRecrCompany_ID_Number]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------          
Proc Name   : dbo.Proc_GetNextPolUmbrellaRecrCompany_ID_Number         
Created by  :Sumit Chhabra          
Date        :27 March,2006        
Purpose     : Get new company ID for POL_UMBRELLA_RECREATIONAL_VEHICLES
Revison History  :                
 ------------------------------------------------------------                      
Date     Review By          Comments                    
           
------   ------------       -------------------------*/    
  
CREATE      PROCEDURE Proc_GetNextPolUmbrellaRecrCompany_ID_Number  
(  
   
 @CUSTOMER_ID Int,  
 @POLICY_ID Int,  
 @POLICY_VERSION_ID SmallInt  
)  
  
As  
  
 DECLARE @MAX BigInt  
   
 SELECT @MAX = ISNULL(MAX(COMPANY_ID_NUMBER),0)  
 FROM POL_UMBRELLA_RECREATIONAL_VEHICLES  
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

