---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
  
    
--BEGIN TRAN      
--DROP PROC DBO.PROC_GETPROFITLOSS      
--GO      
/*----------------------------------------------------------              
PROC NAME        : DBO.PROC_GETPROFITLOSS              
CREATED BY       : SUKHVEER SINGH              
DATE             : 07/09/2006                       
PURPOSE          : PROCEDURE TO GET PROFIT AND LOSS DETAILS ON DAILY BASIS.                
REVISON HISTORY  :       
DATE             : 20/12/2010.            
PURPOSE          : PROCEDURE TO GET PROFIT AND LOSS DETAILS.     
MODIFIED BY      : VIVEK CHATURVEDI             
USED IN          : WOLVERINE              
------------------------------------------------------------              
DATE     REVIEW BY          COMMENTS              
------   ------------       -------------------------*/              
--DROP PROC DBO.PROC_GETPROFITLOSS        
              
ALTER PROC [DBO].[PROC_GETPROFITLOSS]              
(              
/*INPUT PARAMETERS*/          
 @GLID INT = NULL,          
 @YEARFROM INT = NULL,               
 @YEARTO INT = NULL,               
 @MMONTH INT = NULL ,       
 @SSDATE DATE=NULL      
)              
AS              
BEGIN              
      
 IF NOT @SSDATE IS NULL  --ADDED IF CONDITION FOR ITRACK 6898 ON 20/12/2010    
 BEGIN      
  SELECT LEDGER_ID,ACCOUNT_ID,ACC_NUMBER,ACC_TYPE,ACC_TYPE_DESC,YEAR_MTD,PRIOR_YEAR_MTD,      
  VARIANCE_MTD,CHNG_MTD,YEAR_YTD,PRIOR_YEAR_YTD,VARIANCE_YTD,CHNG_YTD,LEDGER_NAME,      
  ACC_DESCRIPTION AS ACC_DESC      
  FROM RPTPROFITLOSS (NOLOCK)      
  WHERE  DATEDIFF(DD,AS_ON_DATE,@SSDATE)=0  
 END      
 ELSE      
 BEGIN       
  EXEC PROC_GENERATEPROFITLOSS @GLID ,@YEARFROM , @YEARTO ,@MMONTH       
 END      
END              
--            
--GO      
--EXEC PROC_GETPROFITLOSS NULL,2008,2008,12      
--ROLLBACK TRAN       
      
      
      
      
      
      
      
  