using Document;
using Matrixes;
using Vectors;

namespace Vocabulary1;
public class Vocabulary
{
            
    private Dictionary<string,int> Terms; //variable para guardar los terminos<palabra,cantidad de veces que aparece>
    public Documents[] documents; // un array con todos los documentos
    private Vector[] vectors_of_documents; // un array para guardar los documentos ya vectorizados
    #region Constructor
    public Vocabulary(Documents[] documents) 
    {
        Terms =  new Dictionary<string, int>();
        this.documents = documents;
        foreach(var document in documents)
        {
            foreach(var term in document.GetTerms())
            {
                if(this.Terms.ContainsKey(term.Key)){
                     Terms[term.Key]+=term.Value;
                }
                else
                {
                    this.Terms.Add(term.Key,term.Value);
                }
            }
        }
        DocumentVectors(documents);
      
    }
   #endregion
   #region Methods
    private void DocumentVectors(Documents[] documents) // llenar los documentos ya vectorizados
    {
       this.vectors_of_documents = new Vector[documents.Length];
       for(int i=0;i<documents.Length;i++)
       {
           vectors_of_documents[i] = Vectorize(documents[i]);
       }
      
    }

    private Vector Vectorize(IGetTerms elements) // Metodo para vectorizar los documentos y la query
    {
        float[] final_vector =  new float[this.Terms.Count];
        int iterator = 0;
        foreach(var word in Terms) // recorro los terminos 
        {
            if(elements.GetTerms().ContainsKey(word.Key))
            {
              final_vector[iterator] = (float)((float)elements.GetTerms()[word.Key]/(float)elements.GetTerms().Count )* Calculate_IDF(word.Key); //guardo en cada posicion el tf-idf
            }
            iterator++;
        }

        return new Vector(final_vector);
    }
   
    public float[] CalculateScores(Query query) // Metodo para calcular los Scores a cada documento,depende de la query
	{
		Vector vector_of_query= Vectorize(query); // vectorizo la query
		float[] results = new float[this.documents.Length]; // array para los resultados
		var operators=query.Operators; // le pido los operadores de la query en caso de que hayan
		for(int i =0 ;i < this.documents.Length ;i++)//recorro los documentos
		{
		results[i]= Vector.CosDistance(this.vectors_of_documents[i],vector_of_query);		
				foreach(var op in operators )//recorro los operadores y ejecuto la accion de cada uno si esta asignado a la palabra
				{
					switch(op.Key[0])
					{
					case '*':
						int multiplicador = op.Key.Length;

						foreach(var opWord in op.Value)
						{
							if(documents[i].Terms.ContainsKey(opWord))
							{
							results[i]+=multiplicador;
							break;
							}
						}


						break;
					case '!':
						foreach(var blockWrd in op.Value)
						{
							if(documents[i].Terms.ContainsKey(blockWrd))
							{
							results[i]=0;
							break;
							}
						}
						break;
					case '^':
						foreach(var blockWrd in op.Value)
						{
							if(!documents[i].Terms.ContainsKey(blockWrd))
							{
							results[i]=0;
							break;
							}
						}
						break;

                       case '~':
                         for(int k=1;k<op.Value.Count;k++)
                         {
                           if(documents[i].Terms.ContainsKey(op.Value[k]) && documents[i].Terms.ContainsKey(op.Value[k-1])){
                               results[i] /= documents[i].Min_Distance(op.Value[k],op.Value[k-1]);
                           }
                         }
                         break;
					}
                    

				}
		}
		return results;
	}
    
    private float Calculate_IDF(string word)//Calcular los Idf
    {
       
        return (float)Math.Log10((float)((float)documents.Length/(float)documents.Where(elem=>elem.GetTerms().ContainsKey(word)).Count()));
        
    }
   #endregion
    public Dictionary<string,int> GetTerms()
    {
        
       return this.Terms;  
             
    }
}