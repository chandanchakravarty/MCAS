IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMNT_UNDERWRITING_CLAIM_LIMITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetMNT_UNDERWRITING_CLAIM_LIMITS]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author Name>
-- Create date:         <Create Date>
-- Description:	<Description>
-- =============================================

CREATE PROCEDURE [dbo].Get_MNT_UNDERWRITING_CLAIM_LIMITS 
    @ASSIGN_ID int
As


BEGIN

 Select

    ASSIGN_ID,
    USER_ID,
    COUNTRY_ID,
    LOB_ID,
    PML_LIMIT,
    PREMIUM_APPROVAL_LIMIT,
    CLAIM_RESERVE_LIMIT,
    CLAIM_REOPEN,
    CLAIM_SETTLMENT_LIMIT,
    CREATED_BY,
    CREATED_DATETIME,
    MODIFIED_BY,
    LAST_UPDATED_DATETIME 


From MNT_UNDERWRITING_CLAIM_LIMITS

Where  

  ASSIGN_ID = @ASSIGN_ID 

END


GO
