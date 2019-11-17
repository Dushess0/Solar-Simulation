using System;
using System.IO;
using System.Text;
/* Library that should work with csv
using CsvHelper;
*/
using System.Windows.Media.Media3D;
using System.Windows.Media.Media2D;
namespace CsvWriter
{
    public class DataRow{
        private Image firstCamera {get; set;}
        private Image secondCamera {get; set;}
        private Image thirdCamera {get; set;}
        private List<Vector3D> lidarData = List<Vector3D>();
        // private List<int[][][]> lidarData; Is it a better way ??
        private List<Vector2D> ultrasoundData = List<Vector2D>(); 
        // assuming we will obtain sth similat to that https://www.electronicshub.org/wp-content/uploads/2018/06/Arduino-Radar-Project-Processing-Output.jpg 
        
        DataRow(string filename, Image fC,Image sC, Image tC,List<Vector3D> lData, List<Vector2D> uData ){
            firstCamera = fc;
            secondCamera = sC;
            thirdCamera = tC;
            lidarData = lData;
            ultrasoundData = uData;
        }        
    };

    public class Writer{
        private string csvFileName { get; set; }
        private List<DataRow> records = new List<DataRow>();

        public static void Main(){
            
            //using (var writer = new StreamWriter("path\\to\\file.csv"));
            // using (var csv = new CsvWriter(writer));
           // {
                /* redefine this method to make it save images on disk and wrtie path to that images to csv */
                // csv.WriteRecords(records);
            //}
        }

        public void addRecord(Image fC,Image sC, Image tC,List<Vector3D> lData, List<Vector2D> uData){
            records.Add(new DataRow(fC,sC,tC,lData,uData)); 
        }
    }
    
    
}




