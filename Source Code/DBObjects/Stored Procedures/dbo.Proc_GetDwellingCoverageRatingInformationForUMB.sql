IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDwellingCoverageRatingInformationForUMB]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDwellingCoverageRatingInformationForUMB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                        
Proc Name           : Proc_GetDwellingCoverageRatingInformationForUMB                                                              
Created by          : Neeraj singh                                                                       
Date                : 10-01-2006                                                                        
Purpose             : To get the information for creating the input xml                                                                         
Revison History     :                                                                        
Used In             : Wolverine                                                                        
------------------------------------------------------------                                                                        
Date     Review By          Comments                                                                        
------   ------------       -------------------------*/                  
                     
 --drop PROC dbo.Proc_GetDwellingCoverageRatingInformationForUMB                                       
CREATE     PROC dbo.Proc_GetDwellingCoverageRatingInformationForUMB        
 (        
  @CUSTOMER_ID       INT,                                                                        
  @ID           INT,                                                                        
  @VERSION_ID      INT,      
  @UMBRELLA_POLICY_ID nvarchar(20),       
  @DATA_ACCESS_POINT INT,        
  @POLICY_COMPANY nvarchar(300)              
  )                                  
        
AS                                                                 
BEGIN                                                                        
                
SET QUOTED_IDENTIFIER OFF         
             
        
        
------------ UnderLyingPolicy-HOMEOWNERS LiabilityLimit--------------------                    
DECLARE @DWELLINGFIRE_POLICY_LIMIT nvarchar(100)         
DECLARE @STATE_ID SMALLINT        
DECLARE @OFFICE_PROMISES SMALLINT        
DECLARE @RENTAL_DWELLING_UNITS SMALLINT        
---- CONSTANTS        
DECLARE @LOOKUPVALUE_YES int                      
DECLARE @LOOKUPVALUE_NO int        
DECLARE @MATURE_AGE_LL int        
DECLARE @MATURE_AGE_UL int        
DECLARE @STATE_ID_MICHIGAN int        
DECLARE @STATE_ID_INDIANA int        
DECLARE @STATE_ID_WISCONSIN int        
DECLARE @POLICY int        
DECLARE @APPLICTION int        
DECLARE @OTHERS int        
DECLARE @PERSONAL_LIABILITY_MICIGAN int        
DECLARE @PERSONAL_LIABILITY_INDIANA int        
DECLARE @PERSONAL_LIABILITY_INDIANA_COV_CODE NVARCHAR(20)        
DECLARE @PERSONAL_LIABILITY_MICIGAN_COV_CODE NVARCHAR(20)        
    
set @DWELLINGFIRE_POLICY_LIMIT=0        
set @OFFICE_PROMISES=0        
set @RENTAL_DWELLING_UNITS=0        
SET @LOOKUPVALUE_YES =10963        
SET @LOOKUPVALUE_NO =10964        
SET @MATURE_AGE_LL =50        
SET @MATURE_AGE_UL =70         
SET @STATE_ID_MICHIGAN =22        
SET @STATE_ID_INDIANA =14        
SET @STATE_ID_WISCONSIN =49        
SET @POLICY =1        
SET @APPLICTION =2        
SET @OTHERS =3        
SET @PERSONAL_LIABILITY_MICIGAN =797        
SET @PERSONAL_LIABILITY_INDIANA =777        
SET @PERSONAL_LIABILITY_INDIANA_COV_CODE ='PL'        
SET @PERSONAL_LIABILITY_MICIGAN_COV_CODE ='PL'            
        
                
IF ( @DATA_ACCESS_POINT = @POLICY)        
BEGIN        
        
 SELECT @STATE_ID=STATE_ID -- Check for state id         
 FROM POL_CUSTOMER_POLICY_LIST                    
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID        
        
        
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- RANTAL DWELLING WOLVERINE FOR MICIGAN        
   BEGIN                  
           SELECT     
           @DWELLINGFIRE_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0))                  
           FROM POL_DWELLING_SECTION_COVERAGES  WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@PERSONAL_LIABILITY_MICIGAN                  
   END               
       
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- HOMEOWNERS WOLVERINE FOR INDIANA                
   BEGIN                  
             SELECT                  
             @DWELLINGFIRE_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101)                 
             FROM POL_DWELLING_SECTION_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@PERSONAL_LIABILITY_INDIANA                  
       END            
            
END        
IF ( @DATA_ACCESS_POINT = @APPLICTION)        
BEGIN        
        
 SELECT @STATE_ID=STATE_ID -- Check for state id         
 FROM APP_LIST                    
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID        
        
        
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- HOMEOWNERS WOLVERINE FOR MICIGAN        
          BEGIN                  
           SELECT @DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),ISNULL(LIMIT_1,0),101)  -- PERSONAL LIABILITY                 
           FROM APP_DWELLING_SECTION_COVERAGES          
    WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID  AND COVERAGE_CODE_ID = @PERSONAL_LIABILITY_MICIGAN                     
          END              
        
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- HOMEOWNERS WOLVERINE FOR INDIANA                
    BEGIN                  
           SELECT @DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),ISNULL(LIMIT_1,0),101)  -- PERSONAL LIABILITY                 
           FROM APP_DWELLING_SECTION_COVERAGES          
    WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID  AND COVERAGE_CODE_ID = @PERSONAL_LIABILITY_INDIANA                     
          END         
END         
        
IF ( @DATA_ACCESS_POINT = @OTHERS)        
BEGIN        
        
 SELECT @STATE_ID=STATE_ID -- Check for state id         
 FROM APP_LIST                    
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID        
        
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- HOMEOWNERS WOLVERINE FOR MICIGAN        
    BEGIN          
         SELECT           
         @DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)   -- CHECK HOMEOWNERS COVERAGE        
         FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND  POLICY_NUMBER = @UMBRELLA_POLICY_ID AND COV_CODE=@PERSONAL_LIABILITY_MICIGAN_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY         
        END         
         
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- HOMEOWNERS WOLVERINE FOR INDIANA          
    BEGIN          
       SELECT           
        @DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)    --  -- CHECK FOR HOMEOWNERS POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE        
        FROM APP_UMBRELLA_UNDERLYING_POLICIES_COVERAGES WHERE  CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID AND  POLICY_NUMBER = @UMBRELLA_POLICY_ID AND COV_CODE=@PERSONAL_LIABILITY_INDIANA_COV_CODE AND POLICY_COMPANY=@POLICY_COMPANY          
       END          
        
        
      -- number of officepromises and rentaldwellingunit          
    SELECT            
       @OFFICE_PROMISES  = CONVERT(VARCHAR(20),isnull(OFFICE_PREMISES,'0')),    -- office promises        
      @RENTAL_DWELLING_UNITS  =    CONVERT(VARCHAR(20),FAMILIES)      -- rantal Dwelling        
     FROM            
      APP_UMBRELLA_GEN_INFO  
 WITH (NOLOCK)            
     WHERE  CUSTOMER_ID=@CUSTOMER_ID  AND APP_ID = @ID AND APP_VERSION_ID = @VERSION_ID       
END        
END          
        
        
        
---------------------------RETURN VALUES--------------------------                    
BEGIN                    
SELECT          
 @DWELLINGFIRE_POLICY_LIMIT     AS DWELLINGFIREPOLICY,        
 @OFFICE_PROMISES AS OFFICE_PROMISES,        
@RENTAL_DWELLING_UNITS AS RENTAL_DWELLING_UNITS         
END                      
         
--execute Proc_GetDwellingCoverageRatingInformationForUMB 1141,25,1,1,1        
      

    
  







GO

