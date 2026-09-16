namespace ContactApp.Models;

public class Contact
{
    public int Id { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Company { get; set; }
    /*null değer olabilecekleri soru işareti ile tanımladık.
     NullReferenceException (olmayan bir veriye erişmeye çalışma hatası) 
    riskini önceden kestirebilmek için*/
    public  string? Title { get; set; }
    public string? Notes { get; set; }
}
