IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GenerateAppNumber_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GenerateAppNumber_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GenerateAppNumber      
Created by           : Nidhi      
Date                    : 07/15/2005      
Purpose               : Generate the Application number      
Revison History :      
      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC dbo.Proc_GenerateAppNumber_ACORD      
(      
	@APP_LOB int,      
	@AGENCY_ID int  =NULL,    
	@APP_NUMBER VarChar(20) OUTPUT       
)      
AS      
begin
	create table #tmp
	(
		APP_NO varchar(20)
	)

	insert into #TMp  
	exec Proc_GenerateAppNumber @APP_LOB, @AGENCY_ID

	select @APP_NUMBER = APP_NO from #tmp

	DROP TABLE #tmp
END    


GO

