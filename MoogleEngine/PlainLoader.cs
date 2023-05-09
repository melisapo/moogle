using System;
using System.Text;

namespace MoogleEngine
{
    public class  PlainLoader
    {
        public (Dictionary<string,Dictionary<string,MetaData>>,Dictionary<string,MetaData>) Information ;
        public PlainLoader()
        {
            Information = Loader();
        }
        
        public static (Dictionary<string,Dictionary<string,MetaData>>,Dictionary<string,MetaData>) Loader()
        { 
               
            String[] ListDocument = LoaderDocument();
            
            int TotalDocument= ListDocument.Length;
            Dictionary<string,Dictionary<string,MetaData>>Documents= new Dictionary<string, Dictionary<string, MetaData>>();  
            Dictionary<string,MetaData>[] Words= new Dictionary<string, MetaData>[TotalDocument]; 
            Dictionary<string,MetaData> TWords= new Dictionary<string, MetaData>();  
                int i =0;
                foreach(string Document in ListDocument)
                {
                    Words[i]= new Dictionary<string, MetaData>();
                    StreamReader listword = new StreamReader (Document);
                    string Word= "";
                    int j=0;
                    
                    foreach(Char letters in listword.ReadToEnd())
                    {
                        
                        bool isletter;
                        isletter= char.IsLetter(letters);
            
                        if(!isletter)
                        {
                            if(Word.Length==0)
                            {
                                j++;
                                continue;
                            }
                            
                            Word=Word.ToLower();
      
                            if(Words[i].ContainsKey(Word))
                            {
                            Words[i][Word].ocurrence+=1;
                          
                            }
                            else
                             {
                                  
                               Words[i].TryAdd(Word, new MetaData());
                               if(TWords.ContainsKey(Word))
                               {
                                   TWords[Word].TotalOcurrence++;

                               }  
                                else 
                                {
                                    TWords.TryAdd(Word,new MetaData());
                                    
                                }
                             }
                            Words[i][Word].Pos.Add(j);
                            Word="";
                            
                        }
                        
                        else   Word+=letters;
                        j++;
                    }
                    i++;
                    
                }     
                 PoorRelevance(TWords,Words);
                for(int k=0;k<TotalDocument;k++)
                {
                    Documents.TryAdd(ListDocument[k],Words[k]);
                   
                }

            return (Documents,TWords);
        }
        public static string[] LoaderDocument()
        {
              string Content= @"../Content";
              string[] ListDocument=Directory.GetFiles(Content);
              return ListDocument;
        }
        static void PoorRelevance (Dictionary<string,MetaData> TWords,Dictionary<string,MetaData>[] Words)
        {
          List<string> TrashWord=new List<string>();
          
          foreach( KeyValuePair<string,MetaData> word in TWords)
          {
           var TotalOcurrence=word.Value.TotalOcurrence;
           var TotalDocument=Words.Length;
            double Rate=(TotalOcurrence*100)/TotalDocument;
            if(Rate>=80.00)
            {
              TrashWord.Add(word.Key);
            //   TWords.Remove(word.Key) ;
            }
          } 
          foreach(string Item in TrashWord)
          {
            foreach(Dictionary<string,MetaData> words in Words)
            {
                if(words.ContainsKey(Item))
                {
                    words.Remove(Item);
                    
                }
            }


          }
        }

  }
}