using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfCompanyNameExists(customer.CompanyName));
            if (result!=null)
            {
                return result;
            }
            _customerDal.Add(customer);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerNotExists(customer.CustomerId));
            if (result!=null)
            {
                return new ErrorResult(Messages.InvalidDelete);
            }
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Customer>>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.Listed);
        }

        public IDataResult<Customer> GetById(int customerId)
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<Customer>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<Customer>(_customerDal.Get(p=>p.CustomerId==customerId),Messages.Listed);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            IResult result = BusinessRules.Run(CheckIfCustomerNotExists(customer.CustomerId));
            if (result!=null)
            {
                return new ErrorResult(Messages.InvalidUpdate);
            }
            _customerDal.Update(customer);
            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckIfCompanyNameExists(string companyName)
        {
            var result = _customerDal.GetAll(p => p.CompanyName==companyName).Any();
            if (result)
            {
                return new ErrorResult(Messages.CompanyNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCustomerNotExists(int customerId) 
        {
            var result = _customerDal.GetAll(p=>p.CustomerId==customerId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.CustomerNotExists);
            }
            return new SuccessResult();
        }
    }
}
