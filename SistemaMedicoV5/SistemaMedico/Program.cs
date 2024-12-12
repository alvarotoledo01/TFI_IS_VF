using SistemaMedico.Repository;
using SistemaMedico.Services;
using SistemaMedicoV3.Repository;
using SistemaMedicoV3.Services;

var builder = WebApplication.CreateBuilder(args);

// Registro de dependencias
builder.Services.AddSingleton<UsuariosRepository>(); // Registrando UsuariosRepository
builder.Services.AddTransient<AutenticarService>();  // Registrando AutenticarService

// Registrar el servicio PacientesRepository
builder.Services.AddScoped<PacientesRepository>(); // Registrar PacientesRepository con el ciclo de vida adecuado (Scoped)
//a�adido el 11/12 a las 17:26 "posible solucion de construccion del builder"
builder.Services.AddSingleton<MedicosRepository>();
// Registrar el servicio PacienteService
builder.Services.AddScoped<PacienteService>();  // Registrar PacienteService para que pueda recibir PacientesRepository
//a�adido el 11/12 a las 17:23 "posible solucion de construccion del builder"
builder.Services.AddScoped<ApiResponseService>();
builder.Services.AddScoped<RecetaDigitalService>();

// Configurar sesiones 12/12 2:35
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Tiempo de expiraci�n de la sesi�n
    options.Cookie.HttpOnly = true; // Seguridad
    options.Cookie.IsEssential = true; // Cookies esenciales
});
// Agregar otros servicios necesarios
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuraci�n de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); //Con esto ya deberia poder usar session 12/12 2:35
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
