IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SetUmbrellaAppVehicleClassRule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SetUmbrellaAppVehicleClassRule]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------          
PROC NAME       : DBO.PROC_SetUmbrellaAppVehicleClassRule        
CREATED BY      : SWASTIKA GAUR        
DATE            : 24th Apr'06          
PURPOSE         :SET VEHICLE CLASS BASED ON THE DRIVER INFORMATION        
REVISON HISTORY :          
USED IN         : WOLVERINE          
------------------------------------------------------------          
DATE     REVIEW BY          COMMENTS          
------   ------------       -------------------------*/          
--drop proc PROC_SetUmbrellaAppVehicleClassRule    
CREATE PROC DBO.PROC_SetUmbrellaAppVehicleClassRule    
(          
@CUSTOMER_ID     INT,          
@APP_ID     INT,          
@APP_VERSION_ID     SMALLINT,          
@DRIVER_ID     SMALLINT,          
@BUSINESS_TYPE VARCHAR(20)=null        
)          
AS          
DECLARE @VEHICLE_USE VARCHAR(15)        
DECLARE @VEHICLE_ID INT        
DECLARE @DRIVER_DOB DATETIME        
DECLARE @DRIVER_GENDER VARCHAR(5)        
DECLARE @APPLICATION_STATE INT        
DECLARE @DRIVER_PRINCIPAL INT        
DECLARE @DRIVER_MVR_POINTS INT        
DECLARE @DRIVER_AGE INT        
DECLARE @VEHICLE_CLASS VARCHAR(10)        
        
BEGIN          
  
       
 SELECT @VEHICLE_ID=VEHICLE_ID, @DRIVER_DOB=DRIVER_DOB, @DRIVER_GENDER=DRIVER_SEX,        
    @DRIVER_PRINCIPAL=APP_VEHICLE_PRIN_OCC_ID FROM APP_UMBRELLA_DRIVER_DETAILS WHERE         
    CUSTOMER_ID=@CUSTOMER_ID AND        
    APP_ID=@APP_ID AND        
    APP_VERSION_ID=@APP_VERSION_ID AND        
    DRIVER_ID=@DRIVER_ID        
 SELECT @VEHICLE_USE=USE_VEHICLE FROM APP_UMBRELLA_VEHICLE_INFO WHERE         
    CUSTOMER_ID=@CUSTOMER_ID AND        
    APP_ID=@APP_ID AND        
    APP_VERSION_ID=@APP_VERSION_ID AND        
    VEHICLE_ID=@VEHICLE_ID        
        
 SELECT @APPLICATION_STATE=STATE_ID FROM APP_LIST WHERE        
    CUSTOMER_ID=@CUSTOMER_ID AND        
    APP_ID=@APP_ID AND        
    APP_VERSION_ID=@APP_VERSION_ID         
   
 IF (@VEHICLE_USE='11332') --VEHICLE IS FOR PERSONAL USE ONLY        
 BEGIN        
  SET @DRIVER_AGE = DATEDIFF(YY,@DRIVER_DOB,GETDATE())                    
  
  SELECT @DRIVER_MVR_POINTS=SUM(M.MVR_POINTS) FROM APP_UMBRELLA_MVR_INFORMATION A JOIN MNT_VIOLATIONS M        
   ON M.VIOLATION_ID=A.VIOLATION_ID WHERE        
   A.CUSTOMER_ID=@CUSTOMER_ID AND        
   A.APP_ID=@APP_ID AND        
   A.APP_VERSION_ID=@APP_VERSION_ID AND        
   A.DRIVER_ID=@DRIVER_ID   


  exec Proc_SetGeneralClassRule @DRIVER_AGE,@DRIVER_MVR_POINTS,@DRIVER_DOB,@DRIVER_GENDER,@DRIVER_PRINCIPAL,@APPLICATION_STATE,@VEHICLE_CLASS OUT
--/////////////////////////////////////////////////////////////////////////////////////////  
 /* 
  --SETTING CLASS P        
  IF(@DRIVER_AGE>24)--OWNER & OCCASIONAL ARE SIMILAR...GROUP THEM TOGETHER  
  BEGIN  --CHECK FOR MVR POINTS NOW      
   IF(@DRIVER_MVR_POINTS<=4) --CHECK FOR MVR POINTS     
   BEGIN    
    IF(@DRIVER_AGE>=25 AND @DRIVER_AGE<=29)         
      SET @VEHICLE_CLASS='PA'             
    ELSE IF(@DRIVER_AGE>=30 AND @DRIVER_AGE<=34)        
         SET @VEHICLE_CLASS='PB'        
    ELSE IF(@DRIVER_AGE>=35 AND @DRIVER_AGE<=44)        
         SET @VEHICLE_CLASS='PC'        
    ELSE IF(@DRIVER_AGE>=45 AND @DRIVER_AGE<=49)        
         SET @VEHICLE_CLASS='PD'        
    ELSE IF(@DRIVER_AGE>=50 AND @DRIVER_AGE<=69)        
      SET @VEHICLE_CLASS='PE'        
    ELSE IF(@DRIVER_AGE>=70)        
         SET @VEHICLE_CLASS='PF'         
     END   --CHECK FOR MVR POINTS <= 4 ENDS HERE  
   ELSE  
   BEGIN        
    IF(@DRIVER_AGE>=25 AND @DRIVER_AGE<=29)        
     SET @VEHICLE_CLASS='1A'        
    ELSE IF(@DRIVER_AGE>=30 AND @DRIVER_AGE<=34)        
     SET @VEHICLE_CLASS='1B'        
    ELSE IF(@DRIVER_AGE>=35 AND @DRIVER_AGE<=44)        
     SET @VEHICLE_CLASS='1C'        
    ELSE IF(@DRIVER_AGE>=45 AND @DRIVER_AGE<=49)        
     SET @VEHICLE_CLASS='1D'        
    ELSE IF(@DRIVER_AGE>=50 AND @DRIVER_AGE<=69)        
     SET @VEHICLE_CLASS='1E'                   
    ELSE IF(@DRIVER_AGE>=70)  
     SET @VEHICLE_CLASS='1F'                       
   END   
  END   
  ELSE  --CHECK FOR AGE<24  
  BEGIN  
   IF(@DRIVER_PRINCIPAL='11398') --OWNER OR PRINCIPAL OPERATOR        
   BEGIN  
    IF(UPPER(@DRIVER_GENDER)='F')  
    BEGIN  
     IF(@DRIVER_AGE>=21 AND @DRIVER_AGE<=24)        
      SET @VEHICLE_CLASS='4A'        
     ELSE IF(@DRIVER_AGE>=18 AND @DRIVER_AGE<=20)        
      SET @VEHICLE_CLASS='4B'        
     ELSE IF(@DRIVER_AGE>=16 AND @DRIVER_AGE<=17)        
      SET @VEHICLE_CLASS='4C'        
    END  
    ELSE IF(UPPER(@DRIVER_GENDER)='M')  
    BEGIN  
      IF(@DRIVER_AGE>=21 AND @DRIVER_AGE<=24)        
       SET @VEHICLE_CLASS='3A'        
      ELSE IF(@DRIVER_AGE>=18 AND @DRIVER_AGE<=20)        
       SET @VEHICLE_CLASS='3B'        
      ELSE IF(@DRIVER_AGE>=16 AND @DRIVER_AGE<=17)        
       SET @VEHICLE_CLASS='3C'        
    END     
   END  
   ELSE --OCCASIONAL DRIVER      
   BEGIN  
    IF(@APPLICATION_STATE=14 and (UPPER(@DRIVER_GENDER)='F')) --Indiana State        
    BEGIN       
     IF(@DRIVER_AGE>=21 AND @DRIVER_AGE<=24)        
      SET @VEHICLE_CLASS='6A'        
     ELSE IF(@DRIVER_AGE>=18 AND @DRIVER_AGE<=20)        
      SET @VEHICLE_CLASS='6B'        
     ELSE IF(@DRIVER_AGE>=16 AND @DRIVER_AGE<=17)        
      SET @VEHICLE_CLASS='6C'               
    END  
    ELSE  
    BEGIN  
     IF(@DRIVER_AGE>=21 AND @DRIVER_AGE<=24)        
      SET @VEHICLE_CLASS='2A'        
     ELSE IF(@DRIVER_AGE>=18 AND @DRIVER_AGE<=20)        
      SET @VEHICLE_CLASS='2B'        
     ELSE IF(@DRIVER_AGE>=16 AND @DRIVER_AGE<=17)        
      SET @VEHICLE_CLASS='2C'        
    END  
  END   
END  
 SELECT @VEHICLE_CLASS=V.LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES V JOIN MNT_LOOKUP_TABLES T        
  ON V.LOOKUP_ID=T.LOOKUP_ID        
  WHERE T.LOOKUP_NAME='VHCLSP' AND V.LOOKUP_VALUE_DESC=@VEHICLE_CLASS        
  */
  --//////////////////////////////////////////////////////////////////

 IF(not((@VEHICLE_CLASS='') OR (@VEHICLE_CLASS IS NULL)))      
  UPDATE APP_UMBRELLA_VEHICLE_INFO SET CLASS_PER=@VEHICLE_CLASS WHERE        
  CUSTOMER_ID=@CUSTOMER_ID AND        
  APP_ID=@APP_ID AND        
  APP_VERSION_ID=@APP_VERSION_ID AND        
  VEHICLE_ID=@VEHICLE_ID           
  
END        
END  
  







GO

