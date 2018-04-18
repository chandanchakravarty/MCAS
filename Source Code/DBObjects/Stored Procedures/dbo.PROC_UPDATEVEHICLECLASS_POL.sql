IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATEVEHICLECLASS_POL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATEVEHICLECLASS_POL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
PROC NAME       : DBO.PROC_UPDATEVEHICLECLASS_POL                              
CREATED BY      : PRAVEEN KASANA                    
DATE            : 18 MAY 2006                                
PURPOSE         : SET VEHICLE CLASS BASED ON THE DRIVER INFORMATION AT POLICY LEVEL                             
REVISON HISTORY :                                
USED IN         : WOLVERINE           
modified By :pravesh k chandel                           
date  :17 april 2007      
purpose  : to add code for Motor cycle class updation      
------------------------------------------------------------                                
DATE     REVIEW BY          COMMENTS                                
------   ------------       -------------------------*/                                
--      drop proc dbo.PROC_UPDATEVEHICLECLASS_POL 1713,2,1,2,'C',1                    
CREATE PROC [dbo].[PROC_UPDATEVEHICLECLASS_POL]                    
(                                
@CUSTOMER_ID     INT,                                
@POLICY_ID     INT,                                
@POLICY_VERSION_ID     SMALLINT,                                
@VEHICLE_ID     SMALLINT,                    
@VEHICLE_CLASS VARCHAR(20),      
@CLASS_DRIVERID int = null      
                    
)                                
AS                        
DECLARE @INT_LOOKUP_UNIQUE_ID int    
--set @INT_LOOKUP_UNIQUE_ID =0     
DECLARE @STATE_ID int      
DECLARE @APP_EFFECTIVE_DATE NVARCHAR(40)                    
BEGIN                            
declare @POL_LOB int      
 SELECT @POL_LOB=POLICY_LOB ,      
        @STATE_ID= STATE_ID,      
        @APP_EFFECTIVE_DATE=CONVERT(NVARCHAR(50),APP_EFFECTIVE_DATE)      
 FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID =@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID         
      
 IF ( @POL_LOB =2)
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
ELSE IF (@POL_LOB = 3)        
BEGIN         
    SELECT @INT_LOOKUP_UNIQUE_ID = LOOKUP_UNIQUE_ID                      
    FROM MNT_LOOKUP_VALUES with(nolock) WHERE LOOKUP_VALUE_CODE = @VEHICLE_CLASS and LOOKUP_ID = '1217'     AND IS_ACTIVE='Y'          
END           

                
--Update the vehicle class and put the driver used for calculating the class   
   
--IF(not((@INT_LOOKUP_UNIQUE_ID='') OR (@INT_LOOKUP_UNIQUE_ID IS NULL)) or @INT_LOOKUP_UNIQUE_ID=0)                            
  IF (@POL_LOB = 3)
  BEGIN
	  UPDATE POL_VEHICLES 
	  SET   APP_VEHICLE_PERCLASS_ID=@INT_LOOKUP_UNIQUE_ID,
		--Added by Manoj Rathore on 8th May 2009 Itrack # 5087
		APP_VEHICLE_CLASS=@INT_LOOKUP_UNIQUE_ID,
		CLASS_DRIVERID = @CLASS_DRIVERID
	  WHERE CUSTOMER_ID = @CUSTOMER_ID AND                              
	  POLICY_ID = @POLICY_ID AND                              
	  POLICY_VERSION_ID = @POLICY_VERSION_ID AND                              
	  VEHICLE_ID = @VEHICLE_ID                        
  END
  ELSE
  BEGIN 
	UPDATE POL_VEHICLES SET APP_VEHICLE_PERCLASS_ID=@INT_LOOKUP_UNIQUE_ID,CLASS_DRIVERID = @CLASS_DRIVERID
	WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                              
	POLICY_ID = @POLICY_ID AND                              
	POLICY_VERSION_ID = @POLICY_VERSION_ID AND                              
	VEHICLE_ID = @VEHICLE_ID 
  END 
      
-- Moved from Insert Update Pol Driver 22 Sep 08 Mohit Agarwal    
DECLARE @PARENTS_INSURANCES NVarchar(20)    
SET @PARENTS_INSURANCES='FALSE'              
 BEGIN        
IF EXISTS(SELECT CUSTOMER_ID FROM POL_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND PARENTS_INSURANCE = 11934)              
  BEGIN              
   SET @PARENTS_INSURANCES='TRUE'              
  END              
IF EXISTS(SELECT CUSTOMER_ID FROM POL_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID AND PARENTS_INSURANCE = 11935)              
  BEGIN           
   DECLARE @DATE_BEF25YRS DATETIME          
   DECLARE @DATE_APPEFF DATETIME          
   SET @DATE_APPEFF = (SELECT APP_EFFECTIVE_DATE FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID)          
   SET @DATE_BEF25YRS = DATEADD(YY, -25, @DATE_APPEFF)          
   IF EXISTS(SELECT * FROM POL_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID              
    AND DRIVER_ID IN (SELECT T1.DRIVER_ID FROM POL_DRIVER_ASSIGNED_VEHICLE T1 WHERE T1.CUSTOMER_ID=@CUSTOMER_ID AND T1.POLICY_ID=@POLICY_ID AND T1.POLICY_VERSION_ID=@POLICY_VERSION_ID AND   
 EXISTS(SELECT * FROM POL_DRIVER_ASSIGNED_VEHICLE T2 WHERE T2.CUSTOMER_ID=@CUSTOMER_ID AND T2.POLICY_ID=@POLICY_ID AND T2.POLICY_VERSION_ID=@POLICY_VERSION_ID AND T2.DRIVER_ID=T1.DRIVER_ID AND T2.APP_VEHICLE_PRIN_OCC_ID <> 11930))        
    AND DRIVER_DOB < @DATE_BEF25YRS)          
  SET @PARENTS_INSURANCES='FALSE'              
   ELSE           
  SET @PARENTS_INSURANCES='TRUE'              
  END              
   IF(@PARENTS_INSURANCES='TRUE')               
    BEGIN              
     UPDATE POL_AUTO_GEN_INFO              
     SET ANY_OTH_AUTO_INSU = '1'              
     WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID              
    END              
 END                                    
    
END                              


GO

