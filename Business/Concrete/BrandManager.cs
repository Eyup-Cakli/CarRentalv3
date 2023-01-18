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
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNameExist(brand.BrandName));
            if (result != null)
            {
                return result;
            }
            _brandDal.Add(brand);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNotExists(brand.BrandId));
            if (result != null)
            {
                return new ErrorResult(Messages.BrandNotExists);
            }
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Brand>>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.Listed);
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNotExists(brand.BrandId));
            if (result != null)
            {
                return new ErrorResult(Messages.InvalidUpdate);
            }
            _brandDal.Update(brand);
            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckIfBrandNameExist(string branName)
        {
            var result = _brandDal.GetAll(p => p.BrandName == branName).Any();
            if (result)
            {
                return new ErrorResult(Messages.BrandNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfBrandNotExists(int brandID)
        {
            var result = _brandDal.GetAll(p => p.BrandId == brandID).Any();
            if (!result)
            {
                return new ErrorResult(Messages.BrandNotExists);
            }
            return new SuccessResult();
        }
    }
}
