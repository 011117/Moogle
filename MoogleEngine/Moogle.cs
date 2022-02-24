using Vocabulary1;
using Matrixes;
using Document;
using Vectors;
using LD;
namespace MoogleEngine;


public static class Moogle
{
   public static Vocabulary seeker;
    public static SearchResult Query(string query) {
        // Modifique este método para responder a la búsqueda
         
          Query final_query = new Query(query);
          SearchItem[] items =  new SearchItem[seeker.documents.Length];
          float[] scores = seeker.CalculateScores(final_query);
          string[] query_levensthein = final_query.GetTerms().Keys.ToArray<string>();
          System.Console.WriteLine("*"+query_levensthein[0]);
          for(int i = 0 ; i < seeker.documents.Length ; i++)
          {
              items[i] = new SearchItem(seeker.documents[i].Name,seeker.documents[i].Snippet,scores[i]);
          }    
         return new SearchResult(items,LevenstheinDistance.Corrector(query_levensthein,seeker.documents,Moogle.seeker));
    }
}
