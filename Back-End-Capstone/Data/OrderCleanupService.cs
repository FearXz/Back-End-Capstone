namespace Back_End_Capstone.Data
{
    public class OrderCleanupService : BackgroundService
    {
        // IServiceProvider è necessario per creare un nuovo scope per ogni ciclo di pulizia
        // service provider è un'interfaccia per risolvere le dipendenze durante l'esecuzione di un'applicazione .NET Core
        private readonly IServiceProvider _serviceProvider;

        public OrderCleanupService(IServiceProvider serviceProvider)
        {
            // Inietta il servizio IServiceProvider
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // stoppingToken è un token che viene passato automaticamente a ExecuteAsync
            // e può essere utilizzato per controllare se il servizio deve essere arrestato
            while (!stoppingToken.IsCancellationRequested)
            {
                // Crea un nuovo scope per ogni ciclo di pulizia
                // in modo che il DbContext possa essere eliminato correttamente
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Ottieni il DbContext dallo scope
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // Ottieni l'ora attuale
                    var now = DateTime.Now;

                    // Ottieni tutti gli ordini non pagati che sono più vecchi di 30 minuti
                    var oldUnpaidOrders = context.Ordini.Where(o =>
                        !o.IsPagato && o.DataOrdine <= now.AddMinutes(-15)
                    );
                    if (oldUnpaidOrders != null)
                    {
                        // Cancella tutti gli ordini non pagati vecchi
                        context.Ordini.RemoveRange(oldUnpaidOrders);

                        // Salva le modifiche al database
                        context.SaveChanges();
                    }
                }

                // Aspetta 30 minuti prima di eseguire di nuovo
                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }
    }
}
