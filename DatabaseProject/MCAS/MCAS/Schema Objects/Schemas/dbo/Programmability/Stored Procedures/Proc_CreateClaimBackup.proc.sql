CREATE Procedure Proc_CreateClaimBackup
(
@AccidentClaimId int
)
as
begin
-- By Pravesh K Chandel-
--Taking backup of Reported Claim before Linking to Un reported Claim.


declare @mXml xml
--declare @AccidentClaimId int
 
 --Table ClaimAccidentDetails
 select @mXml = 
 (select  * from ClaimAccidentDetails 
 where AccidentClaimId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'ClaimAccidentDetails',@mXml,GETDATE())

--Table CLM_Claims
 select @mXml = 
 (select  * from CLM_Claims 
 where AccidentClaimId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_Claims',@mXml,GETDATE())

--Table CLM_ServiceProvider
 select @mXml = 
 (select  * from CLM_ServiceProvider 
 where AccidentId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_ServiceProvider',@mXml,GETDATE())

--Table from CLM_Notes
select @mXml = 
 (select  * from CLM_Notes 
 where AccidentId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_Notes',@mXml,GETDATE())

--Table CLM_ClaimTask
select @mXml = 
 (select  * from CLM_ClaimTask 
 where AccidentId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_ClaimTask',@mXml,GETDATE())

--Table from Claim_ReAssignmentDairy
select @mXml = 
 (select  * from Claim_ReAssignmentDairy 
 where AccidentClaimId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'Claim_ReAssignmentDairy',@mXml,GETDATE())

--Table  TODODIARYLIST 
select @mXml = 
 (select  * from TODODIARYLIST 
 where AccidentId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'TODODIARYLIST',@mXml,GETDATE())

--Table MNT_AttachmentList
select @mXml = 
 (select  * from MNT_AttachmentList 
 where AccidentId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'MNT_AttachmentList',@mXml,GETDATE())

--Table CLM_ReserveSummary
select @mXml = 
 (select  * from CLM_ReserveSummary 
 where AccidentClaimId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_ReserveSummary',@mXml,GETDATE())

--Table CLM_ReserveDetails
select @mXml = 
 (select  * from CLM_ReserveDetails 
 where AccidentClaimId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_ReserveDetails',@mXml,GETDATE())

--Table CLM_MandateSummary
select @mXml = 
 (select  * from CLM_MandateSummary 
 where AccidentClaimId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_MandateSummary',@mXml,GETDATE())

--Table CLM_MandateDetails
select @mXml = 
 (select  * from CLM_MandateDetails 
 where AccidentClaimId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_MandateDetails',@mXml,GETDATE())

--Table CLM_PaymentSummary
select @mXml = 
 (select  * from CLM_PaymentSummary 
 where AccidentClaimId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_PaymentSummary',@mXml,GETDATE())

--Table CLM_PaymentDetails
select @mXml = 
 (select  * from CLM_PaymentDetails 
 where AccidentClaimId=@AccidentClaimId
 for xml path, root('rows')
 ) 
insert into CLM_ReportedClaimBackup(accidentClaimId,tableName,Tabledata,CreatedDateTime)
values(@AccidentClaimId,'CLM_PaymentDetails',@mXml,GETDATE())

--select * from CLM_ReportedClaimBackup
--select @mXml =Tabledata from CLM_ReportedClaimBackup where tableName='TODODIARYLIST'


--DECLARE @handle INT  
--DECLARE @PrepareXmlStatus INT  
 
--EXEC @PrepareXmlStatus= sp_xml_preparedocument @handle OUTPUT, @mXml  

--select SUBJECTLINE,LISTTYPEID,myName
--from(
--SELECT  *
--FROM    OPENXML(@handle, '/rows/row', 2)  
--   with(LISTID int,
--		LISTTYPEID int,
--		SUBJECTLINE nvarchar(200),
--		myName varchar(50)
--		)
--)t
		
--EXEC sp_xml_removedocument @handle 

end


