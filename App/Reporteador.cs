using System;
using System.Linq;
using System.Collections.Generic;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
  public class Reporteador
  {
    Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
    Reporteador(Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
    {
      if (dicObsEsc == null)
      {
        throw new ArgumentNullException(nameof(dicObsEsc));
      }
      _diccionario = dicObsEsc;
    }

    public IEnumerable<Evaluacion> GetListaEvaluaciones()
    {
      if (_diccionario.TryGetValue(LlavesDiccionario.Evaluaciones,
        out IEnumerable<ObjetoEscuelaBase> lista
      ))
      {
        return lista.Cast<Evaluacion>();
      }
      {
        return new List<Evaluacion>();
        // Escribir en el log de Auditoria
      }
    }
    public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
    {
      listaEvaluaciones = GetListaEvaluaciones();
      return (from Evaluacion ev in listaEvaluaciones  select ev.Asignatura.Nombre).Distinct();
    }

    public Dictionary<string, IEnumerable<Evaluacion>> getDicEvaluaXAsig()
    {
      var dictRta = new Dictionary<string, IEnumerable<Evaluacion>>();
      var listaAsig = GetListaAsignaturas(out var listaEval);

      foreach (var asig in listaAsig)
      {
          var evasAsig = from eval in listaEval where eval.Asignatura.Nombre == asig select eval;
          dictRta.Add(asig, evasAsig);
      }

      return dictRta;
    }

    public Dictionary<string, IEnumerable<object>> GetPromAlumPorAsignatura()
    {
      var rta = new Dictionary<string, IEnumerable<object>>();
      var dicEvalXAsig = getDicEvaluaXAsig();

      foreach (var asigConEval in dicEvalXAsig)
      {
          var promsAlumn = from eval in asigConEval.Value
                            group eval by new{
                              eval.Alumno.UniqueId,
                              eval.Alumno.Nombre
                            }
                            into grupoEvalsAlumno
                            select new  AlumnoPromedio
                            {
                              alumnoId = grupoEvalsAlumno.Key.UniqueId,
                              alumnoNombre = grupoEvalsAlumno.Key.Nombre,
                              Promedio = grupoEvalsAlumno.Average(evaluacion=> evaluacion.Nota)
                            };
          
          rta.Add(asigConEval.Key, promsAlumn);
      }

      return rta;
    }
  }
}