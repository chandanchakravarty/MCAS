IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeactivateTab]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeactivateTab]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_DeactivateTab      
Created by      : Nidhi      
Date                  : 27/05/2005      
Purpose         : To update the data into QUESTIONTABMASTER
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_DeactivateTab     
(      
@TABID INT,
@SCREENID int,
@CARRIERID int,
@ISACTIVE nchar(2),
@LASTMODIFIEDBY INT,
@LASTMODIFIEDDATE DATETIME
)      
AS      
BEGIN    
    

update QUESTIONTABMASTER
    set		 
 
	ISACTIVE		 = @ISACTIVE ,
	LASTMODIFIEDBY	 = @LASTMODIFIEDBY ,
	LASTMODIFIEDDATE	 = @LASTMODIFIEDDATE

where CARRIERID=@CARRIERID and SCREENID=@SCREENID and TABID=@TABID  

END


GO

