    
-- =============================================    
-- Author:  <Author Name>    
-- Create date:         <Create Date>    
-- Description: <Description>    
-- =============================================    
    
CREATE PROCEDURE [dbo].UPDATE_MNT_UNDERWRITING_CLAIM_LIMITS    
    @ASSIGN_ID int,    
    @USER_ID int,    
    @COUNTRY_ID int,    
    @LOB_ID int,    
    @PML_LIMIT decimal(10,2),    
    @PREMIUM_APPROVAL_LIMIT decimal(10,2),    
    @CLAIM_RESERVE_LIMIT decimal(10,2),    
    @CLAIM_REOPEN int,    
    @CLAIM_SETTLMENT_LIMIT decimal(10,2),    
    @MODIFIED_BY int,    
    @LAST_UPDATED_DATETIME datetime    
AS    
    
BEGIN    
    
UPDATE [MNT_UNDERWRITING_CLAIM_LIMITS]    
    
SET    
    
    ASSIGN_ID = @ASSIGN_ID,    
    USER_ID = @USER_ID,    
    COUNTRY_ID = @COUNTRY_ID,    
    LOB_ID = @LOB_ID,    
    PML_LIMIT = @PML_LIMIT,    
    PREMIUM_APPROVAL_LIMIT = @PREMIUM_APPROVAL_LIMIT,    
    CLAIM_RESERVE_LIMIT = @CLAIM_RESERVE_LIMIT,    
    CLAIM_REOPEN = @CLAIM_REOPEN,    
    CLAIM_SETTLMENT_LIMIT = @CLAIM_SETTLMENT_LIMIT,    
    MODIFIED_BY = @MODIFIED_BY,    
    LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME    
    
    
Where      
    
  ASSIGN_ID = @ASSIGN_ID    
    
END    