using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Linq;
using System;
using UnityEngine;
using Google.Protobuf;
namespace CSVWriter
{
   

    public class Converter
    {

        public static string VectorsToString(List<Vector3> vectors)
        {
            string row = "[";

            foreach (var item in vectors)
            {
                row += "["+item.x+"," + item.y + "," + item.z + "],";

            }
            row.Remove(row.Length - 1);
            row += "]";

            return row;
        }

    }

    public class DataRow
    {
        
        
       
        public  ByteString[] Cameras;
  
        public  List<Vector3> lidarData;
        private float[] ultrasoundData;

     
        // two floats indicating state of each engine
        public float[] EnginesData;

        internal DataRow(ByteString[] Cameras,List<Vector3> lData, float[] uData)
        {
           
            this.Cameras = Cameras;
            lidarData = lData;
            ultrasoundData = uData;
           
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

            foreach( DataRow row in records)
            {
                string csv_format_row = "[";

                
                foreach (var item in row.Cameras)
                {

                    csv_format_row += item.ToStringUtf8();
                    
                }
                csv_format_row += "],";


                csv_format_row += Converter.VectorsToString(row.lidarData);
                formatted_data += csv_format_row + "\n";
               
            }

            TextWriter tw = new StreamWriter(csvFileName);
            tw.WriteLine(formatted_data);

            tw.Close();




            Debug.Log("Succesfully saved training data");

        }

        public void addRecord(ByteString[] CamerasImages,List<Vector3> LidarData, float[] UltraSoundData)
        { 

            records.Add(new DataRow(CamerasImages, LidarData, UltraSoundData)); 
        }
    }
     
}



