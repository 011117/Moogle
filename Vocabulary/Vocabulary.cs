using Document;
using Matrixes;
using Vectors;

namespace Vocabulary1;
public class Vocabulary
{
            
    private Dictionary<string,int> Terms;
    private Documents[] documents;
    private Vector[] vectors_of_documents;

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

    private void DocumentVectors(Documents[] documents) // llenar los documentos ya vectorizados
    {
       this.vectors_of_documents = new Vector[documents.Length];
       for(int i=0;i<documents.Length;i++)
       {
           vectors_of_documents[i] = Vectorize(documents[i]);
       }
      
    }

    private Vector Vectorize(IGetTerms elements)
    {
        float[] final_vector =  new float[this.Terms.Count];
        int iterator = 0;
        foreach(var word in Terms)
        {
            if(elements.GetTerms().ContainsKey(word.Key))
            {
              final_vector[iterator] = (float)(elements.GetTerms()[word.Key] )* Calculate_IDF(word.Key); 
            }
            iterator++;
        }

        return new Vector(final_vector);
    }
   
    public float[] CalculateScores(Query query)
	{
		Vector vector_of_query= Vectorize(query);
		float[] results = new float[this.documents.Length];
		var operators=query.Operators;
		for(int i =0 ;i < this.documents.Length ;i++)
		{
		results[i]= Vector.CosDistance(this.vectors_of_documents[i],vector_of_query);		
				foreach(var op in operators )
				{
					switch(op.Key[0])
					{
					case '*':
						int multiplicador = op.Key.Length;

						foreach(var opWord in op.Value)
						{
							if(documents[i].Terms.ContainsKey(opWord))
							{
							results[i]+=multiplicador*documents[i].Terms[opWord];
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

					}
				}
		}
		return results;
	}
    
    private float Calculate_IDF(string word)
    {
       
        return (float)Math.Log10((float)((float)documents.Length/(float)documents.Where(elem=>elem.GetTerms().ContainsKey(word)).Count()));
        
    }
}