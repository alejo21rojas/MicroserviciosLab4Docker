using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;


namespace Unillanos.ArquitecturaMS.Usuarios.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpGet]
        [Route("Leer")]
        public string Get()
        {
            try
            {
                //Lógica para leer el archivo
                using (TextReader leer = new StreamReader("Usuarios.txt"))
                {
                    return leer.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return "No existe un archivo de registro, primero ingrese un usuario";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public string Delete(string id)
        {
            StreamReader lectura;
            StreamWriter escribir;
            bool encontrado;
            encontrado = false;
            string cadena;
            string[] campos = new string[6];
            char[] separador = { ',' };
            try
            {
                lectura = System.IO.File.OpenText("Usuarios.txt");
                escribir = System.IO.File.CreateText("temp.txt");
                cadena = lectura.ReadLine();

                while (cadena != null)
                {
                    campos = cadena.Split(separador);
                    if (campos[0].Trim().Equals(id))
                    {
                        encontrado = true;
                    }
                    else
                    {
                        escribir.WriteLine(cadena);
                    }
                    cadena = lectura.ReadLine();
                }

                lectura.Close();
                escribir.Close();

                //Elimina y renombra
                System.IO.File.Delete("Usuarios.txt");
                System.IO.File.Move("temp.txt", "Usuarios.txt");

                if (encontrado == false)
                {
                    return "Usuario no encontrado";
                }
                else
                {
                    return "Registro eliminado correctamente";
                }
            }
            catch (FileNotFoundException)
            {
                return "No existe un archivo de registro, primero ingrese un usuario";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public string Put(string id, UsuariosDto usuario)
        {
            StreamReader lectura;
            StreamWriter escribir;
            bool encontrado;
            encontrado = false;
            string cadena;
            string[] campos = new string[6];
            char[] separador = { ',' };
            try
            {
                lectura = System.IO.File.OpenText("Usuarios.txt");
                escribir = System.IO.File.CreateText("temp.txt");
                cadena = lectura.ReadLine();

                while (cadena != null)
                {
                    campos = cadena.Split(separador);
                    if (campos[0].Trim().Equals(id))
                    {
                        encontrado = true;
                        escribir.WriteLine(id + "," + usuario.Nombre + "," + usuario.Apellido + "," + usuario.Sexo + "," + usuario.Correo + ","
                        + usuario.Telefono + "," + usuario.Edad);

                    }
                    else
                    {
                        escribir.WriteLine(cadena);
                    }
                    cadena = lectura.ReadLine();
                }

                lectura.Close();
                escribir.Close();

                //Elimina y renombra
                System.IO.File.Delete("Usuarios.txt");
                System.IO.File.Move("temp.txt", "Usuarios.txt");

                if (encontrado == false)
                {
                    return "Usuario no encontrado";
                }
                else
                {
                    return "Registro actualizado correctamente";
                }
            }
            catch (FileNotFoundException)
            {
                return "No existe un archivo de registro, primero ingrese un usuario";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


        [HttpPost]
        [Route("Insertar")]
        public string Post(UsuariosDto usuario)
        {
            bool result = System.IO.File.Exists("Usuarios.txt"); //Valida si el archivo usuarios existe
            if (result == false)
            {
                //Crea el archivo e ingresa el primer usuario con el ID 1
                StreamWriter archivo = new StreamWriter("Usuarios.txt", true);
                usuario.SetID(1);
                archivo.WriteLine(usuario.ID + "," + usuario.Nombre + "," + usuario.Apellido + "," + usuario.Sexo + "," + usuario.Correo + ","
                + usuario.Telefono + "," + usuario.Edad);
                archivo.Close();

                return "Usuario ingresado correctamente";
            }
            else
            {
                //Lee el numero de lineas del archivo y aumenta el contador
                int lineas = System.IO.File.ReadAllLines("Usuarios.txt").Length;
                lineas++;
                //Escribe sobre el archivo existente
                StreamWriter archivo = new StreamWriter("Usuarios.txt", true);
                //Ingresa el usuario con el valor del contador como ID
                usuario.SetID(lineas);
                archivo.WriteLine(usuario.ID + "," + usuario.Nombre + "," + usuario.Apellido + "," + usuario.Sexo + "," + usuario.Correo + ","
                + usuario.Telefono + "," + usuario.Edad);
                archivo.Close();

                return "Usuario ingresado correctamente";

            }

        }

        public class UsuariosDto
        {
            public int ID;
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Sexo { get; set; }
            public string Correo { get; set; }
            public string Telefono { get; set; }
            public int Edad { get; set; }

            public int GetID()
            {
                return ID;
            }
            public void SetID(int ID1)
            {
                ID = ID1;
            }

        }
    }
}