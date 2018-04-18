IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CheckAgenyTermination]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CheckAgenyTermination]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
Proc Name        : dbo.Proc_CheckAgenyTermination
Created by        : Pravesh K. Chandel
Date                : 11 Jan-2007      
Purpose          : Check that the Ageny Termination (Renewal Termination)
Revison History  :              
Used In          :   Wolverine            
drop proc  dbo.Proc_CheckAgenyTermination
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
CREATE PROC dbo.Proc_CheckAgenyTermination
(              
 @CUSTOMER_ID     int,              
 @POLICY_ID     int,              
 @POLICY_VERSION_ID     smallint,
 @PROCESS_ID smallint=null,
 @RETVAL int OUTPUT      
)              
AS              
              
BEGIN              


declare @POL_INCEPTION_DATE DATETIME
declare @POL_EFFECTIVE_DATE DATETIME
declare @POL_EXPIRY_DATE DATETIME
DECLARE @RENEW_TERMINATE_DATE DATETIME
DECLARE @NBS_TERMINATE_DATE DATETIME
DECLARE @POL_DATE_TO_COMPARE DATETIME
DECLARE @AGENCYID INT
declare @POLICY_NEW_BUSINESS_PROCESS_ID int
declare @POLICY_RENEWAL_PROCESS_ID int
declare @DATE_TO_COMPARE DATETIME

SET @POLICY_NEW_BUSINESS_PROCESS_ID = 24
SET @POLICY_RENEWAL_PROCESS_ID = 5

--SELECT @POL_INCEPTION_DATE=POLICY_EXPIRATION_DATE,@AGENCYID=AGENCY_ID FROM POL_CUSTOMER_POLICY_LIST
SELECT @POL_INCEPTION_DATE=APP_INCEPTION_DATE,@POL_EFFECTIVE_DATE=APP_EFFECTIVE_DATE,@POL_EXPIRY_DATE=APP_EXPIRATION_DATE,@AGENCYID=AGENCY_ID FROM POL_CUSTOMER_POLICY_LIST with(nolock)
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID

--RETURN IF THE AGENCY IS NOT ACTIVE CURRENTLY
IF EXISTS(SELECT AGENCY_ID FROM MNT_AGENCY_LIST WHERE AGENCY_ID=@AGENCYID AND UPPER(ISNULL(IS_ACTIVE,'N'))='N')
BEGIN
	set @RETVAL=3
	RETURN @RETVAL
END

SELECT @RENEW_TERMINATE_DATE=TERMINATION_DATE_RENEW,@NBS_TERMINATE_DATE=TERMINATION_DATE FROM MNT_AGENCY_LIST
	WHERE AGENCY_ID=@AGENCYID AND IS_ACTIVE='Y'

IF(@PROCESS_ID=@POLICY_NEW_BUSINESS_PROCESS_ID)
  BEGIN
	SET @DATE_TO_COMPARE = @NBS_TERMINATE_DATE
	SET @POL_DATE_TO_COMPARE=@POL_EFFECTIVE_DATE
  END
ELSE
  BEGIN
	SET @DATE_TO_COMPARE = @RENEW_TERMINATE_DATE
	SET @POL_DATE_TO_COMPARE=@POL_EXPIRY_DATE
  END

--IF (@POL_INCEPTION_DATE is not NULL) changes as per Itrack issue 1545
--IF (@POL_EFFECTIVE_DATE is not NULL) CHANGE AS PER DISCUS WITH PAWAN
IF (@POL_DATE_TO_COMPARE is not NULL)
	BEGIN    
        IF	(@DATE_TO_COMPARE is not NULL)
		BEGIN 	
		  IF (@POL_DATE_TO_COMPARE>=@DATE_TO_COMPARE) -- (@POL_INCEPTION_DATE>=@DATE_TO_COMPARE)
			begin
				set @RETVAL=2
	              	 RETURN 2 --Agency Renewal date Terminate
			end	
		  ELSE	
			begin
				set @RETVAL=1
				return 1
			end
		END
	   ELSE
		set @RETVAL=1	
	END	
ELSE
	set @RETVAL=1     

 RETURN @RETVAL        
END    





GO

