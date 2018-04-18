IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBindingData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBindingData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /**************************************************      
CREATED BY    : Pravesh K Chandel     
CREATED DATETIME : 29 April 2011      
PURPOSE    : fetch DataSource to bind Dropdown/DataListt
Review      :
Review By  Date   
Purpose      
DROP PROC Proc_GetBindingData
***************************************************/     
CREATE PROCEDURE Proc_GetBindingData
(      
 @DbObjectName  Nvarchar(100),
 @DbObjectType  Nvarchar(10),
 @TextField   nvarchar(100),
 @ValueField nvarchar(100),
 @WhereClause nvarchar(200)=null
)      
AS      
BEGIN      
DECLARE @SqlQuery nvarchar(500)


if (@DbObjectType='Table' or @DbObjectType='T')
	begin
		set @SqlQuery='Select ' + @TextField + ',' + @ValueField + ' from ' + @DbObjectName 
		if (@WhereClause is not null and @WhereClause<>'')
		set @SqlQuery = @SqlQuery + ' where ' + @WhereClause
		execute(@SqlQuery)
	end
else
	begin
		execute(@DbObjectName) 
	end

END

GO

