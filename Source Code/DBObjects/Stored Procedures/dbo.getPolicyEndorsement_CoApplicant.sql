IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getPolicyEndorsement_CoApplicant]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getPolicyEndorsement_CoApplicant]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                        
PROC NAME		: DBO.PROC_GETPOLICYINFORMATION                                                                        
CREATED BY      : Lalit Kr Chauhan	
DATE            : April 11, 2011                                                             
PURPOSE         : Get Co-Applicant on endorsement for master policy
                                                                      
------------------------------------------------------------        
drop proc getPolicyEndorsement_CoApplicant                                                                
*/                                                                  
Create proc [dbo].[getPolicyEndorsement_CoApplicant]
(
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT
)
AS 
BEGIN
SELECT * FROM POL_POLICY_PROCESS WITH(NOLOCK)
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID 
AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_STATUS = 'PENDING'
AND PROCESS_ID = 3 --for endorsemet in progress 

END
GO

