IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAddWordingsData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAddWordingsData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC Dbo.Proc_GetAddWordingsData                            
(                      
@STATE_ID int,            
@LOB_ID int,                                    
@PROCESS_ID int                                     
)                                              
AS                                              
BEGIN                                 
DECLARE @PROCESS_ID_ORIG int          
          
IF @PROCESS_ID = 12          
    SET @PROCESS_ID_ORIG = 2          
ELSE IF @PROCESS_ID = 22          
    SET @PROCESS_ID_ORIG = 7          
ELSE IF @PROCESS_ID = 25          
    SET @PROCESS_ID_ORIG = 24          
ELSE IF @PROCESS_ID = 20          
    SET @PROCESS_ID_ORIG = 6          
ELSE IF @PROCESS_ID = 16          
    SET @PROCESS_ID_ORIG = 4          
ELSE IF @PROCESS_ID = 18          
    SET @PROCESS_ID_ORIG = 5          
ELSE IF @PROCESS_ID = 29          
    SET @PROCESS_ID_ORIG = 28          
ELSE IF @PROCESS_ID = 32          
    SET @PROCESS_ID_ORIG = 31          
ELSE IF @PROCESS_ID = 14          
    SET @PROCESS_ID_ORIG = 3          
ELSE IF @PROCESS_ID = 9          
    SET @PROCESS_ID_ORIG = 8          
          
          
SELECT * from MNT_PROCESS_WORDINGS  with(nolock) WHERE STATE_ID=@STATE_ID AND LOB_ID=@LOB_ID AND PROCESS_ID IN (@PROCESS_ID,@PROCESS_ID_ORIG)          
        
SELECT REPLACE(PROCESS_DESC, 'Reinstate', 'Reinstatement') AS PROCESS_DESC FROM POL_PROCESS_MASTER  with(nolock) WHERE PROCESS_ID = @PROCESS_ID_ORIG                    
END                         
            
          
        
      
    
  





GO

