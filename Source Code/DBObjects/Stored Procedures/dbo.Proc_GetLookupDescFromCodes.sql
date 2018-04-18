IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLookupDescFromCodes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLookupDescFromCodes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop PROC dbo.Proc_GetLookupDescFromCodes     
/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetLookupDescFromCodes    
Created by      : Nidhi        
Date            : 9 Feb,2006        
Purpose         : To retrieve records from look up table on the basis of lookup codes in both mnt_lookup_table and mnt_lookup_values   
Revison History :        
Used In         : Procs for getting inputxml for rating        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------    
    
Modification History    
Modified By :  
Date  :  
Comment  :  
 
*/        
    
CREATE PROC dbo.Proc_GetLookupDescFromCodes   
(        
 @LookupTableCode  NVarChar(6),    
 @LookupValueCode  NVarChar(20)    
 
)        
    
AS        
    
   
 
   begin    
	 
	     SELECT isnull(MLV.LOOKUP_VALUE_DESC,'') as LOOKUP_VALUE_DESC, ISNULL(LOOKUP_UNIQUE_ID,11427) as LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES MLV    
	     INNER JOIN MNT_LOOKUP_TABLES MLT ON   MLV.LOOKUP_ID = MLT.LOOKUP_ID    
	     WHERE MLT.LOOKUP_NAME = @LookupTableCode AND MLV.LOOKUP_VALUE_CODE= @LookupValueCode 
	  
   end     
  
  
    
  




GO

