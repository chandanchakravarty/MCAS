IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteUMBRELLA_REC_VEH_COV]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteUMBRELLA_REC_VEH_COV]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO












/*----------------------------------------------------------        
Proc Name   : dbo.Proc_DeleteHOME_OWNER_REC_VEH_COV       
Created by  : Pradeep        
Date        : 16 June,2005      
Purpose     :         
Revison History  :              
 ------------------------------------------------------------                    
Date     Review By          Comments                  
         
------   ------------       -------------------------*/  

CREATE      PROCEDURE Proc_DeleteUMBRELLA_REC_VEH_COV
(	
	@CUSTOMER_ID     int,
	@APP_ID     int,
	@APP_VERSION_ID     smallint,
	@REC_VEH_ID smallint,
	@COVERAGE_ID int
)

As

DELETE FROM APP_UMBRELLA_RECREATIONAL_VEHICLES_COVERAGES
WHERE CUSTOMER_ID = @CUSTOMER_ID AND
	APP_ID = @APP_ID AND
	APP_VERSION_ID = @APP_VERSION_ID AND
	REC_VEH_ID = @REC_VEH_ID AND
	COVERAGE_ID = @COVERAGE_ID
	

RETURN 1 	 












GO

