namespace MoogleEngine
{
    class Items
    {
        static public SearchItem[] Getitems(Dictionary<string,string> GSnippet,Dictionary<String,double> score, List<string> ScoreRanking)  
        {
         int x= ScoreRanking.Count;
         SearchItem[] items=new SearchItem[x];
         int i=0;
         foreach(string Ranking in ScoreRanking)
         {
          items[i]=new SearchItem(Ranking,GSnippet[Ranking],(float)score[Ranking]);
          i++;
         }
         return items;
        }
    }   
}
