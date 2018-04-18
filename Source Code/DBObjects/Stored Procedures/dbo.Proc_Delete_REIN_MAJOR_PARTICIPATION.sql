IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_REIN_MAJOR_PARTICIPATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_REIN_MAJOR_PARTICIPATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Created by  : Pravesh K Chandel
Purpose    	: to Delete REinsurance Majar Participation
Date		: 29 Aug 2007
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
drop proc [dbo].Proc_Delete_REIN_MAJOR_PARTICIPATION
------   ------------       -------------------------*/


create  proc dbo.Proc_Delete_REIN_MAJOR_PARTICIPATION
(
@PARTICIPATION_ID int,
@CONTRACT_ID  int
)
as
begin


delete from MNT_REINSURANCE_MAJORMINOR_PARTICIPATION where PARTICIPATION_ID=@PARTICIPATION_ID
	 and CONTRACT_ID = @CONTRACT_ID

end

   



GO

