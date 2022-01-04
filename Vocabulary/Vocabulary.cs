using Document;
using Matrixes;
using Vectors;

namespace Vocabulary1;
public class Vocabulary
{
            
    private Dictionary<string,int> Terms;
    private Documents[] documents;
    private Matrix system_matrix;

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
       this.system_matrix = CreateSystemMatrix(documents);        

    }
    private Vector Vectorize(IGetTerms elements)
    {
        float[] final_vector =  new float[Terms.Count];
        int iterator = 0;
        foreach(var word in Terms)
        {
            if(elements.GetTerms().ContainsKey(word.Key))
            {
              final_vector[iterator] = (float)elements.GetTerms()[word.Key] * Calculate_IDF(word.Key); 
            }
            iterator++;
        }

        return new Vector(final_vector);
    }
    private Matrix CreateSystemMatrix(Documents[] documents)
    {
       float[,] matrix_result = new float[documents.Length,this.Terms.Count];
       for(int i = 0 ; i < documents.Length ; i++)
       {
           var  document_vector = Vectorize(documents[i]);
           for(int j = 0 ; j < document_vector.Size ; j++)
           {
             matrix_result[i,j] = document_vector[j];
           }

       }
       return new Matrix(matrix_result);
    } 
    public float CalculateScore(int index,Query query)
    {
        float score = 0;
      
        score = Vectorize(documents[index]) * Vectorize(query);
    
      return score;
    }
    
    private float Calculate_IDF(string word)
    {
       return (float)Math.Log10(documents.Length/documents.Select(elem=>elem.GetTerms().ContainsKey(word)).Count());
    }
}