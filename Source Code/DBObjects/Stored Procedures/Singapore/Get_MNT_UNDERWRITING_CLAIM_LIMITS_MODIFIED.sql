  
-- =============================================================================================================  
-- Author:  <Author Name>  
-- Create date:         <Create Date>  
-- Description: <Description>  
-- =============================================================================================================
-- Modified On: 3-Feb-2012
-- Modified By: Ruchika Chauhan
-- Reason:		This proc now fetches Effective and End dates as well, as per the requirement in TFS Bug # 3322
-- =============================================================================================================
-- DROP PROC Get_MNT_UNDERWRITING_CLAIM_LIMITS
  
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
    LAST_UPDATED_DATETIME,
    
    ISNULL(Convert(varchar,EffectiveDate,103),''),
    ISNULL(Convert(varchar,EndDate,103),'')  
  
  
From MNT_UNDERWRITING_CLAIM_LIMITS  
  
Where    
  
  ASSIGN_ID = @ASSIGN_ID   
  
END  
  
  