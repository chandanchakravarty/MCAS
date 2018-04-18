IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_LOBCODE_CEDED]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_LOBCODE_CEDED]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[proc_LOBCODE_CEDED]
(
 @CLAIM_ID varchar(10) 
)
AS
BEGIN
DECLARE @SUSEP_LOB_CODE INT

SELECT MLOB.SUSEP_LOB_CODE AS SUSEP_LOB_CODE
FROM MNT_LOB_MASTER MLOB
LEFT OUTER JOIN CLM_CLAIM_INFO CCI WITH(NOLOCK) ON  CCI.LOB_ID=MLOB.LOB_ID  
LEFT OUTER JOIN CLM_ACTIVITY CA WITH (NOLOCK) ON CA.CLAIM_ID=CCI.CLAIM_ID 
WHERE CCI.CLAIM_ID=@CLAIM_ID 
END


GO

