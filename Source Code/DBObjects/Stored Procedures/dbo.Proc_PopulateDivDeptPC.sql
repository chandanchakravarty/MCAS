IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PopulateDivDeptPC]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PopulateDivDeptPC]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* =============================================          
 Author:  Charles Gomes          
 Created date: 24-May-2010          
 Description: Populates Div/Dept/PC Drop Down 
   
DROP PROC Proc_PopulateDivDeptPC        

 ============================================= */       
     
CREATE PROCEDURE [dbo].[Proc_PopulateDivDeptPC]                  
AS          
BEGIN           

  SELECT CAST(MDDM.DIV_ID AS VARCHAR) + '^' + CAST(DPCM.DEPT_ID AS VARCHAR) + '^' + CAST(DPCM.PC_ID AS VARCHAR) AS [VALUE],
  CAST(ISNULL(MDVL.DIV_NAME,'') AS VARCHAR) + ' / ' + CAST(ISNULL(MDPL.DEPT_NAME,'') AS VARCHAR) + ' / ' + CAST(ISNULL(MPCL.PC_NAME,'') AS VARCHAR) AS [TEXT]
  
  --,MDDM.DIV_ID, DPCM.DEPT_ID,DPCM.PC_ID   
  FROM MNT_DEPT_PC_MAPPING DPCM WITH(NOLOCK)
  LEFT OUTER JOIN MNT_DIV_DEPT_MAPPING MDDM WITH(NOLOCK) ON MDDM.DEPT_ID = DPCM.DEPT_ID
  
  LEFT OUTER JOIN MNT_PROFIT_CENTER_LIST MPCL WITH(NOLOCK) ON MPCL.PC_ID = DPCM.PC_ID AND MPCL.IS_ACTIVE = 'Y'
  LEFT OUTER JOIN MNT_DEPT_LIST MDPL WITH(NOLOCK) ON MDPL.DEPT_ID = DPCM.DEPT_ID AND MDPL.IS_ACTIVE = 'Y'
  LEFT OUTER JOIN MNT_DIV_LIST MDVL WITH(NOLOCK) ON MDVL.DIV_ID = MDDM.DIV_ID AND MDVL.IS_ACTIVE = 'Y'
  
  WHERE MDDM.DIV_ID IS NOT NULL 
  ORDER BY [TEXT] --MDDM.DIV_ID, MDDM.DEPT_ID, DPCM.PC_ID

END     

GO

