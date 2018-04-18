IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_GetExtraInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_GetExtraInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Proc proc_GetExtraInfo   
(  
 @TableName varchar(50),  
 @CUSTOMERID  varchar(10),                  
 @APPID  varchar(10),                  
 @APPVERSIONID varchar(10),                 
 @EntityNAME varchar(50),   
 @EntityID varchar(50)   
)  
AS 
BEGIN  
 Declare @Query as varchar(1000)  
 set @query = 'SELECT * FROM '  + @TableName + ' WHERE Customer_id=' +@CUSTOMERID+' and APP_ID='+@APPID+' and APP_VERSION_ID = ' +@APPVERSIONID + ' and ' + @ENTITYNAME + ' = ' +@EntityID  
 Exec(@Query)  
   
END  


GO

