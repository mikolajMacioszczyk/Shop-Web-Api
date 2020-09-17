using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApi.DAL.Repositories.Orders;
using ShopApi.Models.Orders;

namespace ShopApi.QueryBuilder.Order
{
    public class OrderQueryBuilder : IOrderQueryBuilder
    {
        private readonly IOrderRepository _repository;
        private IQueryable<Models.Orders.Order> _query;

        public OrderQueryBuilder(IOrderRepository repository)
        {
            _repository = repository;
        }

        public IOrderQueryBuilder GetAll()
        {
            _query = _repository.GetIQuerable();
            return this;
        }

        public IOrderQueryBuilder WithTotalPrizeGreaterThan(double minTotalPrize)
        {
            _query = _query.Where(o => o.TotalPrize >= minTotalPrize);
            return this;
        }

        public IOrderQueryBuilder WithTotalPrizeSmallerThan(double maxTotalPrize)
        {
            _query = _query.Where(o => o.TotalPrize <= maxTotalPrize);
            return this;
        }

        public IOrderQueryBuilder WithTotalWeightGreaterThan(int minTotalWeight)
        {
            _query = _query.Where(o => o.TotalWeight >= minTotalWeight);
            return this;
        }

        public IOrderQueryBuilder WithTotalWeightSmallerThan(int maxTotalWeight)
        {
            _query = _query.Where(o => o.TotalWeight <= maxTotalWeight);
            return this;
        }

        public IOrderQueryBuilder WithStatus(string status)
        {
            try
            {
                Status asStatus = (Status) Enum.Parse(typeof(Status), status);
                _query = _query.Where(o => o.Status == asStatus);
                return this;
            }
            catch (ArgumentException)
            {
                return this;
            }
        }

        public IOrderQueryBuilder WithDateOfAdmissionGreaterThan(DateTime minDate)
        {
            _query = _query.Where(o => o.DateOfAdmission >= minDate);
            return this;
        }

        public IOrderQueryBuilder WithDateOfAdmissionSmallerThan(DateTime maxDate)
        {
            _query = _query.Where(o => o.DateOfAdmission <= maxDate);
            return this;
        }

        public IOrderQueryBuilder WithDateOfRealizationGreaterThan(DateTime minDate)
        {
            _query = _query.Where(o => o.DateOfRealization >= minDate);
            return this;
        }

        public IOrderQueryBuilder WithDateOfRealizationSmallerThan(DateTime maxDate)
        {
            _query = _query.Where(o => o.DateOfRealization <= maxDate);
            return this;
        }

        public IOrderQueryBuilder WithFurniture(int[] furnitureIds)
        {
            _query = _query.Where(o => o.Furnitures.Any(f => furnitureIds.Contains(f.FurnitureId)));
            return this;
        }
        
        public async Task<IEnumerable<Models.Orders.Order>> ToListAsync()
        {
            var output = await _query.ToListAsync();
            _query = null;
            return output;
        }
    }
}