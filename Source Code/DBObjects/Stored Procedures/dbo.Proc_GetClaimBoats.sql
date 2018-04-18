IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimBoats]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimBoats]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*     
----------------------------------------------------------              
Proc Name            : dbo.Proc_GetClaimBoats    
Created by             : Sumit Chhabra
Date                    : 09-11-2006    
Purpose                : To get all the vehicles for policy and Claims     
Revison History   :              
Used In                 :   Wolverine              
------   ------------       -------------------------*/              
--drop proc dbo.Proc_GetClaimBoats    
CREATE PROC dbo.Proc_GetClaimBoats         
(              
    @CLAIM_ID  int      
)              
AS              
BEGIN             
 SELECT   
	BOAT_ID,SERIAL_NUMBER,YEAR,MAKE,MODEL
  FROM CLM_INSURED_BOAT WHERE CLAIM_ID = @CLAIM_ID    

END        



GO

