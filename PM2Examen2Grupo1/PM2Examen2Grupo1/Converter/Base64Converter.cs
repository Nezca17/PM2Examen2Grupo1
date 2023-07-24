using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
                // Manejo de errores 
                Console.WriteLine("Error al convertir el archivo a Base64: " + ex.Message);
                return null;
            }
        }

        public async Task<byte[]> ConvertBase64ToImage(string base64String, string outputFilePath)
        {

            try {
                // Convertir el dato Base64 a bytes
                byte[] imageBytes =  Convert.FromBase64String(base64String);

                // Guardar los bytes como archivo de imagen
               // Byte[] archivoNuevo =  File.WriteAllBytes(outputFilePath, imageBytes);

                return imageBytes;
            } catch (Exception ex)
            {
                // Manejo de errores 
                Console.WriteLine("Error al convertir el archivo a Base64: " + ex.Message);
                return null;
            }
          
        }

        public byte[] ConvertBase64ToByteArray(string base64String)
        {
            // Convertir el dato Base64 a bytes
            byte[] fileBytes = Convert.FromBase64String(base64String);
            return fileBytes;
        }
    }
}
