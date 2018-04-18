/* ===========================================================================================================                                                                                  
Proc Name                : Dbo.Proc_GetPPARule_Pol                                                                                                                               
Created by               : Ashwani                                                                                                                                                
Date                     : 01 Mar. 2000                                                                                                                                
Purpose                  : To get the policy info                                                                                                                
Revison History          :                                                                                                                                                
Used In                  : Wolverine                                                      
Modified by     : Praveen kasana                                                
Modifed Date    : 20 June 2008                                                 
Purpose      : As ACT_POL_CREDIT_CARD_DETAILS Fileds are Deleted (Rule will be fired on PAY_PAL_REF_ID)                                                                                                                                             
                    
Modified by     : Praveen kasana                      
Modifed Date    : 07 Oct 2009                     
Purpose      : Irack 6485 --Rule IN HO -2 Repiar and HO - 3 Repair                     
                    
===========================================================================================================                                                                                  
Date     Review By          Comments                                                                                                                                                
==========================================================================================================                                                                     
drop proc dbo.Proc_GetPPARule_Pol 2156,48,1                        
select * from pol_customer_policy_list where policy_number like '%A1003131%'                     
*/                                                                                 
ALTER proc [dbo].[Proc_GetPPARule_Pol]                                                                                                                                       
(                                                                                                                                                
 @CUSTOMER_ID    int,                                                                                                                                                
 @POLICY_ID    int,                                                                                                                                                
 @POLICY_VERSION_ID   int                                                                                                        
)                                                                                                                                                
AS                                                                                                                                 
BEGIN                                                                                                           
  DECLARE @STATE_ID INT                                                                                                                 
  DECLARE @POLICY_LOB NVARCHAR(5)       
  DECLARE @APP_TERMS NVARCHAR(5)                          
  DECLARE @APP_EFFECTIVE_DATE VARCHAR(25)         
  DECLARE @DATE_APP_EFFECTIVE_DATE VARCHAR(25) --Added by Charles on 1-Dec-09 for Itrack 6647                                               
  DECLARE @APP_EXPIRATION_DATE VARCHAR(20)                                                             
  DECLARE @AGENCY_ID INT                                                 
  DECLARE @BILL_TYPE CHAR(2)                                                                                                       
  DECLARE @PROXY_SIGN_OBTAINED nVARCHAR(10)                                                                                              
  DECLARE @CHARGE_OFF_PRMIUM VARCHAR(5)                                        
  DECLARE @APP_VERSION_NO NVARCHAR(75) -- POLICY_VERSION_ID                                                    
  DECLARE @APP_NO NVARCHAR(75) -- POLICY_NUMBER                                                                            
  DECLARE @POLICY_TYPE INT                                                                               
  DECLARE @CALLED_FROM CHAR                                                   
  DECLARE @UNDERWRITER INT                                         
  DECLARE @POLICY_DISP_VERSION NVARCHAR(6)                                                     
  DECLARE @INTDOWN_PAY_MODE INT                                                       
  DECLARE @DOWN_PAY_MENT CHAR                                                   
  DECLARE @EXCCESS_CLAIM VARCHAR(20)                                                        
  DECLARE @POLICYEFFECTIVEDATE VARCHAR(20)                                                  
  DECLARE @APP_SUBLOB INT                                                 
  DECLARE @POLICY_STATUS NVARCHAR(12)                          
  DECLARE @CURRENT_TERM  INT                    
  DECLARE @TRAILBLAZER_EXPIRY_DATE DATETIME    
  SET @TRAILBLAZER_EXPIRY_DATE = '01/01/2010'   
  DECLARE @POLICY_CURRENCY  INT   
  DECLARE @CO_INSURANCE INT          
 --Added by Charles on 20-Nov-09 for Itrack 6592            
-- DECLARE @ISRENEWEDPOLICY CHAR            
-- DECLARE @PRIOR_LOSS CHAR            
-- SET @ISRENEWEDPOLICY = 'N'            
-- SET @PRIOR_LOSS = 'N'            
--                 
-- IF EXISTS(SELECT CUSTOMER_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID               
--    AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_ID = 5)            
-- BEGIN            
-- SET @ISRENEWEDPOLICY = 'Y'             
-- END              
--            
-- IF EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID)            
-- BEGIN            
-- SET @PRIOR_LOSS = 'Y'            
-- END            
 --Added till here                 
                                                                                                                                                  
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                             
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID )                                                                                                                                                          
  
             
  BEGIN                                                 
  SELECT @POLICYEFFECTIVEDATE = APP_EFFECTIVE_DATE, @POLICY_STATUS=POLICY_STATUS,@CURRENT_TERM = CURRENT_TERM,   
  @CALLED_FROM = CASE WHEN UPPER(POLICY_STATUS) = 'APPLICATION' THEN '0' ELSE '1' END,                                                  
   @STATE_ID=ISNULL(STATE_ID,-1),@POLICY_LOB=ISNULL(POLICY_LOB,''),@APP_TERMS=ISNULL(APP_TERMS,''),                                                                            
   @APP_EFFECTIVE_DATE= ISNULL(CONVERT(VARCHAR(25),APP_EFFECTIVE_DATE,101),''),                      
   @APP_EXPIRATION_DATE= ISNULL(CONVERT(VARCHAR(10),APP_EXPIRATION_DATE,101),''),         
   @AGENCY_ID=ISNULL(AGENCY_ID,-1),@BILL_TYPE=ISNULL(BILL_TYPE,''),                                                                                                  
   @PROXY_SIGN_OBTAINED=ISNULL(convert(varchar,PROXY_SIGN_OBTAINED),''),@CHARGE_OFF_PRMIUM=ISNULL(CHARGE_OFF_PRMIUM,''),                                                                                                        
   @POLICY_TYPE=ISNULL(POLICY_TYPE,-1), @APP_VERSION_NO=ISNULL(APP_VERSION,''),  
   @APP_NO=CASE WHEN UPPER(isnull(POLICY_STATUS,APP_STATUS)) = 'APPLICATION' THEN ISNULL(APP_NUMBER,'') ELSE ISNULL(POLICY_NUMBER,'') END,                                                  
   @UNDERWRITER=UNDERWRITER , @POLICY_DISP_VERSION = ISNULL(POLICY_DISP_VERSION,''),                                                  
   @INTDOWN_PAY_MODE=ISNULL(DOWN_PAY_MODE,0), @APP_SUBLOB=POLICY_SUBLOB,        
   @DATE_APP_EFFECTIVE_DATE=ISNULL(CONVERT(VARCHAR(20),APP_EFFECTIVE_DATE),''), --Added by Charles on 1-Dec-09 for Itrack 6647                                                                                                        
   @POLICY_CURRENCY = ISNULL(POLICY_CURRENCY,-1), @CO_INSURANCE = ISNULL(CO_INSURANCE,-1)  
 FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                                                                             
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                            
 end                                                                                                            
  ELSE                                                                                              
  BEGIN                                                                                                                                 
    SET @STATE_ID=-1                                                                                                      
    SET @POLICY_LOB=''                                                                                          
    SET @APP_TERMS=''                                                                                                               
    SET @APP_EFFECTIVE_DATE=''                                                                                                                
    SET @APP_EXPIRATION_DATE=''                                                                         
    SET @AGENCY_ID=-1                                                                                                                
    SET @BILL_TYPE =''                                                                                       
    SET @PROXY_SIGN_OBTAINED='-1'                                                                                                           
    SET @CHARGE_OFF_PRMIUM=''                                                              
    SET @APP_VERSION_NO=''                                                                                                        
    SET @APP_NO=''                                                                                   
    SET @POLICY_TYPE=-1                                                   
    --SET @UNDERWRITER= -1                                                                    
  END                                                                  
                 
  IF @CO_INSURANCE = 0  
  SET @CO_INSURANCE =-1                                                                                           
 --                                                                                                           
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
 declare @CUSTOMER_NAME nvarchar(225)                                                                                             
 declare @ADDRESS nvarchar(225)                                                                    
 declare @CUSTOMER_HOME_PHONE nvarchar(75)                                                                                                             
 declare @CUSTOMER_TYPE nvarchar(75)                                                  
                                                             
    select @CUSTOMER_NAME= isnull(CLT.CUSTOMER_FIRST_NAME,'')+ '  ' +isnull(CLT.CUSTOMER_MIDDLE_NAME,'') +                                                                                                                          
  '  ' + isnull(CLT.CUSTOMER_LAST_NAME,''),                                                                    
                                                                 
     @ADDRESS=  isnull(CLT.CUSTOMER_ADDRESS1,'')+ ', ' +                                                                                      
  isnull(CLT.CUSTOMER_CITY,'')+ ', ' +                      
  isnull(STA.STATE_NAME,'')+ ', '+                                                                                       
  isnull(CON.COUNTRY_NAME,'')+ ', '+                                                                  
  isnull(CLT.CUSTOMER_ZIP,''),                                                                                      
                                                                                        
 @CUSTOMER_HOME_PHONE=isnull(CUSTOMER_HOME_PHONE,'') ,                                                         
 @CUSTOMER_TYPE=ISNULL(CUSTOMER_TYPE_LOOKUP.LOOKUP_VALUE_DESC,'')                                                                                   
 from CLT_CUSTOMER_LIST CLT with(nolock)                                    
 left outer join MNT_COUNTRY_LIST CON  on CLT.CUSTOMER_COUNTRY = CON.COUNTRY_ID                                                                                                              
 left outer join MNT_COUNTRY_STATE_LIST STA  on CLT.CUSTOMER_STATE = STA.STATE_ID                                                                       
 left join MNT_LOOKUP_VALUES CUSTOMER_TYPE_LOOKUP on CUSTOMER_TYPE_LOOKUP.LOOKUP_UNIQUE_ID = CLT.CUSTOMER_TYPE                                     
 where CUSTOMER_ID=@CUSTOMER_ID                                                                                                              
                                                 
 --                                                                                                         
 if(@CUSTOMER_HOME_PHONE='' or @CUSTOMER_HOME_PHONE is null)                                         
 begin                                                                                                         
 set @CUSTOMER_HOME_PHONE='NA'                                                                                                        
 end                                                                 
/*At Policy Level,if Underwriter is not assigned then                                                   
it does not allow to commit any processes.*/                                                  
  DECLARE @POL_UNDERWRITER   CHAR(2)                                                                             
  IF(@UNDERWRITER=0 OR @UNDERWRITER IS NULL) AND @CALLED_FROM = 1                                                  
  SET @POL_UNDERWRITER='-1'                                          
  ELSE SET @POL_UNDERWRITER = '0'           
                                                                     
-- Primary Applicant                                                              
  declare @IS_PRIMARY_APPLICANT char                                                                                        
  declare @intCount int                                                   
                                                                                            
if exists ( select CUSTOMER_ID   from POL_APPLICANT_LIST   with(nolock)                                                                                                  
where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID    )                                                                                                    
   begin                                                                                                     
  select @intCount=count(PAL.IS_PRIMARY_APPLICANT )                                                      
  from POL_APPLICANT_LIST PAL with(nolock)                                                 
  INNER  JOIN CLT_APPLICANT_LIST CLT   ON CLT.CUSTOMER_ID=PAL.CUSTOMER_ID AND CLT.APPLICANT_ID=PAL.APPLICANT_ID                                                                            
  where PAL.CUSTOMER_ID=@CUSTOMER_ID and PAL.POLICY_ID=@POLICY_ID and PAL.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
  and  PAL.IS_PRIMARY_APPLICANT=1  AND CLT.IS_ACTIVE='Y'                                                            
                                                                      
   if(@intCount =0)                                                                 
   begin                                                   
    set @IS_PRIMARY_APPLICANT='' -- plz select PA                                                        
   end                                                                                                    
   else                                                                                                    
   begin                                                                                                     
   set @IS_PRIMARY_APPLICANT='N'                                                                                                    
   end                                                                   
   end                                                                        
  else                                                   
  begin                                                            
 set @IS_PRIMARY_APPLICANT=''                                                               
  end                                                  
  -- Insurance Score for rental Dwelling Only                                                                   
DECLARE @NUM_CUSTOMER_INSURANCE_SCORE NUMERIC                                                                   
SELECT @NUM_CUSTOMER_INSURANCE_SCORE=ISNULL(CUSTOMER_INSURANCE_SCORE,-1) FROM CLT_CUSTOMER_LIST WITH(NOLOCK)                              
WHERE CUSTOMER_ID=@CUSTOMER_ID AND IS_ACTIVE='Y'                                                 
IF(@POLICY_LOB=6)-- RD                                                                                 
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
  DECLARE @MI_CUSTOMER_INSURANCE_SCORE CHAR                                                                       
 IF(@POLICY_LOB=6  AND @STATE_ID=22 )                                                                                   
 BEGIN                                                                                               
   IF(@NUM_CUSTOMER_INSURANCE_SCORE<550 AND @NUM_CUSTOMER_INSURANCE_SCORE >=0)                                                                                              
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
 if(@POLICY_TYPE= 0 or @POLICY_TYPE= -1 or upper(@POLICY_TYPE)=NULL  )                                                                                    
 begin                                                                     
  set @POLICY_TYPE=-1                                                                                    
 end                                                                                     
--------------------------------------------------------                                                  
-- Called from Policy=1 or Application =0                                                    
 --set @CALLED_FROM='1'                                                                              
----------------------------------------------------------------------                                                             
-- App can not be submitted, if trailer is not attached to a boat               
 declare @INTCOUNTNULL int                                      
 declare @TRAILER_ASSOCIATED_BOAT char                                                                          
                                                                     
 if exists(select CUSTOMER_ID from  POL_WATERCRAFT_TRAILER_INFO with(nolock)            
  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID)                                                                           
 begin                                                     
                                                                           
 select @INTCOUNTNULL=count(ASSOCIATED_BOAT) from  POL_WATERCRAFT_TRAILER_INFO with(nolock)                                                                         
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and ASSOCIATED_BOAT=0                                                             
 end                                                         
 else                                                           
 begin                                                      
 set @TRAILER_ASSOCIATED_BOAT='N'                                                                          
 end                       
 if(@INTCOUNTNULL > 0)                                                                         
 begin                                                                             
 set @TRAILER_ASSOCIATED_BOAT='Y'                                                                        
 end                                                                       
 else                                                                            
 begin                                        
 set @TRAILER_ASSOCIATED_BOAT='N'                                                                            
 end                                                                             
                                                                          
 if(@TRAILER_ASSOCIATED_BOAT='Y')                                                     
 begin                                                                           
 set @TRAILER_ASSOCIATED_BOAT=''                                                                          
 end                                                                           
----------------------------------------------------------------------                                                                      
--If Picture of Location Attached is  No Refer :13 June 2006 for HOME                                                         
--If Property Inspection/Cost Estimator is Attached is No - Refer                                                                                   
                                                                                                
Declare @INTPIC_OF_LOC int                                                                                                    
declare @PIC_OF_LOC char                                         
declare @INTPROPRTY_INSP_CREDIT  int                                   
declare @PROPRTY_INSP_CREDIT char                                                                     
declare @INTDOWN_PAY_MENT int                                                                    
--declare @DOWN_PAY_MENT   char                                                                                                                   
                                                                            
IF(@POLICY_LOB=1)-- HOME                              
BEGIN                                                                               
  SELECT @INTPIC_OF_LOC=ISNULL(PIC_OF_LOC,0),                                                
  @INTPROPRTY_INSP_CREDIT=ISNULL(PROPRTY_INSP_CREDIT,0) FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                         
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                    
                                                   
/* 11405 HO-4 Tenants                                                  
   11406 HO-6 Unit Owners                                                    
   11195 HO-4 Tenants                     
   11196 HO-6 Unit Owners                                                   
   11193 HO-2 Repair                                                  
   11194 HO-3 Repair                                                  
   11403 HO-2 Repair                                      
   11404 HO-3 Repair*/                                                             
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
                                   
      IF(@POLICY_TYPE !=11405 and  @POLICY_TYPE!=11406 )                                                              
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
     IF(@POLICY_TYPE !=11405 and @POLICY_TYPE!=11406 )                                                                  
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
--======================= Start Itrack No. 1579 and 3618 =====================================                                                
/*                                                
   11480 DP-2 Repair                                                
11479 DP-2 Replacement                                                
   11482 DP-3 Repair                       
   11481 DP-3 Replacement                                                
                                 
  Rental Michigan :                                                 
 11289 : DP-2 Replacement                                                
 11458 : DP-3 Premier                                                 
 11291 : DP-3 Replacement                                                
                                                
 Rental Indiana  :                                                 
 11479 : DP-2 Replacement                                                 
 11481 : DP-3 Replacement                                                
*/                                                
DECLARE @PICH_OF_LOC CHAR                                                                           
DECLARE @PROPERTY_INSP_CREDIT CHAR                                           
IF(@POLICY_LOB=6)-- Rental                                                    
BEGIN                                            
   SELECT @INTPIC_OF_LOC=ISNULL(PIC_OF_LOC,0),                                                                   
   @INTPROPRTY_INSP_CREDIT=ISNULL(PROPRTY_INSP_CREDIT,0) FROM  POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                                 
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                    
                                                 
                                                 
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
--End Itrack No. 1579                                                
                                             
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
/*11400,11401,11402,11405,11148,11149,11192,11409,11410,11194 */                                                                                   
                                                                          
/*if(@POLICY_TYPE=11400 or @POLICY_TYPE=11401 or @POLICY_TYPE=11402                                  
  or @POLICY_TYPE=11148 or @POLICY_TYPE=11149 or                                                                                 
   @POLICY_TYPE=11192 or @POLICY_TYPE=11409 or @POLICY_TYPE=11410 or @POLICY_TYPE=11405)                                                          
begin                                                        
 if(@INTPIC_OF_LOC=0)                                                                                      
 begin                                          
 set @PIC_OF_LOC = ''                                                                                      
 end                                                              
end */                                                                    
/*ELSE                                                                   
 BEGIN                                                        
 SET @PIC_OF_LOC='N'                                                          
 END */                                                                         
                                                                            
                                                       
/*--PROPRTY_INSP_CREDIT, it should be checked for all home types and blank should be considered as NO                                                                          
IF(@INTPROPRTY_INSP_CREDIT=0 )                                                              
BEGIN                                                               
 IF(@POLICY_TYPE != 11195 AND @POLICY_TYPE != 11196  )           
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
   END */                                                             
 /*                                                                 
ELSE                                                                            
BEGIN                                             
SET @PROPRTY_INSP_CREDIT='N'                                                              
END */                                    
                                       
-------------                                                                        
-----------------------------------------------------------------------                                                                            
/* Losses Tab At renewal if there have been any losses during the past term then                                                                         
   Refer to underwriters before renewing  */                                                                        
                                                                        
declare @PROCESS_ID int                                        
                                                                        
select @PROCESS_ID=PROCESS_ID from POL_POLICY_PROCESS with(nolock)                                                           
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                              
                                                                        
declare @RENW_LOSS char                
  set @RENW_LOSS='N'                                                              
if(@PROCESS_ID=5)                                                                        
begin                                                                         
                  
 if exists (select LOSS_TYPE from FETCH_ACCIDENT with(nolock) where CUSTOMER_ID=@CUSTOMER_ID and                                        
  POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and LOB=2)                                                                        
 begin                                                                         
 set @RENW_LOSS='Y'                                           
 end                                                           
end                                           
                              
/* Loss Tab Check for renewal Applies to Coverage Field             
   Count the number of Comprehensive Losses (where Loss type is comprehensive) in the lister within the last 5 years                                                                      
   based on the effective Date of the policy - Application/Policy Details - Effective Date                                                                         
   If the number is greater than 5                                                                         
   Refer to Underwriters  */                                                                        
                                                                        
declare @intLOSSCOUNT int                                                          declare @COMP_LOSSES char                                                              
set @COMP_LOSSES='N'                                     
                                                                        
-- Comprehensive --@APP_EFFECTIVE_DATE                                                                         
select @intLOSSCOUNT =isnull(LOSS_TYPE,0) from APP_PRIOR_LOSS_INFO with(nolock)                                                           
 where CUSTOMER_ID=@CUSTOMER_ID and LOB=2 and LOSS_TYPE='9765'                                                                        
  and ((isnull(year(@APP_EFFECTIVE_DATE),0) - isnull(year(isnull(OCCURENCE_DATE,0)),0))<=5)                                                                         
                                     
if(@PROCESS_ID=5 and @intLOSSCOUNT>5)                                                                        
begin                                                                         
 set @COMP_LOSSES='Y'                                               
end                                                 
-------------------------------------------                                                                                    
-- If there is a loss for Automobile LOB with Driver Field Empty At Fault is Blank                                                 
-- Refer to underwriters                                                                                     
                                                                
DECLARE @DRIVER_ID VARCHAR(15)                                                             
DECLARE @AT_FAULT VARCHAR(15)                                                                                    
DECLARE @AUTO_DRIVER_FAULT CHAR                               
                                                                        
SELECT @DRIVER_ID=ISNULL(CONVERT(VARCHAR(15),DRIVER_ID),''),@AT_FAULT = ISNULL(CONVERT(VARCHAR(15),AT_FAULT),'')                                                                               
 FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND  LOB=2 --AND (DRIVER_ID IS NULL OR DRIVER_ID =0)                                                
                                                                                    
IF(@DRIVER_ID='' OR @DRIVER_ID ='0' OR @AT_FAULT='' OR  @AT_FAULT='0')                                                                                    
BEGIN                                                                            
 SET @AUTO_DRIVER_FAULT='Y'                                                
END                            
ELSE                     
BEGIN                                                                     
 SET @AUTO_DRIVER_FAULT='N'                          
END                                                               
                                                                                                          
------------------------------------------------                                                  
------------------------------------------------ added    by Pravesh on 26 june 2008                                              
/*Application/Policy Details  If State is Michigan Loss Tab Check for New Business and at renewal                                         
  At Fault Field Count the number of At Fault Fields that have No within the last 5 years                
  based on the effective Date of the policy - Application/Policy Details - Effective Date                         
  If greater than 5  Refer to Underwriters */                                                                                    
  --modified by Pravesh on 26 june as per Itrack 4392, combine comprehensive and not fault /  9765-- Comprehensive    loss type                                                                            
DECLARE @NO_AT_FAULT CHAR                                                                                    
DECLARE @INTAT_FAULT INT ,@CLAIM_COUNT_POL int                                                
 SET @NO_AT_FAULT='N'                                                                                    
                                                                   
IF(@STATE_ID=22) --MI                                                                                    
BEGIN                                                              
-- SELECT @INTAT_FAULT=COUNT(CUSTOMER_ID)                                                                   
-- FROM FETCH_ACCIDENT                                                            
-- WHERE CUSTOMER_ID = @CUSTOMER_ID AND (POLICY_ID IS NULL) AND (POLICY_VERSION_ID IS NULL) AND LOB=2                                                                                     
-- AND (AT_FAULT='10964'OR  LOSS_TYPE='9765'  )                                                
-- AND  ((ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=5                                                                                     
-- AND (ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0)                                                    
                                                
SELECT @INTAT_FAULT=COUNT(CUSTOMER_ID)                                        
 FROM APP_PRIOR_LOSS_INFO  WITH (NOLOCK)                                                                      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND LOB=2                                                                                     
 AND (AT_FAULT='10964'OR  LOSS_TYPE='9765'  )                                                
 AND  ((ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=5                                                                                     
 AND (ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0)            
                                                                                     
  IF(@INTAT_FAULT>5)                                                                                      
  SET @NO_AT_FAULT='Y'                                                 
                         
 SELECT @CLAIM_COUNT_POL=COUNT(CUSTOMER_ID) FROM CLM_CLAIM_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID and LOB_ID=2                           
  --AND dbo.instring(replace(PINK_SLIP_TYPE_LIST,',',' '),'13005')>0   --Commented by Charles on 9-Dec-09 for Itrack 6620        
  AND ISNULL(AT_FAULT_INDICATOR,0) = 2  --Added by Charles on 9-Dec-09 for Itrack 6620                                   
  AND  ((ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(LOSS_DATE,0)),0))<=5                
  AND (ISNULL(YEAR(@APP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(LOSS_DATE,0)),0))>=0)                                                
 IF(@INTAT_FAULT+@CLAIM_COUNT_POL > 5)                                                                                      
  SET @NO_AT_FAULT='Y'                                             
                                                                                  
END                                                   
------------------------------------------------                                                  
                              
-- Umbrella                                                   
-- Customer  Details Applicants Occupation If occupation is any of the following refer to underwriters                                                  
--Actor/Actress ,Athletes ,Author/Writer ,Celebrity ,Government Appointee - State ,Government Appointee - Federal ,                                                                                      
--Newspaper Editor/Columnist ,Entertainer ,Labor Leaders other than Shop Stewards,Newspaper Publisher ,Public Lecturer ,                                                              
--Public Office Holder ,Radio & Television Announcer                                                                                       
                                                  
DECLARE @APPLICANT_OCCU VARCHAR(20)                                                                                      
 SET @APPLICANT_OCCU='N'                                                                          
 IF(@POLICY_LOB=5)-- UMBRELLA                                   
 BEGIN                                                                
   IF EXISTS(SELECT APPLICANT_OCCU = ISNULL(CONVERT(VARCHAR(20),APPLICANT_OCCU),'')                                                                                      
      FROM CLT_CUSTOMER_LIST                                                                                       
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND IS_ACTIVE='Y' AND APPLICANT_OCCU IN (250,275,280,11817,11825,561,432,11819,11820,11822,11823,602))                                                                     
                 
     BEGIN                                                                           
     SET @APPLICANT_OCCU='Y'                                                                                      
     END                                                                         
  END                                                     
                                                
                                                                              
--IF PLAN PAYMENT MODE FOR THE PLAN OPTED BY USER is "EFT" OR THE DOWN PAYMENT MODE IS "EFT" THAN USER WILL NOT BE                                             
--ALLOWED TO SUMIT THE APPLICATION IF THE EFT RELATED INFORMATION IS NOT PROVIDED AT BILLING-INFO TAB.                                                                         
--IF THIS RULES IS VOILATED IT SHOULD BE TREATED AS REFFERED CASE                                                                         
                                               
DECLARE @TRANSIT_ROUTING_NO   NVARCHAR(100)                                                                         
DECLARE @DFI_ACC_NO   NVARCHAR(100)                                                                        
DECLARE @TRANSIT_ROUTING_RULE CHAR                                                                        
DECLARE @DFI_ACC_NO_RULE CHAR        
DECLARE @INSTALL_PLAN_ID INT                                                                         
--DECLARE @INTDOWN_PAY_MODE INT                                                         
DECLARE @PLAN_PAYMENT_MODE Int                                                          
                                                        
DECLARE @Check Int,                                 
 @EFT Int,                                     
 @CreditCard Int                                                        
SET @CHECK = 11972                                  
SET @EFT   = 11973                                               
SET @CreditCArd = 11974                                                      
                                                        
SELECT  @INTDOWN_PAY_MODE   = ISNULL(APLIST.DOWN_PAY_MODE,0) ,@PLAN_PAYMENT_MODE  = ISNULL(INST.PLAN_PAYMENT_MODE,0)                                                         
FROM POL_CUSTOMER_POLICY_LIST APLIST with(nolock)                                                        
INNER JOIN ACT_INSTALL_PLAN_DETAIL INST                                                        
ON APLIST.INSTALL_PLAN_ID = INST.IDEN_PLAN_ID                                                                        
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                    
                                                                
IF(@INTDOWN_PAY_MODE= @EFT OR @PLAN_PAYMENT_MODE = @EFT)                                                        
BEGIN                                     
  SELECT @DFI_ACC_NO=ISNULL(DFI_ACC_NO,'0'),@TRANSIT_ROUTING_NO=ISNULL(TRANSIT_ROUTING_NO,'0')                                                                        
  FROM  ACT_POL_EFT_CUST_INFO   with(nolock)                               
  WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                               
 IF(@DFI_ACC_NO IS NULL or @TRANSIT_ROUTING_NO IS NULL)                                                                        
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
/* Modified on 23 June 2008 */                                                
DECLARE                                                 
 @CARD_TYPE INT,                                                  
 @CUSTOMER_FIRST_NAME CHAR,                                                  
 @CUSTOMER_LAST_NAME CHAR,                                                  
 @CREDIT_CARD CHAR  ,    
 @PAY_PAL_REF_ID VARCHAR(200)                                                
          
IF(@INTDOWN_PAY_MODE= @CreditCard OR @PLAN_PAYMENT_MODE = @CreditCard)                                                   
BEGIN                                                  
                                                
/* SELECT @CUSTOMER_FIRST_NAME=ISNULL(CUSTOMER_FIRST_NAME,''),                                                 
 @CUSTOMER_LAST_NAME=ISNULL(CUSTOMER_LAST_NAME,''),@CARD_TYPE=ISNULL(CARD_TYPE,0)--,CARD_CVV_NUMBER,CARD_NO,CARD_DATE_VALID_TO                                                    
 FROM  ACT_POL_CREDIT_CARD_DETAILS   WITH(NOLOCK)                                                                    
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID   */                                                
                                                 
 SELECT @PAY_PAL_REF_ID = PAY_PAL_REF_ID                                                 
 FROM  ACT_POL_CREDIT_CARD_DETAILS   WITH(NOLOCK)                                                                          
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                           
                                                
                                                 
                                                
 -- IF(@CARD_TYPE is null or @CUSTOMER_FIRST_NAME is null or @CUSTOMER_LAST_NAME is null)                                                     
  IF(@PAY_PAL_REF_ID IS NULL or @PAY_PAL_REF_ID='' or @PAY_PAL_REF_ID=' ')                                                
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
IF EXISTS(SELECT CUSTOMER_ID  FROM CLT_CUSTOMER_LIST  with(nolock)                                                                                                        
          WHERE CUSTOMER_ID=@CUSTOMER_ID AND IS_ACTIVE='N' )                                                      
   SET @INACTIVE_APPLICATION='Y'                                                        
ELSE                                                     
    SET @INACTIVE_APPLICATION='N'                                                    
                                                  
------REJECT THE APPLICATION IF THE Agency IS INACTIVE                                                     
DECLARE @INACTIVE_AGENCY CHAR                                                   
DECLARE @APP_AGENCY_ID INT                                                    
SELECT @APP_AGENCY_ID=AGENCY_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                  
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                   
IF EXISTS(SELECT AGENCY_ID FROM  MNT_Agency_LIST WITH(NOLOCK) WHERE AGENCY_ID=@APP_AGENCY_ID AND IS_ACTIVE='N')                                                  
  SET @INACTIVE_AGENCY='Y'                                                      
ELSE                                                     
     SET @INACTIVE_AGENCY='N'                                                   
--========================================================================                                                    
----------"Any HO claims in the last 3 years , refer to U/w : 9 june 2006                                        
--========================================================================                                                      
/*                  
Itrack 6616                  
System will consider both Prior Losses and HO claims in the last 3 years.                  
If any of the above two exists then refer to U/w                  
*/                  
DECLARE @OCCURENCE_DATE INT                    
DECLARE @HO_CLAIMS CHAR                 
--Added by Charles on 17-Nov-09 for Itrack 6616                
DECLARE @APP_EFFECTIVE_DATE_3_BEFORE DATETIME                
SET @HO_CLAIMS ='N'          
SET @APP_EFFECTIVE_DATE_3_BEFORE=DATEADD(YY,-3,@APP_EFFECTIVE_DATE)                                                                                                   
--Added till here                
        
--Added by Charles on 1-Dec-09 for Itrack 6647          
  DECLARE @WBSPO_LOSS CHAR          
  SET @WBSPO_LOSS = 'N'                                                                          
--Added till here                                                                  
                                                                                        
IF(@POLICY_LOB=1)-- HOME                                                                                               
BEGIN                                
                                                                          
 --Added by Charles on 26-Nov-09 for Itrack 6616                                                     
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB = 1           
 AND (CAST(CONVERT(VARCHAR(25),ISNULL(OCCURENCE_DATE,''),101) AS DATETIME) >=@APP_EFFECTIVE_DATE_3_BEFORE           
 AND CAST(CONVERT(VARCHAR(25),ISNULL(OCCURENCE_DATE,''),101) AS DATETIME)<=@DATE_APP_EFFECTIVE_DATE))        
    OR        
 EXISTS(SELECT CUSTOMER_ID FROM CLM_CLAIM_INFO WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID           
 AND (CAST(CONVERT(VARCHAR(25),ISNULL(LOSS_DATE,''),101) AS DATETIME) >=@APP_EFFECTIVE_DATE_3_BEFORE           
 AND CAST(CONVERT(VARCHAR(25),ISNULL(LOSS_DATE,''),101) AS DATETIME)<=@DATE_APP_EFFECTIVE_DATE))              
 BEGIN           
  SET @HO_CLAIMS ='Y'          
 END          
 --Added till here                                                                                                                                                             
        
 --Added by Charles on 1-Dec-09 for Itrack 6647        
     IF EXISTS(SELECT PRIOR_LOSS_ID FROM PRIOR_LOSS_HOME WITH(NOLOCK) WHERE ISNULL(WATERBACKUP_SUMPPUMP_LOSS,10964)=10963        
     AND CUSTOMER_ID=@CUSTOMER_ID AND LOSS_ID IN (SELECT LOSS_ID FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB = 1           
  AND (CAST(CONVERT(VARCHAR(25),ISNULL(OCCURENCE_DATE,''),101) AS DATETIME) >=@APP_EFFECTIVE_DATE_3_BEFORE           
  AND CAST(CONVERT(VARCHAR(25),ISNULL(OCCURENCE_DATE,''),101) AS DATETIME)<=@DATE_APP_EFFECTIVE_DATE)))          
  OR         
  EXISTS(SELECT OCCURRENCE_DETAIL_ID FROM CLM_OCCURRENCE_DETAIL WITH(NOLOCK) WHERE ISNULL(WATERBACKUP_SUMPPUMP_LOSS,10964)=10963 AND CLAIM_ID IN        
     (SELECT CLAIM_ID FROM CLM_CLAIM_INFO WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID           
  AND (CAST(CONVERT(VARCHAR(25),ISNULL(LOSS_DATE,''),101) AS DATETIME) >=@APP_EFFECTIVE_DATE_3_BEFORE           
  AND CAST(CONVERT(VARCHAR(25),ISNULL(LOSS_DATE,''),101) AS DATETIME)<=@DATE_APP_EFFECTIVE_DATE)))           
  BEGIN         
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_DWELLING_SECTION_COVERAGES WITH(NOLOCK)         
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND COVERAGE_CODE_ID IN(197,198)        
        AND DWELLING_ID IN (SELECT DWELLING_ID FROM POL_DWELLINGS_INFO WITH(NOLOCK)         
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(IS_ACTIVE,'N')='Y'))                                                                  
  BEGIN        
   SET @WBSPO_LOSS='Y'             
  END        
  END        
 --Added till here        
        
END                                                                                                     
-------------                       
-- Application Details , If State is Indiana                                 
-- If no details entered on the Prior Coverage Tab , Refer to underwriters                                                       
                                                                
DECLARE @PRIOR_POLICY_INFO CHAR  ,@PRIOR_POLICY_INFO_EXPIRE CHAR                                                                                    
SET  @PRIOR_POLICY_INFO='N'                                                                   
SET  @PRIOR_POLICY_INFO_EXPIRE='N'                                                 
                                                                            
IF(1=1) --(@STATE_ID = 14)  now applicable to both state as discussed with Rajan                                                
BEGIN                                               
 IF NOT EXISTS (SELECT CUSTOMER_ID FROM  APP_PRIOR_CARRIER_INFO                                                                       
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB=2)                                                               
     BEGIN                                                
    IF (@POLICY_STATUS='SUSPENDED' OR @PROCESS_ID=24)   -- CONDITION ADDED AS PER iTRACK 5200 REFER ONLY IF NBS                                                
  SET @PRIOR_POLICY_INFO='Y'                                                
  END                                                
  ELSE                                                
 BEGIN                             
  --ADDED BY PRAVESH ON 5 AUG 2008 AS PER MAIL BY RAJAN                                                
  --If there is/are AUTO polices in Prior Policy section,                                                 
  --and none of them has expiration date => Eff date of NBS App, then refer.                                                 
  --(In other words, all prior policy are expiring before NBS effective, means there is a lapse in coverage, then refer)             
   IF NOT EXISTS(SELECT CUSTOMER_ID FROM  APP_PRIOR_CARRIER_INFO                                                                          
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB=2                                                 
      AND DATEDIFF(DAY,CAST(@APP_EFFECTIVE_DATE AS DATETIME),EXP_DATE)>=0                                  
      ) --AND @CURRENT_TERM = 1 --Added by Charles on 2-Sep-09 for Itrack 6316              
    --AND (@ISRENEWEDPOLICY = 'N' OR @PRIOR_LOSS = 'Y') -- @ISRENEWEDPOLICY,@PRIOR_LOSS condition added by Charles on 20-Nov-09 for Itrack 6592                                                                          
    SET @PRIOR_POLICY_INFO_EXPIRE='Y'                              
 END                                                          
END                                                                                      
-------------------------------------------                                                                                  
                                                                                             
-----   Rental Paid Claims                                    
IF(@POLICY_LOB=6 or @POLICY_LOB=4)-- RENTAL  /water                                                                                                                                 
BEGIN                                                                                                               
 SELECT @OCCURENCE_DATE = DATEDIFF(DAY,OCCURENCE_DATE,@APP_EFFECTIVE_DATE)                                                                                                          
 FROM  APP_PRIOR_LOSS_INFO  WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB =@POLICY_LOB--RENTAL                                                                                                     
               
  IF(@OCCURENCE_DATE<=1095)                                                                                       
    BEGIN                                                                                                            
    SET @HO_CLAIMS ='Y'                                                    
    END                                                        
  ELSE                                                     
    BEGIN                                          
    SET @HO_CLAIMS ='N'                                                                   
    END                                                 
END                                                   
                                                
                                                
-----IF EFFECTIVE DATE IS LESS THEN 2000 REFER To Underwriter-------                                                  
DECLARE @APPEFFECTIVEDATE VARCHAR                                                  
SET @APPEFFECTIVEDATE ='N'                                   
                                                  
IF(YEAR(@APP_EFFECTIVE_DATE) < 2000 )                                                   
BEGIN                                                  
 SET @APPEFFECTIVEDATE='Y'                                                  
END                                                                
--============ START Itrack No. 3048 ==========================================================                                       
                                                 
DECLARE @TOTAL_PREMIUM_AT_RENEWAL CHAR                                                  
IF(@BILL_TYPE = 'AB')                                                  
BEGIN                                                   
 SET @TOTAL_PREMIUM_AT_RENEWAL ='N'                                                  
END                                                  
ELSE                                                  
BEGIN                                                   
 ----IF TOTAL PAID IS LESS THAN 10 PERCENTAGE OF TOTAL PREMIUM REFER TO UNDERWRITER                                                  
 DECLARE @TOTAL_PREMIUM Decimal(18,2) ,                                                  
  @TOTAL_PAID    Decimal(18,2) ,                                                              
  @INSTALL_PLANID Int,                                              
  @PAST_DUE_RENEW Int,                                    
  @BASE_POLICY_VERSION_ID INT,                                           
  @NEW_POLICY_VERSION_ID INT,                    
  @TOTAL_DISCOUNT Decimal(18,2)                                                 
                                                   
 SELECT @BASE_POLICY_VERSION_ID = MAX(POLICY_VERSION_ID),                                                  
        @NEW_POLICY_VERSION_ID  = MAX(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS WITH(NOLOCK)                                                  
        WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID IN(5,18)                                                  
                                                  
 --Commented here and extracted while extracting other POL_CUSTOMER_POLICY_LIST details,by Charles on 2-Sep-09 for Itrack 6316                                                 
 --SELECT @CURRENT_TERM = CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                  
 --WHERE CUSTOMER_ID = @CUSTOMER_ID  AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID =@POLICY_VERSION_ID --= @BASE_POLICY_VERSION_ID                                                   
                                      
 --LAst version of prior term                                                
SELECT @INSTALL_PLANID=INSTALL_PLAN_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                    
        WHERE CUSTOMER_ID = @CUSTOMER_ID  AND POLICY_ID = @POLICY_ID AND                                       
  POLICY_VERSION_ID in (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                  
  WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID = @POLICY_ID AND CURRENT_TERM = @CURRENT_TERM - 1 )                                                             
                            
                                        
 SELECT @TOTAL_PAID = SUM(ISNULL(TOTAL_PAID,0)) FROM ACT_CUSTOMER_OPEN_ITEMS WITH(NOLOCK)                                                  
  WHERE ITEM_TRAN_CODE_TYPE = 'PREM'  AND CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID = @POLICY_ID                                       
  AND POLICY_VERSION_ID IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                  
  WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID = @POLICY_ID                                       
  AND CURRENT_TERM =  @CURRENT_TERM -1 )                                        
                                         
 SELECT @TOTAL_PREMIUM = SUM(ISNULL(GROSS_PREMIUM,0))  --SELECT @TOTAL_PREMIUM = SUM(GROSS_PREMIUM)                                                     
  FROM ACT_PREMIUM_PROCESS_SUB_DETAILS WITH(NOLOCK)                                                  
  WHERE CUSTOMER_ID = @CUSTOMER_ID   AND POLICY_ID = @POLICY_ID AND                                           
  POLICY_VERSION_ID IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID = @POLICY_ID              
  AND CURRENT_TERM = @CURRENT_TERM -1 AND BILL_TYPE <> 'AB')                      
                     
 SELECT @TOTAL_DISCOUNT = SUM(ISNULL(TOTAL_DUE,0)) * -1 FROM ACT_CUSTOMER_OPEN_ITEMS WITH(NOLOCK)                                            
 WHERE ITEM_TRAN_CODE = 'DISC'  AND CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID = @POLICY_ID                                       
  AND POLICY_VERSION_ID IN (SELECT POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)                                                  
 WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID = @POLICY_ID                                       
  AND CURRENT_TERM =  @CURRENT_TERM -1 )                                      
                                      
 SELECT @PAST_DUE_RENEW=PAST_DUE_RENEW  FROM ACT_INSTALL_PLAN_DETAIL WITH(NOLOCK)                                                  
 WHERE IDEN_PLAN_ID=@INSTALL_PLANID                                             
                                            
 --Itrack # 6161 - 24 July 2009 -Manoj Rathore                            
                                          
                                       
SET @TOTAL_PREMIUM_AT_RENEWAL ='N'                                         
                     
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_POLICY_PROCESS WITH(NOLOCK)                     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID IN(5,18) AND ISNULL(REVERT_BACK,'N')='N')                                           
 BEGIN                                           
   IF(ISNULL(@TOTAL_PREMIUM,0) != 0.00)                                                
   BEGIN                                                
     IF( (((ISNULL(@TOTAL_PREMIUM,0) - (ISNULL(@TOTAL_PAID,0) + ISNULL(@TOTAL_DISCOUNT,0)) ) * 100) / ISNULL(@TOTAL_PREMIUM,0) )- ISNULL(@PAST_DUE_RENEW,0) > 0.0 )                                                  
     BEGIN                                                   
     SET @TOTAL_PREMIUM_AT_RENEWAL ='Y'                                                  
     END                                                       
   END                                            
 END                                       
                       
--== ITRACK No. 2595 ===---                                                
  /*IF EXISTS (SELECT CLAIM_ID FROM CLM_CLAIM_INFO CCI INNER JOIN POL_POLICY_PROCESS PPP                                                
   ON CCI.CUSTOMER_ID=PPP.CUSTOMER_ID AND CCI.POLICY_ID=PPP.POLICY_ID --AND CCI.POLICY_VERSION_ID=PPP.POLICY_VERSION_ID                                                
   INNER JOIN POL_CUSTOMER_POLICY_LIST POL                                     
   ON CCI.CUSTOMER_ID=POL.CUSTOMER_ID AND CCI.POLICY_ID=POL.POLICY_ID --AND CCI.POLICY_VERSION_ID=POL.POLICY_VERSION_ID                                                
   WHERE CCI.CUSTOMER_ID=@CUSTOMER_ID AND CCI.POLICY_ID=@POLICY_ID AND --CCI.POLICY_VERSI    
ON_ID=@BASE_POLICY_VERSION_ID AND                                                 
   PPP.PROCESS_ID IN(5,18) AND POL.CURRENT_TERM=@CURRENT_TERM -- AND                                                 
   --(DATEDIFF(DAY, CCI.CREATED_DATETIME,POL.APP_EFFECTIVE_DATE) >=0 and                                                
   -- DATEDIFF(DAY, CCI.CREATED_DATETIME,POL.APP_EFFECTIVE_DATE) <=45 )                                                
    )                                                
   BEGIN                                                
   SET @CLAIM_EFFECTIVE='Y'                                            
   END                                                
  */                                              
--========================= ITRACK No. 2595 =============================================---                                                
 DECLARE @APP_EFFECTIVEDATE DATETIME,                                                
 --@LOSS_DATE DATETIME,                                                
 @CLAIM_EFFECTIVE CHAR,                                                
 @CLAIM_COUNT int                                                
 SET  @CLAIM_EFFECTIVE='N'                                                
 SET @CLAIM_COUNT =0                                        
                      
 SELECT @APP_EFFECTIVEDATE=DATEADD(DD,-45,APP_EFFECTIVE_DATE)                                                
 FROM POL_CUSTOMER_POLICY_LIST                                                
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID                       
 AND CURRENT_TERM=case when @CURRENT_TERM >1 then @CURRENT_TERM -1 else @CURRENT_TERM end  -- prior term if exists at renewal                      
                                             
 SELECT @CLAIM_COUNT = COUNT(CCI.LOSS_DATE) FROM CLM_CLAIM_INFO CCI                                                  
 WHERE CCI.CUSTOMER_ID=@CUSTOMER_ID AND CCI.POLICY_ID=@POLICY_ID                                                         
 AND CCI.LOSS_DATE BETWEEN @APP_EFFECTIVEDATE AND GETDATE()                                                
                                             
 IF (@CLAIM_COUNT>0)                                                
 BEGIN                           
 SET @CLAIM_EFFECTIVE='Y'                                                
END                                                
--======================END ITRACK NO.2595 ===============================================                                             
END                                                
----  IF MORE THAN 5 CLAIMS THEN REFRE                                             
                                            
DECLARE @COUNTCLAIMDAYS INT            
                                            
IF(@POLICY_LOB=3)                                            
BEGIN SET @COUNTCLAIMDAYS=3*365.25 END --IF MORE THAN 5 CLAIMS WITH IN THREE YEARS THEN REFER                                             
ELSE             
BEGIN SET @COUNTCLAIMDAYS=5*365.50 END --IF MORE THAN 5 CLAIMS WITH IN FIVE YEARS THEN REFER                                             
                            
SELECT @EXCCESS_CLAIM = COUNT(CLAIM_ID)  FROM CLM_CLAIM_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND                                                
POLICY_ID=@POLICY_ID AND DATEDIFF(DAY,@POLICYEFFECTIVEDATE,ISNULL(CLM_CLAIM_INFO.LOSS_DATE,'0'))<@COUNTCLAIMDAYS                                             
AND (ISNULL(PAID_LOSS,'0')>0 OR ISNULL(PAID_EXPENSE,'0')>0)                                             
                               
IF(@POLICY_LOB=3)                                            
BEGIN                                                
 IF(@EXCCESS_CLAIM >5)                                             
 BEGIN SET @EXCCESS_CLAIM = 'Y' END                                             
        ELSE BEGIN SET @EXCCESS_CLAIM ='N'   END                                            
                 
END                                            
ELSE    --IF MORE THAN 5 CLAIMS WITH IN THREE YEARS THEN REFRE                                             
BEGIN                                            
 IF(@EXCCESS_CLAIM >=5)                                             
 BEGIN SET @EXCCESS_CLAIM = 'Y' END                                             
        ELSE BEGIN SET @EXCCESS_CLAIM ='N'   END                                               
                                 
END                                            
----------------------------------------------------------------------------------                                 
                                                
------REJECT THE APPLICATION IF LOB IS AUTO AND NO Vehicle other than Itrack 4536                                                
--If Personal with a                               
--(Vehicle Type) of Utility Trailer or Camper Van & Travel Trailer                                                 
--If Commercial with a vehicle type of Trailer                                                 
DECLARE @OTHER_VEHICLE CHAR,@OTHER_VEHICLE_COUNT INT                                                
IF(@POLICY_LOB=2)                                       
BEGIN                                                
 SELECT @OTHER_VEHICLE_COUNT=COUNT(CUSTOMER_ID) FROM POL_VEHICLES WITH(NOLOCK)                                                
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                
 AND ISNULL(APP_VEHICLE_PERTYPE_ID,0) NOT IN (11337,11870)                                       
 AND ISNULL(APP_VEHICLE_COMTYPE_ID,0) NOT IN (11341) AND  ISNULL(IS_ACTIVE,'Y')='Y'                                             
 IF (@OTHER_VEHICLE_COUNT>0)                                                
   SET @OTHER_VEHICLE='N'                                                    
 ELSE                                                   
      SET @OTHER_VEHICLE='Y'                                                 
                                                     
END                                                
ELSE                                                
  SET @OTHER_VEHICLE='N'                                                
                                                
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
--=========================================================================================                  
  --Trailablazer Discount named insured age should be between (35 to 69)(start)                                                
--=========================================================================================                                                 
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
  -- Itrack 6828           
IF(@POLICYEFFECTIVEDATE<@TRAILBLAZER_EXPIRY_DATE)    
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
    IF EXISTS(SELECT ISNULL(CLT_APPLICANT_LIST.CO_APPL_DOB,0) FROM CLT_APPLICANT_LIST INNER JOIN POL_APPLICANT_LIST                                                
    ON CLT_APPLICANT_LIST.CUSTOMER_ID = POL_APPLICANT_LIST.CUSTOMER_ID AND POL_APPLICANT_LIST.APPLICANT_ID = CLT_APPLICANT_LIST.APPLICANT_ID                                                
    WHERE POL_APPLICANT_LIST.CUSTOMER_ID=@CUSTOMER_ID AND POL_APPLICANT_LIST.POLICY_ID=@POLICY_ID AND POL_APPLICANT_LIST.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                 
    AND POL_APPLICANT_LIST.IS_PRIMARY_APPLICANT = 1 AND CONVERT(INT,DATEDIFF(day,ISNULL(CLT_APPLICANT_LIST.CO_APPL_DOB,0),@APP_EFFECTIVE_DATE)) NOT BETWEEN @THRTFIVEYEARDAYS AND @SEVENTYYEARDAYS)                                                 
      BEGIN                                                                        
      SET @INELIGIBLE_DRIVER='Y' -- REJECT                                                                                    
     END                                                                    
    ELSE                                                                      
     BEGIN                                                
     /* If yes then look at the Prior Loss and Loss Tab Look for losses in the last 3 years -                                      
     Effective date of the policy minus the date of claim If the total amount paid for any of these                  
     losses exceed $75.00  not eligible */                                                                       
      IF EXISTS(SELECT AMOUNT_PAID FROM APP_PRIOR_LOSS_INFO                                                      
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB=2 AND AMOUNT_PAID>75                             
      AND  (DATEDIFF(DAY,ISNULL(OCCURENCE_DATE,0),@APP_EFFECTIVE_DATE)<1095))                                                                                     
       BEGIN                                        
        SET @LOSS_AMT_EXCEED='Y' -- REJECT                                                                                    
       END                                              
     -- IF THE TOTAL AMOUNT PAID IS UNDER $75.00 EACH THEN NEW BUSINESS              
     -- LOOK AT THE DRIVERS/HOUSEHOLD MEMBERS TAB FOR ANY DRIVERS UNDER AGE 35                                                                                    
     -- LOOK ON THE MVR TAB - ORDERED FIELD - IF NO REFER TO UNDERWRITERS                                                                                      
      ELSE IF EXISTS(SELECT AMOUNT_PAID FROM APP_PRIOR_LOSS_INFO                                                                                     
       WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB=2 AND AMOUNT_PAID<75                                                                                     
       AND(DATEDIFF(DAY,ISNULL(OCCURENCE_DATE,0),@APP_EFFECTIVE_DATE)<1095))                                                                                    
       BEGIN                                                                                     
        IF EXISTS(SELECT DRIVER_DOB FROM POL_DRIVER_DETAILS                                                                                     
        WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                              
        AND CONVERT(INT,DATEDIFF(DAY,DRIVER_DOB,@APP_EFFECTIVE_DATE))<12775)                                                                   
         BEGIN                                    
          IF EXISTS (SELECT VERIFIED FROM POL_MVR_INFORMATION                                                                                
          WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND VERIFIED='0')                                               
           BEGIN                                                        
            SET @MVR_VER='Y' -- REFER HERE                                                 
           END                                                       
         END                                                     
       END                                                      
     END                                                                               
      END                                                  
  END    
 END                                                      
--Itrack 5540                                              
-- if policy is from conversion then rejection rule for trailblazer will not fire if trailblazer is given in parent version                                            
IF EXISTS(SELECT PPP.PROCESS_ID FROM POL_CUSTOMER_POLICY_LIST PCPL INNER JOIN                                             
POL_POLICY_PROCESS PPP  WITH(NOLOCK)                                                  
ON PCPL.CUSTOMER_ID= PPP.CUSTOMER_ID                                            
AND  PCPL.POLICY_ID= PPP.POLICY_ID                                               
AND  PCPL.POLICY_VERSION_ID= PPP.POLICY_VERSION_ID                                                            
 WHERE PCPL.CUSTOMER_ID=@CUSTOMER_ID AND PCPL.POLICY_ID=@POLICY_ID AND PPP.PROCESS_STATUS!='ROLLBACK' AND PPP.PROCESS_ID IN (8,9) and PCPL.FROM_AS400='Y')                                              
 BEGIN                                               
  SET @INELIGIBLE_DRIVER='N'                                              
 END                                    
--declare @PROCCESS_ID int                                                      
 IF EXISTS(SELECT PROCESS_ID FROM POL_POLICY_PROCESS   WITH(NOLOCK)                                                                      
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_STATUS!='ROLLBACK' AND PROCESS_ID IN (5,18))                      
 BEGIN                                               
  SET @INELIGIBLE_DRIVER='N'                                              
 END                                                                                   
--=========================================================================================                                                 
   --Trailablazer Discount named insured age should be between (35 to 69)(end)                                                
--=========================================================================================      
    
--Added by Charles on 11-Jan-10 for Itrack 6830                   
/*Look at Vehicle Coverages tab 1st vehicle                                               
  Coverage Single Limit Liability (CSL) or Bodily Injury Liability (Split Limits) to determine the Prior BI/CSL Limit                                        
  If vehicle coverage is suspended then look to see if there are any other vehicles on the policy with Coverage Single Limit Liability (CSL) or Bodily Injury Liability (Split Limits)                                         
  If one or all vehicles have suspended coverage then refer to underwriters - must have an underwriting tier in order to process the policy                                       
  */            
 DECLARE @UNDERWRITING_TIER CHAR            
 DECLARE @IS_RENEWAL CHAR            
 SET @UNDERWRITING_TIER ='N'            
 SET @IS_RENEWAL = 'N'            
             
 IF @POLICY_LOB = 2    
 BEGIN    
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID               
      AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID  <= @POLICY_VERSION_ID AND PROCESS_ID = 5)            
  BEGIN            
  SET @IS_RENEWAL = 'Y'            
  END            
             
  IF @STATE_ID = '14' AND @IS_RENEWAL='Y' AND NOT EXISTS            
  (            
    SELECT TOP 1 VEH.CUSTOMER_ID             
    FROM POL_VEHICLES VEH            
    LEFT JOIN  POL_VEHICLE_COVERAGES COV            
    LEFT JOIN MNT_COVERAGE MNT_COV            
    ON MNT_COV.COV_ID = COV.COVERAGE_CODE_ID            
    ON VEH.CUSTOMER_ID = COV.CUSTOMER_ID            
    AND VEH.POLICY_ID  = COV.POLICY_ID            
    AND VEH.POLICY_VERSION_ID = COV.POLICY_VERSION_ID             
    WHERE COV.CUSTOMER_ID = @CUSTOMER_ID             
    AND COV.POLICY_ID = @POLICY_ID             
    AND COV.POLICY_VERSION_ID = @POLICY_VERSION_ID            
    AND MNT_COV.COV_CODE IN ('BISPL','SLL')            
    AND ISNULL(VEH.IS_SUSPENDED,'') != 10963 --SUSPENDED_VEHILCE         
    AND ISNULL(VEH.IS_ACTIVE,'')='Y'           
    ORDER BY VEH.VEHICLE_ID            
  )            
    BEGIN            
  SET @UNDERWRITING_TIER='Y'            
    END     
 END           
            
 --Added till here             
                                               
--========================================================================================                                           
  SELECT                                                                                             
 @STATE_ID as STATE_ID,  -- if blank chk for -1                                                                                                                 
 @POLICY_LOB as POLICY_LOB,                                                                             
 @APP_TERMS as APP_TERMS,                                                         
 @APP_EFFECTIVE_DATE as APP_EFFECTIVE_DATE,                                                  
 @APP_EXPIRATION_DATE as APP_EXPIRATION_DATE,                                                     
 @HO_CLAIMS AS HO_CLAIMS,                                                 
 @PRIOR_POLICY_INFO as PRIOR_POLICY_INFO ,     @AUTO_DRIVER_FAULT AS AUTO_DRIVER_FAULT,                                                  
 @AGENCY_ID as AGENCY_ID, -- if blank chk for -1                                                                 
 @BILL_TYPE as BILL_TYPE,                                             
 --@PROXY_SIGN_OBTAINED as PROXY_SIGN_OBTAINED, -- if blank chk for -1                                                                        
 --@CHARGE_OFF_PRMIUM as CHARGE_OFF_PRMIUM  ,                                        
 -- client top info                                                                               
 @CUSTOMER_NAME as CUSTOMER_NAME,                  
 @ADDRESS as ADDRESS,                            
 @CUSTOMER_HOME_PHONE as CUSTOMER_HOME_PHONE,                                                                                                              
 @APP_VERSION_NO as APP_VERSION_NO,                                                  
 @POLICY_DISP_VERSION as POLICY_DISP_VERSION,                                                     
 @APP_NO as APP_NO  ,                                                                                    
 @CUSTOMER_TYPE as CUSTOMER_TYPE ,                                                              
 @IS_PRIMARY_APPLICANT  as IS_PRIMARY_APPLICANT,                                                    
 @CUSTOMER_INSURANCE_SCORE as CUSTOMER_INSURANCE_SCORE,                                                                                              
 -- Rule                                                                                               
 @MI_CUSTOMER_INSURANCE_SCORE as MI_CUSTOMER_INSURANCE_SCORE,                                                                                    
 --@POLICY_TYPE as POLICY_TYPE,                                                                              
 @CALLED_FROM as CALLED_FROM,                                                                          
 @TRAILER_ASSOCIATED_BOAT as TRAILER_ASSOCIATED_BOAT,                                                              
 @RENW_LOSS as RENW_LOSS,                                                 
 @COMP_LOSSES as COMP_LOSSES ,                                                    
 @PIC_OF_LOC AS  PIC_OF_LOC,                                                                      
 @PROPRTY_INSP_CREDIT AS PROPRTY_INSP_CREDIT,                                         
 --@DOWN_PAY_MENT as DOWN_PAY_MENT ,                                                          
 @DFI_ACC_NO_RULE  AS DFI_ACC_NO_RULE  ,                                                 
 @INACTIVE_APPLICATION as INACTIVE_APPLICATION ,                                                  
 @POL_UNDERWRITER as POL_UNDERWRITER,                                                  
 @INACTIVE_AGENCY     AS INACTIVE_AGENCY ,                                                  
 @CREDIT_CARD AS CREDIT_CARD,   --Uncommented on 25 sep08 by Pravesh Itrack 4811                                                
 @APPEFFECTIVEDATE AS APPEFFECTIVEDATE ,                                                  
 @TOTAL_PREMIUM_AT_RENEWAL as TOTAL_PREMIUM_AT_RENEWAL  ,                                                
 @PICH_OF_LOC AS PICH_OF_LOC  ,                                                
 @APPLICANT_OCCU AS APPLICANT_OCCU,                                                  
 @PROPERTY_INSP_CREDIT AS PROPERTY_INSP_CREDIT,                                             
 @CLAIM_EFFECTIVE AS CLAIM_EFFECTIVE,                                   
 @EXCCESS_CLAIM AS EXCCESS_CLAIM,                                                
 @NO_AT_FAULT as NO_AT_FAULT,                                                
 @PRIOR_POLICY_INFO_EXPIRE as PRIOR_POLICY_INFO_EXPIRE,                                                
 @MVR_VER AS MVR_VER,                                                
 @OTHER_VEHICLE AS OTHER_VEHICLE,--PRAVEEN KUMAR(03-03-2009):ITRACK 5522                                              
 @INELIGIBLE_DRIVER AS INELIGIBLE_DRIVER,        
 @WBSPO_LOSS AS WBSPO_LOSS,  --Added by Charles on 1-Dec-09 for Itrack 6647        
 @UNDERWRITING_TIER AS UNDERWRITING_TIER , --Added by Charles on 11-Jan-10 for Itrack 6830                                             
 @POLICY_CURRENCY AS POLICY_CURRENCY,  
 @CO_INSURANCE AS CO_INSURANCE                                                 
END     
  