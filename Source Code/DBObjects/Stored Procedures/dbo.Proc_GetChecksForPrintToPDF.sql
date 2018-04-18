IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetChecksForPrintToPDF]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetChecksForPrintToPDF]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_GetChecksForPrintToPDF  
Created by      : Ajit Singh Chahal  
Date            : 7/14/2005  
Purpose       :to print pdf checks  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetChecksForPrintToPDF  
(  
@ACCOUNT_ID int  
)  
AS  
BEGIN  
  
 select IS_COMMITED,SPOOL_STATUS,PAYEE_ENTITY_NAME,  
 PAYEE_ADD1,PAYEE_ADD2,PAYEE_CITY,PAYEE_STATE,PAYEE_ZIP ,CHECK_AMOUNT,CHECK_MEMO,  
 --(ACT_BANK_INFORMATION) as   
 CHECK_TYPE,  
 (select LOOKUP_VALUE_DESC from mnt_lookup_values where LOOKUP_UNIQUE_ID =CHECK_TYPE) as CHECK_TYPE_DESC  ,  
 (select ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=ACT_CHECK_INFORMATION.ACCOUNT_ID)as ACC_DISP_NUMBER ,  
 CHECK_NUMBER ,  
 convert(varchar,CHECK_DATE,101) as CHECK_DATE,  
 PAYEE_ENTITY_NAME ,  
 CHECK_AMOUNT ,  
 case  when GL_UPDATE='2' then 'VOID' else  
 case when IS_BNK_RECONCILED='Y' then 'RECONCILIED'   
 when isnull(IS_BNK_RECONCILED,'N')='N' or  IS_BNK_RECONCILED ='' then 'UNRECONCILIED' end  
 end as Staus,  
 (select ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=ACT_CHECK_INFORMATION.OFFSET_ACC_ID) as "OFFSET_ACC_ID",  
 case isnull(AVAILABLE_BALANCE,0) when 0 then 'No' else 'Yes' end as "Cleared"  
 from  ACT_CHECK_INFORMATION  
   
 where  ACCOUNT_ID = @ACCOUNT_ID  and isnull(IS_COMMITED,'N')='Y' and SPOOL_STATUS=0  
   
 select ATTACH_FILE_NAME from mnt_attachment_list where  ATTACH_ENT_ID=1  and  ATTACH_ENTITY_TYPE='ACT_BANK_INFORMATION'  
END  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  



GO

