IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetFrquency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetFrquency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name        : dbo.Proc_GetFrquency        
Created by       : Vijay Arora  
Date             : 28-03-2006
Purpose       	 : Get the Frequency for Journal Entries.
Revison History  :        
Used In    	 : Wolverine         
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/         
-- drop proc Proc_GetFrquency
CREATE PROCEDURE Proc_GetFrquency  
AS        
BEGIN        
	Select FREQUENCY_CODE, FREQUENCY_DESCRIPTION FROM ACT_FREQUENCY_MASTER
END        
  
        



GO

