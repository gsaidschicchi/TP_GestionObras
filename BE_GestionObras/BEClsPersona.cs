namespace BE_GestionObras
{
    public abstract class BEClsPersona
    {
        // PROPIEDADES
        // IdCodigo pasa a ser el identificador de Persona. Se completa automáticamente desde la UI
        // de forma temporal hasta implementar persistencia en MPP/DAL/BD.
        public string IdCodigo { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public double SueldoBase { get; set; }

        // CONSTRUCTOR EXISTENTE - se mantiene para no romper el código actual.
        public BEClsPersona(string dni, string nombre, string apellido, string telefono)
        {
            IdCodigo = string.Empty;
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            SueldoBase = 0;
        }

        // CONSTRUCTOR SOBRECARGADO con los nuevos datos de Persona.
        public BEClsPersona(string idCodigo, string dni, string nombre, string apellido,
                            string telefono, double sueldoBase)
        {
            IdCodigo = idCodigo;
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            SueldoBase = sueldoBase;
        }

        public BEClsPersona()
        {
            IdCodigo = string.Empty;
            DNI = string.Empty;
            Nombre = string.Empty;
            Apellido = string.Empty;
            Telefono = string.Empty;
            SueldoBase = 0;
        }

        // METODO POLIMORFICO indicado en la corrección del profesor.
        public abstract double CalcularSueldo();
    }
}
