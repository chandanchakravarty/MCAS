IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetNonPayDBCancellationsWithZeroBalance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetNonPayDBCancellationsWithZeroBalance]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[Proc_GetNonPayDBCancellationsWithZeroBalance]     
as
begin   
   
  select  distinct ISNULL(CCL.Customer_First_name,' ') +' '+ ISNULL(CCL.Customer_Middle_name,' ') +' '+ ISNULL(CCL.Customer_Last_name,' ') As Customer_name,     
  CPL.Policy_number, Tbl_bal.BALANCE, PPP.Due_Date AS NOTICE_DUE_DATE , PPP.Effective_DateTime AS CANCELLATION_EFFECTIVE_DATE    
  FROM POL_CUSTOMER_POLICY_LIST CPL    
  left outer join ( SELECT CPL.CUSTOMER_ID , CPL.POLICY_ID , CPL.CURRENT_TERM ,SUM( ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID , 0) ) AS BALANCE     
  FROM ACT_CUSTOMER_OPEN_ITEMS OI  INNER JOIN POL_CUSTOMER_POLICY_LIST CPL  ON OI.CUSTOMER_ID = CPL.CUSTOMER_ID     
  AND OI.POLICY_ID = CPL.POLICY_ID AND OI.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID     
  WHERE OI.UPDATED_FROM IN ('C','D','F','J') OR (OI.UPDATED_FROM IN ('P') AND OI.ITEM_TRAN_CODE_TYPE = 'PREM')    
  GROUP BY CPL.CUSTOMER_ID , CPL.POLICY_ID , CPL.CURRENT_TERM ) AS Tbl_bal  
  ON Tbl_bal.CUSTOMER_ID =  CPL.CUSTOMER_ID AND Tbl_bal.POLICY_ID = CPL.POLICY_ID AND Tbl_bal.CURRENT_TERM = CPL.CURRENT_TERM   
  inner join POL_POLICY_PROCESS PPP  ON CPL.customer_id = PPP.customer_id  and CPL.Policy_id = PPP.Policy_Id    
  and CPL.Policy_version_id = PPP.New_policy_version_id    
  inner join CLT_CUSTOMER_LIST CCL  ON CPL.customer_id = CCL.customer_id    
  WHERE PPP.PROCESS_ID = 2  and PPP.CANCELLATION_TYPE = 11969 and PPP.PROCESS_STATUS = 'PENDING' and Tbl_bal.Balance <='0.00'  
  ORDER BY CPL.Policy_number    
  
 END


GO

