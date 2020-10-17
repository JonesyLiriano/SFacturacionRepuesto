﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Clases
{
    class ControladorImpresoraMatricial
    {
        StringBuilder linea = new StringBuilder();
        //Creamos una variable para almacenar el numero maximo de caracteres que permitiremos en el ticket.
        int maxCar = 40, cortar;//Para una impresora ticketera que imprime a 40 columnas. La variable cortar cortara el texto cuando rebase el limte.

        //Creamos el primer metodo, este dibujara lineas guion.
        public string lineasGuio()
        {
            string lineasGuion = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasGuion += "-";//Agregara un guio hasta llegar la numero maximo de caracteres.
            }
            return linea.AppendLine(lineasGuion).ToString(); //Devolvemos la lineaGuion
        }

        //Metodo para dibujar una linea con asteriscos
        public string lineasAsteriscos()
        {
            string lineasAsterisco = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasAsterisco += "*";//Agregara un asterisco hasta llegar la numero maximo de caracteres.
            }
            return linea.AppendLine(lineasAsterisco).ToString(); //Devolvemos la linea con asteriscos
        }

        //Realizamos el mismo procedimiento para dibujar una lineas con el signo igual
        public string lineasIgual()
        {
            string lineasIgual = "";
            for (int i = 0; i < maxCar; i++)
            {
                lineasIgual += "=";//Agregara un igual hasta llegar la numero maximo de caracteres.
            }
            return linea.AppendLine(lineasIgual).ToString(); //Devolvemos la lienas con iguales
        }

        //Creamos un metodo para poner el texto a la izquierda
        public void TextoIzquierda(string texto)
        {
            //Si la longitud del texto es mayor al numero maximo de caracteres permitidos, realizar el siguiente procedimiento.
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    //Agregamos los fragmentos que salgan del texto
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                //agregamos el fragmento restante
                linea.AppendLine(texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                //Si no es mayor solo agregarlo.
                linea.AppendLine(texto);
            }
        }

        //Creamos un metodo para poner texto a la derecha.
        public void TextoDerecha(string texto)
        {
            //Si la longitud del texto es mayor al numero maximo de caracteres permitidos, realizar el siguiente procedimiento.
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    //Agregamos los fragmentos que salgan del texto
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                //Variable para poner espacios restntes
                string espacios = "";
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length); i++)
                {
                    espacios += " ";//Agrega espacios para alinear a la derecha
                }

                //agregamos el fragmento restante, agregamos antes del texto los espacios
                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                string espacios = "";
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < (maxCar - texto.Length); i++)
                {
                    espacios += " ";//Agrega espacios para alinear a la derecha
                }
                //Si no es mayor solo agregarlo.
                linea.AppendLine(espacios + texto);
            }
        }

        //Metodo para centrar el texto
        public void TextoCentro(string texto)
        {
            if (texto.Length > maxCar)
            {
                int caracterActual = 0;//Nos indicara en que caracter se quedo al bajar el texto a la siguiente linea
                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    //Agregamos los fragmentos que salgan del texto
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                //Variable para poner espacios restntes
                string espacios = "";
                //sacamos la cantidad de espacios libres y el resultado lo dividimos entre dos
                int centrar = (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length) / 2;
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";//Agrega espacios para centrar
                }

                //agregamos el fragmento restante, agregamos antes del texto los espacios
                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));
            }
            else
            {
                string espacios = "";
                //sacamos la cantidad de espacios libres y el resultado lo dividimos entre dos
                int centrar = (maxCar - texto.Length) / 2;
                //Obtenemos la longitud del texto restante.
                for (int i = 0; i < centrar; i++)
                {
                    espacios += " ";//Agrega espacios para centrar
                }

                //agregamos el fragmento restante, agregamos antes del texto los espacios
                linea.AppendLine(espacios + texto);

            }
        }

        //Metodo para poner texto a los extremos
        public void TextoExtremos(string textoIzquierdo, string textoDerecho)
        {
            //variables que utilizaremos
            string textoIzq, textoDer, textoCompleto = "", espacios = "";

            //Si el texto que va a la izquierda es mayor a 18, cortamos el texto.
            if (textoIzquierdo.Length > 22)
            {
                cortar = textoIzquierdo.Length - 22;
                textoIzq = textoIzquierdo.Remove(22, cortar);
            }
            else
            { textoIzq = textoIzquierdo; }

            textoCompleto = textoIzq;//Agregamos el primer texto.

            if (textoDerecho.Length > 24)//Si es mayor a 20 lo cortamos
            {
                cortar = textoDerecho.Length - 24;
                textoDer = textoDerecho.Remove(24, cortar);
            }
            else
            { textoDer = textoDerecho; }

            //Obtenemos el numero de espacios restantes para poner textoDerecho al final
            int nroEspacios = maxCar - (textoIzq.Length + textoDer.Length);
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";//agrega los espacios para poner textoDerecho al final
            }
            textoCompleto += espacios + textoDerecho;//Agregamos el segundo texto con los espacios para alinearlo a la derecha.
            linea.AppendLine(textoCompleto);//agregamos la linea al ticket, al objeto en si.
        }

        //Creamos el encabezado para los articulos
        public void EncabezadoVenta()
        {
            //Escribimos los espacios para mostrar el articulo. En total tienen que ser 40 caracteres
            linea.AppendLine("DESCRIPCION               |ITBIS  |VALOR");
        }

        public void EncabezadoOrdenCompra()
        {
            //Escribimos los espacios para mostrar el articulo. En total tienen que ser 40 caracteres
            linea.AppendLine("DESCRIPCION                       |VALOR");
        }

        //Creamos el encabezado para los articulos
        public void EncabezadoNC()
        {
            //Escribimos los espacios para mostrar el articulo. En total tienen que ser 40 caracteres
            linea.AppendLine("DESCRIPCION/COMENTARIO            |VALOR");
        }

        //Metodo para agregar los totales d ela venta
        public void AgregarTotales(string texto, decimal total)
        {
            //Variables que usaremos
            string resumen, valor, textoCompleto, espacios = "";

            if (texto.Length > 29)//Si es mayor a 25 lo cortamos
            {
                cortar = texto.Length - 29;
                resumen = texto.Remove(29, cortar);
            }
            else
            { resumen = texto; }

            textoCompleto = resumen;
            valor = total.ToString("#,#.00");//Agregamos el total previo formateo.

            //Obtenemos el numero de espacios restantes para alinearlos a la derecha
            int nroEspacios = maxCar - (resumen.Length + valor.Length);
            //agregamos los espacios
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }
            textoCompleto += espacios + valor;
            linea.AppendLine(textoCompleto);
        }

        //Metodo para agreagar articulos al ticket de venta
        public void AgregaArticulo(string articulo, double cant, decimal itbis, decimal precio, decimal descuento)
        {
            if (cant.ToString().Length < 7 && precio.ToString().Length < 11)
            {
                string elemento, espacios = "";
                int nroEspacios = 0;
                decimal importe = 0;
                //Colocar cant y precio
                elemento = cant.ToString() + " " + "X" + " " + precio.ToString();


                //Colocar el ITBIS a la derecha.
                nroEspacios = (26 - elemento.Length);
                espacios = "";
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                elemento += espacios + itbis.ToString();
                //Colocar el precio total.
                importe = Convert.ToDecimal(cant) * (precio + itbis - descuento);
                nroEspacios = (34 - elemento.Length);
                espacios = "";
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                elemento += espacios + importe.ToString();

                linea.AppendLine(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
                linea.AppendLine(articulo);

            }
            else
            {
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportdas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
            }
        }
        //Metodo para agreagar articulos al ticket de orden de compra

        public void AgregaArticuloOrdenCompra(string articulo, double cant, decimal precio)
        {
            if (cant.ToString().Length < 7 && precio.ToString().Length < 11)
            {
                string elemento, espacios = "";
                int nroEspacios = 0;
                decimal importe = 0;
                //Colocar cant y precio
                elemento = cant.ToString() + " " + "X" + " " + precio.ToString();


                
                //Colocar el precio total.
                importe = Convert.ToDecimal(cant) * (precio);
                nroEspacios = (34 - elemento.Length);
                espacios = "";
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                elemento += espacios + importe.ToString();

                linea.AppendLine(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
                linea.AppendLine(articulo);

            }
            else
            {
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportdas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
            }
        }
        //Metodo para agreagar articulos al ticket de nota de credito
        public void AgregaArticuloNC(string articulo, double cant, decimal? precio, string comentario)
        {
            if (cant.ToString().Length < 7 && precio.ToString().Length < 11)
            {
                string elemento, espacios = "";
                int nroEspacios = 0;
                decimal importe = 0;
                //Colocar cant y precio
                elemento = cant.ToString() + " " + "X" + " " + precio.ToString();

                //Colocar el precio total.
                importe = Convert.ToDecimal(cant) * Convert.ToDecimal(precio);
                nroEspacios = (34 - elemento.Length);
                espacios = "";
                for (int i = 0; i < nroEspacios; i++)
                {
                    espacios += " ";
                }
                elemento += espacios + importe.ToString();

                linea.AppendLine(elemento);//Agregamos todo el elemento: nombre del articulo, comentario,cant, precio, importe.
                linea.AppendLine(articulo);
                linea.AppendLine(comentario);


            }
            else
            {                
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportdas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
            }
        }

        //Metodos para enviar secuencias de escape a la impresora
        //Para cortar el ticket
        public void CortaTicket()
        {
            linea.AppendLine("\x1B" + "m"); //Caracteres de corte. Estos comando varian segun el tipo de impresora
            linea.AppendLine("\x1B" + "d" + "\x00"); //Avanza 9 renglones, Tambien varian
        }

        //Para mandara a imprimir el texto a la impresora que le indiquemos.
        public void ImprimirTicket()
        {
            try
            {
                string impresora = Properties.Settings.Default.Impresora;
                //Usaremos un código que nos proporciona Microsoft. https://support.microsoft.com/es-es/kb/322091

                RawPrinterHelper.SendStringToPrinter(impresora, linea.ToString()); //Imprime texto.
                linea.Clear();  //linea.Clear();//Al cabar de imprimir limpia la linea de todo el texto agregado.
            }
            catch (Exception exc)
            {
                Loggeator.EscribeEnArchivo(exc.ToString());
            }
        }
    }
}
