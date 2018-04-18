IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_Orders_INSERT]'))
DROP TRIGGER [dbo].[tr_Orders_INSERT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER tr_Orders_INSERT
ON Orders
FOR INSERT
AS
IF (SELECT COUNT(*) FROM inserted WHERE Ord_Priority = 'High') = 1
 BEGIN
  PRINT 'Email Code Goes Here'
 END

GO

