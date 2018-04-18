IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForWatercraft_PolicyComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForWatercraft_PolicyComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




--sp_helptext Proc_GetRatingInformationForWatercraft_PolicyComponent        
 /*----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
  
    
      
        
           
            
              
                
--                    
Proc Name           :  Proc_GetRatingInformationForWatercraft_PolicyComponent                                                              
Created by          :  srikant                                                   
Date                :  19/12/2005                                                              
Purpose             :  To get the information for creating the input xml for Watercraft                                                              
Revison History     :                                                             
Used In             :  Wolverine                                            
                                          
Modified By         :  Shafi                                          
Date                :  20 March 2006                                           
Purpose             :  To get the information for MULTIPOLICYAUTOHOMEDISCOUNT                                                          
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
            
              
                
                  
                    
Date     Review By          Comments                                                              
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
            
              
                
                  
                    
                            
*/                  
--                                                                                  
create PROC dbo.Proc_GetRatingInformationForWatercraft_PolicyComponent                                                              
(                                                              
 @CUSTOMERID    int,                                                              
 @APPID    int,                                                              
 @APPVERSIONID   int                                                              
)                                                              
AS                                                              
                                                              
BEGIN                                                              
 set quoted_identifier off                       
                                                           
--Basic Policy Page--                                                                                                        
DECLARE         @NEWBUSINESSFACTOR       NVARCHAR(20)                                                              
DECLARE         @STATENAME               NVARCHAR(20)                                                              
DECLARE         @RENEWAL                 NVARCHAR(20)                                                              
DECLARE         @QUOTEEFFDATE            NVARCHAR(20)                                                              
DECLARE         @QUOTEEXPDATE            NVARCHAR(20)                                                              
DECLARE         @TERRITORYCODE           NVARCHAR(20)                                                              
DECLARE         @POLICYTERMS             NVARCHAR(20)                                                              
DECLARE         @ATTACHTOWOLVERINE       NVARCHAR(20)                                                              
DECLARE         @BOATHOMEDISC            NVARCHAR(20)                                                         
DECLARE         @INSURANCESCORE          NVARCHAR(20)                                                              
                                                  
--DECLARE         @PERSONALLIABILITY               NVARCHAR(20)                                                  
--DECLARE         @MEDICALPAYMENTSOTHER            NVARCHAR(20)                                                              
DECLARE         @UNATTACHEDEQUIPMENT             NVARCHAR(20)                                                              
                               
DECLARE         @LIMIT_1                         NVARCHAR(20)                                                              
DECLARE         @LIMIT_2                         NVARCHAR(20)                       
DECLARE         @MULTIPOLICYAUTOHOMEDISCOUNT     NVARCHAR(10)                                          
DECLARE         @INSURANCESCOREDIS NVARCHAR(20)                                      
DECLARE         @MULTIPOLICYDISAPPLICABLE INT                                   
--declare         @UNINSUREDBOATERS          NVARCHAR(20)                                  
--DECLARE         @UNATTACHEDEQUIPMENT_DEDUCTIBLE  VARCHAR(20)                          
--DECLARE         @MEDICALPAYMENT    VARCHAR(20)                              
--DECLARE         @MEDICALPAYMENTSOTHERLIMIT     VARCHAR(20)                    
---------------------------------------------------------------------------                    
--STATENAME                                                              
---------------------------------------------------------------------------                                  
 Declare @APP_NUMBER varchar(20)                  
 declare @APP_VERSION varchar(20)                                                             
SELECT                                                              
   @STATENAME= UPPER(ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'')) ,                                                         
   @QUOTEEFFDATE = CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE,101),                                                        
   @QUOTEEXPDATE = CONVERT(VARCHAR(10),APP_EXPIRATION_DATE,101) ,                                                         
   @POLICYTERMS = APP_TERMS,                    
   @APP_NUMBER = APP_NUMBER,                  
   @APP_VERSION = APP_VERSION                  
                  
                                                   
                                                              
FROM                                                               
 APP_LIST WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST  WITH (NOLOCK)                     
  ON  APP_LIST.STATE_ID=MNT_COUNTRY_STATE_LIST.STATE_ID                                                               
WHERE                                                               
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                                              
------------------------------------------------------------------------------                    
------------------------------------------------------------------------------                                            
SET @MULTIPOLICYAUTOHOMEDISCOUNT='N'                                          
                                                  
                                                    
select @BOATHOMEDISC=case                                                  
  MULTI_POLICY_DISC_APPLIED                                                  
 WHEN '1' then 'Y'                                                  
 ELSE 'N'                                                  
 END,                                          
 @MULTIPOLICYAUTOHOMEDISCOUNT = case ISNULL(MULTI_POLICY_DISC_APPLIED,0)                                                  
 when '1' then 'Y'                                                
 else 'N'                          
 end                                                  
FROM                                                  
 APP_WATERCRAFT_GEN_INFO WITH (NOLOCK) WHERE                                                               
 CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                    
                                                          
--select * from APP_WATERCRAFT_GEN_INFO                                                            
--------------------------------                                                              
--TERRITORY                                            
---------------------------------                    
                                      
if ISNULL(@MULTIPOLICYAUTOHOMEDISCOUNT,'') = ''                                           
 set @MULTIPOLICYAUTOHOMEDISCOUNT='N'                                          
                                      
-------------------------------- Changed By Neeraj Singh for insurance Score -----------------------------------------                                                             
 DECLARE @ZIPCODE  nvarchar(20)                                                      
 DECLARE @LOBID  nvarchar(20)                                                              
                   --by default value for score is 100  ,  --by default value for score is 'N' --@INSURANCESCOREDIS for discounts and surchages                                        
 SELECT @ZIPCODE =CUSTOMER_ZIP --@INSURANCESCORE= ISNULL(CUSTOMER_INSURANCE_SCORE,100),                    
 --@INSURANCESCOREDIS= ISNULL(CUSTOMER_INSURANCE_SCORE,-1)                        
 FROM   CLT_CUSTOMER_LIST WITH (NOLOCK) WHERE  CUSTOMER_ID =@CUSTOMERID                                                         
                        
  SELECT @INSURANCESCORE=case convert(nvarchar(20),ISNULL(APPLY_INSURANCE_SCORE,-1))     
  when -1 then '100'      
 when  -2 then 'NOHITNOSCORE'       
 else convert(nvarchar(20),APPLY_INSURANCE_SCORE) end ,                
 @INSURANCESCOREDIS= ISNULL(APPLY_INSURANCE_SCORE,-1)                        
 FROM   APP_LIST WITH (NOLOCK) WHERE  CUSTOMER_ID =@CUSTOMERID           
 AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                                        
                   
IF(@INSURANCESCOREDIS = '-1')or (@INSURANCESCOREDIS = '-2')                    
 SET @INSURANCESCOREDIS = 'N'                
        
     
                                           
IF (@STATENAME = 'INDIANA')                                      
 SET @TERRITORYCODE = 1                                      
ELSE                                      
 BEGIN                                                     
   IF ( @ZIPCODE !='')                                                            
     BEGIN                                                              
   SELECT  @TERRITORYCODE = ISNULL(TERR ,'') , @LOBID = LOBID FROM  MNT_TERRITORY_CODES  WITH (NOLOCK)                                                             
     WHERE  ZIP = (SUBSTRING(LTRIM(RTRIM(ISNULL(@ZIPCODE,''))),1,5))    and LOBID=4  
	     AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                                                              
     END                                      
   ELSE                                                              
     BEGIN                                                              
     SET @TERRITORYCODE=''                                                              
     END                                                              
 END                                      
                                      
-------------------------------------------                    
                    
                                  
                                    
                 
--waverunners, jetskis (with or without lift bar) or mini-jetboats...there is only 1 discount available                                    
--to them at the policy level and that is Insurance Score                                      
SET @MULTIPOLICYDISAPPLICABLE=0                                    
SELECT @MULTIPOLICYDISAPPLICABLE=COUNT(CUSTOMER_ID) FROM APP_WATERCRAFT_INFO                                    
WHERE  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                     
       AND TYPE_OF_WATERCRAFT NOT IN (11373,11386,11387,11390)                                    
                                    
                            
                                                              
--------------------------------                                                              
/*                                                          
                                                  
---                                    
--- policy level coverages [Start]                                                  
                                                  
--SELECT limit_1 FROM APP_WATERCRAFT_COVERAGE_INFO WHERE                                              
--CUSTOMER_ID=772 AND APP_ID=5 AND APP_VERSION_ID=1 and  coverage_code_id='19'                                                   
                                                  
  IF @STATENAME='INDIANA'                                            
  BEGIN                                                
                                                
SELECT @Limit_1=ISNULL(Limit_1,'0'),  @Limit_2=ISNULL(Limit_2,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                   
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  and  coverage_code_id='21'   -- "21 - MCPAY - Section II - Medical  "                                                  
                                                 
set @MEDICALPAYMENTSOTHER=ISNULL(@Limit_1,'0')           + '/' + @Limit_2                                
SET @MEDICALPAYMENT      =ISNULL(@Limit_1,'0')                               
SET @MEDICALPAYMENTSOTHERLIMIT     = ISNULL(@Limit_2,0)                            
                                            
                                                  
                                                  
 SELECT @PERSONALLIABILITY=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                   
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  and  coverage_code_id='19'   -- "19 -LCCSL - water craft liablity "                                                  
                                            
IF @PERSONALLIABILITY is null                                             
set @PERSONALLIABILITY='0'                                            
                                                   
                                                  
SELECT @UNATTACHEDEQUIPMENT=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                   
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  and  coverage_code_id='26'   -- "26 -EBIUE  - Increase in "Unattached Equipment" And Personal Effects Coverage "                                                  
                                                  
IF @UNATTACHEDEQUIPMENT is not null                                                   
BEGIN                                                    
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                                                          
END                                                    
                                                  
     END                                                
                                                
               
                                                  
  IF @STATENAME='MICHIGAN'                                                
  BEGIN                                                
                                                
SELECT @Limit_1=ISNULL(Limit_1,'0'),  @Limit_2=ISNULL(Limit_2,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                   
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  and  coverage_code_id='68'   -- "21 - MCPAY - Section II - Medical  "                                                  
                                                  
set @MEDICALPAYMENTSOTHER=ISNULL(@Limit_1,'0')           + '/' + @Limit_2                                  
SET @MEDICALPAYMENT      =ISNULL(@Limit_1,'0')                             
SET @MEDICALPAYMENTSOTHERLIMIT     = ISNULL(@Limit_2,0)                                             
                                                  
                                                  
                                                  
 SELECT @PERSONALLIABILITY=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                   
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  and  coverage_code_id='65'   -- "19 -LCCSL - water craft liablity "                                                  
                                                   
                                              
SELECT @UNATTACHEDEQUIPMENT=ISNULL(Limit_1,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                   
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  and  coverage_code_id='71'   -- "26 -EBIUE  - Increase in "Unattached Equipment" And Personal Effects Coverage "                                                  
                                                  
IF @UNATTACHEDEQUIPMENT is not null                                      
BEGIN                                                           
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                                                          
END                                                    
                                                  
END                                                
                      
                                                  
                                                 
IF @STATENAME='WISCONSIN'                                                
                                                
BEGIN                                                
                                                
SELECT @Limit_1=ISNULL(Limit_1,'0'),  @Limit_2=ISNULL(Limit_2,'0') FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                               
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  and  coverage_code_id='821'   -- " MCPAY - Section II - Medical  "                                                  
                                                  
set @MEDICALPAYMENTSOTHER=@Limit_1           + '/' + @Limit_2                                
SET @MEDICALPAYMENT      =ISNULL(@Limit_1,'0')                                   
SET @MEDICALPAYMENTSOTHERLIMIT     = ISNULL(@Limit_2,0)                                         
                                                  
                  
                                                  
 SELECT @PERSONALLIABILITY=Limit_1 FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                   
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  and  coverage_code_id='820'   -- " LCCSL - water craft liablity "                                                  
                                                   
                                                  
SELECT @UNATTACHEDEQUIPMENT=Limit_1 FROM APP_WATERCRAFT_COVERAGE_INFO  WITH (NOLOCK) WHERE                                                   
CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  and  coverage_code_id='823'   -- " EBIUE  - Increase in "Unattached Equipment" And Personal Effects Coverage "                                                
                                                  
IF @UNATTACHEDEQUIPMENT is not null                                                   
BEGIN                                                           
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                                                          
END                                                    
                                                  
END                                  
                                  
--FOR UNINSURED BOATERS                                  
 DECLARE @COV_ID INT                              
 set @UNINSUREDBOATERS =''                             
 EXEC @COV_ID = PROC_GET_WATERCRAFT_COVERAGE_ID @CUSTOMERID,                                                           
       @APPID,                                                                                      
       @APPVERSIONID,                                                                    
       'UMBCS'                                   
  SELECT @UNINSUREDBOATERS=LIMIT_1 FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                   
  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND  COVERAGE_CODE_ID=@COV_ID          
                           
 if @UNINSUREDBOATERS =''                          
    SET    @UNINSUREDBOATERS  =''                          
                                  
--EBIUE                           
SET @UNATTACHEDEQUIPMENT_DEDUCTIBLE=''                                  
EXEC @COV_ID = PROC_GET_WATERCRAFT_COVERAGE_ID @CUSTOMERID,                                                           
       @APPID,                                                                                      
       @APPVERSIONID,                                                                                                
       'EBIUE'                                   
  SELECT @UNATTACHEDEQUIPMENT_DEDUCTIBLE=DEDUCTIBLE_1 FROM APP_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                                   
  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND  COVERAGE_CODE_ID=@COV_ID                            
                          
IF @UNATTACHEDEQUIPMENT_DEDUCTIBLE=''                          
 SET @UNATTACHEDEQUIPMENT_DEDUCTIBLE=''                                
                                                     
                                  
                                                  
                     */                             
                                               
                        
                                                  
                                                    
                                                   
IF(@QUOTEEFFDATE IS NULL)                                                          
BEGIN                                                           
 SET @QUOTEEFFDATE=''                                                          
END                                                              
----                                                          
IF(@QUOTEEXPDATE IS NULL)                                                          
BEGIN                                                           
 SET @QUOTEEXPDATE=''                                                          
END                                                          
----                                                      
IF(@ATTACHTOWOLVERINE IS NULL)                                                          
BEGIN                                                           
 SET @ATTACHTOWOLVERINE='N'                                    
END                                                          
----                                                          
IF(@BOATHOMEDISC IS NULL)                                                          
BEGIN                                                           
 SET @BOATHOMEDISC='N'                                                          
END                                                          
----                                                          
--IF(@PERSONALLIABILITY IS NULL)                                                          
--BEGIN                                                           
 --SET @PERSONALLIABILITY=''                                                          
--END                                                          
----                                                          
--IF(@MEDICALPAYMENTSOTHER IS NULL)                                                          
--BEGIN                                                           
-- SET @MEDICALPAYMENTSOTHER=''                                                     
--END                                                          
----                                                          
--IF(@UNATTACHEDEQUIPMENT IS NULL)             
--BEGIN                                                           
-- SET @UNATTACHEDEQUIPMENT=''                                                          
--END                                                    
                          
                                                  
                                                        
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
          
            
              
               
                  
                     
                      
                       
                          
                             
                              
                                
                                  
                                    
                                     
                                      
                                         
if(@TERRITORYCODE is null or @TERRITORYCODE='')                                      
 set @TERRITORYCODE=1                                        
                                         
                                             
                                             
                    
                                                
                                                    
                                                      
                                                        
                                 
                                 
--------------------------------                                                              
                                               
                                                              
SELECT                                                              
    'Y'                 AS NEWBUSINESS,                                                              
                                                                 
   @STATENAME          AS STATENAME,                                                              
   'N'                 AS RENEWAL,                                                              
   @QUOTEEFFDATE       AS QUOTEEFFDATE,                                                              
   @QUOTEEXPDATE       AS QUOTEEXPDATE,                  
   @APP_NUMBER        AS APP_NUMBER,                    
   @APP_VERSION        AS APP_VERSION,                    
   @TERRITORYCODE      AS TERRITORYCODE,                                                              
   @POLICYTERMS        AS POLICYTERMS,                                 
   @ATTACHTOWOLVERINE  AS ATTACHTOWOLVERINE,                                                              
   @MULTIPOLICYAUTOHOMEDISCOUNT       AS BOATHOMEDISC,                                                              
   @INSURANCESCORE     AS INSURANCESCORE,                                           
   @INSURANCESCOREDIS  AS INSURANCESCOREDIS,                                                               
  -- @PERSONALLIABILITY  AS PERSONALLIABILITY,                                                              
  -- @MEDICALPAYMENTSOTHER   AS MEDICALPAYMENTSOTHER,                                                              
   --@UNATTACHEDEQUIPMENT    AS UNATTACHEDEQUIPMENT ,                                          
   --@MULTIPOLICYAUTOHOMEDISCOUNT AS MULTIPOLICYAUTOHOMEDISCOUNT ,                
   --@MULTIPOLICYAUTOHOMEDISCOUNT AS BOATHOMEDISC,                          
   @MULTIPOLICYDISAPPLICABLE    AS MULTIPOLICYDISAPPLICABLE                               
   --@UNINSUREDBOATERS            AS UNINSUREDBOATERS ,                                  
   --@UNATTACHEDEQUIPMENT_DEDUCTIBLE AS UNATTACHEDEQUIPMENT_DEDUCTIBLE,                              
  -- @MEDICALPAYMENT                 AS MEDICALPAYMENT ,                              
  -- @MEDICALPAYMENTSOTHERLIMIT      AS MEDICALPAYMENTSOTHERLIMIT                  
END                                                            
                                                            
                                                          
                                              
                                                          
                                                      
                                                       
                                                       
                                                       
                                                  
                                                
                              
                                              
                                            
                                          
                                        
                                        
                                        
                                      
                                      
        
                                    
                                  
                                
                              
                            
                          
                        
                      
                    
                    
                    
                  
                
              
            
          
          
        
      
    
  
  





GO

