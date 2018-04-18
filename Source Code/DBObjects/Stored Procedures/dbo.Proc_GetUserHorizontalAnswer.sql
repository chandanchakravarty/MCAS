IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserHorizontalAnswer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserHorizontalAnswer]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetUserHorizontalAnswer
Created by      : Nidhi
Date            : 31/05/2005
Purpose         : To get horizontal answer
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetUserHorizontalAnswer
(
@QID 	int,
@CARRIERID int,
@HQUESGRIDOPTID int,
@VQUESGRIDOPTID int,
@TABID int


 
)
AS
BEGIN
	Select OPTIONVALUE 			
	 from QuestionPrepare 			
	 Where QID =@QID AND HQUESGRIDOPTID = @HQUESGRIDOPTID AND VQUESGRIDOPTID =@VQUESGRIDOPTID
	 AND TABID =@TABID
		
END


GO

