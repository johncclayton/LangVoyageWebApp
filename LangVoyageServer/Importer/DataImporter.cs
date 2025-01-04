using LangVoyageServer.Database;

namespace LangVoyageServer.Importer;

public partial class DataImporter
{
    public DataImporter(LangServerDbContext context, IStorageService service)
    {
        Context = context;
        Service = service;
    }

    public async Task Run()
    {
        // you know what, originally I thought - lets read these from a file... then I figured, why?  just put it in the code... that's a file.
        await ImportA1Nouns();
        await ImportA2Nouns();
        await ImportB1Nouns();
        await ImportB2Nouns();
        await ImportC1Nouns();
        await ImportC2Nouns();

        await Context.SaveChangesAsync();
    }

    public IStorageService Service { get; set; }

    public LangServerDbContext Context { get; set; }
}