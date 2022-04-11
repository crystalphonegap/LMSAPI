 truncate table [AppUserExperienceCenters]

  Update ExperienceCenters set city='Delhi' where id=9
  update ExperienceCenters set city='Trivandrum' where id=17
	
  Insert into [AppUserExperienceCenters]
  Select NEWID(), u.Id, e.Id From AspNetUsers u
  INNER JOIN ExperienceCenters e ON u.FullName = e.City