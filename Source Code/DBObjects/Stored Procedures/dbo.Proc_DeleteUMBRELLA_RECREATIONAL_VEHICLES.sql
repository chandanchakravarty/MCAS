IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteUMBRELLA_RECREATIONAL_VEHICLES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteUMBRELLA_RECREATIONAL_VEHICLES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name   : dbo.Proc_DeleteUMBRELLA_RECREATIONAL_VEHICLES       
Created by  : Pradeep        
Date        : 17 Jun,2005      
Purpose     :         
Revison History  :              
 ------------------------------------------------------------                    
Date     Review By          Comments                  
         
------   ------------       -------------------------*/  
--drop proc Proc_DeleteUMBRELLA_RECREATIONAL_VEHICLES
CREATE     PROCEDURE Proc_DeleteUMBRELLA_RECREATIONAL_VEHICLES
(
	
	@CUSTOMER_ID Int,
	@APP_ID Int,
	@APP_VERSION_ID SmallInt,
	@REC_VEH_ID SmallInt
)

As

DELETE FROM APP_UMBRELLA_RECREATIONAL_VEHICLES
WHERE CUSTOMER_ID = CUSTOMER_ID AND
      APP_ID = @APP_ID AND 
      APP_VERSION_ID = @APP_VERSION_ID AND
      REC_VEH_ID = @REC_VEH_ID	 		
	

RETURN 1 



GO

