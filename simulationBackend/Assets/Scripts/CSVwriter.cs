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
        private byte[] firstCamera ;
        private byte[] secondCamera;
        private byte[] thirdCamera ;
        private List<Vector3> lidarData;
        private List<Vector2> ultrasoundData;

        public string lidarDataLoaction;
        // two floats indicating state of each engine
        private float leftEngine;  
        private float rightEngine;

        
        
		public static void Main(){
			 Console.WriteLine("DataRow");
		}

		
        public byte[] FirstCamera
		{
 			 get { return this.firstCamera; }
  			 set { this.firstCamera = value; }
		}
        public byte[] SecondCamera
        {
            get { return this.firstCamera; }
            set { this.secondCamera = value; }
        }
        public byte[] ThirdCamera
        {
            get { return this.thirdCamera; }
            set { this.thirdCamera = value; }
        }
        
        public float LeftEngine
        {
            get { return this.leftEngine; }
            set { this.LeftEngine= value; }
        }
        public float RightEngine
        {
            get { return this.rightEngine; }
            set { this.RightEngine= value; }
        }
        
        public void writeAdditionalData(){
				using (var writer = new StreamWriter(lidarDataLoaction))
    			using (var csv = new CsvWriter(writer))
    			{
                    csv.WriteRecords(this.lidarData);                    
    			}
        }

        internal DataRow(byte[] fC, byte[] sC, byte[] tC,List<Vector3> lData, List<Vector2> uData,float[] engine_state )
        {
            DataRow.rowsInDataset += 1;
            this.rowNumber = rowsInDataset; 
            this.firstCamera = fC;
            this.secondCamera = sC;
            this.thirdCamera = tC;
            lidarData = lData;
            ultrasoundData = uData;
            this.leftEngine = engine_state[0];
			this.rightEngine = engine_state[1];
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
            // fill location of lidar data
                foreach (DataRow row in records)
                {
                    row.lidarDataLoaction = lidarDirectory + "/lidar_data_" + row.rowNumber.ToString() + ".csv";
                    row.writeAdditionalData();
                }                
				using (var writer = new StreamWriter(csvFileName))
    			using (var csv = new CsvWriter(writer))
    			{
                    csv.WriteRecords(records);                    
    			}

            Debug.Log("Succesfully saved training data");

        }

        public void addRecord(byte[] fC, byte[] sC, byte[] tC,List<Vector3> lData, List<Vector2> uData,float[] eng_data)
        { 
            records.Add(new DataRow(fC,sC,tC,lData,uData,eng_data)); 
        }
    }
     
}




