IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAPP_WATER_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAPP_WATER_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                  
                                  
Proc Name       : Proc_InsertAPP_WATER_MVR_INFORMATION                                  
Created by      : Sumit Chhabra                                  
Date            : 14/10/2005                                  
Purpose         :Insert of WaterCraft Driver MVR Information                      
Revison History :                                  
Modified by      : Pravesh                                 
Date            : 26/10/2006                                  
Purpose         : save created vy and created date time fields value     

Modified by      : Praveen kasana                                 
Date            : 21 feb 2008                            
Purpose         : @OCCURENCE_DATE check
          
Used In                   : Wolverine                                  
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
--drop proc  dbo.Proc_InsertAPP_WATER_MVR_INFORMATION                                  
CREATE PROC dbo.Proc_InsertAPP_WATER_MVR_INFORMATION                                  
(                                  
 @APP_WATER_MVR_ID  int OUTPUT,                                  
 @CUSTOMER_ID  int,                                  
 @APP_ID  int,                                  
 @APP_VERSION_ID int,                                  
 @VIOLATION_ID  int,                                  
 @DRIVER_ID  int,                                  
 @MVR_AMOUNT  decimal(20,0),                        
 @MVR_DEATH  nvarchar(2),                                  
 @MVR_DATE  datetime,                              
 @IS_ACTIVE  nvarchar(2),              
 @CREATED_BY int=null  ,        
 @VIOLATION_TYPE int =null,      
 @VERIFIED int = null,  
 @OCCURENCE_DATE DateTime,  
 @DETAILS varchar(500),              
 @POINTS_ASSIGNED int,
 @ADJUST_VIOLATION_POINTS int                             
)                                  
AS                                  
BEGIN                                  
 /*GENERATING THE NEW MVR ID*/                                            
 SELECT @APP_WATER_MVR_ID=ISNULL(MAX(APP_WATER_MVR_ID),0)+1 FROM APP_WATER_MVR_INFORMATION                            
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID AND DRIVER_ID=@DRIVER_ID                            
--ADDED BY PRAVESH        
DECLARE @TEMP_VIOLATION_TYPE INT        
DECLARE @TEMP_STATE_ID INT        
DECLARE @TEMP_LOB_ID INT        
        
SELECT @TEMP_STATE_ID=STATE_ID,@TEMP_LOB_ID=APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
set  @TEMP_VIOLATION_TYPE=@VIOLATION_TYPE        
if  (@VIOLATION_TYPE = 0 or @VIOLATION_TYPE is null)                              
     begin        
 if (@VIOLATION_ID>25000)        
 set @TEMP_VIOLATION_TYPE=13220;        
 else        
 SELECT  @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS WHERE VIOLATION_GROUP=(SELECT VIOLATION_PARENT FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_ID) AND  STATE=@TEMP_STATE_ID AND LOB=@TEMP_LOB_ID        
     end             
-----END HERE          
        
IF NOT EXISTS(SELECT APP_WATER_MVR_ID FROM  APP_WATER_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID =@APP_ID AND APP_VERSION_ID =@APP_VERSION_ID AND DRIVER_ID=@DRIVER_ID AND VIOLATION_ID =@VIOLATION_ID AND VIOLATION_TYPE=@TEMP_VIOLATION_TYPE
and OCCURENCE_DATE = @OCCURENCE_DATE) 

           
 BEGIN        
    INSERT INTO APP_WATER_MVR_INFORMATION   (                                            
     APP_WATER_MVR_ID, CUSTOMER_ID, APP_ID, APP_VERSION_ID, VIOLATION_ID,DRIVER_ID,                                            
     MVR_AMOUNT, MVR_DEATH, MVR_DATE, IS_ACTIVE, VIOLATION_TYPE,CREATED_BY,VERIFIED, OCCURENCE_DATE, DETAILS,
	POINTS_ASSIGNED, ADJUST_VIOLATION_POINTS
 )                                            
    VALUES                                            
     (@APP_WATER_MVR_ID,@CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@VIOLATION_ID,@DRIVER_ID,                                    
     @MVR_AMOUNT,@MVR_DEATH,@MVR_DATE,@IS_ACTIVE,        
	-- @VIOLATION_TYPE        
	@TEMP_VIOLATION_TYPE ,@CREATED_BY,@VERIFIED, @OCCURENCE_DATE, @DETAILS,
	@POINTS_ASSIGNED, @ADJUST_VIOLATION_POINTS             
  )                                  
                  
  --update "MVR ordered" and "Date MVR ordered" fields on Driver tab         
  Update APP_WATERCRAFT_DRIVER_DETAILS set DATE_ORDERED = getdate(),MVR_ORDERED=10963,VIOLATIONS=10963 --YES       
		
  where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and aPP_VERSION_ID=@APP_VERSION_ID and DRIVER_ID=@DRIVER_ID        
        
 END                   
END                   
 
      
      

GO

