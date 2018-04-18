IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForWatercraft_PolicyComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForWatercraft_PolicyComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------     

Proc Name           :  Proc_GetPolicyRatingInformationForWatercraft_PolicyComponent                                                    
Created by          :  shafi                                         
Date                :  01/03/2006                                              
Purpose             :  To get the information for creating the input xml for Watercraft                                                    
Revison History     :                                                   
Used In             :  Wolverine                                                    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Date     Review By          Comments                                                    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

*/                       
                                                
CREATE PROC [dbo].[Proc_GetPolicyRatingInformationForWatercraft_PolicyComponent]                                                                        
(                                                    
@CUSTOMERID    int,                                                    
@POLICYID    int,                                                    
@POLICYVERSIONID   int                                                    
)                                                    
AS                                                    
                                                    
BEGIN                                                    
 set quoted_identifier off                                                    
--Basic Policy Page--                                                    
                                                    
DECLARE         @NEWBUSINESSFACTOR       nvarchar(20)                                                    
DECLARE         @STATENAME               nvarchar(20)                                                    
DECLARE         @RENEWAL                 nvarchar(20)                                                    
DECLARE         @QUOTEEFFDATE            nvarchar(20)                                                    
DECLARE         @QUOTEEXPDATE            nvarchar(20)                                                    
DECLARE         @TERRITORYCODE           nvarchar(20)                                                    
DECLARE         @POLICYTERMS             nvarchar(20)                                                    
DECLARE         @ATTACHTOWOLVERINE       nvarchar(20)             
DECLARE         @BOATHOMEDISC            nvarchar(20)                                                    
DECLARE         @INSURANCESCORE          nvarchar(20)                                                    
                                        
DECLARE         @PERSONALLIABILITY       nvarchar(20)                                                    
DECLARE         @MEDICALPAYMENTSOTHER    nvarchar(20)                                          
DECLARE         @UNATTACHEDEQUIPMENT     nvarchar(20)                           
                                                     
DECLARE         @Limit_1 nvarchar(20)                                                    
DECLARE         @Limit_2 nvarchar(20)                  
DECLARE         @MULTIPOLICYAUTOHOMEDISCOUNT    nvarchar(10)                              
DECLARE         @INSURANCESCOREDIS nvarchar(20)                        
DECLARE         @MULTIPOLICYDISAPPLICABLE INT                        
declare         @UNINSUREDBOATERS          NVARCHAR(20)                      
DECLARE         @UNATTACHEDEQUIPMENT_DEDUCTIBLE  VARCHAR(20)                                           
                               
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                                        
--STATENAME                                                    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                            
--select app_effective_date,policy_effective_date from pol_customer_policy_list                              
                                        
                                          
                                                  
--------------------------------                    
--test                
declare @POL_NUMBER varchar(20)                      
declare @POL_VERSION varchar(20)                                           
                                                    
SELECT                                                    
   @STATENAME= UPPER(ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'')) ,                                               
   @QUOTEEFFDATE = CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE,101),                                              
   @QUOTEEXPDATE = CONVERT(VARCHAR(10),APP_EXPIRATION_DATE,101),                
--test                
     @POL_NUMBER = POLICY_NUMBER,                
     @POL_VERSION = POLICY_DISP_VERSION                                                
   
                                                    
FROM                                                     
 POL_CUSTOMER_POLICY_LIST WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST  WITH (NOLOCK) ON  POL_CUSTOMER_POLICY_LIST.STATE_ID=MNT_COUNTRY_STATE_LIST.STATE_ID               
WHERE                                                     
 CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                                    
                                                    
       
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------     
                          
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
       
                                
 SET @MULTIPOLICYAUTOHOMEDISCOUNT='N'                                    
                                          
select @BOATHOMEDISC=case                                        
  MULTI_POLICY_DISC_APPLIED                                        
 WHEN '1' then 'Y'                                        
 ELSE 'N'                                        
 END,@MULTIPOLICYAUTOHOMEDISCOUNT = case ISNULL(MULTI_POLICY_DISC_APPLIED,0)                                      
 when '1' then 'Y'                                    
 else 'N'                                    
 end                                                    
FROM                                        
      POL_WATERCRAFT_GEN_INFO   WITH (NOLOCK)               
WHERE                                                     
 CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                          
                                                
                                                  
--------------------------------                  
--TERRITORY                                                    
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
      
--                
                    
                          
if ISNULL(@MULTIPOLICYAUTOHOMEDISCOUNT,'') = ''                  
 set @MULTIPOLICYAUTOHOMEDISCOUNT='N'                             
                            
                              
                                
/*              WAVERUNNERS, JETSKIS (WITH OR WITHOUT LIFT BAR) OR MINI-JETBOATS...THERE IS ONLY 1 DISCOUNT AVAILABLE                        
    TO THEM AT THE POLICY LEVEL AND THAT IS INSURANCE SCORE                    
    MUTIPPOLICY DISCOUNT IS GIVEN ONLY, IF THERE ARE TWO ELIGIBLE BOAT.                   
    TRAILER, AND PERSONAL WATERCRFATS(WAVERUNNER, MINIJET, JET SKI) ARE NOT ELIGIBLE FOR THIS DISCOUNT.                  
    IF AN APP CONTAINS ONE INBOARD AND ONE WAVERUNNER, NONE OF THE BOAT WILL GET THIS DISCOUNT.                  
*/                  
                     
SET @MULTIPOLICYDISAPPLICABLE=0                   
SELECT @MULTIPOLICYDISAPPLICABLE=COUNT(CUSTOMER_ID) FROM POL_WATERCRAFT_INFO   WITH (NOLOCK)                         
WHERE  CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                          
       AND TYPE_OF_WATERCRAFT NOT IN (11373,11386,11387,11390)                        
                                 
                      
                                                  
--------------------------------                                               
 DECLARE @ZIPCODE  nvarchar(20)                                                    
 DECLARE @LOBID  nvarchar(20)                                                    
                                                    
 SELECT @ZIPCODE =ISNULL(CUSTOMER_ZIP,'')                                                     
 FROM   CLT_CUSTOMER_LIST  WITH (NOLOCK)                                                   
 WHERE  CUSTOMER_ID =@CUSTOMERID                                               
                                                 
 IF ( @ZIPCODE !='')                                                    
   BEGIN                                                    
  SELECT  @TERRITORYCODE = ISNULL(TERR ,'') , @LOBID = LOBID FROM  MNT_TERRITORY_CODES  WITH (NOLOCK)                                                
  WHERE  ZIP = (SUBSTRING(LTRIM(RTRIM(ISNULL(@ZIPCODE,''))),1,5))     and LOBID=4    
 AND @QUOTEEFFDATE BETWEEN EFFECTIVE_FROM_DATE  AND ISNULL(EFFECTIVE_TO_DATE,'3000-03-16 16:59:06.630')                                                
   END                                                    
 ELSE                                                    
   BEGIN                                                    
  SET @TERRITORYCODE=''                                                    
   END                                                    
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 ------------------ Changed by Neeraj Singh for Insurance score ----------------------------- 
DECLARE @PROCCESSID INT
DECLARE @CACELATION_TYPE INT
DECLARE @PROCESS_STATUS nvarchar(40)
SELECT @PROCCESSID=PROCESS_ID,
	   @CACELATION_TYPE=CANCELLATION_TYPE,
       @PROCESS_STATUS=PROCESS_STATUS
FROM POL_POLICY_PROCESS WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND NEW_POLICY_VERSION_ID=@POLICYVERSIONID 
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
 SELECT  @INSURANCESCORE= case convert(nvarchar(20),ISNULL(APPLY_INSURANCE_SCORE,-1))       --by default value for score is 100                                         
 when -1 then '100'        
 when  -2 then 'NOHITNOSCORE'         
 else convert(nvarchar(20),APPLY_INSURANCE_SCORE) end       
FROM   POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                                                     
 WHERE  CUSTOMER_ID =@CUSTOMERID          
AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                          
                       
                         
  --@INSURANCESCOREDIS for discounts and surchages                            
                             
--SELECT  @INSURANCESCOREDIS= ISNULL(APPLY_INSURANCE_SCORE,-1)       --by default value for score is 'N'                                             
-- FROM   POL_CUSTOMER_POLICY_LIST WITH (NOLOCK)                                                  
-- WHERE  CUSTOMER_ID =@CUSTOMERID          
-- AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID   
                                    
 END                           
-- IF @INSURANCESCOREDIS=-1 or isnull(@INSURANCESCOREDIS,'')='' or @INSURANCESCOREDIS=-2                           
--  SET @INSURANCESCOREDIS='N'                                               
                                                     
--------------------------------                                                    
--TERMFACTOR                                                    
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 


  
    
      
        
         
            
             
                
                
                  
                    
                      
                        
                          
                          
                            
                            
                              
                                
                                  
                                    
                                      
           
                                          
                                        
                                              
                                                
                                                
                                                  
                                                    
SELECT @POLICYTERMS = APP_TERMS                                                      
FROM   POL_CUSTOMER_POLICY_LIST   WITH (NOLOCK)                             
                                                     
WHERE  CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                                    
                                    
---                                         
--- policy level coverages [Start]                                        
                                        
--SELECT limit_1 FROM POL_WATERCRAFT_COVERAGE_INFO WHERE                                         
--CUSTOMER_ID=772 AND POL_ID=5 AND POL_VERSION_ID=1 and  coverage_code_id='19'                                         
                                        
  IF @STATENAME='INDIANA'                                      
  BEGIN                                      
                                      
SELECT @Limit_1=ISNULL(Limit_1,'0'),  @Limit_2=ISNULL(Limit_2,'0') FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK)                            
WHERE CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id='21'   -- "21 - MCPAY - Section II - Medical  "                                        
                                        
set @MEDICALPAYMENTSOTHER=ISNULL(@Limit_1,'0')          + '/' + @Limit_2                                        
                                        
                                        
                                        
 SELECT @PERSONALLIABILITY=ISNULL(Limit_1,'0') FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK)                            
WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id='19'   -- "19 -LCCSL - water craft liablity "                                        
                                  
IF @PERSONALLIABILITY is null                                   
set @PERSONALLIABILITY='0'                                  
                                         
                                        
SELECT @UNATTACHEDEQUIPMENT=ISNULL(Limit_1,'0') FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id=
'26'   -- "26 -EBIUE  - Increase in "Unattached Equipment" And Personal Effects Coverage "                                        
                                        
IF @UNATTACHEDEQUIPMENT is not null                                         
BEGIN                                                 
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                                                
END                                          
                                        
     END                                      
                           
                                        
                                        
  IF @STATENAME='MICHIGAN'           
  BEGIN                                      
                                      
SELECT @Limit_1=ISNULL(Limit_1,'0'),  @Limit_2=ISNULL(Limit_2,'0') FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id='68'   -- "21 - MCPAY - Section II - Medical  "                                        
                                        
set @MEDICALPAYMENTSOTHER=ISNULL(@Limit_1,'0')         + '/' + @Limit_2                                       
                                        
                           
                                        
 SELECT @PERSONALLIABILITY=ISNULL(Limit_1,'0') FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id='65'   -- "19 -LCCSL - water craft liablity "                                        
                                         
                                        
SELECT @UNATTACHEDEQUIPMENT=ISNULL(Limit_1,'0') FROM POL_WATERCRAFT_COVERAGE_INFO  WITH (NOLOCK) WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id='71'   -- "26 -EBIUE  - Increase in "Unattached Equipment" And Personal Effects Coverage "                                        
                                        
IF @UNATTACHEDEQUIPMENT is not null                                         
BEGIN                                                 
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                                                
END                                          
                                        
END                                      
                                        
                                        
                                       
IF @STATENAME='WISCONSIN'                                      
                                      
BEGIN                                      
                                      
SELECT @Limit_1=ISNULL(Limit_1,'0'),  @Limit_2=ISNULL(Limit_2,'0') FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id='821'   -- " MCPAY - Section II - Medical  "                                        
   
set @MEDICALPAYMENTSOTHER=@Limit_1           + '/' + @Limit_2                                        
                                    
                                        
                                        
 SELECT @PERSONALLIABILITY=Limit_1 FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id='820'   -- " LCCSL - water craft liablity "                
                                         
                                        
SELECT @UNATTACHEDEQUIPMENT=Limit_1 FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and coverage_code_id='823'   -- " EBIUE  - Increase in "Unattached Equipment" And Personal Effects Coverage "                                      
                                        
IF @UNATTACHEDEQUIPMENT is not null                                         
BEGIN                                                 
 SET @UNATTACHEDEQUIPMENT='$' + @UNATTACHEDEQUIPMENT                                                
END                                          
                                        
END                                        
                   
--FOR UNINSURED BOATERS                       
 DECLARE @COV_ID INT                           
EXEC @COV_ID = Proc_Get_POL_WATERCRAFT_COVERAGE_ID @CUSTOMERID,                                                                        
         @POLICYID,                                                           
     @POLICYVERSIONID,                                                                        
        'UMBCS'                             
SELECT @UNATTACHEDEQUIPMENT=Limit_1 FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id= @COV_ID                             
                   
--Unattached Equipment" And Personal Effects Coverage (unscheduled) - Actual Cash Basis                       
EXEC @COV_ID = Proc_Get_POL_WATERCRAFT_COVERAGE_ID @CUSTOMERID,                                                                        
         @POLICYID,                                                           
     @POLICYVERSIONID,                        
      'EBIUE'                       
  SELECT @UNATTACHEDEQUIPMENT_DEDUCTIBLE=DEDUCTIBLE_1 FROM POL_WATERCRAFT_COVERAGE_INFO WITH (NOLOCK) WHERE                                         
CUSTOMER_ID =@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID  and  coverage_code_id= @COV_ID                      
                                        
                                        
                                        
                                          
                                         
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
IF(@PERSONALLIABILITY IS NULL)                                                
BEGIN                                                 
 SET @PERSONALLIABILITY=''                                                
END                                  
----                                                
IF(@MEDICALPAYMENTSOTHER IS NULL)                                                
BEGIN                                                 
 SET @MEDICALPAYMENTSOTHER=''                                           
END                                                
----                                                
IF(@UNATTACHEDEQUIPMENT IS NULL)                                                
BEGIN                                                 
 SET @UNATTACHEDEQUIPMENT=''                  
END                                          
                                         
                                        
                                              
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



   
    
     
        
           
            
             
                
                 
                  
                   
                      
                        
                           
                          
                           
                            
                               
                               
                                   
                                   
                                      
                                      
                                          
                                            
                                              
                                                
                                                  
--------------------------------                                                    
                                                    
        
SELECT                                                    
   'Y'                 AS NEWBUSINESS,                                                    
   @STATENAME          AS STATENAME,                                                    
   'N'                 AS RENEWAL,                                                    
   @QUOTEEFFDATE       AS QUOTEEFFDATE,                                          
   @QUOTEEXPDATE       AS QUOTEEXPDATE,                
   --test                
   @POL_NUMBER  AS POL_NUMBER,  ---POL_NUMBER                
   @POL_VERSION  AS POL_VERSION, ---POL_VERSION                                                       
                
   @TERRITORYCODE      AS TERRITORYCODE,                                                    
   @POLICYTERMS        AS POLICYTERMS,                                                             
   @ATTACHTOWOLVERINE  AS ATTACHTOWOLVERINE,                                                    
   @BOATHOMEDISC       AS BOATHOMEDISC,                                                    
   @INSURANCESCORE     AS INSURANCESCORE,                          
   @INSURANCESCOREDIS  AS INSURANCESCOREDIS,                                                          
   @PERSONALLIABILITY  AS PERSONALLIABILITY,                                                    
   @MEDICALPAYMENTSOTHER   AS MEDICALPAYMENTSOTHER,                                                    
   @UNATTACHEDEQUIPMENT    AS UNATTACHEDEQUIPMENT ,                          
   @MULTIPOLICYAUTOHOMEDISCOUNT AS MULTIPOLICYAUTOHOMEDISCOUNT,                        
   @MULTIPOLICYDISAPPLICABLE as MULTIPOLICYDISAPPLICABLE,                      
   @UNINSUREDBOATERS            AS UNINSUREDBOATERS ,                      
   @UNATTACHEDEQUIPMENT_DEDUCTIBLE AS UNATTACHEDEQUIPMENT_DEDUCTIBLE                                                             
END                                                  
                                                  



GO

