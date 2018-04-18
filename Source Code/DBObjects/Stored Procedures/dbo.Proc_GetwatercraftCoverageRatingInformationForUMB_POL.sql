IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetwatercraftCoverageRatingInformationForUMB_POL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetwatercraftCoverageRatingInformationForUMB_POL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                                      
Proc Name           : Proc_GetwatercraftCoverageRatingInformationForUMB_POL                                                            
Created by          : Neeraj singh                                                                     
Date                : 10-01-2006                                                                      
Purpose             : To get the information for creating the input xml                                                                       
Revison History     :                                                                      
modified by  :prevesh K Chandel  
Modified Date  :8 june 2007  
purpose   : apply no lock condition  
Used In             : Wolverine                                                                      


Reviewed By	:	Anurag Verma
Reviewed On	:	06-07-2007
------------------------------------------------------------                                                                      
Date     Review By          Comments                                                                      
------   ------------       -------------------------*/                
                   
-- DROP PROC dbo.Proc_GetwatercraftCoverageRatingInformationForUMB_POL                                     
CREATE PROC dbo.Proc_GetwatercraftCoverageRatingInformationForUMB_POL      
 (      
  @CUSTOMER_ID       	INT,                                                                      
  @ID           	INT,                                                                      
  @VERSION_ID      	INT,    
  @UMBRELLA_POLICY_ID 	nvarchar(20),      
  @DATA_ACCESS_POINT 	INT,  
  @POLICY_COMPANY	NVARCHAR(300)
  )                                
      
AS                                                               
BEGIN                                                                      
              
SET QUOTED_IDENTIFIER OFF       
           
      
      
------------ UnderLyingPolicy-HOMEOWNERS LiabilityLimit--------------------                  
DECLARE @WATERCRAFT_POLICY_LIMIT nvarchar(100)       
DECLARE @STATE_ID SMALLINT      
      
      
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
DECLARE @WATERCRAFT_LIABILITY_MICIGAN int      
DECLARE @WATERCRAFT_LIABILITY_INDIANA int      
DECLARE @WATERCRAFT_LIABILITY_INDIANA_COV_CODE NVARCHAR(20)      
DECLARE @WATERCRAFT_LIABILITY_MICIGAN_COV_CODE NVARCHAR(20)      
      
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
SET @WATERCRAFT_LIABILITY_MICIGAN =65      
SET @WATERCRAFT_LIABILITY_INDIANA =19      
SET @WATERCRAFT_LIABILITY_INDIANA_COV_CODE ='LCCSL'      
SET @WATERCRAFT_LIABILITY_MICIGAN_COV_CODE ='LCCSL'          
      
              
IF ( @DATA_ACCESS_POINT = @POLICY)      
BEGIN      
      
 SELECT @STATE_ID=STATE_ID -- Check for state id       
 FROM POL_CUSTOMER_POLICY_LIST  with(nolock)                
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID      
      
      
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- WATERCRAFT LIABILITY WOLVERINE FOR MICIGAN      
   BEGIN                
          SELECT                
           @WATERCRAFT_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101)                
           FROM POL_WATERCRAFT_COVERAGE_INFO  with(nolock) WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@WATERCRAFT_LIABILITY_MICIGAN                
        END             
      
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- WATERCRAFT LIABILITY WOLVERINE FOR INDIANA              
   BEGIN                
             SELECT                
            @WATERCRAFT_POLICY_LIMIT = convert(varchar(20),ISNULL(LIMIT_1,0),101)                
             FROM POL_WATERCRAFT_COVERAGE_INFO with(nolock) WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID = @ID and POLICY_VERSION_ID = @VERSION_ID AND COVERAGE_CODE_ID=@WATERCRAFT_LIABILITY_INDIANA                
          END          
          
END      
IF ( @DATA_ACCESS_POINT = @APPLICTION)      
BEGIN      
      
 SELECT @STATE_ID=STATE_ID -- Check for state id       
 FROM APP_LIST      with(nolock)            
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@ID AND APP_VERSION_ID=@VERSION_ID      
      
      
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- WATERCRAFT LIABILITY WOLVERINE FOR MICIGAN      
           BEGIN                
            SELECT @WATERCRAFT_POLICY_LIMIT= convert(varchar(20),ISNULL(LIMIT_1,0),101)  -- WATERCRAFT LIABILITY               
            FROM APP_WATERCRAFT_COVERAGE_INFO        
     WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID  AND COVERAGE_CODE_ID = @WATERCRAFT_LIABILITY_MICIGAN                   
           END            
      
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- WATERCRAFT LIABILITY WOLVERINE FOR INDIANA               
    BEGIN                
            SELECT @WATERCRAFT_POLICY_LIMIT= convert(varchar(20),ISNULL(LIMIT_1,0),101)  -- WATERCRAFT LIABILITY               
            FROM APP_WATERCRAFT_COVERAGE_INFO   with(nolock)      
     WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID = @ID and APP_VERSION_ID = @VERSION_ID  AND COVERAGE_CODE_ID = @WATERCRAFT_LIABILITY_INDIANA                   
          END       
END       
      
IF ( @DATA_ACCESS_POINT = @OTHERS)      
BEGIN      
      
 SELECT @STATE_ID=STATE_ID -- Check for state id       
 FROM POL_CUSTOMER_POLICY_LIST with(nolock)                 
      WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@ID AND POLICY_VERSION_ID=@VERSION_ID      
      
 IF (@STATE_ID = @STATE_ID_MICHIGAN)   -- WATERCRAFT LIABILITY WOLVERINE FOR MICIGAN      
    BEGIN        
          SELECT         
          @WATERCRAFT_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)   -- WATERCRAFT COVERAGE      
          FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES PUUPC with(nolock)
	  INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES PUUP ON PUUPC.CUSTOMER_ID= PUUP.CUSTOMER_ID AND PUUPC.POLICY_ID= PUUP.POLICY_ID AND PUUPC.POLICY_VERSION_ID= PUUP.POLICY_VERSION_ID AND PUUP.POLICY_COMPANY=@POLICY_COMPANY
	  WHERE  PUUPC.CUSTOMER_ID=@CUSTOMER_ID and PUUPC.POLICY_ID = @ID and PUUPC.POLICY_VERSION_ID = @VERSION_ID AND PUUPC.POLICY_NUMBER = @UMBRELLA_POLICY_ID AND COV_CODE=@WATERCRAFT_LIABILITY_MICIGAN_COV_CODE AND PUUP.POLICY_COMPANY=@POLICY_COMPANY   
 
   
        END       
       
 IF (@STATE_ID = @STATE_ID_INDIANA)   -- WATERCRAFT LIABILITY WOLVERINE FOR INDIANA        
    BEGIN        
         SELECT         
          @WATERCRAFT_POLICY_LIMIT= convert(varchar(20),COVERAGE_AMOUNT,101)    --  -- WATERCRAFT COVERAGE      
          FROM POL_UMBRELLA_UNDERLYING_POLICIES_COVERAGES PUUPC with(nolock)
	  INNER JOIN POL_UMBRELLA_UNDERLYING_POLICIES PUUP ON PUUPC.CUSTOMER_ID= PUUP.CUSTOMER_ID AND PUUPC.POLICY_ID= PUUP.POLICY_ID AND PUUPC.POLICY_VERSION_ID= PUUP.POLICY_VERSION_ID AND PUUP.POLICY_COMPANY=@POLICY_COMPANY
	  WHERE  PUUPC.CUSTOMER_ID=@CUSTOMER_ID and PUUPC.POLICY_ID = @ID and PUUPC.POLICY_VERSION_ID = @VERSION_ID AND PUUPC.POLICY_NUMBER = @UMBRELLA_POLICY_ID AND PUUPC.COV_CODE=@WATERCRAFT_LIABILITY_INDIANA_COV_CODE AND PUUP.POLICY_COMPANY=@POLICY_COMPANY   
   
        END        
END      
END        
      
      
      
---------------------------RETURN VALUES--------------------------                  
BEGIN                  
SELECT        
  @WATERCRAFT_POLICY_LIMIT     AS WATERCRAFTPOLICY      
END                    
       
    
  
  
  





GO

