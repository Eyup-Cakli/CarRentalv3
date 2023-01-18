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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            IResult result = BusinessRules.Run(CheckIfEmailAlreadyExists(user.Email));
            if (result != null)
            {
                return result;
            }
            _userDal.Add(user);
            return new SuccessResult(Messages.Added);
        }

        public IResult Delete(User user)
        {
            IResult result = BusinessRules.Run(CheckIfUserNotExists(user.UserId));
            if (result != null)
            {
                return new ErrorResult(Messages.InvalidDelete);
            }
            _userDal.Delete(user);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<User>>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.Listed);
        }

        public IDataResult<User> GetById(int userId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<User>(Messages.MaintananceTime);
            }
            return new SuccessDataResult<User>(_userDal.Get(p => p.UserId == userId), Messages.Listed);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            IResult result = BusinessRules.Run(CheckIfUserNotExists(user.UserId));
            if (result != null)
            {
                return new ErrorResult(Messages.InvalidUpdate);
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckIfEmailAlreadyExists(string email)
        {
            var result = _userDal.GetAll(p => p.Email == email).Any();
            if (result)
            {
                return new ErrorResult(Messages.EmailFieldAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfUserNotExists(int userId)
        {
            var result = _userDal.GetAll(p => p.UserId == userId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.UserNotExists);
            }
            return new SuccessResult();
        }
    }
}
