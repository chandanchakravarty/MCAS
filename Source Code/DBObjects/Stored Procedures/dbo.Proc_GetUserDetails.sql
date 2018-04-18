IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUserDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUserDetails]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*Proc Name          : Dbo.Proc_GetUserDetails
Created by           : Shrikant Bhatt
Date                    : 19/04/2005
Purpose               : To get AccountExcutive  from MNT_USER_LIST table
Revison History :
Used In                :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetUserDetails
(
@USER_TYPE_CODE	varchar(5),
@AgencyId int
)

AS
BEGIN
if(@AgencyId=null and @USER_TYPE_CODE!=NULL)
	begin
		if(@USER_TYPE_CODE!=NULL)
		
		SELECT USER_ID,USER_FNAME,USER_FNAME + '  ' +  USER_LNAME AS USER_NAME  FROM  MNT_USER_LIST  WHERE USER_TYPE_ID = (select USER_TYPE_ID from MNT_USER_TYPES 
		where USER_TYPE_CODE = @USER_TYPE_CODE)  ORDER BY  USER_ID ASC
		
		else
		
		SELECT USER_ID,USER_FNAME,USER_FNAME + '  ' +  USER_LNAME AS USER_NAME  FROM  MNT_USER_LIST   ORDER BY  USER_ID ASC
		
	end
else
		begin
		SELECT USER_ID,USER_FNAME,(Convert(varchar,USER_ID)+'-'+USER_FNAME + '  ' +  USER_LNAME) AS USER_NAME  FROM  
		MNT_AGENCY_LIST INNER JOIN
				MNT_USER_LIST ON MNT_AGENCY_LIST.AGENCY_CODE = MNT_USER_LIST.USER_SYSTEM_ID
				INNER JOIN MNT_USER_TYPES MUT ON  MUT.USER_TYPE_ID=MNT_USER_LIST.USER_TYPE_ID
		
		WHERE     (MNT_AGENCY_LIST.AGENCY_ID = @AgencyId AND MUT.USER_TYPE_CODE=@USER_TYPE_CODE) order by USER_NAME
		end
END


GO

