# Moogle!

![](moogle.png)

> Proyecto de Programación I. Facultad de Matemática y Computación. Universidad de La Habana. Curso 2021.

Moogle! es una aplicación *totalmente original* cuyo propósito es buscar inteligentemente un texto en un conjunto de documentos.
Utiliza un Sistema de Recuperación de la Información en el cual esta implementado un modelo vectorial para realizar las busquedas.
Sobre la Busqueda:
En general se puede buscar una frase cualquiera, el motor de busqueda no esta limitado a tan solo una palabra. Los documentos como es lógico,
no contienen todas las palabras, pero la aplicación es capaz de sugerirle una palabra lo bastante parecida a la que desea el usuario, 
esta palabra si se encuentra en los documentos. El buscador ignora las palabras que aparecen demasiado y en muchos documentos como lo 
son las preposiciones ya que estas palabras no ofrecen valor en una frase. La aplicacion ofrece una serie de operadores los cuales se explican 
a continuación:
## Operadores
- -- ! -- si este operador está delante de una palabra esta palabra no debe aparecer en los resultados.
- -- ^ -- este operador obliga a que la palabra aparezca en los resultados.
- -- * -- este aumenta la prioridad(valor) de la palabra, y son acumulativos.
- -- ~ -- este operador es entre dos palabras e implica que mientras mas cercanas son las palabras mas prioridad debe tener el documento.

## Determinando la similitud entre la consulta del usuario y los documentos:
Primeramente cargamos todos los documentos y calculamos el TF(frecuencia de aparición) de las palabras en cada documento y luego con 
todos los documentos creamos un vocabulario para saber todas las palabras que tenemos y su frecuencia en cada documento. Luego 
calculamos el IDF que no es más que la importancia de la palabra en nuestro corpus(vocabulario) y con estos valores creamos 
nuesto sistema de vectores, esto mismo hacemos con la consulta(query) del usuario. Después aplicamos la distancia coseno entre 
el vector query y los vectores de los documentos que nos dice cuán parecidos son estos y al valor resultante le llamamos score del documento
con respecto a la query, a mayor score mayor es la semejanza entre la query y el documento ,por lo tanto mayor prioridad del documento en los resultados. 

## Método de busqueda:
Luego de haber calculado los `TF-IDF` de las palabras de los documentos y de la query, teniendo ya el sitema de vectores y la query vectorizada 
también en termino de los documentos, como dije anteriormente hallamos la distancia coseno entre la query y los documentos para obtener el `score` 
de cada documento y así saber la relevancia de cada documento. En la parte visual le mandamos solo los documentos que tengan un `score` mayor que 
cero ya que no nos interesan los documentos con `score` igual a cero pues indica que no tienen niguna semejanza con la query.


## Ingenieria del Sofware:
La aplicación está dividida en dos componentes fundamentales:

- `MoogleServer` es un servidor web que renderiza la interfaz gráfica y sirve los resultados.
- `MoogleEngine` es una biblioteca de clases donde está implementada la lógica del algoritmo de búsqueda.
- Dentro de MoogleEngine creé varias clases para realizar todo el proceso descrito anteriormente.
- `Document`:
- En la clase `Document` procesamos los documentos,primero vemos la ruta en la que se encuntran, sabiendo esto tomamos el nombre del documento y el `snippet(pedazo de texto)`, por cada documento lo leemos completo y nos quedamos solo con las cosas que nos interesan, o sea eliminamos caracteres indeseados digase puntos, comas, finales de línea, etc. Guardamos por cada palabra la cantidad de veces que aparece en el documento(`TF`) y las posiciones en las que aparece para poder realizar el operador `~`. La clase `Document` al igual que la clase `Query` de la cual hablaremos más adelante utilizan una `Interface`(Interfaz) llamada GetTerms, por lo que estas dos clases implementan el método GetTerms para devolver sus términos. La clase `Document` tambien implementa un método llamado Min_Distance:
```cs  
  public int Min_Distance(string a,string b)
    {
          int min_distance = int.MaxValue;
          int[] pos_a = words_positions[a].ToArray();
          int[] pos_b = words_positions[b].ToArray();
          int min  = Math.Min(pos_a.Length,pos_b.Length);
          for(int i = 0;i<min ;i++)
          {
             if(Math.Abs(pos_a[i]-pos_b[i]) < min_distance)
             {
                 min_distance = Math.Abs(pos_a[i]-pos_b[i]);
             }
          }
          return min_distance;
    }
```


         
