// https://joshclose.github.io/CsvHelper/
// Import the csv module
using CsvHelper;

// Define an array of different objects
object[] array = new object[]
{
  new List<string>() {"Alice", "25", "Engineer"}, // A list
  new Tuple<string, string, string>("Bob", "30", "Doctor"), // A tuple
  new Dictionary<string, string>() // A dictionary
  {
    {"name", "Charlie"},
    {"age", "35"},
    {"occupation", "Teacher"}
  },
  new SaleOrderDetail("SO-001", "David", 2460, "[tablet: $800 x 3 = $2400, mouse: $20 x 3 = $60]") // A custom class
};

// Open a CSV file in writing mode
using (var writer = new StreamWriter("array.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
  // Write the array of objects as CSV lines
  csv.WriteRecords(array);
}
