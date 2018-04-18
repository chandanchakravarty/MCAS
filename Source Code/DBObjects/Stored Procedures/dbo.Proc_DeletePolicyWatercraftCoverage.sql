IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyWatercraftCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyWatercraftCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_DeletePolicyWatercraftCoverage
Created by      : Ravindra
Date            : 06-09-2006
Purpose         : Delete record from  Watercraft Coverages (Policy Level)
Revison History :    
Modified By	: Pravesh K chandel              
Modified Date	: 13 sep 2007
Purpose		: delete linked Endorsement of the coverage
Used In  : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/         
--- drop proc Proc_DeletePolicyWatercraftCoverage              
CREATE  proc Proc_DeletePolicyWatercraftCoverage
(                  
	@CUSTOMER_ID     int,                  
	@POLICY_ID     int,                  
	@POLICY_VERSION_ID     smallint,                  
	@VEHICLE_ID smallint,                  
	@COVERAGE_CODE VarChar(10)                 
)                  
AS                  
                  
DECLARE	@COVERAGE_CODE_ID int  
declare @STATE_ID     int
declare  @ENDORSEMENT_ID int 
BEGIN                  
      
	SET @COVERAGE_CODE_ID = 0

	SELECT @COVERAGE_CODE_ID = ISNULL(MNT.COV_ID ,0 ),@STATE_ID=POL.STATE_ID
	FROM MNT_COVERAGE MNT INNER JOIN POL_CUSTOMER_POLICY_LIST POL ON      
  		MNT.LOB_ID = 4 AND	--For watercraft only
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
		SELECT * FROM POL_WATERCRAFT_COVERAGE_INFO          
		WHERE CUSTOMER_ID = @CUSTOMER_ID and                   
		POLICY_ID=@POLICY_ID and                   
		POLICY_VERSION_ID = @POLICY_VERSION_ID                   
		and BOAT_ID = @VEHICLE_ID AND          
		COVERAGE_CODE_ID =  @COVERAGE_CODE_ID                  
	)                  
	BEGIN                  
                DELETE FROM POL_WATERCRAFT_COVERAGE_INFO          
		WHERE CUSTOMER_ID = @CUSTOMER_ID and                   
		POLICY_ID=@POLICY_ID and                   
		POLICY_VERSION_ID = @POLICY_VERSION_ID                   
		and BOAT_ID = @VEHICLE_ID AND          
		COVERAGE_CODE_ID =  @COVERAGE_CODE_ID
	  ------added by Pravesh	
		  --Get the endorsementst associated with  this coverage if any  and delete it         
	 	 SELECT  @ENDORSEMENT_ID = ENDORSMENT_ID   FROM MNT_ENDORSMENT_DETAILS MED                            
		  WHERE SELECT_COVERAGE = @COVERAGE_CODE_ID                            
		  AND STATE_ID = @STATE_ID AND                        
		   LOB_ID = 4 AND                
		   ENDORS_ASSOC_COVERAGE = 'Y' AND                
		   IS_ACTIVE='Y' 
		EXEC Proc_Delete_POL_WATERCRAFT_ENDORSEMENT_BY_ID          
			@CUSTOMER_ID,               
			@POLICY_ID,                 
			@POLICY_VERSION_ID,                    
			@ENDORSEMENT_ID,                
			@VEHICLE_ID               
	 -----end here
		
	END                   
            
	IF @@ERROR <> 0      
	BEGIN      
 		RAISERROR ('Unable to remove coverage from Watercraft.', 16, 1)      
      
	END           
	--************************************************************     
  

              
END






GO

