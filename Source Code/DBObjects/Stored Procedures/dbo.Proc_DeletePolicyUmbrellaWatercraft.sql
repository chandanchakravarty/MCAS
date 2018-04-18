IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyUmbrellaWatercraft]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyUmbrellaWatercraft]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** Object:  Stored Procedure dbo.Proc_DeletePolicyUmbrellaWatercraft    Script Date: 6/2/2006 11:38:52 AM ******/
--drop proc Proc_DeletePolicyUmbrellaWatercraft        
  
/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_DeletePolicyUmbrellaWatercraft                  
Created by      : Sumit Chhabra              
Date            : 21-03-2006              
Purpose         : To delete record from POL_UMBRELLA_WATERCRAFT_INFO Table                  
Revison History :                  
Used In         : Wolverine                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
CREATE  PROC Dbo.Proc_DeletePolicyUmbrellaWatercraft                  
(                  
    @CUSTOMER_ID int,                
    @POLICY_ID int,                
    @POLICY_VERSION_ID smallint,                  
    @BOAT_ID INT                 
)                  
AS                  
BEGIN                  
            
/* DELETE FROM  POL_WATERCRAFT_COV_ADD_INT                
 WHERE                 
      CUSTOMER_ID = @CUSTOMER_ID AND                
      POLICY_ID =  @POLICY_ID AND                
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND                 
      BOAT_ID = @BOAT_ID              
            
 DELETE FROM   POL_WATERCRAFT_COVERAGE_INFO          
 WHERE                 
      CUSTOMER_ID = @CUSTOMER_ID AND                
      POLICY_ID =  @POLICY_ID AND                
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND                 
      BOAT_ID = @BOAT_ID              
          
--Endorsements        
DELETE FROM   POL_WATERCRAFT_ENDORSEMENTS          
 WHERE                 
      CUSTOMER_ID = @CUSTOMER_ID AND                
      POLICY_ID =  @POLICY_ID AND                
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND                 
      BOAT_ID = @BOAT_ID              
 */        
        
DELETE FROM  POL_UMBRELLA_WATERCRAFT_ENGINE_INFO                
 WHERE                 
      CUSTOMER_ID = @CUSTOMER_ID AND                
      POLICY_ID =  @POLICY_ID AND                
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND                 
      ASSOCIATED_BOAT  = @BOAT_ID              
            
 DELETE FROM  POL_UMBRELLA_WATERCRAFT_INFO                
 WHERE                 
      CUSTOMER_ID = @CUSTOMER_ID AND                
      POLICY_ID =  @POLICY_ID AND                
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND                 
      BOAT_ID = @BOAT_ID      
--UPDATE TABLES WHERE THIS VEHICLE IS ASSIGNED      
 UPDATE POL_UMBRELLA_DRIVER_DETAILS             
   SET OP_VEHICLE_ID=NULL          
 WHERE                 
      CUSTOMER_ID = @CUSTOMER_ID AND                
      POLICY_ID =  @POLICY_ID AND                
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND                 
      OP_VEHICLE_ID=@BOAT_ID                 
      
      
--UPDATE TABLES WHERE THIS VEHICLE IS ASSIGNED      
/* UPDATE POL_WATERCRAFT_DRIVER_DETAILS              
 SET VEHICLE_ID=NULL            
 WHERE             
   CUSTOMER_ID = @CUSTOMER_ID AND                
      POLICY_ID =  @POLICY_ID AND                
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND                 
    VEHICLE_ID = @BOAT_ID            
         
 UPDATE POL_WATERCRAFT_TRAILER_INFO          
 SET ASSOCIATED_BOAT=NULL            
 WHERE             
    CUSTOMER_ID = @CUSTOMER_ID AND                
      POLICY_ID =  @POLICY_ID AND                
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND                 
    ASSOCIATED_BOAT= @BOAT_ID           
         
 UPDATE POL_WATERCRAFT_EQUIP_DETAILLS          
 SET  ASSOCIATED_BOAT=NULL          
 WHERE          
    CUSTOMER_ID = @CUSTOMER_ID AND                
      POLICY_ID =  @POLICY_ID AND                
      POLICY_VERSION_ID =  @POLICY_VERSION_ID AND                 
    ASSOCIATED_BOAT= @BOAT_ID              
   */             
              
END                  
  




GO

