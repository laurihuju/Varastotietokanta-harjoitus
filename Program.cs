using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Varastotietokanta_harjoitus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tervetuloa varastonhallintaan!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Kirjoita tietokannan osoite tai \"Lopeta\":");

                string? address = Console.ReadLine();
                if (address == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Et voi antaa tyhjää osoitetta!");
                    continue;
                }
                if(address.ToLower() == "lopeta")
                {
                    Console.WriteLine();
                    Console.WriteLine("Sovellus sammutetaan!");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Kirjoita käyttäjänimi tietokantaan tai \"Lopeta\":");

                string? user = Console.ReadLine();
                if (user == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Et voi antaa tyhjää käyttäjänimeä!");
                    continue;
                }
                if (address.ToLower() == "lopeta")
                {
                    Console.WriteLine();
                    Console.WriteLine("Sovellus sammutetaan!");
                    return;
                }

                Console.WriteLine();
                Console.WriteLine("Kirjoita salasana tietokantaan tai \"Lopeta\":");

                string? password = Console.ReadLine();
                if (password == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Et voi antaa tyhjää salasanaa!");
                    continue;
                }
                if (address.ToLower() == "lopeta")
                {
                    Console.WriteLine();
                    Console.WriteLine("Sovellus sammutetaan!");
                    return;
                }

                if (!CanConnect(address, user, password))
                {
                    Console.WriteLine();
                    Console.WriteLine("Tietokantaan ei saatu yhteyttä! Yritä uudelleen.");
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Tietokantaan yhdistäminen onnistui!");
                Console.WriteLine();

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("VARASTONHALLINTA");
                    Console.WriteLine("1 – Lisää uusi tuote");
                    Console.WriteLine("2 – Poista tuote");
                    Console.WriteLine("3 – Tulosta eri tuotteiden määrät");
                    Console.WriteLine("4 – Muokka tuotenimeä");
                    Console.WriteLine("0 – Lopeta sovellus");
                    Console.WriteLine("--------------------");
                    Console.WriteLine("Valintasi on:");

                    string? input = Console.ReadLine();
                    if(input == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Et voi antaa tyhjää syötettä!");
                        continue;
                    }
                    if(input.ToLower() == "lopeta" || input == "0")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Sovellus sammutetaan!");
                        return;
                    }

                    if(input == "1")
                    {
                        Console.WriteLine("Kirjoita tuotteen id (kokonaisluku):");
                        string? productID = Console.ReadLine();
                        if(productID == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Et voi antaa tyhjää syötettä!");
                            continue;
                        }

                        Console.WriteLine("Kirjoita tuotteen nimi:");
                        string? productName = Console.ReadLine();
                        if (productName == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Et voi antaa tyhjää syötettä!");
                            continue;
                        }

                        Console.WriteLine("Kirjoita tuotteen hinta (kokonaisluku):");
                        string? productPrice = Console.ReadLine();
                        if (productPrice == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Et voi antaa tyhjää syötettä!");
                            continue;
                        }

                        Console.WriteLine("Kirjoita tuotteen varastosaldo (kokonaisluku):");
                        string? productAmount = Console.ReadLine();
                        if (productAmount == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Et voi antaa tyhjää syötettä!");
                            continue;
                        }

                        if (!AddProduct(address, user, password, productID, productName, productPrice, productAmount))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Tuotteen lisääminen tietokantaan epäonnistui!");
                            continue;
                        }

                        Console.WriteLine();
                        Console.WriteLine("Tuote lisätty tietokantaan!");
                    } else if (input == "2")
                    {
                        Console.WriteLine("Kirjoita poistettavan tuotteen nimi:");
                        string? productName = Console.ReadLine();
                        if (productName == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Et voi antaa tyhjää syötettä!");
                            continue;
                        }

                        if (!RemoveProduct(address, user, password, productName))
                        {
                            Console.WriteLine();
                            Console.WriteLine("Tuotteen poistaminen tietokannasta epäonnistui!");
                            continue;
                        }

                        Console.WriteLine();
                        Console.WriteLine("Tuote poistettu tietokannasta!");
                    } else if (input == "3")
                    {

                    } else if (input == "4")
                    {
                        Console.WriteLine("Kirjoita tuotteen nykyinen nimi:");
                        string? productName = Console.ReadLine();
                        if (productName == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Et voi antaa tyhjää syötettä!");
                            continue;
                        }

                        Console.WriteLine("Kirjoita tuotteen uusi nimi:");
                        string? newName = Console.ReadLine();
                        if (newName == null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Et voi antaa tyhjää syötettä!");
                            continue;
                        }

                        if (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Tuotteen uudelleennimeäminen epäonnistui!");
                            continue;
                        }

                        Console.WriteLine();
                        Console.WriteLine("Tuotteen uudelleennimeäminen onnistui!");
                    } else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Antamaasi syötettä ei tunnistettu!");
                    }
                }
            }
        }

        private static bool CanConnect(string address, string user, string password)
        {
            using (VarastonhallintaDBContext database = new VarastonhallintaDBContext(address, user, password))
            {
                if (!database.Database.CanConnect())
                    return false;
                return true;
            }
        }

        private static bool AddProduct(string address, string user, string password, string productID, string productName, string productPrice, string productAmount)
        {
            int idInt;
            try
            {
                idInt = Int32.Parse(productID);
                if (idInt < 0)
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }

            int priceInt;
            try
            {
                priceInt = Int32.Parse(productPrice);
                if (priceInt < 0)
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }

            int amountInt;
            try
            {
                amountInt = Int32.Parse(productAmount);
                if (amountInt < 0)
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }

            using(VarastonhallintaDBContext database = new VarastonhallintaDBContext(address, user, password))
            {
                Tuote product = new Tuote();
                product.ID = idInt;
                product.Tuotenimi = productName;
                product.Hinta = priceInt;
                product.VarastoSaldo = amountInt;

                database.Tuote?.Add(product);

                try
                {
                    return database.SaveChanges() == 1;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }

        private static bool RemoveProduct(string address, string user, string password, string productName)
        {
            using (VarastonhallintaDBContext database = new VarastonhallintaDBContext(address, user, password))
            {
                Tuote? product = database.Tuote?.Where(product => product.Tuotenimi == productName).FirstOrDefault();
                if (product == null)
                    return false;

                database.Tuote?.Remove(product);

                try
                {
                    return database.SaveChanges() == 1;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }
    }
}
