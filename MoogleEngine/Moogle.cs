using Vocabulary1;
using Matrixes;
using Document;
using Vectors;
namespace MoogleEngine;


public static class Moogle
{
   
    public static SearchResult Query(string query) {
        // Modifique este método para responder a la búsqueda
          var docs = Directory.GetFiles("../Content","*.txt");
          Documents[] documents =  new Documents[docs.Length];
          for(int i = 0 ; i < docs.Length ; i++)
          {
              documents[i] = new Documents(docs[i]);
          }
          Vocabulary vocabulary =  new Vocabulary(documents);
          Query final_query = new Query(query);
          SearchItem[] items =  new SearchItem[docs.Length];
          for(int i = 0 ; i < docs.Length ; i++)
          {
              items[i] = new SearchItem(documents[i].Name,documents[i].Snippet + vocabulary.CalculateScore(i,final_query),vocabulary.CalculateScore(i,final_query));
          }    
         return new SearchResult(items, query);
    }
}
