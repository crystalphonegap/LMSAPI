using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HRJ.LMS.Application.Dto;
using HRJ.LMS.Application.Errors;
using HRJ.LMS.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRJ.LMS.Application.AppLead
{
    public class InvoiceFile
    {
        public class InvoiceFileQuery : IRequest<LeadInvoiceFileDto>
        {
            public Guid LeadInvoiceFileId { get; set; }
        }
        
        public class Handler : IRequestHandler<InvoiceFileQuery, LeadInvoiceFileDto>
        {
            private readonly AppDbContext _context;
            public Handler(AppDbContext context)
            {
                _context = context;
            }
            public async Task<LeadInvoiceFileDto> Handle(InvoiceFileQuery request,
                CancellationToken cancellationToken)
            {
                //handler logic goes here
                var leadInvoiceFileDetails = await _context.LeadInvoiceFileDetails.FindAsync(request.LeadInvoiceFileId);

                if (leadInvoiceFileDetails == null)
                {
                    throw new RestException(System.Net.HttpStatusCode.NotFound, "Invoice file not found");
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "LeadInvoices", leadInvoiceFileDetails.SystemFileName);

                var memory = new MemoryStream();

                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;

                var invoiceFileContent = new LeadInvoiceFileDto
                {
                    FileContents = memory,
                    FileName = leadInvoiceFileDetails.FileName,
                    ContentType = GetContentType(filePath)
                };

                return invoiceFileContent;
            }
            private string GetContentType(string path)  
            {  
                var types = GetMimeTypes();  
                var ext = Path.GetExtension(path).ToLowerInvariant();  
                return types[ext];  
            } 

            private Dictionary<string, string> GetMimeTypes()  
            {  
                return new Dictionary<string, string>  
                {  
                    {".txt", "text/plain"},  
                    {".pdf", "application/pdf"},  
                    {".doc", "application/vnd.ms-word"},  
                    {".docx", "application/vnd.ms-word"},  
                    {".xls", "application/vnd.ms-excel"},  
                    {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
                    {".png", "image/png"},  
                    {".jpg", "image/jpeg"},  
                    {".jpeg", "image/jpeg"},  
                    {".gif", "image/gif"},  
                    {".csv", "text/csv"}  
                };  
            } 
        }

         
   
        
    }

        
}