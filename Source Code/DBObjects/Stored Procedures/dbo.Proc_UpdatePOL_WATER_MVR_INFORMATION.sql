IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_WATER_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_WATER_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name       : Dbo.Proc_UpdatePOL_WATER_MVR_INFORMATION              
Created by      : Anurag Verma            
Date            : 08/11/2005              
Purpose         :Update of Water Driver Policy MVR Information              
Revison History :              
Used In                   : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC Dbo.Proc_UpdatePOL_WATER_MVR_INFORMATION              
(              
 @POL_WATER_MVR_ID  int,              
 @CUSTOMER_ID    int,              
 @POL_ID      int,              
 @POL_VERSION_ID int,              
 @VIOLATION_ID      int,              
 @DRIVER_ID      int,              
 @MVR_AMOUNT     decimal(20,0),    
 @MVR_DEATH      nvarchar(2),              
 @MVR_DATE      datetime        
)              
AS              
BEGIN              
              
 UPDATE  POL_WATER_MVR_INFORMATION              
 SET                            
  VIOLATION_ID    =  @VIOLATION_ID,              
  MVR_AMOUNT   =  @MVR_AMOUNT,              
  MVR_DEATH    =  @MVR_DEATH,              
  MVR_DATE    =  @MVR_DATE        
 WHERE                  
  CUSTOMER_ID	   = @CUSTOMER_ID and
  POLICY_ID	   = @POL_ID and
  POLICY_VERSION_ID   = @POL_VERSION_ID and
  POL_WATER_MVR_ID = @POL_WATER_MVR_ID                        
END        
  





GO

