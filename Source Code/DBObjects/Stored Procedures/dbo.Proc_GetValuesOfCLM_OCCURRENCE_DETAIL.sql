IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetValuesOfCLM_OCCURRENCE_DETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetValuesOfCLM_OCCURRENCE_DETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------          
Proc Name       : dbo.Proc_GetValuesOfCLM_OCCURRENCE_DETAIL          
Created by      : Vijay Arora          
Date            : 5/3/2006          
Purpose     : To get the values from table named CLM_OCCURRENCE_DETAIL          
Revison History :          
Used In  : Wolverine          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/    
--drop proc Proc_GetValuesOfCLM_OCCURRENCE_DETAIL        
CREATE PROC [dbo].[Proc_GetValuesOfCLM_OCCURRENCE_DETAIL]          
(          
@OCCURRENCE_DETAIL_ID     int,          
@CLAIM_ID int          
)          
AS          
BEGIN          
select OCCURRENCE_DETAIL_ID,          
CLAIM_ID,          
LOSS_DESCRIPTION,          
AUTHORITY_CONTACTED,          
REPORT_NUMBER,          
VIOLATIONS,          
LOSS_TYPE,          
LOSS_LOCATION, 
LOSS_LOCATION_ZIP,    -- Added by Santosh Kumar Gautam on 25 Nov 2010
LOSS_LOCATION_CITY,	  -- Added by Santosh Kumar Gautam on 25 Nov 2010    
LOSS_LOCATION_STATE,  -- Added by Santosh Kumar Gautam on 25 Nov 2010   
ESTIMATE_AMOUNT,    
OTHER_DESCRIPTION,  
WATERBACKUP_SUMPPUMP_LOSS, --Added by Charles on 1-Dec-09 for Itrack 6647    
WEATHER_RELATED_LOSS  --Added for Itrack 6640 on 9 Dec 09      
from  CLM_OCCURRENCE_DETAIL          
where CLAIM_ID = @CLAIM_ID          
          
END          
          
        
      
    
  
GO

