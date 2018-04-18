IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetREIN_MAJOR_MINOR_TOTALPERCENTAGE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetREIN_MAJOR_MINOR_TOTALPERCENTAGE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  /*----------------------------------------------------------                    
Proc Name       : dbo.Proc_GetREIN_MAJOR_MINOR_TOTALPERCENTAGE                    
Created by      : Pravesh K Chandel    
Date            : 20 Aug, 2007                  
Purpose         : To get Total % of Majar or minor    
Revison History :                   
Used In         : Wolverine             
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------    
drop proc [dbo].[Proc_GetREIN_MAJOR_MINOR_TOTALPERCENTAGE]    
*/                    
CREATE proc [dbo].[Proc_GetREIN_MAJOR_MINOR_TOTALPERCENTAGE]    
(                  
 @CONTRACT_ID int  ,    
 @CALLED_FROM    varchar(10),  
 @LAYER int   ,
 @PARTICIPATION_ID int=0
 
  )                    
AS     
begin    
 if (@CALLED_FROM='MAJOR')      
 --select isnull(sum(WHOLE_PERCENT),0) as OLDTOTALPERCENT 
 --from MNT_REINSURANCE_MAJORMINOR_PARTICIPATION 
 --where CONTRACT_ID=@CONTRACT_ID and isnull(IS_ACTIVE,'Y')='Y'     
     SELECT  (CAST(LAYER    AS VARCHAR(20))+'^'+CAST(isnull(sum(WHOLE_PERCENT),0)  AS VARCHAR(20))+'@') AS OLDTOTALPERCENT
     FROM MNT_REINSURANCE_MAJORMINOR_PARTICIPATION 
     where CONTRACT_ID=@CONTRACT_ID AND 
           isnull(IS_ACTIVE,'Y')='Y'  
           AND LAYER= CASE WHEN @LAYER=0 THEN LAYER ELSE @LAYER END
           AND PARTICIPATION_ID <> CASE WHEN @PARTICIPATION_ID=0 THEN 0 ELSE @PARTICIPATION_ID END
     GROUP BY CONTRACT_ID,LAYER
     
 else    
 select isnull(sum(MINOR_WHOLE_PERCENT),0) as OLDTOTALPERCENT from MNT_REIN_MINOR_PARTICIPATION where CONTRACT_ID=@CONTRACT_ID and isnull(IS_ACTIVE,'Y')='Y' and MINOR_LAYER =@LAYER    
     
end    

    
    
  
  

GO

