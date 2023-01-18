using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class Messages
    {
        public static string Added = "Ekleme işlemi başarılı.";
        public static string Deleted = "Silme işlemi başarılı.";
        public static string Updated = "Güncelleme işlemi başarılı.";

        public static string InvalidAdd = "Ekleme işlemi başarısız.";
        public static string InvalidUpdate = "Güncelleme işlemi başarısız.";
        public static string InvalidDelete = "Silme işlemi başarısız.";

        public static string NameInvalid = "Geçersiz isim.";
        public static string IdInvalid = "Geçersiz ID.";

        public static string MaintananceTime = "Sistem bakımda.";
        public static string Listed = "Listelendi.";

        public static string BrandNameAlreadyExists = "Bu isimde zaten bir marka kaydı bulunmakta.";
        public static string ColorNameAlreadyExists = "Bu isimde zaten bir renk kaydı bulunmakta.";
        public static string CompanyNameAlreadyExists = "Bu isimde zaten bir şirket kaydı bulunmakta.";
        public static string EmailFieldAlreadyExists = "Bu isimde zaten bir email kaydı bulunmakta.";

        public static string ImageUploaded = "Resim başarıyla yüklendi.";
        public static string ImageDeleted = "Resim başarıyla silindi.";
        public static string ImageUpdated = "Resim başarıyla güncellendi.";

        public static string BrandNotExists = "Marka bulunamadı.";
        public static string CarNotExists = "Araç bulunamadı.";
        public static string ColorNotExists = "Renk bulunamadı.";
        public static string CustomerNotExists = "Müşteri bulunamadı.";
        public static string RentalNotExists = "Kiralama bilgisi bulunamadı.";
        public static string UserNotExists = "Kullanıcı bulunamadı.";
    }
}
