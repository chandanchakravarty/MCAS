IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_UMBRELLA_WATERCRAFT_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_UMBRELLA_WATERCRAFT_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_SAVE_UMBRELLA_WATERCRAFT_ENDORSEMENTS      
Created by      : Pradeep      
Date            : 10/25/2005      
Purpose      : Inserts/Updates records in Umbrella Vehicle Endorsements       
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE            PROC Dbo.Proc_SAVE_UMBRELLA_WATERCRAFT_ENDORSEMENTS      
(      
  @CUSTOMER_ID     int,      
  @APP_ID     int,      
  @APP_VERSION_ID     smallint,      
  @BOAT_ID smallint,      
  @ENDORSEMENT_ID int,      
  @REMARKS NVarChar(500),    
  @VEHICLE_ENDORSEMENT_ID Int    
)      
AS      
      
DECLARE @VEHICLE_END_ID_MAX int      
    
BEGIN      
       
 IF EXISTS    
 (    
  SELECT * FROM APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS    
  WHERE CUSTOMER_ID = @CUSTOMER_ID and       
      APP_ID=@APP_ID and       
      APP_VERSION_ID = @APP_VERSION_ID       
      and BOAT_ID = @BOAT_ID  AND    
    ENDORSEMENT_ID = @ENDORSEMENT_ID    
 )    
 BEGIN    
  UPDATE APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS    
  SET REMARKS = @REMARKS    
  WHERE CUSTOMER_ID = @CUSTOMER_ID and       
      APP_ID=@APP_ID and       
      APP_VERSION_ID = @APP_VERSION_ID       
      and BOAT_ID = @BOAT_ID  AND    
    ENDORSEMENT_ID = @ENDORSEMENT_ID    
 END     
 ELSE    
 BEGIN    
      
  select  @VEHICLE_END_ID_MAX = isnull(Max(BOAT_ENDORSEMENT_ID),0)+1     
   from APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS      
    where CUSTOMER_ID = @CUSTOMER_ID and       
     APP_ID=@APP_ID and       
     APP_VERSION_ID = @APP_VERSION_ID       
     and BOAT_ID = @BOAT_ID  
     
  INSERT INTO APP_UMBRELLA_WATERCRAFT_ENDORSEMENTS    
  (    
   CUSTOMER_ID,    
   APP_ID,    
   APP_VERSION_ID,    
   BOAT_ID,    
   ENDORSEMENT_ID,    
   REMARKS,    
   BOAT_ENDORSEMENT_ID    
  )    
  VALUES    
  (    
   @CUSTOMER_ID,    
   @APP_ID,    
   @APP_VERSION_ID,    
   @BOAT_ID,    
   @ENDORSEMENT_ID,    
   @REMARKS,    
   @VEHICLE_END_ID_MAX    
  )    
     
     
 END    
END     
     
      
      
    
    
    
    
  



GO

