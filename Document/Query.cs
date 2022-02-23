namespace Document;

public class Query : IGetTerms
{
    private Dictionary<string,int> tokens;
    private Dictionary<string,List<String>> operators;
    public Query(string query)
    {
        operators = new Dictionary<string, List<string>>();
        tokens =  new Dictionary<string, int>();
        string[] words = query.ToLower().Replace('.',' ').Replace(',',' ').Replace('\n',' ').Split(" ");
        foreach (var word in words)
        {
             int position = GetOperators(word);
             if( position != -1)
		  {
			string key = word.Substring(0,position+1);
			string Value = word.Substring(position+1);
            
            System.Console.WriteLine("operador" + key);
            System.Console.WriteLine("Valor"+ Value);
			if(operators.ContainsKey(key) && !operators[key].Contains(Value))
			{
				operators[key].Add(Value);
			}else
			{
				operators.Add(key,new List<string>());	
				operators[key].Add(Value);
			}
            
		  }
          int pos = 0;
          for(int i=0;i<word.Length;i++)
          {
              if(!IsOperator(word[i]))
              {
                  pos = i;
                  break;
              }
          }
          string word_final = word.Substring(pos);
          System.Console.WriteLine(word_final);
            if(tokens.ContainsKey(word_final))
            {
                tokens[word_final]++;
            }
            else
            {
                tokens.Add(word_final,1);
            }
        }
    }
    public Dictionary<string,List<string>> Operators {get{return this.operators;}}
    public static int GetOperators(string text)
    {
          if(IsOperator(text[0]))
          {
              for(int i=0;i<text.Length;i++)
              {
                  if(IsOperator(text[i]))
                  {

                     return i;
                  }
              }
          }  
          return -1;
    }
    public static bool IsOperator(char c)
    {
        if(c == '*' || c == '^' || c == '!' || c == '~')return true;
        return false;
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
}