IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDefaultValuesDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDefaultValuesDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------              
Proc Name       : dbo.Proc_GetDefaultValuesDetails        
Created by      : Sumit Chhabra            
Date            : 21/04/2006              
Purpose        : Fetch Individual values for clm_type_master        
Revison History :              
Used In        : Wolverine              
------------------------------------------------------------      
Modified By : Vijay Arora      
Modified Date : 29-05-2006      
Purpose  : To add the parameter for using the transanction code      
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--DROP PROC Proc_GetDefaultValuesDetails      
CREATE PROC [dbo].[Proc_GetDefaultValuesDetails]          
(      
 @TYPE_ID int, 
 @TRANSACTION_CODE int = NULL      ,
  @LANG_ID int = 1     
)      
AS              
BEGIN              
 IF (@TRANSACTION_CODE IS NULL)       
      SELECT L.DETAIL_TYPE_ID,ISNULL(M.TYPE_DESC,DETAIL_TYPE_DESCRIPTION ) AS DETAIL_TYPE_DESCRIPTION
      FROM  CLM_TYPE_DETAIL L LEFT OUTER JOIN
		    CLM_TYPE_DETAIL_MULTILINGUAL M ON M.DETAIL_TYPE_ID=L.DETAIL_TYPE_ID AND LANG_ID=@LANG_ID
      WHERE TYPE_ID=@TYPE_ID AND ISNULL(IS_ACTIVE,'N')='Y' 
      ORDER BY DETAIL_TYPE_DESCRIPTION    
 ELSE      
 
  SELECT L.DETAIL_TYPE_ID,ISNULL(M.TYPE_DESC,DETAIL_TYPE_DESCRIPTION ) AS DETAIL_TYPE_DESCRIPTION
  FROM   CLM_TYPE_DETAIL L LEFT OUTER JOIN
		 CLM_TYPE_DETAIL_MULTILINGUAL M ON M.DETAIL_TYPE_ID=L.DETAIL_TYPE_ID AND LANG_ID=@LANG_ID
  WHERE  TYPE_ID=@TYPE_ID AND ISNULL(IS_ACTIVE,'N')='Y'  
         AND TRANSACTION_CODE = @TRANSACTION_CODE    
  ORDER BY DETAIL_TYPE_DESCRIPTION    
       
END         
  

GO

