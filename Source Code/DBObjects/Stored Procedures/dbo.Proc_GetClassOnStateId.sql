IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClassOnStateId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClassOnStateId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       	: dbo.Proc_GetClassOnStateId
Created by      	: Shafi
Date            	: 22/09/06
Purpose         	: Return Xml Class Bases On State Id
Revison History :
Used In 	: Wolverine 
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_GetClassOnStateId
CREATE  PROC dbo.Proc_GetClassOnStateId
(
	@STATE_ID INT = null
 )
AS
BEGIN
if @STATE_ID<>0

 SELECT LOB_ID,ISNULL(SUB_LOB_ID,0) AS SUB_LOB_ID ,COMMISSION_CLASS_ID,CLASS_DESCRIPTION
 FROM ACT_COMMISION_CLASS_MASTER (NOLOCK)  WHERE STATE_ID=@STATE_ID  FOR XML RAW
else
  SELECT LOB_ID,ISNULL(SUB_LOB_ID,0) AS SUB_LOB_ID ,STATE_ID,COMMISSION_CLASS_ID,CLASS_DESCRIPTION
  FROM ACT_COMMISION_CLASS_MASTER (NOLOCK)    FOR XML RAW
end


GO

