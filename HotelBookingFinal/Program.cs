using HotelBookingFinal.Repositories;

public class Program
{
    public static void Main(string[] args)
    {
        var adminRepository = new AdminRepository();
        adminRepository.TestRepository();
    }
}
