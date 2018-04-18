IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_POLICY_WATERCRAFT_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_POLICY_WATERCRAFT_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_SAVE_POLICY_WATERCRAFT_ENDORSEMENTS      
Created by      : shafi  
Date            : 16/02/06  
Purpose      : Inserts/Updates records in Vehicle_Endorsements       
Revison History :      
Used In  : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
drop proc dbo.Proc_SAVE_POLICY_WATERCRAFT_ENDORSEMENTS      
------   ------------       -------------------------*/      
CREATE            PROC dbo.Proc_SAVE_POLICY_WATERCRAFT_ENDORSEMENTS      
(      
  @CUSTOMER_ID     int,      
  @POL_ID     int,      
  @POL_VERSION_ID     smallint,      
  @BOAT_ID smallint,      
  @ENDORSEMENT_ID int,      
  @REMARKS NVarChar(500),    
  @VEHICLE_ENDORSEMENT_ID Int,
  @EDITION_DATE varchar(10)=null    
)      
AS      
      
DECLARE @VEHICLE_END_ID_MAX int      
    
BEGIN      
       
 IF EXISTS    
 (    
  SELECT * FROM POL_WATERCRAFT_ENDORSEMENTS    
  WHERE CUSTOMER_ID = @CUSTOMER_ID and       
      POLICY_ID=@POL_ID and       
      POLICY_VERSION_ID = @POL_VERSION_ID       
      and BOAT_ID = @BOAT_ID  AND    
      ENDORSEMENT_ID = @ENDORSEMENT_ID    
 )    
 BEGIN    
  UPDATE POL_WATERCRAFT_ENDORSEMENTS    
  SET REMARKS = @REMARKS,EDITION_DATE=@EDITION_DATE    
  WHERE CUSTOMER_ID = @CUSTOMER_ID and       
      POLICY_ID=@POL_ID and       
      POLICY_VERSION_ID = @POL_VERSION_ID       
      and BOAT_ID = @BOAT_ID  AND    
      ENDORSEMENT_ID = @ENDORSEMENT_ID    
 END     
 ELSE    
 BEGIN    
      
  select  @VEHICLE_END_ID_MAX = isnull(Max(VEHICLE_ENDORSEMENT_ID),0)+1     
   from POL_WATERCRAFT_ENDORSEMENTS      
    where CUSTOMER_ID = @CUSTOMER_ID and       
     POLICY_ID=@POL_ID and       
     POLICY_VERSION_ID = @POL_VERSION_ID       
     and BOAT_ID = @BOAT_ID  
     
  INSERT INTO POL_WATERCRAFT_ENDORSEMENTS    
  (    
   CUSTOMER_ID,    
   POLICY_ID,    
   POLICY_VERSION_ID,    
   BOAT_ID,    
   ENDORSEMENT_ID,    
   REMARKS,    
   VEHICLE_ENDORSEMENT_ID ,
   EDITION_DATE   
  )    
  VALUES    
  (    
   @CUSTOMER_ID,    
   @POL_ID,    
   @POL_VERSION_ID,    
   @BOAT_ID,    
   @ENDORSEMENT_ID,    
   @REMARKS,    
   @VEHICLE_END_ID_MAX    ,
@EDITION_DATE
  )    
     
     
 END    
END     
     
      
      
    
    
    
    
  
  
  



GO

