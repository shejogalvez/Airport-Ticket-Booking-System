namespace app.RepositoryClasses;

using app.Model;

using CsvHelper;
using System.Globalization;
public static class FlightDataManager
{

    static Flight[]? _data;

    public static Flight[] Data
    {
        get {
            if (_data is null) {
                // TODO: populate with data from "database"
                Flight[] test_data = [
                    new Flight(100, "USA", "Canada", new DateTime(2025, 12, 23), "JFK", "YYZ", FlightClass.Economy),
                    new Flight(200, "USA", "Canada", new DateTime(2025, 12, 24), "LAX", "YYZ", FlightClass.Business),
                    new Flight(300, "USA", "Canada", new DateTime(2025, 12, 25), "JFK", "YYZ", FlightClass.FirstClass),
                    new Flight(50, "Chile", "Argentina", new DateTime(2025, 12, 26), "SCL", "REL", FlightClass.Economy),
                    new Flight(80, "Chile", "Argentina", new DateTime(2025, 12, 28), "SCL", "CSZ", FlightClass.Business),
                    new Flight(100, "Chile", "Argentina", new DateTime(2025, 12, 28), "SCL", "REL", FlightClass.FirstClass),
                    new Flight(200, "CountryA", "CountryB", new DateTime(2025, 12, 29), "ABC", "DFG", FlightClass.Economy),
                    new Flight(350, "CountryB", "CountryA", new DateTime(2025, 12, 29), "DFG", "ABC", FlightClass.FirstClass),
                ];
                _data = test_data;
            }
            return _data;
        }
    }

    public static IEnumerable<Flight> GetAllFlights()
    {
        return Data;
    }


    public static void ImportFromCsv(string filePath, bool overwrite = false)
    {
        filePath = Path.GetFullPath(filePath);
        Flight[] records;
        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            // read CSV file
            records = csv.GetRecords<Flight>().ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }

        // Validate records
        if (records is null) return;
        
        bool errorsFound = false;
        for (int i=0; i<records.Length; i++)
        {
            var record = records[i];
            var errors = record.GetErrors();
            if (errors != ErrorCode.NoErrors) {
                Console.WriteLine($"Invalid flight record found in row {i+1}:");
                Console.WriteLine(errors);
                errorsFound = true;
            }
            // Console.WriteLine($"{record}");
        }
        if (errorsFound) {
            Console.WriteLine("Import aborted due to invalid records.");
            return;
        }

        if (overwrite || _data is null)
        {
            _data = [.. records];
            return;
        }
        else
        {
            _data = [.. _data.Concat([.. records])];
        }
        
        
    }

}