﻿using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";
// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

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
    if (int.TryParse(Console.ReadLine(), out int weeks)){
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
    } else {
        // log error
        logger.Error("You must enter a valid number");
    }

}
else if (resp == "2")
{
        // read data from file
        if (File.Exists(file))
        {
            StreamReader sr = new(file);
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            // convert string to array
            string[] arr = String.IsNullOrEmpty(line) ? [] : line.Split(',');
            // convert array to DateTime
            DateTime week = Convert.ToDateTime(arr[0]);
            // split hours
            string[] hours = arr[1].Split('|');
            // make sum of hours
            int sum = 0;
            for (int i = 0; i < hours.Length; i++)
            {
                sum += Convert.ToInt32(hours[i]);
            }
            // make average of hours
            double avg = sum / 7.0;
            // display data
            Console.WriteLine($"Week of {week: MMM, dd, yyyy}");
            Console.WriteLine($"Su Mo Tu We Th Fr Sa Tot Avg");
            Console.WriteLine($"-- -- -- -- -- -- -- --- ---");
            Console.WriteLine($"{hours[0],2} {hours[1],2} {hours[2],2} {hours[3],2} {hours[4],2} {hours[5],2} {hours[6],2} {sum, 3} {Math.Round(avg, 1), 3}");

        }
            sr.Close();
        }
        else
        {
            Console.WriteLine("File does not exist");
        }

}