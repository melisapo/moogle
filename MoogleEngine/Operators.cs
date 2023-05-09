namespace MoogleEngine
{
    class Operators 
    {

        public static void DecreaseScore( Dictionary<string,double> Score, Dictionary<string,Dictionary<string,MetaData>> Documents,string query,Dictionary<char,MetaData> Operators)
        {
             char C='!';
             var words=SearchWord(query,Operators,C);
             for(int i=0;i<words.Length;i++)
             {
                 foreach(KeyValuePair<string,Dictionary<string,MetaData>> Document in Documents)
                {
                  if(Document.Value.ContainsKey(words[i])) 
                  {
                      Score[Document.Key]=0;
                  }     
                }
             }  

        }
        public static void IncreaseScore( Dictionary<string,double> Score, Dictionary<string,Dictionary<string,MetaData>> Documents,string query,Dictionary<char,MetaData> Operators)
        {
           char C='^';
           var words=SearchWord(query,Operators,C);
           for(int i=0;i<words.Length;i++)
           {
               foreach(KeyValuePair<string,Dictionary<string,MetaData>> Document in Documents)
                {
                  if(!Document.Value.ContainsKey(words[i])) 
                  {
                      Score[Document.Key]=0;
                  }     
                }
           }

        }
        public static void IncreaseRelevance( Dictionary<string,Dictionary<string,MetaData>> Documents,string query,Dictionary<char,MetaData> Operators,  Dictionary<string,MetaData> TWords)
        {
            char C='*';
            var words=SearchWord(query,Operators,C);
            for(int i=0;i<words.Length;i++)
           {
               foreach(KeyValuePair<string,Dictionary<string,MetaData>> Document in Documents)
                {
                  if(Document.Value.ContainsKey(words[i])) 
                  {
                      Document.Value[words[i]].ocurrence+= (Document.Value[words[i]].ocurrence*25)/100;
                      TWords[words[i]].TotalOcurrence-= (TWords[words[i]].TotalOcurrence*25)/100;

                  }     
                }
           }


        }
        static public void NearbyWords (Dictionary<string,Dictionary<string,MetaData>> Documents,string query,Dictionary<char,MetaData> Operators,Dictionary<string,double> Score)
        {
            Dictionary<int,string> DistanceWords =new Dictionary<int, string>();
           
            char C='~';
            var words=SearchParticularWords(query,Operators,C);
            
             for(int i=0;i<words.Length;i++)
           {    int vueltas=0;
                List<int> support=new List<int>();
                
               foreach(KeyValuePair<string,Dictionary<string,MetaData>> Document in Documents)
                {
                    
                  if(Document.Value.ContainsKey(words[i][0]) && Document.Value.ContainsKey(words[i][1])) 
                  {  var k=0;
                     var t=0;
                     var lowDistance=int.MaxValue;
                    do
                    {
                     var Word1pos=Document.Value[words[i][0]].Pos[k];  
                     var word2pos=Document.Value[words[i][1]].Pos[t];  
                     var distance=Math.Abs(word2pos-Word1pos);
                     if(distance<lowDistance)
                     {lowDistance=distance;}      
                     if(word2pos>Word1pos)
                     {k++;}
                     else t++;
                    }while(k<Document.Value[words[i][0]].Pos.Count && t<Document.Value[words[i][1]].Pos.Count);
                     
                    DistanceWords.Add(lowDistance,Document.Key);
                    support.Add(lowDistance);
                  } 
                   vueltas++;
                }
                int[] order=new int[support.Count]; 
                int counter=0;
                foreach(int item in support)
                {
                  order[counter]=item;
                  counter++;
                }
                 System.Array.Sort(order);
                int rate=100;
                for(int Q=0;Q<order.Length;Q++)
                {   if(rate==0){break;}
                    var Document=DistanceWords[order[Q]];
                    Score[Document]+=(Score[Document]*rate)/100;
                    rate-=5;
                }
            }
        }
        static string[] SearchWord ( string query,Dictionary<char,MetaData> Operators, char C )
        {
            var ocurrence=Operators[C].ocurrence-1;
            string[] Words=new string[ocurrence];
            string SubQuery;
            string word="";
            query+='.';
            List<string>[] DuoWords =new List<string>[ocurrence];
            var DQuery=QueryDocument.QDocument(query).Item1;
            
            
            
            
            for(int i=0;i<ocurrence;i++)
            {  
               
               var pos= Operators[C].Pos[i];
               var x=query.Length-pos;
                   
                    SubQuery=query.Substring(pos,x);
                    foreach(char letters in SubQuery)
                    {
                        bool isletter=char.IsLetter(letters);
                        if(!isletter)
                        {
                            if(word.Length==0){continue;}
                            
                            Words[i]=word;
                            break;
                        }
                        word+=letters;
                    } 
                    word="";
            }
             
            return Words;       
        }
        static public List<string>[] SearchParticularWords(string query,Dictionary<char,MetaData> Operators, char C )
        {
            var ocurrence=Operators[C].ocurrence-1;
            string[] Words=new string[ocurrence];
            string SubQuery;
            string word="";
            List<string>[] DuoWords =new List<string>[ocurrence];
            var DQuery=QueryDocument.QDocument(query).Item1;
              
            for(int i=0;i<ocurrence;i++)
            {  
               DuoWords[i]=new List<string>();
               var pos= Operators[C].Pos[i];
               var x=query.Length-pos;

                   for(int k=pos-1;;k--)
                   {     
                       if(char.IsWhiteSpace(query[pos-1]))
                         {k--;}
                       if(!char.IsLetter(query[k]))
                         {break;}
                       word=query[k]+word;
                       if( k==0){break;}
                   }
                   DuoWords[i].Add(word);
                   word="";
            
                    SubQuery=query.Substring(pos,x);
                    foreach(char letters in SubQuery)
                    {
                        bool isletter=char.IsLetter(letters);
                        if(!isletter)
                        {
                            if(word.Length==0){continue;}
                            
                             DuoWords[i].Add(word);
                             break;    
                        }
                        word+=letters;
                    } 
                    word="";
            }
           return DuoWords;  
        }

    } 

}
