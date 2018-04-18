IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillCoverage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillCoverage]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name       : dbo.Proc_FillCoverage      
Created by      : Gaurav      
Date            : 8/26/2005      
Purpose       :Evaluation      
Revison History :      
Used In        : Wolverine      
    
Modified By : Pradeep    
Modified On : 10/18/2005       
Purpose  : Changed where clause of Type from 1 to 2    
    
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
      
--drop proc Proc_FillCoverage  
CREATE  procedure dbo.Proc_FillCoverage      
(      
@StateId int,      
@LobId int,
@CovId int = 0      
)      
as  
BEGIN 
IF @CovId = 0    
SELECT COV_ID, COV_CODE+'-'+COV_DES AS COVERAGE     
FROM MNT_COVERAGE     
where   
---Type = 2 and   
state_id=@StateId and lob_id=@LobId 

ELSE      
SELECT COV_ID, COV_CODE+'-'+COV_DES AS COVERAGE     
FROM MNT_COVERAGE     
where   
---Type = 2 and   
state_id=@StateId and lob_id=@LobId AND COV_ID = @CovId
END 
      
    



GO

