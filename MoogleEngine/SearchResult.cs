namespace MoogleEngine;

public class SearchResult
{
    private SearchItem[] items;

    public SearchResult(SearchItem[] items, string suggestion="")
    {
        if (items == null) {
            throw new ArgumentNullException("items");
        }
        
        this.items = items;
        this.Suggestion = suggestion;
        if(items.Length >= 2)
        {
        QuickSort(items,0,items.Length-1);
        }
    }

    public SearchResult() : this(new SearchItem[0]) {

    }

    public string Suggestion { get; private set; }

    public IEnumerable<SearchItem> Items() {
        return this.items.Where(elem=>elem.Score>0);
    }

    public int Count { get { return this.items.Length; } }
    
     private void QuickSort(SearchItem[] items,int s,int e)
     {
         if(s >= e) return;
         float pivote = items[s].Score;
         int pos = Particiona(items,s,e,pivote);
         QuickSort(items,s,pos);
         QuickSort(items,pos+1,e);

     }
     private int Particiona(SearchItem[] items,int s,int e,float pivote)
     {
         int i = s-1;
         int j = e+1;
         while(true)
         {
             do i++;while(items[i].Score > pivote);
             do j--;while(items[j].Score < pivote);
             if(i >= j)return j;
             var aux = items[j];
             items[j] = items[i];
             items[i] = aux;
          }
     }

    private void QuickSort(int start,int end){
	
    }
}
