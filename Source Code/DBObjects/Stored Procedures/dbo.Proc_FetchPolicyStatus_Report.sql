
/**************************************************      
CREATED BY    : Pravesh K Chandel     
CREATED DATETIME : 14 Nov 2011     
PURPOSE    : Fetch Policy Status Report (iTrack 1196)
Used	: EAW-ALBA
Review      :
Review By  Date   
Purpose      
DROP PROC Proc_FetchPolicyStatus_Report 
***************************************************/      
create proc dbo.Proc_FetchPolicyStatus_Report
as


-- this report is same as Policy Tab Under Customer Asistance.
declare @LANG_ID smallint
set @LANG_ID=2

select Apólice,Versão,Status,Proposta, Início_de_Vigência,Final_de_Vigência,Início_Vig_Alterado,Produto,Tipo_Cobrança
from

(select distinct PPCL.POLICY_DISP_VERSION Versão,
 
 ISNULL(PPCL.POLICY_NUMBER,'') AS Apólice,
 --isnull(ppcl.policy_lob,'') policy_lob,
dbo.fun_GetPolicyDisplayStatus(ppcl.customer_id,ppcl.policy_id,ppcl.policy_version_id,@LANG_ID)
 + CASE WHEN PPP.REVERT_BACK  = 'Y' THEN 
 CASE WHEN @LANG_ID=2 THEN   '(Cancelar)' ELSE  '(Undo)' END
   ELSE '' END  AS Status,
isnull(convert(varchar(15),ppcl.app_effective_date,
case @LANG_ID when 2 then 103 else 101 end) ,'') AS Início_de_Vigência,
isnull(convert(varchar(15),ppcl.APP_EXPIRATION_DATE,
case @LANG_ID when 2 then 103 else 101 end),'') AS Final_de_Vigência,
isnull(LVM.LOB_DESC,LV.LOB_DESC) as Produto, 
--ppcl.policy_id,ppcl.app_id,ppcl.app_version_id,ppcl.customer_id customer_id,
--LV.LOB_ID,ppcl.policy_version_id,1 DRP, 
--case @LANG_ID when 2 then 'IR' else 'GO' end as [GO],
 --'" + strProcess + @"' as process_value,
 ISNULL(STATE_CODE,case @LANG_ID when 2 then 'Todos' else 'All' end) AS STATE_CODE,
 ISNULL(ppcl.APP_NUMBER,'') + ' ' + ISNULL(ppcl.APP_VERSION,'') Proposta,
 --CASE WHEN PPCL.POLICY_STATUS IN('INACTIVE','EXPIRED') THEN 'N' ELSE 'Y' END AS STATUS_POLICY ,   
 case when LVM.LANG_ID=2 then case when ppcl.BILL_TYPE='DB'then 'DIR' else 'AB'end   else PPCL.BILL_TYPE end   AS Tipo_Cobrança,
 --case when quote_xml is not null  then '<img src=" + (rootPath) +   @"/cmsweb/images/quote.gif style=''Border-Width:0'' ImageAlign=absMiddle>'  else '' end  as quote_xml ,
 isnull(convert(varchar(15),ppcl.POL_VER_EFFECTIVE_DATE,case @LANG_ID when 2 then 103 else 101 end),'')  as Início_Vig_Alterado,--policy_VER_EFFECTIVE_date,
 --pol_VER_EFFECTIVE_date,
 --PPCL.AGENCY_ID,
 AG.AGENCY_CODE 
 FROM POL_CUSTOMER_POLICY_LIST ppcl with(nolock) 
 left OUTER join MNT_LOB_MASTER LV with(nolock) on ppcl.policy_lob=LV.LOB_ID	
 left OUTER join MNT_LOB_MASTER_MULTILINGUAL LVM with(nolock) on ppcl.policy_lob=LVM.LOB_ID and LVM.LANG_ID = @LANG_ID
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST MSCL with(nolock) ON MSCL.STATE_ID=ppcl.STATE_ID 
 LEFT OUTER JOIN QOT_CUSTOMER_QUOTE_LIST_POL QC with(nolock) ON PPCL.CUSTOMER_ID = QC.CUSTOMER_ID AND PPCL.POLICY_ID = QC.POLICY_ID 
 AND PPCL.POLICY_VERSION_ID = QC.POLICY_VERSION_ID 
 LEFT OUTER JOIN MNT_AGENCY_LIST AG with(nolock) ON AG.AGENCY_ID=PPCL.AGENCY_ID LEFT OUTER JOIN POL_POLICY_PROCESS PPP WITH(NOLOCK) ON PPP.CUSTOMER_ID = ppcl.customer_id and 
 PPP.POLICY_ID = ppcl.POLICY_ID and PPP.NEW_POLICY_VERSION_ID = ppcl.POLICY_VERSION_ID
 where ppcl.APP_STATUS<>'REJECT'  
 ) ppcl 

where UPPER(RTRIM(LTRIM(ISNULL(Apólice,'')))) <> ''
--AND UPPER(RTRIM(LTRIM(ISNULL(policy_number,''))))='050452011020982000040'
 Order By UPPER(RTRIM(LTRIM(ISNULL(Apólice,''))))
  , cast(ppcl.Versão as nvarchar(6))

 asc
 