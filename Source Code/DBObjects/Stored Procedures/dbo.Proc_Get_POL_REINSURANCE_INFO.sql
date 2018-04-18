IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POL_REINSURANCE_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POL_REINSURANCE_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

  
    
    
/*----------------------------------------------------------                              
Proc Name        : dbo.[Proc_Get_POL_REINSURANCE_INFO]                              
Created by       : Chetna Agarwal                            
Date             : 20-04-2010                              
Purpose          : retrieving data from POL_REINSURANCE_INFO                                                      
Used In          : Ebix Advantage                          
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--drop proc dbo.[Proc_Get_POL_REINSURANCE_INFO]    
    
CREATE PROC [dbo].[Proc_Get_POL_REINSURANCE_INFO]        
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
@POLICY_VERSION_ID INT          
AS      
BEGIN      
   
    
 SELECT REINSURANCE_ID,CONTRACT_FACULTATIVE,CONTRACT,REINSURANCE_CEDED,REINSURANCE_COMMISSION,MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_NAME as REINSURER_NAME,      
 COMPANY_ID  , REINSURER_NUMBER,MAX_NO_INSTALLMENT,RISK_ID,COMM_AMOUNT,LAYER_AMOUNT,REIN_PREMIUM     --Added by Aditya for TFS BUG # 2514
 FROM POL_REINSURANCE_INFO WITH(NOLOCK)      
 join MNT_REIN_COMAPANY_LIST       
 on POL_REINSURANCE_INFO.COMPANY_ID=MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_ID      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID         
 ORDER BY REINSURER_NAME        
END
GO

