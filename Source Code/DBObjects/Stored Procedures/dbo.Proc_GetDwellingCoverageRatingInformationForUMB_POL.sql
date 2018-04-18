IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDwellingCoverageRatingInformationForUMB_POL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDwellingCoverageRatingInformationForUMB_POL]
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


Reviewed By	:	Anurag Verma
Reviewed On	:	06-07-2007
------------------------------------------------------------                                                                            
Date     Review By          Comments                                                                            
------   ------------       -------------------------*/                      
                      
-- DROP    PROC dbo.Proc_GetDwellingCoverageRatingInformationForUMB_POL                                                      
CREATE     PROC dbo.Proc_GetDwellingCoverageRatingInformationForUMB_POL            
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
 FROM POL_CUSTOMER_POLICY_LIST with(nolock)                       
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID            
            
            
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- RANTAL DWELLING WOLVERINE FOR MICIGAN            
   BEGIN                      
           SELECT         
           @DWELLINGFIRE_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0))                      
           FROM POL_DWELLING_SECTION_COVERAGES with(nolock) WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@PERSONAL_LIABILITY_MICIGAN                      
   END                   
           
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- HOMEOWNERS WOLVERINE FOR INDIANA                    
   BEGIN                      
             SELECT                      
             @DWELLINGFIRE_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101)                     
             FROM POL_DWELLING_SECTION_COVERAGES with(nolock) WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@PERSONAL_LIABILITY_INDIANA                      
       END                
                
END            
IF ( @DATA_ACCESS_POINT = @APPLICTION)            
BEGIN            
            
 SELECT @STATE_ID=STATE_ID -- Check for state id             
 FROM APP_LIST  with(nolock)                      
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID            
            
            
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- HOMEOWNERS WOLVERINE FOR MICIGAN            
          BEGIN                      
           SELECT @DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),ISNULL(LIMIT_1,0),101)  -- PERSONAL LIABILITY                     
           FROM APP_DWELLING_SECTION_COVERAGES with(nolock)             
    WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID  AND COVERAGE_CODE_ID = @PERSONAL_LIABILITY_MICIGAN                         
          END                  
            
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- HOMEOWNERS WOLVERINE FOR INDIANA                    
    BEGIN                      
           SELECT @DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),ISNULL(LIMIT_1,0),101)  -- PERSONAL LIABILITY                     
           FROM APP_DWELLING_SECTION_COVERAGES  with(nolock)            
    WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID  AND COVERAGE_CODE_ID = @PERSONAL_LIABILITY_INDIANA                         
          END             
END             
            
IF ( @DATA_ACCESS_POINT = @OTHERS)            
BEGIN            
            
 SELECT @STATE_ID=STATE_ID -- Check for state id             
 FROM POL_CUSTOMER_POLICY_LIST with(nolock)                       
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID            
            
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- HOMEOWNERS WOLVERINE FOR MICIGAN            
    BEGIN              
         SELECT               
         @DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)   -- CHECK HOMEOWNERS COVERAGE            
         FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES PUUPC with(nolock) 
	INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES PUUP
	ON PUUPC.CUSTOMER_ID= PUUP.CUSTOMER_ID AND PUUPC.POLICY_ID = PUUP.POLICY_ID AND PUUPC.POLICY_VERSION_ID= PUUP.POLICY_VERSION_ID
	WHERE  PUUPC.CUSTOMER_ID=@CUSTOMER_ID and PUUPC.POLICY_ID = @ID and PUUPC.POLICY_VERSION_ID = @VERSION_ID 
	AND  PUUPC.POLICY_NUMBER = @UMBRELLA_POLICY_ID AND PUUPC.COV_CODE=@PERSONAL_LIABILITY_MICIGAN_COV_CODE       
	AND PUUP.POLICY_COMPANY=@POLICY_COMPANY

       
        END             
             
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- HOMEOWNERS WOLVERINE FOR INDIANA              
    BEGIN              
       SELECT               
        @DWELLINGFIRE_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)    --  -- CHECK FOR HOMEOWNERS POLICY LIMIT FOR STATE INDIANA IN OTHERS CASE            
        FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES PUUPC with(nolock)
	INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES PUUP
	ON PUUPC.CUSTOMER_ID= PUUP.CUSTOMER_ID AND PUUPC.POLICY_ID = PUUP.POLICY_ID AND PUUPC.POLICY_VERSION_ID= PUUP.POLICY_VERSION_ID
	WHERE  PUUPC.CUSTOMER_ID=@CUSTOMER_ID and PUUPC.POLICY_ID = @ID and PUUPC.POLICY_VERSION_ID = @VERSION_ID AND  PUUPC.POLICY_NUMBER = @UMBRELLA_POLICY_ID 
	AND PUUPC.COV_CODE=@PERSONAL_LIABILITY_INDIANA_COV_CODE AND PUUP.POLICY_COMPANY=@POLICY_COMPANY       

      
       END              
            
                    
END            
END              
            
            
            
---------------------------RETURN VALUES--------------------------                        
BEGIN                        
SELECT              
 @DWELLINGFIRE_POLICY_LIMIT     AS DWELLINGFIREPOLICY            
END                          
             
            
          
    
        
      
    
  
  
  





GO

