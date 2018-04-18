IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_WATERCRAFT_MVR_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_WATERCRAFT_MVR_INFORMATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                          
Proc Name       : Proc_InsertPOL_WATERCRAFT_MVR_INFORMATION      
Created by      : Vijay Arora      
Date            : 25-11-2005      
Purpose         : Insert of WaterCraft Operator MVR Information              
Revison History :                          
Used In         : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments    
drop PROC Dbo.Proc_InsertPOL_WATERCRAFT_MVR_INFORMATION                       
------   ------------       -------------------------*/                          
CREATE PROC Dbo.Proc_InsertPOL_WATERCRAFT_MVR_INFORMATION                          
(                          
 @CUSTOMER_ID  int,                          
 @POLICY_ID  int,                          
 @POLICY_VERSION_ID int,                          
 @DRIVER_ID  int,                          
 @APP_WATER_MVR_ID  int OUTPUT,                          
 @VIOLATION_ID  int,                          
 @MVR_AMOUNT  decimal(20,0),                
 @MVR_DEATH  nvarchar(2),                          
 @MVR_DATE  datetime,                      
 @IS_ACTIVE  nvarchar(2) ,            
 @VIOLATION_SOURCE varchar(10) = null,    
 @VERIFIED smallint,  
 @VIOLATION_TYPE int,
 @OCCURENCE_DATE DateTime,    
 @DETAILS varchar(500),                
 @POINTS_ASSIGNED int,  
 @ADJUST_VIOLATION_POINTS int                               
                             
)                          
AS                          
BEGIN                          
      
  SELECT @APP_WATER_MVR_ID=ISNULL(MAX(APP_WATER_MVR_ID),0)+1 FROM POL_WATERCRAFT_MVR_INFORMATION          
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID  


--ADDED BY Praveen        
DECLARE @TEMP_VIOLATION_TYPE INT
DECLARE @TEMP_STATE_ID INT
DECLARE @TEMP_LOB_ID INT


        
SELECT @TEMP_STATE_ID=STATE_ID,@TEMP_LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
set  @TEMP_VIOLATION_TYPE=@VIOLATION_TYPE        
if  (@VIOLATION_TYPE = 0 or @VIOLATION_TYPE is null)                              
     begin        
 if (@VIOLATION_ID>25000)        
 set @TEMP_VIOLATION_TYPE=13220;        
 else        
 SELECT  @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS WHERE VIOLATION_GROUP=(SELECT VIOLATION_PARENT FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_ID) AND  STATE=@TEMP_STATE_ID AND LOB=@TEMP_LOB_ID        
     end             
-----END HERE      

IF NOT EXISTS(SELECT APP_WATER_MVR_ID FROM  POL_WATERCRAFT_MVR_INFORMATION WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID AND VIOLATION_ID =@VIOLATION_ID AND VIOLATION_TYPE=@TEMP_VIOLATION_TYPE
and OCCURENCE_DATE = @OCCURENCE_DATE) 
begin                          
                 
  INSERT INTO POL_WATERCRAFT_MVR_INFORMATION      
     (CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, DRIVER_ID,APP_WATER_MVR_ID,VIOLATION_ID,                          
     MVR_AMOUNT, MVR_DEATH, MVR_DATE, IS_ACTIVE, VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE, OCCURENCE_DATE, DETAILS,  
     POINTS_ASSIGNED, ADJUST_VIOLATION_POINTS)                          
  VALUES                          
     (@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@DRIVER_ID,@APP_WATER_MVR_ID,@VIOLATION_ID,                  
     @MVR_AMOUNT,@MVR_DEATH,@MVR_DATE,@IS_ACTIVE, @VIOLATION_SOURCE,@VERIFIED, @VIOLATION_TYPE, @OCCURENCE_DATE, @DETAILS,  
     @POINTS_ASSIGNED, @ADJUST_VIOLATION_POINTS   )     

end           
END    
    
  






GO

