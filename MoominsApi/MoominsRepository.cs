namespace MoominsApi.Repo
{
    /// <summary>
    /// Luokka muumien hallinnointiin
    /// </summary>
    public class MoominsRepository
    {
        private List<Moomin> moomins;

        public MoominsRepository()
        {
            // Alustetaan moomit
            moomins = new List<Moomin>();
            moomins.Add(new Moomin { Number = 1, Name = "Muumipeikko" });
            moomins.Add(new Moomin { Number = 2, Name = "Muumimamma" });
            moomins.Add(new Moomin { Number = 3, Name = "Muumipappa" });
            moomins.Add(new Moomin { Number = 4, Name = "Nuuskamuikkunen" });
            moomins.Add(new Moomin { Number = 5, Name = "Pikku Myy" });
            moomins.Add(new Moomin { Number = 6, Name = "Nipsu" });
            moomins.Add(new Moomin { Number = 7, Name = "Niiskuneiti" });
            moomins.Add(new Moomin { Number = 8, Name = "Niisku" });
            moomins.Add(new Moomin { Number = 9, Name = "Mymmeli" });
            moomins.Add(new Moomin { Number = 10, Name = "Hattivatti" });
            moomins.Add(new Moomin { Number = 11, Name = "Hemuli" });
        }

        /// <summary>
        /// Palauttaa kaikki muumit
        /// </summary>
        /// <returns></returns>
        public List<Moomin> GetAllMoomins()
        {
            return moomins;
        }

        /// <summary>
        /// Palauttaa kaikki nimi hakukriteeriin osuvat muumit
        /// </summary>
        /// <param name="name">Haettava muumin nimi tai osa siitä esim muumi tai muumip</param>
        /// <returns></returns>
        public List<Moomin> GetAllMoomins(string name)
        {
            var moominsFound = moomins.Where(moomin => moomin.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)).ToList();

            return moominsFound;
        }

        /// <summary>
        /// Hakee yhden muumin tiedot numerolla
        /// </summary>
        /// <param name="number">Haettavan muumin numero</param>
        /// <returns>
        /// Löydetty muumi tai null jos muumia ei löydy
        /// </returns>
        public Moomin? GetMoomin(int number)
        {
            var selectedMoomin = moomins.Where(moomin => moomin.Number == number).FirstOrDefault();

            return selectedMoomin;
        }

        /// <summary>
        /// Lisää uuden muumin
        /// </summary>
        /// <param name="moomin">Lisättävän muumin tiedot</param>
        /// <returns>
        /// True, jos kaikki ok
        /// False, jos numerolla on jo olemassa muumi
        /// </returns>
        public bool AddMoomin(Moomin moomin)
        {
            var existsMomin = moomins.Where(_ => _.Number == moomin.Number).FirstOrDefault();

            if (existsMomin == null)
            {
                moomins.Add(moomin);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Poistaa muumin listasta
        /// </summary>
        /// <param name="number">Poistettavan muumin numero</param>
        /// <returns>
        /// True, jos muumi löytyy ja poistaminen onnistuu
        /// False, jos numerolla ei löydy muumia
        /// </returns>
        public bool DeleteMoomin(int number)
        {
            Moomin delete = null;

            foreach (var p in moomins)
            {
                if (p.Number == number)
                {
                    delete = p;
                    break;
                }
            }
            if (delete != null)
            {
                moomins.Remove(delete);
                return true;
            }

            return false;

        }

    }


}