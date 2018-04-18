IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteUmbrellaFarmInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteUmbrellaFarmInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
            
Proc Name       : Proc_DeleteUmbrellaFarmInfo            
Created by      : Sumit Chhabra            
Date            : 23/03/2006            
Purpose         : Delete data of Farm Info
Revison History :            
Used In         : Wolverine            
    
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE PROC Proc_DeleteUmbrellaFarmInfo
(    
 @CUSTOMER_ID              int,    
 @APP_ID                   int,    
 @APP_VERSION_ID           smallint,    
 @FARM_ID               smallint     
)    
AS    
    
BEGIN    

	DELETE FROM APP_UMBRELLA_FARM_INFO WHERE   		
		CUSTOMER_ID=@CUSTOMER_ID AND       
		APP_ID=@APP_ID AND                   
		APP_VERSION_ID=@APP_VERSION_ID AND  
		FARM_ID =@FARM_ID       
  
END  


GO

