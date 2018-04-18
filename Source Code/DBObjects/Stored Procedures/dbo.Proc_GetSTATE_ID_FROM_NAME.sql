IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSTATE_ID_FROM_NAME]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSTATE_ID_FROM_NAME]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------      
Proc Name       : Dbo.Proc_GetSTATE_ID_FROM_NAME      
Created by      : Pradeep      
Date            : 9/5/2005      
Purpose       :Returns the StateID for the passed State code. At some places code is passed therefore
		checking for state_code too along with state_name (Nidhi)     
Revison History :      
Used In        : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE  PROCEDURE Proc_GetSTATE_ID_FROM_NAME    
(    
  @COUNTRY_ID int,    
  @STATE_CODE NVarChar(100)    
)    
AS    
    
 DECLARE @STATEID SmallInt    
     
 SELECT @STATEID = STATE_ID    
 FROM MNT_COUNTRY_STATE_LIST    
 WHERE COUNTRY_ID = @COUNTRY_ID AND    
 (STATE_NAME = @STATE_CODE or STATE_CODE = @STATE_CODE)  
     
 RETURN ISNULL(@STATEID,0)    
    
    
    
    
    
  




GO

