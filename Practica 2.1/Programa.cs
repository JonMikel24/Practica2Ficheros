using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Programa
{
    static string nombreFichero = "";

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        int opcion;

        do
        {
            Console.WriteLine("\n===== GESTIÓN DE FICHEROS =====");
            Console.WriteLine("1. Crear un nuevo fichero");
            Console.WriteLine("2. Añadir registros");
            Console.WriteLine("3. Eliminar todos los registros");
            Console.WriteLine("4. Eliminar el fichero");
            Console.WriteLine("5. Visualizar los registros");
            Console.WriteLine("6. Cerrar la aplicación");
            Console.Write("Selecciona una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Introduce una opción válida.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    CrearFichero();
                    break;

                case 2:
                    AnadirRegistros();
                    break;

                case 3:
                    EliminarRegistros();
                    break;

                case 4:
                    EliminarFichero();
                    break;

                case 5:
                    VisualizarRegistros();
                    break;

                case 6:
                    Console.WriteLine("Aplicación cerrada.");
                    break;

                default:
                    Console.WriteLine("Opción incorrecta.");
                    break;
            }

        } while (opcion != 6);
    }

    static void CrearFichero()
    {
        Console.Write("Introduce el nombre del fichero: ");
        string nombre = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("El nombre no puede estar vacío.");
            return;
        }

        string rutaArchivo = "./" + nombre;

        try
        {
            if (File.Exists(rutaArchivo))
            {
                Console.WriteLine("No se ha podido crear el fichero. Ya existe.");
                return;
            }

            FileStream archivo = File.Create(rutaArchivo);
            archivo.Close();

            nombreFichero = rutaArchivo;
            Console.WriteLine("Archivo creado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al crear el archivo: " + ex.Message);
        }
    }

    static bool ComprobarFichero()
    {
        if (string.IsNullOrWhiteSpace(nombreFichero))
        {
            Console.WriteLine("Primero debes crear o seleccionar un fichero.");
            return false;
        }

        if (!File.Exists(nombreFichero))
        {
            Console.WriteLine("El fichero no existe.");
            nombreFichero = "";
            return false;
        }

        return true;
    }

    static void AnadirRegistros()
    {
        if (!ComprobarFichero())
            return;

        Console.WriteLine("Introduce los registros. Escribe FIN para terminar.");

        try
        {
            List<string> lineas = File.ReadAllLines(nombreFichero).ToList();
            string registro;

            do
            {
                Console.Write("Registro: ");
                registro = Console.ReadLine() ?? "";

                if (!registro.Equals("FIN", StringComparison.OrdinalIgnoreCase)
                    && !string.IsNullOrWhiteSpace(registro))
                {
                    lineas.Add(registro);
                }

            } while (!registro.Equals("FIN", StringComparison.OrdinalIgnoreCase));

            File.WriteAllLines(nombreFichero, lineas);

            Console.WriteLine("Registros guardados correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al escribir: " + ex.Message);
        }
    }

    static void EliminarRegistros()
    {
        if (!ComprobarFichero())
            return;

        Console.Write("¿Seguro que quieres eliminar todos los registros? (S/N): ");
        string respuesta = Console.ReadLine() ?? "";

        if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                File.WriteAllLines(nombreFichero, new string[0]);
                Console.WriteLine("Se han eliminado todos los registros.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }

    static void EliminarFichero()
    {
        if (!ComprobarFichero())
            return;

        Console.Write("¿Seguro que quieres eliminar el fichero? (S/N): ");
        string respuesta = Console.ReadLine() ?? "";

        if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                File.Delete(nombreFichero);
                Console.WriteLine("Fichero eliminado correctamente.");
                nombreFichero = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }

    static void VisualizarRegistros()
    {
        if (!ComprobarFichero())
            return;

        try
        {
            string[] lineas = File.ReadAllLines(nombreFichero).ToArray();

            if (lineas.Length == 0)
            {
                Console.WriteLine("El fichero está vacío.");
                return;
            }

            Console.WriteLine("\n===== REGISTROS DEL FICHERO =====");

            int contador = 1;
            foreach (string s in lineas)
            {
                Console.WriteLine($"{contador}. {s}");
                contador++;
            }

            Console.WriteLine("Total de registros: " + lineas.Length);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer: " + ex.Message);
        }
    }
}