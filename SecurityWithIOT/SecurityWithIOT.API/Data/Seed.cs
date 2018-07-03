using System.Collections.Generic;
using Newtonsoft.Json;
using SecurityWithIOT.API.Model;

namespace SecurityWithIOT.API.Data
{
    public class Seed
    {
        public DataContext _context { get; }

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers() {

            _context.Users.RemoveRange(_context.Users);
            _context.SaveChanges();

            var userData = System.IO.File.ReadAllText("Data/SeedFiles/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();
                _context.Users.Add(user);
            }
            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                        passwordSalt = hmac.Key;
                        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                }
        }

        public  void SeedCountry() {

             _context.Countries.RemoveRange(_context.Countries);
            _context.SaveChanges();

            var countryData = System.IO.File.ReadAllText("Data/SeedFiles/CountrySeedData.json");

            var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);

            _context.Countries.AddRange(countries);
            _context.SaveChanges();

        }

        public void SeedCity() {

             _context.Cities.RemoveRange(_context.Cities);
            _context.SaveChanges();

            var cityData = System.IO.File.ReadAllText("Data/SeedFiles/CitySeedData.json");

            var countries = JsonConvert.DeserializeObject<List<City>>(cityData);

            _context.Cities.AddRange(countries);
            _context.SaveChanges();
        }
    }
}