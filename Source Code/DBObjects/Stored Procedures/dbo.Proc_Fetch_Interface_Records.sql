IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Fetch_Interface_Records]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Fetch_Interface_Records]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name    : dbo.Proc_Fetch_Interface_Records                            
Created by   : Shubhanshu Pandey           
Date         : 27-4-2010                  
Purpose      :        
                              
 ----------------------------------------------------------                                          
Date     Review By          Comments                                        
 drop proc dbo.Proc_Fetch_Interface_Records 4,1,'sent to pagnet','05/02/2011','05/16/2011'               
----   ------------       -------------------------*/                             
CREATE PROC [dbo].[Proc_Fetch_Interface_Records]   
( 
	@FILE_ID INT=null, 
	@flagStatus INT=null, 
	@STATUS VARCHAR(500)='',
	@FROM_DATE datetime=null,  
    @TO_DATE datetime=null,
    @FILE_TYPE INT=null 
)
AS  
BEGIN
	
IF (@flagStatus	=0)
BEGIN
SELECT 
 ROW_NUMBER() OVER (ORDER BY [SEQUENCE_OF_RECORD]) AS RECORD_ID,
 [FILE_ID], APÓLICE+APÓLICE_CONT AS APÓLICE_CONT,PAYMENT_METHOD,
 PAYEE_ID_CPF_CNPJ,PAYMENT_ID,
 CARRIER_POLICY_BRANCH_CODE,INVOICE_DUE_DATE,
 INVOICE_ISSUANCE_DATE,REFUND_AMOUNT,REFUND_PAYMENT_DESCRIPTION,
 RETURN_STATUS,PEF.FILE_NAMES,
 ENDORSEMENT_NUMBER,PARCELA AS INSTALLMENT_NUMBER,
 CLAIM_NUMBER = (SELECT CLAIM_NUMBER FROM CLM_CLAIM_INFO WHERE CLAIM_ID =  CASE WHEN CHARINDEX('SEP',PER.PAYMENT_ID,1) > 0
  THEN CAST( dbo.Piece(PER.PAYMENT_ID,'SEP',1) AS INT)
  ELSE NULL
  END ),BENEFICIARY_NAME
 
  FROM PAGNET_EXPORT_RECORD PER WITH(NOLOCK)  
  INNER JOIN PAGNET_EXPORT_FILES PEF WITH(NOLOCK)
  ON PER.[FILE_ID] = PEF.ID
 WHERE PER.[FILE_ID] = @FILE_ID 
END

IF (@flagStatus	=1)
BEGIN
SELECT 
 ROW_NUMBER() OVER (ORDER BY [SEQUENCE_OF_RECORD]) AS RECORD_ID,
 [FILE_ID], APÓLICE+APÓLICE_CONT AS APÓLICE_CONT,PAYMENT_METHOD,
 PAYEE_ID_CPF_CNPJ,PAYMENT_ID,
 CARRIER_POLICY_BRANCH_CODE,INVOICE_DUE_DATE,
 INVOICE_ISSUANCE_DATE,REFUND_AMOUNT,REFUND_PAYMENT_DESCRIPTION,
 RETURN_STATUS,PEF.FILE_NAMES,
 ENDORSEMENT_NUMBER,PARCELA AS INSTALLMENT_NUMBER,
 CLAIM_NUMBER = (SELECT CLAIM_NUMBER FROM CLM_CLAIM_INFO WHERE CLAIM_ID =  CASE WHEN CHARINDEX('SEP',PER.PAYMENT_ID,1) > 0
  THEN CAST( dbo.Piece(PER.PAYMENT_ID,'SEP',1) AS INT)
  ELSE NULL
  END ),BENEFICIARY_NAME
 
  FROM PAGNET_EXPORT_RECORD PER WITH(NOLOCK)  
  INNER JOIN PAGNET_EXPORT_FILES PEF WITH(NOLOCK)
  ON PER.[FILE_ID] = PEF.ID
 WHERE  PER.RETURN_STATUS = @STATUS
  AND   
 PER.CREATE_DATETIME between @FROM_DATE AND DATEADD(DD,1,@TO_DATE)  
 and cast(PEF.INTERFACE_CODE as int) = CAST(@FILE_ID as INT)
END
END

GO

