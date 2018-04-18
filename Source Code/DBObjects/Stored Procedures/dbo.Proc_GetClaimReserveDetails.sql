IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimReserveDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimReserveDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
                  
----begin tran                    
----DROP PROC dbo.Proc_GetClaimReserveDetails             
----go                    
----/*----------------------------------------------------------                                                          
--Proc Name       : dbo.Proc_GetClaimReserveDetails                                                         
--Created by      : Santosh Kumar Gautam                                                      
--Date            : 08 Dec 2010                                                       
--Purpose         :                                                      
--Revison History :                                                          
--Used In         : CLAIM                           
--------   ------------       -------------------------*/                                                          
---- DROP PROC dbo.Proc_GetClaimReserveDetails          
                                                      
CREATE PROC [dbo].[Proc_GetClaimReserveDetails]                                                                                                 
 @CLAIM_ID int ,                          
 @ACTIVITY_ID int ,  
 @RESERVE_ID int ,              
 @LANG_ID int                   
            
AS                                            
BEGIN              
          
    SELECT RESERVE_AMT,TRAN_RESERVE_AMT,PAYMENT_AMT,  
           M.REIN_COMAPANY_NAME,  
           COMP_PERCENTAGE  ,      
           CASE WHEN C.COMP_TYPE='CO' THEN dbo.fun_GetMessage(302,@LANG_ID )ELSE dbo.fun_GetMessage(303,@LANG_ID ) END AS COMP_TYPE  
    FROM   [CLM_ACTIVITY_CO_RI_BREAKDOWN]  C INNER JOIN     
           MNT_REIN_COMAPANY_LIST M ON M.REIN_COMAPANY_ID=COMP_ID      
    WHERE  C.CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID AND C.RESERVE_ID=@RESERVE_ID AND C.IS_ACTIVE='Y'    
            
   
    SELECT CLAIM_NUMBER FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y'  
   
   
             
            
END                
                                                      
--go                    
--exec Proc_CalculateBreakdown 159,2            
               
--rollback tran                        
----                        
----   
  
GO

