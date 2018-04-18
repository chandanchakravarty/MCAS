IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAgencyCodeForAgency]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAgencyCodeForAgency]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create proc dbo.Proc_GetAgencyCodeForAgency
(
	@POLICY_NUMBER varchar(20),
	@AGENCY_COMBINED_CODE nvarchar(20)
)
as
begin
declare @agency_id int
select @agency_id =  agency_id from pol_customer_policy_list with (nolock) 
where policy_number=@POLICY_NUMBER and agency_id in 
(select agency_id from mnt_Agency_list with(nolock)  where AGENCY_COMBINED_CODE = @AGENCY_COMBINED_CODE)

select isnull(AGENCY_CODE,'') AS AGENCY_CODE from mnt_Agency_list with(nolock) where agency_id = @agency_id

end
GO

