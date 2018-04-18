IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForRV_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForRV_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*-----------------------------------------------------------------        
Proc Name           :  Proc_GetRatingInformationForRV_Pol                                              
Created by          :  Nidhi                                   
Date                :  Aug 22,2006                                       
Purpose             :  To get the information for creating the input xml for RV                                              
Revison History     :                                             
Used In             :  Wolverine                            
                          
Modified By         :                       
Date                :                            
Purpose             :                                      
--------------------------------------------------------------------    
Date     Review By          Comments                                              
--------------------------------------------------------------------*/              
                                           
CREATE  PROC Proc_GetRatingInformationForRV_Pol                                              
(                                              
@CUSTOMERID    int,                                              
@POL_ID    int,                                              
@POL_VERSION_ID   int  ,    
@REC_VEHICLE_ID int                                           
    
)                                              
AS                                              
                                              
BEGIN                                              
 set quoted_identifier off                                              
--Basic Policy Page--                                              
                                              
DECLARE     @STATENAME                  NVARCHAR(20)    
DECLARE     @STATE_ID                  int    
DECLARE     @LOB_ID     nvarchar(2)    
DECLARE     @QUOTEEFFDATE               NVARCHAR(20)                                              
DECLARE     @QUOTEEXPDATE               NVARCHAR(20)                                              
DECLARE     @POLICYTERMS              int                     
DECLARE     @VEHICLETYPE             NVARCHAR(100)                                                                              
DECLARE     @VEHICLETYPECODE         NVARCHAR(100)                                                                              
DECLARE     @YEAR                  NVARCHAR(12)                                                                              
DECLARE     @MAKE                 NVARCHAR(150)     
DECLARE     @MODEL                 NVARCHAR(150)                                                                              
DECLARE     @SERIAL         NVARCHAR(150)     
DECLARE     @HORSEPOWER            NVARCHAR(20)         
DECLARE     @PERSONALLIABILITY_LIMIT                NVARCHAR(20)       
DECLARE     @PERSONALLIABILITY_DEDUCTIBLE   INT                          
DECLARE     @MEDICALPAYMENTSTOOTHERS_LIMIT             NVARCHAR(20)     
DECLARE     @MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE      nvarchar(20)    
DECLARE     @MANUFACTURER          NVARCHAR(20)                                              
DECLARE     @COVG_AMOUNT           INT    
DECLARE     @DEDUCTIBLE    int          
DECLARE @CC nvarchar(50)   
    
DECLARE @STATE_INDIANA int      
DECLARE @STATE_MICHIGAN int      
DECLARE @CALLED_FROM NVARCHAR(10)    -- will be one of the lobcodes from mnt_lob_master      
    
    
SET @STATE_MICHIGAN =22    
SET @STATE_INDIANA =14                 
                        
    
    
--------CALLED FROM    
SELECT @CALLED_FROM = MLM.LOB_CODE ,@LOB_ID = AL.POLICY_LOB    
FROM POL_CUSTOMER_POLICY_LIST AL WITH(NOLOCK)   
  INNER JOIN MNT_LOB_MASTER MLM WITH(NOLOCK) ON AL.POLICY_LOB  = MLM.LOB_ID    
WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID    
    
     
    
--------------------------------------------------------------------------------               
-- STATENAME       
     
SELECT                                              
   @STATENAME= UPPER(ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'')) ,     
   @STATE_ID = POL_CUSTOMER_POLICY_LIST.STATE_ID,                                        
   @QUOTEEFFDATE = CONVERT(VARCHAR(10),APP_EFFECTIVE_DATE,101),                                        
   @QUOTEEXPDATE = CONVERT(VARCHAR(10),APP_EXPIRATION_DATE,101)   ,    
   @POLICYTERMS = APP_TERMS     
FROM                                               
 POL_CUSTOMER_POLICY_LIST WITH (NOLOCK) INNER JOIN MNT_COUNTRY_STATE_LIST  WITH (NOLOCK)     
 ON  POL_CUSTOMER_POLICY_LIST.STATE_ID=MNT_COUNTRY_STATE_LIST.STATE_ID                                               
WHERE                                               
 CUSTOMER_ID =@CUSTOMERID AND   POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID    
--------------------------------------------------------------------------------    
                    
--------------------------------------------------------------------------------                                  
----  Personal Liability                                                       
IF @STATE_ID = @STATE_INDIANA                                               
BEGIN                                                                         
 SELECT top 1    
  @PERSONALLIABILITY_LIMIT = CAST(ISNULL(LIMIT_1,'0') AS INT),                                                                        
  @PERSONALLIABILITY_DEDUCTIBLE = ISNULL(DEDUCTIBLE_1,'0.00')                                                              
 FROM                                                                               
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                             
 WHERE                                                                               
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND COVERAGE_CODE_ID = 170                                                             
END                                                                           
                                                        
IF @STATE_ID = @STATE_MICHIGAN                                                                         
BEGIN                                                                         
 SELECT     top 1                                                                    
  @PERSONALLIABILITY_LIMIT = CAST(ISNULL(LIMIT_1,'0') AS INT),                                                                        
  @PERSONALLIABILITY_DEDUCTIBLE = ISNULL(DEDUCTIBLE_1,'0.00')                                                                        
 FROM                                                                               
  POL_DWELLING_SECTION_COVERAGES   WITH (NOLOCK)                                                                     
 WHERE                                                                            
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND COVERAGE_CODE_ID = 10                                                       
END                                                                           
--------------------------------------------------------------------------------     
    
--------------------------------------------------------------------------------     
-- Medical Payments        
IF @STATE_ID = @STATE_INDIANA                                                                         
BEGIN                               
                                                            
 SELECT     top 1                                                                    
  @MEDICALPAYMENTSTOOTHERS_LIMIT =  CAST(ISNULL(LIMIT_1,'0') AS INT) ,                                                           
  @MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE = ISNULL(DEDUCTIBLE_1,'.00')                                                                        
 FROM                                                                   
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)            
 WHERE                                                                               
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND COVERAGE_CODE_ID = 171                                                                        
END                                                                        
                                                                           
IF @STATE_ID = @STATE_MICHIGAN                                           
BEGIN                                                                         
                                                                        
 SELECT   top 1                                                                      
  @MEDICALPAYMENTSTOOTHERS_LIMIT =  CAST(ISNULL(LIMIT_1,'0') AS INT) ,                                                                        
  @MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE = ISNULL(DEDUCTIBLE_1,'.00')                                                                        
 FROM                                                                               
  POL_DWELLING_SECTION_COVERAGES  WITH (NOLOCK)                                                                             
 WHERE                                                                               
  CUSTOMER_ID = @CUSTOMERID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID AND COVERAGE_CODE_ID = 13                                                                        
END       
--------------------------------------------------------------------------------    
    
    
--------------------------------------------------------------------------------     
-- Recreational Vehicles Details    
    
/* Any one of  the foll tables to be used depending on @CALLED_FROM    
APP_UMBRELLA_RECREATIONAL_VEHICLES    
APP_HOME_OWNER_RECREATIONAL_VEHICLES    
*/    
DECLARE @VEH_TYPE_UNIQUE_ID AS INT    
    
IF (@CALLED_FROM = 'HOME')    
    
 SELECT                                                                         
        
    @YEAR =  [YEAR],    
    @MAKE =  MAKE,    
    @MODEL =  MODEL,    
    @SERIAL =  SERIAL,    
    @HORSEPOWER =  HORSE_POWER,    
    @DEDUCTIBLE =  DEDUCTIBLE,        
    @MANUFACTURER =  MANUFACTURER_DESC,                                                                     
    @COVG_AMOUNT = ISNULL(INSURING_VALUE ,0),    
   @VEH_TYPE_UNIQUE_ID =VEHICLE_TYPE    ,
	@CC =isnull(HORSE_POWER, '0')        
       
                                                                  
 FROM                                                                               
    POL_HOME_OWNER_RECREATIONAL_VEHICLES  WITH (NOLOCK)            
 WHERE                                                                               
    CUSTOMER_ID = @CUSTOMERID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID and REC_VEH_ID =@REC_VEHICLE_ID    
ELSE    
 IF (@CALLED_FROM = 'UMB')    
 SELECT                                                                         
         
    @YEAR =  [YEAR],    
    @MAKE =  MAKE,    
    @MODEL =  MODEL,    
    @SERIAL =  SERIAL,    
    @HORSEPOWER =  HORSE_POWER,       
    @MANUFACTURER =  MANUFACTURER_DESC,         
    @VEH_TYPE_UNIQUE_ID =VEHICLE_TYPE,                      
    @COVG_AMOUNT = '',    
    @DEDUCTIBLE =''    
                                                                  
 FROM                                                                               
    POL_UMBRELLA_RECREATIONAL_VEHICLES  WITH (NOLOCK)            
 WHERE                                        
    CUSTOMER_ID = @CUSTOMERID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID and REC_VEH_ID =@REC_VEHICLE_ID    
--------------------------------------------------------------------------------     
                     
SELECT @VEHICLETYPE=LOOKUP_VALUE_DESC,@VEHICLETYPECODE =LOOKUP_VALUE_CODE FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=@VEH_TYPE_UNIQUE_ID    
    
DECLARE @LIABILITY NVARCHAR(30)    
DECLARE @MEDICAL_PAYMENTS NVARCHAR(30)
DECLARE @PHYSICAL_DAMAGE NVARCHAR(30)
 SELECT @LIABILITY= CASE LIABILITY WHEN 10963 THEN 'Y' ELSE 'N' END,
		@MEDICAL_PAYMENTS = CASE MEDICAL_PAYMENTS  WHEN 10963 THEN 'Y' ELSE 'N' END,
		@PHYSICAL_DAMAGE = CASE PHYSICAL_DAMAGE  WHEN 10963 THEN 'Y' ELSE 'N' END
 FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES WHERE   CUSTOMER_ID = @CUSTOMERID AND POLICY_ID=@POL_ID AND POLICY_VERSION_ID=@POL_VERSION_ID 
	and REC_VEH_ID =@REC_VEHICLE_ID  
--------------------------------------------------------------------------------        
IF(@QUOTEEFFDATE IS NULL)                 
 SET @QUOTEEFFDATE=''                                          
                                              
----                                          
IF(@QUOTEEXPDATE IS NULL)           
 SET @QUOTEEXPDATE=''                                          
                          
----                                          
--IF(@PERSONALLIABILITY_LIMIT IS NULL)       
-- SET @PERSONALLIABILITY_LIMIT=''                                          
                                          
----                                          
IF(@MEDICALPAYMENTSTOOTHERS_LIMIT IS NULL)    
 SET @MEDICALPAYMENTSTOOTHERS_LIMIT=''                                     
                                         
----                                          
                                   
                                   
                                  
                                        
-----------------------------------------------------------------------    
SELECT                                          
   @STATENAME          AS STATENAME,    
   @STATE_ID as STATE_ID,     
	@LOB_ID as LOB_ID,    
   @QUOTEEFFDATE       AS QUOTEEFFDATE,                                              
   @QUOTEEXPDATE       AS QUOTEEXPDATE,    
   @POLICYTERMS        AS POLICYTERMS,                                 
   @PERSONALLIABILITY_LIMIT    as PERSONALLIABILITY_LIMIT,                    
   @PERSONALLIABILITY_DEDUCTIBLE  as PERSONALLIABILITY_DEDUCTIBLE,                                  
   @MEDICALPAYMENTSTOOTHERS_LIMIT  as    MEDICALPAYMENTSTOOTHERS_LIMIT,             
   @MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE as MEDICALPAYMENTSTOOTHERS_DEDUCTIBLE,     
--    
     @VEHICLETYPE AS VEHICLETYPE,                                                                                
     @VEHICLETYPECODE  AS    VEHICLETYPECODE    ,                                                                               
     @YEAR     as [YEAR] ,                                                                                         
     @MAKE        as MAKE,              
     @MODEL       as MODEL,                                                                                        
     @SERIAL   as SERIAL,                                                                 
     @HORSEPOWER  as HORSEPOWER,      
     @MANUFACTURER  as MANUFACTURER ,                                                      
     isnull(@COVG_AMOUNT,0) as COVG_AMOUNT ,              
     isnull(@DEDUCTIBLE,0) as DEDUCTIBLE     ,
@CC as CC,
@LIABILITY AS LIABILITY,    
@MEDICAL_PAYMENTS AS MEDICAL_PAYMENTS, 
@PHYSICAL_DAMAGE AS PHYSICAL_DAMAGE                        
END                                            

GO

