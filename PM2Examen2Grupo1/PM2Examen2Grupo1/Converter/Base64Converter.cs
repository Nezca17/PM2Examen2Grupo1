using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PM2Examen2Grupo1.Converter
{
    public class Base64Converter
    {
        public static string FileToBase64(string filePath)
        {
            try
            {
                // Lee el contenido del archivo y conviértelo en un array de bytes
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // Convierte el array de bytes a una cadena Base64
                string base64String = Convert.ToBase64String(fileBytes);

                return base64String;
            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes personalizar el manejo según tus necesidades)
                Console.WriteLine("Error al convertir el archivo a Base64: " + ex.Message);
                return null;
            }
        }
    }
}
