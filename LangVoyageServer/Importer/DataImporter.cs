using LangVoyageServer.Database;

namespace LangVoyageServer.Importer;

public partial class DataImporter
{
    public DataImporter(LangServerDbContext context, IStorageService service)
    {
        Context = context;
        Service = service;
    }

    public void Run()
    {
        // you know what, originally I thought - lets read these from a file... then I figured, why?  just put it in the code... that's a file.
        ImportA1Nouns();
        ImportA2Nouns();
        ImportB1Nouns();
        ImportB2Nouns();
        ImportC1Nouns();
        ImportC2Nouns();

        Context.SaveChanges();
    }

    public IStorageService Service { get; set; }

    public LangServerDbContext Context { get; set; }
}