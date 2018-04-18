IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertApp_Refusal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertApp_Refusal]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--SELECT * FROM ACT_PREMIUM_NOTICE_PROCCESS_LOG
--sp_helptext Proc_InsertPRINT_JOBS

/*----------------------------------------------------------                            
Proc Name    : dbo.Proc_InsertApp_Refusal                          
Created by   : Shubhanshu Pandey         
Date         : 10-2-2010                
Purpose      : For insertion of Application_Refusal      
                            
 ------------------------------------------------------------                                        
Date     Review By          Comments                                      
 drop proc dbo.Proc_InsertApp_Refusal 2164,1,1                      
------   ------------       -------------------------*/                           
CREATE PROC [dbo].[Proc_InsertApp_Refusal]                        
(                                     
@CUSTOMER_ID int,          
@POLICY_ID int,          
@POLICY_VERSION_ID smallint,
@PROCESS_INFORMATION text,
@DEC_CUSTOMERXML text
--@DATE_TIME datetime   = getdate
)                        
AS                        
BEGIN                          
 INSERT INTO ACT_PREMIUM_NOTICE_PROCCESS_LOG            
 (          
  CUSTOMER_ID,          
  POLICY_ID,          
  POLICY_VERSION_ID,
  PROCCESS_INFORMATION,
  DEC_CUSTOMERXML,
  DATE_TIME,
  CALLED_FOR,
  IS_FILE_GENERATED                     
 )          
 VALUES          
 (          
  @CUSTOMER_ID,          
  @POLICY_ID,          
  @POLICY_VERSION_ID,      
  @PROCESS_INFORMATION,
  @DEC_CUSTOMERXML,
  GETDATE(),
  'REFUSAL',
  'Y'
 )          
           
END        



GO

