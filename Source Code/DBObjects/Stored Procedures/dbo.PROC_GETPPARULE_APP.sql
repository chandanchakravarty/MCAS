IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETPPARULE_APP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETPPARULE_APP]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ===========================================================================================================                                                        
Proc Name                : Dbo.Proc_GetPPARule_app                                                                                                     
Created by               : Ashwani                                                                                                                      
Date                     : 01 Mar. 2000                                                                                                      
Purpose                  : To get the application info                                                                                      
Revison History          :                                                                                                                      
Used In                  : Wolverine             
Modified by     : Praveen kasana          
Modifed Date    : 20 June 2008           
Purpose    : As ACT_APP_CREDIT_CARD_DETAILS Fileds are Deleted  (Rule will be fired on PAY_PAL_REF_ID)          
        
Modified by     : Praveen kasana          
Modifed Date    : 07 Oct 2009         
Purpose      : Irack 6485 --Rule IN HO -2 Repiar and HO - 3 Repair         
                                                                                                               
===========================================================================================================                                                        
Date     Review By          Comments                                                                                                                      
========================================================================================================== */                                                
-- DROP PROC dbo.PROC_GETPPARULE_APP   1692,76,1, ''                                      
CREATE PROC [dbo].[PROC_GETPPARULE_APP]                                                                                                                                 
(                                                                                                                                            
 @CUSTOMERID    INT,                                                                                                                                            
 @APPID    INT,                                                                                                                                            
 @APPVERSIONID   INT,                                                                                                    
 @DESC TEXT                                                                                                                       
)                                                                                                                                            
AS                                                                                                                             
BEGIN                                                                                                                                     
  DECLARE @APP_ID INT                                      
  DECLARE @APP_VERSION_ID SMALLINT                                                                                                          
  DECLARE @STATE_ID INT                                                                                                             
  DECLARE @APP_LOB NVARCHAR(5)                                                                                                            
  DECLARE @APP_TERMS NVARCHAR(5)                                                                                                            
  DECLARE @APP_EFFECTIVE_DATE VARCHAR(25)                
  DECLARE @APP_EXPIRATION_DATE VARCHAR(20)                    
  DECLARE @APP_AGENCY_ID INT                                                                                            
  DECLARE @BILL_TYPE CHAR(2)                                                                     
DECLARE @PROXY_SIGN_OBTAINED nVARCHAR(10)                                                                                                            
  DECLARE @CHARGE_OFF_PRMIUM VARCHAR(5)             
DECLARE @APP_VERSION_NO NVARCHAR(75)                         
  DECLARE @APP_NO NVARCHAR(75)                                                                 
  DECLARE @POLICY_TYPE INT                                                   
  DECLARE @CALLED_FROM CHAR                                                 
  DECLARE @APP_SUBLOB INT                              
  DECLARE @DATE_APP_EFFECTIVE_DATE DATETIME                                      
  DECLARE @INTBILLING_PLAN INT                                 
  DECLARE @INTDOWN_PAY_MODE INT                                           
  DECLARE @DOWN_PAY_MENT CHAR       
  DECLARE @TRAILBLAZER_EXPIRY_DATE DATETIME
  SET @TRAILBLAZER_EXPIRY_DATE = '01/01/2010'                            
  IF EXISTS(SELECT CUSTOMER_ID FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID )                                                            
  BEGIN                                                                              
 SELECT      @STATE_ID=ISNULL(STATE_ID,-1),@APP_LOB=ISNULL(APP_LOB,''),@APP_TERMS=ISNULL(APP_TERMS,''),                                                  
    @APP_EFFECTIVE_DATE=ISNULL(CONVERT(VARCHAR(20),APP_EFFECTIVE_DATE),''),                                                   
    @APP_EXPIRATION_DATE=ISNULL(CONVERT(VARCHAR(20),APP_EXPIRATION_DATE),''),                                                                          
    @APP_AGENCY_ID=ISNULL(APP_AGENCY_ID,-1),@BILL_TYPE=ISNULL(BILL_TYPE,''),                                                                                                      
    @PROXY_SIGN_OBTAINED=ISNULL(convert(varchar,PROXY_SIGN_OBTAINED),''),@CHARGE_OFF_PRMIUM=ISNULL(CHARGE_OFF_PRMIUM,''),                                                                                                    
    @POLICY_TYPE=ISNULL(POLICY_TYPE,-1), @APP_VERSION_NO=ISNULL(APP_VERSION,''),@APP_NO=ISNULL(APP_NUMBER,''),                                                                                                      
    @APP_SUBLOB=APP_SUBLOB ,@DATE_APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE ,                                      
    @INTBILLING_PLAN=ISNULL(INSTALL_PLAN_ID,''),                                
    @INTDOWN_PAY_MODE=ISNULL(DOWN_PAY_MODE,0)    
 FROM  APP_LIST                                                                                
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                                                                  
  END                                                                                                                        
  ELSE                                                                                                   
  BEGIN                                                                                                                             
    SET @STATE_ID=-1                                                                                                            
    SET @APP_LOB=''                                                                                               
    SET @APP_TERMS=''                                                                                                            
    SET @APP_EFFECTIVE_DATE=''                                                                                                            
    SET @APP_EXPIRATION_DATE=''                                                                                         
    SET @APP_AGENCY_ID=-1                                                                                
    SET @BILL_TYPE =''                                                    
    SET @PROXY_SIGN_OBTAINED=-1                                                                                         
    SET @CHARGE_OFF_PRMIUM=''                                                                                                  
    SET @APP_VERSION_NO=''                 
    SET @APP_NO=''                                                             
    SET @POLICY_TYPE=-1                        
END                                                                                                     
 --------------------------------------------                                         
   IF(@CHARGE_OFF_PRMIUM='10964')                                          
    BEGIN                                                                                                             
    SET @CHARGE_OFF_PRMIUM='N'                               
    END                                                                                                      
   ELSE IF(@CHARGE_OFF_PRMIUM='10963')                                                                                                          
    BEGIN                                                                     
    SET @CHARGE_OFF_PRMIUM='Y'                             
    END                                                                                                               
   ELSE IF(@CHARGE_OFF_PRMIUM='')                                                        
    BEGIN                                                                                       
    SET @CHARGE_OFF_PRMIUM='N'                                                                                      
    END                                                                                    
                                                                                                    
--- Info for Client Top                                               
 DECLARE @CUSTOMER_NAME NVARCHAR(225)                                       
 DECLARE @ADDRESS NVARCHAR(225)                                                                                                          
 DECLARE @CUSTOMER_HOME_PHONE NVARCHAR(75)                                                                                       
 DECLARE @CUSTOMER_TYPE NVARCHAR(75)                                                                                                        
                                                               
  SELECT @CUSTOMER_NAME= ISNULL(CLT.CUSTOMER_FIRST_NAME,'')+ '  ' + ISNULL(CLT.CUSTOMER_MIDDLE_NAME,'') + '  ' +                                
 ISNULL(CLT.CUSTOMER_LAST_NAME,''),@ADDRESS=  ISNULL(CLT.CUSTOMER_ADDRESS1,'')+ ', ' +                                                                                  
 ISNULL(CLT.CUSTOMER_CITY,'')+ ', ' +                                                    
 ISNULL(STA.STATE_NAME,'')+ ', '+                                                                                   
 ISNULL(CON.COUNTRY_NAME,'')+ ', '+                                                                                  
 ISNULL(CLT.CUSTOMER_ZIP,''),                                                      
 @CUSTOMER_HOME_PHONE=ISNULL(CUSTOMER_HOME_PHONE,'') ,                                                                                                        
 @CUSTOMER_TYPE=ISNULL(CUSTOMER_TYPE_LOOKUP.LOOKUP_VALUE_DESC,'')                                                                                                         
 FROM CLT_CUSTOMER_LIST CLT                                                                                                           
 LEFT OUTER JOIN MNT_COUNTRY_LIST CON  ON CLT.CUSTOMER_COUNTRY = CON.COUNTRY_ID                                                          
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST STA   ON CLT.CUSTOMER_STATE = STA.STATE_ID                                                                                                          
 LEFT JOIN MNT_LOOKUP_VALUES CUSTOMER_TYPE_LOOKUP ON CUSTOMER_TYPE_LOOKUP.LOOKUP_UNIQUE_ID = CLT.CUSTOMER_TYPE                                             
 WHERE CUSTOMER_ID=@CUSTOMERID                                                                     
 --                                                                                           
 IF(@CUSTOMER_HOME_PHONE='' OR @CUSTOMER_HOME_PHONE IS NULL)                                                                      
   BEGIN                                                  
   SET @CUSTOMER_HOME_PHONE='NA'                         
  END                
-- Primary Applicant            
 DECLARE @IS_PRIMARY_APPLICANT CHAR                                                                                                 
 DECLARE @INTCOUNT INT                                                
 IF EXISTS ( SELECT CUSTOMER_ID  FROM APP_APPLICANT_LIST                                                                               
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID    )                                                                                                
 BEGIN                                                                                            
  SELECT @INTCOUNT=COUNT(APP.IS_PRIMARY_APPLICANT )                                                                                                
  FROM APP_APPLICANT_LIST APP INNER  JOIN CLT_APPLICANT_LIST CLT          
  ON CLT.CUSTOMER_ID=APP.CUSTOMER_ID AND CLT.APPLICANT_ID=APP.APPLICANT_ID                                                                                                                                   
  WHERE APP.CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND CLT.IS_ACTIVE='Y'           
  AND  APP.IS_PRIMARY_APPLICANT=1                                                                               
                                                                                     
   IF(@INTCOUNT =0)                                                                
    BEGIN                                                                                                 
    SET @IS_PRIMARY_APPLICANT='' -- PLZ SELECT PA                                                                                                
    END                                                                                                
   ELSE                                    
    BEGIN                                                                 
    SET @IS_PRIMARY_APPLICANT='N'                                                                                                
    END                                                                                      
                                                                           
 END                                                                                                 
 ELSE                                                                                                
 BEGIN                                                                                            
 SET @IS_PRIMARY_APPLICANT=''                                                                                        
 END                                                                                            
  --SELECT @INTCOUNT ,@IS_PRIMARY_APPLICANT                                                                                      
                                                                                          
  -- Insurance Score for rental Dwelling Only                                
   DECLARE @NUM_CUSTOMER_INSURANCE_SCORE NUMERIC                              
   SELECT @NUM_CUSTOMER_INSURANCE_SCORE=ISNULL(CUSTOMER_INSURANCE_SCORE,-1) FROM CLT_CUSTOMER_LIST                                                                                     
   WHERE CUSTOMER_ID=@CUSTOMERID AND IS_ACTIVE='Y'            
                                                         
 IF(@APP_LOB=6)-- RD                                  
 BEGIN                                                                                       
  DECLARE @CUSTOMER_INSURANCE_SCORE CHAR                               
  IF(@NUM_CUSTOMER_INSURANCE_SCORE=-1)                                                                        
   BEGIN                                                                                           
SET @CUSTOMER_INSURANCE_SCORE =''            
   END                                                                                        
  ELSE                                                                           
   BEGIN                                  
   SET @CUSTOMER_INSURANCE_SCORE ='N'             
   END                 
 END           
 ELSE                         
 BEGIN                                                                       
 SET @CUSTOMER_INSURANCE_SCORE='N'                        
 END                                                                                         
                                                                                        
                                                                                                      
                                                                                          
--Only for RD and Michigan                                                  
  declare @MI_CUSTOMER_INSURANCE_SCORE char                                                                                           
                                                                                          
 IF(@APP_LOB=6  AND @STATE_ID=22 )                                                                   
 BEGIN                                                                                      
 IF(@NUM_CUSTOMER_INSURANCE_SCORE<550 AND @NUM_CUSTOMER_INSURANCE_SCORE>0)                                                                                          
 BEGIN                                                                                        
 SET @MI_CUSTOMER_INSURANCE_SCORE='Y'                                                                                          
 END                                                                                          
 ELSE                                     
 BEGIN                                                                                           
 SET @MI_CUSTOMER_INSURANCE_SCORE='N'                                                                                          
 END                                              
 END                                                                                           
 ELSE                                                                                           
 BEGIN                                                                                           
 SET @MI_CUSTOMER_INSURANCE_SCORE='N'                                                                                           
 END            
                                                                                     
---------------------------------------------------                                                                                
-- Only for Rental/Homeowner to chk the product type or policy type like HO-3 , Ho-4 etc                                                                                 
-- HO -- 1 & RD -- 6                                                                                  
 IF(@POLICY_TYPE= 0 OR @POLICY_TYPE= -1 OR UPPER(@POLICY_TYPE)=NULL  )                                                                                
  BEGIN                                                                                 
  SET @POLICY_TYPE=-1                                                                 
  END                                                                                 
                                                                              
--------------------------------------------------------                   
-- Called from Policy=1 or Application =0                                                                              
 set @CALLED_FROM='0'                                                                        
----------------------------------------------------------------------                                                                                
-- App can not be submitted, if trailer is not attached to a boat                                                                                 
 DECLARE @INTCOUNTNULL INT          
 DECLARE @TRAILER_ASSOCIATED_BOAT CHAR                        
                    
 IF EXISTS(SELECT CUSTOMER_ID FROM  APP_WATERCRAFT_TRAILER_INFO                                     
  WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID)                 
 BEGIN              
  SELECT @INTCOUNTNULL=COUNT(ASSOCIATED_BOAT) FROM  APP_WATERCRAFT_TRAILER_INFO                   
  WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID                                                                               
  AND ASSOCIATED_BOAT=0  AND IS_ACTIVE='Y'                                                  
 END                                                        
 ELSE                                               
 BEGIN                                                                               
  SET @TRAILER_ASSOCIATED_BOAT='N'                                                            
 END             
                                                                           
 IF(@INTCOUNTNULL > 0)                                                                                
 BEGIN                                                                                 
  SET @TRAILER_ASSOCIATED_BOAT='Y'                                                                                
 END                                              
 ELSE                                                                                
 BEGIN                                                                                 
  SET @TRAILER_ASSOCIATED_BOAT='N'                                                                                
 END                                                                 
                                                                              
 IF(@TRAILER_ASSOCIATED_BOAT='Y')                                                                      
 BEGIN                                                                               
  SET @TRAILER_ASSOCIATED_BOAT=''                                                                              
 END                                             
-------           
 /*IF(@APP_LOB=4)            
 BEGIN          
  DECLARE @TRAILER_DED_ID VARCHAR             
  DECLARE @TRAILER_DEDUCTIBLE VARCHAR                                                 
   SELECT @TRAILER_DED_ID=isnull(convert(VARCHAR,TRAILER_DED_ID),'') FROM  APP_WATERCRAFT_TRAILER_INFO                                                             
   WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID           
   IF (@TRAILER_DED_ID = '' OR @TRAILER_DED_ID = 0)             
  BEGIN           
   SET @TRAILER_DEDUCTIBLE = ''          
  END            
 ELSE           
  BEGIN           
   SET @TRAILER_DEDUCTIBLE = 'N'          
  END            
 END           
 ELSE              
 BEGIN           
  SET @TRAILER_DEDUCTIBLE = 'N'          
 END   */                                       
-------                                                                             
-----------------------------------------------------------------------                                                             
                                 
----------------                                                                        
--If Picture of Location Attached is  No Refer :13 June 2006 for HOME                                                                        
--If Property Inspection/Cost Estimator is Attached is No - Refer        -- Itrack No.1579 In Rental also                                                                   
                                                                    
Declare @INTPIC_OF_LOC int                                                                       
declare @PIC_OF_LOC char           
declare @INTPROPRTY_INSP_CREDIT  int                                                                    
declare @PROPRTY_INSP_CREDIT char           
                                                           
IF(@APP_LOB=1)-- HOME                                                                          
BEGIN                                       
   SELECT @INTPIC_OF_LOC=ISNULL(PIC_OF_LOC,''),@INTPROPRTY_INSP_CREDIT=ISNULL(PROPRTY_INSP_CREDIT,0)                        
   FROM  APP_LIST                                            
  WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID           
           
/* 11405 HO-4 Tenants          
   11406 HO-6 Unit Owners            
   11195 HO-4 Tenants          
   11196 HO-6 Unit Owners           
   11193 HO-2 Repair             
   11194 HO-3 Repair          
   11403 HO-2 Repair             
 11404 HO-3 Repair          
   11480 DP-2 Repair          
   11479 DP-2 Replacement          
   11482 DP-3 Repair          
 11481 DP-3 Replacement          
*/            
 --For Property inspection credit            
  IF(@STATE_ID=14 )           
  BEGIN                        
      IF(@POLICY_TYPE IN (11192,11148,11149,11193,11194))                           
          BEGIN                                        
           IF(@INTPROPRTY_INSP_CREDIT=10964 ) --OR @INTPROPRTY_INSP_CREDIT=0                            
       BEGIN                                                                    
       SET @PROPRTY_INSP_CREDIT='Y'                                                                    
       END                           
           ELSE  IF(@INTPROPRTY_INSP_CREDIT=0 )                           
       BEGIN                                                                    
       SET @PROPRTY_INSP_CREDIT=''                                                                    
       END                                                                    
          ELSE                                                                    
       BEGIN                                               
       SET @PROPRTY_INSP_CREDIT='N'                                                                    
       END                           
      END                          
      ELSE                                                                    
     BEGIN                                                                    
     SET @PROPRTY_INSP_CREDIT='N'                                                                    
     END           
    --===============          
     IF(@POLICY_TYPE IN (11192,11148,11149,11193,11194))                          
     BEGIN               
          IF(@INTPIC_OF_LOC = 10964 ) --NO                           
            BEGIN 
            SET @PIC_OF_LOC='Y'                                                                        
            END                                                                    
          ELSE IF (@INTPIC_OF_LOC = 0)                                                                        
            BEGIN                                                                        
            SET @PIC_OF_LOC=''                   
            END                             
                                     
          ELSE                 
                   BEGIN                                                                        
                   SET @PIC_OF_LOC='N'                                                                        
                   END                           
     END                          
     ELSE                                                                        
         BEGIN                                                      
         SET @PIC_OF_LOC='N'                                                                       
         END                           
    --===============                                                              
  END           
          
  ELSE IF(@STATE_ID=22)          
  BEGIN              
                  
      IF(@POLICY_TYPE !=11405 and  @POLICY_TYPE!=11406)         
          BEGIN                                        
           IF(@INTPROPRTY_INSP_CREDIT=10964 ) --OR @INTPROPRTY_INSP_CREDIT=0                            
       BEGIN                                                                    
       SET @PROPRTY_INSP_CREDIT='Y'                                                                    
       END                           
           ELSE  IF(@INTPROPRTY_INSP_CREDIT=0 )                           
       BEGIN                                                                    
       SET @PROPRTY_INSP_CREDIT=''                 
       END                 
          ELSE                                                                    
       BEGIN                                               
       SET @PROPRTY_INSP_CREDIT='N'                                        
       END                           
      END                          
      ELSE                          
     BEGIN             
     SET @PROPRTY_INSP_CREDIT='N'                                          
     END           
    --===================          
     IF(@POLICY_TYPE !=11405 and @POLICY_TYPE!=11406)            
     BEGIN               
          IF(@INTPIC_OF_LOC = 10964 ) --NO                           
            BEGIN                                                                        
            SET @PIC_OF_LOC='Y'                                                                        
            END                                                                    
          ELSE IF (@INTPIC_OF_LOC = 0)                                                                        
            BEGIN                                                                        
            SET @PIC_OF_LOC=''                                                                        
            END                             
                                     
          ELSE                 
            BEGIN                                                                        
            SET @PIC_OF_LOC='N'                                                                        
            END                           
     END                          
     ELSE                                                                        
         BEGIN                                                                        
         SET @PIC_OF_LOC='N'                                                                       
         END                           
   --==================          
          
             
END                
END          
--******************Start Itrack No. 1579 **************************************          
                                                                      
declare @PICH_OF_LOC char                                     
declare @PROPERTY_INSP_CREDIT char           
          
IF(@APP_LOB=6)-- Rental                                                                          
BEGIN                                       
   SELECT @INTPIC_OF_LOC=ISNULL(PIC_OF_LOC,''),@INTPROPRTY_INSP_CREDIT=ISNULL(PROPRTY_INSP_CREDIT,0)                                
   FROM  APP_LIST                                                      
   WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID           
          
 /*          
 11480 DP-2 Repair          
     11479 DP-2 Replacement          
 11482 DP-3 Repair          
 11481 DP-3 Replacement          
          
 Rental Michigan  :          
           
 11289 : DP-2 Replacement          
 11458 : DP-3 Premier           
 11291 : DP-3 Replacement          
          
 Rental Indiana  :           
 11479 : DP-2 Replacement           
 11481 : DP-3 Replacement          
            
 */          
  --IF(@POLICY_TYPE IN (11479,11480,11481,11482,11290,11289,11291,11292,11458))           
  IF(@POLICY_TYPE IN (11289,11458,11291,11479,11481))                           
       BEGIN                                        
        IF(@INTPROPRTY_INSP_CREDIT=10964 ) --OR @INTPROPRTY_INSP_CREDIT=0                            
    BEGIN                                                                    
    SET @PROPERTY_INSP_CREDIT='Y'                                                                    
    END                           
        ELSE  IF(@INTPROPRTY_INSP_CREDIT=0 )                           
    BEGIN                                                                    
    SET @PROPERTY_INSP_CREDIT=''                                                                    
    END                                                                    
       ELSE                                                                    
    BEGIN                                               
    SET @PROPERTY_INSP_CREDIT='N'                                                                    
    END                           
     END                          
    ELSE                                                                    
   BEGIN                                                       
   SET @PROPERTY_INSP_CREDIT='N'                                                                
   END           
    --===============          
   IF(@POLICY_TYPE IN (11479,11480,11481,11482,11290,11289,11291,11292,11458))                          
  --IF(@POLICY_TYPE IN (11289,11458,11291,11479,11481))            
   BEGIN               
        IF(@INTPIC_OF_LOC = 10964 ) --NO                           
          BEGIN                                       
          SET @PICH_OF_LOC='Y'                             
          END                                                                    
        ELSE IF (@INTPIC_OF_LOC = 0)                 
          BEGIN           
          SET @PICH_OF_LOC=''                                       
          END                             
                          
        ELSE                 
           BEGIN                                                                        
                 SET @PICH_OF_LOC='N'                                                                        
                 END                           
   END                          
   ELSE                                                                        
       BEGIN                                                                        
       SET @PICH_OF_LOC='N'                                                                      
       END                           
END            
          
          
---*****************End Itrack No. 1579           
--Mandatory check for the :Down payment in cases of   Insured Bill types                                        
                                
  IF(@BILL_TYPE='DB' AND @INTDOWN_PAY_MODE =0 )                                        
    BEGIN                                        
    SET  @DOWN_PAY_MENT=''                                        
    END                                        
  ELSE                                        
    BEGIN                                        
    SET @DOWN_PAY_MENT='N'                                        
    END                                 
                                
--Mandatory check for the : PIC OF LOC :22 June 2006                                                  
/*11400,11401,11402,11405,11148,11149,11192,11409,11410*/                                                          
                                           
/*IF(@POLICY_TYPE=11400 OR @POLICY_TYPE=11401 OR @POLICY_TYPE=11402                                
 OR @POLICY_TYPE=11148 OR @POLICY_TYPE=11149 OR                             
 @POLICY_TYPE=11192 OR @POLICY_TYPE=11409 OR @POLICY_TYPE=11410 OR @POLICY_TYPE=11405)                                                          
                                                        
 IF(@INTPIC_OF_LOC='' or @INTPIC_OF_LOC=0)                                                          
  BEGIN                                          
  SET @PIC_OF_LOC = ''           
  END  */                           
                           
--PROPRTY_INSP_CREDIT, it should be checked for all home types and blank should be considered as NO                                        
                            
 /*IF(@POLICY_TYPE != 11195 AND @POLICY_TYPE != 11196  )                          
                              
 IF(@INTPROPRTY_INSP_CREDIT=0 )                                                        
     BEGIN                                                          
     SET @PROPRTY_INSP_CREDIT=''                                                  
     END */                         
                           
/*                                                 
ELSE                                                
BEGIN                                                
SET @PROPRTY_INSP_CREDIT='N'                                                 
END */                                         
-------------==============================================================                                                    
--"Any HO claims in the last 3 years , refer to U/w          
-- Claims History - new or renewal in the last 3 years           
--   liability claim arising our of the negligence of the insured                                                                  
--=========================================================================          
DECLARE @OCCURENCE_DATE INT                                         
DECLARE @HO_CLAIMS CHAR           
      
--Added by Charles on 26-Nov-09 for Itrack 6616       
SET @HO_CLAIMS ='N'                                                     
DECLARE @APP_EFFECTIVE_DATE_3_BEFORE DATETIME     
SET @APP_EFFECTIVE_DATE_3_BEFORE=DATEADD(YY,-3,@APP_EFFECTIVE_DATE)             
--Added till here             
    
--Added by Charles on 30-Nov-09 for Itrack 6647      
  DECLARE @WBSPO_LOSS CHAR      
  SET @WBSPO_LOSS = 'N'                                                                      
--Added till here       
                                                             
--DECLARE @intAMOUNT_PAID INT                             
IF(@APP_LOB=1)-- HOME                                                     
BEGIN        
 -- IF(@OCCURENCE_DATE<=1095 )--AND @intAMOUNT_PAID>1000)  --Commented by Charles on 26-Nov-09 for Itrack 6616            
      
  --Added by Charles on 26-Nov-09 for Itrack 6616                                                 
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB = 1       
 AND (CAST(CONVERT(VARCHAR(25),ISNULL(OCCURENCE_DATE,''),101) AS DATETIME) >=@APP_EFFECTIVE_DATE_3_BEFORE       
 AND CAST(CONVERT(VARCHAR(25),ISNULL(OCCURENCE_DATE,''),101) AS DATETIME)<=@APP_EFFECTIVE_DATE))        
 BEGIN       
  SET @HO_CLAIMS ='Y'      
 END        
 --Added till here        
    
  --Added by Charles on 30-Nov-09 for Itrack 6647    
     IF EXISTS(SELECT PRIOR_LOSS_ID FROM PRIOR_LOSS_HOME WITH(NOLOCK) WHERE ISNULL(WATERBACKUP_SUMPPUMP_LOSS,10964)=10963    
     AND CUSTOMER_ID=@CUSTOMERID AND LOSS_ID IN (SELECT LOSS_ID FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB = 1       
  AND (CAST(CONVERT(VARCHAR(25),ISNULL(OCCURENCE_DATE,''),101) AS DATETIME) >=@APP_EFFECTIVE_DATE_3_BEFORE       
  AND CAST(CONVERT(VARCHAR(25),ISNULL(OCCURENCE_DATE,''),101) AS DATETIME)<=@APP_EFFECTIVE_DATE)))      
  BEGIN     
  IF EXISTS(SELECT CUSTOMER_ID FROM APP_DWELLING_SECTION_COVERAGES WITH(NOLOCK)     
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND COVERAGE_CODE_ID IN(197,198)    
        AND DWELLING_ID IN (SELECT DWELLING_ID FROM APP_DWELLINGS_INFO WITH(NOLOCK)     
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND ISNULL(IS_ACTIVE,'N')='Y'))                                                              
  BEGIN    
   SET @WBSPO_LOSS='Y'         
  END    
  END    
 --Added till here                           
END                                  
                                                
-----   Rental Paid Claims          
                                                   
IF(@APP_LOB=6 or @APP_LOB=4 )-- RENTAL                                                                                         
BEGIN                                                                     
 SELECT @OCCURENCE_DATE = DATEDIFF(DAY,OCCURENCE_DATE,@APP_EFFECTIVE_DATE)                                                                
 FROM  APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB =@APP_LOB --6--RENTAL                                                           
           
 --SELECT @intAMOUNT_PAID=SUM(AMOUNT_PAID)                                                           
 --FROM  APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND LOB =6             
                                                                 
  IF(@OCCURENCE_DATE<=1095) --AND @intAMOUNT_PAID>1000) -- Amount Paid more than $1000  and Renewal in the last 3 years(1095 days)                                                                
    BEGIN                                                                  
    SET @HO_CLAIMS ='Y'                                                                  
    END                                                                  
   ELSE                                                                  
    BEGIN                                                                  
    SET @HO_CLAIMS ='N'                                                              
    END                                                                  
END                                                  
-------------                                           
-- Application Details , If State is Indiana                                               
-- If no details entered on the Prior Coverage Tab , Refer to underwriters                 
                          
DECLARE @PRIOR_POLICY_INFO CHAR  ,@PRIOR_POLICY_INFO_EXPIRE CHAR                                             
SET  @PRIOR_POLICY_INFO='N'            
SET  @PRIOR_POLICY_INFO_EXPIRE='N'                                              
                                      
IF(1=1) --(@STATE_ID = 14)    now applicable to both state as per Rajan Discussion          
BEGIN                    
 IF NOT EXISTS (SELECT CUSTOMER_ID FROM  APP_PRIOR_CARRIER_INFO                                    
    WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2)                                              
  SET @PRIOR_POLICY_INFO='Y'          
  ELSE          
 BEGIN          
  --ADDED BY PRAVESH ON 5 AUG 2008 AS PER MAIL BY RAJAN          
  --If there is/are AUTO polices in Prior Policy section,           
  --and none of them has expiration date => Eff date of NBS App, then refer.           
  --(In other words, all prior policy are expiring before NBS effective, means there is a lapse in coverage, then refer)           
   IF NOT EXISTS(SELECT CUSTOMER_ID FROM  APP_PRIOR_CARRIER_INFO                                   
      WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2           
      AND DATEDIFF(DAY,CAST(@APP_EFFECTIVE_DATE AS DATETIME),EXP_DATE)>=0          
      )                                              
    SET @PRIOR_POLICY_INFO_EXPIRE='Y'                          
 END                            
END           
-------------------------------------------                                              
-- If there is a loss for Automobile LOB with Driver Field Empty At Fault is Blank                                               
-- Refer to underwriters                                               
                    
              
DECLARE @DRIVER_ID VARCHAR(15)                       
DECLARE @AT_FAULT VARCHAR(15)                                      
DECLARE @AUTO_DRIVER_FAULT CHAR                                              
                          
SELECT @DRIVER_ID=ISNULL(CONVERT(VARCHAR(15),DRIVER_ID),''),@AT_FAULT = ISNULL(CONVERT(VARCHAR(15),AT_FAULT),'')             
 FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND  LOB=2 --AND (DRIVER_ID IS NULL OR DRIVER_ID =0)          
                                              
IF(@DRIVER_ID='' OR @DRIVER_ID ='0' OR @AT_FAULT='' OR  @AT_FAULT='0')                                              
BEGIN                                      
 SET @AUTO_DRIVER_FAULT='Y'               
END                                               
ELSE             
BEGIN                               
 SET @AUTO_DRIVER_FAULT='N'                                              
END                                      
                                                                    
------------------------------------------------                                                                                                
/*Application/Policy Details  If State is Michigan Loss Tab Check for New Business and at renewal                                               
  At Fault Field Count the number of At Fault Fields that have No within the last 5 years                                               
  based on the effective Date of the policy - Application/Policy Details - Effective Date                                               
  If greater than 5  Refer to Underwriters */                                              
  --modified by Pravesh on 26 june as per Itrack 4392, combine comprehensive and not fault /  9765-- Comprehensive    loss type                                      
DECLARE @NO_AT_FAULT CHAR                                              
DECLARE @INTAT_FAULT INT,@CLAIM_COUNT INT                                              
 SET @NO_AT_FAULT='N'                                              
                                              
IF(@STATE_ID=22) --MI                                              
BEGIN                                               
-- SELECT @INTAT_FAULT=COUNT(CUSTOMER_ID)                             
-- FROM FETCH_ACCIDENT                                           
-- WHERE CUSTOMER_ID = @CUSTOMERID AND (POLICY_ID IS NULL) AND (POLICY_VERSION_ID IS NULL) AND LOB=2                                               
-- AND (AT_FAULT='10964'OR  LOSS_TYPE='9765'  )          
-- AND  ((ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=5                                               
-- AND (ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0)             
 SELECT @INTAT_FAULT=COUNT(CUSTOMER_ID)                             
 FROM APP_PRIOR_LOSS_INFO  WITH (NOLOCK)                                                           
 WHERE CUSTOMER_ID = @CUSTOMERID  AND LOB=2                                               
 AND (AT_FAULT='10964'OR  LOSS_TYPE='9765'  )          
 AND  ((ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=5                                               
 AND (ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0)                                           
                                          
  IF(@INTAT_FAULT>5)               
  SET @NO_AT_FAULT='Y'          
          
 SELECT @CLAIM_COUNT=COUNT(CUSTOMER_ID) FROM CLM_CLAIM_INFO WHERE CUSTOMER_ID=@CUSTOMERID and LOB_ID=2          
  --AND dbo.instring(replace(PINK_SLIP_TYPE_LIST,',',' '),'13005')>0    --Commented by Charles on 9-Dec-09 for Itrack 6620      
  AND ISNULL(AT_FAULT_INDICATOR,0) = 2  --Added by Charles on 9-Dec-09 for Itrack 6620  
  AND  ((ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(LOSS_DATE,0)),0))<=5                                               
  AND (ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(LOSS_DATE,0)),0))>=0)          
 IF(@INTAT_FAULT+@CLAIM_COUNT > 5)                                                
  SET @NO_AT_FAULT='Y'          
          
END                                              
------------------------------------------------                                                    
/*Application Details  If the State is Michigan  On New business Prior Information tab Prior Policy                     
 Take the effective date of the policy and subtract the Expiration date Field                                               
 if the answer is greater than 6 months Then refer to underwriters */                            
DECLARE @INT_EFF_EXP_DATE INT                                        
DECLARE @EFF_DATE DATETIME                                              
DECLARE @EXP_DATE DATETIME                                              
DECLARE @EFF_EXP_DATE CHAR                                              
SET @EFF_EXP_DATE='N'                
DECLARE @CARRIER CHAR                                               
SET @CARRIER='N'                        
                                              
SELECT @EFF_DATE=EFF_DATE,@EXP_DATE=EXP_DATE,@CARRIER=ISNULL(CARRIER,'')                                               
 FROM APP_PRIOR_CARRIER_INFO WHERE CUSTOMER_ID = @CUSTOMERID AND LOB=2                                               
                                              
SET @INT_EFF_EXP_DATE= DATEDIFF(MM,@EFF_DATE,@EXP_DATE)                                              
                                              
IF(@STATE_ID =22 AND @INT_EFF_EXP_DATE>6)                                              
BEGIN             
SET @EFF_EXP_DATE='Y'                                              
END                
------------------------------------------------             
/* PRIOR INFORMATION TAB IF PREVIOUS CARRIER FIELD IS NOT FILLED.                                              
OTHER WISE REFER TO UNDERWRITERS */                                              
IF(@CARRIER='')                                              
 SET @CARRIER='Y'                 
------------------------------------------------------------------------------------------                                   
/* Application/Policy Information                                               
If State is Indiana ,and Sub line of Business is Trailblazer need to check for the following`                   
                        
Vehicle Infor Tab                                               
Vehicle Use is Personal for all vehicles on the policy                                               
Must meet all the other requirements for a Standard Policy and in addition                                               
                                              
Look at the Customer details screen the Insurance Score Field                                               
If 700 or less - not eligible                                               
`If greater than 700'                                              
Then Look at co-applicants tab take the effective date of the policy minus the date of birth for all applicants                                               
if all drivers are not between the ages of 35- 69 not eligible                                               
                  
If yes then look at the Prior Loss and Loss Tab                                               
Look for losses in the last 3 years - Effective date of the policy minus the date of claim                                              
If the total amount paid for any of these losses exceed $75.00  not eligible                                               
If the total amount paid is under $75.00 each then                             
New Business                                               
Look at the Drivers/Household Members tab for any drivers under age 35                                              
Look on the MVR tab - Ordered Field - If no refer to underwriters  */                                              
 DECLARE @THRTFIVEYEARDATE DATETIME          
DECLARE @THRTFIVEYEARDAYS INT          
DECLARE @SEVENTYYEARDATE DATETIME          
DECLARE @SEVENTYYEARDAYS INT          
SET @THRTFIVEYEARDAYS=0          
SET @SEVENTYYEARDAYS=0          
SET @THRTFIVEYEARDATE = DATEADD(YEAR,-35,@APP_EFFECTIVE_DATE)          
SET @THRTFIVEYEARDAYS = DATEDIFF(DAY,@THRTFIVEYEARDATE,@APP_EFFECTIVE_DATE)          
SET @SEVENTYYEARDATE = DATEADD(YEAR,-70,@APP_EFFECTIVE_DATE)          
SET @SEVENTYYEARDAYS = DATEDIFF(DAY,@SEVENTYYEARDATE,@APP_EFFECTIVE_DATE)                                             
                
                                              
--If State is Indiana ,and Sub line of Business is Trailblazer need to check for the following                                              
-- Look at the Customer details screen the Insurance Score Field                                               
-- If 700 or less - not eligible                                               
 DECLARE @LESS_INSURANCE_SCORE CHAR                                              
 DECLARE @MVR_VER CHAR                                      
 DECLARE @INELIGIBLE_DRIVER CHAR                                         
 DECLARE  @LOSS_AMT_EXCEED CHAR                             
                  
 SET @LOSS_AMT_EXCEED='N'                           
 SET @INELIGIBLE_DRIVER='N'                                              
 SET @LESS_INSURANCE_SCORE ='N'                                               
SET @MVR_VER='N'                               
                       
  -- Itrack 6828 Trailblazer  
IF(@APP_EFFECTIVE_DATE<@TRAILBLAZER_EXPIRY_DATE) 
	BEGIN       
		IF(@STATE_ID=14 AND @APP_SUBLOB=1)                                              
			BEGIN                                               
			IF(@NUM_CUSTOMER_INSURANCE_SCORE <=700)                                              
				BEGIN                                    
					SET @LESS_INSURANCE_SCORE='Y' --REJECT                                              
				END                  
			ELSE                                              
				BEGIN                                               
		   -- If greater than 700'Then Look at Drivers tab take the effective date of the policy minus the date                                               
		   -- of birth for all drivers  if all drivers are not between the ages of 35- 69 not eligible                                       
		                              
				IF EXISTS(SELECT ISNULL(CLT_APPLICANT_LIST.CO_APPL_DOB,0) FROM CLT_APPLICANT_LIST INNER JOIN APP_APPLICANT_LIST          
					ON CLT_APPLICANT_LIST.CUSTOMER_ID = APP_APPLICANT_LIST.CUSTOMER_ID AND APP_APPLICANT_LIST.APPLICANT_ID = CLT_APPLICANT_LIST.APPLICANT_ID          
					WHERE APP_APPLICANT_LIST.CUSTOMER_ID=@CUSTOMERID AND APP_APPLICANT_LIST.APP_ID=@APPID AND APP_APPLICANT_LIST.APP_VERSION_ID=@APPVERSIONID           
					AND APP_APPLICANT_LIST.IS_PRIMARY_APPLICANT = 1 AND CONVERT(INT,DATEDIFF(day,ISNULL(CLT_APPLICANT_LIST.CO_APPL_DOB,0),@DATE_APP_EFFECTIVE_DATE)) NOT BETWEEN @THRTFIVEYEARDAYS AND @SEVENTYYEARDAYS)                                              
						BEGIN                                               
							SET @INELIGIBLE_DRIVER='Y' -- REJECT                                              
						END                              
				ELSE                                
					BEGIN   
		   
					 /* If yes then look at the Prior Loss and Loss Tab Look for losses in the last 3 years -                                             
					 Effective date of the policy minus the date of claim If the total amount paid for any of these                                               
					 losses exceed $75.00  not eligible */                                 
						   -- Itrack 6828 Trailblazer
							                                          
							IF EXISTS(SELECT AMOUNT_PAID FROM APP_PRIOR_LOSS_INFO                                               
							WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND AMOUNT_PAID>75                                               
							AND  (DATEDIFF(DAY,ISNULL(OCCURENCE_DATE,0),@DATE_APP_EFFECTIVE_DATE)<1095))                                               
								BEGIN          
									SET @LOSS_AMT_EXCEED='Y' -- REJECT                                               
								END                                             
					   -- IF THE TOTAL AMOUNT PAID IS UNDER $75.00 EACH THEN NEW BUSINESS                                               
					   -- LOOK AT THE DRIVERS/HOUSEHOLD MEMBERS TAB FOR ANY DRIVERS UNDER AGE 35                                              
					   -- LOOK ON THE MVR TAB - ORDERED FIELD - IF NO REFER TO UNDERWRITERS    
						-- Itrack 6828 Trailblazer
					ELSE IF EXISTS(SELECT AMOUNT_PAID FROM APP_PRIOR_LOSS_INFO                                               
									WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=2 AND AMOUNT_PAID<75                                               
									 AND(DATEDIFF(day,ISNULL(OCCURENCE_DATE,0),@DATE_APP_EFFECTIVE_DATE)<1095))                                              
						BEGIN                                               
							IF EXISTS(SELECT DRIVER_DOB FROM APP_DRIVER_DETAILS                                               
								WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                               
								AND CONVERT(INT,DATEDIFF(day,DRIVER_DOB,@DATE_APP_EFFECTIVE_DATE))<12775)                                              
								BEGIN                                                
									IF EXISTS (SELECT VERIFIED FROM APP_MVR_INFORMATION                                              
										WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND VERIFIED='0')                                              
											BEGIN                 
												SET @MVR_VER='Y' -- REFER HERE           
											END                                              
								END               
						END               
					END                                         
				END            
			END                                              
    END     
--select @LESS_INSURANCE_SCORE as LESS_INSURANCE_SCORE,@INELIGIBLE_DRIVER as INELIGIBLE_DRIVER,                                
--@LOSS_AMT_EXCEED as LOSS_AMT_EXCEED,@MVR_VER as MVR_VER                                              
-------------------------------------------------------------------------------------------------------            
-- Umbrella                                               
-- Customer  Details Applicants Occupation If occupation is any of the following refer to underwriters                                               
--Actor/Actress ,Athletes ,Author/Writer ,Celebrity ,Government Appointee - State ,Government Appointee - Federal ,                                              
--Newspaper Editor/Columnist ,Entertainer ,Labor Leaders other than Shop Stewards,Newspaper Publisher ,Public Lecturer ,                                              
--Public Office Holder ,Radio & Television Announcer                                               
               
 DECLARE @APPLICANT_OCCU VARCHAR(20)                              
 SET @APPLICANT_OCCU='N'                                              
 IF(@APP_LOB=5)-- UMBRELLA                                              
 BEGIN                                               
   IF EXISTS(SELECT APPLICANT_OCCU = ISNULL(CONVERT(VARCHAR(20),APPLICANT_OCCU),'')                     
      FROM CLT_CUSTOMER_LIST                                               
   WHERE CUSTOMER_ID=@CUSTOMERID AND IS_ACTIVE='Y' AND APPLICANT_OCCU IN                             
      (250,275,280,11817,11825,561,432,11819,11820,11822,11823,602))                                
     BEGIN                                   
     SET @APPLICANT_OCCU='Y'                                              
     END                                 
  END                                 
--IF PLAN PAYMENT MODE FOR THE PLAN OPTED BY USER is "EFT" OR THE DOWN PAYMENT MODE IS "EFT" THAN USER WILL NOT BE                                     
--ALLOWED TO SUMIT THE APPLICATION IF THE EFT RELATED INFORMATION IS NOT PROVIDED AT BILLING-INFO TAB.                                     
--IF THIS RULES IS VOILATED IT SHOULD BE TREATED AS REFFERED CASE                  
                                    
DECLARE @TRANSIT_ROUTING_NO   VARCHAR(100)                                   
DECLARE @DFI_ACC_NO   VARCHAR(100)                                    
DECLARE @TRANSIT_ROUTING_RULE CHAR                                    
DECLARE @DFI_ACC_NO_RULE CHAR                       
DECLARE @INSTALL_PLAN_ID INT                                     
--DECLARE @INTDOWN_PAY_MODE INT          
DECLARE @PLAN_PAYMENT_MODE INT           
          
DECLARE @Check Int,                    
 @EFT Int,                    
 @CreditCard Int                    
SET @CHECK = 11972                    
SET @EFT = 11973                    
SET @CreditCard = 11974                    
                    
SELECT  @INTDOWN_PAY_MODE   = ISNULL(APLIST.DOWN_PAY_MODE,0) , @PLAN_PAYMENT_MODE  = ISNULL(INST.PLAN_PAYMENT_MODE,0)                     
FROM APP_LIST APLIST  INNER JOIN ACT_INSTALL_PLAN_DETAIL INST                    
ON APLIST.INSTALL_PLAN_ID = INST.IDEN_PLAN_ID                                    
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID  AND APP_VERSION_ID=@APPVERSIONID             
                            
IF(@INTDOWN_PAY_MODE= @EFT OR @PLAN_PAYMENT_MODE = @EFT)                    
BEGIN                                      
  SELECT @DFI_ACC_NO=ISNULL(DFI_ACC_NO,0),@TRANSIT_ROUTING_NO=ISNULL(TRANSIT_ROUTING_NO,0)                                    
  FROM  ACT_APP_EFT_CUST_INFO                                     
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND   APP_VERSION_ID=@APPVERSIONID           
  IF(@DFI_ACC_NO is null or @TRANSIT_ROUTING_NO is null)             
   BEGIN                                    
   SET @DFI_ACC_NO_RULE='Y'                             
   END                              
  ELSE                                     
   BEGIN                                    
   SET @DFI_ACC_NO_RULE='N'                      
   END                               
END              
ELSE                                     
 BEGIN                      
 SET @DFI_ACC_NO_RULE='N'                                    
 END                       
          
/*Modified on 24 june 2008*/          
        
DECLARE @CARD_TYPE INT,          
 @CUSTOMER_FIRST_NAME CHAR,          
 @CUSTOMER_LAST_NAME CHAR,          
 @CREDIT_CARD CHAR,          
 @PAY_PAL_REF_ID varchar(200)          
IF(@INTDOWN_PAY_MODE= @CreditCard OR @PLAN_PAYMENT_MODE = @CreditCard)           
BEGIN          
 /*SELECT @CUSTOMER_FIRST_NAME=ISNULL(CUSTOMER_FIRST_NAME,''),          
 @CUSTOMER_LAST_NAME=ISNULL(CUSTOMER_LAST_NAME,''),@CARD_TYPE=ISNULL(CARD_TYPE,0)--,CARD_CVV_NUMBER,CARD_NO,CARD_DATE_VALID_TO                                  
 FROM  ACT_APP_CREDIT_CARD_DETAILS                                
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND   APP_VERSION_ID=@APPVERSIONID  */          
          
 SELECT @PAY_PAL_REF_ID = PAY_PAL_REF_ID           
 FROM  ACT_APP_CREDIT_CARD_DETAILS                                   
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND   APP_VERSION_ID=@APPVERSIONID             
          
 IF(@PAY_PAL_REF_ID is null)             
 BEGIN                                    
  SET @CREDIT_CARD='Y'                                    
 END                           
  ELSE                                     
 BEGIN              
  SET @CREDIT_CARD='N'                                    
 END                               
END              
ELSE                                     
 BEGIN                 
  SET @CREDIT_CARD='N'                                    
 END                       
                       
          
------REJECT THE APPLICATION IF THE CUSTOMER IS INACTIVE             
DECLARE @INACTIVE_APPLICATION CHAR             
IF EXISTS(SELECT CUSTOMER_ID  FROM CLT_CUSTOMER_LIST WITH(NOLOCK)                                               
          WHERE CUSTOMER_ID=@CUSTOMERID AND IS_ACTIVE='N' )            
   SET @INACTIVE_APPLICATION='Y'              
ELSE             
    SET @INACTIVE_APPLICATION='N'                                 
------                  
------REJECT THE APPLICATION IF THE Agency IS INACTIVE             
DECLARE @INACTIVE_AGENCY CHAR             
SELECT @APP_AGENCY_ID=APP_AGENCY_ID FROM APP_LIST WITH(NOLOCK)          
WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND   APP_VERSION_ID=@APPVERSIONID          
IF EXISTS(SELECT AGENCY_ID FROM  MNT_Agency_LIST WITH(NOLOCK) WHERE AGENCY_ID=@APP_AGENCY_ID AND IS_ACTIVE='N')          
  SET @INACTIVE_AGENCY='Y'              
ELSE             
     SET @INACTIVE_AGENCY='N'           
        
                
-----REFER IF EFFECTIVE DATE IS LESS THEN 2000 -------          
DECLARE @APPEFFECTIVEDATE VARCHAR          
SET @APPEFFECTIVEDATE ='N'          
          
IF(YEAR(@DATE_APP_EFFECTIVE_DATE) < 2000 )           
BEGIN          
 SET @APPEFFECTIVEDATE='Y'          
END            
          
------REJECT THE APPLICATION IF LOB IS AUTO AND NO Vehicle other than Itrack 4536          
--If Personal with a           
--(Vehicle Type) of Utility Trailer or Camper Van & Travel Trailer           
--If Commercial with a vehicle type of Trailer           
DECLARE @OTHER_VEHICLE CHAR,@OTHER_VEHICLE_COUNT INT          
IF(@APP_LOB=2)          
BEGIN          
 SELECT @OTHER_VEHICLE_COUNT=COUNT(CUSTOMER_ID) FROM APP_VEHICLES WITH(NOLOCK)          
 WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND   APP_VERSION_ID=@APPVERSIONID          
 AND ISNULL(VEHICLE_TYPE_PER,0) NOT IN (11337,11870)          
 AND ISNULL(VEHICLE_TYPE_COM,0) NOT IN (11341) AND ISNULL(IS_ACTIVE,'Y')='Y'         
 IF (@OTHER_VEHICLE_COUNT>0)          
   SET @OTHER_VEHICLE='N'              
 ELSE             
      SET @OTHER_VEHICLE='Y'           
               
END          
ELSE          
 SET @OTHER_VEHICLE='N'           
--=================================================================================    

 --Added by Charles on 11-Jan-10 for Itrack 6830  
 DECLARE @UNDERWRITING_TIER CHAR  
 SET @UNDERWRITING_TIER ='N'  
 --Added till here           
                                                  
SELECT                                                                                                 
 @STATE_ID AS STATE_ID,  -- IF BLANK CHK FOR -1                            
 @APP_LOB AS APP_LOB,                                                                                                            
 @APP_TERMS AS APP_TERMS,                                                                                                            
 @APP_EFFECTIVE_DATE AS APP_EFFECTIVE_DATE,                                                                                                       
 @APP_EXPIRATION_DATE AS APP_EXPIRATION_DATE,                                      
 @APP_AGENCY_ID AS APP_AGENCY_ID, -- IF BLANK CHK FOR -1                                   
 @BILL_TYPE AS BILL_TYPE,                                                                     
 @PROXY_SIGN_OBTAINED AS PROXY_SIGN_OBTAINED, -- IF BLANK CHK FOR -1                   
 @CHARGE_OFF_PRMIUM AS CHARGE_OFF_PRMIUM  ,                                
 -- CLIENT TOP INFO                                                                                                           
 @CUSTOMER_NAME AS CUSTOMER_NAME,                                                                                         
 @ADDRESS AS ADDRESS,                                                                
 @CUSTOMER_HOME_PHONE AS CUSTOMER_HOME_PHONE,                            
 @APP_VERSION_NO AS APP_VERSION_NO,                                                         
 @APP_NO AS APP_NO  ,                                                                                             
 @CUSTOMER_TYPE AS CUSTOMER_TYPE ,                                                                                                
 @IS_PRIMARY_APPLICANT  AS IS_PRIMARY_APPLICANT,                                                                                          
 @CUSTOMER_INSURANCE_SCORE AS CUSTOMER_INSURANCE_SCORE,                                                                                          
 -- RULE                                                                                           
 @MI_CUSTOMER_INSURANCE_SCORE AS MI_CUSTOMER_INSURANCE_SCORE,                                                                                
 @POLICY_TYPE AS POLICY_TYPE,                                                                              
 @CALLED_FROM AS CALLED_FROM ,                               
 @TRAILER_ASSOCIATED_BOAT AS TRAILER_ASSOCIATED_BOAT,                              
 --                 
 @PIC_OF_LOC AS PIC_OF_LOC,                                                     
 @PROPRTY_INSP_CREDIT AS PROPRTY_INSP_CREDIT,                                                    
 @HO_CLAIMS AS HO_CLAIMS,                                              
 @PRIOR_POLICY_INFO AS PRIOR_POLICY_INFO,                                              
 @AUTO_DRIVER_FAULT AS AUTO_DRIVER_FAULT,              
 @NO_AT_FAULT AS NO_AT_FAULT,                       
 @EFF_EXP_DATE AS EFF_EXP_DATE,                                              
 @CARRIER AS CARRIER,                                   
 @LESS_INSURANCE_SCORE AS LESS_INSURANCE_SCORE,                         
 @INELIGIBLE_DRIVER AS INELIGIBLE_DRIVER,                                
 @LOSS_AMT_EXCEED AS LOSS_AMT_EXCEED,               
 @MVR_VER AS MVR_VER,                                              
 @APPLICANT_OCCU AS APPLICANT_OCCU,                                        
 @DOWN_PAY_MENT  AS DOWN_PAY_MENT,                                      
 --@TRANSIT_ROUTING_RULE AS TRANSIT_ROUTING_RULE ,                                      
 @DFI_ACC_NO_RULE  AS DFI_ACC_NO_RULE,          
 @CREDIT_CARD AS CREDIT_CARD,            
 @INACTIVE_APPLICATION AS INACTIVE_APPLICATION ,          
 @INACTIVE_AGENCY     AS INACTIVE_AGENCY ,          
 @APPEFFECTIVEDATE AS APPEFFECTIVEDATE,          
 @PICH_OF_LOC AS PICH_OF_LOC,          
 @PROPERTY_INSP_CREDIT AS PROPERTY_INSP_CREDIT,          
 --@TRAILER_DEDUCTIBLE AS TRAILER_DEDUCTIBLE             
 @OTHER_VEHICLE as OTHER_VEHICLE,          
 @PRIOR_POLICY_INFO_EXPIRE AS PRIOR_POLICY_INFO_EXPIRE,    
 @WBSPO_LOSS AS WBSPO_LOSS,  --Added by Charles on 30-Nov-09 for Itrack 6647 
 @UNDERWRITING_TIER AS UNDERWRITING_TIER    --Added by Charles on 11-Jan-10 for Itrack 6830       
          
END 

GO

