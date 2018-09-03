using System;

namespace TokenGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            String token = Authorization.GenerateToken();
            Console.WriteLine("Token: " + token);
            Console.WriteLine("Correct Token: " + Authorization.CheckToken(token));
            Console.ReadLine();

        }
    }
}
