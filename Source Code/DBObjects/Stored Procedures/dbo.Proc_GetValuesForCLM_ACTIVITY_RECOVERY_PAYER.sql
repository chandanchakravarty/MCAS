IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetValuesForCLM_ACTIVITY_RECOVERY_PAYER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetValuesForCLM_ACTIVITY_RECOVERY_PAYER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
----------------------------------------------------------                      
Proc Name       : dbo.Proc_GetValuesForCLM_ACTIVITY_RECOVERY_PAYER                      
Created by      : Sumit Chhabra
Date            : 6/1/2006                      
Purpose         : To Get the record in table named CLM_ACTIVITY_RECOVERY_PAYER                      
Revison History :                      
Used In        : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
CREATE PROC dbo.Proc_GetValuesForCLM_ACTIVITY_RECOVERY_PAYER                      
(                      
@CLAIM_ID     int,                      
@ACTIVITY_ID int,                    
@PAYER_ID     int     
)                      
AS                      
BEGIN 

	SELECT 
		RECOVERY_TYPE,
		CONVERT(CHAR,RECEIVED_DATE,101) RECEIVED_DATE,  
		RECEIVED_FROM,
		CHECK_NUMBER,
		DESCRIPTION
	FROM                                     
	 CLM_ACTIVITY_RECOVERY_PAYER                      
	WHERE                     
		CLAIM_ID = @CLAIM_ID and ACTIVITY_ID=@ACTIVITY_ID AND PAYER_ID=@PAYER_ID
    
END                      
                      
                    
                  
                
              
            
          
        
      
    
  



GO

