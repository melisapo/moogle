namespace MoogleEngine
{
    public class Snippet
    {
        public static Dictionary<string,string> GSnippet(Dictionary<string,MetaData> Query, Dictionary<string,Dictionary<string,MetaData>> Document,Dictionary<string,MetaData> Twords,Dictionary<string,double> Score,List<string> ScoreRanking)
        {    
            var snippet = new Dictionary<string,string>();
            
           foreach(KeyValuePair<string,Dictionary<string,MetaData>> ListDocuments in Document)
           {
             if(ScoreRanking.Contains(ListDocuments.Key))
             {
                var bestWord = BestWord(Query,ListDocuments,Twords); 
                var ListDocument=PlainLoader.LoaderDocument();
                foreach( string Document1 in ListDocument)
                {
                    if(Document1==ListDocuments.Key)
                    {  
                        
                        var D  = new StreamReader(Document1);
                        
                        var text = D.ReadToEnd();
                        
                        
                        var Pos= ListDocuments.Value[bestWord].Pos[0];
                        

                        var getSnippet= GetSnippet(Pos,text,bestWord);
                        snippet.TryAdd(ListDocuments.Key,getSnippet);
                        break;
                    }
                }   
         
              }
            }
            
            return snippet;


        }
    
        public static string BestWord(Dictionary<string,MetaData> Query, KeyValuePair<string,Dictionary<string,MetaData>> ListDocuments,Dictionary<string,MetaData> Twords)
        {   
            string Word="";
            double BestRelevance=0;
            foreach(KeyValuePair<string,MetaData> words in Query)
            {
                    foreach(KeyValuePair<string,MetaData> Listwords in ListDocuments.Value)
                    {
                        if(words.Key==Listwords.Key)
                        {   
                            var x=ListDocuments.Key.Count();
                            var y=Twords[words.Key].TotalOcurrence;
                            var i=Math.Log((x/y));
                            if(i>BestRelevance)
                            {
                                BestRelevance=i;   
                                Word=words.Key;
                                  break;                   
                            }
                            

                        } 
                    }
            }
                 
            return Word;
        }
        private static string GetSnippet (int pos, string Text,string bestWord)
        {
            
           var TSize = Text.Length;
           var WSize = bestWord.Length;
           var x= 10*WSize;
           var SizeLS=pos-x;
           var SizeTS= 2*x;
           var SizeRS=SizeLS+SizeTS;
           var restLS=0;
           var restRS=0;
           
           if(SizeTS>TSize)
           {
               var rest=SizeTS-TSize;
               var y= (rest/2)+(rest%2);
               SizeRS-=y;
               SizeLS-=y; 
               
           }
           
           if(SizeLS<0)
           {
               var rest= -1*SizeLS;
               restLS=rest;
               SizeLS+=rest;
           }    
            if(SizeRS>TSize)
           {    
               var rest= SizeRS-TSize;
               restRS=rest;               
           }   

           string GetSnippet= Text.Substring(SizeLS-restRS,SizeTS+restLS) ;
           return GetSnippet;


        }
    }
}        