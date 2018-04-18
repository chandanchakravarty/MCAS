IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_WATER_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_WATER_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
                
Proc Name       : Proc_InsertPOL_WATER_MVR_INFORMATION                
Created by      : Anurag verma                
Date            : 08/11/2005                
Purpose         :Insert of WaterCraft Driver Policy MVR Information    
Revison History :                
Used In                   : Wolverine                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/  
--DROP PROC Dbo.Proc_InsertPOL_WATER_MVR_INFORMATION               
CREATE PROC Dbo.Proc_InsertPOL_WATER_MVR_INFORMATION                
(                
 @POL_WATER_MVR_ID  int OUTPUT,                
 @CUSTOMER_ID  int,                
 @POL_ID  int,                
 @POL_VERSION_ID int,                
 @VIOLATION_ID  int,                
 @DRIVER_ID  int,                
 @MVR_AMOUNT  decimal(20,0),      
 @MVR_DEATH  nvarchar(2),                
 @MVR_DATE  datetime,            
 @IS_ACTIVE  nvarchar(2),
 @CREATED_BY int,            
 @VIOLATION_TYPE int =null ,
 @RET_VIOLATION_TYPE int  =null  output,
 @RET_VERIFIED smallint = null output,            
 @VERIFIED  smallint =NULL,
 @OCCURENCE_DATE DateTime,    
 @DETAILS varchar(500),  
 @POINTS_ASSIGNED int,  
 @ADJUST_VIOLATION_POINTS int
)                              
AS                              
BEGIN                              
 /*GENERATING THE NEW MVR ID*/                                        
 SELECT @POL_WATER_MVR_ID=ISNULL(MAX(APP_WATER_MVR_ID),0)+1 FROM POL_WATERCRAFT_MVR_INFORMATION                        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND DRIVER_ID=@DRIVER_ID                        
--ADDED BY PRAVESH    
 set @RET_VERIFIED=0 
DECLARE @TEMP_VIOLATION_TYPE INT    
DECLARE @TEMP_STATE_ID INT    
DECLARE @TEMP_LOB_ID INT    
    
SELECT @TEMP_STATE_ID=STATE_ID,@TEMP_LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID    
set  @TEMP_VIOLATION_TYPE=@VIOLATION_TYPE    
if  (@VIOLATION_TYPE = 0 or @VIOLATION_TYPE is null)                          
     begin    
	 if (@VIOLATION_ID>25000)    
		 set @TEMP_VIOLATION_TYPE=13220;    
	 else    
		 SELECT  @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS WHERE VIOLATION_GROUP=(SELECT VIOLATION_PARENT FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_ID) AND  STATE=@TEMP_STATE_ID AND LOB=@TEMP_LOB_ID    
      end         
-----END HERE      
 set @RET_VIOLATION_TYPE= @TEMP_VIOLATION_TYPE     
IF NOT EXISTS(SELECT APP_WATER_MVR_ID FROM  POL_WATERCRAFT_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID =@POL_ID AND POLICY_VERSION_ID =@POL_VERSION_ID AND DRIVER_ID=@DRIVER_ID AND VIOLATION_ID =@VIOLATION_ID 
AND VIOLATION_TYPE=@TEMP_VIOLATION_TYPE and OCCURENCE_DATE = @OCCURENCE_DATE) 
 BEGIN    
    INSERT INTO POL_WATERCRAFT_MVR_INFORMATION   (                                        
     APP_WATER_MVR_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, VIOLATION_ID,DRIVER_ID,                                        
     MVR_AMOUNT, MVR_DEATH, MVR_DATE, IS_ACTIVE, VIOLATION_TYPE,CREATED_BY,OCCURENCE_DATE, DETAILS,  
	POINTS_ASSIGNED, ADJUST_VIOLATION_POINTS,VERIFIED )                                        
    VALUES                                        
	 (@POL_WATER_MVR_ID,@CUSTOMER_ID,@POL_ID,@POL_VERSION_ID,@VIOLATION_ID,@DRIVER_ID,                                
	   @MVR_AMOUNT,@MVR_DEATH,@MVR_DATE,@IS_ACTIVE,    
	 -- @VIOLATION_TYPE    
	  @TEMP_VIOLATION_TYPE ,@CREATED_BY  ,@OCCURENCE_DATE, @DETAILS, @POINTS_ASSIGNED, @ADJUST_VIOLATION_POINTS,@VERIFIED
 	 )                              
              
  --update "MVR ordered" and "Date MVR ordered" fields on Driver tab     
  Update POL_WATERCRAFT_DRIVER_DETAILS set DATE_ORDERED = getdate(),MVR_ORDERED=10963   --yes    
  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POL_ID and POLICY_VERSION_ID=@POL_VERSION_ID and DRIVER_ID=@DRIVER_ID    
   SET @RET_VERIFIED = 1; 
 END               
END               









GO

