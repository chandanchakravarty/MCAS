IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRenewalDiaryReminder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRenewalDiaryReminder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------            
Proc Name       : dbo.Proc_GetPolicyRenewalDiaryReminder    
Created by      : Vijay Arora        
Date            : 02-02-2006    
Purpose         : Selects the records from policy table which has to be reviewed before expiration.    
Revison History :            
Used In         : Wolverine     
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/          
CREATE PROC Proc_GetPolicyRenewalDiaryReminder        
AS        
BEGIN        
     
 DECLARE @RENEWAL_DAYS SMALLINT     
 DECLARE @TEMP_DATE DATETIME    
 DECLARE @TEMP_PRIOR_DATE DATETIME    
    
 SELECT @RENEWAL_DAYS = SYS_RENEWAL_FOR_ALERT FROM MNT_SYSTEM_PARAMS   
 SELECT @TEMP_DATE = DATEADD(day,@RENEWAL_DAYS,GETDATE()) 
 SELECT @TEMP_PRIOR_DATE = DATEADD(day,-90,GETDATE()) 
    
 SELECT * FROM POL_CUSTOMER_POLICY_LIST
 WHERE (APP_EXPIRATION_DATE >= @TEMP_PRIOR_DATE AND APP_EXPIRATION_DATE <= @TEMP_DATE) 
 AND (SEND_RENEWAL_DIARY_REM <> 'Y' OR SEND_RENEWAL_DIARY_REM IS NULL)
 ORDER BY APP_EXPIRATION_DATE DESC
    
END        
      


GO

