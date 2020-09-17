using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.People.Emplyee;
using ShopApi.Models.People;

namespace ShopApi.QueryBuilder.People.Employee
{
    public class EmployeeQueryBuilder : IEmployeeQueryBuilder
    {
        private readonly IEmployeeRepository _repository;
        private IQueryable<Models.People.Employee> _query;

        public EmployeeQueryBuilder(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public IEmployeeQueryBuilder GetAll()
        {
            _query = _repository.GetIQuerable();
            return this;
        }

        public IEmployeeQueryBuilder WithNameLike(string pattern)
        {
            _query = from p in _query
                where EF.Functions.Like(p.Name, pattern)
                select p;
            return this;
        }

        public IEmployeeQueryBuilder WithAddress(int addressId)
        {
            _query = _query.Where(p => p.Address.Id == addressId);
            return this;
        }

        public IEmployeeQueryBuilder WithSalaryGreaterThan(double minSalary)
        {
            _query = _query.Where(e => e.Salary >= minSalary);
            return this;
        }

        public IEmployeeQueryBuilder WithSalarySmallerThan(double maxSalary)
        {
            _query = _query.Where(e => e.Salary <= maxSalary);
            return this;
        }

        public IEmployeeQueryBuilder WithJobTitle(string jobTitle)
        {
            try
            {
                JobTitles asJobTitles = (JobTitles) Enum.Parse(typeof(JobTitles), jobTitle);
                _query = _query.Where(e => e.JobTitles == asJobTitles);
                return this;
            }
            catch (ArgumentException)
            {
                return this;
            }
        }

        public IEmployeeQueryBuilder WithPermission(string permission)
        {
            try
            {
                Permission asPermission = (Permission) Enum.Parse(typeof(Permission), permission);
                _query = _query.Where(e => e.Permission == asPermission);
                return this;
            }
            catch (ArgumentException)
            {
                return this;
            }
        }

        public IEmployeeQueryBuilder WithDateOfBirthGreaterThan(DateTime minDate)
        {
            _query = _query.Where(e => e.DateOfBirth >= minDate);
            return this;
        }

        public IEmployeeQueryBuilder WithDateOfBirthSmallerThan(DateTime maxDate)
        {
            _query = _query.Where(e => e.DateOfBirth <= maxDate);
            return this;
        }

        public IEmployeeQueryBuilder WithDateOfEmploymentGreaterThan(DateTime minDate)
        {
            _query = _query.Where(e => e.DateOfEmployment >= minDate);
            return this;
        }

        public IEmployeeQueryBuilder WithDateOfEmploymentSmallerThan(DateTime maxDate)
        {
            _query = _query.Where(e => e.DateOfEmployment <= maxDate);
            return this;
        }

        public async Task<List<Models.People.Employee>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}