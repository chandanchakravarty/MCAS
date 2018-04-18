IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteUmbrellaWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteUmbrellaWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** Object:  Stored Procedure dbo.Proc_DeleteUmbrellaWatercraft    Script Date: 5/25/2006 11:48:09 AM ******/
/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DeleteUmbrellaWatercraft                
Created by      : Sumit Chhabra                
Date            : 16 Nov,2005                
Purpose         : To delete record from APP_UMBRELLA_WATERCRAFT_INFO Table                
Revison History :                
Used In         :   wolverine    
  
Modified By     : shafi  
Modified On     : 17 jan 2006  
Purpose         : Delete from APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS,APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO and check for relationship  

  
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/   
--drop proc Proc_DeleteUmbrellaWatercraft                             
CREATE  PROC Dbo.Proc_DeleteUmbrellaWatercraft                
(                
    @CUSTOMER_ID int,              
    @APP_ID int,              
    @APP_VERSION_ID smallint,                
    @BOAT_ID INT               
)                
AS                
BEGIN                
   
--Delete APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS  
/*DELETE FROM APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS  
WHERE  
CUSTOMER_ID =@CUSTOMER_ID AND              
APP_ID      = @APP_ID AND    
APP_VERSION_ID=@APP_VERSION_ID AND    
BOAT_ID = @BOAT_ID   */
  
--DELETE FROM APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  
DELETE FROM APP_UMBRELLA_WATERCRAFT_COVERAGE_INFO  
WHERE  
CUSTOMER_ID =@CUSTOMER_ID AND              
APP_ID      = @APP_ID AND    
APP_VERSION_ID=@APP_VERSION_ID AND    
BOAT_ID = @BOAT_ID   
--Delete Engine Info  
 DELETE FROM APP_WATERCRAFT_ENGINE_INFO        
  WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      APP_ID =  @APP_ID AND              
      APP_VERSION_ID =  @APP_VERSION_ID AND               
      ASSOCIATED_BOAT=@BOAT_ID   
  
 DELETE FROM  APP_UMBRELLA_WATERCRAFT_INFO            
 WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      APP_ID =  @APP_ID AND              
      APP_VERSION_ID =  @APP_VERSION_ID AND               
      BOAT_ID = @BOAT_ID                
 UPDATE APP_UMBRELLA_DRIVER_DETAILS   SET OP_VEHICLE_ID=NULL              
 WHERE               
      CUSTOMER_ID = @CUSTOMER_ID AND              
      APP_ID =  @APP_ID AND              
      APP_VERSION_ID =  @APP_VERSION_ID AND               
      OP_VEHICLE_ID = @BOAT_ID           
               
END      



GO

