IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyProcessStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyProcessStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
/*               
----------------------------------------------------------                                    
Proc Name       : dbo.Proc_GetPolicyProcessStatus                          
Created by      : LALIT CHAUHAN                        
Date            : 05/20/2010                                    
Purpose         : Get Policy Process and staty    
Revison History :                                    
Used In        : Ebix Advantage                                    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------                    
drop proc Proc_GetPolicyProcessStatus        
exec Proc_GetPolicyProcessStatus 2156,679,2    
--POL_POLICY_STATUS_MASTER    
*/        
CREATE PROC [dbo].[Proc_GetPolicyProcessStatus]    
(    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID SMALLINT    
)    
AS    
 BEGIN    
  DECLARE @POLICY_EFF_DATE DATETIME,@POLICY_EXP_DATE DATETIME    
  IF EXISTS(SELECT CUSTOMER_ID FROM POL_CUSTOMER_POLICY_LIST  WITH(NOLOCK) WHERE CUSTOMER_ID= @CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)    
  BEGIN    
     SELECT @POLICY_EFF_DATE = ISNULL(POLICY_EFFECTIVE_DATE,APP_EFFECTIVE_DATE) ,  
     @POLICY_EXP_DATE = ISNULL(POLICY_EXPIRATION_DATE,APP_EXPIRATION_DATE)   
     FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  WHERE CUSTOMER_ID= @CUSTOMER_ID   
     AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
         
  IF EXISTS(SELECT PROCESS_STATUS FROM POL_POLICY_PROCESS WITH(NOLOCK)     
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID     
       AND PROCESS_STATUS <> 'ROLLBACK')    
   BEGIN    
    SELECT POLICY_PREVIOUS_STATUS,POLICY_CURRENT_STATUS,PROCESS_STATUS,POL_PROCESS_MASTER.PROCESS_ID,    
    CASE WHEN POL_POLICY_PROCESS.PROCESS_ID =2 
    THEN POL_POLICY_PROCESS.EFFECTIVE_DATETIME ELSE
    ISNULL(POL_POLICY_PROCESS.EFFECTIVE_DATETIME,@POLICY_EFF_DATE) END POLICY_EFFECTIVE_DATE,
    CASE WHEN POL_POLICY_PROCESS.PROCESS_ID =2 --Added By Lait for cancellation in progress,if cancellation effective date is not set then installment cant be genrated,
        THEN POL_POLICY_PROCESS.[EXPIRY_DATE] ELSE
    ISNULL(POL_POLICY_PROCESS.[EXPIRY_DATE],@POLICY_EXP_DATE)END POLICY_EXPIRATION_DATE,    
    POL_PROCESS_MASTER.PROCESS_CODE,POL_PROCESS_MASTER.PROCESS_DESC,COMPLETED_DATETIME,  
    POLICY_VERSION_ID,NEW_POLICY_VERSION_ID ,LAST_REVERT_BACK  ,CANCELLATION_TYPE
       
    FROM POL_POLICY_PROCESS WITH(NOLOCK)     
    INNER JOIN POL_PROCESS_MASTER WITH(NOLOCK) ON    
    POL_PROCESS_MASTER.PROCESS_ID=POL_POLICY_PROCESS.PROCESS_ID    
        
    WHERE CUSTOMER_ID=@CUSTOMER_ID AND     
     POLICY_ID=@POLICY_ID AND     
     NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID AND     
     PROCESS_STATUS <> 'ROLLBACK'    
   END    
  END    
 END
 

GO

