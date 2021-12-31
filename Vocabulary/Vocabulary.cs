using System;
namespace Vocabulary;
public class Vocabulary
{
    string[] documents = Directory.GetFiles(@"G:\Study\!!!!Matcom\Prog\Programas\prueba proyecto","*.txt");
    public Vocabulary(Dictionary<string,int> words){
       this.words = words; 
    }
    
         Dictionary<string,int> words =  new Dictionary<string, int>();
    void GetWords()
    {
        
       for(int i = 0; i < documents.Length;i++)
       {
        StreamReader reader =  new StreamReader(documents[i]);
        string[] record = reader.ReadToEnd().Replace('\n',' ').Split(' ');
         foreach(var word in record)
         {
             if(words.ContainsKey(word))
             {
                 words[word]++;
             }
             else
             {
                 words.Add(word,1);
             }
         }
       }
    }


}