﻿using application.cqrs._base;
using application.exceptions;
using application.interfaces;
using AutoMapper;
using domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenCage.Geocode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Employee.Commands
{
    public class CreateBioLogRequestHandler : RequestHandlerBase, IRequestHandler<CreateBioLogRequest, CreateBioLogResponse>
    {
        public CreateBioLogRequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CreateBioLogResponse> Handle(CreateBioLogRequest request, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(i => i.EmployeeNumber.ToLower() == request.EmployeeNumber.ToLower());

            if (employee == null) throw new NotFoundException(nameof(domain.Employee), request.EmployeeNumber);


            var geoCoder = new Geocoder("271460ae98bf4e2baae91c779d36992b");
            var result = await geoCoder.ReverseGeocodeAsync(request.Lat, request.Long);
            var bio = new BioLog
            {
                Lat = request.Lat,
                Long = request.Long,
                EmployeeID = employee.ID,
                LogType = request.LogType,
                Time = DateTimeOffset.Now,
                Location = result.Results.Length != 0 ? result.Results[0].Formatted : "n/a",

            };

            Blame(bio, employee.CompanyEmail, employee.CompanyEmail);

            dbContext.RawLogs.Add(bio);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateBioLogResponse(nameof(BioLog), bio.ID);
        }
    }
}
