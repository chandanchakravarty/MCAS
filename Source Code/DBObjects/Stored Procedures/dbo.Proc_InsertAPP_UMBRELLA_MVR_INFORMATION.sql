IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_UMBRELLA_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_UMBRELLA_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
                          
Proc Name       : Proc_InsertAPP_UMBRELLA_MVR_INFORMATION                          
Created by      : Sumit Chhabra                          
Date            : 22/03/2006                          
Purpose         :Insert of Umbrella Driver/Operator MVR Information              
Revison History :                          
Used In                   : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
CREATE PROC Dbo.Proc_InsertAPP_UMBRELLA_MVR_INFORMATION                          
(                          
 @APP_UMB_MVR_ID  int OUTPUT,                          
 @CUSTOMER_ID  int,                          
 @APP_ID  int,                          
 @APP_VERSION_ID int,                          
 @VIOLATION_ID  int,                          
 @DRIVER_ID  int,                          
 @MVR_AMOUNT  decimal(20,0),                
 @MVR_DEATH  nvarchar(2),                          
 @MVR_DATE  datetime,                      
 @IS_ACTIVE  nvarchar(2),      
 @VERIFIED smallint,    
 @VIOLATION_TYPE int,
 @CREATED_BY int,
 @CREATED_DATETIME datetime
                      

)                          
AS                          
BEGIN                          
 /*Generating the new mvr id*/                          
 SELECT @APP_UMB_MVR_ID=ISNULL(MAX(APP_UMB_MVR_ID),0)+1 FROM APP_UMBRELLA_MVR_INFORMATION          
WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND DRIVER_ID=@DRIVER_ID                        
                
  
                  
   INSERT INTO APP_UMBRELLA_MVR_INFORMATION   (                          
    APP_UMB_MVR_ID, CUSTOMER_ID, APP_ID, APP_VERSION_ID, VIOLATION_ID,DRIVER_ID,                          
    MVR_AMOUNT, MVR_DEATH, MVR_DATE, IS_ACTIVE,VERIFIED,VIOLATION_TYPE,CREATED_BY,CREATED_DATETIME )                          
   VALUES                          
    (@APP_UMB_MVR_ID,@CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@VIOLATION_ID,@DRIVER_ID,                  
    @MVR_AMOUNT,@MVR_DEATH,@MVR_DATE,@IS_ACTIVE,@VERIFIED,@VIOLATION_TYPE,@CREATED_BY,@CREATED_DATETIME)                
                 
                   
  END                  
  
            
          
        
      
    
  



GO

