using ContactApp.Models;

namespace ContactApp.Services;
//ContactApp projesi altındaki Services te olduğumuzu anlatıyor burası

public interface IContactRepository
    /*şuan in-memory iş yapıyoruz. daha sonra fiziksel
     bir veritabanına geçince kodları değiştirmeden veritabanıyla
    çalışır hale getirebiliriz.*/
{/*ara birimler sınıflara davranış kazandırır.
  sınıflar için söz konusu olan metot imzalarının
    implemente edilmesi(gövdelerinin yazılması)
    zorunlu hale gelir(sadeece kalıtıldığı yani ':'ile
    yazılan sınıflarda).Ve interface den örnek üretilemez 
    yani new lenemez*/
    IEnumerable<Contact> GetAll();
    //contact ların listesini alacağız bunla.
    /*contact kısmı kırmızı uyarı verdi.ctrl+. yap
    models ile konuşması için using ekleyecek üste.
    çünkü Contact models in sınıfı ve o klasöre ait*/
    Contact? GetById(int id);
    Contact Add(Contact contact);
    bool Update(Contact contact);
    bool Delete(int id);
}
