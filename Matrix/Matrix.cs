namespace Matrix;
public  class Matrix
{
    private float[,] elements;
    private int rows,columns;
    
    public Matrix(float[,] elements){
       this.elements = elements;
       this.rows = elements.GetLength(0);
       this.columns = elements.GetLength(1);
    }
}
