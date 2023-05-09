namespace MoogleEngine
{
    class QueryDocument
    {    
       public static (Dictionary<string,MetaData>,Dictionary<char,MetaData>) QDocument(string query)
       {
        Dictionary<string,MetaData> Query = new Dictionary<string, MetaData>();
        Dictionary<char,MetaData> Operators= new Dictionary<char, MetaData>();
        string words="";
        
        Operators.Add('!', new MetaData());
        Operators.Add('^',new MetaData());
        Operators.Add('~',new MetaData());
        Operators.Add('*',new MetaData());
         query+='.';
        int i=0;
        foreach(char letters in query )

        {
            bool IsLetter;
            IsLetter= char.IsLetter(letters);
            
            if(!IsLetter)
            {
                if(Operators.ContainsKey(letters))
                {
                    Operators[letters].ocurrence++;
                    Operators[letters].Pos.Add(i);
                }
                
                if(words.Length==0){ i++; continue;}
                
                words= words.ToLower();
                if(Query.ContainsKey(words))
                {
                   
                    Query[words].ocurrence++;
                }
                else 
               
                Query.Add(words,new MetaData());
                Query[words].Pos.Add(i);
                      words="";
                
            }
             else  words+= letters;
            
            i++;
        }
        return (Query,Operators);
       }
    }
}
