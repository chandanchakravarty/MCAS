IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SetEndorsementPolicyStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SetEndorsementPolicyStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--begin tran
--drop proc Proc_SetEndorsementPolicyStatus
--go
/*=======================================================================================    
PROC NAME        : dbo.Proc_SetEndorsementPolicyStatus          
CREATED BY       : Ashwani    
DATE             : 27 Oct., 06    
PURPOSE          : Set the policy ststus in case of endorsement        
  
modified BY       : Pravesh K Chandel  
DATE             :  11 May, 07    
PURPOSE          : Set the policy status of New Version Marked for endorsement  if it is effective from future      
modified BY       : Pravesh K Chandel  
DATE             : 22 nOV, 07    
PURPOSE          : Set the cURRENT policy status of New Version IN POL_POLICY_PROCESS  
modified BY       : Pravesh K Chandel  
DATE             : 8 April, 09    
PURPOSE          : Set the policy status of old Version IN POLicy list Table  
  
REVISON HISTORY  :                  
USED IN          :   WOLVERINE                  
=========================================================================================    
DATE     REVIEW BY          COMMENTS                  
=====   =============      ==============================================================  
drop proc dbo.Proc_SetEndorsementPolicyStatus    
*/    
CREATE PROC [dbo].[Proc_SetEndorsementPolicyStatus]    
(                  
 @CUSTOMER_ID       INT,      
 @POLICY_ID        INT,      
 @POLICY_VERSION_ID  SMALLINT,    
 @POLICY_VERSION_ID_NEW SMALLINT,      
 @PROCESS_ID     INT,    
 @POLICY_STATUS  VARCHAR(50) = NULL,    
 @POLICY_STATUS_DESC VARCHAR(30) = NULL OUTPUT    
)                  
AS                                
BEGIN                  
     DECLARE  @SOURCE_VERSION_ID  INT,
     @END_RE_ISSUE INT,@LOOKUP_YES INT = 10963,
     @OLD_PREV_STATUS NVARCHAR(50),@OLD_CURRENT_STATUS NVARCHAR(50),@OLD_POLICY_STATUS NVARCHAR(50)
     
     SELECT @END_RE_ISSUE = ENDORSEMENT_RE_ISSUE,@SOURCE_VERSION_ID  = SOURCE_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND 
     POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW
     
     IF (@END_RE_ISSUE = @LOOKUP_YES) --Addedby Lalit May 9,2011.if re-issueendorsemnt then current version status should b as re-issued version status
     BEGIN 
     	SELECT @OLD_PREV_STATUS = POLICY_PREVIOUS_STATUS,
     	@OLD_CURRENT_STATUS = POLICY_CURRENT_STATUS
     	 FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND 
     	NEW_POLICY_VERSION_ID  = @SOURCE_VERSION_ID AND UPPER(PROCESS_STATUS) <> 'ROLLBACK'
     	
     	SELECT @OLD_POLICY_STATUS = POLICY_STATUS FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @SOURCE_VERSION_ID
      
     	UPDATE POL_CUSTOMER_POLICY_LIST SET POLICY_STATUS = @OLD_POLICY_STATUS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW
     	--set re-issue version in-active.itrack # 1357
     	UPDATE POL_CUSTOMER_POLICY_LIST SET POLICY_STATUS = 'INACTIVE'  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @SOURCE_VERSION_ID AND UPPER(POLICY_STATUS) <> 'INACTIVE'
     	
		 UPDATE POL_POLICY_PROCESS SET POLICY_CURRENT_STATUS = @OLD_CURRENT_STATUS		
		  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW
	      
		--RETURN 
     END
     
     
     
     
     
     
 IF @POLICY_STATUS IS NULL    
 BEGIN   --Policy status is not passes, hence fetching the policy status on the   basis of process id      
   SELECT     
    @POLICY_STATUS = POLICY_STATUS,    
    @POLICY_STATUS_DESC = POL_POLICY_STATUS_MASTER.POLICY_DESCRIPTION      
   FROM POL_PROCESS_MASTER WITH(NOLOCK)    
   LEFT JOIN POL_POLICY_STATUS_MASTER ON POL_PROCESS_MASTER.POLICY_STATUS = POL_POLICY_STATUS_MASTER.POLICY_STATUS_CODE    
   WHERE PROCESS_ID = @PROCESS_ID      
 END         
    
    
 DECLARE @POL_VER_EFFECTIVE_DATE DATETIME      
 SELECT @POL_VER_EFFECTIVE_DATE=POL_VER_EFFECTIVE_DATE    
 FROM POL_CUSTOMER_POLICY_LIST with(nolock)   
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW       
  
 -- if endorsement effective date is future date then old policy version should remain active and    
 -- new  policy version will be maked for Endorsement   
 IF(@POL_VER_EFFECTIVE_DATE>=GETDATE())    
  BEGIN     
   UPDATE POL_CUSTOMER_POLICY_LIST       
   SET POLICY_STATUS = 'MENDORSE'  ---@POLICY_STATUS          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW   
        --UPDATING PROCESS LOG TABLE   
    UPDATE  POL_POLICY_PROCESS SET POLICY_CURRENT_STATUS='MENDORSE'   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW AND PROCESS_ID=@PROCESS_ID  
  END    
 ELSE    
  BEGIN     
   --Updating the policy status  of parent policy to inactive  
   UPDATE POL_CUSTOMER_POLICY_LIST       
   SET POLICY_STATUS = @POLICY_STATUS          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID   
  
  --Updating the policy status  of new policy to normal  
   UPDATE POL_CUSTOMER_POLICY_LIST       
   SET POLICY_STATUS = 'NORMAL'          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW   
  
    --Updating the policy status  of old policy Version to inactive WHOSE status is Normal as only one version will be active at a time // changed by Pravehs on 8 April 2009  
   UPDATE POL_CUSTOMER_POLICY_LIST       
   SET POLICY_STATUS = 'INACTIVE'          
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID < @POLICY_VERSION_ID_NEW   
    AND POLICY_STATUS = 'NORMAL'          
   --UPDATING PROCESS LOG TABLE   
    UPDATE  POL_POLICY_PROCESS SET POLICY_CURRENT_STATUS='NORMAL'  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW AND PROCESS_ID=@PROCESS_ID  
   
  END    
  /*
  
   --Added by Lalit May 03,2011 
   --Policy previos version which is re-issued will be inactive
   --i-track # 948
   --implimentation of endorsemnt re-issue
   
    IF EXISTS(SELECT * FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE 
    CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW )
    BEGIN
		IF EXISTS (SELECT SOURCE_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE 
		    CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW )			
				BEGIN
					
					SELECT @SOURCE_VERSION_ID =  SOURCE_VERSION_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE 
						CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW 
				
			    UPDATE POL_CUSTOMER_POLICY_LIST       
			    SET POLICY_STATUS = 'INACTIVE'          
			    WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @SOURCE_VERSION_ID   
				-- 	AND POLICY_STATUS = 'NORMAL'          
			  
			
				UPDATE  POL_POLICY_PROCESS SET POLICY_PREVIOUS_STATUS ='INACTIVE'  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
				AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID_NEW AND PROCESS_ID= @PROCESS_ID  
			  
							
				END 
    
    
    END*/
  -- SET IS_ACTIVE = @IS_ACTIVE     for new version    
    
END      
      
--      go
--      exec Proc_SetEndorsementPolicyStatus 28070,732,3,4,14,'INACTIVE'
      
--      select POLICY_STATUS,* from POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK ) wHERE CUSTOMER_ID = 28070 and POLICY_ID = 732

--      rollback tran
      
    
    
    

GO

