namespace Document;

public class Query : IGetTerms
{
    private Dictionary<string,int> tokens;
    public Query(string query)
    {
        tokens =  new Dictionary<string, int>();
        string[] words = query.Replace('\n',' ').Replace('.',' ').Replace(',',' ').Split(" ");
        foreach (var word in words)
        {
            if(tokens.ContainsKey(word))
            {
                tokens[word]++;
            }
            else
            {
                tokens.Add(word,1);
            }
        }
    }
    public Dictionary<string,int> GetTerms()
    {
       return this.tokens;        
    }
}