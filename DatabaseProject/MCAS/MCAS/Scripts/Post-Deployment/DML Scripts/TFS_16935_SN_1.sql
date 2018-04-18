  If not exists( select * from MNT_Adjusters where AdjusterCode='SVY000121')
    Begin
      Update MNT_Adjusters set AdjusterCode='SVY000121' where AdjusterId=45
    End 
  If not exists( select * from MNT_Adjusters where AdjusterCode='SVY000122')
    Begin
      Update MNT_Adjusters set AdjusterCode='SVY000122' where AdjusterId=47
    End 
  If not exists( select * from MNT_Adjusters where AdjusterCode='ADJ400')
    Begin
      Update MNT_Adjusters set AdjusterCode='ADJ400' where AdjusterId=57
    End
  If not exists( select * from MNT_Adjusters where AdjusterCode='ADJ0001087')
    Begin
      Update MNT_Adjusters set AdjusterCode='ADJ0001087' where AdjusterId=5
    End