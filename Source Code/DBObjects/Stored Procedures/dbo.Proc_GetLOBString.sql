IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLOBString]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLOBString]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
----------------------------------------------------------    
Proc Name       : Vijay Arora    
Created by      : Proc_GetLOBString
Date            : 07-11-2005
Purpose     	: To Get the LOB String
Revison History :    
Used In  	: Wolverine    
    
------------------------------------------------------------    
Date        Review By          Comments    
------   ------------       -------------------------*/    
CREATE  PROC Dbo.Proc_GetLOBString    
   
 @LOB_ID      INT

AS    
BEGIN    
   
SELECT LOB_DESC FROM MNT_LOB_MASTER WHERE LOB_ID = @LOB_ID  
END    
    

  



GO

