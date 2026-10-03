using MoominsApi.Repo;
using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MoominClient
{
    internal class Program
    {
        private static Moomin muumi = new Moomin();
        static async Task Main(string[] args)
        {


            // https://localhost:7099/MoominValley linkki muistiin
            using var client = new HttpClient();

            Console.WriteLine("Valitse toiminto:");
            Console.WriteLine("Etsi muumeja\nLisää muumi");
            Console.WriteLine();
            var answer = Console.ReadLine();

            switch (answer.ToUpper())
            {
                case "ETSI MUUMEJA":

                    Console.WriteLine("Etsi muumin nimeä kirjoittamalla kirjain/kirjaimia:");
                    var LoytyykoMuumi = Console.ReadLine(); //pyydetään käyttäjää syöttämään muumin nimen kirjaimia

                    string linkki = $"https://localhost:7099/MoominValley?name={LoytyykoMuumi}";

                    var MissaMuumit = await client.GetAsync(linkki);

                    if (MissaMuumit.IsSuccessStatusCode) //jos on onnistunut niin sitten tullaan tähän if:iin
                    {
                        var str = await MissaMuumit.Content.ReadAsStringAsync();

                        Console.WriteLine(str);
                        Console.ReadLine();
                    }

                    break;


                case "LISÄÄ MUUMI":

                    Moomin moomin = new Moomin(); //luodaan uusi muumi ilmentymä

                    Console.WriteLine("Kirjoita muumin nimi:");
                    moomin.Name = Console.ReadLine(); //tallennetaan annettu nimi muumin tietoihin

                    Console.WriteLine();
                    Console.WriteLine("Anna muumin numero:");
                    moomin.Number = int.Parse(Console.ReadLine()); //tallennetaan annettu numero muumin tietoihin

                    var jsonMoomin = JsonSerializer.Serialize(moomin); //muutetaan muumi json muotoon

                    var content = new StringContent(jsonMoomin, System.Text.Encoding.UTF8, "application/json"); //sisältö, (application/json) meta tietoa jotka menevät sanoman headeriin

                    string linkki2 = "https://localhost:7099/MoominValley";

                    var result = await client.PostAsync(linkki2, content); //pitää määrittää content eli mitä viedään sanomassa

                    if (result.IsSuccessStatusCode) //palautuuko OK tai vastaava statuskoodi 200 - 299
                    {
                        var value = await result.Content.ReadAsStringAsync(); //palauttaa merkkijonon muuttujaan
                        Console.WriteLine($"{value}"); //näytetään lisätty muumi käyttäjälle
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine($"Status code: {result.StatusCode}"); //näytetään status koodi käyttäjälle jos ei onnistunut
                        Console.ReadLine();
                    }

                    break;

            }
        }
    }
}
