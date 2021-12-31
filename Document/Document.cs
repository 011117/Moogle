namespace Document;
public class Document : IGetTerms
{
    private string path;
    private string name;
    private string snippet;
    private Dictionary<string,int> tokens;
   
    public string Name
    {
        get
        {
            return this.name;
        }
        
    }
    public string Snippet
    {
        get
        {
            return this.snippet;
        }
    } 
    public Dictionary<string,int> GetTerms()
    {
          return this.tokens;
    }
    public Document(string path)
    {
        tokens =  new Dictionary<string, int>();
       StreamReader reader = new StreamReader(path);
       this.name = path.Split("/")[2];
       string lecture = reader.ReadToEnd();
       this.snippet = lecture.Substring(0,100) + "...";
       string[] words = lecture.Replace('\n',' ').Replace(',',' ').Replace('.',' ').Split(" ");
       foreach(var word in words)
       {
         if(tokens.ContainsKey(word)){
             tokens[word]++;
         }
         else
         {
             tokens.Add(word,1);
         }
       }
    }



}
