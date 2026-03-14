namespace Functions;

class Funciones
{
        static public int Cantidades (string producto, Dictionary<string, Dictionary<double, int>> dic)
        {
            int cantidad = 0;
            int cantidad_compra;
            Console.WriteLine("Digite la cantidad de compra: \n . . .");
            int.TryParse(Console.ReadLine()!, out cantidad_compra);

            if (dic[producto].ElementAt(0).Value > 0)
            {
                cantidad = cantidad_compra;
            }
            return cantidad;
        }
        static public void cambia_linea(string datos, string ruta, int linea_selec, int valor)
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
            if (producto>= 0 && producto < Productos.Capacity)
            {
                valor_f = Productos.Contains(dic.ElementAt(producto).Key);        
            }
            return valor_f;
        }
    static public void Actualizar_Lista(string ruta)
        {
                string confirmar = "";
                bool confirm = false;
                do
                {
                    int numerador = 0;
                    Dictionary<string, Dictionary<double, int>> dic = Inventario_Principal(ruta);
                    Console.WriteLine("Indique el producto que desea alterar. . .");
                    List<string> Lista = dic.Keys.ToList();
                foreach (string producto in Lista)
                {
                    Console.WriteLine($"//{numerador + 1} {producto} ${dic[producto].ElementAt(0).Key} - - {dic[producto].ElementAt(0).Value} Und. . .");
                    numerador += 1;
                }
                string nombre = Console.ReadLine()!;
                if (int.TryParse(nombre, out numerador) == false)
                {
                    confirm = comprobar(nombre, dic);
                }
                else if (int.TryParse(nombre, out numerador) == true)
                {
                    confirm = comprobar_int(numerador, dic);
                if (numerador > Lista.Count)
                {
                    Console.WriteLine("Producto no registrado o con existencia en 0, favor de reintentar");
                    continue;
                }
                }
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
                Console.WriteLine("Digite el nombre del producto que desea agregar \n . . . Si no desea agregar ningún producto, Presione Enter");
            string nombre = Console.ReadLine()!;
            bool confirm = comprobar(nombre, dic);
            if (nombre == "")
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
            Console.WriteLine($"¿Desea agregar {cantidad} de {nombre} por ${precio}?. . . \n 1. Si \n 2. No");
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
                Console.WriteLine("Digite el nombre del producto que desea eliminar \n . . . Si no desea eliminar ningún producto, presione Enter");
                foreach (string producto in dic.Keys.ToList())
                {
                    Console.WriteLine($"// {numerador += 1} {producto} ${dic[producto].ElementAt(0).Key} - - {dic[producto].ElementAt(0).Value} Und. . .");
                }
            string nombre = Console.ReadLine()!;
            if (nombre == "")
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