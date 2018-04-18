IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillPolicyProcess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillPolicyProcess]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc [Proc_FillPolicyProcess]
CREATE     PROC [dbo].[Proc_FillPolicyProcess] AS        
begin        
         
 SELECT PPM.PROCESS_ID,PPM.PROCESS_DESC,PPM.PROCESS_SHORTNAME,1 AS LANG_ID   --LANG_ID added by Charles on 12-Apr-10 for Multilingual Support
 FROM POL_PROCESS_MASTER PPM         
 WHERE PPM.IS_ACTIVE = 'Y' AND PPM.PROCESS_ID IN(2,7,24,6,4,5,28,31)  
 UNION
 --Added by Charles on 12-Apr-10 for Multilingual Support
 SELECT PPML.PROCESS_ID,PPML.PROCESS_DESC,PPML.PROCESS_SHORTNAME,PPML.LANG_ID    
 FROM POL_PROCESS_MASTER_MULTILINGUAL PPML        
 WHERE PPML.IS_ACTIVE = 'Y' AND PPML.PROCESS_ID IN(2,7,24,6,4,5,28,31) 
 --Added till here
 order by PROCESS_DESC        
End        
        
  
GO

