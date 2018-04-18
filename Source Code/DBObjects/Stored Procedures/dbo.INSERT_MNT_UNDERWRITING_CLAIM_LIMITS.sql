IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[INSERT_MNT_UNDERWRITING_CLAIM_LIMITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[INSERT_MNT_UNDERWRITING_CLAIM_LIMITS]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Amit Kr. Mishra
-- Create date: 1st November,2011
-- Description:	-
-- =============================================

  
CREATE PROCEDURE dbo.INSERT_MNT_UNDERWRITING_CLAIM_LIMITS  
    @ASSIGN_ID INT OUTPUT,  
    @USER_ID int,  
    @COUNTRY_ID int,  
    @LOB_ID int,  
    @PML_LIMIT decimal,  
    @PREMIUM_APPROVAL_LIMIT decimal,  
    @CLAIM_RESERVE_LIMIT decimal,  
    @CLAIM_REOPEN int,  
    @CLAIM_SETTLMENT_LIMIT decimal,  
    @CREATED_BY int,  
    @CREATED_DATETIME datetime  
AS  
  
BEGIN  
--DECLARE @ASSIGN_ID1 int  
SELECT @ASSIGN_ID= ISNULL(MAX(ASSIGN_ID),0)+1 FROM MNT_UNDERWRITING_CLAIM_LIMITS  
INSERT INTO MNT_UNDERWRITING_CLAIM_LIMITS  
  (  
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
    CREATED_DATETIME  
   )  
  
VALUES   
 (  
  @ASSIGN_ID,  
  @USER_ID,  
  @COUNTRY_ID,  
  @LOB_ID,  
  @PML_LIMIT,  
  @PREMIUM_APPROVAL_LIMIT,  
  @CLAIM_RESERVE_LIMIT,  
  @CLAIM_REOPEN,  
  @CLAIM_SETTLMENT_LIMIT,  
  @CREATED_BY,  
  @CREATED_DATETIME  
 )  
return @Assign_id  
END  
  