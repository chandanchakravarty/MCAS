IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_lock_EX]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_lock_EX]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--- 1996/04/08 00:00
create procedure dbo.sp_lock_EX --- 1996/04/08 00:00
@spid1 int = NULL,		/* server process id to check for locks */
@spid2 int = NULL		/* other process id to check for locks */
as

set nocount on
/*
**  Show the locks for both parameters.
*/
set transaction isolation level read committed
if @spid1 is not NULL
begin
	select 	convert (smallint, req_spid) As spid,
		rsc_dbid As dbid,
		rsc_objid As ObjId,
		rsc_indid As IndId,
		substring (v.name, 1, 4) As Type,
		substring (rsc_text, 1, 32) as Resource,
		substring (u.name, 1, 8) As Mode,
		substring (x.name, 1, 5) As Status

	from 	master.dbo.syslockinfo,
		master.dbo.spt_values v,
		master.dbo.spt_values x,
		master.dbo.spt_values u

	where   master.dbo.syslockinfo.rsc_type = v.number
			and v.type = 'LR'
			and master.dbo.syslockinfo.req_status = x.number
			and x.type = 'LS'
			and master.dbo.syslockinfo.req_mode + 1 = u.number
			and u.type = 'L'

			and req_spid in (@spid1, @spid2)
end

/*
**  No parameters, so show all the locks.
*/
else
begin
	select 	convert (smallint, req_spid) As spid,
		rsc_dbid As dbid,
		rsc_objid As ObjId,
		(SELECT object_name(rsc_objid) )as ObjectName,
		rsc_indid As IndId,
		substring (v.name, 1, 4) As Type,
		substring (rsc_text, 1, 32) as Resource,
		substring (u.name, 1, 8) As Mode,
		substring (x.name, 1, 5) As Status

	from 	master.dbo.syslockinfo,
		master.dbo.spt_values v,
		master.dbo.spt_values x,
		master.dbo.spt_values u

	where   master.dbo.syslockinfo.rsc_type = v.number
			and v.type = 'LR'
			and master.dbo.syslockinfo.req_status = x.number
			and x.type = 'LS'
			and master.dbo.syslockinfo.req_mode + 1 = u.number
			and u.type = 'L'
	order by spid
end

return (0) -- sp_lock


GO

