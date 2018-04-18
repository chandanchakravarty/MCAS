IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForAuto_AppComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForAuto_AppComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                          
Proc Name                : Dbo.Proc_GetPolicyRatingInformationForAuto_AppComponent                          
Created by               : shafi.                          
Date                     : 02/03/2006                         
Purpose                 : To get the information for creating the input xml                            
Revison History    :                          
Used In                 :   Creating InputXML for vehicle                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                   
--         drop proc Proc_GetPolicyRatingInformationForAuto_AppComponent        
CREATE PROC [dbo].[Proc_GetPolicyRatingInformationForAuto_AppComponent]                
(                          
@CUSTOMERID    int,                          
@POLICYID    int,                          
@POLICYVERSIONID   int                          
)                          
AS                          
                          
BEGIN                          
                           
                          
DECLARE @STATENAME             nvarchar(100)                                                         
DECLARE @NEWBUSINESS            nvarchar(10)                                                      
DECLARE @RENEWAL             nvarchar(10)                                  
DECLARE @QUOTEEFFDATE           nvarchar(20)                                        
DECLARE @LOBID         nvarchar(10)                                             
DECLARE @ZIPCODE             nvarchar(20)       
DECLARE @YEARSCONTINSURED       nvarchar(5)                                                           
DECLARE @YEARSCONTINSUREDWITHWOLVERINE            nvarchar(5)     
DECLARE @SAFEDRIVERDISCOUNT     nvarchar(20)                              
DECLARE @TERMFACTOR   nvarchar(5)                                                    
/* DECLARE @BI            nvarchar(200)                                                          
DECLARE @BILIMIT1            nvarchar(100)                                                           
DECLARE @BILIMIT2            nvarchar(100)                                                           
DECLARE @PD            nvarchar(100)                                                           
DECLARE @CSL            nvarchar(100)                                                           
DECLARE @MEDPM            nvarchar(100)                                                           
DECLARE @TYPE            nvarchar(200)                                                           
DECLARE @ISUNDERINSUREDMOTORISTS            nvarchar(2)                                                         
DECLARE @UMSPLIT            nvarchar(100)                                                           
DECLARE @UMSPLITLIMIT1            nvarchar(100)                                                           
DECLARE @UMSPLITLIMIT2            nvarchar(100)                                                           
DECLARE @UMCSL            nvarchar(100)                          
DECLARE @WEARINGSEATBELT            nvarchar(2)                                                          
DECLARE @PDLIMIT            nvarchar(100)                                   */                        
                        
DECLARE @MULTIPOLICYAUTOHOMEDISCOUNT             nvarchar(100)                                                           
DECLARE @QUALIFIESTRAIBLAZERPROGRAM              nvarchar(5)                                                           
DECLARE @INSURANCESCORE               nvarchar(20)                          
DECLARE @INSURANCESCOREDIS                    nvarchar(20)                        
DECLARE @TERRITORY                     nvarchar(20)                             
DECLARE @POLICYEFFECTIVEDATE           nvarchar(20)                          
                        
                             
----------Fetch App/Version                    
DECLARE @POL_NUMBER      varchar(20)                  
DECLARE @POL_VERSION      varchar(20)                    
 SELECT                     
  @POL_NUMBER = ISNULL(POLICY_NUMBER,APP_NUMBER)   ,                 
  @POL_VERSION = POLICY_DISP_VERSION                    
 FROM POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                    
 WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                         
   SET @QUALIFIESTRAIBLAZERPROGRAM = 'N'      
                    
-- STATENAME ,TERM FACTOR, QUOTEEFF DATE     
                        
 SELECT     
    @STATENAME = UPPER(STATE_NAME) ,                         
         @TERMFACTOR = POLICY_TERMS,                            
         @QUOTEEFFDATE = CONVERT(CHAR(10),POL_VER_EFFECTIVE_DATE,101) ,  
 --@POLICYEFFECTIVEDATE = APP_EFFECTIVE_DATE -- changed by Manoj Itrack # 6378    
    @POLICYEFFECTIVEDATE = CONVERT(CHAR(10),APP_EFFECTIVE_DATE,101),                        
         @QUALIFIESTRAIBLAZERPROGRAM = CASE ISNULL(POLICY_SUBLOB,'0')                              
        WHEN '1' THEN 'Y'                        
        ELSE 'N'                        
        END,                        
         @LOBID=POLICY_LOB                         
 FROM POL_CUSTOMER_POLICY_LIST  WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST WITH (NOLOCK)     
 ON MNT_COUNTRY_STATE_LIST.STATE_ID=POL_CUSTOMER_POLICY_LIST.STATE_ID                           
 WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                         
                      
-- INSURANCE SCORE        
                
 SELECT                        
   @ZIPCODE =CUSTOMER_ZIP                        
  FROM CLT_CUSTOMER_LIST WITH (NOLOCK)                         
  WHERE CUSTOMER_ID =@CUSTOMERID     
DECLARE @PROCCESSID INT    
DECLARE @CACELATION_TYPE INT    
DECLARE @PROCESS_STATUS nvarchar(40)    
SELECT @PROCCESSID=PROCESS_ID,    
    @CACELATION_TYPE=CANCELLATION_TYPE,    
    @PROCESS_STATUS=PROCESS_STATUS    
FROM POL_POLICY_PROCESS WITH (NOLOCK)            
WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND NEW_POLICY_VERSION_ID=@POLICYVERSIONID     
    
IF((@PROCCESSID=4 OR @PROCCESSID=16) AND  @CACELATION_TYPE=14244 AND @PROCESS_STATUS != 'ROLLBACK')    
BEGIN    
 SELECT @INSURANCESCORE =CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1))       --BY DEFAULT VALUE FOR SCORE IS 100                                             
         WHEN -1 THEN '100'            
         WHEN  -2 THEN 'NOHITNOSCORE'             
       ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) END     
--     @INSURANCESCOREDIS =CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1))       --BY DEFAULT VALUE FOR SCORE IS 100                                             
--         WHEN -1 THEN '100'            
--         WHEN  -2 THEN 'NOHITNOSCORE'             
--       ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) END    
   FROM   CLT_CUSTOMER_LIST  WITH (NOLOCK)                                                       
   WHERE  CUSTOMER_ID =@CUSTOMERID    
END    
ELSE    
BEGIN                         
  SELECT     
  @INSURANCESCORE=CASE CONVERT(NVARCHAR(20),(ISNULL(APPLY_INSURANCE_SCORE,-1)))                         
     WHEN -1 THEN '100'            
      WHEN  -2 THEN 'NOHITNOSCORE'             
      ELSE CONVERT(NVARCHAR(20),APPLY_INSURANCE_SCORE)     
    END            
  FROM POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                         
  WHERE CUSTOMER_ID =@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID      
--@INSURANCESCOREDIS FOR DISCOUNTS AND SURCHAGES                        
-- SELECT      
--  @INSURANCESCOREDIS= ISNULL(APPLY_INSURANCE_SCORE,-1)       --BY DEFAULT VALUE FOR SCORE IS 'N'                                       
--  FROM    POL_CUSTOMER_POLICY_LIST   WITH (NOLOCK)             
-- WHERE   CUSTOMER_ID =@CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                
           
END    
-- DATE CHECK HAS BEEN APPLIED ON QUALIFIES TRAIBLAZERPROGRAM         
--Calculate    @QUALIFIESTRAIBLAZERPROGRAM if @INSURANCESCORE is not NOHITNOSCORE : praveen 02/20/2008    
  if(@INSURANCESCORE <> 'NOHITNOSCORE')    
  BEGIN    
 IF (@QUALIFIESTRAIBLAZERPROGRAM = 'Y')    
  IF ((DATEDIFF(DAY,@POLICYEFFECTIVEDATE,'2007-08-01 00:00:00.000') < 0)AND @INSURANCESCORE > 750)     
   BEGIN    
    SET @QUALIFIESTRAIBLAZERPROGRAM = 'Y'    
   END     
  IF ((DATEDIFF(DAY,@POLICYEFFECTIVEDATE,'2007-08-01 00:00:00.000') > 0) AND @INSURANCESCORE > 700)     
   BEGIN    
    SET @QUALIFIESTRAIBLAZERPROGRAM = 'Y'    
   END    
  END    
    
                      
--  IF @INSURANCESCOREDIS=-1 or ISNULL(@INSURANCESCOREDIS,'')=''  or @INSURANCESCOREDIS=-2                     
--    BEGIN    
--   SET @INSURANCESCOREDIS='N'                      
--  END    
                          
 SET @TERRITORY=''                                      
 IF ( @ZIPCODE !='')                                  
    BEGIN       
      SELECT      
    @TERRITORY = ISNULL(TERR ,'')       
   FROM  MNT_TERRITORY_CODES WITH (NOLOCK)                                  
    WHERE  ZIP = (SUBSTRING(LTRIM(RTRIM(ISNULL(@ZIPCODE,''))),1,5)) and LOBID=@LOBID                                  
     AND @POLICYEFFECTIVEDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                                   
    END                           
                          
                          
--  NEW BUSINESS OR RENEWAL                
 SET @NEWBUSINESS ='TRUE'                                                            
 SET @RENEWAL ='FALSE'   -- WILL ALWAYS BE NEW BUSINESS FOR APPLICATION                         
----    MULTI POLICY AUTO  HOME  DISCOUNT                         
--  SET @MULTIPOLICYAUTOHOMEDISCOUNT='FALSE'                       
                      
 SELECT     
  @MULTIPOLICYAUTOHOMEDISCOUNT= ISNULL(MULTI_POLICY_DISC_APPLIED,'0')                      
   FROM POL_AUTO_GEN_INFO  WITH (NOLOCK)                        
 WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                      
                      
 IF @MULTIPOLICYAUTOHOMEDISCOUNT = '1'    
  BEGIN                      
    SET @MULTIPOLICYAUTOHOMEDISCOUNT='Y'                      
                END    
 ELSE                      
  BEGIN    
    SET @MULTIPOLICYAUTOHOMEDISCOUNT='N'                      
  END    
                      
-- CHECK FROM THE PRIOR LOSS TABLE. SEND COUNT OF CHARGEABLE/NONCHARGEABLE LOSSES.                    
      
 DECLARE @LOSSES_CHARGEABLE_NONCHARGEABLE VARCHAR(10)                    
  SET @LOSSES_CHARGEABLE_NONCHARGEABLE ='0'                    
                    
--  SELECT     
--  @LOSSES_CHARGEABLE_NONCHARGEABLE=COUNT(*)     
-- FROM APP_PRIOR_LOSS_INFO  WITH (NOLOCK)                  
--  WHERE CUSTOMER_ID=@CUSTOMERID                     
    
 IF EXISTS (SELECT * FROM POL_AUTO_GEN_INFO WITH (NOLOCK) WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID)                          
   SELECT                        
     @YEARSCONTINSURED = isnull(YEARS_INSU,'0') ,                      
   @YEARSCONTINSUREDWITHWOLVERINE = isnull(YEARS_INSU_WOL,'0')                        
   FROM POL_AUTO_GEN_INFO WITH (NOLOCK)     
  WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                          
      
 if(@YEARSCONTINSUREDWITHWOLVERINE is null)    
  BEGIN           
   SET  @YEARSCONTINSUREDWITHWOLVERINE='0'       
  END    
    
 DECLARE @SAFERENEWALDISCOUNT decimal      
  SELECT     
   @SAFERENEWALDISCOUNT = SAFE_DRIVER_RENEWAL_DISCOUNT      
  FROM POL_DRIVER_DETAILS WITH (NOLOCK)     
  WHERE  CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID      
     
 IF (@SAFERENEWALDISCOUNT > 0)    
  BEGIN    
   SET @SAFEDRIVERDISCOUNT = 'YES'    
  END    
 ELSE    
  BEGIN    
   SET @SAFEDRIVERDISCOUNT = 'NO'     
  END    
      
 DECLARE @UNDERWRITING_TIER NVARCHAR(10)  
 SET @UNDERWRITING_TIER=''  
  
 SELECT @UNDERWRITING_TIER=UNDERWRITING_TIER FROM POL_UNDERWRITING_TIER WHERE   CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  
  /*  These are not required at the Policy Level by the Quoting Engine ..therefore we are not sending these.                          
 SET @BI=''                                                                                  
 SET @BILIMIT1=''                          
 SET @BILIMIT2=''                                                            
 SET @PD=''              
 SET @CSL=''                                                            
 SET @MEDPM=''          
 SET @TYPE=''                                                              
 SET @ISUNDERINSUREDMOTORISTS=''                                                               
 SET @UMSPLIT=''                            
 SET @UMSPLITLIMIT1=''                                                               
 SET @UMSPLITLIMIT2=''                                                              
 SET @UMCSL=''                                                              
 SET @PDLIMIT=''                                                         
 SET @MULTIPOLICYAUTOHOMEDISCOUNT=''                                                               
 SET @WEARINGSEATBELT=''                                                               
 SET @QUALIFIESTRAIBLAZERPROGRAM=''         */                                                       
                          
                                                    
  SELECT                           
   @STATENAME         AS   STATENAME,                                       
   @NEWBUSINESS      AS      NEWBUSINESS,                                              
   @RENEWAL      AS     RENEWAL,                          
  -- @QUOTEEFFDATE      AS      QUOTEEFFDATE, -- (Commented as disscussed with Rajan and Rvinder 07/22/2009)                   
   @POLICYEFFECTIVEDATE AS QUOTEEFFDATE,    
   @POL_NUMBER     AS  POL_NUMBER,                    
   @POL_VERSION     AS  POL_VERSION,                     
   @TERMFACTOR     AS  POLICYTERMS,                        
   @ZIPCODE             AS  ZIPCODE, -- ********** NOT NEEDED FOR RATER                        
   @YEARSCONTINSURED        AS     YEARSCONTINSURED,                                                 
   @YEARSCONTINSUREDWITHWOLVERINE        AS      YEARSCONTINSUREDWITHWOLVERINE,                    
   @LOSSES_CHARGEABLE_NONCHARGEABLE   AS  LOSSES_CHARGEABLE_NONCHARGEABLE,                                                
   @MULTIPOLICYAUTOHOMEDISCOUNT     AS      MULTIPOLICYAUTOHOMEDISCOUNT,                           
   @INSURANCESCORE    AS  INSURANCESCORE  ,                      
   @INSURANCESCOREDIS     AS  INSURANCESCOREDIS ,                      
   @QUALIFIESTRAIBLAZERPROGRAM   AS  QUALIFIESTRAIBLAZERPROGRAM,                        
   @TERRITORY     AS  TERRITORY      ,                    
   'CALLED'       AS  CALLEDFROM,                        
   @SAFEDRIVERDISCOUNT    AS  SAFEDRIVER,  
 @UNDERWRITING_TIER AS UNDRWRTINGTIER                              
 END                        
  
GO

