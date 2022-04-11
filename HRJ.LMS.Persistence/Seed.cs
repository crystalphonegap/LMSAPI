using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HRJ.LMS.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Persistence
{
    public class Seed
    {
        public static async Task SeedData(AppDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<AppUserRole> roleManager)
        {


            if (!(await roleManager.Roles.AnyAsync()))
            {
                var roles = new List<AppUserRole>
                {
                    new AppUserRole
                    {
                        Name = "Admin",
                        Description = "Application Administrator"
                    },
                    new AppUserRole
                    {
                        Name = "KPOAgent",
                        Description = "KPO Agent"
                    },
                    new AppUserRole
                    {
                        Name = "ECManager",
                        Description = "Experience Center Manager"
                    },
                    new AppUserRole
                    {
                        Name = "SalesOfficer",
                        Description = "Sales Officer"
                    }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if (!(await userManager.Users.AnyAsync()))
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        FullName = "Arnab Roy",
                        UserName = "7337041313",
                        UserType = "Business",
                        PhoneNumber = "7337041313",
                        Email = "roy.arnab@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Satish Kumar",
                        UserName = "6292242884",
                        UserType = "Business",
                        PhoneNumber = "6292242884",
                        Email = "k.satish@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Vivek Sharma",
                        UserName = "9781999889",
                        UserType = "Business",
                        PhoneNumber = "9781999889",
                        Email = "sharma.vivek@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "M V Sabrinath",
                        UserName = "9048001062",
                        UserType = "Business",
                        PhoneNumber = "9048001062",
                        Email = "mv.sabarinath@hrjohnsonindia.com",
                        Status = 2
                    }
                };

                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, "Welcome@123");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "KPOAgent");
                    }
                }

                var adminuser = new AppUser
                {
                    FullName = "Saeesh Gawade",
                    UserName = "9604002922",
                    UserType = "Business",
                    PhoneNumber = "9604002922",
                    Email = "gawde.saeesh@hrjohnsonindia.com",
                    Status = 2
                };

                var result1 = await userManager.CreateAsync(adminuser, "Welcome@123");

                if (result1.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminuser, "Admin");
                }

                var usersEC = new List<AppUser>
                {
                    new AppUser
                    {
                        FullName = "Chennai",
                        UserName = "chennaihoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "chennaihoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Ernakulam",
                        UserName = "ernakulamhoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "ernakulamhoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Coimbatore",
                        UserName = "coimbatorehoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "coimbatorehoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Kolkata",
                        UserName = "kolkattahoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "kolkattahoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Guwahati",
                        UserName = "guwahatihoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "guwahatihoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Bhubaneshwar",
                        UserName = "bhubaneshwarhoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "bhubaneshwarhoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Thane",
                        UserName = "thanedisplaycenter@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "thanedisplaycenter@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Lucknow",
                        UserName = "lucknow.hoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "lucknow.hoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Ahmedabad",
                        UserName = "ahmedabad.hoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "ahmedabad.hoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Indore",
                        UserName = "indorehoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "indorehoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Raipur",
                        UserName = "raipurhoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "raipurhoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Calicut",
                        UserName = "calicuthoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "calicuthoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Varanasi",
                        UserName = "varanasihoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "varanasihoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Trivandrum",
                        UserName = "trivandrumhoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "trivandrumhoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Pune",
                        UserName = "punehoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "punehoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Andheri",
                        UserName = "andherihoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "andherihoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Delhi",
                        UserName = "delhihoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "delhihoj@hrjohnsonindia.com",
                        Status = 2
                    },
                    new AppUser
                    {
                        FullName = "Chandigarh",
                        UserName = "chandigarhhoj@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "chandigarhhoj@hrjohnsonindia.com",
                        Status = 2
                    },
                      new AppUser
                    {
                        FullName = "Karnataka",
                        UserName = "sabarimv7@gmail.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "sabarimv7@gmail.com",
                        Status = 2
                    },
                        new AppUser
                    {
                        FullName = "Rajasthan",
                        UserName = "vivek.sharma3065@gmail.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "vivek.sharma3065@gmail.com",
                        Status = 2
                    },
                          new AppUser
                    {
                        FullName = "Bihar-Jharkhand",
                        UserName = "satishone.kumar1@gmail.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "satishone.kumar1@gmail.comm",
                        Status = 2
                    },
                            new AppUser
                    {
                        FullName = "AP-Telangana",
                        UserName = "arnab321999@gmail.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "arnab321999@gmail.com",
                        Status = 2
                    },
                              new AppUser
                    {
                        FullName = "JMQ",
                        UserName = "saraf.rajeev@hrjohnsonindia.com",
                        UserType = "Business",
                        /* PhoneNumber = "7337041313", */
                        Email = "saraf.rajeev@hrjohnsonindia.com",
                        Status = 2
                    }
                };

                foreach (var user in usersEC)
                {
                    var result = await userManager.CreateAsync(user, "Welcome@123");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "ECManager");
                    }
                }
            }

            if (!(await context.AppUserMenus.AnyAsync()))
            {
                var roles = await roleManager.Roles.ToListAsync();
                foreach (var role in roles)
                {
                    var appUserMenus = new List<AppUserMenu>
                    {
                        new AppUserMenu
                        {
                            MenuIcon = "dashboard",
                            MenuName = "Dashboard",
                            RouterLink = "/lms/dashboard",
                            AppUserRole = role,
                            RowOrder = 1
                        },
                        new AppUserMenu
                        {
                            MenuIcon = "playlist_play",
                            MenuName = "Leads",
                            RouterLink = "/lms/leads",
                            AppUserRole = role,
                            RowOrder = 2
                        },
                        new AppUserMenu
                        {
                            MenuIcon = "library_books",
                            MenuName = "Reports",
                            RouterLink = "/lms/reports",
                            AppUserRole = role,
                            RowOrder = 3
                        },
                        new AppUserMenu
                        {
                            MenuIcon = "all_inclusive",
                            MenuName = "Change Password",
                            RouterLink = "/lms/changePassword",
                            AppUserRole = role,
                            RowOrder = 4
                        }
                    };

                    if (role.Name == "Admin")
                    {
                        appUserMenus.Add(new AppUserMenu
                        {
                            MenuIcon = "file_upload",
                            MenuName = "Lead Upload",
                            RouterLink = "/lms/leadUpload",
                            AppUserRole = role,
                            RowOrder = 5
                        });
                    }

                    await context.AppUserMenus.AddRangeAsync(appUserMenus);
                    await context.SaveChangesAsync();
                }
            }

            if (!(await context.LeadCallingStatuses.AnyAsync()))
            {
                await context.LeadCallingStatuses.AddAsync(new LeadCallingStatus
                {
                    CallingStatus = "Lost To Competition",
                    RowOrder = 4
                });
                await context.SaveChangesAsync();

                await context.LeadCallingStatuses.AddAsync(new LeadCallingStatus
                {
                    CallingStatus = "Qualified",
                    RowOrder = 3
                });
                await context.SaveChangesAsync();

                await context.LeadCallingStatuses.AddAsync(new LeadCallingStatus
                {
                    CallingStatus = "Not Reachable",
                    RowOrder = 2
                });
                await context.SaveChangesAsync();

                await context.LeadCallingStatuses.AddAsync(new LeadCallingStatus
                {
                    CallingStatus = "Not Qualified",
                    RowOrder = 1
                });
                await context.SaveChangesAsync();
            }

            if (!(await context.LeadClassifications.AnyAsync()))
            {
                await context.LeadClassifications.AddAsync(new LeadClassification { Classification = "Hot", RowOrder = 1 });
                await context.LeadClassifications.AddAsync(new LeadClassification { Classification = "Warm", RowOrder = 2 });
                await context.LeadClassifications.AddAsync(new LeadClassification { Classification = "Cold", RowOrder = 3 });

                await context.SaveChangesAsync();
            }

            if (!(await context.LeadEnquiryTypes.AnyAsync()))
            {
                await context.LeadEnquiryTypes.AddAsync(new LeadEnquiryType { EnquiryType = "Dealer", RowOrder = 4 });
                await context.SaveChangesAsync();
                await context.LeadEnquiryTypes.AddAsync(new LeadEnquiryType { EnquiryType = "Builder", RowOrder = 3 });
                await context.SaveChangesAsync();
                await context.LeadEnquiryTypes.AddAsync(new LeadEnquiryType { EnquiryType = "Architect/Interior Designer", RowOrder = 2 });
                await context.SaveChangesAsync();
                await context.LeadEnquiryTypes.AddAsync(new LeadEnquiryType { EnquiryType = "IHB (End Customer)", RowOrder = 1 });
                await context.SaveChangesAsync();
                await context.LeadEnquiryTypes.AddAsync(new LeadEnquiryType { EnquiryType = "Contractor", RowOrder = 5 });
                await context.SaveChangesAsync();
                await context.LeadEnquiryTypes.AddAsync(new LeadEnquiryType { EnquiryType = "Others", RowOrder = 6 });
                await context.SaveChangesAsync();
            }

            if (!(await context.LeadSpaceTypes.AnyAsync()))
            {
                await context.LeadSpaceTypes.AddAsync(new LeadSpaceType { SpaceType = "Residential", RowOrder = 1 });
                await context.LeadSpaceTypes.AddAsync(new LeadSpaceType { SpaceType = "Commerical", RowOrder = 2 });
                await context.LeadSpaceTypes.AddAsync(new LeadSpaceType { SpaceType = "Industrial", RowOrder = 3 });
                await context.LeadSpaceTypes.AddAsync(new LeadSpaceType { SpaceType = "Projects", RowOrder = 4 });
                await context.LeadSpaceTypes.AddAsync(new LeadSpaceType { SpaceType = "Others", RowOrder = 5 });

                await context.SaveChangesAsync();
            }

            if (!(await context.LeadSources.AnyAsync()))
            {
                var leadSources = new List<LeadSource>
                {
                    new LeadSource
                    {
                        Source = "India Mart",
                        RowOrder = 1
                    },
                    new LeadSource
                    {
                        Source = "Just Dial",
                        RowOrder = 2
                    },
                    new LeadSource
                    {
                        Source = "JMQ",
                        RowOrder = 3
                    },
                    new LeadSource
                    {
                        Source = "JBD",
                        RowOrder = 4
                    },
                    new LeadSource
                    {
                        Source = "Johnson",
                        RowOrder = 5
                    },
                    new LeadSource
                    {
                        Source = "Endura",
                        RowOrder = 6
                    },
                    new LeadSource
                    {
                        Source = "Cement",
                        RowOrder = 7
                    },
                    new LeadSource
                    {
                        Source = "Social Media",
                        RowOrder = 8
                    },
                    new LeadSource
                    {
                        Source = "Affiliate Campaign",
                        RowOrder = 9
                    }
                };

                await context.LeadSources.AddRangeAsync(leadSources);
                await context.SaveChangesAsync();
            }

            if (!(await context.LeadReminderOptions.AnyAsync()))
            {
                var leadReminderOpts = new List<LeadReminderOption>
                {
                    new LeadReminderOption
                    {
                        Reminder = "3 Hours",
                        Duration = 3,
                        DurationType = 1,   //Hours
                        DurationTypeInString = "HOURS",
                        RowOrder = 1
                    },
                    new LeadReminderOption
                    {
                        Reminder = "1 Day",
                        Duration = 1,
                        DurationType = 2,   //Days
                        DurationTypeInString = "DAYS",
                        RowOrder = 2
                    },
                    new LeadReminderOption
                    {
                        Reminder = "3 Days",
                        Duration = 3,
                        DurationType = 2,   //Days
                        DurationTypeInString = "DAYS",
                        RowOrder = 3
                    },
                    new LeadReminderOption
                    {
                        Reminder = "1 Week",
                        Duration = 7,
                        DurationType = 2,   //Days
                        DurationTypeInString = "DAYS",
                        RowOrder = 4
                    },
                    new LeadReminderOption
                    {
                        Reminder = "2 Weeks",
                        Duration = 14,
                        DurationType = 2,   //Days
                        DurationTypeInString = "DAYS",
                        RowOrder = 5
                    }

                };
                await context.LeadReminderOptions.AddRangeAsync(leadReminderOpts);
                await context.SaveChangesAsync();
            }

            if (!(await context.States.AnyAsync()))
            {
                var states = new List<State>
                {
                    new State
                    {
                        StateName = "Maharashtra",
                        Region = "West",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },
                    new State
                    {
                        StateName = "Gujarat",
                        Region = "West",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },
                    new State
                    {
                        StateName = "Goa",
                        Region = "West",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },
                    new State
                    {
                        StateName = "Odisha",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Kerala",
                        Region = "South",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Punjab",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Himachal Pradesh",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Jammu and Kashmir",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Ladakh",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Tamil Nadu",
                        Region = "South",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Andhra Pradesh",
                        Region = "South",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Telangana",
                        Region = "South",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Pondicherry",
                        Region = "South",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Delhi",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Haryana",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Uttarakhand",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Rajasthan",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Assam",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Arunachal Pradesh",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Mizoram",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Tripura",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Sikkim",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Nagaland",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Meghalaya",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Madhya Pradesh",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "West Bengal",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Andaman and Nicobar Islands",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Uttar Pradesh",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Chattisgarh",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Daman and Diu",
                        Region = "West",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Jharkhand",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Bihar",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Karnataka",
                        Region = "South",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Puducherry",
                        Region = "South",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Dadra Nagar Haveli and Daman Diu",
                        Region = "South",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Chandigarh",
                        Region = "North",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Lakshadweep",
                        Region = "South",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    },new State
                    {
                        StateName = "Manipur",
                        Region = "East",
                        LastUpdatedAt = DateTime.Now,
                        LastUpdatedBy = "System"
                    }
                    //
                };

                await context.States.AddRangeAsync(states);
                await context.SaveChangesAsync();
            }

            if (!(await context.StateCityMappings.AnyAsync()))
            {
                var stateCities = new List<StateCityMapping>
                {
                    new StateCityMapping
                    {
                        StateName = "Delhi",
                        City = "Delhi"
                    },
                    new StateCityMapping
                    {
                        StateName = "Kerala",
                        City = "Kozhikode"
                    },
                    new StateCityMapping
                    {
                        StateName = "Kerala",
                        City = "Thiruvananthapuram"
                    },
                    new StateCityMapping
                    {
                        StateName = "Bhubaneswar",
                        City = "Odisha"
                    },
                    new StateCityMapping
                    {
                        StateName = "Punjab",
                        City = "Chandigarh"
                    },
                    new StateCityMapping
                    {
                        StateName = "Tamil Nadu",
                        City = "Chennai"
                    },
                    new StateCityMapping
                    {
                        StateName = "Tamil Nadu",
                        City = "Coimbatore"
                    },
                    new StateCityMapping
                    {
                        StateName = "Uttar Pradesh",
                        City = "Varanasi"
                    },
                    new StateCityMapping
                    {
                        StateName = "Uttar Pradesh",
                        City = "Lucknow"
                    },
                    new StateCityMapping
                    {
                        StateName = "West Bengal",
                        City = "Kolkata"
                    }
                };

                await context.StateCityMappings.AddRangeAsync(stateCities);
                await context.SaveChangesAsync();
            }

            if (!(await context.LeadStatuses.AnyAsync()))
            {
                await context.LeadStatuses.AddAsync(new LeadStatus
                {
                    Status = "Not Reachable",
                    RowOrder = 5
                });
                await context.SaveChangesAsync();
                await context.LeadStatuses.AddAsync(new LeadStatus
                {
                    Status = "Follow up",
                    RowOrder = 4
                });
                await context.SaveChangesAsync();
                await context.LeadStatuses.AddAsync(new LeadStatus
                {
                    Status = "No Requirement",
                    RowOrder = 3
                });
                await context.SaveChangesAsync();
                await context.LeadStatuses.AddAsync(new LeadStatus
                {
                    Status = "Closed with other brand",
                    RowOrder = 2
                });
                await context.SaveChangesAsync();
                await context.LeadStatuses.AddAsync(new LeadStatus
                {
                    Status = "Converted Lead",
                    RowOrder = 1
                });
                await context.SaveChangesAsync();
            }

            if (!(await context.ExperienceCenters.AnyAsync()))
            {
                var expCenters = new List<ExperienceCenter>
                {
                    new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Ahmedabad Experience Centre",
                        Address = "F-106-107-108, Shivalik Plaza, Near Panjrapole Ambawadi, IIM Road Ahmedabad, Gujarat.",
                        City = "Ahmedabad",
                        Pincode = "380015",
                        State = "GUJARAT",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "079-48480010",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Andheri Experience Centre",
                        Address = "1st Floor, Bonanza Building, Sahar Plaza, CTS no 243 (a), Kondivita Village, Mathuradas Vasanji Road, Andheri Kurla Road, Andheri East, Mumbai 400059, Maharashtra",
                        City = "Andheri",
                        Pincode = "400059",
                        State = "MAHARASHTRA",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "8108030975",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Bhubaneshwar Experience Centre",
                        Address = "229/5332, Gautam Nagar, Bhubaneshwar - 751014.",
                        City = "Bhubaneshwar",
                        Pincode = "751014",
                        State = "ODISHA",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "0674-2430240",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Calicut Experience Centre",
                        Address = "Tilal Tower, Survey no 279/32, 279/33, Next to Nexa Showroom, Malaparamba Bypass, Kudilthod, Calicut, Kerala- 673017",
                        City = "Calicut",
                        Pincode = "673017",
                        State = "KERALA",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "0495-2352474",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Chandigarh",
                        Address = "Sco-214 Sector 14 Panchkula (Haryana) 134113",
                        City = "Chandigarh",
                        Pincode = "134113",
                        State = "HARYANA",
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Chennai Experience Centre",
                        Address = "82, 84 & 86 Bascon Maeru, 1st floor, floor, Kodambakkam High Road, Nungambakkam, Chennai 600034",
                        City = "Chennai",
                        Pincode = "600034",
                        State = "TAMIL NADU",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "044-48611632",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Coimbatore Experience Centre",
                        Address = "Elysium Towers, No. 21, ATT Colony, Park Gate Road, Gopalapuram, Coimbatore, Tamil Nadu 641018",
                        City = "Coimbatore",
                        Pincode = "641018",
                        State = "TAMIL NADU",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "0422-4382698",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Delhi Experience Centre",
                        Address = "First & Mezzanine Floor, Block B-1/G-3, Mohan Cooperative Industrial Estate, Badarpur New Delhi 110044",
                        City = "Delhi",
                        Pincode = "110044",
                        State = "DELHI",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "9319244955",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Ernakulam Experience Centre",
                        Address = "Door no 11/30, Next to VJT Hyundai, NH 47, Kudanoor, Maradu Ernakulam 682304",
                        City = "Ernakulam",
                        Pincode = "682304",
                        State = "KERALA",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "0484-4871277",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Guwahati Experience Centre",
                        Address = "1st Floor, Above Royal Riders Honda, Near Rahul Kata, Pub Boragaon, Garchuk, Guwahati 781035",
                        City = "Guwahati",
                        Pincode = "781035",
                        State = "ASSAM",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "022-40395666",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Indore Experience Centre",
                        Address = "Shop No5, 6, 7 and 8, Upper Ground Floor, Exotica, Shalimar Township, Village Niranjanpur, A.B. Road, Indore (M.P) 452001",
                        City = "Indore",
                        Pincode = "452001",
                        State = "MADHYA PRADESH",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "0731-4988340",
                            },
                            new ECContactDetail
                            {
                                PhoneNumber = "0731-4007441",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Kolkata Experience Centre",
                        Address = "83 JBS Haldane Avenue, TOPSIA Trinity Towers, Kolkata 700046.",
                        City = "Kolkata",
                        Pincode = "700046",
                        State = "WEST BENGAL",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = " 033-46021880",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Lucknow Experience Centre",
                        Address = "1st Floor, Plot No CP-4, Vijayant khand 3, Gomtinagar Yojna, Lucknow.",
                        City = "Lucknow",
                        Pincode = "226028",
                        State = "UTTAR PRADESH",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "0522-2721699",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Pune Experience Centre",
                        Address = "Sable House, Poona Snuff Factory, Sr. No 11A, Plot 408/4/5, Satara Road, Gultekadi, Pune – 411037",
                        City = "Pune",
                        Pincode = "411037",
                        State = "MAHARASHTRA",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "7350009364",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Raipur Experience Centre",
                        Address = "M.M. Silver Plaza, Shop No.203, 2nd Floor, Opp-udyog Bhawan, Ring Road No. 1 Mahaveer Nagar, Raipur.",
                        City = "Raipur",
                        Pincode = "492001",
                        State = "CHATTISGARH",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "9644002717",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Thane Experience Centre",
                        Address = "Shop No 5, Cosmos Jewels, Near D Mart, Ghodbunder Road, Thane West-400607, Maharashtra",
                        City = "Thane",
                        Pincode = "400607",
                        State = "MAHARASHTRA",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "022-25867600",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Trivendrum Experience Centre",
                        Address = "1st Floor, 'Sreedhanya Apex', TC 22/3720 (4) Vellayambalam, Sasthamangalam PO, Thiruvananthapuram Kerala 695010.",
                        City = "Trivandrum",
                        Pincode = "695010",
                        State = "KERALA",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "0471-2551141",
                            }
                        }
                    },new ExperienceCenter
                    {
                        ExperienceCenterName = "House of Johnson - Varanasi Experience Centre",
                        Address = "First floor, D59/103 D4, 'Ashakunj', in front of Siddharth Complex, Sigra Mehmoorganj Road, Varanasi. ",
                        City = "Varanasi",
                        Pincode = "221010",
                        State = "UTTAR PRADESH",
                        ECContactDetails = new List<ECContactDetail>
                        {
                            new ECContactDetail
                            {
                                PhoneNumber = "0542-2222620",
                            }
                        }
                    }
                };

                await context.ExperienceCenters.AddRangeAsync(expCenters);
                await context.SaveChangesAsync();
            }

            if (!(await context.UploadExcelTemplates.AnyAsync()))
            {
                var excelTemplates = new List<UploadExcelTemplate>
                {
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "#",
                        ColumnOrder = 1
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "Date",
                        ColumnOrder = 2
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "Name",
                        ColumnOrder = 3
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "Email",
                        ColumnOrder = 4
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "Contact No.",
                        ColumnOrder = 5
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "Enquiry For",
                        ColumnOrder = 6
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "State",
                        ColumnOrder = 7
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "City",
                        ColumnOrder = 8
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "Message",
                        ColumnOrder = 9
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "WebsiteLeads",
                        ColumnName = "Source",
                        ColumnOrder = 10
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Sr No",
                        ColumnOrder = 1
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Date",
                        ColumnOrder = 2
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Time",
                        ColumnOrder = 3
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Prefix",
                        ColumnOrder = 4
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Caller",
                        ColumnOrder = 5
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Phone",
                        ColumnOrder = 6
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Mobile",
                        ColumnOrder = 7
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Source",
                        ColumnOrder = 8
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Email",
                        ColumnOrder = 9
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Category Name",
                        ColumnOrder = 10
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Area",
                        ColumnOrder = 11
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Data City",
                        ColumnOrder = 12
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Branch",
                        ColumnOrder = 13
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "company name",
                        ColumnOrder = 14
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "Branch ParentId",
                        ColumnOrder = 15
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Just Dial",
                        ColumnName = "BranchPin",
                        ColumnOrder = 16
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Serial_Number",
                        ColumnOrder = 1
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "State",
                        ColumnOrder = 2
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Region",
                        ColumnOrder = 3
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Branch",
                        ColumnOrder = 4
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Territory",
                        ColumnOrder = 5
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Name_of_Officer",
                        ColumnOrder = 6
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Officer_Contact_Number",
                        ColumnOrder = 7
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Lead_Source",
                        ColumnOrder = 8
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Lead_Genertion_Date",
                        ColumnOrder = 9
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "IHB_Name",
                        ColumnOrder = 10
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "IHB_Address",
                        ColumnOrder = 11
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "IHB_Tehsil",
                        ColumnOrder = 12
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "IHB_Phone_Number",
                        ColumnOrder = 13
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Stage_of_Construction",
                        ColumnOrder = 14
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Cement_Potential_in_bags",
                        ColumnOrder = 15
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Current_brand_used_at_site",
                        ColumnOrder = 16
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Lead_status",
                        ColumnOrder = 17
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Prism_Brand_sold",
                        ColumnOrder = 18
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Ordered_Volume_in_bags",
                        ColumnOrder = 19
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Order_date",
                        ColumnOrder = 20
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Cement",
                        ColumnName = "Service_offered",
                        ColumnOrder = 21
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Date",
                        ColumnOrder = 1
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Time",
                        ColumnOrder = 2
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Enquiry_Type",
                        ColumnOrder = 3
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Name",
                        ColumnOrder = 4
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Email",
                        ColumnOrder = 5
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Subject",
                        ColumnOrder = 6
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Description",
                        ColumnOrder = 7
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Mobile",
                        ColumnOrder = 8
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Alternate No.",
                        ColumnOrder = 9
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Phone No.",
                        ColumnOrder = 10
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Alternate Phone No.",
                        ColumnOrder = 11
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Alternate Email",
                        ColumnOrder = 12
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Company",
                        ColumnOrder = 13
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "Address",
                        ColumnOrder = 14
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "City",
                        ColumnOrder = 15
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Generic",
                        ColumnName = "State",
                        ColumnOrder = 16
                    }, ///
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Lead Date",
                        ColumnOrder = 1
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Lead Time",
                        ColumnOrder = 2
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Lead Source",
                        ColumnOrder = 3
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Status",
                        ColumnOrder = 4
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Lead Name",
                        ColumnOrder = 5
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Email",
                        ColumnOrder = 6
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Subject",
                        ColumnOrder = 7
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Description",
                        ColumnOrder = 8
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Mobile",
                        ColumnOrder = 9
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Alternate Mobile",
                        ColumnOrder = 10
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Phone No.",
                        ColumnOrder = 11
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Alternate Phone No.",
                        ColumnOrder = 12
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Alternate Email",
                        ColumnOrder = 13
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Company",
                        ColumnOrder = 14
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Address",
                        ColumnOrder = 15
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "City",
                        ColumnOrder = 16
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "State",
                        ColumnOrder = 17
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Assigned EC",
                        ColumnOrder = 18
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Calling Status",
                        ColumnOrder = 19
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Called by",
                        ColumnOrder = 20
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Caller Remarks",
                        ColumnOrder = 21
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Lead Classification",
                        ColumnOrder = 22
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Quantity (sqft)",
                        ColumnOrder = 23
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Enquiry type",
                        ColumnOrder = 24
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Space type",
                        ColumnOrder = 25
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Assigned Person",
                        ColumnOrder = 26
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Assigned person Remarks",
                        ColumnOrder = 27
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "EC Remarks/follow up",
                        ColumnOrder = 28
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Lead Status",
                        ColumnOrder = 29
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Conversion Date",
                        ColumnOrder = 30
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Name of Dealer Involved",
                        ColumnOrder = 31
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Dealer Code",
                        ColumnOrder = 32
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Value (INR)",
                        ColumnOrder = 33
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Volume (Sq ft)",
                        ColumnOrder = 34
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Future Requirement",
                        ColumnOrder = 35
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Future requirement – which tiles are needed?",
                        ColumnOrder = 36
                    },
                    new UploadExcelTemplate
                    {
                        LeadSource = "Download",
                        ColumnName = "Future volume requirement(M2)",
                        ColumnOrder = 37
                    }
                };
                await context.UploadExcelTemplates.AddRangeAsync(excelTemplates);
                await context.SaveChangesAsync();
            }

            if (!(await context.ExcludeLeads.AnyAsync()))
            {
                var excludeLeads = new List<ExcludeLead>
                {
                    new ExcludeLead
                    {
                        EnquiryFor = "Dealership"
                    },
                    new ExcludeLead
                    {
                        EnquiryFor = "Complaints"
                    },
                };
                await context.ExcludeLeads.AddRangeAsync(excludeLeads);
                await context.SaveChangesAsync();
            }
        }
    }
}