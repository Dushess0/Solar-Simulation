using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
/* Library that should work with csv
using CsvHelper;
*/
using Unity;
using UnityEngine;

namespace CsvWriter
{
    public class DataRow
    {
        private byte[] firstCamera;
        private byte[] secondCamera;
        private byte[] thirdCamera;
        private List<Vector3> lidarData;
        // private List<int[][][]> lidarData; Is it a better way ??
        private List<Vector3> ultrasoundData;
        private float[] engines_state;  // two floats indicating state of each engine
        
        // assuming we will obtain sth similat to that https://www.electronicshub.org/wp-content/uploads/2018/06/Arduino-Radar-Project-Processing-Output.jpg 
        
        internal DataRow(byte[] fC, byte[] sC, byte[] tC,List<Vector3> lData, List<Vector3> uData,float[] engines_state )
        {
            firstCamera = fC;
            secondCamera = sC;
            thirdCamera = tC;
            lidarData = lData;
            ultrasoundData = uData;
            this.engines_state = engines_state;
        }        
    };

    public class Writer
    {
        private string csvFileName;
        private List<DataRow> records = new List<DataRow>();

        public static void Main()
        {
            
            //using (var writer = new StreamWriter("path\\to\\file.csv"));
            // using (var csv = new CsvWriter(writer));
           // {
                /* redefine this method to make it save images on disk and wrtie path to that images to csv */
                // csv.WriteRecords(records);
            //}
        }

        public void addRecord(byte[] fC, byte[] sC, byte[] tC,List<Vector3> lData, List<Vector3> uData,float[] eng_data)
        {
            records.Add(new DataRow(fC,sC,tC,lData,uData,eng_data)); 
        }
    }
    
    
}




