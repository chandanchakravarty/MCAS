IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CLAIM_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CLAIM_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                                          
 Proc Name       : [dbo].[SP_CLAIM_DETAILS]                   
 Created by      : praveer panghal 
 Date            : 25 june 2011                         
 Purpose         : This stored procedure should return the information about values of reserves paid and pending                    of claims in a specific period.              
 Revison History :                                          
 Used In       : EbixAdvanatge  
 
 DROP PROC [dbo].[SP_CLAIM_DETAILS] '08/01/2011','10/01/2011','99000004''SH000003''01/10/2011','08/12/2012'
------   ------------       -------------------------*/    
CREATE PROC [dbo].[SP_CLAIM_DETAILS]
(

@ACTIVITY_START_DATE DATETIME=NULL,
@ACTIVITY_END_DATE DATETIME=NULL,
@CLAIM_NUMBER NVARCHAR(10)=NULL
)
AS
BEGIN       
         SELECT  
			ISNULL(CCI.CLAIM_NUMBER,'') AS id_claim 
		  , ISNULL(PCPL.POLICY_DISP_VERSION,'')cd_endorsement 
		  , CA.COMPLETED_DATE AS dt_payment
		 ,  ISNULL(CA.CLAIM_PAYMENT_AMOUNT,0)vl_totalPaid
		 ,  ISNULL(COI_NET_PAID_RESERVE,0)vl_totalPaidCoInsurer
		 ,  ISNULL(RI_NET_PAID_RESERVE,0)vl_totalPaidReinsurer
		 ,  ISNULL(( SELECT SUM(ISNULL(PAYMENT_AMT,0)) 
          FROM CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB  WITH(NOLOCK)
            WHERE  CACRB.CLAIM_ID = CA.CLAIM_ID AND COMP_ID=1 AND COMP_TYPE='CO'
            AND CACRB.ACTIVITY_ID=CA.ACTIVITY_ID) ,0)vl_reservePaidInsurer 
                         
          FROM POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK)        
             JOIN CLM_CLAIM_INFO CCI WITH(NOLOCK)  ON CCI.CUSTOMER_ID=PCPL.CUSTOMER_ID 
             AND CCI.POLICY_ID=PCPL.POLICY_ID AND CCI.POLICY_VERSION_ID= PCPL.POLICY_VERSION_ID 
             LEFT OUTER JOIN CLM_ACTIVITY CA WITH(NOLOCK) on CA.CLAIM_ID=CCI.CLAIM_ID
             
          WHERE  CCI.CLAIM_NUMBER IS NOT NULL AND CA.ACTION_ON_PAYMENT IN(180,181) AND CA.IS_ACTIVE='Y'
          AND CA.IS_VOIDED_REVERSED_ACTIVITY IS NULL
          AND(@ACTIVITY_START_DATE IS NULL 
               OR CA.COMPLETED_DATE >=CONVERT(NVARCHAR(20), @ACTIVITY_START_DATE , 101))        
          AND (@ACTIVITY_END_DATE IS NULL 
               OR CA.COMPLETED_DATE <=CONVERT(NVARCHAR(20), @ACTIVITY_END_DATE,101))
          AND (@CLAIM_NUMBER IS NULL OR CCI.CLAIM_NUMBER=CAST(@CLAIM_NUMBER as varchar(50)))    

END
GO

