IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_GETXML_SPLIT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_GETXML_SPLIT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



 /*----------------------------------------------------------            
Proc Name        : dbo.[Proc_MNT_REIN_GETXML_SPLIT]            
Created by       : Harmanjeet Singh          
Date             : April 20,2007         
Purpose          : retrieving data from MNT_REIN_split           
Revison History  :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/        
--DROP PROC dbo.Proc_MNT_REIN_GETXML_SPLIT        
         
CREATE PROC [dbo].[Proc_MNT_REIN_GETXML_SPLIT]            
@REIN_SPLIT_DEDUCTION_ID varchar(30)            
AS            
            
BEGIN            
SELECT     
ISNULL(REIN_SPLIT_DEDUCTION_ID,0) REIN_SPLIT_DEDUCTION_ID,    
    
REIN_EFFECTIVE_DATE,    
ISNULL(REIN_LINE_OF_BUSINESS,'') REIN_LINE_OF_BUSINESS,          
    
    
ISNULL(REIN_STATE,'') REIN_STATE,    
ISNULL(REIN_COVERAGE,'') REIN_COVERAGE,    
ISNULL(replace(REIN_IST_SPLIT,'%',''),'') REIN_IST_SPLIT,    
ISNULL(REIN_IST_SPLIT_COVERAGE,'') REIN_IST_SPLIT_COVERAGE,    
    
ISNULL(replace(REIN_2ND_SPLIT,'%',''),'') REIN_2ND_SPLIT,            
ISNULL(REIN_2ND_SPLIT_COVERAGE,'') REIN_2ND_SPLIT_COVERAGE,   
ISNULL(POLICY_TYPE,'') POLICY_TYPE,            
ISNULL(IS_ACTIVE,'') IS_ACTIVE,            
ISNULL(CREATED_BY,'') CREATED_BY,            
CONVERT(VARCHAR,CREATED_DATETIME,101) CREATED_DATETIME,            
ISNULL(MODIFIED_BY,'') MODIFIED_BY,            
    
    
CONVERT(VARCHAR,LAST_UPDATED_DATETIME,101) LAST_UPDATED_DATETIME    
    
      
--CONVERT(VARCHAR,TERMINATION_DATE,101) TERMINATION_DATE,        
        
       
FROM MNT_REIN_SPLIT           
          
where CONVERT(VARCHAR,REIN_SPLIT_DEDUCTION_ID)=@REIN_SPLIT_DEDUCTION_ID          
END     
    
    
  
  
  
  


GO

