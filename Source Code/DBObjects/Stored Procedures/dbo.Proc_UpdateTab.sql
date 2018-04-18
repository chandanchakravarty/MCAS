IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateTab]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateTab]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------      
Proc Name     : dbo.Proc_UpdateTab      
Created by      : Nidhi      
Date                  : 27/05/2005      
Purpose         : To update the data into QUESTIONTABMASTER
Revison History :      
Modified by 		: Manab
Description		: Adding Reptable Controls Column
Used In        		: Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE  PROC Dbo.Proc_UpdateTab     
(      
@TABID INT,
@CARRIERID INT,
@SCREENID INT,
@TABNAME NVARCHAR(100),
@REPEATCONTROLS INT,
@LASTMODIFIEDBY INT,
@LASTMODIFIEDDATE DATETIME
)      
AS      
BEGIN    
    

update QUESTIONTABMASTER
    set	
	
	TABNAME =	@TABNAME ,
	REPEATCONTROLS = @REPEATCONTROLS,
	LASTMODIFIEDBY =@LASTMODIFIEDBY ,
	LASTMODIFIEDDATE =@LASTMODIFIEDDATE

where TABID=@TABID  and  SCREENID =@SCREENID

END



GO

