using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Application.Interfaces;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace HRJ.LMS.Application.AppLead
{
    public class UploadInvoice
    {
        public class UploadInvoiceCommand : IRequest<BaseDto>
        {
            public IFormFile InvoiceFile { get; set; }
            public Guid LeadId { get; set; }
        }

        public class Handler : IRequestHandler<UploadInvoiceCommand, BaseDto>
        {
            private readonly AppDbContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<AppUser> _userManager;
            public Handler(AppDbContext context, IUserAccessor userAccessor, UserManager<AppUser> userManager)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<BaseDto> Handle(UploadInvoiceCommand request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var userId = _userAccessor.GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId);
                var lead = await _context.Leads.FindAsync(request.LeadId);

                var leadInvoiceFile = new LeadInvoiceFileDetail
                {
                    FileName = request.InvoiceFile.FileName,
                    Lead = lead,
                    UploadedAt = DateTime.Now,
                    UploadedBy = user,
                    UploadedByName = user.FullName,
                    IsActive = true,
                };

                var filename = request.InvoiceFile.FileName;
                var fileWithoutExtension = filename.Substring(0, filename.LastIndexOf("."));
                var fileExtension = filename.Substring(filename.LastIndexOf("."));
                leadInvoiceFile.SystemFileName = string.Format("{0}_{1}{2}", fileWithoutExtension, DateTime.Now.Ticks, fileExtension);


                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "LeadInvoices", leadInvoiceFile.SystemFileName);
                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.InvoiceFile.CopyToAsync(stream);
                }

                _context.LeadInvoiceFileDetails.Add(leadInvoiceFile);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return new BaseDto { };

                return new BaseDto { };

                throw new RestException(System.Net.HttpStatusCode.InternalServerError, "Problem saving changes");
            }
        }
    }
}






