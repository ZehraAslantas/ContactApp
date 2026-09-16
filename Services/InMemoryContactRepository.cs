using ContactApp.Models;


namespace ContactApp.Services;

public class InMemoryContactRepository : IContactRepository
{
    private readonly List<Contact> _contacts;
    /*referans tanımlı ifadeyi kullanabilmek için başlatılması şart
     bu da contructor da yazdığımız ilk satır ile olur.yada ctor
    olmadan tanımlandığı satırda başlatılmalı.*/
    private int _nextId=1;
    public InMemoryContactRepository()
        //ctor içine yazdıklarımız bellekte nesnenin hazır olarak barındırmasını istediklerimiz
        //ve bunlara veriyide burda eklersek bellekte o veriler de hazır olur mesela Ahmet yılmaz için
    //ctor yaz constructor oluşturur.
    /*Constructor (yapıcı metot), bir nesne belleğe ilk çıktığı (new dendiği) 
     * anda çalışan ve o nesnenin sağlıklı,
     * güvenli ve eksiksiz bir şekilde hayata başlamasını sağlar*/
    {
        _contacts =new List<Contact>();
        /*uygulama üzerinde doğrudan sonuç almak için listeyi ctor da başlatcazç
        çekirdek data denen seed data eklicez bu listeye
        alt satırdaki yapı*/
        var seed = new List<Contact>()
        {//bu bi liste her veriden sonra virgül koy
            //birkaç nesne eklicez seed data'ya
            new Contact(){FirstName="Ahmet",LastName="Yılmaz",Email="ahmet.yilmaz@example.com",Phone="+905551112233",Company="BTK Akademi",Title="Yazılım Geliştirme Uzmanı",Notes=".NET"},
            new Contact(){FirstName="Ayşe",LastName="Demir",Email="ayse.yilmaz@example.com",Phone="+905551122233",Company="BTK Akademi",Title="Yazılım Geliştirme Uzmanı",Notes=".NET"},
            new Contact(){FirstName="Mehmet",LastName="Kaya",Email="mehmet.yilmaz@example.com",Phone="+905551222233",Company="BTK Akademi",Title="Yazılım Geliştirme Uzmanı",Notes=".NET"},
            new Contact(){FirstName="Elif",LastName="Çetin",Email="elif.yilmaz@example.com",Phone="+905552222233",Company="BTK Akademi",Title="Yazılım Geliştirme Uzmanı",Notes=".NET"},
            new Contact(){FirstName="Can",LastName="Aydın",Email="can.yilmaz@example.com",Phone="+905551112133",Company="BTK Akademi",Title="Yazılım Geliştirme Uzmanı",Notes=".NET"},
            new Contact(){FirstName="Zeynep",LastName="Bulut",Email="zeynep.yilmaz@example.com",Phone="+905551112113",Company="BTK Akademi",Title="Yazılım Geliştirme Uzmanı",Notes=".NET"},
            new Contact(){FirstName="Emre",LastName="Arslan",Email="emre.yilmaz@example.com",Phone="+905551112123",Company="BTK Akademi",Title="Yazılım Geliştirme Uzmanı",Notes=".NET"},
            new Contact(){FirstName="Selin",LastName="Koç",Email="selin.yilmaz@example.com",Phone="+905551122133",Company="BTK Akademi",Title="Yazılım Geliştirme Uzmanı",Notes=".NET"}

        };
        foreach(var c in seed)
        {
            c.Id = _nextId++;
            //c nin ıd alanına atamayı yap sonra ıd yi artır
            _contacts.Add(c);//listeye c yi de ekliyoruz
        }
    }
    /*gelen implementler throw hata fırlatmayla geliyo oraları
     sil istediğin şekilde doldur metotları*/
    public Contact Add(Contact contact)//contact dışardan alınan parametre
    {
        contact.Id = _nextId++;//ıd yi unutma contact kısmına eklemeyi
        _contacts.Add(contact);//listeye ekliyoruz
        return contact;
        //id değeri her veride 1 fazla olacak şekilde ayarlandı.bunu veritabanı tabloları gibi düşün
    }

    public bool Delete(int id)
    {
        //önce silmek istenen veri varmı diye kontrol edelim
        var existing = GetById(id);
        if (existing is null)
            return false;
        _contacts.Remove(existing);//else durumunda existing veriyi remove ile listeden siliyoruz
        return true;
    }

    public IEnumerable<Contact> GetAll() =>
        //lambda expression body yani metot gövdesi tek satırda döner.
        _contacts
        .OrderBy(c => c.LastName)//orderby ile sıralıyoruz soyada göre c ile aldığımız verileri
        .ThenBy(c => c.FirstName);//eğer soyadları aynı olan varsa onları isme göre sırala
    //ordeby,thenby vb. bunlar LINQ kütüphanesine bağlı ilerde bahseder veri tabanı ve EF de

    public Contact? GetById(int id) =>
        _contacts.FirstOrDefault(c  => c.Id.Equals(id));
    //contak a git filtreleme yap.contağın id si parametreden gelene
    //eşit olacak ve ilk gördüğü kaydı dönecek

    public bool Update(Contact contact)
    {//güncelleme
        //önce kayıt varmı emin olalım
        var existing = GetById(contact.Id);//parametreden gelen contact ıd sini al kontrol için
        if(existing is null)
            return false;
        existing.FirstName = contact.FirstName;
        existing.LastName = contact.LastName;
        existing.Email = contact.Email;
        existing.Phone = contact.Phone;
        existing.Company = contact.Company;
        existing.Title = contact.Title;
        existing.Notes = contact.Notes;
        //contact senin dışardan aldığın parametre yani kullanıcının girdiği
        //metot içine yazılan parametreler sınıfta tanımlı olan değildir dışardan alınanlardır
        //bu nedenle mesela OOP de metottaki değişkeni sınıftaki o değişkene this. olarak constructor ile atıyoz ya:))
        return true;


    }
}
