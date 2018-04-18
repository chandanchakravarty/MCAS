IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UPolicyStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UPolicyStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                
 Proc Name      : dbo.Proc_UPolicyStatus                
 Created By     : Praveen Kumar
 Created date   : 30/07/2010   
 Purpose        : To get under progress policy status   
 used in        : EbixAdvantage    
  
 DROP PROC Proc_UPolicyStatus    
 -------------------------------------------------------*/     
CREATE proc [dbo].[Proc_UPolicyStatus]                
( 
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID int  
 )    
AS    
 BEGIN 
 
SELECT POLICY_STATUS FROM POL_CUSTOMER_POLICY_LIST where POLICY_STATUS in ('UCANCL',
'UENDRS','UREINST','URENEW','UNDERNONRENEW','UNEGTE','UCORUSER','UISSUE','UREWRITE',
'UDECPOL','UREVERT','SUSPENDED','APPLICATION')
AND CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID

END

GO

