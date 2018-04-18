IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertList]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_InsertList
Created by      : Anurag Verma      
Date                  : 30/05/2005      
Purpose         : To insert the data into QUESTIONLISTMASTER
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_InsertList     
(      
@LISTID INT OUTPUT,
@TYPE nchar(4),
@NAME NVARCHAR(200),
@CARRIERID NUMERIC,	
@ISACTIVE NCHAR(2)
)      
AS      
BEGIN    

 Declare @Count numeric    
  SELECT @Count = ISNULL(MAX(LISTID),0)+1   FROM QUESTIONLISTMASTER
     

  INSERT INTO QUESTIONLISTMASTER
  (     
	LISTID,
	TYPE,
	NAME,
	CARRIERID,
	ISACTIVE
  )      
  VALUES      
  (      
	@COUNT,
	@TYPE,
	@NAME,
	@CARRIERID,
	@ISACTIVE
	
 )      
	
SELECT @LISTID = ISNULL(Max(LISTID),0) FROM QUESTIONLISTMASTER
END


GO

