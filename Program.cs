Console.WriteLine("Hello,World!");
// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();
string file = "data.txt";

if (resp == "1")
{
    // create data file
    // ask question
    Console.WriteLine("How many weeks of data is needed?");
    //input the response (convert to int)
    int weeks = Convert.ToInt32(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new();
    // create file
    StreamWriter sw = new("data.txt");

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
        // read data from file
        if (File.Exists(file))
        {
            // accumulators needed for GPA
            int gradePoints = 0;
            int count = 0;
            // read data from file
            StreamReader sr = new(file);
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                // convert string to array
                string[] arr = String.IsNullOrEmpty(line) ? [] : line.Split('|');
                // display array data
                Console.WriteLine("Course: {0}, Grade: {1}", arr[0], arr[1]);
                // add to accumulators
                gradePoints += arr[1] == "A" ? 4 : arr[1] == "B" ? 3 : arr[1] == "C" ? 2 : arr[1] == "D" ? 1 : 0;
                count += 1;
            }
            sr.Close();
            // calculate GPA
            double GPA = (double)gradePoints / count;
            Console.WriteLine("GPA: {0:n2}", GPA);
        }
        else
        {
            Console.WriteLine("File does not exist");
        }

}