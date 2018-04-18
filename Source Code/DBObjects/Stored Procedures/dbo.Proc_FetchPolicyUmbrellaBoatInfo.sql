IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyUmbrellaBoatInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyUmbrellaBoatInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_FetchPolicyUmbrellaBoatInfo        
Created by     : Sumit Chhabra  
Date            : 21-03-2003    
Purpose        : retrieving data from POL_UMBRELLA_WATERCRAFT_INFO         
Revison History :        
Used In         : Wolverine        
*/     
CREATE   PROC Dbo.Proc_FetchPolicyUmbrellaBoatInfo        
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT,        
@BOATID int=null        
AS        
if @BOATID is null         
 BEGIN        
  SELECT BOAT_ID, MAKE + ' ' + MODEL AS BOAT  FROM POL_UMBRELLA_WATERCRAFT_INFO WHERE         
  POLICY_ID=@POLICY_ID AND         
  POLICY_VERSION_ID=@POLICY_VERSION_ID        
  AND CUSTOMER_ID=@CUSTOMER_ID    
	AND IS_ACTIVE='Y'
         
 END        
else        
 begin        
  SELECT BOAT_ID, MAKE + ' ' + MODEL AS BOAT  FROM POL_UMBRELLA_WATERCRAFT_INFO WHERE         
  POLICY_ID=@POLICY_ID AND         
  POLICY_VERSION_ID=@POLICY_VERSION_ID        
  AND CUSTOMER_ID=@CUSTOMER_ID         
  and BOAT_ID=@BOATID      
     
end        
        
      
    
    



GO

