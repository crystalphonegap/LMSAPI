using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Extensions;
using HRJ.LMS.Domain;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.AppLead
{
    public class UploadLeadLog
    {
        public class UploadLogEnvelope
        {
            public int TotalLeadLogs { get; set; }
            public List<UploadExcelLogDto> UploadExcelLogs { get; set; }
        }
        public class UploadLeadLogQuery : IRequest<UploadLogEnvelope>, IQueryObject
        {
            public int? PageNo { get; set; }
            public int? PageSize { get; set; }
            public string SortBy { get; set; }
            public bool IsSortingAscending { get; set; }
            public DateTime? UploadFromDate { get; set; }
            public DateTime? UploadUptoDate { get; set; }
        }

        public class Handler : IRequestHandler<UploadLeadLogQuery, UploadLogEnvelope>
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;
            public Handler(AppDbContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<UploadLogEnvelope> Handle(UploadLeadLogQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                //check user validation
                var uploadLogQueryable = _context.UploadExcelLogs.AsQueryable();

                if (request.UploadFromDate != null && request.UploadUptoDate != null)
                {
                    request.UploadUptoDate = request.UploadUptoDate.GetValueOrDefault().AddHours(23).AddMinutes(59).AddSeconds(59);

                    uploadLogQueryable = uploadLogQueryable.Where(x => x.UploadedAt >= request.UploadFromDate && x.UploadedAt <= request.UploadUptoDate);
                }

                var uploadLogs = await uploadLogQueryable
                                    .OrderByDescending(x => x.UploadedAt)
                                    .ApplyPaging(request)
                                    .ToListAsync();

                return new UploadLogEnvelope
                {
                    UploadExcelLogs = _mapper.Map<List<UploadExcelLog>, List<UploadExcelLogDto>>(uploadLogs),
                    TotalLeadLogs = uploadLogQueryable.Count()
                };
            }
        }
    }
}