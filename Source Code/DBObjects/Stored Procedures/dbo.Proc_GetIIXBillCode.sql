IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetIIXBillCode]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetIIXBillCode]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name        : dbo.Proc_GetIIXBillCode                           
mODIFIED BY 	:PRAVESH  K CHANDEL
MODIFIED DATE	:20 June 2008
PURPOSE		: FETCH Bill code from lookup for iix web service  on the basis of product code
Used In             :           Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments   
drop PROC dbo.Proc_GetIIXBillCode                    
------   ------------       -------------------------*/                              
CREATE PROC [dbo].[Proc_GetIIXBillCode]  
(
@PRODUCT_CODE NVARCHAR(10)
)
AS
BEGIN
DECLARE @IS_LIVE_SERVER  CHAR(1)

SELECT @IS_LIVE_SERVER =ISNULL(SYS_IS_LIVE_SERVER,'') FROM MNT_SYSTEM_PARAMS

IF (@IS_LIVE_SERVER='Y')
	SELECT LOOKUP_VALUE_CODE FROM MNT_LOOKUP_VALUES WHERE [TYPE]=@PRODUCT_CODE AND isnull(LOOKUP_FRAME_OR_MASONRY,'')='LIVE' AND LOOKUP_ID= 1349
ELSE
	SELECT LOOKUP_VALUE_CODE FROM MNT_LOOKUP_VALUES WHERE [TYPE]=@PRODUCT_CODE AND isnull(LOOKUP_FRAME_OR_MASONRY,'')='TEST' AND LOOKUP_ID= 1348

END        

	
GO

