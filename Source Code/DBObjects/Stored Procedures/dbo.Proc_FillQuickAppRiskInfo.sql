IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FillQuickAppRiskInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FillQuickAppRiskInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
Created By		: Charles Gomes  
Comments		: Attach Quick Application to Customer
Created Date	: 24-Jun-10
modified by:sonal
modified date:15-july-10

drop proc Proc_FillQuickAppRiskInfo
*/
create proc [dbo].[Proc_FillQuickAppRiskInfo]
(
@CUSTOMER_ID INT,
@POLICY_ID INT, 
@POLICY_VERSION_ID SMALLINT
)
AS
begin

declare @POLICY_LOB nvarchar(10)

select @POLICY_LOB = policy_lob from POL_CUSTOMER_POLICY_LIST where CUSTOMER_ID = @CUSTOMER_ID
and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID

IF (@POLICY_LOB='9')  
		 BEGIN  
		  IF EXISTS(SELECT PERIL_ID FROM POL_PERILS WITH(NOLOCK)                                                                                                               
		  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)
		  BEGIN
			 SELECT PERIL_ID as id, CONVERT(varchar(250),ISNULL(p.TYPE,''))+ ' ' +LKP.LOOKUP_VALUE_DESC as value 
			FROM POL_PERILS p WITH(NOLOCK) LEFT OUTER JOIN  MNT_LOOKUP_VALUES LKP on 
			CONVERT(varchar(250),LKP.LOOKUP_UNIQUE_ID)=CONVERT(varchar(250),p.ACTIVITY_TYPE)                                                                                                                
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
			/* SELECT PERIL_ID as id, ACTIVITY_TYPE+' '+TYPE value FROM POL_PERILS WITH(NOLOCK)                                                                                                                   
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID*/
		  END                                         
		 END  
		 ELSE IF (@POLICY_LOB='13')  
		 BEGIN  
		  IF EXISTS(SELECT MARITIME_ID FROM POL_MARITIME WITH(NOLOCK)                                                                                                               
		  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)
		  BEGIN
			SELECT MARITIME_ID as id,TYPE_OF_VESSEL + ' ' + NAME_OF_VESSEL as value FROM POL_MARITIME WITH(NOLOCK)                                                                                                               
		    WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
		  END                                              
		 END  
		 ELSE IF (@POLICY_LOB IN('17','18'))  
		  BEGIN
			IF EXISTS(SELECT VEHICLE_ID FROM POL_CIVIL_TRANSPORT_VEHICLES WITH(NOLOCK)                                                                                                               
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)
			BEGIN
				SELECT VEHICLE_ID as id, CONVERT(VARCHAR(200),CLIENT_ORDER)+' '+CONVERT(VARCHAR(200),VEHICLE_NUMBER) as value FROM POL_CIVIL_TRANSPORT_VEHICLES WITH(NOLOCK)                                                                                                               
				WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
			END                                              
		  END
		 ELSE IF (@POLICY_LOB IN ('20','23'))  
		 BEGIN  
		  IF EXISTS(SELECT COMMODITY_ID FROM POL_COMMODITY_INFO WITH(NOLOCK)                                                                                                               
		  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)
		  BEGIN
			SELECT COMMODITY_ID as id, COMMODITY_ID as value FROM POL_COMMODITY_INFO WITH(NOLOCK)                                                                                                               
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
		  END                                              
		 END  
		 ELSE IF (@POLICY_LOB IN ('15','21'))  
		 BEGIN  
		  IF EXISTS(SELECT PERSONAL_INFO_ID FROM POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)                                                                                                               
		  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)
		  BEGIN
			SELECT PERSONAL_INFO_ID as id,  ISNULL(INDIVIDUAL_NAME,'')+' '+ISNULL(CODE,'') as value FROM POL_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)                                                                                                               
			WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
		  END                                              
		 END  
		 ELSE IF (@POLICY_LOB ='22' )  
		 BEGIN  
		  IF EXISTS(SELECT PERSONAL_ACCIDENT_ID FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)                                                                                                               
		  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)
		  BEGIN
			SELECT PERSONAL_ACCIDENT_ID as id, NUMBER_OF_PASSENGERS as value FROM POL_PASSENGERS_PERSONAL_ACCIDENT_INFO WITH(NOLOCK)                                                                                                               
		    WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
		  END                                              
		 END 
		  ELSE IF (@POLICY_LOB IN ('10','12','14') )
		  
		  BEGIN
		   IF EXISTS(SELECT PRODUCT_RISK_ID FROM POL_PRODUCT_LOCATION_INFO WITH(NOLOCK)                                                                                                               
		  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)
		  BEGIN
			SELECT PRODUCT_RISK_ID as id, CONVERT(varchar(250),ISNULL(VALUE_AT_RISK,0))+' '+CONVERT(varchar(250),ISNULL(MAXIMUM_LIMIT,0)) as value  FROM POL_PRODUCT_LOCATION_INFO WITH(NOLOCK)                                                                                                               
		    WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID
		  END  
		  END
		  
		 ELSE  --IN(11,16,19)
		 BEGIN  
		  IF EXISTS(SELECT PRODUCT_RISK_ID FROM POL_PRODUCT_LOCATION_INFO WITH(NOLOCK)                                                                                                               
		  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID)
		  BEGIN
			SELECT PRODUCT_RISK_ID as id, CONVERT(varchar(250),ISNULL(VALUE_AT_RISK,0))+ ' ' +ISNULL(LKP.LOOKUP_VALUE_DESC,'') as value 
			FROM POL_PRODUCT_LOCATION_INFO p WITH(NOLOCK) LEFT OUTER JOIN  MNT_LOOKUP_VALUES LKP on 
			LKP.LOOKUP_UNIQUE_ID=p.ACTIVITY_TYPE  WHERE CUSTOMER_ID=@CUSTOMER_ID AND
			POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID 
		  END                                              
		 END
end
GO

