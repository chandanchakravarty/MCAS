IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCommission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCommission]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                          
Proc Name               : Dbo.Proc_GetCommission                                          
Created by              : Swarup                                          
Date                    : 25/06/07                                        
Purpose                 : To get the information of  commission                                            
Revison History    :                                          
                                 
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------*/                                          
-- drop PROC Dbo.Proc_GetCommission 'A'      
CREATE  PROC [dbo].[Proc_GetCommission]                                       
(                                          
 @COMMISSION_TYPE varchar(10)                                          
)                                          
AS                                          
                                          
BEGIN     
 IF  (@COMMISSION_TYPE = 'R')    
 BEGIN     
  SELECT       
   t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,      
   convert(varchar,t1.EFFECTIVE_FROM_DATE,101) as EFFECTIVE_FROM_DATE,      
  convert(varchar,t1.EFFECTIVE_TO_DATE,101) as EFFECTIVE_TO_DATE,      
   t1.COMMISSION_PERCENT,t1.MODIFIED_BY,      
   convert(varchar,t1.LAST_UPDATED_DATETIME,101) as LAST_UPDATED_DATETIME,      
   t1.IS_ACTIVE,      
   case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end as STATE_NAME,      
   case t1.LOB_ID when 0 then 'All' else t3.LOB_DESC end as LOB_DESC,      
   case t1.SUB_LOB_ID when 0 then 'All' else t4.SUB_LOB_DESC end as SUB_LOB_DESC,      
   case t1.TERM when 'F' then 'First Term(NBS)' else 'Other Term' END     
  as TermDesc,t5.USER_FNAME+' '+t5.USER_LNAME as userName,      
   T7.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC,t3.LOB_ID as LobId,t1.COUNTRY_ID      
  FROM           
   dbo.MNT_LOB_MASTER t3(nolock) RIGHT OUTER JOIN      
   dbo.ACT_REG_COMM_SETUP t1(nolock)  INNER JOIN       
   dbo.MNT_USER_LIST t5(nolock) ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN      
   dbo.MNT_LOOKUP_VALUES t6(nolock) ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON t3.LOB_ID = t1.LOB_ID FULL OUTER JOIN       
   dbo.MNT_SUB_LOB_MASTER t4(nolock) ON t1.SUB_LOB_ID = t4.SUB_LOB_ID AND t3.LOB_ID = t4.LOB_ID LEFT OUTER JOIN       
   dbo.MNT_COUNTRY_STATE_LIST t2(nolock) ON t1.STATE_ID = t2.STATE_ID LEFT OUTER JOIN      
   ACT_COMMISION_CLASS_MASTER T7(nolock) ON T1.CLASS_RISK=T7.COMMISSION_CLASS_ID      
  WHERE       
   COMMISSION_TYPE= @COMMISSION_TYPE     
  ORDER BY t1.STATE_ID     
 END     
 ELSE IF (@COMMISSION_TYPE = 'A')    
 BEGIN     
  SELECT t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,    
  convert(varchar,t1.EFFECTIVE_FROM_DATE,101) as EFFECTIVE_FROM_DATE,    
  convert(varchar,t1.EFFECTIVE_TO_DATE,101) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,    
  convert(varchar,t1.LAST_UPDATED_DATETIME,101) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,    
  case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end as STATE_NAME,    
  case t1.LOB_ID when 0 then 'All' else t3.LOB_DESC end as LOB_DESC,    
  case t1.SUB_LOB_ID when 0 then 'All' else t4.SUB_LOB_DESC end as SUB_LOB_DESC,    
  case t1.TERM when 'F' then 'First Term(NBS)' else 'Other Term' END     
  as TermDesc,t5.USER_FNAME+' '+t5.USER_LNAME as userName,    
  T8.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC,t7.AGENCY_DISPLAY_NAME,t3.LOB_ID as LobId,t7.AGENCY_ID,t1.COUNTRY_ID     
  FROM       
  dbo.MNT_SUB_LOB_MASTER t4(nolock) FULL OUTER JOIN    
  dbo.MNT_LOB_MASTER t3(nolock) RIGHT OUTER JOIN    
  dbo.MNT_AGENCY_LIST t7(nolock) INNER JOIN    
  dbo.ACT_REG_COMM_SETUP t1(nolock) INNER JOIN    
  dbo.MNT_USER_LIST t5(nolock) ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN    
  dbo.MNT_LOOKUP_VALUES t6(nolock) ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON     
  t7.AGENCY_ID = t1.AGENCY_ID ON t3.LOB_ID = t1.LOB_ID ON     
  t4.SUB_LOB_ID = t1.SUB_LOB_ID AND t4.LOB_ID = t3.LOB_ID LEFT OUTER JOIN    
  dbo.MNT_COUNTRY_STATE_LIST t2(nolock) ON t1.STATE_ID = t2.STATE_ID  LEFT OUTER JOIN    
  ACT_COMMISION_CLASS_MASTER T8(nolock) ON T1.CLASS_RISK=T8.COMMISSION_CLASS_ID    
  WHERE    
  COMMISSION_TYPE =@COMMISSION_TYPE    
  ORDER BY AGENCY_DISPLAY_NAME asc    
 END     
 ELSE IF ( @COMMISSION_TYPE='P')    
 BEGIN     
  SELECT t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.TERM,    
  convert(varchar,t1.EFFECTIVE_FROM_DATE,101) as EFFECTIVE_FROM_DATE,    
  convert(varchar,t1.EFFECTIVE_TO_DATE,101) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,    
  convert(varchar,t1.LAST_UPDATED_DATETIME,101) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,    
  t2.STATE_NAME as STATE_NAME,    
  t3.LOB_DESC as LOB_DESC,    
  case t1.TERM when 'F' then 'First Term(NBS)' else 'Other Term' end as TermDesc,    
  t5.USER_FNAME+' '+t5.USER_LNAME as userName,    
  T8.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC,t1.REMARKS    
  FROM        
  MNT_LOB_MASTER t3(nolock) INNER JOIN    
  ACT_REG_COMM_SETUP t1(nolock) INNER JOIN    
  MNT_USER_LIST t5(nolock) ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN    
  MNT_LOOKUP_VALUES t6(nolock) ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON t3.LOB_ID = t1.LOB_ID LEFT OUTER JOIN    
  MNT_COUNTRY_STATE_LIST t2(nolock) ON t1.STATE_ID = t2.STATE_ID  LEFT OUTER JOIN    
  ACT_COMMISION_CLASS_MASTER T8(nolock) ON T1.CLASS_RISK=T8.COMMISSION_CLASS_ID    
  WHERE COMMISSION_TYPE =@COMMISSION_TYPE    
 END     
 ELSE IF ( @COMMISSION_TYPE='C')    
 BEGIN     
  SELECT t1.COMM_ID,t1.STATE_ID,t1.LOB_ID,t1.SUB_LOB_ID,t1.CLASS_RISK,t1.TERM,    
  convert(varchar,t1.EFFECTIVE_FROM_DATE,101) as EFFECTIVE_FROM_DATE,    
  convert(varchar,t1.EFFECTIVE_TO_DATE,101) as EFFECTIVE_TO_DATE,t1.COMMISSION_PERCENT,t1.MODIFIED_BY,    
  convert(varchar,t1.LAST_UPDATED_DATETIME,101) as LAST_UPDATED_DATETIME,t1.IS_ACTIVE,    
  case t1.STATE_ID when 0 then 'All' else t2.STATE_NAME end as STATE_NAME,    
  case t1.LOB_ID when 0 then 'All' else t3.LOB_DESC end as LOB_DESC,    
  case t1.TERM when 'F' then 'First Term(NBS)' else 'Other Term' end as TermDesc,    
  t5.USER_FNAME+' '+t5.USER_LNAME as userName,    
  T8.CLASS_DESCRIPTION as LOOKUP_VALUE_DESC,t1.AMOUNT_TYPE    
  FROM     
  MNT_LOB_MASTER t3(nolock) INNER JOIN        
  ACT_REG_COMM_SETUP t1(nolock) INNER JOIN    
  MNT_USER_LIST t5(nolock) ON t1.MODIFIED_BY = t5.USER_ID LEFT OUTER JOIN    
  MNT_LOOKUP_VALUES t6(nolock) ON t1.CLASS_RISK = t6.LOOKUP_UNIQUE_ID ON t3.LOB_ID = t1.LOB_ID LEFT OUTER JOIN    
  MNT_COUNTRY_STATE_LIST t2(nolock) ON t1.STATE_ID = t2.STATE_ID  LEFT OUTER JOIN    
  ACT_COMMISION_CLASS_MASTER T8(nolock) ON T1.CLASS_RISK=T8.COMMISSION_CLASS_ID    
    
  WHERE COMMISSION_TYPE=@COMMISSION_TYPE    
  ORDER BY STATE_NAME ASC    
 END     
END      
  
  
  
  
  
  
  
GO

