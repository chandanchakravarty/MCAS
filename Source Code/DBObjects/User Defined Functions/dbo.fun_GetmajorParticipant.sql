IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_GetmajorParticipant]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_GetmajorParticipant]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
created by: Pravesh K Chandel 
Date   : 23 oct 2007  
Purpose   : to feth Majar Participant on Breakdown screen
modified Date   : 17 March 09
Purpose   : to Optimize the code for Reinsurance Report Purpose

used in Wolvorine/Reinsurance  
*/  
--DROP FUNCTION dbo.fun_GetmajorParticipant
create FUNCTION dbo.fun_GetmajorParticipant
(
@CONTRACT_ID int
) returns varchar(200)
as 
begin 

	declare @RESULT nvarchar(100)
	declare @RESULT_STR nvarchar(200)
	set @RESULT=''
	set @RESULT_STR=''

--	DECLARE  CR CURSOR FOR
--		SELECT DISTINCT COMP.REIN_COMAPANY_NAME FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
--	 	INNER JOIN MNT_REIN_COMAPANY_LIST COMP with(nolock) ON COMP.REIN_COMAPANY_ID=MJR.REINSURANCE_COMPANY 
-- 		where MJR.CONTRACT_ID=@CONTRACT_ID
--   OPEN CR        
--   FETCH NEXT FROM CR INTO @RESULT    
--   WHILE @@FETCH_STATUS = 0        
--    BEGIN         
--	   SET @RESULT_STR= @RESULT_STR + RTRIM(@RESULT) + ', '    
--	    FETCH NEXT FROM CR INTO @RESULT    
--    END       
--   CLOSE CR        
--   DEALLOCATE CR   

select @RESULT_STR=major_minor_participant from
(
select distinct 
isnull(tt1.REIN_COMAPANY_NAME+ ';','')  +
isnull(tt2.REIN_COMAPANY_NAME+ ';','')  +
isnull(tt3.REIN_COMAPANY_NAME + ';','')  +
isnull(tt4.REIN_COMAPANY_NAME + ';','')  +
isnull(tt5.REIN_COMAPANY_NAME + ';','')  +
isnull(tt6.REIN_COMAPANY_NAME + ';','')  +
isnull(tt7.REIN_COMAPANY_NAME + ';','')  +
isnull(tt8.REIN_COMAPANY_NAME + ';','')  +
isnull(tt9.REIN_COMAPANY_NAME + ';','')  +
isnull(tt10.REIN_COMAPANY_NAME + ';','')
as major_minor_participant
 from
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,REIN_COMAPANY_ID,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt1
left outer join 
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt2 on tt2.row_id=2

left outer join 
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt3 on tt3.row_id=3

left outer join 
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt4 on tt4.row_id=4

left outer join 
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt5 on tt5.row_id=5

left outer join 
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt6 on tt6.row_id=6

left outer join 
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt7 on tt7.row_id=7

left outer join 
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt8 on tt8.row_id=8

left outer join 
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt9 on tt9.row_id=9

left outer join 
(			
     select ROW_NUMBER() OVER (ORDER BY COMP.REIN_COMAPANY_NAME ASC) as ROW_ID ,COMP.REIN_COMAPANY_NAME from MNT_REIN_COMAPANY_LIST COMP with(nolock)
     where COMP.REIN_COMAPANY_ID in
		(
		SELECT REINSURANCE_COMPANY  FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION MJR with(nolock)
		where  	MJR.CONTRACT_ID=@CONTRACT_ID
		)
)tt10 on tt10.row_id=10


where tt1.row_id=1
) tmp



set  @RESULT_STR = substring(@RESULT_STR,0,len(@RESULT_STR))     
return @RESULT_STR

end









GO

