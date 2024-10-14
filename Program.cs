using System.Numerics;
using System.Reflection.Metadata;

namespace OE_PMP_GYAK_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. feladat
            #region
            Console.WriteLine("1. feladat");
            Book b = new Book("The Hobbit - or There and Back Again", "J.R.R. Tolkien", 1937, 312);
            Console.WriteLine(b.AllData());
            Console.ReadLine();
            #endregion

            // 3. feladat
            #region
            Console.WriteLine("\n3. feladat");

            Runner runner1 = new Runner("Milán", 0, 5.0);
            Runner runner2 = new Runner("Ákos", 1, 4.5);

            int finalDistance = 50;

            while (runner1.GetDistance() < finalDistance && runner2.GetDistance() < finalDistance)
            {
                runner1.RefreshDistance(1);
                runner2.RefreshDistance(1);

                Console.Clear();

                runner1.Show();
                runner2.Show();

                System.Threading.Thread.Sleep(1000);
            }

            if (runner1.GetDistance() >= finalDistance)
            {
                Console.WriteLine($"\n{runner1.GetName()} nyerte a versenyt!");
            }
            else if (runner2.GetDistance() >= finalDistance)
            {
                Console.WriteLine($"\n{runner2.GetName()} nyerte a versenyt!");
            }
            #endregion

            // 4. feladat
            #region
            Console.WriteLine("\n4. feladat");

            SecretMessageMaker scm = new SecretMessageMaker(2);
            string originalText =
                "Az Óbudai Egyetem a V4 régió legjobb kutatás minősége (Reseach Quality) mutatóval rendelkező műszaki egyeteme a THE legújabb 2024-es ranglistáján";
            string encodedText = scm.Encode(originalText);
            string decodedText = scm.Decode(encodedText);
            bool isEncodingWorking = decodedText.CompareTo(originalText) == 0;

            Console.WriteLine($"Bemenet:\n{originalText}");
            Console.WriteLine();
            Console.WriteLine($"Kódolt szöveg: \n{encodedText}");
            Console.WriteLine();
            Console.WriteLine($"Visszafejtett üzenet: \n{decodedText}");
            Console.WriteLine(
                $"A két üzenet {(isEncodingWorking ? "megegyezik. (SIKERES)" : "eltérő. (SIKERTELEN)")}."
            );

            #endregion

            // 5. feladat
            #region
            List<string> fileContent = new List<string>(File.ReadAllLines("NHANES_1999-2018.csv"));
            List<Survey> surveys = new List<Survey>();

            fileContent.RemoveAt(0);

            foreach (string line in fileContent)
            {
                surveys.Add(new Survey(line));
            }

            // 5/1
            string choosenPeriod = surveys[0].Period;

            List<Survey> filteredSurvey = new List<Survey>();

            foreach (Survey s in surveys)
            {
                if (s.Period.CompareTo(choosenPeriod) == 0)
                {
                    filteredSurvey.Add(s);
                }
            }

            int numOfMales = 0;
            int numofFemales = 0;
            double sumOfMaleBMI = 0.0;
            double sumOfFemaleBMI = 0.0;

            foreach (Survey s in filteredSurvey)
            {
                if (s.isMale())
                {
                    numOfMales++;
                    sumOfMaleBMI += s.Bmxbmi;
                }
                else
                {
                    numofFemales++;
                    sumOfFemaleBMI += s.Bmxbmi;
                }
            }

            double avgMaleBMI = sumOfMaleBMI / numOfMales;
            double avgFemaleBMI = sumOfFemaleBMI / numofFemales;

            Console.WriteLine("\n5/1. feladat");
            Console.WriteLine(
                $"A {choosenPeriod} időszakban a férfiak átlagos BMI-je: {avgMaleBMI}, a hölgyek átlagos BMI-je: {avgFemaleBMI}."
            );

            // 5/2

            int numOfFatPeople = 0;
            double sumOfFatBloodSugar = 0.0;

            foreach (Survey s in filteredSurvey)
            {
                if (s.isFat())
                {
                    numOfFatPeople++;
                    sumOfFatBloodSugar += s.Lbdglusi;
                }
            }

            double percentageOfFatPeople = (double)numOfFatPeople / filteredSurvey.Count * 100;
            double roundedPercOfFatPeople = Math.Round(percentageOfFatPeople, 2);

            Console.WriteLine("\n5/2. feladat");
            Console.WriteLine(
                $"A {choosenPeriod} időszakban 5.6-nál magasabb vércukorszint százaléka: {roundedPercOfFatPeople} %."
            );

            // 5/3
            Console.WriteLine("\n5/3. feladat");
            double maxBMI = 0;
            Survey queriedSurvey = surveys[0];

            foreach (Survey s in surveys)
            {
                if (s.Bmxbmi > maxBMI)
                {
                    maxBMI = s.Bmxbmi;
                    queriedSurvey = s;
                }
            }

            Console.WriteLine(
                $"Egy max BMI-s ({maxBMI}) alany vércukorszintje: {queriedSurvey.Lbdglusi}."
            );

            // 5/4
            Console.WriteLine("\n5/4. feladat");

            int numberOfFatPeople = 0;
            int sumOfAge = 0;

            foreach (Survey s in surveys)
            {
                if (s.isHighBMI())
                {
                    numberOfFatPeople++;
                    sumOfAge += (int)s.Ridageyr;
                }
            }

            double avgAge = (double)sumOfAge / numberOfFatPeople;
            double roundedAvgAge = Math.Round(avgAge, 2);

            Console.WriteLine(
                $"A teljes adathalmazban a túlsúlyosak átlagéletkora: {roundedAvgAge}."
            );

            #endregion
        }
    }

    // 1. feladat
    class Book
    {
        // tagváltozók (adatmezők)
        string title = string.Empty;
        string author = string.Empty;
        int yearOfPublish = 0;
        int numOfPages = 0;

        // konstruktor
        public Book(string iTitle, string iAuthor, int iYearOfPublish, int iNumOfPages)
        {
            title = iTitle;
            author = iAuthor;
            yearOfPublish = iYearOfPublish;
            numOfPages = iNumOfPages;
        }

        // metódusok
        public string AllData()
        {
            return $"{author}: {title}, {yearOfPublish} ({numOfPages} pages)";
        }
    }

    // 3. feladat
    class Runner
    {
        // tagváltozók (adatmezők)
        string name = string.Empty;
        int serialNumber = 0;
        double speed = 0.0;
        double distanceFromStartLine = 0.0;

        // konstruktor
        public Runner(string iName, int iSerialNumber, double iSpeed)
        {
            name = iName;
            serialNumber = iSerialNumber;
            speed = iSpeed;
            distanceFromStartLine = 0;
        }

        // metódusok
        public void RefreshDistance(int s)
        {
            distanceFromStartLine += s * speed;
        }

        public void Show()
        {
            int column = (int)distanceFromStartLine;

            if (column > Console.WindowWidth)
            {
                column = Console.WindowWidth - 1;
            }

            Console.SetCursorPosition(column, serialNumber);
            Console.Write(name[0]);
        }

        public double GetDistance()
        {
            return distanceFromStartLine;
        }

        public string GetName()
        {
            return name;
        }
    }

    // 4. feladat
    class SecretMessageMaker
    {
        // adatmezők
        int key = 0;

        // konstruktor
        public SecretMessageMaker(int ikey)
        {
            key = ikey;
        }

        // metódusok
        private string TransformMessage(string text, int key)
        {
            string transformedText = string.Empty;
            List<char> karakterLista = new List<char>
            {
                // Kisbetűk (a-z)
                'a',
                'b',
                'c',
                'd',
                'e',
                'f',
                'g',
                'h',
                'i',
                'j',
                'k',
                'l',
                'm',
                'n',
                'o',
                'p',
                'q',
                'r',
                's',
                't',
                'u',
                'v',
                'w',
                'x',
                'y',
                'z',
                // Nagybetűk (A-Z)
                'A',
                'B',
                'C',
                'D',
                'E',
                'F',
                'G',
                'H',
                'I',
                'J',
                'K',
                'L',
                'M',
                'N',
                'O',
                'P',
                'Q',
                'R',
                'S',
                'T',
                'U',
                'V',
                'W',
                'X',
                'Y',
                'Z',
                // Számok (0-9)
                '0',
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                // Speciális írásjelek
                '!',
                '@',
                '#',
                '$',
                '%',
                '^',
                '&',
                '*',
                '(',
                ')',
                '-',
                '_',
                '=',
                '+',
                '[',
                ']',
                '{',
                '}',
                ';',
                ':',
                '\'',
                '"',
                ',',
                '.',
                '/',
                '?',
                '<',
                '>',
                '\\',
                '|',
                // Magyar ékezetes betűk
                'á',
                'é',
                'í',
                'ó',
                'ö',
                'ő',
                'ú',
                'ü',
                'ű',
                'Á',
                'É',
                'Í',
                'Ó',
                'Ö',
                'Ő',
                'Ú',
                'Ü',
                'Ű',
                ' ',
            };

            foreach (char c in text)
            {
                int newIdx = (
                    (karakterLista.IndexOf(c) + key + karakterLista.Count) % karakterLista.Count
                );

                transformedText += karakterLista.ElementAt(newIdx);
            }

            return transformedText;
        }

        public string Decode(string text)
        {
            string decodedText = TransformMessage(text, -key);
            return decodedText;
        }

        public string Encode(string text)
        {
            string encodedText = TransformMessage(text, key);
            return encodedText;
        }
    }

    // 5. feladat
    class Survey
    {
        // adatmezők
        private int _seqn;
        private string _period = string.Empty;
        private double _riagendr;
        private double _ridageyr;
        private double _bmxbmi;
        private double _lbdglusi;

        // konstruktor
        public Survey(string line)
        {
            string[] details = line.Split(',');
            Seqn = int.Parse(details[0]);
            Period = details[1];
            Riagendr = double.Parse(details[2].Replace('.', ','));
            Ridageyr = double.Parse(details[3].Replace('.', ','));
            Bmxbmi = double.Parse(details[4].Replace('.', ','));
            Lbdglusi = double.Parse(details[5].Replace('.', ','));
        }

        // metódusok
        public bool isMale()
        {
            return Riagendr == 1;
        }

        public bool isFat()
        {
            return Lbdglusi > 5.6;
        }

        public bool isHighBMI()
        {
            return Bmxbmi > 30.0;
        }

        // getterek, setterek
        public int Seqn
        {
            get => _seqn;
            set => _seqn = value;
        }
        public string Period
        {
            get => _period;
            set => _period = value;
        }
        public double Riagendr
        {
            get => _riagendr;
            set => _riagendr = value;
        }
        public double Ridageyr
        {
            get => _ridageyr;
            set => _ridageyr = value;
        }
        public double Bmxbmi
        {
            get => _bmxbmi;
            set => _bmxbmi = value;
        }
        public double Lbdglusi
        {
            get => _lbdglusi;
            set => _lbdglusi = value;
        }
    }
}
