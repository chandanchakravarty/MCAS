IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MNT_REIN_DELETE_CONTRACT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MNT_REIN_DELETE_CONTRACT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name       : dbo.Proc_MNT_REIN_DELETE_CONTRACT                  
Created by      : Harmanjeet Singh                 
Date            : May 10, 2007                
Purpose         : To insert the data into Reinsurer TIV GROUP table.                  
Revison History :                  
modified by :pravesh K chandel  
modified date : 24 aug 2007   
purpose  : delete records to child tables  
Used In         : Wolverine           
drop proc Proc_MNT_REIN_DELETE_CONTRACT                 
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
CREATE PROC [dbo].[Proc_MNT_REIN_DELETE_CONTRACT] 
(                  
                 
 @CONTRACT_ID int   ,
 @HAS_ERROR int OUTPUT 

 )                  
AS                  
BEGIN        


  --===============================================================  
  -- ADDED BY SANTOSH KUMAR GAUTAM ON 11 APRIL 2011 ITRACK:462  
  -- IF ANY POLICY OR CLAIM IS CREATED FOR THIS CONTRACT THEN USER
  -- CANNOT DELETE IT.
  --===============================================================  
  
  IF (EXISTS(SELECT [CONTRACT] FROM POL_REINSURANCE_INFO WHERE  [CONTRACT]=@CONTRACT_ID  AND IS_ACTIVE='Y')  OR          
     EXISTS(SELECT CLAIM_ID FROM CLM_CLAIM_INFO WHERE  CATASTROPHE_EVENT_CODE=@CONTRACT_ID  AND IS_ACTIVE='Y') OR
     EXISTS(SELECT A.LOB_ID  FROM POL_REINSURANCE_BREAKDOWN_DETAILS A INNER JOIN
			(SELECT CONTRACT_NUMBER ,MRCL.CONTRACT_LOB FROM MNT_REINSURANCE_CONTRACT MRC 
			 LEFT OUTER JOIN MNT_REIN_CONTRACT_LOB MRCL ON MRC.CONTRACT_ID = MRCL.CONTRACT_ID WHERE MRC.CONTRACT_ID = @CONTRACT_ID) T ON T.CONTRACT_NUMBER=A.CONTRACT_NUMBER AND A.LOB_ID=CAST(ISNULL(T.CONTRACT_LOB,0) AS INT)) )                
  BEGIN  
  
   SET @HAS_ERROR =-4  
   RETURN  
     
  END         
  
  
   DELETE FROM  MNT_REIN_LOSSLAYER WHERE CONTRACT_ID =@CONTRACT_ID  
   DELETE FROM  MNT_XOL_INFORMATION WHERE CONTRACT_ID =@CONTRACT_ID  
  
   DELETE FROM  MNT_REIN_PREMIUM_BUILDER WHERE CONTRACT_ID =@CONTRACT_ID  
   delete from MNT_REINSURANCE_MAJORMINOR_PARTICIPATION WHERE CONTRACT_ID =@CONTRACT_ID  
   delete from MNT_REIN_MINOR_PARTICIPATION WHERE CONTRACT_ID =@CONTRACT_ID  
  delete from  MNT_ATTACHMENT_LIST where ATTACH_ENT_ID=@CONTRACT_ID and ATTACH_ENTITY_TYPE='REINSURANCE'  
 delete from MNT_REIN_CONTRACT_RISKEXPOSURE where CONTRACT_ID=@CONTRACT_ID  
 delete from MNT_REIN_CONTRACT_STATE where CONTRACT_ID=@CONTRACT_ID  
 delete  from MNT_REIN_CONTRACT_LOB where CONTRACT_ID=@CONTRACT_ID  
  
   DELETE MNT_REINSURANCE_CONTRACT  
 WHERE   
 CONTRACT_ID=@CONTRACT_ID;       
    
  END  
  
  
  
  
GO

