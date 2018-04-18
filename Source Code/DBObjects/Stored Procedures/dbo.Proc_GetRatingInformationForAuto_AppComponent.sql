IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForAuto_AppComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForAuto_AppComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name           : Dbo.Proc_GetRatingInformationForAuto_AppComponent                              
Created by            : Nidhi.                              
Date                     : 04/10/2005                              
Purpose                : To get the information for creating the input xml                                
Revison History    :                              
Used In                 :   Creating InputXML for vehicle                              
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--  
-- DROP PROC dbo.Proc_GetRatingInformationForAuto_AppComponent 1491, 1, 1  
CREATE  PROC [dbo].[Proc_GetRatingInformationForAuto_AppComponent]   
               
(                              
@CUSTOMERID    int,                              
@APPID    int,                              
@APPVERSIONID   int                              
)                              
AS                              
                              
BEGIN                              
                               
                              
DECLARE @STATENAME            nvarchar(100)                                                             
DECLARE @NEWBUSINESS            nvarchar(10)                                                          
DECLARE @RENEWAL            nvarchar(10)                                      
DECLARE @QUOTEEFFDATE            nvarchar(20)                                            
DECLARE         @LOBID       nvarchar(10)                                                 
DECLARE @ZIPCODE            nvarchar(20)   /*  ZIP Code  -- NOT NEEDED FOR RATER */                                                              
DECLARE @YEARSCONTINSURED            nvarchar(5)                                                               
DECLARE @YEARSCONTINSUREDWITHWOLVERINE            nvarchar(5)  
DECLARE @SAFEDRIVERDISCOUNT          nvarchar(20)                                
DECLARE @TERMFACTOR nvarchar(5)                                                        
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
                            
DECLARE @MULTIPOLICYAUTOHOMEDISCOUNT            nvarchar(100)                                                     
DECLARE @QUALIFIESTRAIBLAZERPROGRAM    nvarchar(5)  
 set    @QUALIFIESTRAIBLAZERPROGRAM ='N'                        
DECLARE @INSURANCESCORE            nvarchar(20)                               
DECLARE         @TERRITORY                 nvarchar(20)                    
DECLARE @INSURANCESCOREDIS                  nvarchar(20)                    
DECLARE @APP_EFFECTIVE_DATE varchar(50)                              
  
-- Statename ,Term factor, quoteeff date                
            
--Fetch APP/VERSION            
declare @APP_NUMBER varchar(20)            
declare @APP_VERSION varchar(20)            
            
SELECT            
@APP_NUMBER = APP_NUMBER,            
@APP_VERSION = APP_VERSION,            
@APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE  
FROM APP_LIST WITH (NOLOCK)            
WHERE CUSTOMER_ID= @CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID                 
            
            
----------------                         
select @STATENAME = upper(STATE_NAME) ,                             
       @TERMFACTOR = APP_TERMS,                                
       @QUOTEEFFDATE = convert(char(20),APP_EFFECTIVE_DATE,101) ,                            
       @QUALIFIESTRAIBLAZERPROGRAM = case isnull(app_sublob,'0')                            
 when '1' then 'Y'                            
 else 'N'         
 end,                            
       @LOBID=APP_LOB                             
FROM APP_LIST WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST WITH (NOLOCK) ON MNT_COUNTRY_STATE_LIST.STATE_ID=APP_LIST.STATE_ID                               
WHERE CUSTOMER_ID= @CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID                             
                 
  
-- insurance score                            
 SELECT --@INSURANCESCORE=CAST(ISNULL(CUSTOMER_INSURANCE_SCORE,100) AS varchar(10)) ,               
 @ZIPCODE =CUSTOMER_ZIP                           
 FROM CLT_CUSTOMER_LIST  WITH (NOLOCK)                            
 WHERE CUSTOMER_ID =@CUSTOMERID             
            
             
--------------------------------                 
 SELECT @INSURANCESCORE=case CAST(ISNULL(APPLY_INSURANCE_SCORE,-1) AS varchar(20))                             
when -1 then '100'        
 when  -2 then 'NOHITNOSCORE'         
 else convert(nvarchar(20), APPLY_INSURANCE_SCORE) end       
      
 FROM APP_LIST  WITH (NOLOCK)                            
 WHERE CUSTOMER_ID =@CUSTOMERID                   
     and   APP_ID = @APPID          
 and APP_VERSION_ID=@APPVERSIONID   
     
-- @QUALIFIESTRAIBLAZERPROGRAM for trailblazer programme  

 IF (@QUALIFIESTRAIBLAZERPROGRAM = 'Y' AND @INSURANCESCORE <> 'NOHITNOSCORE')  
BEGIN  
 IF ((DATEDIFF(DAY,@APP_EFFECTIVE_DATE,'2007-08-01 00:00:00.000') < 0)AND @INSURANCESCORE > 750)   
  SET @QUALIFIESTRAIBLAZERPROGRAM = 'Y'  
   
 ELSE IF ((DATEDIFF(DAY,@APP_EFFECTIVE_DATE,'2007-08-01 00:00:00.000') > 0) AND (@INSURANCESCORE > 700) )   
  SET @QUALIFIESTRAIBLAZERPROGRAM = 'Y'  

 ELSE
  SET @QUALIFIESTRAIBLAZERPROGRAM = 'N'  

   
END  
ELSE IF   @INSURANCESCORE = 'NOHITNOSCORE'
  SET @QUALIFIESTRAIBLAZERPROGRAM = 'N' 
  
  
  
--if (@INSURANCESCORE > '700')  
  
  
  
       
          
--@INSURANCESCOREDIS for discounts and surchages                  
                   
SELECT  @INSURANCESCOREDIS= ISNULL(APPLY_INSURANCE_SCORE,-1)       --by default value for score is 'N'                                   
 FROM   APP_LIST   WITH (NOLOCK)                                      
 WHERE  CUSTOMER_ID =@CUSTOMERID          
 and APP_ID = @APPID          
 and APP_VERSION_ID=@APPVERSIONID                              
---------------------------------                  
 IF @INSURANCESCOREDIS=-1 or ISNULL(@INSURANCESCOREDIS,'')=''  or @INSURANCESCOREDIS=-2                
  SET @INSURANCESCOREDIS='N'                  
                                
                            
SET @TERRITORY=''                    
IF ( @ZIPCODE !='')                                      
  BEGIN        
   SELECT  @TERRITORY = ISNULL(TERR ,'')   FROM  MNT_TERRITORY_CODES WITH (NOLOCK)                                      
 WHERE  ZIP =(SUBSTRING(LTRIM(RTRIM(ISNULL(@ZIPCODE,''))),1,5))  and LOBID=@LOBID  
 AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                                      
                                         
  END                               
                              
                              
-- New Business or Renewal                             
SET @NEWBUSINESS ='TRUE'                               
SET @RENEWAL ='FALSE'   -- will always be new business for application                             
 ----   MULTI POLICY AUTO  HOME  DISCOUNT                             
                             
                              
-- SET @MULTIPOLICYAUTOHOMEDISCOUNT='FALSE'                           
                          
SELECT @MULTIPOLICYAUTOHOMEDISCOUNT= ISNULL(MULTI_POLICY_DISC_APPLIED,'0')                          
                          
  FROM POL_AUTO_GEN_INFO  WITH (NOLOCK)                            
                          
                          
WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @APPID AND POLICY_VERSION_ID=@APPVERSIONID                          
                          
IF @MULTIPOLICYAUTOHOMEDISCOUNT = '1'                          
 SET @MULTIPOLICYAUTOHOMEDISCOUNT='Y'                          
                          
ELSE                          
 SET @MULTIPOLICYAUTOHOMEDISCOUNT='N'                           
                        
                       
   
                               
                          
IF EXISTS (SELECT * FROM POL_AUTO_GEN_INFO WITH (NOLOCK) WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @APPID AND POLICY_VERSION_ID=@APPVERSIONID)                               
 SELECT                            
  /*@MULTIPOLICYAUTOHOMEDISCOUNT=                              
   CASE ISNULL(MULTI_POLICY_DISC_APPLIED_PP_DESC ,'0')                              
   WHEN '0' THEN 'FALSE'                              
   WHEN '1' THEN 'TRUE'                              
   WHEN '' THEN 'FALSE'                      
   END ,  */                          
 @YEARSCONTINSURED = isnull(YEARS_INSU,'0'),                            
 @YEARSCONTINSUREDWITHWOLVERINE = isnull(YEARS_INSU_WOL,'')                            
 FROM POL_AUTO_GEN_INFO WITH (NOLOCK) WHERE CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @APPID AND POLICY_VERSION_ID=@APPVERSIONID                           
                                        
 IF @YEARSCONTINSUREDWITHWOLVERINE  is null or @YEARSCONTINSUREDWITHWOLVERINE=''                         
   SET  @YEARSCONTINSUREDWITHWOLVERINE='0'   
  
   
--check for safe driver discount    
DECLARE @SAFERENEWALDISCOUNT decimal                
SELECT   
@SAFERENEWALDISCOUNT = SAFE_DRIVER_RENEWAL_DISCOUNT  
FROM POL_DRIVER_DETAILS  WITH (NOLOCK)   WHERE  CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @APPID AND POLICY_VERSION_ID=@APPVERSIONID   
  
if (@SAFERENEWALDISCOUNT > 0)  
set @SAFEDRIVERDISCOUNT = 'YES'  
else  
set @SAFEDRIVERDISCOUNT = 'NO'                           
----------------------------------------------------------------------------------------------              
 -- Check from the Prior Loss table. Send count of chargeable/nonchargeable losses.              
 DECLARE @LOSSES_CHARGEABLE_NONCHARGEABLE VARCHAR(10)              
 SET @LOSSES_CHARGEABLE_NONCHARGEABLE ='0'              
  

	DECLARE @UNDERWRITING_TIER NVARCHAR(10)
	SET @UNDERWRITING_TIER=''

	SELECT @UNDERWRITING_TIER=UNDERWRITING_TIER FROM POL_UNDERWRITING_TIER WHERE  CUSTOMER_ID= @CUSTOMERID AND POLICY_ID= @APPID AND POLICY_VERSION_ID=@APPVERSIONID             
-- SELECT @LOSSES_CHARGEABLE_NONCHARGEABLE=COUNT(*) FROM APP_PRIOR_LOSS_INFO     WITH (NOLOCK)  
-- WHERE CUSTOMER_ID=@CUSTOMERID               
--              
              
----------------------------------------------------------------------------------------------              

 --Set YEARSCONTINSURED ,YEARSCONTINSUREDWITHWOLVERINE to 0            
--set @YEARSCONTINSURED = '0'            
--set @YEARSCONTINSUREDWITHWOLVERINE = '0'                             
                            
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
 @STATENAME      as  STATENAME,                   
 @NEWBUSINESS  as     NEWBUSINESS,                                                  
 @RENEWAL   as    RENEWAL,                              
 @INSURANCESCOREDIS AS INSURANCESCOREDIS,                  
 @QUOTEEFFDATE   as     QUOTEEFFDATE,                     
 @APP_NUMBER as APP_NUMBER,            
 @APP_VERSION as APP_VERSION,            
 @TERMFACTOR as POLICYTERMS,                            
 @ZIPCODE          as ZIPCODE, -- ********** NOT NEEDED FOR RATER                            
 @TERRITORY as TERRITORY ,                
 @YEARSCONTINSURED        as    YEARSCONTINSURED,                                
 @YEARSCONTINSUREDWITHWOLVERINE       as       YEARSCONTINSUREDWITHWOLVERINE,              
 @LOSSES_CHARGEABLE_NONCHARGEABLE     as   LOSSES_CHARGEABLE_NONCHARGEABLE,                                                  
 @MULTIPOLICYAUTOHOMEDISCOUNT    as          MULTIPOLICYAUTOHOMEDISCOUNT,               
 @QUALIFIESTRAIBLAZERPROGRAM as QUALIFIESTRAIBLAZERPROGRAM,                            
 @INSURANCESCORE as INSURANCESCORE ,                            
 'CALLED'   AS CALLEDFROM,                     
 @SAFEDRIVERDISCOUNT as SAFEDRIVER,
 @UNDERWRITING_TIER AS UNDRWRTINGTIER                        
                              
END                            

GO

