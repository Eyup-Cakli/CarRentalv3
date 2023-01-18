using Business.Concrete;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            UserManager userManager = new UserManager(new EfUserDal());

            Console.WriteLine("DTO test\n");

            var result = carManager.GetCarDetails();
            if (result.Success==true)
            {
                foreach (var car in result.Data)
                {
                    Console.WriteLine(" CarId : " + car.CarId + " Marka : " + car.BrandName +
                        " Model : " + car.CarDescription + " Yıl : " + car.CarModelYear +
                        " Ücret : " + car.CarDailyPrice + " Renk : " + car.ColorName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            userManager.Add(new User { UserId = 1, UserFirstName = "Eyüp", UserLastName = "Caklı", Email = "eyupcakli@hotmail.com", Password = "111111" });
            Console.WriteLine(Messages.Added);
        }
    }
}
