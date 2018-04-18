IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePOL_WATERCRAFT_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePOL_WATERCRAFT_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
Proc Name       : Dbo.Proc_UpdatePOL_WATERCRAFT_MVR_INFORMATION                        
Created by      : Vijay Arora      
Date            : 25-11-2005      
Purpose         : Update of WaterCraft Operator MVR Information                        
Revison History :                        
Used In         : Wolverine                        
------------------------------------------------------------                        
Date     Review By          Comments     
drop PROC Dbo.Proc_UpdatePOL_WATERCRAFT_MVR_INFORMATION                     
------   ------------       -------------------------*/                        
CREATE PROC Dbo.Proc_UpdatePOL_WATERCRAFT_MVR_INFORMATION            
(                        
 @CUSTOMER_ID    int,                        
 @POLICY_ID      int,                        
 @POLICY_VERSION_ID int,                        
 @DRIVER_ID      int,                        
 @APP_WATER_MVR_ID  int,                        
 @VIOLATION_ID      int,                        
 @MVR_AMOUNT     decimal(20,0),              
 @MVR_DEATH      nvarchar(2),                        
 @MVR_DATE      datetime,    
 @VERIFIED smallint,  
 @VIOLATION_TYPE int,
 @OCCURENCE_DATE DateTime,    
 @DETAILS varchar(500),      
 @POINTS_ASSIGNED int,  
 @ADJUST_VIOLATION_POINTS int          
                 
)                        
AS                        
BEGIN                        
 UPDATE POL_WATERCRAFT_MVR_INFORMATION                              
 SET                                      
  VIOLATION_ID    =  @VIOLATION_ID,                        
  MVR_AMOUNT     =  @MVR_AMOUNT,                        
  MVR_DEATH       =  @MVR_DEATH,                        
  MVR_DATE        =  @MVR_DATE,    
  VERIFIED        =  @VERIFIED,  
  VIOLATION_TYPE  =  @VIOLATION_TYPE,
  OCCURENCE_DATE = @OCCURENCE_DATE,    
  DETAILS = @DETAILS,    
  POINTS_ASSIGNED = @POINTS_ASSIGNED,  
  ADJUST_VIOLATION_POINTS = @ADJUST_VIOLATION_POINTS                      
      
 WHERE                            
  CUSTOMER_ID    = @CUSTOMER_ID and          
  POLICY_ID    = @POLICY_ID and          
  POLICY_VERSION_ID   = @POLICY_VERSION_ID and          
  DRIVER_ID=@DRIVER_ID  and      
  APP_WATER_MVR_ID = @APP_WATER_MVR_ID       
END                  
            
          



GO

