IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeactivateGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeactivateGroup]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_DeactivateGroup     
Created by      : Nidhi      
Date                  : 27/05/2005      
Purpose         : To update the data into QUESTIONGROUPMASTER
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC dbo.Proc_DeactivateGroup   
(      
@TABID INT,
@SCREENID int,
@CARRIERID int,
@GROUPID int,
@ISACTIVE nchar(2),
@LASTMODIFIEDBY INT,
@LASTMODIFIEDDATE DATETIME
)      
AS      
BEGIN    
    

update QUESTIONGROUPMASTER
    set		 
 
	ISACTIVE		 = @ISACTIVE ,
	LASTMODIFIEDBY	 = @LASTMODIFIEDBY ,
	LASTMODIFIEDDATE	 = @LASTMODIFIEDDATE

where  SCREENID=@SCREENID and TABID=@TABID  and GROUPID=@GROUPID

END


GO

