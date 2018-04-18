IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyCancelRenewedDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyCancelRenewedDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*      
proc dbo.Proc_GetPolicyCancelRenewedDate      
created by  :Pravesh k. Chandel      
dated  :20 feb 2007      
purpose  : to get Cancel or Renewed date of a policy which has been cancelled or renewed to launch Reinstate Process      
  
modified by  :Pravesh K chandel  
Modified date : 6 sep 2007  
Purpose  : to get Carry forwar amount a policy which has been cancelled for Reinstate Process      

modified by  :Lalit kr Chauhan
Modified date : March 08,2011
DROP PROC dbo.[Proc_GetPolicyCancelRenewedDate]       2156,509,2
*/      
      
CREATE proc [dbo].[Proc_GetPolicyCancelRenewedDate]      
(      
@CUSTOMER_ID INT,      
@POLICY_ID INT,      
@POLICY_VERSION_ID SMALLINT,      
@RETVAL  VARCHAR(20) = null OUT ,  
@CFD_AMOUNT  numeric(10,2) =null OUT      
)      
as      
begin      
      
DECLARE @EFFECTIVE_DATE DATETIME      
DECLARE @CFD_AMT numeric(10,2)      
SELECT @EFFECTIVE_DATE=max(EFFECTIVE_DATETIME) ,@CFD_AMT=max(isnull(CFD_AMT,0)) FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID       
-- AND POLICY_VERSION_ID=@POLICY_VERSION_ID-1   
AND POLICY_VERSION_ID=  
 (select max(policy_version_id) FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID IN (2,12) and PROCESS_STATUS='COMPLETE')  
AND PROCESS_ID in (2,12) and PROCESS_STATUS='COMPLETE'  
  
 --12 FOR COMMIT CONCELLATION      
IF (@EFFECTIVE_DATE IS NULL)      
SELECT @EFFECTIVE_DATE=max(EFFECTIVE_DATETIME),@CFD_AMT=max(isnull(CFD_AMT,0)) FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID       
-- AND POLICY_VERSION_ID=@POLICY_VERSION_ID-1   
 AND POLICY_VERSION_ID =(select max(policy_version_id) FROM POL_POLICY_PROCESS with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_ID=20)  
 AND PROCESS_ID=20      
 --20 FOR COMMIT NON RENEWAL       
SET @RETVAL=CAST(@EFFECTIVE_DATE AS DATE)
SET @CFD_AMOUNT=isnull(@CFD_AMT,0)  
      SELECT  @RETVAL
end      
--go    
--  EXEc [Proc_GetPolicyCancelRenewedDate]       2156,509,2

  
  
  
  
GO

