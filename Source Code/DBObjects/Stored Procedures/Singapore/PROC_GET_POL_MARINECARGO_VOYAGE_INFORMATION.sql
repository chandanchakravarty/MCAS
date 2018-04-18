/*----------------------------------------------------------          
Proc Name       : [PROC_GET_POL_MARINECARGO_VOYAGE_INFORMATION]          
Created by      : Ruchika Chauhan  
Date            : 23-March-2012        
Purpose   : Demo          
Revison History :          
Used In        : Singapore          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_POL_MARINECARGO_VOYAGE_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].PROC_GET_POL_MARINECARGO_VOYAGE_INFORMATION
GO


CREATE PROC PROC_GET_POL_MARINECARGO_VOYAGE_INFORMATION  
(
@VOYAGE_INFO_ID INT  
 ) 
 AS  
 BEGIN  
 IF EXISTS (SELECT CUSTOMER_ID FROM POL_MARINECARGO_VOYAGE_INFORMATION  WHERE VOYAGE_INFO_ID=@VOYAGE_INFO_ID)  
 BEGIN  
SELECT * FROM POL_MARINECARGO_VOYAGE_INFORMATION 
 WHERE VOYAGE_INFO_ID=@VOYAGE_INFO_ID  
 END  
  
END