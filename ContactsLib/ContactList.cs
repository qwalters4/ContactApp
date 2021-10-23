using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsLib
{
    public class ContactList
    {
        private ObservableCollection<Contact> m_Contacts;
        private string m_Folder;

        public ObservableCollection<Contact> Contacts { get { return m_Contacts; } }

        public ContactList()
        {
            m_Contacts = new ObservableCollection<Contact>();
            m_Folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            m_Folder = Path.Combine(m_Folder, "ContactApp/Contacts");
            Directory.CreateDirectory(Folder);
        }

        public string Folder
        {
            get
            { 
                return m_Folder; 
            }
            set
            { 
                m_Folder = value; 
            }
        }

        public void LoadTestData()
        {
            LoadTestData(40, 4);
        }

        public void LoadTestData(int numContacts, int seed)
        {
            string[] firstNames = new[]
            {
                "Quin",
                "Hemanth",
                "Yash",
                "Tien",
                "Adam",
                "Patrick",
                "Tom",
                "Jonathan",
                "Jing",
                "Erika",
                "Mamie",
                "Vincent",
                "Elizabeth",
                "Patricia"
            };

            string[] lastNames = new[]
            {
                "Walters",
                "Alapati",
                "Tiwari",
                "Wong",
                "Honts",
                "McCoy",
                "Bakken",
                "Tan",
                "Shang",
                "Stelljes",
                "Kester",
                "Rollins",
                "Maynard",
                "Kolasinski"
            };

            string[] areaCodes = new[]
            {
                "414",
                "262",
                "608",
                "920",
                "715"
            };

            string[] phoneTypes = new[]
            {
                "Home",
                "Mobile",
                "Work"
            };

            string[] emailTypes = new[]
            {
                "Personal",
                "Private",
                "Work"
            };

            string[] emailProviders = new[]
            {
                "gmail.com",
                "outlook.com",
                "uwm.edu",
                "hotmail.com",
                "ezmail.org"
            };

            string[] streets = new[]
            {
                "999 Hill Lane",
                "2870 Ketch Harbour St.",
                "2967 Acacia Dr.",
                "0751 East Thompson Street",
                "74 Jones St.",
                "2198 NW. King Street",
                "237 Mill St.",
                "1 Court Dr.",
                "245 Blackburn Court",
                "908 Glenholme St.",
                "789 Bridle Street",
                "123 Penn Rd.",
                "675 South Rockland Court",
                "678 Coffee Circle"
            };

            string[] cities = new[]
            {
                "Twp",
                "Chattanooga",
                "Clearwater",
                "Thomasville",
                "San Angelo",
                "Woonsocket",
                "Spring Valley",
                "Lakeville",
                "Maplewood",
                "Helotes",
                "Glen Ellyn",
                "Bonita Springs",
                "Mesa",
                "Paducah"
            };

            string[] states = new[]
            {
                "PA",
                "TN",
                "FL",
                "NC",
                "TX",
                "RI",
                "NY",
                "MN",
                "WI",
                "NJ",
                "TX",
                "IL",
                "FL",
                "AZ",
                "KY"
            };

            string[] postalCodes = new[]
            {
                "16066",
                "37421",
                "33756",
                "27360",
                "76901",
                "02895",
                "10977",
                "55044",
                "07040",
                "78023",
                "60137",
                "34135",
                "85203",
                "42001"
            };

            Dictionary<string, Contact> contacts = new Dictionary<string, Contact>();

            Random r = new Random(seed);

            while (contacts.Count < numContacts)
            {
                string firstName = firstNames[(int)Math.Floor(firstNames.Length * r.NextDouble())];
                string lastName = lastNames[(int)Math.Floor(lastNames.Length * r.NextDouble())];
                string fullName = lastName + firstName;
                string street = streets[(int)Math.Floor(streets.Length * r.NextDouble())];
                string city = cities[(int)Math.Floor(cities.Length * r.NextDouble())];
                string state = states[(int)Math.Floor(states.Length * r.NextDouble())];
                string postalcode = postalCodes[(int)Math.Floor(postalCodes.Length * r.NextDouble())];
                string country = "US";

                if (!contacts.TryGetValue(fullName, out Contact c))
                {
                    Address a = new Address()
                    {
                        Street = street,
                        City = city,
                        State = state,
                        PostalCode = postalcode,
                        Country = country
                    };
                    c = new Contact()
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };
                    c.Addresses.Add(a);
                    c.Company = "Test";
                    c.Photo = new Photo();
                    contacts.Add(fullName, c);

                    int numPhones = (int)Math.Ceiling(3.0 * r.NextDouble());
                    for (int i = 0; i < numPhones; ++i)
                    {
                        string areaCode = areaCodes[(int)Math.Floor(areaCodes.Length * r.NextDouble())];
                        string exchange = (200 + (int)(800 * r.NextDouble())).ToString();
                        string num = (1000 + (int)(9000 * r.NextDouble())).ToString();
                        string phoneNumber = "(" + areaCode + ")" + exchange + "-" + num;
                        string type = phoneTypes[(int)Math.Ceiling(2.0 * r.NextDouble())];
                        string ext = ((int)Math.Ceiling(9999.0 * r.NextDouble())).ToString();

                        Phone newPhone = new Phone()
                        {
                            Number = exchange + "-" + num,
                            Type = type,
                            AreaCode = areaCode,
                            Extension = ext
                        };
                        c.Phones.Add(newPhone);
                    }

                    int numEmails = (int)Math.Ceiling(3.0 * r.NextDouble());
                    for (int i = 0; i < numEmails; ++i)
                    {
                        string provider = emailProviders[(int)Math.Floor(emailProviders.Length * r.NextDouble())];
                        double pick = r.NextDouble();
                        string eName = null;
                        string type = emailTypes[(int)Math.Ceiling(2.0 * r.NextDouble())];

                        if (pick < 1.0 / 3.0)
                            eName = c.LastName.ToLower() + "." + c.FirstName.ToLower()[0];
                        else if (pick < 2.0 / 3.0)
                            eName = c.FirstName.ToLower()[0] + c.LastName.ToLower();
                        else
                            eName = c.FirstName.ToLower() + "." + c.LastName.ToLower();

                        string emailAddress = eName + "@" + provider;
                        Email newEmail = new Email()
                        {
                            Address = emailAddress,
                            Type = type
                        };
                        c.Emails.Add(newEmail);
                    }
                }
            }

            Contacts.Clear();
            foreach (Contact contact in contacts.Values
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                )
            {
                Contacts.Add(contact);
            }
        }

        public void Print()
        {
            foreach(Contact c in m_Contacts)
            {
                Console.WriteLine("Name: {0}, {1}", c.LastName, c.FirstName);
                Console.WriteLine("Company: {0}", c.Company);
                foreach(Phone p in c.Phones)
                {
                    Console.WriteLine("{0} phone number: {1} {2} {3}", p.Type, p.Extension, p.AreaCode, p.Number);
                }
                foreach(Email e in c.Emails)
                {
                    Console.WriteLine("{0} email: {1}", e.Type, e.Address);
                }
                foreach(Address a in c.Addresses)
                {
                    Console.WriteLine("Address: {0} {1}, {2}, {3}, {4}", a.Street, a.City, a.State, a.PostalCode, a.Country);
                }
                Console.WriteLine("PHOTO PLACEHOLDER\n");
            }
        }

        public void Export(string filename)
        {
            while (filename == null)
            {
                Console.Write("Enter a filename(or type 'cancel'): ");
                filename = Console.ReadLine();
            }

            if(filename == "cancel")
                    return;

            string jsonFile = Path.Combine(Folder, filename);
            using (StreamWriter sw = new StreamWriter(jsonFile))
            {
                sw.Write(JsonConvert.SerializeObject(m_Contacts));
            }
            Console.WriteLine(jsonFile);
        }

        public static void Export(string fileName, ObservableCollection<Contact> contacts)
        {
            ////TODO check if file existsand overwrite or make new name
            //string jsonFile = Path.Combine(folderName, "MyContacts.json");
            //using (StreamWriter sw = new StreamWriter(jsonFile))
            //{
            //    sw.Write(JsonConvert.SerializeObject(contacts));
            //}
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ContactApp\\Contacts");
            Directory.CreateDirectory(folder);

            string jsonFile = Path.Combine(folder, fileName);
            using (StreamWriter sw = new StreamWriter(jsonFile))
            {
                sw.Write(JsonConvert.SerializeObject(contacts));
            }
        }

        public void Import(string filename)
        {
            while(filename == null || !File.Exists(Path.Combine(Folder, filename)))
            {
                Console.Write("Enter a filename(or type 'cancel'): ");
                filename = Console.ReadLine();
                if(filename == "cancel")
                {
                    return;
                }
            }

            string jsonFile = Path.Combine(Folder, filename);
            if(!File.Exists(jsonFile))
            {
                Console.WriteLine("File does not exist.");
                return;
            }
            string jsonString = File.ReadAllText(jsonFile);
            m_Contacts = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(jsonString);
        }

        public static void Import(string fileName, ObservableCollection<Contact> contacts)
        {
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ContactApp\\Contacts");
            Directory.CreateDirectory(folder);

            string jsonFile = Path.Combine(folder, fileName);
            if (!File.Exists(jsonFile))
            {
                Console.WriteLine("File does not exist.");
                return;
            }
            string jsonString = File.ReadAllText(jsonFile);
            ObservableCollection<Contact> temp = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(jsonString);
            contacts.Clear();
            foreach(Contact c in temp)
            {
                contacts.Add(c);
            }
        }
    }
}
