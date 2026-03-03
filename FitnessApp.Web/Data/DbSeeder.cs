using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Web.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
    {
        var userManager = service.GetRequiredService<UserManager<AppUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        var context = service.GetRequiredService<ApplicationDbContext>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }
        
        if (!await roleManager.RoleExistsAsync("Member"))
        {
            await roleManager.CreateAsync(new IdentityRole("Member"));
        }

        var adminEmail = "admin@saufitness.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Admin User",
                EmailConfirmed = true,
                MembershipDate = DateTime.Now
            };

            var result = await userManager.CreateAsync(adminUser, "Admin1234"); 
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        if (!await context.Gyms.AnyAsync())
        {
            var gyms = new List<Gym>
            {
                new Gym
                {
                    Name = "SAU Fight Academy",
                    Address = "Sakarya Üniversitesi Kampüsü, Spor Kompleksi",
                    Phone = "0264 295 60 00",
                    OpeningHours = "07:00 - 23:00"
                },
                new Gym
                {
                    Name = "Champions MMA Arena",
                    Address = "Serdivan, Spor Caddesi No:42",
                    Phone = "0264 123 45 67",
                    OpeningHours = "08:00 - 22:00"
                },
                new Gym
                {
                    Name = "Elite Combat Center",
                    Address = "Adapazarı Merkez, Atatürk Bulvarı No:156",
                    Phone = "0264 987 65 43",
                    OpeningHours = "06:00 - 24:00"
                }
            };
            context.Gyms.AddRange(gyms);
            await context.SaveChangesAsync();
        }

        if (!await context.Services.AnyAsync())
        {
            var services = new List<Service>
            {
                new Service { Name = "Boks", Description = "Klasik boks teknikleri, yumruk kombinasyonları ve savunma pozisyonları", DurationMinutes = 60, Price = 300 },
                new Service { Name = "Muay Thai", Description = "Tayland boksu, dirsek ve diz teknikleri, clinch çalışmaları", DurationMinutes = 60, Price = 350 },
                new Service { Name = "Brazilian Jiu-Jitsu", Description = "Zemin güreşi, boğma ve kilit teknikleri, guard çalışmaları", DurationMinutes = 90, Price = 400 },
                new Service { Name = "Güreş", Description = "Serbest ve grekoromen güreş teknikleri, takedown ve kontrol", DurationMinutes = 60, Price = 300 },
                new Service { Name = "Sambo", Description = "Rus dövüş sanatı, fırlatma teknikleri ve bacak kilitleri", DurationMinutes = 75, Price = 350 },
                new Service { Name = "Kickboks", Description = "Yumruk ve tekme kombinasyonları, low kick ve high kick çalışmaları", DurationMinutes = 60, Price = 320 },
                new Service { Name = "MMA", Description = "Karma dövüş sanatları, tüm disiplinlerin birleşimi, kafes çalışmaları", DurationMinutes = 90, Price = 450 }
            };
            context.Services.AddRange(services);
            await context.SaveChangesAsync();
        }

        if (!await context.Trainers.AnyAsync())
        {
            var boks = await context.Services.FirstAsync(s => s.Name == "Boks");
            var muayThai = await context.Services.FirstAsync(s => s.Name == "Muay Thai");
            var bjj = await context.Services.FirstAsync(s => s.Name == "Brazilian Jiu-Jitsu");
            var gures = await context.Services.FirstAsync(s => s.Name == "Güreş");
            var sambo = await context.Services.FirstAsync(s => s.Name == "Sambo");
            var kickboks = await context.Services.FirstAsync(s => s.Name == "Kickboks");
            var mma = await context.Services.FirstAsync(s => s.Name == "MMA");

            var trainers = new List<Trainer>
            {
                new Trainer
                {
                    FullName = "Khabib Nurmagomedov",
                    Bio = "Dağıstan'ın gururu, 29-0 yenilmez rekor. UFC Hafif Siklet Şampiyonu. Sambo ve güreş ustası.",
                    PhotoUrl = "https://ui-avatars.com/api/?name=Khabib+Nurmagomedov&size=300&background=dc3545&color=fff&bold=true",
                    Specialties = new List<Service> { sambo, gures }
                },
                new Trainer
                {
                    FullName = "Conor McGregor",
                    Bio = "İrlanda'nın 'Notorious' lakaplı efsanesi. UFC'nin ilk çift şampiyonu. Güçlü sol kroşesi ile ünlü.",
                    PhotoUrl = "https://ui-avatars.com/api/?name=Conor+McGregor&size=300&background=28a745&color=fff&bold=true",
                    Specialties = new List<Service> { boks, kickboks }
                },
                new Trainer
                {
                    FullName = "Israel Adesanya",
                    Bio = "'The Last Stylebender' lakaplı Nijeryalı-Yeni Zelandalı dövüşçü. Orta siklet hakimi, kickboks geçmişli.",
                    PhotoUrl = "https://ui-avatars.com/api/?name=Israel+Adesanya&size=300&background=007bff&color=fff&bold=true",
                    Specialties = new List<Service> { kickboks, muayThai }
                },
                new Trainer
                {
                    FullName = "Charles Oliveira",
                    Bio = "'Do Bronx' lakaplı Brezilyalı efsane. UFC tarihinin en fazla submission kazanan dövüşçüsü. BJJ ustası.",
                    PhotoUrl = "https://ui-avatars.com/api/?name=Charles+Oliveira&size=300&background=ffc107&color=000&bold=true",
                    Specialties = new List<Service> { bjj }
                },
                new Trainer
                {
                    FullName = "Jon Jones",
                    Bio = "Tüm zamanların en iyisi tartışmalarının değişmez ismi. Yaratıcı teknikler ve üstün ring IQ'su.",
                    PhotoUrl = "https://ui-avatars.com/api/?name=Jon+Jones&size=300&background=6f42c1&color=fff&bold=true",
                    Specialties = new List<Service> { gures, mma }
                },
                new Trainer
                {
                    FullName = "Amanda Nunes",
                    Bio = "Kadınlar MMA tarihinin en iyisi. Tüy ve bantam sıkletlerinde çift şampiyon. Brezilya'nın 'Lioness'i.",
                    PhotoUrl = "https://ui-avatars.com/api/?name=Amanda+Nunes&size=300&background=e83e8c&color=fff&bold=true",
                    Specialties = new List<Service> { boks, bjj, muayThai }
                }
            };
            context.Trainers.AddRange(trainers);
            await context.SaveChangesAsync();

            var allTrainers = await context.Trainers.ToListAsync();
            foreach (var trainer in allTrainers)
            {
                var availabilities = new List<TrainerAvailability>
                {
                    new TrainerAvailability { TrainerId = trainer.Id, DayOfWeek = DayOfWeek.Monday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                    new TrainerAvailability { TrainerId = trainer.Id, DayOfWeek = DayOfWeek.Tuesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                    new TrainerAvailability { TrainerId = trainer.Id, DayOfWeek = DayOfWeek.Wednesday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                    new TrainerAvailability { TrainerId = trainer.Id, DayOfWeek = DayOfWeek.Thursday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) },
                    new TrainerAvailability { TrainerId = trainer.Id, DayOfWeek = DayOfWeek.Friday, StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0) }
                };
                context.TrainerAvailabilities.AddRange(availabilities);
            }
            await context.SaveChangesAsync();
        }
    }
}
