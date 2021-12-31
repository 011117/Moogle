namespace Vectors;
public class Vector
{
    #region Constructors

     float[] elements;
     
     public int Size
     {
       get{ return this.elements.Length; }
     }
     public Vector(float[] elements)
     {
         if(elements == null)
         {
             throw new Exception("Vector can't be null");
         }
       this.elements = elements;
     } 
     public Vector(int size)
     {
        this.elements = new float[size];
     }
     #endregion
    #region Iterator
    public float this[int index]{
       get{
           if(index < this.Size && index > 0)
           {
              return elements[index];
           }
           else
           {
             throw new Exception("Index out of the range");  
           }
       }
       set{
           if(index < this.Size && index > 0)
           {
           elements[index] = value;
           }
           else
           {
               throw new Exception("Index out of the range");
           }
       }
    }
   #endregion
    #region CheckNULL
      private static void CheckNull(Vector vector)
      {
          if(vector == null)
          {
              throw new Exception("Vector can't be null");
          }
      }
      
     #endregion
    #region Methods
    private static bool CheckDimensions(Vector vector1,Vector vector2){
        if(vector1.Size == vector2.Size)
        {
            return true;
        }
        return false;
    }
     private static Vector Sum(Vector vector1,Vector vector2)
     {
         CheckNull(vector1);
         CheckNull(vector2);
         float[] result =  new float[Math.Max(vector1.Size,vector2.Size)];
         for(int i = 0 ; i < result.Length; i++)
         {
             result[i] = vector1[i]+ vector2[i];
         }
         return new Vector(result);
     }
     private static float DotProduct(Vector vector1,Vector vector2)
     {
         CheckNull(vector1);
         CheckNull(vector2);
         float result=0;
         if(CheckDimensions(vector1,vector2))
         {
         for(int i = 0; i < vector1.Size ; i++)
         {
             result += vector1[i] * vector2[i];
         }
         }
          else
          {
              throw new Exception("Not Same Size");
          }
        return result;
     }
     public static Vector ScalarProduct(float scalar,Vector vector)
     {
        float[] result = new float[vector.Size];
        for(int i = 0; i < vector.Size; i++)
        {
            result[i] = scalar * vector[i];
        }
        return new Vector(result);
     }
    #endregion
    #region Operators
     public static Vector operator +(Vector vector1,Vector vector2)
     {
         return Sum(vector1,vector2);
     }
     public static Vector operator -(Vector vector1,Vector vector2)
     {
         return vector1 + (vector2 * -1);
     }
     public static float operator *(Vector vector1,Vector vector2)
     {
         return DotProduct(vector1,vector2);
     }
     public static Vector operator *(float scalar,Vector vector)
     {
         return ScalarProduct(scalar,vector);
     }
     public static Vector operator *(Vector vector, float scalar)
     {
         return ScalarProduct(scalar,vector);
     }
    #endregion
}
