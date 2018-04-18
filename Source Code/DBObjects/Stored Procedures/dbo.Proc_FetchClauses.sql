IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchClauses]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchClauses]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================            
-- Author:  Charles Gomes            
-- Create date: 13-April-2010            
-- Description: Fetch Policy & Maintenance Clauses             
-- DROP PROC Proc_FetchClauses   
--- EXEC      Proc_FetchClauses 9,2,28033,89,1   

--select IS_ACTIVE,* from POL_CLAUSES where CUSTOMER_ID =  28033 and policy_id =  89 and  policy_version_id =1   
--select POLICY_LOB,POLICY_SUBLOB,* from POL_CUSTOMER_POLICY_LIST where CUSTOMER_ID =  28033 and policy_id =  89 ---and  policy_version_id =1 
--drop proc   Proc_FetchClauses
-- =============================================            
CREATE PROC [dbo].[Proc_FetchClauses]             
@LOB_ID int,             
@SUBLOB_ID int,            
@CUSTOMER_ID int,            
@POLICY_ID int,            
@POLICY_VERSION_ID int,  
@LANG_ID INT=1             
AS 
BEGIN   -- changes by praveer for itrack no 1414 (when policy get committed only default and selected clauses on policy tab should be shown)
  DECLARE @PROCESS_STATUS NVARCHAR(30)
  SELECT @PROCESS_STATUS=PROCESS_STATUS from POL_POLICY_PROCESS WITH(NOLOCK) where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID                  
  IF(@PROCESS_STATUS ='COMPLETE')  
 BEGIN
   SELECT PC.CLAUSE_ID, PC.CLAUSE_CODE as CLAUSE_CODE, PC.CLAUSE_TITLE, PC.CLAUSE_DESCRIPTION,ISNULL(SUS.SUSEP_LOB_DESC,susep.SUSEP_LOB_DESC) AS SUSEP_LOB_DESC,susep.SUSEP_LOB_ID    
   FROM POL_CLAUSES PC WITH(NOLOCK)   
   LEFT OUTER JOIN MNT_LOB_MASTER lob WITH(NOLOCK) ON PC.SUSEP_LOB_ID=lob.LOB_ID 
   LEFT OUTER JOIN MNT_SUSEP_LOB_MASTER susep WITH(NOLOCK) ON susep.SUSEP_LOB_ID =lob.SUSEP_LOB_ID    
   LEFT OUTER JOIN MNT_SUSEP_LOB_MASTER_MULTILINGUAL SUS WITH(NOLOCK) ON SUS.SUSEP_LOB_ID =lob.SUSEP_LOB_ID AND SUS.LANG_ID=@LANG_ID     
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CLAUSE_ID<>0 
 END
  ELSE 
 BEGIN        
 SELECT mntc.CLAUSE_ID, mntc.CLAUSE_CODE as CLAUSE_CODE, mntc.CLAUSE_TITLE, mntc.CLAUSE_DESCRIPTION,ISNULL(SUS.SUSEP_LOB_DESC,susep.SUSEP_LOB_DESC) AS SUSEP_LOB_DESC,susep.SUSEP_LOB_ID  ,SUBLOB_ID
   
 --, pol.POL_CLAUSE_ID      
 FROM MNT_CLAUSES mntc WITH(NOLOCK)    
 
left outer join MNT_LOB_MASTER lob WITH(NOLOCK) on mntc.LOB_ID=lob.LOB_ID 
left outer join MNT_SUSEP_LOB_MASTER susep WITH(NOLOCK) on susep.SUSEP_LOB_ID =lob.SUSEP_LOB_ID    
left outer join MNT_SUSEP_LOB_MASTER_MULTILINGUAL SUS WITH(NOLOCK) on SUS.SUSEP_LOB_ID =lob.SUSEP_LOB_ID AND SUS.LANG_ID=@LANG_ID       
 -- left outer join POL_CLAUSES pol on mntc.CLAUSE_ID = pol.CLAUSE_ID      
         
         
  WHERE mntc.LOB_ID = @LOB_ID     
  --AND (mntc.SUBLOB_ID = @SUBLOB_ID or mntc.SUBLOB_ID=0) -- show all clauses of the product    
  AND ISNULL(mntc.IS_ACTIVE,'N') = 'Y'     --  or mntc.LOB_ID = 0
  UNION ALL
  SELECT mntc.CLAUSE_ID,mntc.CLAUSE_CODE as CLAUSE_CODE, mntc.CLAUSE_TITLE, mntc.CLAUSE_DESCRIPTION,ISNULL(SUS.SUSEP_LOB_DESC,susep.SUSEP_LOB_DESC) AS SUSEP_LOB_DESC,susep.SUSEP_LOB_ID   ,SUBLOB_ID
     
 --, pol.POL_CLAUSE_ID      
 FROM MNT_CLAUSES mntc WITH(NOLOCK)    
INNER JOIN POL_CLAUSES ON POL_CLAUSES.CLAUSE_ID = mntc.CLAUSE_ID
left outer join MNT_LOB_MASTER lob WITH(NOLOCK) ON lob.LOB_ID= @LOB_ID
left outer join MNT_SUSEP_LOB_MASTER susep WITH(NOLOCK) on susep.SUSEP_LOB_ID =lob.SUSEP_LOB_ID    
left outer join MNT_SUSEP_LOB_MASTER_MULTILINGUAL SUS WITH(NOLOCK) on SUS.SUSEP_LOB_ID =lob.SUSEP_LOB_ID AND SUS.LANG_ID=@LANG_ID        
 -- left outer join POL_CLAUSES pol on mntc.CLAUSE_ID = pol.CLAUSE_ID      
         
         
  WHERE mntc.LOB_ID = 0
  --AND (mntc.SUBLOB_ID = @SUBLOB_ID or mntc.SUBLOB_ID=0) -- show all clauses of the product    
  AND ISNULL(mntc.IS_ACTIVE,'N') = 'Y' AND POL_CLAUSES.CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID
   and POLICY_VERSION_ID = @POLICY_VERSION_ID
  END
 
        
 SELECT POL_CLAUSE_ID, ISNULL(PC.CLAUSE_ID,0) AS CLAUSE_ID,PC.CLAUSE_CODE as CLAUSE_CODE, PC.CLAUSE_TITLE, PC.CLAUSE_DESCRIPTION ,SUSEP_LOB_ID   , RTRIM(LTRIM(PC.IS_ACTIVE)) IS_ACTIVE,SUBLOB_ID,PREVIOUS_VERSION_ID
 FROM POL_CLAUSES PC WITH(NOLOCK)
 left outer join MNT_CLAUSES on MNT_CLAUSES.CLAUSE_ID=PC.CLAUSE_ID WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND            
 POLICY_VERSION_ID = @POLICY_VERSION_ID --AND ISNULL(IS_ACTIVE,'N') = 'Y' 
       
             
END     

GO

