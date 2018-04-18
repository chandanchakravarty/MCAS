    
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
--drop proc dbo.[Proc_Get_POL_REINSURANCE_INFO]  28325,146,1      
        
ALTER PROC [dbo].[Proc_Get_POL_REINSURANCE_INFO]            
@CUSTOMER_ID INT,            
@POLICY_ID INT,            
@POLICY_VERSION_ID INT              
AS          
BEGIN          
       
        
 SELECT REINSURANCE_ID,CONTRACT_FACULTATIVE,CONTRACT,REINSURANCE_CEDED,REINSURANCE_COMMISSION,MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_NAME as REINSURER_NAME,          
 COMPANY_ID  , REINSURER_NUMBER,MAX_NO_INSTALLMENT,RISK_ID,isnull(COMM_AMOUNT,0) as COMM_AMOUNT,isnull(LAYER_AMOUNT,0) as LAYER_AMOUNT,isnull(REIN_PREMIUM,0) as REIN_PREMIUM     --Added by Aditya for TFS BUG # 2514    
 FROM POL_REINSURANCE_INFO WITH(NOLOCK)          
 join MNT_REIN_COMAPANY_LIST           
 on POL_REINSURANCE_INFO.COMPANY_ID=MNT_REIN_COMAPANY_LIST.REIN_COMAPANY_ID          
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID             
 ORDER BY REINSURER_NAME            
END 