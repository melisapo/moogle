namespace MoogleEngine
{

    public class Base 
    {
        static public void Filter(Dictionary<char,MetaData> Operator,  Dictionary<string,Dictionary<string,MetaData>> Documents,string query,  Dictionary<string,MetaData> TWords)
        {
        var signo1= Operator['*'].ocurrence-1;
        if(signo1>0)
        {Operators.IncreaseRelevance(  Documents,query,Operator, TWords);}
        }
        static public void ReviseQuery( Dictionary<string,MetaData> Query,Dictionary<string,string> ChangeWords)
        {
            foreach(KeyValuePair<string,string> item in ChangeWords)
            {
                if(Query.ContainsKey(item.Value))
                {  
                    Query[item.Value].ocurrence++;
                    Query.Remove(item.Key);
                    continue;
                }
                MetaData  WordInfo ;
                Query.Remove(item.Key, out WordInfo!);
                
                Query.Add(item.Value,WordInfo);
                
            }
        }
        
        
        

         static public List<string> ScoreRanking (Dictionary<string,double> Score)
        {
        var Switch  = Change(Score);
        var ScoreRanking = new List<string>();
        var Classify= List(Switch);
        var promedy= Promedy(Classify);
         
            for(int i=0;i<Switch.Count;i++)
            {
                if(Classify[i]>=promedy)
                {
                ScoreRanking.Add(Switch[Classify[i]]);
                }
                
            }
           
            
          
          return ScoreRanking;
        }
        private static Dictionary<double,string> Change (Dictionary<string,double> Score)
        {
            var change=new Dictionary<double, string>();
            foreach (KeyValuePair<string,double> score in Score)
         {
             change.TryAdd(score.Value,score.Key);      
         }
         return change;
        }
        private static double[] List (Dictionary<double,string> Switch)
        {
             var list = new double[Switch.Count];
             for(int i=0; i<Switch.Count;i++)
            {   double V =0;
                foreach(KeyValuePair<double,string> score in Switch)
                {
                   if(i==0)
                   {
                     if(score.Key>V)
                     {
                       V=score.Key;
                       list[i]=V;
                       
                     }
                      continue;
                   }
                    if( score.Key>V && score.Key<list[i-1])
                   {   
                       V=score.Key;
                       list[i]=V;
                   }
                }
            }
            return list;
        }
        private static double Promedy(double[] BestScore)
        {
            double promedy=0;
            int x=BestScore.Length;
            foreach(double score in BestScore)
            {
             promedy+=score;
            }
            promedy/=x;
            return promedy;
        }






    } 
}