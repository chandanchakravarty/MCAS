IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUtilLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUtilLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* --------------------------------------------------------------------------------------------------                                                                                                                        
Proc Name                : Dbo.Proc_InsertUtilLog                                                                                                        
Created by               :Sibin Thomas                                                                                                                        
Date                     : 4 March 2009                                                                                                                      
Purpose                  : To insert encrypted log for Util section                                                                                                                     
Revison History          :                                                                                                                        
Used In                  : Wolverine                                                                                                                        
----------------------------------------------------------------------------------------------------                                                                                                                        
Date     Review By          Comments                                     
                     
drop proc dbo.Proc_InsertUtilLog                                                                                                                  
------   ------------       ------------------------------------------------------------------------*/                                                                                                                        
CREATE  Proc [dbo].[Proc_InsertUtilLog]                                                                                                                        
(                                                                                                                     @EXECUTED_BY int,
	@EXECUTED_ON DATETIME,
	@QUERY_SQL varchar(3000)
)

AS

BEGIN
 
 INSERT INTO Q_EXEC_LOG
 (
   EXECUTED_BY,
   EXECUTED_ON,
   QUERY_SQL
 )
 
 VALUES
 (
   @EXECUTED_BY,
   @EXECUTED_ON,
   @QUERY_SQL
)

END
GO

