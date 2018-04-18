IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_SetVehicleClassRulePol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_SetVehicleClassRulePol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
PROC NAME       : DBO.PROC_SetVehicleClassRulePol      
CREATED BY      : SUMIT CHHABRA          
DATE            : 08/12/2005            
PURPOSE        :SET VEHICLE CLASS BASED ON THE DRIVER INFORMATION for policy      
REVISON HISTORY :            
USED IN        : WOLVERINE            
------------------------------------------------------------            
DATE     REVIEW BY          COMMENTS            
------   ------------       -------------------------*/            
CREATE PROC DBO.PROC_SetVehicleClassRulePol      
(            
@CUSTOMER_ID     INT,            
@POLICY_ID     INT,            
@POLICY_VERSION_ID     SMALLINT,            
@DRIVER_ID     SMALLINT,            
@BUSINESS_TYPE VARCHAR(20)=NULL          
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
SET @DRIVER_MVR_POINTS=0          
--IF(UPPER(@BUSINESS_TYPE)='NEWBUSINESS')          
--BEGIN          
      
      
 SELECT @VEHICLE_ID=VEHICLE_ID, @DRIVER_DOB=DRIVER_DOB, @DRIVER_GENDER=DRIVER_SEX,          
   @DRIVER_PRINCIPAL=APP_VEHICLE_PRIN_OCC_ID FROM POL_DRIVER_DETAILS WHERE           
   CUSTOMER_ID=@CUSTOMER_ID AND          
   POLICY_ID=@POLICY_ID AND          
   POLICY_VERSION_ID=@POLICY_VERSION_ID AND          
   DRIVER_ID=@DRIVER_ID          
 SELECT @VEHICLE_USE=APP_USE_VEHICLE_ID FROM POL_VEHICLES WHERE           
   CUSTOMER_ID=@CUSTOMER_ID AND          
   POLICY_ID=@POLICY_ID AND          
   POLICY_VERSION_ID=@POLICY_VERSION_ID AND          
   VEHICLE_ID=@VEHICLE_ID          
          
 SELECT @APPLICATION_STATE=STATE_ID FROM POL_CUSTOMER_POLICY_LIST WHERE          
   CUSTOMER_ID=@CUSTOMER_ID AND          
   POLICY_ID=@POLICY_ID AND          
   POLICY_VERSION_ID=@POLICY_VERSION_ID           
           
 IF (@VEHICLE_USE='11332') --VEHICLE IS FOR PERSONAL USE ONLY        
 BEGIN        
  SET @DRIVER_AGE = DATEDIFF(YY,@DRIVER_DOB,GETDATE())                    
  
  SELECT @DRIVER_MVR_POINTS=SUM(M.MVR_POINTS) FROM POL_MVR_INFORMATION A JOIN MNT_VIOLATIONS M        
   ON M.VIOLATION_ID=A.VIOLATION_ID WHERE        
   A.CUSTOMER_ID=@CUSTOMER_ID AND        
   A.POLICY_ID=@POLICY_ID AND        
   A.POLICY_VERSION_ID=@POLICY_VERSION_ID AND        
   A.DRIVER_ID=@DRIVER_ID     
  
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
  ELSE  -- FOR AGE<24  
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
   ELSE  --OCCASIONAL DRIVER      
   BEGIN  
    IF(@APPLICATION_STATE=14 and (UPPER(@DRIVER_GENDER)='F')) --Indian State        
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
        
 IF(NOT((@VEHICLE_CLASS='') OR (@VEHICLE_CLASS IS NULL)))        
    UPDATE POL_VEHICLES SET APP_VEHICLE_PERCLASS_ID=@VEHICLE_CLASS WHERE          
     CUSTOMER_ID=@CUSTOMER_ID AND          
     POLICY_ID=@POLICY_ID AND          
     POLICY_VERSION_ID=@POLICY_VERSION_ID AND          
     VEHICLE_ID=@VEHICLE_ID             
  END          
 --END          
END          
    
    
  



GO

