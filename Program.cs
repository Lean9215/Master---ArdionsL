using ConsoleApp1.Modelos;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        var listaUsuarios = new List<Usuario>();
        var listaProductos = new List<Producto>();
        var listaVentas = new List<Venta>();
        var listaProductoVenta = new List<ProductoVenta>();

        SqlConnectionStringBuilder connectionBuilder = new();
        connectionBuilder.DataSource = "WKS43";
        connectionBuilder.InitialCatalog ="SistemaGestion";
        connectionBuilder.IntegratedSecurity = true;
        var cs = connectionBuilder.ConnectionString;

        //USUARIOS

        using (SqlConnection connection = new SqlConnection(cs))
        {
            Console.WriteLine("1 - TRAER USUARIO");
            Console.WriteLine("2 - TRAER PRODUCTO");
            Console.WriteLine("3 - TRAER PRODUCTO VENDIDOS");
            Console.WriteLine("4 - TRAER VENTAS");
            Console.WriteLine("5 - INICIO SESION");

            Console.WriteLine("INGRESE LA OPCION");
            var opcion = Console.ReadLine();
            while (opcion != null)
            {
                switch (opcion)
                {
                    case "1":
                        connection.Open();

                        SqlCommand cmd = connection.CreateCommand();
                        Console.WriteLine("Ingrese el Nombre de Usuario que desea buscar");
                        string nombre = Console.ReadLine();
                        cmd.CommandText = $"Select * from usuario where NombreUsuario = @userName ";

                        cmd.Parameters.Add("@userName", SqlDbType.VarChar);
                        cmd.Parameters["@userName"].Value = nombre;

                        var readerUsuario = cmd.ExecuteReader();

                        while (readerUsuario.Read())
                        {
                            var usu = new Usuario();

                            usu.Id = Convert.ToInt32(readerUsuario.GetValue(0));
                            usu.Nombre = readerUsuario.GetValue(1).ToString();
                            usu.Apellido = readerUsuario.GetValue(2).ToString();
                            usu.NombreUsuario = readerUsuario.GetValue(3).ToString();
                            usu.Contraseña = readerUsuario.GetValue(4).ToString();
                            usu.Mail = readerUsuario.GetValue(5).ToString();

                            listaUsuarios.Add(usu);
                        }
                        Console.WriteLine("---------Usuario------------");
                        foreach (var usu in listaUsuarios)
                        {
                            Console.WriteLine("Id = " + usu.Id);
                            Console.WriteLine("Nombre = " + usu.Nombre);
                            Console.WriteLine("Apellido = " + usu.Apellido);
                            Console.WriteLine("NombreUsuario = " + usu.NombreUsuario);
                            Console.WriteLine("Contraseña = " + usu.Contraseña);
                            Console.WriteLine("Mail = " + usu.Mail);

                        }
                        readerUsuario.Close();
                        connection.Close();
                        break;

                    case "2":
                        connection.Open();

                        SqlCommand cmdProd = connection.CreateCommand();
                        Console.WriteLine("Ingrese el Id del Usuario que desea buscar");
                        int idUsuario = Convert.ToInt32(Console.ReadLine());
                        cmdProd.CommandText = $"Select * from Producto where IdUsuario = @idUsuario ";

                        cmdProd.Parameters.Add("@idUsuario", SqlDbType.BigInt);
                        cmdProd.Parameters["@idUsuario"].Value = idUsuario;

                        var readerProducto = cmdProd.ExecuteReader();

                        while (readerProducto.Read())
                        {
                            var produc = new Producto();

                            produc.Id = Convert.ToInt32(readerProducto.GetValue(0));
                            produc.Descripciones = readerProducto.GetValue(1).ToString();
                            produc.Costo = Convert.ToDouble(readerProducto.GetValue(2));
                            produc.PrecioVenta = Convert.ToDouble(readerProducto.GetValue(3));
                            produc.Stock = Convert.ToInt32(readerProducto.GetValue(4));
                            produc.IdUsuario = Convert.ToInt32(readerProducto.GetValue(5));

                            listaProductos.Add(produc);
                        }
                        Console.WriteLine("---------Producto------------");
                        foreach (var produc in listaProductos)
                        {
                            Console.WriteLine("Id = " + produc.Id);
                            Console.WriteLine("Descripciones = " + produc.Descripciones);
                            Console.WriteLine("Costo = " + produc.Costo);
                            Console.WriteLine("PrecioVenta = " + produc.PrecioVenta);
                            Console.WriteLine("Stock = " + produc.Stock);
                            Console.WriteLine("IdUsuario = " + produc.IdUsuario);
                        }
                        readerProducto.Close();
                        connection.Close();
                        break;

                    case "3":

                        connection.Open();

                        SqlCommand cmdProdVend = connection.CreateCommand();
                        Console.WriteLine("Ingrese el Id del Usuario de Venta del producto Vendido que desea buscar");
                        int idUsuarioVenProd = Convert.ToInt32(Console.ReadLine());
                        cmdProdVend.CommandText = $"SELECT T0.* FROM Producto T0  LEFT JOIN productovendido T1 ON T1.IdProducto = T0.Id  WHERE T0.IdUsuario = @idUsuarioVenProd";

                        cmdProdVend.Parameters.Add("@idUsuarioVen", SqlDbType.BigInt);
                        cmdProdVend.Parameters["@idUsuarioVen"].Value = idUsuarioVenProd;

                        var readerProdVenta = cmdProdVend.ExecuteReader();

                        while (readerProdVenta.Read())
                        {
                            var producVend = new Producto();

                            producVend.Id = Convert.ToInt32(readerProdVenta.GetValue(0));
                            producVend.Descripciones = readerProdVenta.GetValue(1).ToString();
                            producVend.Costo = Convert.ToDouble(readerProdVenta.GetValue(2));
                            producVend.PrecioVenta = Convert.ToDouble(readerProdVenta.GetValue(3));
                            producVend.Stock = Convert.ToInt32(readerProdVenta.GetValue(4));
                            producVend.IdUsuario = Convert.ToInt32(readerProdVenta.GetValue(5));

                            listaProductos.Add(producVend);
                        }
                        Console.WriteLine("---------Producto Vendido------------");
                        foreach (var producVend in listaProductos)
                        {
                            Console.WriteLine("Id = " + producVend.Id);
                            Console.WriteLine("Descripciones = " + producVend.Descripciones);
                            Console.WriteLine("Costo = " + producVend.Costo);
                            Console.WriteLine("PrecioVenta = " + producVend.PrecioVenta);
                            Console.WriteLine("Stock = " + producVend.Stock);
                            Console.WriteLine("IdUsuario = " + producVend.IdUsuario);
                        }
                        readerProdVenta.Close();
                        connection.Close();
                        break;

                    case "4":

                        connection.Open();

                        SqlCommand cmdVenUsu = connection.CreateCommand();
                        Console.WriteLine("Ingrese el Id del Usuario de Venta que desea buscar");
                        int idUsuarioVen = Convert.ToInt32(Console.ReadLine());
                        cmdVenUsu.CommandText = $"SELECT * FROM Venta WHERE IdUsuario = @idUsuarioVen";

                        cmdVenUsu.Parameters.Add("@idUsuarioVen", SqlDbType.BigInt);
                        cmdVenUsu.Parameters["@idUsuarioVen"].Value = idUsuarioVen;

                        var readerVenUsu= cmdVenUsu.ExecuteReader();

                        while (readerVenUsu.Read())
                        {
                            var venUsu = new Venta();

                            venUsu.Id = Convert.ToInt32(readerVenUsu.GetValue(0));
                            venUsu.Comentarios = readerVenUsu.GetValue(1).ToString();
                            venUsu.IdUsuario = Convert.ToInt32(readerVenUsu.GetValue(2));

                            listaVentas.Add(venUsu);
                        }
                        Console.WriteLine("---------Producto Vendido------------");
                        foreach (var venUsu in listaVentas)
                        {
                            Console.WriteLine("Id = " + venUsu.Id);
                            Console.WriteLine("Comentarios = " + venUsu.Comentarios);
                            Console.WriteLine("IdUsuario = " + venUsu.IdUsuario);
                        }
                        readerVenUsu.Close();
                        connection.Close();
                        break;

                        case "5":
                        connection.Open();
                        SqlCommand cmdIniSesion = connection.CreateCommand();
                        SqlCommand cmdUser = connection.CreateCommand();
                        SqlCommand cmdPass = connection.CreateCommand();
                        cmdUser.CommandText = "SELECT NombreUsuario FROM Usuario";
                        var listavalidausuario = new List<string>();
                        var listavalidapass = new List<string>();
                        var validauser = cmdUser.ExecuteReader();
                        
                        while (validauser.Read())
                        {
                            for (int i = 0; i < validauser.FieldCount; i++) {

                                listavalidausuario.Add(Convert.ToString(validauser.GetValue(i)));
                            }
                        }
                        validauser.Close();

                        cmdPass.CommandText = "SELECT Contraseña FROM Usuario";
                        var validaPass = cmdPass.ExecuteReader();
                        while (validaPass.Read())
                        {
                            for (int i = 0; i < validaPass.FieldCount; i++)
                            {

                                listavalidapass.Add(Convert.ToString(validaPass.GetValue(i)));
                            }
                        }
                        validaPass.Close();

                        bool contieneUser = false;
                        bool contienePass = false;
                        
                        do
                        {

                            Console.WriteLine("Ingrese Usuario");
                            string usuario = Console.ReadLine();
                            Console.WriteLine("Ingrese Contraseña");
                            string pass = Console.ReadLine();
                            cmdIniSesion.CommandText = $"SELECT * FROM Usuario WHERE NombreUsuario = @usuario and Contraeña = @pass";

                            cmdIniSesion.Parameters.Add("@usuario", SqlDbType.VarChar);
                            cmdIniSesion.Parameters["@usuario"].Value = usuario;

                            cmdIniSesion.Parameters.Add("@pass", SqlDbType.VarChar);
                            cmdIniSesion.Parameters["@pass"].Value = pass;

                            contieneUser = listavalidausuario.Contains(usuario);
                            contienePass = listavalidapass.Contains(pass);
                           
                        }

                        while (contieneUser != true && contienePass != true);
                        
                        Console.WriteLine("Felicitaciones");
                        connection.Close();
                        break;


                }
            }
         }

    }
}