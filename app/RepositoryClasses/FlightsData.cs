namespace app.RepositoryClasses;

using app.Model;

using CsvHelper;
using CsvHelper.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
public static class FlightDataManager
{

    private static DataSaverComponent<Flight> SaveComponent { get; } = new ("data");
    static Flight[]? _data;

    public static Flight[] Data
    {
        get {
            _data ??= [.. SaveComponent.Data];
            return _data;
        }
    }

    public static IEnumerable<Flight> GetAllFlights()
    {
        return Data;
    }

    public static IList<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);

        Validator.TryValidateObject(
            model,
            context,
            results,
            validateAllProperties: true
        );

        return results;
    }


    public static void ImportFromCsv(
        string filePath,
        bool validate = true,
        bool skip = false,
        bool overwrite = false
     )
    {
        filePath = Path.GetFullPath(filePath);
        Flight[] records;
        try
        {

            bool errorsFound = false;

            CsvConfiguration csvConfig = new (CultureInfo.InvariantCulture) { 
                ReadingExceptionOccurred = e =>
                {         
                    Console.WriteLine($"""
                        Exception while reading csv on row {e.Exception.Context?.Reader?.CurrentIndex}:
                        {(validate ? e.Exception.Message : e)}
                        """
                    );
                    errorsFound = true;
                    return false;
                }, 
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, csvConfig);
            // read CSV file
            records = csv.GetRecords<Flight>().ToArray();
            
            // Validate records
            if (records is null) return;
            
            List<Flight> valid_records = [];
            if (validate)
            {
                for (int i=0; i<records.Length; i++)
                {
                    var record = records[i];
                    var errors = Validate(record);
                    if (errors.Any()) {
                        Console.WriteLine($"\nInvalid flight record found in row {i+1}:");
                        foreach (var error in errors)
                        {
                            Console.WriteLine($"  - {error.ErrorMessage}");
                        }
                        errorsFound = true;
                    }
                    else valid_records.Add(record);
                    // Console.WriteLine($"{record}");
                }
                if (!errorsFound) Console.WriteLine("No errors were found on the file!");
            }
            if (errorsFound && !skip) {
                Console.WriteLine("\nImport aborted due to invalid records...");
                return;
            }

            if (overwrite || _data is null)
            {
                _data = [.. records];
            }
            else
            {
                _data = [.. _data.Concat([.. records])];
            }
            SaveComponent.Save(_data);
            Console.WriteLine("\nData successfully imported!");
        
        }
        catch (Exception e)
        {
            Console.WriteLine($"unknown exception encountered: {e}");
            return;
        }


        
    }

}