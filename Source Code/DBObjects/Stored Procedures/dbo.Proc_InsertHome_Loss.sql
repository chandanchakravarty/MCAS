IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertHome_Loss]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertHome_Loss]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
Author  :Pravesh K Chandel  
Dated   : 10 Jan 2007  
Purpose : to insert Loss Information for REntal/Home    
drop proc dbo.Proc_InsertHome_Loss   
*/  
  
CREATE procedure dbo.Proc_InsertHome_Loss   
(  
--PRIOR_LOSS_ID   INT,  
@LOSS_ID  SMALLINT ,  
@CUSTOMER_ID  INT,  
@LOCATION_ID INT,  
@LOSS_ADD1    NVARCHAR(80),  
@LOSS_ADD2   NVARCHAR(80),  
@LOSS_CITY   NVARCHAR(75),  
@LOSS_STATE  NVARCHAR(20),  
@LOSS_ZIP    NVARCHAR(11),   
@CURRENT_ADD1 NVARCHAR(80),  
@CURRENT_ADD2 NVARCHAR(80),  
@CURRENT_CITY NVARCHAR(75),   
@CURRENT_STATE NVARCHAR(20),  
@CURRENT_ZIP  NVARCHAR(11),   
@POLICY_TYPE  NVARCHAR(10)=null,   
@POLICY_NUMBER NVARCHAR(30)=null,  
@WATERBACKUP_SUMPPUMP_LOSS INT = NULL, --Added by Charles on 30-Nov-09 for Itrack 6647
@WEATHER_RELATED_LOSS INT = NULL --Added for Itrack 6640 on 9 Dec 09
)  
as  
begin  
  
declare @tempPRIOR_LOSS_ID int  
select @tempPRIOR_LOSS_ID=isnull(max(PRIOR_LOSS_ID),0)+1 from PRIOR_LOSS_HOME  
insert into PRIOR_LOSS_HOME  
(  
PRIOR_LOSS_ID  ,  
LOSS_ID   ,  
CUSTOMER_ID  ,  
LOCATION_ID ,  
LOSS_ADD1    ,  
LOSS_ADD2   ,  
LOSS_CITY   ,  
LOSS_STATE  ,  
LOSS_ZIP    ,   
CURRENT_ADD1,  
CURRENT_ADD2,  
CURRENT_CITY,   
CURRENT_STATE ,  
CURRENT_ZIP  ,   
POLICY_TYPE  ,   
POLICY_NUMBER,
WATERBACKUP_SUMPPUMP_LOSS,   --Added by Charles on 30-Nov-09 for Itrack 6647 
WEATHER_RELATED_LOSS --Added for Itrack 6640 on 9 Dec 09
)  
values  
(  
@tempPRIOR_LOSS_ID  ,  
@LOSS_ID   ,  
@CUSTOMER_ID  ,  
@LOCATION_ID ,  
@LOSS_ADD1    ,  
@LOSS_ADD2   ,  
@LOSS_CITY   ,  
@LOSS_STATE  ,  
@LOSS_ZIP    ,   
@CURRENT_ADD1,  
@CURRENT_ADD2,  
@CURRENT_CITY,   
@CURRENT_STATE ,  
@CURRENT_ZIP  ,   
@POLICY_TYPE  ,   
@POLICY_NUMBER,
@WATERBACKUP_SUMPPUMP_LOSS,    --Added by Charles on 30-Nov-09 for Itrack 6647
@WEATHER_RELATED_LOSS --Added for Itrack 6640 on 9 Dec 09
)  
  
end  
  


GO

