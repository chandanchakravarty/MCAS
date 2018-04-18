IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FINDCOLUMNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_FINDCOLUMNS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- by Pravesh
CREATE PROCEDURE SP_FINDCOLUMNS
	@lstrCOLUMN_NAME		NVARCHAR(100) = '',
	@lstrDATAType		NVARCHAR(10) = '',
	@lstrTABLENAME		NVARCHAR(50) = ''
AS
	DECLARE @XTYPE NVARCHAR(5)
BEGIN
/*
		Stored Procedure To Find the name of COLUMNS
*/

select * from INFORMATION_SCHEMA.columns 
where 1=1
AND CASE WHEN @lstrTABLENAME ='' THEN '1' ELSE TABLE_NAME END  LIKE
 '%' + CASE WHEN @lstrTABLENAME ='' THEN '1' ELSE @lstrTABLENAME END  + '%' 

AND CASE WHEN @lstrDATAType ='' THEN '1' ELSE DATA_TYPE END  =
 CASE WHEN @lstrDATAType ='' THEN '1' ELSE @lstrDATAType END  

AND CASE WHEN @lstrCOLUMN_NAME ='' THEN '1' ELSE column_name END  LIKE
 '%' + CASE WHEN @lstrCOLUMN_NAME ='' THEN '1' ELSE @lstrCOLUMN_NAME END  + '%' 



END


GO

