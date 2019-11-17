using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Linq;
using System;

namespace CSVWriter{
	
    public class DataRow
    {
        private byte[] firstCamera =  Enumerable.Repeat((byte)0x20, 1000 ).ToArray();
        private byte[] secondCamera =  Enumerable.Repeat((byte)0x20, 1000).ToArray();
        private byte[] thirdCamera =  Enumerable.Repeat((byte)0x20, 1000).ToArray();
        private List<Vector3> lidarData;
        private List<Vector3> ultrasoundData;
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
		
        internal DataRow(byte[] fC, byte[] sC, byte[] tC,List<Vector3> lData, List<Vector3> uData,float[] engine_state )
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
		
			
        public void write(){
				  using (var writer = new StreamWriter("/data/data_set.csv"))
    			  using (var csv = new CsvWriter(writer))
    			 {
        			csv.WriteRecords(records);
    			}    
        }

        public void addRecord(byte[] fC, byte[] sC, byte[] tC,List<Vector3> lData, List<Vector3> uData,float[] eng_data)
        {
            records.Add(new DataRow(fC,sC,tC,lData,uData,eng_data)); 
        }
    }
     
}




