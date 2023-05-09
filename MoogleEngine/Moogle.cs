namespace MoogleEngine
{
    public static class Moogle
{
    public static SearchResult Query(string query, PlainLoader database) 
    {
        var Tupla1 = database.Information ;
        var Documents = Tupla1.Item1;
        var TWords =Tupla1.Item2;
        var Tupla2 = QueryDocument.QDocument(query);
        var Query = Tupla2.Item1;
        var Operators = Tupla2.Item2;
        var Tupla3=Suggestion.suggestion(Query,TWords,query);
        var Suggest=Tupla3.Item1;
        Base.ReviseQuery( Query,Tupla3.Item2);
        Base.Filter(Operators, Documents,query,  TWords);
        var Scores = Score.score(Query,Documents,TWords,Operators,query);
        var SRanking = Base.ScoreRanking(Scores);
        var Snippets = Snippet.GSnippet(Query,Documents,TWords,Scores,SRanking);
        var items = Items.Getitems(Snippets,Scores,SRanking);
        

        return new SearchResult(items, Suggest);
    }
   

}
}
