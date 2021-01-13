using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;

namespace CoreEscuela
{
  public sealed class EscuelaEngine
  {
    public EscuelaEngine(Escuela escuela)
    {
      this.Escuela = escuela;

    }
    public Escuela Escuela { get; set; }
    public EscuelaEngine()
    {

    }
    public void Inicializar()
    {

      Escuela = new Escuela(
        "Platzi Academy", 2012, TiposEscuela.Secundaria, pais: "Canada", ciudad: "Cartagena"
      );

      CargarCursos();
      CargarAsignaturas();
      CargarEvaluaciones();
    }

    public void ImprimirDiccionario(Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> dic, bool imprEval = false)
    {
      foreach (var objDic in dic)
      {
        Console.WriteLine(objDic);

        foreach (var val in objDic.Value)
        {
          switch (objDic.Key)
          {
            case LlavesDiccionario.Evaluaciones:
              if(imprEval){
                Console.WriteLine(val);
              }
              break;
            case LlavesDiccionario.Escuela:
              if(imprEval){
                Console.WriteLine("Escuela: " + val);
              }
              break;
            case LlavesDiccionario.Alumnos:
              if(imprEval){
                Console.WriteLine("Alumnos: " + val.Nombre);
              }
              break;
            case LlavesDiccionario.Cursos:
              var curtmp = val as Curso;
              if(curtmp != null){
                int count = curtmp.Alumnos.Count;
                Console.WriteLine("Cursos: " + val.Nombre + "Cantidad Alimnos: " + count);
              }
              break;
              
            default:
              Console.WriteLine(val);
              break;
          }
          Console.WriteLine(val);
        }
      }
    }

    public Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioDeObjetos()
    {

      var diccionario = new Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>>();

      diccionario.Add(LlavesDiccionario.Escuela, new[] { Escuela });
      diccionario.Add(LlavesDiccionario.Cursos, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

      var listaTmp = new List<Evaluacion>();
      var listaTmpas = new List<Asignatura>();
      var listaTmpal = new List<Alumno>();


      foreach (var curs in Escuela.Cursos)
      {
        listaTmpas.AddRange(curs.Asignaturas);
        listaTmpal.AddRange(curs.Alumnos);

        foreach (var alumn in curs.Alumnos)
        {
          listaTmp.AddRange(alumn.Evaluaciones);
        }

      }
      diccionario.Add(LlavesDiccionario.Asignatura, listaTmpas.Cast<ObjetoEscuelaBase>());
      diccionario.Add(LlavesDiccionario.Alumnos, listaTmpal.Cast<ObjetoEscuelaBase>());
      diccionario.Add(LlavesDiccionario.Evaluaciones, listaTmp.Cast<ObjetoEscuelaBase>());
      return diccionario;
    }

    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
      bool traeEvaluaciones = true,
      bool traeAlumnos = true,
      bool traeAsignaturas = true,
      bool traeCursos = true
    )
    {
      return GetObjetosEscuela(out int dummy, out dummy, out dummy, out dummy);
    }

    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
    out int conteoEvaluaciones,
    bool traeEvaluaciones = true,
    bool traeAlumnos = true,
    bool traeAsignaturas = true,
    bool traeCursos = true
    )
    {
      return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out dummy);
    }

    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
    out int conteoEvaluaciones,
    out int conteoCursos,
    bool traeEvaluaciones = true,
    bool traeAlumnos = true,
    bool traeAsignaturas = true,
    bool traeCursos = true
    )
    {
      return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out int dummy, out dummy);
    }

    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
    out int conteoEvaluaciones,
    out int conteoCursos,
    out int conteoAsignaturas,
    bool traeEvaluaciones = true,
    bool traeAlumnos = true,
    bool traeAsignaturas = true,
    bool traeCursos = true
    )
    {
      return GetObjetosEscuela(out conteoEvaluaciones, out conteoCursos, out conteoAsignaturas, out int dummy);
    }
    public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
      out int conteoEvaluaciones,
      out int conteoCursos,
      out int conteoAsignaturas,
      out int conteoAlumnos,
      bool traeEvaluaciones = true,
      bool traeAlumnos = true,
      bool traeAsignaturas = true,
      bool traeCursos = true
    )
    {

      conteoAlumnos = conteoAsignaturas = conteoEvaluaciones = conteoCursos = 0;

      var listaObj = new List<ObjetoEscuelaBase>();
      listaObj.Add(Escuela);

      if (traeCursos)
      {
        listaObj.AddRange(Escuela.Cursos);
        conteoCursos = Escuela.Cursos.Count;

        foreach (var curso in Escuela.Cursos)
        {
          conteoAsignaturas += curso.Asignaturas.Count;
          conteoAlumnos += curso.Alumnos.Count;
          if (traeAsignaturas)
          {
            listaObj.AddRange(curso.Asignaturas);
          }
          if (traeAlumnos)
          {
            listaObj.AddRange(curso.Alumnos);
          }
          if (traeEvaluaciones)
          {
            foreach (var alumno in curso.Alumnos)
            {
              listaObj.AddRange(alumno.Evaluaciones);
              conteoEvaluaciones += alumno.Evaluaciones.Count;
            }
          }
        }
      }
      return listaObj.AsReadOnly();
    }



    #region CargarBase
    private void CargarAsignaturas()
    {
      foreach (var curso in Escuela.Cursos)
      {
        List<Asignatura> listaAsignaturas = new List<Asignatura>(){
          new Asignatura{ Nombre = "Matematicas"},
          new Asignatura{ Nombre = "Español"},
          new Asignatura{ Nombre = "Ciencias Naturales"},
          new Asignatura{ Nombre = "Informatica"},
        };
        curso.Asignaturas = listaAsignaturas;
      }
    }

    private List<Alumno> GetAlumnosAzar(int cantidad)
    {
      string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
      string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
      string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

      var listaAlumnos = from n1 in nombre1
                         from n2 in nombre2
                         from a1 in apellido1
                         select new Alumno { Nombre = $"{n1} {n2} {a1}" };

      return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();

    }

    private void CargarCursos()
    {
      Escuela.Cursos = new List<Curso>(){
            new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
            new Curso(){ Nombre = "201", Jornada = TiposJornada.Mañana },
            new Curso(){ Nombre = "301", Jornada = TiposJornada.Mañana }
          };

      Random rnd = new Random();

      foreach (var curso in Escuela.Cursos)
      {
        int cantidadRamdom = rnd.Next(5, 20);
        curso.Alumnos = GetAlumnosAzar(cantidadRamdom);
      }
    }

    private void CargarEvaluaciones()
    {
      var lista = new List<Evaluacion>();
      var rnd = new Random();

      foreach (var curso in Escuela.Cursos)
      {
        foreach (var asignatura in curso.Asignaturas)
        {
          foreach (var alumno in curso.Alumnos)
          {
            for (int i = 0; i < 5; i++)
            {
              var ev = new Evaluacion
              {
                Asignatura = asignatura,
                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                Nota = MathF.Round((5 * (float)rnd.NextDouble()), 2),
                Alumno = alumno
              };
              alumno.Evaluaciones.Add(ev);
            }
          }
        }
      }
    }
  }

  #endregion
}