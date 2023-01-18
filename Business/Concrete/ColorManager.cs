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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorNameExists(color.ColorName));

            if (result != null)
            {
                return result;
            }
            _colorDal.Add(color);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorNotExists(color.ColorId));
            if (result != null)
            {
                return new ErrorResult(Messages.InvalidDelete);
            }
            _colorDal.Delete(color);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<Color>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Color>>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.Listed);
        }

        [ValidationAspect(typeof(ValidationAspect))]
        public IResult Update(Color color)
        {
            IResult result = BusinessRules.Run(CheckIfColorNotExists(color.ColorId));
            if (result != null)
            {
                return new ErrorResult(Messages.InvalidUpdate);
            }
            _colorDal.Update(color);
            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckIfColorNameExists(string colorName)
        {
            var result = _colorDal.GetAll(p => p.ColorName == colorName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ColorNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfColorNotExists(int colorId)
        {
            var result = _colorDal.GetAll(p => p.ColorId == colorId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.ColorNotExists);
            }
            return new SuccessResult();
        }
    }
}
