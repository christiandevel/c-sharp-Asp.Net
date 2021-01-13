using static System.Console;

namespace CoreEscuela.util
{
    public static class Printer
    {
        public static void DibujarLinea(int tam = 10)
        {
          var linea = "".PadLeft(10, '=');
          WriteLine(linea);
        }

        public static void PresioneEnter()
        {
          WriteLine("Presione Enter para COntinuar");
        }

        public static void Title(string texto)
        {
          WriteLine(texto);
        }
    }
}