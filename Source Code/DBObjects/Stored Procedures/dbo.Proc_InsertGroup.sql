IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertGroup]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_InsertGroup
Created by      : Anurag Verma      
Date                  : 26/05/2005      
Purpose         : To insert the data into QUESTIONGROUPMASTER
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     			Review By          Comments      
------   			------------       -------------------------
27/05/05		Nidhi		Added ScreenID and TabID in the first query to get @COUNT

*/      
CREATE PROC Dbo.Proc_InsertGroup
(      
@GROUPID int OUTPUT,
@TABID int,
@SCREENID int,
@GROUPNAME nvarchar(1000),
@GROUPTYPE nchar(2),
@CARRIERID int,
@LASTMODIFIEDDATE datetime,
@LASTMODIFIEDBY int
)      
AS      
BEGIN    
 /*Check for Unique Code of Agency  */    
 Declare @Count numeric    
 DECLARE @CNTSEQNO NUMERIC
 SELECT @Count = ISNULL(MAX(GROUPID),0)+1,@CNTSEQNO = ISNULL(MAX(SEQNO),0)+1   FROM QUESTIONGROUPMASTER  where  SCREENID=@SCREENID and TABID=@TABID
     

  INSERT INTO QUESTIONGROUPMASTER
  (     

	CARRIERID,	
	SCREENID,
	TABID,
	GROUPID, 
	GROUPNAME,
	SEQNO,
	GROUPTYPE,
	ISACTIVE,
	
	LASTMODIFIEDDATE,
	LASTMODIFIEDBY
  )      
  VALUES      
  (      

	@CARRIERID,
	@SCREENID,	
	@TABID,
	@Count, 
	@GROUPNAME,
	@CNTSEQNO,
	@GROUPTYPE,
	'Y',	
	@LASTMODIFIEDDATE,
	@LASTMODIFIEDBY

 )      
	
SELECT @GROUPID = ISNULL(Max(GROUPID),0) FROM QUESTIONGROUPMASTER  where  SCREENID=@SCREENID and TABID=@TABID
END


GO

