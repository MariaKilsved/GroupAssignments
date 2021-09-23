using System;

namespace GroupAssignment1
{
    public enum GrapeVariants
    {
        Null, CabernetSauvignon, PinotNoir, Corvina, Shiraz, Merlot, Chablis,
        Riesling, Tempranillo
    }
    public enum GrapeRegions
    {
        Null, Bordeaux, Burgundy, Veneto, Piedmonte, RiberaDelDuero,
        NapaValley, Puglia, Pfalz
    }
    public struct Wine
    {
        public int? Year;                   // null = undefined
        public string Name;
        public GrapeVariants Grape;
        public GrapeRegions Region;

        /// <summary>
        /// Creates a string representing the content of the Wine struct
        /// </summary>
        /// <returns>string that can be printed out using Console.WriteLine</returns>
        public string StringToPrint()
        {
            return $"Wine {Year} {Name} is made of {Grape} from {Region}";   
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            const int maxNrBottles = 4;
            Wine[] myCellar = new Wine[maxNrBottles];

            Console.WriteLine($"My cellar can have maximum {maxNrBottles} bottles");

            // Creating all wines and adding them by using the InsertWine method
            Wine wine1 = new Wine { Year = 2000, Name = "Château Lafite Rothschild", Grape = GrapeVariants.CabernetSauvignon, Region = GrapeRegions.Bordeaux };
            bool bOK = InsertWine(myCellar, wine1);

            Wine wine2 = new Wine { Year = 2010, Name = "Domaine de la Romanée-Conti", Grape = GrapeVariants.PinotNoir, Region = GrapeRegions.Burgundy };
            bOK = InsertWine(myCellar, wine2);

            Wine wine3 = new Wine { Year = 2005, Name = "Giuseppe Quintarelli Amarone", Grape = GrapeVariants.Corvina, Region = GrapeRegions.Veneto };
            bOK = InsertWine(myCellar, wine3);

            Wine wine4 = new Wine { Year = 2008, Name = "Sierra Cantabria", Grape = GrapeVariants.Tempranillo, Region = GrapeRegions.RiberaDelDuero };
            //bOK = InsertWine(myCellar, wine3);
            bOK = InsertWine(myCellar, wine4);

            Wine wine5 = new Wine { Year = 1992, Name = "Screaming Eagle", Grape = GrapeVariants.CabernetSauvignon, Region = GrapeRegions.RiberaDelDuero };
            //bOK = InsertWine(myCellar, wine3);
            bOK = InsertWine(myCellar, wine5);

            PrintWines(myCellar);

            // Search for wine
            if(SearchForAString(myCellar, out string word) == true)
            {
                Console.WriteLine($"Wine {word} found.");
            }
            else
            {
                Console.WriteLine("Wine not found.");
            }

            //Delete a wine
            Console.WriteLine();
            Console.WriteLine("Please delete a wine:");
            string wineToBeDeleted = Console.ReadLine();

            Console.WriteLine();
            bool delete = DeleteWine(myCellar, wineToBeDeleted, out string confirmationString);
            Console.WriteLine(confirmationString);

            PrintWines(myCellar);

        }

        /// <summary>
        /// Inserts a wine into myCellar at first available position
        /// </summary>
        /// <param name="myCellar"></param>
        /// <param name="wine"></param>
        /// <returns>true - if insertion was possible, otherwise false</returns>
        private static bool InsertWine(Wine[] myCellar, Wine wine)
        {
            for (int i = 0; i < myCellar.Length; i++)
            {
                if (myCellar[i].Name == null)
                {
                    myCellar[i] = wine;
                    Console.WriteLine($"Added to my cellar: {myCellar[i].StringToPrint()}");
                    return true;
                }
            }
            Console.WriteLine($"Could NOT add to my cellar: {wine.StringToPrint()}");
            return false;
        }

        /// <summary>
        /// Print out all the wines in myCellar
        /// </summary>
        /// <param name="myCellar"></param>
        private static void PrintWines(Wine[] myCellar)
        {
            Console.WriteLine();
            int nrOfBottles = NrOfBottles(myCellar);

            Console.WriteLine($"My cellar has {nrOfBottles} wines:");
            for (int i = 0; i < myCellar.Length; i++)
            {
                if (myCellar[i].Name != null)
                {
                    Console.WriteLine(myCellar[i].StringToPrint());
                }

            }
        }

        /// <summary>
        /// Counts the number of bottles in myCellar. That is all items
        /// where Year is not null 
        /// </summary>
        /// <param name="myCellar"></param>
        /// <returns>Number of bottles in myCellar</returns>
        private static int NrOfBottles(Wine[] myCellar)
        {
            int nrBottles = 0;
            for (int i = 0; i < myCellar.Length; i++)
            {
                if (myCellar[i].Year != null && myCellar[i].Year != 9999)
                {
                    nrBottles++;
                }
            }
            return nrBottles;
        }

        /// <summary>
        /// Search for the first wine containing a certain string
        /// </summary>
        /// <param name="myCellar"></param>
        /// <returns>Bool for if the string was found</returns>
        private static bool SearchForAString(Wine[] myCellar, out string word)
        {

            Console.WriteLine();
            Console.WriteLine("Please input your search word:");
            word = Console.ReadLine();

            for (int i = 0; i < myCellar.Length; i++)
            {
                if (myCellar[i].Name != null && myCellar[i].Name.Contains(word))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool DeleteWine(Wine[] myCellar, string name, out string confirmationString)
        {
            for (int i = 0; i < myCellar.Length; i++)
            {
                if (myCellar[i].Name != null && myCellar[i].Name == name)
                {
                    confirmationString = $"Wine {myCellar[i].Year} {myCellar[i].Name} made of {myCellar[i].Grape} from {myCellar[i].Region} was deleted!"; ;

                    myCellar[i].Year = 9999;
                    myCellar[i].Name = null;
                    myCellar[i].Grape = GrapeVariants.Null;
                    myCellar[i].Region = GrapeRegions.Null;
                    return true;
                }
            }
            confirmationString = $"Unable to delete {name}. {name} not found.";
            return false;
        }

    }
}
