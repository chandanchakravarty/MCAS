IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_delete_UNDISCLOSED_DRIVER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_delete_UNDISCLOSED_DRIVER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*
proc dbo.Proc_delete_UNDISCLOSED_DRIVER
created by   :Pravesh Chandel
Dated	      : 20 Dec 2006
purpose	      : to Delete UNDISCLOSED Driver Information 	
drop proc	dbo.Proc_delete_UNDISCLOSED_DRIVER
*/

create procedure dbo.Proc_delete_UNDISCLOSED_DRIVER
(
@UNDISCLOSED_DRIVER_IDS VARCHAR(100)
)
as
begin
DECLARE @strQUERY VARCHAR(500)

SET @strQUERY='
		DELETE FROM APP_UNDISCLOSED_DRIVER WHERE UNDISCLOSED_DRIVER_ID IN (' + @UNDISCLOSED_DRIVER_IDS + ')'

  EXEC (@strQUERY);
end







GO

