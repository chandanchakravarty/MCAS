  
---------------------------------------------------------------  
--Proc Name          : dbo.[PROC_GET_MNT_CURRENCY_MASTER]
--Created by         :           
--Date               : 
--------------------------------------------------------  
--Date     Review By          Comments          
------   ------------       -------------------------*/         
-- drop proc dbo.[PROC_GET_MNT_CURRENCY_MASTER]      
alter PROCEDURE [dbo].[PROC_GET_MNT_CURRENCY_MASTER]        
(         
 @CURRENCY_ID int  
)          
AS         
BEGIN        
Select  CURRENCY_ID,CURR_CODE,CURR_DESC,CURR_SYMBOL,IS_ACTIVE FROM MNT_CURRENCY_MASTER

where CURRENCY_ID=@CURRENCY_ID 
End

