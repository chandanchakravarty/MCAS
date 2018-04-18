IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteWatercraftCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteWatercraftCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_DeleteWatercraftCoverage
Created by      : Ravindra
Date            : 06-06-2006
Purpose         : Delete record from  Watercraft Coverages 
Revison History :       
modified by	:Pravesh K Chandel
Modified Date	: 13 sep 20007
Purpose		: Delete Linked Endorsement of the Coverage           
Used In  : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/         
--- drop proc Proc_DeleteWatercraftCoverage              
CREATE  proc Proc_DeleteWatercraftCoverage
(                  
	@CUSTOMER_ID     int,                  
	@APP_ID     int,                  
	@APP_VERSION_ID     smallint,                  
	@VEHICLE_ID smallint,                  
	@COVERAGE_CODE VarChar(10)                 
)                  
AS                  
                  
DECLARE	@COVERAGE_CODE_ID int       
DECLARE @ENDORSEMENT_ID int
DECLARE @STATE_ID int
     
BEGIN                  
      
	SET @COVERAGE_CODE_ID = 0

	SELECT @COVERAGE_CODE_ID = ISNULL(MNT.COV_ID ,0 ),@STATE_ID=APP.STATE_ID
	FROM MNT_COVERAGE MNT INNER JOIN APP_LIST APP ON      
  		MNT.LOB_ID = 4 AND	--For watercraft only
  		MNT.STATE_ID=APP.STATE_ID 
	WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
		APP.APP_ID = @APP_ID AND      
		APP.APP_VERSION_ID = @APP_VERSION_ID and     
		MNT.COV_CODE = @COVERAGE_CODE AND       
		MNT.IS_ACTIVE = 'Y'      
      

      
	IF (  @COVERAGE_CODE_ID = 0 )      
	BEGIN      
		RETURN      
	END


               
	IF EXISTS          
	(          
		SELECT * FROM APP_WATERCRAFT_COVERAGE_INFO          
		WHERE CUSTOMER_ID = @CUSTOMER_ID and                   
		APP_ID=@APP_ID and                   
		APP_VERSION_ID = @APP_VERSION_ID                   
		and BOAT_ID = @VEHICLE_ID AND          
		COVERAGE_CODE_ID =  @COVERAGE_CODE_ID                  
	)                  
	BEGIN                  
                DELETE FROM APP_WATERCRAFT_COVERAGE_INFO          
		WHERE CUSTOMER_ID = @CUSTOMER_ID and                   
		APP_ID=@APP_ID and                   
		APP_VERSION_ID = @APP_VERSION_ID                   
		and BOAT_ID = @VEHICLE_ID AND          
		COVERAGE_CODE_ID =  @COVERAGE_CODE_ID
     ------added by Pravesh	
		  --Get the endorsementst associated with  this coverage if any           
	  SELECT  @ENDORSEMENT_ID = ENDORSMENT_ID   FROM MNT_ENDORSMENT_DETAILS MED                            
		  WHERE SELECT_COVERAGE = @COVERAGE_CODE_ID                            
		  AND STATE_ID = @STATE_ID AND                        
		   LOB_ID = 4 AND                
		   ENDORS_ASSOC_COVERAGE = 'Y' AND                
		   IS_ACTIVE='Y' 
	 EXEC Proc_Delete_APP_WATERCRAFT_ENDORSEMENT_BY_ID 
				 @CUSTOMER_ID,      
				@APP_ID,   
				@APP_VERSION_ID,           
				@ENDORSEMENT_ID , 
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

