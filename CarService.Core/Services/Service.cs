using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Core.Records;
using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.Core.Services;

public class Service
{
    private Service(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } 

    public string Description { get; private set; } 
    
    public bool IsShowLending { get; private set; } = false;
    public virtual List<ServiceType> ServiceTypes { get; private set; } = [];
    
    public virtual List<Record> Records { get; private set; } = [];
    
    public virtual List<UserAuth> Users { get; private set; } = [];
    

    public static Result<Service> Create(Guid id, string name, string description)
    {
        if (id == Guid.Empty)
            return Result.Failure<Service>("Id can't be empty");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Service>("Name can't be empty");

        if (string.IsNullOrWhiteSpace(description))
            return Result.Failure<Service>("Description can't be empty");

        var service = new Service(id, name, description);

        return Result.Success(service);
    }
}