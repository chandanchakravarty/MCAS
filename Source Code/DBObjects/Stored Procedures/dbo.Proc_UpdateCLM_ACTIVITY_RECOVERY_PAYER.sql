IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_ACTIVITY_RECOVERY_PAYER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_ACTIVITY_RECOVERY_PAYER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
----------------------------------------------------------                      
Proc Name       : dbo.Proc_UpdateCLM_ACTIVITY_RECOVERY_PAYER                      
Created by      : Sumit Chhabra
Date            : 6/1/2006                      
Purpose         : To Update the record in table named CLM_ACTIVITY_RECOVERY_PAYER                      
Revison History :                      
Used In        : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
CREATE PROC dbo.Proc_UpdateCLM_ACTIVITY_RECOVERY_PAYER                      
(                      
@CLAIM_ID     int,                      
@ACTIVITY_ID int,                    
@PAYER_ID     int, 
@RECOVERY_TYPE int,
@RECEIVED_DATE datetime,
@RECEIVED_FROM varchar(50),
@CHECK_NUMBER varchar(50),
@DESCRIPTION varchar(300),
@MODIFIED_BY int,
@LAST_UPDATED_DATETIME datetime
                      
)                      
AS                      
BEGIN                                     

	UPDATE CLM_ACTIVITY_RECOVERY_PAYER                      
	SET
		RECOVERY_TYPE=@RECOVERY_TYPE,
		RECEIVED_DATE=@RECEIVED_DATE,
		RECEIVED_FROM=@RECEIVED_FROM,
		CHECK_NUMBER=@CHECK_NUMBER,
		DESCRIPTION=@DESCRIPTION,
		MODIFIED_BY=@MODIFIED_BY,
		LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
	WHERE                     
		CLAIM_ID = @CLAIM_ID and ACTIVITY_ID=@ACTIVITY_ID AND PAYER_ID=@PAYER_ID
    
END                      
                      
                    
                  
                
              
            
          
        
      
    
  



GO

