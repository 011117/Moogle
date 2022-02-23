using Vocabulary1;
using Document;
namespace LD;
public class LevenstheinDistance
{
   public static string Corrector(string query,Documents[] vocabulary)// Analizar cual es la palabra que mas se parece de 
   {                                                    // las que estan en el documento
       foreach (var document in vocabulary)
       {
         foreach(var word in document.GetTerms().Keys)
         {
             if(LD(query,word) <=2)
             {
                 return word;
             }
         } 
       }
       return query;
   }
  private static int LD(string a, string b)
   {
       int n = a.Length;
       int m = b.Length;
       int[,] dp = new int[n+1,m+1];
       for(int i =0 ; i <= n ; i++)
       {
           dp[i,0] = i;
       }
       for(int i =0 ; i <= m ; i++)
       {
           dp[0,i] = i;
       }
       for(int i=1;i<=n;i++)
       {
           for(int j=1;j<=m;j++)
           {
               if(a[i-1] == b[j-1])
               {
                   dp[i,j] = dp[i-1,j-1];
               }
               else
               {
                 int insert = 1 + dp[i,j-1];
                 int remp = 1 + dp[i-1,j-1];
                 int elim = 1 + dp[i-1,j];

                 dp[i,j] = Math.Min(insert,Math.Min(remp,elim));
               }
           }
       }
       return dp[n,m];
   }
}
