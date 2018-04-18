IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetStateCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetStateCode]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------
Proc Name         	 : 	Proc_GetStateCode
Created by        		 : 	kranti singh
Date               		 : 	21 Dec 2006
Purpose           		 : 
Revison History  	 :
Used In           		 :   	Wolverine  
------------------------------------------------------------
Date     			:
Review By         		:
 Comments		:
------   ------------       -------------------------*/ 


CREATE PROCEDURE dbo.Proc_GetStateCode 
(
	@StateProvCd nvarchar(5)
	
)
AS
BEGIN 

   SELECT STATE_ID from MNT_COUNTRY_STATE_LIST
   WHERE  STATE_CODE  = @StateProvCd

END

GO

