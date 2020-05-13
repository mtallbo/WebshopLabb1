using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.ProductAPI.Models;

namespace Webshop.ProductAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProductContext context)
        {
            //ensure db is created
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
                new Product() { Name="Logitech G29 Racing Wheel", Description="Driving Force har utformats för de senaste racingspelen för Xbox One™-konsolen. Du kommer aldrig mer att vilja köra med en vanlig spelkontroll igen när du har spelat med G920 Driving Force.", Price = 3290.00M},
                new Product() { Name="DXRacer FORMULA F08-N - Svart", Description="DXRacer’s Formula series offers a premium seating experience at an entry level price. All the components and materials used in the Formula series have been carefully selected to deliver the support you deserve during long sessions in front of the computer. ", Price = 2790.00M},
                new Product() { Name="Elgato Key Light", Description="Quality lighting is the secret to making your camera feed shine. From the way you smirk and celebrate to the way you focus and freak out, viewers want to see every expression in detail. After all, you’re the star of the show. They subscribe to see you. And with Key Light, they instantly know you’re pro. ", Price = 2399.00M},
                new Product() { Name="ASUS GeForce RTX 2070 Super 8GB Dual OC EVO", Description="Gör dig redo för de senaste spelen genom att uppgradera din dator med ett nytt grafikkort (även kallat VGA eller GPU) från NVIDIA:s Geforce GTX-serie, eller AMD:s Radeon. Vi har samlat marknadens mest populära tillverkare som ASUS, Gigabyte, MSI och Sapphire. ", Price = 6899.00M},
                new Product() { Name="Asus ROG Strix GeForce RTX 2080 Super OC 8GB", Description="Nytt grafikkortssläpp ifrån NVIDIA® & ASUS! Ta del av den nya generationens grafikkort ASUS GeForce® RTX 2080S 8 GB STRIX GAMING. GeForce® RTX 2080S drivs av helt nya NVIDIA Turing™-arkitekturen, som ger dig otroliga nya nivåer av gaming-realism, hastighet och energieffektivitet samt en mer uppslukande spelupplevelse. Det här är en helt ny sorts grafik. ", Price = 10990.00M},
                new Product() { Name="Asus RT-AC86U Gaming Router AC2900", Description=" ASUS RT-AC86U är det perfekta valet för gamers med blixtsnabbt Wi-Fi på 2 917 Mbit/s, MU-MIMO-teknik och kraftfulla funktioner som snabbar upp spelen – t.ex. WTFast® Game Accelerator och Adaptive QoS.", Price = 2290.00M},
                new Product() { Name="Braun MultiQuick 7 MQ 725", Description="MQ725 är utrustad med en extra stor startknapp och mjuk, grppvänligt handtag. Det ger dig maximal komfort när du arbetar i köket. Brauns patenterade knivsystem är en kombination av hårda, rostfria blad som ger en perfekt mix varje gång. ", Price = 699.00M},
                new Product() { Name="C3 Smörgåsgrill Ciabatta Svart", Description="Smörgåsgrillen har användarvänliga funktioner inklusive låsbart handtag för enkel öppning och stängning, termostatstyrda element och indikatorlampor. C3 Smörgåsgrillen har en snygg högblank svart finish och är både enkel och nyskapande. ", Price = 285.00M},
                new Product() { Name="HTC Vive Cosmos", Description="Cosmos has the highest resolution display yet. Soft, lightweight and easy to clean with detachable padding gives you an extra comfort for extend gaming time. ", Price = 9390.00M}
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }
            context.SaveChanges();
        }
    }
}
