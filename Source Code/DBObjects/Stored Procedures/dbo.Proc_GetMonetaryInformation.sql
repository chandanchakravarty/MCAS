IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMonetaryInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMonetaryInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                  
Proc Name             : Dbo.[Proc_GetMonetaryInformation]                                                  
Created by            : Aditya Goel                                                 
Date                  : 22/12/2010                                                 
Purpose               : To get Monetary information details        
Revison History       :                                                  
Used In               : Maintenance module        
------------------------------------------------------------                                                  
Date     Review By          Comments                     
            
drop Proc [Proc_GetMonetaryInformation]                                         
------   ------------       -------------------------*/          
        
CREATE PROC [dbo].[Proc_GetMonetaryInformation]          
        
@ROW_ID   int          
        
AS                                                                                    
BEGIN             
          
    SELECT [ROW_ID]      
      ,[DATE]      
      ,[INFLATION_RATE]      
      ,[INTEREST_RATE]      
      ,[IS_ACTIVE]      
      ,[CREATED_BY]      
      ,[CREATED_DATETIME]      
      ,[MODIFIED_BY]      
      ,[LAST_UPDATED_DATETIME]      
  FROM [dbo].[MNT_MONETORY_INDEX]     
  WHERE ([ROW_ID]=@ROW_ID AND IS_ACTIVE='Y')        
      
          
END          
        
        
        
GO

