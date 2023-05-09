namespace MoogleEngine
{

    public class Score
    {
         public static Dictionary<string,double> score( Dictionary<string,MetaData> Query, Dictionary<string,Dictionary<string,MetaData>> Documents,Dictionary<string,MetaData> TWords,Dictionary<char,MetaData> Operators,string query)
        {
   
        Dictionary<string,double> Score = new Dictionary<string, double>();
        var TotalDocument= Documents.Count;
        var TotalWords= TWords.Count;
         foreach(KeyValuePair<string,Dictionary<string,MetaData>> Document in Documents) 
         {  double Sumatory1=0;
            double Sumatory2=0;
            double Sumatory3=0;
           
            foreach(KeyValuePair<string,MetaData> Words in Query)  
            {  
                System.Console.WriteLine( Words);
               var y= QWeight(Words,TWords,Documents);
               if(Document.Value.ContainsKey(Words.Key))
               {
                  var x= DWeight(Document,Words,TWords,Operators, Documents,query);
                  Sumatory1+= x*y; 
             
               }
               Sumatory2+=Math.Pow(y,2);
               
            }
             
            
            foreach(KeyValuePair<string,MetaData> Word in Document.Value)
            {
               var x= DWeight1(Document,Word,TWords);
               Sumatory3+=Math.Pow(x,2);
         
            }
            var S= Sumatory1/(Math.Sqrt(Sumatory2)*Math.Sqrt(Sumatory3));
              
            Score.Add(Document.Key,S);
         } 

         
         Filter(ref Score,Operators,Documents,query); 
         return Score;  
        }
        public static double DWeight (KeyValuePair<string,Dictionary<string,MetaData>> Document, KeyValuePair<string,MetaData> Words,Dictionary<string,MetaData> Twords,Dictionary<char,MetaData> Operator,Dictionary<string, Dictionary<string,MetaData>> Documents,string query)
        {
            double TF;
            double IDF;
            double Weight;
            double TotalOcurrence;


            double ocurrence;
            double TotalDocument= Document.Key.Count(); 
            ocurrence = Document.Value[Words.Key].ocurrence;
            TotalOcurrence= Twords[Words.Key].TotalOcurrence;
            TF= Math.Log(ocurrence);
            IDF= Math.Log((TotalDocument/TotalOcurrence));                
            if(TF==0)
            {TF=1;}
            if(IDF==0)
            {IDF=1;}

           
            Document.Value[Words.Key].TF=TF;
         
            Document.Value[Words.Key].IDF=IDF;
                 
                Weight= TF*IDF;
          
                return Weight;

        }
         public static double QWeight (KeyValuePair<string,MetaData> Words, Dictionary< string,MetaData> Twords, Dictionary<string,Dictionary<string,MetaData>> Documents)
        {
            
            double TF;
            double IDF;
            double Weight;
            double TotalOcurrence;
            double ocurrence;
            double TotalDocument;

            ocurrence= Words.Value.ocurrence+1;
            if(Twords.ContainsKey(Words.Key))
            {
            TotalOcurrence= Twords[Words.Key].TotalOcurrence;
            }
            else return 0;

            TotalDocument=Documents.Count;
            TF = Math.Log(ocurrence);
            IDF= Math.Log(TotalDocument/TotalOcurrence);
            Weight= TF*IDF;
                 
            return Weight;
            
        }
        public static double DWeight1 (KeyValuePair<string,Dictionary<string,MetaData>> Document, KeyValuePair<string,MetaData> Words,Dictionary<string,MetaData> Twords)
        {
            double TF;
            double IDF;
            double Weight;
            double TotalOcurrence;
            double ocurrence;
            double TotalDocument= Document.Key.Count();
            
            ocurrence = Words.Value.ocurrence;
       
            TotalOcurrence= Twords[Words.Key].TotalOcurrence;
            TF= Math.Log(ocurrence);
            IDF= Math.Log(TotalDocument/TotalOcurrence);                
            Words.Value.TF=TF;
         
            Words.Value.IDF=IDF;
                 
                Weight= TF*IDF;
          
                return Weight;

        }
        static public void Filter( ref Dictionary<string,double> Score,Dictionary<char,MetaData> Operator,Dictionary<string,Dictionary<string,MetaData>> Documents,string query)
        {
            var signo1=Operator['!'].ocurrence-1;
            var signo2=Operator['~'].ocurrence-1;
            var signo3=Operator['^'].ocurrence-1;
            if(signo1>0)
            {Operators.DecreaseScore( Score, Documents,query,Operator);}
            if(signo2>0)
            {Operators.NearbyWords( Documents,query,Operator, Score);}
            if(signo3>0)
            {Operators.IncreaseScore( Score, Documents,query,Operator);}
        }

    }
}