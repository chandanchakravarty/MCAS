IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMVRPointsForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMVRPointsForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                            
Proc Name        : dbo.Proc_GetMVRPointsForPolicy        
Created by       : Sumit Chhabra        
Date             : 13/02/2006                            
Purpose       :  Retrieve the sum of MVR points for a driver        
Revison History :                            
Used In  : Wolverine                             
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                             
CREATE PROCEDURE Proc_GetMVRPointsForPolicy    
(                            
                             
 @CUSTOMER_ID int,                            
 @POLicy_ID  int,                            
 @POLicy_VERSION_ID smallint,                            
 @DRIVER_ID INT,    
 @MVR_POINTS int output         
)                            
AS                            
--declare @DATE_LICENSED datetime        
--declare @LICENCED_YEAR INT        
--declare @MVR_POINTS int        
BEGIN             
SELECT @MVR_POINTS=isnull(SUM(isnull(MVR_POINTS,0)),0)  FROM POL_MVR_INFORMATION A JOIN MNT_VIOLATIONS M        
ON A.VIOLATION_ID=M.VIOLATION_ID        
WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.POLICY_ID=@POLicy_ID AND A.POLICY_VERSION_ID=@POLicy_VERSION_ID AND A.DRIVER_ID=@DRIVER_ID               
              
END           
      
    
  



GO

