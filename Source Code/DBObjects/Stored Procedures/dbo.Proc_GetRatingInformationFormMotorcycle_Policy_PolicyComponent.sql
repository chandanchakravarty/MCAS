IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationFormMotorcycle_Policy_PolicyComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationFormMotorcycle_Policy_PolicyComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---
   
Proc Name           : Proc_GetRatingInformationFormMotorcycle_Policy_PolicyComponent                      
Created by          : Shrikant Bhatt                      
Date                : 09/01/2006                      
Purpose             : To get the information for creating the input xml for Motorcycle                      
Revison History     :                      
Used In             : Wolverine                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



  
*/                    
 -- drop proc Proc_GetRatingInformationFormMotorcycle_Policy_PolicyComponent
CREATE  PROC [dbo].[Proc_GetRatingInformationFormMotorcycle_Policy_PolicyComponent]                  
(                      
@CUSTOMERID     int,                      
@POLID     int,                      
@POLVERSIONID   int                      
)                      
AS                      
                      
BEGIN                      
 set quoted_identifier off                      
                      
--Basic Policy Page--                      
DECLARE    @STATENAME          nvarchar(20)                      
DECLARE    @NEWBUSINESSFACTOR  nvarchar(20)                      
DECLARE    @QUOTEEFFDATE       nvarchar(30)                      
DECLARE    @QUOTEEXPDATE       nvarchar(20)                      
DECLARE    @TERMFACTOR         nvarchar(20)                      
DECLARE    @TERRITORY          nvarchar(20)                      
DECLARE    @ZIPCODE            nvarchar(10)                      
DECLARE    @YEARSCONTINSURED   nvarchar(10)                      
DECLARE    @YEARSCONTINSUREDWITHWOLVERINE   nvarchar(10)                      
DECLARE    @STATE_ID           nvarchar(10)                      
DECLARE    @LOBID              nvarchar(10)                      
DECLARE    @MULTIPOLICYAUTOHOMEDISCOUNT     nvarchar(10)                      
DECLARE    @INSURANCESCORE     nvarchar(100)                      
DECLARE    @INSURANCESCOREDIS  NVARCHAR(20)                  
              
              
DECLARE   @MEDICALDEDUCTIBLE    nvarchar(100)                  
DECLARE   @MEDICALTYPE    nvarchar(100)                  
DECLARE   @MEDPMLIMIT     nvarchar(100)                  
                     
                           
              
                      
                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



  
  
    
      
        
          
            
              
              
                
--------------------------------                      
--STATENAME                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


---------------------------- policy ------------------------------------------------------------------                
             
--Fetch POLNUMBER/VERSION/              
declare @POL_NUMBER varchar(20)              
declare @POL_VERSION varchar(20)              
              
SELECT                      
     @POL_NUMBER= POLICY_NUMBER,              
     @POL_VERSION = POLICY_DISP_VERSION ,  
     @QUOTEEFFDATE =convert(char(20),APP_EFFECTIVE_DATE,101),  
	 @QUOTEEXPDATE = convert(char(20),APP_EXPIRATION_DATE,101)
FROM                       
     POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                    
WHERE                       
     CUSTOMER_ID =@CUSTOMERID   and POLICY_ID = @POLID and POLICY_VERSION_ID=@POLVERSIONID                 
              
-- STATE NAME                 
SELECT                      
     @STATENAME= UPPER(MNT_COUNTRY_STATE_LIST.STATE_NAME),                    
     @STATE_ID = POL_CUSTOMER_POLICY_LIST.STATE_ID                      
FROM                       
     POL_CUSTOMER_POLICY_LIST WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST WITH (NOLOCK) ON                      
     POL_CUSTOMER_POLICY_LIST.STATE_ID  =  MNT_COUNTRY_STATE_LIST.STATE_ID                       
WHERE                       
     CUSTOMER_ID =@CUSTOMERID   and POLICY_ID = @POLID and POLICY_VERSION_ID=@POLVERSIONID                 
                
                 
                      
IF ( @STATENAME is null )                      
      BEGIN                        
     SET @STATENAME =''                         
      END                      
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


--------------------------------                      
--TERRITORY                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


DECLARE @PROCCESSID INT
DECLARE @CACELATION_TYPE INT
DECLARE @PROCESS_STATUS nvarchar(40)
SELECT @PROCCESSID=PROCESS_ID,
	   @CACELATION_TYPE=CANCELLATION_TYPE,
       @PROCESS_STATUS=PROCESS_STATUS
FROM POL_POLICY_PROCESS WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLID AND NEW_POLICY_VERSION_ID=@POLVERSIONID 
IF((@PROCCESSID=4 OR @PROCCESSID=16) AND  @CACELATION_TYPE=14244 AND @PROCESS_STATUS != 'ROLLBACK')
BEGIN
	SELECT @INSURANCESCORE =CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1))       --BY DEFAULT VALUE FOR SCORE IS 100                                         
									WHEN -1 THEN '100'        
									WHEN  -2 THEN 'NOHITNOSCORE'         
							ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) END 
--		   @INSURANCESCOREDIS =CASE CONVERT(NVARCHAR(20),ISNULL(CUSTOMER_INSURANCE_SCORE,-1))       --BY DEFAULT VALUE FOR SCORE IS 100                                         
--									WHEN -1 THEN '100'        
--									WHEN  -2 THEN 'NOHITNOSCORE'         
--							ELSE CONVERT(NVARCHAR(20),CUSTOMER_INSURANCE_SCORE) END 
			FROM   CLT_CUSTOMER_LIST  WITH (NOLOCK)                                                   
			WHERE  CUSTOMER_ID =@CUSTOMERID
END
ELSE
BEGIN
SELECT                       
    @INSURANCESCORE=case convert(nvarchar(100),ISNULL( APPLY_INSURANCE_SCORE,-1))         
when -1 then '100'        
 when  -2 then 'NOHITNOSCORE'         
 else convert(nvarchar(100),APPLY_INSURANCE_SCORE) end                     
FROM                        
    POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                      
WHERE                       
     CUSTOMER_ID = @CUSTOMERID             
and POLICY_ID = @POLID and POLICY_VERSION_ID=@POLVERSIONID     

--SELECT  @INSURANCESCOREDIS= ISNULL(APPLY_INSURANCE_SCORE,-1)       --by default value for score is 'N'                            
-- FROM   POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                                                  
-- WHERE  CUSTOMER_ID =@CUSTOMERID         
--and POLICY_ID = @POLID and POLICY_VERSION_ID=@POLVERSIONID                                        
END         
      
--IF @INSURANCESCOREDIS=-1 or ISNULL(@INSURANCESCOREDIS,'')=''  or @INSURANCESCOREDIS=-1                          
--  SET @INSURANCESCOREDIS='N'                       
-- 
                    
SELECT                       
    @ZIPCODE =CUSTOMER_ZIP --,@INSURANCESCORE= ISNULL( CUSTOMER_INSURANCE_SCORE,0)                       
FROM                        
    CLT_CUSTOMER_LIST WITH (NOLOCK)                      
WHERE                       
     CUSTOMER_ID = @CUSTOMERID                       
                      
IF ( @ZIPCODE !='')                      
   BEGIN                      
     SELECT  @TERRITORY = ISNULL(TERR ,'') , @LOBID = LOBID FROM  MNT_TERRITORY_CODES WITH (NOLOCK)                       
  WHERE  ZIP = SUBSTRING(LTRIM(RTRIM(ISNULL(@ZIPCODE,''))),1,5)                      
  AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                                                     
END                      
ELSE                      
     BEGIN                      
         SET @TERRITORY=''                      
     END                      
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 
---------------------------------                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


--------------------------------                      
--TERMFACTOR                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


--------------------------------                      
SELECT                      
   @TERMFACTOR = APP_TERMS                        
                      
FROM                       
   POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                      
WHERE                       
   CUSTOMER_ID =  @CUSTOMERID AND POLICY_ID  =  @POLID  AND  POLICY_VERSION_ID  =  @POLVERSIONID                      
                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------              
                
                
--- Fieldes for Medical payments                
                 
                    
select              
 @MEDICALTYPE = case (LIMIT1_AMOUNT_TEXT)              
 WHEN '1st Party Medical-$300' THEN 'FULL'              
 WHEN '1st Party Medical-Full' THEN 'FULL'              
 WHEN '$1000 Medical' THEN 'EXCESS'              
 WHEN '1st Party Medical-Excess' THEN 'EXCESS'              
 ELSE              
  'FULL'              
 END,              
              
 @MEDICALDEDUCTIBLE = ISNULL(DEDUCTIBLE_1,'0'),          
 @MEDPMLIMIT = ISNULL(LIMIT_1,'0')               
FROM               
 POL_VEHICLE_COVERAGES with(nolock)                
WHERE               
 CUSTOMER_ID =  @CUSTOMERID AND POLICY_ID  =  @POLID  AND  POLICY_VERSION_ID  =  @POLVERSIONID                      
                  
                
                    
                     
                      
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------              
                
                
                  
                    
                    
--------------------------------                      
--MULTI-POLICY-AUTO_HOME DISCOUNT                      
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------              
                
                
                  
                    
                    
--------------------------------                      
                      
SELECT                       
     @MULTIPOLICYAUTOHOMEDISCOUNT = ISNULL(MULTI_POLICY_DISC_APPLIED,0) ,                
     @YEARSCONTINSURED= cast(ISNULL(YEARS_INSU,0) as nvarchar(5)  ) ,                            
     @YEARSCONTINSUREDWITHWOLVERINE= cast(ISNULL(YEARS_INSU_WOL,0) as nvarchar(5)  )                          
FROM                       
     POL_AUTO_GEN_INFO   WITH (NOLOCK)                    
WHERE                       
     CUSTOMER_ID =@CUSTOMERID AND POLICY_ID = @POLID AND POLICY_VERSION_ID = @POLVERSIONID                      
                      
IF (@MULTIPOLICYAUTOHOMEDISCOUNT = '0')                    
      BEGIN                      
   SET  @MULTIPOLICYAUTOHOMEDISCOUNT = 'N'                      
      END                      
                      
IF (@MULTIPOLICYAUTOHOMEDISCOUNT = '1')                      
      BEGIN                      
     SET  @MULTIPOLICYAUTOHOMEDISCOUNT = 'Y'                      
      END                  
                   
                 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



 
  
  
    
      
        
          
            
              
              
           
                 
                
                    
                    
--------------------------------                      
                      
                      
SELECT                      
   @STATENAME     AS STATENAME,          
   @ZIPCODE  AS ZIPCODE,                    
   @TERRITORY      AS TERRITORY,               
   @POL_NUMBER     AS POL_NUMBER,              
   @POL_VERSION   AS POL_VERSION,                     
   'Y'             AS NEWBUSINESS,                      
   'N'             AS RENEWAL,                      
   @QUOTEEFFDATE       AS QUOTEEFFDATE,                       
   --GETDATE()       AS QUOTEEXPDATE,                      
   @QUOTEEXPDATE   AS QUOTEEFFDATE,
   @TERMFACTOR     AS POLICYTERMS,                      
   @YEARSCONTINSURED     AS YEARSCONTINSURED,                           
   @YEARSCONTINSUREDWITHWOLVERINE              AS YEARSCONTINSUREDWITHWOLVERINE,                      
   @MULTIPOLICYAUTOHOMEDISCOUNT    AS MULTIPOLICYAUTOHOMEDISCOUNT,                      
   @INSURANCESCORE            AS INSURANCESCORE ,                  
   @INSURANCESCOREDIS         AS INSURANCESCOREDIS,              
              
 @MEDICALDEDUCTIBLE  AS  MEDICALDEDUCTIBLE,                   
 @MEDICALTYPE    AS MEDICALTYPE,                
 @MEDPMLIMIT    AS  MEDPMLIMIT                
              
                      
                    
END                    
                    







GO

