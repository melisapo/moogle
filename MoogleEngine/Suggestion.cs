namespace MoogleEngine
{

    public class Suggestion
    {
       
        public static (string,Dictionary<string,string>) suggestion(Dictionary<string,MetaData> Query,Dictionary<string,MetaData> Twords,string query)
        {
            var changeWord= new Dictionary<string,string>();
            var Suggest="";
            foreach(KeyValuePair<string,MetaData> WQuery in Query)
            {
                 
                if(Twords.ContainsKey(WQuery.Key))
                    {
                       
                        continue;   
                    }
                DiferentWord(WQuery,Twords,ref changeWord);
               
            }

            MakeSuggest(changeWord,Query,query,ref Suggest);
              
             
            return (Suggest,changeWord);
        }
       
            static int LD(string Query, string Word)
        {   
            
            int[,] Memory = new int[Query.Length + 1, Word.Length + 1];
            int Change = 0;
            if ((Query.Length == 0) && (Word.Length == 0)) return 0;
            for (int i = 0; i <= Query.Length;  i++) 
            {Memory[i, 0] =i; } 
            for (int i = 0; i <= Word.Length;  i++) 
            {Memory[0, i] =i;}
            for (int i = 1; i <= Query.Length; i++)
                for (int j = 1; j <= Word.Length; j++)
                {
                    if (Query[i - 1] == Word[j - 1]) Change=0 ;
                    else Change=1;
                    Memory[i, j] = Math.Min(Math.Min(Memory[i - 1, j] + 1, Memory[i, j - 1] + 1),Memory[i - 1, j - 1] + Change);
                }
            return Memory[Query.Length,Word.Length];
        }
       
        
         static void DiferentWord (KeyValuePair<string,MetaData> WQuery,Dictionary<string,MetaData> Twords,ref Dictionary<string,string> change)
        {
            var Distance= double.MaxValue;               
                foreach(KeyValuePair<string,MetaData> ListWord in Twords)
                {  int x= WQuery.Key.Length;
                   int y= ListWord.Key.Length;
                   int z= Math.Abs(x-y);
                    
                  

                        var  Dist= LD(WQuery.Key,ListWord.Key);
                        if(Dist<Distance)
                        {
                            Distance=Dist;
                            if(change.ContainsKey(WQuery.Key))
                            {
                                change[WQuery.Key]=ListWord.Key;
                                continue;
                            }
                            change.Add(WQuery.Key,ListWord.Key);

                        }
                  
                }
        }
        static void MakeSuggest( Dictionary<string,string> change,Dictionary<string,MetaData> Query,string query, ref string Suggest)
        {   
            
           query+='.';
            var word="";
           foreach(char Char in query) 
           { 
            var IsLetter= char.IsLetter(Char);
             if(!IsLetter)
             {  
                 if(change.ContainsKey(word))
                 {
                     word=change[word];
                 }
                 if(!char.IsWhiteSpace(Char))
                 {
                     word+=Char;
                 }
                 Suggest+=' '+word;
                 word="";
             }         
             else  word+=Char;  
            
           }
            
            
        }
    
      
    } 
}