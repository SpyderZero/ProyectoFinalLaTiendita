using System.Linq.Expressions;
using System.IO;
using Functions;

namespace prueba
{
    class Prueba
    {
        public static void Main()
        {
            string ruta = "src/Inventario.csv";
            int selec_menu = 0;
            Dictionary<string, Dictionary<double, int>> Inventario = Funciones.Inventario_Principal(ruta);

            do
            {Console.WriteLine("Sistema de Inventario Colmado Cocoa Lider \n 1. Facturar productos \n 2. Conduce de producto \n 3. Existencia Inventario \n 4. Salir");
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
            List<int> Lista_cantidades = new List<int>();
            string nombre = "";
            bool confirm = false;
            double suma = 0;
            int num = 0;
            do
            {
                            int numerador = 0;
                Dictionary<string, Dictionary<double, int>> dic = Funciones.Inventario_Principal(ruta);
                List<string> Lista = dic.Keys.ToList();
            Console.WriteLine("Elija los productos que desea facturar. . . \n Presione Enter cuando tenga todos los productos: ");
                foreach (string producto in Lista)
                {
                    Console.WriteLine($"// {numerador += 1}. {producto}. . . ${dic[producto].ElementAt(0).Key}");
                }
                nombre = Console.ReadLine()!;
                if (int.TryParse(nombre, out numerador) == true)
                {
                    confirm = Funciones.comprobar_int(numerador, dic);
                }
                else 
                {
                    confirm = Funciones.comprobar(nombre, dic);
                }
                if (nombre == "")
                {
                    
                }
                else if (confirm == true && int.TryParse(nombre, out numerador) == false)
                {    
                    factura.Add(nombre);
                    Lista_cantidades.Add(Funciones.Cantidades(nombre, dic));
                    if (Lista_cantidades.Contains(0) == true)
                {
                    Console.WriteLine("Producto no registrado o con existencia en 0, favor de reintentar");
                    continue;
                }
                }
                else if (confirm == true && int.TryParse(nombre, out numerador) == true)
                {
                    factura.Add(dic.ElementAt(numerador - 1).Key);
                    Lista_cantidades.Add(Funciones.Cantidades(dic.ElementAt(numerador - 1).Key, dic));
                    if (Lista_cantidades.Contains(0) == true)
                {
                    Console.WriteLine("Producto no registrado o con existencia en 0, favor de reintentar");
                    continue;
                }
                }
                else if (confirm == false)
                {
                    Console.WriteLine("Producto no registrado o con existencia en 0, favor de reintentar");
                    continue;
                }
                if (nombre != "") 
                {
                    continue;
                }

                foreach (string producto in factura)
                {
                    if (producto == "") break;
                    Console.WriteLine($"{Lista_cantidades[num]} -- -- {producto}. . .");
                    num += 1;
                }

                Console.WriteLine("¿Desea Facturar estos Productos? \n 1. Si \n 2. No");
                string completar_factura = "";
                completar_factura = Console.ReadLine()!;
                
                if (completar_factura == "1" || completar_factura == "Si" || completar_factura == "si")
                    {
                        num = 0;
                        foreach (string producto in factura)
                        {
                            int cantidad = dic[producto].ElementAt(0).Value;
                            cantidad -= Lista_cantidades[num];
                            string datos = $"{producto},{dic[producto].ElementAt(0).Key},{cantidad}";
                            int index = Lista.IndexOf(producto);
                            Funciones.cambia_linea(datos, ruta, index,0);
                            suma += dic[producto].ElementAt(0).Key * Lista_cantidades[num];
                            Console.WriteLine($"{producto} {Lista_cantidades[num]}\n ${dic[producto].ElementAt(0).Key}");
                            num += 1;
                        }
                    Console.WriteLine($"Su total es de: ${suma} \n // ¡Gracias por su compra!");
                    nombre = "";
                    }
                else if (completar_factura != "2" || completar_factura != "No" || completar_factura == "no")
                    {
                    nombre = "";
                    }
            }while(nombre != "");
        }
        static public void Conduce_Producto(string ruta)
        {
            
            Console.WriteLine("1. Agregar Producto \n 2. Eliminar Producto \n 3. Volver al Menu Principal");
            int conducir = int.Parse(Console.ReadLine()!);
            switch (conducir)
            {
                case 1:
                Funciones.Agregar_Product(ruta);
                break;

                case 2:
                Funciones.Eliminar_Product(ruta);
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
            Dictionary<string, Dictionary<double, int>> dic = Funciones.Inventario_Principal(ruta);
                foreach (string producto in dic.Keys.ToList())
                {
                    Console.WriteLine($"// {producto} ${dic[producto].ElementAt(0).Key} - - {dic[producto].ElementAt(0).Value} Und. . .");
                }
                Console.WriteLine("¿Desea cambiar la existencia de algun producto? \n 1. Si \n 2. No");
                string confirmar = Console.ReadLine()!;
                if (confirmar == "1" || confirmar == "Si" || confirmar == "si")
            {
                Funciones.Actualizar_Lista(ruta);
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
    }
}
