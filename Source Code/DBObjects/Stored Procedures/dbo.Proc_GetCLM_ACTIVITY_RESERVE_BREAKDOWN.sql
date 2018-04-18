IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ACTIVITY_RESERVE_BREAKDOWN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_ACTIVITY_RESERVE_BREAKDOWN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                  
Proc Name       : dbo.Proc_GetCLM_ACTIVITY_RESERVE_BREAKDOWN                                                      
Created by      : Sumit Chhabra                                                                
Date            : 01/06/2006                                                                  
Purpose         : Fetch data from CLM_ACTIVITY_RESERVE_BREAKDOWN table for claim reserve breakdown screen                                              
Created by      : Sumit Chhabra                                                                 
Revison History :                                                                  
Used In        : Wolverine                                                                  
------------------------------------------------------------                                                                  
Date     Review By          Comments                                                                  
------   ------------       -------------------------*/                                                                  
CREATE PROC dbo.Proc_GetCLM_ACTIVITY_RESERVE_BREAKDOWN                                                        
@CLAIM_ID int,            
@ACTIVITY_ID int,            
@RESERVE_BREAKDOWN_ID int                                              
AS                                                                  
BEGIN                
  
 SELECT                                               
	CLAIM_ID,ACTIVITY_ID,RESERVE_BREAKDOWN_ID,TRANSACTION_CODE,BASIS,VALUE,AMOUNT,IS_ACTIVE
 FROM                                               
  CLM_ACTIVITY_RESERVE_BREAKDOWN                     
 WHERE            
  CLAIM_ID = @CLAIM_ID AND
	ACTIVITY_ID = @ACTIVITY_ID AND
	RESERVE_BREAKDOWN_ID = @RESERVE_BREAKDOWN_ID                                           
                                                      
END  


GO

