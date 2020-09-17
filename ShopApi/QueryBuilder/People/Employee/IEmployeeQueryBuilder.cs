using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.QueryBuilder.People.Employee
{
    public interface IEmployeeQueryBuilder
    {
        IEmployeeQueryBuilder GetAll();
        IEmployeeQueryBuilder WithNameLike(string pattern);
        IEmployeeQueryBuilder WithAddress(int addressId);
        IEmployeeQueryBuilder WithSalaryGreaterThan(double minSalary);
        IEmployeeQueryBuilder WithSalarySmallerThan(double maxSalary);
        IEmployeeQueryBuilder WithJobTitle(string jobTitle);
        IEmployeeQueryBuilder WithPermission(string permission);
        IEmployeeQueryBuilder WithDateOfBirthGreaterThan(DateTime minDate);
        IEmployeeQueryBuilder WithDateOfBirthSmallerThan(DateTime maxDate);
        IEmployeeQueryBuilder WithDateOfEmploymentGreaterThan(DateTime minDate);
        IEmployeeQueryBuilder WithDateOfEmploymentSmallerThan(DateTime maxDate);
        Task<List<Models.People.Employee>> ToListAsync();
    }
}