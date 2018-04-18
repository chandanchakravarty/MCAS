IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_find_procs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_find_procs]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop PROCEDURE dbo.sp_find_procs  
CREATE PROCEDURE dbo.sp_find_procs  
 @search VARCHAR(100) = ''  
AS  
SET @search = '%' + @search + '%'  
SELECT ROUTINE_NAME,ROUTINE_DEFINITION FROM INFORMATION_SCHEMA.ROUTINES WHERE   
ROUTINE_DEFINITION LIKE @search --ORDER BY  ROUTINE_NAME  



GO

