namespace Matrix;
public  class Matrix
{
    #region Constructors
    private float[,] elements;
    private int rows,columns;
    public int Rows {get {return this.rows;}}
    public int Columns {get{return this.columns;}}
    //Constructor publico 
    public Matrix(float[,] elements){
        if(elements == null)
        {
            throw new Exception("Matrix can't be null");
        }
       this.elements = elements;
       this.rows = elements.GetLength(0);
       this.columns = elements.GetLength(1);
    }
    //Constructor de una matriz de ceros para poder rellenar
    public Matrix(int rows,int columns){
        this.elements = new float[rows,columns];
    }
    #endregion
    #region Iterator

    public float this[int r,int c]{
        get{
          return this[r,c];
        }
        set{
            this[r,c] = value;
        }
    }
    #endregion
    #region CheckNUll
    private static void CheckNull(Matrix matrix){
       if(matrix.Equals(null))
       {
           throw new Exception("Matrix can't be null");
       }
    }
    #endregion
    #region Methods

    private static bool SumDimensionVerification(Matrix matrix1,Matrix matrix2)
    {
           if(matrix1.Rows != matrix2.Rows || matrix1.Columns != matrix2.Columns)
           {
               return true;
           }
           return false;
    }
    private static bool ProductDimensionVerification(Matrix matrix1,Matrix matrix2)
    {
          if(matrix1.Rows == matrix2.Columns)
          {
            return true;
          }
          return false;
    }
    private static Matrix Sum(Matrix matrix1,Matrix matrix2)
    {
        CheckNull(matrix1);
        CheckNull(matrix2);
        float[,] result = new float[matrix1.Rows,matrix1.Columns];
        if(SumDimensionVerification(matrix1,matrix2))
        {
         for(int i = 0 ; i < matrix1.Rows ; i++)
         {
             for(int j = 0; j < matrix1.Columns ; j++)
             {
                result[i,j] = matrix1[i,j] + matrix2[i,j];
             }
         }
         
        }
        else
        {
          throw new Exception("Not Same Dimension");
        }
        return new Matrix(result);
    }
    private static Matrix DotProduct(Matrix matrix1,Matrix matrix2)
    {
        CheckNull(matrix1);
        CheckNull(matrix2);
         float[,] result =  new float[matrix1.Rows,matrix2.Columns];
        if(ProductDimensionVerification(matrix1,matrix2)){
        for(int i = 0; i < matrix1.Rows ; i++)
        {
            for(int j =0 ; j < matrix2.Columns ; j++)
            {
                for(int k = 0 ; k < matrix1.Columns ; k++){
                    result[i,j] += matrix1[i,k] * matrix2[k,j];
                }
            }
        }
        }
        else
        {
            throw new Exception("Dimensions are not valid");
        }
        return new Matrix(result);
    }
    private static Matrix ScalarProduct(Matrix matrix,float scalar){
        CheckNull(matrix);
        float[,] result =  new float[matrix.Rows,matrix.Columns];
        for(int i = 0 ; i < matrix.Rows ; i++)
        {
            for(int j = 0 ; j < matrix.Columns ; j++)
            {
                result[i,j] = scalar * matrix[i,j]; 
            }
        }
        return new Matrix(result);
    }
    public static bool Equals(Matrix matrix1,Matrix matrix2)
    {
        if(!SumDimensionVerification(matrix1,matrix2))
        {
            return false;
        }
        for(int i = 0; i < matrix1.Rows ; i++)
        {
            for(int j = 0 ; j < matrix1.Columns ; j++)
            {
                if(matrix1[i,j] != matrix2[i,j])
                {
                    return false;
                }
            }
        }
      return true; 
    }
    #endregion
    #region Operators
     public static Matrix operator +(Matrix matrix1,Matrix matrix2)
     {
         return Sum(matrix1,matrix2);
     }
     public static Matrix operator *(Matrix matrix1,Matrix matrix2)
     {
         return DotProduct(matrix1,matrix2);
     }
     public static Matrix operator *(Matrix matrix,float scalar){
         return ScalarProduct(matrix,scalar);
     }
      public static Matrix operator *(float scalar,Matrix matrix){
         return ScalarProduct(matrix,scalar);
     }
     public static Matrix operator -(Matrix matrix1,Matrix matrix2)
     {
         return matrix1 + (matrix2 * -1);
     }
     public static bool operator ==(Matrix matrix1,Matrix matrix2)
     {
         return Equals(matrix1,matrix2);
     }
     public static bool operator !=(Matrix matrix1,Matrix matrix2)
     {
         return Equals(matrix1,matrix2);
     }
    #endregion
}
