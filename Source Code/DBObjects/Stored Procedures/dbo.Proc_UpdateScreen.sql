IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateScreen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateScreen]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_UpdateScreen      
Created by      : Nidhi
Date                  : 26/05/2005      
Purpose         : To insert the data into ONLINESCREENMASTER
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_UpdateScreen     
(      
@SCREENID INT,
@CLASSID INT,
@SUBCLASSID INT,
@PROFESSIONID INT,
@SCREENNAME NVARCHAR(1000),
@ISACTIVE NCHAR(2),
@DISPLAYNAME NVARCHAR(100), 
@LASTMODIFIEDDATE DATETIME,
@LASTMODIFIEDBY INT
)      
AS      
BEGIN    
   

UPDATE ONLINESCREENMASTER
SET 
	CLASSID=@CLASSID,
	SUBCLASSID =@SUBCLASSID,
	PROFESSIONID=@PROFESSIONID,
	SCREENNAME=@SCREENNAME,
	DISPLAYNAME=@DISPLAYNAME,	
	LASTMODIFIEDDATE=@LASTMODIFIEDDATE,
	LASTMODIFIEDBY  =@LASTMODIFIEDBY   
WHERE SCREENID=@SCREENID	
END


GO

