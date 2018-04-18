IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_temp_table]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GET_temp_table]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC PROC_GET_temp_table 
AS
BEGIN 

SELECT * FROM temp_table WITH(NOLOCK)

INSERT INTO temp_table
(NAME,AGE,SALARY)
VALUES
('test',10,10000)
End
GO

