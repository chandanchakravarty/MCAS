IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DELETE_POLICY_VEHICLE_ENDORSEMENTS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DELETE_POLICY_VEHICLE_ENDORSEMENTS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.Proc_DELETE_POLICY_VEHICLE_ENDORSEMENTS    
Created by      : Pradeep    
Date            : 2/22/2006    
Purpose      : Deletes records in Policy Vehicle Endorsements     
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE            PROC Dbo.Proc_DELETE_POLICY_VEHICLE_ENDORSEMENTS    
(    
  @CUSTOMER_ID     int,    
  @POLICY_ID     int,    
  @POLICY_VERSION_ID     smallint,    
  @VEHICLE_ID smallint,    
  @ENDORSEMENT_ID int,    
  @VEHICLE_ENDORSEMENT_ID Int  
)    
AS    
    
  
  
BEGIN    
     
 IF EXISTS  
 (  
  SELECT * FROM POL_VEHICLE_ENDORSEMENTS  
  WHERE CUSTOMER_ID = @CUSTOMER_ID and     
      POLICY_ID = @POLICY_ID and     
      POLICY_VERSION_ID = @POLICY_VERSION_ID     
      and VEHICLE_ID = @VEHICLE_ID  AND  
    ENDORSEMENT_ID = @ENDORSEMENT_ID AND  
      VEHICLE_ENDORSEMENT_ID = @VEHICLE_ENDORSEMENT_ID  
 )  
 BEGIN  
  DELETE FROM POL_VEHICLE_ENDORSEMENTS  
  WHERE CUSTOMER_ID = @CUSTOMER_ID and     
      POLICY_ID = @POLICY_ID and     
      POLICY_VERSION_ID = @POLICY_VERSION_ID     
      and VEHICLE_ID = @VEHICLE_ID  AND  
    ENDORSEMENT_ID = @ENDORSEMENT_ID AND  
      VEHICLE_ENDORSEMENT_ID = @VEHICLE_ENDORSEMENT_ID  
 END   
   
END   
   
    
    
  
  
  
  



GO

