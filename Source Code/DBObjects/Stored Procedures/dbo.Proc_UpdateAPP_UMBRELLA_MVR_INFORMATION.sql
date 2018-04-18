IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_UMBRELLA_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_UMBRELLA_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : Dbo.Proc_UpdateAPP_UMBRELLA_MVR_INFORMATION                        
Created by      : Sumit Chhabra                        
Date            : 22/03/2006                        
Purpose         :Update of Umbrella Driver/Operator MVR Information                        
Revison History :                        
Used In                   : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
CREATE PROC Dbo.Proc_UpdateAPP_UMBRELLA_MVR_INFORMATION                        
(                        
 @APP_UMB_MVR_ID  int,                        
 @CUSTOMER_ID    int,                        
 @APP_ID      int,                        
 @APP_VERSION_ID int,                        
 @VIOLATION_ID      int,                        
 @DRIVER_ID      int,                        
 @MVR_AMOUNT     decimal(20,0),              
 @MVR_DEATH      nvarchar(2),                        
 @MVR_DATE      datetime,      
 @VERIFIED smallint,    
 @VIOLATION_TYPE int,
 @MODIFIED_BY int,
 @LAST_UPDATED_DATETIME datetime
                  
)                        
AS                        
BEGIN                        
                        
 UPDATE  APP_UMBRELLA_MVR_INFORMATION                        
 SET                                      
  VIOLATION_ID    =  @VIOLATION_ID,                        
  MVR_AMOUNT   =  @MVR_AMOUNT,                        
  MVR_DEATH    =  @MVR_DEATH,                        
  MVR_DATE    =  @MVR_DATE,      
  VERIFIED     = @VERIFIED,    
  VIOLATION_TYPE = @VIOLATION_TYPE,
  MODIFIED_BY = @MODIFIED_BY,
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
                  
 WHERE                            
  CUSTOMER_ID    = @CUSTOMER_ID and          
  APP_ID    = @APP_ID and          
  APP_VERSION_ID   = @APP_VERSION_ID and          
  APP_UMB_MVR_ID = @APP_UMB_MVR_ID  and         
  DRIVER_ID=@DRIVER_ID        
                                 
END                  
            
          



GO

