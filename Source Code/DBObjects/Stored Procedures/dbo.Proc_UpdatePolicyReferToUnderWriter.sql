IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyReferToUnderWriter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyReferToUnderWriter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*      
proc name :Proc_UpdatePolicyReferToUnderWriter      
created by :Pravesh K. Chandel      
dated   :16 jan 2007      
purpose  : to update policy for refer to under writer on renewal      
    
mODIFIED by :Pravesh K. Chandel      
dated   :15 JULY 2008    
purpose  : to update policy for refer to under writer on renewal if customer address is not valid    
    
DROP PROC dbo.Proc_UpdatePolicyReferToUnderWriter      
*/      
create procedure [dbo].[Proc_UpdatePolicyReferToUnderWriter]      
(      
 @CUSTOMER_ID     int,                      
 @POLICY_ID     int,                      
 @POLICY_VERSION_ID     smallint ,                     
 @RETVAL int OUTPUT   ,    
 @CALLEDFOR nvarchar(20)=null    
)      
as      
begin      
declare @REFER_TOUNDERWITER CHAR     
DECLARE @REFERAL_INSTRUCTIONS NVARCHAR(500)    
    
IF (UPPER(ISNULL(@CALLEDFOR,''))='ADDRESS_NOT_VALID')     
BEGIN    
 SELECT @REFERAL_INSTRUCTIONS=ISNULL(REFERAL_INSTRUCTIONS,'') FROM POL_CUSTOMER_POLICY_LIST  with(nolock)     
  WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
  UPDATE POL_CUSTOMER_POLICY_LIST SET REFER_UNDERWRITER='Y',    
    REFERAL_INSTRUCTIONS=    
 CASE WHEN @REFERAL_INSTRUCTIONS LIKE '%Customer Address could not be validated while fetching Insurance Score%' THEN REFERAL_INSTRUCTIONS + ''     
 ELSE    
 ISNULL(REFERAL_INSTRUCTIONS,'') + ' ' + 'Customer Address could not be validated while fetching Insurance Score.'    
 END    
  WHERE CUSTOMER_ID=@CUSTOMER_ID  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
END    
ELSE    
BEGIN    
 IF EXISTS(SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST with(nolock) WHERE CUSTOMER_ID=@CUSTOMER_ID       
  AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)      
  BEGIN      
   SELECT @REFER_TOUNDERWITER=ISNULL(REFER_UNDERWRITER,'') FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID       
   AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
    
   IF (@REFER_TOUNDERWITER='Y')      
    SET @RETVAL=2   --ALREADY CHECKED OFF      
   ELSE      
  begin    
   SET @RETVAL=1    
    if(@CALLEDFOR='VIOLATION')    
    UPDATE POL_CUSTOMER_POLICY_LIST SET REFER_UNDERWRITER='Y',    
    REFERAL_INSTRUCTIONS='Process could not be committed, rule could not be verified and this Policy has been marked as "refer to underwriter".'--Added by Charles on 25-Aug-09 for Itrack 6280    
 WHERE CUSTOMER_ID=@CUSTOMER_ID       
     AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID      
  end    
  END      
END      
      
end      
GO

