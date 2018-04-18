IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchLocationSource]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchLocationSource]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                
Proc Name       : dbo.Proc_FetchLocationSource                
Created by      :  
Modified By  : Pradeep Kushwaha                      
Date            : 16-July-2010                                         
Purpose         :   
Revison History :                                
modified by  :   
Used In         : Ebix Advantage web                                
                               
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/                                
--drop proc DBO.Proc_FetchLocationSource 27987,44  
  
CREATE PROC [dbo].[Proc_FetchLocationSource]      
(      
@CUSTOMER_ID INT,  
@LOCATION_ID INT 
)      
AS      
BEGIN   
Select DISTINCT SOURCE_LOCATION_ID from POL_LOCATIONS where CUSTOMER_ID=@CUSTOMER_ID and LOCATION_ID=@LOCATION_ID  
end

GO

