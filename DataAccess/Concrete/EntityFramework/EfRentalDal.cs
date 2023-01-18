using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.BrandId
                             join rental in context.Rentals on car.CarId equals rental.CarId
                             join color in context.Colors on car.ColorId equals color.ColorId
                             from customer in context.Customers
                             join user in context.Users on customer.UserId equals user.UserId
                             select new RentalDetailDto
                             {
                                 RentalId = rental.RentalId,
                                 CarId = car.CarId,
                                 BrandId = brand.BrandId,
                                 UserFirstName = user.UserFirstName,
                                 UserLastName = user.UserLastName,
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 CarDescription=car.CarDescription,
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
