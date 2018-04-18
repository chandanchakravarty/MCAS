IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertScreen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertScreen]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------      
Proc Name     : dbo.Proc_InsertScreen      
Created by      : Anurag Verma      
Date                  : 25/05/2005      
Purpose         : To insert the data into ONLINESCREENMASTER
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE  PROC Dbo.Proc_InsertScreen     
(      
@SCREENID INT OUTPUT,
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
 /*Check for Unique Code of Agency  */    
 Declare @Count numeric    
  SELECT @Count = ISNULL(MAX(SCREENID),0)+1   FROM ONLINESCREENMASTER 
     
  IF NOT EXISTS(SELECT 1 FROM ONLINESCREENMASTER  WHERE CLASSID = @CLASSID AND SUBCLASSID=@SUBCLASSID)
  BEGIN

	  INSERT INTO ONLINESCREENMASTER
	  (     
		SCREENID,
		CLASSID,
		SUBCLASSID,
		PROFESSIONID,
		SCREENNAME,
		ISACTIVE,
		DISPLAYNAME,	
		LASTMODIFIEDDATE,
		LASTMODIFIEDBY     
	  )      
	  VALUES      
	  (      
		@COUNT,
		@CLASSID,
		@SUBCLASSID,
		@PROFESSIONID,
		@SCREENNAME,
		@ISACTIVE,
		@DISPLAYNAME,
		@LASTMODIFIEDDATE,
		@LASTMODIFIEDBY
	 )   

	SELECT @SCREENID =@Count
 END
 ELSE
	SELECT @SCREENID =-1
 	

END



GO

