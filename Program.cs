
using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela;
using CoreEscuela.Entidades;
using CoreEscuela.util;

using static System.Console;

namespace Etapa1
{
  class Program
  {
    static void Main(string[] args)
    {

      AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;


      var engine = new EscuelaEngine();
      engine.Inicializar();

      Printer.Title("BIENVENIDOS A LA ESCUALA");

      Dictionary<int, string> diccionario = new Dictionary<int, string>();
      diccionario.Add(10, "Christian");
      diccionario.Add(23, "Lorem Imsup");

      foreach (var keyValPair in diccionario)
      {
        WriteLine($"key: {keyValPair.Key} valor: {keyValPair.Value}");  
      }

      var dictmp = engine.GetDiccionarioDeObjetos();
      // engine.ImprimirDiccionario(dictmp, true);


      var NewEval = new Evaluacion();
      string nombre, notaString;
      float nota;

      WriteLine("Ingrese el nombre de la evaluacion");
      Printer.PresioneEnter();
      nombre = Console.ReadLine();

      if(string.IsNullOrWhiteSpace(nombre)){
        throw new ArgumentException("El valor del nombre no puede ser nullo");
      }else{
        NewEval.Nombre = nombre.ToLower();
        WriteLine("EL nombre de la evaluacion ha sido ingresado correctamente");
      }

      WriteLine("Ingrese la nota de la  evaluacion");
      Printer.PresioneEnter();
      notaString = Console.ReadLine();

      if(string.IsNullOrWhiteSpace(notaString)){
        throw new ArgumentException("El valor del nombre no puede ser nullo");
      }else{
        NewEval.Nota = float.Parse(notaString);
        WriteLine("La nota de la evaluacion ha sido ingresada correctamente");
      }





      // var listaILugar = from obj in listaObjetos
      //                   where obj is ILugar
      //                   select (ILugar) obj;

      // engine.Escuela.LimpiarLugar();

    }

    private static void AccionDelEvento(object sender, EventArgs e)
    {
      Printer.Title("Saliendo");
    }

    private static void imprimirCursoEscuela(Escuela escuela)
    {
      WriteLine("|----- Cursos de Nuestra Escuela -----|");
      if (escuela?.Cursos != null)
      {
        foreach (var curso in escuela.Cursos)
        {
          WriteLine($"Nombre => {curso.Nombre} | ID => {curso.UniqueId} | Jornada => {curso.Jornada}");
        }
      }
      else
      {
        WriteLine("Aun no tenemos cursos Registrados, Pronto");
      }
    }

  }
}
