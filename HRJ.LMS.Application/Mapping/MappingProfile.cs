using System;
using System.Collections.Generic;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Dto.User;
using HRJ.LMS.Domain;

namespace HRJ.LMS.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUserMenu, AppUserMenuDto>();

            CreateMap<LeadCaptureDto, Lead>();

            CreateMap<LeadContactDetailDto, LeadContactDetail>();

            CreateMap<LeadCallerRemark, LeadCallerRemarkDto>();

            CreateMap<LeadECManagerRemark, LeadECManagerRemarkDto>();

            CreateMap<LeadContactDetail, LeadContactDetailDto>()
                .ForMember(d => d.MobileNumber, opts => opts.MapFrom(s => GetTenDigitMobileNo(s.MobileNumber)));

            CreateMap<Lead, LeadDetailDto>();
            CreateMap<LeadActivity, LeadActivityDto>();
            CreateMap<State, StateDto>();
            CreateMap<LeadCallingStatus, LeadCallingStatusDto>();
            CreateMap<LeadClassification, LeadClassificationDto>();
            CreateMap<LeadEnquiryType, LeadEnquiryTypeDto>();
            CreateMap<LeadSpaceType, LeadSpaceTypeDto>();
            CreateMap<LeadStatus, LeadStatusDto>();
            CreateMap<LeadReminderOption, LeadReminderOptionDto>();
            CreateMap<LeadSource, LeadSourceDto>();

            CreateMap<LeadReminder, LeadReminderDto>();
           

            CreateMap<ECStateMapping, ExperienceCenterStateDto>()
                .ForMember(d => d.StateId, opts => opts.MapFrom(s => s.State.Id))
                .ForMember(d => d.StateName, opts => opts.MapFrom(s => s.State.StateName))
                .ForMember(d => d.ExperienceCenterId, opts => opts.MapFrom(s => s.ExperienceCenter.Id))
                .ForMember(d => d.ExperienceCenterName, opts => opts.MapFrom(s => s.ExperienceCenter.ExperienceCenterName))
                .ForMember(d => d.Address, opts => opts.MapFrom(s => s.ExperienceCenter.Address));

            CreateMap<ExperienceCenter, ExperienceCenterDto>()
                .ForMember(d => d.ExperienceCenterId, opts => opts.MapFrom(s => s.Id));

            CreateMap<UploadExcelLog, UploadExcelLogDto>();

            CreateMap<LeadIndiaMartDto, LeadCaptureDto>()
                .ForMember(d => d.LeadDateTime, opts => 
                    opts.MapFrom(s => new DateTime(
                        Convert.ToInt32(s.LOG_TIME.Substring(0, 4)),
                        Convert.ToInt32(s.LOG_TIME.Substring(4, 2)),
                        Convert.ToInt32(s.LOG_TIME.Substring(6, 2)),
                        Convert.ToInt32(s.LOG_TIME.Substring(8, 2)),
                        Convert.ToInt32(s.LOG_TIME.Substring(10, 2)),
                        Convert.ToInt32(s.LOG_TIME.Substring(12, 2))
                    )
                ))
                .ForMember(d => d.EnquiryType, opts => opts.MapFrom(s => GetLeadSourceType(s.QTYPE)))
                .ForMember(d => d.ContactPersonName, opts => opts.MapFrom(s => s.SENDERNAME))
                .ForMember(d => d.Address, opts => opts.MapFrom(s => s.ENQ_ADDRESS))
                .ForMember(d => d.City, opts => opts.MapFrom(s => s.ENQ_CITY))
                .ForMember(d => d.State, opts => opts.MapFrom(s => s.ENQ_STATE))
                .ForMember(d => d.Company, opts => opts.MapFrom(s => s.GLUSR_USR_COMPANYNAME))
                .ForMember(d => d.Subject, opts => opts.MapFrom(s => s.SUBJECT))
                .ForMember(d => d.Description, opts => opts.MapFrom(s => s.ENQ_MESSAGE))
                .ForMember(d => d.LastUpdatedBy, opts => opts.MapFrom(s => "System"))
                .ForMember(d => d.LastUpdatedAt, opts => opts.MapFrom(s => DateTime.Now))
                .ForMember(d => d.LeadSource, opts => opts.MapFrom(s => "India Mart"))
                .ForMember(d => d.LeadSourceId, opts => opts.MapFrom(s => s.QUERY_ID))
                .ForMember(d => d.LeadContactDetails, opts => opts.MapFrom(s => GetLeadContactDetails(s)));

            CreateMap<AppLead.LeadJustDial.LeadJustDialCommand, JustDialLead>()
                .ForMember(d => d.LeadCreatedAt, opts => opts.MapFrom(s => DateTime.Now));

            CreateMap<AppLead.LeadJustDial.LeadJustDialCommand, Lead>()
                .ForMember(d => d.LeadSourceId, opts => opts.MapFrom(s => s.LeadId))
                .ForMember(d => d.LeadSource, opts => opts.MapFrom(s => "Just Dial"))
                .ForMember(d => d.ContactPersonName, opts => opts.MapFrom(s => s.Name))
                .ForMember(d => d.LeadDateTime, opts => opts.MapFrom(s => GetLeadDateTime(s)))
                .ForMember(d => d.Subject, opts => opts.MapFrom(s => s.Category))
                .ForMember(d => d.Address, opts => opts.MapFrom(s => s.Area))
                .ForMember(d => d.LeadContactDetails, opts => opts.MapFrom(s => GetJustDialLeadContactDetails(s)))
                .ForMember(d => d.LastUpdatedBy, opts => opts.MapFrom(s => "System"))
                .ForMember(d => d.LastUpdatedAt, opts => opts.MapFrom(s => DateTime.Now));

            CreateMap<AppLead.DashLock.LeadDashlockCommand, LeadDashLock>()
              .ForMember(d => d.LeadCreatedAt, opts => opts.MapFrom(s => DateTime.Now));

            CreateMap<AppLead.DashLock.LeadDashlockCommand, Lead>()
                .ForMember(d => d.LeadSourceId, opts => opts.MapFrom(s => s.Leadsource_Id))
                .ForMember(d => d.LeadSource, opts => opts.MapFrom(s => "Dash Lock"))
                //.ForMember(d => d.ContactPersonName, opts => opts.MapFrom(s => s.))
                .ForMember(d => d.LeadDateTime, opts => opts.MapFrom(s => GetDashLockLeadDateTime(s)))
                //.ForMember(d => d.Subject, opts => opts.MapFrom(s => s.))
                .ForMember(d => d.Address, opts => opts.MapFrom(s => s.locality))
                .ForMember(d => d.LeadContactDetails, opts => opts.MapFrom(s => GetDashLockLeadContactDetails(s)))
                .ForMember(d => d.LastUpdatedBy, opts => opts.MapFrom(s => "System"))
                .ForMember(d => d.LastUpdatedAt, opts => opts.MapFrom(s => DateTime.Now));

            CreateMap<AppLead.DashLockForm.LeadDashLockFormCommand, LeadDashLock_Form>()
              .ForMember(d => d.LeadCreatedAt, opts => opts.MapFrom(s => DateTime.Now));

            CreateMap<AppLead.DashLockForm.LeadDashLockFormCommand, Lead>()
                .ForMember(d => d.LeadSourceId, opts => opts.MapFrom(s => s.Leadsource_Id))
                .ForMember(d => d.LeadSource, opts => opts.MapFrom(s => "Dash Lock"))
                .ForMember(d => d.ContactPersonName, opts => opts.MapFrom(s => s.name))
                .ForMember(d => d.LeadDateTime, opts => opts.MapFrom(s => GetDashLockLeadFormDateTime(s)))
                .ForMember(d => d.Subject, opts => opts.MapFrom(s => s.description))
                .ForMember(d => d.Address, opts => opts.MapFrom(s => s.locality))
                .ForMember(d => d.LeadContactDetails, opts => opts.MapFrom(s => GetDashLockLeadFormContactDetails(s)))
                .ForMember(d => d.LastUpdatedBy, opts => opts.MapFrom(s => "System"))
                .ForMember(d => d.LastUpdatedAt, opts => opts.MapFrom(s => DateTime.Now));


            CreateMap<AppLead.LeadWebPortal.LeadWebPortalCommand, Lead>()
                .ForMember(d => d.LeadSourceId, opts => opts.MapFrom(s => s.LeadId))
                .ForMember(d => d.LeadDateTime, opts => opts.MapFrom(s => s.Date))
                .ForMember(d => d.ContactPersonName, opts => opts.MapFrom(s => s.Name))
                .ForMember(d => d.LeadContactDetails, opts => opts.MapFrom(s => GetWebLeadContactDetails(s)))
                .ForMember(d => d.Subject, opts => opts.MapFrom(s => s.EnquiryFor))
                .ForMember(d => d.Description, opts => opts.MapFrom(s => s.Message))
                .ForMember(d => d.LeadSource, opts => opts.MapFrom(s => s.WebSiteSource)) 
                .ForMember(d => d.LastUpdatedBy, opts => opts.MapFrom(s => "System"))
                .ForMember(d => d.LastUpdatedAt, opts => opts.MapFrom(s => DateTime.Now));

            CreateMap<AppLead.LeadWebPortal.LeadWebPortalCommand, WebLead>()
                .ForMember(d => d.LeadCreatedAt, opts => opts.MapFrom(s => DateTime.Now));

            CreateMap<UploadLeadDto, Lead>()
                .ForMember(d => d.LastUpdatedBy, opts => opts.MapFrom(s => "System"))
                .ForMember(d => d.LastUpdatedAt, opts => opts.MapFrom(s => DateTime.Now))
                .ForMember(d => d.EnquiryType, opts => opts.MapFrom(s => s.EnquireFor))
                .ForMember(d => d.LeadContactDetails, opts => opts.MapFrom(s => GetWebLeadContactDetails(s)));

            CreateMap<LeadInvoiceFileDetail, LeadInvoiceFileDetailDto>()
                .ForMember(d => d.UploadedBy, opts => opts.MapFrom(s => s.UploadedByName));
            
        }

        private string GetTenDigitMobileNo(string mobileNo)
        {
            if(string.IsNullOrEmpty(mobileNo) || mobileNo.Length <= 10)
                return mobileNo;

            return mobileNo?.Substring(mobileNo.Length - 10);
        }

        private DateTime GetLeadDateTime(AppLead.LeadJustDial.LeadJustDialCommand leadJustDialCommand)
        {
            var leadDateTime = leadJustDialCommand.Date;
            if(!string.IsNullOrEmpty(leadJustDialCommand.Time))
            {
                var splittedTime = leadJustDialCommand.Time.Split(":");
                if (splittedTime.Length == 3) 
                {
                    leadDateTime = new DateTime(leadJustDialCommand.Date.Year,
                                    leadJustDialCommand.Date.Month,
                                    leadJustDialCommand.Date.Day,
                                    Convert.ToInt32(splittedTime[0]),
                                    Convert.ToInt32(splittedTime[1]),
                                    Convert.ToInt32(splittedTime[2])
                                    );
                }
            }

            return leadDateTime;
        }

        private List<LeadContactDetail> GetJustDialLeadContactDetails(AppLead.LeadJustDial.LeadJustDialCommand leadJustDialCommand)
        {
            var leadContactDetails = new List<LeadContactDetail>
            {
                new LeadContactDetail
                {
                    MobileNumber = GetTenDigitMobileNo(leadJustDialCommand.Mobile),
                    EmailAddress = leadJustDialCommand.Email,
                    PhoneNumber = leadJustDialCommand.Phone,
                    ContactType = "Primary"
                }
            };

            return leadContactDetails;
        }

        // New Dash Lock

        private DateTime GetDashLockLeadDateTime(AppLead.DashLock.LeadDashlockCommand leadJustDialCommand)
        {
            var leadDateTime = leadJustDialCommand.Date;
            //if (!string.IsNullOrEmpty(leadJustDialCommand.Time))
            //{
            //    var splittedTime = leadJustDialCommand.Time.Split(":");
            //    if (splittedTime.Length == 3)
            //    {
            //        leadDateTime = new DateTime(leadJustDialCommand.Date.Year,
            //                        leadJustDialCommand.Date.Month,
            //                        leadJustDialCommand.Date.Day,
            //                        Convert.ToInt32(splittedTime[0]),
            //                        Convert.ToInt32(splittedTime[1]),
            //                        Convert.ToInt32(splittedTime[2])
            //                        );
            //    }
            //}

            return leadDateTime;
        }

        private DateTime GetDashLockLeadFormDateTime(AppLead.DashLockForm.LeadDashLockFormCommand leadJustDialCommand)
        {
            var leadDateTime = Convert.ToDateTime(leadJustDialCommand.lead_date);
            //if (!string.IsNullOrEmpty(leadJustDialCommand.Time))
            //{
            //    var splittedTime = leadJustDialCommand.Time.Split(":");
            //    if (splittedTime.Length == 3)
            //    {
            //        leadDateTime = new DateTime(leadJustDialCommand.Date.Year,
            //                        leadJustDialCommand.Date.Month,
            //                        leadJustDialCommand.Date.Day,
            //                        Convert.ToInt32(splittedTime[0]),
            //                        Convert.ToInt32(splittedTime[1]),
            //                        Convert.ToInt32(splittedTime[2])
            //                        );
            //    }
            //}

            return leadDateTime;
        }

        private List<LeadContactDetail> GetDashLockLeadContactDetails(AppLead.DashLock.LeadDashlockCommand leadJustDialCommand)
        {
            var leadContactDetails = new List<LeadContactDetail>
            {
                new LeadContactDetail
                {
                    MobileNumber = GetTenDigitMobileNo(leadJustDialCommand.called_number),
                    //EmailAddress = leadJustDialCommand.e,
                    PhoneNumber = leadJustDialCommand.called_number,
                    ContactType = "Primary"
                }
            };

            return leadContactDetails;
        }

        private List<LeadContactDetail> GetDashLockLeadFormContactDetails(AppLead.DashLockForm.LeadDashLockFormCommand leadJustDialCommand)
        {
            var leadContactDetails = new List<LeadContactDetail>
            {
                new LeadContactDetail
                {
                    MobileNumber = GetTenDigitMobileNo(leadJustDialCommand.mobile),
                    EmailAddress = leadJustDialCommand.email,
                    PhoneNumber = leadJustDialCommand.mobile,
                    ContactType = "Primary"
                }
            };

            return leadContactDetails;
        }

        private List<LeadContactDetail> GetWebLeadContactDetails(AppLead.LeadWebPortal.LeadWebPortalCommand leadJustDialCommand)
        {
            var leadContactDetails = new List<LeadContactDetail>
            {
                new LeadContactDetail
                {
                    MobileNumber = GetTenDigitMobileNo(leadJustDialCommand.ContactNumber),
                    EmailAddress = leadJustDialCommand.Email,
                    ContactType = "Primary"
                }
            };

            return leadContactDetails;
        }
        private List<LeadContactDetail> GetWebLeadContactDetails(UploadLeadDto leadSource)
        {
            var leadContactDetails = new List<LeadContactDetail>
            {
                new LeadContactDetail
                {
                    MobileNumber = GetTenDigitMobileNo(leadSource.MobileNumber), // leadSource.MobileNumber?.Substring(leadSource.MobileNumber.Length - 10),
                    EmailAddress = leadSource.EmailAddress,
                    PhoneNumber = leadSource.PhoneNumber,
                    ContactType = "Primary"
                }
            };

            if (!(string.IsNullOrEmpty(leadSource.MobileNumber2) || string.IsNullOrEmpty(leadSource.PhoneNumber2)
                    || string.IsNullOrEmpty(leadSource.EmailAddress2)))
            {
                leadContactDetails.Add
                (
                    new LeadContactDetail
                    {
                        MobileNumber = GetTenDigitMobileNo(leadSource.MobileNumber2), //leadSource.MobileNumber2?.Substring(leadSource.MobileNumber.Length - 10),
                        EmailAddress = leadSource.EmailAddress2,
                        PhoneNumber = leadSource.PhoneNumber2,
                        ContactType = "Secondary"
                    }
                );
            }

            return leadContactDetails;
        }
        private List<LeadContactDetailDto> GetLeadContactDetails(LeadIndiaMartDto leadSource)
        {
            var stringLength = 10;
            if (!string.IsNullOrEmpty(leadSource.MOB) && leadSource.MOB.Length < 10)
            {
                stringLength = leadSource.MOB.Length;
            }

            var leadContactDetails = new List<LeadContactDetailDto>
            {
                new LeadContactDetailDto
                {
                    MobileNumber = leadSource.MOB?.Substring(leadSource.MOB.Length - stringLength),
                    EmailAddress = leadSource.SENDEREMAIL,
                    PhoneNumber = leadSource.PHONE,
                    ContactType = "Primary"
                }
            };

            if (string.IsNullOrEmpty(leadSource.PHONE_ALT) && 
                string.IsNullOrEmpty(leadSource.EMAIL_ALT) && 
                string.IsNullOrEmpty(leadSource.MOBILE_ALT))
            {
                return leadContactDetails;
            }
            
            stringLength = 10;

            if (!string.IsNullOrEmpty(leadSource.MOBILE_ALT) && leadSource.MOBILE_ALT.Length < 10)
            {
                stringLength = leadSource.MOBILE_ALT.Length;
            }

            leadContactDetails.Add
            (
                new LeadContactDetailDto
                {
                    MobileNumber = leadSource.MOBILE_ALT?.Substring(leadSource.MOBILE_ALT.Length - stringLength),
                    EmailAddress = leadSource.EMAIL_ALT,
                    PhoneNumber = leadSource.PHONE_ALT,
                    ContactType = "Secondary"
                }
            );

            return leadContactDetails;
        }
        private string GetLeadSourceType(string LeadSource)
        {
            if ("W".Equals(LeadSource))
            {
                return "Direct";
            }
            else if ("B".Equals(LeadSource))
            {
                return "Buy";
            }
            else if ("P".Equals(LeadSource))
            {
                return "Call";
            }

            return "";
        }
    }
}