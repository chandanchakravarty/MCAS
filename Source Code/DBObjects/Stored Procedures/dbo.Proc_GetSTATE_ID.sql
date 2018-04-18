IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSTATE_ID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSTATE_ID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------    
Proc Name       : Dbo.Proc_GetSTATE_ID    
Created by      : Pradeep    
Date            : 9/5/2005    
Purpose       :Returns the StateID for the passed State code   
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE  PROCEDURE dbo.Proc_GetSTATE_ID  
(  
	 @COUNTRY_ID int,  
	 @STATE_CODE NVarChar(10)  
)  
AS  
  
	DECLARE @STATEID SmallInt  
	  
	SELECT @STATEID = STATE_ID  
	FROM MNT_COUNTRY_STATE_LIST  
	WHERE COUNTRY_ID = @COUNTRY_ID AND  
	 STATE_CODE = @STATE_CODE    
	  
	RETURN ISNULL(@STATEID,0)

GO

