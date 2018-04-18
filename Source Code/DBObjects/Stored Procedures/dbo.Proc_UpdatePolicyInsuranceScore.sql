IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyInsuranceScore]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyInsuranceScore]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
VIEW Name        : dbo.Proc_UpdatePolicyInsuranceScore                               
mODIFIED BY 	:PRAVESH  K CHANDEL
MODIFIED DATE	:25 JUNE 2008
PURPOSE			: Update policy level Insurance Score from Customer
Used In             :           Wolverine                    
DROP proc dbo.Proc_UpdatePolicyInsuranceScore
*/
CREATE proc [dbo].[Proc_UpdatePolicyInsuranceScore]
(
@CUSTOMER_ID int,
@POLICY_ID int,
@POLICY_VERSION_ID int,
@TRAN_DESC nvarchar(100) out
)
AS
BEGIN
 
DECLARE @OLD_APPLY_INSURANCE_SCORE numeric  ,
	 @APPLY_INSURANCE_SCORE NUMERIC ,   
	 @CUSTOMER_REASON_CODE nvarchar(10)  ,  
	 @CUSTOMER_REASON_CODE2 nvarchar(10) ,   
	 @CUSTOMER_REASON_CODE3 nvarchar(10)  ,  
	 @CUSTOMER_REASON_CODE4 nvarchar(10) 

--Get old Inurance score from policy :  

 SELECT @OLD_APPLY_INSURANCE_SCORE = APPLY_INSURANCE_SCORE  
	FROM POL_CUSTOMER_POLICY_LIST with(nolock) 
	WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
 --Get latest Inurance score from customer :   
  SELECT  
  @APPLY_INSURANCE_SCORE=CUSTOMER_INSURANCE_SCORE,  
  @CUSTOMER_REASON_CODE = ISNULL(CUSTOMER_REASON_CODE,''),  
  @CUSTOMER_REASON_CODE2 = ISNULL(CUSTOMER_REASON_CODE2,''),  
  @CUSTOMER_REASON_CODE3 = ISNULL(CUSTOMER_REASON_CODE3,''),  
  @CUSTOMER_REASON_CODE4 = ISNULL(CUSTOMER_REASON_CODE4,'')   
  FROM   
  CLT_CUSTOMER_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID    
  
 IF(@OLD_APPLY_INSURANCE_SCORE <> @APPLY_INSURANCE_SCORE)  
	 BEGIN  
	  UPDATE POL_CUSTOMER_POLICY_LIST SET   
	   APPLY_INSURANCE_SCORE = @APPLY_INSURANCE_SCORE,  
	   CUSTOMER_REASON_CODE  = @CUSTOMER_REASON_CODE,  
	   CUSTOMER_REASON_CODE2 = @CUSTOMER_REASON_CODE2,  
	   CUSTOMER_REASON_CODE3 = @CUSTOMER_REASON_CODE3,  
	   CUSTOMER_REASON_CODE4 = @CUSTOMER_REASON_CODE4   
	  WHERE   
	   CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
	           
	  set  @TRAN_DESC ='Insurance Score of Policy Changed from '+ convert(varchar,@OLD_APPLY_INSURANCE_SCORE)  + ' to '+ convert(varchar,@APPLY_INSURANCE_SCORE) + ' from Customer.;' 
	  END
END

GO

