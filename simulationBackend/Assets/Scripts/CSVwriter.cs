using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Linq;
using System;
using UnityEngine;
namespace CSVWriter{
	
    public class DataRow
    {
        private byte[] firstCamera ;
        private byte[] secondCamera;
        private byte[] thirdCamera ;
        private List<Vector3> lidarData;
        private List<Vector2> ultrasoundData;
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
        public List<Vector3> LidarData
        {
            get { return this.lidarData; }
            set { this.lidarData = value; }
        }
        public List<Vector2> UltrasoundData
        {
            get { return this.ultrasoundData; }
            set { this.ultrasoundData = value; }
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

        internal DataRow(byte[] fC, byte[] sC, byte[] tC,List<Vector3> lData, List<Vector2> uData,float[] engine_state )
        {
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
        private string csvFileName;
        private List<DataRow> records = new List<DataRow>();
		
			
        public void write()
        {
				  using (var writer = new StreamWriter("data_set.csv"))
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




