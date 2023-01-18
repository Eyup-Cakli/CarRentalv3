﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.Listed);
        }

        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.CarDailyPrice >= min && p.CarDailyPrice <= max), Messages.Listed);
        }

        public IDataResult<Car> GetById(int carId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Car>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<Car>(_carDal.Get(p => p.CarId == carId), Messages.Listed);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.Listed);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            IResult result = BusinessRules.Run(CheckICarNotExists(car.CarId));
            if (result != null)
            {
                return new ErrorResult(Messages.InvalidUpdate);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckICarNotExists(int carId)
        {
            var result = _carDal.GetAll(p=>p.CarId == carId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.CarNotExists);
            }
            return new SuccessResult();
        }
    }
}