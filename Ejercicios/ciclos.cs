// namespace Etapa1.Ejercicios
// {
//     public class ciclos
//     {
//         private static void ImprimirCursosWhile(Curso[] arregloCursos)
//     {
//       int contador = 0;
//       while (contador < arregloCursos.Length)
//       {
//         System.Console.WriteLine($"Nombre {arregloCursos[contador].Nombre}, ID: {arregloCursos[contador].UniquedId}");
//         contador++;
//       }
//     }

//     private static void ImprimirCursosDoWhile(Curso[] arregloCursos)
//     {
//       int contador = 0;
//       do
//       {
//         System.Console.WriteLine($"Nombre {arregloCursos[contador].Nombre}, ID: {arregloCursos[contador].UniquedId}");
//         contador++;
//       } while (contador < arregloCursos.Length);
//     }

//     private static void ImprimirCursosFor(Curso[] arregloCursos)
//     {
//       for (int i = 0; i < arregloCursos.Length; i++)
//       {
//         System.Console.WriteLine($"Nombre {arregloCursos[i].Nombre}, ID: {arregloCursos[i].UniquedId}");
//         // System.Console.WriteLine($"Nombre {arregloCursos[contador].Nombre}, ID: {arregloCursos[contador].UniquedId}");


//       }
//     }

//     private static void ImprimirCursosForeach(Curso[] arregloCursos)
//     {
//       foreach (var item in arregloCursos)
//       {
//         System.Console.WriteLine($"Nombre {item.Nombre}, ID: {item.UniquedId}");
//       }
//     }
//     }
// }