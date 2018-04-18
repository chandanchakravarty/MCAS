
ALTER PROCEDURE [dbo].[Proc_ReAssignSave] @ReAssignFrom INT = 0, @ReAssignTo INT = 0
AS
select * into #mytemptable from TODODIARYLIST where TOUSERID=@ReAssignFrom
update #mytemptable set TOUSERID=@ReAssignTo where TOUSERID=@ReAssignFrom
INSERT INTO TODODIARYLIST SELECT [RECBYSYSTEM] ,[RECDATE] ,[FOLLOWUPDATE] ,[LISTTYPEID] ,
[POLICYBROKERID] ,[SUBJECTLINE] ,[LISTOPEN] ,[SYSTEMFOLLOWUPID] ,[PRIORITY] ,[TOUSERID] ,
[FROMUSERID] ,[STARTTIME] ,[ENDTIME] ,[NOTE] ,[PROPOSALVERSION] ,[QUOTEID] ,[CLAIMID] ,
[CLAIMMOVEMENTID] ,[TOENTITYID] ,[FROMENTITYID] ,[CUSTOMER_ID] ,[APP_ID] ,[APP_VERSION_ID] ,
[POLICY_ID] ,[POLICY_VERSION_ID] ,[RULES_VERIFIED] ,[PROCESS_ROW_ID] ,[MODULE_ID] ,[INSURERNAME] ,
[POLICYNO] ,[CLAIMNO] ,[ExpectedPaymentDate] ,[ReminderBeforeCompletion] ,[Escalation] ,[EscalationTo] ,[EmailBody] FROM #mytemptable

GO


