IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPostedPremiumProcessDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPostedPremiumProcessDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*=======================================================================================    
PROC NAME        : dbo.Proc_GetPostedPremiumProcessDetails          
CREATED BY       : Pravesh K Chandel    
DATE             : 10 may 07    
PURPOSE          : fetching the Posted Premium Propces Details While Reverting Endorsement  
REVISON HISTORY  :                  
USED IN          :   WOLVERINE                  
=========================================================================================    
DATE     REVIEW BY          COMMENTS                  
=====   =============      ==============================================================*/    
--Proc_GetPostedPremiumProcessDetails 1430,10,2,'14,14,14,14'  
  
--drop proc Proc_GetPostedPremiumProcessDetails  
create proc Proc_GetPostedPremiumProcessDetails  
(  
 @CUSTOMER_ID       INT,      
 @POLICY_ID        INT,      
 @POLICY_VERSION_ID  SMALLINT  ,  
 @PROCESS_TYPE varchar(50)  
)  
as  
BEGIN  
declare @mySql varchar(1000)  
set @mysql='SELECT PROCESS_ID,PROCESS_TYPE,TERM_TYPE,  
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,Isnull(PREMIUM_AMOUNT,0) PREMIUM_AMOUNT,Isnull(PREMIUM_XML,'''') PREMIUM_XML,isnull(POSTED_PREMIUM_XML,'''') POSTED_PREMIUM_XML  
  FROM ACT_PREMIUM_PROCESS_DETAILS WITH(NOLOCK) WHERE CUSTOMER_ID=' + convert(varchar,@CUSTOMER_ID) + ' AND POLICY_ID=' +  convert(varchar,@POLICY_ID) + ' AND POLICY_VERSION_ID >' +  convert(varchar,@POLICY_VERSION_ID) 
 -- + ' AND PROCESS_TYPE IN (' + @PROCESS_TYPE + ')' 
 
exec (@mysql)  

set @mysql='SELECT  APSD.CUSTOMER_ID,APSD.POLICY_ID,APSD.RISK_ID,APSD.RISK_TYPE AS RISK_TYPE ,SUM(NET_PREMIUM) AS NET_PREMIUM,SUM(STATS_FEES) AS STATS_FEES,
SUM (GROSS_PREMIUM) AS GROSS_PREMIUM,SUM(INFORCE_PREMIUM) AS INFORCE_PREMIUM,SUM(INFORCE_FEES) AS INFORCE_FEES
  FROM ACT_PREMIUM_PROCESS_SUB_DETAILS APSD WITH(NOLOCK) INNER JOIN ACT_PREMIUM_PROCESS_DETAILS APD WITH(NOLOCK) ON APD.PROCESS_ID=APSD.PPD_ROW_ID 
 WHERE APSD.CUSTOMER_ID=' + convert(varchar,@CUSTOMER_ID) + ' AND APSD.POLICY_ID=' +  convert(varchar,@POLICY_ID) + ' AND APSD.POLICY_VERSION_ID>' +  convert(varchar,@POLICY_VERSION_ID) +
-- ' AND APD.PROCESS_TYPE IN (' + @PROCESS_TYPE + ')' + 
' GROUP BY APSD.CUSTOMER_ID,APSD.POLICY_ID,APSD.RISK_ID,APSD.RISK_TYPE '

exec (@mysql)  

  
END  
  
  



GO

