IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDiscountSurcharge]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDiscountSurcharge]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                              
Proc Name       : dbo.Proc_GetPolicyDiscountSurcharge                                              
  
Modified by   : Pradeep Kushwaha                           
Modified On   : 29-June-2010                          
Purpose       : To get the Discount charges based on the called from .
				If the called from policy then retrive the data on customer id ,policy id and policy version id 
				else retrive the data on customer id ,policy id ,policy version id and risk id <>0
Drop PROC Proc_GetPolicyDiscountSurcharge        
*/
CREATE PROC [dbo].[Proc_GetPolicyDiscountSurcharge]        
(        
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT ,
@CALLEDFROM NVARCHAR(20),
@RISK_ID INT=NULL      
)        
AS        
BEGIN   
IF(@CALLEDFROM='POLICY')
	BEGIN
		SELECT POLDISCOUNT.DISCOUNT_ID,POLDISCOUNT.DISCOUNT_ROW_ID,MNTDISCOUNT.DISCOUNT_DESCRIPTION,POLDISCOUNT.PERCENTAGE,POLDISCOUNT.IS_ACTIVE STATUS FROM POL_DISCOUNT_SURCHARGE AS POLDISCOUNT     
		LEFT OUTER JOIN MNT_DISCOUNT_SURCHARGE AS MNTDISCOUNT ON POLDISCOUNT.DISCOUNT_ID=MNTDISCOUNT.DISCOUNT_ID      
		WHERE POLDISCOUNT.CUSTOMER_ID=@CUSTOMER_ID        
		AND POLDISCOUNT.POLICY_ID=@POLICY_ID AND POLDISCOUNT.POLICY_VERSION_ID=@POLICY_VERSION_ID AND RISK_ID=0       
	END
	ELSE
	BEGIN
		SELECT POLDISCOUNT.DISCOUNT_ID,POLDISCOUNT.DISCOUNT_ROW_ID,MNTDISCOUNT.DISCOUNT_DESCRIPTION,POLDISCOUNT.PERCENTAGE,POLDISCOUNT.IS_ACTIVE STATUS FROM POL_DISCOUNT_SURCHARGE AS POLDISCOUNT     
		LEFT OUTER JOIN MNT_DISCOUNT_SURCHARGE AS MNTDISCOUNT ON POLDISCOUNT.DISCOUNT_ID=MNTDISCOUNT.DISCOUNT_ID      
		WHERE POLDISCOUNT.CUSTOMER_ID=@CUSTOMER_ID        
		AND POLDISCOUNT.POLICY_ID=@POLICY_ID AND POLDISCOUNT.POLICY_VERSION_ID=@POLICY_VERSION_ID  AND RISK_ID=@RISK_ID      
	END

END 

GO

