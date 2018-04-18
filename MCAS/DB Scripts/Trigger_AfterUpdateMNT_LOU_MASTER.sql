

/****** Object:  Trigger [AfterUpdateMNT_LOU_MASTER]    Script Date: 08/25/2014 14:41:14 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[AfterUpdateMNT_LOU_MASTER]'))
DROP TRIGGER [dbo].[AfterUpdateMNT_LOU_MASTER]
GO



/****** Object:  Trigger [dbo].[AfterUpdateMNT_LOU_MASTER]    Script Date: 08/25/2014 14:41:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE TRIGGER [dbo].[AfterUpdateMNT_LOU_MASTER]
ON [dbo].[MNT_LOU_MASTER]
AFTER UPDATE 
AS BEGIN
   UPDATE dbo.MNT_LOU_MASTER
   SET ModifiedDate = GETDATE()
   FROM INSERTED i
   WHERE i.Id = MNT_LOU_MASTER.Id
END


GO


