IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyWaterCraftInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyWaterCraftInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC dbo.Proc_FetchPolicyWaterCraftInfo        
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT,        
@BOATID int=null        
AS        
if @BOATID is null         
 BEGIN        
  SELECT BOAT_ID, IsNull(MAKE,' ') + ' ' + IsNull(MODEL,'') + '(' + cast(YEAR as varchar) + ')' AS BOAT  FROM POL_WATERCRAFT_INFO WHERE         
   POLICY_ID=@POLICY_ID AND         
  POLICY_VERSION_ID=@POLICY_VERSION_ID        
  AND CUSTOMER_ID=@CUSTOMER_ID       
   AND UPPER(IS_ACTIVE)='Y'       
   
  SELECT (ISNULL(MAKE,'')  + ' ' + ISNULL(MODEL,'') + ' ' + ISNULL(SERIAL,'') + '(' + CAST(ISNULL(YEAR,'') AS VARCHAR)  +')'  )  
  AS REC_VEH, REC_VEH_ID  
 FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES   
 WHERE         
   POLICY_ID=@POLICY_ID AND  POLICY_VERSION_ID=@POLICY_VERSION_ID      
   AND CUSTOMER_ID=@CUSTOMER_ID AND ACTIVE = 'Y'    
 END        
else        
 begin        
  SELECT BOAT_ID, IsNull(MAKE,' ') + ' ' + IsNull(MODEL,'') + '(' + cast(YEAR as varchar) + ')' AS BOAT  FROM POL_WATERCRAFT_INFO WHERE         
   POLICY_ID=@POLICY_ID AND         
  POLICY_VERSION_ID=@POLICY_VERSION_ID        
  AND CUSTOMER_ID=@CUSTOMER_ID         
  and BOAT_ID=@BOATID    
  AND UPPER(IS_ACTIVE)='Y'         
end        
  



GO

