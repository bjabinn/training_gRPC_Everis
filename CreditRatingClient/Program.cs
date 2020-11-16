using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using GreeterService;
using CreditRatingService;

namespace CreditRatingClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client1 = new Greeter.GreeterClient(channel);
            var greetingRequest = new HelloRequest { Name = "Beltrán" };            
            var reply1 = await client1.SayHelloRequestAsync(greetingRequest);
            Console.WriteLine(reply1.Message);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


            var client = new CreditRatingCheck.CreditRatingCheckClient(channel);
            var creditRequest = new CreditRequest { CustomerId = 201, CreditQuantity = 7000 };
            var reply = await client.CheckCreditRequestAsync(creditRequest);

            Console.WriteLine($"El credito para el cliente con Id {creditRequest.CustomerId} esta {(reply.IsAccepted ? "aprobado" : "rechazado")}!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            creditRequest = new CreditRequest { CustomerId = 201, CreditQuantity = 40000 };
            reply = await client.CheckCreditRequestAsync(creditRequest);

            Console.WriteLine($"El credito para el cliente con Id {creditRequest.CustomerId} esta {(reply.IsAccepted ? "aprobado" : "rechazado")}!");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
