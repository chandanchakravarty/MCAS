IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForAuto_VehicleCovgComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForAuto_VehicleCovgComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Proc_GetRatingInformationForAuto_VehicleCovgComponent 900,151,1,1
/*----------------------------------------------------------                                  
Proc Name            : Dbo.Proc_GetRatingInformationForAuto_VehicleCovgComponent                                  
Created by              : Nidhi.                                  
Date                    :11/10/2005                                  
Purpose                 : To get the coverage information for creating the input xml                                    
Revison History      :                                  
Used In                 :   Creating InputXML for vehicle  15                     
                           
------------------------------------------------------------                                  
Date     Review By          Comments                                  
------   ------------       -------------------------*/                                  
-- 
/* ----------------------------------------------------------                          
Proc Name                 : Dbo.Proc_GetPolicyRatingInformationForAuto_VehicleCovgComponent                          
Created by                : SHAFI.                          
Date                      : 02/06/2006                          
Purpose                   : To get the coverage information for creating the input xml                            
Revison History    :                          
Used In                   :   Creating InputXML for vehicle                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                     
--                  
                  
CREATE PROC [dbo].[Proc_GetPolicyRatingInformationForAuto_VehicleCovgComponent]                  
(                          
@CUSTOMERID    int,                          
@POLICYID    int,                          
@POLICYVERSIONID   int,                          
@VEHICLEID    int                           
)                          
AS                          
                          
BEGIN                                    
 set quoted_identifier off                                    

DECLARE  @COMPREHENSIVEDEDUCTIBLE   nvarchar(100)                        
DECLARE  @COLLISIONDEDUCTIBLE   nvarchar(100)                                    
DECLARE  @COVGCOLLISIONTYPE   nvarchar(100)                                    
DECLARE  @COVGCOLLISIONDEDUCTIBLE   nvarchar(100)                                    
DECLARE  @ROADSERVICE   nvarchar(100)                                    
DECLARE  @RENTALREIMBURSEMENT   nvarchar(100)                                    
DECLARE  @RENTALREIMLIMITDAY   nvarchar(100)                                    
DECLARE  @RENTALREIMMAXCOVG   nvarchar(100)                                    
DECLARE  @MINITORTPDLIAB   nvarchar(100)                                 
DECLARE  @LOANLEASEGAP   nvarchar(100)                                    
DECLARE  @ISANTILOCKBRAKESDISCOUNTS   nvarchar(100)                                    
DECLARE  @AIRBAGDISCOUNT   nvarchar(100)                                    
DECLARE  @IS200SOUNDREPRODUCING   nvarchar(100)                                    
DECLARE  @SOUNDRECEIVINGTRANSMITTINGSYSTEM   nvarchar(100)                                    
DECLARE  @MULTICARDISCOUNT   nvarchar(100)                                    
DECLARE  @INSURANCEAMOUNT   nvarchar(100)                                    
DECLARE  @EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE   nvarchar(100)                                    
DECLARE  @COLLISIONTYPEDED   nvarchar(100)                                    
DECLARE  @EXTRAEQUIPCOLLISIONTYPE   nvarchar(100)                                    
DECLARE  @EXTRAEQUIPCOLLISIONDEDUCTIBLE   nvarchar(100)           
                                    
DECLARE  @BI   nvarchar(100)                                    
DECLARE  @BILIMIT1   nvarchar(100)                                    
DECLARE  @BILIMIT2   nvarchar(100)                                    
DECLARE  @PD   nvarchar(100)                                    
DECLARE  @CSL   nvarchar(100)                                    
DECLARE  @MEDPM   nvarchar(100)                                    
DECLARE  @TYPE   nvarchar(100)                                    
DECLARE  @ISUNDERINSUREDMOTORISTS   nvarchar(100)                                    
DECLARE  @UMSPLIT   nvarchar(100)                                    
DECLARE  @UMSPLITLIMIT1   nvarchar(100)                                    
DECLARE  @UMSPLITLIMIT2   nvarchar(100)                                    
DECLARE  @UMCSL   nvarchar(100)                                    
DECLARE  @PDLIMIT   nvarchar(100)                                    
DECLARE  @PDDEDUCTIBLE   nvarchar(100)                                    
                                    
DECLARE  @QUALIFIESTRAIBLAZERPROGRAM   nvarchar(100)                                    
                                    
                                     
 
                                    
 ------------------------------------------------------------------------------*/                                    
 DECLARE @ReturnValue  varchar(8000)                                    
 DECLARE @STATEID varchar(5)                                    
 DECLARE @LOBID varchar(3)                                    
 SET @ReturnValue = ''                                    
                                     
 /* IF ELSE for coverages for different states as the codes may differ for different states */                                    
 SELECT @STATEID=STATE_ID ,@LOBID= POLICY_LOB from POL_CUSTOMER_POLICY_LIST WITH (NOLOCK) where CUSTOMER_ID =@CUSTOMERID and POLICY_ID =@POLICYID and POLICY_VERSION_ID=@POLICYVERSIONID                                    
                     
 if @STATEID ='22'  -- MICHIGAN                                    
  /************************************************************** BEGINNING  OF MICHIGAN  **********************************************************************************************************************************/                                 





  
   
  begin                                    
   SELECT @ReturnValue =                                     
       (CASE COV_CODE                                      
 		WHEN 'COMP' 	THEN  	'<COMPREHENSIVEDEDUCTIBLE>' 
					+ case  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                     
 					when '0' then ''
					else  	
					+ CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) AS VARCHAR)
					end
					+ '</COMPREHENSIVEDEDUCTIBLE>'                                    
 		WHEN 'COLL' 	THEN 	'<COLLISIONDEDUCTIBLE>' 
					+ case  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                     
 					when '0' then '' + '  ' +  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR)
					else  	
					+ CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) + '  ' +  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR)
					end
					+'</COLLISIONDEDUCTIBLE>'
					+'<COVGCOLLISIONTYPE>'                  
  	   				+ case  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR)                     
 					when '' then ''
					else  	
					+CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT,'') AS VARCHAR)
					end
					+'</COVGCOLLISIONTYPE>'
					+'<COVGCOLLISIONDEDUCTIBLE>'                                    
					+case  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                     
 					when '' then ''
					else  	
					+ CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)
					end
					+'</COVGCOLLISIONDEDUCTIBLE>'                                    
                                 
 		WHEN 'ROAD' 	THEN 	'<ROADSERVICE>'
					+case  CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                     
 					when '0' then ''
					else  	
					+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))              
 					end
					+'</ROADSERVICE>'                                     
                      
      		 WHEN 'RREIM' 	THEN 	'<RENTALREIMBURSEMENT>'
					+case  CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                     
 					when '0' then ''
					else  	
					+ CONVERT(varchar(30),isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)) +'/'                       
 					+ CONVERT(varchar(30),isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0))
					end
					+'</RENTALREIMBURSEMENT>'                                  
					+'<RENTALREIMLIMITDAY>'
					+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                      
 					+ '</RENTALREIMLIMITDAY>'
					+'<RENTALREIMMAXCOVG>'                      
 					+ CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)AS VARCHAR)
					+'</RENTALREIMMAXCOVG>'                                                       
   		WHEN 'LPD' 	THEN 	'<MINITORTPDLIAB>'
					+ CAST( (case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0)
					when 0 then 'FALSE'          
 					else 'TRUE'
					end) AS VARCHAR)
					+'</MINITORTPDLIAB>'                                  
              	WHEN 'LLGC' 	THEN 	'<LOANLEASEGAP>'
					+CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)
 					+'</LOANLEASEGAP>'                                    
       		WHEN 'SORPE' 	THEN 	'<IS200SOUNDREPRODUCING>'
					+ CAST( (CASE isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0)                                       
             				when  0 then  'FALSE'                                    
              				else 'TRUE'                                    
  					end)AS VARCHAR) 
					+ '</IS200SOUNDREPRODUCING>'                                    
              	WHEN 'SRTE' 	THEN 	'<SOUNDRECEIVINGTRANSMITTINGSYSTEM>'
					+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) 
					+ '</SOUNDRECEIVINGTRANSMITTINGSYSTEM>'                          
  
 		WHEN 'EECOMP' 	THEN 	'<EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE>'                  
 					+ case CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                   
 						when 0 then ''                  
 						else  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                   
						end                  
 					+'</EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE>'                                    
  					+'<INSURANCEAMOUNT>'
					+ convert(varchar(30),isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0))
					+'</INSURANCEAMOUNT>'                               
                WHEN 'EECOLL' 	THEN 	'<COLLISIONTYPEDED>'
					+CASE CAST(ISNULL(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)+' '+  CAST(ISNULL(POL_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, 0) AS VARCHAR)                  
                                                    WHEN '0 0' THEN ''                  
                                                    WHEN '0  ' THEN ''                  
                                                    ELSE                  
               				CAST(ISNULL(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)+' '+  CAST(ISNULL(POL_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, 0) AS VARCHAR)                     
             					END                  
                          		+'</COLLISIONTYPEDED>'                        
        				+'<EXTRAEQUIPCOLLISIONDEDUCTIBLE>' +
						CASE WHEN ISNULL(POL_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, 0)='Limited'
								THEN CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) + ' '+  CAST(ISNULL(POL_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, 0) AS VARCHAR)
							 ELSE
									CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) 
						END
					+'</EXTRAEQUIPCOLLISIONDEDUCTIBLE>'                                    
        				+'<EXTRAEQUIPCOLLISIONTYPE>'
					+CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE1_AMOUNT_TEXT, '') AS VARCHAR)
					+'</EXTRAEQUIPCOLLISIONTYPE>'                                      
         	WHEN 'BISPL' 	THEN  	'<BI>'                          
         				+Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                      
   					+'/'                          
         				+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))                          
         				+ '</BI>'
					+'<BILIMIT1>'                           
         				+ CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                          
         				+'</BILIMIT1>'
					+'<BILIMIT2>'                                 
         				+ Substring(CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR)))                        
         				+'</BILIMIT2>'                              
      		WHEN 'PD' 	THEN 	'<PD>'                       
 					+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                                  
 					+ '</PD>'                                    
                                              
      		WHEN 'SLL' 	THEN 	'<CSL>'
					+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))
					+'</CSL>'                          
      		WHEN 'PUMSP' 	THEN 	'<UMSPLIT>'
					+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                       
    					+'/'                       
    					+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))                      
    					+'</UMSPLIT>'                                    
           				+'<UMSPLITLIMIT1>'
					+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                        
    					+'</UMSPLITLIMIT1>'                                    
    					+'<UMSPLITLIMIT2>'
					+ Substring(CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR)))     
   					+'</UMSPLITLIMIT2>'  
-- underinsured motorist
			WHEN 'UNDSP' THEN '<UIMSPLIT>'+Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))+'/' + Substring(convert(varchar(30)
,convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))+'</UIMSPLIT>'+
					   '<UIMSPLITLIMIT1>'+Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) +'</UIMSPLITLIMIT1>'+
					   '<UIMSPLITLIMIT2>'+Substring(CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR)))+'</UIMSPLITLIMIT2>'+	
					   case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) when 0  then '' else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  END
               WHEN 'UNCSL' THEN '<UIMCSL>'+CASE isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)                      
									WHEN 0 THEN ''                      
								 ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) END+'</UIMCSL>'+ 
					case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) 
					when 0  then '' 
					else  
					'<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  
					end                                         
               	WHEN 'PUNCS' 	THEN 	'<UMCSL>'                   
  					+  case isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)                  
  						when 0 then ''                   
  						else Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                        
  						end                   
  					+'</UMCSL>'
					+'<PDLIMIT>'
					+'</PDLIMIT>'
					+'<PDDEDUCTIBLE>0</PDDEDUCTIBLE>'                                    
                WHEN 'PIP' 	THEN 	'<PIP>'
					+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))  
					+'</PIP>'      
   					+'<MEDPMDEDUCTIBLE>'
					+ cast(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1,0)as varchar)
					+'</MEDPMDEDUCTIBLE>'    
    					+'<MEDPM>'
					+ case isnull(POL_VEHICLE_COVERAGES.LIMIT_ID, 0)
					when 685  	then 'PRIMARY'
					WHEN 686  	THEN 'EXCESSWAGE'
					WHEN 687  	THEN 'EXCESSMEDICAL'
					WHEN 688 	THEN 'EXCESSBOTH'
					when 1372 	then 'FULLMEDICALFULLWAGELOSS' 
					when 1373 	then 'FULLMEDICALEXCESSWAGEWORKCOMP'       
  					when 1374 	then 'EXCESSMEDICALFULLWAGELOSS' 
					when 1375 	then 'EXCESSMEDICALEXCESSWAGELOSSWORKCOMP'  
					else '' 	END 
					+'</MEDPM>'                                       
     		WHEN 'PPI' 	THEN 	'<PPI>'
					+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))  
					+'</PPI>'                             
  					+'<QUALIFIESTRAIBLAZERPROGRAM>FALSE</QUALIFIESTRAIBLAZERPROGRAM>'                                  
         				+'<TYPE>UNINSURED MOTORISTS</TYPE>'
--          	WHEN 'UNDSP'  	THEN 	case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) 
--					when 0  then ''
--					else
--					'<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'
--					end                                        
--          	WHEN 'UNCSL'  	THEN 	case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) 
--					when 0  then '' 
--					else  
--					'<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  
--					end                                         
                                           
					      /* - TO DO                                    
					      SET @TYPE  ='UNINSURED MOTORISTS'                                    
					      SET @ISUNDERINSUREDMOTORISTS   ='TRUE'                                      
					      Extra Equipment Collision Type   | e.g 500 BROAD                                    
					      Extra Equipment Collision Deductible  |                                    
					      There are two split limits for each state for BI against cov_code 'BISPL'. They should have values for lower limit an d upper limit both.                                    
					      RENTALREIMLIMITDAY (Rental Reimbursement Limit Day)                                    
					      RENTALREIMMAXCOVG (Rental Reimbursement Max Coverage)                                    
					      INSURANCE AMOUNT                                    
					       */                                    
					                                      
      		ELSE ''                                    
                     			END ) + @ReturnValue                                              
			 FROM         MNT_COVERAGE  WITH (NOLOCK)                                   
			 LEFT OUTER JOIN  POL_VEHICLE_COVERAGES WITH (NOLOCK) ON POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                    
			 AND (POL_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID) AND (POL_VEHICLE_COVERAGES.POLICY_ID = @POLICYID) AND                      
			 (POL_VEHICLE_COVERAGES.POLICY_VERSION_ID = @POLICYVERSIONID) AND   (POL_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                                    
			 WHERE     MNT_COVERAGE.STATE_ID =@STATEID  and MNT_COVERAGE.LOB_ID=@LOBID                                    
  END                                    
  /************************************************************** END  OF MICHIGAN  **********************************************************************************************************************************/                                       





 else if @STATEID ='14'   --INDIANA                                    
                                    
 /************************************************************** BEGINNING  OF INDIANA **********************************************************************************************************************************/            
  	begin                                    
   		SELECT    @ReturnValue =                                     
       			(CASE COV_CODE                                
          			WHEN 'OTC' 	THEN  	'<COMPREHENSIVEDEDUCTIBLE>'
						+ case  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                     
 							when '0' then ''
							else  	
							+ CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)
							end
							+ '</COMPREHENSIVEDEDUCTIBLE>'                                    
             			WHEN 'COLL' 	THEN 	'<COLLISIONDEDUCTIBLE>'
							+ case  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                     
 							when '0' then ''
							else  
							+ CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)
							+ '  ' 
							+  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1_TYPE, '') AS VARCHAR)
							end
							+'</COLLISIONDEDUCTIBLE>'
							+'<COVGCOLLISIONTYPE>'           
           						+CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1_TYPE,'') AS VARCHAR)
							+'</COVGCOLLISIONTYPE>'
							+'<COVGCOLLISIONDEDUCTIBLE>'                                    
        						+ CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)
							+'</COVGCOLLISIONDEDUCTIBLE>'                                         
             			WHEN 'ROAD' 	THEN 	'<ROADSERVICE>' 
							+ case  CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                     
 							when '0' then ''
							else 
							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))
							end
							+'</ROADSERVICE>    
							'+'<MINITORTPDLIAB>FALSE</MINITORTPDLIAB>'
       				WHEN 'RREIM' 	THEN 	'<RENTALREIMBURSEMENT>'
							+ case  CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                     
 							when '0' then ''
							else                        
 							+CONVERT(varchar(30),isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)) +'/'                       
 							+CONVERT(varchar(30),isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)) 
							end
							+'</RENTALREIMBURSEMENT>'                                    
        						+ '<RENTALREIMLIMITDAY>'                      
							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                      
							+ '</RENTALREIMLIMITDAY>'
							+'<RENTALREIMMAXCOVG>'                      
							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))                      
 							+ '</RENTALREIMMAXCOVG>'                                                       
                              	WHEN 'LLGC' 	THEN 	'<LOANLEASEGAP>'
							+ CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT1_AMOUNT_TEXT, '') AS VARCHAR)
							+'</LOANLEASEGAP>'                                    
        			WHEN 'SORPE' 	THEN 	'<IS200SOUNDREPRODUCING>'
							+ CAST( (CASE isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0)                                       
              							when  0 then  'FALSE'                                    
              							else 'TRUE'
								end) AS VARCHAR)
							+'</IS200SOUNDREPRODUCING>'                                    
  				WHEN 'SRTE' 	THEN 	'<SOUNDRECEIVINGTRANSMITTINGSYSTEM>'
							+ CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR) 
							+ '</SOUNDRECEIVINGTRANSMITTINGSYSTEM>'                                    
        			WHEN 'EECOMP' 	THEN 	'<EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE>'                
 							+ case  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) 
 							when '0' then ''  
 							else CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)                   
 							end
							+'</EXTRAEQUIPCOMPREHENSIVEDEDUCTIBLE>'                                    
         						+'<INSURANCEAMOUNT>'
							+convert(varchar(30),isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0))
							+'</INSURANCEAMOUNT>'           
                                WHEN 'EECOLL' 	THEN 	'<EXTRAEQUIPCOLLISIONDEDUCTIBLE>'+  CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR)   
							+'</EXTRAEQUIPCOLLISIONDEDUCTIBLE>'                              
        						+'<COLLISIONTYPEDED></COLLISIONTYPEDED>'                                         
        						+'<EXTRAEQUIPCOLLISIONTYPE></EXTRAEQUIPCOLLISIONTYPE>'                                      
                                        
  				WHEN 'BISPL' 	THEN 	'<BI>'
							+ case  CAST(isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0) AS VARCHAR)                     
 							when '0' then ''
							else 
 							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                        
 							+'/'                          
 							+ replace(convert(varchar(20),convert(money,POL_VEHICLE_COVERAGES.LIMIT_2),1),'.00','')                          
							end 
							+ '</BI>'
							+'<BILIMIT1>'
							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                          
 							+'</BILIMIT1>'
							+'<BILIMIT2>'                          
 							+ Substring(CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR))  )                         
 							+'</BILIMIT2>'                           
                               WHEN 'PD' 	THEN 	'<PD>' 
							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))
							+ '</PD>'                             
      				WHEN 'SLL' 	THEN 	'<CSL>'                   
 							+ case isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)                   
 							when 0 then ''                   
							else Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                  
 							end
							+ '</CSL>'                     
                   		WHEN 'PUMSP' 	THEN 	'<UMSPLIT>'                       
 							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                      
 							+ '/'                       
 							+  Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))                      
 							+ '</UMSPLIT>'                               
 							+ '<UMSPLITLIMIT1>'                      
 							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))                      
 							+ '</UMSPLITLIMIT1>'                                    
 							+ '<UMSPLITLIMIT2>'                      
							+ Substring(CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR))  )                      
 							+ '</UMSPLITLIMIT2>'            
     				-- underinsured motorist
						WHEN 'UNDSP' THEN '<UIMSPLIT>'
							+Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))+'/' + Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_2,0)),1),0))
							+'</UIMSPLIT>'
							+'<UIMSPLITLIMIT1>'
							+Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) 
							+'</UIMSPLITLIMIT1>'
							+'<UIMSPLITLIMIT2>'
							+Substring(CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR),0,charindex('.',CAST((isnull(POL_VEHICLE_COVERAGES.LIMIT_2, 0)/1000) AS VARCHAR)))
							+'</UIMSPLITLIMIT2>'+	
							case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) 
								when 0  then ''
							else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  
							END
						WHEN 'UNCSL' THEN '<UIMCSL>'
							+CASE isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)                      
									WHEN 0 THEN ''                      
							 ELSE SUBSTRING(CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,CHARINDEX('.',CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) END
							+'</UIMCSL>'+ 
							case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) 
							when 0  then '' 
							else  
							'<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  
							end                                         
						WHEN 'PUNCS' 	THEN 	'<UMCSL>'
							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0))  
							+'</UMCSL>'                      
                      		+ '<PIP></PIP>'                                    
         					+ '<PPI></PPI>'    
     					WHEN 'MP' 	THEN 	'<MEDPM>'
							+ case isnull(POL_VEHICLE_COVERAGES.LIMIT_ID, 0)
							when 685  then 'PRIMARY'
							WHEN 686 THEN 'EXCESSWAGE' 
							WHEN 687 THEN 'EXCESSMEDICAL' 
							WHEN 688 THEN 'EXCESSBOTH'  
							else '' END  
							+'</MEDPM>'        
     						+'<MEDPMDEDUCTIBLE>'
							+ cast (isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1,0) as varchar )
							+'</MEDPMDEDUCTIBLE>'    
    						+ '<MPLIMIT>'
							+ CONVERT(VARCHAR(30),isnull(POL_VEHICLE_COVERAGES.LIMIT_1, 0))  
							+'</MPLIMIT>'                                     
     				WHEN 'UMPD' 	THEN 	'<PDLIMIT>'
							+ Substring(convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(POL_VEHICLE_COVERAGES.LIMIT_1,0)),1),0)) 
							+'</PDLIMIT>'                     
         						+'<PDDEDUCTIBLE>'
							+ CAST(isnull(POL_VEHICLE_COVERAGES.DEDUCTIBLE_1, 0) AS VARCHAR) 
							+'</PDDEDUCTIBLE>'                                    
--     				WHEN 'UNDSP'  	THEN 	case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) 
--							when 0  then '' 
--							else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  
--							end 
--     				WHEN 'UNCSL'  	THEN 	case isnull(POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID, 0) 
--							when 0  then '' 
--							else  '<isUnderInsuredMotorists>TRUE</isUnderInsuredMotorists>'  
--							end -- only in case of  indiana                                    
								                               /* - TO DO                                      
											      1. RENTALREIMLIMITDAY (Rental Reimbursement Limit Day)                                    
											      2. RENTALREIMMAXCOVG (Rental Reimbursement Max Coverage)                                    
											    3. Extra Equipment Collision Type                                    
											      4. Extra Equipment Collision Deductible                                    
											      5. TYPE                                    
											      6. ISUNDERINSUREDMOTORIST                                    
											      7. INSURANCE AMOUNT                                    
											      */                                    
     				ELSE ''                                     
     						END ) + @ReturnValue                                     
   						FROM    MNT_COVERAGE   WITH (NOLOCK)                                  
    							left OUTER JOIN  POL_VEHICLE_COVERAGES WITH (NOLOCK) ON POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                    
      							AND (POL_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID) AND (POL_VEHICLE_COVERAGES.POLICY_ID = @POLICYID) AND                                     
                 					(POL_VEHICLE_COVERAGES.POLICY_VERSION_ID = @POLICYVERSIONID) AND   (POL_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                                    
   							WHERE     MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID                                    
                                       
                                      
                                       
  	END                                    
                         
 /************************************************************** END OF INDIANA ****************************************************************************************************************************************/                                    





                   
                          
 DECLARE @RetVal varchar(8000)                                    
                      
-- SET @RetVal = @ReturnValue                        
                      
 select @RetVal =                             
 case                              
  when charindex('<isUnderInsuredMotorists>',@ReturnValue) > 0  then @ReturnValue                             
  when charindex('<isUnderInsuredMotorists>',@ReturnValue) = 0                         
  then @ReturnValue + '<isUnderInsuredMotorists>False</isUnderInsuredMotorists>'                                      
  else @ReturnValue                                      
 end                      
 select @RetVal=                      
  case                      
  when charindex('<CSL>',@ReturnValue) > 0  then @ReturnValue                       
  when charindex('<CSL>',@ReturnValue) = 0                         
  then @ReturnValue + '<CSL></CSL>'                                      
  else @ReturnValue                                      
 end             
                           
 select @RetVal=                      
  case                      
  when charindex('<MINITORTPDLIAB>',@ReturnValue) > 0  then @ReturnValue                             
  when charindex('<MINITORTPDLIAB>',@ReturnValue) = 0                         
  then @ReturnValue + '<MINITORTPDLIAB>FALSE</MINITORTPDLIAB>'                                      
  else @ReturnValue                                      
 end                   
                      
                   
 /* Add 'type' node in case of INDIANA state/                                      
 1. If BI coverage is present then 'BI ONLY'                                      
 2. If BI and UMPD both do not exists then '' or 'NO COVERAGE'                                         
 3. IF UMPD exists then check the deductible and 'BI/PD with 0 deductible' or 'BI/PD with 300 deductible' */                                      
   IF @STATEID ='14'   --INDIANA                                         
     BEGIN                                      
      if exists                                      
   ( SELECT *                                      
     FROM                                                    
        MNT_COVERAGE WITH (NOLOCK) inner JOIN  POL_VEHICLE_COVERAGES WITH (NOLOCK) ON POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                            
         AND (POL_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                            
       AND (POL_VEHICLE_COVERAGES.POLICY_ID = @POLICYID)                                             
       AND (POL_VEHICLE_COVERAGES.POLICY_VERSION_ID = @POLICYVERSIONID)                                             
       AND (POL_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                                            
      WHERE                                                 
       MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID and MNT_COVERAGE.COV_CODE='BI'                                      
   )                                      
    BEGIN                                      
    SET @RetVal = @RetVal + '<Type>BI ONLY</Type>'                                      
    END                                  
   else if exists                                      
   ( SELECT *                                      
     FROM                                         
        MNT_COVERAGE WITH (NOLOCK) inner JOIN  POL_VEHICLE_COVERAGES WITH (NOLOCK) ON POL_VEHICLE_COVERAGES.COVERAGE_CODE_ID = MNT_COVERAGE.COV_ID                                       
         AND (POL_VEHICLE_COVERAGES.CUSTOMER_ID = @CUSTOMERID)                 
       AND (POL_VEHICLE_COVERAGES.POLICY_ID = @POLICYID)                                             
       AND (POL_VEHICLE_COVERAGES.POLICY_VERSION_ID = @POLICYVERSIONID)      
       AND (POL_VEHICLE_COVERAGES.VEHICLE_ID = @VEHICLEID)                        
      WHERE                                                 
       MNT_COVERAGE.STATE_ID =@STATEID and MNT_COVERAGE.LOB_ID=@LOBID and MNT_COVERAGE.COV_CODE in ('BIPD','UMPD')                                      
   )                                      
     BEGIN                                      
      SET @RetVal = @RetVal + '<Type>BIPD</Type>'                  
     END                                      
   else                                       
   BEGIN                                      
    SET @RetVal = @RetVal + '<Type></Type>'                                      
   END                                      
     END                                       
                  
            
/*            
-----------------------                                       
DECLARE @ENDORSEMENTS  nvarchar(4000)                  
DECLARE @ENDORSEMENT_ID NVARCHAR(4)                  
                  
IF @STATEID ='14'                     
 SET @ENDORSEMENT_ID ='6' --SOUND RECEIVING AND TRANSMITTING ENDORSEMENT CODE FOR INDIANA                  
ELSE                  
 SET @ENDORSEMENT_ID ='28'--SOUND RECEIVING AND TRANSMITTING ENDORSEMENT CODE FOR MICHIGAN                  
                  
SET @ENDORSEMENTS=''                  
                             
   SELECT @ENDORSEMENTS =  '<SOUNDRECEIVINGTRANSMITTINGSYSTEM>'                       
   + Substring(convert(varchar(30),convert(money,isnull(REMARKS,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(REMARKS,0)),1),0)) + '</SOUNDRECEIVINGTRANSMITTINGSYSTEM>'                                      
   FROM POL_VEHICLE_ENDORSEMENTS                    
 WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND                   
 VEHICLE_ID = @VEHICLEID AND ENDORSEMENT_ID =@ENDORSEMENT_ID                    
                  
                  
IF @STATEID ='14'                     
 SET @ENDORSEMENT_ID ='3' --extra equipment amount  ENDORSEMENT CODE FOR INDIANA                  
ELSE                  
 SET @ENDORSEMENT_ID ='26'--extra equipment amount  ENDORSEMENT CODE FOR michigan                  
                  
                  
SELECT @ENDORSEMENTS =  @ENDORSEMENTS + '<INSURANCEAMOUNT>'                       
   + Substring(convert(varchar(30),convert(money,isnull(REMARKS,0)),1),0,charindex('.',convert(varchar(30),convert(money,isnull(REMARKS,0)),1),0)) + '</INSURANCEAMOUNT>'                                      
FROM POL_VEHICLE_ENDORSEMENTS                    
 WHERE CUSTOMER_ID = @CUSTOMERID AND POLICY_ID = @POLICYID AND POLICY_VERSION_ID = @POLICYVERSIONID AND                   
 VEHICLE_ID = @VEHICLEID AND ENDORSEMENT_ID =@ENDORSEMENT_ID                   
  SELECT                                  
   @RetVal + @ENDORSEMENTS as COVERAGES  */                
                                    
                                      
 /* FINAL SELECT */                          
 SELECT               
   @RetVal  as COVERAGES                                    
                                      
END                                    
                  




GO

