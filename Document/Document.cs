namespace Document;

public class Documents :IGetTerms
{
    
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
    public Dictionary<string,int> Terms
    {
        get
        {
       return this.tokens;  
        }      
    }
    public Dictionary<string,int> GetTerms()
    {
        
       return this.tokens;  
             
    }
    public Documents(string path)
    {
        tokens =  new Dictionary<string, int>();
       StreamReader reader = new StreamReader(path);
       this.name = path.Split("/")[2];
       string lecture = reader.ReadToEnd().ToLower();
       this.snippet = (lecture.Length > 100) ?lecture.Substring(0,60) + "...":lecture.Substring(0,lecture.Length) + "..." ;
       string[] words = lecture.Replace('.',' ').Replace(',',' ').Replace('\n',' ').Split(" ");
       Console.WriteLine("aqui " +words.Length);
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
