IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetGeneralClassRule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetGeneralClassRule]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
PROC NAME       : DBO.Proc_SetGeneralClassRule          
CREATED BY      : SWASTIKA GAUR          
DATE            : 24th Apr'06            
PURPOSE         :SET VEHICLE CLASS BASED ON THE DRIVER INFORMATION          
REVISON HISTORY :            
USED IN         : WOLVERINE            
------------------------------------------------------------            
DATE     REVIEW BY          COMMENTS            
------   ------------       -------------------------*/            
--DROP PROC Proc_SetGeneralClassRule      
CREATE PROC dBO.Proc_SetGeneralClassRule      
(            
@DRIVER_AGE     INT,            
@DRIVER_MVR_POINTS     INT,            
@DRIVER_DOB     DATETIME,            
@DRIVER_GENDER VARCHAR(5),  
@DRIVER_PRINCIPAL INT,   
@APPLICATION_STATE INT,  
@VEHICLE_CLASS VARCHAR(10) OUT        
)            
AS            
         
BEGIN            
  
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
/*  SELECT @VEHICLE_CLASS=V.LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES V JOIN MNT_LOOKUP_TABLES T          
   ON V.LOOKUP_ID=T.LOOKUP_ID          
   WHERE T.LOOKUP_NAME='VHCLSP' AND V.LOOKUP_VALUE_DESC=@VEHICLE_CLASS   
*/
--Lookup value for the class will be fetched from the lookup_value_code field as 
--lookup_value_description has been changed
  SELECT @VEHICLE_CLASS=V.LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES V JOIN MNT_LOOKUP_TABLES T          
   ON V.LOOKUP_ID=T.LOOKUP_ID          
   WHERE T.LOOKUP_NAME='VHCLSP' AND V.LOOKUP_VALUE_CODE=@VEHICLE_CLASS   

   
END  



GO

