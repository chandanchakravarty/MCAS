IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchTabData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchTabData]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchTabData
Created by      	: Anurag Verma
Date            	: 5/26/2005
Purpose    	  : retrieving data from questiontabmaster
Revison History :
Modified by 		: Manab
Description		: Adding Reptable Controls Column
Used In 	     	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_FetchTabData
@TABID INT,
@SCREENID int

AS
BEGIN
SELECT TABNAME,ISNULL(REPEATCONTROLS,1) REPEATCONTROLS,ISACTIVE FROM QUESTIONTABMASTER WHERE TABID = @TABID and SCREENID=@SCREENID
END



GO

