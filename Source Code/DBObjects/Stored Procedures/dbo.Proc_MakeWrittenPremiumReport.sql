IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MakeWrittenPremiumReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MakeWrittenPremiumReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran   
--DROP PROC dbo.Proc_MakeWrittenPremiumReport                                           
--  
--go   
/*----------------------------------------------------------                                                                                
Proc Name       : dbo.Proc_MakeWrittenPremiumReport                                                
Created by      : Mohit Agarwal                                                                           
Date            : 22/10/2008                                                                                
Purpose         : Generates written premium report its GL A/c wise           
Revewed by      :                                                                                
Revison History :                                                                                
Used In        : Wolverine                
Proc_MakeWrittenPremiumReport '2008-10-3'                                                                  
------------------------------------------------------------                                                                                
Date     Review By          Comments                                                                                
------   ------------       -------------------------*/                                                                                
--DROP PROC dbo.Proc_MakeWrittenPremiumReport                                           
CREATE PROC [dbo].[Proc_MakeWrittenPremiumReport]    
(  
 @AS_ON_DATE Datetime = NULL   
)                                              
                    
AS                                                                                
BEGIN                               
   
 DECLARE @FIRST_DAY_OF_MONTH Datetime   
 IF ( @AS_ON_DATE IS NULL )   
 BEGIN   
  SET @AS_ON_DATE  = GETDATE()  
 END  
   
 SET @FIRST_DAY_OF_MONTH =  CAST(CONVERT(VARCHAR,MONTH(@AS_ON_DATE))  + '/' + '01' + '/' + CONVERT(VARCHAR,YEAR(@AS_ON_DATE)) AS DATETIME)    ;
   
   
 with        
 NET_PREMIUM_TABLE          
 AS          
 (          
  SELECT SUM(TRANSACTION_AMOUNT)     
  AS WRITTEN_PREMIUM, LOB_ID   
  FROM ACT_ACCOUNTS_POSTING_DETAILS     
  WHERE ACCOUNT_ID =  
   (  
    SELECT INC_PRM_WRTN    
    FROM ACT_GENERAL_LEDGER WHERE 
	CAST(CONVERT(VARCHAR,@AS_ON_DATE,101) AS Datetime) 	>= CAST(CONVERT(VARCHAR,FISCAL_BEGIN_DATE,101) AS Datetime)
	AND	CAST(CONVERT(VARCHAR,@AS_ON_DATE,101) AS Datetime) 	<= CAST(CONVERT(VARCHAR,FISCAL_END_DATE,101) AS Datetime)
 
   )    
  AND DATEDIFF(DD,POSTING_DATE,@FIRST_DAY_OF_MONTH) <=0   
  AND DATEDIFF(DD, POSTING_DATE,@AS_ON_DATE) >=0   
  GROUP BY LOB_ID  
 ),          
           
           
 STAT_PREMIUM_TABLE          
 AS          
 (          
  SELECT SUM(TRANSACTION_AMOUNT)     
  AS WRITTEN_PREMIUM, LOB_ID   
  FROM ACT_ACCOUNTS_POSTING_DETAILS     
  WHERE ACCOUNT_ID =  
  (  
   SELECT INC_PRM_WRTN_OTH_STATE_ASSESS_FEE    
   FROM ACT_GENERAL_LEDGER WHERE 
	CAST(CONVERT(VARCHAR,@AS_ON_DATE,101) AS Datetime) 	>= CAST(CONVERT(VARCHAR,FISCAL_BEGIN_DATE,101) AS Datetime)
		AND	CAST(CONVERT(VARCHAR,@AS_ON_DATE,101) AS Datetime) 	<= CAST(CONVERT(VARCHAR,FISCAL_END_DATE,101) AS Datetime)
 
  )    
  AND DATEDIFF(DD,POSTING_DATE,@FIRST_DAY_OF_MONTH) <=0   
  AND DATEDIFF(DD, POSTING_DATE,@AS_ON_DATE) >=0   
  GROUP BY LOB_ID  
 ),          
    
          
  
 WRITTEN_PREMIUM_FINAL          
 AS          
 (          
  SELECT NPT.WRITTEN_PREMIUM *-1 AS NET_PREMIUM_WRITTEN ,  
  ISNULL(SPT.WRITTEN_PREMIUM ,0)*-1 AS STAT_FEES_WRITTEN_PREMIUM,   
  (ISNULL(NPT.WRITTEN_PREMIUM ,0) + ISNULL(SPT.WRITTEN_PREMIUM ,0)) *-1 AS GROSS_PREMIUM_WRITTEN,  
  MLM.LOB_DESC  
  FROM NET_PREMIUM_TABLE NPT          
  LEFT JOIN STAT_PREMIUM_TABLE SPT ON NPT.LOB_ID = SPT.LOB_ID           
  INNER JOIN MNT_LOB_MASTER MLM ON MLM.LOB_ID = NPT.LOB_ID  
 )          
           
 SELECT * FROM WRITTEN_PREMIUM_FINAL   
  
END  
  
  
--go   
--exec Proc_MakeWrittenPremiumReport '2008-11-17'                                                                  
--  
--rollback tran   
   



GO

