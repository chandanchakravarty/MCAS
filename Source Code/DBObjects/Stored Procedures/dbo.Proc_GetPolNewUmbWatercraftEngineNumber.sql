IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolNewUmbWatercraftEngineNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolNewUmbWatercraftEngineNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name            : dbo.Proc_GetPolNewUmbWatercraftEngineNumber        
Created by           : Swastika        
Date                 : 26th July'06        
Purpose              : To get the new Engine Number          
Revison History      :        
Used In              :   Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--DROP PROC dbo.Proc_GetPolNewUmbWatercraftEngineNumber   
CREATE PROC dbo.Proc_GetPolNewUmbWatercraftEngineNumber        
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT,
@BOAT_NO INT       
as        
BEGIN        

 begin        
  SELECT    (isnull(MAX(ENGINE_NO),0)) +1 as ENGINE_NO  
  FROM         POL_UMBRELLA_WATERCRAFT_ENGINE_INFO WITH(NOLOCK)  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND ASSOCIATED_BOAT = @BOAT_NO       
 end        
     
END        
    




GO

