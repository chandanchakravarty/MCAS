IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                    
Proc Name       : Dbo.Proc_UpdateAPP_MVR_INFORMATION                                    
Created by      : Sumit Chhabra                                    
Date            : 9/27/2005                                    
Purpose         :Update of Driver MVR Information                                    
Revison History :                                    
Used In                   : Wolverine                                    
------------------------------------------------------------                                    
Date     Review By          Comments             
drop PROC dbo.Proc_UpdateAPP_MVR_INFORMATION                            
------   ------------       -------------------------*/                                    
CREATE PROC dbo.Proc_UpdateAPP_MVR_INFORMATION                                    
(                                    
 @APP_MVR_ID  int,                                    
 @CUSTOMER_ID    int,                                    
 @APP_ID      int,                                    
 @APP_VERSION_ID int,                                    
 @VIOLATION_ID      int,                                    
 @DRIVER_ID      int,                                    
 @MVR_AMOUNT     decimal(20,0),                          
 @MVR_DEATH      nvarchar(2),                                    
 @MVR_DATE      datetime,                    
 @CALLED_FROM varchar(10)=null,                
 @VERIFIED smallint,            
 @VIOLATION_TYPE int,  
 @OCCURENCE_DATE DateTime,  
 @DETAILS varchar(500),
 @POINTS_ASSIGNED int,
 @ADJUST_VIOLATION_POINTS int        
                   
)                                    
AS                                    
BEGIN                                    
                                    
 UPDATE  APP_MVR_INFORMATION                                    
 SET                                    
  VIOLATION_ID    =  @VIOLATION_ID,                                    
  MVR_AMOUNT   =  @MVR_AMOUNT,                                    
  MVR_DEATH    =  @MVR_DEATH,                                    
  MVR_DATE    =  @MVR_DATE,                
  VERIFIED    =  @VERIFIED,            
 VIOLATION_TYPE = @VIOLATION_TYPE,  
  OCCURENCE_DATE = @OCCURENCE_DATE,  
  DETAILS = @DETAILS,
  POINTS_ASSIGNED = @POINTS_ASSIGNED,
  ADJUST_VIOLATION_POINTS = @ADJUST_VIOLATION_POINTS        
                              
 WHERE                                  
  CUSTOMER_ID   =  @CUSTOMER_ID and                                    
  APP_ID    =  @APP_ID and                                    
  APP_VERSION_ID  =  @APP_VERSION_ID and                        
  APP_MVR_ID = @APP_MVR_ID                          
 AND DRIVER_ID=@DRIVER_ID                             
                    
                    
--IF (UPPER(@CALLED_FROM)='PPA')                    
-- EXEC PROC_SETVEHICLECLASSRULE @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID                  
              
 IF (UPPER(@CALLED_FROM)='MOT')                    
  EXEC Proc_SetPreferredRiskDiscount @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID                  
              
END                              
                        
                      
                    
                    
                  
                
              
            
          
        
      
    
  



GO

