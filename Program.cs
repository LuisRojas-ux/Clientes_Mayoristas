using System;

namespace Proyecto2
{
    class Program
    {
        private static Nodo<Cliente>? listaClientes = null;

        static void Main(string[] args)
        {
            PrecargarClientes();

            int op = 0;
            Console.WriteLine("Bienvenido a LJP Enterprises");

            while (op != 7)// que fino es esto chamo :)
            {
                Console.WriteLine("\nPor favor Seleccione una operacion:");
                Console.WriteLine("1. Gestion De Clientes mayoristas");
                Console.WriteLine("2. Gestion De Productos");
                Console.WriteLine("3. Gestion De Pedidos");
                Console.WriteLine("7. Salir");
                Console.Write("Ingrese su opción: ");

                try
                {
                    op = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Por favor ingrese un número válido.");
                    continue;
                }

                switch (op)
                {
                    case 1:
                        GestionClientes();
                        break;
                    case 2:
                        Gestion_productos();
                        break;
                    case 3:
                        // Gestion de pedidos
                        break;
                    case 7:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor seleccione 1-3 o 7 para salir.");
                        break;
                }
            }
        }

        static void PrecargarClientes()
        {
            AgregarCliente(new Cliente("AB123456", "Ana", "García", "4123456789", "ana.garcia@email.com"));
            AgregarCliente(new Cliente("CD789012", "Luis", "Pérez", "4249876543", "luis.perez@email.com"));
            AgregarCliente(new Cliente("EF345678", "María", "Rodríguez", "4161122334", "maria.r@email.com"));
            AgregarCliente(new Cliente("GH901234", "Pedro", "González", "4145566778", "pedro.g@email.com"));
            AgregarCliente(new Cliente("IJ567890", "Sofía", "Hernández", "4269988776", "sofia.h@email.com"));
            AgregarCliente(new Cliente("KL123456", "Diego", "Sánchez", "4128877665", "diego.s@email.com"));
            AgregarCliente(new Cliente("MN789012", "Valeria", "Torres", "4243344556", "valeria.t@email.com"));
            AgregarCliente(new Cliente("OP345678", "Ricardo", "Castro", "4167788990", "ricardo.c@email.com"));
            AgregarCliente(new Cliente("BW606060", "Bruce", "Wayne", "4129893479", "yosoybatman.c@email.com"));
        }

        static void GestionClientes()
        {
            int op = -1;
            while (op != 0)
            {
                Console.WriteLine("\n--- GESTIÓN DE CLIENTES MAYORISTAS ---");
                Console.WriteLine("1. Agregar nuevo cliente");
                Console.WriteLine("2. Modificar cliente existente");
                Console.WriteLine("3. Eliminar cliente");
                Console.WriteLine("4. Mostrar todos los clientes");
                Console.WriteLine("0. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                try
                {
                    op = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Por favor ingrese un número válido.");
                    continue;
                }

                switch (op)
                {
                    case 1:
                        AgregarNuevoCliente();
                        break;
                    case 2:
                        ModificarCliente();
                        break;
                    case 3:
                        EliminarCliente();
                        break;
                    case 4:
                        MostrarClientes();
                        break;
                    case 0:
                        Console.WriteLine("Volviendo al menú principal...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void AgregarNuevoCliente()
        {
            Console.WriteLine("\n--- AGREGAR NUEVO CLIENTE ---");

            string idCliente;
            while (true)
            {
                Console.Write("ID Cliente (2 letras mayúsculas + 6 números, ej. AB123456): ");
                idCliente = Console.ReadLine()?.Trim().ToUpper() ?? "";

                if (!ValidarFormatoID(idCliente))
                {
                    Console.WriteLine("Formato de ID inválido. Debe ser exactamente 2 letras mayúsculas seguidas de 6 números.");
                    continue;
                }

                if (ExisteID(idCliente))
                {
                    Console.WriteLine("ERROR: Ya existe un cliente con este ID. Por favor ingrese un ID único.");
                    continue;
                }

                break;
            }

            string nombre;
            while (true)
            {
                Console.Write("Nombre: ");
                nombre = Console.ReadLine() ?? "";

                if (!ValidarTextoNoVacio(nombre))
                {
                    continue;
                }
                break;
            }

            string apellido;
            while (true)
            {
                Console.Write("Apellido: ");
                apellido = Console.ReadLine() ?? "";

                if (!ValidarTextoNoVacio(apellido))
                {
                    continue;
                }

                if (ExisteNombreApellido(nombre, apellido))
                {
                    Console.WriteLine("Ya existe un cliente con este nombre y apellido.");
                    continue;
                }
                break;
            }

            string telefono;
            while (true)
            {
                Console.Write("Teléfono (10 dígitos comenzando con 4): ");
                telefono = Console.ReadLine() ?? "";

                if (!ValidarTelefono(telefono))
                {
                    continue;
                }

                if (ExisteTelefono(telefono))
                {
                    Console.WriteLine("Este teléfono ya está registrado con otro cliente.");
                    continue;
                }
                break;
            }

            string email;
            while (true)
            {
                Console.Write("Email (formato: usuario@email.com): ");
                email = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (!ValidarEmail(email))
                {
                    continue;
                }

                if (ExisteEmail(email))
                {
                    Console.WriteLine("Este email ya está registrado con otro cliente.");
                    continue;
                }
                break;
            }

            Cliente nuevoCliente = new Cliente(idCliente, nombre, apellido, telefono, email);
            AgregarCliente(nuevoCliente);

            Console.WriteLine("\nCliente agregado exitosamente:");
            Console.WriteLine(nuevoCliente);
        }

        static void ModificarCliente()
        {
            Console.WriteLine("\n--- MODIFICAR CLIENTE ---");

            if (listaClientes == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            Console.Write("Ingrese el ID del cliente a modificar: ");
            string idOriginal = Console.ReadLine()?.ToUpper() ?? "";

            Nodo<Cliente>? nodoCliente = BuscarClientePorID(idOriginal);
            if (nodoCliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            Cliente cliente = nodoCliente.info;
            Console.WriteLine("\nDatos actuales del cliente:");
            Console.WriteLine(cliente);

            Console.WriteLine("\n¿Desea modificar el ID del cliente? (s/n)");
            if (Console.ReadLine()?.ToLower() == "s")
            {
                string nuevoID;
                while (true)
                {
                    Console.Write($"Nuevo ID ({cliente.ID_Cliente}): ");
                    nuevoID = Console.ReadLine()?.ToUpper() ?? "";

                    if (string.IsNullOrWhiteSpace(nuevoID))
                    {
                        break;
                    }

                    if (!ValidarFormatoID(nuevoID))
                    {
                        Console.WriteLine("Formato de ID inválido. Debe ser 2 letras mayúsculas seguidas de 6 números.");
                        continue;
                    }

                    if (ExisteID(nuevoID) && nuevoID != idOriginal)
                    {
                        Console.WriteLine("ERROR: Este ID ya está en uso por otro cliente.");
                        continue;
                    }

                    cliente.ID_Cliente = nuevoID;
                    break;
                }
            }

            Console.Write($"Nombre ({cliente.Nombre}): ");
            string nuevoNombre = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
            {
                while (!ValidarTextoNoVacio(nuevoNombre))
                {
                    Console.Write($"Nombre ({cliente.Nombre}): ");
                    nuevoNombre = Console.ReadLine() ?? "";
                }
                cliente.Nombre = nuevoNombre;
            }

            Console.Write($"Apellido ({cliente.Apellido}): ");
            string nuevoApellido = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(nuevoApellido))
            {
                while (!ValidarTextoNoVacio(nuevoApellido))
                {
                    Console.Write($"Apellido ({cliente.Apellido}): ");
                    nuevoApellido = Console.ReadLine() ?? "";
                }

                if (ExisteNombreApellido(cliente.Nombre, nuevoApellido) &&
                    !nuevoApellido.Equals(cliente.Apellido, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Ya existe un cliente con este nombre y apellido.");
                }
                else
                {
                    cliente.Apellido = nuevoApellido;
                }
            }

            Console.Write($"Teléfono ({cliente.Telefono}): ");
            string nuevoTelefono = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(nuevoTelefono))
            {
                while (true)
                {
                    if (!ValidarTelefono(nuevoTelefono))
                    {
                        Console.Write($"Teléfono ({cliente.Telefono}): ");
                        nuevoTelefono = Console.ReadLine() ?? "";
                        continue;
                    }

                    if (ExisteTelefono(nuevoTelefono) && nuevoTelefono != cliente.Telefono)
                    {
                        Console.WriteLine("Este teléfono ya está registrado con otro cliente.");
                        Console.Write($"Teléfono ({cliente.Telefono}): ");
                        nuevoTelefono = Console.ReadLine() ?? "";
                        continue;
                    }

                    cliente.Telefono = nuevoTelefono;
                    break;
                }
            }

            Console.Write($"Email ({cliente.Email}): ");
            string nuevoEmail = Console.ReadLine()?.Trim().ToLower() ?? "";
            if (!string.IsNullOrWhiteSpace(nuevoEmail))
            {
                while (true)
                {
                    if (!ValidarEmail(nuevoEmail))
                    {
                        Console.Write($"Email ({cliente.Email}): ");
                        nuevoEmail = Console.ReadLine()?.Trim().ToLower() ?? "";
                        continue;
                    }

                    if (ExisteEmail(nuevoEmail) && !nuevoEmail.Equals(cliente.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Este email ya está registrado con otro cliente.");
                        Console.Write($"Email ({cliente.Email}): ");
                        nuevoEmail = Console.ReadLine()?.Trim().ToLower() ?? "";
                        continue;
                    }

                    cliente.Email = nuevoEmail;
                    break;
                }
            }

            Console.WriteLine("\nCliente modificado exitosamente:");
            Console.WriteLine(cliente);
        }

        static void EliminarCliente()
        {
            Console.WriteLine("\n--- ELIMINAR CLIENTE ---");

            if (listaClientes == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            Console.Write("Ingrese el ID del cliente a eliminar: ");
            string id = Console.ReadLine()?.ToUpper() ?? "";

            if (listaClientes.info.ID_Cliente == id)
            {
                listaClientes = listaClientes.Sig;
                Console.WriteLine("Cliente eliminado exitosamente.");
                return;
            }

            Nodo<Cliente>? actual = listaClientes;
            while (actual?.Sig != null)
            {
                if (actual.Sig.info.ID_Cliente == id)
                {
                    actual.Sig = actual.Sig.Sig;
                    Console.WriteLine("Cliente eliminado exitosamente.");
                    return;
                }
                actual = actual.Sig;
            }

            Console.WriteLine("Cliente no encontrado.");
        }

        static void MostrarClientes()
        {
            Console.WriteLine("\n--- LISTA DE CLIENTES ---");

            if (listaClientes == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            Nodo<Cliente>? actual = listaClientes;
            while (actual != null)
            {
                Console.WriteLine(actual.info);
                Console.WriteLine("----------------------");
                actual = actual.Sig;
            }
        }

        static void AgregarCliente(Cliente cliente)
        {
            Nodo<Cliente> nuevoNodo = new Nodo<Cliente>(cliente);

            if (listaClientes == null)
            {
                listaClientes = nuevoNodo;
            }
            else
            {
                Nodo<Cliente>? actual = listaClientes;
                while (actual.Sig != null)
                {
                    actual = actual.Sig;
                }
                actual.Sig = nuevoNodo;
            }
        }

        static bool ValidarFormatoID(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Length != 8)
                return false;

            for (int i = 0; i < 2; i++)
            {
                if (!char.IsLetter(id[i]) || !char.IsUpper(id[i]))
                    return false;
            }

            for (int i = 2; i < 8; i++)
            {
                if (!char.IsDigit(id[i]))
                    return false;
            }

            return true;
        }

        static bool ValidarTelefono(string telefono)
        {
            if (telefono.Length != 10)
            {
                Console.WriteLine("El teléfono debe tener exactamente 10 dígitos.");
                return false;
            }

            if (telefono[0] != '4')
            {
                Console.WriteLine("El número de teléfono debe comenzar con 4.");
                return false;
            }

            foreach (char c in telefono)
            {
                if (!char.IsDigit(c))
                {
                    Console.WriteLine("El teléfono solo puede contener dígitos numéricos.");
                    return false;
                }
            }

            return true;
        }

        static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("El email no puede estar vacío.");
                return false;
            }

            int arrobaCount = 0;
            foreach (char c in email)
            {
                if (c == '@') arrobaCount++;
            }

            if (arrobaCount != 1)
            {
                Console.WriteLine("El email debe contener exactamente un símbolo @.");
                return false;
            }

            string[] partes = email.Split('@');
            string parteLocal = partes[0];
            string dominio = partes[1];

            if (string.IsNullOrWhiteSpace(parteLocal))
            {
                Console.WriteLine("El email debe tener contenido antes del @.");
                return false;
            }

            if (!dominio.Equals("email.com", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("El dominio del email debe ser @email.com");
                return false;
            }

            return true;
        }

        static bool ValidarTextoNoVacio(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                Console.WriteLine("Este campo no puede estar vacío.");
                return false;
            }

            foreach (char c in texto)
            {
                if (!char.IsWhiteSpace(c))
                {
                    return true;
                }
            }

            Console.WriteLine("Debe contener al menos un carácter que no sea espacio.");
            return false;
        }

        static bool ExisteID(string id)
        {
            Nodo<Cliente>? actual = listaClientes;
            while (actual != null)
            {
                if (actual.info.ID_Cliente.Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                actual = actual.Sig;
            }
            return false;
        }

        static bool ExisteNombreApellido(string nombre, string apellido)
        {
            Nodo<Cliente>? actual = listaClientes;
            while (actual != null)
            {
                if (actual.info.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                    actual.info.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                actual = actual.Sig;
            }
            return false;
        }

        static bool ExisteTelefono(string telefono)
        {
            Nodo<Cliente>? actual = listaClientes;
            while (actual != null)
            {
                if (actual.info.Telefono == telefono)
                {
                    return true;
                }
                actual = actual.Sig;
            }
            return false;
        }

        static bool ExisteEmail(string email)
        {
            Nodo<Cliente>? actual = listaClientes;
            while (actual != null)
            {
                if (actual.info.Email.Equals(email, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                actual = actual.Sig;
            }
            return false;
        }

        static Nodo<Cliente>? BuscarClientePorID(string id)
        {
            Nodo<Cliente>? actual = listaClientes;
            while (actual != null)
            {
                if (actual.info.ID_Cliente.Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return actual;
                }
                actual = actual.Sig;
            }
            return null;
        }

        static void Gestion_productos()
        {
            int op = -1;
            while (op != 0)
            {
                Console.WriteLine("\n--- GESTIÓN DE PRODUCTOS ---");
                Console.WriteLine("1. Agregar Producto");
                Console.WriteLine("2. Modificar producto");
                Console.WriteLine("3. Eliminar Producto");
                Console.WriteLine("0. Volver al menu principal");
                Console.Write("Seleccione una opción: ");

                try
                {
                    op = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Por favor ingrese un número válido.");
                    continue;
                }

                switch (op)
                {
                    case 1:
                        // Agregar producto
                        break;
                    case 2:
                        // Modificar producto
                        break;
                    case 3:
                        // Eliminar producto
                        break;
                    case 0:
                        Console.WriteLine("Volviendo al menu principal");
                        break;
                    default:
                        Console.WriteLine("Opcion no aceptada");
                        break;
                }
            }
        }
    }

    public class Cliente
    {
        public string ID_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public Cliente(string id, string nombre, string apellido, string telefono, string email)
        {
            ID_Cliente = id;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Email = email;
        }

        public override string ToString()
        {
            return $"ID: {ID_Cliente}\nNombre: {Nombre} {Apellido}\nTeléfono: {Telefono}\nEmail: {Email}";
        }
    }

    public class Producto
    {
        public int ID { get; set; }
        public int Stock { get; set; }
        public int Precio { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public Producto(int Id, int stock, int precio, string nombre)
        {
            ID = Id;
            Stock = stock;
            Precio = precio;
            Nombre = nombre;
        }
    }

    public class Nodo<T>
    {
        public T info { get; set; }
        public Nodo<T>? Sig { get; set; }
        public Nodo(T info) { this.info = info; Sig = null; }
    }
}