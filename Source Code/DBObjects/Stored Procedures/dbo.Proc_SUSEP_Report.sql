IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SUSEP_Report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SUSEP_Report]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                            
Proc Name    : dbo.Proc_SUSEP_Report                          
Created by   : Shubhanshu Pandey         
Date         : 07-04-2011               
Purpose      :      
                            
-------------------------------------------------------------                                       
Date     Review By          Comments                                      
 drop proc dbo.Proc_SUSEP_Report                
------   ------------       --------------------------------*/                           
CREATE PROC [dbo].[Proc_SUSEP_Report] 
(  
  
@REPORT_TYPE varchar(20) =14954  
)  
AS
BEGIN
SELECT 
	REPORT_ID,
	REPORT_NAME + ' (' + isnull(ITRACK_NO,'') + ')' as REPORT_NAME,
	PROC_NAME,
	OUTPUT_FORMAT,
	IS_ACTIVE ,
	REPORT_TYPE, 
	FILE_NAME 
FROM  
	MNT_SUSEP_REPORTS_LIST WITH(NOLOCK) 
WHERE IS_ACTIVE='Y'  and REPORT_TYPE=@REPORT_TYPE  
END
GO

