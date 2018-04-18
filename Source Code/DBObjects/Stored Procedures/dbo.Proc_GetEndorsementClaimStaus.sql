IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetEndorsementClaimStaus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetEndorsementClaimStaus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                        
Proc Name       : Proc_GetEndorsementClaimStaus                                                   
            
Modified by   : Lalir Kr Chauhan    
Modified On   : May 18,2011    
Purpose       : check Claim status open for version to cancel endorsement
drop proc Proc_GetEndorsementClaimStaus   28070,349,5,1

DECLARE  @AA INT
EXEC Proc_GetEndorsementClaimStaus   28070,349,5,1,@AA OUT
SELECT @AA
*/    
CREATE Proc [dbo].[Proc_GetEndorsementClaimStaus]
(
@CUSTOMER_ID  INT,
@POLICY_ID INT ,
@POLICY_VERSION_ID INT,
@BASE_VERSION_ID INT,
@RET_VAL INT = NULL OUT
)
AS
BEGIN
DECLARE @CURRENT_TERM INT 

 SELECT @CURRENT_TERM = CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID     
 AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
 
 IF EXISTS (SELECT * FROM CLM_CLAIM_INFO  CLMI WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
  AND POLICY_VERSION_ID in (SELECT PL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST PL WITH(NOLOCK) WHERE PL.CUSTOMER_ID = CLMI.CUSTOMER_ID and PL.POLICY_ID = CLMI.POLICY_ID
  and PL.POLICY_VERSION_ID > @BASE_VERSION_ID and CURRENT_TERM = @CURRENT_TERM))
	BEGIN
		SET @RET_VAL = - 2 
		RETURN -2
    END
   ELSE
    BEGIN
    
    SET @RET_VAL =  1 
		RETURN 1
    END
    
    
 

END    
GO

