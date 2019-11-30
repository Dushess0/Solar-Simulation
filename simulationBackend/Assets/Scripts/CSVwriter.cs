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
        public static string FloatsToString(List<float> floats)
        {
            string row = "[";

            foreach (var item in floats)
            {
                row += item+",";

            }
            row.Remove(row.Length - 1);
            row += "]";

            return row;
        }

    }

    public class DataRow
    {
        
        
       
        public  Texture2D[] Cameras;
  
        public  List<Vector3> lidarData;
        public  List<float> ultrasoundData;

     
        // two floats indicating state of each engine
        public float[] EnginesData;

        internal DataRow(Texture2D[] Cameras,List<Vector3> lData, List<float> uData)
        {
           
            this.Cameras = Cameras;
            lidarData = lData;
            ultrasoundData = uData;
           
        }        
    };

    public class Writer
    {
        private string SavingDirectory = "Records";
        

        private List<DataRow> records = new List<DataRow>();
		public Writer()
        {
            if (!Directory.Exists(SavingDirectory))
            {
                Directory.CreateDirectory(SavingDirectory);
                Debug.Log("Created new dir");

            }
        }
	
        public void write()
        {
            
            string formatted_data = "";
            int i = 0;
            DateTime dateTime = DateTime.Now;
            string date=dateTime.Hour+"-"+dateTime.Minute+"-"+dateTime.Second;
            Debug.Log(date);
            string currentdir = SavingDirectory + "/" + date;
            Directory.CreateDirectory(currentdir);
            


            List<string> img_dirs=new List<string>();
            for (int j=0;j<this.records[0].Cameras.Length;j++)
            {
                img_dirs.Add(currentdir + "/" + "camera_" + j + "_images");
                Directory.CreateDirectory(img_dirs[j]);
            }

            
          
            foreach ( DataRow row in records)
            {
                string csv_format_row = "";

                
                
                


                csv_format_row += Converter.VectorsToString(row.lidarData);
                csv_format_row= csv_format_row.Remove(csv_format_row.Length - 2, 1);
                
                
                csv_format_row += ",";
                csv_format_row += Converter.FloatsToString(row.ultrasoundData);

                formatted_data += csv_format_row + "\n";
                int id = 0;
                foreach (var texture in row.Cameras)
                {
                    
                    byte[] _bytes = texture.EncodeToPNG();
                    System.IO.File.WriteAllBytes(img_dirs[id]+"/"+ i.ToString()+".png", _bytes);
                    id++;

                }
                id = 0;
                i++;
            }
           
            TextWriter tw = new StreamWriter(currentdir+"/"+ date+".csv");
            tw.WriteLine(formatted_data);

            tw.Close();




            Debug.Log("Succesfully saved training data");

        }

        public void addRecord(Texture2D[] CamerasImages,List<Vector3> LidarData, List<float> UltraSoundData)
        { 

            records.Add(new DataRow(CamerasImages, LidarData, UltraSoundData)); 
        }
    }
     
}



