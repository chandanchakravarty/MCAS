IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyUsers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyUsers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetAgencyUsers
Created by           : Nidhi
Date                    : 26/04/2005
Purpose               : To get Agency Users from the user_list table
Revison History :
Used In                :   Wolverine

Modified by 	 : Anurag Verma
Modified Date	 : 27/04/2005
Purpose	 : To get agency users based on application Id, customer Id, app_version_id	
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_GetAgencyUsers
CREATE PROC dbo.Proc_GetAgencyUsers
(
@AGENCYID 	int=NULL,
@FROM VARCHAR(1) = null,
@CUSTOMER_ID INT=NULL

)
AS

IF @FROM<>'Y'
BEGIN
	SELECT     MNT_USER_LIST.USER_ID, 
	MNT_USER_LIST.USER_FNAME +'  '+  MNT_USER_LIST.USER_LNAME + '-' +cast(MNT_USER_LIST.USER_ID as varchar(7)) as username
	FROM         MNT_AGENCY_LIST INNER JOIN
              	        MNT_USER_LIST ON MNT_AGENCY_LIST.AGENCY_CODE = MNT_USER_LIST.USER_SYSTEM_ID
	WHERE     (MNT_AGENCY_LIST.AGENCY_ID = @AGENCYID) order by UserName
END
ELSE
BEGIN 
	/*SELECT UL.USER_ID, UL.USER_FNAME +'  '+  UL.USER_LNAME as Username 
	FROM APP_LIST AL,MNT_USER_LIST UL,MNT_AGENCY_LIST AI
	WHERE 
	AL.APP_AGENCY_ID=AI.AGENCY_ID AND
	AI.AGENCY_CODE = UL.USER_SYSTEM_ID AND (
	AL.APP_ID=@APP_ID AND AL.CUSTOMER_ID=@CUSTOMER_ID AND AL.APP_VERSION_ID=@APP_VERSION_ID)
	AND UL.IS_ACTIVE='Y'
	AND AL.IS_ACTIVE='Y'
	AND AI.IS_ACTIVE='Y'
	ORDER BY USERNAME*/

select UL.USER_ID, UL.USER_FNAME +'  '+  UL.USER_LNAME as Username from mnt_user_list ul,mnt_user_types ut where 
ul.user_type_id=ut.user_type_id and ut.user_type_code='CSR' order by UserName


END






GO

