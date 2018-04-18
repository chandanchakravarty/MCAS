IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckAgenyNotification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckAgenyNotification]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name        : dbo.Proc_CheckAgenyNotification
Created by        : Pravesh K. Chandel
Date                : 10 Feb-2007      
Purpose          : Check that the Ageny Notificartion (Non Renewal )
Revison History  :              
Used In          :   Wolverine            
drop proc  dbo.Proc_CheckAgenyNotification
------------------------------------------------------------              
Date     Review By          Comments            
drop proc   dbo.Proc_CheckAgenyNotification
------   ------------       -------------------------*/              
CREATE PROC dbo.Proc_CheckAgenyNotification
(              
 @CUSTOMER_ID     int,              
 @POLICY_ID     int,              
 @POLICY_VERSION_ID     smallint,              
 @RETVAL int OUTPUT      
)              
AS              
              
BEGIN              



DECLARE @TERMINATE_NOTICE char(1)
DECLARE @AGENCYID INT

SELECT @AGENCYID=AGENCY_ID FROM POL_CUSTOMER_POLICY_LIST with(nolock)
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID

SELECT @TERMINATE_NOTICE=isnull(TERMINATION_NOTICE,'N') FROM MNT_AGENCY_LIST
	WHERE AGENCY_ID=@AGENCYID
IF (@TERMINATE_NOTICE='Y')
 set @RETVAL=1    --notice
else
 set @RETVAL=2	--no notice


 RETURN @RETVAL        
END    
  
  
  
  
  
  
  
  











GO

