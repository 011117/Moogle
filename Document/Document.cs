namespace Document;

public class Documents :IGetTerms
{
    
    private string name;
    private string snippet;
    private Dictionary<string,int> tokens;
    private Dictionary<string,List<int>> words_positions;
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
        words_positions = new Dictionary<string, List<int>>();
       StreamReader reader = new StreamReader(path);
       this.name = path.Split("/")[2];
       string lecture = reader.ReadToEnd().ToLower();
       int iterator = 0;
       this.snippet = (lecture.Length > 100) ?lecture.Substring(0,60) + "...":lecture.Substring(0,lecture.Length) + "..." ;
       string[] words = lecture.Replace('.',' ').Replace(',',' ').Replace('\n',' ').Split(" ");
       Console.WriteLine("aqui " +words.Length);
       foreach(var word in words)
       {
         if(tokens.ContainsKey(word)){
             tokens[word]++;
             words_positions[word].Add(iterator);
         }
         else
         {
             tokens.Add(word,1);
             words_positions.Add(word,new List<int>());
             words_positions[word].Add(iterator);
         }
         iterator++;
       }
    }
    public int Min_Distance(string a,string b)
    {
          int min_distance = int.MaxValue;
          int[] pos_a = words_positions[a].ToArray();
          int[] pos_b = words_positions[b].ToArray();
          int min  = Math.Min(pos_a.Length,pos_b.Length);
          for(int i = 0;i<min ;i++)
          {
             if(Math.Abs(pos_a[i]-pos_b[i]) < min_distance)
             {
                 min_distance = Math.Abs(pos_a[i]-pos_b[i]);
             }
          }
          return min_distance;
    }


}
