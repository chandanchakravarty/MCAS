IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetStateListForCountry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetStateListForCountry]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--SELECT * FROM CLM_INSURED_PRODUCT WHERE CLAIM_ID=1022

--SELECT * FROM MNT_COUNTRY_STATE_LIST
  
 /*----------------------------------------------------------                                      
Proc Name   : dbo.Proc_GetStateListForCountry                      
Created by  : Sumit Chhabra                            
Date        : 30 April,2007                            
Purpose     : Get the States for a country      
Revison History  :                                    
modified by   :pravesh k chandel    
date   : 23 may 2007    
purpose   : to fetch all list of country and state           
 ------------------------------------------------------------                                                  
Date     Review By          Comments                                                
                                       
------   ------------       -------------------------*/        
--drop proc dbo.Proc_GetStateListForCountry 5                                     
CREATE PROCEDURE dbo.Proc_GetStateListForCountry                            
(                        
@COUNTRY_ID int                           
)                                
AS                                     
BEGIN                                      
 if (@COUNTRY_ID<>0)    
  SELECT STATE_NAME,STATE_ID FROM MNT_COUNTRY_STATE_LIST with (nolock) WHERE COUNTRY_ID=@COUNTRY_ID order by STATE_NAME      
  else    
  SELECT COUNTRY_ID,STATE_ID,STATE_NAME FROM MNT_COUNTRY_STATE_LIST with (nolock) order by STATE_NAME      
End                          
      
      
    
GO

