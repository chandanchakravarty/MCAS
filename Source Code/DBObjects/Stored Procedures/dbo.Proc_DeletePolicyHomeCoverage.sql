IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyHomeCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyHomeCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_DeletePolicyHomeCoverage
Created by      : Ravindra
Date            : 06-23-2006
Purpose         : Delete record from  Home/Rental Coverages (Policy Level)
Revison History :                  
Used In  : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/         
--- drop proc Proc_DeletePolicyHomeCoverage
CREATE   proc dbo.Proc_DeletePolicyHomeCoverage
(                  
	@CUSTOMER_ID     int,                  
	@POLICY_ID     int,                  
	@POLICY_VERSION_ID     smallint,                  
	@DWELLING_ID smallint,                  
	@COVERAGE_CODE VarChar(10)                 
)                  
AS                  
                  
DECLARE	@COVERAGE_CODE_ID int       
     
BEGIN                  
      
	SET @COVERAGE_CODE_ID = 0

	SELECT @COVERAGE_CODE_ID = ISNULL(MNT.COV_ID ,0 )
	FROM MNT_COVERAGE MNT INNER JOIN POL_CUSTOMER_POLICY_LIST POL ON      
  		MNT.LOB_ID = POL.POLICY_LOB AND 
  		MNT.STATE_ID=POL.STATE_ID 
	WHERE   POL.CUSTOMER_ID = @CUSTOMER_ID AND      
		POL.POLICY_ID = @POLICY_ID AND      
		POL.POLICY_VERSION_ID = @POLICY_VERSION_ID and     
		MNT.COV_CODE = @COVERAGE_CODE AND       
		MNT.IS_ACTIVE = 'Y'      
      

      
	IF (  @COVERAGE_CODE_ID = 0 )      
	BEGIN      
		RETURN      
	END


               
	IF EXISTS          
	(          
		SELECT COVERAGE_CODE_ID FROM POL_DWELLING_SECTION_COVERAGES
		WHERE   CUSTOMER_ID = @CUSTOMER_ID and                   
			POLICY_ID=@POLICY_ID and                   
			POLICY_VERSION_ID = @POLICY_VERSION_ID                   
			and DWELLING_ID = @DWELLING_ID AND          
			COVERAGE_CODE_ID =  @COVERAGE_CODE_ID                  
	)           
	BEGIN                  
                DELETE FROM POL_DWELLING_SECTION_COVERAGES          
		WHERE CUSTOMER_ID = @CUSTOMER_ID and                   
		POLICY_ID=@POLICY_ID and                   
		POLICY_VERSION_ID = @POLICY_VERSION_ID                   
		and DWELLING_ID = @DWELLING_ID AND             
		COVERAGE_CODE_ID =  @COVERAGE_CODE_ID
        --Delete Linked Enforsments
        EXEC Proc_DELETE_LINKED_POL_HOME_ENDORSEMENTS @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID, 
        @DWELLING_ID,@COVERAGE_CODE_ID    
	END                   
            
	IF @@ERROR <> 0      
	BEGIN      
 		RAISERROR ('Unable to remove coverage from Dwelling.', 16, 1)      
      
	END           
	--************************************************************     

              
END








GO

