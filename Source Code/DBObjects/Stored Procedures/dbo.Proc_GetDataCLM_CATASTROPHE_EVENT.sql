IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDataCLM_CATASTROPHE_EVENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDataCLM_CATASTROPHE_EVENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetDataCLM_CATASTROPHE_EVENT    
Created by      : Vijay Arora    
Date            : 4/24/2006    
Purpose     : To get the Data from table named CLM_CATASTROPHE_EVENT    
Revison History :    
Used In  : Wolverine  
Modified By		: Agniswar
Modified On		: 26 Sep 2011
Purpose			: Singapore Implementation     
--DROP PROC Dbo.Proc_GetDataCLM_CATASTROPHE_EVENT   
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC [dbo].[Proc_GetDataCLM_CATASTROPHE_EVENT]     
(      
 @CATASTROPHE_EVENT_ID     int,  
 @LANG_ID INT =NULL      
)      
AS      
BEGIN      
select CATASTROPHE_EVENT_ID,      
CATASTROPHE_EVENT_TYPE,      
--CONVERT(VARCHAR(10),DATE_FROM,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS DATE_FROM,    
--CONVERT(VARCHAR(10),DATE_TO,CASE WHEN @LANG_ID=2 THEN 103 ELSE 101 END) AS DATE_TO,   
DATE_FROM  AS DATE_FROM, 
DATE_TO AS DATE_TO, 
DESCRIPTION,  IS_ACTIVE,     
CAT_CODE      
from  CLM_CATASTROPHE_EVENT      
where  CATASTROPHE_EVENT_ID = @CATASTROPHE_EVENT_ID      
END       
    
  
  
  
  
  
  

GO

