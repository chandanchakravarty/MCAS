IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetChecksForPrint]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetChecksForPrint]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.GetChecksForPrint  
Created by      : Ajit Singh Chahal  
Date            : 7/14/2005  
Purpose       :Evaluation  
Revison History :  
Used In        : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
CREATE PROC Dbo.Proc_GetChecksForPrint  
(  
@fromDate     datetime,  
@toDate datetime,  
@checkType int  
)  
AS  
BEGIN  
if @checktype<>0  
 select   
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
   
 where  CHECK_TYPE = @checktype and CHECK_DATE>=@fromDate and CHECK_DATE<@toDate  
else  
 select   
 CHECK_TYPE,  
 (select LOOKUP_VALUE_DESC from mnt_lookup_values where LOOKUP_UNIQUE_ID =CHECK_TYPE) as CHECK_TYPE_DESC ,  
 (select ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=ACT_CHECK_INFORMATION.ACCOUNT_ID) as ACC_DISP_NUMBER,  
 CHECK_NUMBER ,  
 convert(varchar,CHECK_DATE,101) as CHECK_DATE,  
 PAYEE_ENTITY_NAME,  
 CHECK_AMOUNT ,  
 case  when GL_UPDATE='2' then 'VOID' else  
 case when IS_BNK_RECONCILED='Y' then 'RECONCILIED'   
 when isnull(IS_BNK_RECONCILED,'N')='N' or  IS_BNK_RECONCILED ='' then 'UNRECONCILIED' end  
 end as Staus,  
 (select ACC_DISP_NUMBER from ACT_GL_ACCOUNTS WHERE ACCOUNT_ID=ACT_CHECK_INFORMATION.OFFSET_ACC_ID) as "OFFSET_ACC_ID",  
 case isnull(AVAILABLE_BALANCE,0) when 0 then 'No' else 'Yes' end as "Cleared"  
 from  ACT_CHECK_INFORMATION where  CHECK_DATE>=@fromDate and CHECK_DATE<@toDate 
ORDER BY CHECK_TYPE  
END  
  
  
  
  
  
  
  
  



GO

