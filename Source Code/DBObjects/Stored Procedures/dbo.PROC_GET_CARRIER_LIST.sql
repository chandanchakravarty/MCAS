   IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GET_CARRIER_LIST]') AND type in (N'P', N'PC'))

DROP PROC dbo.[PROC_GET_CARRIER_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
 
    
/*----------------------------------------------------------                                                      
 Proc Name       : [dbo].[PROC_GET_CARRIER_LIST]                             
 Created by      : praveer panghal             
 Date            : 19 SEP 2011                                     
 Purpose         : Stored procedure that returns the  data of menu list                                
 Revison History :                                                      
 Used In       : EbixAdvanatge             
             
 drop proc [dbo].[PROC_GET_CARRIER_LIST]    
------   ------------       -------------------------*/                 
            
    
CREATE PROC dbo.[PROC_GET_CARRIER_LIST]    
    
AS    
BEGIN         
SELECT  CARRIER_ID,(CARRIER_FULL_NAME + '-' + CARRIER_CODE) AS CARRIER_NAME from [BRICS-COMMON].dbo.[MNT_CARRIER_MASTER] WITH(NOLOCK)   
  
END    
GO