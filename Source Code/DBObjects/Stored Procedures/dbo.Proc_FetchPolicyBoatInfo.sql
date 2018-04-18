IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyBoatInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyBoatInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC Proc_FetchPolicyBoatInfo    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID INT,    
@BOATID int=null    
AS    
if @BOATID is null     
 BEGIN    
  SELECT BOAT_ID, IsNull(MAKE,' ') + ' ' + IsNull(MODEL,'') + '(' + cast(YEAR as varchar) + ')' AS BOAT FROM POL_WATERCRAFT_INFO WHERE     
  POLICY_ID=@POLICY_ID AND     
  POLICY_VERSION_ID=@POLICY_VERSION_ID    
  AND CUSTOMER_ID=@CUSTOMER_ID
     
 END    
else    
 begin    
  SELECT BOAT_ID, IsNull(MAKE,' ') + ' ' + IsNull(MODEL,'') + '(' + cast(YEAR as varchar) + ')' AS BOAT FROM POL_WATERCRAFT_INFO WHERE     
  POLICY_ID=@POLICY_ID AND     
  POLICY_VERSION_ID=@POLICY_VERSION_ID    
  AND CUSTOMER_ID=@CUSTOMER_ID     
  and BOAT_ID=@BOATID  
 
end    


GO

