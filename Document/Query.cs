namespace Document;

public class Query : IGetTerms
{
    private Dictionary<string,int> tokens; //aqui guardamos la cantida de veces que sale la palabra
    private Dictionary<string,List<String>> operators;// aqui guardamos para cuada operador cuantas palabras modifica
    public Query(string query)
    {
        operators = new Dictionary<string, List<string>>();// inicializamos 
        tokens =  new Dictionary<string, int>(); // inicializamos
        string[] words = query.ToLower().Replace('.',' ').Replace(',',' ').Replace('\n',' ').Split(" ");//llevamos todo a minuscula,quitamos los puntos y las comas y los saltos de lineas
        foreach (var word in words)//recorremos el array de las palabras
        {
             int position = GetOperators(word);//vemos si hay operadores y en que posicion
             if( position != -1)//si no es -1
		  {
			string key = word.Substring(0,position+1);//me quedo con el operador 
           
			string Value = word.Substring(position+1);//me quedo con la palabra
            
           
			if(operators.ContainsKey(key) && !operators[key].Contains(Value))//para cada operador
			{                                                               //veo si ya tiene la palabra en caso contrario la agrego
				operators[key].Add(Value);
			}else
			{
				operators.Add(key,new List<string>());	
				operators[key].Add(Value);
			}
            
		  }
          int pos = 0;//-------------------------------
          for(int i=0;i<word.Length;i++)//             |
          {//                                          |
              if(!IsOperator(word[i]))//               |
              {//                                      |
                  pos = i;//                           |
                  break;//                             |
              }//                                      |
          }//                                          |____ 
          string word_final = word.Substring(pos);//        \  esto lo hago para el caso de que pongas  
                                               //       ____/ mas de un operador por palabra como es el 
            if(tokens.ContainsKey(word_final))//       |      caso de los asteriscos quedarme con la palabra
            {//                                        |
                tokens[word_final]++;//                |
            }//                                        |
            else//                                     |
            {//                                        |
                tokens.Add(word_final,1);//            |
            }//----------------------------------------
        }
    }
    public Dictionary<string,List<string>> Operators {get{return this.operators;}}//propiedad de los operadores
    public static int GetOperators(string text)
    {
        if(text.Length <=2) // si es un operador y una letra o un operador vacio es absurdo
        {
            return -1;
        }
          if(IsOperator(text[0]))
          {
              for(int i=0;i<text.Length;i++)
              {
                  if(!IsOperator(text[i]))//si no es operador retorna la posicion
                  {

                     return i-1;
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
    public  float Higher_TF()
    {
        float x =int.MinValue;
        foreach(var word in this.tokens.Keys)
        {
            x = Math.Max(x,this.tokens[word]);
        }
        return x;
    }
    public Dictionary<string,int> GetTerms()
    {
        
       return this.tokens;  
             
    }
}