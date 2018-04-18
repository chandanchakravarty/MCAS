
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_CUSTOMER_BALANCE_INFORMATION]    Script Date: 12/02/2011 16:59:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_INSERT_CUSTOMER_BALANCE_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_INSERT_CUSTOMER_BALANCE_INFORMATION]
GO
 
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_INSERT_CUSTOMER_BALANCE_INFORMATION]    Script Date: 12/02/2011 16:59:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Text
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROC  [dbo].[PROC_MIG_IL_INSERT_CUSTOMER_BALANCE_INFORMATION]     
 (      
 @CUSTOMER_ID   int,   -- Id of customer whose premium policy will be posted      
 @POLICY_ID  int,   -- Policy identification number      
 @POLICY_VERSION_ID int,   -- Policy version identification number      
 @SOURCE_ROW_ID  Int,  -- Source Row ID  ,    
 @COMMIT_DATE DATETIME    
)      
AS      
BEGIN      
 DECLARE @INSTALLMENT Int,      
  @FULL_PAY_PLAN_ID Int      
      
 SELECT @FULL_PAY_PLAN_ID = IDEN_PLAN_ID        
 FROM ACT_INSTALL_PLAN_DETAIL with(nolock)      
 WHERE ISNULL(SYSTEM_GENERATED_FULL_PAY,0) = 1      
      
 --Fetching the application or policy information      
 SELECT  @INSTALLMENT  =  ISNULL(CPL.INSTALL_PLAN_ID, 0)      
 FROM POL_CUSTOMER_POLICY_LIST CPL with(nolock)      
 WHERE CPL.CUSTOMER_ID = @CUSTOMER_ID       
 AND CPL.POLICY_ID = @POLICY_ID       
 AND CPL.POLICY_VERSION_ID = @POLICY_VERSION_ID      
      
 --Inserting the entry in customer balance informartion      
 INSERT INTO ACT_CUSTOMER_BALANCE_INFORMATION      
 (      
  CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,      
  OPEN_ITEM_ROW_ID, AMOUNT, AMOUNT_DUE,      
  TRAN_DESC,      
  UPDATED_FROM, CREATED_DATE,      
  IS_PRINTED, PRINT_DATE, SOURCE_ROW_ID      
 )      
 SELECT       
  ACOI.CUSTOMER_ID, ACOI.POLICY_ID, ACOI.POLICY_VERSION_ID,       
  IDEN_ROW_ID, ACOI.TOTAL_DUE, 0,      
  case ITEM_TRAN_CODE_TYPE      
   WHEN 'FEES' THEN       
    CASE ITEM_TRAN_CODE WHEN 'INSF' THEN 'Fees written.'      
    ELSE TRANS_DESC END      
   WHEN 'PREM' THEN       
     CASE ITEM_TRAN_CODE WHEN 'RINSP' THEN      
      CASE @INSTALLMENT       
      WHEN @FULL_PAY_PLAN_ID THEN 'Reinstatement Premium written.'      
      ELSE 'Installment created (Risk-' + CONVERT(VARCHAR, ACOI.RISK_ID) + ').'      
      END      
     WHEN  'NBSP' THEN '253' --'New Business Premium.'      
     WHEN  'NBSI' THEN '254' --'New Business Interest.'      
     WHEN  'NBSF' THEN '255' --'New Business Fee.'      
     WHEN  'NBST' THEN '256' --'New Business Tax.'       
     WHEN  'DISC' THEN '257' --'Discount given.'        
     WHEN  'ENDP' THEN '258' --'Endorsement Premium.'      
     WHEN  'ENDI' THEN '261' --'Endorsement Interest.'      
     WHEN  'ENDF' THEN '260' --'Endorsement Fee.'      
     WHEN  'ENDT' THEN '259' --'Endorsement Tax.'      
     WHEN  'RENP' THEN '438' --'Renewal Premium.'      
     WHEN  'RENI' THEN '439' --'Renewal Interest.'      
     WHEN  'RENF' THEN '440' --'Renewal Fee.'      
     WHEN  'RENT' THEN '441' --'Renewal Tax.'           
     ELSE CASE @INSTALLMENT       
      WHEN @FULL_PAY_PLAN_ID THEN 'Premium written.'      
      ELSE 'Installment created (Risk-' + CONVERT(VARCHAR, ACOI.RISK_ID) + ').'      
      END      
     END      
   WHEN 'DISC' THEN 'Discount given'       
  END,      
  'P', @COMMIT_DATE,      
  0,NULL, @SOURCE_ROW_ID      
 FROM       
  ACT_CUSTOMER_OPEN_ITEMS ACOI with(nolock)      
  LEFT JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD with(nolock)      
  ON ACOI.INSTALLMENT_ROW_ID  = INSD.ROW_ID      
 WHERE       
  ACOI.UPDATED_FROM = 'P'       
  AND ACOI.IDEN_ROW_ID = @SOURCE_ROW_ID      
  AND ACOI.CUSTOMER_ID = @CUSTOMER_ID       
  AND ACOI.POLICY_ID = @POLICY_ID       
  AND ACOI.POLICY_VERSION_ID = @POLICY_VERSION_ID      
 ORDER BY INSD.INSTALLMENT_NO, ACOI.IDEN_ROW_ID ,ACOI.SOURCE_EFF_DATE       
      
      
END      


GO
