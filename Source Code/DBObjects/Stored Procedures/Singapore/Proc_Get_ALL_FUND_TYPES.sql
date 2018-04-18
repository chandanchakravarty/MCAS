IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_ALL_FUND_TYPES]') 
AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].Proc_Get_ALL_FUND_TYPES
GO

SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name       : Dbo.Proc_Get_ALL_FUND_TYPES                              
Created by      : Ruchika Chauhan                              
Date            : 14/1/2012                              
Purpose       :Select all Fund Types                              
Revison History :                              
Used In        : EAW Singapore                              
------------------------------------------------------------                              
Modified By  :           
Date   :          
Purpose  :   
  
------------------------------------------------------------                                                                          
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
--DROP PROC dbo.Proc_Get_ALL_FUND_TYPES

CREATE  PROCEDURE [dbo].[Proc_Get_ALL_FUND_TYPES]        
AS         
BEGIN        
SELECT FUND_TYPE_ID,FUND_TYPE_CODE,FUND_TYPE_NAME
FROM MNT_FUND_TYPES    
End