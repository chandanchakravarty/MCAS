IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '1'
  AND Category = 'ClaimantType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'1', N'Commuter ', N'Commuter ', N'ClaimantType')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '2'
  AND Category = 'ClaimantType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'2', N'Pedestrain ', N'Pedestrain ', N'ClaimantType')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '3'
  AND Category = 'ClaimantType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'3', N'Vehicle Owner ', N'Vehicle Owner ', N'ClaimantType')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '4'
  AND Category = 'ClaimantType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'4', N'Company', N'Company', N'ClaimantType')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '1'
  AND Category = 'ClaimType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'1', N'Own Damage (OD) ', N'Own Damage (OD) ', N'ClaimType')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '2'
  AND Category = 'ClaimType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'2', N'Property Damage (TPPD) ', N'Property Damage (TPPD) ', N'ClaimType')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '3'
  AND Category = 'ClaimType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'3', N'Body Injury (TPBI)', N'Body Injury (TPBI)', N'ClaimType')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '1'
  AND Category = 'ClaimantStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'1', N'Pending ', N'Pending ', N'ClaimantStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '2'
  AND Category = 'ClaimantStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'2', N'Finalized ', N'Finalized ', N'ClaimantStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '3'
  AND Category = 'ClaimantStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'3', N'Cancelled ', N'Cancelled ', N'ClaimantStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '4'
  AND Category = 'ClaimantStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'4', N'Reopened', N'Reopened', N'ClaimantStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '1'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'1', N'Insured Hit Parked TP Vehicle ', N'Insured Hit Parked TP Vehicle ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '2'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'2', N'Changing Lanes ', N'Changing Lanes ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '3'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'3', N'Insured Reversing ', N'Insured Reversing ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '4'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'4', N'Insurered into Rear of TP Vehicle ', N'Insurered into Rear of TP Vehicle ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '5'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'5', N'Alleged Accident ', N'Alleged Accident ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '6'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'6', N'Roundabout Collision ', N'Roundabout Collision ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '7'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'7', N'Multiple Vehicle Accident ', N'Multiple Vehicle Accident ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '8'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'8', N'TP Turns across Oncoming Traffic ', N'TP Turns across Oncoming Traffic ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '9'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'9', N'Traffic Light Collision ', N'Traffic Light Collision ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '10'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'10', N'Insurerd Vehicle Hit - Parked Known ', N'Insurerd Vehicle Hit - Parked Known ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '11'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'11', N'Immobile Property (Excl Canopies) ', N'Immobile Property (Excl Canopies) ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '12'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'12', N'Overtaking ', N'Overtaking ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '13'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'13', N'Insured Vehicle Hit in Rear ', N'Insured Vehicle Hit in Rear ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '14'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'14', N'Junction Collision ', N'Junction Collision ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '15'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'15', N'Pedestrian ', N'Pedestrian ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '16'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'16', N'Insured Turn across Oncoming Traffic ', N'Insured Turn across Oncoming Traffic ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '17'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'17', N'TP Reversing ', N'TP Reversing ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '18'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'18', N'TP Pulling In ', N'TP Pulling In ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '19'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'19', N'Insured Pulling into/from Parked Position ', N'Insured Pulling into/from Parked Position ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '20'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'20', N'Insured from Slip Road ', N'Insured from Slip Road ', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '21'
  AND Category = 'AccidentCause')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'21', N'Cyclist', N'Cyclist', N'AccidentCause')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '1'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'1', N'Law ', N'Law ', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '2'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'2', N'Workshop ', N'Workshop ', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '3'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'3', N'Individual ', N'Individual ', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '4'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'4', N'Insurer ', N'Insurer ', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '5'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'5', N'Malaysia Registered Vehicle ', N'Malaysia Registered Vehicle ', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '6'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'6', N'MLAW ', N'MLAW ', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '7'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'7', N'SMRT ', N'SMRT ', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '8'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'8', N'Unknown TP ', N'Unknown TP ', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '9'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'9', N'Bus not damaged ', N'Bus not damaged ', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '10'
  AND Category = 'CaseCategory')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'10', N'Counter-Claim', N'Counter-Claim', N'CaseCategory')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '1'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'1', N'Negotiation ', N'Negotiation ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '2'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'2', N'Discharge Voucher sent/received ', N'Discharge Voucher sent/received ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '3'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'3', N'Insurers Take Over ', N'Insurers Take Over ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '4'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'4', N'Inactive ', N'Inactive ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '5'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'5', N'Drop ', N'Drop ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '6'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'6', N'Installments ', N'Installments ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '7'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'7', N'CDR ', N'CDR ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '8'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'8', N'1st Reminder ', N'1st Reminder ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '9'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'9', N'2nd Reminder ', N'2nd Reminder ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '10'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'10', N'Settlment at District ', N'Settlment at District ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '11'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'11', N'For COs Action ', N'For COs Action ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '12'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'12', N'Pending Documents (External) ', N'Pending Documents (External) ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '13'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'13', N'Pending Documents (Internal) ', N'Pending Documents (Internal) ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '14'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'14', N'Payment Porcessing ', N'Payment Porcessing ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '15'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'15', N'Pre-Repair ', N'Pre-Repair ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '16'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'16', N'Letter of Demand sent/received ', N'Letter of Demand sent/received ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '17'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'17', N'WRIT ', N'WRIT ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '18'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'18', N'Reject - CCTV Evidence ', N'Reject - CCTV Evidence ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '19'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'19', N'Reject - Others ', N'Reject - Others ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '20'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'20', N'Reject - Scene Photos ', N'Reject - Scene Photos ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '21'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'21', N'Reject - Witness Statement ', N'Reject - Witness Statement ', N'CaseStatus')
END
IF NOT EXISTS (SELECT
    1
  FROM MNT_Lookups WITH (NOLOCK)
  WHERE Lookupvalue = '22'
  AND Category = 'CaseStatus')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'22', N'Reinspection', N'Reinspection', N'CaseStatus')
END


IF NOT EXISTS (SELECT
    1
  FROM [MNT_Lookups]
  WHERE Lookupvalue = '1'
  AND Category = 'MandateType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'1', N'OD Mandate Approval', N'OD Mandate Approval', N'MandateType')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Lookups]
  WHERE Lookupvalue = '2'
  AND Category = 'MandateType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'2', N'TPPD Mandate Approval', N'TPPD Mandate Approval', N'MandateType')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Lookups]
  WHERE Lookupvalue = '3'
  AND Category = 'MandateType')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'3', N'TPBI Mandate Approval', N'TPBI Mandate Approval', N'MandateType')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Lookups]
  WHERE Lookupvalue = '1'
  AND Category = 'Evidence')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'1', N'CCTV', N'CCTV', N'Evidence')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Lookups]
  WHERE Lookupvalue = '2'
  AND Category = 'Evidence')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'2', N'Witness Statement', N'Witness Statement', N'Evidence')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Lookups]
  WHERE Lookupvalue = '3'
  AND Category = 'Evidence')
BEGIN
  INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category])
    VALUES (N'3', N'Scene Pics', N'Scene Pics', N'Evidence')
END


IF NOT EXISTS (SELECT 1 
               FROM   [mnt_lookups] 
               WHERE  [lookupvalue] = '1' 
                      AND category = 'PartyTypeList') 
  BEGIN 
      INSERT [dbo].[mnt_lookups] 
             ([lookupvalue], 
              [lookupdesc], 
              [description], 
              [category]) 
      VALUES (N'1', 
              N'Insurer ', 
              N'Insurer ', 
              N'PartyTypeList') 
  END 

IF NOT EXISTS (SELECT 1 
               FROM   [mnt_lookups] 
               WHERE  [lookupvalue] = '2' 
                      AND category = 'PartyTypeList') 
  BEGIN 
      INSERT [dbo].[mnt_lookups] 
             ([lookupvalue], 
              [lookupdesc], 
              [description], 
              [category]) 
      VALUES (N'2', 
              N'Surveyor ', 
              N'Surveyor ', 
              N'PartyTypeList') 
  END 

IF NOT EXISTS (SELECT 1 
               FROM   [mnt_lookups] 
               WHERE  [lookupvalue] = '3' 
                      AND category = 'PartyTypeList') 
  BEGIN 
      INSERT [dbo].[mnt_lookups] 
             ([lookupvalue], 
              [lookupdesc], 
              [description], 
              [category]) 
      VALUES (N'3', 
              N'Lawyer ', 
              N'Lawyer ', 
              N'PartyTypeList') 
  END 

IF NOT EXISTS (SELECT 1 
               FROM   [mnt_lookups] 
               WHERE  [lookupvalue] = '4' 
                      AND category = 'PartyTypeList') 
  BEGIN 
      INSERT [dbo].[mnt_lookups] 
             ([lookupvalue], 
              [lookupdesc], 
              [description], 
              [category]) 
      VALUES (N'4', 
              N'Workshop', 
              N'Workshop', 
              N'PartyTypeList') 
  END 

IF NOT EXISTS (SELECT 1 
               FROM   [mnt_lookups] 
               WHERE  [lookupvalue] = '1' 
                      AND category = 'StatusList') 
  BEGIN 
      INSERT [dbo].[mnt_lookups] 
             ([lookupvalue], 
              [lookupdesc], 
              [description], 
              [category]) 
      VALUES (N'1', 
              N'Active ', 
              N'Active ', 
              N'StatusList') 
  END 

IF NOT EXISTS (SELECT 1 
               FROM   [mnt_lookups] 
               WHERE  [lookupvalue] = '2' 
                      AND category = 'StatusList') 
  BEGIN 
      INSERT [dbo].[mnt_lookups] 
             ([lookupvalue], 
              [lookupdesc], 
              [description], 
              [category]) 
      VALUES (N'2', 
              N'Inactive', 
              N'Inactive', 
              N'StatusList') 
  END 
  IF NOT EXISTS (SELECT 1 
               FROM   [mnt_lookups] 
               WHERE  [lookupvalue] = 'SubTotal' 
                      AND category = 'TranComponent') 
  BEGIN 
      INSERT [dbo].[mnt_lookups] 
             ([lookupvalue], 
              [lookupdesc], 
              [description], 
              [category],
              IsActive) 
      VALUES (N'SubTotal', 
              N'SubTotal', 
              N'SubTotal', 
              N'TranComponent',
              'Y') 
  END