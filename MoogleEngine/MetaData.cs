namespace MoogleEngine
{
    public class MetaData
    {
        public int ocurrence{get; set;}
        public int TotalOcurrence{get;set;}
        
        public List<int> Pos {get;set;}
        
        public Double TF {get;set;}

        public Double IDF {get;set;}
        public MetaData()
        {
            ocurrence = 1;
            TotalOcurrence=1;
            Pos = new List<int>();
            TF =0;
            IDF=0;

        }
        

    }
}