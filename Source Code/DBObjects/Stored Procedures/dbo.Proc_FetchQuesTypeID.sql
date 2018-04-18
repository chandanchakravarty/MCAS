IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchQuesTypeID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchQuesTypeID]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchQuesTypeId
Created by      	: Anurag Verma
Date            	: 6/15/2005
Purpose    	  : retrieving data from userquestions
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_FetchQuesTypeID
@QID INT,
@GROUPID INT,
@TABID INT,
@SCREENID INT
AS
BEGIN
SELECT QUESTIONTYPEID,GROUPID FROM USERQUESTIONS WHERE QID  =@QID AND GROUPID=@GROUPID AND TABID=@TABID AND SCREENID=@SCREENID
END




GO

