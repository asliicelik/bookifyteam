using Microsoft.EntityFrameworkCore;
using MyMvcProject.Data;
using MyMvcProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=bookrecommendations.db"));

// Add custom services
builder.Services.AddScoped<IRecommendationService, RecommendationService>();

// Add session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
        
        // Seed data if database is empty
        if (!context.Moods.Any())
        {
            var moods = new List<MyMvcProject.Models.Mood>
            {
                new MyMvcProject.Models.Mood 
                { 
                    Id = 1, 
                    Name = "Calm", 
                    Description = "Sakin ve huzurlu",
                    MusicKeywordsJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "lofi", "chill", "calm", "relaxing", "peaceful", "ambient", "meditation" }),
                    MusicGenresJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "lofi", "chill", "ambient", "acoustic" })
                },
                new MyMvcProject.Models.Mood 
                { 
                    Id = 2, 
                    Name = "Intense", 
                    Description = "Yoğun ve enerjik",
                    MusicKeywordsJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "rock", "metal", "intense", "powerful", "aggressive", "heavy", "energetic" }),
                    MusicGenresJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "rock", "metal", "hard rock", "punk" })
                },
                new MyMvcProject.Models.Mood 
                { 
                    Id = 3, 
                    Name = "Melancholic", 
                    Description = "Melankolik ve derin",
                    MusicKeywordsJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "sad", "melancholic", "deep", "emotional", "introspective", "contemplative" }),
                    MusicGenresJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "indie", "alternative", "folk", "singer-songwriter" })
                },
                new MyMvcProject.Models.Mood 
                { 
                    Id = 4, 
                    Name = "Focused", 
                    Description = "Odaklanmış ve konsantre",
                    MusicKeywordsJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "focus", "concentration", "study", "work", "productivity", "deep focus" }),
                    MusicGenresJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "instrumental", "classical", "electronic", "jazz" })
                },
                new MyMvcProject.Models.Mood 
                { 
                    Id = 5, 
                    Name = "Uplifting", 
                    Description = "Neşeli ve yükselten",
                    MusicKeywordsJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "happy", "uplifting", "joyful", "positive", "energetic", "upbeat" }),
                    MusicGenresJson = System.Text.Json.JsonSerializer.Serialize(new List<string> { "pop", "dance", "electronic", "indie pop" })
                }
            };
            
            context.Moods.AddRange(moods);
            context.SaveChanges();
        }
        
        if (!context.Books.Any())
        {
            var books = new List<MyMvcProject.Models.Book>
            {
                new MyMvcProject.Models.Book { Id = 1, Title = "Sakinlik Sanatı", Author = "Eckhart Tolle", 
                    Description = "İç huzuru ve sakinliği bulma üzerine derinlemesine bir rehber. Modern yaşamın stresinden uzaklaşmak için pratik öneriler.", 
                    Genre = "Kişisel Gelişim", Mood = "Calm", Year = 2019, 
                    Isbn = "978-605-09-1234-5", CoverImageUrl = "https://via.placeholder.com/300x400?text=Sakinlik+Sanatı" },
                new MyMvcProject.Models.Book { Id = 2, Title = "Mindfulness: Şimdi ve Burada", Author = "Jon Kabat-Zinn", 
                    Description = "Farkındalık pratiği ile yaşam kalitesini artırma yöntemleri. Meditasyon ve nefes teknikleri.", 
                    Genre = "Kişisel Gelişim", Mood = "Calm", Year = 2020, 
                    Isbn = "978-605-09-1235-6", CoverImageUrl = "https://via.placeholder.com/300x400?text=Mindfulness" },
                new MyMvcProject.Models.Book { Id = 3, Title = "Yavaş Yaşam", Author = "Carl Honoré", 
                    Description = "Hızlı yaşamın alternatifi olarak yavaş yaşam felsefesi. Daha anlamlı ve huzurlu bir yaşam için rehber.", 
                    Genre = "Felsefe", Mood = "Calm", Year = 2018, 
                    Isbn = "978-605-09-1236-7", CoverImageUrl = "https://via.placeholder.com/300x400?text=Yavaş+Yaşam" },
                new MyMvcProject.Models.Book { Id = 4, Title = "1984", Author = "George Orwell", 
                    Description = "Distopik bir gelecekte totaliter bir rejimin kontrolü altındaki toplum. Güç, özgürlük ve gerçeklik üzerine derin bir sorgulama.", 
                    Genre = "Distopya", Mood = "Intense", Year = 1949, 
                    Isbn = "978-605-09-1237-8", CoverImageUrl = "https://via.placeholder.com/300x400?text=1984" },
                new MyMvcProject.Models.Book { Id = 5, Title = "Cesur Yeni Dünya", Author = "Aldous Huxley", 
                    Description = "Bilim ve teknolojinin insanlığı nasıl dönüştürebileceğini anlatan distopik bir klasik.", 
                    Genre = "Distopya", Mood = "Intense", Year = 1932, 
                    Isbn = "978-605-09-1238-9", CoverImageUrl = "https://via.placeholder.com/300x400?text=Cesur+Yeni+Dünya" },
                new MyMvcProject.Models.Book { Id = 6, Title = "Fahrenheit 451", Author = "Ray Bradbury", 
                    Description = "Kitapların yasaklandığı bir dünyada, düşünce özgürlüğü ve bilgi üzerine güçlü bir hikaye.", 
                    Genre = "Distopya", Mood = "Intense", Year = 1953, 
                    Isbn = "978-605-09-1239-0", CoverImageUrl = "https://via.placeholder.com/300x400?text=Fahrenheit+451" },
                new MyMvcProject.Models.Book { Id = 7, Title = "Kürk Mantolu Madonna", Author = "Sabahattin Ali", 
                    Description = "İçsel yalnızlık ve aşkın derinliklerini keşfeden, Türk edebiyatının en etkileyici romanlarından biri.", 
                    Genre = "Edebiyat", Mood = "Melancholic", Year = 1943, 
                    Isbn = "978-605-09-1240-1", CoverImageUrl = "https://via.placeholder.com/300x400?text=Kürk+Mantolu+Madonna" },
                new MyMvcProject.Models.Book { Id = 8, Title = "Genç Werther'in Acıları", Author = "Johann Wolfgang von Goethe", 
                    Description = "Aşk, umutsuzluk ve melankoli üzerine zamansız bir klasik. Romantik edebiyatın başyapıtı.", 
                    Genre = "Edebiyat", Mood = "Melancholic", Year = 1774, 
                    Isbn = "978-605-09-1241-2", CoverImageUrl = "https://via.placeholder.com/300x400?text=Genç+Werther" },
                new MyMvcProject.Models.Book { Id = 9, Title = "Yabancı", Author = "Albert Camus", 
                    Description = "Varoluşçu felsefenin en önemli eserlerinden biri. Anlamsızlık ve yabancılaşma üzerine derin bir sorgulama.", 
                    Genre = "Felsefe", Mood = "Melancholic", Year = 1942, 
                    Isbn = "978-605-09-1242-3", CoverImageUrl = "https://via.placeholder.com/300x400?text=Yabancı" },
                new MyMvcProject.Models.Book { Id = 10, Title = "Derin İş", Author = "Cal Newport", 
                    Description = "Dikkat dağıtıcılardan uzak, derinlemesine çalışma teknikleri. Odaklanma ve verimlilik üzerine.", 
                    Genre = "Kişisel Gelişim", Mood = "Focused", Year = 2016, 
                    Isbn = "978-605-09-1243-4", CoverImageUrl = "https://via.placeholder.com/300x400?text=Derin+İş" },
                new MyMvcProject.Models.Book { Id = 11, Title = "Atomik Alışkanlıklar", Author = "James Clear", 
                    Description = "Küçük değişikliklerle büyük sonuçlar elde etme yöntemleri. Alışkanlık oluşturma ve sürdürme rehberi.", 
                    Genre = "Kişisel Gelişim", Mood = "Focused", Year = 2018, 
                    Isbn = "978-605-09-1244-5", CoverImageUrl = "https://via.placeholder.com/300x400?text=Atomik+Alışkanlıklar" },
                new MyMvcProject.Models.Book { Id = 12, Title = "Odaklanma", Author = "Daniel Goleman", 
                    Description = "Dikkat ve odaklanma becerilerini geliştirme üzerine bilimsel temelli bir rehber.", 
                    Genre = "Kişisel Gelişim", Mood = "Focused", Year = 2013, 
                    Isbn = "978-605-09-1245-6", CoverImageUrl = "https://via.placeholder.com/300x400?text=Odaklanma" },
                new MyMvcProject.Models.Book { Id = 13, Title = "Mutluluk Projesi", Author = "Gretchen Rubin", 
                    Description = "Bir yıl boyunca mutluluğu artırma deneyimi. Pratik ve ilham verici bir yaşam rehberi.", 
                    Genre = "Kişisel Gelişim", Mood = "Uplifting", Year = 2009, 
                    Isbn = "978-605-09-1246-7", CoverImageUrl = "https://via.placeholder.com/300x400?text=Mutluluk+Projesi" },
                new MyMvcProject.Models.Book { Id = 14, Title = "İyi Hissetmek", Author = "David Burns", 
                    Description = "Bilişsel terapi teknikleriyle depresyon ve kaygıyı yenme yöntemleri. Pozitif düşünce pratikleri.", 
                    Genre = "Psikoloji", Mood = "Uplifting", Year = 1980, 
                    Isbn = "978-605-09-1247-8", CoverImageUrl = "https://via.placeholder.com/300x400?text=İyi+Hissetmek" },
                new MyMvcProject.Models.Book { Id = 15, Title = "Yaşam Sevinci", Author = "Mihaly Csikszentmihalyi", 
                    Description = "Akış (flow) durumunu keşfetme ve yaşamdan zevk alma üzerine psikolojik bir inceleme.", 
                    Genre = "Psikoloji", Mood = "Uplifting", Year = 1990, 
                    Isbn = "978-605-09-1248-9", CoverImageUrl = "https://via.placeholder.com/300x400?text=Yaşam+Sevinci" }
            };
            
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
