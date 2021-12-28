
namespace MoogleEngine;


public static class Moogle
{
    public static string[] ReadFolder(){
      
      string[] files = Directory.GetFiles(@"G:\Study\!!!!Matcom\Prog\moogle-2021 (copia 5)\Content","*.txt");
        
        return files;
    }
    public static SearchResult Query(string query) {
        // Modifique este método para responder a la búsqueda
       SearchItem[] element = new SearchItem[ReadFolder().Length];
                
        for(int i = 0;i<element.Length;i++){
            element[i] = new SearchItem(ReadFolder()[i],"snippet",0.9f);
        }    
        
            
        
        
                  
            
        
        

        return new SearchResult(element, query);
    }
}
