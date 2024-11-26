using System.Xml.Serialization;

namespace doga
{
    internal class Program
    {
        static string[] raktarTermekek = new string[10] { "Alma", "Körte", "Barack", "Banán", "Ásványvíz", "Kenyér", "Kertibútor", "Csoki", "McNuggets", "Krumpli" };
        static int[] raktarMennyisegek = new int[10] { 523, 523, 131, 542, 123, 523, 2423, 13, 542, 312 };
        static int[] termekArak = new int[10] { 50, 100, 50, 50, 500, 60, 50000, 600, 1000, 80 };

        static List<string> kosarTartalom = new List<string>();
        static List<int> mennyisegek = new List<int>();
        static List<int> arak = new List<int>();
        static void Main(string[] args)
        {
            bool fut = true;
            while (fut)
            {
                Console.WriteLine("Áruház rendszer");
                Console.WriteLine("1. Raktár áttekintése");
                Console.WriteLine("2. Termék hozzáadása a kosárhoz");
                Console.WriteLine("3. Kosár megtekintése");
                Console.WriteLine("4. Termék törlése a kosárból");
                Console.WriteLine("5. Kosár törlése");
                Console.WriteLine("6. Bevásárlás szimuláció");
                Console.WriteLine("7. Legdrágább termék");
                Console.WriteLine("8. Legolcsóbb termék");
                Console.WriteLine("9. Kosárstatisztika");
                Console.WriteLine("10. Raktárfigyelmeztető");
                Console.WriteLine("11. Kosár érték");
                Console.WriteLine("12. Termék felvétele a raktárba");
                Console.WriteLine("13. Termékek rendezése a raktárban");
                Console.WriteLine("14. Kilépés");
                Console.Write("Válassz egy opciót: ");

                int opcio = Convert.ToInt32(Console.ReadLine());

                switch (opcio)
                {
                    case 1:
                        RaktarMegtekintese();
                        break;
                    case 2:
                        TermekKosar();
                        break;
                    case 3:
                        KosarMegtekintese();
                        break;
                    case 4:
                        TermekTorles();
                        break;
                    case 5:
                        KosarTorlese();
                        break;
                    case 6:
                        Vasarlas();
                        break;
                    case 7:
                        Legdragabb();
                        break;
                    case 8:
                        Legolcsobb();
                        break;
                    case 9:
                        KosarStatisztika();
                        break;
                    case 10:
                        RaktarFigyelmezteto();
                        break;
                    case 11:
                        KosarErtek();
                        break;
                    case 12:
                        FelvetelRaktarba();
                        break;
                    case 13:
                        TermekRendezes();
                        break;
                    case 14:
                        Console.WriteLine("Kilépés...");
                        fut = false;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen opció!");
                        break;
                }
            }
        }
        static void RaktarMegtekintese()
        {
            Console.WriteLine("-------------------------------------------");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{i + 1}. termék a raktárban a(z) {raktarTermekek[i]}, darabja {raktarMennyisegek[i]} és ára {termekArak[i]}Ft");
            }
            Console.WriteLine("------------------------------------------");
        }
        static void TermekKosar()
        {
            int ar = 0;
            Console.Write("Add meg a termék nevét: ");
            string termek = Console.ReadLine();
            if (termek == null || termek == "")
            {
                Console.WriteLine("A termék neve nem lehet üres!");
                return;
            }
            if (Array.IndexOf(raktarTermekek, termek) == -1)
            {
                Console.WriteLine("A termék nincs a raktárban.");
                return;
            }
            else
            {
                int v = termekArak[Array.IndexOf(raktarTermekek, termek)];
                ar += v;
            }

            Console.Write("Add meg a mennyiséget: ");
            string mennyiseg = Console.ReadLine();

            if (mennyiseg == "")
            {
                Console.WriteLine("A mennyiség helye nem lehet üres.");
                return;
            }
            int mennyisegSzam;
            try
            {
                mennyisegSzam = Convert.ToInt32(mennyiseg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kérem számot írjon be!");
                return;
            }
            ar = ar * mennyisegSzam;

            if (raktarMennyisegek[Array.IndexOf(raktarTermekek, termek)] < mennyisegSzam)
            {
                Console.WriteLine("Nincs elég a termékből a raktárban.");
                return;
            }

            kosarTartalom.Add(termek);
            mennyisegek.Add(mennyisegSzam);
            arak.Add(ar);
            Console.WriteLine("Termék hozzáadva a kosárhoz!");

            int index = Array.IndexOf(raktarTermekek, termek);
            raktarMennyisegek[index] -= mennyisegSzam;
        }
        static void KosarMegtekintese()
        {
            Console.WriteLine("Kosár tartalma:");
            for (int i = 0; i <= kosarTartalom.Count - 1; i++)
            {
                Console.WriteLine($"- {kosarTartalom[i]}: {mennyisegek[i]} db, {arak[i]}Ft");
            }
        }
        static void TermekTorles()
        {
            Console.Write("Add meg a törölni kívánt termék nevét: ");
            string termek = Console.ReadLine();

            if (!kosarTartalom.Contains(termek))
            {
                Console.WriteLine("Ez a termék nincs a kosárban!");
            }
            else
            {
                int index = kosarTartalom.IndexOf(termek);
                kosarTartalom.RemoveAt(index);
                mennyisegek.RemoveAt(index);
                arak.RemoveAt(index);
                Console.WriteLine("Termék eltávolítva a kosárból!");
            }
        }
        static void KosarTorlese()
        {
            Console.WriteLine("Kosár törlése...");
            kosarTartalom.Clear();
            mennyisegek.Clear();
            arak.Clear();
            Console.WriteLine("Termékek eltávolítva a kosárból!");
        }
        static void Vasarlas()
        {
            Console.WriteLine("Bevásárlás folyamatban...");
            for (int i = 0; i < kosarTartalom.Count; i++)
            {
                string termek = kosarTartalom[i];
                int mennyiseg = mennyisegek[i];
                int ar = arak[i];

                int index = Array.IndexOf(raktarTermekek, termek);
                if (index == -1)
                {
                    Console.WriteLine($"Nincs a raktárban: {termek}");
                    continue;
                }

                if (raktarMennyisegek[index] < mennyiseg)
                {
                    Console.WriteLine($"Nincs elég {termek} a raktárban!");
                }
                else
                {
                    Console.WriteLine($"Sikeresen megvásárolt: {termek}, {mennyiseg} db, {ar}Ft.");
                }
            }
            kosarTartalom.Clear();
            mennyisegek.Clear();
            arak.Clear();
        }
        static void Legdragabb()
        {
            int legdragabb = termekArak.Max();
            int index = Array.IndexOf(termekArak, legdragabb);
            Console.WriteLine($"A legdrágább termék a(z) {raktarTermekek[index]}, {termekArak[index]}Ft");
        }
        static void Legolcsobb()
        {
            int legolcsobb = termekArak.Min();
            int index = Array.IndexOf(termekArak, legolcsobb);
            Console.WriteLine($"A legdrágább termék a(z) {raktarTermekek[index]}, {termekArak[index]}Ft");
        }
        static void KosarStatisztika()
        {
            int osszes = mennyisegek.Sum();
            int kulonbozo = kosarTartalom.Count();
            Console.WriteLine($"Termékek száma a kosárban: {osszes}");
            Console.WriteLine($"Különböző termékek száma: {kulonbozo}");
        }
        static void RaktarFigyelmezteto()
        {
            for (int i = 0; i < raktarMennyisegek.Length; i++)
            {
                if (raktarMennyisegek[i] <= 5)
                {
                    Console.WriteLine($"{raktarTermekek[i]}-ból kevesebb van, mint 5.");
                }
            }
        }
        static void KosarErtek()
        {
            Console.WriteLine($"A kosár értéke {arak.Sum()}Ft.");
        }
        static void FelvetelRaktarba()
        {
            Console.Write("Add meg a termék nevét: ");
            string termek = Console.ReadLine();

            int index = Array.IndexOf(raktarTermekek, termek);
            if (index == -1)
            {
                Console.WriteLine("Nincs ilyen termék a raktárban.");
                return;
            }
            raktarTermekek[index] = termek;
            raktarMennyisegek[index] = 0;

            Console.Write("Add meg a frissítendő mennyiséget: ");
            int mennyiseg = Convert.ToInt32(Console.ReadLine());

            if (mennyiseg < 0)
            {
                Console.WriteLine("Hiba: negatív mennyiséget nem adhatsz hozzá!");
                return;
            }

            raktarMennyisegek[index] += mennyiseg;
            Console.WriteLine("A raktárkészlet frissítve!");
        }
        static void TermekRendezes()
        {
            Array.Sort(termekArak);
            for (int i = 0; i < termekArak.Length; i++)
            {
                int index = Array.IndexOf(raktarTermekek, termekArak[i]);
                Console.WriteLine($"{i + 1}. {raktarTermekek[i]}, {termekArak[i]}.");
            }
        }
    }
}
