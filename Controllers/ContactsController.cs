using ContactApp.Models;
using ContactApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactRepository _repo;//ctrl . yap ctor gelsin altta
        /*DI ile ilgili nesne buraya bağlanacak.
        readonly olarak tanımlanmış.bu demekki değeri 2 yerde verilebilir.
        tanımlandığı satır yada ctor*/
        private readonly ILogger<Controller> _logger;//loglama yapalım dedik.ctrl . yap ctor gelsin ama repo ya parametre olarak ekle yani add parameters... olanı seçtik
        //IContactRepository için services kaydı yapmıştık program.cs de ama loglama da gerek yok o arka planda hazır.
        public ContactsController(IContactRepository repo, ILogger<Controller> logger)
        {
            _repo = repo;//constructor injection yaparak nesneyi newliyor
            _logger = logger;
        }

        [HttpGet("")] //bu satırı velattaki q parametresini en son arama filresi eklerken yazdık.dolumu boşmu altta metotta kontrol ettik.
        public IActionResult Index(string? q)
        {//çekirdek dataları gösterelim kullanıcıya.
            var items=_repo.GetAll();
            if (!string.IsNullOrEmpty(q))
            {
                var term = q.Trim();
                items=items.Where(c =>
                (c.FirstName + " " + c.LastName).Contains(term, StringComparison.CurrentCultureIgnoreCase)
                    || c.FirstName.Contains(term, StringComparison.CurrentCultureIgnoreCase)
                    || c.LastName.Contains(term, StringComparison.CurrentCultureIgnoreCase));

            }
            ViewData["Title"] = "Kişiler";
            ViewBag.Query = q;
            // q da filtreleme varsa if ile kontrol ettik.var ise fitreleyip altta items ı listeye taşıyoruz.yoksa da filtrelemeden.bunun kontrolü için if i bu araya yazdık
            return View(items.ToList());
            //ama şuan vie// q da filtreleme varsa if ile kontrol ettik.var ise fitreleyip altta  için Deklarasyon yok.View e sağ tıkla go to view de. .cshtml e git onu tanımla
        
        
        }
        public IActionResult Details(int id)
        {
            /*bunu biz ekledik bir kişinin
            id ye bağlı olarak detaylarını alcaz*/
            var contact=_repo.GetById(id);
            if (contact is null)
                return NotFoundView();//bu metot  yok şuan ctrl . ile oluştur
            ViewData["Title"] = "Kişi Güncelleme";
            //solution explorer daki shared dosyasındaki
            //Layout.cshtml de bu yapı var o bailık ismini değiştirdik burda
            return View(contact);
        }
        
        private IActionResult NotFoundView()
        {//mesela şuan 8 kişinin kaydı var 70 numaralı kişiyi istediğimde hata alırız 
            //kayıt bulunmadığındada ilgili sayfayı kullanıcıya gösterelim diye ayarlıyoruz
            Response.StatusCode = 404;//not founded hatası
            ViewData["Title"] = "Bulunamadı";
            ViewBag.Message ="Kişi Bulunamadı";
            return View("NotFound");//NotFound.cshtml sayfası ekliyoruz ki kaıt bulamayınca o sayfaya gitsin
        }
        /*tepesinde etiket olmayan metotlar GET ifadesidir
         POST olmasını istersek onu belirtememiz lazım*/
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Yeni Kişi";//modelimizde olmayan fakat view dekullanmak istediğimiz nesneleri ViewData ile taşıyabiliriz 
            return View(new Contact());//önce boş haliyle nesne oluşturuldu view şablonuyla birleştirildi(HTML;CSS) ve tarayıcıya tek hali döndü
            /*Bak Create sayfası, henüz kimse form doldurmadı. Ama senin asp-for etiketlerin sapıtmasın, kutucuklar hazır dursun diye sana sıfır kilometre, 
             içi tertemiz boş bir Contact kalıbı veriyorum." demek için new Contact() nesnesi oluşturduk.
            Bu mantık Create de hep var*/
            /*Bunu da ekledik rehbere yeni birini
             eklemek gerekince diye*/

        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]//form üzerinde sahtelik olmasın diye ekliyoruz.bu backenddeki kontrolü.Bide frontend kısmınada yazdık
        public IActionResult Create(Contact contact)
        {
            //Get isteği ile gelen ifadeleri model a almamız lazım  
            if (!ModelState.IsValid)
            {//model durumu geçerlimi değilse Yeni Kişi yazısı dönsün
                ViewData["Title"] = "Yeni Kişi";
                return View(contact);//contact yazdıkya metot paarmetresine.ismi başka da yazabilirsin
            // return view de unu diyor.işlem başarısızsa formu eski halinde geri ver
            }
                _repo.Add(contact);
            _logger.LogInformation($"Kişi Eklendi: {contact.Id}{contact.FirstName}{contact.LastName}");
            TempData["Success"] = "Kayıt eklendi";
            return RedirectToAction(nameof(Index));//işlem başarılıysa kullanıcıyı Index sayfasına gönder

        }
        [HttpGet("edit/{id}")]/*bu id yazımı Route Data'dır.Route Data, bir web sayfasının URL adresindeki yolun (path) doğrudan bir parçası olarak gönderilen ve Controller tarafından yakalanan veridir.
        yani bu kısımı URL üzerinden alacağız.oranın otomatik olarak dolması lazım*/
        public IActionResult Edit(int id)
        {
            var contact=_repo.GetById(id);//ilgili eleman varmı
            if(contact is null)
                return NotFoundView();//hata bu.hazılamıştık önceden .bunu onu kullanıyoruz şuan
            ViewData["Title"] = "Kişi Görüntüleme";
            //ilgili id düzenlenecek
            return View(contact);//aldığımız kişi bilgisini contact nesnesi üzerinden sayfaya gönderiyoruz
        }
        [HttpPost("edit/{id}")]/*mantıklı düşün http bir istek ve URL üzerinde gözükecek olan yazım biçimi
                                bunu istediğin şekilde ayarlayabalirsin*/
        [ValidateAntiForgeryToken]//bu sadece post ifadelere yazılır.get ile gelen isteğin sahte olup olmadığına bakar
        public IActionResult Edit(int id,Contact contact)//kullanıcını formda doldurduğu nesne olan contact i parametre olarak al
        {
            if (id != contact.Id)
            {//parametreden gelen ıd ile sayfadan geleni karşılaştırır
                ModelState.AddModelError(string.Empty, "Geçersiz İstek!");
            }
            if(!ModelState.IsValid)
            {
                ViewData["Title"] = "Kişi Güncelleme";
                return View(contact);
            }
            //else durumu altta
            var ok=_repo.Update(contact);
            if (!ok)
            {
                return NotFoundView();

            }
            _logger.LogInformation($"Kişi güncellendi. Id: {contact.Id} {contact.FirstName} {contact.LastName}");
            //logger ifadesi konsola not düşer.bu perojede hata vs. olursa bizim konsoldan takibimizi kolaylaştırır.  
            TempData["Success"] = "Kayıt güncellendi";//sayfaya mesaj gönderdik
            return RedirectToAction(nameof(Index));


        }
        /*şimdi get ile kullanıcı bilgilerini alıp kullanıcıya göstercez.sonra da silme işlemi yapcaz
         redirect edip silme işlemini tamamlayacağız.( return RedirectToAction(nameof(Index)); bu yapı redirect)*/

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            //id ye bağlı silme işlemi
            var contact= _repo.GetById(id);//söz konusu kayıt varmı şuan
            if(contact is null)//eleman yok ise hata fırlat
                return NotFoundView();
            /*else durumunda silme onayını kullanıcıdan al ve repodan aldığın veriyi view e taşı
             sonra bunu post kısmında alıp kullancan*/
            ViewData["Title"] = "Silme Onayı";
            return View(contact);
        }
        [HttpPost("delete/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)/*bu metotta üsttekiyle parametre dahil aynı ya çakışmasın diye metot ismi değişti bide [ActionName("Delete")] eklendi.
            bunun eklenme nedeni View tarafındaki Tag Helper alışkanlığı.<form asp-action="Delete" method="post">
            yazıyorsan, ASP.NET Core Controller'da eylem adı "Delete" olan bir POST metodu arar. Metodun C# adı DeleteConfirmed olduğu için onu eşleştirebilmek adına başına [ActionName("Delete")] rozetini takar.*/
        { 
            var ok=_repo.Delete(id);
            if (!ok)
            {
                return NotFoundView();
            }
            TempData["Success"] = "Kayıt silindi";
            return RedirectToAction(nameof(Index));
        }
        }
}
