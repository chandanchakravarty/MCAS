
 --DROP PROC Proc_GetCLM_LOSS_CODES  
/*----------------------------------------------------------                          
                          
Proc Name       : Proc_GetCLM_LOSS_CODES      
Created by      : Sumit Chhabra          
Date            : 24/04/2006                          
Purpose         : Get Loss Codes data from CLM_LOSS_CODES      
Revison History :                          
Used In                   : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
CREATE PROC [dbo].[Proc_GetCLM_LOSS_CODES]                       
(                          
 @LOB_ID int ,  
 @LANG_ID int=1                     
)                          
AS                          
BEGIN                        
  SELECT          
   CTD.DETAIL_TYPE_ID  AS LOSS_CODE_TYPE,ISNULL( M.TYPE_DESC, CTD.DETAIL_TYPE_DESCRIPTION) AS DESCRIPTION      
   FROM CLM_TYPE_DETAIL CTD  
   LEFT OUTER JOIN CLM_TYPE_DETAIL_MULTILINGUAL M ON M.DETAIL_TYPE_ID =CTD.DETAIL_TYPE_ID AND M.LANG_ID=@LANG_ID  
   INNER JOIN  CLM_LOSS_CODES CLC   ON       
   CLC.LOSS_CODE_TYPE = CTD.DETAIL_TYPE_ID     
   AND CLC.LOB_ID=@LOB_ID       
 WHERE      
   CTD.TYPE_ID=5   --5=Loss types/sub types    
   AND CTD.IS_ACTIVE='Y'  
 ORDER BY   
  DESCRIPTION  
END                
  
  
  