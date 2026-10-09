//Ejercicio 5

namespace ControlServomotorApp
{
    // CLASE: Representa el componente físico del servomotor y su lógica de control
    public class Servomotor
    {
        // PROPIEDADES (con respaldos para encapsulamiento y validación)
        private double posicionActual;
        private double posicionObjetivo;

        public double PosicionActual
        {
            get { return posicionActual; }
            set { posicionActual = value; }
        }

        public double PosicionObjetivo
        {
            get { return posicionObjetivo; }
            set { posicionObjetivo = value; }
        }

        // MÉTODO: Valida si un valor de posición individual está dentro de los límites físicos [0°, 180°]
        public static bool ValidarRango(double posicion)
        {
            return posicion >= 0.0 && posicion <= 180.0;
        }

        // MÉTODO: Calcula el desplazamiento absoluto en grados
        public double CalcularDesplazamiento()
        {
            return Math.Abs(posicionObjetivo - posicionActual);
        }

        // MÉTODO: Evalúa el sentido del movimiento según las posiciones
        public string DeterminarDireccion()
        {
            if (posicionObjetivo > posicionActual)
            {
                return "Hacia una posición mayor";
            }
            else if (posicionObjetivo < posicionActual)
            {
                return "Hacia una posición menor";
            }
            else
            {
                return "No hay movimiento";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Evaluación de Movimiento de Servomotor - Ejercicio 05";

            // OBJETOS: Instanciación de la clase Servomotor
            Servomotor miServomotor = new Servomotor();

            bool entradasValidas = false;

            // CICLO: Garantiza que no se prosiga hasta ingresar posiciones válidas dentro de [0°, 180°]
            while (!entradasValidas)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("    SISTEMA DE CONTROL DE POSICIÓN DE SERVOMOTOR   ");
                Console.WriteLine("==================================================\n");

                double act = LeerPosicion("Ingrese la posición actual del servomotor (°): ");
                double obj = LeerPosicion("Ingrese la posición objetivo del servomotor (°): ");

                // VALIDACIÓN: Comprueba que ambos ángulos estén dentro del rango de 0 a 180 grados
                if (Servomotor.ValidarRango(act) && Servomotor.ValidarRango(obj))
                {
                    miServomotor.PosicionActual = act;
                    miServomotor.PosicionObjetivo = obj;
                    entradasValidas = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[ERROR]: Una o ambas posiciones están fuera del rango permitido (0° a 180°).");
                    Console.WriteLine("Presione cualquier tecla para intentar nuevamente...");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }

            // PROCESAMIENTO Y SALIDA DE RESULTADOS
            double desplazamiento = miServomotor.CalcularDesplazamiento();
            string direccion = miServomotor.DeterminarDireccion();

            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine("             RESULTADOS DEL MOVIMIENTO            ");
            Console.WriteLine("--------------------------------------------------");
            // MOSTRAR UNIDADES DE MEDIDA (° / Grados)
            Console.WriteLine($"Posición Actual:         {miServomotor.PosicionActual:F2} °");
            Console.WriteLine($"Posición Objetivo:       {miServomotor.PosicionObjetivo:F2} °");
            Console.WriteLine($"Desplazamiento Absoluto: {desplazamiento:F2} °");
            Console.WriteLine($"Dirección del Movimiento: {direccion}");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("\nPresione cualquier tecla para finalizar el programa...");
            Console.ReadKey();
        }

        // MÉTODO AUXILIAR: Validación de formato numérico de entrada para evitar excepciones por texto no válido
        static double LeerPosicion(string mensaje)
        {
            double valor;
            Console.Write(mensaje);
            while (!double.TryParse(Console.ReadLine(), out valor))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" [Entrada no válida]. Por favor, ingrese un número real: ");
                Console.ResetColor();
            }
            return valor;
        }
    }
}