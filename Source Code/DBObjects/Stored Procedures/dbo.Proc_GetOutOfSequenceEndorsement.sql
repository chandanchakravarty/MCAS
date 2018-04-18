IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOutOfSequenceEndorsement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOutOfSequenceEndorsement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*    
CREATED BY : PRAVESH K. CHANDEL    
DATE  : 21 JUNE 2007    
PURPOSE  ; TO CHECK OUT OF SEQUENCE ENDORSEMENT    
USED IN  : WOLVERINE     
MODIFIED BY : PRAVESH K CHANDEL    
MODIFIED DATE : 15 APRIL 2008    
PURPOSE  : IF POLICY REINSTATED THEN ENDORSEMENT EFF DATE CAN NOT BE LESS THN REINSTATE DATE    
*/    
--drop proc dbo.Proc_GetOutOfSequenceEndorsement  28070,195,14  
    
    
CREATE PROCEDURE [dbo].[Proc_GetOutOfSequenceEndorsement]    
(    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@PROCESS_ID smallint,
@CO_APPLICANT_ID INT = NULL
)    
AS    
BEGIN    
DECLARE @END_EFFECTIVE_DATETIME DATETIME
DECLARE @REIN_EFFECTIVE_DATETIME DATETIME    
DECLARE @LAPSE_COVERAGE INT    
DECLARE @CO_APPLCIANT_ID INT
DECLARE @END_EXP_DATETIME DATETIME   
set @LAPSE_COVERAGE=14244    
--set @NO_LAPSE_COVERAGE=14245    
    
SELECT @END_EFFECTIVE_DATETIME=Convert(varchar,MAX(EFFECTIVE_DATETIME), 101),
@CO_APPLCIANT_ID = ISNULL(CO_APPLICANT_ID,0)
FROM POL_POLICY_PROCESS with(nolock)    
  WHERE POLICY_ID = @POLICY_ID    
  AND CUSTOMER_ID = @CUSTOMER_ID    
  AND PROCESS_ID = @PROCESS_ID    
  AND PROCESS_STATUS = 'COMPLETE'    
  AND isnull(REVERT_BACK,'N') <>' Y'    
  AND CO_APPLICANT_ID = @CO_APPLICANT_ID
 GROUP BY CO_APPLICANT_ID
 
 
   
SELECT @REIN_EFFECTIVE_DATETIME=Convert(varchar,MAX(EFFECTIVE_DATETIME), 101)
  FROM POL_POLICY_PROCESS with(nolock)    
  WHERE POLICY_ID = @POLICY_ID    
  AND CUSTOMER_ID = @CUSTOMER_ID    
  AND PROCESS_ID = 16 -- 16  Commit Reinstate Process    
  AND PROCESS_STATUS = 'COMPLETE' AND ISNULL(CANCELLATION_TYPE,0)=@LAPSE_COVERAGE    
 AND isnull(REVERT_BACK,'N')<>'Y'    
  GROUP BY CO_APPLICANT_ID
 

    
IF @REIN_EFFECTIVE_DATETIME IS NULL    
 SELECT @END_EFFECTIVE_DATETIME,@CO_APPLCIANT_ID AS CO_APPLCIANT_ID
ELSE IF @END_EFFECTIVE_DATETIME IS NULL    
 SELECT @REIN_EFFECTIVE_DATETIME,'0' AS CO_APPLCIANT_ID
ELSE IF (@END_EFFECTIVE_DATETIME < @REIN_EFFECTIVE_DATETIME)    
 SELECT @REIN_EFFECTIVE_DATETIME,'0' AS  CO_APPLCIANT_ID 
ELSE    
 SELECT @END_EFFECTIVE_DATETIME ,@CO_APPLCIANT_ID AS  CO_APPLCIANT_ID 
    
END 

GO

