IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCLM_ACTIVITY_RECOVERY_PAYER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCLM_ACTIVITY_RECOVERY_PAYER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
----------------------------------------------------------                      
Proc Name       : dbo.Proc_InsertCLM_ACTIVITY_RECOVERY_PAYER                      
Created by      : Sumit Chhabra
Date            : 6/1/2006                      
Purpose         : To Insert the record in table named CLM_ACTIVITY_RECOVERY_PAYER                      
Revison History :                      
Used In        : Wolverine                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/                      
CREATE PROC dbo.Proc_InsertCLM_ACTIVITY_RECOVERY_PAYER                      
(                      
@CLAIM_ID     int,                      
@ACTIVITY_ID int,                    
@PAYER_ID     int OUTPUT, 
@RECOVERY_TYPE int,
@RECEIVED_DATE datetime,
@RECEIVED_FROM varchar(50),
@CHECK_NUMBER varchar(50),
@DESCRIPTION varchar(300),
@CREATED_BY int,
@CREATED_DATETIME datetime
                      
)                      
AS                      
BEGIN              
                       
	select @PAYER_ID=isnull(Max(PAYER_ID),0)+1 from CLM_ACTIVITY_RECOVERY_PAYER WHERE                     
		 CLAIM_ID = @CLAIM_ID and ACTIVITY_ID=@ACTIVITY_ID

INSERT INTO CLM_ACTIVITY_RECOVERY_PAYER                      
(                      
CLAIM_ID,
ACTIVITY_ID,
PAYER_ID,
RECOVERY_TYPE,
RECEIVED_DATE,
RECEIVED_FROM,
CHECK_NUMBER,
DESCRIPTION,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME
)        
VALUES                      
(                      
@CLAIM_ID,
@ACTIVITY_ID,
@PAYER_ID,
@RECOVERY_TYPE,
@RECEIVED_DATE,
@RECEIVED_FROM,
@CHECK_NUMBER,
@DESCRIPTION,
'Y',
@CREATED_BY,
@CREATED_DATETIME
)                      
END                      
                      
                    
                  
                
              
            
          
        
      
    
  



GO

