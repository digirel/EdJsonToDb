using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SimpleNHibernate
{
    class Program
    {

        static void Main(string[] args)
        {
            // get a database connection - this will create the database if it doesn't exist so as you test your first passes
            // you can just delete the database every time and not worry about mucking about in a db editor
            // trying to update tables and data if you make some that are garbage :)

            var dbSession = new DatabaseSession();
            string[] sampleJson;
            string path = @"bodies.json"; // TODO: Handle missing file better. Add possibility to specify path/filename via cmd-line parameter

            DateTime beginTime = DateTime.UtcNow;
            decimal elapsedTime;
            decimal estimatedTotalTime;
            int rowCounter = 0;
            double batchSize = 100000;

            // Handle null-values via settings-parameter
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            Console.WriteLine("Starting to read: " + path);

                List<Bodies> objects = new List<Bodies>();

                foreach (var line in File.ReadLines(path))
                {

                    Bodies newObj = JsonConvert.DeserializeObject<Bodies>(line, settings);
                    //if (newObj.id.Equals(0)) // filter out records with ID=0, as they are data-artifacts/-leftovers and cause a unique constraint failure
                    //{
                    //    Console.WriteLine("Line with ID=0 found, skipping entry");
                    //}
                    //else
                    //{
                        objects.Add(newObj);

                        rowCounter++;
                        if ((rowCounter % batchSize) == 0)
                        {
                            dbSession.SaveManyTestObjs(objects);
                            objects = new List<Bodies>();

                            elapsedTime = Convert.ToInt32((DateTime.UtcNow - beginTime).TotalMilliseconds);
                            Console.WriteLine("Lines done: " + rowCounter.ToString("#,##0") + " _ elapsedTime (s): " + string.Format("{0:0}", elapsedTime / 1000) + " _ ØPerformance (rows/ms): " + (rowCounter / elapsedTime).ToString("#,##0"));
                        }
                    //}
                }
                dbSession.SaveManyTestObjs(objects);
                elapsedTime = Convert.ToInt32((DateTime.UtcNow - beginTime).TotalMilliseconds);
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("Total lines: " + rowCounter.ToString("#,##0") + " _ elapsedTime (s): " + string.Format("{0:0}", elapsedTime / 1000) + " _ ØPerformance (rows/ms): " + (rowCounter / elapsedTime).ToString("#,##0"));


            Console.WriteLine("Press any key");
            do { } while (!Console.KeyAvailable);
        }
    }
}
