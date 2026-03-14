using System.Linq.Expressions;
using System.IO;

namespace prueba
{
    class Prueba
    {
        public static void Main()
        {
            string ruta = "src/Inventario.csv";
            int selec_menu = 0;
            Dictionary<string, Dictionary<double, int>> Inventario = Inventario_Principal(ruta);


            do
            {Console.WriteLine("Sistema de Inventario Colmado Cocoa Lider \n 1. Facturar productos \n 2. Conduce de producto \n 3. Existencia Inventario");
            selec_menu = int.Parse(Console.ReadLine()!);
            switch (selec_menu)
            {
                case 1:
                Facturar(ruta);
                break;
                
                case 2:
                Conduce_Producto(ruta);
                break;

                case 3:
                ListaProducts(ruta);
                break;

                case 4:
                break;

                default:
                Console.WriteLine("Comando no identificado, intente de nuevo. . .");
                if (selec_menu != 1 && selec_menu != 2 && selec_menu != 3) continue;
                break;
            }
            }while(selec_menu != 4);
        }
        static public void Facturar(string ruta)
        {
            
            List<string> factura = new List<string>();
            string nombre = "";
            bool confirm = false;
            double suma = 0;

            do
            {
                            int numerador = 0;
                Dictionary<string, Dictionary<double, int>> dic = Inventario_Principal(ruta);
                List<string> Lista = dic.Keys.ToList();
            Console.WriteLine("Elija los productos que desea facturar. . . \n Ingrese # cuando tenga todos los productos: ");
                foreach (string producto in Lista)
                {
                    Console.WriteLine($"// {numerador += 1}. {producto}. . . ${dic[producto].ElementAt(0).Key}");
                }
                nombre = Console.ReadLine()!;
                if (int.TryParse(nombre, out numerador) == true)
                {
                    confirm = comprobar_int(numerador, dic);
                }
                else 
                {
                    confirm = comprobar(nombre, dic);
                }
                if (nombre == "#")
                {
                  
                }
                else if (confirm == true && int.TryParse(nombre, out numerador) == false)
                {    
                    factura.Add(nombre);
                }
                else if (confirm == true && int.TryParse(nombre, out numerador) == true)
                {
                    factura.Add(dic.ElementAt(numerador - 1).Key);
                }
                else if (confirm == false)
                {
                    Console.WriteLine("Producto no registrado o con existencia en 0, favor de reintentar");
                    continue;
                }
                if (nombre != "#") continue;

                foreach (string producto in factura)
                {
                    if (producto == "#") break;
                    Console.WriteLine(producto);
                }

                Console.WriteLine("¿Desea Facturar estos Productos? \n 1. Si \n 2. No");
                string completar_factura = "";
                completar_factura = Console.ReadLine()!;
                
                if (completar_factura == "1" || completar_factura == "Si" || completar_factura == "si")
                    {
                    foreach (string producto in factura)
                        {
                            int cantidad = dic[producto].ElementAt(0).Value;
                            cantidad -= 1;
                            string datos = $"{producto},{dic[producto].ElementAt(0).Key},{cantidad}";
                            int index = Lista.IndexOf(producto);
                            cambia_linea(datos, ruta, index,0);

                            suma += dic[producto].ElementAt(0).Key;
                            Console.WriteLine($"{producto} \n ${dic[producto].ElementAt(0).Key}");

                        }
                    Console.WriteLine($"Su total es de: ${suma} \n // ¡Gracias por su compra!");
                    nombre = "#";
                    }
                else if (completar_factura != "2" || completar_factura != "No" || completar_factura == "no")
                    {
                    nombre = "#";
                    }
            }while(nombre != "#");
        }
        static void cambia_linea(string datos, string ruta, int linea_selec, int valor)
        {       

            string[] linea = File.ReadAllLines(ruta);
            if (valor == 0)
            {
                linea[linea_selec] = datos;
                File.WriteAllLines(ruta, linea);
            }
            else if (valor == 1 && linea.Length > 0)
            {
                var Inventario_sinultima = linea.Take(linea.Length - 1).ToArray();
                File.WriteAllLines(ruta, Inventario_sinultima);
            }
            else if (valor == 2)
            {
                List<string> Lista = linea.ToList();
                Lista.Add(datos);
                linea = Lista.ToArray();
                File.WriteAllLines(ruta, linea);
            }
        }
        static public bool comprobar (string producto, Dictionary<string, Dictionary<double, int>> dic)
        {
            bool valor_f = false;
            var Productos = dic.Keys.ToList();
            bool valor = Productos.Contains(producto);
            if (valor == true && dic[producto].ElementAt(0).Value > 0)
            {
                valor_f = true;
            }
            return valor_f;
        }
        static public bool comprobar_int (int producto, Dictionary<string, Dictionary<double, int>> dic)
        {
            bool valor_f = false;
            var Productos = dic.Keys.ToList();
            producto -= 1;
            if (producto> 0 && producto < Productos.Capacity)
            {
                valor_f = Productos.Contains(dic.ElementAt(producto).Key);        
            }
            return valor_f;
        }
        static public void Conduce_Producto(string ruta)
        {
            
            Console.WriteLine("1. Agregar Producto \n 2. Eliminar Producto \n 3. Volver al Menu Principal");
            int conducir = int.Parse(Console.ReadLine()!);
            switch (conducir)
            {
                case 1:
                Agregar_Product(ruta);
                break;

                case 2:
                Eliminar_Product(ruta);
                break;

                case 3:
                break;

                default:
                Console.WriteLine("Comando no registrado, volviendo al Menu Principal. . .");
                break;
            }
        }
        static public void ListaProducts(string ruta)
        {
            Dictionary<string, Dictionary<double, int>> dic = Inventario_Principal(ruta);
                foreach (string producto in dic.Keys.ToList())
                {
                    Console.WriteLine($"// {producto} ${dic[producto].ElementAt(0).Key} - - {dic[producto].ElementAt(0).Value} Und. . .");
                }
                Console.WriteLine("¿Desea cambiar la existencia de algun producto? \n 1. Si \n 2. No");
                string confirmar = Console.ReadLine()!;
                if (confirmar == "1" || confirmar == "Si" || confirmar == "si")
            {
                Actualizar_Lista(ruta);
            }
            else if (confirmar == "2" || confirmar == "No" || confirmar == "no")
            {
                Console.WriteLine("Regresando al menú principal. . .");
                confirmar = "";
            }
            else
            {
                Console.WriteLine("Comando no especificado. . .");
                confirmar = "";
            }
        }
        static public void Actualizar_Lista(string ruta)
        {
                string confirmar = "";       
                do
                {
                    Dictionary<string, Dictionary<double, int>> dic = Inventario_Principal(ruta);
                    Console.WriteLine("Indique el producto que desea alterar. . .");
                string nombre = Console.ReadLine()!;
                bool confirm = comprobar(nombre, dic);
                List<string> Lista = dic.Keys.ToList();
                    int index = Lista.IndexOf(nombre);
                if (confirm == true)
                {
                    
                }
                else if (confirm == false)
                {
                    Console.WriteLine("Producto no registrado o con existencia en 0, favor de reintentar");
                    continue;
                }
                Console.WriteLine("Indique la nueva cantidad. .  .");
                int cantidad = int.Parse(Console.ReadLine()!);
                Console.WriteLine("¿Desea cambiar el precio del producto? \n 1. Si \n 2. No");
                confirmar = Console.ReadLine()!;
                if (confirmar == "1" || confirmar == "Si" || confirmar == "si")
                {
                    Console.WriteLine("Indique el nuevo precio. . .");
                    double nuevo_precio = double.Parse(Console.ReadLine()!);
                    cambia_linea($"{nombre},{nuevo_precio},{cantidad}",ruta,index,0);
                    Console.WriteLine("Producto actualizado con exito.");
                }
                else if (confirmar == "2" || confirmar == "No" || confirmar == "no")
                {
                    cambia_linea($"{nombre},{dic[nombre].ElementAt(0).Key},{cantidad}",ruta,index,0);
                    Console.WriteLine("Producto actualizado con exito.");
                }
                else
                {
                    Console.WriteLine("Comando no especificado. . .");
                }
                Console.WriteLine("¿Desea actualizar algun otro producto? \n 1. Si \n 2. No");
                confirmar = Console.ReadLine()!;
                if (confirmar == "2" || confirmar == "No" || confirmar == "no")
                {
                    
                }
                else if (confirmar != "1" || confirmar != "Si" || confirmar != "si")
                {
                    Console.WriteLine("Comando no reconocido. . .");
                }
                }while (confirmar != "2" && confirmar != "No" && confirmar != "no");

        }
        static public void Agregar_Product(string ruta)
        {
            
            string confirmar = "1";
            do
            {
                Dictionary<string, Dictionary<double, int>> dic = Inventario_Principal(ruta);
                Console.WriteLine("Digite el nombre del producto que desea agregar \n . . . Si no desea agregar ningún producto, escriba #");
            string nombre = Console.ReadLine()!;
            bool confirm = comprobar(nombre, dic);
            if (nombre == "#")
                {
                    break;
                }
            else if (confirm == false)
                {
                    
                }
            else if (confirm == true)
                {
                    Console.WriteLine("Producto ya registrado, intente un nombre diferente");
                    continue;
                }
            Console.WriteLine("Digite el precio del producto \n . . .");
            double precio = double.Parse(Console.ReadLine()!);
            Console.WriteLine("Ahora digite la cantidad del producto añadido \n . . .");
            int cantidad = int.Parse(Console.ReadLine()!);
            Console.WriteLine($"¿Desea agregar {cantidad} de {nombre} por {precio}?. . . \n 1. Si \n 2. No");
            confirmar = Console.ReadLine()!;
            if (confirmar == "1" || confirmar == "Si")
            {
                cambia_linea($"{nombre},{precio},{cantidad}",ruta,dic.Keys.ToList().Capacity,2);
                Console.WriteLine("Producto agregado con exito. \n ¿Desea agregar otro producto?. . . \n 1. Si \n 2. No");
                confirmar = Console.ReadLine()!;
                if (confirmar == "2" || confirmar == "No" || confirmar == "no")
                    {
                    confirmar = "";
                    }
                else if (confirmar != "2" || confirmar != "No" || confirmar != "no")
                    {
                        Console.WriteLine("Comando no especificado. . .");
                        confirmar = "1";
                    }
            }
            else if (confirmar == "2")
            {
                Console.WriteLine("Desea agregar otro producto?. . . \n 1. Si \n 2. No");
                if (confirmar == "2" || confirmar == "No" || confirmar == "no")
                    {
                    confirmar = "";
                    }
                else if (confirmar != "1" || confirmar != "Si" || confirmar != "si")
                    {
                        Console.WriteLine("Comando no especificado. . .");
                        confirmar = "1";
                    }
            }
            else
            {
                Console.WriteLine("Comando no especificado. . .");
                confirmar = "1";
            }
            }while(confirmar == "1" || confirmar == "Si" || confirmar == "si");
        }
        static public void Eliminar_Product(string ruta)
        {
            bool confirm = false;
            int numerador = 0;
            string confirmar = "";
            do
            {
                Dictionary<string, Dictionary<double, int>> dic = Inventario_Principal(ruta);
                Console.WriteLine("Digite el nombre del producto que desea eliminar \n . . . Si no desea eliminar ningún producto, escriba #");
                foreach (string producto in dic.Keys.ToList())
                {
                    Console.WriteLine($"// {numerador += 1} {producto} ${dic[producto].ElementAt(0).Key} - - {dic[producto].ElementAt(0).Value} Und. . .");
                }
            string nombre = Console.ReadLine()!;
            if (nombre == "#")
                {
                    break;
                }
            else if (int.TryParse(nombre, out numerador) == true)
                {
                    confirm = comprobar_int(numerador, dic);
                }
                else 
                {
                    confirm = comprobar(nombre, dic);
                }
                if (confirm == true && int.TryParse(nombre, out numerador) == false)
                {
                    
                }
                else if (confirm == true && int.TryParse(nombre, out numerador) == true)
                {
                    nombre = dic.ElementAt(numerador - 1).Key;
                }
                else if (confirm == false)
                {
                    Console.WriteLine("Producto no registrado o con existencia en 0, favor de reintentar");
                    continue;
                }
            Console.WriteLine($"¿Desea eliminar {nombre} del inventario?. . . \n 1. Si \n 2. No");
            confirmar = Console.ReadLine()!;
            if (confirmar == "1" || confirmar == "Si" || confirmar == "si")
            {
                cambia_linea("",ruta,dic.Keys.ToList().IndexOf(nombre),1);
                Console.WriteLine("Producto eliminado con exito. \n ¿Desea eliminar otro producto?. . . \n 1. Si \n 2. No");
                confirmar = Console.ReadLine()!;
                if (confirmar == "2" || confirmar == "No" || confirmar == "no")
                    {
                    confirmar = "";
                    }
                else if (confirmar != "1" || confirmar != "Si" || confirmar != "si")
                    {
                        Console.WriteLine("Comando no especificado. . .");
                        confirmar = "1";
                    }
            }
            else if (confirmar == "2")
            {
                Console.WriteLine("Desea eliminar otro producto?. . . \n 1. Si \n 2. No");
                if (confirmar == "2" || confirmar == "No" || confirmar == "no")
                    {
                    confirmar = "";
                    }
                else if (confirmar != "1" || confirmar != "Si" || confirmar != "si")
                    {
                        Console.WriteLine("Comando no especificado. . .");
                        confirmar = "1";
                    }
            }
            else
            {
                Console.WriteLine("Comando no especificado. . .");
                confirmar = "1";
            }
            }while(confirmar == "1" || confirmar == "Si" || confirmar == "si");
        }
        public static Dictionary<string, Dictionary<double, int>> Inventario_Principal (string ruta)
        {
            Dictionary<string, Dictionary<double, int>> Inventario = new Dictionary<string, Dictionary<double, int>>();

            if (File.Exists(ruta))
                {using (StreamReader sr = new StreamReader(ruta))
                {
                    string linea;

                    while ((linea = sr.ReadLine()) != null)
                    {
                        Dictionary<double, int> dic = new Dictionary<double, int>();
                        List<string> datos = linea.Split(",").ToList();
                        var fallo = dic.TryAdd(double.Parse(datos[1]), int.Parse(datos[2]));
                        if (fallo == false) break;
                        Inventario.Add(datos[0], dic);
                    }
                }
                }
            return Inventario;
        }
        static public Dictionary<double, int> crear_dic (double precio, int cantidad)
        {
            Dictionary<double, int> dic = new Dictionary<double, int>();
            dic.Add(precio, cantidad);

            return dic;
        }
    }
}
