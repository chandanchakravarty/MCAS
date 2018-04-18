IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATEVEHICLECLASS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATEVEHICLECLASS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                  
PROC NAME       : DBO.PROC_UPDATEVEHICLECLASS                                
CREATED BY      : PRAVEEN KASANA                      
DATE            : 15 MAY 2006                                  
PURPOSE         : SET VEHICLE CLASS BASED ON THE DRIVER INFORMATION                                
REVISON HISTORY :                                  
USED IN         : WOLVERINE                                  
------------------------------------------------------------                                  
DATE     REVIEW BY          COMMENTS                                  
------   ------------       -------------------------*/                                  
--DROP PROC dbo.PROC_UPDATEVEHICLECLASS  1199,2,1,1,'PA',156                      
CREATE PROC [dbo].[PROC_UPDATEVEHICLECLASS]              
(                                  
@CUSTOMER_ID     INT,                                  
@APP_ID     INT,                                  
@APP_VERSION_ID     SMALLINT,                                  
@VEHICLE_ID     SMALLINT,                      
@VEHICLE_CLASS VARCHAR(20),        
@CLASS_DRIVERID int = null        
                      
)                                  
AS                          
DECLARE @INT_LOOKUP_UNIQUE_ID int,        
  @APP_LOB     int      
DECLARE @STATE_ID int      
DECLARE @APP_EFFECTIVE_DATE NVARCHAR(40)    
  
set @INT_LOOKUP_UNIQUE_ID=0       
BEGIN                              
                
SELECT @APP_LOB=APP_LOB ,      
  @STATE_ID= STATE_ID,      
        @APP_EFFECTIVE_DATE=CONVERT(NVARCHAR(50),APP_EFFECTIVE_DATE)       
 FROM APP_LIST with (nolock) WHERE CUSTOMER_ID =@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID         
IF ( @APP_LOB =2)        
BEGIN        
              
	IF(@STATE_ID = 22)      
	BEGIN                   
	SELECT @INT_LOOKUP_UNIQUE_ID = LOOKUP_UNIQUE_ID                  
	FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_VALUE_CODE = @VEHICLE_CLASS and LOOKUP_ID = '1211'  AND   Type=22   AND IS_ACTIVE='Y'      
	END   
   
	ELSE IF(@STATE_ID=14 AND (DATEDIFF(DAY,'2008-05-31 00:00:00.000',@APP_EFFECTIVE_DATE) <=0))      
	BEGIN      
	SELECT @INT_LOOKUP_UNIQUE_ID = LOOKUP_UNIQUE_ID                  
	FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_VALUE_CODE = @VEHICLE_CLASS and LOOKUP_ID = '1211'   AND   Type=14 AND LOOKUP_FRAME_OR_MASONRY IS NOT NULL    AND IS_ACTIVE='Y'      
	END       

	ELSE IF(@STATE_ID=14 AND (DATEDIFF(DAY,'2008-05-31 00:00:00.000',@APP_EFFECTIVE_DATE) >= 0))      
	BEGIN      
	SELECT @INT_LOOKUP_UNIQUE_ID = LOOKUP_UNIQUE_ID                  
	FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_VALUE_CODE = @VEHICLE_CLASS and LOOKUP_ID = '1211'  AND   Type=14 AND LOOKUP_FRAME_OR_MASONRY IS NULL    AND IS_ACTIVE='Y'      
	END                         
END         
ELSE IF (@APP_LOB = 3)        
BEGIN         
    SELECT @INT_LOOKUP_UNIQUE_ID = LOOKUP_UNIQUE_ID                      
    FROM MNT_LOOKUP_VALUES  with (nolock) WHERE LOOKUP_VALUE_CODE = @VEHICLE_CLASS and LOOKUP_ID = '1217'      AND IS_ACTIVE='Y'          
END                     
--Update the vehicle class and put the driver used for calculating the class        
--IF(not((@INT_LOOKUP_UNIQUE_ID='') OR (@INT_LOOKUP_UNIQUE_ID IS NULL)))   
  
--IF(@INT_LOOKUP_UNIQUE_ID <> 0)      
--BEGIN
  
  --Added by Manoj Rathore on 12 May 2009 Itrack # 5120   
  IF(@APP_LOB = 3)
  BEGIN
	UPDATE APP_VEHICLES SET CLASS_PER=@INT_LOOKUP_UNIQUE_ID,
				APP_VEHICLE_CLASS=@INT_LOOKUP_UNIQUE_ID,
				CLASS_DRIVERID=@CLASS_DRIVERID 
	WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                
	APP_ID=@APP_ID AND                                
	APP_VERSION_ID=@APP_VERSION_ID AND                                
	VEHICLE_ID=@VEHICLE_ID
  END
  ELSE
  BEGIN          
	UPDATE APP_VEHICLES SET CLASS_PER=@INT_LOOKUP_UNIQUE_ID,CLASS_DRIVERID=@CLASS_DRIVERID WHERE                                
	CUSTOMER_ID=@CUSTOMER_ID AND                                
	APP_ID=@APP_ID AND                                
	APP_VERSION_ID=@APP_VERSION_ID AND                                
	VEHICLE_ID=@VEHICLE_ID                                   
  END
--END      
    
-- Added 22-Sep-08 Mohit Agarwal from Update Insert Driver    
DECLARE @PARENTS_INSURANCES NVarchar(20)            
SET @PARENTS_INSURANCES='FALSE'             
--Modified by Mohit Agarwal ITrack 4737 16-Sep-08        
BEGIN            
IF EXISTS(SELECT CUSTOMER_ID FROM APP_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID AND PARENTS_INSURANCE = 11934)            
  BEGIN            
   SET @PARENTS_INSURANCES='TRUE'            
  END            
IF EXISTS(SELECT CUSTOMER_ID FROM APP_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID AND PARENTS_INSURANCE = 11935)            
  BEGIN         
   DECLARE @DATE_BEF25YRS DATETIME        
   DECLARE @DATE_APPEFF DATETIME        
   SET @DATE_APPEFF = (SELECT APP_EFFECTIVE_DATE FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID)        
   SET @DATE_BEF25YRS = DATEADD(YY, -25, @DATE_APPEFF)        
   IF EXISTS(SELECT * FROM APP_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID            
    AND DRIVER_ID IN (SELECT T1.DRIVER_ID FROM APP_DRIVER_ASSIGNED_VEHICLE T1 WHERE T1.CUSTOMER_ID=@CUSTOMER_ID AND T1.APP_ID=@APP_ID AND T1.APP_VERSION_ID=@APP_VERSION_ID AND   
 EXISTS(SELECT * FROM APP_DRIVER_ASSIGNED_VEHICLE T2 WHERE T2.CUSTOMER_ID=@CUSTOMER_ID AND T2.APP_ID=@APP_ID AND T2.APP_VERSION_ID=@APP_VERSION_ID AND T2.DRIVER_ID=T1.DRIVER_ID AND T2.APP_VEHICLE_PRIN_OCC_ID <> 11930))        
    AND DRIVER_DOB < @DATE_BEF25YRS)        
  SET @PARENTS_INSURANCES='FALSE'            
   ELSE         
  SET @PARENTS_INSURANCES='TRUE'            
  END            
   IF(@PARENTS_INSURANCES='TRUE')              
    BEGIN            
     UPDATE APP_AUTO_GEN_INFO            
     SET ANY_OTH_AUTO_INSU = '1'            
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID            
    END            
END                           
END                
     

GO

