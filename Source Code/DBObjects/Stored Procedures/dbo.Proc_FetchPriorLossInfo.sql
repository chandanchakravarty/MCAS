IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPriorLossInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPriorLossInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name      : dbo.Proc_FetchPriorLossInfo            
Created by       : Anurag Verma            
Date             : 4/2/2005            
Purpose       : retrieving data from app_prior_loss_info            
Revison History :            
Used In         : Wolverine            
Modified By            : Mohit Gupta            
Modified Date          : 14/10/2005            
Purpose                : Changing table MNT_LOOKUP_VALUES to MNT_LOB_MASTER in where clause.             
            
Modified By            : Mohit            
Modified Date          : 18/10/2005.            
Purpose                : Applying isnull function on OCCURENCE_DATE.               
------------------------------------------------------------            
Date     Review By          Comments            
DROP PROC dbo.Proc_FetchPriorLossInfo  1,2043  ,2      
------   ------------       -------------------------*/          
Create PROC [dbo].[Proc_FetchPriorLossInfo]            
@LOSS_ID INT,            
@CUSTOMER_ID INT  ,
@LANG_ID INT=NULL          
AS            
            
BEGIN            
SELECT PL.CUSTOMER_ID,PL.LOSS_ID,            
CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,case when @LANG_ID=2 then 103 else 101 end) OCCURENCE_DATE,            
convert(varchar(50),PL.CLAIM_DATE,case when @LANG_ID=2 then 103 else 101 end) CLAIM_DATE,PL.LOB,PL.LOSS_TYPE,            
--dbo.fun_FormatCurrency(PL.AMOUNT_PAID,@LANG_ID) AMOUNT_PAID,
PL.AMOUNT_PAID as AMOUNT_PAID,
--CONVERT(VARCHAR(15),PL.AMOUNT_PAID,1) AMOUNT_PAID,
CONVERT(VARCHAR(15),PL.AMOUNT_RESERVED,1) AMOUNT_RESERVED,
PL.CLAIM_STATUS,PL.LOSS_DESC,            
PL.REMARKS as DESC_OF_LOSS_AND_REMARKS,PL.MOD,PL.LOSS_RUN,PL.CAT_NO,PL.CLAIMID,PL.IS_ACTIVE,            
PL.CREATED_BY,PL.CREATED_DATETIME,PL.MODIFIED_BY,            
PL.LAST_UPDATED_DATETIME,        
ISNULL(CONVERT(VARCHAR(50),PL.OCCURENCE_DATE,case when @LANG_ID=2 then 103 else 101 end),'') + ' ' +  (CASE PL.LOB WHEN '-1' THEN 'Other' WHEN '' THEN '' ELSE isNull(LVM.LOB_DESC,LV.LOB_DESC) END) LOSS,            
LV.LOB_DESC LOB,'LOSS_ID='+cast(PL.LOSS_ID as varchar(8000)) as UniqueGrdId,          
PL.APLUS_REPORT_ORDERED,PL.DRIVER_ID,PL.DRIVER_NAME,PL.RELATIONSHIP,PL.CLAIMS_TYPE,PL.AT_FAULT,PL.CHARGEABLE,    
PL.NAME_MATCH, --Done for Itrack Issue 6723 on 27 Nov 09            
PL.LOSS_LOCATION,        
PL.CAUSE_OF_LOSS,        
case when PL.LOB in (1,6) then PLH.POLICY_NUMBER else PL.POLICY_NUM end as POLICY_NUM,        
PL.LOSS_CARRIER,      
PL.OTHER_DESC ,      
--ADDED BY PRAVESH        
PLH.LOCATION_ID ,        
PLH.LOSS_ADD1   ,        
PLH.LOSS_ADD2   ,        
PLH.LOSS_CITY   ,        
PLH.LOSS_STATE  ,        
PLH.LOSS_ZIP    ,         
PLH.CURRENT_ADD1,        
PLH.CURRENT_ADD2,        
PLH.CURRENT_CITY,         
PLH.CURRENT_STATE ,        
PLH.CURRENT_ZIP  ,         
PLH.POLICY_TYPE  ,         
PLH.POLICY_NUMBER,  
PLH.WATERBACKUP_SUMPPUMP_LOSS, --Added by Charles on 30-Nov-09 for Itrack 6647   
PLH.WEATHER_RELATED_LOSS --Added by Charles on 30-Nov-09 for Itrack 6647     
FROM APP_PRIOR_LOSS_INFO PL            
LEFT JOIN MNT_LOB_MASTER LV ON PL.LOB=LV.LOB_ID 
LEFT outer JOIN MNT_LOB_MASTER_MULTILINGUAL LVM ON LV.LOB_ID=LVM.LOB_ID and LANG_ID=@LANG_ID             
--added by pravesh        
left join PRIOR_LOSS_HOME PLH ON PLH.LOSS_ID=PL.LOSS_ID AND PLH.CUSTOMER_ID=PL.CUSTOMER_ID        
       
WHERE             
PL.LOSS_ID=@LOSS_ID AND PL.CUSTOMER_ID=@CUSTOMER_ID            
END            
      
    
  
GO

