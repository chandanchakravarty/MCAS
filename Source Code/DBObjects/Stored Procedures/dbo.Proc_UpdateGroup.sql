IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateGroup]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_UpdateGroup
Created by      : nidhi
Date                  : 27/05/2005      
Purpose         : To update the data of  QUESTIONGROUPMASTER
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_UpdateGroup
(      
@GROUPID int ,
@TABID int,
@SCREENID int,
@GROUPNAME nvarchar(1000),
@CARRIERID int,
@LASTMODIFIEDDATE datetime,
@LASTMODIFIEDBY int
)      
AS      
BEGIN    
 
update QUESTIONGROUPMASTER
set
	GROUPNAME =	@GROUPNAME,
	LASTMODIFIEDDATE=	@LASTMODIFIEDDATE,
	LASTMODIFIEDBY=	@LASTMODIFIEDBY
where 	SCREENID=@SCREENID and TABID=@TABID and 	GROUPID=@GROUPID

END


GO

