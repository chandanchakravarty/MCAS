IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyTermEffectiveDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyTermEffectiveDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*  
proc dbo.Proc_UpdatePolicyTermEffectiveDate  
created by  :Pravesh k. Chandel  
dated  :20 feb 2007  
purpose  : to UPDATE POLICY TERM AND EFFECTIVE DATE,EXPIRY DATE WHILE REINSTATE PROCESS  
DROP PROC dbo.Proc_UpdatePolicyTermEffectiveDate  
*/  
  
CREATE proc dbo.Proc_UpdatePolicyTermEffectiveDate  
(  
@CUSTOMER_ID INT,  
@POLICY_ID  INT,  
@POLICY_VERSION_ID SMALLINT,  
@POLICY_EFFECTIVE_DATE DATETIME,  
@POLICY_EXPIRY_DATE DATETIME,  
@POLICY_TERMS INT,  
@POLICY_TYPE int =null  
)  
as  
begin  
  
IF EXISTS(SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID   
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID)  
BEGIN  
UPDATE POL_CUSTOMER_POLICY_LIST SET APP_TERMS=@POLICY_TERMS,

APP_EFFECTIVE_DATE=@POLICY_EFFECTIVE_DATE, 
 POLICY_EFFECTIVE_DATE=@POLICY_EFFECTIVE_DATE,
 POL_VER_EFFECTIVE_DATE=@POLICY_EFFECTIVE_DATE,

APP_EXPIRATION_DATE=@POLICY_EXPIRY_DATE,
 POLICY_EXPIRATION_DATE=@POLICY_EXPIRY_DATE,  
 POL_VER_EXPIRATION_DATE=@POLICY_EXPIRY_DATE 

WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID   
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
if (@POLICY_TYPE is not null)  
 UPDATE POL_CUSTOMER_POLICY_LIST SET POLICY_TYPE=@POLICY_TYPE  
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID   
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
  
END  
  
  
END  
  
  



GO

