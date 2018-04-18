IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeactivateQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeactivateQuestion]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_DeactivateQuestion      
Created by      : Anurag
Date                  : 1/06/2005      
Purpose         : set status
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_DeactivateQuestion     
(      
@QID INT,
@ISACTIVE NCHAR(2),
@LASTMODIFIEDDATE DATETIME,
@LASTMODIFIEDBY INT,
@SCREENID INT,
@TABID INT,
@GRPID INT
)      
AS      
BEGIN    
   
UPDATE USERQUESTIONS 
	SET 
		LASTMODIFIEDDATE=GETDATE(),
		LASTMODIFIEDBY=@LASTMODIFIEDBY,
		ISACTIVE=@ISACTIVE 
	WHERE 
	QID=@QID AND SCREENID=@SCREENID AND
	GROUPID=@GRPID AND TABID=@TABID
END


GO

