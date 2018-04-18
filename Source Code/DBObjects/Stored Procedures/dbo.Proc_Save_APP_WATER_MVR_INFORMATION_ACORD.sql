IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_APP_WATER_MVR_INFORMATION_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_APP_WATER_MVR_INFORMATION_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                    
                    
Proc Name       : Proc_Save_APP_WATER_MVR_INFORMATION_ACORD                    
Created by      : Pradeep Iyer         
Date            : 10/27/2005                    
Purpose         :Insert of Driver MVR Information                    
Revison History :                    
Used In                   : Wolverine                    
------------------------------------------------------------  
Modified : Praveen k : 15 feb 2006 : Set Watercrat LOBID in case it is attached to Home Pol.                    
Date     Review By          Comments                    
------   ------------       -------------------------*/          
--drop proc Dbo.Proc_Save_APP_WATER_MVR_INFORMATION_ACORD     
  
Create PROC Dbo.Proc_Save_APP_WATER_MVR_INFORMATION_ACORD                    
(                    
 @APP_MVR_ID  int OUTPUT,                    
 @CUSTOMER_ID  int,                    
 @APP_ID  int,                    
 @APP_VERSION_ID int,                    
 @VIOLATION_ID  int,                    
 @VIOLATION_CODE  varchar(50),                    
 @DRIVER_ID  int,                    
 @MVR_AMOUNT  decimal(20,2),          
 @MVR_DEATH  nvarchar(2),                    
 @MVR_DATE  datetime,                
 @IS_ACTIVE  nvarchar(2),
 @POINTS_ASSIGNED int =null               
)                    
AS                    
BEGIN        
        
        
/*IF EXISTS        
(        
 SELECT * FROM APP_WATER_MVR_INFORMATION MI        
 INNER JOIN MNT_VIOLATIONS V ON        
  MI.VIOLATION_ID = V.VIOLATION_ID         
 WHERE V.VIOLATION_CODE = @VIOLATION_CODE AND        
  MI.DRIVER_ID = @DRIVER_ID  AND      
 MI.CUSTOMER_ID = @CUSTOMER_ID AND      
 MI.APP_ID = @APP_ID AND      
 MI.APP_VERSION_ID = @APP_VERSION_ID      
)        
BEGIN        
         
 UPDATE APP_WATER_MVR_INFORMATION        
 SET  MVR_AMOUNT = @MVR_AMOUNT,         
  MVR_DEATH = @MVR_DEATH,        
   MVR_DATE = @MVR_DATE        
 WHERE CUSTOMER_ID  = @CUSTOMER_ID AND        
  APP_ID = @APP_ID AND        
  APP_VERSION_ID  = @APP_VERSION_ID AND        
  DRIVER_ID = @DRIVER_ID        
        
END        
ELSE        
BEGIN      */  
      
 --DECLARE @LOBID smallint  
 --SET @LOBID = 4  
  
 --EXEC @VIOLATION_ID = Proc_GetVIOLATIONID_FROM_CODE  @VIOLATION_CODE,@CUSTOMER_ID, @APP_ID ,@APP_VERSION_ID,@LOBID   
DECLARE @VIOLATION_IDS varchar(100)
DECLARE @VIOLATION_TYPE_ID varchar(20) 
DECLARE @VIOLATION_TYPE varchar(20) 

/* Fetch the violation id of the selected violation  and the vioaltionid of the violation type(category)*/

if(@VIOLATION_ID = 0)
begin
	EXEC Proc_GetVIOLATIONID_FROM_CODE_ACCORD  @VIOLATION_CODE,@CUSTOMER_ID, @APP_ID ,@APP_VERSION_ID ,@VIOLATION_IDS OUTPUT, 4	--For watercraft only
	SET @VIOLATION_TYPE_ID = dbo.piece(@VIOLATION_IDS,':',1)
	SET @VIOLATION_TYPE = dbo.piece(@VIOLATION_IDS,':',2)
end
else
begin
	SET @VIOLATION_TYPE_ID = '0'
	SET @VIOLATION_TYPE = @VIOLATION_ID
end

      
  /*Generating the new mvr id*/                    
  SELECT @APP_MVR_ID=ISNULL(MAX(APP_WATER_MVR_ID),0)+1 FROM APP_WATER_MVR_INFORMATION                    
        


 IF ( @VIOLATION_ID IS NULL )        
 BEGIN        
  RETURN        
 END         
        
IF(@VIOLATION_ID <> 15052 and @VIOLATION_ID <> 15053 and @VIOLATION_ID <> 15054)
BEGIN
	  INSERT INTO APP_WATER_MVR_INFORMATION          
	  (                    
		  APP_WATER_MVR_ID,         
		  CUSTOMER_ID,        
		  APP_ID,         
		  APP_VERSION_ID,         
		  VIOLATION_ID,        
		  DRIVER_ID,                    
		  MVR_AMOUNT,         
		  MVR_DEATH,        
		  MVR_DATE,         
		  IS_ACTIVE ,
		  VIOLATION_TYPE,
		  OCCURENCE_DATE,      
		  POINTS_ASSIGNED       
	   )                    
	   VALUES                    
		(        
		  @APP_MVR_ID,        
		  @CUSTOMER_ID,        
		  @APP_ID,        
		  @APP_VERSION_ID,        
		  @VIOLATION_TYPE_ID,        
		  @DRIVER_ID,     
		  @MVR_AMOUNT,        
		  @MVR_DEATH,        
		  @MVR_DATE,        
		  @IS_ACTIVE,
		  @VIOLATION_TYPE,
		  @MVR_DATE, --same as conviction date
 		  @POINTS_ASSIGNED        
	 )       
 END 
END           
          
        











GO

