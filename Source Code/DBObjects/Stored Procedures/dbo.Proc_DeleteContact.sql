IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteContact]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteContact]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.MNT_CONTACT_LIST
Created by      :Gaurav
Date            : 7/13/2005
Purpose       :To delete recods from contact
Revison History :
Used In        : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteContact
(
@CONTACT_ID int
)
As
Begin
DELETE FROM MNT_CONTACT_LIST WHERE CONTACT_ID=@CONTACT_ID 
END



GO

