IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeactivateScreen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeactivateScreen]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_DeactivateScreen      
Created by      : Nidhi
Date                  : 26/05/2005      
Purpose         : set status
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_DeactivateScreen     
(      
@SCREENID INT,
@ISACTIVE NCHAR(2),
@LASTMODIFIEDDATE DATETIME,
@LASTMODIFIEDBY INT
)      
AS      
BEGIN    
   

UPDATE ONLINESCREENMASTER
SET 
	ISACTIVE=@ISACTIVE,	
	LASTMODIFIEDDATE=@LASTMODIFIEDDATE,
	LASTMODIFIEDBY  =@LASTMODIFIEDBY   
WHERE SCREENID=@SCREENID	
END


GO

