IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSchItemCovgForClaims]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSchItemCovgForClaims]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].Proc_GetSchItemCovgForClaims              
 @CLAIM_ID INTEGER              
AS            
BEGIN              
 if exists(select claim_id from clm_activity_reserve where claim_id=@claim_id and (vehicle_id=0 or vehicle_id is null))    
 begin    
 exec Proc_GetOldSchItemCovgForClaims @CLAIM_ID    
 end    
 else    
 begin    
 exec Proc_GetExistingSchItemCovgForClaims @CLAIM_ID    
 end    
END    
  



GO

