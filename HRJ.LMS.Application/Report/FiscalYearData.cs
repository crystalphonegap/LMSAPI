using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRJ.LMS.Application.Report
{
    public class FiscalYearData
    {
        public class FiscalYearDataQuery : IRequest<List<FiscalYearDto>> { }

        public class Handler : IRequestHandler<FiscalYearDataQuery, List<FiscalYearDto>>
        {
            private readonly AppDbContext _context;
            public Handler(AppDbContext context)
            {
                _context = context;
            }
            public async Task<List<FiscalYearDto>> Handle(FiscalYearDataQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var fiscalYears = await _context.FiscalYears
                                    .Where(x => x.IsActive == true)
                                    .OrderByDescending(x => x.StartYearDate)
                                    .Select(x => new FiscalYearDto
                                    {
                                        Id = x.Id,
                                        FiscalYearDuration = x.FiscalYearDuration
                                    })
                                    .ToListAsync();

                return fiscalYears;
            }
        }
    }
}