IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchQuesType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchQuesType]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchQuesType
Created by      	: Anurag Verma
Date            	: 5/27/2005
Purpose    	  : retrieving data from QUESTIONTYPE
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_FetchQuesType
@QTYPE INT
AS
BEGIN
SELECT QUESTIONTYPEID,QUESTIONTYPENAME FROM QUESTIONTYPE WHERE LEVELNUMBER=@QTYPE  ORDER BY UPPER(QUESTIONTYPENAME)
END


GO

