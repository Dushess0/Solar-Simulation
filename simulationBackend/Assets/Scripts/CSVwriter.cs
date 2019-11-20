using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Linq;
using System;
using UnityEngine;

namespace CSVWriter{
   

    public class DataRow
    {
        private static int rowsInDataset = 0;
        // fix 
        public int rowNumber; 
        public  List<byte[]> Cameras;
  
        public  List<Vector3> lidarData;
        private List<Vector2> ultrasoundData;

        public string lidarDataLoaction;
        // two floats indicating state of each engine
        public float[] EnginesData;

        
  
        public string FolderName
        {
            get { return lidarDataLoaction; }
           
        }

      
        
       

        internal DataRow(List<byte[]> Cameras,List<Vector3> lData, List<Vector2> uData,float[] engine_state )
        {
            DataRow.rowsInDataset += 1;
            this.rowNumber = rowsInDataset;
            this.Cameras = Cameras;
            lidarData = lData;
            ultrasoundData = uData;
            this.EnginesData = engine_state;
        }        
    };

    public class Writer
    {
        private string lidarDirectory = "lidar_data";
        private string csvFileName = "data_set.csv";

        private List<DataRow> records = new List<DataRow>();
		internal Writer(){
            if (Directory.Exists(lidarDirectory)) 
            {
                Console.WriteLine("That path exists already.");
            }
            else
            {
                Directory.CreateDirectory(lidarDirectory);
                Console.WriteLine("The directory was created successfully ");
            }
        }
	
        public void write()
        {
            
            string formatted_data = "";

            string csv_format_row = "";

            foreach( DataRow row in records)
            {
                csv_format_row += "[";
                foreach (var item in row.Cameras)
                {
                    
                    csv_format_row += System.Text.Encoding.UTF8.GetString(item) +",";
                    
                }
                csv_format_row += "],";
                csv_format_row += LidarSensor.VectorsToString(row.lidarData)+",";
                formatted_data += csv_format_row + "\n";
                csv_format_row = "";
            }

            TextWriter tw = new StreamWriter(csvFileName);
            tw.WriteLine(formatted_data);

            tw.Close();




            Debug.Log("Succesfully saved training data");

        }

        public void addRecord(List<byte[]> Cameras,List<Vector3> lData, List<Vector2> uData,float[] eng_data)
        { 

            records.Add(new DataRow(Cameras, lData,uData,eng_data)); 
        }
    }
     
}



